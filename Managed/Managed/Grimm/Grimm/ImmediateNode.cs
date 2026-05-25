using System;

namespace GrimmLib
{
	// Token: 0x02000017 RID: 23
	public class ImmediateNode : DialogueNode
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x000068FC File Offset: 0x00004AFC
		public override void OnEnter()
		{
			base.Stop();
			base.StartNextNode();
		}
	}
}
