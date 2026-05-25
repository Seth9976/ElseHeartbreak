using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace System.Data.Common
{
	/// <summary>Automatically generates single-table commands used to reconcile changes made to a <see cref="T:System.Data.DataSet" /> with the associated database. This is an abstract class that can only be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B1 RID: 177
	public abstract class DbCommandBuilder : Component
	{
		// Token: 0x06000804 RID: 2052 RVA: 0x00025C58 File Offset: 0x00023E58
		private void BuildCache(bool closeConnection)
		{
			DbCommand sourceCommand = this.SourceCommand;
			if (sourceCommand == null)
			{
				throw new InvalidOperationException("The DataAdapter.SelectCommand property needs to be initialized.");
			}
			DbConnection connection = sourceCommand.Connection;
			if (connection == null)
			{
				throw new InvalidOperationException("The DataAdapter.SelectCommand.Connection property needs to be initialized.");
			}
			if (this._dbSchemaTable == null)
			{
				if (connection.State == ConnectionState.Open)
				{
					closeConnection = false;
				}
				else
				{
					connection.Open();
				}
				DbDataReader dbDataReader = sourceCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
				this._dbSchemaTable = dbDataReader.GetSchemaTable();
				dbDataReader.Close();
				if (closeConnection)
				{
					connection.Close();
				}
				this.BuildInformation(this._dbSchemaTable);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00025CEC File Offset: 0x00023EEC
		private string QuotedTableName
		{
			get
			{
				return this.GetQuotedString(this._tableName);
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00025CFC File Offset: 0x00023EFC
		private bool IsCommandGenerated
		{
			get
			{
				return this._insertCommand != null || this._updateCommand != null || this._deleteCommand != null;
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00025D24 File Offset: 0x00023F24
		private string GetQuotedString(string value)
		{
			if (value == string.Empty || value == null)
			{
				return value;
			}
			string quotePrefix = this.QuotePrefix;
			string quoteSuffix = this.QuoteSuffix;
			if (quotePrefix.Length == 0 && quoteSuffix.Length == 0)
			{
				return value;
			}
			return string.Format("{0}{1}{2}", quotePrefix, value, quoteSuffix);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00025D7C File Offset: 0x00023F7C
		private void BuildInformation(DataTable schemaTable)
		{
			this._tableName = string.Empty;
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (!dataRow.IsNull("BaseTableName") && !((string)dataRow["BaseTableName"] == string.Empty))
				{
					if (this._tableName == string.Empty)
					{
						this._tableName = (string)dataRow["BaseTableName"];
					}
					else if (this._tableName != (string)dataRow["BaseTableName"])
					{
						throw new InvalidOperationException("Dynamic SQL generation is not supported against multiple base tables.");
					}
				}
			}
			if (this._tableName == string.Empty)
			{
				throw new InvalidOperationException("Dynamic SQL generation is not supported with no base table.");
			}
			this._dbSchemaTable = schemaTable;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00025EA8 File Offset: 0x000240A8
		private bool IncludedInInsert(DataRow schemaRow)
		{
			return (schemaRow.IsNull("IsAutoIncrement") || !(bool)schemaRow["IsAutoIncrement"]) && (schemaRow.IsNull("IsExpression") || !(bool)schemaRow["IsExpression"]) && (schemaRow.IsNull("IsRowVersion") || !(bool)schemaRow["IsRowVersion"]) && (schemaRow.IsNull("IsReadOnly") || !(bool)schemaRow["IsReadOnly"]);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00025F54 File Offset: 0x00024154
		private bool IncludedInUpdate(DataRow schemaRow)
		{
			return (schemaRow.IsNull("IsAutoIncrement") || !(bool)schemaRow["IsAutoIncrement"]) && (schemaRow.IsNull("IsRowVersion") || !(bool)schemaRow["IsRowVersion"]) && (schemaRow.IsNull("IsExpression") || !(bool)schemaRow["IsExpression"]) && (schemaRow.IsNull("IsReadOnly") || !(bool)schemaRow["IsReadOnly"]);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00026000 File Offset: 0x00024200
		private bool IncludedInWhereClause(DataRow schemaRow)
		{
			return !(bool)schemaRow["IsLong"];
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002601C File Offset: 0x0002421C
		private DbCommand CreateDeleteCommand(bool option)
		{
			if (this.QuotedTableName == string.Empty)
			{
				return null;
			}
			this.CreateNewCommand(ref this._deleteCommand);
			string text = string.Format("DELETE FROM {0}", this.QuotedTableName);
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			int num = 1;
			foreach (object obj in this._dbSchemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.IsNull("IsExpression") || !(bool)dataRow["IsExpression"])
				{
					if (this.IncludedInWhereClause(dataRow))
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(" AND ");
						}
						bool flag2 = (bool)dataRow["IsKey"];
						if (flag2)
						{
							flag = true;
						}
						bool flag3 = (bool)dataRow["AllowDBNull"];
						DbParameter dbParameter;
						if (!flag2 && flag3)
						{
							dbParameter = this._deleteCommand.CreateParameter();
							if (option)
							{
								dbParameter.ParameterName = string.Format("@IsNull_{0}", dataRow["BaseColumnName"]);
							}
							else
							{
								dbParameter.ParameterName = string.Format("@p{0}", num++);
							}
							dbParameter.Value = 1;
							dbParameter.DbType = DbType.Int32;
							string text2 = (string)dataRow["BaseColumnName"];
							dbParameter.SourceColumn = text2;
							dbParameter.SourceColumnNullMapping = true;
							dbParameter.SourceVersion = DataRowVersion.Original;
							this._deleteCommand.Parameters.Add(dbParameter);
							stringBuilder.Append("(");
							stringBuilder.Append(string.Format(DbCommandBuilder.clause1, dbParameter.ParameterName, this.GetQuotedString(text2)));
							stringBuilder.Append(" OR ");
						}
						if (option)
						{
							dbParameter = this.CreateParameter(this._deleteCommand, dataRow, true);
						}
						else
						{
							dbParameter = this.CreateParameter(this._deleteCommand, num++, dataRow);
						}
						dbParameter.SourceVersion = DataRowVersion.Original;
						this.ApplyParameterInfo(dbParameter, dataRow, StatementType.Delete, true);
						stringBuilder.Append(string.Format(DbCommandBuilder.clause2, this.GetQuotedString(dbParameter.SourceColumn), dbParameter.ParameterName));
						if (!flag2 && flag3)
						{
							stringBuilder.Append(")");
						}
					}
				}
			}
			if (!flag)
			{
				throw new InvalidOperationException("Dynamic SQL generation for the DeleteCommand is not supported against a SelectCommand that does not return any key column information.");
			}
			string text3 = string.Format("{0} WHERE ({1})", text, stringBuilder.ToString());
			this._deleteCommand.CommandText = text3;
			this._dbCommand = this._deleteCommand;
			return this._deleteCommand;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00026314 File Offset: 0x00024514
		private DbCommand CreateInsertCommand(bool option, DataRow row)
		{
			if (this.QuotedTableName == string.Empty)
			{
				return null;
			}
			this.CreateNewCommand(ref this._insertCommand);
			string text = string.Format("INSERT INTO {0}", this.QuotedTableName);
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			int num = 1;
			foreach (object obj in this._dbSchemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (this.IncludedInInsert(dataRow))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
						stringBuilder2.Append(", ");
					}
					DbParameter dbParameter;
					if (option)
					{
						dbParameter = this.CreateParameter(this._insertCommand, dataRow, false);
					}
					else
					{
						dbParameter = this.CreateParameter(this._insertCommand, num++, dataRow);
					}
					dbParameter.SourceVersion = DataRowVersion.Current;
					this.ApplyParameterInfo(dbParameter, dataRow, StatementType.Insert, false);
					stringBuilder.Append(this.GetQuotedString(dbParameter.SourceColumn));
					string text2 = dataRow["ColumnName"] as string;
					if (!(!dataRow.IsNull("AllowDBNull") & (bool)dataRow["AllowDBNull"]) && row != null && (row[text2] == DBNull.Value || row[text2] == null))
					{
						stringBuilder2.Append("DEFAULT");
					}
					else
					{
						stringBuilder2.Append(dbParameter.ParameterName);
					}
				}
			}
			string text3 = string.Format("{0} ({1}) VALUES ({2})", text, stringBuilder.ToString(), stringBuilder2.ToString());
			this._insertCommand.CommandText = text3;
			this._dbCommand = this._insertCommand;
			return this._insertCommand;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0002651C File Offset: 0x0002471C
		private void CreateNewCommand(ref DbCommand command)
		{
			DbCommand sourceCommand = this.SourceCommand;
			if (command == null)
			{
				command = sourceCommand.Connection.CreateCommand();
				command.CommandTimeout = sourceCommand.CommandTimeout;
				command.Transaction = sourceCommand.Transaction;
			}
			command.CommandType = CommandType.Text;
			command.UpdatedRowSource = UpdateRowSource.None;
			command.Parameters.Clear();
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0002657C File Offset: 0x0002477C
		private DbCommand CreateUpdateCommand(bool option)
		{
			if (this.QuotedTableName == string.Empty)
			{
				return null;
			}
			this.CreateNewCommand(ref this._updateCommand);
			string text = string.Format("UPDATE {0} SET ", this.QuotedTableName);
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			int num = 1;
			bool flag = false;
			foreach (object obj in this._dbSchemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (this.IncludedInUpdate(dataRow))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					DbParameter dbParameter;
					if (option)
					{
						dbParameter = this.CreateParameter(this._updateCommand, dataRow, false);
					}
					else
					{
						dbParameter = this.CreateParameter(this._updateCommand, num++, dataRow);
					}
					dbParameter.SourceVersion = DataRowVersion.Current;
					this.ApplyParameterInfo(dbParameter, dataRow, StatementType.Update, false);
					stringBuilder.Append(string.Format("{0} = {1}", this.GetQuotedString(dbParameter.SourceColumn), dbParameter.ParameterName));
				}
			}
			foreach (object obj2 in this._dbSchemaTable.Rows)
			{
				DataRow dataRow2 = (DataRow)obj2;
				if (dataRow2.IsNull("IsExpression") || !(bool)dataRow2["IsExpression"])
				{
					if (this.IncludedInWhereClause(dataRow2))
					{
						if (stringBuilder2.Length > 0)
						{
							stringBuilder2.Append(" AND ");
						}
						bool flag2 = (bool)dataRow2["IsKey"];
						if (flag2)
						{
							flag = true;
						}
						bool flag3 = (bool)dataRow2["AllowDBNull"];
						DbParameter dbParameter;
						if (!flag2 && flag3)
						{
							dbParameter = this._updateCommand.CreateParameter();
							if (option)
							{
								dbParameter.ParameterName = string.Format("@IsNull_{0}", dataRow2["BaseColumnName"]);
							}
							else
							{
								dbParameter.ParameterName = string.Format("@p{0}", num++);
							}
							dbParameter.DbType = DbType.Int32;
							dbParameter.Value = 1;
							dbParameter.SourceColumn = (string)dataRow2["BaseColumnName"];
							dbParameter.SourceColumnNullMapping = true;
							dbParameter.SourceVersion = DataRowVersion.Original;
							stringBuilder2.Append("(");
							stringBuilder2.Append(string.Format(DbCommandBuilder.clause1, dbParameter.ParameterName, this.GetQuotedString((string)dataRow2["BaseColumnName"])));
							stringBuilder2.Append(" OR ");
							this._updateCommand.Parameters.Add(dbParameter);
						}
						if (option)
						{
							dbParameter = this.CreateParameter(this._updateCommand, dataRow2, true);
						}
						else
						{
							dbParameter = this.CreateParameter(this._updateCommand, num++, dataRow2);
						}
						dbParameter.SourceVersion = DataRowVersion.Original;
						this.ApplyParameterInfo(dbParameter, dataRow2, StatementType.Update, true);
						stringBuilder2.Append(string.Format(DbCommandBuilder.clause2, this.GetQuotedString(dbParameter.SourceColumn), dbParameter.ParameterName));
						if (!flag2 && flag3)
						{
							stringBuilder2.Append(")");
						}
					}
				}
			}
			if (!flag)
			{
				throw new InvalidOperationException("Dynamic SQL generation for the UpdateCommand is not supported against a SelectCommand that does not return any key column information.");
			}
			string text2 = string.Format("{0}{1} WHERE ({2})", text, stringBuilder.ToString(), stringBuilder2.ToString());
			this._updateCommand.CommandText = text2;
			this._dbCommand = this._updateCommand;
			return this._updateCommand;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00026988 File Offset: 0x00024B88
		private DbParameter CreateParameter(DbCommand _dbCommand, DataRow schemaRow, bool whereClause)
		{
			string text = (string)schemaRow["BaseColumnName"];
			DbParameter dbParameter = _dbCommand.CreateParameter();
			if (whereClause)
			{
				dbParameter.ParameterName = this.GetParameterName("Original_" + text);
			}
			else
			{
				dbParameter.ParameterName = this.GetParameterName(text);
			}
			dbParameter.SourceColumn = text;
			_dbCommand.Parameters.Add(dbParameter);
			return dbParameter;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000269F4 File Offset: 0x00024BF4
		private DbParameter CreateParameter(DbCommand _dbCommand, int paramIndex, DataRow schemaRow)
		{
			string text = (string)schemaRow["BaseColumnName"];
			DbParameter dbParameter = _dbCommand.CreateParameter();
			dbParameter.ParameterName = this.GetParameterName(paramIndex);
			dbParameter.SourceColumn = text;
			_dbCommand.Parameters.Add(dbParameter);
			return dbParameter;
		}

		/// <summary>Sets or gets the <see cref="T:System.Data.Common.CatalogLocation" /> for an instance of the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.</summary>
		/// <returns>A <see cref="T:System.Data.Common.CatalogLocation" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00026A3C File Offset: 0x00024C3C
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x00026A44 File Offset: 0x00024C44
		[DefaultValue(CatalogLocation.Start)]
		public virtual CatalogLocation CatalogLocation
		{
			get
			{
				return this._catalogLocation;
			}
			set
			{
				DbCommandBuilder.CheckEnumValue(typeof(CatalogLocation), (int)value);
				this._catalogLocation = value;
			}
		}

		/// <summary>Sets or gets a string used as the catalog separator for an instance of the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.</summary>
		/// <returns>A string indicating the catalog separator for use with an instance of the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00026A60 File Offset: 0x00024C60
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x00026A8C File Offset: 0x00024C8C
		[DefaultValue(".")]
		public virtual string CatalogSeparator
		{
			get
			{
				if (this._catalogSeparator == null || this._catalogSeparator.Length == 0)
				{
					return DbCommandBuilder.SEPARATOR_DEFAULT;
				}
				return this._catalogSeparator;
			}
			set
			{
				this._catalogSeparator = value;
			}
		}

		/// <summary>Specifies which <see cref="T:System.Data.ConflictOption" /> is to be used by the <see cref="T:System.Data.Common.DbCommandBuilder" />.</summary>
		/// <returns>Returns one of the <see cref="T:System.Data.ConflictOption" /> values describing the behavior of this <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00026A98 File Offset: 0x00024C98
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x00026AA0 File Offset: 0x00024CA0
		[DefaultValue(ConflictOption.CompareAllSearchableValues)]
		public virtual ConflictOption ConflictOption
		{
			get
			{
				return this._conflictOption;
			}
			set
			{
				DbCommandBuilder.CheckEnumValue(typeof(ConflictOption), (int)value);
				this._conflictOption = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.Common.DbDataAdapter" /> object for which Transact-SQL statements are automatically generated.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataAdapter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00026ABC File Offset: 0x00024CBC
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x00026AC4 File Offset: 0x00024CC4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbDataAdapter DataAdapter
		{
			get
			{
				return this._dbDataAdapter;
			}
			set
			{
				if (value != null)
				{
					this.SetRowUpdatingHandler(value);
				}
				this._dbDataAdapter = value;
			}
		}

		/// <summary>Gets or sets the beginning character or characters to use when specifying database objects (for example, tables or columns) whose names contain characters such as spaces or reserved tokens.</summary>
		/// <returns>The beginning character or characters to use. The default is an empty string.</returns>
		/// <exception cref="T:System.InvalidOperationException">This property cannot be changed after an insert, update, or delete command has been generated. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00026ADC File Offset: 0x00024CDC
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x00026AF8 File Offset: 0x00024CF8
		[DefaultValue("")]
		public virtual string QuotePrefix
		{
			get
			{
				if (this._quotePrefix == null)
				{
					return string.Empty;
				}
				return this._quotePrefix;
			}
			set
			{
				if (this.IsCommandGenerated)
				{
					throw new InvalidOperationException("QuotePrefix cannot be set after an Insert, Update or Delete command has been generated.");
				}
				this._quotePrefix = value;
			}
		}

		/// <summary>Gets or sets the beginning character or characters to use when specifying database objects (for example, tables or columns) whose names contain characters such as spaces or reserved tokens.</summary>
		/// <returns>The ending character or characters to use. The default is an empty string.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00026B18 File Offset: 0x00024D18
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x00026B34 File Offset: 0x00024D34
		[DefaultValue("")]
		public virtual string QuoteSuffix
		{
			get
			{
				if (this._quoteSuffix == null)
				{
					return string.Empty;
				}
				return this._quoteSuffix;
			}
			set
			{
				if (this.IsCommandGenerated)
				{
					throw new InvalidOperationException("QuoteSuffix cannot be set after an Insert, Update or Delete command has been generated.");
				}
				this._quoteSuffix = value;
			}
		}

		/// <summary>Gets or sets the character to be used for the separator between the schema identifier and any other identifiers.</summary>
		/// <returns>The character to be used as the schema separator.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00026B54 File Offset: 0x00024D54
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x00026B80 File Offset: 0x00024D80
		[DefaultValue(".")]
		public virtual string SchemaSeparator
		{
			get
			{
				if (this._schemaSeparator == null || this._schemaSeparator.Length == 0)
				{
					return DbCommandBuilder.SEPARATOR_DEFAULT;
				}
				return this._schemaSeparator;
			}
			set
			{
				this._schemaSeparator = value;
			}
		}

		/// <summary>Specifies whether all column values in an update statement are included or only changed ones.</summary>
		/// <returns>true if the UPDATE statement generated by the <see cref="T:System.Data.Common.DbCommandBuilder" /> includes all columns; false if it includes only changed columns.</returns>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00026B8C File Offset: 0x00024D8C
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x00026B94 File Offset: 0x00024D94
		[DefaultValue(false)]
		public bool SetAllValues
		{
			get
			{
				return this._setAllValues;
			}
			set
			{
				this._setAllValues = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00026BA0 File Offset: 0x00024DA0
		private DbCommand SourceCommand
		{
			get
			{
				if (this._dbDataAdapter != null)
				{
					return this._dbDataAdapter.SelectCommand;
				}
				return null;
			}
		}

		/// <summary>Allows the provider implementation of the <see cref="T:System.Data.Common.DbCommandBuilder" /> class to handle additional parameter properties.</summary>
		/// <param name="parameter">A <see cref="T:System.Data.Common.DbParameter" /> to which the additional modifications are applied. </param>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> from the schema table provided by <see cref="M:System.Data.Common.DbDataReader.GetSchemaTable" />. </param>
		/// <param name="statementType">The type of command being generated; INSERT, UPDATE or DELETE. </param>
		/// <param name="whereClause">true if the parameter is part of the update or delete WHERE clause, false if it is part of the insert or update values. </param>
		// Token: 0x06000823 RID: 2083
		protected abstract void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause);

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbCommandBuilder" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06000824 RID: 2084 RVA: 0x00026BBC File Offset: 0x00024DBC
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					if (this._insertCommand != null)
					{
						this._insertCommand.Dispose();
					}
					if (this._deleteCommand != null)
					{
						this._deleteCommand.Dispose();
					}
					if (this._updateCommand != null)
					{
						this._updateCommand.Dispose();
					}
					if (this._dbSchemaTable != null)
					{
						this._dbSchemaTable.Dispose();
					}
				}
				this._disposed = true;
			}
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform deletions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000825 RID: 2085 RVA: 0x00026C3C File Offset: 0x00024E3C
		public DbCommand GetDeleteCommand()
		{
			return this.GetDeleteCommand(false);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform deletions at the data source, optionally using columns for parameter names.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform deletions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000826 RID: 2086 RVA: 0x00026C48 File Offset: 0x00024E48
		public DbCommand GetDeleteCommand(bool option)
		{
			this.BuildCache(true);
			if (this._deleteCommand == null || option)
			{
				return this.CreateDeleteCommand(option);
			}
			return this._deleteCommand;
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform insertions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000827 RID: 2087 RVA: 0x00026C7C File Offset: 0x00024E7C
		public DbCommand GetInsertCommand()
		{
			return this.GetInsertCommand(false, null);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform insertions at the data source, optionally using columns for parameter names.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform insertions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000828 RID: 2088 RVA: 0x00026C88 File Offset: 0x00024E88
		public DbCommand GetInsertCommand(bool option)
		{
			return this.GetInsertCommand(option, null);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00026C94 File Offset: 0x00024E94
		internal DbCommand GetInsertCommand(bool option, DataRow row)
		{
			this.BuildCache(true);
			if (this._insertCommand == null || option)
			{
				return this.CreateInsertCommand(option, row);
			}
			return this._insertCommand;
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform updates at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform updates.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600082A RID: 2090 RVA: 0x00026CC0 File Offset: 0x00024EC0
		public DbCommand GetUpdateCommand()
		{
			return this.GetUpdateCommand(false);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform updates at the data source, optionally using columns for parameter names.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.Common.DbCommand" /> object required to perform updates.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600082B RID: 2091 RVA: 0x00026CCC File Offset: 0x00024ECC
		public DbCommand GetUpdateCommand(bool option)
		{
			this.BuildCache(true);
			if (this._updateCommand == null || option)
			{
				return this.CreateUpdateCommand(option);
			}
			return this._updateCommand;
		}

		/// <summary>Resets the <see cref="P:System.Data.Common.DbCommand.CommandTimeout" />, <see cref="P:System.Data.Common.DbCommand.Transaction" />, <see cref="P:System.Data.Common.DbCommand.CommandType" />, and <see cref="T:System.Data.UpdateRowSource" /> properties on the <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbCommand" /> instance to use for each insert, update, or delete operation. Passing a null value allows the <see cref="M:System.Data.Common.DbCommandBuilder.InitializeCommand(System.Data.Common.DbCommand)" /> method to create a <see cref="T:System.Data.Common.DbCommand" /> object based on the Select command associated with the <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <param name="command">The <see cref="T:System.Data.Common.DbCommand" /> to be used by the command builder for the corresponding insert, update, or delete command.</param>
		// Token: 0x0600082C RID: 2092 RVA: 0x00026D00 File Offset: 0x00024F00
		protected virtual DbCommand InitializeCommand(DbCommand command)
		{
			if (this._dbCommand == null)
			{
				this._dbCommand = this.SourceCommand;
			}
			else
			{
				this._dbCommand.CommandTimeout = 30;
				this._dbCommand.Transaction = null;
				this._dbCommand.CommandType = CommandType.Text;
				this._dbCommand.UpdatedRowSource = UpdateRowSource.None;
			}
			return this._dbCommand;
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier, including properly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are properly escaped.</returns>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600082D RID: 2093 RVA: 0x00026D60 File Offset: 0x00024F60
		public virtual string QuoteIdentifier(string unquotedIdentifier)
		{
			throw new NotSupportedException();
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier, including properly un-escaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes properly un-escaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600082E RID: 2094 RVA: 0x00026D68 File Offset: 0x00024F68
		public virtual string UnquoteIdentifier(string quotedIdentifier)
		{
			if (quotedIdentifier == null)
			{
				throw new ArgumentNullException("Quoted identifier parameter cannot be null");
			}
			string text = quotedIdentifier.Trim();
			if (text.StartsWith(this.QuotePrefix))
			{
				text = text.Remove(0, 1);
			}
			if (text.EndsWith(this.QuoteSuffix))
			{
				text = text.Remove(text.Length - 1, 1);
			}
			return text;
		}

		/// <summary>Clears the commands associated with this <see cref="T:System.Data.Common.DbCommandBuilder" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600082F RID: 2095 RVA: 0x00026DCC File Offset: 0x00024FCC
		public virtual void RefreshSchema()
		{
			this._tableName = string.Empty;
			this._dbSchemaTable = null;
			this._deleteCommand = null;
			this._updateCommand = null;
			this._insertCommand = null;
		}

		/// <summary>Adds an event handler for the <see cref="E:System.Data.OleDb.OleDbDataAdapter.RowUpdating" /> event.</summary>
		/// <param name="rowUpdatingEvent">A <see cref="T:System.Data.Common.RowUpdatingEventArgs" /> instance containing information about the event.</param>
		// Token: 0x06000830 RID: 2096 RVA: 0x00026DF8 File Offset: 0x00024FF8
		protected void RowUpdatingHandler(RowUpdatingEventArgs args)
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
					args.Command = this.GetInsertCommand(false, args.Row);
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

		/// <summary>Returns the name of the specified parameter in the format of @p#. Use when building a custom command builder.</summary>
		/// <returns>The name of the parameter with the specified number appended as part of the parameter name.</returns>
		/// <param name="parameterOrdinal">The number to be included as part of the parameter's name..</param>
		// Token: 0x06000831 RID: 2097
		protected abstract string GetParameterName(int parameterOrdinal);

		/// <summary>Returns the full parameter name, given the partial parameter name.</summary>
		/// <returns>The full parameter name corresponding to the partial parameter name requested.</returns>
		/// <param name="parameterName">The partial name of the parameter.</param>
		// Token: 0x06000832 RID: 2098
		protected abstract string GetParameterName(string parameterName);

		/// <summary>Returns the placeholder for the parameter in the associated SQL statement.</summary>
		/// <returns>The name of the parameter with the specified number appended.</returns>
		/// <param name="parameterOrdinal">The number to be included as part of the parameter's name.</param>
		// Token: 0x06000833 RID: 2099
		protected abstract string GetParameterPlaceholder(int parameterOrdinal);

		/// <summary>Registers the <see cref="T:System.Data.Common.DbCommandBuilder" /> to handle the <see cref="E:System.Data.OleDb.OleDbDataAdapter.RowUpdating" /> event for a <see cref="T:System.Data.Common.DbDataAdapter" />. </summary>
		/// <param name="adapter">The <see cref="T:System.Data.Common.DbDataAdapter" /> to be used for the update.</param>
		// Token: 0x06000834 RID: 2100
		protected abstract void SetRowUpdatingHandler(DbDataAdapter adapter);

		/// <summary>Returns the schema table for the <see cref="T:System.Data.Common.DbCommandBuilder" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that represents the schema for the specific <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <param name="sourceCommand">The <see cref="T:System.Data.Common.DbCommand" /> for which to retrieve the corresponding schema table.</param>
		// Token: 0x06000835 RID: 2101 RVA: 0x00026EA0 File Offset: 0x000250A0
		protected virtual DataTable GetSchemaTable(DbCommand cmd)
		{
			DataTable schemaTable;
			using (DbDataReader dbDataReader = cmd.ExecuteReader())
			{
				schemaTable = dbDataReader.GetSchemaTable();
			}
			return schemaTable;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00026EF0 File Offset: 0x000250F0
		private static void CheckEnumValue(Type type, int value)
		{
			if (Enum.IsDefined(type, value))
			{
				return;
			}
			string name = type.Name;
			string text = string.Format(CultureInfo.CurrentCulture, "Value {0} is not valid for {1}.", new object[] { value, name });
			throw new ArgumentOutOfRangeException(name, text);
		}

		// Token: 0x040002FE RID: 766
		private bool _setAllValues;

		// Token: 0x040002FF RID: 767
		private bool _disposed;

		// Token: 0x04000300 RID: 768
		private DataTable _dbSchemaTable;

		// Token: 0x04000301 RID: 769
		private DbDataAdapter _dbDataAdapter;

		// Token: 0x04000302 RID: 770
		private CatalogLocation _catalogLocation = CatalogLocation.Start;

		// Token: 0x04000303 RID: 771
		private ConflictOption _conflictOption = ConflictOption.CompareAllSearchableValues;

		// Token: 0x04000304 RID: 772
		private string _tableName;

		// Token: 0x04000305 RID: 773
		private string _catalogSeparator;

		// Token: 0x04000306 RID: 774
		private string _quotePrefix;

		// Token: 0x04000307 RID: 775
		private string _quoteSuffix;

		// Token: 0x04000308 RID: 776
		private string _schemaSeparator;

		// Token: 0x04000309 RID: 777
		private DbCommand _dbCommand;

		// Token: 0x0400030A RID: 778
		private DbCommand _deleteCommand;

		// Token: 0x0400030B RID: 779
		private DbCommand _insertCommand;

		// Token: 0x0400030C RID: 780
		private DbCommand _updateCommand;

		// Token: 0x0400030D RID: 781
		private static readonly string SEPARATOR_DEFAULT = ".";

		// Token: 0x0400030E RID: 782
		private static readonly string clause1 = "({0} = 1 AND {1} IS NULL)";

		// Token: 0x0400030F RID: 783
		private static readonly string clause2 = "({0} = {1})";
	}
}
