using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001F4 RID: 500
	public sealed class AnimationState : TrackedReference
	{
		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001831 RID: 6193
		// (set) Token: 0x06001832 RID: 6194
		public extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001833 RID: 6195
		// (set) Token: 0x06001834 RID: 6196
		public extern float weight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001835 RID: 6197
		// (set) Token: 0x06001836 RID: 6198
		public extern WrapMode wrapMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001837 RID: 6199
		// (set) Token: 0x06001838 RID: 6200
		public extern float time
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001839 RID: 6201
		// (set) Token: 0x0600183A RID: 6202
		public extern float normalizedTime
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600183B RID: 6203
		// (set) Token: 0x0600183C RID: 6204
		public extern float speed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x0600183D RID: 6205
		// (set) Token: 0x0600183E RID: 6206
		public extern float normalizedSpeed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x0600183F RID: 6207
		public extern float length
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001840 RID: 6208
		// (set) Token: 0x06001841 RID: 6209
		public extern int layer
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001842 RID: 6210
		public extern AnimationClip clip
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06001843 RID: 6211
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddMixingTransform(Transform mix, [DefaultValue("true")] bool recursive);

		// Token: 0x06001844 RID: 6212 RVA: 0x000241FC File Offset: 0x000223FC
		[ExcludeFromDocs]
		public void AddMixingTransform(Transform mix)
		{
			bool flag = true;
			this.AddMixingTransform(mix, flag);
		}

		// Token: 0x06001845 RID: 6213
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveMixingTransform(Transform mix);

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001846 RID: 6214
		// (set) Token: 0x06001847 RID: 6215
		public extern string name
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001848 RID: 6216
		// (set) Token: 0x06001849 RID: 6217
		public extern AnimationBlendMode blendMode
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
