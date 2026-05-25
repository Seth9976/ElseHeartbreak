using System;

namespace System.Data.Odbc
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.Odbc.OdbcDataAdapter.RowUpdated" /> event of an <see cref="T:System.Data.Odbc.OdbcDataAdapter" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">The <see cref="T:System.Data.Odbc.OdbcRowUpdatedEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B7 RID: 439
	// (Invoke) Token: 0x060015C1 RID: 5569
	public delegate void OdbcRowUpdatedEventHandler(object sender, OdbcRowUpdatedEventArgs e);
}
