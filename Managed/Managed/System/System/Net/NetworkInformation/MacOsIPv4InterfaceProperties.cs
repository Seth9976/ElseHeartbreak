using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000386 RID: 902
	internal sealed class MacOsIPv4InterfaceProperties : UnixIPv4InterfaceProperties
	{
		// Token: 0x0600201B RID: 8219 RVA: 0x0005F7BC File Offset: 0x0005D9BC
		public MacOsIPv4InterfaceProperties(MacOsNetworkInterface iface)
			: base(iface)
		{
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x0005F7C8 File Offset: 0x0005D9C8
		public override bool IsForwardingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x0005F7CC File Offset: 0x0005D9CC
		public override int Mtu
		{
			get
			{
				return 0;
			}
		}
	}
}
