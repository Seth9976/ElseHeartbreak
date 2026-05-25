using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000BC RID: 188
	public sealed class LineRenderer : Renderer
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x0000BE48 File Offset: 0x0000A048
		public void SetWidth(float start, float end)
		{
			LineRenderer.INTERNAL_CALL_SetWidth(this, start, end);
		}

		// Token: 0x060004DE RID: 1246
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetWidth(LineRenderer self, float start, float end);

		// Token: 0x060004DF RID: 1247 RVA: 0x0000BE54 File Offset: 0x0000A054
		public void SetColors(Color start, Color end)
		{
			LineRenderer.INTERNAL_CALL_SetColors(this, ref start, ref end);
		}

		// Token: 0x060004E0 RID: 1248
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetColors(LineRenderer self, ref Color start, ref Color end);

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000BE60 File Offset: 0x0000A060
		public void SetVertexCount(int count)
		{
			LineRenderer.INTERNAL_CALL_SetVertexCount(this, count);
		}

		// Token: 0x060004E2 RID: 1250
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetVertexCount(LineRenderer self, int count);

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000BE6C File Offset: 0x0000A06C
		public void SetPosition(int index, Vector3 position)
		{
			LineRenderer.INTERNAL_CALL_SetPosition(this, index, ref position);
		}

		// Token: 0x060004E4 RID: 1252
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetPosition(LineRenderer self, int index, ref Vector3 position);

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004E5 RID: 1253
		// (set) Token: 0x060004E6 RID: 1254
		public extern bool useWorldSpace
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
