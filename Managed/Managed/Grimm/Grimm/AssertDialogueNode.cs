using System;

namespace GrimmLib
{
	// Token: 0x0200001C RID: 28
	public class AssertDialogueNode : ExpressionDialogueNode
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00007CFC File Offset: 0x00005EFC
		public override void Update(float dt)
		{
			base.Stop();
			if (!this._dialogueRunner.EvaluateExpression(base.expression, base.args))
			{
				string text = string.Join(", ", base.args);
				throw new GrimmAssertException(string.Concat(new string[] { "Assertion ", base.expression, "(", text, ") failed in conversation '", base.conversation, "'" }));
			}
			base.StartNextNode();
		}
	}
}
