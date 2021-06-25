namespace Akeneo.Search
{
	public class CompletenessCriteria : Criteria
	{
		public const string Key = "completeness";

		public string Scope { get; set; }

		public static CompletenessCriteria Greater(int completness, string channel, bool allLocales = false)
		{
			return new CompletenessCriteria
			{
				Operator = allLocales
					? Operators.GreaterOnAllLocales
					: Operators.Greater,
				Value = completness,
				Scope = channel
			};
		}

		public static CompletenessCriteria GreaterOrEqual(int completness, string channel, bool allLocales = false)
		{
			return new CompletenessCriteria
			{
				Operator = allLocales
					? Operators.GreaterOrEqualOnAllLocales
					: Operators.GreaterOrEqual,
				Value = completness,
				Scope = channel
			};
		}

		public static CompletenessCriteria Lower(int completness, string channel, bool allLocales = false)
		{
			return new CompletenessCriteria
			{
				Operator = allLocales
					? Operators.LowerOnAllLocales
					: Operators.Lower,
				Value = completness,
				Scope = channel
			};
		}

		public static CompletenessCriteria LowerOrEqual(int completness, string channel, bool allLocales = false)
		{
			return new CompletenessCriteria
			{
				Operator = allLocales
					? Operators.LowerOrEqualOnAllLocales
					: Operators.LowerOrEqual,
				Value = completness,
				Scope = channel
			};
		}

		public static CompletenessCriteria Equal(int completness, string channel)
		{
			return new CompletenessCriteria
			{
				Operator = Operators.Equal,
				Value = completness,
				Scope = channel
			};
		}

		public static CompletenessCriteria NotEqual(int completness, string channel)
		{
			return new CompletenessCriteria
			{
				Operator = Operators.NotEqual,
				Value = completness,
				Scope = channel
			};
		}
	}
}
