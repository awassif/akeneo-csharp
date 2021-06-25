using System.Collections.Generic;
using System.Linq;

namespace Akeneo.Search
{
	public class CategoryCriteria
		: Criteria
	{
		public const string Key = "categories";

		private CategoryCriteria() { }

		public static CategoryCriteria In(params string[] categories)
		{
			return new CategoryCriteria
			{
				Operator = Operators.In,
				Value = categories.ToList()
			};
		}


		public static CategoryCriteria In(IEnumerable<string> categories)
		{
			return new CategoryCriteria
			{
				Operator = Operators.In,
				Value = categories.ToList()
			};
		}

		public static CategoryCriteria NotIn(params string[] categories)
		{
			return new CategoryCriteria
			{
				Operator = Operators.NotIn,
				Value = categories.ToList()
			};
		}
		public static CategoryCriteria NotIn(IEnumerable<string> categories)
		{
			return new CategoryCriteria
			{
				Operator = Operators.NotIn,
				Value = categories.ToList()
			};
		}

		public static CategoryCriteria InOrUnclassified(params string[] categories)
		{
			return new CategoryCriteria
			{
				Operator = Operators.InOrUnclassified,
				Value = categories.ToList()
			};
		}

		public static CategoryCriteria InChild(params string[] categories)
		{
			return new CategoryCriteria
			{
				Operator = Operators.InChildren,
				Value = categories.ToList()
			};
		}

		public static CategoryCriteria NotInChild(params string[] categories)
		{
			return new CategoryCriteria
			{
				Operator = Operators.NotInChildren,
				Value = categories.ToList()
			};
		}

		public static CategoryCriteria Unclassified()
		{
			return new CategoryCriteria
			{
				Operator = Operators.Unclassified
			};
		}
	}
}