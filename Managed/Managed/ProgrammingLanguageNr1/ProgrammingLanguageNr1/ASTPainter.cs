using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000017 RID: 23
	public class ASTPainter
	{
		// Token: 0x1700000E RID: 14
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00007780 File Offset: 0x00005980
		public bool PrintExecutions
		{
			set
			{
				this._printExecutions = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000077E0 File Offset: 0x000059E0
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000778C File Offset: 0x0000598C
		public AST currentNode
		{
			get
			{
				return this._currentNode;
			}
			set
			{
				this._currentNode = value;
				this._currentParentNode = this.GetParent(this._currentNode);
				this._currentNodeIsLastChild = this.IsLastChild(this._currentNode, (this._currentParentNode != null) ? this._currentParentNode : this._root);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000077E8 File Offset: 0x000059E8
		public AST currentParentNode
		{
			get
			{
				return this._currentParentNode;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000077F0 File Offset: 0x000059F0
		public bool currentNodeIsLastChild
		{
			get
			{
				return this._currentNodeIsLastChild;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000077F8 File Offset: 0x000059F8
		public void PaintAST(AST pRoot)
		{
			if (pRoot == null)
			{
				throw new Exception("Can't paint pRoot since it's null");
			}
			this._root = pRoot;
			List<AST> list = new List<AST>();
			this.BuildNodeList(pRoot, list);
			this.allNodes = list.ToArray();
			List<AST> list2 = new List<AST>();
			this.GetLeafNodes(pRoot, list2);
			foreach (AST ast in this.allNodes)
			{
				Console.WriteLine(this.BuildRow(ast, pRoot));
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007874 File Offset: 0x00005A74
		private void GetLeafNodes(AST pCurrentNode, List<AST> pList)
		{
			List<AST> children = pCurrentNode.getChildren();
			if (children == null || children.Count <= 0)
			{
				pList.Add(pCurrentNode);
			}
			else
			{
				foreach (AST ast in children)
				{
					this.GetLeafNodes(ast, pList);
				}
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007900 File Offset: 0x00005B00
		private string GetNodeString(AST pNode)
		{
			string text = pNode.getTokenType().ToString() + " : " + pNode.ToString();
			if (this._printExecutions)
			{
				text = text + " : " + pNode.Executions;
			}
			return text;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007954 File Offset: 0x00005B54
		private string BuildRow(AST pLeaf, AST pRoot)
		{
			this.currentNode = pLeaf;
			StringBuilder stringBuilder = new StringBuilder();
			if (this.currentNode == this._root)
			{
				this.PrependString(stringBuilder, this.GetNodeString(this.currentNode));
			}
			else if (this.currentNode.getChildren().Count <= 0)
			{
				this.PrependString(stringBuilder, "-------- " + this.GetNodeString(this.currentNode));
			}
			else
			{
				this.PrependString(stringBuilder, "-------- " + this.GetNodeString(this.currentNode));
			}
			if (this.currentNodeIsLastChild)
			{
				this.PrependString(stringBuilder, "      |");
			}
			else
			{
				this.PrependString(stringBuilder, "      |");
			}
			this.currentNode = this.currentParentNode;
			while (this.currentParentNode != null)
			{
				if (this.currentNodeIsLastChild)
				{
					this.PrependString(stringBuilder, "       ");
				}
				else
				{
					this.PrependString(stringBuilder, "      |");
				}
				this.currentNode = this.currentParentNode;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007A6C File Offset: 0x00005C6C
		private void PrependString(StringBuilder pStringBuilder, string pString)
		{
			pStringBuilder.Insert(0, pString);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007A78 File Offset: 0x00005C78
		private AST GetParent(AST pLeaf)
		{
			foreach (AST ast in this.allNodes)
			{
				foreach (AST ast2 in ast.getChildren())
				{
					if (ast2 == pLeaf)
					{
						return ast;
					}
				}
			}
			return null;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007B10 File Offset: 0x00005D10
		private void BuildNodeList(AST pCurrentList, List<AST> pList)
		{
			if (pCurrentList == null)
			{
				Console.WriteLine("Can't paint node, will return");
				return;
			}
			pList.Add(pCurrentList);
			foreach (AST ast in pCurrentList.getChildren())
			{
				this.BuildNodeList(ast, pList);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007B94 File Offset: 0x00005D94
		private bool IsLastChild(AST pNode, AST pParent)
		{
			return pParent == null || pParent.getChildren().IndexOf(pNode) == pParent.getChildren().Count - 1;
		}

		// Token: 0x0400007D RID: 125
		private AST _root;

		// Token: 0x0400007E RID: 126
		private AST[] allNodes;

		// Token: 0x0400007F RID: 127
		private AST _currentNode;

		// Token: 0x04000080 RID: 128
		private AST _currentParentNode;

		// Token: 0x04000081 RID: 129
		private bool _currentNodeIsLastChild = true;

		// Token: 0x04000082 RID: 130
		private bool _printExecutions;
	}
}
