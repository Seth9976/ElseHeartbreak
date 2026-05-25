using System;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x02000027 RID: 39
	public class StopDialogueNode : DialogueNode
	{
		// Token: 0x0600017C RID: 380 RVA: 0x00008388 File Offset: 0x00006588
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_conversationToStop = base.EnsureCell<string>("conversationToStop", "");
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000083A8 File Offset: 0x000065A8
		public override void OnEnter()
		{
			base.Stop();
			this._dialogueRunner.DefocusConversation(this.conversationToStop);
			this._dialogueRunner.StopConversation(this.conversationToStop);
			if (this.conversationToStop != base.conversation)
			{
				base.StartNextNode();
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000083FC File Offset: 0x000065FC
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000840C File Offset: 0x0000660C
		public string conversationToStop
		{
			get
			{
				return this.CELL_conversationToStop.data;
			}
			set
			{
				this.CELL_conversationToStop.data = value;
			}
		}

		// Token: 0x04000089 RID: 137
		private ValueEntry<string> CELL_conversationToStop;
	}
}
