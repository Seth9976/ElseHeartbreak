using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000090 RID: 144
	[AddComponentMenu("UI/Effects/Outline", 15)]
	public class Outline : Shadow
	{
		// Token: 0x060004D1 RID: 1233 RVA: 0x00013E6C File Offset: 0x0001206C
		protected Outline()
		{
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00013E74 File Offset: 0x00012074
		public override void ModifyVertices(List<UIVertex> verts)
		{
			if (!this.IsActive())
			{
				return;
			}
			int num = 0;
			int num2 = verts.Count;
			base.ApplyShadow(verts, base.effectColor, num, verts.Count, base.effectDistance.x, base.effectDistance.y);
			num = num2;
			num2 = verts.Count;
			base.ApplyShadow(verts, base.effectColor, num, verts.Count, base.effectDistance.x, -base.effectDistance.y);
			num = num2;
			num2 = verts.Count;
			base.ApplyShadow(verts, base.effectColor, num, verts.Count, -base.effectDistance.x, base.effectDistance.y);
			num = num2;
			num2 = verts.Count;
			base.ApplyShadow(verts, base.effectColor, num, verts.Count, -base.effectDistance.x, -base.effectDistance.y);
		}
	}
}
