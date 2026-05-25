using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000083 RID: 131
	[AddComponentMenu("Layout/Layout Element", 140)]
	[ExecuteInEditMode]
	[RequireComponent(typeof(RectTransform))]
	public class LayoutElement : UIBehaviour, ILayoutElement, ILayoutIgnorer
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x000128E4 File Offset: 0x00010AE4
		protected LayoutElement()
		{
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0001293C File Offset: 0x00010B3C
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00012944 File Offset: 0x00010B44
		public virtual bool ignoreLayout
		{
			get
			{
				return this.m_IgnoreLayout;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_IgnoreLayout, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00012960 File Offset: 0x00010B60
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00012964 File Offset: 0x00010B64
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00012968 File Offset: 0x00010B68
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00012970 File Offset: 0x00010B70
		public virtual float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0001298C File Offset: 0x00010B8C
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x00012994 File Offset: 0x00010B94
		public virtual float minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000129B0 File Offset: 0x00010BB0
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x000129B8 File Offset: 0x00010BB8
		public virtual float preferredWidth
		{
			get
			{
				return this.m_PreferredWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x000129D4 File Offset: 0x00010BD4
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x000129DC File Offset: 0x00010BDC
		public virtual float preferredHeight
		{
			get
			{
				return this.m_PreferredHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x000129F8 File Offset: 0x00010BF8
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x00012A00 File Offset: 0x00010C00
		public virtual float flexibleWidth
		{
			get
			{
				return this.m_FlexibleWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00012A1C File Offset: 0x00010C1C
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00012A24 File Offset: 0x00010C24
		public virtual float flexibleHeight
		{
			get
			{
				return this.m_FlexibleHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00012A40 File Offset: 0x00010C40
		public virtual int layoutPriority
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00012A44 File Offset: 0x00010C44
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00012A54 File Offset: 0x00010C54
		protected override void OnTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00012A5C File Offset: 0x00010C5C
		protected override void OnDisable()
		{
			this.SetDirty();
			base.OnDisable();
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00012A6C File Offset: 0x00010C6C
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00012A74 File Offset: 0x00010C74
		protected override void OnBeforeTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00012A7C File Offset: 0x00010C7C
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(base.transform as RectTransform);
		}

		// Token: 0x04000223 RID: 547
		[SerializeField]
		private bool m_IgnoreLayout;

		// Token: 0x04000224 RID: 548
		[SerializeField]
		private float m_MinWidth = -1f;

		// Token: 0x04000225 RID: 549
		[SerializeField]
		private float m_MinHeight = -1f;

		// Token: 0x04000226 RID: 550
		[SerializeField]
		private float m_PreferredWidth = -1f;

		// Token: 0x04000227 RID: 551
		[SerializeField]
		private float m_PreferredHeight = -1f;

		// Token: 0x04000228 RID: 552
		[SerializeField]
		private float m_FlexibleWidth = -1f;

		// Token: 0x04000229 RID: 553
		[SerializeField]
		private float m_FlexibleHeight = -1f;
	}
}
