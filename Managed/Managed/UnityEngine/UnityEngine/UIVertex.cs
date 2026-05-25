using System;

namespace UnityEngine
{
	// Token: 0x02000226 RID: 550
	public struct UIVertex
	{
		// Token: 0x04000870 RID: 2160
		public Vector3 position;

		// Token: 0x04000871 RID: 2161
		public Vector3 normal;

		// Token: 0x04000872 RID: 2162
		public Color32 color;

		// Token: 0x04000873 RID: 2163
		public Vector2 uv0;

		// Token: 0x04000874 RID: 2164
		public Vector2 uv1;

		// Token: 0x04000875 RID: 2165
		public Vector4 tangent;

		// Token: 0x04000876 RID: 2166
		private static readonly Color32 s_DefaultColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

		// Token: 0x04000877 RID: 2167
		private static readonly Vector4 s_DefaultTangent = new Vector4(1f, 0f, 0f, -1f);

		// Token: 0x04000878 RID: 2168
		public static UIVertex simpleVert = new UIVertex
		{
			position = Vector3.zero,
			normal = Vector3.back,
			tangent = UIVertex.s_DefaultTangent,
			color = UIVertex.s_DefaultColor,
			uv0 = Vector2.zero,
			uv1 = Vector2.zero
		};
	}
}
