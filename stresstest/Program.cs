using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akeneo;
using Akeneo.Client;
using Akeneo.Model;
using Akeneo.Search;
using Newtonsoft.Json;
using NLog;

namespace stresstest
{
    class Program
    {
        static AkeneoClient clt;


        static void Main(string[] args)
        {
            var test = new AkeneoTest();
        }

        public class AkeneoTest
        {
            public AkeneoTest()
            {
                var config = new NLog.Config.LoggingConfiguration();

                // Targets where to log to: File and Console
                var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };
                var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

                // Rules for mapping loggers to targets            
                config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
                config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

                // Apply config           
                NLog.LogManager.Configuration = config;
                const string URL = "https://staging-schmidt-groupe.cloud.akeneo.com";
                const string CLIENT_ID = "4_5npl2sqyq3k0oc84cg0wc0cososks0oowo0wsgw8sw44gc0wsc";
                const string SECRET = "37educgyboqo0wsogswwsgk8gk0cswk8swkgcw48skoc0844w4";
                const string USERNAME = "bip_6005";
                const string PASSWORD = "065b9662d";

                clt = new AkeneoClient(new AkeneoOptions
                {
                    ApiEndpoint = new Uri(URL),
                    ClientId = CLIENT_ID,
                    ClientSecret = SECRET,
                    UserName = USERNAME,
                    Password = PASSWORD
                });

                //récupération familles
                var cats = GetData<Category>()
                    .GetAwaiter()
                    .GetResult()
                    .Where(c => c.Code.StartsWith("bip_", StringComparison.OrdinalIgnoreCase));


                var products = new Dictionary<string, List<Product>>(
                    cats.Where(c => c.Code.Split('_')[1].Length == 6)
                    .Select(c => new KeyValuePair<string, List<Product>>(c.Code, new List<Product>())));

                var example = clt.SearchAsync<Product>(new List<Criteria>
                {
                    CategoryCriteria.In(products.Keys.ElementAt(0)) ,
                    EnabledCriteria.Equal(true)
                })
                    .GetAwaiter().GetResult();
                var keys = products.Keys.ToList();

                var ppp = Parallel.ForEach(keys, key =>
                {
                    var a = clt.SearchAsync<Product>(
                        new List<Criteria>
                        {
                            CategoryCriteria.In(key),
                            EnabledCriteria.Equal(false)
                        })
                    .GetAwaiter().GetResult();
                    products[key] = a.Embedded.Items;
                });



                //    var subResults = clt.SearchAsync<Product>(new List<Criteria>
                //{
                //    CategoryCriteria.In(allCuisson)
                //}).GetAwaiter().GetResult();
                //clt.FilterAsync<Product>()

                Console.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));


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

                } while (response.Links.ContainsKey("next") && page < maxPage);

                Console.WriteLine($"found {result.Count} items of type {typeof(T).Name}");
                return result;
            }
        }
    }
}
