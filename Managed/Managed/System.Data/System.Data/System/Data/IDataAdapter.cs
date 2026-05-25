using System;

namespace System.Data
{
	/// <summary>Allows an object to implement a DataAdapter, and represents a set of methods and mapping action-related properties that are used to fill and update a <see cref="T:System.Data.DataSet" /> and update a data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004B RID: 75
	public interface IDataAdapter
	{
		/// <summary>Adds or updates rows in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> name, and creates a <see cref="T:System.Data.DataTable" /> named "Table".</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000578 RID: 1400
		int Fill(DataSet dataSet);

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> named "Table" to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataTable" /> objects that contain schema information returned from the data source.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000579 RID: 1401
		DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType);

		/// <summary>Gets the parameters set by the user when executing an SQL SELECT statement.</summary>
		/// <returns>An array of <see cref="T:System.Data.IDataParameter" /> objects that contains the parameters set by the user.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600057A RID: 1402
		IDataParameter[] GetFillParameters();

		/// <summary>Calls the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified <see cref="T:System.Data.DataSet" /> from a <see cref="T:System.Data.DataTable" /> named "Table".</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> used to update the data source. </param>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600057B RID: 1403
		int Update(DataSet dataSet);

		/// <summary>Indicates or specifies whether unmapped source tables or columns are passed with their source names in order to be filtered or to raise an error.</summary>
		/// <returns>One of the <see cref="T:System.Data.MissingMappingAction" /> values. The default is Passthrough.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not one of the <see cref="T:System.Data.MissingMappingAction" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600057C RID: 1404
		// (set) Token: 0x0600057D RID: 1405
		MissingMappingAction MissingMappingAction { get; set; }

		/// <summary>Indicates or specifies whether missing source tables, columns, and their relationships are added to the dataset schema, ignored, or cause an error to be raised.</summary>
		/// <returns>One of the <see cref="T:System.Data.MissingSchemaAction" /> values. The default is Add.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is not one of the <see cref="T:System.Data.MissingSchemaAction" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600057E RID: 1406
		// (set) Token: 0x0600057F RID: 1407
		MissingSchemaAction MissingSchemaAction { get; set; }

		/// <summary>Indicates how a source table is mapped to a dataset table.</summary>
		/// <returns>A collection that provides the master mapping between the returned records and the <see cref="T:System.Data.DataSet" />. The default value is an empty collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000580 RID: 1408
		ITableMappingCollection TableMappings { get; }
	}
}
