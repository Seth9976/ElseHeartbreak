using System;

namespace System.Data
{
	/// <summary>Provides data for the state change event of a .NET Framework data provider.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000073 RID: 115
	public sealed class StateChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.StateChangeEventArgs" /> class, when given the original state and the current state of the object.</summary>
		/// <param name="originalState">One of the <see cref="T:System.Data.ConnectionState" /> values. </param>
		/// <param name="currentState">One of the <see cref="T:System.Data.ConnectionState" /> values. </param>
		// Token: 0x06000649 RID: 1609 RVA: 0x0001F310 File Offset: 0x0001D510
		public StateChangeEventArgs(ConnectionState originalState, ConnectionState currentState)
		{
			this.originalState = originalState;
			this.currentState = currentState;
		}

		/// <summary>Gets the new state of the connection. The connection object will be in the new state already when the event is fired.</summary>
		/// <returns>One of the <see cref="T:System.Data.ConnectionState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0001F328 File Offset: 0x0001D528
		public ConnectionState CurrentState
		{
			get
			{
				return this.currentState;
			}
		}

		/// <summary>Gets the original state of the connection.</summary>
		/// <returns>One of the <see cref="T:System.Data.ConnectionState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001F330 File Offset: 0x0001D530
		public ConnectionState OriginalState
		{
			get
			{
				return this.originalState;
			}
		}

		// Token: 0x0400022B RID: 555
		private ConnectionState originalState;

		// Token: 0x0400022C RID: 556
		private ConnectionState currentState;
	}
}
