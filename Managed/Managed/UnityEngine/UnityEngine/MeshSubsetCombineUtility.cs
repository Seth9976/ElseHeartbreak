using System;

namespace UnityEngine
{
	// Token: 0x02000072 RID: 114
	internal class MeshSubsetCombineUtility
	{
		// Token: 0x02000073 RID: 115
		public struct MeshInstance
		{
			// Token: 0x040001A0 RID: 416
			public int meshInstanceID;

			// Token: 0x040001A1 RID: 417
			public Matrix4x4 transform;

			// Token: 0x040001A2 RID: 418
			public Vector4 lightmapTilingOffset;
		}

		// Token: 0x02000074 RID: 116
		public struct SubMeshInstance
		{
			// Token: 0x040001A3 RID: 419
			public int meshInstanceID;

			// Token: 0x040001A4 RID: 420
			public int vertexOffset;

			// Token: 0x040001A5 RID: 421
			public int gameObjectInstanceID;

			// Token: 0x040001A6 RID: 422
			public int subMeshIndex;

			// Token: 0x040001A7 RID: 423
			public Matrix4x4 transform;
		}
	}
}
