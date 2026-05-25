using System;

namespace System.Data
{
	/// <summary>Gets the state of a <see cref="T:System.Data.DataRow" /> object.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200002E RID: 46
	[Flags]
	public enum DataRowState
	{
		/// <summary>The row has been created but is not part of any <see cref="T:System.Data.DataRowCollection" />. A <see cref="T:System.Data.DataRow" /> is in this state immediately after it has been created and before it is added to a collection, or if it has been removed from a collection.</summary>
		// Token: 0x04000108 RID: 264
		Detached = 1,
		/// <summary>The row has not changed since <see cref="M:System.Data.DataRow.AcceptChanges" /> was last called.</summary>
		// Token: 0x04000109 RID: 265
		Unchanged = 2,
		/// <summary>The row has been added to a <see cref="T:System.Data.DataRowCollection" />, and <see cref="M:System.Data.DataRow.AcceptChanges" /> has not been called.</summary>
		// Token: 0x0400010A RID: 266
		Added = 4,
		/// <summary>The row was deleted using the <see cref="M:System.Data.DataRow.Delete" /> method of the <see cref="T:System.Data.DataRow" />.</summary>
		// Token: 0x0400010B RID: 267
		Deleted = 8,
		/// <summary>The row has been modified and <see cref="M:System.Data.DataRow.AcceptChanges" /> has not been called.</summary>
		// Token: 0x0400010C RID: 268
		Modified = 16
	}
}
