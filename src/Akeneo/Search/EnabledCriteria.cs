using System.Collections.Generic;
using System.Linq;

namespace Akeneo.Search
{
    public class EnabledCriteria : Criteria
    {
        public const string Key = "enabled";

        private EnabledCriteria() { }

        public static EnabledCriteria Equal(bool enabled)
        {
            return new EnabledCriteria
            {
                Operator = Operators.Equal,
                Value = enabled
            };
        }

        public static EnabledCriteria NotEqual(bool enabled)
        {
            return new EnabledCriteria
            {
                Operator = Operators.NotEqual,
                Value = enabled
            };
        }
    }
}