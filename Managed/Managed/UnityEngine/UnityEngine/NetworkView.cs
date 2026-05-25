using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000128 RID: 296
	public sealed class NetworkView : Behaviour
	{
		// Token: 0x06000BFF RID: 3071
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RPC(NetworkView view, string name, RPCMode mode, object[] args);

		// Token: 0x06000C00 RID: 3072
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RPC_Target(NetworkView view, string name, NetworkPlayer target, object[] args);

		// Token: 0x06000C01 RID: 3073 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		public void RPC(string name, RPCMode mode, params object[] args)
		{
			NetworkView.Internal_RPC(this, name, mode, args);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0001C7B0 File Offset: 0x0001A9B0
		public void RPC(string name, NetworkPlayer target, params object[] args)
		{
			NetworkView.Internal_RPC_Target(this, name, target, args);
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000C03 RID: 3075
		// (set) Token: 0x06000C04 RID: 3076
		public extern Component observed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000C05 RID: 3077
		// (set) Token: 0x06000C06 RID: 3078
		public extern NetworkStateSynchronization stateSynchronization
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000C07 RID: 3079
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetViewID(out NetworkViewID viewID);

		// Token: 0x06000C08 RID: 3080 RVA: 0x0001C7BC File Offset: 0x0001A9BC
		private void Internal_SetViewID(NetworkViewID viewID)
		{
			NetworkView.INTERNAL_CALL_Internal_SetViewID(this, ref viewID);
		}

		// Token: 0x06000C09 RID: 3081
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_SetViewID(NetworkView self, ref NetworkViewID viewID);

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0001C7C8 File Offset: 0x0001A9C8
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x0001C7E0 File Offset: 0x0001A9E0
		public NetworkViewID viewID
		{
			get
			{
				NetworkViewID networkViewID;
				this.Internal_GetViewID(out networkViewID);
				return networkViewID;
			}
			set
			{
				this.Internal_SetViewID(value);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000C0C RID: 3084
		// (set) Token: 0x06000C0D RID: 3085
		public extern int group
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0001C7EC File Offset: 0x0001A9EC
		public bool isMine
		{
			get
			{
				return this.viewID.isMine;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0001C808 File Offset: 0x0001AA08
		public NetworkPlayer owner
		{
			get
			{
				return this.viewID.owner;
			}
		}

		// Token: 0x06000C10 RID: 3088
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetScope(NetworkPlayer player, bool relevancy);

		// Token: 0x06000C11 RID: 3089 RVA: 0x0001C824 File Offset: 0x0001AA24
		public static NetworkView Find(NetworkViewID viewID)
		{
			return NetworkView.INTERNAL_CALL_Find(ref viewID);
		}

		// Token: 0x06000C12 RID: 3090
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NetworkView INTERNAL_CALL_Find(ref NetworkViewID viewID);
	}
}
