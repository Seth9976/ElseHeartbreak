using System;
using System.Collections.Generic;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x02000016 RID: 22
	public class IfDialogueNode : DialogueNode
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00006668 File Offset: 0x00004868
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_ifTrueNode = base.EnsureCell<string>("ifTrueNode", "");
			this.CELL_elifNodes = base.EnsureCell<string[]>("elifNode", new string[0]);
			this.CELL_ifFalseNode = base.EnsureCell<string>("ifFalseNode", "");
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000066C0 File Offset: 0x000048C0
		public override void OnEnter()
		{
			string nextNode = base.nextNode;
			bool flag = false;
			if (this.ifTrueNode.Evaluate())
			{
				flag = true;
				base.nextNode = this.ifTrueNode.nextNode;
			}
			if (!flag)
			{
				foreach (ExpressionDialogueNode expressionDialogueNode in this.elifNodes)
				{
					if (expressionDialogueNode.Evaluate())
					{
						flag = true;
						base.nextNode = expressionDialogueNode.nextNode;
					}
				}
			}
			if (!flag && this.ifFalseNode != null)
			{
				base.nextNode = this.ifFalseNode.nextNode;
			}
			base.Stop();
			base.StartNextNode();
			base.nextNode = nextNode;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00006770 File Offset: 0x00004970
		// (set) Token: 0x060000EF RID: 239 RVA: 0x000067BC File Offset: 0x000049BC
		public ExpressionDialogueNode ifTrueNode
		{
			get
			{
				if (this.CELL_ifTrueNode.data != "")
				{
					return this._dialogueRunner.GetDialogueNode(base.conversation, this.CELL_ifTrueNode.data) as ExpressionDialogueNode;
				}
				return null;
			}
			set
			{
				this.CELL_ifTrueNode.data = ((value == null) ? "" : value.name);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000067E0 File Offset: 0x000049E0
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000683C File Offset: 0x00004A3C
		public ExpressionDialogueNode[] elifNodes
		{
			get
			{
				List<ExpressionDialogueNode> list = new List<ExpressionDialogueNode>();
				foreach (string text in this.CELL_elifNodes.data)
				{
					list.Add(this._dialogueRunner.GetDialogueNode(base.conversation, text) as ExpressionDialogueNode);
				}
				return list.ToArray();
			}
			set
			{
				List<string> list = new List<string>();
				for (int i = 0; i < value.Length; i++)
				{
					ExpressionDialogueNode expressionDialogueNode = value[i];
					list.Add(expressionDialogueNode.name);
				}
				this.CELL_elifNodes.data = list.ToArray();
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00006888 File Offset: 0x00004A88
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000068D0 File Offset: 0x00004AD0
		public DialogueNode ifFalseNode
		{
			get
			{
				if (this.CELL_ifFalseNode.data != "")
				{
					return this._dialogueRunner.GetDialogueNode(base.conversation, this.CELL_ifFalseNode.data);
				}
				return null;
			}
			set
			{
				this.CELL_ifFalseNode.data = ((value == null) ? "" : value.name);
			}
		}

		// Token: 0x0400006C RID: 108
		private ValueEntry<string> CELL_ifTrueNode;

		// Token: 0x0400006D RID: 109
		private ValueEntry<string[]> CELL_elifNodes;

		// Token: 0x0400006E RID: 110
		private ValueEntry<string> CELL_ifFalseNode;
	}
}
