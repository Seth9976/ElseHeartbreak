using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000070 RID: 112
	[RequireComponent(typeof(RectTransform))]
	[ExecuteInEditMode]
	[AddComponentMenu("Layout/Aspect Ratio Fitter", 142)]
	public class AspectRatioFitter : UIBehaviour, ILayoutController, ILayoutSelfController
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0001142C File Offset: 0x0000F62C
		protected AspectRatioFitter()
		{
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00011440 File Offset: 0x0000F640
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00011448 File Offset: 0x0000F648
		public AspectRatioFitter.AspectMode aspectMode
		{
			get
			{
				return this.m_AspectMode;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<AspectRatioFitter.AspectMode>(ref this.m_AspectMode, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00011464 File Offset: 0x0000F664
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x0001146C File Offset: 0x0000F66C
		public float aspectRatio
		{
			get
			{
				return this.m_AspectRatio;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_AspectRatio, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00011488 File Offset: 0x0000F688
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000114B0 File Offset: 0x0000F6B0
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000114C0 File Offset: 0x0000F6C0
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000114E0 File Offset: 0x0000F6E0
		protected override void OnRectTransformDimensionsChange()
		{
			this.UpdateRect();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000114E8 File Offset: 0x0000F6E8
		private void UpdateRect()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_Tracker.Clear();
			switch (this.m_AspectMode)
			{
			case AspectRatioFitter.AspectMode.WidthControlsHeight:
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDeltaY);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.rectTransform.rect.width / this.m_AspectRatio);
				break;
			case AspectRatioFitter.AspectMode.HeightControlsWidth:
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.SizeDeltaX);
				this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.rectTransform.rect.height * this.m_AspectRatio);
				break;
			case AspectRatioFitter.AspectMode.FitInParent:
			case AspectRatioFitter.AspectMode.EnvelopeParent:
			{
				this.m_Tracker.Add(this, this.rectTransform, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
				this.rectTransform.anchorMin = Vector2.zero;
				this.rectTransform.anchorMax = Vector2.one;
				this.rectTransform.anchoredPosition = Vector2.zero;
				Vector2 zero = Vector2.zero;
				Vector2 parentSize = this.GetParentSize();
				if ((parentSize.y * this.aspectRatio < parentSize.x) ^ (this.m_AspectMode == AspectRatioFitter.AspectMode.FitInParent))
				{
					zero.y = this.GetSizeDeltaToProduceSize(parentSize.x / this.aspectRatio, 1);
				}
				else
				{
					zero.x = this.GetSizeDeltaToProduceSize(parentSize.y * this.aspectRatio, 0);
				}
				this.rectTransform.sizeDelta = zero;
				break;
			}
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00011680 File Offset: 0x0000F880
		private float GetSizeDeltaToProduceSize(float size, int axis)
		{
			return size - this.GetParentSize()[axis] * (this.rectTransform.anchorMax[axis] - this.rectTransform.anchorMin[axis]);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000116C8 File Offset: 0x0000F8C8
		private Vector2 GetParentSize()
		{
			RectTransform rectTransform = this.rectTransform.parent as RectTransform;
			if (!rectTransform)
			{
				return Vector2.zero;
			}
			return rectTransform.rect.size;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00011708 File Offset: 0x0000F908
		public virtual void SetLayoutHorizontal()
		{
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001170C File Offset: 0x0000F90C
		public virtual void SetLayoutVertical()
		{
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00011710 File Offset: 0x0000F910
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateRect();
		}

		// Token: 0x040001E0 RID: 480
		[SerializeField]
		private AspectRatioFitter.AspectMode m_AspectMode;

		// Token: 0x040001E1 RID: 481
		[SerializeField]
		private float m_AspectRatio = 1f;

		// Token: 0x040001E2 RID: 482
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x040001E3 RID: 483
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02000071 RID: 113
		public enum AspectMode
		{
			// Token: 0x040001E5 RID: 485
			None,
			// Token: 0x040001E6 RID: 486
			WidthControlsHeight,
			// Token: 0x040001E7 RID: 487
			HeightControlsWidth,
			// Token: 0x040001E8 RID: 488
			FitInParent,
			// Token: 0x040001E9 RID: 489
			EnvelopeParent
		}
	}
}
