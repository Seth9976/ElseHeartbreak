using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003C3 RID: 963
	internal class MibUdpStatistics : UdpStatistics
	{
		// Token: 0x06002160 RID: 8544 RVA: 0x000622EC File Offset: 0x000604EC
		public MibUdpStatistics(global::System.Collections.Specialized.StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000622FC File Offset: 0x000604FC
		private long Get(string name)
		{
			return (this.dic[name] == null) ? 0L : long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x00062338 File Offset: 0x00060538
		public override long DatagramsReceived
		{
			get
			{
				return this.Get("InDatagrams");
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002163 RID: 8547 RVA: 0x00062348 File Offset: 0x00060548
		public override long DatagramsSent
		{
			get
			{
				return this.Get("OutDatagrams");
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x00062358 File Offset: 0x00060558
		public override long IncomingDatagramsDiscarded
		{
			get
			{
				return this.Get("NoPorts");
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x00062368 File Offset: 0x00060568
		public override long IncomingDatagramsWithErrors
		{
			get
			{
				return this.Get("InErrors");
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x00062378 File Offset: 0x00060578
		public override int UdpListeners
		{
			get
			{
				return (int)this.Get("NumAddrs");
			}
		}

		// Token: 0x04001463 RID: 5219
		private global::System.Collections.Specialized.StringDictionary dic;
	}
}
