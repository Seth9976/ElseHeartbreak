using System;

namespace GrimmLib
{
	// Token: 0x02000013 RID: 19
	public class UnifiedEndNodeForScope : DialogueNode
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00004A00 File Offset: 0x00002C00
		public override void OnEnter()
		{
			base.Stop();
			base.StartNextNode();
		}
	}
}
