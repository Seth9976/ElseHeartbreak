using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000366 RID: 870
	internal class MibIcmpV6Statistics : IcmpV6Statistics
	{
		// Token: 0x06001ED3 RID: 7891 RVA: 0x0005D208 File Offset: 0x0005B408
		public MibIcmpV6Statistics(global::System.Collections.Specialized.StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0005D218 File Offset: 0x0005B418
		private long Get(string name)
		{
			return (this.dic[name] == null) ? 0L : long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x0005D254 File Offset: 0x0005B454
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return this.Get("InDestUnreachs");
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x0005D264 File Offset: 0x0005B464
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return this.Get("OutDestUnreachs");
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x0005D274 File Offset: 0x0005B474
		public override long EchoRepliesReceived
		{
			get
			{
				return this.Get("InEchoReplies");
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x0005D284 File Offset: 0x0005B484
		public override long EchoRepliesSent
		{
			get
			{
				return this.Get("OutEchoReplies");
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x0005D294 File Offset: 0x0005B494
		public override long EchoRequestsReceived
		{
			get
			{
				return this.Get("InEchos");
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x0005D2A4 File Offset: 0x0005B4A4
		public override long EchoRequestsSent
		{
			get
			{
				return this.Get("OutEchos");
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x0005D2B4 File Offset: 0x0005B4B4
		public override long ErrorsReceived
		{
			get
			{
				return this.Get("InErrors");
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x0005D2C4 File Offset: 0x0005B4C4
		public override long ErrorsSent
		{
			get
			{
				return this.Get("OutErrors");
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x0005D2D4 File Offset: 0x0005B4D4
		public override long MembershipQueriesReceived
		{
			get
			{
				return this.Get("InGroupMembQueries");
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x0005D2E4 File Offset: 0x0005B4E4
		public override long MembershipQueriesSent
		{
			get
			{
				return this.Get("OutGroupMembQueries");
			}
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001EDF RID: 7903 RVA: 0x0005D2F4 File Offset: 0x0005B4F4
		public override long MembershipReductionsReceived
		{
			get
			{
				return this.Get("InGroupMembReductiions");
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x0005D304 File Offset: 0x0005B504
		public override long MembershipReductionsSent
		{
			get
			{
				return this.Get("OutGroupMembReductiions");
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x0005D314 File Offset: 0x0005B514
		public override long MembershipReportsReceived
		{
			get
			{
				return this.Get("InGroupMembRespons");
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x0005D324 File Offset: 0x0005B524
		public override long MembershipReportsSent
		{
			get
			{
				return this.Get("OutGroupMembRespons");
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x0005D334 File Offset: 0x0005B534
		public override long MessagesReceived
		{
			get
			{
				return this.Get("InMsgs");
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x0005D344 File Offset: 0x0005B544
		public override long MessagesSent
		{
			get
			{
				return this.Get("OutMsgs");
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x0005D354 File Offset: 0x0005B554
		public override long NeighborAdvertisementsReceived
		{
			get
			{
				return this.Get("InNeighborAdvertisements");
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x0005D364 File Offset: 0x0005B564
		public override long NeighborAdvertisementsSent
		{
			get
			{
				return this.Get("OutNeighborAdvertisements");
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x0005D374 File Offset: 0x0005B574
		public override long NeighborSolicitsReceived
		{
			get
			{
				return this.Get("InNeighborSolicits");
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x0005D384 File Offset: 0x0005B584
		public override long NeighborSolicitsSent
		{
			get
			{
				return this.Get("OutNeighborSolicits");
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x0005D394 File Offset: 0x0005B594
		public override long PacketTooBigMessagesReceived
		{
			get
			{
				return this.Get("InPktTooBigs");
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x0005D3A4 File Offset: 0x0005B5A4
		public override long PacketTooBigMessagesSent
		{
			get
			{
				return this.Get("OutPktTooBigs");
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x0005D3B4 File Offset: 0x0005B5B4
		public override long ParameterProblemsReceived
		{
			get
			{
				return this.Get("InParmProblems");
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x0005D3C4 File Offset: 0x0005B5C4
		public override long ParameterProblemsSent
		{
			get
			{
				return this.Get("OutParmProblems");
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x0005D3D4 File Offset: 0x0005B5D4
		public override long RedirectsReceived
		{
			get
			{
				return this.Get("InRedirects");
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0005D3E4 File Offset: 0x0005B5E4
		public override long RedirectsSent
		{
			get
			{
				return this.Get("OutRedirects");
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x0005D3F4 File Offset: 0x0005B5F4
		public override long RouterAdvertisementsReceived
		{
			get
			{
				return this.Get("InRouterAdvertisements");
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x0005D404 File Offset: 0x0005B604
		public override long RouterAdvertisementsSent
		{
			get
			{
				return this.Get("OutRouterAdvertisements");
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x0005D414 File Offset: 0x0005B614
		public override long RouterSolicitsReceived
		{
			get
			{
				return this.Get("InRouterSolicits");
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0005D424 File Offset: 0x0005B624
		public override long RouterSolicitsSent
		{
			get
			{
				return this.Get("OutRouterSolicits");
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x0005D434 File Offset: 0x0005B634
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return this.Get("InTimeExcds");
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x0005D444 File Offset: 0x0005B644
		public override long TimeExceededMessagesSent
		{
			get
			{
				return this.Get("OutTimeExcds");
			}
		}

		// Token: 0x040012F5 RID: 4853
		private global::System.Collections.Specialized.StringDictionary dic;
	}
}
