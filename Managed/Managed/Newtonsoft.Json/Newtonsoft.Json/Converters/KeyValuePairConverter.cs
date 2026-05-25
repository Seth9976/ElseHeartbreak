using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200001E RID: 30
	public class KeyValuePairConverter : JsonConverter
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00005A00 File Offset: 0x00003C00
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Type type = value.GetType();
			PropertyInfo property = type.GetProperty("Key");
			PropertyInfo property2 = type.GetProperty("Value");
			writer.WriteStartObject();
			writer.WritePropertyName("Key");
			serializer.Serialize(writer, ReflectionUtils.GetMemberValue(property, value));
			writer.WritePropertyName("Value");
			serializer.Serialize(writer, ReflectionUtils.GetMemberValue(property2, value));
			writer.WriteEndObject();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005A6C File Offset: 0x00003C6C
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			bool flag = ReflectionUtils.IsNullableType(objectType);
			if (reader.TokenType != JsonToken.Null)
			{
				Type type = (flag ? Nullable.GetUnderlyingType(objectType) : objectType);
				IList<Type> genericArguments = type.GetGenericArguments();
				Type type2 = genericArguments[0];
				Type type3 = genericArguments[1];
				object obj = null;
				object obj2 = null;
				reader.Read();
				while (reader.TokenType == JsonToken.PropertyName)
				{
					string text;
					if ((text = reader.Value.ToString()) == null)
					{
						goto IL_00AC;
					}
					if (!(text == "Key"))
					{
						if (!(text == "Value"))
						{
							goto IL_00AC;
						}
						reader.Read();
						obj2 = serializer.Deserialize(reader, type3);
					}
					else
					{
						reader.Read();
						obj = serializer.Deserialize(reader, type2);
					}
					IL_00B2:
					reader.Read();
					continue;
					IL_00AC:
					reader.Skip();
					goto IL_00B2;
				}
				return ReflectionUtils.CreateInstance(type, new object[] { obj, obj2 });
			}
			if (!flag)
			{
				throw new Exception("Could not deserialize Null to KeyValuePair.");
			}
			return null;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005B58 File Offset: 0x00003D58
		public override bool CanConvert(Type objectType)
		{
			Type type = (ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType);
			return type.IsValueType && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<, >);
		}
	}
}
