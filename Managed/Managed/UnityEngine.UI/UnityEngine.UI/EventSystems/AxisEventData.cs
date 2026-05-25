using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200001F RID: 31
	public class AxisEventData : BaseEventData
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00003214 File Offset: 0x00001414
		public AxisEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.moveVector = Vector2.zero;
			this.moveDir = MoveDirection.None;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003230 File Offset: 0x00001430
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003238 File Offset: 0x00001438
		public Vector2 moveVector { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003244 File Offset: 0x00001444
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000324C File Offset: 0x0000144C
		public MoveDirection moveDir { get; set; }
	}
}
