using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Represents a set of SQL commands and a database connection that are used to fill the <see cref="T:System.Data.DataSet" /> and update the data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000099 RID: 153
	public class DataAdapter : Component, IDataAdapter
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DataAdapter" /> class.</summary>
		// Token: 0x060006D3 RID: 1747 RVA: 0x00022C74 File Offset: 0x00020E74
		protected DataAdapter()
		{
			this.acceptChangesDuringFill = true;
			this.continueUpdateOnError = false;
			this.missingMappingAction = MissingMappingAction.Passthrough;
			this.missingSchemaAction = MissingSchemaAction.Add;
			this.tableMappings = new DataTableMappingCollection();
			this.acceptChangesDuringUpdate = true;
			this.fillLoadOption = LoadOption.OverwriteChanges;
			this.returnProviderSpecificTypes = false;
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DataAdapter" /> class from an existing object of the same type.</summary>
		/// <param name="from">A <see cref="T:System.Data.Common.DataAdapter" /> object used to create the new <see cref="T:System.Data.Common.DataAdapter" />. </param>
		// Token: 0x060006D4 RID: 1748 RVA: 0x00022CC4 File Offset: 0x00020EC4
		protected DataAdapter(DataAdapter adapter)
		{
			this.AcceptChangesDuringFill = adapter.AcceptChangesDuringFill;
			this.ContinueUpdateOnError = adapter.ContinueUpdateOnError;
			this.MissingMappingAction = adapter.MissingMappingAction;
			this.MissingSchemaAction = adapter.MissingSchemaAction;
			if (adapter.tableMappings != null)
			{
				foreach (object obj in adapter.TableMappings)
				{
					ICloneable cloneable = (ICloneable)obj;
					this.TableMappings.Add(cloneable.Clone());
				}
			}
			this.acceptChangesDuringUpdate = adapter.AcceptChangesDuringUpdate;
			this.fillLoadOption = adapter.FillLoadOption;
			this.returnProviderSpecificTypes = adapter.ReturnProviderSpecificTypes;
		}

		/// <summary>Returned when an error occurs during a fill operation.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060006D5 RID: 1749 RVA: 0x00022DA4 File Offset: 0x00020FA4
		// (remove) Token: 0x060006D6 RID: 1750 RVA: 0x00022DC0 File Offset: 0x00020FC0
		public event FillErrorEventHandler FillError;

		/// <summary>For a description of this member, see <see cref="P:System.Data.IDataAdapter.TableMappings" />.</summary>
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00022DDC File Offset: 0x00020FDC
		ITableMappingCollection IDataAdapter.TableMappings
		{
			get
			{
				return this.TableMappings;
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="M:System.Data.DataRow.AcceptChanges" /> is called on a <see cref="T:System.Data.DataRow" /> after it is added to the <see cref="T:System.Data.DataTable" /> during any of the Fill operations.</summary>
		/// <returns>true if <see cref="M:System.Data.DataRow.AcceptChanges" /> is called on the <see cref="T:System.Data.DataRow" />; otherwise false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00022DE4 File Offset: 0x00020FE4
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00022DEC File Offset: 0x00020FEC
		[DataCategory("Fill")]
		[DefaultValue(true)]
		public bool AcceptChangesDuringFill
		{
			get
			{
				return this.acceptChangesDuringFill;
			}
			set
			{
				this.acceptChangesDuringFill = value;
			}
		}

		/// <summary>Gets or sets whether <see cref="M:System.Data.DataRow.AcceptChanges" /> is called during a <see cref="M:System.Data.Common.DataAdapter.Update(System.Data.DataSet)" />.</summary>
		/// <returns>true if <see cref="M:System.Data.DataRow.AcceptChanges" /> is called during an <see cref="M:System.Data.Common.DataAdapter.Update(System.Data.DataSet)" />; otherwise false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00022DF8 File Offset: 0x00020FF8
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x00022E00 File Offset: 0x00021000
		[DefaultValue(true)]
		public bool AcceptChangesDuringUpdate
		{
			get
			{
				return this.acceptChangesDuringUpdate;
			}
			set
			{
				this.acceptChangesDuringUpdate = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether to generate an exception when an error is encountered during a row update.</summary>
		/// <returns>true to continue the update without generating an exception; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00022E0C File Offset: 0x0002100C
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x00022E14 File Offset: 0x00021014
		[DataCategory("Update")]
		[DefaultValue(false)]
		public bool ContinueUpdateOnError
		{
			get
			{
				return this.continueUpdateOnError;
			}
			set
			{
				this.continueUpdateOnError = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.LoadOption" /> that determines how the adapter fills the <see cref="T:System.Data.DataTable" /> from the <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.LoadOption" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00022E20 File Offset: 0x00021020
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x00022E28 File Offset: 0x00021028
		[RefreshProperties(RefreshProperties.All)]
		public LoadOption FillLoadOption
		{
			get
			{
				return this.fillLoadOption;
			}
			set
			{
				ExceptionHelper.CheckEnumValue(typeof(LoadOption), value);
				this.fillLoadOption = value;
			}
		}

		/// <summary>Determines the action to take when incoming data does not have a matching table or column.</summary>
		/// <returns>One of the <see cref="T:System.Data.MissingMappingAction" /> values. The default is Passthrough.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not one of the <see cref="T:System.Data.MissingMappingAction" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00022E48 File Offset: 0x00021048
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x00022E50 File Offset: 0x00021050
		[DefaultValue(MissingMappingAction.Passthrough)]
		[DataCategory("Mapping")]
		public MissingMappingAction MissingMappingAction
		{
			get
			{
				return this.missingMappingAction;
			}
			set
			{
				ExceptionHelper.CheckEnumValue(typeof(MissingMappingAction), value);
				this.missingMappingAction = value;
			}
		}

		/// <summary>Determines the action to take when existing <see cref="T:System.Data.DataSet" /> schema does not match incoming data.</summary>
		/// <returns>One of the <see cref="T:System.Data.MissingSchemaAction" /> values. The default is Add.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not one of the <see cref="T:System.Data.MissingSchemaAction" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00022E70 File Offset: 0x00021070
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x00022E78 File Offset: 0x00021078
		[DefaultValue(MissingSchemaAction.Add)]
		[DataCategory("Mapping")]
		public MissingSchemaAction MissingSchemaAction
		{
			get
			{
				return this.missingSchemaAction;
			}
			set
			{
				ExceptionHelper.CheckEnumValue(typeof(MissingSchemaAction), value);
				this.missingSchemaAction = value;
			}
		}

		/// <summary>Gets or sets whether the Fill method should return provider-specific values or common CLS-compliant values.</summary>
		/// <returns>true if the Fill method should return provider-specific values; otherwise false to return common CLS-compliant values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00022E98 File Offset: 0x00021098
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x00022EA0 File Offset: 0x000210A0
		[DefaultValue(false)]
		public virtual bool ReturnProviderSpecificTypes
		{
			get
			{
				return this.returnProviderSpecificTypes;
			}
			set
			{
				this.returnProviderSpecificTypes = value;
			}
		}

		/// <summary>Gets a collection that provides the master mapping between a source table and a <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A collection that provides the master mapping between the returned records and the <see cref="T:System.Data.DataSet" />. The default value is an empty collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00022EAC File Offset: 0x000210AC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DataCategory("Mapping")]
		public DataTableMappingCollection TableMappings
		{
			get
			{
				return this.tableMappings;
			}
		}

		/// <summary>Creates a copy of this instance of <see cref="T:System.Data.Common.DataAdapter" />.</summary>
		/// <returns>The cloned instance of <see cref="T:System.Data.Common.DataAdapter" />.</returns>
		// Token: 0x060006E7 RID: 1767 RVA: 0x00022EB4 File Offset: 0x000210B4
		[MonoTODO]
		[Obsolete("Use the protected constructor instead", false)]
		protected virtual DataAdapter CloneInternals()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new <see cref="T:System.Data.Common.DataTableMappingCollection" />.</summary>
		/// <returns>A new <see cref="T:System.Data.Common.DataTableMappingCollection" />.</returns>
		// Token: 0x060006E8 RID: 1768 RVA: 0x00022EBC File Offset: 0x000210BC
		protected virtual DataTableMappingCollection CreateTableMappings()
		{
			return new DataTableMappingCollection();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DataAdapter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060006E9 RID: 1769 RVA: 0x00022EC4 File Offset: 0x000210C4
		[MonoTODO]
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether one or more <see cref="T:System.Data.Common.DataTableMapping" /> objects exist and they should be persisted.</summary>
		/// <returns>true if one or more <see cref="T:System.Data.Common.DataTableMapping" /> objects exist; otherwise false.</returns>
		// Token: 0x060006EA RID: 1770 RVA: 0x00022ECC File Offset: 0x000210CC
		protected virtual bool ShouldSerializeTableMappings()
		{
			return true;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00022ED0 File Offset: 0x000210D0
		internal int FillInternal(DataTable dataTable, IDataReader dataReader)
		{
			if (dataReader.FieldCount == 0)
			{
				dataReader.Close();
				return 0;
			}
			int num = 0;
			try
			{
				string text = this.SetupSchema(SchemaType.Mapped, dataTable.TableName);
				if (text != null)
				{
					dataTable.TableName = text;
					this.FillTable(dataTable, dataReader, 0, 0, ref num);
				}
			}
			finally
			{
				dataReader.Close();
			}
			return num;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00022F44 File Offset: 0x00021144
		internal int[] BuildSchema(IDataReader reader, DataTable table, SchemaType schemaType)
		{
			return DataAdapter.BuildSchema(reader, table, schemaType, this.MissingSchemaAction, this.MissingMappingAction, this.TableMappings);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00022F6C File Offset: 0x0002116C
		internal static int[] BuildSchema(IDataReader reader, DataTable table, SchemaType schemaType, MissingSchemaAction missingSchAction, MissingMappingAction missingMapAction, DataTableMappingCollection dtMapping)
		{
			int num = 0;
			int[] array = new int[table.Columns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = -1;
			}
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			bool flag = true;
			DataTable schemaTable = reader.GetSchemaTable();
			DataColumn dataColumn = schemaTable.Columns["ColumnName"];
			DataColumn dataColumn2 = schemaTable.Columns["DataType"];
			DataColumn dataColumn3 = schemaTable.Columns["IsAutoIncrement"];
			DataColumn dataColumn4 = schemaTable.Columns["AllowDBNull"];
			DataColumn dataColumn5 = schemaTable.Columns["IsReadOnly"];
			DataColumn dataColumn6 = schemaTable.Columns["IsKey"];
			DataColumn dataColumn7 = schemaTable.Columns["IsUnique"];
			DataColumn dataColumn8 = schemaTable.Columns["ColumnSize"];
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text;
				string text2;
				if (dataColumn == null || dataRow.IsNull(dataColumn) || (string)dataRow[dataColumn] == string.Empty)
				{
					text = "Column";
					text2 = "Column1";
				}
				else
				{
					text = (string)dataRow[dataColumn];
					text2 = text;
				}
				int num2 = 1;
				while (arrayList2.Contains(text2))
				{
					text2 = string.Format("{0}{1}", text, num2);
					num2++;
				}
				arrayList2.Add(text2);
				int num3 = dtMapping.IndexOfDataSetTable(table.TableName);
				string text3 = ((num3 == -1) ? table.TableName : dtMapping[num3].SourceTable);
				DataTableMapping tableMappingBySchemaAction = DataTableMappingCollection.GetTableMappingBySchemaAction(dtMapping, text3, table.TableName, missingMapAction);
				if (tableMappingBySchemaAction != null)
				{
					table.TableName = tableMappingBySchemaAction.DataSetTable;
					DataColumnMapping columnMappingBySchemaAction = DataColumnMappingCollection.GetColumnMappingBySchemaAction(tableMappingBySchemaAction.ColumnMappings, text2, missingMapAction);
					if (columnMappingBySchemaAction != null)
					{
						Type type = dataRow[dataColumn2] as Type;
						DataColumn dataColumn9 = ((type == null) ? null : columnMappingBySchemaAction.GetDataColumnBySchemaAction(table, type, missingSchAction));
						if (dataColumn9 != null)
						{
							if (table.Columns.IndexOf(dataColumn9) == -1)
							{
								if (missingSchAction == MissingSchemaAction.Add || missingSchAction == MissingSchemaAction.AddWithKey)
								{
									table.Columns.Add(dataColumn9);
								}
								int[] array2 = new int[array.Length + 1];
								Array.Copy(array, 0, array2, 0, dataColumn9.Ordinal);
								Array.Copy(array, dataColumn9.Ordinal, array2, dataColumn9.Ordinal + 1, array.Length - dataColumn9.Ordinal);
								array = array2;
							}
							if (missingSchAction == MissingSchemaAction.AddWithKey)
							{
								object obj2 = ((dataColumn4 == null) ? null : dataRow[dataColumn4]);
								bool flag2 = !(obj2 is bool) || (bool)obj2;
								obj2 = ((dataColumn6 == null) ? null : dataRow[dataColumn6]);
								bool flag3 = obj2 is bool && (bool)obj2;
								obj2 = ((dataColumn3 == null) ? null : dataRow[dataColumn3]);
								bool flag4 = obj2 is bool && (bool)obj2;
								obj2 = ((dataColumn5 == null) ? null : dataRow[dataColumn5]);
								bool flag5 = obj2 is bool && (bool)obj2;
								obj2 = ((dataColumn7 == null) ? null : dataRow[dataColumn7]);
								bool flag6 = obj2 is bool && (bool)obj2;
								dataColumn9.AllowDBNull = flag2;
								if (flag4 && DataColumn.CanAutoIncrement(type))
								{
									dataColumn9.AutoIncrement = true;
									if (!flag2)
									{
										dataColumn9.AllowDBNull = false;
									}
								}
								if (type == DbTypes.TypeOfString)
								{
									dataColumn9.MaxLength = ((dataColumn8 == null) ? 0 : ((int)dataRow[dataColumn8]));
								}
								if (flag5)
								{
									dataColumn9.ReadOnly = true;
								}
								if (!flag2 && (!flag5 || flag3))
								{
									dataColumn9.AllowDBNull = false;
								}
								if (flag6 && !flag3 && !type.IsArray)
								{
									dataColumn9.Unique = true;
									if (!flag2)
									{
										dataColumn9.AllowDBNull = false;
									}
								}
								bool flag7 = false;
								if (schemaTable.Columns.Contains("IsHidden"))
								{
									obj2 = dataRow["IsHidden"];
									flag7 = obj2 is bool && (bool)obj2;
								}
								if (flag3 && !flag7)
								{
									arrayList.Add(dataColumn9);
									if (flag2)
									{
										flag = false;
									}
								}
							}
							array[dataColumn9.Ordinal] = num++;
						}
					}
				}
			}
			if (arrayList.Count > 0)
			{
				DataColumn[] array3 = (DataColumn[])arrayList.ToArray(typeof(DataColumn));
				if (flag)
				{
					table.PrimaryKey = array3;
				}
				else
				{
					UniqueConstraint uniqueConstraint = new UniqueConstraint(array3);
					for (int j = 0; j < table.Constraints.Count; j++)
					{
						if (table.Constraints[j].Equals(uniqueConstraint))
						{
							uniqueConstraint = null;
							break;
						}
					}
					if (uniqueConstraint != null)
					{
						table.Constraints.Add(uniqueConstraint);
					}
				}
			}
			return array;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00023530 File Offset: 0x00021730
		internal bool FillTable(DataTable dataTable, IDataReader dataReader, int startRecord, int maxRecords, ref int counter)
		{
			if (dataReader.FieldCount == 0)
			{
				return false;
			}
			int num = counter;
			int[] array = this.BuildSchema(dataReader, dataTable, SchemaType.Mapped);
			int[] array2 = new int[array.Length];
			int num2 = array2.Length;
			for (int i = 0; i < array2.Length; i++)
			{
				if (array[i] >= 0)
				{
					array2[array[i]] = i;
				}
				else
				{
					array2[--num2] = i;
				}
			}
			for (int j = 0; j < startRecord; j++)
			{
				dataReader.Read();
			}
			dataTable.BeginLoadData();
			while (dataReader.Read() && (maxRecords == 0 || counter - num < maxRecords))
			{
				try
				{
					dataTable.LoadDataRow(dataReader, array2, num2, this.AcceptChangesDuringFill);
					counter++;
				}
				catch (Exception ex)
				{
					object[] array3 = new object[dataReader.FieldCount];
					object[] array4 = new object[array.Length];
					dataReader.GetValues(array3);
					for (int k = 0; k < array.Length; k++)
					{
						if (array[k] >= 0)
						{
							array4[k] = array3[array[k]];
						}
					}
					FillErrorEventArgs fillErrorEventArgs = this.CreateFillErrorEvent(dataTable, array4, ex);
					this.OnFillErrorInternal(fillErrorEventArgs);
					if (!fillErrorEventArgs.Continue)
					{
						throw ex;
					}
				}
			}
			dataTable.EndLoadData();
			return true;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0002369C File Offset: 0x0002189C
		internal virtual void OnFillErrorInternal(FillErrorEventArgs value)
		{
			this.OnFillError(value);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000236A8 File Offset: 0x000218A8
		internal FillErrorEventArgs CreateFillErrorEvent(DataTable dataTable, object[] values, Exception e)
		{
			return new FillErrorEventArgs(dataTable, values)
			{
				Errors = e,
				Continue = false
			};
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000236CC File Offset: 0x000218CC
		internal string SetupSchema(SchemaType schemaType, string sourceTableName)
		{
			if (schemaType != SchemaType.Mapped)
			{
				return sourceTableName;
			}
			DataTableMapping tableMappingBySchemaAction = DataTableMappingCollection.GetTableMappingBySchemaAction(this.TableMappings, sourceTableName, sourceTableName, this.MissingMappingAction);
			if (tableMappingBySchemaAction != null)
			{
				return tableMappingBySchemaAction.DataSetTable;
			}
			return null;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00023708 File Offset: 0x00021908
		internal int FillInternal(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords)
		{
			if (dataSet == null)
			{
				throw new ArgumentNullException("DataSet");
			}
			if (startRecord < 0)
			{
				throw new ArgumentException("The startRecord parameter was less than 0.");
			}
			if (maxRecords < 0)
			{
				throw new ArgumentException("The maxRecords parameter was less than 0.");
			}
			int num = 0;
			int num2 = 0;
			try
			{
				string text = srcTable;
				do
				{
					if (dataReader.FieldCount != -1)
					{
						text = this.SetupSchema(SchemaType.Mapped, text);
						if (text != null)
						{
							DataTable dataTable;
							if (dataSet.Tables.Contains(text))
							{
								dataTable = dataSet.Tables[text];
							}
							else
							{
								if (this.MissingSchemaAction == MissingSchemaAction.Ignore)
								{
									goto IL_00CF;
								}
								dataTable = dataSet.Tables.Add(text);
							}
							if (this.FillTable(dataTable, dataReader, startRecord, maxRecords, ref num2))
							{
								text = string.Format("{0}{1}", srcTable, ++num);
								startRecord = 0;
								maxRecords = 0;
							}
						}
					}
					IL_00CF:;
				}
				while (dataReader.NextResult());
			}
			finally
			{
				dataReader.Close();
			}
			return num2;
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" /> to match those in the data source.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060006F3 RID: 1779 RVA: 0x00023818 File Offset: 0x00021A18
		public virtual int Fill(DataSet dataSet)
		{
			throw new NotSupportedException();
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataTable" /> to match those in the data source using the <see cref="T:System.Data.DataTable" /> name and the specified <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTable">A <see cref="T:System.Data.DataTable" /> to fill with records.</param>
		/// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
		// Token: 0x060006F4 RID: 1780 RVA: 0x00023820 File Offset: 0x00021A20
		protected virtual int Fill(DataTable dataTable, IDataReader dataReader)
		{
			return this.FillInternal(dataTable, dataReader);
		}

		/// <summary>Adds or refreshes rows in a specified range in the collection of <see cref="T:System.Data.DataTable" /> objects to match those in the data source.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTables">A collection of <see cref="T:System.Data.DataTable" /> objects to fill with records.</param>
		/// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="startRecord">An integer indicating the location of the starting record.</param>
		/// <param name="maxRecords">An integer indicating the maximum number of records.</param>
		// Token: 0x060006F5 RID: 1781 RVA: 0x0002382C File Offset: 0x00021A2C
		protected virtual int Fill(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
		{
			int num = 0;
			if (dataReader.IsClosed)
			{
				return 0;
			}
			if (startRecord < 0)
			{
				throw new ArgumentException("The startRecord parameter was less than 0.");
			}
			if (maxRecords < 0)
			{
				throw new ArgumentException("The maxRecords parameter was less than 0.");
			}
			try
			{
				foreach (DataTable dataTable in dataTables)
				{
					string text = this.SetupSchema(SchemaType.Mapped, dataTable.TableName);
					if (text != null)
					{
						dataTable.TableName = text;
						this.FillTable(dataTable, dataReader, 0, 0, ref num);
					}
				}
			}
			finally
			{
				dataReader.Close();
			}
			return num;
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records.</param>
		/// <param name="srcTable">A string indicating the name of the source table.</param>
		/// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="startRecord">An integer indicating the location of the starting record.</param>
		/// <param name="maxRecords">An integer indicating the maximum number of records.</param>
		// Token: 0x060006F6 RID: 1782 RVA: 0x000238DC File Offset: 0x00021ADC
		protected virtual int Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords)
		{
			return this.FillInternal(dataSet, srcTable, dataReader, startRecord, maxRecords);
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object that contains schema information returned from the data source.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to be filled from the <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values.</param>
		/// <param name="dataReader">The <see cref="T:System.Data.IDataReader" /> to be used as the data source when filling the <see cref="T:System.Data.DataTable" />.</param>
		// Token: 0x060006F7 RID: 1783 RVA: 0x000238EC File Offset: 0x00021AEC
		[MonoTODO]
		protected virtual DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDataReader dataReader)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataTable" /> to be filled from the <see cref="T:System.Data.IDataReader" />.</param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values.</param>
		/// <param name="srcTable">The name of the source table to use for table mapping.</param>
		/// <param name="dataReader">The <see cref="T:System.Data.IDataReader" /> to be used as the data source when filling the <see cref="T:System.Data.DataTable" />.</param>
		// Token: 0x060006F8 RID: 1784 RVA: 0x000238F4 File Offset: 0x00021AF4
		[MonoTODO]
		protected virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable, IDataReader dataReader)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object that contains schema information returned from the data source.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060006F9 RID: 1785 RVA: 0x000238FC File Offset: 0x00021AFC
		public virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the parameters set by the user when executing an SQL SELECT statement.</summary>
		/// <returns>An array of <see cref="T:System.Data.IDataParameter" /> objects that contains the parameters set by the user.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006FA RID: 1786 RVA: 0x00023904 File Offset: 0x00021B04
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[MonoTODO]
		public virtual IDataParameter[] GetFillParameters()
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether a <see cref="T:System.Data.Common.DataTableMappingCollection" /> has been created.</summary>
		/// <returns>true if a <see cref="T:System.Data.Common.DataTableMappingCollection" /> has been created; otherwise false.</returns>
		// Token: 0x060006FB RID: 1787 RVA: 0x0002390C File Offset: 0x00021B0C
		protected bool HasTableMappings()
		{
			return this.TableMappings.Count != 0;
		}

		/// <summary>Invoked when an error occurs during a Fill.</summary>
		/// <param name="value">A <see cref="T:System.Data.FillErrorEventArgs" /> object.</param>
		// Token: 0x060006FC RID: 1788 RVA: 0x00023920 File Offset: 0x00021B20
		protected virtual void OnFillError(FillErrorEventArgs value)
		{
			if (this.FillError != null)
			{
				this.FillError(this, value);
			}
		}

		/// <summary>Resets <see cref="P:System.Data.Common.DataAdapter.FillLoadOption" /> to its default state and causes <see cref="M:System.Data.Common.DataAdapter.Fill(System.Data.DataSet)" /> to honor <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006FD RID: 1789 RVA: 0x0002393C File Offset: 0x00021B3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetFillLoadOption()
		{
			this.FillLoadOption = LoadOption.OverwriteChanges;
		}

		/// <summary>Determines whether the <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill" /> property should be persisted.</summary>
		/// <returns>true if the <see cref="P:System.Data.Common.DataAdapter.AcceptChangesDuringFill" /> property is persisted; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006FE RID: 1790 RVA: 0x00023948 File Offset: 0x00021B48
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ShouldSerializeAcceptChangesDuringFill()
		{
			return true;
		}

		/// <summary>Determines whether the <see cref="P:System.Data.Common.DataAdapter.FillLoadOption" /> property should be persisted.</summary>
		/// <returns>true if the <see cref="P:System.Data.Common.DataAdapter.FillLoadOption" /> property is persisted; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060006FF RID: 1791 RVA: 0x0002394C File Offset: 0x00021B4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual bool ShouldSerializeFillLoadOption()
		{
			return false;
		}

		/// <summary>Calls the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified <see cref="T:System.Data.DataSet" /> from a <see cref="T:System.Data.DataTable" /> named "Table."</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> used to update the data source. </param>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000700 RID: 1792 RVA: 0x00023950 File Offset: 0x00021B50
		[MonoTODO]
		public virtual int Update(DataSet dataSet)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040002D8 RID: 728
		private const string DefaultSourceTableName = "Table";

		// Token: 0x040002D9 RID: 729
		private const string DefaultSourceColumnName = "Column";

		// Token: 0x040002DA RID: 730
		private bool acceptChangesDuringFill;

		// Token: 0x040002DB RID: 731
		private bool continueUpdateOnError;

		// Token: 0x040002DC RID: 732
		private MissingMappingAction missingMappingAction;

		// Token: 0x040002DD RID: 733
		private MissingSchemaAction missingSchemaAction;

		// Token: 0x040002DE RID: 734
		private DataTableMappingCollection tableMappings;

		// Token: 0x040002DF RID: 735
		private bool acceptChangesDuringUpdate;

		// Token: 0x040002E0 RID: 736
		private LoadOption fillLoadOption;

		// Token: 0x040002E1 RID: 737
		private bool returnProviderSpecificTypes;
	}
}
