using System;
using System.Collections.Generic;
using GameTypes;

namespace RelayLib
{
	// Token: 0x02000002 RID: 2
	public class RelayTreeRunner
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
		public RelayTreeRunner(RelayTwo pRelay, string pTableName)
		{
			D.isNull(pRelay);
			if (!pRelay.tables.ContainsKey(pTableName))
			{
				pRelay.CreateTable(pTableName);
			}
			this._table = pRelay.GetTable(pTableName);
			List<RelayTreeNode> list = InstantiatorTwo.Process<RelayTreeNode>(this._table);
			foreach (RelayTreeNode relayTreeNode in list)
			{
				D.assert(relayTreeNode.hasSetupCells, "an object of type " + relayTreeNode.GetType().Name + " did not call base method of SetupCells");
				this._nodes.Add(relayTreeNode.objectId, relayTreeNode);
				relayTreeNode.SetRunner(this);
			}
			foreach (RelayTreeNode relayTreeNode2 in list)
			{
				relayTreeNode2.RestoreRelations();
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002224 File Offset: 0x00000424
		public T CreateNode<T>() where T : RelayTreeNode
		{
			Type typeFromHandle = typeof(T);
			T t = Activator.CreateInstance(typeFromHandle) as T;
			t.SetRunner(this);
			t.CreateNewRelayEntry(this._table, typeFromHandle.Name);
			this._nodes.Add(t.objectId, t);
			D.assert(t.hasSetupCells, "an object of type " + t.GetType().Name + " did not call base method of SetupCells");
			return t;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000022C4 File Offset: 0x000004C4
		public IEnumerable<RelayTreeNode> nodes
		{
			get
			{
				return this._nodes.Values;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000022D4 File Offset: 0x000004D4
		public T GetNode<T>(int pID) where T : RelayTreeNode
		{
			RelayTreeNode relayTreeNode = null;
			this._nodes.TryGetValue(pID, out relayTreeNode);
			return (T)((object)relayTreeNode);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000022F8 File Offset: 0x000004F8
		internal void Destroy(RelayTreeNode pNode)
		{
			this._table.RemoveRowAt(pNode.objectId);
			this._nodes.Remove(pNode.objectId);
		}

		// Token: 0x04000001 RID: 1
		private TableTwo _table;

		// Token: 0x04000002 RID: 2
		private Dictionary<int, RelayTreeNode> _nodes = new Dictionary<int, RelayTreeNode>();
	}
}
