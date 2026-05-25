using System;

namespace System.Data
{
	/// <summary>Describes the current state of the connection to a data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000010 RID: 16
	[Flags]
	public enum ConnectionState
	{
		/// <summary>The connection is closed.</summary>
		// Token: 0x0400006F RID: 111
		Closed = 0,
		/// <summary>The connection is open.</summary>
		// Token: 0x04000070 RID: 112
		Open = 1,
		/// <summary>The connection object is connecting to the data source. (This value is reserved for future versions of the product.) </summary>
		// Token: 0x04000071 RID: 113
		Connecting = 2,
		/// <summary>The connection object is executing a command. (This value is reserved for future versions of the product.) </summary>
		// Token: 0x04000072 RID: 114
		Executing = 4,
		/// <summary>The connection object is retrieving data. (This value is reserved for future versions of the product.) </summary>
		// Token: 0x04000073 RID: 115
		Fetching = 8,
		/// <summary>The connection to the data source is broken. This can occur only after the connection has been opened. A connection in this state may be closed and then re-opened. (This value is reserved for future versions of the product.) </summary>
		// Token: 0x04000074 RID: 116
		Broken = 16
	}
}
