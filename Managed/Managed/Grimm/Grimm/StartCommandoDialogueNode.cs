using System;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x02000019 RID: 25
	public class StartCommandoDialogueNode : DialogueNode
	{
		// Token: 0x06000113 RID: 275 RVA: 0x0000777C File Offset: 0x0000597C
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_commando = base.EnsureCell<string>("commando", "undefined");
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000779C File Offset: 0x0000599C
		public override void OnEnter()
		{
			base.Stop();
			this._dialogueRunner.StartConversation(this.commando);
			base.StartNextNode();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000115 RID: 277 RVA: 0x000077BC File Offset: 0x000059BC
		// (set) Token: 0x06000116 RID: 278 RVA: 0x000077CC File Offset: 0x000059CC
		public string commando
		{
			get
			{
				return this.CELL_commando.data;
			}
			set
			{
				this.CELL_commando.data = value;
			}
		}

		// Token: 0x04000073 RID: 115
		private ValueEntry<string> CELL_commando;
	}
}
