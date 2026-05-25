using System;

namespace GrimmLib
{
	// Token: 0x0200000F RID: 15
	public class ConversationEndDialogueNode : DialogueNode
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00004490 File Offset: 0x00002690
		public override void Update(float dt)
		{
			base.Stop();
			this._dialogueRunner.StopConversation(base.conversation);
		}
	}
}
