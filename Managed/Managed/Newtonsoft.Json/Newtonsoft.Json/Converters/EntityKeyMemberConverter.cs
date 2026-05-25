using System;
using System.Globalization;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200001D RID: 29
	public class EntityKeyMemberConverter : JsonConverter
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00005850 File Offset: 0x00003A50
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			IEntityKeyMember entityKeyMember = DynamicWrapper.CreateWrapper<IEntityKeyMember>(value);
			Type type = ((entityKeyMember.Value != null) ? entityKeyMember.Value.GetType() : null);
			writer.WriteStartObject();
			writer.WritePropertyName("Key");
			writer.WriteValue(entityKeyMember.Key);
			writer.WritePropertyName("Type");
			writer.WriteValue((type != null) ? type.FullName : null);
			writer.WritePropertyName("Value");
			if (type != null)
			{
				string text;
				if (JsonSerializerInternalWriter.TryConvertToString(entityKeyMember.Value, type, out text))
				{
					writer.WriteValue(text);
				}
				else
				{
					writer.WriteValue(entityKeyMember.Value);
				}
			}
			else
			{
				writer.WriteNull();
			}
			writer.WriteEndObject();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000058F8 File Offset: 0x00003AF8
		private static void ReadAndAssertProperty(JsonReader reader, string propertyName)
		{
			EntityKeyMemberConverter.ReadAndAssert(reader);
			if (reader.TokenType != JsonToken.PropertyName || reader.Value.ToString() != propertyName)
			{
				throw new JsonSerializationException("Expected JSON property '{0}'.".FormatWith(CultureInfo.InvariantCulture, new object[] { propertyName }));
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005948 File Offset: 0x00003B48
		private static void ReadAndAssert(JsonReader reader)
		{
			if (!reader.Read())
			{
				throw new JsonSerializationException("Unexpected end.");
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005960 File Offset: 0x00003B60
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			IEntityKeyMember entityKeyMember = DynamicWrapper.CreateWrapper<IEntityKeyMember>(Activator.CreateInstance(objectType));
			EntityKeyMemberConverter.ReadAndAssertProperty(reader, "Key");
			EntityKeyMemberConverter.ReadAndAssert(reader);
			entityKeyMember.Key = reader.Value.ToString();
			EntityKeyMemberConverter.ReadAndAssertProperty(reader, "Type");
			EntityKeyMemberConverter.ReadAndAssert(reader);
			string text = reader.Value.ToString();
			Type type = Type.GetType(text);
			EntityKeyMemberConverter.ReadAndAssertProperty(reader, "Value");
			EntityKeyMemberConverter.ReadAndAssert(reader);
			entityKeyMember.Value = serializer.Deserialize(reader, type);
			EntityKeyMemberConverter.ReadAndAssert(reader);
			return DynamicWrapper.GetUnderlyingObject(entityKeyMember);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000059EB File Offset: 0x00003BEB
		public override bool CanConvert(Type objectType)
		{
			return objectType.AssignableToTypeName("System.Data.EntityKeyMember");
		}

		// Token: 0x04000079 RID: 121
		private const string EntityKeyMemberFullTypeName = "System.Data.EntityKeyMember";
	}
}
