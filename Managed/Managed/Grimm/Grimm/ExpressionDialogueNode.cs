using System;
using GameTypes;
using RelayLib;

namespace GrimmLib
{
	// Token: 0x0200001D RID: 29
	public class ExpressionDialogueNode : DialogueNode
	{
		// Token: 0x0600013A RID: 314 RVA: 0x00007D94 File Offset: 0x00005F94
		protected override void SetupCells()
		{
			base.SetupCells();
			this.CELL_expression = base.EnsureCell<string>("expression", "undefined");
			this.CELL_args = base.EnsureCell<string[]>("args", new string[0]);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00007DD4 File Offset: 0x00005FD4
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00007DE4 File Offset: 0x00005FE4
		public string expression
		{
			get
			{
				return this.CELL_expression.data;
			}
			set
			{
				this.CELL_expression.data = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00007DF4 File Offset: 0x00005FF4
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00007E04 File Offset: 0x00006004
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

		// Token: 0x0600013F RID: 319 RVA: 0x00007E14 File Offset: 0x00006014
		public bool Evaluate()
		{
			bool flag;
			try
			{
				flag = this._dialogueRunner.EvaluateExpression(this.expression, this.args);
			}
			catch (Exception ex)
			{
				string text = string.Concat(new string[]
				{
					"Error when evaluating expression ",
					this.expression,
					" in ",
					base.conversation,
					" with args: ",
					string.Join(", ", this.args),
					" e: ",
					ex.Message,
					" stack: ",
					ex.StackTrace
				});
				D.Log(text);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0400007D RID: 125
		private ValueEntry<string> CELL_expression;

		// Token: 0x0400007E RID: 126
		private ValueEntry<string[]> CELL_args;
	}
}
