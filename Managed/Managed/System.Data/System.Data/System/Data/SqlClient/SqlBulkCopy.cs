using System;
using System.Collections;
using Mono.Data.Tds.Protocol;

namespace System.Data.SqlClient
{
	/// <summary>Lets you efficiently bulk load a SQL Server table with data from another source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017A RID: 378
	public sealed class SqlBulkCopy : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class using the specified open instance of <see cref="T:System.Data.SqlClient.SqlConnection" />. </summary>
		/// <param name="connection">The already open <see cref="T:System.Data.SqlClient.SqlConnection" /> instance that will be used to perform the bulk copy operation.</param>
		// Token: 0x06001439 RID: 5177 RVA: 0x000548D8 File Offset: 0x00052AD8
		public SqlBulkCopy(SqlConnection connection)
		{
			this.connection = connection;
		}

		/// <summary>Initializes and opens a new instance of <see cref="T:System.Data.SqlClient.SqlConnection" /> based on the supplied <paramref name="connectionString" />. The constructor uses the <see cref="T:System.Data.SqlClient.SqlConnection" /> to initialize a new instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class.</summary>
		/// <param name="connectionString">The string defining the connection that will be opened for use by the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance.</param>
		// Token: 0x0600143A RID: 5178 RVA: 0x000548F4 File Offset: 0x00052AF4
		public SqlBulkCopy(string connectionString)
		{
			this.connection = new SqlConnection(connectionString);
			this.isLocalConnection = true;
		}

		/// <summary>Initializes and opens a new instance of <see cref="T:System.Data.SqlClient.SqlConnection" /> based on the supplied <paramref name="connectionString" />. The constructor uses that <see cref="T:System.Data.SqlClient.SqlConnection" /> to initialize a new instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class. The <see cref="T:System.Data.SqlClient.SqlConnection" /> instance behaves according to options supplied in the <paramref name="copyOptions" /> parameter.</summary>
		/// <param name="connectionString">The string defining the connection that will be opened for use by the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance.</param>
		/// <param name="copyOptions">A combination of values from the <see cref="T:System.Data.SqlClient.SqlBulkCopyOptions" />  enumeration that determines which data source rows are copied to the destination table.</param>
		// Token: 0x0600143B RID: 5179 RVA: 0x00054928 File Offset: 0x00052B28
		[MonoTODO]
		public SqlBulkCopy(string connectionString, SqlBulkCopyOptions copyOptions)
		{
			this.connection = new SqlConnection(connectionString);
			this.copyOptions = copyOptions;
			this.isLocalConnection = true;
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class using the supplied existing open instance of <see cref="T:System.Data.SqlClient.SqlConnection" />. The <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance behaves according to options supplied in the <paramref name="copyOptions" /> parameter. If a non-null <see cref="T:System.Data.SqlClient.SqlTransaction" /> is supplied, the copy operations will be performed within that transaction.</summary>
		/// <param name="connection">The already open <see cref="T:System.Data.SqlClient.SqlConnection" /> instance that will be used to perform the bulk copy. </param>
		/// <param name="copyOptions">A combination of values from the <see cref="T:System.Data.SqlClient.SqlBulkCopyOptions" />  enumeration that determines which data source rows are copied to the destination table.</param>
		/// <param name="externalTransaction">An existing <see cref="T:System.Data.SqlClient.SqlTransaction" /> instance under which the bulk copy will occur.</param>
		// Token: 0x0600143C RID: 5180 RVA: 0x00054968 File Offset: 0x00052B68
		[MonoTODO]
		public SqlBulkCopy(SqlConnection connection, SqlBulkCopyOptions copyOptions, SqlTransaction externalTransaction)
		{
			this.connection = connection;
			this.copyOptions = copyOptions;
			throw new NotImplementedException();
		}

		/// <summary>Occurs every time that the number of rows specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.NotifyAfter" /> property have been processed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x0600143D RID: 5181 RVA: 0x0005499C File Offset: 0x00052B9C
		// (remove) Token: 0x0600143E RID: 5182 RVA: 0x000549B8 File Offset: 0x00052BB8
		public event SqlRowsCopiedEventHandler SqlRowsCopied;

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> class.</summary>
		// Token: 0x0600143F RID: 5183 RVA: 0x000549D4 File Offset: 0x00052BD4
		void IDisposable.Dispose()
		{
			if (this.isLocalConnection)
			{
				this.Close();
				this.connection = null;
			}
		}

		/// <summary>Number of rows in each batch. At the end of each batch, the rows in the batch are sent to the server.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopy.BatchSize" /> property, or zero if no value has been set.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x000549F0 File Offset: 0x00052BF0
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x000549F8 File Offset: 0x00052BF8
		public int BatchSize
		{
			get
			{
				return this._batchSize;
			}
			set
			{
				this._batchSize = value;
			}
		}

		/// <summary>Number of seconds for the operation to complete before it times out. </summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopy.BulkCopyTimeout" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00054A04 File Offset: 0x00052C04
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x00054A0C File Offset: 0x00052C0C
		public int BulkCopyTimeout
		{
			get
			{
				return this._bulkCopyTimeout;
			}
			set
			{
				this._bulkCopyTimeout = value;
			}
		}

		/// <summary>Returns a collection of <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> items. Column mappings define the relationships between columns in the data source and columns in the destination.</summary>
		/// <returns>A collection of column mappings. By default, it is an empty collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00054A18 File Offset: 0x00052C18
		public SqlBulkCopyColumnMappingCollection ColumnMappings
		{
			get
			{
				return this._columnMappingCollection;
			}
		}

		/// <summary>Name of the destination table on the server. </summary>
		/// <returns>The string value of the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property, or null if none as been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00054A20 File Offset: 0x00052C20
		// (set) Token: 0x06001446 RID: 5190 RVA: 0x00054A28 File Offset: 0x00052C28
		public string DestinationTableName
		{
			get
			{
				return this._destinationTableName;
			}
			set
			{
				this._destinationTableName = value;
			}
		}

		/// <summary>Defines the number of rows to be processed before generating a notification event.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopy.NotifyAfter" /> property, or zero if the property has not been set.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x00054A34 File Offset: 0x00052C34
		// (set) Token: 0x06001448 RID: 5192 RVA: 0x00054A3C File Offset: 0x00052C3C
		public int NotifyAfter
		{
			get
			{
				return this._notifyAfter;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("NotifyAfter should be greater than or equal to 0");
				}
				this._notifyAfter = value;
			}
		}

		/// <summary>Closes the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001449 RID: 5193 RVA: 0x00054A58 File Offset: 0x00052C58
		public void Close()
		{
			if (this.sqlRowsCopied)
			{
				throw new InvalidOperationException("Close should not be called from SqlRowsCopied event");
			}
			if (this.connection == null || this.connection.State == ConnectionState.Closed)
			{
				return;
			}
			this.connection.Close();
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00054A98 File Offset: 0x00052C98
		private DataTable[] GetColumnMetaData()
		{
			DataTable[] array = new DataTable[2];
			SqlCommand sqlCommand = new SqlCommand(string.Concat(new string[] { "select @@trancount; set fmtonly on select * from ", this.DestinationTableName, " set fmtonly off;exec sp_tablecollations_90 '", this.DestinationTableName, "'" }), this.connection);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			int num = 0;
			do
			{
				if (num == 1)
				{
					array[num - 1] = sqlDataReader.GetSchemaTable();
				}
				else if (num == 2)
				{
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
					sqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
					array[num - 1] = new DataTable();
					sqlDataAdapter.FillInternal(array[num - 1], sqlDataReader);
				}
				num++;
			}
			while (!sqlDataReader.IsClosed && sqlDataReader.NextResult());
			sqlDataReader.Close();
			return array;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00054B5C File Offset: 0x00052D5C
		private string GenerateColumnMetaData(SqlCommand tmpCmd, DataTable colMetaData, DataTable tableCollations)
		{
			bool flag = false;
			string text = string.Empty;
			int num = 0;
			foreach (object obj in colMetaData.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				flag = false;
				using (IEnumerator enumerator2 = colMetaData.Columns.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						DataColumn dataColumn = (DataColumn)enumerator2.Current;
						object obj2 = null;
						if (this._columnMappingCollection.Count > 0)
						{
							if (this.ordinalMapping)
							{
								foreach (object obj3 in this._columnMappingCollection)
								{
									SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = (SqlBulkCopyColumnMapping)obj3;
									if (sqlBulkCopyColumnMapping.DestinationOrdinal == num)
									{
										flag = true;
										break;
									}
								}
							}
							else
							{
								foreach (object obj4 in this._columnMappingCollection)
								{
									SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping2 = (SqlBulkCopyColumnMapping)obj4;
									if (sqlBulkCopyColumnMapping2.DestinationColumn == (string)dataRow["ColumnName"])
									{
										flag = true;
										break;
									}
								}
							}
							if (!flag)
							{
								goto IL_01F2;
							}
						}
						if ((bool)dataRow["IsReadOnly"])
						{
							if (!this.ordinalMapping)
							{
								goto IL_01F2;
							}
							obj2 = false;
						}
						SqlParameter sqlParameter = new SqlParameter((string)dataRow["ColumnName"], (SqlDbType)((int)dataRow["ProviderType"]));
						sqlParameter.Value = obj2;
						if ((int)dataRow["ColumnSize"] != -1)
						{
							sqlParameter.Size = (int)dataRow["ColumnSize"];
						}
						tmpCmd.Parameters.Add(sqlParameter);
					}
					IL_01F2:;
				}
				num++;
			}
			flag = false;
			bool flag2 = false;
			foreach (object obj5 in colMetaData.Rows)
			{
				DataRow dataRow2 = (DataRow)obj5;
				if (this._columnMappingCollection.Count > 0)
				{
					num = 0;
					flag2 = false;
					foreach (object obj6 in tmpCmd.Parameters)
					{
						SqlParameter sqlParameter2 = (SqlParameter)obj6;
						if (this.ordinalMapping)
						{
							foreach (object obj7 in this._columnMappingCollection)
							{
								SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping3 = (SqlBulkCopyColumnMapping)obj7;
								if (sqlBulkCopyColumnMapping3.DestinationOrdinal == num && sqlParameter2.Value == null)
								{
									flag2 = true;
								}
							}
						}
						else
						{
							foreach (object obj8 in this._columnMappingCollection)
							{
								SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping4 = (SqlBulkCopyColumnMapping)obj8;
								if (sqlBulkCopyColumnMapping4.DestinationColumn == sqlParameter2.ParameterName && (string)dataRow2["ColumnName"] == sqlParameter2.ParameterName)
								{
									flag2 = true;
									sqlParameter2.Value = null;
								}
							}
						}
						num++;
						if (flag2)
						{
							break;
						}
					}
					if (!flag2)
					{
						continue;
					}
				}
				if (!(bool)dataRow2["IsReadOnly"])
				{
					string text2 = string.Empty;
					if ((int)dataRow2["ColumnSize"] != -1)
					{
						text2 = string.Format("{0}({1})", (SqlDbType)((int)dataRow2["ProviderType"]), dataRow2["ColumnSize"]);
					}
					else
					{
						text2 = string.Format("{0}", (SqlDbType)((int)dataRow2["ProviderType"]));
					}
					if (flag)
					{
						text += ", ";
					}
					string text3 = (string)dataRow2["ColumnName"];
					text += string.Format("[{0}] {1}", text3, text2);
					if (!flag)
					{
						flag = true;
					}
					if (tableCollations != null)
					{
						foreach (object obj9 in tableCollations.Rows)
						{
							DataRow dataRow3 = (DataRow)obj9;
							if ((string)dataRow3["name"] == text3)
							{
								text += string.Format(" COLLATE {0}", dataRow3["collation"]);
								break;
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x000551A0 File Offset: 0x000533A0
		private void ValidateColumnMapping(DataTable table, DataTable tableCollations)
		{
			foreach (object obj in this._columnMappingCollection)
			{
				SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = (SqlBulkCopyColumnMapping)obj;
				if (!this.ordinalMapping && (sqlBulkCopyColumnMapping.DestinationColumn == string.Empty || sqlBulkCopyColumnMapping.SourceColumn == string.Empty))
				{
					throw new InvalidOperationException("Mappings must be either all null or ordinal");
				}
				if (this.ordinalMapping && (sqlBulkCopyColumnMapping.DestinationOrdinal == -1 || sqlBulkCopyColumnMapping.SourceOrdinal == -1))
				{
					throw new InvalidOperationException("Mappings must be either all null or ordinal");
				}
				bool flag = false;
				if (!this.ordinalMapping)
				{
					foreach (object obj2 in tableCollations.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						if ((string)dataRow["name"] == sqlBulkCopyColumnMapping.DestinationColumn)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						throw new InvalidOperationException("ColumnMapping does not match");
					}
					flag = false;
					foreach (object obj3 in table.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj3;
						if (dataColumn.ColumnName == sqlBulkCopyColumnMapping.SourceColumn)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						throw new InvalidOperationException("ColumnName " + sqlBulkCopyColumnMapping.SourceColumn + " does not match");
					}
				}
				else if (sqlBulkCopyColumnMapping.DestinationOrdinal >= tableCollations.Rows.Count)
				{
					throw new InvalidOperationException("ColumnMapping does not match");
				}
			}
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x000553E4 File Offset: 0x000535E4
		private void BulkCopyToServer(DataTable table, DataRowState state)
		{
			if (this.connection == null || this.connection.State == ConnectionState.Closed)
			{
				throw new InvalidOperationException("This method should not be called on a closed connection");
			}
			if (this._destinationTableName == null)
			{
				throw new ArgumentNullException("DestinationTableName");
			}
			if (this.identityInsert)
			{
				SqlCommand sqlCommand = new SqlCommand("set identity_insert " + table.TableName + " on", this.connection);
				sqlCommand.ExecuteScalar();
			}
			DataTable[] columnMetaData = this.GetColumnMetaData();
			DataTable dataTable = columnMetaData[0];
			DataTable dataTable2 = columnMetaData[1];
			if (this._columnMappingCollection.Count > 0)
			{
				if (this._columnMappingCollection[0].SourceOrdinal != -1)
				{
					this.ordinalMapping = true;
				}
				this.ValidateColumnMapping(table, dataTable2);
			}
			SqlCommand sqlCommand2 = new SqlCommand();
			TdsBulkCopy tdsBulkCopy = new TdsBulkCopy(this.connection.Tds);
			if (this.connection.Tds.TdsVersion >= TdsVersion.tds70)
			{
				string text = "insert bulk " + this.DestinationTableName + " (";
				text += this.GenerateColumnMetaData(sqlCommand2, dataTable, dataTable2);
				text += ")";
				tdsBulkCopy.SendColumnMetaData(text);
			}
			tdsBulkCopy.BulkCopyStart(sqlCommand2.Parameters.MetaParameters);
			long num = 0L;
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted)
				{
					if (state == (DataRowState)0 || dataRow.RowState == state)
					{
						bool flag = true;
						int num2 = 0;
						foreach (object obj2 in sqlCommand2.Parameters)
						{
							SqlParameter sqlParameter = (SqlParameter)obj2;
							int num3 = 0;
							object obj3 = null;
							if (this._columnMappingCollection.Count > 0)
							{
								if (this.ordinalMapping)
								{
									foreach (object obj4 in this._columnMappingCollection)
									{
										SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = (SqlBulkCopyColumnMapping)obj4;
										if (sqlBulkCopyColumnMapping.DestinationOrdinal == num2 && sqlParameter.Value == null)
										{
											obj3 = dataRow[sqlBulkCopyColumnMapping.SourceOrdinal];
											SqlParameter sqlParameter2 = new SqlParameter(sqlBulkCopyColumnMapping.SourceOrdinal.ToString(), obj3);
											if (sqlParameter.MetaParameter.TypeName != sqlParameter2.MetaParameter.TypeName)
											{
												sqlParameter2.SqlDbType = sqlParameter.SqlDbType;
												object obj5 = sqlParameter2.ConvertToFrameworkType(obj3);
												sqlParameter2.Value = obj5;
												obj3 = obj5;
											}
											string text2 = string.Format("{0}", sqlParameter2.MetaParameter.TypeName);
											if (text2 == "nvarchar")
											{
												if (dataRow[num2] != null)
												{
													num3 = ((string)sqlParameter2.Value).Length;
													num3 <<= 1;
												}
											}
											else
											{
												num3 = sqlParameter2.Size;
											}
											break;
										}
									}
								}
								else
								{
									foreach (object obj6 in this._columnMappingCollection)
									{
										SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping2 = (SqlBulkCopyColumnMapping)obj6;
										if (sqlBulkCopyColumnMapping2.DestinationColumn == sqlParameter.ParameterName)
										{
											obj3 = dataRow[sqlBulkCopyColumnMapping2.SourceColumn];
											SqlParameter sqlParameter3 = new SqlParameter(sqlBulkCopyColumnMapping2.SourceColumn, obj3);
											if (sqlParameter.MetaParameter.TypeName != sqlParameter3.MetaParameter.TypeName)
											{
												sqlParameter3.SqlDbType = sqlParameter.SqlDbType;
												object obj5 = sqlParameter3.ConvertToFrameworkType(obj3);
												sqlParameter3.Value = obj5;
												obj3 = obj5;
											}
											string text3 = string.Format("{0}", sqlParameter3.MetaParameter.TypeName);
											if (text3 == "nvarchar")
											{
												if (dataRow[sqlBulkCopyColumnMapping2.SourceColumn] != null)
												{
													num3 = ((string)obj3).Length;
													num3 <<= 1;
												}
											}
											else
											{
												num3 = sqlParameter3.Size;
											}
											break;
										}
									}
								}
								num2++;
							}
							else
							{
								obj3 = dataRow[sqlParameter.ParameterName];
								string typeName = sqlParameter.MetaParameter.TypeName;
								if (typeName == "nvarchar")
								{
									num3 = ((string)dataRow[sqlParameter.ParameterName]).Length;
									num3 <<= 1;
								}
								else
								{
									num3 = sqlParameter.Size;
								}
							}
							if (obj3 != null)
							{
								tdsBulkCopy.BulkCopyData(obj3, num3, flag);
								if (flag)
								{
									flag = false;
								}
							}
						}
						if (this._notifyAfter > 0)
						{
							num += 1L;
							if (num >= (long)this._notifyAfter)
							{
								this.RowsCopied(num);
								num = 0L;
							}
						}
					}
				}
			}
			tdsBulkCopy.BulkCopyEnd();
		}

		/// <summary>Copies all rows from the supplied <see cref="T:System.Data.DataRow" /> array to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="rows">An array of <see cref="T:System.Data.DataRow" /> objects that will be copied to the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600144E RID: 5198 RVA: 0x0005599C File Offset: 0x00053B9C
		public void WriteToServer(DataRow[] rows)
		{
			if (rows == null)
			{
				throw new ArgumentNullException("rows");
			}
			DataTable dataTable = new DataTable(rows[0].Table.TableName);
			foreach (object obj in rows[0].Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				DataColumn dataColumn2 = new DataColumn(dataColumn.ColumnName, dataColumn.DataType);
				dataTable.Columns.Add(dataColumn2);
			}
			foreach (DataRow dataRow in rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					dataRow2[j] = dataRow[j];
				}
				dataTable.Rows.Add(dataRow2);
			}
			this.BulkCopyToServer(dataTable, (DataRowState)0);
		}

		/// <summary>Copies all rows in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600144F RID: 5199 RVA: 0x00055AC4 File Offset: 0x00053CC4
		public void WriteToServer(DataTable table)
		{
			this.BulkCopyToServer(table, (DataRowState)0);
		}

		/// <summary>Copies all rows in the supplied <see cref="T:System.Data.IDataReader" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="reader">A <see cref="T:System.Data.IDataReader" /> whose rows will be copied to the destination table.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001450 RID: 5200 RVA: 0x00055AD0 File Offset: 0x00053CD0
		public void WriteToServer(IDataReader reader)
		{
			DataTable dataTable = new DataTable();
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
			sqlDataAdapter.FillInternal(dataTable, reader);
			this.BulkCopyToServer(dataTable, (DataRowState)0);
		}

		/// <summary>Copies only rows that match the supplied row state in the supplied <see cref="T:System.Data.DataTable" /> to a destination table specified by the <see cref="P:System.Data.SqlClient.SqlBulkCopy.DestinationTableName" /> property of the <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> object.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> whose rows will be copied to the destination table.</param>
		/// <param name="rowState">A value from the <see cref="T:System.Data.DataRowState" /> enumeration. Only rows matching the row state are copied to the destination.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001451 RID: 5201 RVA: 0x00055AFC File Offset: 0x00053CFC
		public void WriteToServer(DataTable table, DataRowState rowState)
		{
			this.BulkCopyToServer(table, rowState);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00055B08 File Offset: 0x00053D08
		private void RowsCopied(long rowsCopied)
		{
			SqlRowsCopiedEventArgs sqlRowsCopiedEventArgs = new SqlRowsCopiedEventArgs(rowsCopied);
			if (this.SqlRowsCopied != null)
			{
				this.SqlRowsCopied(this, sqlRowsCopiedEventArgs);
			}
		}

		// Token: 0x0400080F RID: 2063
		private int _batchSize;

		// Token: 0x04000810 RID: 2064
		private int _notifyAfter;

		// Token: 0x04000811 RID: 2065
		private int _bulkCopyTimeout;

		// Token: 0x04000812 RID: 2066
		private SqlBulkCopyColumnMappingCollection _columnMappingCollection = new SqlBulkCopyColumnMappingCollection();

		// Token: 0x04000813 RID: 2067
		private string _destinationTableName;

		// Token: 0x04000814 RID: 2068
		private bool ordinalMapping;

		// Token: 0x04000815 RID: 2069
		private bool sqlRowsCopied;

		// Token: 0x04000816 RID: 2070
		private bool identityInsert;

		// Token: 0x04000817 RID: 2071
		private bool isLocalConnection;

		// Token: 0x04000818 RID: 2072
		private SqlConnection connection;

		// Token: 0x04000819 RID: 2073
		private SqlBulkCopyOptions copyOptions;
	}
}
