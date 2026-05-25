using System;
using System.Collections;
using System.Collections.Generic;
using TingTing;

namespace Pathfinding
{
	// Token: 0x02000019 RID: 25
	public class PathLink : IEnumerable, IEnumerable<PointTileNode>
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x0000556C File Offset: 0x0000376C
		public PathLink(PointTileNode pNodeA, PointTileNode pNodeB)
		{
			this.distance = pNodeA.DistanceTo(pNodeB);
			this.nodeA = pNodeA;
			this.nodeB = pNodeB;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005590 File Offset: 0x00003790
		IEnumerator IEnumerable.GetEnumerator()
		{
			yield return this.nodeA;
			yield return this.nodeB;
			yield break;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000055AC File Offset: 0x000037AC
		public PointTileNode GetOtherNode(PointTileNode pSelf)
		{
			if (this.nodeA == pSelf)
			{
				return this.nodeB;
			}
			if (this.nodeB == pSelf)
			{
				return this.nodeA;
			}
			throw new Exception("Function must be used with a parameter that's contained by the link");
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000055EC File Offset: 0x000037EC
		public int IndexOf(PointTileNode item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000055F4 File Offset: 0x000037F4
		public void Insert(int index, PointTileNode item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000055FC File Offset: 0x000037FC
		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000034 RID: 52
		public PointTileNode this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.nodeA;
				}
				if (index == 1)
				{
					return this.nodeB;
				}
				return null;
			}
			set
			{
				if (index == 0)
				{
					this.nodeA = value;
				}
				if (index == 1)
				{
					this.nodeB = value;
				}
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005644 File Offset: 0x00003844
		public void Add(PointTileNode item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000564C File Offset: 0x0000384C
		public void Clear()
		{
			this.nodeA = null;
			this.nodeB = null;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000565C File Offset: 0x0000385C
		public bool Contains(PointTileNode item)
		{
			return this.nodeA == item || this.nodeB == item;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000567C File Offset: 0x0000387C
		public int Count
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005680 File Offset: 0x00003880
		public IEnumerator<PointTileNode> GetEnumerator()
		{
			yield return this.nodeA;
			yield return this.nodeB;
			yield break;
		}

		// Token: 0x0400005F RID: 95
		public float distance;

		// Token: 0x04000060 RID: 96
		public PointTileNode nodeA;

		// Token: 0x04000061 RID: 97
		public PointTileNode nodeB;
	}
}
