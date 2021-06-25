using System.Linq;

namespace Akeneo.Search
{
	public class FamilyCriteria : Criteria
	{
		public const string Key = "family";

		public static FamilyCriteria In(params string[] group)
		{
			return new FamilyCriteria
			{
				Operator = Operators.In,
				Value = group.ToList()
			};
		}

		public static FamilyCriteria NotIn(params string[] group)
		{
			return new FamilyCriteria
			{
				Operator = Operators.NotIn,
				Value = group.ToList()
			};
		}

		public static FamilyCriteria Empty()
		{
			return new FamilyCriteria
			{
				Operator = Operators.Equal,
			};
		}

		public static FamilyCriteria NotEmpty()
		{
			return new FamilyCriteria
			{
				Operator = Operators.Equal,
			};
		}
	}
}
