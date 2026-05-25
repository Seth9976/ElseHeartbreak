using System;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Contains a generic column mapping for an object that inherits from <see cref="T:System.Data.Common.DataAdapter" />. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200009B RID: 155
	[TypeConverter("System.Data.Common.DataColumnMapping+DataColumnMappingConverter, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public sealed class DataColumnMapping : MarshalByRefObject, IColumnMapping, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataColumnMapping" /> class.</summary>
		// Token: 0x06000727 RID: 1831 RVA: 0x00023FDC File Offset: 0x000221DC
		public DataColumnMapping()
		{
			this.sourceColumn = string.Empty;
			this.dataSetColumn = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataColumnMapping" /> class with the specified source column name and <see cref="T:System.Data.DataSet" /> column name to map to.</summary>
		/// <param name="sourceColumn">The case-sensitive column name from a data source. </param>
		/// <param name="dataSetColumn">The column name, which is not case sensitive, from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		// Token: 0x06000728 RID: 1832 RVA: 0x00023FFC File Offset: 0x000221FC
		public DataColumnMapping(string sourceColumn, string dataSetColumn)
		{
			this.sourceColumn = sourceColumn;
			this.dataSetColumn = dataSetColumn;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00024014 File Offset: 0x00022214
		object ICloneable.Clone()
		{
			return new DataColumnMapping(this.SourceColumn, this.DataSetColumn);
		}

		/// <summary>Gets or sets the name of the column within the <see cref="T:System.Data.DataSet" /> to map to.</summary>
		/// <returns>The name of the column within the <see cref="T:System.Data.DataSet" /> to map to. The name is not case sensitive.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x00024028 File Offset: 0x00022228
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x00024030 File Offset: 0x00022230
		[DefaultValue("")]
		public string DataSetColumn
		{
			get
			{
				return this.dataSetColumn;
			}
			set
			{
				this.dataSetColumn = value;
			}
		}

		/// <summary>Gets or sets the name of the column within the data source to map from. The name is case-sensitive.</summary>
		/// <returns>The case-sensitive name of the column in the data source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0002403C File Offset: 0x0002223C
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x00024044 File Offset: 0x00022244
		[DefaultValue("")]
		public string SourceColumn
		{
			get
			{
				return this.sourceColumn;
			}
			set
			{
				this.sourceColumn = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Data.DataColumn" /> from the given <see cref="T:System.Data.DataTable" /> using the <see cref="T:System.Data.MissingSchemaAction" /> and the <see cref="P:System.Data.Common.DataColumnMapping.DataSetColumn" /> property.</summary>
		/// <returns>A <see cref="T:System.Data.DataColumn" />.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to get the column from.</param>
		/// <param name="dataType">The <see cref="T:System.Type" /> of the data column.</param>
		/// <param name="schemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600072E RID: 1838 RVA: 0x00024050 File Offset: 0x00022250
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DataColumn GetDataColumnBySchemaAction(DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			if (dataTable.Columns.Contains(this.dataSetColumn))
			{
				return dataTable.Columns[this.dataSetColumn];
			}
			if (schemaAction == MissingSchemaAction.Ignore)
			{
				return null;
			}
			if (schemaAction == MissingSchemaAction.Error)
			{
				throw new InvalidOperationException(string.Format("Missing the DataColumn '{0}' in the DataTable '{1}' for the SourceColumn '{2}'", this.DataSetColumn, dataTable.TableName, this.SourceColumn));
			}
			return new DataColumn(this.dataSetColumn, dataType);
		}

		/// <summary>A static version of <see cref="M:System.Data.Common.DataColumnMapping.GetDataColumnBySchemaAction(System.Data.DataTable,System.Type,System.Data.MissingSchemaAction)" /> that can be called without instantiating a <see cref="T:System.Data.Common.DataColumnMapping" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.DataColumn" /> object.</returns>
		/// <param name="sourceColumn">The case-sensitive column name from a data source. </param>
		/// <param name="dataSetColumn">The column name, which is not case sensitive, from a <see cref="T:System.Data.DataSet" /> to map to. </param>
		/// <param name="dataTable">An instance of <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="dataType">The data type for the column being mapped.</param>
		/// <param name="schemaAction">Determines the action to take when existing <see cref="T:System.Data.DataSet" /> schema does not match incoming data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600072F RID: 1839 RVA: 0x000240C4 File Offset: 0x000222C4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataColumn GetDataColumnBySchemaAction(string sourceColumn, string dataSetColumn, DataTable dataTable, Type dataType, MissingSchemaAction schemaAction)
		{
			if (dataTable.Columns.Contains(dataSetColumn))
			{
				return dataTable.Columns[dataSetColumn];
			}
			if (schemaAction == MissingSchemaAction.Ignore)
			{
				return null;
			}
			if (schemaAction == MissingSchemaAction.Error)
			{
				throw new InvalidOperationException(string.Format("Missing the DataColumn '{0}' in the DataTable '{1}' for the SourceColumn '{2}'", dataSetColumn, dataTable.TableName, sourceColumn));
			}
			return new DataColumn(dataSetColumn, dataType);
		}

		/// <summary>Converts the current <see cref="P:System.Data.Common.DataColumnMapping.SourceColumn" /> name to a string.</summary>
		/// <returns>The current <see cref="P:System.Data.Common.DataColumnMapping.SourceColumn" /> name as a string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000730 RID: 1840 RVA: 0x00024120 File Offset: 0x00022320
		public override string ToString()
		{
			return this.SourceColumn;
		}

		// Token: 0x040002E6 RID: 742
		private string sourceColumn;

		// Token: 0x040002E7 RID: 743
		private string dataSetColumn;
	}
}
