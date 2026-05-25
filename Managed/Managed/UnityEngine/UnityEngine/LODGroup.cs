using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000E5 RID: 229
	public sealed class LODGroup : Component
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0000D048 File Offset: 0x0000B248
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x0000D060 File Offset: 0x0000B260
		public Vector3 localReferencePoint
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_localReferencePoint(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_localReferencePoint(ref value);
			}
		}

		// Token: 0x060006C6 RID: 1734
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localReferencePoint(out Vector3 value);

		// Token: 0x060006C7 RID: 1735
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localReferencePoint(ref Vector3 value);

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006C8 RID: 1736
		// (set) Token: 0x060006C9 RID: 1737
		public extern float size
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006CA RID: 1738
		public extern int lodCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006CB RID: 1739
		// (set) Token: 0x060006CC RID: 1740
		public extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060006CD RID: 1741
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RecalculateBounds();

		// Token: 0x060006CE RID: 1742
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLODS(LOD[] scriptingLODs);

		// Token: 0x060006CF RID: 1743
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ForceLOD(int index);
	}
}
