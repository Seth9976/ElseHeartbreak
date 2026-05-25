using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataTable.RowChanged" />, <see cref="E:System.Data.DataTable.RowChanging" />, <see cref="M:System.Data.DataTable.OnRowDeleting(System.Data.DataRowChangeEventArgs)" />, and <see cref="M:System.Data.DataTable.OnRowDeleted(System.Data.DataRowChangeEventArgs)" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200002C RID: 44
	public class DataRowChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRowChangeEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> upon which an action is occuring. </param>
		/// <param name="action">One of the <see cref="T:System.Data.DataRowAction" /> values. </param>
		// Token: 0x06000260 RID: 608 RVA: 0x00010B50 File Offset: 0x0000ED50
		public DataRowChangeEventArgs(DataRow row, DataRowAction action)
		{
			this.row = row;
			this.action = action;
		}

		/// <summary>Gets the action that has occurred on a <see cref="T:System.Data.DataRow" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowAction" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00010B68 File Offset: 0x0000ED68
		public DataRowAction Action
		{
			get
			{
				return this.action;
			}
		}

		/// <summary>Gets the row upon which an action has occurred.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> upon which an action has occurred.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00010B70 File Offset: 0x0000ED70
		public DataRow Row
		{
			get
			{
				return this.row;
			}
		}

		// Token: 0x04000103 RID: 259
		private DataRow row;

		// Token: 0x04000104 RID: 260
		private DataRowAction action;
	}
}
