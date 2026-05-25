using System;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine.Sprites;

namespace UnityEngine.UI
{
	// Token: 0x02000042 RID: 66
	[AddComponentMenu("UI/Image", 10)]
	public class Image : MaskableGraphic, ICanvasRaycastFilter, ISerializationCallbackReceiver, ILayoutElement
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x000070B0 File Offset: 0x000052B0
		protected Image()
		{
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00007120 File Offset: 0x00005320
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00007128 File Offset: 0x00005328
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Sprite>(ref this.m_Sprite, value))
				{
					this.SetAllDirty();
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00007144 File Offset: 0x00005344
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00007174 File Offset: 0x00005374
		public Sprite overrideSprite
		{
			get
			{
				return (!(this.m_OverrideSprite == null)) ? this.m_OverrideSprite : this.sprite;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Sprite>(ref this.m_OverrideSprite, value))
				{
					this.SetAllDirty();
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00007190 File Offset: 0x00005390
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00007198 File Offset: 0x00005398
		public Image.Type type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Image.Type>(ref this.m_Type, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000071B4 File Offset: 0x000053B4
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000071BC File Offset: 0x000053BC
		public bool preserveAspect
		{
			get
			{
				return this.m_PreserveAspect;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_PreserveAspect, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000071D8 File Offset: 0x000053D8
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000071E0 File Offset: 0x000053E0
		public bool fillCenter
		{
			get
			{
				return this.m_FillCenter;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_FillCenter, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000071FC File Offset: 0x000053FC
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00007204 File Offset: 0x00005404
		public Image.FillMethod fillMethod
		{
			get
			{
				return this.m_FillMethod;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Image.FillMethod>(ref this.m_FillMethod, value))
				{
					this.SetVerticesDirty();
					this.m_FillOrigin = 0;
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00007224 File Offset: 0x00005424
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000722C File Offset: 0x0000542C
		public float fillAmount
		{
			get
			{
				return this.m_FillAmount;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FillAmount, Mathf.Clamp01(value)))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000724C File Offset: 0x0000544C
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00007254 File Offset: 0x00005454
		public bool fillClockwise
		{
			get
			{
				return this.m_FillClockwise;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_FillClockwise, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00007270 File Offset: 0x00005470
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00007278 File Offset: 0x00005478
		public int fillOrigin
		{
			get
			{
				return this.m_FillOrigin;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_FillOrigin, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00007294 File Offset: 0x00005494
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000729C File Offset: 0x0000549C
		public float eventAlphaThreshold
		{
			get
			{
				return this.m_EventAlphaThreshold;
			}
			set
			{
				this.m_EventAlphaThreshold = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000072A8 File Offset: 0x000054A8
		public override Texture mainTexture
		{
			get
			{
				return (!(this.overrideSprite == null)) ? this.overrideSprite.texture : Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001EF RID: 495 RVA: 0x000072DC File Offset: 0x000054DC
		public bool hasBorder
		{
			get
			{
				return this.overrideSprite != null && this.overrideSprite.border.sqrMagnitude > 0f;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00007318 File Offset: 0x00005518
		public float pixelsPerUnit
		{
			get
			{
				float num = 100f;
				if (this.sprite)
				{
					num = this.sprite.pixelsPerUnit;
				}
				float num2 = 100f;
				if (base.canvas)
				{
					num2 = base.canvas.referencePixelsPerUnit;
				}
				return num / num2;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000736C File Offset: 0x0000556C
		public virtual void OnBeforeSerialize()
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007370 File Offset: 0x00005570
		public virtual void OnAfterDeserialize()
		{
			if (this.m_FillOrigin < 0)
			{
				this.m_FillOrigin = 0;
			}
			else if (this.m_FillMethod == Image.FillMethod.Horizontal && this.m_FillOrigin > 1)
			{
				this.m_FillOrigin = 0;
			}
			else if (this.m_FillMethod == Image.FillMethod.Vertical && this.m_FillOrigin > 1)
			{
				this.m_FillOrigin = 0;
			}
			else if (this.m_FillOrigin > 3)
			{
				this.m_FillOrigin = 0;
			}
			this.m_FillAmount = Mathf.Clamp(this.m_FillAmount, 0f, 1f);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000740C File Offset: 0x0000560C
		private Vector4 GetDrawingDimensions(bool shouldPreserveAspect)
		{
			Vector4 vector = ((!(this.overrideSprite == null)) ? DataUtility.GetPadding(this.overrideSprite) : Vector4.zero);
			Vector2 vector2 = ((!(this.overrideSprite == null)) ? new Vector2(this.overrideSprite.rect.width, this.overrideSprite.rect.height) : Vector2.zero);
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			int num = Mathf.RoundToInt(vector2.x);
			int num2 = Mathf.RoundToInt(vector2.y);
			Vector4 vector3 = new Vector4(vector.x / (float)num, vector.y / (float)num2, ((float)num - vector.z) / (float)num, ((float)num2 - vector.w) / (float)num2);
			if (shouldPreserveAspect && vector2.sqrMagnitude > 0f)
			{
				float num3 = vector2.x / vector2.y;
				float num4 = pixelAdjustedRect.width / pixelAdjustedRect.height;
				if (num3 > num4)
				{
					float height = pixelAdjustedRect.height;
					pixelAdjustedRect.height = pixelAdjustedRect.width * (1f / num3);
					pixelAdjustedRect.y += (height - pixelAdjustedRect.height) * base.rectTransform.pivot.y;
				}
				else
				{
					float width = pixelAdjustedRect.width;
					pixelAdjustedRect.width = pixelAdjustedRect.height * num3;
					pixelAdjustedRect.x += (width - pixelAdjustedRect.width) * base.rectTransform.pivot.x;
				}
			}
			vector3 = new Vector4(pixelAdjustedRect.x + pixelAdjustedRect.width * vector3.x, pixelAdjustedRect.y + pixelAdjustedRect.height * vector3.y, pixelAdjustedRect.x + pixelAdjustedRect.width * vector3.z, pixelAdjustedRect.y + pixelAdjustedRect.height * vector3.w);
			return vector3;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007624 File Offset: 0x00005824
		public override void SetNativeSize()
		{
			if (this.overrideSprite != null)
			{
				float num = this.overrideSprite.rect.width / this.pixelsPerUnit;
				float num2 = this.overrideSprite.rect.height / this.pixelsPerUnit;
				base.rectTransform.anchorMax = base.rectTransform.anchorMin;
				base.rectTransform.sizeDelta = new Vector2(num, num2);
				this.SetAllDirty();
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000076A8 File Offset: 0x000058A8
		protected override void OnFillVBO(List<UIVertex> vbo)
		{
			if (this.overrideSprite == null)
			{
				base.OnFillVBO(vbo);
				return;
			}
			switch (this.type)
			{
			case Image.Type.Simple:
				this.GenerateSimpleSprite(vbo, this.m_PreserveAspect);
				break;
			case Image.Type.Sliced:
				this.GenerateSlicedSprite(vbo);
				break;
			case Image.Type.Tiled:
				this.GenerateTiledSprite(vbo);
				break;
			case Image.Type.Filled:
				this.GenerateFilledSprite(vbo, this.m_PreserveAspect);
				break;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000772C File Offset: 0x0000592C
		private void GenerateSimpleSprite(List<UIVertex> vbo, bool preserveAspect)
		{
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.color = base.color;
			Vector4 drawingDimensions = this.GetDrawingDimensions(preserveAspect);
			Vector4 vector = ((!(this.overrideSprite != null)) ? Vector4.zero : DataUtility.GetOuterUV(this.overrideSprite));
			simpleVert.position = new Vector3(drawingDimensions.x, drawingDimensions.y);
			simpleVert.uv0 = new Vector2(vector.x, vector.y);
			vbo.Add(simpleVert);
			simpleVert.position = new Vector3(drawingDimensions.x, drawingDimensions.w);
			simpleVert.uv0 = new Vector2(vector.x, vector.w);
			vbo.Add(simpleVert);
			simpleVert.position = new Vector3(drawingDimensions.z, drawingDimensions.w);
			simpleVert.uv0 = new Vector2(vector.z, vector.w);
			vbo.Add(simpleVert);
			simpleVert.position = new Vector3(drawingDimensions.z, drawingDimensions.y);
			simpleVert.uv0 = new Vector2(vector.z, vector.y);
			vbo.Add(simpleVert);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000786C File Offset: 0x00005A6C
		private void GenerateSlicedSprite(List<UIVertex> vbo)
		{
			if (!this.hasBorder)
			{
				this.GenerateSimpleSprite(vbo, false);
				return;
			}
			Vector4 vector;
			Vector4 vector2;
			Vector4 vector3;
			Vector4 vector4;
			if (this.overrideSprite != null)
			{
				vector = DataUtility.GetOuterUV(this.overrideSprite);
				vector2 = DataUtility.GetInnerUV(this.overrideSprite);
				vector3 = DataUtility.GetPadding(this.overrideSprite);
				vector4 = this.overrideSprite.border;
			}
			else
			{
				vector = Vector4.zero;
				vector2 = Vector4.zero;
				vector3 = Vector4.zero;
				vector4 = Vector4.zero;
			}
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			vector4 = this.GetAdjustedBorders(vector4 / this.pixelsPerUnit, pixelAdjustedRect);
			vector3 /= this.pixelsPerUnit;
			Image.s_VertScratch[0] = new Vector2(vector3.x, vector3.y);
			Image.s_VertScratch[3] = new Vector2(pixelAdjustedRect.width - vector3.z, pixelAdjustedRect.height - vector3.w);
			Image.s_VertScratch[1].x = vector4.x;
			Image.s_VertScratch[1].y = vector4.y;
			Image.s_VertScratch[2].x = pixelAdjustedRect.width - vector4.z;
			Image.s_VertScratch[2].y = pixelAdjustedRect.height - vector4.w;
			for (int i = 0; i < 4; i++)
			{
				Vector2[] array = Image.s_VertScratch;
				int num = i;
				array[num].x = array[num].x + pixelAdjustedRect.x;
				Vector2[] array2 = Image.s_VertScratch;
				int num2 = i;
				array2[num2].y = array2[num2].y + pixelAdjustedRect.y;
			}
			Image.s_UVScratch[0] = new Vector2(vector.x, vector.y);
			Image.s_UVScratch[1] = new Vector2(vector2.x, vector2.y);
			Image.s_UVScratch[2] = new Vector2(vector2.z, vector2.w);
			Image.s_UVScratch[3] = new Vector2(vector.z, vector.w);
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.color = base.color;
			for (int j = 0; j < 3; j++)
			{
				int num3 = j + 1;
				for (int k = 0; k < 3; k++)
				{
					if (this.m_FillCenter || j != 1 || k != 1)
					{
						int num4 = k + 1;
						this.AddQuad(vbo, simpleVert, new Vector2(Image.s_VertScratch[j].x, Image.s_VertScratch[k].y), new Vector2(Image.s_VertScratch[num3].x, Image.s_VertScratch[num4].y), new Vector2(Image.s_UVScratch[j].x, Image.s_UVScratch[k].y), new Vector2(Image.s_UVScratch[num3].x, Image.s_UVScratch[num4].y));
					}
				}
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00007BD0 File Offset: 0x00005DD0
		private void GenerateTiledSprite(List<UIVertex> vbo)
		{
			Vector4 vector;
			Vector4 vector2;
			Vector4 vector3;
			Vector2 vector4;
			if (this.overrideSprite != null)
			{
				vector = DataUtility.GetOuterUV(this.overrideSprite);
				vector2 = DataUtility.GetInnerUV(this.overrideSprite);
				vector3 = this.overrideSprite.border;
				vector4 = this.overrideSprite.rect.size;
			}
			else
			{
				vector = Vector4.zero;
				vector2 = Vector4.zero;
				vector3 = Vector4.zero;
				vector4 = Vector2.one * 100f;
			}
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			float num = (vector4.x - vector3.x - vector3.z) / this.pixelsPerUnit;
			float num2 = (vector4.y - vector3.y - vector3.w) / this.pixelsPerUnit;
			vector3 = this.GetAdjustedBorders(vector3 / this.pixelsPerUnit, pixelAdjustedRect);
			Vector2 vector5 = new Vector2(vector2.x, vector2.y);
			Vector2 vector6 = new Vector2(vector2.z, vector2.w);
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.color = base.color;
			float x = vector3.x;
			float num3 = pixelAdjustedRect.width - vector3.z;
			float y = vector3.y;
			float num4 = pixelAdjustedRect.height - vector3.w;
			if (num3 - x > num * 100f || num4 - y > num2 * 100f)
			{
				num = (num3 - x) / 100f;
				num2 = (num4 - y) / 100f;
			}
			Vector2 vector7 = vector6;
			if (this.m_FillCenter)
			{
				for (float num5 = y; num5 < num4; num5 += num2)
				{
					float num6 = num5 + num2;
					if (num6 > num4)
					{
						vector7.y = vector5.y + (vector6.y - vector5.y) * (num4 - num5) / (num6 - num5);
						num6 = num4;
					}
					vector7.x = vector6.x;
					for (float num7 = x; num7 < num3; num7 += num)
					{
						float num8 = num7 + num;
						if (num8 > num3)
						{
							vector7.x = vector5.x + (vector6.x - vector5.x) * (num3 - num7) / (num8 - num7);
							num8 = num3;
						}
						this.AddQuad(vbo, simpleVert, new Vector2(num7, num5) + pixelAdjustedRect.position, new Vector2(num8, num6) + pixelAdjustedRect.position, vector5, vector7);
					}
				}
			}
			if (!this.hasBorder)
			{
				return;
			}
			vector7 = vector6;
			for (float num9 = y; num9 < num4; num9 += num2)
			{
				float num10 = num9 + num2;
				if (num10 > num4)
				{
					vector7.y = vector5.y + (vector6.y - vector5.y) * (num4 - num9) / (num10 - num9);
					num10 = num4;
				}
				this.AddQuad(vbo, simpleVert, new Vector2(0f, num9) + pixelAdjustedRect.position, new Vector2(x, num10) + pixelAdjustedRect.position, new Vector2(vector.x, vector5.y), new Vector2(vector5.x, vector7.y));
				this.AddQuad(vbo, simpleVert, new Vector2(num3, num9) + pixelAdjustedRect.position, new Vector2(pixelAdjustedRect.width, num10) + pixelAdjustedRect.position, new Vector2(vector6.x, vector5.y), new Vector2(vector.z, vector7.y));
			}
			vector7 = vector6;
			for (float num11 = x; num11 < num3; num11 += num)
			{
				float num12 = num11 + num;
				if (num12 > num3)
				{
					vector7.x = vector5.x + (vector6.x - vector5.x) * (num3 - num11) / (num12 - num11);
					num12 = num3;
				}
				this.AddQuad(vbo, simpleVert, new Vector2(num11, 0f) + pixelAdjustedRect.position, new Vector2(num12, y) + pixelAdjustedRect.position, new Vector2(vector5.x, vector.y), new Vector2(vector7.x, vector5.y));
				this.AddQuad(vbo, simpleVert, new Vector2(num11, num4) + pixelAdjustedRect.position, new Vector2(num12, pixelAdjustedRect.height) + pixelAdjustedRect.position, new Vector2(vector5.x, vector6.y), new Vector2(vector7.x, vector.w));
			}
			this.AddQuad(vbo, simpleVert, new Vector2(0f, 0f) + pixelAdjustedRect.position, new Vector2(x, y) + pixelAdjustedRect.position, new Vector2(vector.x, vector.y), new Vector2(vector5.x, vector5.y));
			this.AddQuad(vbo, simpleVert, new Vector2(num3, 0f) + pixelAdjustedRect.position, new Vector2(pixelAdjustedRect.width, y) + pixelAdjustedRect.position, new Vector2(vector6.x, vector.y), new Vector2(vector.z, vector5.y));
			this.AddQuad(vbo, simpleVert, new Vector2(0f, num4) + pixelAdjustedRect.position, new Vector2(x, pixelAdjustedRect.height) + pixelAdjustedRect.position, new Vector2(vector.x, vector6.y), new Vector2(vector5.x, vector.w));
			this.AddQuad(vbo, simpleVert, new Vector2(num3, num4) + pixelAdjustedRect.position, new Vector2(pixelAdjustedRect.width, pixelAdjustedRect.height) + pixelAdjustedRect.position, new Vector2(vector6.x, vector6.y), new Vector2(vector.z, vector.w));
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000821C File Offset: 0x0000641C
		private void AddQuad(List<UIVertex> vbo, UIVertex v, Vector2 posMin, Vector2 posMax, Vector2 uvMin, Vector2 uvMax)
		{
			v.position = new Vector3(posMin.x, posMin.y, 0f);
			v.uv0 = new Vector2(uvMin.x, uvMin.y);
			vbo.Add(v);
			v.position = new Vector3(posMin.x, posMax.y, 0f);
			v.uv0 = new Vector2(uvMin.x, uvMax.y);
			vbo.Add(v);
			v.position = new Vector3(posMax.x, posMax.y, 0f);
			v.uv0 = new Vector2(uvMax.x, uvMax.y);
			vbo.Add(v);
			v.position = new Vector3(posMax.x, posMin.y, 0f);
			v.uv0 = new Vector2(uvMax.x, uvMin.y);
			vbo.Add(v);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000832C File Offset: 0x0000652C
		private Vector4 GetAdjustedBorders(Vector4 border, Rect rect)
		{
			for (int i = 0; i <= 1; i++)
			{
				float num = border[i] + border[i + 2];
				if (rect.size[i] < num && num != 0f)
				{
					float num2 = rect.size[i] / num;
					ref Vector4 ptr = ref border;
					int num4;
					int num3 = (num4 = i);
					float num5 = ptr[num4];
					border[num3] = num5 * num2;
					ref Vector4 ptr2 = ref border;
					int num6 = (num4 = i + 2);
					num5 = ptr2[num4];
					border[num6] = num5 * num2;
				}
			}
			return border;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000083D4 File Offset: 0x000065D4
		private void GenerateFilledSprite(List<UIVertex> vbo, bool preserveAspect)
		{
			if (this.m_FillAmount < 0.001f)
			{
				return;
			}
			Vector4 drawingDimensions = this.GetDrawingDimensions(preserveAspect);
			Vector4 vector = ((!(this.overrideSprite != null)) ? Vector4.zero : DataUtility.GetOuterUV(this.overrideSprite));
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.color = base.color;
			float num = vector.x;
			float num2 = vector.y;
			float num3 = vector.z;
			float num4 = vector.w;
			if (this.m_FillMethod == Image.FillMethod.Horizontal || this.m_FillMethod == Image.FillMethod.Vertical)
			{
				if (this.fillMethod == Image.FillMethod.Horizontal)
				{
					float num5 = (num3 - num) * this.m_FillAmount;
					if (this.m_FillOrigin == 1)
					{
						drawingDimensions.x = drawingDimensions.z - (drawingDimensions.z - drawingDimensions.x) * this.m_FillAmount;
						num = num3 - num5;
					}
					else
					{
						drawingDimensions.z = drawingDimensions.x + (drawingDimensions.z - drawingDimensions.x) * this.m_FillAmount;
						num3 = num + num5;
					}
				}
				else if (this.fillMethod == Image.FillMethod.Vertical)
				{
					float num6 = (num4 - num2) * this.m_FillAmount;
					if (this.m_FillOrigin == 1)
					{
						drawingDimensions.y = drawingDimensions.w - (drawingDimensions.w - drawingDimensions.y) * this.m_FillAmount;
						num2 = num4 - num6;
					}
					else
					{
						drawingDimensions.w = drawingDimensions.y + (drawingDimensions.w - drawingDimensions.y) * this.m_FillAmount;
						num4 = num2 + num6;
					}
				}
			}
			Image.s_Xy[0] = new Vector2(drawingDimensions.x, drawingDimensions.y);
			Image.s_Xy[1] = new Vector2(drawingDimensions.x, drawingDimensions.w);
			Image.s_Xy[2] = new Vector2(drawingDimensions.z, drawingDimensions.w);
			Image.s_Xy[3] = new Vector2(drawingDimensions.z, drawingDimensions.y);
			Image.s_Uv[0] = new Vector2(num, num2);
			Image.s_Uv[1] = new Vector2(num, num4);
			Image.s_Uv[2] = new Vector2(num3, num4);
			Image.s_Uv[3] = new Vector2(num3, num2);
			if (this.m_FillAmount < 1f)
			{
				if (this.fillMethod == Image.FillMethod.Radial90)
				{
					if (Image.RadialCut(Image.s_Xy, Image.s_Uv, this.m_FillAmount, this.m_FillClockwise, this.m_FillOrigin))
					{
						for (int i = 0; i < 4; i++)
						{
							simpleVert.position = Image.s_Xy[i];
							simpleVert.uv0 = Image.s_Uv[i];
							vbo.Add(simpleVert);
						}
					}
					return;
				}
				if (this.fillMethod == Image.FillMethod.Radial180)
				{
					for (int j = 0; j < 2; j++)
					{
						int num7 = ((this.m_FillOrigin <= 1) ? 0 : 1);
						float num8;
						float num9;
						float num10;
						float num11;
						if (this.m_FillOrigin == 0 || this.m_FillOrigin == 2)
						{
							num8 = 0f;
							num9 = 1f;
							if (j == num7)
							{
								num10 = 0f;
								num11 = 0.5f;
							}
							else
							{
								num10 = 0.5f;
								num11 = 1f;
							}
						}
						else
						{
							num10 = 0f;
							num11 = 1f;
							if (j == num7)
							{
								num8 = 0.5f;
								num9 = 1f;
							}
							else
							{
								num8 = 0f;
								num9 = 0.5f;
							}
						}
						Image.s_Xy[0].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num10);
						Image.s_Xy[1].x = Image.s_Xy[0].x;
						Image.s_Xy[2].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num11);
						Image.s_Xy[3].x = Image.s_Xy[2].x;
						Image.s_Xy[0].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num8);
						Image.s_Xy[1].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num9);
						Image.s_Xy[2].y = Image.s_Xy[1].y;
						Image.s_Xy[3].y = Image.s_Xy[0].y;
						Image.s_Uv[0].x = Mathf.Lerp(num, num3, num10);
						Image.s_Uv[1].x = Image.s_Uv[0].x;
						Image.s_Uv[2].x = Mathf.Lerp(num, num3, num11);
						Image.s_Uv[3].x = Image.s_Uv[2].x;
						Image.s_Uv[0].y = Mathf.Lerp(num2, num4, num8);
						Image.s_Uv[1].y = Mathf.Lerp(num2, num4, num9);
						Image.s_Uv[2].y = Image.s_Uv[1].y;
						Image.s_Uv[3].y = Image.s_Uv[0].y;
						float num12 = ((!this.m_FillClockwise) ? (this.m_FillAmount * 2f - (float)(1 - j)) : (this.fillAmount * 2f - (float)j));
						if (Image.RadialCut(Image.s_Xy, Image.s_Uv, Mathf.Clamp01(num12), this.m_FillClockwise, (j + this.m_FillOrigin + 3) % 4))
						{
							for (int k = 0; k < 4; k++)
							{
								simpleVert.position = Image.s_Xy[k];
								simpleVert.uv0 = Image.s_Uv[k];
								vbo.Add(simpleVert);
							}
						}
					}
					return;
				}
				if (this.fillMethod == Image.FillMethod.Radial360)
				{
					for (int l = 0; l < 4; l++)
					{
						float num13;
						float num14;
						if (l < 2)
						{
							num13 = 0f;
							num14 = 0.5f;
						}
						else
						{
							num13 = 0.5f;
							num14 = 1f;
						}
						float num15;
						float num16;
						if (l == 0 || l == 3)
						{
							num15 = 0f;
							num16 = 0.5f;
						}
						else
						{
							num15 = 0.5f;
							num16 = 1f;
						}
						Image.s_Xy[0].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num13);
						Image.s_Xy[1].x = Image.s_Xy[0].x;
						Image.s_Xy[2].x = Mathf.Lerp(drawingDimensions.x, drawingDimensions.z, num14);
						Image.s_Xy[3].x = Image.s_Xy[2].x;
						Image.s_Xy[0].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num15);
						Image.s_Xy[1].y = Mathf.Lerp(drawingDimensions.y, drawingDimensions.w, num16);
						Image.s_Xy[2].y = Image.s_Xy[1].y;
						Image.s_Xy[3].y = Image.s_Xy[0].y;
						Image.s_Uv[0].x = Mathf.Lerp(num, num3, num13);
						Image.s_Uv[1].x = Image.s_Uv[0].x;
						Image.s_Uv[2].x = Mathf.Lerp(num, num3, num14);
						Image.s_Uv[3].x = Image.s_Uv[2].x;
						Image.s_Uv[0].y = Mathf.Lerp(num2, num4, num15);
						Image.s_Uv[1].y = Mathf.Lerp(num2, num4, num16);
						Image.s_Uv[2].y = Image.s_Uv[1].y;
						Image.s_Uv[3].y = Image.s_Uv[0].y;
						float num17 = ((!this.m_FillClockwise) ? (this.m_FillAmount * 4f - (float)(3 - (l + this.m_FillOrigin) % 4)) : (this.m_FillAmount * 4f - (float)((l + this.m_FillOrigin) % 4)));
						if (Image.RadialCut(Image.s_Xy, Image.s_Uv, Mathf.Clamp01(num17), this.m_FillClockwise, (l + 2) % 4))
						{
							for (int m = 0; m < 4; m++)
							{
								simpleVert.position = Image.s_Xy[m];
								simpleVert.uv0 = Image.s_Uv[m];
								vbo.Add(simpleVert);
							}
						}
					}
					return;
				}
			}
			for (int n = 0; n < 4; n++)
			{
				simpleVert.position = Image.s_Xy[n];
				simpleVert.uv0 = Image.s_Uv[n];
				vbo.Add(simpleVert);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008E00 File Offset: 0x00007000
		private static bool RadialCut(Vector2[] xy, Vector2[] uv, float fill, bool invert, int corner)
		{
			if (fill < 0.001f)
			{
				return false;
			}
			if ((corner & 1) == 1)
			{
				invert = !invert;
			}
			if (!invert && fill > 0.999f)
			{
				return true;
			}
			float num = Mathf.Clamp01(fill);
			if (invert)
			{
				num = 1f - num;
			}
			num *= 1.5707964f;
			float num2 = Mathf.Cos(num);
			float num3 = Mathf.Sin(num);
			Image.RadialCut(xy, num2, num3, invert, corner);
			Image.RadialCut(uv, num2, num3, invert, corner);
			return true;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008E80 File Offset: 0x00007080
		private static void RadialCut(Vector2[] xy, float cos, float sin, bool invert, int corner)
		{
			int num = (corner + 1) % 4;
			int num2 = (corner + 2) % 4;
			int num3 = (corner + 3) % 4;
			if ((corner & 1) == 1)
			{
				if (sin > cos)
				{
					cos /= sin;
					sin = 1f;
					if (invert)
					{
						xy[num].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
						xy[num2].x = xy[num].x;
					}
				}
				else if (cos > sin)
				{
					sin /= cos;
					cos = 1f;
					if (!invert)
					{
						xy[num2].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
						xy[num3].y = xy[num2].y;
					}
				}
				else
				{
					cos = 1f;
					sin = 1f;
				}
				if (!invert)
				{
					xy[num3].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
				}
				else
				{
					xy[num].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
				}
			}
			else
			{
				if (cos > sin)
				{
					sin /= cos;
					cos = 1f;
					if (!invert)
					{
						xy[num].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
						xy[num2].y = xy[num].y;
					}
				}
				else if (sin > cos)
				{
					cos /= sin;
					sin = 1f;
					if (invert)
					{
						xy[num2].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
						xy[num3].x = xy[num2].x;
					}
				}
				else
				{
					cos = 1f;
					sin = 1f;
				}
				if (invert)
				{
					xy[num3].y = Mathf.Lerp(xy[corner].y, xy[num2].y, sin);
				}
				else
				{
					xy[num].x = Mathf.Lerp(xy[corner].x, xy[num2].x, cos);
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009110 File Offset: 0x00007310
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009114 File Offset: 0x00007314
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00009118 File Offset: 0x00007318
		public virtual float minWidth
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00009120 File Offset: 0x00007320
		public virtual float preferredWidth
		{
			get
			{
				if (this.overrideSprite == null)
				{
					return 0f;
				}
				if (this.type == Image.Type.Sliced || this.type == Image.Type.Tiled)
				{
					return DataUtility.GetMinSize(this.overrideSprite).x / this.pixelsPerUnit;
				}
				return this.overrideSprite.rect.size.x / this.pixelsPerUnit;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000919C File Offset: 0x0000739C
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000091A4 File Offset: 0x000073A4
		public virtual float minHeight
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000091AC File Offset: 0x000073AC
		public virtual float preferredHeight
		{
			get
			{
				if (this.overrideSprite == null)
				{
					return 0f;
				}
				if (this.type == Image.Type.Sliced || this.type == Image.Type.Tiled)
				{
					return DataUtility.GetMinSize(this.overrideSprite).y / this.pixelsPerUnit;
				}
				return this.overrideSprite.rect.size.y / this.pixelsPerUnit;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000205 RID: 517 RVA: 0x00009228 File Offset: 0x00007428
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00009230 File Offset: 0x00007430
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00009234 File Offset: 0x00007434
		public virtual bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
		{
			if (this.m_EventAlphaThreshold >= 1f)
			{
				return true;
			}
			Sprite overrideSprite = this.overrideSprite;
			if (overrideSprite == null)
			{
				return true;
			}
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.rectTransform, screenPoint, eventCamera, out vector);
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			vector.x += base.rectTransform.pivot.x * pixelAdjustedRect.width;
			vector.y += base.rectTransform.pivot.y * pixelAdjustedRect.height;
			vector = this.MapCoordinate(vector, pixelAdjustedRect);
			Rect textureRect = overrideSprite.textureRect;
			Vector2 vector2 = new Vector2(vector.x / textureRect.width, vector.y / textureRect.height);
			float num = Mathf.Lerp(textureRect.x, textureRect.xMax, vector2.x) / (float)overrideSprite.texture.width;
			float num2 = Mathf.Lerp(textureRect.y, textureRect.yMax, vector2.y) / (float)overrideSprite.texture.height;
			bool flag;
			try
			{
				flag = overrideSprite.texture.GetPixelBilinear(num, num2).a >= this.m_EventAlphaThreshold;
			}
			catch (UnityException ex)
			{
				Debug.LogError("Using clickAlphaThreshold lower than 1 on Image whose sprite texture cannot be read. " + ex.Message + " Also make sure to disable sprite packing for this sprite.", this);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000093D4 File Offset: 0x000075D4
		private Vector2 MapCoordinate(Vector2 local, Rect rect)
		{
			Rect rect2 = this.sprite.rect;
			if (this.type == Image.Type.Simple || this.type == Image.Type.Filled)
			{
				return new Vector2(local.x * rect2.width / rect.width, local.y * rect2.height / rect.height);
			}
			Vector4 border = this.sprite.border;
			Vector4 adjustedBorders = this.GetAdjustedBorders(border / this.pixelsPerUnit, rect);
			for (int i = 0; i < 2; i++)
			{
				if (local[i] > adjustedBorders[i])
				{
					if (rect.size[i] - local[i] <= adjustedBorders[i + 2])
					{
						ref Vector2 ptr = ref local;
						int num2;
						int num = (num2 = i);
						float num3 = ptr[num2];
						local[num] = num3 - (rect.size[i] - rect2.size[i]);
					}
					else if (this.type == Image.Type.Sliced)
					{
						float num4 = Mathf.InverseLerp(adjustedBorders[i], rect.size[i] - adjustedBorders[i + 2], local[i]);
						local[i] = Mathf.Lerp(border[i], rect2.size[i] - border[i + 2], num4);
					}
					else
					{
						ref Vector2 ptr2 = ref local;
						int num2;
						int num5 = (num2 = i);
						float num3 = ptr2[num2];
						local[num5] = num3 - adjustedBorders[i];
						local[i] = Mathf.Repeat(local[i], rect2.size[i] - border[i] - border[i + 2]);
						ref Vector2 ptr3 = ref local;
						int num6 = (num2 = i);
						num3 = ptr3[num2];
						local[num6] = num3 + border[i];
					}
				}
			}
			return local;
		}

		// Token: 0x040000E1 RID: 225
		[FormerlySerializedAs("m_Frame")]
		[SerializeField]
		private Sprite m_Sprite;

		// Token: 0x040000E2 RID: 226
		[NonSerialized]
		private Sprite m_OverrideSprite;

		// Token: 0x040000E3 RID: 227
		[SerializeField]
		private Image.Type m_Type;

		// Token: 0x040000E4 RID: 228
		[SerializeField]
		private bool m_PreserveAspect;

		// Token: 0x040000E5 RID: 229
		[SerializeField]
		private bool m_FillCenter = true;

		// Token: 0x040000E6 RID: 230
		[SerializeField]
		private Image.FillMethod m_FillMethod = Image.FillMethod.Radial360;

		// Token: 0x040000E7 RID: 231
		[SerializeField]
		[Range(0f, 1f)]
		private float m_FillAmount = 1f;

		// Token: 0x040000E8 RID: 232
		[SerializeField]
		private bool m_FillClockwise = true;

		// Token: 0x040000E9 RID: 233
		[SerializeField]
		private int m_FillOrigin;

		// Token: 0x040000EA RID: 234
		private float m_EventAlphaThreshold = 1f;

		// Token: 0x040000EB RID: 235
		private static readonly Vector2[] s_VertScratch = new Vector2[4];

		// Token: 0x040000EC RID: 236
		private static readonly Vector2[] s_UVScratch = new Vector2[4];

		// Token: 0x040000ED RID: 237
		private static readonly Vector2[] s_Xy = new Vector2[4];

		// Token: 0x040000EE RID: 238
		private static readonly Vector2[] s_Uv = new Vector2[4];

		// Token: 0x02000043 RID: 67
		public enum Type
		{
			// Token: 0x040000F0 RID: 240
			Simple,
			// Token: 0x040000F1 RID: 241
			Sliced,
			// Token: 0x040000F2 RID: 242
			Tiled,
			// Token: 0x040000F3 RID: 243
			Filled
		}

		// Token: 0x02000044 RID: 68
		public enum FillMethod
		{
			// Token: 0x040000F5 RID: 245
			Horizontal,
			// Token: 0x040000F6 RID: 246
			Vertical,
			// Token: 0x040000F7 RID: 247
			Radial90,
			// Token: 0x040000F8 RID: 248
			Radial180,
			// Token: 0x040000F9 RID: 249
			Radial360
		}

		// Token: 0x02000045 RID: 69
		public enum OriginHorizontal
		{
			// Token: 0x040000FB RID: 251
			Left,
			// Token: 0x040000FC RID: 252
			Right
		}

		// Token: 0x02000046 RID: 70
		public enum OriginVertical
		{
			// Token: 0x040000FE RID: 254
			Bottom,
			// Token: 0x040000FF RID: 255
			Top
		}

		// Token: 0x02000047 RID: 71
		public enum Origin90
		{
			// Token: 0x04000101 RID: 257
			BottomLeft,
			// Token: 0x04000102 RID: 258
			TopLeft,
			// Token: 0x04000103 RID: 259
			TopRight,
			// Token: 0x04000104 RID: 260
			BottomRight
		}

		// Token: 0x02000048 RID: 72
		public enum Origin180
		{
			// Token: 0x04000106 RID: 262
			Bottom,
			// Token: 0x04000107 RID: 263
			Left,
			// Token: 0x04000108 RID: 264
			Top,
			// Token: 0x04000109 RID: 265
			Right
		}

		// Token: 0x02000049 RID: 73
		public enum Origin360
		{
			// Token: 0x0400010B RID: 267
			Bottom,
			// Token: 0x0400010C RID: 268
			Right,
			// Token: 0x0400010D RID: 269
			Top,
			// Token: 0x0400010E RID: 270
			Left
		}
	}
}
