using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="M:System.Data.DataTable.NewRow" /> method.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200003A RID: 58
	public sealed class DataTableNewRowEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Data.DataTableNewRowEventArgs" />.</summary>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> being added.</param>
		// Token: 0x06000460 RID: 1120 RVA: 0x0001ADD8 File Offset: 0x00018FD8
		public DataTableNewRowEventArgs(DataRow row)
		{
			this._row = row;
		}

		/// <summary>Gets the row that is being added.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> that is being added. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0001ADE8 File Offset: 0x00018FE8
		public DataRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x0400016C RID: 364
		private readonly DataRow _row;
	}
}
