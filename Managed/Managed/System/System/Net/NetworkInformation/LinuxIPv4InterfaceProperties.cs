using System;
using System.IO;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000385 RID: 901
	internal sealed class LinuxIPv4InterfaceProperties : UnixIPv4InterfaceProperties
	{
		// Token: 0x06002018 RID: 8216 RVA: 0x0005F6FC File Offset: 0x0005D8FC
		public LinuxIPv4InterfaceProperties(LinuxNetworkInterface iface)
			: base(iface)
		{
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x0005F708 File Offset: 0x0005D908
		public override bool IsForwardingEnabled
		{
			get
			{
				string text = "/proc/sys/net/ipv4/conf/" + this.iface.Name + "/forwarding";
				if (File.Exists(text))
				{
					string text2 = NetworkInterface.ReadLine(text);
					return text2 != "0";
				}
				return false;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x0005F750 File Offset: 0x0005D950
		public override int Mtu
		{
			get
			{
				string text = (this.iface as LinuxNetworkInterface).IfacePath + "mtu";
				int num = 0;
				if (File.Exists(text))
				{
					string text2 = NetworkInterface.ReadLine(text);
					try
					{
						num = int.Parse(text2);
					}
					catch
					{
					}
				}
				return num;
			}
		}
	}
}
