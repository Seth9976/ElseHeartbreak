using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000151 RID: 337
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncOperation : YieldInstruction
	{
		// Token: 0x06000E1C RID: 3612
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalDestroy();

		// Token: 0x06000E1D RID: 3613 RVA: 0x0001E428 File Offset: 0x0001C628
		~AsyncOperation()
		{
			this.InternalDestroy();
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000E1E RID: 3614
		public extern bool isDone
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000E1F RID: 3615
		public extern float progress
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000E20 RID: 3616
		// (set) Token: 0x06000E21 RID: 3617
		public extern int priority
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000E22 RID: 3618
		// (set) Token: 0x06000E23 RID: 3619
		public extern bool allowSceneActivation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x040005CF RID: 1487
		[NotRenamed]
		internal IntPtr m_Ptr;
	}
}
