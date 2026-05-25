using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200012E RID: 302
	public struct NetworkMessageInfo
	{
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0001CD20 File Offset: 0x0001AF20
		public double timestamp
		{
			get
			{
				return this.m_TimeStamp;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0001CD28 File Offset: 0x0001AF28
		public NetworkPlayer sender
		{
			get
			{
				return this.m_Sender;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0001CD30 File Offset: 0x0001AF30
		public NetworkView networkView
		{
			get
			{
				if (this.m_ViewID == NetworkViewID.unassigned)
				{
					Debug.LogError("No NetworkView is assigned to this NetworkMessageInfo object. Note that this is expected in OnNetworkInstantiate().");
					return this.NullNetworkView();
				}
				return NetworkView.Find(this.m_ViewID);
			}
		}

		// Token: 0x06000CAC RID: 3244
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern NetworkView NullNetworkView();

		// Token: 0x0400055F RID: 1375
		private double m_TimeStamp;

		// Token: 0x04000560 RID: 1376
		private NetworkPlayer m_Sender;

		// Token: 0x04000561 RID: 1377
		private NetworkViewID m_ViewID;
	}
}
