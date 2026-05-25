using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000073 RID: 115
	internal class JsonSchemaNode
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001341A File Offset: 0x0001161A
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x00013422 File Offset: 0x00011622
		public string Id { get; private set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001342B File Offset: 0x0001162B
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x00013433 File Offset: 0x00011633
		public ReadOnlyCollection<JsonSchema> Schemas { get; private set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0001343C File Offset: 0x0001163C
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x00013444 File Offset: 0x00011644
		public Dictionary<string, JsonSchemaNode> Properties { get; private set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0001344D File Offset: 0x0001164D
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x00013455 File Offset: 0x00011655
		public Dictionary<string, JsonSchemaNode> PatternProperties { get; private set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0001345E File Offset: 0x0001165E
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x00013466 File Offset: 0x00011666
		public List<JsonSchemaNode> Items { get; private set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0001346F File Offset: 0x0001166F
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x00013477 File Offset: 0x00011677
		public JsonSchemaNode AdditionalProperties { get; set; }

		// Token: 0x060005B7 RID: 1463 RVA: 0x00013480 File Offset: 0x00011680
		public JsonSchemaNode(JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(new JsonSchema[] { schema });
			this.Properties = new Dictionary<string, JsonSchemaNode>();
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>();
			this.Items = new List<JsonSchemaNode>();
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000134DC File Offset: 0x000116DC
		private JsonSchemaNode(JsonSchemaNode source, JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(source.Schemas.Union(new JsonSchema[] { schema }).ToList<JsonSchema>());
			this.Properties = new Dictionary<string, JsonSchemaNode>(source.Properties);
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>(source.PatternProperties);
			this.Items = new List<JsonSchemaNode>(source.Items);
			this.AdditionalProperties = source.AdditionalProperties;
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00013566 File Offset: 0x00011766
		public JsonSchemaNode Combine(JsonSchema schema)
		{
			return new JsonSchemaNode(this, schema);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001357C File Offset: 0x0001177C
		public static string GetId(IEnumerable<JsonSchema> schemata)
		{
			return string.Join("-", schemata.Select((JsonSchema s) => s.InternalId).OrderBy((string id) => id, StringComparer.Ordinal).ToArray<string>());
		}
	}
}
