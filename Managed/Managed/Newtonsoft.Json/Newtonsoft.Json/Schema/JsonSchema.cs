using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000087 RID: 135
	public class JsonSchema
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x000159E8 File Offset: 0x00013BE8
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x000159F0 File Offset: 0x00013BF0
		public string Id { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x000159F9 File Offset: 0x00013BF9
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x00015A01 File Offset: 0x00013C01
		public string Title { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00015A0A File Offset: 0x00013C0A
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x00015A12 File Offset: 0x00013C12
		public bool? Required { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00015A1B File Offset: 0x00013C1B
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x00015A23 File Offset: 0x00013C23
		public bool? ReadOnly { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00015A2C File Offset: 0x00013C2C
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x00015A34 File Offset: 0x00013C34
		public bool? Hidden { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00015A3D File Offset: 0x00013C3D
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x00015A45 File Offset: 0x00013C45
		public bool? Transient { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00015A4E File Offset: 0x00013C4E
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x00015A56 File Offset: 0x00013C56
		public string Description { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00015A5F File Offset: 0x00013C5F
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x00015A67 File Offset: 0x00013C67
		public JsonSchemaType? Type { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00015A70 File Offset: 0x00013C70
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x00015A78 File Offset: 0x00013C78
		public string Pattern { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00015A81 File Offset: 0x00013C81
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x00015A89 File Offset: 0x00013C89
		public int? MinimumLength { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00015A92 File Offset: 0x00013C92
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x00015A9A File Offset: 0x00013C9A
		public int? MaximumLength { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00015AA3 File Offset: 0x00013CA3
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x00015AAB File Offset: 0x00013CAB
		public double? DivisibleBy { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00015AB4 File Offset: 0x00013CB4
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x00015ABC File Offset: 0x00013CBC
		public double? Minimum { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00015AC5 File Offset: 0x00013CC5
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x00015ACD File Offset: 0x00013CCD
		public double? Maximum { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00015AD6 File Offset: 0x00013CD6
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x00015ADE File Offset: 0x00013CDE
		public bool? ExclusiveMinimum { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00015AE7 File Offset: 0x00013CE7
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x00015AEF File Offset: 0x00013CEF
		public bool? ExclusiveMaximum { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00015AF8 File Offset: 0x00013CF8
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x00015B00 File Offset: 0x00013D00
		public int? MinimumItems { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00015B09 File Offset: 0x00013D09
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x00015B11 File Offset: 0x00013D11
		public int? MaximumItems { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00015B1A File Offset: 0x00013D1A
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x00015B22 File Offset: 0x00013D22
		public IList<JsonSchema> Items { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00015B2B File Offset: 0x00013D2B
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x00015B33 File Offset: 0x00013D33
		public IDictionary<string, JsonSchema> Properties { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00015B3C File Offset: 0x00013D3C
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x00015B44 File Offset: 0x00013D44
		public JsonSchema AdditionalProperties { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00015B4D File Offset: 0x00013D4D
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x00015B55 File Offset: 0x00013D55
		public IDictionary<string, JsonSchema> PatternProperties { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00015B5E File Offset: 0x00013D5E
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x00015B66 File Offset: 0x00013D66
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00015B6F File Offset: 0x00013D6F
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x00015B77 File Offset: 0x00013D77
		public string Requires { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00015B80 File Offset: 0x00013D80
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x00015B88 File Offset: 0x00013D88
		public IList<string> Identity { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00015B91 File Offset: 0x00013D91
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x00015B99 File Offset: 0x00013D99
		public IList<JToken> Enum { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00015BA2 File Offset: 0x00013DA2
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x00015BAA File Offset: 0x00013DAA
		public IDictionary<JToken, string> Options { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00015BB3 File Offset: 0x00013DB3
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x00015BBB File Offset: 0x00013DBB
		public JsonSchemaType? Disallow { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00015BC4 File Offset: 0x00013DC4
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x00015BCC File Offset: 0x00013DCC
		public JToken Default { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00015BD5 File Offset: 0x00013DD5
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x00015BDD File Offset: 0x00013DDD
		public JsonSchema Extends { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00015BE6 File Offset: 0x00013DE6
		// (set) Token: 0x06000693 RID: 1683 RVA: 0x00015BEE File Offset: 0x00013DEE
		public string Format { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00015BF7 File Offset: 0x00013DF7
		internal string InternalId
		{
			get
			{
				return this._internalId;
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00015C00 File Offset: 0x00013E00
		public JsonSchema()
		{
			this.AllowAdditionalProperties = true;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00015C32 File Offset: 0x00013E32
		public static JsonSchema Read(JsonReader reader)
		{
			return JsonSchema.Read(reader, new JsonSchemaResolver());
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00015C40 File Offset: 0x00013E40
		public static JsonSchema Read(JsonReader reader, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			JsonSchemaBuilder jsonSchemaBuilder = new JsonSchemaBuilder(resolver);
			return jsonSchemaBuilder.Parse(reader);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00015C71 File Offset: 0x00013E71
		public static JsonSchema Parse(string json)
		{
			return JsonSchema.Parse(json, new JsonSchemaResolver());
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00015C80 File Offset: 0x00013E80
		public static JsonSchema Parse(string json, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(json, "json");
			JsonReader jsonReader = new JsonTextReader(new StringReader(json));
			return JsonSchema.Read(jsonReader, resolver);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00015CAB File Offset: 0x00013EAB
		public void WriteTo(JsonWriter writer)
		{
			this.WriteTo(writer, new JsonSchemaResolver());
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00015CBC File Offset: 0x00013EBC
		public void WriteTo(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			JsonSchemaWriter jsonSchemaWriter = new JsonSchemaWriter(writer, resolver);
			jsonSchemaWriter.WriteSchema(this);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00015CF0 File Offset: 0x00013EF0
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			this.WriteTo(new JsonTextWriter(stringWriter)
			{
				Formatting = Formatting.Indented
			});
			return stringWriter.ToString();
		}

		// Token: 0x040001D1 RID: 465
		private readonly string _internalId = Guid.NewGuid().ToString("N");
	}
}
