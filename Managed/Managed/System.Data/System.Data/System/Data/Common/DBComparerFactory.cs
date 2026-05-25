using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x02000094 RID: 148
	internal class DBComparerFactory
	{
		// Token: 0x060006CA RID: 1738 RVA: 0x00022AA4 File Offset: 0x00020CA4
		public static IComparer GetComparer(Type type, bool ignoreCase)
		{
			if (type == typeof(string))
			{
				if (ignoreCase)
				{
					return DBComparerFactory.ignoreCaseComparer;
				}
				return DBComparerFactory.caseComparer;
			}
			else
			{
				if (DBComparerFactory.icomparerType.IsAssignableFrom(type))
				{
					return DBComparerFactory.comparableComparer;
				}
				if (type == typeof(byte[]))
				{
					return DBComparerFactory.byteArrayComparer;
				}
				return null;
			}
		}

		// Token: 0x040002D3 RID: 723
		private static IComparer comparableComparer = new DBComparerFactory.ComparebleComparer();

		// Token: 0x040002D4 RID: 724
		private static IComparer ignoreCaseComparer = new DBComparerFactory.IgnoreCaseComparer();

		// Token: 0x040002D5 RID: 725
		private static IComparer caseComparer = new DBComparerFactory.CaseComparer();

		// Token: 0x040002D6 RID: 726
		private static IComparer byteArrayComparer = new DBComparerFactory.ByteArrayComparer();

		// Token: 0x040002D7 RID: 727
		private static Type icomparerType = typeof(IComparable);

		// Token: 0x02000095 RID: 149
		private class ComparebleComparer : IComparer
		{
			// Token: 0x060006CC RID: 1740 RVA: 0x00022B08 File Offset: 0x00020D08
			public int Compare(object x, object y)
			{
				if (x == DBNull.Value)
				{
					if (y == DBNull.Value)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (y == DBNull.Value)
					{
						return 1;
					}
					return ((IComparable)x).CompareTo(y);
				}
			}
		}

		// Token: 0x02000096 RID: 150
		private class CaseComparer : IComparer
		{
			// Token: 0x060006CE RID: 1742 RVA: 0x00022B48 File Offset: 0x00020D48
			public int Compare(object x, object y)
			{
				if (x == DBNull.Value)
				{
					if (y == DBNull.Value)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (y == DBNull.Value)
					{
						return 1;
					}
					return string.Compare((string)x, (string)y, false);
				}
			}
		}

		// Token: 0x02000097 RID: 151
		private class IgnoreCaseComparer : IComparer
		{
			// Token: 0x060006D0 RID: 1744 RVA: 0x00022B8C File Offset: 0x00020D8C
			public int Compare(object x, object y)
			{
				if (x == DBNull.Value)
				{
					if (y == DBNull.Value)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (y == DBNull.Value)
					{
						return 1;
					}
					return string.Compare((string)x, (string)y, true);
				}
			}
		}

		// Token: 0x02000098 RID: 152
		private class ByteArrayComparer : IComparer
		{
			// Token: 0x060006D2 RID: 1746 RVA: 0x00022BD0 File Offset: 0x00020DD0
			public int Compare(object x, object y)
			{
				if (x == DBNull.Value)
				{
					if (y == DBNull.Value)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (y == DBNull.Value)
					{
						return 1;
					}
					byte[] array = (byte[])x;
					byte[] array2 = (byte[])y;
					int num = array.Length;
					int num2 = array2.Length;
					int num3 = 0;
					for (;;)
					{
						int num4 = 0;
						int num5 = 0;
						if (num3 < num)
						{
							num4 = (int)array[num3];
						}
						else if (num3 >= num2)
						{
							break;
						}
						if (num3 < num2)
						{
							num5 = (int)array2[num3];
						}
						if (num4 > num5)
						{
							return 1;
						}
						if (num5 > num4)
						{
							return -1;
						}
						num3++;
					}
					return 0;
				}
			}
		}
	}
}
