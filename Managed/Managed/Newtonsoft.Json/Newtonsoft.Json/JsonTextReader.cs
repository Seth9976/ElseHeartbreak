using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000057 RID: 87
	public class JsonTextReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000C55D File Offset: 0x0000A75D
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000C56E File Offset: 0x0000A76E
		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? CultureInfo.InvariantCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000C577 File Offset: 0x0000A777
		public JsonTextReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this._reader = reader;
			this._buffer = new StringBuffer(4096);
			this._currentLineNumber = 1;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		private void ParseString(char quote)
		{
			this.ReadStringIntoBuffer(quote);
			if (this._readType == JsonTextReader.ReadType.ReadAsBytes)
			{
				byte[] array;
				if (this._buffer.Position == 0)
				{
					array = new byte[0];
				}
				else
				{
					array = Convert.FromBase64CharArray(this._buffer.GetInternalBuffer(), 0, this._buffer.Position);
					this._buffer.Position = 0;
				}
				this.SetToken(JsonToken.Bytes, array);
				return;
			}
			string text = this._buffer.ToString();
			this._buffer.Position = 0;
			if (text.StartsWith("/Date(", StringComparison.Ordinal) && text.EndsWith(")/", StringComparison.Ordinal))
			{
				this.ParseDate(text);
				return;
			}
			this.SetToken(JsonToken.String, text);
			this.QuoteChar = quote;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000C660 File Offset: 0x0000A860
		private void ReadStringIntoBuffer(char quote)
		{
			char c;
			for (;;)
			{
				c = this.MoveNext();
				char c2 = c;
				if (c2 <= '"')
				{
					if (c2 != '\0')
					{
						if (c2 != '"')
						{
							goto IL_02C1;
						}
					}
					else
					{
						if (this._end)
						{
							break;
						}
						this._buffer.Append('\0');
						continue;
					}
				}
				else if (c2 != '\'')
				{
					if (c2 != '\\')
					{
						goto IL_02C1;
					}
					if ((c = this.MoveNext()) == '\0' && this._end)
					{
						goto IL_026D;
					}
					char c3 = c;
					if (c3 <= '\\')
					{
						if (c3 <= '\'')
						{
							if (c3 != '"' && c3 != '\'')
							{
								goto Block_10;
							}
						}
						else if (c3 != '/')
						{
							if (c3 != '\\')
							{
								goto Block_12;
							}
							this._buffer.Append('\\');
							continue;
						}
						this._buffer.Append(c);
						continue;
					}
					if (c3 <= 'f')
					{
						if (c3 == 'b')
						{
							this._buffer.Append('\b');
							continue;
						}
						if (c3 != 'f')
						{
							goto Block_15;
						}
						this._buffer.Append('\f');
						continue;
					}
					else
					{
						if (c3 != 'n')
						{
							switch (c3)
							{
							case 'r':
								this._buffer.Append('\r');
								continue;
							case 't':
								this._buffer.Append('\t');
								continue;
							case 'u':
							{
								char[] array = new char[4];
								for (int i = 0; i < array.Length; i++)
								{
									if ((c = this.MoveNext()) == '\0' && this._end)
									{
										goto IL_01BB;
									}
									array[i] = c;
								}
								char c4 = Convert.ToChar(int.Parse(new string(array), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo));
								this._buffer.Append(c4);
								continue;
							}
							}
							goto Block_17;
						}
						this._buffer.Append('\n');
						continue;
					}
				}
				if (c == quote)
				{
					return;
				}
				this._buffer.Append(c);
				continue;
				IL_02C1:
				this._buffer.Append(c);
			}
			throw this.CreateJsonReaderException("Unterminated string. Expected delimiter: {0}. Line {1}, position {2}.", new object[] { quote, this._currentLineNumber, this._currentLinePosition });
			Block_10:
			Block_12:
			Block_15:
			Block_17:
			goto IL_0225;
			IL_01BB:
			throw this.CreateJsonReaderException("Unexpected end while parsing unicode character. Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
			IL_0225:
			throw this.CreateJsonReaderException("Bad JSON escape sequence: {0}. Line {1}, position {2}.", new object[]
			{
				"\\" + c,
				this._currentLineNumber,
				this._currentLinePosition
			});
			IL_026D:
			throw this.CreateJsonReaderException("Unterminated string. Expected delimiter: {0}. Line {1}, position {2}.", new object[] { quote, this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000C940 File Offset: 0x0000AB40
		private JsonReaderException CreateJsonReaderException(string format, params object[] args)
		{
			string text = format.FormatWith(CultureInfo.InvariantCulture, args);
			return new JsonReaderException(text, null, this._currentLineNumber, this._currentLinePosition);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000C970 File Offset: 0x0000AB70
		private TimeSpan ReadOffset(string offsetText)
		{
			bool flag = offsetText[0] == '-';
			int num = int.Parse(offsetText.Substring(1, 2), NumberStyles.Integer, CultureInfo.InvariantCulture);
			int num2 = 0;
			if (offsetText.Length >= 5)
			{
				num2 = int.Parse(offsetText.Substring(3, 2), NumberStyles.Integer, CultureInfo.InvariantCulture);
			}
			TimeSpan timeSpan = TimeSpan.FromHours((double)num) + TimeSpan.FromMinutes((double)num2);
			if (flag)
			{
				timeSpan = timeSpan.Negate();
			}
			return timeSpan;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000C9DC File Offset: 0x0000ABDC
		private void ParseDate(string text)
		{
			string text2 = text.Substring(6, text.Length - 8);
			DateTimeKind dateTimeKind = DateTimeKind.Utc;
			int num = text2.IndexOf('+', 1);
			if (num == -1)
			{
				num = text2.IndexOf('-', 1);
			}
			TimeSpan timeSpan = TimeSpan.Zero;
			if (num != -1)
			{
				dateTimeKind = DateTimeKind.Local;
				timeSpan = this.ReadOffset(text2.Substring(num));
				text2 = text2.Substring(0, num);
			}
			long num2 = long.Parse(text2, NumberStyles.Integer, CultureInfo.InvariantCulture);
			DateTime dateTime = JsonConvert.ConvertJavaScriptTicksToDateTime(num2);
			if (this._readType == JsonTextReader.ReadType.ReadAsDateTimeOffset)
			{
				this.SetToken(JsonToken.Date, new DateTimeOffset(dateTime.Add(timeSpan).Ticks, timeSpan));
				return;
			}
			DateTime dateTime2;
			switch (dateTimeKind)
			{
			case DateTimeKind.Unspecified:
				dateTime2 = DateTime.SpecifyKind(dateTime.ToLocalTime(), DateTimeKind.Unspecified);
				goto IL_00CA;
			case DateTimeKind.Local:
				dateTime2 = dateTime.ToLocalTime();
				goto IL_00CA;
			}
			dateTime2 = dateTime;
			IL_00CA:
			this.SetToken(JsonToken.Date, dateTime2);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		private char MoveNext()
		{
			int num = this._reader.Read();
			int num2 = num;
			if (num2 != -1)
			{
				if (num2 != 10)
				{
					if (num2 != 13)
					{
						this._currentLinePosition++;
					}
					else
					{
						if (this._reader.Peek() == 10)
						{
							this._reader.Read();
						}
						this._currentLineNumber++;
						this._currentLinePosition = 0;
					}
				}
				else
				{
					this._currentLineNumber++;
					this._currentLinePosition = 0;
				}
				return (char)num;
			}
			this._end = true;
			return '\0';
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000CB51 File Offset: 0x0000AD51
		private bool HasNext()
		{
			return this._reader.Peek() != -1;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000CB64 File Offset: 0x0000AD64
		private int PeekNext()
		{
			return this._reader.Peek();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000CB71 File Offset: 0x0000AD71
		public override bool Read()
		{
			this._readType = JsonTextReader.ReadType.Read;
			return this.ReadInternal();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000CB80 File Offset: 0x0000AD80
		private bool IsWrappedInTypeObject()
		{
			this._readType = JsonTextReader.ReadType.Read;
			if (this.TokenType == JsonToken.StartObject)
			{
				int currentLineNumber = this._currentLineNumber;
				int currentLinePosition = this._currentLinePosition;
				this.ReadInternal();
				if (this.Value.ToString() == "$type")
				{
					this.ReadInternal();
					if (this.Value != null && this.Value.ToString().StartsWith("System.Byte[]"))
					{
						this.ReadInternal();
						if (this.Value.ToString() == "$value")
						{
							return true;
						}
					}
				}
				throw this.CreateJsonReaderException("Unexpected token when reading bytes: {0}. Line {1}, position {2}.", new object[]
				{
					JsonToken.StartObject,
					currentLineNumber,
					currentLinePosition
				});
			}
			return false;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000CC44 File Offset: 0x0000AE44
		public override byte[] ReadAsBytes()
		{
			this._readType = JsonTextReader.ReadType.ReadAsBytes;
			while (this.ReadInternal())
			{
				if (this.TokenType != JsonToken.Comment)
				{
					if (this.IsWrappedInTypeObject())
					{
						byte[] array = this.ReadAsBytes();
						this.ReadInternal();
						this.SetToken(JsonToken.Bytes, array);
						return array;
					}
					if (this.TokenType == JsonToken.Null)
					{
						return null;
					}
					if (this.TokenType == JsonToken.Bytes)
					{
						return (byte[])this.Value;
					}
					if (this.TokenType == JsonToken.StartArray)
					{
						List<byte> list = new List<byte>();
						while (this.ReadInternal())
						{
							JsonToken tokenType = this.TokenType;
							switch (tokenType)
							{
							case JsonToken.Comment:
								continue;
							case JsonToken.Raw:
								break;
							case JsonToken.Integer:
								list.Add(Convert.ToByte(this.Value, CultureInfo.InvariantCulture));
								continue;
							default:
								if (tokenType == JsonToken.EndArray)
								{
									byte[] array2 = list.ToArray();
									this.SetToken(JsonToken.Bytes, array2);
									return array2;
								}
								break;
							}
							throw this.CreateJsonReaderException("Unexpected token when reading bytes: {0}. Line {1}, position {2}.", new object[] { this.TokenType, this._currentLineNumber, this._currentLinePosition });
						}
						throw this.CreateJsonReaderException("Unexpected end when reading bytes: Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
					}
					throw this.CreateJsonReaderException("Unexpected token when reading bytes: {0}. Line {1}, position {2}.", new object[] { this.TokenType, this._currentLineNumber, this._currentLinePosition });
				}
			}
			throw this.CreateJsonReaderException("Unexpected end when reading bytes: Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000CE08 File Offset: 0x0000B008
		public override decimal? ReadAsDecimal()
		{
			this._readType = JsonTextReader.ReadType.ReadAsDecimal;
			while (this.ReadInternal())
			{
				if (this.TokenType != JsonToken.Comment)
				{
					if (this.TokenType == JsonToken.Null)
					{
						return null;
					}
					if (this.TokenType == JsonToken.Float)
					{
						return (decimal?)this.Value;
					}
					decimal num;
					if (this.TokenType == JsonToken.String && decimal.TryParse((string)this.Value, NumberStyles.Number, this.Culture, out num))
					{
						this.SetToken(JsonToken.Float, num);
						return new decimal?(num);
					}
					throw this.CreateJsonReaderException("Unexpected token when reading decimal: {0}. Line {1}, position {2}.", new object[] { this.TokenType, this._currentLineNumber, this._currentLinePosition });
				}
			}
			throw this.CreateJsonReaderException("Unexpected end when reading decimal: Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000CF00 File Offset: 0x0000B100
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			this._readType = JsonTextReader.ReadType.ReadAsDateTimeOffset;
			while (this.ReadInternal())
			{
				if (this.TokenType != JsonToken.Comment)
				{
					if (this.TokenType == JsonToken.Null)
					{
						return null;
					}
					if (this.TokenType == JsonToken.Date)
					{
						return new DateTimeOffset?((DateTimeOffset)this.Value);
					}
					DateTimeOffset dateTimeOffset;
					if (this.TokenType == JsonToken.String && DateTimeOffset.TryParse((string)this.Value, this.Culture, DateTimeStyles.None, out dateTimeOffset))
					{
						this.SetToken(JsonToken.Date, dateTimeOffset);
						return new DateTimeOffset?(dateTimeOffset);
					}
					throw this.CreateJsonReaderException("Unexpected token when reading date: {0}. Line {1}, position {2}.", new object[] { this.TokenType, this._currentLineNumber, this._currentLinePosition });
				}
			}
			throw this.CreateJsonReaderException("Unexpected end when reading date: Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000CFFC File Offset: 0x0000B1FC
		private bool ReadInternal()
		{
			char c;
			for (;;)
			{
				char? lastChar = this._lastChar;
				int? num = ((lastChar != null) ? new int?((int)lastChar.GetValueOrDefault()) : null);
				if (num != null)
				{
					c = this._lastChar.Value;
					this._lastChar = null;
				}
				else
				{
					c = this.MoveNext();
				}
				if (c == '\0' && this._end)
				{
					break;
				}
				switch (base.CurrentState)
				{
				case JsonReader.State.Start:
				case JsonReader.State.Property:
				case JsonReader.State.ArrayStart:
				case JsonReader.State.Array:
				case JsonReader.State.ConstructorStart:
				case JsonReader.State.Constructor:
					goto IL_00A0;
				case JsonReader.State.Complete:
				case JsonReader.State.Closed:
				case JsonReader.State.Error:
					continue;
				case JsonReader.State.ObjectStart:
				case JsonReader.State.Object:
					goto IL_00A8;
				case JsonReader.State.PostValue:
					if (this.ParsePostValue(c))
					{
						return true;
					}
					continue;
				}
				goto Block_4;
			}
			return false;
			Block_4:
			throw this.CreateJsonReaderException("Unexpected state: {0}. Line {1}, position {2}.", new object[] { base.CurrentState, this._currentLineNumber, this._currentLinePosition });
			IL_00A0:
			return this.ParseValue(c);
			IL_00A8:
			return this.ParseObject(c);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000D10C File Offset: 0x0000B30C
		private bool ParsePostValue(char currentChar)
		{
			for (;;)
			{
				char c = currentChar;
				if (c <= ')')
				{
					switch (c)
					{
					case '\t':
					case '\n':
					case '\r':
						break;
					case '\v':
					case '\f':
						goto IL_007C;
					default:
						if (c != ' ')
						{
							if (c != ')')
							{
								goto IL_007C;
							}
							goto IL_0062;
						}
						break;
					}
				}
				else if (c <= '/')
				{
					if (c == ',')
					{
						goto IL_0074;
					}
					if (c != '/')
					{
						goto IL_007C;
					}
					goto IL_006C;
				}
				else
				{
					if (c == ']')
					{
						goto IL_0058;
					}
					if (c == '}')
					{
						break;
					}
					goto IL_007C;
				}
				IL_00BD:
				if ((currentChar = this.MoveNext()) == '\0' && this._end)
				{
					return false;
				}
				continue;
				IL_007C:
				if (!char.IsWhiteSpace(currentChar))
				{
					goto Block_9;
				}
				goto IL_00BD;
			}
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_0058:
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_0062:
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_006C:
			this.ParseComment();
			return true;
			IL_0074:
			base.SetStateBasedOnCurrent();
			return false;
			Block_9:
			throw this.CreateJsonReaderException("After parsing a value an unexpected character was encountered: {0}. Line {1}, position {2}.", new object[] { currentChar, this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000D1F0 File Offset: 0x0000B3F0
		private bool ParseObject(char currentChar)
		{
			for (;;)
			{
				char c = currentChar;
				if (c <= ' ')
				{
					switch (c)
					{
					case '\t':
					case '\n':
					case '\r':
						break;
					case '\v':
					case '\f':
						goto IL_0047;
					default:
						if (c != ' ')
						{
							goto IL_0047;
						}
						break;
					}
				}
				else
				{
					if (c == '/')
					{
						goto IL_003F;
					}
					if (c == '}')
					{
						break;
					}
					goto IL_0047;
				}
				IL_0057:
				if ((currentChar = this.MoveNext()) == '\0' && this._end)
				{
					return false;
				}
				continue;
				IL_0047:
				if (!char.IsWhiteSpace(currentChar))
				{
					goto Block_5;
				}
				goto IL_0057;
			}
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_003F:
			this.ParseComment();
			return true;
			Block_5:
			return this.ParseProperty(currentChar);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000D268 File Offset: 0x0000B468
		private bool ParseProperty(char firstChar)
		{
			char c = firstChar;
			char c2;
			if (this.ValidIdentifierChar(c))
			{
				c2 = '\0';
				c = this.ParseUnquotedProperty(c);
			}
			else
			{
				if (c != '"' && c != '\'')
				{
					throw this.CreateJsonReaderException("Invalid property identifier character: {0}. Line {1}, position {2}.", new object[] { c, this._currentLineNumber, this._currentLinePosition });
				}
				c2 = c;
				this.ReadStringIntoBuffer(c2);
				c = this.MoveNext();
			}
			if (c != ':')
			{
				c = this.MoveNext();
				this.EatWhitespace(c, false, out c);
				if (c != ':')
				{
					throw this.CreateJsonReaderException("Invalid character after parsing property name. Expected ':' but got: {0}. Line {1}, position {2}.", new object[] { c, this._currentLineNumber, this._currentLinePosition });
				}
			}
			this.SetToken(JsonToken.PropertyName, this._buffer.ToString());
			this.QuoteChar = c2;
			this._buffer.Position = 0;
			return true;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000D35C File Offset: 0x0000B55C
		private bool ValidIdentifierChar(char value)
		{
			return char.IsLetterOrDigit(value) || value == '_' || value == '$';
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000D374 File Offset: 0x0000B574
		private char ParseUnquotedProperty(char firstChar)
		{
			this._buffer.Append(firstChar);
			char c;
			while ((c = this.MoveNext()) != '\0' || !this._end)
			{
				if (char.IsWhiteSpace(c) || c == ':')
				{
					return c;
				}
				if (!this.ValidIdentifierChar(c))
				{
					throw this.CreateJsonReaderException("Invalid JavaScript property identifier character: {0}. Line {1}, position {2}.", new object[] { c, this._currentLineNumber, this._currentLinePosition });
				}
				this._buffer.Append(c);
			}
			throw this.CreateJsonReaderException("Unexpected end when parsing unquoted property name. Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000D430 File Offset: 0x0000B630
		private bool ParseValue(char currentChar)
		{
			for (;;)
			{
				char c = currentChar;
				if (c <= 'N')
				{
					if (c <= '"')
					{
						switch (c)
						{
						case '\t':
						case '\n':
						case '\r':
							break;
						case '\v':
						case '\f':
							goto IL_01FC;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
								goto IL_01FC;
							case '"':
								goto IL_00D9;
							default:
								goto IL_01FC;
							}
							break;
						}
					}
					else
					{
						switch (c)
						{
						case '\'':
							goto IL_00D9;
						case '(':
						case '*':
						case '+':
						case '.':
							goto IL_01FC;
						case ')':
							goto IL_01F2;
						case ',':
							goto IL_01E8;
						case '-':
							goto IL_0197;
						case '/':
							goto IL_01B2;
						default:
							if (c == 'I')
							{
								goto IL_018F;
							}
							if (c != 'N')
							{
								goto IL_01FC;
							}
							goto IL_0187;
						}
					}
				}
				else if (c <= 'f')
				{
					switch (c)
					{
					case '[':
						goto IL_01CB;
					case '\\':
						goto IL_01FC;
					case ']':
						goto IL_01DE;
					default:
						if (c != 'f')
						{
							goto IL_01FC;
						}
						goto IL_00EA;
					}
				}
				else
				{
					if (c == 'n')
					{
						goto IL_00F2;
					}
					switch (c)
					{
					case 't':
						goto IL_00E2;
					case 'u':
						goto IL_01BA;
					default:
						switch (c)
						{
						case '{':
							goto IL_01C2;
						case '|':
							goto IL_01FC;
						case '}':
							goto IL_01D4;
						default:
							goto IL_01FC;
						}
						break;
					}
				}
				IL_025D:
				if ((currentChar = this.MoveNext()) == '\0' && this._end)
				{
					return false;
				}
				continue;
				IL_01FC:
				if (!char.IsWhiteSpace(currentChar))
				{
					goto Block_17;
				}
				goto IL_025D;
			}
			IL_00D9:
			this.ParseString(currentChar);
			return true;
			IL_00E2:
			this.ParseTrue();
			return true;
			IL_00EA:
			this.ParseFalse();
			return true;
			IL_00F2:
			if (this.HasNext())
			{
				char c2 = (char)this.PeekNext();
				if (c2 == 'u')
				{
					this.ParseNull();
				}
				else
				{
					if (c2 != 'e')
					{
						throw this.CreateJsonReaderException("Unexpected character encountered while parsing value: {0}. Line {1}, position {2}.", new object[] { currentChar, this._currentLineNumber, this._currentLinePosition });
					}
					this.ParseConstructor();
				}
				return true;
			}
			throw this.CreateJsonReaderException("Unexpected end. Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
			IL_0187:
			this.ParseNumberNaN();
			return true;
			IL_018F:
			this.ParseNumberPositiveInfinity();
			return true;
			IL_0197:
			if (this.PeekNext() == 73)
			{
				this.ParseNumberNegativeInfinity();
			}
			else
			{
				this.ParseNumber(currentChar);
			}
			return true;
			IL_01B2:
			this.ParseComment();
			return true;
			IL_01BA:
			this.ParseUndefined();
			return true;
			IL_01C2:
			base.SetToken(JsonToken.StartObject);
			return true;
			IL_01CB:
			base.SetToken(JsonToken.StartArray);
			return true;
			IL_01D4:
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_01DE:
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_01E8:
			base.SetToken(JsonToken.Undefined);
			return true;
			IL_01F2:
			base.SetToken(JsonToken.EndConstructor);
			return true;
			Block_17:
			if (char.IsNumber(currentChar) || currentChar == '-' || currentChar == '.')
			{
				this.ParseNumber(currentChar);
				return true;
			}
			throw this.CreateJsonReaderException("Unexpected character encountered while parsing value: {0}. Line {1}, position {2}.", new object[] { currentChar, this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000D6B4 File Offset: 0x0000B8B4
		private bool EatWhitespace(char initialChar, bool oneOrMore, out char finalChar)
		{
			bool flag = false;
			char c = initialChar;
			while (c == ' ' || char.IsWhiteSpace(c))
			{
				flag = true;
				c = this.MoveNext();
			}
			finalChar = c;
			return !oneOrMore || flag;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000D6E8 File Offset: 0x0000B8E8
		private void ParseConstructor()
		{
			if (this.MatchValue('n', "new", true))
			{
				char c = this.MoveNext();
				if (this.EatWhitespace(c, true, out c))
				{
					while (char.IsLetter(c))
					{
						this._buffer.Append(c);
						c = this.MoveNext();
					}
					this.EatWhitespace(c, false, out c);
					if (c != '(')
					{
						throw this.CreateJsonReaderException("Unexpected character while parsing constructor: {0}. Line {1}, position {2}.", new object[] { c, this._currentLineNumber, this._currentLinePosition });
					}
					string text = this._buffer.ToString();
					this._buffer.Position = 0;
					this.SetToken(JsonToken.StartConstructor, text);
				}
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
		private void ParseNumber(char firstChar)
		{
			char c = firstChar;
			bool flag = false;
			do
			{
				if (this.IsSeperator(c))
				{
					flag = true;
					this._lastChar = new char?(c);
				}
				else
				{
					this._buffer.Append(c);
				}
			}
			while (!flag && ((c = this.MoveNext()) != '\0' || !this._end));
			string text = this._buffer.ToString();
			bool flag2 = firstChar == '0' && !text.StartsWith("0.", StringComparison.OrdinalIgnoreCase);
			object obj;
			JsonToken jsonToken;
			if (this._readType == JsonTextReader.ReadType.ReadAsDecimal)
			{
				if (flag2)
				{
					long num = (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text, 16) : Convert.ToInt64(text, 8));
					obj = Convert.ToDecimal(num);
				}
				else
				{
					obj = decimal.Parse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, CultureInfo.InvariantCulture);
				}
				jsonToken = JsonToken.Float;
			}
			else if (flag2)
			{
				obj = (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text, 16) : Convert.ToInt64(text, 8));
				jsonToken = JsonToken.Integer;
			}
			else if (text.IndexOf(".", StringComparison.OrdinalIgnoreCase) != -1 || text.IndexOf("e", StringComparison.OrdinalIgnoreCase) != -1)
			{
				obj = Convert.ToDouble(text, CultureInfo.InvariantCulture);
				jsonToken = JsonToken.Float;
			}
			else
			{
				try
				{
					obj = Convert.ToInt64(text, CultureInfo.InvariantCulture);
				}
				catch (OverflowException ex)
				{
					throw new JsonReaderException("JSON integer {0} is too large or small for an Int64.".FormatWith(CultureInfo.InvariantCulture, new object[] { text }), ex);
				}
				jsonToken = JsonToken.Integer;
			}
			this._buffer.Position = 0;
			this.SetToken(jsonToken, obj);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000D934 File Offset: 0x0000BB34
		private void ParseComment()
		{
			char c = this.MoveNext();
			if (c == '*')
			{
				while ((c = this.MoveNext()) != '\0' || !this._end)
				{
					if (c == '*')
					{
						if ((c = this.MoveNext()) != '\0' || !this._end)
						{
							if (c == '/')
							{
								IL_0095:
								this.SetToken(JsonToken.Comment, this._buffer.ToString());
								this._buffer.Position = 0;
								return;
							}
							this._buffer.Append('*');
							this._buffer.Append(c);
						}
					}
					else
					{
						this._buffer.Append(c);
					}
				}
				goto IL_0095;
			}
			throw this.CreateJsonReaderException("Error parsing comment. Expected: *. Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		private bool MatchValue(char firstChar, string value)
		{
			char c = firstChar;
			int num = 0;
			while (c == value[num])
			{
				num++;
				if (num >= value.Length || ((c = this.MoveNext()) == '\0' && this._end))
				{
					break;
				}
			}
			return num == value.Length;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000DA38 File Offset: 0x0000BC38
		private bool MatchValue(char firstChar, string value, bool noTrailingNonSeperatorCharacters)
		{
			bool flag = this.MatchValue(firstChar, value);
			if (!noTrailingNonSeperatorCharacters)
			{
				return flag;
			}
			int num = this.PeekNext();
			char c = ((num != -1) ? ((char)num) : '\0');
			return flag && (c == '\0' || this.IsSeperator(c));
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000DA7C File Offset: 0x0000BC7C
		private bool IsSeperator(char c)
		{
			if (c <= ')')
			{
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				case '\v':
				case '\f':
					goto IL_007A;
				default:
					if (c != ' ')
					{
						if (c != ')')
						{
							goto IL_007A;
						}
						if (base.CurrentState == JsonReader.State.Constructor || base.CurrentState == JsonReader.State.ConstructorStart)
						{
							return true;
						}
						return false;
					}
					break;
				}
				return true;
			}
			if (c <= '/')
			{
				if (c != ',')
				{
					if (c != '/')
					{
						goto IL_007A;
					}
					return this.HasNext() && this.PeekNext() == 42;
				}
			}
			else if (c != ']' && c != '}')
			{
				goto IL_007A;
			}
			return true;
			IL_007A:
			if (char.IsWhiteSpace(c))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000DB10 File Offset: 0x0000BD10
		private void ParseTrue()
		{
			if (this.MatchValue('t', JsonConvert.True, true))
			{
				this.SetToken(JsonToken.Boolean, true);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing boolean value. Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		private void ParseNull()
		{
			if (this.MatchValue('n', JsonConvert.Null, true))
			{
				base.SetToken(JsonToken.Null);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing null value. Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		private void ParseUndefined()
		{
			if (this.MatchValue('u', JsonConvert.Undefined, true))
			{
				base.SetToken(JsonToken.Undefined);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing undefined value. Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		private void ParseFalse()
		{
			if (this.MatchValue('f', JsonConvert.False, true))
			{
				this.SetToken(JsonToken.Boolean, false);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing boolean value. Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000DC78 File Offset: 0x0000BE78
		private void ParseNumberNegativeInfinity()
		{
			if (this.MatchValue('-', JsonConvert.NegativeInfinity, true))
			{
				this.SetToken(JsonToken.Float, double.NegativeInfinity);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing negative infinity value. Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000DCDC File Offset: 0x0000BEDC
		private void ParseNumberPositiveInfinity()
		{
			if (this.MatchValue('I', JsonConvert.PositiveInfinity, true))
			{
				this.SetToken(JsonToken.Float, double.PositiveInfinity);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing positive infinity value. Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000DD40 File Offset: 0x0000BF40
		private void ParseNumberNaN()
		{
			if (this.MatchValue('N', JsonConvert.NaN, true))
			{
				this.SetToken(JsonToken.Float, double.NaN);
				return;
			}
			throw this.CreateJsonReaderException("Error parsing NaN value. Line {0}, position {1}.", new object[] { this._currentLineNumber, this._currentLinePosition });
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000DDA2 File Offset: 0x0000BFA2
		public override void Close()
		{
			base.Close();
			if (base.CloseInput && this._reader != null)
			{
				this._reader.Close();
			}
			if (this._buffer != null)
			{
				this._buffer.Clear();
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000DDDB File Offset: 0x0000BFDB
		public int LineNumber
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				return this._currentLineNumber;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000DDED File Offset: 0x0000BFED
		public int LinePosition
		{
			get
			{
				return this._currentLinePosition;
			}
		}

		// Token: 0x04000104 RID: 260
		private const int LineFeedValue = 10;

		// Token: 0x04000105 RID: 261
		private const int CarriageReturnValue = 13;

		// Token: 0x04000106 RID: 262
		private readonly TextReader _reader;

		// Token: 0x04000107 RID: 263
		private readonly StringBuffer _buffer;

		// Token: 0x04000108 RID: 264
		private char? _lastChar;

		// Token: 0x04000109 RID: 265
		private int _currentLinePosition;

		// Token: 0x0400010A RID: 266
		private int _currentLineNumber;

		// Token: 0x0400010B RID: 267
		private bool _end;

		// Token: 0x0400010C RID: 268
		private JsonTextReader.ReadType _readType;

		// Token: 0x0400010D RID: 269
		private CultureInfo _culture;

		// Token: 0x02000058 RID: 88
		private enum ReadType
		{
			// Token: 0x0400010F RID: 271
			Read,
			// Token: 0x04000110 RID: 272
			ReadAsBytes,
			// Token: 0x04000111 RID: 273
			ReadAsDecimal,
			// Token: 0x04000112 RID: 274
			ReadAsDateTimeOffset
		}
	}
}
