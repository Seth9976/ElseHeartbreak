using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000125 RID: 293
	public struct NetworkPlayer
	{
		// Token: 0x06000BD2 RID: 3026 RVA: 0x0001C558 File Offset: 0x0001A758
		public NetworkPlayer(string ip, int port)
		{
			Debug.LogError("Not yet implemented");
			this.index = 0;
		}

		// Token: 0x06000BD3 RID: 3027
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetIPAddress(int index);

		// Token: 0x06000BD4 RID: 3028
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetPort(int index);

		// Token: 0x06000BD5 RID: 3029
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetExternalIP();

		// Token: 0x06000BD6 RID: 3030
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetExternalPort();

		// Token: 0x06000BD7 RID: 3031
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetLocalIP();

		// Token: 0x06000BD8 RID: 3032
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetLocalPort();

		// Token: 0x06000BD9 RID: 3033
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetPlayerIndex();

		// Token: 0x06000BDA RID: 3034
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetGUID(int index);

		// Token: 0x06000BDB RID: 3035
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetLocalGUID();

		// Token: 0x06000BDC RID: 3036 RVA: 0x0001C56C File Offset: 0x0001A76C
		public override int GetHashCode()
		{
			return this.index.GetHashCode();
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0001C57C File Offset: 0x0001A77C
		public override bool Equals(object other)
		{
			return other is NetworkPlayer && ((NetworkPlayer)other).index == this.index;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0001C5AC File Offset: 0x0001A7AC
		public string ipAddress
		{
			get
			{
				if (this.index == NetworkPlayer.Internal_GetPlayerIndex())
				{
					return NetworkPlayer.Internal_GetLocalIP();
				}
				return NetworkPlayer.Internal_GetIPAddress(this.index);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
		public int port
		{
			get
			{
				if (this.index == NetworkPlayer.Internal_GetPlayerIndex())
				{
					return NetworkPlayer.Internal_GetLocalPort();
				}
				return NetworkPlayer.Internal_GetPort(this.index);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
		public string guid
		{
			get
			{
				if (this.index == NetworkPlayer.Internal_GetPlayerIndex())
				{
					return NetworkPlayer.Internal_GetLocalGUID();
				}
				return NetworkPlayer.Internal_GetGUID(this.index);
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0001C618 File Offset: 0x0001A818
		public override string ToString()
		{
			return this.index.ToString();
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0001C628 File Offset: 0x0001A828
		public string externalIP
		{
			get
			{
				return NetworkPlayer.Internal_GetExternalIP();
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0001C630 File Offset: 0x0001A830
		public int externalPort
		{
			get
			{
				return NetworkPlayer.Internal_GetExternalPort();
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0001C638 File Offset: 0x0001A838
		internal static NetworkPlayer unassigned
		{
			get
			{
				NetworkPlayer networkPlayer;
				networkPlayer.index = -1;
				return networkPlayer;
			}
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0001C650 File Offset: 0x0001A850
		public static bool operator ==(NetworkPlayer lhs, NetworkPlayer rhs)
		{
			return lhs.index == rhs.index;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0001C664 File Offset: 0x0001A864
		public static bool operator !=(NetworkPlayer lhs, NetworkPlayer rhs)
		{
			return lhs.index != rhs.index;
		}

		// Token: 0x0400054F RID: 1359
		internal int index;
	}
}
