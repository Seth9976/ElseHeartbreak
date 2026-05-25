using System;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x02000021 RID: 33
	public class CancelDialogueNode : DialogueNode
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00008178 File Offset: 0x00006378
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_handle = base.EnsureCell<string>("handle", "");
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008198 File Offset: 0x00006398
		public override void Update(float dt)
		{
			base.Stop();
			this._dialogueRunner.CancelRegisteredNode(base.conversation, this.handle);
			base.StartNextNode();
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000081C8 File Offset: 0x000063C8
		// (set) Token: 0x0600015F RID: 351 RVA: 0x000081D8 File Offset: 0x000063D8
		public string handle
		{
			get
			{
				return this.CELL_handle.data;
			}
			set
			{
				this.CELL_handle.data = value;
			}
		}

		// Token: 0x04000085 RID: 133
		private ValueEntry<string> CELL_handle;
	}
}
