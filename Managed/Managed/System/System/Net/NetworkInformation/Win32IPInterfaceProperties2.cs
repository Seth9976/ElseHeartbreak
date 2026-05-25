using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000381 RID: 897
	internal class Win32IPInterfaceProperties2 : IPInterfaceProperties
	{
		// Token: 0x06001FFD RID: 8189 RVA: 0x0005F474 File Offset: 0x0005D674
		public Win32IPInterfaceProperties2(Win32_IP_ADAPTER_ADDRESSES addr, Win32_MIB_IFROW mib4, Win32_MIB_IFROW mib6)
		{
			this.addr = addr;
			this.mib4 = mib4;
			this.mib6 = mib6;
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x0005F494 File Offset: 0x0005D694
		public override IPv4InterfaceProperties GetIPv4Properties()
		{
			Win32_IP_ADAPTER_INFO adapterInfoByIndex = Win32NetworkInterface2.GetAdapterInfoByIndex(this.mib4.Index);
			return (adapterInfoByIndex == null) ? null : new Win32IPv4InterfaceProperties(adapterInfoByIndex, this.mib4);
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x0005F4D0 File Offset: 0x0005D6D0
		public override IPv6InterfaceProperties GetIPv6Properties()
		{
			Win32_IP_ADAPTER_INFO adapterInfoByIndex = Win32NetworkInterface2.GetAdapterInfoByIndex(this.mib6.Index);
			return (adapterInfoByIndex == null) ? null : new Win32IPv6InterfaceProperties(this.mib6);
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06002000 RID: 8192 RVA: 0x0005F508 File Offset: 0x0005D708
		public override IPAddressInformationCollection AnycastAddresses
		{
			get
			{
				return IPAddressInformationImplCollection.Win32FromAnycast(this.addr.FirstAnycastAddress);
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x0005F51C File Offset: 0x0005D71C
		public override IPAddressCollection DhcpServerAddresses
		{
			get
			{
				Win32_IP_ADAPTER_INFO adapterInfoByIndex = Win32NetworkInterface2.GetAdapterInfoByIndex(this.mib4.Index);
				return (adapterInfoByIndex == null) ? Win32IPAddressCollection.Empty : new Win32IPAddressCollection(new Win32_IP_ADDR_STRING[] { adapterInfoByIndex.DhcpServer });
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x0005F56C File Offset: 0x0005D76C
		public override IPAddressCollection DnsAddresses
		{
			get
			{
				return Win32IPAddressCollection.FromDnsServer(this.addr.FirstDnsServerAddress);
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x0005F580 File Offset: 0x0005D780
		public override string DnsSuffix
		{
			get
			{
				return this.addr.DnsSuffix;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002004 RID: 8196 RVA: 0x0005F590 File Offset: 0x0005D790
		public override GatewayIPAddressInformationCollection GatewayAddresses
		{
			get
			{
				Win32_IP_ADAPTER_INFO adapterInfoByIndex = Win32NetworkInterface2.GetAdapterInfoByIndex(this.mib4.Index);
				return (adapterInfoByIndex == null) ? Win32GatewayIPAddressInformationCollection.Empty : new Win32GatewayIPAddressInformationCollection(new Win32_IP_ADDR_STRING[] { adapterInfoByIndex.GatewayList });
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002005 RID: 8197 RVA: 0x0005F5E0 File Offset: 0x0005D7E0
		public override bool IsDnsEnabled
		{
			get
			{
				return Win32_FIXED_INFO.Instance.EnableDns != 0U;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002006 RID: 8198 RVA: 0x0005F5F4 File Offset: 0x0005D7F4
		public override bool IsDynamicDnsEnabled
		{
			get
			{
				return this.addr.DdnsEnabled;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002007 RID: 8199 RVA: 0x0005F604 File Offset: 0x0005D804
		public override MulticastIPAddressInformationCollection MulticastAddresses
		{
			get
			{
				return MulticastIPAddressInformationImplCollection.Win32FromMulticast(this.addr.FirstMulticastAddress);
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06002008 RID: 8200 RVA: 0x0005F618 File Offset: 0x0005D818
		public override UnicastIPAddressInformationCollection UnicastAddresses
		{
			get
			{
				Win32_IP_ADAPTER_INFO adapterInfoByIndex = Win32NetworkInterface2.GetAdapterInfoByIndex(this.mib4.Index);
				return (adapterInfoByIndex == null) ? UnicastIPAddressInformationImplCollection.Empty : UnicastIPAddressInformationImplCollection.Win32FromUnicast((int)adapterInfoByIndex.Index, this.addr.FirstUnicastAddress);
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x0005F660 File Offset: 0x0005D860
		public override IPAddressCollection WinsServersAddresses
		{
			get
			{
				Win32_IP_ADAPTER_INFO adapterInfoByIndex = Win32NetworkInterface2.GetAdapterInfoByIndex(this.mib4.Index);
				return (adapterInfoByIndex == null) ? Win32IPAddressCollection.Empty : new Win32IPAddressCollection(new Win32_IP_ADDR_STRING[] { adapterInfoByIndex.PrimaryWinsServer, adapterInfoByIndex.SecondaryWinsServer });
			}
		}

		// Token: 0x04001354 RID: 4948
		private readonly Win32_IP_ADAPTER_ADDRESSES addr;

		// Token: 0x04001355 RID: 4949
		private readonly Win32_MIB_IFROW mib4;

		// Token: 0x04001356 RID: 4950
		private readonly Win32_MIB_IFROW mib6;
	}
}
