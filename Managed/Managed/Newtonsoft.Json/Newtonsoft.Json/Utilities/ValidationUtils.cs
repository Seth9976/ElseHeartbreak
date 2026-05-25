using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C3 RID: 195
	internal static class ValidationUtils
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x00020A44 File Offset: 0x0001EC44
		public static void ArgumentNotNullOrEmpty(string value, string parameterName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (value.Length == 0)
			{
				throw new ArgumentException("'{0}' cannot be empty.".FormatWith(CultureInfo.InvariantCulture, new object[] { parameterName }), parameterName);
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00020A88 File Offset: 0x0001EC88
		public static void ArgumentNotNullOrEmptyOrWhitespace(string value, string parameterName)
		{
			ValidationUtils.ArgumentNotNullOrEmpty(value, parameterName);
			if (StringUtils.IsWhiteSpace(value))
			{
				throw new ArgumentException("'{0}' cannot only be whitespace.".FormatWith(CultureInfo.InvariantCulture, new object[] { parameterName }), parameterName);
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00020AC8 File Offset: 0x0001ECC8
		public static void ArgumentTypeIsEnum(Type enumType, string parameterName)
		{
			ValidationUtils.ArgumentNotNull(enumType, "enumType");
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("Type {0} is not an Enum.".FormatWith(CultureInfo.InvariantCulture, new object[] { enumType }), parameterName);
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00020B0C File Offset: 0x0001ED0C
		public static void ArgumentNotNullOrEmpty<T>(ICollection<T> collection, string parameterName)
		{
			ValidationUtils.ArgumentNotNullOrEmpty<T>(collection, parameterName, "Collection '{0}' cannot be empty.".FormatWith(CultureInfo.InvariantCulture, new object[] { parameterName }));
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00020B3B File Offset: 0x0001ED3B
		public static void ArgumentNotNullOrEmpty<T>(ICollection<T> collection, string parameterName, string message)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (collection.Count == 0)
			{
				throw new ArgumentException(message, parameterName);
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00020B58 File Offset: 0x0001ED58
		public static void ArgumentNotNullOrEmpty(ICollection collection, string parameterName)
		{
			ValidationUtils.ArgumentNotNullOrEmpty(collection, parameterName, "Collection '{0}' cannot be empty.".FormatWith(CultureInfo.InvariantCulture, new object[] { parameterName }));
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00020B87 File Offset: 0x0001ED87
		public static void ArgumentNotNullOrEmpty(ICollection collection, string parameterName, string message)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (collection.Count == 0)
			{
				throw new ArgumentException(message, parameterName);
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00020BA3 File Offset: 0x0001EDA3
		public static void ArgumentNotNull(object value, string parameterName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00020BAF File Offset: 0x0001EDAF
		public static void ArgumentNotNegative(int value, string parameterName)
		{
			if (value <= 0)
			{
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException(parameterName, value, "Argument cannot be negative.");
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00020BC7 File Offset: 0x0001EDC7
		public static void ArgumentNotNegative(int value, string parameterName, string message)
		{
			if (value <= 0)
			{
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00020BDB File Offset: 0x0001EDDB
		public static void ArgumentNotZero(int value, string parameterName)
		{
			if (value == 0)
			{
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException(parameterName, value, "Argument cannot be zero.");
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00020BF2 File Offset: 0x0001EDF2
		public static void ArgumentNotZero(int value, string parameterName, string message)
		{
			if (value == 0)
			{
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00020C08 File Offset: 0x0001EE08
		public static void ArgumentIsPositive<T>(T value, string parameterName) where T : struct, IComparable<T>
		{
			if (value.CompareTo(default(T)) != 1)
			{
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException(parameterName, value, "Positive number required.");
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00020C40 File Offset: 0x0001EE40
		public static void ArgumentIsPositive(int value, string parameterName, string message)
		{
			if (value > 0)
			{
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00020C54 File Offset: 0x0001EE54
		public static void ObjectNotDisposed(bool disposed, Type objectType)
		{
			if (disposed)
			{
				throw new ObjectDisposedException(objectType.Name);
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00020C65 File Offset: 0x0001EE65
		public static void ArgumentConditionTrue(bool condition, string parameterName, string message)
		{
			if (!condition)
			{
				throw new ArgumentException(message, parameterName);
			}
		}

		// Token: 0x040002AA RID: 682
		public const string EmailAddressRegex = "^([a-zA-Z0-9_'+*$%\\^&!\\.\\-])+\\@(([a-zA-Z0-9\\-])+\\.)+([a-zA-Z0-9:]{2,4})+$";

		// Token: 0x040002AB RID: 683
		public const string CurrencyRegex = "(^\\$?(?!0,?\\d)\\d{1,3}(,?\\d{3})*(\\.\\d\\d)?)$";

		// Token: 0x040002AC RID: 684
		public const string DateRegex = "^(((0?[1-9]|[12]\\d|3[01])[\\.\\-\\/](0?[13578]|1[02])[\\.\\-\\/]((1[6-9]|[2-9]\\d)?\\d{2}|\\d))|((0?[1-9]|[12]\\d|30)[\\.\\-\\/](0?[13456789]|1[012])[\\.\\-\\/]((1[6-9]|[2-9]\\d)?\\d{2}|\\d))|((0?[1-9]|1\\d|2[0-8])[\\.\\-\\/]0?2[\\.\\-\\/]((1[6-9]|[2-9]\\d)?\\d{2}|\\d))|(29[\\.\\-\\/]0?2[\\.\\-\\/]((1[6-9]|[2-9]\\d)?(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)|00|[048])))$";

		// Token: 0x040002AD RID: 685
		public const string NumericRegex = "\\d*";
	}
}
