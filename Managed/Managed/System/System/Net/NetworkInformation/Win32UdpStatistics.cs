using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003C4 RID: 964
	internal class Win32UdpStatistics : UdpStatistics
	{
		// Token: 0x06002167 RID: 8551 RVA: 0x00062388 File Offset: 0x00060588
		public Win32UdpStatistics(Win32_MIB_UDPSTATS info)
		{
			this.info = info;
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x00062398 File Offset: 0x00060598
		public override long DatagramsReceived
		{
			get
			{
				return (long)((ulong)this.info.InDatagrams);
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x000623A8 File Offset: 0x000605A8
		public override long DatagramsSent
		{
			get
			{
				return (long)((ulong)this.info.OutDatagrams);
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x000623B8 File Offset: 0x000605B8
		public override long IncomingDatagramsDiscarded
		{
			get
			{
				return (long)((ulong)this.info.NoPorts);
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600216B RID: 8555 RVA: 0x000623C8 File Offset: 0x000605C8
		public override long IncomingDatagramsWithErrors
		{
			get
			{
				return (long)((ulong)this.info.InErrors);
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x000623D8 File Offset: 0x000605D8
		public override int UdpListeners
		{
			get
			{
				return this.info.NumAddrs;
			}
		}

		// Token: 0x04001464 RID: 5220
		private Win32_MIB_UDPSTATS info;
	}
}
