using System;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x02000024 RID: 36
	public class LoopDialogueNode : DialogueNode
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00008240 File Offset: 0x00006440
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_branchNode = base.EnsureCell<string>("branchNode", "undefined");
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008260 File Offset: 0x00006460
		public override void Update(float dt)
		{
			if (this._branchNodeCache == null)
			{
				this._branchNodeCache = this._dialogueRunner.GetDialogueNode(base.conversation, this.branchNode);
			}
			this._branchNodeCache.Start();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000082A0 File Offset: 0x000064A0
		public void Break()
		{
			base.Stop();
			this._dialogueRunner.ScopeEnded(base.conversation, base.name);
			base.StartNextNode();
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000082D0 File Offset: 0x000064D0
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000082E0 File Offset: 0x000064E0
		public string branchNode
		{
			get
			{
				return this.CELL_branchNode.data;
			}
			set
			{
				this.CELL_branchNode.data = value;
			}
		}

		// Token: 0x04000086 RID: 134
		private ValueEntry<string> CELL_branchNode;

		// Token: 0x04000087 RID: 135
		private DialogueNode _branchNodeCache;
	}
}
