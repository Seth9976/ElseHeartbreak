using System;

namespace System.Net.Sockets
{
	/// <summary>Encapsulates the information that is necessary to duplicate a <see cref="T:System.Net.Sockets.Socket" />.</summary>
	// Token: 0x02000402 RID: 1026
	[Serializable]
	public struct SocketInformation
	{
		/// <summary>Gets or sets the options for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>A <see cref="T:System.Net.Sockets.SocketInformationOptions" /> instance.</returns>
		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600242E RID: 9262 RVA: 0x0006CBE0 File Offset: 0x0006ADE0
		// (set) Token: 0x0600242F RID: 9263 RVA: 0x0006CBE8 File Offset: 0x0006ADE8
		public SocketInformationOptions Options
		{
			get
			{
				return this.options;
			}
			set
			{
				this.options = value;
			}
		}

		/// <summary>Gets or sets the protocol information for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
		/// <returns>An array of type <see cref="T:System.Byte" />.</returns>
		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002430 RID: 9264 RVA: 0x0006CBF4 File Offset: 0x0006ADF4
		// (set) Token: 0x06002431 RID: 9265 RVA: 0x0006CBFC File Offset: 0x0006ADFC
		public byte[] ProtocolInformation
		{
			get
			{
				return this.protocol_info;
			}
			set
			{
				this.protocol_info = value;
			}
		}

		// Token: 0x0400166D RID: 5741
		private SocketInformationOptions options;

		// Token: 0x0400166E RID: 5742
		private byte[] protocol_info;
	}
}
