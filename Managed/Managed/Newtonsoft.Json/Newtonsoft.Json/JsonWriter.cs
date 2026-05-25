using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000011 RID: 17
	public abstract class JsonWriter : IDisposable
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003C96 File Offset: 0x00001E96
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003C9E File Offset: 0x00001E9E
		public bool CloseOutput { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003CA7 File Offset: 0x00001EA7
		protected internal int Top
		{
			get
			{
				return this._top;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public WriteState WriteState
		{
			get
			{
				switch (this._currentState)
				{
				case JsonWriter.State.Start:
					return WriteState.Start;
				case JsonWriter.State.Property:
					return WriteState.Property;
				case JsonWriter.State.ObjectStart:
				case JsonWriter.State.Object:
					return WriteState.Object;
				case JsonWriter.State.ArrayStart:
				case JsonWriter.State.Array:
					return WriteState.Array;
				case JsonWriter.State.ConstructorStart:
				case JsonWriter.State.Constructor:
					return WriteState.Constructor;
				case JsonWriter.State.Closed:
					return WriteState.Closed;
				case JsonWriter.State.Error:
					return WriteState.Error;
				}
				throw new JsonWriterException("Invalid state: " + this._currentState);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003D20 File Offset: 0x00001F20
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003D28 File Offset: 0x00001F28
		public Formatting Formatting
		{
			get
			{
				return this._formatting;
			}
			set
			{
				this._formatting = value;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003D31 File Offset: 0x00001F31
		protected JsonWriter()
		{
			this._stack = new List<JTokenType>(8);
			this._stack.Add(JTokenType.None);
			this._currentState = JsonWriter.State.Start;
			this._formatting = Formatting.None;
			this.CloseOutput = true;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003D68 File Offset: 0x00001F68
		private void Push(JTokenType value)
		{
			this._top++;
			if (this._stack.Count <= this._top)
			{
				this._stack.Add(value);
				return;
			}
			this._stack[this._top] = value;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003DB8 File Offset: 0x00001FB8
		private JTokenType Pop()
		{
			JTokenType jtokenType = this.Peek();
			this._top--;
			return jtokenType;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003DDB File Offset: 0x00001FDB
		private JTokenType Peek()
		{
			return this._stack[this._top];
		}

		// Token: 0x0600007C RID: 124
		public abstract void Flush();

		// Token: 0x0600007D RID: 125 RVA: 0x00003DEE File Offset: 0x00001FEE
		public virtual void Close()
		{
			this.AutoCompleteAll();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public virtual void WriteStartObject()
		{
			this.AutoComplete(JsonToken.StartObject);
			this.Push(JTokenType.Object);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003E06 File Offset: 0x00002006
		public virtual void WriteEndObject()
		{
			this.AutoCompleteClose(JsonToken.EndObject);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003E10 File Offset: 0x00002010
		public virtual void WriteStartArray()
		{
			this.AutoComplete(JsonToken.StartArray);
			this.Push(JTokenType.Array);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003E20 File Offset: 0x00002020
		public virtual void WriteEndArray()
		{
			this.AutoCompleteClose(JsonToken.EndArray);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003E2A File Offset: 0x0000202A
		public virtual void WriteStartConstructor(string name)
		{
			this.AutoComplete(JsonToken.StartConstructor);
			this.Push(JTokenType.Constructor);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003E3A File Offset: 0x0000203A
		public virtual void WriteEndConstructor()
		{
			this.AutoCompleteClose(JsonToken.EndConstructor);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003E44 File Offset: 0x00002044
		public virtual void WritePropertyName(string name)
		{
			this.AutoComplete(JsonToken.PropertyName);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003E4D File Offset: 0x0000204D
		public virtual void WriteEnd()
		{
			this.WriteEnd(this.Peek());
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003E5C File Offset: 0x0000205C
		public void WriteToken(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			int num;
			if (reader.TokenType == JsonToken.None)
			{
				num = -1;
			}
			else if (!this.IsStartToken(reader.TokenType))
			{
				num = reader.Depth + 1;
			}
			else
			{
				num = reader.Depth;
			}
			this.WriteToken(reader, num);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003EA8 File Offset: 0x000020A8
		internal void WriteToken(JsonReader reader, int initialDepth)
		{
			for (;;)
			{
				switch (reader.TokenType)
				{
				case JsonToken.None:
					goto IL_01B8;
				case JsonToken.StartObject:
					this.WriteStartObject();
					goto IL_01B8;
				case JsonToken.StartArray:
					this.WriteStartArray();
					goto IL_01B8;
				case JsonToken.StartConstructor:
				{
					string text = reader.Value.ToString();
					if (string.Compare(text, "Date", StringComparison.Ordinal) == 0)
					{
						this.WriteConstructorDate(reader);
						goto IL_01B8;
					}
					this.WriteStartConstructor(reader.Value.ToString());
					goto IL_01B8;
				}
				case JsonToken.PropertyName:
					this.WritePropertyName(reader.Value.ToString());
					goto IL_01B8;
				case JsonToken.Comment:
					this.WriteComment(reader.Value.ToString());
					goto IL_01B8;
				case JsonToken.Raw:
					this.WriteRawValue((string)reader.Value);
					goto IL_01B8;
				case JsonToken.Integer:
					this.WriteValue(Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture));
					goto IL_01B8;
				case JsonToken.Float:
					this.WriteValue(Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture));
					goto IL_01B8;
				case JsonToken.String:
					this.WriteValue(reader.Value.ToString());
					goto IL_01B8;
				case JsonToken.Boolean:
					this.WriteValue(Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture));
					goto IL_01B8;
				case JsonToken.Null:
					this.WriteNull();
					goto IL_01B8;
				case JsonToken.Undefined:
					this.WriteUndefined();
					goto IL_01B8;
				case JsonToken.EndObject:
					this.WriteEndObject();
					goto IL_01B8;
				case JsonToken.EndArray:
					this.WriteEndArray();
					goto IL_01B8;
				case JsonToken.EndConstructor:
					this.WriteEndConstructor();
					goto IL_01B8;
				case JsonToken.Date:
					this.WriteValue((DateTime)reader.Value);
					goto IL_01B8;
				case JsonToken.Bytes:
					this.WriteValue((byte[])reader.Value);
					goto IL_01B8;
				}
				break;
				IL_01B8:
				if (initialDepth - 1 >= reader.Depth - (this.IsEndToken(reader.TokenType) ? 1 : 0) || !reader.Read())
				{
					return;
				}
			}
			throw MiscellaneousUtils.CreateArgumentOutOfRangeException("TokenType", reader.TokenType, "Unexpected token type.");
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004098 File Offset: 0x00002298
		private void WriteConstructorDate(JsonReader reader)
		{
			if (!reader.Read())
			{
				throw new Exception("Unexpected end while reading date constructor.");
			}
			if (reader.TokenType != JsonToken.Integer)
			{
				throw new Exception("Unexpected token while reading date constructor. Expected Integer, got " + reader.TokenType);
			}
			long num = (long)reader.Value;
			DateTime dateTime = JsonConvert.ConvertJavaScriptTicksToDateTime(num);
			if (!reader.Read())
			{
				throw new Exception("Unexpected end while reading date constructor.");
			}
			if (reader.TokenType != JsonToken.EndConstructor)
			{
				throw new Exception("Unexpected token while reading date constructor. Expected EndConstructor, got " + reader.TokenType);
			}
			this.WriteValue(dateTime);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004130 File Offset: 0x00002330
		private bool IsEndToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
			case JsonToken.EndArray:
			case JsonToken.EndConstructor:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000415C File Offset: 0x0000235C
		private bool IsStartToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.StartObject:
			case JsonToken.StartArray:
			case JsonToken.StartConstructor:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004184 File Offset: 0x00002384
		private void WriteEnd(JTokenType type)
		{
			switch (type)
			{
			case JTokenType.Object:
				this.WriteEndObject();
				return;
			case JTokenType.Array:
				this.WriteEndArray();
				return;
			case JTokenType.Constructor:
				this.WriteEndConstructor();
				return;
			default:
				throw new JsonWriterException("Unexpected type when writing end: " + type);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000041D3 File Offset: 0x000023D3
		private void AutoCompleteAll()
		{
			while (this._top > 0)
			{
				this.WriteEnd();
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000041E8 File Offset: 0x000023E8
		private JTokenType GetTypeForCloseToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				return JTokenType.Object;
			case JsonToken.EndArray:
				return JTokenType.Array;
			case JsonToken.EndConstructor:
				return JTokenType.Constructor;
			default:
				throw new JsonWriterException("No type for token: " + token);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000422C File Offset: 0x0000242C
		private JsonToken GetCloseTokenForType(JTokenType type)
		{
			switch (type)
			{
			case JTokenType.Object:
				return JsonToken.EndObject;
			case JTokenType.Array:
				return JsonToken.EndArray;
			case JTokenType.Constructor:
				return JsonToken.EndConstructor;
			default:
				throw new JsonWriterException("No close token for type: " + type);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004270 File Offset: 0x00002470
		private void AutoCompleteClose(JsonToken tokenBeingClosed)
		{
			int num = 0;
			for (int i = 0; i < this._top; i++)
			{
				int num2 = this._top - i;
				if (this._stack[num2] == this.GetTypeForCloseToken(tokenBeingClosed))
				{
					num = i + 1;
					break;
				}
			}
			if (num == 0)
			{
				throw new JsonWriterException("No token to close.");
			}
			for (int j = 0; j < num; j++)
			{
				JsonToken closeTokenForType = this.GetCloseTokenForType(this.Pop());
				if (this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
				{
					this.WriteIndent();
				}
				this.WriteEnd(closeTokenForType);
			}
			JTokenType jtokenType = this.Peek();
			switch (jtokenType)
			{
			case JTokenType.None:
				this._currentState = JsonWriter.State.Start;
				return;
			case JTokenType.Object:
				this._currentState = JsonWriter.State.Object;
				return;
			case JTokenType.Array:
				this._currentState = JsonWriter.State.Array;
				return;
			case JTokenType.Constructor:
				this._currentState = JsonWriter.State.Array;
				return;
			default:
				throw new JsonWriterException("Unknown JsonType: " + jtokenType);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004357 File Offset: 0x00002557
		protected virtual void WriteEnd(JsonToken token)
		{
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004359 File Offset: 0x00002559
		protected virtual void WriteIndent()
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000435B File Offset: 0x0000255B
		protected virtual void WriteValueDelimiter()
		{
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000435D File Offset: 0x0000255D
		protected virtual void WriteIndentSpace()
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004360 File Offset: 0x00002560
		internal void AutoComplete(JsonToken tokenBeingWritten)
		{
			int num;
			switch (tokenBeingWritten)
			{
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				num = 7;
				break;
			default:
				num = (int)tokenBeingWritten;
				break;
			}
			JsonWriter.State state = JsonWriter.stateArray[num][(int)this._currentState];
			if (state == JsonWriter.State.Error)
			{
				throw new JsonWriterException("Token {0} in state {1} would result in an invalid JavaScript object.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					tokenBeingWritten.ToString(),
					this._currentState.ToString()
				}));
			}
			if ((this._currentState == JsonWriter.State.Object || this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.Constructor) && tokenBeingWritten != JsonToken.Comment)
			{
				this.WriteValueDelimiter();
			}
			else if (this._currentState == JsonWriter.State.Property && this._formatting == Formatting.Indented)
			{
				this.WriteIndentSpace();
			}
			WriteState writeState = this.WriteState;
			if ((tokenBeingWritten == JsonToken.PropertyName && writeState != WriteState.Start) || writeState == WriteState.Array || writeState == WriteState.Constructor)
			{
				this.WriteIndent();
			}
			this._currentState = state;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004462 File Offset: 0x00002662
		public virtual void WriteNull()
		{
			this.AutoComplete(JsonToken.Null);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000446C File Offset: 0x0000266C
		public virtual void WriteUndefined()
		{
			this.AutoComplete(JsonToken.Undefined);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004476 File Offset: 0x00002676
		public virtual void WriteRaw(string json)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004478 File Offset: 0x00002678
		public virtual void WriteRawValue(string json)
		{
			this.AutoComplete(JsonToken.Undefined);
			this.WriteRaw(json);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004489 File Offset: 0x00002689
		public virtual void WriteValue(string value)
		{
			this.AutoComplete(JsonToken.String);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004493 File Offset: 0x00002693
		public virtual void WriteValue(int value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000449C File Offset: 0x0000269C
		[CLSCompliant(false)]
		public virtual void WriteValue(uint value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000044A5 File Offset: 0x000026A5
		public virtual void WriteValue(long value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000044AE File Offset: 0x000026AE
		[CLSCompliant(false)]
		public virtual void WriteValue(ulong value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000044B7 File Offset: 0x000026B7
		public virtual void WriteValue(float value)
		{
			this.AutoComplete(JsonToken.Float);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000044C0 File Offset: 0x000026C0
		public virtual void WriteValue(double value)
		{
			this.AutoComplete(JsonToken.Float);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000044C9 File Offset: 0x000026C9
		public virtual void WriteValue(bool value)
		{
			this.AutoComplete(JsonToken.Boolean);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000044D3 File Offset: 0x000026D3
		public virtual void WriteValue(short value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000044DC File Offset: 0x000026DC
		[CLSCompliant(false)]
		public virtual void WriteValue(ushort value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000044E5 File Offset: 0x000026E5
		public virtual void WriteValue(char value)
		{
			this.AutoComplete(JsonToken.String);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000044EF File Offset: 0x000026EF
		public virtual void WriteValue(byte value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000044F8 File Offset: 0x000026F8
		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte value)
		{
			this.AutoComplete(JsonToken.Integer);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004501 File Offset: 0x00002701
		public virtual void WriteValue(decimal value)
		{
			this.AutoComplete(JsonToken.Float);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000450A File Offset: 0x0000270A
		public virtual void WriteValue(DateTime value)
		{
			this.AutoComplete(JsonToken.Date);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004514 File Offset: 0x00002714
		public virtual void WriteValue(DateTimeOffset value)
		{
			this.AutoComplete(JsonToken.Date);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000451E File Offset: 0x0000271E
		public virtual void WriteValue(Guid value)
		{
			this.AutoComplete(JsonToken.String);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004528 File Offset: 0x00002728
		public virtual void WriteValue(TimeSpan value)
		{
			this.AutoComplete(JsonToken.String);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004532 File Offset: 0x00002732
		public virtual void WriteValue(int? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004551 File Offset: 0x00002751
		[CLSCompliant(false)]
		public virtual void WriteValue(uint? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004570 File Offset: 0x00002770
		public virtual void WriteValue(long? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000458F File Offset: 0x0000278F
		[CLSCompliant(false)]
		public virtual void WriteValue(ulong? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000045AE File Offset: 0x000027AE
		public virtual void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000045CD File Offset: 0x000027CD
		public virtual void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000045EC File Offset: 0x000027EC
		public virtual void WriteValue(bool? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000460C File Offset: 0x0000280C
		public virtual void WriteValue(short? value)
		{
			short? num = value;
			int? num2 = ((num != null) ? new int?((int)num.GetValueOrDefault()) : null);
			if (num2 == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000465C File Offset: 0x0000285C
		[CLSCompliant(false)]
		public virtual void WriteValue(ushort? value)
		{
			ushort? num = value;
			int? num2 = ((num != null) ? new int?((int)num.GetValueOrDefault()) : null);
			if (num2 == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000046AC File Offset: 0x000028AC
		public virtual void WriteValue(char? value)
		{
			char? c = value;
			int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : null);
			if (num == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000046FC File Offset: 0x000028FC
		public virtual void WriteValue(byte? value)
		{
			byte? b = value;
			int? num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
			if (num == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000474C File Offset: 0x0000294C
		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte? value)
		{
			sbyte? b = value;
			int? num = ((b != null) ? new int?((int)b.GetValueOrDefault()) : null);
			if (num == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004799 File Offset: 0x00002999
		public virtual void WriteValue(decimal? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000047B8 File Offset: 0x000029B8
		public virtual void WriteValue(DateTime? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000047D7 File Offset: 0x000029D7
		public virtual void WriteValue(DateTimeOffset? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000047F6 File Offset: 0x000029F6
		public virtual void WriteValue(Guid? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004815 File Offset: 0x00002A15
		public virtual void WriteValue(TimeSpan? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004834 File Offset: 0x00002A34
		public virtual void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.AutoComplete(JsonToken.Bytes);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004848 File Offset: 0x00002A48
		public virtual void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.AutoComplete(JsonToken.String);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004864 File Offset: 0x00002A64
		public virtual void WriteValue(object value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			if (value is IConvertible)
			{
				IConvertible convertible = value as IConvertible;
				switch (convertible.GetTypeCode())
				{
				case TypeCode.DBNull:
					this.WriteNull();
					return;
				case TypeCode.Boolean:
					this.WriteValue(convertible.ToBoolean(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Char:
					this.WriteValue(convertible.ToChar(CultureInfo.InvariantCulture));
					return;
				case TypeCode.SByte:
					this.WriteValue(convertible.ToSByte(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Byte:
					this.WriteValue(convertible.ToByte(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Int16:
					this.WriteValue(convertible.ToInt16(CultureInfo.InvariantCulture));
					return;
				case TypeCode.UInt16:
					this.WriteValue(convertible.ToUInt16(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Int32:
					this.WriteValue(convertible.ToInt32(CultureInfo.InvariantCulture));
					return;
				case TypeCode.UInt32:
					this.WriteValue(convertible.ToUInt32(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Int64:
					this.WriteValue(convertible.ToInt64(CultureInfo.InvariantCulture));
					return;
				case TypeCode.UInt64:
					this.WriteValue(convertible.ToUInt64(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Single:
					this.WriteValue(convertible.ToSingle(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Double:
					this.WriteValue(convertible.ToDouble(CultureInfo.InvariantCulture));
					return;
				case TypeCode.Decimal:
					this.WriteValue(convertible.ToDecimal(CultureInfo.InvariantCulture));
					return;
				case TypeCode.DateTime:
					this.WriteValue(convertible.ToDateTime(CultureInfo.InvariantCulture));
					return;
				case TypeCode.String:
					this.WriteValue(convertible.ToString(CultureInfo.InvariantCulture));
					return;
				}
			}
			else
			{
				if (value is DateTimeOffset)
				{
					this.WriteValue((DateTimeOffset)value);
					return;
				}
				if (value is byte[])
				{
					this.WriteValue((byte[])value);
					return;
				}
				if (value is Guid)
				{
					this.WriteValue((Guid)value);
					return;
				}
				if (value is Uri)
				{
					this.WriteValue((Uri)value);
					return;
				}
				if (value is TimeSpan)
				{
					this.WriteValue((TimeSpan)value);
					return;
				}
			}
			throw new ArgumentException("Unsupported type: {0}. Use the JsonSerializer class to get the object's JSON representation.".FormatWith(CultureInfo.InvariantCulture, new object[] { value.GetType() }));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004A88 File Offset: 0x00002C88
		public virtual void WriteComment(string text)
		{
			this.AutoComplete(JsonToken.Comment);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004A91 File Offset: 0x00002C91
		public virtual void WriteWhitespace(string ws)
		{
			if (ws != null && !StringUtils.IsWhiteSpace(ws))
			{
				throw new JsonWriterException("Only white space characters should be used.");
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004AA9 File Offset: 0x00002CA9
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004AB2 File Offset: 0x00002CB2
		private void Dispose(bool disposing)
		{
			if (this.WriteState != WriteState.Closed)
			{
				this.Close();
			}
		}

		// Token: 0x04000061 RID: 97
		private static readonly JsonWriter.State[][] stateArray = new JsonWriter.State[][]
		{
			new JsonWriter.State[]
			{
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Property,
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Object,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Array,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			}
		};

		// Token: 0x04000062 RID: 98
		private int _top;

		// Token: 0x04000063 RID: 99
		private readonly List<JTokenType> _stack;

		// Token: 0x04000064 RID: 100
		private JsonWriter.State _currentState;

		// Token: 0x04000065 RID: 101
		private Formatting _formatting;

		// Token: 0x02000012 RID: 18
		private enum State
		{
			// Token: 0x04000068 RID: 104
			Start,
			// Token: 0x04000069 RID: 105
			Property,
			// Token: 0x0400006A RID: 106
			ObjectStart,
			// Token: 0x0400006B RID: 107
			Object,
			// Token: 0x0400006C RID: 108
			ArrayStart,
			// Token: 0x0400006D RID: 109
			Array,
			// Token: 0x0400006E RID: 110
			ConstructorStart,
			// Token: 0x0400006F RID: 111
			Constructor,
			// Token: 0x04000070 RID: 112
			Bytes,
			// Token: 0x04000071 RID: 113
			Closed,
			// Token: 0x04000072 RID: 114
			Error
		}
	}
}
