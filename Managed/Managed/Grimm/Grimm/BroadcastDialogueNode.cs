using System;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x02000020 RID: 32
	public class BroadcastDialogueNode : DialogueNode
	{
		// Token: 0x06000157 RID: 343 RVA: 0x00008110 File Offset: 0x00006310
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_eventName = base.EnsureCell<string>("eventName", "undefined");
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00008130 File Offset: 0x00006330
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00008140 File Offset: 0x00006340
		public string eventName
		{
			get
			{
				return this.CELL_eventName.data;
			}
			set
			{
				this.CELL_eventName.data = value;
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008150 File Offset: 0x00006350
		public override void OnEnter()
		{
			base.Stop();
			this._dialogueRunner.EventHappened(this.eventName);
			base.StartNextNode();
		}

		// Token: 0x04000084 RID: 132
		private ValueEntry<string> CELL_eventName;
	}
}
