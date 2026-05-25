using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Provides data for the <see cref="E:System.Data.SqlClient.SqlDataAdapter.RowUpdating" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000175 RID: 373
	public sealed class SqlRowUpdatingEventArgs : RowUpdatingEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlRowUpdatingEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> to execute during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="statementType">One of the <see cref="T:System.Data.StatementType" /> values that specifies the type of query executed. </param>
		/// <param name="tableMapping">The <see cref="T:System.Data.Common.DataTableMapping" /> sent through an <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />. </param>
		// Token: 0x06001413 RID: 5139 RVA: 0x000543A0 File Offset: 0x000525A0
		public SqlRowUpdatingEventArgs(DataRow row, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
			: base(row, command, statementType, tableMapping)
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlClient.SqlCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlCommand" /> to execute when performing the <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x000543B0 File Offset: 0x000525B0
		// (set) Token: 0x06001415 RID: 5141 RVA: 0x000543C0 File Offset: 0x000525C0
		public new SqlCommand Command
		{
			get
			{
				return (SqlCommand)base.Command;
			}
			set
			{
				base.Command = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x000543CC File Offset: 0x000525CC
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x000543D4 File Offset: 0x000525D4
		protected override IDbCommand BaseCommand
		{
			get
			{
				return base.BaseCommand;
			}
			set
			{
				base.BaseCommand = value as SqlCommand;
			}
		}
	}
}
