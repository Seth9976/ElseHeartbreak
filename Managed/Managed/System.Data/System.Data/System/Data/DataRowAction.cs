using System;

namespace System.Data
{
	/// <summary>Describes an action performed on a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200002A RID: 42
	[Flags]
	public enum DataRowAction
	{
		/// <summary>The row has been added to the table.</summary>
		// Token: 0x040000F9 RID: 249
		Add = 16,
		/// <summary>The row has changed.</summary>
		// Token: 0x040000FA RID: 250
		Change = 2,
		/// <summary>The original and the current versions of the row have been changed.</summary>
		// Token: 0x040000FB RID: 251
		ChangeCurrentAndOriginal = 64,
		/// <summary>The original version of the row has been changed.</summary>
		// Token: 0x040000FC RID: 252
		ChangeOriginal = 32,
		/// <summary>The changes to the row have been committed.</summary>
		// Token: 0x040000FD RID: 253
		Commit = 8,
		/// <summary>The row was deleted from the table.</summary>
		// Token: 0x040000FE RID: 254
		Delete = 1,
		/// <summary>The row has not changed.</summary>
		// Token: 0x040000FF RID: 255
		Nothing = 0,
		/// <summary>The most recent change to the row has been rolled back.</summary>
		// Token: 0x04000100 RID: 256
		Rollback = 4
	}
}
