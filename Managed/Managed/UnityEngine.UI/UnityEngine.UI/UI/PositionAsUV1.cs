using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000091 RID: 145
	[AddComponentMenu("UI/Effects/Position As UV1", 16)]
	public class PositionAsUV1 : BaseVertexEffect
	{
		// Token: 0x060004D3 RID: 1235 RVA: 0x00013F90 File Offset: 0x00012190
		protected PositionAsUV1()
		{
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00013F98 File Offset: 0x00012198
		public override void ModifyVertices(List<UIVertex> verts)
		{
			if (!this.IsActive())
			{
				return;
			}
			for (int i = 0; i < verts.Count; i++)
			{
				UIVertex uivertex = verts[i];
				uivertex.uv1 = new Vector2(verts[i].position.x, verts[i].position.y);
				verts[i] = uivertex;
			}
		}
	}
}
