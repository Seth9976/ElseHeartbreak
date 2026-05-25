using System;
using System.Text;
using TingTing;

namespace Pathfinding
{
	// Token: 0x02000015 RID: 21
	public struct Path
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x0000511C File Offset: 0x0000331C
		public Path(PointTileNode[] pNodes, float pPathLength, PathStatus pStatus, int pPathSearchTestCount)
		{
			this.nodes = pNodes;
			this.pathLength = pPathLength;
			this.status = pStatus;
			this.pathSearchTestCount = pPathSearchTestCount;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000513C File Offset: 0x0000333C
		public static Path EMPTY
		{
			get
			{
				return new Path(new PointTileNode[0], 0f, PathStatus.NOT_CALCULATED_YET, 0);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005150 File Offset: 0x00003350
		public PointTileNode LastNode
		{
			get
			{
				return this.nodes[this.nodes.Length - 1];
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005164 File Offset: 0x00003364
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Path: \n[ ");
			foreach (PointTileNode pointTileNode in this.nodes)
			{
				stringBuilder.Append(pointTileNode.ToString() + ",\n");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000051CC File Offset: 0x000033CC
		public override bool Equals(object pOther)
		{
			if (!(pOther is Path))
			{
				return false;
			}
			Path path = (Path)pOther;
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
				if ((IEquatable<PointTileNode>)this.nodes[num] != (IEquatable<PointTileNode>)path.nodes[num])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005250 File Offset: 0x00003450
		public static bool operator ==(Path a, Path b)
		{
			return a.Equals(b);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005268 File Offset: 0x00003468
		public static bool operator !=(Path a, Path b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0400005A RID: 90
		public PathStatus status;

		// Token: 0x0400005B RID: 91
		public float pathLength;

		// Token: 0x0400005C RID: 92
		public PointTileNode[] nodes;

		// Token: 0x0400005D RID: 93
		public int pathSearchTestCount;
	}
}
