using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200036C RID: 876
	internal class Win32IPAddressCollection : IPAddressCollection
	{
		// Token: 0x06001F23 RID: 7971 RVA: 0x0005D808 File Offset: 0x0005BA08
		private Win32IPAddressCollection()
		{
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0005D810 File Offset: 0x0005BA10
		public Win32IPAddressCollection(params IntPtr[] heads)
		{
			foreach (IntPtr intPtr in heads)
			{
				this.AddSubsequentlyString(intPtr);
			}
			this.is_readonly = true;
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0005D84C File Offset: 0x0005BA4C
		public Win32IPAddressCollection(params Win32_IP_ADDR_STRING[] al)
		{
			foreach (Win32_IP_ADDR_STRING win32_IP_ADDR_STRING in al)
			{
				if (!string.IsNullOrEmpty(win32_IP_ADDR_STRING.IpAddress))
				{
					this.Add(IPAddress.Parse(win32_IP_ADDR_STRING.IpAddress));
					this.AddSubsequentlyString(win32_IP_ADDR_STRING.Next);
				}
			}
			this.is_readonly = true;
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0005D8E4 File Offset: 0x0005BAE4
		public static Win32IPAddressCollection FromAnycast(IntPtr ptr)
		{
			Win32IPAddressCollection win32IPAddressCollection = new Win32IPAddressCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_ANYCAST_ADDRESS win32_IP_ADAPTER_ANYCAST_ADDRESS = (Win32_IP_ADAPTER_ANYCAST_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_ANYCAST_ADDRESS));
				win32IPAddressCollection.Add(win32_IP_ADAPTER_ANYCAST_ADDRESS.Address.GetIPAddress());
				intPtr = win32_IP_ADAPTER_ANYCAST_ADDRESS.Next;
			}
			win32IPAddressCollection.is_readonly = true;
			return win32IPAddressCollection;
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0005D948 File Offset: 0x0005BB48
		public static Win32IPAddressCollection FromDnsServer(IntPtr ptr)
		{
			Win32IPAddressCollection win32IPAddressCollection = new Win32IPAddressCollection();
			IntPtr intPtr = ptr;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADAPTER_DNS_SERVER_ADDRESS win32_IP_ADAPTER_DNS_SERVER_ADDRESS = (Win32_IP_ADAPTER_DNS_SERVER_ADDRESS)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADAPTER_DNS_SERVER_ADDRESS));
				win32IPAddressCollection.Add(win32_IP_ADAPTER_DNS_SERVER_ADDRESS.Address.GetIPAddress());
				intPtr = win32_IP_ADAPTER_DNS_SERVER_ADDRESS.Next;
			}
			win32IPAddressCollection.is_readonly = true;
			return win32IPAddressCollection;
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x0005D9AC File Offset: 0x0005BBAC
		private void AddSubsequentlyString(IntPtr head)
		{
			IntPtr intPtr = head;
			while (intPtr != IntPtr.Zero)
			{
				Win32_IP_ADDR_STRING win32_IP_ADDR_STRING = (Win32_IP_ADDR_STRING)Marshal.PtrToStructure(intPtr, typeof(Win32_IP_ADDR_STRING));
				this.Add(IPAddress.Parse(win32_IP_ADDR_STRING.IpAddress));
				intPtr = win32_IP_ADDR_STRING.Next;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x0005DA00 File Offset: 0x0005BC00
		public override bool IsReadOnly
		{
			get
			{
				return this.is_readonly;
			}
		}

		// Token: 0x0400130D RID: 4877
		public static readonly Win32IPAddressCollection Empty = new Win32IPAddressCollection(new IntPtr[] { IntPtr.Zero });

		// Token: 0x0400130E RID: 4878
		private bool is_readonly;
	}
}
