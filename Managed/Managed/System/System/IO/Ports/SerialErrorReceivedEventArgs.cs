using System;

namespace System.IO.Ports
{
	/// <summary>Prepares data for the <see cref="E:System.IO.Ports.SerialPort.ErrorReceived" /> event.</summary>
	// Token: 0x0200029A RID: 666
	public class SerialErrorReceivedEventArgs : EventArgs
	{
		// Token: 0x060016FF RID: 5887 RVA: 0x0003F5B0 File Offset: 0x0003D7B0
		internal SerialErrorReceivedEventArgs(SerialError eventType)
		{
			this.eventType = eventType;
		}

		/// <summary>Gets or sets the event type.</summary>
		/// <returns>One of the <see cref="T:System.IO.Ports.SerialError" /> values.</returns>
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0003F5C0 File Offset: 0x0003D7C0
		public SerialError EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x04000E9C RID: 3740
		private SerialError eventType;
	}
}
