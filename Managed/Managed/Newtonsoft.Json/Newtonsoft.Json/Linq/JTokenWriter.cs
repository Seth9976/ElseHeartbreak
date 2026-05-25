using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200006B RID: 107
	public class JTokenWriter : JsonWriter
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00012490 File Offset: 0x00010690
		public JToken Token
		{
			get
			{
				if (this._token != null)
				{
					return this._token;
				}
				return this._value;
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000124A7 File Offset: 0x000106A7
		public JTokenWriter(JContainer container)
		{
			ValidationUtils.ArgumentNotNull(container, "container");
			this._token = container;
			this._parent = container;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000124C8 File Offset: 0x000106C8
		public JTokenWriter()
		{
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000124D0 File Offset: 0x000106D0
		public override void Flush()
		{
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000124D2 File Offset: 0x000106D2
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x000124DA File Offset: 0x000106DA
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new JObject());
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000124ED File Offset: 0x000106ED
		private void AddParent(JContainer container)
		{
			if (this._parent == null)
			{
				this._token = container;
			}
			else
			{
				this._parent.Add(container);
			}
			this._parent = container;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00012513 File Offset: 0x00010713
		private void RemoveParent()
		{
			this._parent = this._parent.Parent;
			if (this._parent != null && this._parent.Type == JTokenType.Property)
			{
				this._parent = this._parent.Parent;
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001254D File Offset: 0x0001074D
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new JArray());
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00012560 File Offset: 0x00010760
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this.AddParent(new JConstructor(name));
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00012575 File Offset: 0x00010775
		protected override void WriteEnd(JsonToken token)
		{
			this.RemoveParent();
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001257D File Offset: 0x0001077D
		public override void WritePropertyName(string name)
		{
			base.WritePropertyName(name);
			this.AddParent(new JProperty(name));
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00012592 File Offset: 0x00010792
		private void AddValue(object value, JsonToken token)
		{
			this.AddValue(new JValue(value), token);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000125A1 File Offset: 0x000107A1
		internal void AddValue(JValue value, JsonToken token)
		{
			if (this._parent != null)
			{
				this._parent.Add(value);
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					return;
				}
			}
			else
			{
				this._value = value;
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000125DE File Offset: 0x000107DE
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddValue(null, JsonToken.Null);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000125EF File Offset: 0x000107EF
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddValue(null, JsonToken.Undefined);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00012600 File Offset: 0x00010800
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this.AddValue(new JRaw(json), JsonToken.Raw);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00012616 File Offset: 0x00010816
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this.AddValue(JValue.CreateComment(text), JsonToken.Comment);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001262C File Offset: 0x0001082C
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddValue(value ?? string.Empty, JsonToken.String);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00012647 File Offset: 0x00010847
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001265D File Offset: 0x0001085D
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00012673 File Offset: 0x00010873
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00012689 File Offset: 0x00010889
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001269F File Offset: 0x0001089F
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000126B5 File Offset: 0x000108B5
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000126CB File Offset: 0x000108CB
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Boolean);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000126E2 File Offset: 0x000108E2
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000126F8 File Offset: 0x000108F8
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001270E File Offset: 0x0001090E
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			this.AddValue(value.ToString(), JsonToken.String);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00012726 File Offset: 0x00010926
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0001273C File Offset: 0x0001093C
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00012752 File Offset: 0x00010952
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00012768 File Offset: 0x00010968
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001277F File Offset: 0x0001097F
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00012796 File Offset: 0x00010996
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Bytes);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000127A8 File Offset: 0x000109A8
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000127BF File Offset: 0x000109BF
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000127D6 File Offset: 0x000109D6
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x0400014D RID: 333
		private JContainer _token;

		// Token: 0x0400014E RID: 334
		private JContainer _parent;

		// Token: 0x0400014F RID: 335
		private JValue _value;
	}
}
