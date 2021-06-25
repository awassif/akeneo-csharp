using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Akeneo;
using Akeneo.Client;
using Akeneo.Model;
using Akeneo.Model.Attributes;
using Akeneo.Search;
using Newtonsoft.Json;
//using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.OutputEncoding = System.Text.Encoding.Unicode;
            var test = new AkeneoTest();
        }
    }

    public class AkeneoTest
    {
        static AkeneoClient clt;

        const string URL = "https://staging-schmidt-groupe.cloud.akeneo.com";
        const string CLIENT_ID = "4_5npl2sqyq3k0oc84cg0wc0cososks0oowo0wsgw8sw44gc0wsc";
        const string SECRET = "37educgyboqo0wsogswwsgk8gk0cswk8swkgcw48skoc0844w4";
        const string USERNAME = "bip_6005";
        const string PASSWORD = "065b9662d";

        public AkeneoTest()
        {
            clt = new AkeneoClient(new AkeneoOptions
            {
                ApiEndpoint = new Uri(URL),
                ClientId = CLIENT_ID,
                ClientSecret = SECRET,
                UserName = USERNAME,
                Password = PASSWORD
            });

            var txts = new List<string>();

            //var result = GetItem<Product>("TR90IX91")
            //    .GetAwaiter()
            //    .GetResult();

            //txts.Add(JsonConvert.SerializeObject(result, Formatting.Indented));

            //var cats = this.GetData<Category>()
            //    .GetAwaiter()
            //    .GetResult()
            //    .Where(c => c.Code.StartsWith("bip", StringComparison.OrdinalIgnoreCase));

            //txts.Add(
            //    JsonConvert.SerializeObject(
            //        cats,
            //        Formatting.Indented));

            //string crit = "";
            //if (cats != null && cats.Count() > 0)
            //    crit = cats.ElementAt(3).Code;
            //else
            //    crit = "bip_0101";

            //var allCuisson = cats.Where(c => c.Parent == "bip_0101")
            //    .Select(c => c.Code);


            //var subResults = clt.SearchAsync<Product>(new List<Criteria>
            //{
            //    CategoryCriteria.In(allCuisson)
            //}).GetAwaiter().GetResult();

            //var rr = clt.SearchAsync<Product>()

            //txts.Add(JsonConvert.SerializeObject(subResults, Formatting.Indented));

            //var tt = clt.SearchAsync<Category>(new List<Criteria>
            //{
            //    CategoryCriteria.In("bip_*")
            //}).GetAwaiter().GetResult();

            //txts.Add(JsonConvert.SerializeObject(tt, Formatting.Indented));

            //txts.Add(
            //    JsonConvert.SerializeObject(
            //        this.GetData<MediaFile>().GetAwaiter().GetResult(),
            //        Formatting.Indented));

            txts.Add(
                JsonConvert.SerializeObject(
                    this.GetItem<ReferenceEntity>("brand").GetAwaiter().GetResult(),
                    Formatting.Indented));

            //txts.Add(
            //    JsonConvert.SerializeObject(
            //        this.GetData<ReferenceEntity>().GetAwaiter().GetResult(),
            //        Formatting.Indented));

            //txts.Add(
            //    JsonConvert.SerializeObject(
            //        this.GetData<Channel>().GetAwaiter().GetResult(),
            //        Formatting.Indented));

            //txts.Add(
            //    JsonConvert.SerializeObject(
            //        this.GetData<Locale>().GetAwaiter().GetResult(),
            //        Formatting.Indented));

            //txts.Add(
            //    JsonConvert.SerializeObject(
            //        this.GetData<MediaFile>().GetAwaiter().GetResult(),
            //        Formatting.Indented));

            //txts.Add(
            //    JsonConvert.SerializeObject(
            //        this.GetData<AttributeBase>().GetAwaiter().GetResult(),
            //        Formatting.Indented));



            //var f = GetItem<Family>("cooking_center").GetAwaiter().GetResult();

            //txt = JsonConvert.SerializeObject(f, Formatting.Indented);
            //Console.WriteLine(txt);
            var n = 1;
            foreach (var txt in txts)
            {
                File.WriteAllText($"file_{n:00}.txt", txt);
                //Console.WriteLine(txt);
                //Console.WriteLine("************************************************************************");
                n++;
            }
        }


        public Task<T> GetItem<T>(string code) where T : ModelBase
        {
            return clt.GetAsync<T>(code.ToUpper());
        }

        public async Task<List<T>> GetData<T>() where T : ModelBase
        {
            //liste d'items
            var result = new List<T>();
            //nombre d'items par page
            var limit = 30;
            //numéro de page
            var page = 1;
            //page maximum
            var maxPage = 200;

            PaginationResult<T> response;

            do
            {
                response = await clt.GetManyAsync<T>(page, limit, true);

                if (response.Embedded.Items.Count() > 0)
                    result.AddRange(response.Embedded.Items);
                page++;

            } while (response.Links.ContainsKey("next")  && page < maxPage);

             Console.WriteLine($"found {result.Count} items of type {typeof(T).Name}");
            return result;
        }
    }


}
