using System;

namespace System.Data
{
	/// <summary>Occurs when a target and source DataRow have the same primary key value, and the <see cref="P:System.Data.DataSet.EnforceConstraints" /> property is set to true.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000060 RID: 96
	public class MergeFailedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.MergeFailedEventArgs" /> class with the <see cref="T:System.Data.DataTable" /> and a description of the merge conflict.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> object. </param>
		/// <param name="conflict">A description of the merge conflict. </param>
		// Token: 0x06000610 RID: 1552 RVA: 0x0001E4CC File Offset: 0x0001C6CC
		public MergeFailedEventArgs(DataTable table, string conflict)
		{
			this.data_table = table;
			this.conflict = conflict;
		}

		/// <summary>Returns the <see cref="T:System.Data.DataTable" /> object.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001E4E4 File Offset: 0x0001C6E4
		public DataTable Table
		{
			get
			{
				return this.data_table;
			}
		}

		/// <summary>Returns a description of the merge conflict.</summary>
		/// <returns>A description of the merge conflict.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0001E4EC File Offset: 0x0001C6EC
		public string Conflict
		{
			get
			{
				return this.conflict;
			}
		}

		// Token: 0x040001E3 RID: 483
		private readonly DataTable data_table;

		// Token: 0x040001E4 RID: 484
		private readonly string conflict;
	}
}
