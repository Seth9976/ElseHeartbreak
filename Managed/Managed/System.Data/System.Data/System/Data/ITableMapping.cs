using System;

namespace System.Data
{
	/// <summary>Associates a source table with a table in a <see cref="T:System.Data.DataSet" />, and is implemented by the <see cref="T:System.Data.Common.DataTableMapping" /> class, which is used in common by .NET Framework data providers.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005B RID: 91
	public interface ITableMapping
	{
		/// <summary>Gets the derived <see cref="T:System.Data.Common.DataColumnMappingCollection" /> for the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DataColumnMappingCollection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000604 RID: 1540
		IColumnMappingCollection ColumnMappings { get; }

		/// <summary>Gets or sets the case-insensitive name of the table within the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The case-insensitive name of the table within the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000605 RID: 1541
		// (set) Token: 0x06000606 RID: 1542
		string DataSetTable { get; set; }

		/// <summary>Gets or sets the case-sensitive name of the source table.</summary>
		/// <returns>The case-sensitive name of the source table.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000607 RID: 1543
		// (set) Token: 0x06000608 RID: 1544
		string SourceTable { get; set; }
	}
}
