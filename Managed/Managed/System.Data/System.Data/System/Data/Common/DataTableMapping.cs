using System;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Contains a description of a mapped relationship between a source table and a <see cref="T:System.Data.DataTable" />. This class is used by a <see cref="T:System.Data.Common.DataAdapter" /> when populating a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000AF RID: 175
	[TypeConverter("System.Data.Common.DataTableMapping+DataTableMappingConverter, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public sealed class DataTableMapping : MarshalByRefObject, ITableMapping, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataTableMapping" /> class.</summary>
		// Token: 0x060007F1 RID: 2033 RVA: 0x00025AA0 File Offset: 0x00023CA0
		public DataTableMapping()
		{
			this.dataSetTable = string.Empty;
			this.sourceTable = string.Empty;
			this.columnMappings = new DataColumnMappingCollection();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataTableMapping" /> class with a source when given a source table name and a <see cref="T:System.Data.DataTable" /> name.</summary>
		/// <param name="sourceTable">The case-sensitive source table name from a data source. </param>
		/// <param name="dataSetTable">The table name from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		// Token: 0x060007F2 RID: 2034 RVA: 0x00025ACC File Offset: 0x00023CCC
		public DataTableMapping(string sourceTable, string dataSetTable)
			: this()
		{
			this.sourceTable = sourceTable;
			this.dataSetTable = dataSetTable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataTableMapping" /> class when given a source table name, a <see cref="T:System.Data.DataTable" /> name, and an array of <see cref="T:System.Data.Common.DataColumnMapping" /> objects.</summary>
		/// <param name="sourceTable">The case-sensitive source table name from a data source. </param>
		/// <param name="dataSetTable">The table name from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		/// <param name="columnMappings">An array of <see cref="T:System.Data.Common.DataColumnMapping" /> objects. </param>
		// Token: 0x060007F3 RID: 2035 RVA: 0x00025AE4 File Offset: 0x00023CE4
		public DataTableMapping(string sourceTable, string dataSetTable, DataColumnMapping[] columnMappings)
			: this(sourceTable, dataSetTable)
		{
			this.columnMappings.AddRange(columnMappings);
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00025AFC File Offset: 0x00023CFC
		IColumnMappingCollection ITableMapping.ColumnMappings
		{
			get
			{
				return this.ColumnMappings;
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00025B04 File Offset: 0x00023D04
		object ICloneable.Clone()
		{
			DataColumnMapping[] array = new DataColumnMapping[this.columnMappings.Count];
			this.columnMappings.CopyTo(array, 0);
			return new DataTableMapping(this.SourceTable, this.DataSetTable, array);
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> for the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DataColumnMappingCollection" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00025B44 File Offset: 0x00023D44
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataColumnMappingCollection ColumnMappings
		{
			get
			{
				return this.columnMappings;
			}
		}

		/// <summary>Gets or sets the table name from a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The table name from a <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00025B4C File Offset: 0x00023D4C
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x00025B54 File Offset: 0x00023D54
		[DefaultValue("")]
		public string DataSetTable
		{
			get
			{
				return this.dataSetTable;
			}
			set
			{
				this.dataSetTable = value;
			}
		}

		/// <summary>Gets or sets the case-sensitive source table name from a data source.</summary>
		/// <returns>The case-sensitive source table name from a data source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00025B60 File Offset: 0x00023D60
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x00025B68 File Offset: 0x00023D68
		[DefaultValue("")]
		public string SourceTable
		{
			get
			{
				return this.sourceTable;
			}
			set
			{
				this.sourceTable = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Data.DataColumn" /> from the specified <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.Data.MissingMappingAction" /> value and the name of the <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataColumn" />.</returns>
		/// <param name="sourceColumn">The name of the <see cref="T:System.Data.DataColumn" />. </param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="mappingAction" /> parameter was set to Error, and no mapping was specified. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007FB RID: 2043 RVA: 0x00025B74 File Offset: 0x00023D74
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumnMapping GetColumnMappingBySchemaAction(string sourceColumn, MissingMappingAction mappingAction)
		{
			return DataColumnMappingCollection.GetColumnMappingBySchemaAction(this.columnMappings, sourceColumn, mappingAction);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataColumn" /> object for a given column name.</summary>
		/// <returns>A <see cref="T:System.Data.DataColumn" /> object.</returns>
		/// <param name="sourceColumn">The name of the <see cref="T:System.Data.DataColumn" />. </param>
		/// <param name="dataType">The data type for <paramref name="sourceColumn" />.</param>
		/// <param name="dataTable">The table name from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values. </param>
		/// <param name="schemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060007FC RID: 2044 RVA: 0x00025B84 File Offset: 0x00023D84
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[MonoTODO]
		public DataColumn GetDataColumn(string sourceColumn, Type dataType, DataTable dataTable, MissingMappingAction mappingAction, MissingSchemaAction schemaAction)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the current <see cref="T:System.Data.DataTable" /> for the specified <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Data.MissingSchemaAction" /> value.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> from which to get the <see cref="T:System.Data.DataTable" />. </param>
		/// <param name="schemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007FD RID: 2045 RVA: 0x00025B8C File Offset: 0x00023D8C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataTable GetDataTableBySchemaAction(DataSet dataSet, MissingSchemaAction schemaAction)
		{
			if (dataSet.Tables.Contains(this.DataSetTable))
			{
				return dataSet.Tables[this.DataSetTable];
			}
			if (schemaAction == MissingSchemaAction.Ignore)
			{
				return null;
			}
			if (schemaAction == MissingSchemaAction.Error)
			{
				throw new InvalidOperationException(string.Format("Missing the '{0} DataTable for the '{1}' SourceTable", this.DataSetTable, this.SourceTable));
			}
			return new DataTable(this.DataSetTable);
		}

		/// <summary>Converts the current <see cref="P:System.Data.Common.DataTableMapping.SourceTable" /> name to a string.</summary>
		/// <returns>The current <see cref="P:System.Data.Common.DataTableMapping.SourceTable" /> name, as a string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060007FE RID: 2046 RVA: 0x00025BF8 File Offset: 0x00023DF8
		public override string ToString()
		{
			return this.SourceTable;
		}

		// Token: 0x040002FB RID: 763
		private string sourceTable;

		// Token: 0x040002FC RID: 764
		private string dataSetTable;

		// Token: 0x040002FD RID: 765
		private DataColumnMappingCollection columnMappings;
	}
}
