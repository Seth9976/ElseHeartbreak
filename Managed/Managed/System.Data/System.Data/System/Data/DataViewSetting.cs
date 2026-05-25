using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Represents the default settings for <see cref="P:System.Data.DataView.ApplyDefaultSort" />, <see cref="P:System.Data.DataView.DataViewManager" />, <see cref="P:System.Data.DataView.RowFilter" />, <see cref="P:System.Data.DataView.RowStateFilter" />, <see cref="P:System.Data.DataView.Sort" />, and <see cref="P:System.Data.DataView.Table" /> for DataViews created from the <see cref="T:System.Data.DataViewManager" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200003F RID: 63
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DataViewSetting
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x0001D450 File Offset: 0x0001B650
		internal DataViewSetting(DataViewManager manager, DataTable table)
		{
			this.dataViewManager = manager;
			this.dataTable = table;
		}

		/// <summary>Gets or sets a value indicating whether to use the default sort.</summary>
		/// <returns>true if the default sort is used; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001D490 File Offset: 0x0001B690
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0001D498 File Offset: 0x0001B698
		public bool ApplyDefaultSort
		{
			get
			{
				return this.applyDefaultSort;
			}
			set
			{
				this.applyDefaultSort = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewManager" /> that contains this <see cref="T:System.Data.DataViewSetting" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewManager" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0001D4A4 File Offset: 0x0001B6A4
		[Browsable(false)]
		public DataViewManager DataViewManager
		{
			get
			{
				return this.dataViewManager;
			}
		}

		/// <summary>Gets or sets the filter to apply in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A string that contains the filter to apply.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0001D4AC File Offset: 0x0001B6AC
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0001D4B4 File Offset: 0x0001B6B4
		public string RowFilter
		{
			get
			{
				return this.rowFilter;
			}
			set
			{
				this.rowFilter = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to display Current, Deleted, Modified Current, ModifiedOriginal, New, Original, Unchanged, or no rows in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A value that indicates which rows to display.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001D4C0 File Offset: 0x0001B6C0
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0001D4C8 File Offset: 0x0001B6C8
		public DataViewRowState RowStateFilter
		{
			get
			{
				return this.rowStateFilter;
			}
			set
			{
				this.rowStateFilter = value;
			}
		}

		/// <summary>Gets or sets a value indicating the sort to apply in the <see cref="T:System.Data.DataView" />. </summary>
		/// <returns>The sort to apply in the <see cref="T:System.Data.DataView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0001D4D4 File Offset: 0x0001B6D4
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0001D4DC File Offset: 0x0001B6DC
		public string Sort
		{
			get
			{
				return this.sort;
			}
			set
			{
				this.sort = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> to which the <see cref="T:System.Data.DataViewSetting" /> properties apply.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001D4E8 File Offset: 0x0001B6E8
		[Browsable(false)]
		public DataTable Table
		{
			get
			{
				return this.dataTable;
			}
		}

		// Token: 0x04000199 RID: 409
		private bool applyDefaultSort;

		// Token: 0x0400019A RID: 410
		private DataViewManager dataViewManager;

		// Token: 0x0400019B RID: 411
		private string rowFilter = string.Empty;

		// Token: 0x0400019C RID: 412
		private DataViewRowState rowStateFilter = DataViewRowState.CurrentRows;

		// Token: 0x0400019D RID: 413
		private string sort = string.Empty;

		// Token: 0x0400019E RID: 414
		private DataTable dataTable;
	}
}
