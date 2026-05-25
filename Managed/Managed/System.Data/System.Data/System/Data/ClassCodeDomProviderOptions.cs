using System;
using System.CodeDom.Compiler;

namespace System.Data
{
	// Token: 0x02000018 RID: 24
	internal class ClassCodeDomProviderOptions : ClassGeneratorOptions
	{
		// Token: 0x06000086 RID: 134 RVA: 0x0000477C File Offset: 0x0000297C
		public ClassCodeDomProviderOptions(CodeDomProvider codeProvider)
		{
			this.provider = codeProvider;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000478C File Offset: 0x0000298C
		internal override string DataSetName(string source)
		{
			if (this.CreateDataSetName != null)
			{
				return this.CreateDataSetName(source, this.provider);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.provider);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000047C4 File Offset: 0x000029C4
		internal override string TableTypeName(string source)
		{
			if (this.CreateTableTypeName != null)
			{
				return this.CreateTableTypeName(source, this.provider);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.provider) + "DataTable";
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004808 File Offset: 0x00002A08
		internal override string TableMemberName(string source)
		{
			if (this.CreateTableMemberName != null)
			{
				return this.CreateTableMemberName(source, this.provider);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.provider);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004840 File Offset: 0x00002A40
		internal override string TableColName(string source)
		{
			if (this.CreateTableColumnName != null)
			{
				return this.CreateTableColumnName(source, this.provider);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.provider);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004878 File Offset: 0x00002A78
		internal override string TableDelegateName(string source)
		{
			if (this.CreateTableDelegateName != null)
			{
				return this.CreateTableDelegateName(source, this.provider);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.provider) + "RowChangedEventHandler";
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000048BC File Offset: 0x00002ABC
		internal override string EventArgsName(string source)
		{
			if (this.CreateEventArgsName != null)
			{
				return this.CreateEventArgsName(source, this.provider);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.provider) + "RowChangedEventArgs";
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004900 File Offset: 0x00002B00
		internal override string ColumnName(string source)
		{
			if (this.CreateColumnName != null)
			{
				return this.CreateColumnName(source, this.provider);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.provider);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004938 File Offset: 0x00002B38
		internal override string RowName(string source)
		{
			if (this.CreateRowName != null)
			{
				return this.CreateRowName(source, this.provider);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.provider) + "Row";
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000497C File Offset: 0x00002B7C
		internal override string RelationName(string source)
		{
			if (this.CreateRelationName != null)
			{
				return this.CreateRelationName(source, this.provider);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.provider) + "Relation";
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000049C0 File Offset: 0x00002BC0
		internal override string TableAdapterNSName(string source)
		{
			if (this.CreateTableAdapterNSName != null)
			{
				return this.CreateTableAdapterNSName(source, this.provider);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.provider) + "TableAdapters";
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004A04 File Offset: 0x00002C04
		internal override string TableAdapterName(string source)
		{
			if (this.CreateTableAdapterName != null)
			{
				return this.CreateTableAdapterName(source, this.provider);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.provider);
		}

		// Token: 0x04000090 RID: 144
		private CodeDomProvider provider;

		// Token: 0x04000091 RID: 145
		public CodeDomNamingMethod CreateDataSetName;

		// Token: 0x04000092 RID: 146
		public CodeDomNamingMethod CreateTableTypeName;

		// Token: 0x04000093 RID: 147
		public CodeDomNamingMethod CreateTableMemberName;

		// Token: 0x04000094 RID: 148
		public CodeDomNamingMethod CreateTableColumnName;

		// Token: 0x04000095 RID: 149
		public CodeDomNamingMethod CreateColumnName;

		// Token: 0x04000096 RID: 150
		public CodeDomNamingMethod CreateRowName;

		// Token: 0x04000097 RID: 151
		public CodeDomNamingMethod CreateRelationName;

		// Token: 0x04000098 RID: 152
		public CodeDomNamingMethod CreateTableDelegateName;

		// Token: 0x04000099 RID: 153
		public CodeDomNamingMethod CreateEventArgsName;

		// Token: 0x0400009A RID: 154
		public CodeDomNamingMethod CreateTableAdapterNSName;

		// Token: 0x0400009B RID: 155
		public CodeDomNamingMethod CreateTableAdapterName;
	}
}
