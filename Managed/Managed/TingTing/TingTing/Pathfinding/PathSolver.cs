using System;
using System.Collections.Generic;
using Pathfinding.Datastructures;
using TingTing;

namespace Pathfinding
{
	// Token: 0x02000016 RID: 22
	public class PathSolver
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00005288 File Offset: 0x00003488
		private void TryQueueNewTile(PointTileNode pNewNode, PathLink pLink, AStarStack pNodesToVisit, PointTileNode pGoal)
		{
			PointTileNode otherNode = pLink.GetOtherNode(pNewNode);
			float distance = pLink.distance;
			float num = otherNode.pathCostHere + pNewNode.baseCost + distance;
			if (pNewNode.linkLeadingHere == null || pNewNode.pathCostHere > num)
			{
				pNewNode.distanceToGoal = pNewNode.DistanceTo(pGoal) * 2f;
				pNewNode.pathCostHere = num;
				pNewNode.linkLeadingHere = pLink;
				pNodesToVisit.Push(pNewNode);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000052F4 File Offset: 0x000034F4
		public Path FindPath(PointTileNode pStart, PointTileNode pGoal, RoomRunner pNetwork, bool pReset)
		{
			if (pNetwork == null)
			{
				throw new Exception("pNetwork is null");
			}
			if (pStart == null || pGoal == null)
			{
				return new Path(new PointTileNode[0], 0f, PathStatus.DESTINATION_UNREACHABLE, 0);
			}
			if (pStart == pGoal)
			{
				return new Path(new PointTileNode[0], 0f, PathStatus.ALREADY_THERE, 0);
			}
			int num = 0;
			if (pReset)
			{
				pNetwork.Reset();
			}
			pStart.isStartNode = true;
			pGoal.isGoalNode = true;
			List<PointTileNode> list = new List<PointTileNode>();
			PointTileNode pointTileNode = pStart;
			pointTileNode.visited = true;
			pointTileNode.linkLeadingHere = null;
			AStarStack astarStack = new AStarStack();
			PathStatus pathStatus = PathStatus.NOT_CALCULATED_YET;
			num = 1;
			while (pathStatus == PathStatus.NOT_CALCULATED_YET)
			{
				foreach (PathLink pathLink in pointTileNode.links)
				{
					PointTileNode otherNode = pathLink.GetOtherNode(pointTileNode);
					if (!otherNode.visited)
					{
						this.TryQueueNewTile(otherNode, pathLink, astarStack, pGoal);
					}
				}
				if (astarStack.Count == 0)
				{
					pathStatus = PathStatus.DESTINATION_UNREACHABLE;
				}
				else
				{
					pointTileNode = astarStack.Pop();
					num++;
					pointTileNode.visited = true;
					if (pointTileNode == pGoal)
					{
						pathStatus = PathStatus.FOUND_GOAL;
					}
				}
			}
			float num2 = 0f;
			if (pathStatus == PathStatus.FOUND_GOAL)
			{
				num2 = pointTileNode.pathCostHere;
				while (pointTileNode != pStart)
				{
					list.Add(pointTileNode);
					pointTileNode = pointTileNode.linkLeadingHere.GetOtherNode(pointTileNode);
				}
				list.Add(pointTileNode);
				list.Reverse();
			}
			return new Path(list.ToArray(), num2, pathStatus, num);
		}
	}
}
