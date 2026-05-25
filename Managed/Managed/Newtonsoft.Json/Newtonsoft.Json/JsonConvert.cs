using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200005F RID: 95
	public static class JsonConvert
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000E544 File Offset: 0x0000C744
		public static string ToString(DateTime value)
		{
			string text;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
			{
				JsonConvert.WriteDateTimeString(stringWriter, value, value.GetUtcOffset(), value.Kind);
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000E594 File Offset: 0x0000C794
		public static string ToString(DateTimeOffset value)
		{
			string text;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
			{
				JsonConvert.WriteDateTimeString(stringWriter, value.UtcDateTime, value.Offset, DateTimeKind.Local);
				text = stringWriter.ToString();
			}
			return text;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000E5E4 File Offset: 0x0000C7E4
		internal static void WriteDateTimeString(TextWriter writer, DateTime value)
		{
			JsonConvert.WriteDateTimeString(writer, value, value.GetUtcOffset(), value.Kind);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		internal static void WriteDateTimeString(TextWriter writer, DateTime value, TimeSpan offset, DateTimeKind kind)
		{
			long num = JsonConvert.ConvertDateTimeToJavaScriptTicks(value, offset);
			writer.Write("\"\\/Date(");
			writer.Write(num);
			switch (kind)
			{
			case DateTimeKind.Unspecified:
			case DateTimeKind.Local:
			{
				writer.Write((offset.Ticks >= 0L) ? "+" : "-");
				int num2 = Math.Abs(offset.Hours);
				if (num2 < 10)
				{
					writer.Write(0);
				}
				writer.Write(num2);
				int num3 = Math.Abs(offset.Minutes);
				if (num3 < 10)
				{
					writer.Write(0);
				}
				writer.Write(num3);
				break;
			}
			}
			writer.Write(")\\/\"");
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000E6A1 File Offset: 0x0000C8A1
		private static long ToUniversalTicks(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime.Ticks;
			}
			return JsonConvert.ToUniversalTicks(dateTime, dateTime.GetUtcOffset());
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
		private static long ToUniversalTicks(DateTime dateTime, TimeSpan offset)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime.Ticks;
			}
			long num = dateTime.Ticks - offset.Ticks;
			if (num > 3155378975999999999L)
			{
				return 3155378975999999999L;
			}
			if (num < 0L)
			{
				return 0L;
			}
			return num;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000E714 File Offset: 0x0000C914
		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime, TimeSpan offset)
		{
			long num = JsonConvert.ToUniversalTicks(dateTime, offset);
			return JsonConvert.UniversialTicksToJavaScriptTicks(num);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000E72F File Offset: 0x0000C92F
		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime)
		{
			return JsonConvert.ConvertDateTimeToJavaScriptTicks(dateTime, true);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000E738 File Offset: 0x0000C938
		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime, bool convertToUtc)
		{
			long num = (convertToUtc ? JsonConvert.ToUniversalTicks(dateTime) : dateTime.Ticks);
			return JsonConvert.UniversialTicksToJavaScriptTicks(num);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000E760 File Offset: 0x0000C960
		private static long UniversialTicksToJavaScriptTicks(long universialTicks)
		{
			return (universialTicks - JsonConvert.InitialJavaScriptDateTicks) / 10000L;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000E780 File Offset: 0x0000C980
		internal static DateTime ConvertJavaScriptTicksToDateTime(long javaScriptTicks)
		{
			DateTime dateTime = new DateTime(javaScriptTicks * 10000L + JsonConvert.InitialJavaScriptDateTicks, DateTimeKind.Utc);
			return dateTime;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000E7A4 File Offset: 0x0000C9A4
		public static string ToString(bool value)
		{
			if (!value)
			{
				return JsonConvert.False;
			}
			return JsonConvert.True;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000E7B4 File Offset: 0x0000C9B4
		public static string ToString(char value)
		{
			return JsonConvert.ToString(char.ToString(value));
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000E7C1 File Offset: 0x0000C9C1
		public static string ToString(Enum value)
		{
			return value.ToString("D");
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000E7CE File Offset: 0x0000C9CE
		public static string ToString(int value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000E7DD File Offset: 0x0000C9DD
		public static string ToString(short value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		[CLSCompliant(false)]
		public static string ToString(ushort value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000E7FB File Offset: 0x0000C9FB
		[CLSCompliant(false)]
		public static string ToString(uint value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000E80A File Offset: 0x0000CA0A
		public static string ToString(long value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000E819 File Offset: 0x0000CA19
		[CLSCompliant(false)]
		public static string ToString(ulong value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000E828 File Offset: 0x0000CA28
		public static string ToString(float value)
		{
			return JsonConvert.EnsureDecimalPlace((double)value, value.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000E842 File Offset: 0x0000CA42
		public static string ToString(double value)
		{
			return JsonConvert.EnsureDecimalPlace(value, value.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000E85B File Offset: 0x0000CA5B
		private static string EnsureDecimalPlace(double value, string text)
		{
			if (double.IsNaN(value) || double.IsInfinity(value) || text.IndexOf('.') != -1 || text.IndexOf('E') != -1)
			{
				return text;
			}
			return text + ".0";
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000E890 File Offset: 0x0000CA90
		private static string EnsureDecimalPlace(string text)
		{
			if (text.IndexOf('.') != -1)
			{
				return text;
			}
			return text + ".0";
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000E8AA File Offset: 0x0000CAAA
		public static string ToString(byte value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000E8B9 File Offset: 0x0000CAB9
		[CLSCompliant(false)]
		public static string ToString(sbyte value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		public static string ToString(decimal value)
		{
			return JsonConvert.EnsureDecimalPlace(value.ToString(null, CultureInfo.InvariantCulture));
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000E8DC File Offset: 0x0000CADC
		public static string ToString(Guid value)
		{
			return '"' + value.ToString("D", CultureInfo.InvariantCulture) + '"';
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000E902 File Offset: 0x0000CB02
		public static string ToString(TimeSpan value)
		{
			return '"' + value.ToString() + '"';
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000E924 File Offset: 0x0000CB24
		public static string ToString(Uri value)
		{
			if (value == null)
			{
				return JsonConvert.Null;
			}
			return JsonConvert.ToString(value.ToString());
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000E940 File Offset: 0x0000CB40
		public static string ToString(string value)
		{
			return JsonConvert.ToString(value, '"');
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000E94A File Offset: 0x0000CB4A
		public static string ToString(string value, char delimter)
		{
			return JavaScriptUtils.ToEscapedJavaScriptString(value, delimter, true);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000E954 File Offset: 0x0000CB54
		public static string ToString(object value)
		{
			if (value == null)
			{
				return JsonConvert.Null;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				switch (convertible.GetTypeCode())
				{
				case TypeCode.DBNull:
					return JsonConvert.Null;
				case TypeCode.Boolean:
					return JsonConvert.ToString(convertible.ToBoolean(CultureInfo.InvariantCulture));
				case TypeCode.Char:
					return JsonConvert.ToString(convertible.ToChar(CultureInfo.InvariantCulture));
				case TypeCode.SByte:
					return JsonConvert.ToString(convertible.ToSByte(CultureInfo.InvariantCulture));
				case TypeCode.Byte:
					return JsonConvert.ToString(convertible.ToByte(CultureInfo.InvariantCulture));
				case TypeCode.Int16:
					return JsonConvert.ToString(convertible.ToInt16(CultureInfo.InvariantCulture));
				case TypeCode.UInt16:
					return JsonConvert.ToString(convertible.ToUInt16(CultureInfo.InvariantCulture));
				case TypeCode.Int32:
					return JsonConvert.ToString(convertible.ToInt32(CultureInfo.InvariantCulture));
				case TypeCode.UInt32:
					return JsonConvert.ToString(convertible.ToUInt32(CultureInfo.InvariantCulture));
				case TypeCode.Int64:
					return JsonConvert.ToString(convertible.ToInt64(CultureInfo.InvariantCulture));
				case TypeCode.UInt64:
					return JsonConvert.ToString(convertible.ToUInt64(CultureInfo.InvariantCulture));
				case TypeCode.Single:
					return JsonConvert.ToString(convertible.ToSingle(CultureInfo.InvariantCulture));
				case TypeCode.Double:
					return JsonConvert.ToString(convertible.ToDouble(CultureInfo.InvariantCulture));
				case TypeCode.Decimal:
					return JsonConvert.ToString(convertible.ToDecimal(CultureInfo.InvariantCulture));
				case TypeCode.DateTime:
					return JsonConvert.ToString(convertible.ToDateTime(CultureInfo.InvariantCulture));
				case TypeCode.String:
					return JsonConvert.ToString(convertible.ToString(CultureInfo.InvariantCulture));
				}
			}
			else
			{
				if (value is DateTimeOffset)
				{
					return JsonConvert.ToString((DateTimeOffset)value);
				}
				if (value is Guid)
				{
					return JsonConvert.ToString((Guid)value);
				}
				if (value is Uri)
				{
					return JsonConvert.ToString((Uri)value);
				}
				if (value is TimeSpan)
				{
					return JsonConvert.ToString((TimeSpan)value);
				}
			}
			throw new ArgumentException("Unsupported type: {0}. Use the JsonSerializer class to get the object's JSON representation.".FormatWith(CultureInfo.InvariantCulture, new object[] { value.GetType() }));
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		private static bool IsJsonPrimitiveTypeCode(TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.DBNull:
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
			case TypeCode.DateTime:
			case TypeCode.String:
				return true;
			}
			return false;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		internal static bool IsJsonPrimitiveType(Type type)
		{
			if (ReflectionUtils.IsNullableType(type))
			{
				type = Nullable.GetUnderlyingType(type);
			}
			return type == typeof(DateTimeOffset) || type == typeof(byte[]) || type == typeof(Uri) || type == typeof(TimeSpan) || type == typeof(Guid) || JsonConvert.IsJsonPrimitiveTypeCode(Type.GetTypeCode(type));
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000EC20 File Offset: 0x0000CE20
		internal static bool IsJsonPrimitive(object value)
		{
			if (value == null)
			{
				return true;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return JsonConvert.IsJsonPrimitiveTypeCode(convertible.GetTypeCode());
			}
			return value is DateTimeOffset || value is byte[] || value is Uri || value is TimeSpan || value is Guid;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000EC7B File Offset: 0x0000CE7B
		public static string SerializeObject(object value)
		{
			return JsonConvert.SerializeObject(value, Formatting.None, null);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000EC85 File Offset: 0x0000CE85
		public static string SerializeObject(object value, Formatting formatting)
		{
			return JsonConvert.SerializeObject(value, formatting, null);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000EC8F File Offset: 0x0000CE8F
		public static string SerializeObject(object value, params JsonConverter[] converters)
		{
			return JsonConvert.SerializeObject(value, Formatting.None, converters);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		public static string SerializeObject(object value, Formatting formatting, params JsonConverter[] converters)
		{
			JsonSerializerSettings jsonSerializerSettings = ((converters != null && converters.Length > 0) ? new JsonSerializerSettings
			{
				Converters = converters
			} : null);
			return JsonConvert.SerializeObject(value, formatting, jsonSerializerSettings);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000ECCC File Offset: 0x0000CECC
		public static string SerializeObject(object value, Formatting formatting, JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.Create(settings);
			StringBuilder stringBuilder = new StringBuilder(128);
			StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture);
			using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
			{
				jsonTextWriter.Formatting = formatting;
				jsonSerializer.Serialize(jsonTextWriter, value);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000ED30 File Offset: 0x0000CF30
		public static object DeserializeObject(string value)
		{
			return JsonConvert.DeserializeObject(value, null, null);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000ED3A File Offset: 0x0000CF3A
		public static object DeserializeObject(string value, JsonSerializerSettings settings)
		{
			return JsonConvert.DeserializeObject(value, null, settings);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000ED44 File Offset: 0x0000CF44
		public static object DeserializeObject(string value, Type type)
		{
			return JsonConvert.DeserializeObject(value, type, null);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000ED4E File Offset: 0x0000CF4E
		public static T DeserializeObject<T>(string value)
		{
			return JsonConvert.DeserializeObject<T>(value, null);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000ED57 File Offset: 0x0000CF57
		public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject)
		{
			return JsonConvert.DeserializeObject<T>(value);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000ED5F File Offset: 0x0000CF5F
		public static T DeserializeObject<T>(string value, params JsonConverter[] converters)
		{
			return (T)((object)JsonConvert.DeserializeObject(value, typeof(T), converters));
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000ED77 File Offset: 0x0000CF77
		public static T DeserializeObject<T>(string value, JsonSerializerSettings settings)
		{
			return (T)((object)JsonConvert.DeserializeObject(value, typeof(T), settings));
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000ED90 File Offset: 0x0000CF90
		public static object DeserializeObject(string value, Type type, params JsonConverter[] converters)
		{
			JsonSerializerSettings jsonSerializerSettings = ((converters != null && converters.Length > 0) ? new JsonSerializerSettings
			{
				Converters = converters
			} : null);
			return JsonConvert.DeserializeObject(value, type, jsonSerializerSettings);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		public static object DeserializeObject(string value, Type type, JsonSerializerSettings settings)
		{
			StringReader stringReader = new StringReader(value);
			JsonSerializer jsonSerializer = JsonSerializer.Create(settings);
			object obj;
			using (JsonReader jsonReader = new JsonTextReader(stringReader))
			{
				obj = jsonSerializer.Deserialize(jsonReader, type);
				if (jsonReader.Read() && jsonReader.TokenType != JsonToken.Comment)
				{
					throw new JsonSerializationException("Additional text found in JSON string after finishing deserializing object.");
				}
			}
			return obj;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000EE24 File Offset: 0x0000D024
		public static void PopulateObject(string value, object target)
		{
			JsonConvert.PopulateObject(value, target, null);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000EE30 File Offset: 0x0000D030
		public static void PopulateObject(string value, object target, JsonSerializerSettings settings)
		{
			StringReader stringReader = new StringReader(value);
			JsonSerializer jsonSerializer = JsonSerializer.Create(settings);
			using (JsonReader jsonReader = new JsonTextReader(stringReader))
			{
				jsonSerializer.Populate(jsonReader, target);
				if (jsonReader.Read() && jsonReader.TokenType != JsonToken.Comment)
				{
					throw new JsonSerializationException("Additional text found in JSON string after finishing deserializing object.");
				}
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000EE94 File Offset: 0x0000D094
		public static string SerializeXmlNode(XmlNode node)
		{
			return JsonConvert.SerializeXmlNode(node, Formatting.None);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000EEA0 File Offset: 0x0000D0A0
		public static string SerializeXmlNode(XmlNode node, Formatting formatting)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter();
			return JsonConvert.SerializeObject(node, formatting, new JsonConverter[] { xmlNodeConverter });
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000EEC8 File Offset: 0x0000D0C8
		public static string SerializeXmlNode(XmlNode node, Formatting formatting, bool omitRootObject)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter
			{
				OmitRootObject = omitRootObject
			};
			return JsonConvert.SerializeObject(node, formatting, new JsonConverter[] { xmlNodeConverter });
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000EEF7 File Offset: 0x0000D0F7
		public static XmlDocument DeserializeXmlNode(string value)
		{
			return JsonConvert.DeserializeXmlNode(value, null);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000EF00 File Offset: 0x0000D100
		public static XmlDocument DeserializeXmlNode(string value, string deserializeRootElementName)
		{
			return JsonConvert.DeserializeXmlNode(value, deserializeRootElementName, false);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000EF0C File Offset: 0x0000D10C
		public static XmlDocument DeserializeXmlNode(string value, string deserializeRootElementName, bool writeArrayAttribute)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter();
			xmlNodeConverter.DeserializeRootElementName = deserializeRootElementName;
			xmlNodeConverter.WriteArrayAttribute = writeArrayAttribute;
			return (XmlDocument)JsonConvert.DeserializeObject(value, typeof(XmlDocument), new JsonConverter[] { xmlNodeConverter });
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000EF4E File Offset: 0x0000D14E
		public static string SerializeXNode(XObject node)
		{
			return JsonConvert.SerializeXNode(node, Formatting.None);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000EF57 File Offset: 0x0000D157
		public static string SerializeXNode(XObject node, Formatting formatting)
		{
			return JsonConvert.SerializeXNode(node, formatting, false);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000EF64 File Offset: 0x0000D164
		public static string SerializeXNode(XObject node, Formatting formatting, bool omitRootObject)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter
			{
				OmitRootObject = omitRootObject
			};
			return JsonConvert.SerializeObject(node, formatting, new JsonConverter[] { xmlNodeConverter });
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000EF93 File Offset: 0x0000D193
		public static XDocument DeserializeXNode(string value)
		{
			return JsonConvert.DeserializeXNode(value, null);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000EF9C File Offset: 0x0000D19C
		public static XDocument DeserializeXNode(string value, string deserializeRootElementName)
		{
			return JsonConvert.DeserializeXNode(value, deserializeRootElementName, false);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		public static XDocument DeserializeXNode(string value, string deserializeRootElementName, bool writeArrayAttribute)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter();
			xmlNodeConverter.DeserializeRootElementName = deserializeRootElementName;
			xmlNodeConverter.WriteArrayAttribute = writeArrayAttribute;
			return (XDocument)JsonConvert.DeserializeObject(value, typeof(XDocument), new JsonConverter[] { xmlNodeConverter });
		}

		// Token: 0x04000124 RID: 292
		public static readonly string True = "true";

		// Token: 0x04000125 RID: 293
		public static readonly string False = "false";

		// Token: 0x04000126 RID: 294
		public static readonly string Null = "null";

		// Token: 0x04000127 RID: 295
		public static readonly string Undefined = "undefined";

		// Token: 0x04000128 RID: 296
		public static readonly string PositiveInfinity = "Infinity";

		// Token: 0x04000129 RID: 297
		public static readonly string NegativeInfinity = "-Infinity";

		// Token: 0x0400012A RID: 298
		public static readonly string NaN = "NaN";

		// Token: 0x0400012B RID: 299
		internal static readonly long InitialJavaScriptDateTicks = 621355968000000000L;
	}
}
