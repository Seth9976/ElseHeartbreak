using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003BF RID: 959
	internal class MibTcpStatistics : TcpStatistics
	{
		// Token: 0x0600213B RID: 8507 RVA: 0x000620C8 File Offset: 0x000602C8
		public MibTcpStatistics(global::System.Collections.Specialized.StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x000620D8 File Offset: 0x000602D8
		private long Get(string name)
		{
			return (this.dic[name] == null) ? 0L : long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x00062114 File Offset: 0x00060314
		public override long ConnectionsAccepted
		{
			get
			{
				return this.Get("PassiveOpens");
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x00062124 File Offset: 0x00060324
		public override long ConnectionsInitiated
		{
			get
			{
				return this.Get("ActiveOpens");
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x00062134 File Offset: 0x00060334
		public override long CumulativeConnections
		{
			get
			{
				return this.Get("NumConns");
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x00062144 File Offset: 0x00060344
		public override long CurrentConnections
		{
			get
			{
				return this.Get("CurrEstab");
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x00062154 File Offset: 0x00060354
		public override long ErrorsReceived
		{
			get
			{
				return this.Get("InErrs");
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x00062164 File Offset: 0x00060364
		public override long FailedConnectionAttempts
		{
			get
			{
				return this.Get("AttemptFails");
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x00062174 File Offset: 0x00060374
		public override long MaximumConnections
		{
			get
			{
				return this.Get("MaxConn");
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x00062184 File Offset: 0x00060384
		public override long MaximumTransmissionTimeout
		{
			get
			{
				return this.Get("RtoMax");
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x00062194 File Offset: 0x00060394
		public override long MinimumTransmissionTimeout
		{
			get
			{
				return this.Get("RtoMin");
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x000621A4 File Offset: 0x000603A4
		public override long ResetConnections
		{
			get
			{
				return this.Get("EstabResets");
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x000621B4 File Offset: 0x000603B4
		public override long ResetsSent
		{
			get
			{
				return this.Get("OutRsts");
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x000621C4 File Offset: 0x000603C4
		public override long SegmentsReceived
		{
			get
			{
				return this.Get("InSegs");
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x000621D4 File Offset: 0x000603D4
		public override long SegmentsResent
		{
			get
			{
				return this.Get("RetransSegs");
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x000621E4 File Offset: 0x000603E4
		public override long SegmentsSent
		{
			get
			{
				return this.Get("OutSegs");
			}
		}

		// Token: 0x04001452 RID: 5202
		private global::System.Collections.Specialized.StringDictionary dic;
	}
}
