using System;
using System.IO;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200005B RID: 91
	public class JsonTextWriter : JsonWriter
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000DFA5 File Offset: 0x0000C1A5
		private Base64Encoder Base64Encoder
		{
			get
			{
				if (this._base64Encoder == null)
				{
					this._base64Encoder = new Base64Encoder(this._writer);
				}
				return this._base64Encoder;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000DFC6 File Offset: 0x0000C1C6
		// (set) Token: 0x06000397 RID: 919 RVA: 0x0000DFCE File Offset: 0x0000C1CE
		public int Indentation
		{
			get
			{
				return this._indentation;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Indentation value must be greater than 0.");
				}
				this._indentation = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000DFE6 File Offset: 0x0000C1E6
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000DFEE File Offset: 0x0000C1EE
		public char QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			set
			{
				if (value != '"' && value != '\'')
				{
					throw new ArgumentException("Invalid JavaScript string quote character. Valid quote characters are ' and \".");
				}
				this._quoteChar = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000E00C File Offset: 0x0000C20C
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000E014 File Offset: 0x0000C214
		public char IndentChar
		{
			get
			{
				return this._indentChar;
			}
			set
			{
				this._indentChar = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0000E01D File Offset: 0x0000C21D
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000E025 File Offset: 0x0000C225
		public bool QuoteName
		{
			get
			{
				return this._quoteName;
			}
			set
			{
				this._quoteName = value;
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000E02E File Offset: 0x0000C22E
		public JsonTextWriter(TextWriter textWriter)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			this._writer = textWriter;
			this._quoteChar = '"';
			this._quoteName = true;
			this._indentChar = ' ';
			this._indentation = 2;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000E069 File Offset: 0x0000C269
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000E076 File Offset: 0x0000C276
		public override void Close()
		{
			base.Close();
			if (base.CloseOutput && this._writer != null)
			{
				this._writer.Close();
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000E099 File Offset: 0x0000C299
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this._writer.Write("{");
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000E0B1 File Offset: 0x0000C2B1
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this._writer.Write("[");
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000E0C9 File Offset: 0x0000C2C9
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this._writer.Write("new ");
			this._writer.Write(name);
			this._writer.Write("(");
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000E100 File Offset: 0x0000C300
		protected override void WriteEnd(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				this._writer.Write("}");
				return;
			case JsonToken.EndArray:
				this._writer.Write("]");
				return;
			case JsonToken.EndConstructor:
				this._writer.Write(")");
				return;
			default:
				throw new JsonWriterException("Invalid JsonToken: " + token);
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000E16E File Offset: 0x0000C36E
		public override void WritePropertyName(string name)
		{
			base.WritePropertyName(name);
			JavaScriptUtils.WriteEscapedJavaScriptString(this._writer, name, this._quoteChar, this._quoteName);
			this._writer.Write(':');
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000E19C File Offset: 0x0000C39C
		protected override void WriteIndent()
		{
			if (base.Formatting == Formatting.Indented)
			{
				this._writer.Write(Environment.NewLine);
				int num = base.Top * this._indentation;
				for (int i = 0; i < num; i++)
				{
					this._writer.Write(this._indentChar);
				}
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000E1ED File Offset: 0x0000C3ED
		protected override void WriteValueDelimiter()
		{
			this._writer.Write(',');
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000E1FC File Offset: 0x0000C3FC
		protected override void WriteIndentSpace()
		{
			this._writer.Write(' ');
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000E20B File Offset: 0x0000C40B
		private void WriteValueInternal(string value, JsonToken token)
		{
			this._writer.Write(value);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000E219 File Offset: 0x0000C419
		public override void WriteNull()
		{
			base.WriteNull();
			this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000E22E File Offset: 0x0000C42E
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.WriteValueInternal(JsonConvert.Undefined, JsonToken.Undefined);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000E243 File Offset: 0x0000C443
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this._writer.Write(json);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000E258 File Offset: 0x0000C458
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			if (value == null)
			{
				this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
				return;
			}
			JavaScriptUtils.WriteEscapedJavaScriptString(this._writer, value, this._quoteChar, true);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000E285 File Offset: 0x0000C485
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000E29B File Offset: 0x0000C49B
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000E2B1 File Offset: 0x0000C4B1
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000E2C7 File Offset: 0x0000C4C7
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000E2DD File Offset: 0x0000C4DD
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Float);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000E2F3 File Offset: 0x0000C4F3
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Float);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000E309 File Offset: 0x0000C509
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Boolean);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000E320 File Offset: 0x0000C520
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000E336 File Offset: 0x0000C536
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000E34C File Offset: 0x0000C54C
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000E362 File Offset: 0x0000C562
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000E378 File Offset: 0x0000C578
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Integer);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000E38E File Offset: 0x0000C58E
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Float);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			JsonConvert.WriteDateTimeString(this._writer, value);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000E3BC File Offset: 0x0000C5BC
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			if (value != null)
			{
				this._writer.Write(this._quoteChar);
				this.Base64Encoder.Encode(value, 0, value.Length);
				this.Base64Encoder.Flush();
				this._writer.Write(this._quoteChar);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000E410 File Offset: 0x0000C610
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Date);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000E427 File Offset: 0x0000C627
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.String);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000E43E File Offset: 0x0000C63E
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.String);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000E455 File Offset: 0x0000C655
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.String);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000E46C File Offset: 0x0000C66C
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this._writer.Write("/*");
			this._writer.Write(text);
			this._writer.Write("*/");
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000E4A1 File Offset: 0x0000C6A1
		public override void WriteWhitespace(string ws)
		{
			base.WriteWhitespace(ws);
			this._writer.Write(ws);
		}

		// Token: 0x0400011C RID: 284
		private readonly TextWriter _writer;

		// Token: 0x0400011D RID: 285
		private Base64Encoder _base64Encoder;

		// Token: 0x0400011E RID: 286
		private char _indentChar;

		// Token: 0x0400011F RID: 287
		private int _indentation;

		// Token: 0x04000120 RID: 288
		private char _quoteChar;

		// Token: 0x04000121 RID: 289
		private bool _quoteName;
	}
}
