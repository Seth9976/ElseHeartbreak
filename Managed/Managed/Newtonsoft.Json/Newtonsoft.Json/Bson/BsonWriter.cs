using System;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000013 RID: 19
	public class BsonWriter : JsonWriter
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004CD8 File Offset: 0x00002ED8
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00004CE5 File Offset: 0x00002EE5
		public DateTimeKind DateTimeKindHandling
		{
			get
			{
				return this._writer.DateTimeKindHandling;
			}
			set
			{
				this._writer.DateTimeKindHandling = value;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004CF3 File Offset: 0x00002EF3
		public BsonWriter(Stream stream)
		{
			ValidationUtils.ArgumentNotNull(stream, "stream");
			this._writer = new BsonBinaryWriter(stream);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004D12 File Offset: 0x00002F12
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004D1F File Offset: 0x00002F1F
		protected override void WriteEnd(JsonToken token)
		{
			base.WriteEnd(token);
			this.RemoveParent();
			if (base.Top == 0)
			{
				this._writer.WriteToken(this._root);
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004D47 File Offset: 0x00002F47
		public override void WriteComment(string text)
		{
			throw new JsonWriterException("Cannot write JSON comment as BSON.");
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004D53 File Offset: 0x00002F53
		public override void WriteStartConstructor(string name)
		{
			throw new JsonWriterException("Cannot write JSON constructor as BSON.");
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004D5F File Offset: 0x00002F5F
		public override void WriteRaw(string json)
		{
			throw new JsonWriterException("Cannot write raw JSON as BSON.");
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004D6B File Offset: 0x00002F6B
		public override void WriteRawValue(string json)
		{
			throw new JsonWriterException("Cannot write raw JSON as BSON.");
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004D77 File Offset: 0x00002F77
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new BsonArray());
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004D8A File Offset: 0x00002F8A
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new BsonObject());
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004D9D File Offset: 0x00002F9D
		public override void WritePropertyName(string name)
		{
			base.WritePropertyName(name);
			this._propertyName = name;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004DAD File Offset: 0x00002FAD
		public override void Close()
		{
			base.Close();
			if (base.CloseOutput && this._writer != null)
			{
				this._writer.Close();
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004DD0 File Offset: 0x00002FD0
		private void AddParent(BsonToken container)
		{
			this.AddToken(container);
			this._parent = container;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004DE0 File Offset: 0x00002FE0
		private void RemoveParent()
		{
			this._parent = this._parent.Parent;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004DF3 File Offset: 0x00002FF3
		private void AddValue(object value, BsonType type)
		{
			this.AddToken(new BsonValue(value, type));
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004E04 File Offset: 0x00003004
		internal void AddToken(BsonToken token)
		{
			if (this._parent != null)
			{
				if (this._parent is BsonObject)
				{
					((BsonObject)this._parent).Add(this._propertyName, token);
					this._propertyName = null;
					return;
				}
				((BsonArray)this._parent).Add(token);
				return;
			}
			else
			{
				if (token.Type != BsonType.Object && token.Type != BsonType.Array)
				{
					throw new JsonWriterException("Error writing {0} value. BSON must start with an Object or Array.".FormatWith(CultureInfo.InvariantCulture, new object[] { token.Type }));
				}
				this._parent = token;
				this._root = token;
				return;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004EA2 File Offset: 0x000030A2
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddValue(null, BsonType.Null);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004EB3 File Offset: 0x000030B3
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddValue(null, BsonType.Undefined);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004EC3 File Offset: 0x000030C3
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			if (value == null)
			{
				this.AddValue(null, BsonType.Null);
				return;
			}
			this.AddToken(new BsonString(value, true));
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004EE6 File Offset: 0x000030E6
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004EFD File Offset: 0x000030FD
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			if (value > 2147483647U)
			{
				throw new JsonWriterException("Value is too large to fit in a signed 32 bit integer. BSON does not support unsigned values.");
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004F27 File Offset: 0x00003127
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004F3E File Offset: 0x0000313E
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				throw new JsonWriterException("Value is too large to fit in a signed 64 bit integer. BSON does not support unsigned values.");
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004F6C File Offset: 0x0000316C
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004F82 File Offset: 0x00003182
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004F98 File Offset: 0x00003198
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Boolean);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004FAE File Offset: 0x000031AE
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004FC5 File Offset: 0x000031C5
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004FDC File Offset: 0x000031DC
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004FF8 File Offset: 0x000031F8
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000500F File Offset: 0x0000320F
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005026 File Offset: 0x00003226
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000503C File Offset: 0x0000323C
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005053 File Offset: 0x00003253
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000506A File Offset: 0x0000326A
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Binary);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000507B File Offset: 0x0000327B
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000509D File Offset: 0x0000329D
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000050BF File Offset: 0x000032BF
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000050DA File Offset: 0x000032DA
		public void WriteObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw new Exception("An object id must be 12 bytes");
			}
			base.AutoComplete(JsonToken.Undefined);
			this.AddValue(value, BsonType.Oid);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005109 File Offset: 0x00003309
		public void WriteRegex(string pattern, string options)
		{
			ValidationUtils.ArgumentNotNull(pattern, "pattern");
			base.AutoComplete(JsonToken.Undefined);
			this.AddToken(new BsonRegex(pattern, options));
		}

		// Token: 0x04000073 RID: 115
		private readonly BsonBinaryWriter _writer;

		// Token: 0x04000074 RID: 116
		private BsonToken _root;

		// Token: 0x04000075 RID: 117
		private BsonToken _parent;

		// Token: 0x04000076 RID: 118
		private string _propertyName;
	}
}
