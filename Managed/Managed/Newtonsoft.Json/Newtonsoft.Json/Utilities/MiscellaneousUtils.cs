using System;
using System.Globalization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000BF RID: 191
	internal static class MiscellaneousUtils
	{
		// Token: 0x06000885 RID: 2181 RVA: 0x0001EEE4 File Offset: 0x0001D0E4
		public static bool ValueEquals(object objA, object objB)
		{
			if (objA == null && objB == null)
			{
				return true;
			}
			if (objA != null && objB == null)
			{
				return false;
			}
			if (objA == null && objB != null)
			{
				return false;
			}
			if (objA.GetType() == objB.GetType())
			{
				return objA.Equals(objB);
			}
			if (ConvertUtils.IsInteger(objA) && ConvertUtils.IsInteger(objB))
			{
				return Convert.ToDecimal(objA, CultureInfo.CurrentCulture).Equals(Convert.ToDecimal(objB, CultureInfo.CurrentCulture));
			}
			return (objA is double || objA is float || objA is decimal) && (objB is double || objB is float || objB is decimal) && MathUtils.ApproxEquals(Convert.ToDouble(objA, CultureInfo.CurrentCulture), Convert.ToDouble(objB, CultureInfo.CurrentCulture));
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001EF9C File Offset: 0x0001D19C
		public static ArgumentOutOfRangeException CreateArgumentOutOfRangeException(string paramName, object actualValue, string message)
		{
			string text = message + Environment.NewLine + "Actual value was {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { actualValue });
			return new ArgumentOutOfRangeException(paramName, text);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001EFD8 File Offset: 0x0001D1D8
		public static bool TryAction<T>(Creator<T> creator, out T output)
		{
			ValidationUtils.ArgumentNotNull(creator, "creator");
			bool flag;
			try
			{
				output = creator();
				flag = true;
			}
			catch
			{
				output = default(T);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001F020 File Offset: 0x0001D220
		public static string ToString(object value)
		{
			if (value == null)
			{
				return "{null}";
			}
			if (!(value is string))
			{
				return value.ToString();
			}
			return "\"" + value.ToString() + "\"";
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001F050 File Offset: 0x0001D250
		public static byte[] HexToBytes(string hex)
		{
			string text = hex.Replace("-", string.Empty);
			byte[] array = new byte[text.Length / 2];
			int num = 4;
			int num2 = 0;
			foreach (char c in text)
			{
				int num3 = (int)((c - '0') % ' ');
				if (num3 > 9)
				{
					num3 -= 7;
				}
				byte[] array2 = array;
				int num4 = num2;
				array2[num4] |= (byte)(num3 << num);
				num ^= 4;
				if (num != 0)
				{
					num2++;
				}
			}
			return array;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001F0E2 File Offset: 0x0001D2E2
		public static string BytesToHex(byte[] bytes)
		{
			return MiscellaneousUtils.BytesToHex(bytes, false);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001F0EC File Offset: 0x0001D2EC
		public static string BytesToHex(byte[] bytes, bool removeDashes)
		{
			string text = BitConverter.ToString(bytes);
			if (removeDashes)
			{
				text = text.Replace("-", "");
			}
			return text;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001F118 File Offset: 0x0001D318
		public static int ByteArrayCompare(byte[] a1, byte[] a2)
		{
			int num = a1.Length.CompareTo(a2.Length);
			if (num != 0)
			{
				return num;
			}
			for (int i = 0; i < a1.Length; i++)
			{
				int num2 = a1[i].CompareTo(a2[i]);
				if (num2 != 0)
				{
					return num2;
				}
			}
			return 0;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001F160 File Offset: 0x0001D360
		public static string GetPrefix(string qualifiedName)
		{
			string text;
			string text2;
			MiscellaneousUtils.GetQualifiedNameParts(qualifiedName, out text, out text2);
			return text;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001F178 File Offset: 0x0001D378
		public static string GetLocalName(string qualifiedName)
		{
			string text;
			string text2;
			MiscellaneousUtils.GetQualifiedNameParts(qualifiedName, out text, out text2);
			return text2;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001F190 File Offset: 0x0001D390
		public static void GetQualifiedNameParts(string qualifiedName, out string prefix, out string localName)
		{
			int num = qualifiedName.IndexOf(':');
			if (num == -1 || num == 0 || qualifiedName.Length - 1 == num)
			{
				prefix = null;
				localName = qualifiedName;
				return;
			}
			prefix = qualifiedName.Substring(0, num);
			localName = qualifiedName.Substring(num + 1);
		}
	}
}
