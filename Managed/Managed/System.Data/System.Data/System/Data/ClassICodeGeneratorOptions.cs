using System;
using System.CodeDom.Compiler;

namespace System.Data
{
	// Token: 0x02000017 RID: 23
	internal class ClassICodeGeneratorOptions : ClassGeneratorOptions
	{
		// Token: 0x0600007A RID: 122 RVA: 0x000044BC File Offset: 0x000026BC
		public ClassICodeGeneratorOptions(ICodeGenerator codeGen)
		{
			this.gen = codeGen;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000044CC File Offset: 0x000026CC
		internal override string DataSetName(string source)
		{
			if (this.CreateDataSetName != null)
			{
				return this.CreateDataSetName(source, this.gen);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.gen);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004504 File Offset: 0x00002704
		internal override string TableTypeName(string source)
		{
			if (this.CreateTableTypeName != null)
			{
				return this.CreateTableTypeName(source, this.gen);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.gen) + "DataTable";
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004548 File Offset: 0x00002748
		internal override string TableMemberName(string source)
		{
			if (this.CreateTableMemberName != null)
			{
				return this.CreateTableMemberName(source, this.gen);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.gen);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004580 File Offset: 0x00002780
		internal override string TableColName(string source)
		{
			if (this.CreateTableColumnName != null)
			{
				return this.CreateTableColumnName(source, this.gen);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.gen);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000045B8 File Offset: 0x000027B8
		internal override string TableDelegateName(string source)
		{
			if (this.CreateTableDelegateName != null)
			{
				return this.CreateTableDelegateName(source, this.gen);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.gen) + "RowChangedEventHandler";
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000045FC File Offset: 0x000027FC
		internal override string EventArgsName(string source)
		{
			if (this.CreateEventArgsName != null)
			{
				return this.CreateEventArgsName(source, this.gen);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.gen) + "RowChangedEventArgs";
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004640 File Offset: 0x00002840
		internal override string ColumnName(string source)
		{
			if (this.CreateColumnName != null)
			{
				return this.CreateColumnName(source, this.gen);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.gen);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004678 File Offset: 0x00002878
		internal override string RowName(string source)
		{
			if (this.CreateRowName != null)
			{
				return this.CreateRowName(source, this.gen);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.gen) + "Row";
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000046BC File Offset: 0x000028BC
		internal override string RelationName(string source)
		{
			if (this.CreateRelationName != null)
			{
				return this.CreateRelationName(source, this.gen);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.gen) + "Relation";
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004700 File Offset: 0x00002900
		internal override string TableAdapterNSName(string source)
		{
			if (this.CreateTableAdapterNSName != null)
			{
				return this.CreateTableAdapterNSName(source, this.gen);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.gen) + "TableAdapters";
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004744 File Offset: 0x00002944
		internal override string TableAdapterName(string source)
		{
			if (this.CreateTableAdapterName != null)
			{
				return this.CreateTableAdapterName(source, this.gen);
			}
			return CustomDataClassGenerator.MakeSafeName(source, this.gen);
		}

		// Token: 0x04000084 RID: 132
		private ICodeGenerator gen;

		// Token: 0x04000085 RID: 133
		public CodeNamingMethod CreateDataSetName;

		// Token: 0x04000086 RID: 134
		public CodeNamingMethod CreateTableTypeName;

		// Token: 0x04000087 RID: 135
		public CodeNamingMethod CreateTableMemberName;

		// Token: 0x04000088 RID: 136
		public CodeNamingMethod CreateTableColumnName;

		// Token: 0x04000089 RID: 137
		public CodeNamingMethod CreateColumnName;

		// Token: 0x0400008A RID: 138
		public CodeNamingMethod CreateRowName;

		// Token: 0x0400008B RID: 139
		public CodeNamingMethod CreateRelationName;

		// Token: 0x0400008C RID: 140
		public CodeNamingMethod CreateTableDelegateName;

		// Token: 0x0400008D RID: 141
		public CodeNamingMethod CreateEventArgsName;

		// Token: 0x0400008E RID: 142
		public CodeNamingMethod CreateTableAdapterNSName;

		// Token: 0x0400008F RID: 143
		public CodeNamingMethod CreateTableAdapterName;
	}
}
