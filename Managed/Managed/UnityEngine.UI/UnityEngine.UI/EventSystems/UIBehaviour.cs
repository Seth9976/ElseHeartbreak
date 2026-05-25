using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200001E RID: 30
	public abstract class UIBehaviour : MonoBehaviour
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000031A8 File Offset: 0x000013A8
		protected virtual void Awake()
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000031AC File Offset: 0x000013AC
		protected virtual void OnEnable()
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000031B0 File Offset: 0x000013B0
		protected virtual void Start()
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000031B4 File Offset: 0x000013B4
		protected virtual void OnDisable()
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000031B8 File Offset: 0x000013B8
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000031BC File Offset: 0x000013BC
		public virtual bool IsActive()
		{
			return base.enabled && base.isActiveAndEnabled && base.gameObject.activeInHierarchy;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000031F0 File Offset: 0x000013F0
		protected virtual void OnRectTransformDimensionsChange()
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000031F4 File Offset: 0x000013F4
		protected virtual void OnBeforeTransformParentChanged()
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000031F8 File Offset: 0x000013F8
		protected virtual void OnTransformParentChanged()
		{
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000031FC File Offset: 0x000013FC
		protected virtual void OnDidApplyAnimationProperties()
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003200 File Offset: 0x00001400
		protected virtual void OnCanvasGroupChanged()
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003204 File Offset: 0x00001404
		protected virtual void OnCanvasHierarchyChanged()
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003208 File Offset: 0x00001408
		public bool IsDestroyed()
		{
			return this == null;
		}
	}
}
