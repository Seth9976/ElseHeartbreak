using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200037A RID: 890
	internal class MibIPGlobalStatistics : IPGlobalStatistics
	{
		// Token: 0x06001FAE RID: 8110 RVA: 0x0005EB5C File Offset: 0x0005CD5C
		public MibIPGlobalStatistics(global::System.Collections.Specialized.StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0005EB6C File Offset: 0x0005CD6C
		private long Get(string name)
		{
			return (this.dic[name] == null) ? 0L : long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x0005EBA8 File Offset: 0x0005CDA8
		public override int DefaultTtl
		{
			get
			{
				return (int)this.Get("DefaultTTL");
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x0005EBB8 File Offset: 0x0005CDB8
		public override bool ForwardingEnabled
		{
			get
			{
				return this.Get("Forwarding") != 0L;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x0005EBCC File Offset: 0x0005CDCC
		public override int NumberOfInterfaces
		{
			get
			{
				return (int)this.Get("NumIf");
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x0005EBDC File Offset: 0x0005CDDC
		public override int NumberOfIPAddresses
		{
			get
			{
				return (int)this.Get("NumAddr");
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0005EBEC File Offset: 0x0005CDEC
		public override int NumberOfRoutes
		{
			get
			{
				return (int)this.Get("NumRoutes");
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001FB5 RID: 8117 RVA: 0x0005EBFC File Offset: 0x0005CDFC
		public override long OutputPacketRequests
		{
			get
			{
				return this.Get("OutRequests");
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x0005EC0C File Offset: 0x0005CE0C
		public override long OutputPacketRoutingDiscards
		{
			get
			{
				return this.Get("RoutingDiscards");
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x0005EC1C File Offset: 0x0005CE1C
		public override long OutputPacketsDiscarded
		{
			get
			{
				return this.Get("OutDiscards");
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x0005EC2C File Offset: 0x0005CE2C
		public override long OutputPacketsWithNoRoute
		{
			get
			{
				return this.Get("OutNoRoutes");
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x0005EC3C File Offset: 0x0005CE3C
		public override long PacketFragmentFailures
		{
			get
			{
				return this.Get("FragFails");
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x0005EC4C File Offset: 0x0005CE4C
		public override long PacketReassembliesRequired
		{
			get
			{
				return this.Get("ReasmReqds");
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x0005EC5C File Offset: 0x0005CE5C
		public override long PacketReassemblyFailures
		{
			get
			{
				return this.Get("ReasmFails");
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x0005EC6C File Offset: 0x0005CE6C
		public override long PacketReassemblyTimeout
		{
			get
			{
				return this.Get("ReasmTimeout");
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x0005EC7C File Offset: 0x0005CE7C
		public override long PacketsFragmented
		{
			get
			{
				return this.Get("FragOks");
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x0005EC8C File Offset: 0x0005CE8C
		public override long PacketsReassembled
		{
			get
			{
				return this.Get("ReasmOks");
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x0005EC9C File Offset: 0x0005CE9C
		public override long ReceivedPackets
		{
			get
			{
				return this.Get("InReceives");
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x0005ECAC File Offset: 0x0005CEAC
		public override long ReceivedPacketsDelivered
		{
			get
			{
				return this.Get("InDelivers");
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x0005ECBC File Offset: 0x0005CEBC
		public override long ReceivedPacketsDiscarded
		{
			get
			{
				return this.Get("InDiscards");
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x0005ECCC File Offset: 0x0005CECC
		public override long ReceivedPacketsForwarded
		{
			get
			{
				return this.Get("ForwDatagrams");
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x0005ECDC File Offset: 0x0005CEDC
		public override long ReceivedPacketsWithAddressErrors
		{
			get
			{
				return this.Get("InAddrErrors");
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x0005ECEC File Offset: 0x0005CEEC
		public override long ReceivedPacketsWithHeadersErrors
		{
			get
			{
				return this.Get("InHdrErrors");
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x0005ECFC File Offset: 0x0005CEFC
		public override long ReceivedPacketsWithUnknownProtocol
		{
			get
			{
				return this.Get("InUnknownProtos");
			}
		}

		// Token: 0x04001332 RID: 4914
		private global::System.Collections.Specialized.StringDictionary dic;
	}
}
