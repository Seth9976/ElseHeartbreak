using System;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x02000015 RID: 21
	public class GotoDialogueNode : DialogueNode
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000065EC File Offset: 0x000047EC
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_linkedNode = base.EnsureCell<string>("linkedNode", "");
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000660C File Offset: 0x0000480C
		public override void Update(float dt)
		{
			string nextNode = base.nextNode;
			base.nextNode = this.linkedNode;
			base.Stop();
			base.StartNextNode();
			base.nextNode = nextNode;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00006640 File Offset: 0x00004840
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00006650 File Offset: 0x00004850
		public string linkedNode
		{
			get
			{
				return this.CELL_linkedNode.data;
			}
			set
			{
				this.CELL_linkedNode.data = value;
			}
		}

		// Token: 0x0400006B RID: 107
		private ValueEntry<string> CELL_linkedNode;
	}
}
