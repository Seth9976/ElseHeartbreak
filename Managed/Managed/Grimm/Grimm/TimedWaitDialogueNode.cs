using System;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x02000029 RID: 41
	public class TimedWaitDialogueNode : DialogueNode
	{
		// Token: 0x06000187 RID: 391 RVA: 0x000084D0 File Offset: 0x000066D0
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_timer = base.EnsureCell<float>("timer", 1f);
			this.CELL_timerStartValue = base.EnsureCell<float>("timerStartValue", this.CELL_timer.data);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008518 File Offset: 0x00006718
		public override void OnExit()
		{
			this.timer = this.timerStartValue;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00008528 File Offset: 0x00006728
		public override void Update(float dt)
		{
			if (this.timer > 0f)
			{
				this.timer -= dt;
				if (this.timer <= 0f)
				{
					base.Stop();
					base.StartNextNode();
				}
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00008570 File Offset: 0x00006770
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00008580 File Offset: 0x00006780
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00008590 File Offset: 0x00006790
		// (set) Token: 0x0600018D RID: 397 RVA: 0x000085A0 File Offset: 0x000067A0
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

		// Token: 0x0400008B RID: 139
		private ValueEntry<float> CELL_timer;

		// Token: 0x0400008C RID: 140
		private ValueEntry<float> CELL_timerStartValue;
	}
}
