using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000058 RID: 88
	[AddComponentMenu("UI/Raw Image", 12)]
	public class RawImage : MaskableGraphic
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0000CA90 File Offset: 0x0000AC90
		protected RawImage()
		{
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		public override Texture mainTexture
		{
			get
			{
				return (!(this.m_Texture == null)) ? this.m_Texture : Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000CADC File Offset: 0x0000ACDC
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		public Texture texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				if (this.m_Texture == value)
				{
					return;
				}
				this.m_Texture = value;
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000CB14 File Offset: 0x0000AD14
		public Rect uvRect
		{
			get
			{
				return this.m_UVRect;
			}
			set
			{
				if (this.m_UVRect == value)
				{
					return;
				}
				this.m_UVRect = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000CB38 File Offset: 0x0000AD38
		public override void SetNativeSize()
		{
			Texture mainTexture = this.mainTexture;
			if (mainTexture != null)
			{
				int num = Mathf.RoundToInt((float)mainTexture.width * this.uvRect.width);
				int num2 = Mathf.RoundToInt((float)mainTexture.height * this.uvRect.height);
				base.rectTransform.anchorMax = base.rectTransform.anchorMin;
				base.rectTransform.sizeDelta = new Vector2((float)num, (float)num2);
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		protected override void OnFillVBO(List<UIVertex> vbo)
		{
			Texture mainTexture = this.mainTexture;
			if (mainTexture != null)
			{
				Vector4 zero = Vector4.zero;
				int num = Mathf.RoundToInt((float)mainTexture.width * this.uvRect.width);
				int num2 = Mathf.RoundToInt((float)mainTexture.height * this.uvRect.height);
				float num3 = (float)(((num & 1) != 0) ? (num + 1) : num);
				float num4 = (float)(((num2 & 1) != 0) ? (num2 + 1) : num2);
				zero.x = 0f;
				zero.y = 0f;
				zero.z = (float)num / num3;
				zero.w = (float)num2 / num4;
				zero.x -= base.rectTransform.pivot.x;
				zero.y -= base.rectTransform.pivot.y;
				zero.z -= base.rectTransform.pivot.x;
				zero.w -= base.rectTransform.pivot.y;
				zero.x *= base.rectTransform.rect.width;
				zero.y *= base.rectTransform.rect.height;
				zero.z *= base.rectTransform.rect.width;
				zero.w *= base.rectTransform.rect.height;
				vbo.Clear();
				UIVertex simpleVert = UIVertex.simpleVert;
				simpleVert.position = new Vector2(zero.x, zero.y);
				simpleVert.uv0 = new Vector2(this.m_UVRect.xMin, this.m_UVRect.yMin);
				simpleVert.color = base.color;
				vbo.Add(simpleVert);
				simpleVert.position = new Vector2(zero.x, zero.w);
				simpleVert.uv0 = new Vector2(this.m_UVRect.xMin, this.m_UVRect.yMax);
				simpleVert.color = base.color;
				vbo.Add(simpleVert);
				simpleVert.position = new Vector2(zero.z, zero.w);
				simpleVert.uv0 = new Vector2(this.m_UVRect.xMax, this.m_UVRect.yMax);
				simpleVert.color = base.color;
				vbo.Add(simpleVert);
				simpleVert.position = new Vector2(zero.z, zero.y);
				simpleVert.uv0 = new Vector2(this.m_UVRect.xMax, this.m_UVRect.yMin);
				simpleVert.color = base.color;
				vbo.Add(simpleVert);
			}
		}

		// Token: 0x04000167 RID: 359
		[FormerlySerializedAs("m_Tex")]
		[SerializeField]
		private Texture m_Texture;

		// Token: 0x04000168 RID: 360
		[SerializeField]
		private Rect m_UVRect = new Rect(0f, 0f, 1f, 1f);
	}
}
