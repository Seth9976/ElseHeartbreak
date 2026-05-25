using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000029 RID: 41
	public class MemorySpace
	{
		// Token: 0x06000165 RID: 357 RVA: 0x0000AC9C File Offset: 0x00008E9C
		public MemorySpace(string name, AST root, Scope scope, MemorySpaceNodeListCache cache)
		{
			Debug.Assert(name != null);
			Debug.Assert(root != null);
			Debug.Assert(scope != null);
			Debug.Assert(cache != null);
			this.m_name = name;
			this.m_scope = scope;
			this.m_cache = cache;
			if (this.m_cache.hasCachedFunction(root))
			{
				this.m_nodes = this.m_cache.getList(root).ToArray();
			}
			else
			{
				List<AST> list = new List<AST>();
				this.addToList(list, root);
				this.m_cache.addMemorySpaceList(list, root);
				this.m_nodes = list.ToArray();
			}
			this.m_currentNode = -1;
			MemorySpace.nrOfMemorySpacesInMemory++;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000AD70 File Offset: 0x00008F70
		~MemorySpace()
		{
			MemorySpace.nrOfMemorySpacesInMemory--;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		private void addToList(List<AST> list, AST ast)
		{
			Token.TokenType tokenType = ast.getTokenType();
			switch (tokenType)
			{
			case Token.TokenType.IF:
				this.addToList(list, ast.getChild(0));
				list.Add(ast);
				break;
			case Token.TokenType.LOOP:
				list.Add(ast);
				break;
			default:
				if (tokenType != Token.TokenType.FUNC_DECLARATION)
				{
					this.addChildren(list, ast);
					list.Add(ast);
				}
				else
				{
					this.addToList(list, ast.getChild(2));
					this.addToList(list, ast.getChild(3));
				}
				break;
			case Token.TokenType.LOOP_BLOCK:
				list.Add(ast);
				break;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000AE58 File Offset: 0x00009058
		private void addChildren(List<AST> list, AST ast)
		{
			List<AST> children = ast.getChildren();
			if (children != null)
			{
				foreach (AST ast2 in children)
				{
					this.addToList(list, ast2);
				}
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000AECC File Offset: 0x000090CC
		public void setValue(string name, object val)
		{
			Debug.Assert(name != null);
			Debug.Assert(val != null);
			if (this.m_valuesForStrings.ContainsKey(name))
			{
				this.m_valuesForStrings[name] = val;
			}
			else
			{
				this.m_valuesForStrings.Add(name, val);
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000AF24 File Offset: 0x00009124
		public bool hasValue(string name)
		{
			return this.m_valuesForStrings.ContainsKey(name);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000AF34 File Offset: 0x00009134
		public object getValue(string name)
		{
			Debug.Assert(name != null);
			if (!this.m_valuesForStrings.ContainsKey(name))
			{
				throw new Error("Can't find variable with name '" + name + "' (forgot quotes?)");
			}
			return this.m_valuesForStrings[name];
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000AF80 File Offset: 0x00009180
		public void PrintValues()
		{
			foreach (string text in this.m_valuesForStrings.Keys)
			{
				Console.WriteLine("\t\t" + text + " = " + this.m_valuesForStrings[text].ToString());
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000B00C File Offset: 0x0000920C
		public string getName()
		{
			return this.m_name;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000B014 File Offset: 0x00009214
		public AST CurrentNode
		{
			get
			{
				return this.m_nodes[this.m_currentNode];
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B024 File Offset: 0x00009224
		public bool Next()
		{
			if (this.m_currentNode < this.m_nodes.Length - 1)
			{
				this.m_currentNode++;
				return true;
			}
			return false;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000B058 File Offset: 0x00009258
		public void MoveToStart()
		{
			this.m_currentNode = -1;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000B064 File Offset: 0x00009264
		public void MoveToEnd()
		{
			this.m_currentNode = this.m_nodes.Length;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000B074 File Offset: 0x00009274
		public void Jump(int steps)
		{
			this.m_currentNode += steps;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000B084 File Offset: 0x00009284
		public void SetCurrentNode()
		{
			this.m_currentNode = this.m_nodes.Length;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000B094 File Offset: 0x00009294
		public Scope Scope
		{
			get
			{
				return this.m_scope;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000B09C File Offset: 0x0000929C
		public void TraceParentScopes()
		{
			Console.Write("Parent scopes of " + this.getName() + ": ");
			for (Scope scope = this.m_scope; scope != null; scope = scope.getEnclosingScope())
			{
				Console.Write(scope.getName() + ", ");
			}
			Console.WriteLine("");
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000B0FC File Offset: 0x000092FC
		public void Delete()
		{
			this.m_name = "";
			this.m_valuesForStrings = null;
			this.m_nodes = null;
			this.m_currentNode = -1;
			this.m_scope = null;
			this.m_cache = null;
		}

		// Token: 0x040000BA RID: 186
		public static int nrOfMemorySpacesInMemory = 0;

		// Token: 0x040000BB RID: 187
		private string m_name;

		// Token: 0x040000BC RID: 188
		private Dictionary<string, object> m_valuesForStrings = new Dictionary<string, object>();

		// Token: 0x040000BD RID: 189
		private AST[] m_nodes;

		// Token: 0x040000BE RID: 190
		private int m_currentNode;

		// Token: 0x040000BF RID: 191
		private Scope m_scope;

		// Token: 0x040000C0 RID: 192
		private MemorySpaceNodeListCache m_cache;
	}
}
