using System;
using System.Collections.Generic;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200002F RID: 47
	public class MemorySpaceNodeListCache
	{
		// Token: 0x06000189 RID: 393 RVA: 0x0000B264 File Offset: 0x00009464
		public bool hasCachedFunction(AST rootNode)
		{
			return this.m_lists.ContainsKey(rootNode);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000B274 File Offset: 0x00009474
		public void addMemorySpaceList(List<AST> list, AST rootNode)
		{
			this.m_lists.Add(rootNode, list);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000B284 File Offset: 0x00009484
		public List<AST> getList(AST rootNode)
		{
			return this.m_lists[rootNode];
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000B294 File Offset: 0x00009494
		public void clear()
		{
			this.m_lists.Clear();
		}

		// Token: 0x040000C8 RID: 200
		private Dictionary<AST, List<AST>> m_lists = new Dictionary<AST, List<AST>>();
	}
}
