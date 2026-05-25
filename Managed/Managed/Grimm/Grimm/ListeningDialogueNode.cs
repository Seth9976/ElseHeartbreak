using System;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x0200001E RID: 30
	public class ListeningDialogueNode : DialogueNode, IRegisteredDialogueNode
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00007EE8 File Offset: 0x000060E8
		public string ScopeNode()
		{
			return base.scopeNode;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007EF0 File Offset: 0x000060F0
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_eventName = base.EnsureCell<string>("eventName", "undefined");
			this.CELL_hasBranch = base.EnsureCell<bool>("hasBranch", false);
			this.CELL_branchNode = base.EnsureCell<string>("branchNode", "undefined");
			this.CELL_isListening = base.EnsureCell<bool>("isListening", false);
			this.CELL_handle = base.EnsureCell<string>("handle", "");
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007F6C File Offset: 0x0000616C
		public override void OnEnter()
		{
			this.isListening = true;
			if (this.hasBranch)
			{
				base.Stop();
				base.StartNextNode();
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007F8C File Offset: 0x0000618C
		public void EventHappened()
		{
			this.isListening = false;
			if (this.hasBranch)
			{
				DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(base.conversation, this.branchNode);
				dialogueNode.Start();
			}
			else
			{
				base.Stop();
				base.StartNextNode();
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007FDC File Offset: 0x000061DC
		public override string ToString()
		{
			return string.Format("[ListeningDialogueNode: eventName={0}, hasBranch={1}, branchNode={2}, isListening={3}, handle={4}]", new object[] { this.eventName, this.hasBranch, this.branchNode, this.isListening, this.handle });
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00008030 File Offset: 0x00006230
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00008040 File Offset: 0x00006240
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00008050 File Offset: 0x00006250
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00008060 File Offset: 0x00006260
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00008070 File Offset: 0x00006270
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00008080 File Offset: 0x00006280
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00008090 File Offset: 0x00006290
		// (set) Token: 0x0600014D RID: 333 RVA: 0x000080A0 File Offset: 0x000062A0
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000080B0 File Offset: 0x000062B0
		// (set) Token: 0x0600014F RID: 335 RVA: 0x000080C0 File Offset: 0x000062C0
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

		// Token: 0x06000150 RID: 336 RVA: 0x000080D0 File Offset: 0x000062D0
		virtual string GrimmLib.IRegisteredDialogueNode.get_conversation()
		{
			return base.conversation;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000080D8 File Offset: 0x000062D8
		virtual void GrimmLib.IRegisteredDialogueNode.set_conversation(string value)
		{
			base.conversation = value;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000080E4 File Offset: 0x000062E4
		virtual string GrimmLib.IRegisteredDialogueNode.get_name()
		{
			return base.name;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000080EC File Offset: 0x000062EC
		virtual void GrimmLib.IRegisteredDialogueNode.set_name(string value)
		{
			base.name = value;
		}

		// Token: 0x0400007F RID: 127
		private ValueEntry<string> CELL_eventName;

		// Token: 0x04000080 RID: 128
		private ValueEntry<bool> CELL_isListening;

		// Token: 0x04000081 RID: 129
		private ValueEntry<string> CELL_branchNode;

		// Token: 0x04000082 RID: 130
		private ValueEntry<bool> CELL_hasBranch;

		// Token: 0x04000083 RID: 131
		private ValueEntry<string> CELL_handle;
	}
}
