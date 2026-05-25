using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000092 RID: 146
	[AddComponentMenu("UI/Effects/Shadow", 14)]
	public class Shadow : BaseVertexEffect
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x0001400C File Offset: 0x0001220C
		protected Shadow()
		{
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0001405C File Offset: 0x0001225C
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00014064 File Offset: 0x00012264
		public Color effectColor
		{
			get
			{
				return this.m_EffectColor;
			}
			set
			{
				this.m_EffectColor = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0001408C File Offset: 0x0001228C
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00014094 File Offset: 0x00012294
		public Vector2 effectDistance
		{
			get
			{
				return this.m_EffectDistance;
			}
			set
			{
				if (value.x > 600f)
				{
					value.x = 600f;
				}
				if (value.x < -600f)
				{
					value.x = -600f;
				}
				if (value.y > 600f)
				{
					value.y = 600f;
				}
				if (value.y < -600f)
				{
					value.y = -600f;
				}
				if (this.m_EffectDistance == value)
				{
					return;
				}
				this.m_EffectDistance = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0001414C File Offset: 0x0001234C
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00014154 File Offset: 0x00012354
		public bool useGraphicAlpha
		{
			get
			{
				return this.m_UseGraphicAlpha;
			}
			set
			{
				this.m_UseGraphicAlpha = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001417C File Offset: 0x0001237C
		protected void ApplyShadow(List<UIVertex> verts, Color32 color, int start, int end, float x, float y)
		{
			int num = verts.Count * 2;
			if (verts.Capacity < num)
			{
				verts.Capacity = num;
			}
			for (int i = start; i < end; i++)
			{
				UIVertex uivertex = verts[i];
				verts.Add(uivertex);
				Vector3 position = uivertex.position;
				position.x += x;
				position.y += y;
				uivertex.position = position;
				Color32 color2 = color;
				if (this.m_UseGraphicAlpha)
				{
					color2.a = color2.a * verts[i].color.a / byte.MaxValue;
				}
				uivertex.color = color2;
				verts[i] = uivertex;
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00014244 File Offset: 0x00012444
		public override void ModifyVertices(List<UIVertex> verts)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.ApplyShadow(verts, this.effectColor, 0, verts.Count, this.effectDistance.x, this.effectDistance.y);
		}

		// Token: 0x04000250 RID: 592
		[SerializeField]
		private Color m_EffectColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04000251 RID: 593
		[SerializeField]
		private Vector2 m_EffectDistance = new Vector2(1f, -1f);

		// Token: 0x04000252 RID: 594
		[SerializeField]
		private bool m_UseGraphicAlpha = true;
	}
}
