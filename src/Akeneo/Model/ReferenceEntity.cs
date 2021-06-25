using System;
using System.Collections.Generic;
using Akeneo.Client;
using Newtonsoft.Json;

namespace Akeneo.Model
{
	public class ReferenceEntity : ModelBase
	{
		/// <summary>
		/// Reference entity code (required)
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Reference entity labels for each locale
		/// </summary>
		public Dictionary<string, string> Labels { get; set; }

		/// <summary>
		/// Reference entity image Url
		/// </summary>
		public string Image { get; set; }

		[JsonProperty("_links")]
		public Dictionary<string, PaginationLink> Links { get; set; }


		public ReferenceEntity()
		{
			Labels = new Dictionary<string, string>();
		}
	}
}
