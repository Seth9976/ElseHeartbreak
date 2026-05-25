using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	// Token: 0x02000015 RID: 21
	internal class XChildrenIterator : IEnumerable, IEnumerable<object>
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00005328 File Offset: 0x00003528
		public XChildrenIterator(XContainer source)
		{
			this.source = source;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005338 File Offset: 0x00003538
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005340 File Offset: 0x00003540
		public IEnumerator<object> GetEnumerator()
		{
			if (this.n == null)
			{
				this.n = this.source.FirstNode;
				if (this.n == null)
				{
					yield break;
				}
			}
			do
			{
				yield return this.n;
				this.n = this.n.NextNode;
			}
			while (this.n != this.source.LastNode);
			yield break;
		}

		// Token: 0x0400003C RID: 60
		private XContainer source;

		// Token: 0x0400003D RID: 61
		private XNode n;
	}
}
