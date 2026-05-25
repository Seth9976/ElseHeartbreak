using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Provides data for the <see cref="E:System.Data.Odbc.OdbcDataAdapter.RowUpdating" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200013D RID: 317
	public sealed class OdbcRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcRowUpdatingEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to update. </param>
		/// <param name="command">The <see cref="T:System.Data.Odbc.OdbcCommand" /> to execute during the update operation. </param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed. </param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		// Token: 0x06001129 RID: 4393 RVA: 0x00043378 File Offset: 0x00041578
		public OdbcRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcCommand" /> to execute when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</summary>
		/// <returns>The <see cref="T:System.Data.Odbc.OdbcCommand" /> to execute when <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> is called.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00043388 File Offset: 0x00041588
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x00043398 File Offset: 0x00041598
		public new OdbcCommand Command
		{
			get
			{
				return (OdbcCommand)base.Command;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x000433A4 File Offset: 0x000415A4
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x000433AC File Offset: 0x000415AC
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.Command;
			}
			set
			{
				base.Command = value;
			}
		}
	}
}
