using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000368 RID: 872
	internal class Win32IcmpV6Statistics : IcmpV6Statistics
	{
		// Token: 0x06001EF6 RID: 7926 RVA: 0x0005D45C File Offset: 0x0005B65C
		public Win32IcmpV6Statistics(Win32_MIB_ICMP_EX info)
		{
			this.iin = info.InStats;
			this.iout = info.OutStats;
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x0005D48C File Offset: 0x0005B68C
		public override long DestinationUnreachableMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[1]);
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x0005D49C File Offset: 0x0005B69C
		public override long DestinationUnreachableMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[1]);
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x0005D4AC File Offset: 0x0005B6AC
		public override long EchoRepliesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[129]);
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x0005D4C0 File Offset: 0x0005B6C0
		public override long EchoRepliesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[129]);
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x0005D4D4 File Offset: 0x0005B6D4
		public override long EchoRequestsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[128]);
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x0005D4E8 File Offset: 0x0005B6E8
		public override long EchoRequestsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[128]);
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x0005D4FC File Offset: 0x0005B6FC
		public override long ErrorsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Errors);
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x0005D50C File Offset: 0x0005B70C
		public override long ErrorsSent
		{
			get
			{
				return (long)((ulong)this.iout.Errors);
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x0005D51C File Offset: 0x0005B71C
		public override long MembershipQueriesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[130]);
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x0005D530 File Offset: 0x0005B730
		public override long MembershipQueriesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[130]);
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x0005D544 File Offset: 0x0005B744
		public override long MembershipReductionsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[132]);
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x0005D558 File Offset: 0x0005B758
		public override long MembershipReductionsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[132]);
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x0005D56C File Offset: 0x0005B76C
		public override long MembershipReportsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[131]);
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x0005D580 File Offset: 0x0005B780
		public override long MembershipReportsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[131]);
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x0005D594 File Offset: 0x0005B794
		public override long MessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Msgs);
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x0005D5A4 File Offset: 0x0005B7A4
		public override long MessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Msgs);
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x0005D5B4 File Offset: 0x0005B7B4
		public override long NeighborAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[136]);
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x0005D5C8 File Offset: 0x0005B7C8
		public override long NeighborAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[136]);
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x0005D5DC File Offset: 0x0005B7DC
		public override long NeighborSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[135]);
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x0005D5F0 File Offset: 0x0005B7F0
		public override long NeighborSolicitsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[135]);
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x0005D604 File Offset: 0x0005B804
		public override long PacketTooBigMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[2]);
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x0005D614 File Offset: 0x0005B814
		public override long PacketTooBigMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[2]);
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x0005D624 File Offset: 0x0005B824
		public override long ParameterProblemsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[4]);
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x0005D634 File Offset: 0x0005B834
		public override long ParameterProblemsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[4]);
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x0005D644 File Offset: 0x0005B844
		public override long RedirectsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[137]);
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x0005D658 File Offset: 0x0005B858
		public override long RedirectsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[137]);
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x0005D66C File Offset: 0x0005B86C
		public override long RouterAdvertisementsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[134]);
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x0005D680 File Offset: 0x0005B880
		public override long RouterAdvertisementsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[134]);
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x0005D694 File Offset: 0x0005B894
		public override long RouterSolicitsReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[133]);
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x0005D6A8 File Offset: 0x0005B8A8
		public override long RouterSolicitsSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[133]);
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x0005D6BC File Offset: 0x0005B8BC
		public override long TimeExceededMessagesReceived
		{
			get
			{
				return (long)((ulong)this.iin.Counts[3]);
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x0005D6CC File Offset: 0x0005B8CC
		public override long TimeExceededMessagesSent
		{
			get
			{
				return (long)((ulong)this.iout.Counts[3]);
			}
		}

		// Token: 0x04001305 RID: 4869
		private Win32_MIBICMPSTATS_EX iin;

		// Token: 0x04001306 RID: 4870
		private Win32_MIBICMPSTATS_EX iout;
	}
}
