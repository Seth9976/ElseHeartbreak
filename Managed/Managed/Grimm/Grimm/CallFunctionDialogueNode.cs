using System;
using GameTypes;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x0200001B RID: 27
	public class CallFunctionDialogueNode : DialogueNode
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00007B6C File Offset: 0x00005D6C
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_function = base.EnsureCell<string>("function", "undefined");
			this.CELL_args = base.EnsureCell<string[]>("args", new string[0]);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007BAC File Offset: 0x00005DAC
		public override void OnEnter()
		{
			base.Stop();
			try
			{
				this._dialogueRunner.CallFunction(this.function, this.args);
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				string text = string.Concat(new string[] { "Error when calling function from node ", base.name, " in conversation '", base.conversation, "': ", ex.Message, " \nStack trace: ", ex.StackTrace });
				D.Log(text);
				Console.ForegroundColor = ConsoleColor.White;
				if (this._dialogueRunner.onGrimmError != null)
				{
					this._dialogueRunner.onGrimmError(text);
				}
			}
			base.StartNextNode();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00007C88 File Offset: 0x00005E88
		public override string ToString()
		{
			return string.Format("[CallFunctionDialogueNode: function={0}, args={1}, conversation={2}]", this.function, this.args, base.conversation);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00007CB4 File Offset: 0x00005EB4
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00007CC4 File Offset: 0x00005EC4
		public string function
		{
			get
			{
				return this.CELL_function.data;
			}
			set
			{
				this.CELL_function.data = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00007CD4 File Offset: 0x00005ED4
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00007CE4 File Offset: 0x00005EE4
		public string[] args
		{
			get
			{
				return this.CELL_args.data;
			}
			set
			{
				this.CELL_args.data = value;
			}
		}

		// Token: 0x0400007B RID: 123
		private ValueEntry<string> CELL_function;

		// Token: 0x0400007C RID: 124
		private ValueEntry<string[]> CELL_args;
	}
}
