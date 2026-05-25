using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200006C RID: 108
	public class JProperty : JContainer
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000127E8 File Offset: 0x000109E8
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000127F0 File Offset: 0x000109F0
		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x000127F8 File Offset: 0x000109F8
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00012818 File Offset: 0x00010A18
		public new JToken Value
		{
			[DebuggerStepThrough]
			get
			{
				if (this.ChildrenTokens.Count <= 0)
				{
					return null;
				}
				return this.ChildrenTokens[0];
			}
			set
			{
				base.CheckReentrancy();
				JToken jtoken = value ?? new JValue(null);
				if (this.ChildrenTokens.Count == 0)
				{
					this.InsertItem(0, jtoken);
					return;
				}
				this.SetItem(0, jtoken);
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00012855 File Offset: 0x00010A55
		public JProperty(JProperty other)
			: base(other)
		{
			this._name = other.Name;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00012875 File Offset: 0x00010A75
		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.Value;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00012888 File Offset: 0x00010A88
		internal override void SetItem(int index, JToken item)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (JContainer.IsTokenUnchanged(this.Value, item))
			{
				return;
			}
			if (base.Parent != null)
			{
				((JObject)base.Parent).InternalPropertyChanging(this);
			}
			base.SetItem(0, item);
			if (base.Parent != null)
			{
				((JObject)base.Parent).InternalPropertyChanged(this);
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000128E8 File Offset: 0x00010AE8
		internal override bool RemoveItem(JToken item)
		{
			throw new Exception("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { typeof(JProperty) }));
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00012920 File Offset: 0x00010B20
		internal override void RemoveItemAt(int index)
		{
			throw new Exception("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { typeof(JProperty) }));
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00012958 File Offset: 0x00010B58
		internal override void InsertItem(int index, JToken item)
		{
			if (this.Value != null)
			{
				throw new Exception("{0} cannot have multiple values.".FormatWith(CultureInfo.InvariantCulture, new object[] { typeof(JProperty) }));
			}
			base.InsertItem(0, item);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001299F File Offset: 0x00010B9F
		internal override bool ContainsItem(JToken item)
		{
			return this.Value == item;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000129AC File Offset: 0x00010BAC
		internal override void ClearItems()
		{
			throw new Exception("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { typeof(JProperty) }));
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000129E4 File Offset: 0x00010BE4
		internal override bool DeepEquals(JToken node)
		{
			JProperty jproperty = node as JProperty;
			return jproperty != null && this._name == jproperty.Name && base.ContentsEqual(jproperty);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00012A17 File Offset: 0x00010C17
		internal override JToken CloneToken()
		{
			return new JProperty(this);
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x00012A1F File Offset: 0x00010C1F
		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00012A22 File Offset: 0x00010C22
		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00012A47 File Offset: 0x00010C47
		public JProperty(string name, params object[] content)
			: this(name, content)
		{
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00012A54 File Offset: 0x00010C54
		public JProperty(string name, object content)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
			this.Value = (base.IsMultiContent(content) ? new JArray(content) : base.CreateFromContent(content));
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00012AA2 File Offset: 0x00010CA2
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WritePropertyName(this._name);
			this.Value.WriteTo(writer, converters);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00012ABD File Offset: 0x00010CBD
		internal override int GetDeepHashCode()
		{
			return this._name.GetHashCode() ^ ((this.Value != null) ? this.Value.GetDeepHashCode() : 0);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00012AE4 File Offset: 0x00010CE4
		public new static JProperty Load(JsonReader reader)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw new Exception("Error reading JProperty from JsonReader.");
			}
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw new Exception("Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { reader.TokenType }));
			}
			JProperty jproperty = new JProperty((string)reader.Value);
			jproperty.SetLineInfo(reader as IJsonLineInfo);
			jproperty.ReadTokenFrom(reader);
			return jproperty;
		}

		// Token: 0x04000150 RID: 336
		private readonly List<JToken> _content = new List<JToken>();

		// Token: 0x04000151 RID: 337
		private readonly string _name;
	}
}
