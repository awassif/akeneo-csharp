namespace Akeneo.Search
{
	public class StatusCriteria : Criteria
	{
		public const string Key = "enabled";

		public static StatusCriteria Enabled()
		{
			return new StatusCriteria
			{
				Operator = Operators.Equal,
				Value = true
			};
		}

		public static StatusCriteria Disabled()
		{
			return new StatusCriteria
			{
				Operator = Operators.Equal,
				Value = false
			};
		}
	}
}
