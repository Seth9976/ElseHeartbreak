using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000B2 RID: 178
	internal static class EnumUtils
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x0001D435 File Offset: 0x0001B635
		public static T Parse<T>(string enumMemberName) where T : struct
		{
			return EnumUtils.Parse<T>(enumMemberName, false);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001D43E File Offset: 0x0001B63E
		public static T Parse<T>(string enumMemberName, bool ignoreCase) where T : struct
		{
			ValidationUtils.ArgumentTypeIsEnum(typeof(T), "T");
			return (T)((object)Enum.Parse(typeof(T), enumMemberName, ignoreCase));
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001D488 File Offset: 0x0001B688
		public static bool TryParse<T>(string enumMemberName, bool ignoreCase, out T value) where T : struct
		{
			ValidationUtils.ArgumentTypeIsEnum(typeof(T), "T");
			return MiscellaneousUtils.TryAction<T>(() => EnumUtils.Parse<T>(enumMemberName, ignoreCase), out value);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001D4DC File Offset: 0x0001B6DC
		public static IList<T> GetFlagsValues<T>(T value) where T : struct
		{
			Type typeFromHandle = typeof(T);
			if (!typeFromHandle.IsDefined(typeof(FlagsAttribute), false))
			{
				throw new Exception("Enum type {0} is not a set of flags.".FormatWith(CultureInfo.InvariantCulture, new object[] { typeFromHandle }));
			}
			Type underlyingType = Enum.GetUnderlyingType(value.GetType());
			ulong num = Convert.ToUInt64(value, CultureInfo.InvariantCulture);
			EnumValues<ulong> namesAndValues = EnumUtils.GetNamesAndValues<T>();
			IList<T> list = new List<T>();
			foreach (EnumValue<ulong> enumValue in namesAndValues)
			{
				if ((num & enumValue.Value) == enumValue.Value && enumValue.Value != 0UL)
				{
					list.Add((T)((object)Convert.ChangeType(enumValue.Value, underlyingType, CultureInfo.CurrentCulture)));
				}
			}
			if (list.Count == 0 && namesAndValues.SingleOrDefault((EnumValue<ulong> v) => v.Value == 0UL) != null)
			{
				list.Add(default(T));
			}
			return list;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001D608 File Offset: 0x0001B808
		public static EnumValues<ulong> GetNamesAndValues<T>() where T : struct
		{
			return EnumUtils.GetNamesAndValues<ulong>(typeof(T));
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001D619 File Offset: 0x0001B819
		public static EnumValues<TUnderlyingType> GetNamesAndValues<TEnum, TUnderlyingType>() where TEnum : struct where TUnderlyingType : struct
		{
			return EnumUtils.GetNamesAndValues<TUnderlyingType>(typeof(TEnum));
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001D62C File Offset: 0x0001B82C
		public static EnumValues<TUnderlyingType> GetNamesAndValues<TUnderlyingType>(Type enumType) where TUnderlyingType : struct
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			ValidationUtils.ArgumentTypeIsEnum(enumType, "enumType");
			IList<object> values = EnumUtils.GetValues(enumType);
			IList<string> names = EnumUtils.GetNames(enumType);
			EnumValues<TUnderlyingType> enumValues = new EnumValues<TUnderlyingType>();
			for (int i = 0; i < values.Count; i++)
			{
				try
				{
					enumValues.Add(new EnumValue<TUnderlyingType>(names[i], (TUnderlyingType)((object)Convert.ChangeType(values[i], typeof(TUnderlyingType), CultureInfo.CurrentCulture))));
				}
				catch (OverflowException ex)
				{
					throw new Exception(string.Format(CultureInfo.InvariantCulture, "Value from enum with the underlying type of {0} cannot be added to dictionary with a value type of {1}. Value was too large: {2}", new object[]
					{
						Enum.GetUnderlyingType(enumType),
						typeof(TUnderlyingType),
						Convert.ToUInt64(values[i], CultureInfo.InvariantCulture)
					}), ex);
				}
			}
			return enumValues;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001D718 File Offset: 0x0001B918
		public static IList<T> GetValues<T>()
		{
			return EnumUtils.GetValues(typeof(T)).Cast<T>().ToList<T>();
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001D73C File Offset: 0x0001B93C
		public static IList<object> GetValues(Type enumType)
		{
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("Type '" + enumType.Name + "' is not an enum.");
			}
			List<object> list = new List<object>();
			IEnumerable<FieldInfo> enumerable = from field in enumType.GetFields()
				where field.IsLiteral
				select field;
			foreach (FieldInfo fieldInfo in enumerable)
			{
				object value = fieldInfo.GetValue(enumType);
				list.Add(value);
			}
			return list;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001D7E4 File Offset: 0x0001B9E4
		public static IList<string> GetNames<T>()
		{
			return EnumUtils.GetNames(typeof(T));
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001D800 File Offset: 0x0001BA00
		public static IList<string> GetNames(Type enumType)
		{
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("Type '" + enumType.Name + "' is not an enum.");
			}
			List<string> list = new List<string>();
			IEnumerable<FieldInfo> enumerable = from field in enumType.GetFields()
				where field.IsLiteral
				select field;
			foreach (FieldInfo fieldInfo in enumerable)
			{
				list.Add(fieldInfo.Name);
			}
			return list;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001D8A0 File Offset: 0x0001BAA0
		public static TEnumType GetMaximumValue<TEnumType>(Type enumType) where TEnumType : IConvertible, IComparable<TEnumType>
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			Type underlyingType = Enum.GetUnderlyingType(enumType);
			if (!typeof(TEnumType).IsAssignableFrom(underlyingType))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "TEnumType is not assignable from the enum's underlying type of {0}.", new object[] { underlyingType.Name }));
			}
			ulong num = 0UL;
			IList<object> values = EnumUtils.GetValues(enumType);
			if (enumType.IsDefined(typeof(FlagsAttribute), false))
			{
				using (IEnumerator<object> enumerator = values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						TEnumType tenumType = (TEnumType)((object)obj);
						num |= tenumType.ToUInt64(CultureInfo.InvariantCulture);
					}
					goto IL_0102;
				}
			}
			foreach (object obj2 in values)
			{
				TEnumType tenumType2 = (TEnumType)((object)obj2);
				ulong num2 = tenumType2.ToUInt64(CultureInfo.InvariantCulture);
				if (num.CompareTo(num2) == -1)
				{
					num = num2;
				}
			}
			IL_0102:
			return (TEnumType)((object)Convert.ChangeType(num, typeof(TEnumType), CultureInfo.InvariantCulture));
		}
	}
}
