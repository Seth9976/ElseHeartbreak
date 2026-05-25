using System;

namespace GrimmLib
{
	// Token: 0x0200001F RID: 31
	public class SilentDialogueNode : DialogueNode
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00008100 File Offset: 0x00006300
		public override void OnEnter()
		{
			base.Stop();
		}
	}
}
