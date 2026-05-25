using System;
using System.Collections.Generic;
using GameTypes;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x0200000E RID: 14
	public class BranchingDialogueNode : DialogueNode
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000042E0 File Offset: 0x000024E0
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_nextNodes = base.EnsureCell<string[]>("nextNodes", new string[0]);
			this.CELL_unifiedEndNodeForScope = base.EnsureCell<string>("unifiedEndNodeForScope", "");
			this.CELL_eternal = base.EnsureCell<bool>("eternal", false);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004334 File Offset: 0x00002534
		public override void Update(float dt)
		{
			if (base.nextNode != "")
			{
				base.Stop();
				base.StartNextNode();
				base.nextNode = "";
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004370 File Offset: 0x00002570
		public void Choose(int pOptionNr)
		{
			D.assert(pOptionNr >= 0);
			D.assert(pOptionNr < this.nextNodes.Length);
			string text = this.nextNodes[pOptionNr];
			if (!this.eternal && this.nextNodes.Length > 1)
			{
				this.RemoveOptionFromNextNodes(pOptionNr);
			}
			base.nextNode = text;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000043CC File Offset: 0x000025CC
		private void RemoveOptionFromNextNodes(int pOptionNr)
		{
			string[] data = this.CELL_nextNodes.data;
			List<string> list = new List<string>();
			for (int i = 0; i < data.Length; i++)
			{
				if (i != pOptionNr)
				{
					list.Add(data[i]);
				}
			}
			this.CELL_nextNodes.data = list.ToArray();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00004428 File Offset: 0x00002628
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00004438 File Offset: 0x00002638
		public string[] nextNodes
		{
			get
			{
				return this.CELL_nextNodes.data;
			}
			set
			{
				this.CELL_nextNodes.data = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004448 File Offset: 0x00002648
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00004458 File Offset: 0x00002658
		public string unifiedEndNodeForScope
		{
			get
			{
				return this.CELL_unifiedEndNodeForScope.data;
			}
			set
			{
				this.CELL_unifiedEndNodeForScope.data = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004468 File Offset: 0x00002668
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00004478 File Offset: 0x00002678
		public bool eternal
		{
			get
			{
				return this.CELL_eternal.data;
			}
			set
			{
				this.CELL_eternal.data = value;
			}
		}

		// Token: 0x0400004C RID: 76
		private ValueEntry<string[]> CELL_nextNodes;

		// Token: 0x0400004D RID: 77
		private ValueEntry<string> CELL_unifiedEndNodeForScope;

		// Token: 0x0400004E RID: 78
		private ValueEntry<bool> CELL_eternal;
	}
}
