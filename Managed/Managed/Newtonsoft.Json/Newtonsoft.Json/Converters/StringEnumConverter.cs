using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000021 RID: 33
	public class StringEnumConverter : JsonConverter
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005EA7 File Offset: 0x000040A7
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00005EAF File Offset: 0x000040AF
		public bool CamelCaseText { get; set; }

		// Token: 0x0600012C RID: 300 RVA: 0x00005EC8 File Offset: 0x000040C8
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			Enum @enum = (Enum)value;
			string text = @enum.ToString("G");
			if (char.IsNumber(text[0]) || text[0] == '-')
			{
				writer.WriteValue(value);
				return;
			}
			BidirectionalDictionary<string, string> enumNameMap = this.GetEnumNameMap(@enum.GetType());
			string text2;
			enumNameMap.TryGetByFirst(text, out text2);
			text2 = text2 ?? text;
			if (this.CamelCaseText)
			{
				string[] array = (from item in text2.Split(new char[] { ',' })
					select StringUtils.ToCamelCase(item.Trim())).ToArray<string>();
				text2 = string.Join(", ", array);
			}
			writer.WriteValue(text2);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005F8C File Offset: 0x0000418C
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			Type type = (ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType);
			if (reader.TokenType == JsonToken.Null)
			{
				if (!ReflectionUtils.IsNullableType(objectType))
				{
					throw new Exception("Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { objectType }));
				}
				return null;
			}
			else
			{
				if (reader.TokenType == JsonToken.String)
				{
					BidirectionalDictionary<string, string> enumNameMap = this.GetEnumNameMap(type);
					string text;
					enumNameMap.TryGetBySecond(reader.Value.ToString(), out text);
					text = text ?? reader.Value.ToString();
					return Enum.Parse(type, text, true);
				}
				if (reader.TokenType == JsonToken.Integer)
				{
					return ConvertUtils.ConvertOrCast(reader.Value, CultureInfo.InvariantCulture, type);
				}
				throw new Exception("Unexpected token when parsing enum. Expected String or Integer, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { reader.TokenType }));
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006070 File Offset: 0x00004270
		private BidirectionalDictionary<string, string> GetEnumNameMap(Type t)
		{
			BidirectionalDictionary<string, string> bidirectionalDictionary;
			if (!this._enumMemberNamesPerType.TryGetValue(t, out bidirectionalDictionary))
			{
				lock (this._enumMemberNamesPerType)
				{
					if (this._enumMemberNamesPerType.TryGetValue(t, out bidirectionalDictionary))
					{
						return bidirectionalDictionary;
					}
					bidirectionalDictionary = new BidirectionalDictionary<string, string>(StringComparer.OrdinalIgnoreCase, StringComparer.OrdinalIgnoreCase);
					foreach (FieldInfo fieldInfo in t.GetFields())
					{
						string name = fieldInfo.Name;
						string text = (from EnumMemberAttribute a in fieldInfo.GetCustomAttributes(typeof(EnumMemberAttribute), true)
							select a.Value).SingleOrDefault<string>() ?? fieldInfo.Name;
						string text2;
						if (bidirectionalDictionary.TryGetBySecond(text, out text2))
						{
							throw new Exception("Enum name '{0}' already exists on enum '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[] { text, t.Name }));
						}
						bidirectionalDictionary.Add(name, text);
					}
					this._enumMemberNamesPerType[t] = bidirectionalDictionary;
				}
				return bidirectionalDictionary;
			}
			return bidirectionalDictionary;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000061A8 File Offset: 0x000043A8
		public override bool CanConvert(Type objectType)
		{
			Type type = (ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType);
			return type.IsEnum;
		}

		// Token: 0x0400007A RID: 122
		private readonly Dictionary<Type, BidirectionalDictionary<string, string>> _enumMemberNamesPerType = new Dictionary<Type, BidirectionalDictionary<string, string>>();
	}
}
