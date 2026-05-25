using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Automatically generates single-table commands that are used to reconcile changes made to a <see cref="T:System.Data.DataSet" /> with the associated SQL Server database. This class cannot be inherited. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015D RID: 349
	public sealed class SqlCommandBuilder : DbCommandBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class.</summary>
		// Token: 0x06001244 RID: 4676 RVA: 0x0004785C File Offset: 0x00045A5C
		public SqlCommandBuilder()
		{
			this.QuoteSuffix = "]";
			this.QuotePrefix = "[";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class with the associated <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> object.</summary>
		/// <param name="adapter">The name of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />. </param>
		// Token: 0x06001245 RID: 4677 RVA: 0x00047898 File Offset: 0x00045A98
		public SqlCommandBuilder(SqlDataAdapter adapter)
			: this()
		{
			this.DataAdapter = adapter;
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> object for which Transact-SQL statements are automatically generated.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x000478A8 File Offset: 0x00045AA8
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x000478B8 File Offset: 0x00045AB8
		[DefaultValue(null)]
		public new SqlDataAdapter DataAdapter
		{
			get
			{
				return (SqlDataAdapter)base.DataAdapter;
			}
			set
			{
				base.DataAdapter = value;
			}
		}

		/// <summary>Gets or sets the starting character or characters to use when specifying SQL Server database objects, such as tables or columns, whose names contain characters such as spaces or reserved tokens.</summary>
		/// <returns>The starting character or characters to use. The default is an empty string.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property cannot be changed after an INSERT, UPDATE, or DELETE command has been generated. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x000478C4 File Offset: 0x00045AC4
		// (set) Token: 0x06001249 RID: 4681 RVA: 0x000478CC File Offset: 0x00045ACC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string QuotePrefix
		{
			get
			{
				return base.QuotePrefix;
			}
			set
			{
				if (value != "[" && value != "\"")
				{
					throw new ArgumentException("Only '[' and '\"' are allowed as value for the 'QuoteSuffix' property.");
				}
				base.QuotePrefix = value;
			}
		}

		/// <summary>Gets or sets the ending character or characters to use when specifying SQL Server database objects, such as tables or columns, whose names contain characters such as spaces or reserved tokens.</summary>
		/// <returns>The ending character or characters to use. The default is an empty string.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property cannot be changed after an insert, update, or delete command has been generated. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0004790C File Offset: 0x00045B0C
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x00047914 File Offset: 0x00045B14
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string QuoteSuffix
		{
			get
			{
				return base.QuoteSuffix;
			}
			set
			{
				if (value != "]" && value != "\"")
				{
					throw new ArgumentException("Only ']' and '\"' are allowed as value for the 'QuoteSuffix' property.");
				}
				base.QuoteSuffix = value;
			}
		}

		/// <summary>Sets or gets a string used as the catalog separator for an instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class.</summary>
		/// <returns>A string that indicates the catalog separator for use with an instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x00047954 File Offset: 0x00045B54
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x0004795C File Offset: 0x00045B5C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string CatalogSeparator
		{
			get
			{
				return this._catalogSeparator;
			}
			set
			{
				if (value != this._catalogSeparator)
				{
					throw new ArgumentException("Only '.' is allowed as value for the 'CatalogSeparator' property.");
				}
			}
		}

		/// <summary>Gets or sets the character to be used for the separator between the schema identifier and any other identifiers.</summary>
		/// <returns>The character to be used as the schema separator.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0004797C File Offset: 0x00045B7C
		// (set) Token: 0x0600124F RID: 4687 RVA: 0x00047984 File Offset: 0x00045B84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string SchemaSeparator
		{
			get
			{
				return this._schemaSeparator;
			}
			set
			{
				if (value != this._schemaSeparator)
				{
					throw new ArgumentException("Only '.' is allowed as value for the 'SchemaSeparator' property.");
				}
			}
		}

		/// <summary>Sets or gets the <see cref="T:System.Data.Common.CatalogLocation" /> for an instance of the <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> class.</summary>
		/// <returns>A <see cref="T:System.Data.Common.CatalogLocation" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x000479A4 File Offset: 0x00045BA4
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x000479AC File Offset: 0x00045BAC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override CatalogLocation CatalogLocation
		{
			get
			{
				return this._catalogLocation;
			}
			set
			{
				if (value != CatalogLocation.Start)
				{
					throw new ArgumentException("Only 'Start' is allowed as value for the 'CatalogLocation' property.");
				}
			}
		}

		/// <summary>Retrieves parameter information from the stored procedure specified in the <see cref="T:System.Data.SqlClient.SqlCommand" /> and populates the <see cref="P:System.Data.SqlClient.SqlCommand.Parameters" /> collection of the specified <see cref="T:System.Data.SqlClient.SqlCommand" /> object.</summary>
		/// <param name="command">The <see cref="T:System.Data.SqlClient.SqlCommand" /> referencing the stored procedure from which the parameter information is to be derived. The derived parameters are added to the <see cref="P:System.Data.SqlClient.SqlCommand.Parameters" /> collection of the <see cref="T:System.Data.SqlClient.SqlCommand" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The command text is not a valid stored procedure name. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001252 RID: 4690 RVA: 0x000479C0 File Offset: 0x00045BC0
		public static void DeriveParameters(SqlCommand command)
		{
			command.DeriveParameters();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform deletions on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform deletions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001253 RID: 4691 RVA: 0x000479C8 File Offset: 0x00045BC8
		public new SqlCommand GetDeleteCommand()
		{
			return (SqlCommand)base.GetDeleteCommand(false);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform insertions on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform insertions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001254 RID: 4692 RVA: 0x000479D8 File Offset: 0x00045BD8
		public new SqlCommand GetInsertCommand()
		{
			return (SqlCommand)base.GetInsertCommand(false);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform updates on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform updates.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001255 RID: 4693 RVA: 0x000479E8 File Offset: 0x00045BE8
		public new SqlCommand GetUpdateCommand()
		{
			return (SqlCommand)base.GetUpdateCommand(false);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform updates on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object required to perform updates.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names if possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001256 RID: 4694 RVA: 0x000479F8 File Offset: 0x00045BF8
		public new SqlCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform deletions on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform deletions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names if possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001257 RID: 4695 RVA: 0x00047A08 File Offset: 0x00045C08
		public new SqlCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform insertions on the database.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is required to perform insertions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names if possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001258 RID: 4696 RVA: 0x00047A18 File Offset: 0x00045C18
		public new SqlCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001259 RID: 4697 RVA: 0x00047A28 File Offset: 0x00045C28
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			if (unquotedIdentifier == null)
			{
				throw new ArgumentNullException("unquotedIdentifier");
			}
			string quotePrefix = this.QuotePrefix;
			string quoteSuffix = this.QuoteSuffix;
			if ((quotePrefix == "[" && quoteSuffix != "]") || (quotePrefix == "\"" && quoteSuffix != "\""))
			{
				throw new ArgumentException("The QuotePrefix and QuoteSuffix properties do not match.");
			}
			string text = unquotedIdentifier.Replace(quoteSuffix, quoteSuffix + quoteSuffix);
			return quotePrefix + text + quoteSuffix;
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier. This includes correctly unescaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes properly unescaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600125A RID: 4698 RVA: 0x00047AB8 File Offset: 0x00045CB8
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			return base.UnquoteIdentifier(quotedIdentifier);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00047AC4 File Offset: 0x00045CC4
		private bool IncludedInInsert(DataRow schemaRow)
		{
			return (schemaRow.IsNull("IsAutoIncrement") || !(bool)schemaRow["IsAutoIncrement"]) && (schemaRow.IsNull("IsHidden") || !(bool)schemaRow["IsHidden"]) && (schemaRow.IsNull("IsExpression") || !(bool)schemaRow["IsExpression"]) && (schemaRow.IsNull("IsRowVersion") || !(bool)schemaRow["IsRowVersion"]) && (schemaRow.IsNull("IsReadOnly") || !(bool)schemaRow["IsReadOnly"]);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00047B98 File Offset: 0x00045D98
		private bool IncludedInUpdate(DataRow schemaRow)
		{
			return (schemaRow.IsNull("IsAutoIncrement") || !(bool)schemaRow["IsAutoIncrement"]) && (schemaRow.IsNull("IsHidden") || !(bool)schemaRow["IsHidden"]) && (schemaRow.IsNull("IsRowVersion") || !(bool)schemaRow["IsRowVersion"]) && (schemaRow.IsNull("IsExpression") || !(bool)schemaRow["IsExpression"]) && (schemaRow.IsNull("IsReadOnly") || !(bool)schemaRow["IsReadOnly"]);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00047C6C File Offset: 0x00045E6C
		private bool IncludedInWhereClause(DataRow schemaRow)
		{
			return !(bool)schemaRow["IsLong"];
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00047C88 File Offset: 0x00045E88
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause)
		{
			SqlParameter sqlParameter = (SqlParameter)parameter;
			sqlParameter.SqlDbType = (SqlDbType)((int)datarow["ProviderType"]);
			object obj = datarow["NumericPrecision"];
			if (obj != DBNull.Value)
			{
				short num = (short)obj;
				if (num < 255 && num >= 0)
				{
					sqlParameter.Precision = (byte)num;
				}
			}
			object obj2 = datarow["NumericScale"];
			if (obj2 != DBNull.Value)
			{
				short num2 = (short)obj2;
				if (num2 < 255 && num2 >= 0)
				{
					sqlParameter.Scale = (byte)num2;
				}
			}
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00047D28 File Offset: 0x00045F28
		protected override string GetParameterName(int parameterOrdinal)
		{
			return string.Format("@p{0}", parameterOrdinal);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00047D3C File Offset: 0x00045F3C
		protected override string GetParameterName(string parameterName)
		{
			return string.Format("@{0}", parameterName);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00047D4C File Offset: 0x00045F4C
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return this.GetParameterName(parameterOrdinal);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00047D58 File Offset: 0x00045F58
		private void RowUpdatingHandler(object sender, SqlRowUpdatingEventArgs args)
		{
			base.RowUpdatingHandler(args);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00047D64 File Offset: 0x00045F64
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			SqlDataAdapter sqlDataAdapter = adapter as SqlDataAdapter;
			if (sqlDataAdapter == null)
			{
				throw new InvalidOperationException("Adapter needs to be a SqlDataAdapter");
			}
			if (sqlDataAdapter != base.DataAdapter)
			{
				sqlDataAdapter.RowUpdating += this.RowUpdatingHandler;
			}
			else
			{
				sqlDataAdapter.RowUpdating -= this.RowUpdatingHandler;
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00047DC0 File Offset: 0x00045FC0
		protected override DataTable GetSchemaTable(DbCommand srcCommand)
		{
			DataTable schemaTable;
			using (SqlDataReader sqlDataReader = (SqlDataReader)srcCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
			{
				schemaTable = sqlDataReader.GetSchemaTable();
			}
			return schemaTable;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00047E18 File Offset: 0x00046018
		protected override DbCommand InitializeCommand(DbCommand command)
		{
			if (command == null)
			{
				command = new SqlCommand();
			}
			else
			{
				command.CommandTimeout = 30;
				command.Transaction = null;
				command.CommandType = CommandType.Text;
				command.UpdatedRowSource = UpdateRowSource.None;
			}
			return command;
		}

		// Token: 0x04000741 RID: 1857
		private readonly string _catalogSeparator = ".";

		// Token: 0x04000742 RID: 1858
		private readonly string _schemaSeparator = ".";

		// Token: 0x04000743 RID: 1859
		private readonly CatalogLocation _catalogLocation = CatalogLocation.Start;
	}
}
