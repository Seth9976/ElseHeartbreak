using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000227 RID: 551
	public sealed class CanvasRenderer : Component
	{
		// Token: 0x06001AB4 RID: 6836 RVA: 0x000263E8 File Offset: 0x000245E8
		public void SetColor(Color color)
		{
			CanvasRenderer.INTERNAL_CALL_SetColor(this, ref color);
		}

		// Token: 0x06001AB5 RID: 6837
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetColor(CanvasRenderer self, ref Color color);

		// Token: 0x06001AB6 RID: 6838
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color GetColor();

		// Token: 0x06001AB7 RID: 6839
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetAlpha();

		// Token: 0x06001AB8 RID: 6840
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAlpha(float alpha);

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001AB9 RID: 6841
		// (set) Token: 0x06001ABA RID: 6842
		public extern bool isMask
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001ABB RID: 6843
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMaterial(Material material, Texture texture);

		// Token: 0x06001ABC RID: 6844
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Material GetMaterial();

		// Token: 0x06001ABD RID: 6845 RVA: 0x000263F4 File Offset: 0x000245F4
		public void SetVertices(List<UIVertex> vertices)
		{
			if (vertices.Count > 65535)
			{
				Debug.LogWarning(UnityString.Format("Number of vertices set exceeds {0}, rendering of this element will be skipped", new object[] { ushort.MaxValue }), this);
				vertices.Clear();
			}
			this.SetVerticesInternal(vertices);
		}

		// Token: 0x06001ABE RID: 6846
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVerticesInternal(object vertices);

		// Token: 0x06001ABF RID: 6847 RVA: 0x00026444 File Offset: 0x00024644
		public void SetVertices(UIVertex[] vertices, int size)
		{
			if (size > 65535)
			{
				Debug.LogWarning(UnityString.Format("Number of vertices set exceeds {0}, rendering of this element will be skipped", new object[] { ushort.MaxValue }), this);
				size = 0;
			}
			this.SetVerticesInternalArray(vertices, size);
		}

		// Token: 0x06001AC0 RID: 6848
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVerticesInternalArray(UIVertex[] vertices, int size);

		// Token: 0x06001AC1 RID: 6849
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001AC2 RID: 6850
		public extern int relativeDepth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001AC3 RID: 6851
		public extern int absoluteDepth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
