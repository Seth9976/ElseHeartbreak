using System;
using System.Collections.Generic;
using GameTypes;

namespace Pathfinding
{
	// Token: 0x0200000A RID: 10
	public class TileNode : IPoint, IComparable, IPathNode
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002694 File Offset: 0x00000894
		public TileNode(IntPoint pLocalPoint)
		{
			this.links = new List<PathLink>(5);
			this.localPoint = pLocalPoint;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026B0 File Offset: 0x000008B0
		public void Reset()
		{
			this.distanceToGoal = 0f;
			this.isGoalNode = false;
			this.isStartNode = false;
			this.linkLeadingHere = null;
			this.pathCostHere = 0f;
			this.visited = false;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000026FC File Offset: 0x000008FC
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000026F0 File Offset: 0x000008F0
		public IntPoint localPoint { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002704 File Offset: 0x00000904
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000270C File Offset: 0x0000090C
		public float pathCostHere { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002718 File Offset: 0x00000918
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002720 File Offset: 0x00000920
		public float distanceToGoal { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002738 File Offset: 0x00000938
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000272C File Offset: 0x0000092C
		public float baseCost { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002740 File Offset: 0x00000940
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002748 File Offset: 0x00000948
		public bool isStartNode { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002754 File Offset: 0x00000954
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000275C File Offset: 0x0000095C
		public bool isGoalNode { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002768 File Offset: 0x00000968
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002770 File Offset: 0x00000970
		public bool visited { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000277C File Offset: 0x0000097C
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002784 File Offset: 0x00000984
		public PathLink linkLeadingHere { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002790 File Offset: 0x00000990
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002798 File Offset: 0x00000998
		public List<PathLink> links { get; set; }

		// Token: 0x06000044 RID: 68 RVA: 0x000027A4 File Offset: 0x000009A4
		public void AddLink(PathLink pLink)
		{
			this.links.Add(pLink);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000027B4 File Offset: 0x000009B4
		public void RemoveLink(PathLink pLink)
		{
			this.links.Remove(pLink);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000027C4 File Offset: 0x000009C4
		public void RemoveAllLinks()
		{
			this.links.Clear();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000027D4 File Offset: 0x000009D4
		public PathLink GetLinkTo(IPathNode pNode)
		{
			if (this.links != null)
			{
				foreach (PathLink pathLink in this.links)
				{
					if (pathLink.Contains(pNode))
					{
						return pathLink;
					}
				}
			}
			return null;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002858 File Offset: 0x00000A58
		public bool isIsolated()
		{
			return this.links.Count == 0;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002868 File Offset: 0x00000A68
		public virtual float DistanceTo(IPoint pPoint)
		{
			if (pPoint is TileNode)
			{
				TileNode tileNode = pPoint as TileNode;
				return this.localPoint.EuclidianDistanceTo(tileNode.localPoint);
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000028A4 File Offset: 0x00000AA4
		public int CompareTo(object obj)
		{
			D.assert(obj is TileNode);
			TileNode tileNode = obj as TileNode;
			float num = tileNode.pathCostHere + tileNode.distanceToGoal;
			float num2 = this.pathCostHere + this.distanceToGoal;
			if (num > num2)
			{
				return 1;
			}
			if (num == num2)
			{
				return 0;
			}
			return -1;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000028F8 File Offset: 0x00000AF8
		public virtual long GetUniqueID()
		{
			return BitCruncher.PackTwoInts(this.localPoint.x, this.localPoint.y);
		}
	}
}
