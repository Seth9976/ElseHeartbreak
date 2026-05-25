using System;

namespace System.Data
{
	// Token: 0x02000185 RID: 389
	internal class ConstraintStructure
	{
		// Token: 0x0600148D RID: 5261 RVA: 0x00056D38 File Offset: 0x00054F38
		public ConstraintStructure(string tname, string[] cols, bool[] isAttr, string cname, bool isPK, string refName, bool isNested, bool isConstraintOnly)
		{
			this.TableName = tname;
			this.Columns = cols;
			this.IsAttribute = isAttr;
			this.ConstraintName = XmlHelper.Decode(cname);
			this.IsPrimaryKey = isPK;
			this.ReferName = refName;
			this.IsNested = isNested;
			this.IsConstraintOnly = isConstraintOnly;
		}

		// Token: 0x04000838 RID: 2104
		public readonly string TableName;

		// Token: 0x04000839 RID: 2105
		public readonly string[] Columns;

		// Token: 0x0400083A RID: 2106
		public readonly bool[] IsAttribute;

		// Token: 0x0400083B RID: 2107
		public readonly string ConstraintName;

		// Token: 0x0400083C RID: 2108
		public readonly bool IsPrimaryKey;

		// Token: 0x0400083D RID: 2109
		public readonly string ReferName;

		// Token: 0x0400083E RID: 2110
		public readonly bool IsNested;

		// Token: 0x0400083F RID: 2111
		public readonly bool IsConstraintOnly;
	}
}
