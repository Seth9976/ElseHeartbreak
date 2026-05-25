using System;

namespace System.Data.Common
{
	// Token: 0x020000DB RID: 219
	internal class SchemaInfo
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0003141C File Offset: 0x0002F61C
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x00031424 File Offset: 0x0002F624
		public bool AllowDBNull
		{
			get
			{
				return this.allowDBNull;
			}
			set
			{
				this.allowDBNull = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00031430 File Offset: 0x0002F630
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x00031438 File Offset: 0x0002F638
		public string ColumnName
		{
			get
			{
				return this.columnName;
			}
			set
			{
				this.columnName = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x00031444 File Offset: 0x0002F644
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x0003144C File Offset: 0x0002F64C
		public int ColumnOrdinal
		{
			get
			{
				return this.ordinal;
			}
			set
			{
				this.ordinal = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00031458 File Offset: 0x0002F658
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x00031460 File Offset: 0x0002F660
		public int ColumnSize
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0003146C File Offset: 0x0002F66C
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x00031474 File Offset: 0x0002F674
		public string DataTypeName
		{
			get
			{
				return this.dataTypeName;
			}
			set
			{
				this.dataTypeName = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x00031480 File Offset: 0x0002F680
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x00031488 File Offset: 0x0002F688
		public Type FieldType
		{
			get
			{
				return this.fieldType;
			}
			set
			{
				this.fieldType = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00031494 File Offset: 0x0002F694
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x0003149C File Offset: 0x0002F69C
		public byte NumericPrecision
		{
			get
			{
				return this.precision;
			}
			set
			{
				this.precision = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x000314A8 File Offset: 0x0002F6A8
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x000314B0 File Offset: 0x0002F6B0
		public byte NumericScale
		{
			get
			{
				return this.scale;
			}
			set
			{
				this.scale = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x000314BC File Offset: 0x0002F6BC
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x000314C4 File Offset: 0x0002F6C4
		public string TableName
		{
			get
			{
				return this.tableName;
			}
			set
			{
				this.tableName = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x000314D0 File Offset: 0x0002F6D0
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x000314D8 File Offset: 0x0002F6D8
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		// Token: 0x040003B1 RID: 945
		private string columnName;

		// Token: 0x040003B2 RID: 946
		private string tableName;

		// Token: 0x040003B3 RID: 947
		private string dataTypeName;

		// Token: 0x040003B4 RID: 948
		private bool allowDBNull;

		// Token: 0x040003B5 RID: 949
		private bool isReadOnly;

		// Token: 0x040003B6 RID: 950
		private int ordinal;

		// Token: 0x040003B7 RID: 951
		private int size;

		// Token: 0x040003B8 RID: 952
		private byte precision;

		// Token: 0x040003B9 RID: 953
		private byte scale;

		// Token: 0x040003BA RID: 954
		private Type fieldType;
	}
}
