using System.Collections.Generic;

namespace Akeneo.Model
{
    public class ReferenceEntityRecord
    {
        /// <summary>
        /// Reference entity record code (required)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///  Reference entity values
        /// </summary>
        public Dictionary<string, List<ProductValue>> Values { get; set; }

        public ReferenceEntityRecord()
        {
            Values = new Dictionary<string, List<ProductValue>>();
        }
    }
}