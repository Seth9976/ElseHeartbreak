using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200002E RID: 46
	[AddComponentMenu("Event/Physics Raycaster")]
	[RequireComponent(typeof(Camera))]
	public class PhysicsRaycaster : BaseRaycaster
	{
		// Token: 0x06000123 RID: 291 RVA: 0x000050B4 File Offset: 0x000032B4
		protected PhysicsRaycaster()
		{
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000050C8 File Offset: 0x000032C8
		public override Camera eventCamera
		{
			get
			{
				if (this.m_EventCamera == null)
				{
					this.m_EventCamera = base.GetComponent<Camera>();
				}
				return this.m_EventCamera ?? Camera.main;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000050FC File Offset: 0x000032FC
		public virtual int depth
		{
			get
			{
				return (!(this.eventCamera != null)) ? 16777215 : ((int)this.eventCamera.depth);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005130 File Offset: 0x00003330
		public int finalEventMask
		{
			get
			{
				return (!(this.eventCamera != null)) ? (-1) : (this.eventCamera.cullingMask & this.m_EventMask);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000516C File Offset: 0x0000336C
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00005174 File Offset: 0x00003374
		public LayerMask eventMask
		{
			get
			{
				return this.m_EventMask;
			}
			set
			{
				this.m_EventMask = value;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005180 File Offset: 0x00003380
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.eventCamera == null)
			{
				return;
			}
			Ray ray = this.eventCamera.ScreenPointToRay(eventData.position);
			float num = this.eventCamera.farClipPlane - this.eventCamera.nearClipPlane;
			RaycastHit[] array = Physics.RaycastAll(ray, num, this.finalEventMask);
			if (array.Length > 1)
			{
				Array.Sort<RaycastHit>(array, (RaycastHit r1, RaycastHit r2) => r1.distance.CompareTo(r2.distance));
			}
			if (array.Length != 0)
			{
				int i = 0;
				int num2 = array.Length;
				while (i < num2)
				{
					RaycastResult raycastResult = new RaycastResult
					{
						gameObject = array[i].collider.gameObject,
						module = this,
						distance = array[i].distance,
						worldPosition = array[i].point,
						worldNormal = array[i].normal,
						screenPosition = eventData.position,
						index = (float)resultAppendList.Count
					};
					resultAppendList.Add(raycastResult);
					i++;
				}
			}
		}

		// Token: 0x04000087 RID: 135
		protected const int kNoEventMaskSet = -1;

		// Token: 0x04000088 RID: 136
		protected Camera m_EventCamera;

		// Token: 0x04000089 RID: 137
		[SerializeField]
		protected LayerMask m_EventMask = -1;
	}
}
