using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000074 RID: 116
	public class JsonSchemaResolver
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x000135E2 File Offset: 0x000117E2
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x000135EA File Offset: 0x000117EA
		public IList<JsonSchema> LoadedSchemas { get; protected set; }

		// Token: 0x060005BF RID: 1471 RVA: 0x000135F3 File Offset: 0x000117F3
		public JsonSchemaResolver()
		{
			this.LoadedSchemas = new List<JsonSchema>();
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00013624 File Offset: 0x00011824
		public virtual JsonSchema GetSchema(string id)
		{
			return this.LoadedSchemas.SingleOrDefault((JsonSchema s) => s.Id == id);
		}
	}
}
