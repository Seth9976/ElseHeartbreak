using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x0200008A RID: 138
	internal class TableMappingCollection : CollectionBase
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x0001FFCC File Offset: 0x0001E1CC
		public void Add(TableMapping map)
		{
			base.List.Add(map);
		}

		// Token: 0x1700013C RID: 316
		public TableMapping this[string name]
		{
			get
			{
				foreach (object obj in base.List)
				{
					TableMapping tableMapping = (TableMapping)obj;
					if (tableMapping.Table.TableName == name)
					{
						return tableMapping;
					}
				}
				return null;
			}
		}
	}
}
