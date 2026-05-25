using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000038 RID: 56
	internal class DataTablePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x00019DEC File Offset: 0x00017FEC
		internal DataTablePropertyDescriptor(DataTable table)
			: base(table.TableName, null)
		{
			this.table = table;
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00019E04 File Offset: 0x00018004
		public DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00019E0C File Offset: 0x0001800C
		public override object GetValue(object component)
		{
			DataViewManagerListItemTypeDescriptor dataViewManagerListItemTypeDescriptor = component as DataViewManagerListItemTypeDescriptor;
			if (dataViewManagerListItemTypeDescriptor == null)
			{
				return null;
			}
			return new DataView(this.table, dataViewManagerListItemTypeDescriptor.DataViewManager);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00019E3C File Offset: 0x0001803C
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00019E40 File Offset: 0x00018040
		public override bool Equals(object other)
		{
			return other is DataTablePropertyDescriptor && ((DataTablePropertyDescriptor)other).table == this.table;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00019E64 File Offset: 0x00018064
		public override int GetHashCode()
		{
			return this.table.GetHashCode();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00019E74 File Offset: 0x00018074
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00019E78 File Offset: 0x00018078
		public override void ResetValue(object component)
		{
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00019E7C File Offset: 0x0001807C
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00019E80 File Offset: 0x00018080
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00019E84 File Offset: 0x00018084
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00019E90 File Offset: 0x00018090
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x04000162 RID: 354
		private DataTable table;
	}
}
