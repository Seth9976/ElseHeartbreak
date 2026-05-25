using System;

namespace System.Data
{
	// Token: 0x02000184 RID: 388
	internal class RelationStructure
	{
		// Token: 0x04000831 RID: 2097
		public string ExplicitName;

		// Token: 0x04000832 RID: 2098
		public string ParentTableName;

		// Token: 0x04000833 RID: 2099
		public string ChildTableName;

		// Token: 0x04000834 RID: 2100
		public string ParentColumnName;

		// Token: 0x04000835 RID: 2101
		public string ChildColumnName;

		// Token: 0x04000836 RID: 2102
		public bool IsNested;

		// Token: 0x04000837 RID: 2103
		public bool CreateConstraint;
	}
}
