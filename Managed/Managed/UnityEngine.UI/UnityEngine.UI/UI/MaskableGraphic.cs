using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000054 RID: 84
	public abstract class MaskableGraphic : Graphic, IMaskable
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000C758 File Offset: 0x0000A958
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000C760 File Offset: 0x0000A960
		public bool maskable
		{
			get
			{
				return this.m_Maskable;
			}
			set
			{
				if (value == this.m_Maskable)
				{
					return;
				}
				this.m_Maskable = value;
				this.SetMaterialDirty();
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000C77C File Offset: 0x0000A97C
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		public override Material material
		{
			get
			{
				this.UpdateInternalState();
				if (this.m_IncludeForMasking)
				{
					if (this.m_MaskMaterial == null)
					{
						this.m_MaskMaterial = StencilMaterial.Add(base.material, (1 << this.m_StencilValue) - 1);
					}
					return this.m_MaskMaterial ?? base.material;
				}
				return base.material;
			}
			set
			{
				base.material = value;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		private void UpdateInternalState()
		{
			if (!this.m_ShouldRecalculate)
			{
				return;
			}
			this.m_StencilValue = this.GetStencilForGraphic();
			Transform transform = base.transform.parent;
			this.m_IncludeForMasking = false;
			List<Component> list = ComponentListPool.Get();
			while (this.m_Maskable && transform != null)
			{
				transform.GetComponents(typeof(IMask), list);
				if (list.Count > 0)
				{
					this.m_IncludeForMasking = true;
					break;
				}
				transform = transform.parent;
			}
			this.m_ShouldRecalculate = false;
			ComponentListPool.Release(list);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000C888 File Offset: 0x0000AA88
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_ShouldRecalculate = true;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000C898 File Offset: 0x0000AA98
		protected override void OnDisable()
		{
			base.OnDisable();
			this.ClearMaskMaterial();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.m_ShouldRecalculate = true;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		public virtual void ParentMaskStateChanged()
		{
			this.m_ShouldRecalculate = true;
			this.SetMaterialDirty();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000C8C8 File Offset: 0x0000AAC8
		private void ClearMaskMaterial()
		{
			StencilMaterial.Remove(this.m_MaskMaterial);
			this.m_MaskMaterial = null;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C8DC File Offset: 0x0000AADC
		public override void SetMaterialDirty()
		{
			base.SetMaterialDirty();
			this.ClearMaskMaterial();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		private int GetStencilForGraphic()
		{
			int num = 0;
			Transform transform = base.transform.parent;
			List<Component> list = ComponentListPool.Get();
			while (transform != null)
			{
				transform.GetComponents(typeof(IMask), list);
				for (int i = 0; i < list.Count; i++)
				{
					IMask mask = list[i] as IMask;
					if (mask != null && mask.MaskEnabled())
					{
						num++;
						num = Mathf.Clamp(num, 0, 8);
						break;
					}
				}
				transform = transform.parent;
			}
			ComponentListPool.Release(list);
			return num;
		}

		// Token: 0x04000157 RID: 343
		[NonSerialized]
		private bool m_Maskable = true;

		// Token: 0x04000158 RID: 344
		[NonSerialized]
		protected Material m_MaskMaterial;

		// Token: 0x04000159 RID: 345
		[NonSerialized]
		protected bool m_IncludeForMasking;

		// Token: 0x0400015A RID: 346
		[NonSerialized]
		protected int m_StencilValue;

		// Token: 0x0400015B RID: 347
		[NonSerialized]
		protected bool m_ShouldRecalculate = true;
	}
}
