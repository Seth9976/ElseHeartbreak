using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000076 RID: 118
	[AddComponentMenu("Layout/Content Size Fitter", 141)]
	[RequireComponent(typeof(RectTransform))]
	[ExecuteInEditMode]
	public class ContentSizeFitter : UIBehaviour, ILayoutController, ILayoutSelfController
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x00011B90 File Offset: 0x0000FD90
		protected ContentSizeFitter()
		{
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00011B98 File Offset: 0x0000FD98
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		public ContentSizeFitter.FitMode horizontalFit
		{
			get
			{
				return this.m_HorizontalFit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ContentSizeFitter.FitMode>(ref this.m_HorizontalFit, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00011BBC File Offset: 0x0000FDBC
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x00011BC4 File Offset: 0x0000FDC4
		public ContentSizeFitter.FitMode verticalFit
		{
			get
			{
				return this.m_VerticalFit;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ContentSizeFitter.FitMode>(ref this.m_VerticalFit, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00011BE0 File Offset: 0x0000FDE0
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

		// Token: 0x06000405 RID: 1029 RVA: 0x00011C08 File Offset: 0x0000FE08
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00011C18 File Offset: 0x0000FE18
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00011C38 File Offset: 0x0000FE38
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00011C40 File Offset: 0x0000FE40
		private void HandleSelfFittingAlongAxis(int axis)
		{
			ContentSizeFitter.FitMode fitMode = ((axis != 0) ? this.verticalFit : this.horizontalFit);
			if (fitMode == ContentSizeFitter.FitMode.Unconstrained)
			{
				return;
			}
			this.m_Tracker.Add(this, this.rectTransform, (axis != 0) ? DrivenTransformProperties.SizeDeltaY : DrivenTransformProperties.SizeDeltaX);
			if (fitMode == ContentSizeFitter.FitMode.MinSize)
			{
				this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetMinSize(this.m_Rect, axis));
			}
			else
			{
				this.rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, LayoutUtility.GetPreferredSize(this.m_Rect, axis));
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00011CD0 File Offset: 0x0000FED0
		public virtual void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			this.HandleSelfFittingAlongAxis(0);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		public virtual void SetLayoutVertical()
		{
			this.HandleSelfFittingAlongAxis(1);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x04000206 RID: 518
		[SerializeField]
		protected ContentSizeFitter.FitMode m_HorizontalFit;

		// Token: 0x04000207 RID: 519
		[SerializeField]
		protected ContentSizeFitter.FitMode m_VerticalFit;

		// Token: 0x04000208 RID: 520
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x04000209 RID: 521
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02000077 RID: 119
		public enum FitMode
		{
			// Token: 0x0400020B RID: 523
			Unconstrained,
			// Token: 0x0400020C RID: 524
			MinSize,
			// Token: 0x0400020D RID: 525
			PreferredSize
		}
	}
}
