using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000067 RID: 103
	public class JObject : JContainer, IDictionary<string, JToken>, ICollection<KeyValuePair<string, JToken>>, IEnumerable<KeyValuePair<string, JToken>>, IEnumerable, INotifyPropertyChanged, ICustomTypeDescriptor, INotifyPropertyChanging
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00011086 File Offset: 0x0000F286
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060004C8 RID: 1224 RVA: 0x00011090 File Offset: 0x0000F290
		// (remove) Token: 0x060004C9 RID: 1225 RVA: 0x000110C8 File Offset: 0x0000F2C8
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060004CA RID: 1226 RVA: 0x00011100 File Offset: 0x0000F300
		// (remove) Token: 0x060004CB RID: 1227 RVA: 0x00011138 File Offset: 0x0000F338
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x060004CC RID: 1228 RVA: 0x0001116D File Offset: 0x0000F36D
		public JObject()
		{
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00011185 File Offset: 0x0000F385
		public JObject(JObject other)
			: base(other)
		{
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001119E File Offset: 0x0000F39E
		public JObject(params object[] content)
			: this(content)
		{
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000111A7 File Offset: 0x0000F3A7
		public JObject(object content)
		{
			this.Add(content);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000111C8 File Offset: 0x0000F3C8
		internal override bool DeepEquals(JToken node)
		{
			JObject jobject = node as JObject;
			return jobject != null && base.ContentsEqual(jobject);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000111E8 File Offset: 0x0000F3E8
		internal override void InsertItem(int index, JToken item)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return;
			}
			base.InsertItem(index, item);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00011200 File Offset: 0x0000F400
		internal override void ValidateToken(JToken o, JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type != JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					o.GetType(),
					base.GetType()
				}));
			}
			JProperty jproperty = (JProperty)o;
			if (existing != null)
			{
				JProperty jproperty2 = (JProperty)existing;
				if (jproperty.Name == jproperty2.Name)
				{
					return;
				}
			}
			if (this._properties.Dictionary != null && this._properties.Dictionary.TryGetValue(jproperty.Name, out existing))
			{
				throw new ArgumentException("Can not add property {0} to {1}. Property with the same name already exists on object.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					jproperty.Name,
					base.GetType()
				}));
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000112CB File Offset: 0x0000F4CB
		internal void InternalPropertyChanged(JProperty childProperty)
		{
			this.OnPropertyChanged(childProperty.Name);
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, base.IndexOfItem(childProperty)));
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000112EC File Offset: 0x0000F4EC
		internal void InternalPropertyChanging(JProperty childProperty)
		{
			this.OnPropertyChanging(childProperty.Name);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000112FA File Offset: 0x0000F4FA
		internal override JToken CloneToken()
		{
			return new JObject(this);
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00011302 File Offset: 0x0000F502
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Object;
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00011305 File Offset: 0x0000F505
		public IEnumerable<JProperty> Properties()
		{
			return this.ChildrenTokens.Cast<JProperty>();
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00011314 File Offset: 0x0000F514
		public JProperty Property(string name)
		{
			if (this._properties.Dictionary == null)
			{
				return null;
			}
			if (name == null)
			{
				return null;
			}
			JToken jtoken;
			this._properties.Dictionary.TryGetValue(name, out jtoken);
			return (JProperty)jtoken;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00011357 File Offset: 0x0000F557
		public JEnumerable<JToken> PropertyValues()
		{
			return new JEnumerable<JToken>(from p in this.Properties()
				select p.Value);
		}

		// Token: 0x170000F5 RID: 245
		public override JToken this[object key]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Accessed JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, new object[] { MiscellaneousUtils.ToString(key) }));
				}
				return this[text];
			}
			set
			{
				ValidationUtils.ArgumentNotNull(key, "o");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Set JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, new object[] { MiscellaneousUtils.ToString(key) }));
				}
				this[text] = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		public JToken this[string propertyName]
		{
			get
			{
				ValidationUtils.ArgumentNotNull(propertyName, "propertyName");
				JProperty jproperty = this.Property(propertyName);
				if (jproperty == null)
				{
					return null;
				}
				return jproperty.Value;
			}
			set
			{
				JProperty jproperty = this.Property(propertyName);
				if (jproperty != null)
				{
					jproperty.Value = value;
					return;
				}
				this.OnPropertyChanging(propertyName);
				this.Add(new JProperty(propertyName, value));
				this.OnPropertyChanged(propertyName);
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00011490 File Offset: 0x0000F690
		public new static JObject Load(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw new Exception("Error reading JObject from JsonReader.");
			}
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw new Exception("Error reading JObject from JsonReader. Current JsonReader item is not an object: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { reader.TokenType }));
			}
			JObject jobject = new JObject();
			jobject.SetLineInfo(reader as IJsonLineInfo);
			jobject.ReadTokenFrom(reader);
			return jobject;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00011514 File Offset: 0x0000F714
		public new static JObject Parse(string json)
		{
			JsonReader jsonReader = new JsonTextReader(new StringReader(json));
			JObject jobject = JObject.Load(jsonReader);
			if (jsonReader.Read() && jsonReader.TokenType != JsonToken.Comment)
			{
				throw new Exception("Additional text found in JSON string after parsing content.");
			}
			return jobject;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00011551 File Offset: 0x0000F751
		public new static JObject FromObject(object o)
		{
			return JObject.FromObject(o, new JsonSerializer());
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00011560 File Offset: 0x0000F760
		public new static JObject FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken != null && jtoken.Type != JTokenType.Object)
			{
				throw new ArgumentException("Object serialized to {0}. JObject instance expected.".FormatWith(CultureInfo.InvariantCulture, new object[] { jtoken.Type }));
			}
			return (JObject)jtoken;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000115B4 File Offset: 0x0000F7B4
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartObject();
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				JProperty jproperty = (JProperty)jtoken;
				jproperty.WriteTo(writer, converters);
			}
			writer.WriteEndObject();
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00011614 File Offset: 0x0000F814
		public void Add(string propertyName, JToken value)
		{
			this.Add(new JProperty(propertyName, value));
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00011623 File Offset: 0x0000F823
		bool IDictionary<string, JToken>.ContainsKey(string key)
		{
			return this._properties.Dictionary != null && this._properties.Dictionary.ContainsKey(key);
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00011645 File Offset: 0x0000F845
		ICollection<string> IDictionary<string, JToken>.Keys
		{
			get
			{
				return this._properties.Dictionary.Keys;
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00011658 File Offset: 0x0000F858
		public bool Remove(string propertyName)
		{
			JProperty jproperty = this.Property(propertyName);
			if (jproperty == null)
			{
				return false;
			}
			jproperty.Remove();
			return true;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001167C File Offset: 0x0000F87C
		public bool TryGetValue(string propertyName, out JToken value)
		{
			JProperty jproperty = this.Property(propertyName);
			if (jproperty == null)
			{
				value = null;
				return false;
			}
			value = jproperty.Value;
			return true;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x000116A2 File Offset: 0x0000F8A2
		ICollection<JToken> IDictionary<string, JToken>.Values
		{
			get
			{
				return this._properties.Dictionary.Values;
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x000116B4 File Offset: 0x0000F8B4
		void ICollection<KeyValuePair<string, JToken>>.Add(KeyValuePair<string, JToken> item)
		{
			this.Add(new JProperty(item.Key, item.Value));
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000116CF File Offset: 0x0000F8CF
		void ICollection<KeyValuePair<string, JToken>>.Clear()
		{
			base.RemoveAll();
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000116D8 File Offset: 0x0000F8D8
		bool ICollection<KeyValuePair<string, JToken>>.Contains(KeyValuePair<string, JToken> item)
		{
			JProperty jproperty = this.Property(item.Key);
			return jproperty != null && jproperty.Value == item.Value;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00011708 File Offset: 0x0000F908
		void ICollection<KeyValuePair<string, JToken>>.CopyTo(KeyValuePair<string, JToken>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
			}
			if (arrayIndex >= array.Length)
			{
				throw new ArgumentException("arrayIndex is equal to or greater than the length of array.");
			}
			if (base.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				JProperty jproperty = (JProperty)jtoken;
				array[arrayIndex + num] = new KeyValuePair<string, JToken>(jproperty.Name, jproperty.Value);
				num++;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x000117C4 File Offset: 0x0000F9C4
		bool ICollection<KeyValuePair<string, JToken>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000117C7 File Offset: 0x0000F9C7
		bool ICollection<KeyValuePair<string, JToken>>.Remove(KeyValuePair<string, JToken> item)
		{
			if (!((ICollection<KeyValuePair<string, JToken>>)this).Contains(item))
			{
				return false;
			}
			((IDictionary<string, JToken>)this).Remove(item.Key);
			return true;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000117E3 File Offset: 0x0000F9E3
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00011940 File Offset: 0x0000FB40
		public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
		{
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				JProperty property = (JProperty)jtoken;
				yield return new KeyValuePair<string, JToken>(property.Name, property.Value);
			}
			yield break;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001195C File Offset: 0x0000FB5C
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00011978 File Offset: 0x0000FB78
		protected virtual void OnPropertyChanging(string propertyName)
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00011994 File Offset: 0x0000FB94
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000119A0 File Offset: 0x0000FBA0
		private static Type GetTokenPropertyType(JToken token)
		{
			if (!(token is JValue))
			{
				return token.GetType();
			}
			JValue jvalue = (JValue)token;
			if (jvalue.Value == null)
			{
				return typeof(object);
			}
			return jvalue.Value.GetType();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000119E4 File Offset: 0x0000FBE4
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			foreach (KeyValuePair<string, JToken> keyValuePair in this)
			{
				propertyDescriptorCollection.Add(new JPropertyDescriptor(keyValuePair.Key, JObject.GetTokenPropertyType(keyValuePair.Value)));
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00011A4C File Offset: 0x0000FC4C
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00011A53 File Offset: 0x0000FC53
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00011A56 File Offset: 0x0000FC56
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00011A59 File Offset: 0x0000FC59
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return new TypeConverter();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00011A60 File Offset: 0x0000FC60
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00011A63 File Offset: 0x0000FC63
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00011A66 File Offset: 0x0000FC66
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00011A69 File Offset: 0x0000FC69
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00011A70 File Offset: 0x0000FC70
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00011A77 File Offset: 0x0000FC77
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return null;
		}

		// Token: 0x04000145 RID: 325
		private JObject.JPropertKeyedCollection _properties = new JObject.JPropertKeyedCollection(StringComparer.Ordinal);

		// Token: 0x02000068 RID: 104
		private class JPropertKeyedCollection : KeyedCollection<string, JToken>
		{
			// Token: 0x06000501 RID: 1281 RVA: 0x00011A7A File Offset: 0x0000FC7A
			public JPropertKeyedCollection(IEqualityComparer<string> comparer)
				: base(comparer)
			{
			}

			// Token: 0x06000502 RID: 1282 RVA: 0x00011A83 File Offset: 0x0000FC83
			protected override string GetKeyForItem(JToken item)
			{
				return ((JProperty)item).Name;
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x00011A90 File Offset: 0x0000FC90
			protected override void InsertItem(int index, JToken item)
			{
				if (this.Dictionary == null)
				{
					base.InsertItem(index, item);
					return;
				}
				string keyForItem = this.GetKeyForItem(item);
				this.Dictionary[keyForItem] = item;
				base.Items.Insert(index, item);
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000504 RID: 1284 RVA: 0x00011AD0 File Offset: 0x0000FCD0
			public new IDictionary<string, JToken> Dictionary
			{
				get
				{
					return base.Dictionary;
				}
			}
		}
	}
}
