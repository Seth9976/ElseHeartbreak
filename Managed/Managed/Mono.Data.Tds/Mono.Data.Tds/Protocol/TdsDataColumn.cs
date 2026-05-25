using System;
using System.Collections;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x02000018 RID: 24
	public class TdsDataColumn
	{
		// Token: 0x0600014B RID: 331 RVA: 0x0000D6E4 File Offset: 0x0000B8E4
		public TdsDataColumn()
		{
			this.IsAutoIncrement = new bool?(false);
			this.IsIdentity = new bool?(false);
			this.IsRowVersion = new bool?(false);
			this.IsUnique = new bool?(false);
			this.IsHidden = new bool?(false);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000D734 File Offset: 0x0000B934
		// (set) Token: 0x0600014D RID: 333 RVA: 0x0000D73C File Offset: 0x0000B93C
		public TdsColumnType? ColumnType { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000D748 File Offset: 0x0000B948
		// (set) Token: 0x0600014F RID: 335 RVA: 0x0000D750 File Offset: 0x0000B950
		public string ColumnName { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000D75C File Offset: 0x0000B95C
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000D764 File Offset: 0x0000B964
		public int? ColumnSize { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000D770 File Offset: 0x0000B970
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000D778 File Offset: 0x0000B978
		public int? ColumnOrdinal { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000D784 File Offset: 0x0000B984
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000D78C File Offset: 0x0000B98C
		public bool? IsAutoIncrement { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000D798 File Offset: 0x0000B998
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		public bool? IsIdentity { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000D7B4 File Offset: 0x0000B9B4
		public bool? IsRowVersion { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		// (set) Token: 0x0600015B RID: 347 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		public bool? IsUnique { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
		// (set) Token: 0x0600015D RID: 349 RVA: 0x0000D7DC File Offset: 0x0000B9DC
		public bool? IsHidden { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000D7E8 File Offset: 0x0000B9E8
		// (set) Token: 0x0600015F RID: 351 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		public bool? IsKey { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000D7FC File Offset: 0x0000B9FC
		// (set) Token: 0x06000161 RID: 353 RVA: 0x0000D804 File Offset: 0x0000BA04
		public bool? IsAliased { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000D810 File Offset: 0x0000BA10
		// (set) Token: 0x06000163 RID: 355 RVA: 0x0000D818 File Offset: 0x0000BA18
		public bool? IsExpression { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000D824 File Offset: 0x0000BA24
		// (set) Token: 0x06000165 RID: 357 RVA: 0x0000D82C File Offset: 0x0000BA2C
		public bool? IsReadOnly { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000D838 File Offset: 0x0000BA38
		// (set) Token: 0x06000167 RID: 359 RVA: 0x0000D840 File Offset: 0x0000BA40
		public short? NumericPrecision { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000D84C File Offset: 0x0000BA4C
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000D854 File Offset: 0x0000BA54
		public short? NumericScale { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000D860 File Offset: 0x0000BA60
		// (set) Token: 0x0600016B RID: 363 RVA: 0x0000D868 File Offset: 0x0000BA68
		public string BaseServerName { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000D874 File Offset: 0x0000BA74
		// (set) Token: 0x0600016D RID: 365 RVA: 0x0000D87C File Offset: 0x0000BA7C
		public string BaseCatalogName { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000D888 File Offset: 0x0000BA88
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000D890 File Offset: 0x0000BA90
		public string BaseColumnName { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000D89C File Offset: 0x0000BA9C
		// (set) Token: 0x06000171 RID: 369 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		public string BaseSchemaName { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000D8B8 File Offset: 0x0000BAB8
		public string BaseTableName { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000D8C4 File Offset: 0x0000BAC4
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000D8CC File Offset: 0x0000BACC
		public bool? AllowDBNull { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000D8E0 File Offset: 0x0000BAE0
		public int? LCID { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000D8F4 File Offset: 0x0000BAF4
		public int? SortOrder { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000D900 File Offset: 0x0000BB00
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000D908 File Offset: 0x0000BB08
		public string DataTypeName { get; set; }

		// Token: 0x1700004A RID: 74
		public object this[string key]
		{
			get
			{
				if (this.properties == null)
				{
					return null;
				}
				return this.properties[key];
			}
			set
			{
				if (this.properties == null)
				{
					this.properties = new Hashtable();
				}
				this.properties[key] = value;
			}
		}

		// Token: 0x040000D9 RID: 217
		private Hashtable properties;
	}
}
