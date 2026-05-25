using System;

namespace GrimmLib
{
	// Token: 0x02000023 RID: 35
	public class DefocusDialogueNode : DialogueNode
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00008218 File Offset: 0x00006418
		public override void OnEnter()
		{
			this._dialogueRunner.DefocusConversation(base.conversation);
			base.Stop();
			base.StartNextNode();
		}
	}
}
