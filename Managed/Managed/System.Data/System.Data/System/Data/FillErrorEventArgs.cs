using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="E:System.Data.Common.DataAdapter.FillError" /> event of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000047 RID: 71
	public class FillErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.FillErrorEventArgs" /> class.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> being updated. </param>
		/// <param name="values">The values for the row being updated. </param>
		// Token: 0x06000548 RID: 1352 RVA: 0x0001D84C File Offset: 0x0001BA4C
		public FillErrorEventArgs(DataTable dataTable, object[] values)
		{
			this.data_table = dataTable;
			this.values = values;
		}

		/// <summary>Gets or sets a value indicating whether to continue the fill operation despite the error.</summary>
		/// <returns>true if the fill operation should continue; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001D864 File Offset: 0x0001BA64
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0001D86C File Offset: 0x0001BA6C
		public bool Continue
		{
			get
			{
				return this.f_continue;
			}
			set
			{
				this.f_continue = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> being updated when the error occurred.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> being updated.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0001D878 File Offset: 0x0001BA78
		public DataTable DataTable
		{
			get
			{
				return this.data_table;
			}
		}

		/// <summary>Gets the errors being handled.</summary>
		/// <returns>The errors being handled.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0001D880 File Offset: 0x0001BA80
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x0001D888 File Offset: 0x0001BA88
		public Exception Errors
		{
			get
			{
				return this.errors;
			}
			set
			{
				this.errors = value;
			}
		}

		/// <summary>Gets the values for the row being updated when the error occurred.</summary>
		/// <returns>The values for the row being updated.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0001D894 File Offset: 0x0001BA94
		public object[] Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x040001BD RID: 445
		private DataTable data_table;

		// Token: 0x040001BE RID: 446
		private object[] values;

		// Token: 0x040001BF RID: 447
		private Exception errors;

		// Token: 0x040001C0 RID: 448
		private bool f_continue;
	}
}
