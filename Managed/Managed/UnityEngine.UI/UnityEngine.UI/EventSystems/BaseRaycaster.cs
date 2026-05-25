using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200002C RID: 44
	public abstract class BaseRaycaster : UIBehaviour
	{
		// Token: 0x0600011A RID: 282
		public abstract void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList);

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600011B RID: 283
		public abstract Camera eventCamera { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004ED8 File Offset: 0x000030D8
		[Obsolete("Please use sortOrderPriority and renderOrderPriority", false)]
		public virtual int priority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00004EDC File Offset: 0x000030DC
		public virtual int sortOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004EE4 File Offset: 0x000030E4
		public virtual int renderOrderPriority
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004EEC File Offset: 0x000030EC
		protected override void OnEnable()
		{
			base.OnEnable();
			RaycasterManager.AddRaycaster(this);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004EFC File Offset: 0x000030FC
		protected override void OnDisable()
		{
			RaycasterManager.RemoveRaycasters(this);
			base.OnDisable();
		}
	}
}
