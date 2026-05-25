using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000017 RID: 23
	public class BinaryConverter : JsonConverter
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x0000517C File Offset: 0x0000337C
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			byte[] byteArray = this.GetByteArray(value);
			writer.WriteValue(byteArray);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000051A4 File Offset: 0x000033A4
		private byte[] GetByteArray(object value)
		{
			if (value.GetType().AssignableToTypeName("System.Data.Linq.Binary"))
			{
				IBinary binary = DynamicWrapper.CreateWrapper<IBinary>(value);
				return binary.ToArray();
			}
			if (value is SqlBinary)
			{
				return ((SqlBinary)value).Value;
			}
			throw new Exception("Unexpected value type when writing binary: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { value.GetType() }));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005210 File Offset: 0x00003410
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			Type type = (ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType);
			if (reader.TokenType == JsonToken.Null)
			{
				if (!ReflectionUtils.IsNullable(objectType))
				{
					throw new Exception("Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { objectType }));
				}
				return null;
			}
			else
			{
				byte[] array;
				if (reader.TokenType == JsonToken.StartArray)
				{
					array = this.ReadByteArray(reader);
				}
				else
				{
					if (reader.TokenType != JsonToken.String)
					{
						throw new Exception("Unexpected token parsing binary. Expected String or StartArray, got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { reader.TokenType }));
					}
					string text = reader.Value.ToString();
					array = Convert.FromBase64String(text);
				}
				if (type.AssignableToTypeName("System.Data.Linq.Binary"))
				{
					return Activator.CreateInstance(type, new object[] { array });
				}
				if (type == typeof(SqlBinary))
				{
					return new SqlBinary(array);
				}
				throw new Exception("Unexpected object type when writing binary: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { objectType }));
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005324 File Offset: 0x00003524
		private byte[] ReadByteArray(JsonReader reader)
		{
			List<byte> list = new List<byte>();
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.Comment:
					continue;
				case JsonToken.Raw:
					break;
				case JsonToken.Integer:
					list.Add(Convert.ToByte(reader.Value, CultureInfo.InvariantCulture));
					continue;
				default:
					if (tokenType == JsonToken.EndArray)
					{
						return list.ToArray();
					}
					break;
				}
				throw new Exception("Unexpected token when reading bytes: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { reader.TokenType }));
			}
			throw new Exception("Unexpected end when reading bytes.");
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000053B7 File Offset: 0x000035B7
		public override bool CanConvert(Type objectType)
		{
			return objectType.AssignableToTypeName("System.Data.Linq.Binary") || (objectType == typeof(SqlBinary) || objectType == typeof(SqlBinary?));
		}

		// Token: 0x04000078 RID: 120
		private const string BinaryTypeName = "System.Data.Linq.Binary";
	}
}
