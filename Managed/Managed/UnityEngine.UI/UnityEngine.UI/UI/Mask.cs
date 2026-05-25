using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000089 RID: 137
	[ExecuteInEditMode]
	[AddComponentMenu("UI/Mask", 13)]
	public class Mask : UIBehaviour, IGraphicEnabledDisabled, IMask, ICanvasRaycastFilter, IMaterialModifier
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x000136C0 File Offset: 0x000118C0
		protected Mask()
		{
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x000136D0 File Offset: 0x000118D0
		private Graphic graphic
		{
			get
			{
				if (this.m_Graphic == null)
				{
					this.m_Graphic = base.GetComponent<Graphic>();
				}
				return this.m_Graphic;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000136F8 File Offset: 0x000118F8
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00013700 File Offset: 0x00011900
		public bool showMaskGraphic
		{
			get
			{
				return this.m_ShowMaskGraphic;
			}
			set
			{
				if (this.m_ShowMaskGraphic == value)
				{
					return;
				}
				this.m_ShowMaskGraphic = value;
				if (this.graphic != null)
				{
					this.graphic.SetMaterialDirty();
				}
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00013740 File Offset: 0x00011940
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

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001376C File Offset: 0x0001196C
		public virtual bool MaskEnabled()
		{
			return this.IsActive() && this.graphic != null;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00013788 File Offset: 0x00011988
		public virtual void OnSiblingGraphicEnabledDisabled()
		{
			this.NotifyMaskStateChanged();
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00013790 File Offset: 0x00011990
		private void NotifyMaskStateChanged()
		{
			if (this.graphic != null)
			{
				this.graphic.canvasRenderer.isMask = this.IsActive();
				this.graphic.SetMaterialDirty();
			}
			List<Component> list = ComponentListPool.Get();
			base.GetComponentsInChildren<Component>(list);
			for (int i = 0; i < list.Count; i++)
			{
				if (!(list[i] == null) && !(list[i].gameObject == base.gameObject))
				{
					IMaskable maskable = list[i] as IMaskable;
					if (maskable != null)
					{
						maskable.ParentMaskStateChanged();
					}
				}
			}
			ComponentListPool.Release(list);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00013848 File Offset: 0x00011A48
		private void ClearCachedMaterial()
		{
			if (this.m_RenderMaterial != null)
			{
				Misc.DestroyImmediate(this.m_RenderMaterial);
			}
			this.m_RenderMaterial = null;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00013870 File Offset: 0x00011A70
		protected override void OnEnable()
		{
			base.OnEnable();
			this.NotifyMaskStateChanged();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00013880 File Offset: 0x00011A80
		protected override void OnDisable()
		{
			base.OnDisable();
			this.ClearCachedMaterial();
			this.NotifyMaskStateChanged();
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00013894 File Offset: 0x00011A94
		public virtual bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
		{
			return RectTransformUtility.RectangleContainsScreenPoint(this.rectTransform, sp, eventCamera);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000138A4 File Offset: 0x00011AA4
		public virtual Material GetModifiedMaterial(Material baseMaterial)
		{
			this.ClearCachedMaterial();
			if (!this.IsActive())
			{
				return baseMaterial;
			}
			this.m_RenderMaterial = new Material(baseMaterial)
			{
				name = "Mask  (" + baseMaterial.name + ")",
				hideFlags = HideFlags.HideAndDontSave
			};
			if (this.m_RenderMaterial.HasProperty("_ColorMask"))
			{
				this.m_RenderMaterial.SetInt("_ColorMask", (!this.m_ShowMaskGraphic) ? 0 : 15);
			}
			else
			{
				Debug.LogWarning("Material " + baseMaterial + " doesn't have color mask", baseMaterial);
			}
			return this.m_RenderMaterial;
		}

		// Token: 0x04000241 RID: 577
		[SerializeField]
		[FormerlySerializedAs("m_ShowGraphic")]
		private bool m_ShowMaskGraphic = true;

		// Token: 0x04000242 RID: 578
		private Material m_RenderMaterial;

		// Token: 0x04000243 RID: 579
		private Graphic m_Graphic;

		// Token: 0x04000244 RID: 580
		private RectTransform m_RectTransform;
	}
}
