using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020001CE RID: 462
	[StructLayout(LayoutKind.Sequential)]
	public sealed class NavMeshPath
	{
		// Token: 0x06001665 RID: 5733
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern NavMeshPath();

		// Token: 0x06001666 RID: 5734
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void DestroyNavMeshPath();

		// Token: 0x06001667 RID: 5735 RVA: 0x0002366C File Offset: 0x0002186C
		~NavMeshPath()
		{
			this.DestroyNavMeshPath();
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06001668 RID: 5736
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Vector3[] CalculateCornersInternal();

		// Token: 0x06001669 RID: 5737
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ClearCornersInternal();

		// Token: 0x0600166A RID: 5738 RVA: 0x000236B4 File Offset: 0x000218B4
		public void ClearCorners()
		{
			this.ClearCornersInternal();
			this.m_corners = null;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x000236C4 File Offset: 0x000218C4
		private void CalculateCorners()
		{
			if (this.m_corners == null)
			{
				this.m_corners = this.CalculateCornersInternal();
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x000236E0 File Offset: 0x000218E0
		public Vector3[] corners
		{
			get
			{
				this.CalculateCorners();
				return this.m_corners;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600166D RID: 5741
		public extern NavMeshPathStatus status
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x040006DC RID: 1756
		internal IntPtr m_Ptr;

		// Token: 0x040006DD RID: 1757
		internal Vector3[] m_corners;
	}
}
