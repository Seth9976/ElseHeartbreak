using System;
using System.ComponentModel;
using System.Data.Common;
using System.Text;

namespace System.Data.Odbc
{
	/// <summary>Automatically generates single-table commands that are used to reconcile changes made to a <see cref="T:System.Data.DataSet" /> with the associated data source. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000141 RID: 321
	public sealed class OdbcCommandBuilder : DbCommandBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommandBuilder" /> class.</summary>
		// Token: 0x0600113B RID: 4411 RVA: 0x000434C4 File Offset: 0x000416C4
		public OdbcCommandBuilder()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommandBuilder" /> class with the associated <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> object.</summary>
		/// <param name="adapter">An <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> object to associate with this <see cref="T:System.Data.Odbc.OdbcCommandBuilder" />. </param>
		// Token: 0x0600113C RID: 4412 RVA: 0x000434CC File Offset: 0x000416CC
		public OdbcCommandBuilder(OdbcDataAdapter adapter)
			: this()
		{
			this.DataAdapter = adapter;
		}

		/// <summary>Gets or sets an <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> object for which this <see cref="T:System.Data.Odbc.OdbcCommandBuilder" /> object will generate SQL statements.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> object that is associated with this <see cref="T:System.Data.Odbc.OdbcCommandBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x000434DC File Offset: 0x000416DC
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x000434E4 File Offset: 0x000416E4
		[DefaultValue(null)]
		[OdbcDescription("The DataAdapter for which to automatically generate OdbcCommands")]
		public new OdbcDataAdapter DataAdapter
		{
			get
			{
				return this._adapter;
			}
			set
			{
				if (this._adapter == value)
				{
					return;
				}
				if (this.rowUpdatingHandler != null)
				{
					this.rowUpdatingHandler = new OdbcRowUpdatingEventHandler(this.OnRowUpdating);
				}
				if (this._adapter != null)
				{
					this._adapter.RowUpdating -= this.rowUpdatingHandler;
				}
				this._adapter = value;
				if (this._adapter != null)
				{
					this._adapter.RowUpdating += this.rowUpdatingHandler;
				}
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x0004355C File Offset: 0x0004175C
		private OdbcCommand SelectCommand
		{
			get
			{
				if (this.DataAdapter == null)
				{
					return null;
				}
				return this.DataAdapter.SelectCommand;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x00043578 File Offset: 0x00041778
		private DataTable Schema
		{
			get
			{
				if (this._schema == null)
				{
					this.RefreshSchema();
				}
				return this._schema;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00043594 File Offset: 0x00041794
		private string TableName
		{
			get
			{
				if (this._tableName != string.Empty)
				{
					return this._tableName;
				}
				DataRow[] array = this.Schema.Select("BaseTableName is not null and BaseTableName <> ''");
				if (array.Length > 1)
				{
					string text = (string)array[0]["BaseTableName"];
					foreach (DataRow dataRow in array)
					{
						if ((string)dataRow["BaseTableName"] != text)
						{
							throw new InvalidOperationException("Dynamic SQL generation is not supported against multiple base tables.");
						}
					}
				}
				if (array.Length == 0)
				{
					throw new InvalidOperationException("Cannot determine the base table name. Cannot proceed");
				}
				this._tableName = array[0]["BaseTableName"].ToString();
				return this._tableName;
			}
		}

		/// <summary>Retrieves parameter information from the stored procedure specified in the <see cref="T:System.Data.Odbc.OdbcCommand" /> and populates the <see cref="P:System.Data.Odbc.OdbcCommand.Parameters" /> collection of the specified <see cref="T:System.Data.Odbc.OdbcCommand" /> object.</summary>
		/// <param name="command">The <see cref="T:System.Data.Odbc.OdbcCommand" /> referencing the stored procedure from which the parameter information is to be derived. The derived parameters are added to the <see cref="P:System.Data.Odbc.OdbcCommand.Parameters" /> collection of the <see cref="T:System.Data.Odbc.OdbcCommand" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The underlying ODBC driver does not support returning stored procedure parameter information, or the command text is not a valid stored procedure name, or the <see cref="T:System.Data.CommandType" /> specified was not CommandType.StoredProcedure. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001142 RID: 4418 RVA: 0x00043660 File Offset: 0x00041860
		[MonoTODO]
		public static void DeriveParameters(OdbcCommand command)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00043668 File Offset: 0x00041868
		private new void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing)
			{
				if (this._insertCommand != null)
				{
					this._insertCommand.Dispose();
				}
				if (this._updateCommand != null)
				{
					this._updateCommand.Dispose();
				}
				if (this._deleteCommand != null)
				{
					this._deleteCommand.Dispose();
				}
				if (this._schema != null)
				{
					this._schema.Dispose();
				}
				this._insertCommand = null;
				this._updateCommand = null;
				this._deleteCommand = null;
				this._schema = null;
			}
			this._disposed = true;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00043704 File Offset: 0x00041904
		private bool IsUpdatable(DataRow schemaRow)
		{
			return (schemaRow.IsNull("IsAutoIncrement") || !(bool)schemaRow["IsAutoIncrement"]) && (schemaRow.IsNull("IsRowVersion") || !(bool)schemaRow["IsRowVersion"]) && (schemaRow.IsNull("IsReadOnly") || !(bool)schemaRow["IsReadOnly"]) && !schemaRow.IsNull("BaseTableName") && ((string)schemaRow["BaseTableName"]).Length != 0;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x000437B0 File Offset: 0x000419B0
		private string GetColumnName(DataRow schemaRow)
		{
			string text = ((!schemaRow.IsNull("BaseColumnName")) ? ((string)schemaRow["BaseColumnName"]) : string.Empty);
			if (text == string.Empty)
			{
				text = ((!schemaRow.IsNull("ColumnName")) ? ((string)schemaRow["ColumnName"]) : string.Empty);
			}
			return text;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00043824 File Offset: 0x00041A24
		private OdbcParameter AddParameter(OdbcCommand cmd, string paramName, OdbcType odbcType, int length, string sourceColumnName, DataRowVersion rowVersion)
		{
			OdbcParameter odbcParameter;
			if (length >= 0 && sourceColumnName != string.Empty)
			{
				odbcParameter = cmd.Parameters.Add(paramName, odbcType, length, sourceColumnName);
			}
			else
			{
				odbcParameter = cmd.Parameters.Add(paramName, odbcType);
			}
			odbcParameter.SourceVersion = rowVersion;
			return odbcParameter;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00043878 File Offset: 0x00041A78
		private string CreateOptWhereClause(OdbcCommand command, int paramCount)
		{
			string[] array = new string[this.Schema.Rows.Count];
			int num = 0;
			foreach (object obj in this.Schema.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (this.IsUpdatable(dataRow))
				{
					string columnName = this.GetColumnName(dataRow);
					if (columnName == string.Empty)
					{
						throw new InvalidOperationException("Cannot form delete command. Column name is missing!");
					}
					bool flag = dataRow.IsNull("AllowDBNull") || (bool)dataRow["AllowDBNull"];
					OdbcType odbcType = (OdbcType)((!dataRow.IsNull("ProviderType")) ? ((int)dataRow["ProviderType"]) : 22);
					int num2 = ((!dataRow.IsNull("ColumnSize")) ? ((int)dataRow["ColumnSize"]) : (-1));
					if (flag)
					{
						array[num++] = string.Format("((? = 1 AND {0} IS NULL) OR ({0} = ?))", this.GetQuotedString(columnName));
						OdbcParameter odbcParameter = this.AddParameter(command, this.GetParameterName(++paramCount), OdbcType.Int, num2, columnName, DataRowVersion.Original);
						odbcParameter.Value = 1;
						this.AddParameter(command, this.GetParameterName(++paramCount), odbcType, num2, columnName, DataRowVersion.Original);
					}
					else
					{
						array[num++] = string.Format("({0} = ?)", this.GetQuotedString(columnName));
						this.AddParameter(command, this.GetParameterName(++paramCount), odbcType, num2, columnName, DataRowVersion.Original);
					}
				}
			}
			return string.Join(" AND ", array, 0, num);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00043A64 File Offset: 0x00041C64
		private void CreateNewCommand(ref OdbcCommand command)
		{
			OdbcCommand selectCommand = this.SelectCommand;
			if (command == null)
			{
				command = new OdbcCommand();
				command.Connection = selectCommand.Connection;
				command.CommandTimeout = selectCommand.CommandTimeout;
				command.Transaction = selectCommand.Transaction;
			}
			command.CommandType = CommandType.Text;
			command.UpdatedRowSource = UpdateRowSource.None;
			command.Parameters.Clear();
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00043ACC File Offset: 0x00041CCC
		private OdbcCommand CreateInsertCommand(bool option)
		{
			this.CreateNewCommand(ref this._insertCommand);
			string text = string.Format("INSERT INTO {0}", this.GetQuotedString(this.TableName));
			string[] array = new string[this.Schema.Rows.Count];
			string[] array2 = new string[this.Schema.Rows.Count];
			int num = 0;
			foreach (object obj in this.Schema.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (this.IsUpdatable(dataRow))
				{
					string columnName = this.GetColumnName(dataRow);
					if (columnName == string.Empty)
					{
						throw new InvalidOperationException("Cannot form insert command. Column name is missing!");
					}
					array[num] = this.GetQuotedString(columnName);
					array2[num++] = "?";
					OdbcType odbcType = (OdbcType)((!dataRow.IsNull("ProviderType")) ? ((int)dataRow["ProviderType"]) : 22);
					int num2 = ((!dataRow.IsNull("ColumnSize")) ? ((int)dataRow["ColumnSize"]) : (-1));
					this.AddParameter(this._insertCommand, this.GetParameterName(num), odbcType, num2, columnName, DataRowVersion.Current);
				}
			}
			text = string.Format("{0} ({1}) VALUES ({2})", text, string.Join(", ", array, 0, num), string.Join(", ", array2, 0, num));
			this._insertCommand.CommandText = text;
			return this._insertCommand;
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform insertions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600114A RID: 4426 RVA: 0x00043C90 File Offset: 0x00041E90
		public new OdbcCommand GetInsertCommand()
		{
			if (this._insertCommand != null)
			{
				return this._insertCommand;
			}
			if (this._schema == null)
			{
				this.RefreshSchema();
			}
			return this.CreateInsertCommand(false);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform insertions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600114B RID: 4427 RVA: 0x00043CC8 File Offset: 0x00041EC8
		public new OdbcCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			if (this._insertCommand != null)
			{
				return this._insertCommand;
			}
			if (this._schema == null)
			{
				this.RefreshSchema();
			}
			return this.CreateInsertCommand(useColumnsForParameterNames);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00043D00 File Offset: 0x00041F00
		private OdbcCommand CreateUpdateCommand(bool option)
		{
			this.CreateNewCommand(ref this._updateCommand);
			string text = string.Format("UPDATE {0} SET", this.GetQuotedString(this.TableName));
			string[] array = new string[this.Schema.Rows.Count];
			int num = 0;
			foreach (object obj in this.Schema.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (this.IsUpdatable(dataRow))
				{
					string columnName = this.GetColumnName(dataRow);
					if (columnName == string.Empty)
					{
						throw new InvalidOperationException("Cannot form update command. Column name is missing!");
					}
					OdbcType odbcType = (OdbcType)((!dataRow.IsNull("ProviderType")) ? ((int)dataRow["ProviderType"]) : 22);
					int num2 = ((!dataRow.IsNull("ColumnSize")) ? ((int)dataRow["ColumnSize"]) : (-1));
					array[num++] = string.Format("{0} = ?", this.GetQuotedString(columnName));
					this.AddParameter(this._updateCommand, this.GetParameterName(num), odbcType, num2, columnName, DataRowVersion.Current);
				}
			}
			string text2 = this.CreateOptWhereClause(this._updateCommand, num);
			text = string.Format("{0} {1} WHERE ({2})", text, string.Join(", ", array, 0, num), text2);
			this._updateCommand.CommandText = text;
			return this._updateCommand;
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform updates at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform updates.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600114D RID: 4429 RVA: 0x00043EAC File Offset: 0x000420AC
		public new OdbcCommand GetUpdateCommand()
		{
			if (this._updateCommand != null)
			{
				return this._updateCommand;
			}
			if (this._schema == null)
			{
				this.RefreshSchema();
			}
			return this.CreateUpdateCommand(false);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform updates at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform updates.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600114E RID: 4430 RVA: 0x00043EE4 File Offset: 0x000420E4
		public new OdbcCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			if (this._updateCommand != null)
			{
				return this._updateCommand;
			}
			if (this._schema == null)
			{
				this.RefreshSchema();
			}
			return this.CreateUpdateCommand(useColumnsForParameterNames);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00043F1C File Offset: 0x0004211C
		private OdbcCommand CreateDeleteCommand(bool option)
		{
			this.CreateNewCommand(ref this._deleteCommand);
			string text = string.Format("DELETE FROM {0}", this.GetQuotedString(this.TableName));
			string text2 = this.CreateOptWhereClause(this._deleteCommand, 0);
			text = string.Format("{0} WHERE ({1})", text, text2);
			this._deleteCommand.CommandText = text;
			return this._deleteCommand;
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform deletions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001150 RID: 4432 RVA: 0x00043F7C File Offset: 0x0004217C
		public new OdbcCommand GetDeleteCommand()
		{
			if (this._deleteCommand != null)
			{
				return this._deleteCommand;
			}
			if (this._schema == null)
			{
				this.RefreshSchema();
			}
			return this.CreateDeleteCommand(false);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Odbc.OdbcCommand" /> object required to perform deletions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001151 RID: 4433 RVA: 0x00043FB4 File Offset: 0x000421B4
		public new OdbcCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			if (this._deleteCommand != null)
			{
				return this._deleteCommand;
			}
			if (this._schema == null)
			{
				this.RefreshSchema();
			}
			return this.CreateDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00043FEC File Offset: 0x000421EC
		private new void RefreshSchema()
		{
			if (this.SelectCommand == null)
			{
				throw new InvalidOperationException("SelectCommand should be valid");
			}
			if (this.SelectCommand.Connection == null)
			{
				throw new InvalidOperationException("SelectCommand's Connection should be valid");
			}
			CommandBehavior commandBehavior = CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo;
			if (this.SelectCommand.Connection.State != ConnectionState.Open)
			{
				this.SelectCommand.Connection.Open();
				commandBehavior |= CommandBehavior.CloseConnection;
			}
			OdbcDataReader odbcDataReader = this.SelectCommand.ExecuteReader(commandBehavior);
			this._schema = odbcDataReader.GetSchemaTable();
			odbcDataReader.Close();
			this._insertCommand = null;
			this._updateCommand = null;
			this._deleteCommand = null;
			this._tableName = string.Empty;
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00044098 File Offset: 0x00042298
		protected override string GetParameterName(int parameterOrdinal)
		{
			return string.Format("p{0}", parameterOrdinal);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000440AC File Offset: 0x000422AC
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause)
		{
			OdbcParameter odbcParameter = (OdbcParameter)parameter;
			odbcParameter.Size = int.Parse(row["ColumnSize"].ToString());
			if (row["NumericPrecision"] != DBNull.Value)
			{
				odbcParameter.Precision = byte.Parse(row["NumericPrecision"].ToString());
			}
			if (row["NumericScale"] != DBNull.Value)
			{
				odbcParameter.Scale = byte.Parse(row["NumericScale"].ToString());
			}
			odbcParameter.DbType = (DbType)((int)row["ProviderType"]);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00044154 File Offset: 0x00042354
		protected override string GetParameterName(string parameterName)
		{
			return string.Format("@{0}", parameterName);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00044164 File Offset: 0x00042364
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return this.GetParameterName(parameterOrdinal);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00044170 File Offset: 0x00042370
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (!(adapter is OdbcDataAdapter))
			{
				throw new InvalidOperationException("Adapter needs to be a SqlDataAdapter");
			}
			if (this.rowUpdatingHandler == null)
			{
				this.rowUpdatingHandler = new OdbcRowUpdatingEventHandler(this.OnRowUpdating);
			}
			((OdbcDataAdapter)adapter).RowUpdating += this.rowUpdatingHandler;
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001158 RID: 4440 RVA: 0x000441C4 File Offset: 0x000423C4
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			return this.QuoteIdentifier(unquotedIdentifier, null);
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <param name="connection">The <see cref="T:System.Data.Odbc.OdbcConnection" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001159 RID: 4441 RVA: 0x000441D0 File Offset: 0x000423D0
		public string QuoteIdentifier(string unquotedIdentifier, OdbcConnection connection)
		{
			if (unquotedIdentifier == null)
			{
				throw new ArgumentNullException("unquotedIdentifier");
			}
			string text = this.QuotePrefix;
			string text2 = this.QuoteSuffix;
			if (this.QuotePrefix.Length == 0)
			{
				if (connection == null)
				{
					throw new InvalidOperationException("An open connection is required if QuotePrefix is not set.");
				}
				text2 = (text = this.GetQuoteCharacter(connection));
			}
			if (text.Length > 0 && text != " ")
			{
				string text3;
				if (text2.Length > 0)
				{
					text3 = unquotedIdentifier.Replace(text2, text2 + text2);
				}
				else
				{
					text3 = unquotedIdentifier;
				}
				return text + text3 + text2;
			}
			return unquotedIdentifier;
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier, including correctly unescaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes correctly unescaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <param name="connection">The <see cref="T:System.Data.Odbc.OdbcConnection" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600115A RID: 4442 RVA: 0x00044270 File Offset: 0x00042470
		public string UnquoteIdentifier(string quotedIdentifier, OdbcConnection connection)
		{
			return this.UnquoteIdentifier(quotedIdentifier);
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier, including correctly unescaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes correctly unescaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600115B RID: 4443 RVA: 0x0004427C File Offset: 0x0004247C
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			if (quotedIdentifier == null || quotedIdentifier.Length == 0)
			{
				return quotedIdentifier;
			}
			StringBuilder stringBuilder = new StringBuilder(quotedIdentifier.Length);
			stringBuilder.Append(quotedIdentifier);
			if (quotedIdentifier.StartsWith(this.QuotePrefix))
			{
				stringBuilder.Remove(0, this.QuotePrefix.Length);
			}
			if (quotedIdentifier.EndsWith(this.QuoteSuffix))
			{
				stringBuilder.Remove(stringBuilder.Length - this.QuoteSuffix.Length, this.QuoteSuffix.Length);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00044310 File Offset: 0x00042510
		private void OnRowUpdating(object sender, OdbcRowUpdatingEventArgs args)
		{
			if (args.Command != null)
			{
				return;
			}
			try
			{
				switch (args.StatementType)
				{
				case StatementType.Insert:
					args.Command = this.GetInsertCommand();
					break;
				case StatementType.Update:
					args.Command = this.GetUpdateCommand();
					break;
				case StatementType.Delete:
					args.Command = this.GetDeleteCommand();
					break;
				}
			}
			catch (Exception ex)
			{
				args.Errors = ex;
				args.Status = UpdateStatus.ErrorsOccurred;
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x000443B4 File Offset: 0x000425B4
		private string GetQuotedString(string unquotedIdentifier)
		{
			string quotePrefix = this.QuotePrefix;
			string quoteSuffix = this.QuoteSuffix;
			if (quotePrefix.Length == 0 && quoteSuffix.Length == 0)
			{
				return unquotedIdentifier;
			}
			return string.Format("{0}{1}{2}", quotePrefix, unquotedIdentifier, quoteSuffix);
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x000443F4 File Offset: 0x000425F4
		private bool IsCommandGenerated
		{
			get
			{
				return this._insertCommand != null || this._updateCommand != null || this._deleteCommand != null;
			}
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0004441C File Offset: 0x0004261C
		private string GetQuoteCharacter(OdbcConnection conn)
		{
			return conn.GetInfo(OdbcInfo.IdentifierQuoteChar);
		}

		// Token: 0x04000668 RID: 1640
		private OdbcDataAdapter _adapter;

		// Token: 0x04000669 RID: 1641
		private DataTable _schema;

		// Token: 0x0400066A RID: 1642
		private string _tableName;

		// Token: 0x0400066B RID: 1643
		private OdbcCommand _insertCommand;

		// Token: 0x0400066C RID: 1644
		private OdbcCommand _updateCommand;

		// Token: 0x0400066D RID: 1645
		private OdbcCommand _deleteCommand;

		// Token: 0x0400066E RID: 1646
		private bool _disposed;

		// Token: 0x0400066F RID: 1647
		private OdbcRowUpdatingEventHandler rowUpdatingHandler;
	}
}
