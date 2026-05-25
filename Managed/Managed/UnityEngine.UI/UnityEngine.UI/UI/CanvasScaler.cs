using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000072 RID: 114
	[AddComponentMenu("Layout/Canvas Scaler", 101)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(Canvas))]
	public class CanvasScaler : UIBehaviour
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x00011724 File Offset: 0x0000F924
		protected CanvasScaler()
		{
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x000117A0 File Offset: 0x0000F9A0
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x000117A8 File Offset: 0x0000F9A8
		public CanvasScaler.ScaleMode uiScaleMode
		{
			get
			{
				return this.m_UiScaleMode;
			}
			set
			{
				this.m_UiScaleMode = value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x000117B4 File Offset: 0x0000F9B4
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x000117BC File Offset: 0x0000F9BC
		public float referencePixelsPerUnit
		{
			get
			{
				return this.m_ReferencePixelsPerUnit;
			}
			set
			{
				this.m_ReferencePixelsPerUnit = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x000117C8 File Offset: 0x0000F9C8
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x000117D0 File Offset: 0x0000F9D0
		public float scaleFactor
		{
			get
			{
				return this.m_ScaleFactor;
			}
			set
			{
				this.m_ScaleFactor = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x000117DC File Offset: 0x0000F9DC
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x000117E4 File Offset: 0x0000F9E4
		public Vector2 referenceResolution
		{
			get
			{
				return this.m_ReferenceResolution;
			}
			set
			{
				this.m_ReferenceResolution = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000117F0 File Offset: 0x0000F9F0
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x000117F8 File Offset: 0x0000F9F8
		public CanvasScaler.ScreenMatchMode screenMatchMode
		{
			get
			{
				return this.m_ScreenMatchMode;
			}
			set
			{
				this.m_ScreenMatchMode = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00011804 File Offset: 0x0000FA04
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0001180C File Offset: 0x0000FA0C
		public float matchWidthOrHeight
		{
			get
			{
				return this.m_MatchWidthOrHeight;
			}
			set
			{
				this.m_MatchWidthOrHeight = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00011818 File Offset: 0x0000FA18
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00011820 File Offset: 0x0000FA20
		public CanvasScaler.Unit physicalUnit
		{
			get
			{
				return this.m_PhysicalUnit;
			}
			set
			{
				this.m_PhysicalUnit = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001182C File Offset: 0x0000FA2C
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00011834 File Offset: 0x0000FA34
		public float fallbackScreenDPI
		{
			get
			{
				return this.m_FallbackScreenDPI;
			}
			set
			{
				this.m_FallbackScreenDPI = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00011840 File Offset: 0x0000FA40
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00011848 File Offset: 0x0000FA48
		public float defaultSpriteDPI
		{
			get
			{
				return this.m_DefaultSpriteDPI;
			}
			set
			{
				this.m_DefaultSpriteDPI = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00011854 File Offset: 0x0000FA54
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0001185C File Offset: 0x0000FA5C
		public float dynamicPixelsPerUnit
		{
			get
			{
				return this.m_DynamicPixelsPerUnit;
			}
			set
			{
				this.m_DynamicPixelsPerUnit = value;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00011868 File Offset: 0x0000FA68
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_Canvas = base.GetComponent<Canvas>();
			this.Handle();
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00011884 File Offset: 0x0000FA84
		protected override void OnDisable()
		{
			this.SetScaleFactor(1f);
			this.SetReferencePixelsPerUnit(100f);
			base.OnDisable();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000118A4 File Offset: 0x0000FAA4
		protected virtual void Update()
		{
			this.Handle();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000118AC File Offset: 0x0000FAAC
		protected virtual void Handle()
		{
			if (this.m_Canvas == null || !this.m_Canvas.isRootCanvas)
			{
				return;
			}
			if (this.m_Canvas.renderMode == RenderMode.WorldSpace)
			{
				this.HandleWorldCanvas();
				return;
			}
			switch (this.m_UiScaleMode)
			{
			case CanvasScaler.ScaleMode.ConstantPixelSize:
				this.HandleConstantPixelSize();
				break;
			case CanvasScaler.ScaleMode.ScaleWithScreenSize:
				this.HandleScaleWithScreenSize();
				break;
			case CanvasScaler.ScaleMode.ConstantPhysicalSize:
				this.HandleConstantPhysicalSize();
				break;
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00011934 File Offset: 0x0000FB34
		protected virtual void HandleWorldCanvas()
		{
			this.SetScaleFactor(this.m_DynamicPixelsPerUnit);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00011950 File Offset: 0x0000FB50
		protected virtual void HandleConstantPixelSize()
		{
			this.SetScaleFactor(this.m_ScaleFactor);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001196C File Offset: 0x0000FB6C
		protected virtual void HandleScaleWithScreenSize()
		{
			Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
			float num = 0f;
			switch (this.m_ScreenMatchMode)
			{
			case CanvasScaler.ScreenMatchMode.MatchWidthOrHeight:
			{
				float num2 = Mathf.Log(vector.x / this.m_ReferenceResolution.x, 2f);
				float num3 = Mathf.Log(vector.y / this.m_ReferenceResolution.y, 2f);
				float num4 = Mathf.Lerp(num2, num3, this.m_MatchWidthOrHeight);
				num = Mathf.Pow(2f, num4);
				break;
			}
			case CanvasScaler.ScreenMatchMode.Expand:
				num = Mathf.Min(vector.x / this.m_ReferenceResolution.x, vector.y / this.m_ReferenceResolution.y);
				break;
			case CanvasScaler.ScreenMatchMode.Shrink:
				num = Mathf.Max(vector.x / this.m_ReferenceResolution.x, vector.y / this.m_ReferenceResolution.y);
				break;
			}
			this.SetScaleFactor(num);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00011A84 File Offset: 0x0000FC84
		protected virtual void HandleConstantPhysicalSize()
		{
			float dpi = Screen.dpi;
			float num = ((dpi != 0f) ? dpi : this.m_FallbackScreenDPI);
			float num2 = 1f;
			switch (this.m_PhysicalUnit)
			{
			case CanvasScaler.Unit.Centimeters:
				num2 = 2.54f;
				break;
			case CanvasScaler.Unit.Millimeters:
				num2 = 25.4f;
				break;
			case CanvasScaler.Unit.Inches:
				num2 = 1f;
				break;
			case CanvasScaler.Unit.Points:
				num2 = 72f;
				break;
			case CanvasScaler.Unit.Picas:
				num2 = 6f;
				break;
			}
			this.SetScaleFactor(num / num2);
			this.SetReferencePixelsPerUnit(this.m_ReferencePixelsPerUnit * num2 / this.m_DefaultSpriteDPI);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00011B30 File Offset: 0x0000FD30
		protected void SetScaleFactor(float scaleFactor)
		{
			if (scaleFactor == this.m_PrevScaleFactor)
			{
				return;
			}
			this.m_Canvas.scaleFactor = scaleFactor;
			this.m_PrevScaleFactor = scaleFactor;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00011B60 File Offset: 0x0000FD60
		protected void SetReferencePixelsPerUnit(float referencePixelsPerUnit)
		{
			if (referencePixelsPerUnit == this.m_PrevReferencePixelsPerUnit)
			{
				return;
			}
			this.m_Canvas.referencePixelsPerUnit = referencePixelsPerUnit;
			this.m_PrevReferencePixelsPerUnit = referencePixelsPerUnit;
		}

		// Token: 0x040001EA RID: 490
		private const float kLogBase = 2f;

		// Token: 0x040001EB RID: 491
		[Tooltip("Determines how UI elements in the Canvas are scaled.")]
		[SerializeField]
		private CanvasScaler.ScaleMode m_UiScaleMode;

		// Token: 0x040001EC RID: 492
		[Tooltip("If a sprite has this 'Pixels Per Unit' setting, then one pixel in the sprite will cover one unit in the UI.")]
		[SerializeField]
		protected float m_ReferencePixelsPerUnit = 100f;

		// Token: 0x040001ED RID: 493
		[SerializeField]
		[Tooltip("Scales all UI elements in the Canvas by this factor.")]
		protected float m_ScaleFactor = 1f;

		// Token: 0x040001EE RID: 494
		[SerializeField]
		[Tooltip("The resolution the UI layout is designed for. If the screen resolution is larger, the UI will be scaled up, and if it's smaller, the UI will be scaled down. This is done in accordance with the Screen Match Mode.")]
		protected Vector2 m_ReferenceResolution = new Vector2(800f, 600f);

		// Token: 0x040001EF RID: 495
		[Tooltip("A mode used to scale the canvas area if the aspect ratio of the current resolution doesn't fit the reference resolution.")]
		[SerializeField]
		protected CanvasScaler.ScreenMatchMode m_ScreenMatchMode;

		// Token: 0x040001F0 RID: 496
		[Range(0f, 1f)]
		[Tooltip("Determines if the scaling is using the width or height as reference, or a mix in between.")]
		[SerializeField]
		protected float m_MatchWidthOrHeight;

		// Token: 0x040001F1 RID: 497
		[SerializeField]
		[Tooltip("The physical unit to specify positions and sizes in.")]
		protected CanvasScaler.Unit m_PhysicalUnit = CanvasScaler.Unit.Points;

		// Token: 0x040001F2 RID: 498
		[Tooltip("The DPI to assume if the screen DPI is not known.")]
		[SerializeField]
		protected float m_FallbackScreenDPI = 96f;

		// Token: 0x040001F3 RID: 499
		[Tooltip("The pixels per inch to use for sprites that have a 'Pixels Per Unit' setting that matches the 'Reference Pixels Per Unit' setting.")]
		[SerializeField]
		protected float m_DefaultSpriteDPI = 96f;

		// Token: 0x040001F4 RID: 500
		[Tooltip("The amount of pixels per unit to use for dynamically created bitmaps in the UI, such as Text.")]
		[SerializeField]
		protected float m_DynamicPixelsPerUnit = 1f;

		// Token: 0x040001F5 RID: 501
		private Canvas m_Canvas;

		// Token: 0x040001F6 RID: 502
		[NonSerialized]
		private float m_PrevScaleFactor = 1f;

		// Token: 0x040001F7 RID: 503
		[NonSerialized]
		private float m_PrevReferencePixelsPerUnit = 100f;

		// Token: 0x02000073 RID: 115
		public enum ScaleMode
		{
			// Token: 0x040001F9 RID: 505
			ConstantPixelSize,
			// Token: 0x040001FA RID: 506
			ScaleWithScreenSize,
			// Token: 0x040001FB RID: 507
			ConstantPhysicalSize
		}

		// Token: 0x02000074 RID: 116
		public enum ScreenMatchMode
		{
			// Token: 0x040001FD RID: 509
			MatchWidthOrHeight,
			// Token: 0x040001FE RID: 510
			Expand,
			// Token: 0x040001FF RID: 511
			Shrink
		}

		// Token: 0x02000075 RID: 117
		public enum Unit
		{
			// Token: 0x04000201 RID: 513
			Centimeters,
			// Token: 0x04000202 RID: 514
			Millimeters,
			// Token: 0x04000203 RID: 515
			Inches,
			// Token: 0x04000204 RID: 516
			Points,
			// Token: 0x04000205 RID: 517
			Picas
		}
	}
}
