using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000AA RID: 170
	internal static class ConvertUtils
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x0001C0E0 File Offset: 0x0001A2E0
		private static Func<object, object> CreateCastConverter(ConvertUtils.TypeConvertKey t)
		{
			MethodInfo methodInfo = t.TargetType.GetMethod("op_Implicit", new Type[] { t.InitialType });
			if (methodInfo == null)
			{
				methodInfo = t.TargetType.GetMethod("op_Explicit", new Type[] { t.InitialType });
			}
			if (methodInfo == null)
			{
				return null;
			}
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
			return (object o) => call(null, new object[] { o });
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001C160 File Offset: 0x0001A360
		public static bool CanConvertType(Type initialType, Type targetType, bool allowTypeNameToString)
		{
			ValidationUtils.ArgumentNotNull(initialType, "initialType");
			ValidationUtils.ArgumentNotNull(targetType, "targetType");
			if (ReflectionUtils.IsNullableType(targetType))
			{
				targetType = Nullable.GetUnderlyingType(targetType);
			}
			if (targetType == initialType)
			{
				return true;
			}
			if (typeof(IConvertible).IsAssignableFrom(initialType) && typeof(IConvertible).IsAssignableFrom(targetType))
			{
				return true;
			}
			if (initialType == typeof(DateTime) && targetType == typeof(DateTimeOffset))
			{
				return true;
			}
			if (initialType == typeof(Guid) && (targetType == typeof(Guid) || targetType == typeof(string)))
			{
				return true;
			}
			if (initialType == typeof(Type) && targetType == typeof(string))
			{
				return true;
			}
			TypeConverter converter = ConvertUtils.GetConverter(initialType);
			if (converter != null && !ConvertUtils.IsComponentConverter(converter) && converter.CanConvertTo(targetType) && (allowTypeNameToString || converter.GetType() != typeof(TypeConverter)))
			{
				return true;
			}
			TypeConverter converter2 = ConvertUtils.GetConverter(targetType);
			return (converter2 != null && !ConvertUtils.IsComponentConverter(converter2) && converter2.CanConvertFrom(initialType)) || (initialType == typeof(DBNull) && ReflectionUtils.IsNullable(targetType));
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001C287 File Offset: 0x0001A487
		private static bool IsComponentConverter(TypeConverter converter)
		{
			return converter is ComponentConverter;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001C292 File Offset: 0x0001A492
		public static T Convert<T>(object initialValue)
		{
			return ConvertUtils.Convert<T>(initialValue, CultureInfo.CurrentCulture);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001C29F File Offset: 0x0001A49F
		public static T Convert<T>(object initialValue, CultureInfo culture)
		{
			return (T)((object)ConvertUtils.Convert(initialValue, culture, typeof(T)));
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001C2B8 File Offset: 0x0001A4B8
		public static object Convert(object initialValue, CultureInfo culture, Type targetType)
		{
			if (initialValue == null)
			{
				throw new ArgumentNullException("initialValue");
			}
			if (ReflectionUtils.IsNullableType(targetType))
			{
				targetType = Nullable.GetUnderlyingType(targetType);
			}
			Type type = initialValue.GetType();
			if (targetType == type)
			{
				return initialValue;
			}
			if (initialValue is string && typeof(Type).IsAssignableFrom(targetType))
			{
				return Type.GetType((string)initialValue, true);
			}
			if (targetType.IsInterface || targetType.IsGenericTypeDefinition || targetType.IsAbstract)
			{
				throw new ArgumentException("Target type {0} is not a value type or a non-abstract class.".FormatWith(CultureInfo.InvariantCulture, new object[] { targetType }), "targetType");
			}
			if (initialValue is IConvertible && typeof(IConvertible).IsAssignableFrom(targetType))
			{
				if (targetType.IsEnum)
				{
					if (initialValue is string)
					{
						return Enum.Parse(targetType, initialValue.ToString(), true);
					}
					if (ConvertUtils.IsInteger(initialValue))
					{
						return Enum.ToObject(targetType, initialValue);
					}
				}
				return global::System.Convert.ChangeType(initialValue, targetType, culture);
			}
			if (initialValue is DateTime && targetType == typeof(DateTimeOffset))
			{
				return new DateTimeOffset((DateTime)initialValue);
			}
			if (initialValue is string)
			{
				if (targetType == typeof(Guid))
				{
					return new Guid((string)initialValue);
				}
				if (targetType == typeof(Uri))
				{
					return new Uri((string)initialValue);
				}
				if (targetType == typeof(TimeSpan))
				{
					return TimeSpan.Parse((string)initialValue);
				}
			}
			TypeConverter converter = ConvertUtils.GetConverter(type);
			if (converter != null && converter.CanConvertTo(targetType))
			{
				return converter.ConvertTo(null, culture, initialValue, targetType);
			}
			TypeConverter converter2 = ConvertUtils.GetConverter(targetType);
			if (converter2 != null && converter2.CanConvertFrom(type))
			{
				return converter2.ConvertFrom(null, culture, initialValue);
			}
			if (initialValue == DBNull.Value)
			{
				if (ReflectionUtils.IsNullable(targetType))
				{
					return ConvertUtils.EnsureTypeAssignable(null, type, targetType);
				}
				throw new Exception("Can not convert null {0} into non-nullable {1}.".FormatWith(CultureInfo.InvariantCulture, new object[] { type, targetType }));
			}
			else
			{
				if (initialValue is INullable)
				{
					return ConvertUtils.EnsureTypeAssignable(ConvertUtils.ToValue((INullable)initialValue), type, targetType);
				}
				throw new Exception("Can not convert from {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, new object[] { type, targetType }));
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001C4E9 File Offset: 0x0001A6E9
		public static bool TryConvert<T>(object initialValue, out T convertedValue)
		{
			return ConvertUtils.TryConvert<T>(initialValue, CultureInfo.CurrentCulture, out convertedValue);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001C530 File Offset: 0x0001A730
		public static bool TryConvert<T>(object initialValue, CultureInfo culture, out T convertedValue)
		{
			return MiscellaneousUtils.TryAction<T>(delegate
			{
				object obj;
				ConvertUtils.TryConvert(initialValue, CultureInfo.CurrentCulture, typeof(T), out obj);
				return (T)((object)obj);
			}, out convertedValue);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001C580 File Offset: 0x0001A780
		public static bool TryConvert(object initialValue, CultureInfo culture, Type targetType, out object convertedValue)
		{
			return MiscellaneousUtils.TryAction<object>(() => ConvertUtils.Convert(initialValue, culture, targetType), out convertedValue);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001C5BA File Offset: 0x0001A7BA
		public static T ConvertOrCast<T>(object initialValue)
		{
			return ConvertUtils.ConvertOrCast<T>(initialValue, CultureInfo.CurrentCulture);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001C5C7 File Offset: 0x0001A7C7
		public static T ConvertOrCast<T>(object initialValue, CultureInfo culture)
		{
			return (T)((object)ConvertUtils.ConvertOrCast(initialValue, culture, typeof(T)));
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		public static object ConvertOrCast(object initialValue, CultureInfo culture, Type targetType)
		{
			if (targetType == typeof(object))
			{
				return initialValue;
			}
			if (initialValue == null && ReflectionUtils.IsNullable(targetType))
			{
				return null;
			}
			object obj;
			if (ConvertUtils.TryConvert(initialValue, culture, targetType, out obj))
			{
				return obj;
			}
			return ConvertUtils.EnsureTypeAssignable(initialValue, ReflectionUtils.GetObjectType(initialValue), targetType);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001C624 File Offset: 0x0001A824
		public static bool TryConvertOrCast<T>(object initialValue, out T convertedValue)
		{
			return ConvertUtils.TryConvertOrCast<T>(initialValue, CultureInfo.CurrentCulture, out convertedValue);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001C66C File Offset: 0x0001A86C
		public static bool TryConvertOrCast<T>(object initialValue, CultureInfo culture, out T convertedValue)
		{
			return MiscellaneousUtils.TryAction<T>(delegate
			{
				object obj;
				ConvertUtils.TryConvertOrCast(initialValue, CultureInfo.CurrentCulture, typeof(T), out obj);
				return (T)((object)obj);
			}, out convertedValue);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001C6BC File Offset: 0x0001A8BC
		public static bool TryConvertOrCast(object initialValue, CultureInfo culture, Type targetType, out object convertedValue)
		{
			return MiscellaneousUtils.TryAction<object>(() => ConvertUtils.ConvertOrCast(initialValue, culture, targetType), out convertedValue);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001C6F8 File Offset: 0x0001A8F8
		private static object EnsureTypeAssignable(object value, Type initialType, Type targetType)
		{
			Type type = ((value != null) ? value.GetType() : null);
			if (value != null)
			{
				if (targetType.IsAssignableFrom(type))
				{
					return value;
				}
				Func<object, object> func = ConvertUtils.CastConverters.Get(new ConvertUtils.TypeConvertKey(type, targetType));
				if (func != null)
				{
					return func(value);
				}
			}
			else if (ReflectionUtils.IsNullable(targetType))
			{
				return null;
			}
			throw new Exception("Could not cast or convert from {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				(initialType != null) ? initialType.ToString() : "{null}",
				targetType
			}));
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001C77C File Offset: 0x0001A97C
		public static object ToValue(INullable nullableValue)
		{
			if (nullableValue == null)
			{
				return null;
			}
			if (nullableValue is SqlInt32)
			{
				return ConvertUtils.ToValue((SqlInt32)nullableValue);
			}
			if (nullableValue is SqlInt64)
			{
				return ConvertUtils.ToValue((SqlInt64)nullableValue);
			}
			if (nullableValue is SqlBoolean)
			{
				return ConvertUtils.ToValue((SqlBoolean)nullableValue);
			}
			if (nullableValue is SqlString)
			{
				return ConvertUtils.ToValue((SqlString)nullableValue);
			}
			if (nullableValue is SqlDateTime)
			{
				return ConvertUtils.ToValue((SqlDateTime)nullableValue);
			}
			throw new Exception("Unsupported INullable type: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { nullableValue.GetType() }));
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001C830 File Offset: 0x0001AA30
		internal static TypeConverter GetConverter(Type t)
		{
			return JsonTypeReflector.GetTypeConverter(t);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001C838 File Offset: 0x0001AA38
		public static bool IsInteger(object value)
		{
			switch (global::System.Convert.GetTypeCode(value))
			{
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0400026F RID: 623
		private static readonly ThreadSafeStore<ConvertUtils.TypeConvertKey, Func<object, object>> CastConverters = new ThreadSafeStore<ConvertUtils.TypeConvertKey, Func<object, object>>(new Func<ConvertUtils.TypeConvertKey, Func<object, object>>(ConvertUtils.CreateCastConverter));

		// Token: 0x020000AB RID: 171
		internal struct TypeConvertKey : IEquatable<ConvertUtils.TypeConvertKey>
		{
			// Token: 0x17000189 RID: 393
			// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0001C891 File Offset: 0x0001AA91
			public Type InitialType
			{
				get
				{
					return this._initialType;
				}
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0001C899 File Offset: 0x0001AA99
			public Type TargetType
			{
				get
				{
					return this._targetType;
				}
			}

			// Token: 0x060007D2 RID: 2002 RVA: 0x0001C8A1 File Offset: 0x0001AAA1
			public TypeConvertKey(Type initialType, Type targetType)
			{
				this._initialType = initialType;
				this._targetType = targetType;
			}

			// Token: 0x060007D3 RID: 2003 RVA: 0x0001C8B1 File Offset: 0x0001AAB1
			public override int GetHashCode()
			{
				return this._initialType.GetHashCode() ^ this._targetType.GetHashCode();
			}

			// Token: 0x060007D4 RID: 2004 RVA: 0x0001C8CA File Offset: 0x0001AACA
			public override bool Equals(object obj)
			{
				return obj is ConvertUtils.TypeConvertKey && this.Equals((ConvertUtils.TypeConvertKey)obj);
			}

			// Token: 0x060007D5 RID: 2005 RVA: 0x0001C8E2 File Offset: 0x0001AAE2
			public bool Equals(ConvertUtils.TypeConvertKey other)
			{
				return this._initialType == other._initialType && this._targetType == other._targetType;
			}

			// Token: 0x04000270 RID: 624
			private readonly Type _initialType;

			// Token: 0x04000271 RID: 625
			private readonly Type _targetType;
		}
	}
}
