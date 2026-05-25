using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Provides data for the <see cref="E:System.Data.OleDb.OleDbDataAdapter.RowUpdating" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FC RID: 252
	public sealed class OleDbRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbRowUpdatingEventArgs" /> class.</summary>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> to <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> to execute during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed. </param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		// Token: 0x06000C31 RID: 3121 RVA: 0x000343A4 File Offset: 0x000325A4
		public OleDbRowUpdatingEventArgs(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(dataRow, command, statementType, tableMapping)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x000343B4 File Offset: 0x000325B4
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x000343C4 File Offset: 0x000325C4
		public new OleDbCommand Command
		{
			get
			{
				return (OleDbCommand)base.Command;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x000343D0 File Offset: 0x000325D0
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x000343D8 File Offset: 0x000325D8
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = value;
			}
		}
	}
}
