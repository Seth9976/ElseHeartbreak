using System;
using System.Collections.Generic;

namespace RelayLib
{
	// Token: 0x02000006 RID: 6
	public abstract class RelayTreeNode : RelayObjectTwo
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000235C File Offset: 0x0000055C
		internal void SetRunner(RelayTreeRunner pRunner)
		{
			this._relayTreeRunner = pRunner;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002368 File Offset: 0x00000568
		protected override void SetupCells()
		{
			this.CELL_children = base.EnsureCell<int[]>("childrenIDs", new int[0]);
			this.parent = null;
			this.hasSetupCells = true;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002390 File Offset: 0x00000590
		internal void RestoreRelations()
		{
			foreach (int num in this.CELL_children.data)
			{
				RelayTreeNode node = this._relayTreeRunner.GetNode<RelayTreeNode>(num);
				node.parent = this;
				this._children.Add(node);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000023F0 File Offset: 0x000005F0
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000023E4 File Offset: 0x000005E4
		public RelayTreeNode parent { get; private set; }

		// Token: 0x0600000F RID: 15 RVA: 0x000023F8 File Offset: 0x000005F8
		public void AddChild(RelayTreeNode pChild)
		{
			pChild.parent = this;
			this._children.Add(pChild);
			this.UpdateCELL_Children();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002414 File Offset: 0x00000614
		public void RemoveAndDestroyChild(RelayTreeNode pChild)
		{
			this._children.Remove(pChild);
			pChild.parent = null;
			List<RelayTreeNode> list = new List<RelayTreeNode>(pChild.children);
			foreach (RelayTreeNode relayTreeNode in list)
			{
				pChild.RemoveAndDestroyChild(relayTreeNode);
			}
			this._relayTreeRunner.Destroy(pChild);
			this.UpdateCELL_Children();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024AC File Offset: 0x000006AC
		private void UpdateCELL_Children()
		{
			List<int> list = new List<int>();
			foreach (RelayTreeNode relayTreeNode in this._children)
			{
				list.Add(relayTreeNode.objectId);
			}
			this.CELL_children.data = list.ToArray();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002530 File Offset: 0x00000730
		public IEnumerable<RelayTreeNode> children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002538 File Offset: 0x00000738
		public int childCount
		{
			get
			{
				return this.CELL_children.data.Length;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002548 File Offset: 0x00000748
		public T[] GetChildrenRecursive<T>() where T : RelayTreeNode
		{
			List<T> list = new List<T>();
			foreach (RelayTreeNode relayTreeNode in this._children)
			{
				if (relayTreeNode is T)
				{
					list.Add(relayTreeNode as T);
				}
				T[] childrenRecursive = relayTreeNode.GetChildrenRecursive<T>();
				if (childrenRecursive.Length > 0)
				{
					list.AddRange(childrenRecursive);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025E8 File Offset: 0x000007E8
		public int CountChildrenRecursive()
		{
			int num = 0;
			foreach (RelayTreeNode relayTreeNode in this._children)
			{
				num += relayTreeNode.CountChildrenRecursive() + 1;
			}
			return num;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002658 File Offset: 0x00000858
		public IEnumerable<T> GetChildren<T>() where T : RelayTreeNode
		{
			foreach (RelayTreeNode i in this._children)
			{
				if (i is T)
				{
					yield return i as T;
				}
			}
			yield break;
		}

		// Token: 0x04000003 RID: 3
		internal bool hasSetupCells;

		// Token: 0x04000004 RID: 4
		public ValueEntry<int[]> CELL_children;

		// Token: 0x04000005 RID: 5
		private List<RelayTreeNode> _children = new List<RelayTreeNode>();

		// Token: 0x04000006 RID: 6
		protected RelayTreeRunner _relayTreeRunner;
	}
}
