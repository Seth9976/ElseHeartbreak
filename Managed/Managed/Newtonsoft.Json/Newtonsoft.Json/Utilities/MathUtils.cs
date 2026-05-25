using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000BD RID: 189
	internal class MathUtils
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x0001ED41 File Offset: 0x0001CF41
		public static int IntLength(int i)
		{
			if (i < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (i == 0)
			{
				return 1;
			}
			return (int)Math.Floor(Math.Log10((double)i)) + 1;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001ED61 File Offset: 0x0001CF61
		public static int HexToInt(char h)
		{
			if (h >= '0' && h <= '9')
			{
				return (int)(h - '0');
			}
			if (h >= 'a' && h <= 'f')
			{
				return (int)(h - 'a' + '\n');
			}
			if (h >= 'A' && h <= 'F')
			{
				return (int)(h - 'A' + '\n');
			}
			return -1;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001ED97 File Offset: 0x0001CF97
		public static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 97);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001EDAC File Offset: 0x0001CFAC
		public static int GetDecimalPlaces(double value)
		{
			int num = 10;
			double num2 = Math.Pow(0.1, (double)num);
			if (value == 0.0)
			{
				return 0;
			}
			int num3 = 0;
			while (value - Math.Floor(value) > num2 && num3 < num)
			{
				value *= 10.0;
				num3++;
			}
			return num3;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001EE00 File Offset: 0x0001D000
		public static int? Min(int? val1, int? val2)
		{
			if (val1 == null)
			{
				return val2;
			}
			if (val2 == null)
			{
				return val1;
			}
			return new int?(Math.Min(val1.Value, val2.Value));
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001EE30 File Offset: 0x0001D030
		public static int? Max(int? val1, int? val2)
		{
			if (val1 == null)
			{
				return val2;
			}
			if (val2 == null)
			{
				return val1;
			}
			return new int?(Math.Max(val1.Value, val2.Value));
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001EE60 File Offset: 0x0001D060
		public static double? Min(double? val1, double? val2)
		{
			if (val1 == null)
			{
				return val2;
			}
			if (val2 == null)
			{
				return val1;
			}
			return new double?(Math.Min(val1.Value, val2.Value));
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001EE90 File Offset: 0x0001D090
		public static double? Max(double? val1, double? val2)
		{
			if (val1 == null)
			{
				return val2;
			}
			if (val2 == null)
			{
				return val1;
			}
			return new double?(Math.Max(val1.Value, val2.Value));
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001EEC0 File Offset: 0x0001D0C0
		public static bool ApproxEquals(double d1, double d2)
		{
			return Math.Abs(d1 - d2) < Math.Abs(d1) * 1E-06;
		}
	}
}
