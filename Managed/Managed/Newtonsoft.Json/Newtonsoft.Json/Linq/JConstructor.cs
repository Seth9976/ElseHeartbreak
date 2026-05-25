using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000065 RID: 101
	public class JConstructor : JContainer
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00010D97 File Offset: 0x0000EF97
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00010D9F File Offset: 0x0000EF9F
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x00010DA7 File Offset: 0x0000EFA7
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Constructor;
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00010DB3 File Offset: 0x0000EFB3
		public JConstructor()
		{
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00010DC6 File Offset: 0x0000EFC6
		public JConstructor(JConstructor other)
			: base(other)
		{
			this._name = other.Name;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00010DE6 File Offset: 0x0000EFE6
		public JConstructor(string name, params object[] content)
			: this(name, content)
		{
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00010DF0 File Offset: 0x0000EFF0
		public JConstructor(string name, object content)
			: this(name)
		{
			this.Add(content);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00010E00 File Offset: 0x0000F000
		public JConstructor(string name)
		{
			ValidationUtils.ArgumentNotNullOrEmpty(name, "name");
			this._name = name;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00010E28 File Offset: 0x0000F028
		internal override bool DeepEquals(JToken node)
		{
			JConstructor jconstructor = node as JConstructor;
			return jconstructor != null && this._name == jconstructor.Name && base.ContentsEqual(jconstructor);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00010E5B File Offset: 0x0000F05B
		internal override JToken CloneToken()
		{
			return new JConstructor(this);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00010E64 File Offset: 0x0000F064
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartConstructor(this._name);
			foreach (JToken jtoken in this.Children())
			{
				jtoken.WriteTo(writer, converters);
			}
			writer.WriteEndConstructor();
		}

		// Token: 0x170000F1 RID: 241
		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				if (!(key is int))
				{
					throw new ArgumentException("Accessed JConstructor values with invalid key value: {0}. Argument position index expected.".FormatWith(CultureInfo.InvariantCulture, new object[] { MiscellaneousUtils.ToString(key) }));
				}
				return this.GetItem((int)key);
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				if (!(key is int))
				{
					throw new ArgumentException("Set JConstructor values with invalid key value: {0}. Argument position index expected.".FormatWith(CultureInfo.InvariantCulture, new object[] { MiscellaneousUtils.ToString(key) }));
				}
				this.SetItem((int)key, value);
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00010F6F File Offset: 0x0000F16F
		internal override int GetDeepHashCode()
		{
			return this._name.GetHashCode() ^ base.ContentsHashCode();
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00010F84 File Offset: 0x0000F184
		public new static JConstructor Load(JsonReader reader)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw new Exception("Error reading JConstructor from JsonReader.");
			}
			if (reader.TokenType != JsonToken.StartConstructor)
			{
				throw new Exception("Error reading JConstructor from JsonReader. Current JsonReader item is not a constructor: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { reader.TokenType }));
			}
			JConstructor jconstructor = new JConstructor((string)reader.Value);
			jconstructor.SetLineInfo(reader as IJsonLineInfo);
			jconstructor.ReadTokenFrom(reader);
			return jconstructor;
		}

		// Token: 0x04000141 RID: 321
		private string _name;

		// Token: 0x04000142 RID: 322
		private IList<JToken> _values = new List<JToken>();
	}
}
