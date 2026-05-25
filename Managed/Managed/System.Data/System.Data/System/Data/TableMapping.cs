using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x0200008B RID: 139
	internal class TableMapping
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x00020064 File Offset: 0x0001E264
		public TableMapping(string name, string ns)
		{
			this.Table = new DataTable(name);
			this.Table.Namespace = ns;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000200B8 File Offset: 0x0001E2B8
		public TableMapping(DataTable dt)
		{
			this.existsInDataSet = true;
			this.Table = dt;
			foreach (object obj in dt.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				switch (dataColumn.ColumnMapping)
				{
				case MappingType.Element:
					this.Elements.Add(dataColumn);
					break;
				case MappingType.Attribute:
					this.Attributes.Add(dataColumn);
					break;
				case MappingType.SimpleContent:
					this.SimpleContent = dataColumn;
					break;
				}
			}
			this.PrimaryKey = ((dt.PrimaryKey.Length <= 0) ? null : dt.PrimaryKey[0]);
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x000201D0 File Offset: 0x0001E3D0
		public bool ExistsInDataSet
		{
			get
			{
				return this.existsInDataSet;
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000201D8 File Offset: 0x0001E3D8
		public bool ContainsColumn(string name)
		{
			return this.GetColumn(name) != null;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000201E8 File Offset: 0x0001E3E8
		public DataColumn GetColumn(string name)
		{
			foreach (object obj in this.Elements)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.ColumnName == name)
				{
					return dataColumn;
				}
			}
			foreach (object obj2 in this.Attributes)
			{
				DataColumn dataColumn2 = (DataColumn)obj2;
				if (dataColumn2.ColumnName == name)
				{
					return dataColumn2;
				}
			}
			if (this.SimpleContent != null && name == this.SimpleContent.ColumnName)
			{
				return this.SimpleContent;
			}
			if (this.PrimaryKey != null && name == this.PrimaryKey.ColumnName)
			{
				return this.PrimaryKey;
			}
			return null;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00020334 File Offset: 0x0001E534
		public void RemoveElementColumn(string name)
		{
			foreach (object obj in this.Elements)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.ColumnName == name)
				{
					this.Elements.Remove(dataColumn);
					break;
				}
			}
		}

		// Token: 0x04000262 RID: 610
		private bool existsInDataSet;

		// Token: 0x04000263 RID: 611
		public DataTable Table;

		// Token: 0x04000264 RID: 612
		public ArrayList Elements = new ArrayList();

		// Token: 0x04000265 RID: 613
		public ArrayList Attributes = new ArrayList();

		// Token: 0x04000266 RID: 614
		public DataColumn SimpleContent;

		// Token: 0x04000267 RID: 615
		public DataColumn PrimaryKey;

		// Token: 0x04000268 RID: 616
		public DataColumn ReferenceKey;

		// Token: 0x04000269 RID: 617
		public int lastElementIndex = -1;

		// Token: 0x0400026A RID: 618
		public TableMapping ParentTable;

		// Token: 0x0400026B RID: 619
		public TableMappingCollection ChildTables = new TableMappingCollection();
	}
}
