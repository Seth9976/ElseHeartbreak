using System;
using System.Globalization;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200001F RID: 31
	public class BsonObjectIdConverter : JsonConverter
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00005BA4 File Offset: 0x00003DA4
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			BsonObjectId bsonObjectId = (BsonObjectId)value;
			BsonWriter bsonWriter = writer as BsonWriter;
			if (bsonWriter != null)
			{
				bsonWriter.WriteObjectId(bsonObjectId.Value);
				return;
			}
			writer.WriteValue(bsonObjectId.Value);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005BDC File Offset: 0x00003DDC
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Bytes)
			{
				throw new JsonSerializationException("Expected Bytes but got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { reader.TokenType }));
			}
			byte[] array = (byte[])reader.Value;
			return new BsonObjectId(array);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005C30 File Offset: 0x00003E30
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(BsonObjectId);
		}
	}
}
