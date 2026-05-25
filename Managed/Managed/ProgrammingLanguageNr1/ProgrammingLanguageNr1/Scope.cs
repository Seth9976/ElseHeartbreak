using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200001C RID: 28
	public class Scope
	{
		// Token: 0x060000FC RID: 252 RVA: 0x000089BC File Offset: 0x00006BBC
		public Scope(Scope.ScopeType scopeType, string name)
		{
			this.m_scopeType = scopeType;
			this.m_name = name;
			Scope.nrOfScopesInMemory++;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00008A00 File Offset: 0x00006C00
		public Scope(Scope.ScopeType scopeType, string name, Scope enclosingScope)
		{
			Debug.Assert(name != null && name != "");
			Debug.Assert(enclosingScope != null);
			this.m_scopeType = scopeType;
			this.m_name = name;
			this.m_enclosingScope = enclosingScope;
			Scope.nrOfScopesInMemory++;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00008A78 File Offset: 0x00006C78
		~Scope()
		{
			Scope.nrOfScopesInMemory--;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00008ABC File Offset: 0x00006CBC
		public virtual Scope getEnclosingScope()
		{
			return this.m_enclosingScope;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008AC4 File Offset: 0x00006CC4
		public virtual string getName()
		{
			return this.m_name;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00008ACC File Offset: 0x00006CCC
		public Symbol resolve(string name)
		{
			if (this.m_symbols.ContainsKey(name))
			{
				return this.m_symbols[name];
			}
			if (this.m_enclosingScope != null)
			{
				return this.m_enclosingScope.resolve(name);
			}
			return null;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00008B10 File Offset: 0x00006D10
		public Scope resolveToScope(string name)
		{
			if (this.m_symbols.ContainsKey(name))
			{
				return this;
			}
			if (this.m_enclosingScope != null)
			{
				return this.m_enclosingScope.resolveToScope(name);
			}
			return null;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00008B4C File Offset: 0x00006D4C
		public virtual void define(Symbol symbol)
		{
			if (this.m_symbols.ContainsKey(symbol.getName()))
			{
				throw new Error("Program already contains a symbol called " + symbol.getName());
			}
			this.m_symbols.Add(symbol.getName(), symbol);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00008B98 File Offset: 0x00006D98
		public virtual bool isDefined(string symbolName)
		{
			return this.m_symbols.ContainsKey(symbolName);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008BA8 File Offset: 0x00006DA8
		public override string ToString()
		{
			return this.m_name;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00008BB0 File Offset: 0x00006DB0
		public void PushMemorySpace(MemorySpace pMemorySpace)
		{
			if (this.m_memorySpaces.Count > 0 && this.m_memorySpaces.Peek() == pMemorySpace)
			{
				return;
			}
			this.m_memorySpaces.Push(pMemorySpace);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008BE4 File Offset: 0x00006DE4
		public MemorySpace PopMemorySpace()
		{
			return this.m_memorySpaces.Pop();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00008BF4 File Offset: 0x00006DF4
		public void setValue(string name, object val)
		{
			Scope scope = this.resolveToScope(name);
			if (scope == null)
			{
				throw new Exception(string.Concat(new object[] { "scope is null, trying to set '", name, "' to value ", val, " from scope '", this.m_name, "'" }));
			}
			if (scope.m_memorySpaces == null)
			{
				throw new Exception("scope.m_memorySpaces is null, trying to set " + name + " from scope " + this.m_name);
			}
			if (scope.m_memorySpaces.Peek() == null)
			{
				throw new Exception("scope.m_memorySpaces.Peek() is null, trying to set " + name + " from scope " + this.m_name);
			}
			scope.m_memorySpaces.Peek().setValue(name, val);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00008CB8 File Offset: 0x00006EB8
		public object getValue(string name)
		{
			Scope scope = this.resolveToScope(name);
			Debug.Assert(scope != null, "Can't resolve scope " + name);
			Stack<MemorySpace> memorySpaces = scope.m_memorySpaces;
			if (memorySpaces == null)
			{
				throw new Exception("memorySpaceStack is null");
			}
			MemorySpace memorySpace = memorySpaces.Peek();
			if (memorySpace == null)
			{
				throw new Exception("memorySpace is null");
			}
			if (!memorySpace.hasValue(name))
			{
				throw new Error("Can't access '" + name + "' (calling function too early?)");
			}
			return memorySpace.getValue(name);
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00008D40 File Offset: 0x00006F40
		public Scope.ScopeType scopeType
		{
			get
			{
				return this.m_scopeType;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00008D48 File Offset: 0x00006F48
		public Stack<MemorySpace> memorySpaces
		{
			get
			{
				return this.m_memorySpaces;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00008D50 File Offset: 0x00006F50
		internal void ClearMemorySpaces()
		{
			this.m_memorySpaces.Clear();
		}

		// Token: 0x0400008C RID: 140
		public static int nrOfScopesInMemory = 0;

		// Token: 0x0400008D RID: 141
		protected Dictionary<string, Symbol> m_symbols = new Dictionary<string, Symbol>();

		// Token: 0x0400008E RID: 142
		protected Scope m_enclosingScope;

		// Token: 0x0400008F RID: 143
		protected string m_name;

		// Token: 0x04000090 RID: 144
		private Stack<MemorySpace> m_memorySpaces = new Stack<MemorySpace>();

		// Token: 0x04000091 RID: 145
		private Scope.ScopeType m_scopeType;

		// Token: 0x0200001D RID: 29
		public enum ScopeType
		{
			// Token: 0x04000093 RID: 147
			MAIN_SCOPE,
			// Token: 0x04000094 RID: 148
			FUNCTION_SCOPE,
			// Token: 0x04000095 RID: 149
			LOOP_SCOPE,
			// Token: 0x04000096 RID: 150
			LOOP_BLOCK_SCOPE,
			// Token: 0x04000097 RID: 151
			IF_SCOPE
		}
	}
}
