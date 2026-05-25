using System;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding
{
	// Token: 0x02000006 RID: 6
	public class PathLink : IEnumerable<IPathNode>, IEnumerable
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002344 File Offset: 0x00000544
		public PathLink(IPathNode pNodeA, IPathNode pNodeB)
		{
			this.distance = pNodeA.DistanceTo(pNodeB);
			this.nodeA = pNodeA;
			this.nodeB = pNodeB;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002368 File Offset: 0x00000568
		IEnumerator IEnumerable.GetEnumerator()
		{
			yield return this.nodeA;
			yield return this.nodeB;
			yield break;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002384 File Offset: 0x00000584
		public IPathNode GetOtherNode(IPathNode pSelf)
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

		// Token: 0x06000010 RID: 16 RVA: 0x000023C4 File Offset: 0x000005C4
		public int IndexOf(IPathNode item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023CC File Offset: 0x000005CC
		public void Insert(int index, IPathNode item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023D4 File Offset: 0x000005D4
		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000004 RID: 4
		public IPathNode this[int index]
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

		// Token: 0x06000015 RID: 21 RVA: 0x0000241C File Offset: 0x0000061C
		public void Add(IPathNode item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002424 File Offset: 0x00000624
		public void Clear()
		{
			this.nodeA = null;
			this.nodeB = null;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002434 File Offset: 0x00000634
		public bool Contains(IPathNode item)
		{
			return this.nodeA == item || this.nodeB == item;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002454 File Offset: 0x00000654
		public int Count
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002458 File Offset: 0x00000658
		public IEnumerator<IPathNode> GetEnumerator()
		{
			yield return this.nodeA;
			yield return this.nodeB;
			yield break;
		}

		// Token: 0x0400000B RID: 11
		public float distance;

		// Token: 0x0400000C RID: 12
		public IPathNode nodeA;

		// Token: 0x0400000D RID: 13
		public IPathNode nodeB;
	}
}
