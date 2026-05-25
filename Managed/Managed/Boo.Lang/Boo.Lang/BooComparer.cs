using System;
using System.Collections;

namespace Boo.Lang
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class BooComparer : IComparer
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000215C File Offset: 0x0000035C
		private BooComparer()
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002170 File Offset: 0x00000370
		public int Compare(object lhs, object rhs)
		{
			if (lhs == null)
			{
				return (rhs != null) ? (-1) : 0;
			}
			if (rhs == null)
			{
				return 1;
			}
			IComparable comparable = lhs as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(rhs);
			}
			IComparable comparable2 = rhs as IComparable;
			if (comparable2 != null)
			{
				return -1 * comparable2.CompareTo(lhs);
			}
			IEnumerable enumerable = lhs as IEnumerable;
			IEnumerable enumerable2 = rhs as IEnumerable;
			if (enumerable != null && enumerable2 != null)
			{
				return this.CompareEnumerables(enumerable, enumerable2);
			}
			return (!lhs.Equals(rhs)) ? 1 : 0;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021FC File Offset: 0x000003FC
		private int CompareEnumerables(IEnumerable lhs, IEnumerable rhs)
		{
			IEnumerator enumerator = lhs.GetEnumerator();
			IEnumerator enumerator2 = rhs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator2.MoveNext())
				{
					return 1;
				}
				int num = this.Compare(enumerator.Current, enumerator2.Current);
				if (num != 0)
				{
					return num;
				}
			}
			if (enumerator2.MoveNext())
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x04000003 RID: 3
		public static readonly IComparer Default = new BooComparer();
	}
}
