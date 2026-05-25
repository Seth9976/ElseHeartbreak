using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000126 RID: 294
	public struct NetworkViewID
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0001C67C File Offset: 0x0001A87C
		public static NetworkViewID unassigned
		{
			get
			{
				NetworkViewID networkViewID;
				NetworkViewID.INTERNAL_get_unassigned(out networkViewID);
				return networkViewID;
			}
		}

		// Token: 0x06000BE8 RID: 3048
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_unassigned(out NetworkViewID value);

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0001C694 File Offset: 0x0001A894
		internal static bool Internal_IsMine(NetworkViewID value)
		{
			return NetworkViewID.INTERNAL_CALL_Internal_IsMine(ref value);
		}

		// Token: 0x06000BEA RID: 3050
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_IsMine(ref NetworkViewID value);

		// Token: 0x06000BEB RID: 3051 RVA: 0x0001C6A0 File Offset: 0x0001A8A0
		internal static void Internal_GetOwner(NetworkViewID value, out NetworkPlayer player)
		{
			NetworkViewID.INTERNAL_CALL_Internal_GetOwner(ref value, out player);
		}

		// Token: 0x06000BEC RID: 3052
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_GetOwner(ref NetworkViewID value, out NetworkPlayer player);

		// Token: 0x06000BED RID: 3053 RVA: 0x0001C6AC File Offset: 0x0001A8AC
		internal static string Internal_GetString(NetworkViewID value)
		{
			return NetworkViewID.INTERNAL_CALL_Internal_GetString(ref value);
		}

		// Token: 0x06000BEE RID: 3054
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string INTERNAL_CALL_Internal_GetString(ref NetworkViewID value);

		// Token: 0x06000BEF RID: 3055 RVA: 0x0001C6B8 File Offset: 0x0001A8B8
		internal static bool Internal_Compare(NetworkViewID lhs, NetworkViewID rhs)
		{
			return NetworkViewID.INTERNAL_CALL_Internal_Compare(ref lhs, ref rhs);
		}

		// Token: 0x06000BF0 RID: 3056
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_Compare(ref NetworkViewID lhs, ref NetworkViewID rhs);

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0001C6C4 File Offset: 0x0001A8C4
		public override int GetHashCode()
		{
			return this.a ^ this.b ^ this.c;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0001C6DC File Offset: 0x0001A8DC
		public override bool Equals(object other)
		{
			if (!(other is NetworkViewID))
			{
				return false;
			}
			NetworkViewID networkViewID = (NetworkViewID)other;
			return NetworkViewID.Internal_Compare(this, networkViewID);
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0001C70C File Offset: 0x0001A90C
		public bool isMine
		{
			get
			{
				return NetworkViewID.Internal_IsMine(this);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0001C71C File Offset: 0x0001A91C
		public NetworkPlayer owner
		{
			get
			{
				NetworkPlayer networkPlayer;
				NetworkViewID.Internal_GetOwner(this, out networkPlayer);
				return networkPlayer;
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0001C738 File Offset: 0x0001A938
		public override string ToString()
		{
			return NetworkViewID.Internal_GetString(this);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0001C748 File Offset: 0x0001A948
		public static bool operator ==(NetworkViewID lhs, NetworkViewID rhs)
		{
			return NetworkViewID.Internal_Compare(lhs, rhs);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0001C754 File Offset: 0x0001A954
		public static bool operator !=(NetworkViewID lhs, NetworkViewID rhs)
		{
			return !NetworkViewID.Internal_Compare(lhs, rhs);
		}

		// Token: 0x04000550 RID: 1360
		private int a;

		// Token: 0x04000551 RID: 1361
		private int b;

		// Token: 0x04000552 RID: 1362
		private int c;
	}
}
