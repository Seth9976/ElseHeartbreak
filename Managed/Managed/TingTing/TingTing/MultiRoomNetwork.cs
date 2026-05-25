using System;
using System.Collections.Generic;

namespace TingTing
{
	// Token: 0x0200000D RID: 13
	internal class MultiRoomNetwork
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004C20 File Offset: 0x00002E20
		public MultiRoomNetwork(IList<Room> pRooms)
		{
			List<PointTileNode> list = new List<PointTileNode>();
			foreach (Room room in pRooms)
			{
				list.AddRange(room._tilesByLocalPositionHash.Values);
			}
			this.nodes = list.ToArray();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004CAC File Offset: 0x00002EAC
		public void Reset()
		{
			foreach (PointTileNode pointTileNode in this.nodes)
			{
				pointTileNode.isGoalNode = false;
				pointTileNode.isStartNode = false;
				pointTileNode.distanceToGoal = 0f;
				pointTileNode.pathCostHere = 0f;
				pointTileNode.visited = false;
				pointTileNode.linkLeadingHere = null;
			}
		}

		// Token: 0x0400004D RID: 77
		private PointTileNode[] nodes = null;
	}
}
