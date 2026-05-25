using System;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x02000012 RID: 18
	public class TimedDialogueNode : DialogueNode
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00004790 File Offset: 0x00002990
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_timer = base.EnsureCell<float>("timer", 2f);
			this.CELL_timerStartValue = base.EnsureCell<float>("timerStartValue", this.CELL_timer.data);
			this.CELL_speaker = base.EnsureCell<string>("speaker", "unknown");
			this.CELL_line = base.EnsureCell<string>("line", "");
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004804 File Offset: 0x00002A04
		public void CalculateAndSetTimeBasedOnLineLength(bool isOptionNode)
		{
			float num = ((!isOptionNode) ? 1.3f : 0.8f);
			float num2 = ((!isOptionNode) ? 0.04f : 0.02f);
			float num3 = num + (float)this.line.Length * num2;
			this.timer = num3;
			this.timerStartValue = num3;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000485C File Offset: 0x00002A5C
		public override void OnEnter()
		{
			this._dialogueRunner.SomeoneSaidSomething(new Speech(base.conversation, base.name, this.speaker, this.line));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004894 File Offset: 0x00002A94
		public override void OnExit()
		{
			this._dialogueRunner.SomeoneSaidSomething(new Speech(base.conversation, base.name, this.speaker, ""));
			this.timer = this.timerStartValue;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000048D4 File Offset: 0x00002AD4
		public override void Update(float dt)
		{
			if (this.timer > 0f)
			{
				this.timer -= dt * TimedDialogueNode.speedScaling;
				if (this.timer <= 0f)
				{
					base.Stop();
					base.StartNextNode();
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004924 File Offset: 0x00002B24
		public override string ToString()
		{
			return string.Format("[TimedDialogueNode: timer={0}, timerStartValue={1}, speaker={2}, line={3}, conversionat = {4}]", new object[] { this.timer, this.timerStartValue, this.speaker, this.line, base.conversation });
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004978 File Offset: 0x00002B78
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00004988 File Offset: 0x00002B88
		public float timer
		{
			get
			{
				return this.CELL_timer.data;
			}
			set
			{
				this.CELL_timer.data = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004998 File Offset: 0x00002B98
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000049A8 File Offset: 0x00002BA8
		public float timerStartValue
		{
			get
			{
				return this.CELL_timerStartValue.data;
			}
			set
			{
				this.CELL_timerStartValue.data = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000049B8 File Offset: 0x00002BB8
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000049C8 File Offset: 0x00002BC8
		public string speaker
		{
			get
			{
				return this.CELL_speaker.data;
			}
			set
			{
				this.CELL_speaker.data = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000049D8 File Offset: 0x00002BD8
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000049E8 File Offset: 0x00002BE8
		public string line
		{
			get
			{
				return this.CELL_line.data;
			}
			set
			{
				this.CELL_line.data = value;
			}
		}

		// Token: 0x04000058 RID: 88
		public static float speedScaling = 1f;

		// Token: 0x04000059 RID: 89
		private ValueEntry<float> CELL_timer;

		// Token: 0x0400005A RID: 90
		private ValueEntry<float> CELL_timerStartValue;

		// Token: 0x0400005B RID: 91
		private ValueEntry<string> CELL_speaker;

		// Token: 0x0400005C RID: 92
		private ValueEntry<string> CELL_line;
	}
}
