using System;
using System.Collections.Generic;
using Pathfinding.Datastructures;

namespace Pathfinding
{
	// Token: 0x02000009 RID: 9
	public class PathSolver<PathNodeType> where PathNodeType : IPathNode
	{
		// Token: 0x0600002E RID: 46 RVA: 0x0000247C File Offset: 0x0000067C
		private void TryQueueNewTile(IPathNode pNewNode, PathLink pLink, AStarStack pNodesToVisit, IPathNode pGoal)
		{
			IPathNode otherNode = pLink.GetOtherNode(pNewNode);
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

		// Token: 0x0600002F RID: 47 RVA: 0x000024E8 File Offset: 0x000006E8
		public Path<PathNodeType> FindPath(IPathNode pStart, IPathNode pGoal, IPathNetwork<PathNodeType> pNetwork, bool pReset)
		{
			if (pNetwork == null)
			{
				throw new Exception("pNetwork is null");
			}
			if (pStart == null || pGoal == null)
			{
				return new Path<PathNodeType>(new PathNodeType[0], 0f, PathStatus.DESTINATION_UNREACHABLE, 0);
			}
			if (pStart == pGoal)
			{
				return new Path<PathNodeType>(new PathNodeType[0], 0f, PathStatus.ALREADY_THERE, 0);
			}
			int num = 0;
			if (pReset)
			{
				pNetwork.Reset();
			}
			pStart.isStartNode = true;
			pGoal.isGoalNode = true;
			List<PathNodeType> list = new List<PathNodeType>();
			IPathNode pathNode = pStart;
			pathNode.visited = true;
			pathNode.linkLeadingHere = null;
			AStarStack astarStack = new AStarStack();
			PathStatus pathStatus = PathStatus.NOT_CALCULATED_YET;
			num = 1;
			while (pathStatus == PathStatus.NOT_CALCULATED_YET)
			{
				foreach (PathLink pathLink in pathNode.links)
				{
					IPathNode otherNode = pathLink.GetOtherNode(pathNode);
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
					pathNode = astarStack.Pop();
					num++;
					pathNode.visited = true;
					if (pathNode == pGoal)
					{
						pathStatus = PathStatus.FOUND_GOAL;
					}
				}
			}
			float num2 = 0f;
			if (pathStatus == PathStatus.FOUND_GOAL)
			{
				num2 = pathNode.pathCostHere;
				while (pathNode != pStart)
				{
					list.Add((PathNodeType)((object)pathNode));
					pathNode = pathNode.linkLeadingHere.GetOtherNode(pathNode);
				}
				list.Add((PathNodeType)((object)pathNode));
				list.Reverse();
			}
			return new Path<PathNodeType>(list.ToArray(), num2, pathStatus, num);
		}
	}
}
