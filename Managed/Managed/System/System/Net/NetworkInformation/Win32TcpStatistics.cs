using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003C0 RID: 960
	internal class Win32TcpStatistics : TcpStatistics
	{
		// Token: 0x0600214B RID: 8523 RVA: 0x000621F4 File Offset: 0x000603F4
		public Win32TcpStatistics(Win32_MIB_TCPSTATS info)
		{
			this.info = info;
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x00062204 File Offset: 0x00060404
		public override long ConnectionsAccepted
		{
			get
			{
				return (long)((ulong)this.info.PassiveOpens);
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x00062214 File Offset: 0x00060414
		public override long ConnectionsInitiated
		{
			get
			{
				return (long)((ulong)this.info.ActiveOpens);
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x00062224 File Offset: 0x00060424
		public override long CumulativeConnections
		{
			get
			{
				return (long)((ulong)this.info.NumConns);
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x00062234 File Offset: 0x00060434
		public override long CurrentConnections
		{
			get
			{
				return (long)((ulong)this.info.CurrEstab);
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x00062244 File Offset: 0x00060444
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.info.InErrs);
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x00062254 File Offset: 0x00060454
		public override long FailedConnectionAttempts
		{
			get
			{
				return (long)((ulong)this.info.AttemptFails);
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x00062264 File Offset: 0x00060464
		public override long MaximumConnections
		{
			get
			{
				return (long)((ulong)this.info.MaxConn);
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x00062274 File Offset: 0x00060474
		public override long MaximumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.info.RtoMax);
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x00062284 File Offset: 0x00060484
		public override long MinimumTransmissionTimeout
		{
			get
			{
				return (long)((ulong)this.info.RtoMin);
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x00062294 File Offset: 0x00060494
		public override long ResetConnections
		{
			get
			{
				return (long)((ulong)this.info.EstabResets);
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x000622A4 File Offset: 0x000604A4
		public override long ResetsSent
		{
			get
			{
				return (long)((ulong)this.info.OutRsts);
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x000622B4 File Offset: 0x000604B4
		public override long SegmentsReceived
		{
			get
			{
				return (long)((ulong)this.info.InSegs);
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x000622C4 File Offset: 0x000604C4
		public override long SegmentsResent
		{
			get
			{
				return (long)((ulong)this.info.RetransSegs);
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x000622D4 File Offset: 0x000604D4
		public override long SegmentsSent
		{
			get
			{
				return (long)((ulong)this.info.OutSegs);
			}
		}

		// Token: 0x04001453 RID: 5203
		private Win32_MIB_TCPSTATS info;
	}
}
