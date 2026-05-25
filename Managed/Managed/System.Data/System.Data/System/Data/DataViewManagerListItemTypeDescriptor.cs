using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x0200003D RID: 61
	internal class DataViewManagerListItemTypeDescriptor : ICustomTypeDescriptor
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x0001D354 File Offset: 0x0001B554
		internal DataViewManagerListItemTypeDescriptor(DataViewManager dvm)
		{
			this.dvm = dvm;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001D364 File Offset: 0x0001B564
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001D36C File Offset: 0x0001B56C
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001D370 File Offset: 0x0001B570
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001D374 File Offset: 0x0001B574
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001D378 File Offset: 0x0001B578
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0001D37C File Offset: 0x0001B57C
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0001D380 File Offset: 0x0001B580
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001D384 File Offset: 0x0001B584
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001D38C File Offset: 0x0001B58C
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0001D394 File Offset: 0x0001B594
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return this.GetProperties();
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001D39C File Offset: 0x0001B59C
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001D3A0 File Offset: 0x0001B5A0
		internal DataViewManager DataViewManager
		{
			get
			{
				return this.dvm;
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		public PropertyDescriptorCollection GetProperties()
		{
			DataSet dataSet = this.dvm.DataSet;
			if (dataSet == null)
			{
				return null;
			}
			DataTableCollection tables = dataSet.Tables;
			int num = 0;
			PropertyDescriptor[] array = new PropertyDescriptor[tables.Count];
			foreach (object obj in tables)
			{
				DataTable dataTable = (DataTable)obj;
				array[num++] = new DataTablePropertyDescriptor(dataTable);
			}
			return new PropertyDescriptorCollection(array);
		}

		// Token: 0x0400018F RID: 399
		private DataViewManager dvm;
	}
}
