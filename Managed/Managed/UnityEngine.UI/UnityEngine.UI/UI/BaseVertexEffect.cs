using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200008E RID: 142
	[ExecuteInEditMode]
	public abstract class BaseVertexEffect : UIBehaviour, IVertexModifier
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00013DE4 File Offset: 0x00011FE4
		protected Graphic graphic
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

		// Token: 0x060004CD RID: 1229 RVA: 0x00013E0C File Offset: 0x0001200C
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00013E3C File Offset: 0x0001203C
		protected override void OnDisable()
		{
			if (this.graphic != null)
			{
				this.graphic.SetVerticesDirty();
			}
			base.OnDisable();
		}

		// Token: 0x060004CF RID: 1231
		public abstract void ModifyVertices(List<UIVertex> verts);

		// Token: 0x0400024F RID: 591
		[NonSerialized]
		private Graphic m_Graphic;
	}
}
