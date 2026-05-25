using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000101 RID: 257
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class RectOffset
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x00015180 File Offset: 0x00013380
		public RectOffset()
		{
			this.Init();
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00015190 File Offset: 0x00013390
		internal RectOffset(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000151A8 File Offset: 0x000133A8
		public RectOffset(int left, int right, int top, int bottom)
		{
			this.Init();
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x000151E0 File Offset: 0x000133E0
		~RectOffset()
		{
			if (this.m_SourceStyle == null)
			{
				this.Cleanup();
			}
		}

		// Token: 0x06000916 RID: 2326
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init();

		// Token: 0x06000917 RID: 2327
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000918 RID: 2328
		// (set) Token: 0x06000919 RID: 2329
		public extern int left
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600091A RID: 2330
		// (set) Token: 0x0600091B RID: 2331
		public extern int right
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600091C RID: 2332
		// (set) Token: 0x0600091D RID: 2333
		public extern int top
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600091E RID: 2334
		// (set) Token: 0x0600091F RID: 2335
		public extern int bottom
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000920 RID: 2336
		public extern int horizontal
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000921 RID: 2337
		public extern int vertical
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00015228 File Offset: 0x00013428
		public Rect Add(Rect rect)
		{
			return RectOffset.INTERNAL_CALL_Add(this, ref rect);
		}

		// Token: 0x06000923 RID: 2339
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Rect INTERNAL_CALL_Add(RectOffset self, ref Rect rect);

		// Token: 0x06000924 RID: 2340 RVA: 0x00015234 File Offset: 0x00013434
		public Rect Remove(Rect rect)
		{
			return RectOffset.INTERNAL_CALL_Remove(this, ref rect);
		}

		// Token: 0x06000925 RID: 2341
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Rect INTERNAL_CALL_Remove(RectOffset self, ref Rect rect);

		// Token: 0x06000926 RID: 2342 RVA: 0x00015240 File Offset: 0x00013440
		public override string ToString()
		{
			return UnityString.Format("RectOffset (l:{0} r:{1} t:{2} b:{3})", new object[] { this.left, this.right, this.top, this.bottom });
		}

		// Token: 0x04000385 RID: 901
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000386 RID: 902
		private GUIStyle m_SourceStyle;
	}
}
