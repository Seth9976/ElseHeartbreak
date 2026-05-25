using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000361 RID: 865
	internal class MibIcmpV4Statistics : IcmpV4Statistics
	{
		// Token: 0x06001E7B RID: 7803 RVA: 0x0005CE44 File Offset: 0x0005B044
		public MibIcmpV4Statistics(global::System.Collections.Specialized.StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x0005CE54 File Offset: 0x0005B054
		private long Get(string name)
		{
			return (this.dic[name] == null) ? 0L : long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x0005CE90 File Offset: 0x0005B090
		public override long AddressMaskRepliesReceived
		{
			get
			{
				return this.Get("InAddrMaskReps");
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001E7E RID: 7806 RVA: 0x0005CEA0 File Offset: 0x0005B0A0
		public override long AddressMaskRepliesSent
		{
			get
			{
				return this.Get("OutAddrMaskReps");
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x0005CEB0 File Offset: 0x0005B0B0
		public override long AddressMaskRequestsReceived
		{
			get
			{
				return this.Get("InAddrMasks");
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x0005CEC0 File Offset: 0x0005B0C0
		public override long AddressMaskRequestsSent
		{
			get
			{
				return this.Get("OutAddrMasks");
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x0005CED0 File Offset: 0x0005B0D0
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return this.Get("InDestUnreachs");
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x0005CEE0 File Offset: 0x0005B0E0
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return this.Get("OutDestUnreachs");
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x0005CEF0 File Offset: 0x0005B0F0
		public override long EchoRepliesReceived
		{
			get
			{
				return this.Get("InEchoReps");
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001E84 RID: 7812 RVA: 0x0005CF00 File Offset: 0x0005B100
		public override long EchoRepliesSent
		{
			get
			{
				return this.Get("OutEchoReps");
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x0005CF10 File Offset: 0x0005B110
		public override long EchoRequestsReceived
		{
			get
			{
				return this.Get("InEchos");
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x0005CF20 File Offset: 0x0005B120
		public override long EchoRequestsSent
		{
			get
			{
				return this.Get("OutEchos");
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0005CF30 File Offset: 0x0005B130
		public override long ErrorsReceived
		{
			get
			{
				return this.Get("InErrors");
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x0005CF40 File Offset: 0x0005B140
		public override long ErrorsSent
		{
			get
			{
				return this.Get("OutErrors");
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x0005CF50 File Offset: 0x0005B150
		public override long MessagesReceived
		{
			get
			{
				return this.Get("InMsgs");
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x0005CF60 File Offset: 0x0005B160
		public override long MessagesSent
		{
			get
			{
				return this.Get("OutMsgs");
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x0005CF70 File Offset: 0x0005B170
		public override long ParameterProblemsReceived
		{
			get
			{
				return this.Get("InParmProbs");
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0005CF80 File Offset: 0x0005B180
		public override long ParameterProblemsSent
		{
			get
			{
				return this.Get("OutParmProbs");
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x0005CF90 File Offset: 0x0005B190
		public override long RedirectsReceived
		{
			get
			{
				return this.Get("InRedirects");
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001E8E RID: 7822 RVA: 0x0005CFA0 File Offset: 0x0005B1A0
		public override long RedirectsSent
		{
			get
			{
				return this.Get("OutRedirects");
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x0005CFB0 File Offset: 0x0005B1B0
		public override long SourceQuenchesReceived
		{
			get
			{
				return this.Get("InSrcQuenchs");
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x0005CFC0 File Offset: 0x0005B1C0
		public override long SourceQuenchesSent
		{
			get
			{
				return this.Get("OutSrcQuenchs");
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x0005CFD0 File Offset: 0x0005B1D0
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return this.Get("InTimeExcds");
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x0005CFE0 File Offset: 0x0005B1E0
		public override long TimeExceededMessagesSent
		{
			get
			{
				return this.Get("OutTimeExcds");
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x0005CFF0 File Offset: 0x0005B1F0
		public override long TimestampRepliesReceived
		{
			get
			{
				return this.Get("InTimestampReps");
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x0005D000 File Offset: 0x0005B200
		public override long TimestampRepliesSent
		{
			get
			{
				return this.Get("OutTimestampReps");
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x0005D010 File Offset: 0x0005B210
		public override long TimestampRequestsReceived
		{
			get
			{
				return this.Get("InTimestamps");
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0005D020 File Offset: 0x0005B220
		public override long TimestampRequestsSent
		{
			get
			{
				return this.Get("OutTimestamps");
			}
		}

		// Token: 0x040012E3 RID: 4835
		private global::System.Collections.Specialized.StringDictionary dic;
	}
}
