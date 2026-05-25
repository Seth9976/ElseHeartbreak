using System;
using System.Text;

namespace Pathfinding
{
	// Token: 0x02000005 RID: 5
	public struct Path<PathNodeType> where PathNodeType : IPathNode
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000021C0 File Offset: 0x000003C0
		public Path(PathNodeType[] pNodes, float pPathLength, PathStatus pStatus, int pPathSearchTestCount)
		{
			this.nodes = pNodes;
			this.pathLength = pPathLength;
			this.status = pStatus;
			this.pathSearchTestCount = pPathSearchTestCount;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021E0 File Offset: 0x000003E0
		public static Path<PathNodeType> EMPTY
		{
			get
			{
				return new Path<PathNodeType>(new PathNodeType[0], 0f, PathStatus.NOT_CALCULATED_YET, 0);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021F4 File Offset: 0x000003F4
		public PathNodeType LastNode
		{
			get
			{
				return this.nodes[this.nodes.Length - 1];
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000220C File Offset: 0x0000040C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Path: \n[ ");
			foreach (PathNodeType pathNode in this.nodes)
			{
				stringBuilder.Append(pathNode.ToString() + ",\n");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000227C File Offset: 0x0000047C
		public override bool Equals(object pOther)
		{
			if (!(pOther is Path<PathNodeType>))
			{
				return false;
			}
			Path<PathNodeType> path = (Path<PathNodeType>)pOther;
			if (this.status != path.status)
			{
				return false;
			}
			if (this.pathLength != path.pathLength)
			{
				return false;
			}
			int num = 0;
			while ((float)num < this.pathLength)
			{
				if ((IEquatable<PathNodeType>)((object)this.nodes[num]) != (IEquatable<PathNodeType>)((object)path.nodes[num]))
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002314 File Offset: 0x00000514
		public static bool operator ==(Path<PathNodeType> a, Path<PathNodeType> b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000232C File Offset: 0x0000052C
		public static bool operator !=(Path<PathNodeType> a, Path<PathNodeType> b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04000007 RID: 7
		public PathStatus status;

		// Token: 0x04000008 RID: 8
		public float pathLength;

		// Token: 0x04000009 RID: 9
		public PathNodeType[] nodes;

		// Token: 0x0400000A RID: 10
		public int pathSearchTestCount;
	}
}
