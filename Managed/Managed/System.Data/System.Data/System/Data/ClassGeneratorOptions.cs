using System;

namespace System.Data
{
	// Token: 0x02000019 RID: 25
	internal abstract class ClassGeneratorOptions
	{
		// Token: 0x06000093 RID: 147
		internal abstract string DataSetName(string source);

		// Token: 0x06000094 RID: 148
		internal abstract string TableTypeName(string source);

		// Token: 0x06000095 RID: 149
		internal abstract string TableMemberName(string source);

		// Token: 0x06000096 RID: 150
		internal abstract string TableColName(string source);

		// Token: 0x06000097 RID: 151
		internal abstract string TableDelegateName(string source);

		// Token: 0x06000098 RID: 152
		internal abstract string EventArgsName(string source);

		// Token: 0x06000099 RID: 153
		internal abstract string ColumnName(string source);

		// Token: 0x0600009A RID: 154
		internal abstract string RowName(string source);

		// Token: 0x0600009B RID: 155
		internal abstract string RelationName(string source);

		// Token: 0x0600009C RID: 156
		internal abstract string TableAdapterNSName(string source);

		// Token: 0x0600009D RID: 157
		internal abstract string TableAdapterName(string source);

		// Token: 0x0400009C RID: 156
		public bool MakeClassesInsideDataSet = true;
	}
}
