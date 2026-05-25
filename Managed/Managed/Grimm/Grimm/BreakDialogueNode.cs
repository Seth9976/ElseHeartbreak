using System;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x02000025 RID: 37
	public class BreakDialogueNode : DialogueNode
	{
		// Token: 0x0600016B RID: 363 RVA: 0x000082F8 File Offset: 0x000064F8
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_breakTargetLoop = base.EnsureCell<string>("breakTarget", "undefined");
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008318 File Offset: 0x00006518
		public override void Update(float dt)
		{
			base.Stop();
			LoopDialogueNode loopDialogueNode = this._dialogueRunner.GetDialogueNode(base.conversation, this.breakTargetLoop) as LoopDialogueNode;
			if (loopDialogueNode == null)
			{
				throw new GrimmException("targetLoopDialogueNode was not of type LoopDialogueNode");
			}
			loopDialogueNode.Break();
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00008360 File Offset: 0x00006560
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00008370 File Offset: 0x00006570
		public string breakTargetLoop
		{
			get
			{
				return this.CELL_breakTargetLoop.data;
			}
			set
			{
				this.CELL_breakTargetLoop.data = value;
			}
		}

		// Token: 0x04000088 RID: 136
		private ValueEntry<string> CELL_breakTargetLoop;
	}
}
