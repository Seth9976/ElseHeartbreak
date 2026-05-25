using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000024 RID: 36
	[RequireComponent(typeof(EventSystem))]
	public abstract class BaseInputModule : UIBehaviour
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000036A0 File Offset: 0x000018A0
		protected EventSystem eventSystem
		{
			get
			{
				return this.m_EventSystem;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000036A8 File Offset: 0x000018A8
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_EventSystem = base.GetComponent<EventSystem>();
			this.m_EventSystem.UpdateModules();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000036C8 File Offset: 0x000018C8
		protected override void OnDisable()
		{
			this.m_EventSystem.UpdateModules();
			base.OnDisable();
		}

		// Token: 0x060000C4 RID: 196
		public abstract void Process();

		// Token: 0x060000C5 RID: 197 RVA: 0x000036DC File Offset: 0x000018DC
		protected static RaycastResult FindFirstRaycast(List<RaycastResult> candidates)
		{
			for (int i = 0; i < candidates.Count; i++)
			{
				if (!(candidates[i].gameObject == null))
				{
					return candidates[i];
				}
			}
			return default(RaycastResult);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003730 File Offset: 0x00001930
		protected static MoveDirection DetermineMoveDirection(float x, float y)
		{
			return BaseInputModule.DetermineMoveDirection(x, y, 0.6f);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003740 File Offset: 0x00001940
		protected static MoveDirection DetermineMoveDirection(float x, float y, float deadZone)
		{
			Vector2 vector = new Vector2(x, y);
			if (vector.sqrMagnitude < deadZone * deadZone)
			{
				return MoveDirection.None;
			}
			if (Mathf.Abs(x) > Mathf.Abs(y))
			{
				if (x > 0f)
				{
					return MoveDirection.Right;
				}
				return MoveDirection.Left;
			}
			else
			{
				if (y > 0f)
				{
					return MoveDirection.Up;
				}
				return MoveDirection.Down;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003798 File Offset: 0x00001998
		protected static GameObject FindCommonRoot(GameObject g1, GameObject g2)
		{
			if (g1 == null || g2 == null)
			{
				return null;
			}
			Transform transform = g1.transform;
			while (transform != null)
			{
				Transform transform2 = g2.transform;
				while (transform2 != null)
				{
					if (transform == transform2)
					{
						return transform.gameObject;
					}
					transform2 = transform2.parent;
				}
				transform = transform.parent;
			}
			return null;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003814 File Offset: 0x00001A14
		protected void HandlePointerExitAndEnter(PointerEventData currentPointerData, GameObject newEnterTarget)
		{
			if (currentPointerData.pointerEnter == newEnterTarget)
			{
				return;
			}
			GameObject gameObject = BaseInputModule.FindCommonRoot(currentPointerData.pointerEnter, newEnterTarget);
			if (currentPointerData.pointerEnter != null)
			{
				Transform transform = currentPointerData.pointerEnter.transform;
				while (transform != null)
				{
					if (gameObject != null && gameObject.transform == transform)
					{
						break;
					}
					ExecuteEvents.Execute<IPointerExitHandler>(transform.gameObject, currentPointerData, ExecuteEvents.pointerExitHandler);
					transform = transform.parent;
				}
			}
			if (newEnterTarget != null)
			{
				Transform transform2 = newEnterTarget.transform;
				while (transform2 != null && transform2.gameObject != gameObject)
				{
					ExecuteEvents.Execute<IPointerEnterHandler>(transform2.gameObject, currentPointerData, ExecuteEvents.pointerEnterHandler);
					transform2 = transform2.parent;
				}
			}
			currentPointerData.pointerEnter = newEnterTarget;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003900 File Offset: 0x00001B00
		protected virtual AxisEventData GetAxisEventData(float x, float y, float moveDeadZone)
		{
			if (this.m_AxisEventData == null)
			{
				this.m_AxisEventData = new AxisEventData(this.eventSystem);
			}
			this.m_AxisEventData.Reset();
			this.m_AxisEventData.moveVector = new Vector2(x, y);
			this.m_AxisEventData.moveDir = BaseInputModule.DetermineMoveDirection(x, y, moveDeadZone);
			return this.m_AxisEventData;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003960 File Offset: 0x00001B60
		protected virtual BaseEventData GetBaseEventData()
		{
			if (this.m_BaseEventData == null)
			{
				this.m_BaseEventData = new BaseEventData(this.eventSystem);
			}
			this.m_BaseEventData.Reset();
			return this.m_BaseEventData;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003990 File Offset: 0x00001B90
		public virtual bool IsPointerOverGameObject(int pointerId)
		{
			return false;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003994 File Offset: 0x00001B94
		public virtual bool ShouldActivateModule()
		{
			return base.enabled && base.gameObject.activeInHierarchy;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000039B0 File Offset: 0x00001BB0
		public virtual void DeactivateModule()
		{
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000039B4 File Offset: 0x00001BB4
		public virtual void ActivateModule()
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000039B8 File Offset: 0x00001BB8
		public virtual void UpdateModule()
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000039BC File Offset: 0x00001BBC
		public virtual bool IsModuleSupported()
		{
			return true;
		}

		// Token: 0x04000069 RID: 105
		[NonSerialized]
		protected List<RaycastResult> m_RaycastResultCache = new List<RaycastResult>();

		// Token: 0x0400006A RID: 106
		private AxisEventData m_AxisEventData;

		// Token: 0x0400006B RID: 107
		private EventSystem m_EventSystem;

		// Token: 0x0400006C RID: 108
		private BaseEventData m_BaseEventData;
	}
}
