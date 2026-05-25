using System;
using System.Collections;

namespace System.Data
{
	// Token: 0x02000182 RID: 386
	internal class RelationStructureCollection : CollectionBase
	{
		// Token: 0x06001487 RID: 5255 RVA: 0x00056B50 File Offset: 0x00054D50
		public void Add(RelationStructure rel)
		{
			base.List.Add(rel);
		}

		// Token: 0x170003D7 RID: 983
		public RelationStructure this[int i]
		{
			get
			{
				return base.List[i] as RelationStructure;
			}
		}

		// Token: 0x170003D8 RID: 984
		public RelationStructure this[string parent, string child]
		{
			get
			{
				foreach (object obj in base.List)
				{
					RelationStructure relationStructure = (RelationStructure)obj;
					if (relationStructure.ParentTableName == parent && relationStructure.ChildTableName == child)
					{
						return relationStructure;
					}
				}
				return null;
			}
		}
	}
}
