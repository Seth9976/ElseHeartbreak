using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x02000181 RID: 385
	internal class TableStructureCollection : CollectionBase
	{
		// Token: 0x06001483 RID: 5251 RVA: 0x00056A9C File Offset: 0x00054C9C
		public void Add(TableStructure table)
		{
			base.List.Add(table);
		}

		// Token: 0x170003D5 RID: 981
		public TableStructure this[int i]
		{
			get
			{
				return base.List[i] as TableStructure;
			}
		}

		// Token: 0x170003D6 RID: 982
		public TableStructure this[string name]
		{
			get
			{
				foreach (object obj in base.List)
				{
					TableStructure tableStructure = (TableStructure)obj;
					if (tableStructure.Table.TableName == name)
					{
						return tableStructure;
					}
				}
				return null;
			}
		}
	}
}
