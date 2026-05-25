using System;

namespace System.Data.OleDb
{
	/// <summary>Returns the type of schema table specified by the <see cref="M:System.Data.OleDb.OleDbConnection.GetOleDbSchemaTable(System.Guid,System.Object[])" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FD RID: 253
	public sealed class OleDbSchemaGuid
	{
		/// <summary>Returns the assertions defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000478 RID: 1144
		public static readonly Guid Assertions = new Guid("df855bea-fb95-4abc-8932-e57e45c7ddae");

		/// <summary>Returns the physical attributes associated with catalogs accessible from the data source. Returns the assertions defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000479 RID: 1145
		public static readonly Guid Catalogs = new Guid("e4a67334-f03c-45af-8b1d-531f99268045");

		/// <summary>Returns the character sets defined in the catalog that is accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400047A RID: 1146
		public static readonly Guid Character_Sets = new Guid("e4533bdb-0b55-48ee-986d-17d07143657d");

		/// <summary>Returns the check constraints defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400047B RID: 1147
		public static readonly Guid Check_Constraints = new Guid("fedf7f5d-cfb4-4635-af02-45eb4bb4e8f3");

		/// <summary>Returns the check constraints defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400047C RID: 1148
		public static readonly Guid Check_Constraints_By_Table = new Guid("d76547ef-837d-413c-8d76-bab1d7bb014a");

		/// <summary>Returns the character collations defined in the catalog that is accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400047D RID: 1149
		public static readonly Guid Collations = new Guid("5145b85c-c448-4b9e-8929-4c2de31ffa30");

		/// <summary>Returns the columns of tables (including views) defined in the catalog that is accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400047E RID: 1150
		public static readonly Guid Columns = new Guid("86dcd6e2-9a8c-4c6d-bc1c-e0e334c727c9");

		/// <summary>Returns the columns defined in the catalog that are dependent on a domain defined in the catalog and owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400047F RID: 1151
		public static readonly Guid Column_Domain_Usage = new Guid("058acb5e-eb1d-4b6e-8e98-a7d59a959ff1");

		/// <summary>Returns the privileges on columns of tables defined in the catalog that are available to or granted by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000480 RID: 1152
		public static readonly Guid Column_Privileges = new Guid("43152796-f3b4-4342-9647-008f1060e352");

		/// <summary>Returns the columns used by referential constraints, unique constraints, check constraints, and assertions, defined in the catalog and owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000481 RID: 1153
		public static readonly Guid Constraint_Column_Usage = new Guid("3a39f999-f481-4293-8b9f-af7e91b4ee7d");

		/// <summary>Returns the tables that are used by referential constraints, unique constraints, check constraints, and assertions defined in the catalog and owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000482 RID: 1154
		public static readonly Guid Constraint_Table_Usage = new Guid("d689719b-24b0-4963-a635-097c480edcd2");

		/// <summary>Returns a list of provider-specific literals used in text commands.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000483 RID: 1155
		public static readonly Guid DbInfoLiterals = new Guid("7a564da6-f3bc-474b-9e66-71cb47bde5b0");

		/// <summary>Returns the foreign key columns defined in the catalog by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000484 RID: 1156
		public static readonly Guid Foreign_Keys = new Guid("d9e547ce-e62d-4200-b849-566bc3dc29de");

		/// <summary>Returns the indexes defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000485 RID: 1157
		public static readonly Guid Indexes = new Guid("69d8523c-96ad-40cb-a89a-ee98d2d6fcec");

		/// <summary>Returns the columns defined in the catalog that is constrained as keys by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000486 RID: 1158
		public static readonly Guid Key_Column_Usage = new Guid("65423211-805e-4822-8eb4-f4f6d540056e");

		/// <summary>Returns the primary key columns defined in the catalog by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000487 RID: 1159
		public static readonly Guid Primary_Keys = new Guid("c6e5b174-fbd8-4055-b757-8585040e463f");

		/// <summary>Returns the procedures defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000488 RID: 1160
		public static readonly Guid Procedures = new Guid("61f276ad-4f25-4c26-b4ae-8238e06d56db");

		/// <summary>Returns information about the columns of rowsets returned by procedures.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000489 RID: 1161
		public static readonly Guid Procedure_Columns = new Guid("7148080d-e053-4ada-b79a-9a2ff614a3d4");

		/// <summary>Returns information about the parameters and return codes of procedures.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400048A RID: 1162
		public static readonly Guid Procedure_Parameters = new Guid("984af700-8fe7-476f-81c2-4b814df67907");

		/// <summary>Returns the base data types supported by the .NET Framework Data Provider for OLE DB.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400048B RID: 1163
		public static readonly Guid Provider_Types = new Guid("0bc2da44-d834-4136-9ff0-3cef477784b9");

		/// <summary>Returns the referential constraints defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400048C RID: 1164
		public static readonly Guid Referential_Constraints = new Guid("d2eab85e-49a7-462d-aa22-1d97c74178ae");

		/// <summary>Returns the schema objects that are owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400048D RID: 1165
		public static readonly Guid Schemata = new Guid("2fbd7503-0af3-43d2-92c6-51e78b84dd37");

		/// <summary>Returns the conformance levels, options, and dialects supported by the SQL-implementation processing data defined in the catalog.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400048E RID: 1166
		public static readonly Guid Sql_Languages = new Guid("d60a511d-a07f-4e59-aac2-71c25fab5b02");

		/// <summary>Returns the statistics defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400048F RID: 1167
		public static readonly Guid Statistics = new Guid("03ed9f7d-35bc-45fe-993f-ee7a5f29fb74");

		/// <summary>Returns the tables (including views) defined in the catalog that are accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000490 RID: 1168
		public static readonly Guid Tables = new Guid("ceac88ba-240c-4bb4-821e-4a49fc013371");

		/// <summary>Returns the tables (including views) that are accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000491 RID: 1169
		public static readonly Guid Tables_Info = new Guid("9ff81c59-2b1e-4371-a08b-3a2d373189fa");

		/// <summary>Returns the table constraints defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000492 RID: 1170
		public static readonly Guid Table_Constraints = new Guid("62883c55-082d-42cb-bb00-747985ca6047");

		/// <summary>Returns the privileges on tables defined in the catalog that are available to, or granted by, a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000493 RID: 1171
		public static readonly Guid Table_Privileges = new Guid("1a73f478-8c8e-4ede-b3ec-22ba13ab55a0");

		/// <summary>Describes the available set of statistics on tables in the provider.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000494 RID: 1172
		public static readonly Guid Table_Statistics = new Guid("9c944744-cd51-448a-8be4-7095f039d0ef");

		/// <summary>Returns the character translations defined in the catalog that is accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000495 RID: 1173
		public static readonly Guid Translations = new Guid("5578b57e-a682-4f1b-bdb4-f8a14ad6f61e");

		/// <summary>Identifies the trustees defined in the data source.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000496 RID: 1174
		public static readonly Guid Trustee = new Guid("521207e2-3a23-42b6-ac78-810a3fce3271");

		/// <summary>Returns the USAGE privileges on objects defined in the catalog that are available to or granted by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000497 RID: 1175
		public static readonly Guid Usage_Privileges = new Guid("f8113a2b-2934-4c67-ab7b-adbe3ab74973");

		/// <summary>Returns the views defined in the catalog that is accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000498 RID: 1176
		public static readonly Guid Views = new Guid("9a6345b6-61a0-40fd-9b45-402f3c9c9c3e");

		/// <summary>Returns the columns on which viewed tables depend, as defined in the catalog and owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000499 RID: 1177
		public static readonly Guid View_Column_Usage = new Guid("2c91ef91-02d8-4d38-ae5a-1e826873d6ea");

		/// <summary>Returns the tables on which viewed tables, defined in the catalog and owned by a given user, are dependent.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400049A RID: 1178
		public static readonly Guid View_Table_Usage = new Guid("7dcb7f53-1045-4fdf-86a1-d3caaf27c7f5");
	}
}
