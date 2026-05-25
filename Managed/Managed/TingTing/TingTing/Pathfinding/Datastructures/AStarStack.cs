using System;
using System.Collections.Generic;
using TingTing;

namespace Pathfinding.Datastructures
{
	// Token: 0x02000017 RID: 23
	public class AStarStack
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x000054AC File Offset: 0x000036AC
		public void Push(PointTileNode pNode)
		{
			this._nodes[pNode.GetUniqueID()] = pNode;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000054C0 File Offset: 0x000036C0
		public PointTileNode Pop()
		{
			PointTileNode pointTileNode = null;
			foreach (PointTileNode pointTileNode2 in this._nodes.Values)
			{
				if (pointTileNode == null || pointTileNode2.CompareTo(pointTileNode) == 1)
				{
					pointTileNode = pointTileNode2;
				}
			}
			if (pointTileNode == null)
			{
				return null;
			}
			this._nodes.Remove(pointTileNode.GetUniqueID());
			return pointTileNode;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00005558 File Offset: 0x00003758
		public int Count
		{
			get
			{
				return this._nodes.Values.Count;
			}
		}

		// Token: 0x0400005E RID: 94
		private Dictionary<long, PointTileNode> _nodes = new Dictionary<long, PointTileNode>();
	}
}
