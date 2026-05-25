using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000069 RID: 105
	public class JArray : JContainer, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00011AE0 File Offset: 0x0000FCE0
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Array;
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00011AE3 File Offset: 0x0000FCE3
		public JArray()
		{
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00011AF6 File Offset: 0x0000FCF6
		public JArray(JArray other)
			: base(other)
		{
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00011B0A File Offset: 0x0000FD0A
		public JArray(params object[] content)
			: this(content)
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00011B13 File Offset: 0x0000FD13
		public JArray(object content)
		{
			this.Add(content);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00011B30 File Offset: 0x0000FD30
		internal override bool DeepEquals(JToken node)
		{
			JArray jarray = node as JArray;
			return jarray != null && base.ContentsEqual(jarray);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00011B50 File Offset: 0x0000FD50
		internal override JToken CloneToken()
		{
			return new JArray(this);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00011B58 File Offset: 0x0000FD58
		public new static JArray Load(JsonReader reader)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw new Exception("Error reading JArray from JsonReader.");
			}
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw new Exception("Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { reader.TokenType }));
			}
			JArray jarray = new JArray();
			jarray.SetLineInfo(reader as IJsonLineInfo);
			jarray.ReadTokenFrom(reader);
			return jarray;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00011BD0 File Offset: 0x0000FDD0
		public new static JArray Parse(string json)
		{
			JsonReader jsonReader = new JsonTextReader(new StringReader(json));
			JArray jarray = JArray.Load(jsonReader);
			if (jsonReader.Read() && jsonReader.TokenType != JsonToken.Comment)
			{
				throw new Exception("Additional text found in JSON string after parsing content.");
			}
			return jarray;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00011C0D File Offset: 0x0000FE0D
		public new static JArray FromObject(object o)
		{
			return JArray.FromObject(o, new JsonSerializer());
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00011C1C File Offset: 0x0000FE1C
		public new static JArray FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Array)
			{
				throw new ArgumentException("Object serialized to {0}. JArray instance expected.".FormatWith(CultureInfo.InvariantCulture, new object[] { jtoken.Type }));
			}
			return (JArray)jtoken;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00011C6C File Offset: 0x0000FE6C
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartArray();
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				jtoken.WriteTo(writer, converters);
			}
			writer.WriteEndArray();
		}

		// Token: 0x170000FD RID: 253
		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				if (!(key is int))
				{
					throw new ArgumentException("Accessed JArray values with invalid key value: {0}. Array position index expected.".FormatWith(CultureInfo.InvariantCulture, new object[] { MiscellaneousUtils.ToString(key) }));
				}
				return this.GetItem((int)key);
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				if (!(key is int))
				{
					throw new ArgumentException("Set JArray values with invalid key value: {0}. Array position index expected.".FormatWith(CultureInfo.InvariantCulture, new object[] { MiscellaneousUtils.ToString(key) }));
				}
				this.SetItem((int)key, value);
			}
		}

		// Token: 0x170000FE RID: 254
		public JToken this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00011D82 File Offset: 0x0000FF82
		public int IndexOf(JToken item)
		{
			return base.IndexOfItem(item);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00011D8B File Offset: 0x0000FF8B
		public void Insert(int index, JToken item)
		{
			this.InsertItem(index, item);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00011D95 File Offset: 0x0000FF95
		public void RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00011D9E File Offset: 0x0000FF9E
		public void Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00011DA7 File Offset: 0x0000FFA7
		public void Clear()
		{
			this.ClearItems();
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00011DAF File Offset: 0x0000FFAF
		public bool Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00011DB8 File Offset: 0x0000FFB8
		void ICollection<JToken>.CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00011DC2 File Offset: 0x0000FFC2
		bool ICollection<JToken>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00011DC5 File Offset: 0x0000FFC5
		public bool Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00011DCE File Offset: 0x0000FFCE
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x04000149 RID: 329
		private IList<JToken> _values = new List<JToken>();
	}
}
