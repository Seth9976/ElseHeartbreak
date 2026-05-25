using System;

namespace GrimmLib
{
	// Token: 0x02000022 RID: 34
	public class FocusDialogueNode : DialogueNode
	{
		// Token: 0x06000161 RID: 353 RVA: 0x000081F0 File Offset: 0x000063F0
		public override void OnEnter()
		{
			this._dialogueRunner.FocusConversation(base.conversation);
			base.Stop();
			base.StartNextNode();
		}
	}
}
