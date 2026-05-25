using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	/// <summary>Contains functionality to compare nodes for their document order. This class cannot be inherited. </summary>
	// Token: 0x02000019 RID: 25
	public sealed class XNodeDocumentOrderComparer : IComparer, IComparer<XNode>
	{
		/// <summary>Compares two nodes to determine their relative document order.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains 0 if the nodes are equal; -1 if <paramref name="x" /> is before <paramref name="y" />; 1 if <paramref name="x" /> is after <paramref name="y" />.</returns>
		/// <param name="x">The first <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="y">The second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <exception cref="T:System.InvalidOperationException">The two nodes do not share a common ancestor.</exception>
		/// <exception cref="T:System.ArgumentException">The two nodes are not derived from <see cref="T:System.Xml.Linq.XNode" />.</exception>
		// Token: 0x0600013A RID: 314 RVA: 0x00005ED0 File Offset: 0x000040D0
		int IComparer.Compare(object n1, object n2)
		{
			return this.Compare((XNode)n1, (XNode)n2);
		}

		/// <summary>Compares two nodes to determine their relative document order.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains 0 if the nodes are equal; -1 if <paramref name="x" /> is before <paramref name="y" />; 1 if <paramref name="x" /> is after <paramref name="y" />.</returns>
		/// <param name="x">The first <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="y">The second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <exception cref="T:System.InvalidOperationException">The two nodes do not share a common ancestor.</exception>
		// Token: 0x0600013B RID: 315 RVA: 0x00005EE4 File Offset: 0x000040E4
		public int Compare(XNode n1, XNode n2)
		{
			switch (this.CompareCore(n1, n2))
			{
			case XNodeDocumentOrderComparer.CompareResult.Same:
				return 0;
			case XNodeDocumentOrderComparer.CompareResult.Random:
				return (DateTime.Now.Ticks % 2L != 1L) ? (-1) : 1;
			case XNodeDocumentOrderComparer.CompareResult.Parent:
			case XNodeDocumentOrderComparer.CompareResult.Ancestor:
			case XNodeDocumentOrderComparer.CompareResult.Preceding:
				return 1;
			}
			return -1;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005F48 File Offset: 0x00004148
		private XNodeDocumentOrderComparer.CompareResult CompareCore(XNode n1, XNode n2)
		{
			if (n1 == n2)
			{
				return XNodeDocumentOrderComparer.CompareResult.Same;
			}
			if (n1.Owner != null)
			{
				if (n2.Owner == null)
				{
					XNodeDocumentOrderComparer.CompareResult compareResult = this.CompareCore(n2, n1);
					switch (compareResult)
					{
					case XNodeDocumentOrderComparer.CompareResult.Same:
					case XNodeDocumentOrderComparer.CompareResult.Random:
						return compareResult;
					case XNodeDocumentOrderComparer.CompareResult.Parent:
						return XNodeDocumentOrderComparer.CompareResult.Child;
					case XNodeDocumentOrderComparer.CompareResult.Child:
						return XNodeDocumentOrderComparer.CompareResult.Parent;
					case XNodeDocumentOrderComparer.CompareResult.Ancestor:
						return XNodeDocumentOrderComparer.CompareResult.Descendant;
					case XNodeDocumentOrderComparer.CompareResult.Descendant:
						return XNodeDocumentOrderComparer.CompareResult.Ancestor;
					case XNodeDocumentOrderComparer.CompareResult.Preceding:
						return XNodeDocumentOrderComparer.CompareResult.Following;
					case XNodeDocumentOrderComparer.CompareResult.Following:
						return XNodeDocumentOrderComparer.CompareResult.Preceding;
					}
				}
				XNodeDocumentOrderComparer.CompareResult compareResult2 = this.CompareCore(n1.Owner, n2.Owner);
				switch (compareResult2)
				{
				case XNodeDocumentOrderComparer.CompareResult.Same:
					return this.CompareSibling(n1, n2, XNodeDocumentOrderComparer.CompareResult.Same);
				case XNodeDocumentOrderComparer.CompareResult.Parent:
					return this.CompareSibling(n1.Owner, n2, XNodeDocumentOrderComparer.CompareResult.Parent);
				case XNodeDocumentOrderComparer.CompareResult.Child:
					return this.CompareSibling(n1, n2.Owner, XNodeDocumentOrderComparer.CompareResult.Child);
				case XNodeDocumentOrderComparer.CompareResult.Ancestor:
				{
					XNode xnode = n1;
					while (xnode.Owner != n2.Owner)
					{
						xnode = xnode.Owner;
					}
					return this.CompareSibling(xnode, n2, XNodeDocumentOrderComparer.CompareResult.Ancestor);
				}
				case XNodeDocumentOrderComparer.CompareResult.Descendant:
				{
					XNode xnode2 = n2;
					while (xnode2.Owner != n1.Owner)
					{
						xnode2 = xnode2.Owner;
					}
					return this.CompareSibling(n1, xnode2, XNodeDocumentOrderComparer.CompareResult.Descendant);
				}
				}
				return compareResult2;
			}
			if (n2.Owner == null)
			{
				return XNodeDocumentOrderComparer.CompareResult.Random;
			}
			XNodeDocumentOrderComparer.CompareResult compareResult3 = this.CompareCore(n1, n2.Owner);
			switch (compareResult3)
			{
			case XNodeDocumentOrderComparer.CompareResult.Same:
				return XNodeDocumentOrderComparer.CompareResult.Child;
			case XNodeDocumentOrderComparer.CompareResult.Parent:
			case XNodeDocumentOrderComparer.CompareResult.Ancestor:
				throw new Exception("INTERNAL ERROR: should not happen");
			case XNodeDocumentOrderComparer.CompareResult.Child:
			case XNodeDocumentOrderComparer.CompareResult.Descendant:
				return XNodeDocumentOrderComparer.CompareResult.Descendant;
			}
			return compareResult3;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000060D4 File Offset: 0x000042D4
		private XNodeDocumentOrderComparer.CompareResult CompareSibling(XNode n1, XNode n2, XNodeDocumentOrderComparer.CompareResult forSameValue)
		{
			if (n1 == n2)
			{
				return forSameValue;
			}
			for (XNode xnode = n1.NextNode; xnode != null; xnode = xnode.NextNode)
			{
				if (xnode == n2)
				{
					return XNodeDocumentOrderComparer.CompareResult.Following;
				}
			}
			return XNodeDocumentOrderComparer.CompareResult.Preceding;
		}

		// Token: 0x0200001A RID: 26
		private enum CompareResult
		{
			// Token: 0x0400004B RID: 75
			Same,
			// Token: 0x0400004C RID: 76
			Random,
			// Token: 0x0400004D RID: 77
			Parent,
			// Token: 0x0400004E RID: 78
			Child,
			// Token: 0x0400004F RID: 79
			Ancestor,
			// Token: 0x04000050 RID: 80
			Descendant,
			// Token: 0x04000051 RID: 81
			Preceding,
			// Token: 0x04000052 RID: 82
			Following
		}
	}
}
