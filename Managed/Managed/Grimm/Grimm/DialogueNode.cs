using System;
using GameTypes;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x02000011 RID: 17
	public abstract class DialogueNode : RelayObjectTwo
	{
		// Token: 0x06000094 RID: 148 RVA: 0x000044CC File Offset: 0x000026CC
		public void SetRunner(DialogueRunner pRunner)
		{
			this._dialogueRunner = pRunner;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000044D8 File Offset: 0x000026D8
		protected void Invariant()
		{
			D.assert(this._dialogueRunner != null);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000044EC File Offset: 0x000026EC
		protected override void SetupCells()
		{
			this.CELL_name = base.EnsureCell<string>("name", "unnamed");
			this.CELL_isOn = base.EnsureCell<bool>("isOn", false);
			this.CELL_nextNode = base.EnsureCell<string>("nextNode", "");
			this.CELL_conversation = base.EnsureCell<string>("conversation", "");
			this.CELL_language = base.EnsureCell<Language>("language", Language.SWEDISH);
			this.CELL_scopeNode = base.EnsureCell<string>("scopeNode", "");
			this._isOnCache = this.CELL_isOn.data;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004588 File Offset: 0x00002788
		public void Start()
		{
			this.Invariant();
			this.isOn = true;
			this.OnEnter();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000045A0 File Offset: 0x000027A0
		public void Stop()
		{
			this.Invariant();
			this.isOn = false;
			this.OnExit();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000045B8 File Offset: 0x000027B8
		protected void StartNextNode()
		{
			if (this.nextNode == "")
			{
				throw new GrimmException(string.Concat(new string[] { "No nextNode in dialogue node '", this.name, "' in conversation '", this.conversation, "'" }));
			}
			DialogueNode dialogueNode = this._dialogueRunner.GetDialogueNode(this.conversation, this.nextNode);
			dialogueNode.Start();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004634 File Offset: 0x00002834
		public virtual void OnEnter()
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004638 File Offset: 0x00002838
		public virtual void OnExit()
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000463C File Offset: 0x0000283C
		public virtual void Update(float dt)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004640 File Offset: 0x00002840
		public override string ToString()
		{
			return string.Format("[DialogueNode: name={0}, isOn={1}, nextNode={2}, conversation={3}, scopeNode={4}]", new object[] { this.name, this.isOn, this.nextNode, this.conversation, this.scopeNode });
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004690 File Offset: 0x00002890
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000046A0 File Offset: 0x000028A0
		public string name
		{
			get
			{
				return this.CELL_name.data;
			}
			set
			{
				this.CELL_name.data = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000046B0 File Offset: 0x000028B0
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000046B8 File Offset: 0x000028B8
		public bool isOn
		{
			get
			{
				return this._isOnCache;
			}
			private set
			{
				this._isOnCache = value;
				this.CELL_isOn.data = value;
				if (value)
				{
					this._dialogueRunner.AddToTurnOnNodeList(this);
				}
				else
				{
					this._dialogueRunner.AddToTurnOffNodeList(this);
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000046FC File Offset: 0x000028FC
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000470C File Offset: 0x0000290C
		public string nextNode
		{
			get
			{
				return this.CELL_nextNode.data;
			}
			set
			{
				this.CELL_nextNode.data = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000471C File Offset: 0x0000291C
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x0000472C File Offset: 0x0000292C
		public string conversation
		{
			get
			{
				return this.CELL_conversation.data;
			}
			set
			{
				this.CELL_conversation.data = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000473C File Offset: 0x0000293C
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x0000474C File Offset: 0x0000294C
		public string scopeNode
		{
			get
			{
				return this.CELL_scopeNode.data;
			}
			set
			{
				this.CELL_scopeNode.data = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000475C File Offset: 0x0000295C
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000476C File Offset: 0x0000296C
		public Language language
		{
			get
			{
				return this.CELL_language.data;
			}
			set
			{
				this.CELL_language.data = value;
			}
		}

		// Token: 0x0400004F RID: 79
		public const string TABLE_NAME = "Dialogues";

		// Token: 0x04000050 RID: 80
		protected DialogueRunner _dialogueRunner;

		// Token: 0x04000051 RID: 81
		private ValueEntry<string> CELL_name;

		// Token: 0x04000052 RID: 82
		private ValueEntry<bool> CELL_isOn;

		// Token: 0x04000053 RID: 83
		private ValueEntry<string> CELL_conversation;

		// Token: 0x04000054 RID: 84
		private ValueEntry<Language> CELL_language;

		// Token: 0x04000055 RID: 85
		private ValueEntry<string> CELL_nextNode;

		// Token: 0x04000056 RID: 86
		private ValueEntry<string> CELL_scopeNode;

		// Token: 0x04000057 RID: 87
		private bool _isOnCache;
	}
}
