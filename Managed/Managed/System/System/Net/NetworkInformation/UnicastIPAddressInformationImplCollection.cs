using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020003C7 RID: 967
	internal class UnicastIPAddressInformationImplCollection : UnicastIPAddressInformationCollection
	{
		// Token: 0x06002178 RID: 8568 RVA: 0x000624E8 File Offset: 0x000606E8
		private UnicastIPAddressInformationImplCollection(bool isReadOnly)
		{
			this.is_readonly = isReadOnly;
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x00062508 File Offset: 0x00060708
		public override bool IsReadOnly
		{
			get
			{
				return this.is_readonly;
			}
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x00062510 File Offset: 0x00060710
		public static UnicastIPAddressInformationCollection Win32FromUnicast(int ifIndex, IntPtr ptr)
		{
			UnicastIPAddressInformationImplCollection unicastIPAddressInformationImplCollection = new UnicastIPAddressInformationImplCollection(false);
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_UNICAST_ADDRESS win32_IP_ADAPTER_UNICAST_ADDRESS = (Win32_IP_ADAPTER_UNICAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_UNICAST_ADDRESS));
				unicastIPAddressInformationImplCollection.Add(new Win32UnicastIPAddressInformation(ifIndex, win32_IP_ADAPTER_UNICAST_ADDRESS));
				intPtr = win32_IP_ADAPTER_UNICAST_ADDRESS.Next;
			}
			unicastIPAddressInformationImplCollection.is_readonly = true;
			return unicastIPAddressInformationImplCollection;
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x00062570 File Offset: 0x00060770
		public static UnicastIPAddressInformationCollection LinuxFromList(List<IPAddress> addresses)
		{
			UnicastIPAddressInformationImplCollection unicastIPAddressInformationImplCollection = new UnicastIPAddressInformationImplCollection(false);
			foreach (IPAddress ipaddress in addresses)
			{
				unicastIPAddressInformationImplCollection.Add(new LinuxUnicastIPAddressInformation(ipaddress));
			}
			unicastIPAddressInformationImplCollection.is_readonly = true;
			return unicastIPAddressInformationImplCollection;
		}

		// Token: 0x0400146B RID: 5227
		public static readonly UnicastIPAddressInformationImplCollection Empty = new UnicastIPAddressInformationImplCollection(true);

		// Token: 0x0400146C RID: 5228
		private bool is_readonly;
	}
}
