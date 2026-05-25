using System;

namespace GrimmLib
{
	// Token: 0x02000010 RID: 16
	public class ConversationStartDialogueNode : DialogueNode
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000044B4 File Offset: 0x000026B4
		public override void OnEnter()
		{
			base.Stop();
			base.StartNextNode();
		}
	}
}
