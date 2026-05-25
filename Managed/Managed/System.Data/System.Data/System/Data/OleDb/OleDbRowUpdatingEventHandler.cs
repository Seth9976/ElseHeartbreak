using System;

namespace System.Data.OleDb
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.OleDb.OleDbDataAdapter.RowUpdating" /> event of an <see cref="T:System.Data.OleDb.OleDbDataAdapter" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">The <see cref="T:System.Data.OleDb.OleDbRowUpdatingEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B6 RID: 438
	// (Invoke) Token: 0x060015BD RID: 5565
	public delegate void OleDbRowUpdatingEventHandler(object sender, OleDbRowUpdatingEventArgs e);
}
