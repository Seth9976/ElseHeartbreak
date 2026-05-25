using System;

namespace System.Data.SqlClient
{
	/// <summary>Represents the set of arguments passed to the <see cref="T:System.Data.SqlClient.SqlRowsCopiedEventHandler" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017E RID: 382
	public class SqlRowsCopiedEventArgs : EventArgs
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlRowsCopiedEventArgs" /> object.</summary>
		/// <param name="rowsCopied">An <see cref="T:System.Int64" /> that indicates the number of rows copied during the current bulk copy operation. </param>
		// Token: 0x0600146E RID: 5230 RVA: 0x00055EA0 File Offset: 0x000540A0
		public SqlRowsCopiedEventArgs(long rowsCopied)
		{
			this.rowsCopied = rowsCopied;
		}

		/// <summary>Gets or sets a value that indicates whether the bulk copy operation should be aborted.</summary>
		/// <returns>true if the bulk copy operation should be aborted; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x00055EB0 File Offset: 0x000540B0
		// (set) Token: 0x06001470 RID: 5232 RVA: 0x00055EB8 File Offset: 0x000540B8
		public bool Abort
		{
			get
			{
				return this.abort;
			}
			set
			{
				this.abort = value;
			}
		}

		/// <summary>Gets a value that returns the number of rows copied during the current bulk copy operation.</summary>
		/// <returns>int that returns the number of rows copied.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x00055EC4 File Offset: 0x000540C4
		public long RowsCopied
		{
			get
			{
				return this.rowsCopied;
			}
		}

		// Token: 0x04000827 RID: 2087
		private long rowsCopied;

		// Token: 0x04000828 RID: 2088
		private bool abort;
	}
}
