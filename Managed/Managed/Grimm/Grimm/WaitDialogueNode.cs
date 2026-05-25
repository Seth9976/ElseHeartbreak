using System;
using System.Collections.Generic;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x0200001A RID: 26
	public class WaitDialogueNode : DialogueNode, IRegisteredDialogueNode
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000077E4 File Offset: 0x000059E4
		public string ScopeNode()
		{
			return base.scopeNode;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000077EC File Offset: 0x000059EC
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_hasBranch = base.EnsureCell<bool>("hasBranch", false);
			this.CELL_branchNode = base.EnsureCell<string>("branchNode", "undefined");
			this.CELL_handle = base.EnsureCell<string>("handle", "");
			this.CELL_isListening = base.EnsureCell<bool>("isListening", false);
			this.CELL_expressions = base.EnsureCell<string[]>("expressions", new string[0]);
			this.CELL_eventName = base.EnsureCell<string>("eventName", "");
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000787C File Offset: 0x00005A7C
		public override void OnEnter()
		{
			if (this.hasBranch)
			{
				base.StartNextNode();
			}
			this.isListening = true;
			if (this.eventName == "")
			{
				this.Evaluate();
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000078BC File Offset: 0x00005ABC
		public override void Update(float dt)
		{
			if (this.eventName != "")
			{
				return;
			}
			if (this.isListening)
			{
				this.Evaluate();
			}
			else
			{
				base.Stop();
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000078FC File Offset: 0x00005AFC
		public override string ToString()
		{
			return string.Format("[WaitDialogueNode: hasBranch={0}, branchNode={1}, handle={2}, isListening={3}, eventName={4}, conversation={5}]", new object[] { this.hasBranch, this.branchNode, this.handle, this.isListening, this.eventName, base.conversation });
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000795C File Offset: 0x00005B5C
		private void Evaluate()
		{
			foreach (ExpressionDialogueNode expressionDialogueNode in this.expressions)
			{
				if (!expressionDialogueNode.Evaluate())
				{
					return;
				}
			}
			this.isListening = false;
			base.Stop();
			if (this.hasBranch)
			{
				this._dialogueRunner.GetDialogueNode(base.conversation, this.branchNode).Start();
			}
			else
			{
				base.StartNextNode();
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000079D4 File Offset: 0x00005BD4
		public void EventHappened()
		{
			this.Evaluate();
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000079DC File Offset: 0x00005BDC
		// (set) Token: 0x06000120 RID: 288 RVA: 0x000079EC File Offset: 0x00005BEC
		public bool hasBranch
		{
			get
			{
				return this.CELL_hasBranch.data;
			}
			set
			{
				this.CELL_hasBranch.data = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000079FC File Offset: 0x00005BFC
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00007A0C File Offset: 0x00005C0C
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00007A1C File Offset: 0x00005C1C
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00007A2C File Offset: 0x00005C2C
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00007A3C File Offset: 0x00005C3C
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00007A4C File Offset: 0x00005C4C
		public bool isListening
		{
			get
			{
				return this.CELL_isListening.data;
			}
			set
			{
				this.CELL_isListening.data = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00007A5C File Offset: 0x00005C5C
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00007A6C File Offset: 0x00005C6C
		public string eventName
		{
			get
			{
				return this.CELL_eventName.data;
			}
			set
			{
				this.CELL_eventName.data = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00007A7C File Offset: 0x00005C7C
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public ExpressionDialogueNode[] expressions
		{
			get
			{
				if (this._expressionCACHE == null)
				{
					List<ExpressionDialogueNode> list = new List<ExpressionDialogueNode>();
					foreach (string text in this.CELL_expressions.data)
					{
						list.Add(this._dialogueRunner.GetDialogueNode(base.conversation, text) as ExpressionDialogueNode);
					}
					this._expressionCACHE = list.ToArray();
				}
				return this._expressionCACHE;
			}
			set
			{
				List<string> list = new List<string>();
				for (int i = 0; i < value.Length; i++)
				{
					ExpressionDialogueNode expressionDialogueNode = value[i];
					list.Add(expressionDialogueNode.name);
				}
				this.CELL_expressions.data = list.ToArray();
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00007B3C File Offset: 0x00005D3C
		virtual string GrimmLib.IRegisteredDialogueNode.get_conversation()
		{
			return base.conversation;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007B44 File Offset: 0x00005D44
		virtual void GrimmLib.IRegisteredDialogueNode.set_conversation(string value)
		{
			base.conversation = value;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007B50 File Offset: 0x00005D50
		virtual string GrimmLib.IRegisteredDialogueNode.get_name()
		{
			return base.name;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007B58 File Offset: 0x00005D58
		virtual void GrimmLib.IRegisteredDialogueNode.set_name(string value)
		{
			base.name = value;
		}

		// Token: 0x04000074 RID: 116
		private ValueEntry<string> CELL_branchNode;

		// Token: 0x04000075 RID: 117
		private ValueEntry<bool> CELL_hasBranch;

		// Token: 0x04000076 RID: 118
		private ValueEntry<string> CELL_handle;

		// Token: 0x04000077 RID: 119
		private ValueEntry<bool> CELL_isListening;

		// Token: 0x04000078 RID: 120
		private ValueEntry<string[]> CELL_expressions;

		// Token: 0x04000079 RID: 121
		private ValueEntry<string> CELL_eventName;

		// Token: 0x0400007A RID: 122
		private ExpressionDialogueNode[] _expressionCACHE;
	}
}
