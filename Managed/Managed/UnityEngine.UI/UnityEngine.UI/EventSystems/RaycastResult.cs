using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200001D RID: 29
	public struct RaycastResult
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002FB8 File Offset: 0x000011B8
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002FC0 File Offset: 0x000011C0
		public GameObject gameObject
		{
			get
			{
				return this.m_GameObject;
			}
			set
			{
				this.m_GameObject = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002FCC File Offset: 0x000011CC
		public bool isValid
		{
			get
			{
				return this.module != null && this.gameObject != null;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002FFC File Offset: 0x000011FC
		public void Clear()
		{
			this.gameObject = null;
			this.module = null;
			this.distance = 0f;
			this.index = 0f;
			this.depth = 0;
			this.sortingLayer = 0;
			this.sortingOrder = 0;
			this.worldNormal = Vector3.up;
			this.worldPosition = Vector3.zero;
			this.screenPosition = Vector2.zero;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003064 File Offset: 0x00001264
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Name: ",
				this.gameObject.name,
				"\nmodule: ",
				this.module.camera,
				"\ndistance: ",
				this.distance,
				"\nindex: ",
				this.index,
				"\ndepth: ",
				this.depth,
				"\nworldNormal: ",
				this.worldNormal,
				"\nworldPosition: ",
				this.worldPosition,
				"\nscreenPosition: ",
				this.screenPosition,
				"\nmodule.sortOrderPriority: ",
				this.module.sortOrderPriority,
				"\nmodule.renderOrderPriority: ",
				this.module.renderOrderPriority,
				"\nsortingLayer: ",
				this.sortingLayer,
				"\nsortingOrder: ",
				this.sortingOrder
			});
		}

		// Token: 0x0400003E RID: 62
		private GameObject m_GameObject;

		// Token: 0x0400003F RID: 63
		public BaseRaycaster module;

		// Token: 0x04000040 RID: 64
		public float distance;

		// Token: 0x04000041 RID: 65
		public float index;

		// Token: 0x04000042 RID: 66
		public int depth;

		// Token: 0x04000043 RID: 67
		public int sortingLayer;

		// Token: 0x04000044 RID: 68
		public int sortingOrder;

		// Token: 0x04000045 RID: 69
		public Vector3 worldPosition;

		// Token: 0x04000046 RID: 70
		public Vector3 worldNormal;

		// Token: 0x04000047 RID: 71
		public Vector2 screenPosition;
	}
}
