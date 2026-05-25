using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200006A RID: 106
	public class JTokenReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x00011DD6 File Offset: 0x0000FFD6
		public JTokenReader(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, "token");
			this._root = token;
			this._current = token;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00011DF8 File Offset: 0x0000FFF8
		public override byte[] ReadAsBytes()
		{
			this.Read();
			if (this.IsWrappedInTypeObject())
			{
				byte[] array = this.ReadAsBytes();
				this.Read();
				this.SetToken(JsonToken.Bytes, array);
				return array;
			}
			if (this.TokenType == JsonToken.String)
			{
				string text = (string)this.Value;
				byte[] array2 = ((text.Length == 0) ? new byte[0] : Convert.FromBase64String(text));
				this.SetToken(JsonToken.Bytes, array2);
			}
			if (this.TokenType == JsonToken.Null)
			{
				return null;
			}
			if (this.TokenType == JsonToken.Bytes)
			{
				return (byte[])this.Value;
			}
			throw new JsonReaderException("Error reading bytes. Expected bytes but got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { this.TokenType }));
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00011EB0 File Offset: 0x000100B0
		private bool IsWrappedInTypeObject()
		{
			if (this.TokenType == JsonToken.StartObject)
			{
				this.Read();
				if (this.Value.ToString() == "$type")
				{
					this.Read();
					if (this.Value != null && this.Value.ToString().StartsWith("System.Byte[]"))
					{
						this.Read();
						if (this.Value.ToString() == "$value")
						{
							return true;
						}
					}
				}
				throw new JsonReaderException("Unexpected token when reading bytes: {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { JsonToken.StartObject }));
			}
			return false;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00011F54 File Offset: 0x00010154
		public override decimal? ReadAsDecimal()
		{
			this.Read();
			if (this.TokenType == JsonToken.Null)
			{
				return null;
			}
			if (this.TokenType == JsonToken.Integer || this.TokenType == JsonToken.Float)
			{
				this.SetToken(JsonToken.Float, Convert.ToDecimal(this.Value, CultureInfo.InvariantCulture));
				return new decimal?((decimal)this.Value);
			}
			throw new JsonReaderException("Error reading decimal. Expected a number but got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { this.TokenType }));
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00011FE8 File Offset: 0x000101E8
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			this.Read();
			if (this.TokenType == JsonToken.Null)
			{
				return null;
			}
			if (this.TokenType == JsonToken.Date)
			{
				this.SetToken(JsonToken.Date, new DateTimeOffset((DateTime)this.Value));
				return new DateTimeOffset?((DateTimeOffset)this.Value);
			}
			throw new JsonReaderException("Error reading date. Expected bytes but got {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { this.TokenType }));
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00012074 File Offset: 0x00010274
		public override bool Read()
		{
			if (base.CurrentState == JsonReader.State.Start)
			{
				this.SetToken(this._current);
				return true;
			}
			JContainer jcontainer = this._current as JContainer;
			if (jcontainer != null && this._parent != jcontainer)
			{
				return this.ReadInto(jcontainer);
			}
			return this.ReadOver(this._current);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000120C4 File Offset: 0x000102C4
		private bool ReadOver(JToken t)
		{
			if (t == this._root)
			{
				return this.ReadToEnd();
			}
			JToken next = t.Next;
			if (next != null && next != t && t != t.Parent.Last)
			{
				this._current = next;
				this.SetToken(this._current);
				return true;
			}
			if (t.Parent == null)
			{
				return this.ReadToEnd();
			}
			return this.SetEnd(t.Parent);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001212D File Offset: 0x0001032D
		private bool ReadToEnd()
		{
			return false;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00012130 File Offset: 0x00010330
		private bool IsEndElement
		{
			get
			{
				return this._current == this._parent;
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00012140 File Offset: 0x00010340
		private JsonToken? GetEndToken(JContainer c)
		{
			switch (c.Type)
			{
			case JTokenType.Object:
				return new JsonToken?(JsonToken.EndObject);
			case JTokenType.Array:
				return new JsonToken?(JsonToken.EndArray);
			case JTokenType.Constructor:
				return new JsonToken?(JsonToken.EndConstructor);
			case JTokenType.Property:
				return null;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", c.Type, "Unexpected JContainer type.");
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000121AC File Offset: 0x000103AC
		private bool ReadInto(JContainer c)
		{
			JToken first = c.First;
			if (first == null)
			{
				return this.SetEnd(c);
			}
			this.SetToken(first);
			this._current = first;
			this._parent = c;
			return true;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000121E4 File Offset: 0x000103E4
		private bool SetEnd(JContainer c)
		{
			JsonToken? endToken = this.GetEndToken(c);
			if (endToken != null)
			{
				base.SetToken(endToken.Value);
				this._current = c;
				this._parent = c;
				return true;
			}
			return this.ReadOver(c);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00012228 File Offset: 0x00010428
		private void SetToken(JToken token)
		{
			switch (token.Type)
			{
			case JTokenType.Object:
				base.SetToken(JsonToken.StartObject);
				return;
			case JTokenType.Array:
				base.SetToken(JsonToken.StartArray);
				return;
			case JTokenType.Constructor:
				base.SetToken(JsonToken.StartConstructor);
				return;
			case JTokenType.Property:
				this.SetToken(JsonToken.PropertyName, ((JProperty)token).Name);
				return;
			case JTokenType.Comment:
				this.SetToken(JsonToken.Comment, ((JValue)token).Value);
				return;
			case JTokenType.Integer:
				this.SetToken(JsonToken.Integer, ((JValue)token).Value);
				return;
			case JTokenType.Float:
				this.SetToken(JsonToken.Float, ((JValue)token).Value);
				return;
			case JTokenType.String:
				this.SetToken(JsonToken.String, ((JValue)token).Value);
				return;
			case JTokenType.Boolean:
				this.SetToken(JsonToken.Boolean, ((JValue)token).Value);
				return;
			case JTokenType.Null:
				this.SetToken(JsonToken.Null, ((JValue)token).Value);
				return;
			case JTokenType.Undefined:
				this.SetToken(JsonToken.Undefined, ((JValue)token).Value);
				return;
			case JTokenType.Date:
				this.SetToken(JsonToken.Date, ((JValue)token).Value);
				return;
			case JTokenType.Raw:
				this.SetToken(JsonToken.Raw, ((JValue)token).Value);
				return;
			case JTokenType.Bytes:
				this.SetToken(JsonToken.Bytes, ((JValue)token).Value);
				return;
			case JTokenType.Guid:
				this.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				return;
			case JTokenType.Uri:
				this.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				return;
			case JTokenType.TimeSpan:
				this.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", token.Type, "Unexpected JTokenType.");
			}
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000123E4 File Offset: 0x000105E4
		private string SafeToString(object value)
		{
			if (value == null)
			{
				return null;
			}
			return value.ToString();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000123F4 File Offset: 0x000105F4
		bool IJsonLineInfo.HasLineInfo()
		{
			if (base.CurrentState == JsonReader.State.Start)
			{
				return false;
			}
			IJsonLineInfo jsonLineInfo = (this.IsEndElement ? null : this._current);
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00012428 File Offset: 0x00010628
		int IJsonLineInfo.LineNumber
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				IJsonLineInfo jsonLineInfo = (this.IsEndElement ? null : this._current);
				if (jsonLineInfo != null)
				{
					return jsonLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001245C File Offset: 0x0001065C
		int IJsonLineInfo.LinePosition
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				IJsonLineInfo jsonLineInfo = (this.IsEndElement ? null : this._current);
				if (jsonLineInfo != null)
				{
					return jsonLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x0400014A RID: 330
		private readonly JToken _root;

		// Token: 0x0400014B RID: 331
		private JToken _parent;

		// Token: 0x0400014C RID: 332
		private JToken _current;
	}
}
