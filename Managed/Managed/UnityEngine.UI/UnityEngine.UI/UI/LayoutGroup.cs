using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000084 RID: 132
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	[ExecuteInEditMode]
	public abstract class LayoutGroup : UIBehaviour, ILayoutElement, ILayoutController, ILayoutGroup
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x00012A9C File Offset: 0x00010C9C
		protected LayoutGroup()
		{
			if (this.m_Padding == null)
			{
				this.m_Padding = new RectOffset();
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00012AFC File Offset: 0x00010CFC
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00012B04 File Offset: 0x00010D04
		public RectOffset padding
		{
			get
			{
				return this.m_Padding;
			}
			set
			{
				this.SetProperty<RectOffset>(ref this.m_Padding, value);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00012B14 File Offset: 0x00010D14
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00012B1C File Offset: 0x00010D1C
		public TextAnchor childAlignment
		{
			get
			{
				return this.m_ChildAlignment;
			}
			set
			{
				this.SetProperty<TextAnchor>(ref this.m_ChildAlignment, value);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00012B2C File Offset: 0x00010D2C
		protected RectTransform rectTransform
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

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00012B54 File Offset: 0x00010D54
		protected List<RectTransform> rectChildren
		{
			get
			{
				return this.m_RectChildren;
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00012B5C File Offset: 0x00010D5C
		public virtual void CalculateLayoutInputHorizontal()
		{
			this.m_RectChildren.Clear();
			for (int i = 0; i < this.rectTransform.childCount; i++)
			{
				RectTransform rectTransform = this.rectTransform.GetChild(i) as RectTransform;
				if (!(rectTransform == null))
				{
					ILayoutIgnorer layoutIgnorer = rectTransform.GetComponent(typeof(ILayoutIgnorer)) as ILayoutIgnorer;
					if (rectTransform.gameObject.activeInHierarchy && (layoutIgnorer == null || !layoutIgnorer.ignoreLayout))
					{
						this.m_RectChildren.Add(rectTransform);
					}
				}
			}
			this.m_Tracker.Clear();
		}

		// Token: 0x06000458 RID: 1112
		public abstract void CalculateLayoutInputVertical();

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00012C04 File Offset: 0x00010E04
		public virtual float minWidth
		{
			get
			{
				return this.GetTotalMinSize(0);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00012C10 File Offset: 0x00010E10
		public virtual float preferredWidth
		{
			get
			{
				return this.GetTotalPreferredSize(0);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00012C1C File Offset: 0x00010E1C
		public virtual float flexibleWidth
		{
			get
			{
				return this.GetTotalFlexibleSize(0);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00012C28 File Offset: 0x00010E28
		public virtual float minHeight
		{
			get
			{
				return this.GetTotalMinSize(1);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00012C34 File Offset: 0x00010E34
		public virtual float preferredHeight
		{
			get
			{
				return this.GetTotalPreferredSize(1);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00012C40 File Offset: 0x00010E40
		public virtual float flexibleHeight
		{
			get
			{
				return this.GetTotalFlexibleSize(1);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00012C4C File Offset: 0x00010E4C
		public virtual int layoutPriority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000460 RID: 1120
		public abstract void SetLayoutHorizontal();

		// Token: 0x06000461 RID: 1121
		public abstract void SetLayoutVertical();

		// Token: 0x06000462 RID: 1122 RVA: 0x00012C50 File Offset: 0x00010E50
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00012C60 File Offset: 0x00010E60
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00012C80 File Offset: 0x00010E80
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00012C88 File Offset: 0x00010E88
		protected float GetTotalMinSize(int axis)
		{
			return this.m_TotalMinSize[axis];
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00012C98 File Offset: 0x00010E98
		protected float GetTotalPreferredSize(int axis)
		{
			return this.m_TotalPreferredSize[axis];
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00012CA8 File Offset: 0x00010EA8
		protected float GetTotalFlexibleSize(int axis)
		{
			return this.m_TotalFlexibleSize[axis];
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00012CB8 File Offset: 0x00010EB8
		protected float GetStartOffset(int axis, float requiredSpaceWithoutPadding)
		{
			float num = requiredSpaceWithoutPadding + (float)((axis != 0) ? this.padding.vertical : this.padding.horizontal);
			float num2 = this.rectTransform.rect.size[axis];
			float num3 = num2 - num;
			float num4;
			if (axis == 0)
			{
				num4 = (float)(this.childAlignment % TextAnchor.MiddleLeft) * 0.5f;
			}
			else
			{
				num4 = (float)(this.childAlignment / TextAnchor.MiddleLeft) * 0.5f;
			}
			return (float)((axis != 0) ? this.padding.top : this.padding.left) + num3 * num4;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00012D64 File Offset: 0x00010F64
		protected void SetLayoutInputForAxis(float totalMin, float totalPreferred, float totalFlexible, int axis)
		{
			this.m_TotalMinSize[axis] = totalMin;
			this.m_TotalPreferredSize[axis] = totalPreferred;
			this.m_TotalFlexibleSize[axis] = totalFlexible;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00012D9C File Offset: 0x00010F9C
		protected void SetChildAlongAxis(RectTransform rect, int axis, float pos, float size)
		{
			if (rect == null)
			{
				return;
			}
			this.m_Tracker.Add(this, rect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
			rect.SetInsetAndSizeFromParentEdge((axis != 0) ? RectTransform.Edge.Top : RectTransform.Edge.Left, pos, size);
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00012DE0 File Offset: 0x00010FE0
		private bool isRootLayoutGroup
		{
			get
			{
				Transform parent = base.transform.parent;
				return parent == null || base.transform.parent.GetComponent(typeof(ILayoutGroup)) == null;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00012E28 File Offset: 0x00011028
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (this.isRootLayoutGroup)
			{
				this.SetDirty();
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00012E44 File Offset: 0x00011044
		protected virtual void OnTransformChildrenChanged()
		{
			this.SetDirty();
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00012E4C File Offset: 0x0001104C
		protected void SetProperty<T>(ref T currentValue, T newValue)
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return;
			}
			currentValue = newValue;
			this.SetDirty();
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00012EAC File Offset: 0x000110AC
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x0400022A RID: 554
		[SerializeField]
		protected RectOffset m_Padding = new RectOffset();

		// Token: 0x0400022B RID: 555
		[SerializeField]
		[FormerlySerializedAs("m_Alignment")]
		protected TextAnchor m_ChildAlignment;

		// Token: 0x0400022C RID: 556
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x0400022D RID: 557
		protected DrivenRectTransformTracker m_Tracker;

		// Token: 0x0400022E RID: 558
		private Vector2 m_TotalMinSize = Vector2.zero;

		// Token: 0x0400022F RID: 559
		private Vector2 m_TotalPreferredSize = Vector2.zero;

		// Token: 0x04000230 RID: 560
		private Vector2 m_TotalFlexibleSize = Vector2.zero;

		// Token: 0x04000231 RID: 561
		[NonSerialized]
		private List<RectTransform> m_RectChildren = new List<RectTransform>();
	}
}
