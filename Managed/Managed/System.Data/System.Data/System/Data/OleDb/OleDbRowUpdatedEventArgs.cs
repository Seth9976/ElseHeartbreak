using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Provides data for the <see cref="E:System.Data.OleDb.OleDbDataAdapter.RowUpdated" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FB RID: 251
	public sealed class OleDbRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbRowUpdatedEventArgs" /> class.</summary>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> sent through an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called. </param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed. </param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		// Token: 0x06000C2F RID: 3119 RVA: 0x00034384 File Offset: 0x00032584
		public OleDbRowUpdatedEventArgs(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(dataRow, command, statementType, tableMapping)
		{
		}

		/// <summary>Gets the <see cref="T:System.Data.OleDb.OleDbCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00034394 File Offset: 0x00032594
		public new OleDbCommand Command
		{
			get
			{
				return (OleDbCommand)base.Command;
			}
		}
	}
}
