using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000018 RID: 24
	public class ExpressionEvaluator
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00007BCC File Offset: 0x00005DCC
		public ExpressionEvaluator(AST expressionTree)
		{
			this.m_expressionTree = expressionTree;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007BDC File Offset: 0x00005DDC
		public float getValue()
		{
			return this.evaluate(this.m_expressionTree);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007BEC File Offset: 0x00005DEC
		private float evaluate(AST tree)
		{
			float num;
			if (tree.getTokenType() == Token.TokenType.NUMBER)
			{
				num = (float)Convert.ToDouble(tree.getTokenString());
			}
			else
			{
				if (tree.getTokenType() != Token.TokenType.OPERATOR)
				{
					throw new InvalidOperationException("ExpressionEvaluator can't handle tokens of type " + tree.getTokenType());
				}
				if (tree.getTokenString() == "+")
				{
					num = this.evaluate(tree.getChild(0)) + this.evaluate(tree.getChild(1));
				}
				else if (tree.getTokenString() == "-")
				{
					num = this.evaluate(tree.getChild(0)) - this.evaluate(tree.getChild(1));
				}
				else if (tree.getTokenString() == "*")
				{
					num = this.evaluate(tree.getChild(0)) * this.evaluate(tree.getChild(1));
				}
				else if (tree.getTokenString() == "/")
				{
					num = this.evaluate(tree.getChild(0)) / this.evaluate(tree.getChild(1));
				}
				else if (tree.getTokenString() == "<")
				{
					num = (float)((this.evaluate(tree.getChild(0)) >= this.evaluate(tree.getChild(1))) ? 0 : 1);
				}
				else if (tree.getTokenString() == ">")
				{
					num = (float)((this.evaluate(tree.getChild(0)) <= this.evaluate(tree.getChild(1))) ? 0 : 1);
				}
				else if (tree.getTokenString() == "<=")
				{
					num = (float)((this.evaluate(tree.getChild(0)) > this.evaluate(tree.getChild(1))) ? 0 : 1);
				}
				else if (tree.getTokenString() == ">=")
				{
					num = (float)((this.evaluate(tree.getChild(0)) < this.evaluate(tree.getChild(1))) ? 0 : 1);
				}
				else if (tree.getTokenString() == "&&")
				{
					num = (float)((this.evaluate(tree.getChild(0)) == 0f || this.evaluate(tree.getChild(1)) == 0f) ? 0 : 1);
				}
				else if (tree.getTokenString() == "||")
				{
					num = (float)((this.evaluate(tree.getChild(0)) == 0f && this.evaluate(tree.getChild(1)) == 0f) ? 0 : 1);
				}
				else if (tree.getTokenString() == "!=")
				{
					num = (float)((this.evaluate(tree.getChild(0)) == this.evaluate(tree.getChild(1))) ? 0 : 1);
				}
				else
				{
					if (!(tree.getTokenString() == "=="))
					{
						throw new InvalidOperationException("ExpressionEvaluator can't handle operators with string " + tree.getTokenString());
					}
					num = (float)((this.evaluate(tree.getChild(0)) != this.evaluate(tree.getChild(1))) ? 0 : 1);
				}
			}
			return num;
		}

		// Token: 0x04000083 RID: 131
		private AST m_expressionTree;
	}
}
