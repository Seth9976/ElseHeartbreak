using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Provides data for the <see cref="E:System.Data.Odbc.OdbcDataAdapter.RowUpdated" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200013C RID: 316
	public sealed class OdbcRowUpdatedEventArgs : RowUpdatedEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcRowUpdatedEventArgs" /> class.</summary>
		/// <param name="row">The DataRow sent through an update operation. </param>
		/// <param name="command">The <see cref="T:System.Data.Odbc.OdbcCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called. </param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed. </param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		// Token: 0x06001127 RID: 4391 RVA: 0x00043358 File Offset: 0x00041558
		public OdbcRowUpdatedEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}

		/// <summary>Gets the <see cref="T:System.Data.Odbc.OdbcCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</summary>
		/// <returns>The <see cref="T:System.Data.Odbc.OdbcCommand" /> executed when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x00043368 File Offset: 0x00041568
		public new OdbcCommand Command
		{
			get
			{
				return (OdbcCommand)base.Command;
			}
		}
	}
}
