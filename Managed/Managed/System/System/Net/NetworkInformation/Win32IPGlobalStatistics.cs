using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200037B RID: 891
	internal class Win32IPGlobalStatistics : IPGlobalStatistics
	{
		// Token: 0x06001FC6 RID: 8134 RVA: 0x0005ED0C File Offset: 0x0005CF0C
		public Win32IPGlobalStatistics(Win32_MIB_IPSTATS info)
		{
			this.info = info;
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x0005ED1C File Offset: 0x0005CF1C
		public override int DefaultTtl
		{
			get
			{
				return this.info.DefaultTTL;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x0005ED2C File Offset: 0x0005CF2C
		public override bool ForwardingEnabled
		{
			get
			{
				return this.info.Forwarding != 0;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x0005ED40 File Offset: 0x0005CF40
		public override int NumberOfInterfaces
		{
			get
			{
				return this.info.NumIf;
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x0005ED50 File Offset: 0x0005CF50
		public override int NumberOfIPAddresses
		{
			get
			{
				return this.info.NumAddr;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x0005ED60 File Offset: 0x0005CF60
		public override int NumberOfRoutes
		{
			get
			{
				return this.info.NumRoutes;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x0005ED70 File Offset: 0x0005CF70
		public override long OutputPacketRequests
		{
			get
			{
				return (long)((ulong)this.info.OutRequests);
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x0005ED80 File Offset: 0x0005CF80
		public override long OutputPacketRoutingDiscards
		{
			get
			{
				return (long)((ulong)this.info.RoutingDiscards);
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x0005ED90 File Offset: 0x0005CF90
		public override long OutputPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.info.OutDiscards);
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x0005EDA0 File Offset: 0x0005CFA0
		public override long OutputPacketsWithNoRoute
		{
			get
			{
				return (long)((ulong)this.info.OutNoRoutes);
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x0005EDB0 File Offset: 0x0005CFB0
		public override long PacketFragmentFailures
		{
			get
			{
				return (long)((ulong)this.info.FragFails);
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x0005EDC0 File Offset: 0x0005CFC0
		public override long PacketReassembliesRequired
		{
			get
			{
				return (long)((ulong)this.info.ReasmReqds);
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x0005EDD0 File Offset: 0x0005CFD0
		public override long PacketReassemblyFailures
		{
			get
			{
				return (long)((ulong)this.info.ReasmFails);
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x0005EDE0 File Offset: 0x0005CFE0
		public override long PacketReassemblyTimeout
		{
			get
			{
				return (long)((ulong)this.info.ReasmTimeout);
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x0005EDF0 File Offset: 0x0005CFF0
		public override long PacketsFragmented
		{
			get
			{
				return (long)((ulong)this.info.FragOks);
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x0005EE00 File Offset: 0x0005D000
		public override long PacketsReassembled
		{
			get
			{
				return (long)((ulong)this.info.ReasmOks);
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x0005EE10 File Offset: 0x0005D010
		public override long ReceivedPackets
		{
			get
			{
				return (long)((ulong)this.info.InReceives);
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x0005EE20 File Offset: 0x0005D020
		public override long ReceivedPacketsDelivered
		{
			get
			{
				return (long)((ulong)this.info.InDelivers);
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x0005EE30 File Offset: 0x0005D030
		public override long ReceivedPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.info.InDiscards);
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x0005EE40 File Offset: 0x0005D040
		public override long ReceivedPacketsForwarded
		{
			get
			{
				return (long)((ulong)this.info.ForwDatagrams);
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x0005EE50 File Offset: 0x0005D050
		public override long ReceivedPacketsWithAddressErrors
		{
			get
			{
				return (long)((ulong)this.info.InAddrErrors);
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x0005EE60 File Offset: 0x0005D060
		public override long ReceivedPacketsWithHeadersErrors
		{
			get
			{
				return (long)((ulong)this.info.InHdrErrors);
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x0005EE70 File Offset: 0x0005D070
		public override long ReceivedPacketsWithUnknownProtocol
		{
			get
			{
				return (long)((ulong)this.info.InUnknownProtos);
			}
		}

		// Token: 0x04001333 RID: 4915
		private Win32_MIB_IPSTATS info;
	}
}
