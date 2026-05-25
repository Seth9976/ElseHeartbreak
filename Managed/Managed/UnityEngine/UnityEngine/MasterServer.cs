using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200012D RID: 301
	public sealed class MasterServer
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000C9B RID: 3227
		// (set) Token: 0x06000C9C RID: 3228
		public static extern string ipAddress
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000C9D RID: 3229
		// (set) Token: 0x06000C9E RID: 3230
		public static extern int port
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000C9F RID: 3231
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RequestHostList(string gameTypeName);

		// Token: 0x06000CA0 RID: 3232
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern HostData[] PollHostList();

		// Token: 0x06000CA1 RID: 3233
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RegisterHost(string gameTypeName, string gameName, [DefaultValue("\"\"")] string comment);

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0001CD04 File Offset: 0x0001AF04
		[ExcludeFromDocs]
		public static void RegisterHost(string gameTypeName, string gameName)
		{
			string empty = string.Empty;
			MasterServer.RegisterHost(gameTypeName, gameName, empty);
		}

		// Token: 0x06000CA3 RID: 3235
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnregisterHost();

		// Token: 0x06000CA4 RID: 3236
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ClearHostList();

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000CA5 RID: 3237
		// (set) Token: 0x06000CA6 RID: 3238
		public static extern int updateRate
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000CA7 RID: 3239
		// (set) Token: 0x06000CA8 RID: 3240
		public static extern bool dedicatedServer
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
