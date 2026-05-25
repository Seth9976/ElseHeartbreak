using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200001A RID: 26
	public abstract class CustomCreationConverter<T> : JsonConverter
	{
		// Token: 0x06000107 RID: 263 RVA: 0x0000579C File Offset: 0x0000399C
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException("CustomCreationConverter should only be used while deserializing.");
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000057A8 File Offset: 0x000039A8
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			T t = this.Create(objectType);
			if (t == null)
			{
				throw new JsonSerializationException("No object created.");
			}
			serializer.Populate(reader, t);
			return t;
		}

		// Token: 0x06000109 RID: 265
		public abstract T Create(Type objectType);

		// Token: 0x0600010A RID: 266 RVA: 0x000057F0 File Offset: 0x000039F0
		public override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005802 File Offset: 0x00003A02
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}
