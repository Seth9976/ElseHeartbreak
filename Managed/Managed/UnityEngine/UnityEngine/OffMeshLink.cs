using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020001CC RID: 460
	public sealed class OffMeshLink : Component
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001655 RID: 5717
		// (set) Token: 0x06001656 RID: 5718
		public extern bool activated
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001657 RID: 5719
		public extern bool occupied
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001658 RID: 5720
		// (set) Token: 0x06001659 RID: 5721
		public extern float costOverride
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600165A RID: 5722
		// (set) Token: 0x0600165B RID: 5723
		public extern bool biDirectional
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600165C RID: 5724
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdatePositions();

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600165D RID: 5725
		// (set) Token: 0x0600165E RID: 5726
		public extern int navMeshLayer
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600165F RID: 5727
		// (set) Token: 0x06001660 RID: 5728
		public extern bool autoUpdatePositions
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001661 RID: 5729
		// (set) Token: 0x06001662 RID: 5730
		public extern Transform startTransform
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001663 RID: 5731
		// (set) Token: 0x06001664 RID: 5732
		public extern Transform endTransform
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
