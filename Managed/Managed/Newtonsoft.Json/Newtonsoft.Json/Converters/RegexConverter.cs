using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Bson;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000020 RID: 32
	public class RegexConverter : JsonConverter
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00005C48 File Offset: 0x00003E48
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Regex regex = (Regex)value;
			BsonWriter bsonWriter = writer as BsonWriter;
			if (bsonWriter != null)
			{
				this.WriteBson(bsonWriter, regex);
				return;
			}
			this.WriteJson(writer, regex);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005C77 File Offset: 0x00003E77
		private bool HasFlag(RegexOptions options, RegexOptions flag)
		{
			return (options & flag) == flag;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005C80 File Offset: 0x00003E80
		private void WriteBson(BsonWriter writer, Regex regex)
		{
			string text = null;
			if (this.HasFlag(regex.Options, RegexOptions.IgnoreCase))
			{
				text += "i";
			}
			if (this.HasFlag(regex.Options, RegexOptions.Multiline))
			{
				text += "m";
			}
			if (this.HasFlag(regex.Options, RegexOptions.Singleline))
			{
				text += "s";
			}
			text += "u";
			if (this.HasFlag(regex.Options, RegexOptions.ExplicitCapture))
			{
				text += "x";
			}
			writer.WriteRegex(regex.ToString(), text);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005D18 File Offset: 0x00003F18
		private void WriteJson(JsonWriter writer, Regex regex)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("Pattern");
			writer.WriteValue(regex.ToString());
			writer.WritePropertyName("Options");
			writer.WriteValue(regex.Options);
			writer.WriteEndObject();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005D64 File Offset: 0x00003F64
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			BsonReader bsonReader = reader as BsonReader;
			if (bsonReader != null)
			{
				return this.ReadBson(bsonReader);
			}
			return this.ReadJson(reader);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005D8C File Offset: 0x00003F8C
		private object ReadBson(BsonReader reader)
		{
			string text = (string)reader.Value;
			int num = text.LastIndexOf("/");
			string text2 = text.Substring(1, num - 1);
			string text3 = text.Substring(num + 1);
			RegexOptions regexOptions = RegexOptions.None;
			foreach (char c in text3)
			{
				char c2 = c;
				if (c2 <= 'm')
				{
					if (c2 != 'i')
					{
						if (c2 == 'm')
						{
							regexOptions |= RegexOptions.Multiline;
						}
					}
					else
					{
						regexOptions |= RegexOptions.IgnoreCase;
					}
				}
				else if (c2 != 's')
				{
					if (c2 == 'x')
					{
						regexOptions |= RegexOptions.ExplicitCapture;
					}
				}
				else
				{
					regexOptions |= RegexOptions.Singleline;
				}
			}
			return new Regex(text2, regexOptions);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005E3C File Offset: 0x0000403C
		private Regex ReadJson(JsonReader reader)
		{
			reader.Read();
			reader.Read();
			string text = (string)reader.Value;
			reader.Read();
			reader.Read();
			int num = Convert.ToInt32(reader.Value, CultureInfo.InvariantCulture);
			reader.Read();
			return new Regex(text, (RegexOptions)num);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005E90 File Offset: 0x00004090
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Regex);
		}
	}
}
