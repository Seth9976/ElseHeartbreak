using System;
using System.Collections.Generic;

namespace Pathfinding.Datastructures
{
	// Token: 0x02000002 RID: 2
	public class AStarStack
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002100 File Offset: 0x00000300
		public void Push(IPathNode pNode)
		{
			this._nodes[pNode.GetUniqueID()] = pNode;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002114 File Offset: 0x00000314
		public IPathNode Pop()
		{
			IPathNode pathNode = null;
			foreach (IPathNode pathNode2 in this._nodes.Values)
			{
				if (pathNode == null || pathNode2.CompareTo(pathNode) == 1)
				{
					pathNode = pathNode2;
				}
			}
			if (pathNode == null)
			{
				return null;
			}
			this._nodes.Remove(pathNode.GetUniqueID());
			return pathNode;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000021AC File Offset: 0x000003AC
		public int Count
		{
			get
			{
				return this._nodes.Values.Count;
			}
		}

		// Token: 0x04000001 RID: 1
		private Dictionary<long, IPathNode> _nodes = new Dictionary<long, IPathNode>();
	}
}
