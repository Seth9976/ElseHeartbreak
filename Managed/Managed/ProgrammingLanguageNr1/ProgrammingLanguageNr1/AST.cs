using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200000C RID: 12
	public class AST
	{
		// Token: 0x06000049 RID: 73 RVA: 0x000040F0 File Offset: 0x000022F0
		public AST()
		{
			AST.nrOfASTsInMemory++;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004104 File Offset: 0x00002304
		public AST(Token token)
		{
			this.m_token = token;
			AST.nrOfASTsInMemory++;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004128 File Offset: 0x00002328
		~AST()
		{
			AST.nrOfASTsInMemory--;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000416C File Offset: 0x0000236C
		public Token.TokenType getTokenType()
		{
			if (this.m_token != null)
			{
				return this.m_token.getTokenType();
			}
			return Token.TokenType.NO_TOKEN_TYPE;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004188 File Offset: 0x00002388
		public bool isNil()
		{
			return this.m_token == null;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004194 File Offset: 0x00002394
		public void addChild(AST childTree)
		{
			if (childTree == null && childTree == null)
			{
				throw new Error("Failed to understand source code", Error.ErrorType.SYNTAX, -1, -1);
			}
			this.allocateListIfNecessary();
			this.m_children.Add(childTree);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000041D0 File Offset: 0x000023D0
		public void addChildFirst(AST childTree)
		{
			Debug.Assert(childTree != null);
			this.allocateListIfNecessary();
			this.m_children.Insert(0, childTree);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000041F4 File Offset: 0x000023F4
		public void addChildAtPosition(AST childTree, int pos)
		{
			Debug.Assert(childTree != null);
			this.allocateListIfNecessary();
			this.m_children.Insert(pos, childTree);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00004218 File Offset: 0x00002418
		private void allocateListIfNecessary()
		{
			if (this.m_children == null)
			{
				this.m_children = new List<AST>();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004230 File Offset: 0x00002430
		public void addChild(Token token)
		{
			AST ast = new AST(token);
			this.addChild(ast);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000424C File Offset: 0x0000244C
		public AST findParent(AST ofThisChild)
		{
			Console.WriteLine("Going into " + this.getTokenString());
			if (ofThisChild == null)
			{
				return null;
			}
			if (this.m_children == null)
			{
				return null;
			}
			int num = this.m_children.IndexOf(ofThisChild);
			if (num >= 0)
			{
				Console.WriteLine("Found " + ofThisChild.getTokenString());
				return this;
			}
			foreach (AST ast in this.m_children)
			{
				AST ast2 = ast.findParent(ofThisChild);
				if (ast2 != null)
				{
					return ast2;
				}
			}
			return null;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000431C File Offset: 0x0000251C
		public int getIndexOfChild(AST child)
		{
			if (this.m_children != null)
			{
				return this.m_children.IndexOf(child);
			}
			return -1;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004338 File Offset: 0x00002538
		public AST getChild(int n)
		{
			this.allocateListIfNecessary();
			Debug.Assert(n >= 0);
			Debug.Assert(n < this.m_children.Count);
			AST ast = this.m_children[n];
			Debug.Assert(ast != null);
			return ast;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004384 File Offset: 0x00002584
		public void removeChild(int index)
		{
			this.m_children.RemoveAt(index);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004394 File Offset: 0x00002594
		public List<AST> getChildren()
		{
			this.allocateListIfNecessary();
			return this.m_children;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000043A4 File Offset: 0x000025A4
		public string getTokenString()
		{
			return (this.m_token == null) ? "nil" : this.m_token.getTokenString();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000043D4 File Offset: 0x000025D4
		public Token getToken()
		{
			return this.m_token;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000043DC File Offset: 0x000025DC
		public override string ToString()
		{
			return this.getTokenString();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000043E4 File Offset: 0x000025E4
		public string getTreeAsString()
		{
			if (this.m_children == null || this.m_children.Count == 0)
			{
				return this.getTokenString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (!this.isNil())
			{
				stringBuilder.Append("(");
				stringBuilder.Append(this.getTokenString());
				stringBuilder.Append(" ");
			}
			int num = 0;
			foreach (AST ast in this.m_children)
			{
				if (num > 0)
				{
					stringBuilder.Append(" ");
				}
				num++;
				stringBuilder.Append(ast.getTreeAsString());
			}
			if (!this.isNil())
			{
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000044E0 File Offset: 0x000026E0
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000044E8 File Offset: 0x000026E8
		public int Executions
		{
			get
			{
				return this.m_executions;
			}
			set
			{
				this.m_executions = value;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000044F4 File Offset: 0x000026F4
		public virtual void ClearMemorySpaces()
		{
			if (this.m_children == null)
			{
				return;
			}
			foreach (AST ast in this.m_children)
			{
				if (ast != null)
				{
					ast.ClearMemorySpaces();
				}
			}
		}

		// Token: 0x04000024 RID: 36
		public static int nrOfASTsInMemory = 0;

		// Token: 0x04000025 RID: 37
		private Token m_token;

		// Token: 0x04000026 RID: 38
		private List<AST> m_children;

		// Token: 0x04000027 RID: 39
		private int m_executions;
	}
}
