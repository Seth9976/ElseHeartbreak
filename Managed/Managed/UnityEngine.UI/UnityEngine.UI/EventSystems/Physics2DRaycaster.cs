using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200002D RID: 45
	[AddComponentMenu("Event/Physics 2D Raycaster")]
	[RequireComponent(typeof(Camera))]
	public class Physics2DRaycaster : PhysicsRaycaster
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00004F0C File Offset: 0x0000310C
		protected Physics2DRaycaster()
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004F14 File Offset: 0x00003114
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.eventCamera == null)
			{
				return;
			}
			Ray ray = this.eventCamera.ScreenPointToRay(eventData.position);
			float num = this.eventCamera.farClipPlane - this.eventCamera.nearClipPlane;
			RaycastHit2D[] array = Physics2D.RaycastAll(ray.origin, ray.direction, num, base.finalEventMask);
			if (array.Length != 0)
			{
				int i = 0;
				int num2 = array.Length;
				while (i < num2)
				{
					SpriteRenderer component = array[i].collider.gameObject.GetComponent<SpriteRenderer>();
					RaycastResult raycastResult = new RaycastResult
					{
						gameObject = array[i].collider.gameObject,
						module = this,
						distance = Vector3.Distance(this.eventCamera.transform.position, array[i].transform.position),
						worldPosition = array[i].point,
						worldNormal = array[i].normal,
						screenPosition = eventData.position,
						index = (float)resultAppendList.Count,
						sortingLayer = ((!(component != null)) ? 0 : component.sortingLayerID),
						sortingOrder = ((!(component != null)) ? 0 : component.sortingOrder)
					};
					resultAppendList.Add(raycastResult);
					i++;
				}
			}
		}
	}
}
