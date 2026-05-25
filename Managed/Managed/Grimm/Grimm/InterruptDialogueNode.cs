using System;
using GameTypes;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x02000028 RID: 40
	public class InterruptDialogueNode : DialogueNode
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00008424 File Offset: 0x00006624
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_interruptingConversation = base.EnsureCell<string>("interconvo", "undefined");
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008444 File Offset: 0x00006644
		public override void OnEnter()
		{
			this._dialogueRunner.StartConversation(this.interruptingConversation);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008458 File Offset: 0x00006658
		public override void Update(float dt)
		{
			if (!this._dialogueRunner.ConversationIsRunning(this.interruptingConversation))
			{
				D.Log("Detected that interrupting conversation " + this.interruptingConversation + " has stopped, will continue in " + base.conversation);
				base.Stop();
				base.StartNextNode();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000184 RID: 388 RVA: 0x000084A8 File Offset: 0x000066A8
		// (set) Token: 0x06000185 RID: 389 RVA: 0x000084B8 File Offset: 0x000066B8
		public string interruptingConversation
		{
			get
			{
				return this.CELL_interruptingConversation.data;
			}
			set
			{
				this.CELL_interruptingConversation.data = value;
			}
		}

		// Token: 0x0400008A RID: 138
		private ValueEntry<string> CELL_interruptingConversation;
	}
}
