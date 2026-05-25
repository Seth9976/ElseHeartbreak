using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI.CoroutineTween;

namespace UnityEngine.UI
{
	// Token: 0x0200003D RID: 61
	[RequireComponent(typeof(CanvasRenderer))]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	[ExecuteInEditMode]
	public abstract class Graphic : UIBehaviour, ICanvasElement
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00005E80 File Offset: 0x00004080
		protected Graphic()
		{
			if (this.m_ColorTweenRunner == null)
			{
				this.m_ColorTweenRunner = new TweenRunner<ColorTween>();
			}
			this.m_ColorTweenRunner.Init(this);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00005F18 File Offset: 0x00004118
		public static Material defaultGraphicMaterial
		{
			get
			{
				if (Graphic.s_DefaultUI == null)
				{
					Graphic.s_DefaultUI = Canvas.GetDefaultCanvasMaterial();
				}
				return Graphic.s_DefaultUI;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00005F3C File Offset: 0x0000413C
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00005F44 File Offset: 0x00004144
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				if (SetPropertyUtility.SetColor(ref this.m_Color, value))
				{
					this.SetVerticesDirty();
				}
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005F60 File Offset: 0x00004160
		public virtual void SetAllDirty()
		{
			this.SetLayoutDirty();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005F74 File Offset: 0x00004174
		public virtual void SetLayoutDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			if (this.m_OnDirtyLayoutCallback != null)
			{
				this.m_OnDirtyLayoutCallback();
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005FA4 File Offset: 0x000041A4
		public virtual void SetVerticesDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_VertsDirty = true;
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			if (this.m_OnDirtyVertsCallback != null)
			{
				this.m_OnDirtyVertsCallback();
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005FD8 File Offset: 0x000041D8
		public virtual void SetMaterialDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_MaterialDirty = true;
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			if (this.m_OnDirtyMaterialCallback != null)
			{
				this.m_OnDirtyMaterialCallback();
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000600C File Offset: 0x0000420C
		protected override void OnRectTransformDimensionsChange()
		{
			if (base.gameObject.activeInHierarchy)
			{
				if (CanvasUpdateRegistry.IsRebuildingLayout())
				{
					this.SetVerticesDirty();
				}
				else
				{
					this.SetVerticesDirty();
					this.SetLayoutDirty();
				}
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000604C File Offset: 0x0000424C
		protected override void OnBeforeTransformParentChanged()
		{
			GraphicRegistry.UnregisterGraphicForCanvas(this.canvas, this);
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006068 File Offset: 0x00004268
		protected override void OnTransformParentChanged()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.CacheCanvas();
			GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
			this.SetAllDirty();
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000609C File Offset: 0x0000429C
		public int depth
		{
			get
			{
				return this.canvasRenderer.absoluteDepth;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000060AC File Offset: 0x000042AC
		public RectTransform rectTransform
		{
			get
			{
				RectTransform rectTransform;
				if ((rectTransform = this.m_RectTransform) == null)
				{
					rectTransform = (this.m_RectTransform = base.GetComponent<RectTransform>());
				}
				return rectTransform;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000060D8 File Offset: 0x000042D8
		public Canvas canvas
		{
			get
			{
				if (this.m_Canvas == null)
				{
					this.CacheCanvas();
				}
				return this.m_Canvas;
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000060F8 File Offset: 0x000042F8
		private void CacheCanvas()
		{
			List<Canvas> list = CanvasListPool.Get();
			base.gameObject.GetComponentsInParent<Canvas>(false, list);
			if (list.Count > 0)
			{
				this.m_Canvas = list[0];
			}
			CanvasListPool.Release(list);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006138 File Offset: 0x00004338
		public CanvasRenderer canvasRenderer
		{
			get
			{
				if (this.m_CanvasRender == null)
				{
					this.m_CanvasRender = base.GetComponent<CanvasRenderer>();
				}
				return this.m_CanvasRender;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006160 File Offset: 0x00004360
		public virtual Material defaultMaterial
		{
			get
			{
				return Graphic.defaultGraphicMaterial;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00006168 File Offset: 0x00004368
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00006198 File Offset: 0x00004398
		public virtual Material material
		{
			get
			{
				return (!(this.m_Material != null)) ? this.defaultMaterial : this.m_Material;
			}
			set
			{
				if (this.m_Material == value)
				{
					return;
				}
				this.m_Material = value;
				this.SetMaterialDirty();
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000061BC File Offset: 0x000043BC
		public virtual Material materialForRendering
		{
			get
			{
				List<Component> list = ComponentListPool.Get();
				base.GetComponents(typeof(IMaterialModifier), list);
				Material material = this.material;
				for (int i = 0; i < list.Count; i++)
				{
					material = (list[i] as IMaterialModifier).GetModifiedMaterial(material);
				}
				ComponentListPool.Release(list);
				return material;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00006218 File Offset: 0x00004418
		public virtual Texture mainTexture
		{
			get
			{
				return Graphic.s_WhiteTexture;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006220 File Offset: 0x00004420
		protected override void OnEnable()
		{
			base.OnEnable();
			this.CacheCanvas();
			GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
			if (Graphic.s_WhiteTexture == null)
			{
				Graphic.s_WhiteTexture = Texture2D.whiteTexture;
			}
			this.SetAllDirty();
			this.SendGraphicEnabledDisabled();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000626C File Offset: 0x0000446C
		protected override void OnDisable()
		{
			GraphicRegistry.UnregisterGraphicForCanvas(this.canvas, this);
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.canvasRenderer != null)
			{
				this.canvasRenderer.Clear();
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			this.SendGraphicEnabledDisabled();
			base.OnDisable();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000062C0 File Offset: 0x000044C0
		private void SendGraphicEnabledDisabled()
		{
			List<Component> list = ComponentListPool.Get();
			base.GetComponents(typeof(IGraphicEnabledDisabled), list);
			for (int i = 0; i < list.Count; i++)
			{
				((IGraphicEnabledDisabled)list[i]).OnSiblingGraphicEnabledDisabled();
			}
			ComponentListPool.Release(list);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006314 File Offset: 0x00004514
		protected override void OnCanvasHierarchyChanged()
		{
			if (!this.IsActive())
			{
				return;
			}
			Canvas canvas = this.m_Canvas;
			this.CacheCanvas();
			if (canvas != this.m_Canvas)
			{
				GraphicRegistry.UnregisterGraphicForCanvas(canvas, this);
				GraphicRegistry.RegisterGraphicForCanvas(this.canvas, this);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006360 File Offset: 0x00004560
		public virtual void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.PreRender)
			{
				if (this.m_VertsDirty)
				{
					this.UpdateGeometry();
					this.m_VertsDirty = false;
				}
				if (this.m_MaterialDirty)
				{
					this.UpdateMaterial();
					this.m_MaterialDirty = false;
				}
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000063B0 File Offset: 0x000045B0
		protected virtual void UpdateGeometry()
		{
			List<UIVertex> list = Graphic.s_VboPool.Get();
			if (this.rectTransform != null && this.rectTransform.rect.width >= 0f && this.rectTransform.rect.height >= 0f)
			{
				this.OnFillVBO(list);
			}
			List<Component> list2 = ComponentListPool.Get();
			base.GetComponents(typeof(IVertexModifier), list2);
			for (int i = 0; i < list2.Count; i++)
			{
				(list2[i] as IVertexModifier).ModifyVertices(list);
			}
			ComponentListPool.Release(list2);
			this.canvasRenderer.SetVertices(list);
			Graphic.s_VboPool.Release(list);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006478 File Offset: 0x00004678
		protected virtual void UpdateMaterial()
		{
			if (this.IsActive())
			{
				this.canvasRenderer.SetMaterial(this.materialForRendering, this.mainTexture);
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000064A8 File Offset: 0x000046A8
		protected virtual void OnFillVBO(List<UIVertex> vbo)
		{
			Rect pixelAdjustedRect = this.GetPixelAdjustedRect();
			Vector4 vector = new Vector4(pixelAdjustedRect.x, pixelAdjustedRect.y, pixelAdjustedRect.x + pixelAdjustedRect.width, pixelAdjustedRect.y + pixelAdjustedRect.height);
			UIVertex simpleVert = UIVertex.simpleVert;
			simpleVert.color = this.color;
			simpleVert.position = new Vector3(vector.x, vector.y);
			simpleVert.uv0 = new Vector2(0f, 0f);
			vbo.Add(simpleVert);
			simpleVert.position = new Vector3(vector.x, vector.w);
			simpleVert.uv0 = new Vector2(0f, 1f);
			vbo.Add(simpleVert);
			simpleVert.position = new Vector3(vector.z, vector.w);
			simpleVert.uv0 = new Vector2(1f, 1f);
			vbo.Add(simpleVert);
			simpleVert.position = new Vector3(vector.z, vector.y);
			simpleVert.uv0 = new Vector2(1f, 0f);
			vbo.Add(simpleVert);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000065E4 File Offset: 0x000047E4
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetAllDirty();
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000065EC File Offset: 0x000047EC
		public virtual void SetNativeSize()
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000065F0 File Offset: 0x000047F0
		public virtual bool Raycast(Vector2 sp, Camera eventCamera)
		{
			Transform transform = base.transform;
			List<Component> list = ComponentListPool.Get();
			bool flag = false;
			while (transform != null)
			{
				transform.GetComponents<Component>(list);
				for (int i = 0; i < list.Count; i++)
				{
					ICanvasRaycastFilter canvasRaycastFilter = list[i] as ICanvasRaycastFilter;
					if (canvasRaycastFilter != null)
					{
						bool flag2 = true;
						CanvasGroup canvasGroup = list[i] as CanvasGroup;
						if (canvasGroup != null)
						{
							if (!flag && canvasGroup.ignoreParentGroups)
							{
								flag = true;
								flag2 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
							}
							else if (!flag)
							{
								flag2 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
							}
						}
						else
						{
							flag2 = canvasRaycastFilter.IsRaycastLocationValid(sp, eventCamera);
						}
						if (!flag2)
						{
							ComponentListPool.Release(list);
							return false;
						}
					}
				}
				transform = transform.parent;
			}
			ComponentListPool.Release(list);
			return true;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000066D8 File Offset: 0x000048D8
		public Vector2 PixelAdjustPoint(Vector2 point)
		{
			if (!this.canvas || !this.canvas.pixelPerfect)
			{
				return point;
			}
			return RectTransformUtility.PixelAdjustPoint(point, base.transform, this.canvas);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000671C File Offset: 0x0000491C
		public Rect GetPixelAdjustedRect()
		{
			if (!this.canvas || !this.canvas.pixelPerfect)
			{
				return this.rectTransform.rect;
			}
			return RectTransformUtility.PixelAdjustRect(this.rectTransform, this.canvas);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006768 File Offset: 0x00004968
		public void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
		{
			this.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha, true);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006778 File Offset: 0x00004978
		private void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha, bool useRGB)
		{
			if (this.canvasRenderer == null || (!useRGB && !useAlpha))
			{
				return;
			}
			if (this.canvasRenderer.GetColor().Equals(targetColor))
			{
				return;
			}
			ColorTween.ColorTweenMode colorTweenMode = ((!useRGB || !useAlpha) ? ((!useRGB) ? ColorTween.ColorTweenMode.Alpha : ColorTween.ColorTweenMode.RGB) : ColorTween.ColorTweenMode.All);
			ColorTween colorTween = new ColorTween
			{
				duration = duration,
				startColor = this.canvasRenderer.GetColor(),
				targetColor = targetColor
			};
			colorTween.AddOnChangedCallback(new UnityAction<Color>(this.canvasRenderer.SetColor));
			colorTween.ignoreTimeScale = ignoreTimeScale;
			colorTween.tweenMode = colorTweenMode;
			this.m_ColorTweenRunner.StartTween(colorTween);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000684C File Offset: 0x00004A4C
		private static Color CreateColorFromAlpha(float alpha)
		{
			Color black = Color.black;
			black.a = alpha;
			return black;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00006868 File Offset: 0x00004A68
		public void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
		{
			this.CrossFadeColor(Graphic.CreateColorFromAlpha(alpha), duration, ignoreTimeScale, true, false);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000687C File Offset: 0x00004A7C
		public void RegisterDirtyLayoutCallback(UnityAction action)
		{
			this.m_OnDirtyLayoutCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyLayoutCallback, action);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00006898 File Offset: 0x00004A98
		public void UnregisterDirtyLayoutCallback(UnityAction action)
		{
			this.m_OnDirtyLayoutCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyLayoutCallback, action);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000068B4 File Offset: 0x00004AB4
		public void RegisterDirtyVerticesCallback(UnityAction action)
		{
			this.m_OnDirtyVertsCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyVertsCallback, action);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000068D0 File Offset: 0x00004AD0
		public void UnregisterDirtyVerticesCallback(UnityAction action)
		{
			this.m_OnDirtyVertsCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyVertsCallback, action);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000068EC File Offset: 0x00004AEC
		public void RegisterDirtyMaterialCallback(UnityAction action)
		{
			this.m_OnDirtyMaterialCallback = (UnityAction)Delegate.Combine(this.m_OnDirtyMaterialCallback, action);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006908 File Offset: 0x00004B08
		public void UnregisterDirtyMaterialCallback(UnityAction action)
		{
			this.m_OnDirtyMaterialCallback = (UnityAction)Delegate.Remove(this.m_OnDirtyMaterialCallback, action);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000694C File Offset: 0x00004B4C
		virtual bool UnityEngine.UI.ICanvasElement.IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006954 File Offset: 0x00004B54
		virtual Transform UnityEngine.UI.ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x040000C1 RID: 193
		protected static Material s_DefaultUI = null;

		// Token: 0x040000C2 RID: 194
		protected static Texture2D s_WhiteTexture = null;

		// Token: 0x040000C3 RID: 195
		private static readonly ObjectPool<List<UIVertex>> s_VboPool = new ObjectPool<List<UIVertex>>(delegate(List<UIVertex> x)
		{
			if (x.Capacity < 300)
			{
				x.Capacity = 300;
			}
		}, delegate(List<UIVertex> l)
		{
			l.Clear();
		});

		// Token: 0x040000C4 RID: 196
		[SerializeField]
		[FormerlySerializedAs("m_Mat")]
		protected Material m_Material;

		// Token: 0x040000C5 RID: 197
		[SerializeField]
		private Color m_Color = Color.white;

		// Token: 0x040000C6 RID: 198
		[NonSerialized]
		private RectTransform m_RectTransform;

		// Token: 0x040000C7 RID: 199
		[NonSerialized]
		private CanvasRenderer m_CanvasRender;

		// Token: 0x040000C8 RID: 200
		[NonSerialized]
		private Canvas m_Canvas;

		// Token: 0x040000C9 RID: 201
		[NonSerialized]
		private bool m_VertsDirty;

		// Token: 0x040000CA RID: 202
		[NonSerialized]
		private bool m_MaterialDirty;

		// Token: 0x040000CB RID: 203
		[NonSerialized]
		protected UnityAction m_OnDirtyLayoutCallback;

		// Token: 0x040000CC RID: 204
		[NonSerialized]
		protected UnityAction m_OnDirtyVertsCallback;

		// Token: 0x040000CD RID: 205
		[NonSerialized]
		protected UnityAction m_OnDirtyMaterialCallback;

		// Token: 0x040000CE RID: 206
		[NonSerialized]
		private readonly TweenRunner<ColorTween> m_ColorTweenRunner;
	}
}
