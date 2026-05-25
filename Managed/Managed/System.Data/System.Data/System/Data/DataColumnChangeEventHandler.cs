using System;

namespace System.Data
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Data.DataTable.ColumnChanging" /> event.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Data.DataColumnChangeEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001AC RID: 428
	// (Invoke) Token: 0x06001595 RID: 5525
	public delegate void DataColumnChangeEventHandler(object sender, DataColumnChangeEventArgs e);
}
