using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000070 RID: 112
	internal class JsonSchemaModel
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00012C7C File Offset: 0x00010E7C
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00012C84 File Offset: 0x00010E84
		public bool Required { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00012C8D File Offset: 0x00010E8D
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x00012C95 File Offset: 0x00010E95
		public JsonSchemaType Type { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00012C9E File Offset: 0x00010E9E
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00012CA6 File Offset: 0x00010EA6
		public int? MinimumLength { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00012CAF File Offset: 0x00010EAF
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x00012CB7 File Offset: 0x00010EB7
		public int? MaximumLength { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00012CC0 File Offset: 0x00010EC0
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x00012CC8 File Offset: 0x00010EC8
		public double? DivisibleBy { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00012CD1 File Offset: 0x00010ED1
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x00012CD9 File Offset: 0x00010ED9
		public double? Minimum { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00012CE2 File Offset: 0x00010EE2
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x00012CEA File Offset: 0x00010EEA
		public double? Maximum { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00012CF3 File Offset: 0x00010EF3
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x00012CFB File Offset: 0x00010EFB
		public bool ExclusiveMinimum { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00012D04 File Offset: 0x00010F04
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x00012D0C File Offset: 0x00010F0C
		public bool ExclusiveMaximum { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00012D15 File Offset: 0x00010F15
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x00012D1D File Offset: 0x00010F1D
		public int? MinimumItems { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00012D26 File Offset: 0x00010F26
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x00012D2E File Offset: 0x00010F2E
		public int? MaximumItems { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00012D37 File Offset: 0x00010F37
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00012D3F File Offset: 0x00010F3F
		public IList<string> Patterns { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00012D48 File Offset: 0x00010F48
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x00012D50 File Offset: 0x00010F50
		public IList<JsonSchemaModel> Items { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x00012D59 File Offset: 0x00010F59
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00012D61 File Offset: 0x00010F61
		public IDictionary<string, JsonSchemaModel> Properties { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00012D6A File Offset: 0x00010F6A
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x00012D72 File Offset: 0x00010F72
		public IDictionary<string, JsonSchemaModel> PatternProperties { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00012D7B File Offset: 0x00010F7B
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x00012D83 File Offset: 0x00010F83
		public JsonSchemaModel AdditionalProperties { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00012D8C File Offset: 0x00010F8C
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x00012D94 File Offset: 0x00010F94
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x00012D9D File Offset: 0x00010F9D
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x00012DA5 File Offset: 0x00010FA5
		public IList<JToken> Enum { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00012DAE File Offset: 0x00010FAE
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x00012DB6 File Offset: 0x00010FB6
		public JsonSchemaType Disallow { get; set; }

		// Token: 0x0600059E RID: 1438 RVA: 0x00012DBF File Offset: 0x00010FBF
		public JsonSchemaModel()
		{
			this.Type = JsonSchemaType.Any;
			this.AllowAdditionalProperties = true;
			this.Required = false;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00012DE0 File Offset: 0x00010FE0
		public static JsonSchemaModel Create(IList<JsonSchema> schemata)
		{
			JsonSchemaModel jsonSchemaModel = new JsonSchemaModel();
			foreach (JsonSchema jsonSchema in schemata)
			{
				JsonSchemaModel.Combine(jsonSchemaModel, jsonSchema);
			}
			return jsonSchemaModel;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00012E30 File Offset: 0x00011030
		private static void Combine(JsonSchemaModel model, JsonSchema schema)
		{
			model.Required = model.Required || (schema.Required ?? false);
			model.Type &= schema.Type ?? JsonSchemaType.Any;
			model.MinimumLength = MathUtils.Max(model.MinimumLength, schema.MinimumLength);
			model.MaximumLength = MathUtils.Min(model.MaximumLength, schema.MaximumLength);
			model.DivisibleBy = MathUtils.Max(model.DivisibleBy, schema.DivisibleBy);
			model.Minimum = MathUtils.Max(model.Minimum, schema.Minimum);
			model.Maximum = MathUtils.Max(model.Maximum, schema.Maximum);
			model.ExclusiveMinimum = model.ExclusiveMinimum || (schema.ExclusiveMinimum ?? false);
			model.ExclusiveMaximum = model.ExclusiveMaximum || (schema.ExclusiveMaximum ?? false);
			model.MinimumItems = MathUtils.Max(model.MinimumItems, schema.MinimumItems);
			model.MaximumItems = MathUtils.Min(model.MaximumItems, schema.MaximumItems);
			model.AllowAdditionalProperties = model.AllowAdditionalProperties && schema.AllowAdditionalProperties;
			if (schema.Enum != null)
			{
				if (model.Enum == null)
				{
					model.Enum = new List<JToken>();
				}
				model.Enum.AddRangeDistinct(schema.Enum, new JTokenEqualityComparer());
			}
			model.Disallow |= schema.Disallow ?? JsonSchemaType.None;
			if (schema.Pattern != null)
			{
				if (model.Patterns == null)
				{
					model.Patterns = new List<string>();
				}
				model.Patterns.AddDistinct(schema.Pattern);
			}
		}
	}
}
