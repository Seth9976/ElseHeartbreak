using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000090 RID: 144
	internal class JsonSerializerInternalReader : JsonSerializerInternalBase
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x00017864 File Offset: 0x00015A64
		public JsonSerializerInternalReader(JsonSerializer serializer)
			: base(serializer)
		{
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00017870 File Offset: 0x00015A70
		public void Populate(JsonReader reader, object target)
		{
			ValidationUtils.ArgumentNotNull(target, "target");
			Type type = target.GetType();
			JsonContract jsonContract = base.Serializer.ContractResolver.ResolveContract(type);
			if (reader.TokenType == JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				if (jsonContract is JsonArrayContract)
				{
					this.PopulateList(CollectionUtils.CreateCollectionWrapper(target), reader, null, (JsonArrayContract)jsonContract);
					return;
				}
				throw new JsonSerializationException("Cannot populate JSON array onto type '{0}'.".FormatWith(CultureInfo.InvariantCulture, new object[] { type }));
			}
			else
			{
				if (reader.TokenType != JsonToken.StartObject)
				{
					throw new JsonSerializationException("Unexpected initial token '{0}' when populating object. Expected JSON object or array.".FormatWith(CultureInfo.InvariantCulture, new object[] { reader.TokenType }));
				}
				this.CheckedRead(reader);
				string text = null;
				if (reader.TokenType == JsonToken.PropertyName && string.Equals(reader.Value.ToString(), "$id", StringComparison.Ordinal))
				{
					this.CheckedRead(reader);
					text = ((reader.Value != null) ? reader.Value.ToString() : null);
					this.CheckedRead(reader);
				}
				if (jsonContract is JsonDictionaryContract)
				{
					this.PopulateDictionary(CollectionUtils.CreateDictionaryWrapper(target), reader, (JsonDictionaryContract)jsonContract, text);
					return;
				}
				if (jsonContract is JsonObjectContract)
				{
					this.PopulateObject(target, reader, (JsonObjectContract)jsonContract, text);
					return;
				}
				throw new JsonSerializationException("Cannot populate JSON object onto type '{0}'.".FormatWith(CultureInfo.InvariantCulture, new object[] { type }));
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000179DB File Offset: 0x00015BDB
		private JsonContract GetContractSafe(Type type)
		{
			if (type == null)
			{
				return null;
			}
			return base.Serializer.ContractResolver.ResolveContract(type);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000179F3 File Offset: 0x00015BF3
		private JsonContract GetContractSafe(Type type, object value)
		{
			if (value == null)
			{
				return this.GetContractSafe(type);
			}
			return base.Serializer.ContractResolver.ResolveContract(value.GetType());
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00017A16 File Offset: 0x00015C16
		public object Deserialize(JsonReader reader, Type objectType)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.TokenType == JsonToken.None && !this.ReadForType(reader, objectType, null))
			{
				return null;
			}
			return this.CreateValueNonProperty(reader, objectType, this.GetContractSafe(objectType));
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00017A4A File Offset: 0x00015C4A
		private JsonSerializerProxy GetInternalSerializer()
		{
			if (this._internalSerializer == null)
			{
				this._internalSerializer = new JsonSerializerProxy(this);
			}
			return this._internalSerializer;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00017A66 File Offset: 0x00015C66
		private JsonFormatterConverter GetFormatterConverter()
		{
			if (this._formatterConverter == null)
			{
				this._formatterConverter = new JsonFormatterConverter(this.GetInternalSerializer());
			}
			return this._formatterConverter;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00017A88 File Offset: 0x00015C88
		private JToken CreateJToken(JsonReader reader, JsonContract contract)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (contract != null && contract.UnderlyingType == typeof(JRaw))
			{
				return JRaw.Create(reader);
			}
			JToken token;
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jtokenWriter.WriteToken(reader);
				token = jtokenWriter.Token;
			}
			return token;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00017AF0 File Offset: 0x00015CF0
		private JToken CreateJObject(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			JToken token;
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jtokenWriter.WriteStartObject();
				if (reader.TokenType == JsonToken.PropertyName)
				{
					jtokenWriter.WriteToken(reader, reader.Depth - 1);
				}
				else
				{
					jtokenWriter.WriteEndObject();
				}
				token = jtokenWriter.Token;
			}
			return token;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00017B58 File Offset: 0x00015D58
		private object CreateValueProperty(JsonReader reader, JsonProperty property, object target, bool gottenCurrentValue, object currentValue)
		{
			JsonContract contractSafe = this.GetContractSafe(property.PropertyType, currentValue);
			Type propertyType = property.PropertyType;
			JsonConverter converter = this.GetConverter(contractSafe, property.MemberConverter);
			if (converter != null && converter.CanRead)
			{
				if (!gottenCurrentValue && target != null && property.Readable)
				{
					currentValue = property.ValueProvider.GetValue(target);
				}
				return converter.ReadJson(reader, propertyType, currentValue, this.GetInternalSerializer());
			}
			return this.CreateValueInternal(reader, propertyType, contractSafe, property, currentValue);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00017BD0 File Offset: 0x00015DD0
		private object CreateValueNonProperty(JsonReader reader, Type objectType, JsonContract contract)
		{
			JsonConverter converter = this.GetConverter(contract, null);
			if (converter != null && converter.CanRead)
			{
				return converter.ReadJson(reader, objectType, null, this.GetInternalSerializer());
			}
			return this.CreateValueInternal(reader, objectType, contract, null, null);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00017C0C File Offset: 0x00015E0C
		private object CreateValueInternal(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, object existingValue)
		{
			if (contract is JsonLinqContract)
			{
				return this.CreateJToken(reader, contract);
			}
			for (;;)
			{
				switch (reader.TokenType)
				{
				case JsonToken.StartObject:
					goto IL_0069;
				case JsonToken.StartArray:
					goto IL_0077;
				case JsonToken.StartConstructor:
				case JsonToken.EndConstructor:
					goto IL_00E9;
				case JsonToken.Comment:
					if (!reader.Read())
					{
						goto Block_8;
					}
					continue;
				case JsonToken.Raw:
					goto IL_011D;
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
					goto IL_0086;
				case JsonToken.String:
					goto IL_0099;
				case JsonToken.Null:
				case JsonToken.Undefined:
					goto IL_00F7;
				}
				break;
			}
			goto IL_012E;
			IL_0069:
			return this.CreateObject(reader, objectType, contract, member, existingValue);
			IL_0077:
			return this.CreateList(reader, objectType, contract, member, existingValue, null);
			IL_0086:
			return this.EnsureType(reader.Value, CultureInfo.InvariantCulture, objectType);
			IL_0099:
			if (string.IsNullOrEmpty((string)reader.Value) && objectType != null && ReflectionUtils.IsNullableType(objectType))
			{
				return null;
			}
			if (objectType == typeof(byte[]))
			{
				return Convert.FromBase64String((string)reader.Value);
			}
			return this.EnsureType(reader.Value, CultureInfo.InvariantCulture, objectType);
			IL_00E9:
			return reader.Value.ToString();
			IL_00F7:
			if (objectType == typeof(DBNull))
			{
				return DBNull.Value;
			}
			return this.EnsureType(reader.Value, CultureInfo.InvariantCulture, objectType);
			IL_011D:
			return new JRaw((string)reader.Value);
			IL_012E:
			throw new JsonSerializationException("Unexpected token while deserializing object: " + reader.TokenType);
			Block_8:
			throw new JsonSerializationException("Unexpected end when deserializing object.");
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00017D78 File Offset: 0x00015F78
		private JsonConverter GetConverter(JsonContract contract, JsonConverter memberConverter)
		{
			JsonConverter jsonConverter = null;
			if (memberConverter != null)
			{
				jsonConverter = memberConverter;
			}
			else if (contract != null)
			{
				JsonConverter matchingConverter;
				if (contract.Converter != null)
				{
					jsonConverter = contract.Converter;
				}
				else if ((matchingConverter = base.Serializer.GetMatchingConverter(contract.UnderlyingType)) != null)
				{
					jsonConverter = matchingConverter;
				}
				else if (contract.InternalConverter != null)
				{
					jsonConverter = contract.InternalConverter;
				}
			}
			return jsonConverter;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00017DCC File Offset: 0x00015FCC
		private object CreateObject(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, object existingValue)
		{
			this.CheckedRead(reader);
			string text = null;
			if (reader.TokenType == JsonToken.PropertyName)
			{
				string text3;
				string text4;
				Type type;
				for (;;)
				{
					string text2 = reader.Value.ToString();
					bool flag;
					if (string.Equals(text2, "$ref", StringComparison.Ordinal))
					{
						this.CheckedRead(reader);
						if (reader.TokenType != JsonToken.String && reader.TokenType != JsonToken.Null)
						{
							break;
						}
						text3 = ((reader.Value != null) ? reader.Value.ToString() : null);
						this.CheckedRead(reader);
						if (text3 != null)
						{
							goto Block_5;
						}
						flag = true;
					}
					else if (string.Equals(text2, "$type", StringComparison.Ordinal))
					{
						this.CheckedRead(reader);
						text4 = reader.Value.ToString();
						this.CheckedRead(reader);
						if ((((member != null) ? member.TypeNameHandling : null) ?? base.Serializer.TypeNameHandling) != TypeNameHandling.None)
						{
							string text5;
							string text6;
							ReflectionUtils.SplitFullyQualifiedTypeName(text4, out text5, out text6);
							try
							{
								type = base.Serializer.Binder.BindToType(text6, text5);
							}
							catch (Exception ex)
							{
								throw new JsonSerializationException("Error resolving type specified in JSON '{0}'.".FormatWith(CultureInfo.InvariantCulture, new object[] { text4 }), ex);
							}
							if (type == null)
							{
								goto Block_12;
							}
							if (objectType != null && !objectType.IsAssignableFrom(type))
							{
								goto Block_14;
							}
							objectType = type;
							contract = this.GetContractSafe(type);
						}
						flag = true;
					}
					else if (string.Equals(text2, "$id", StringComparison.Ordinal))
					{
						this.CheckedRead(reader);
						text = ((reader.Value != null) ? reader.Value.ToString() : null);
						this.CheckedRead(reader);
						flag = true;
					}
					else
					{
						if (string.Equals(text2, "$values", StringComparison.Ordinal))
						{
							goto Block_17;
						}
						flag = false;
					}
					if (!flag || reader.TokenType != JsonToken.PropertyName)
					{
						goto IL_0287;
					}
				}
				throw new JsonSerializationException("JSON reference {0} property must have a string or null value.".FormatWith(CultureInfo.InvariantCulture, new object[] { "$ref" }));
				Block_5:
				if (reader.TokenType == JsonToken.PropertyName)
				{
					throw new JsonSerializationException("Additional content found in JSON reference object. A JSON reference object should only have a {0} property.".FormatWith(CultureInfo.InvariantCulture, new object[] { "$ref" }));
				}
				return base.Serializer.ReferenceResolver.ResolveReference(this, text3);
				Block_12:
				throw new JsonSerializationException("Type specified in JSON '{0}' was not resolved.".FormatWith(CultureInfo.InvariantCulture, new object[] { text4 }));
				Block_14:
				throw new JsonSerializationException("Type specified in JSON '{0}' is not compatible with '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[] { type.AssemblyQualifiedName, objectType.AssemblyQualifiedName }));
				Block_17:
				this.CheckedRead(reader);
				object obj = this.CreateList(reader, objectType, contract, member, existingValue, text);
				this.CheckedRead(reader);
				return obj;
			}
			IL_0287:
			if (!this.HasDefinedType(objectType))
			{
				return this.CreateJObject(reader);
			}
			if (contract == null)
			{
				throw new JsonSerializationException("Could not resolve type '{0}' to a JsonContract.".FormatWith(CultureInfo.InvariantCulture, new object[] { objectType }));
			}
			JsonDictionaryContract jsonDictionaryContract = contract as JsonDictionaryContract;
			if (jsonDictionaryContract != null)
			{
				if (existingValue == null)
				{
					return this.CreateAndPopulateDictionary(reader, jsonDictionaryContract, text);
				}
				return this.PopulateDictionary(jsonDictionaryContract.CreateWrapper(existingValue), reader, jsonDictionaryContract, text);
			}
			else
			{
				JsonObjectContract jsonObjectContract = contract as JsonObjectContract;
				if (jsonObjectContract != null)
				{
					if (existingValue == null)
					{
						return this.CreateAndPopulateObject(reader, jsonObjectContract, text);
					}
					return this.PopulateObject(existingValue, reader, jsonObjectContract, text);
				}
				else
				{
					JsonPrimitiveContract jsonPrimitiveContract = contract as JsonPrimitiveContract;
					if (jsonPrimitiveContract != null && reader.TokenType == JsonToken.PropertyName && string.Equals(reader.Value.ToString(), "$value", StringComparison.Ordinal))
					{
						this.CheckedRead(reader);
						object obj2 = this.CreateValueInternal(reader, objectType, jsonPrimitiveContract, member, existingValue);
						this.CheckedRead(reader);
						return obj2;
					}
					JsonISerializableContract jsonISerializableContract = contract as JsonISerializableContract;
					if (jsonISerializableContract != null)
					{
						return this.CreateISerializable(reader, jsonISerializableContract, text);
					}
					throw new JsonSerializationException("Cannot deserialize JSON object into type '{0}'.".FormatWith(CultureInfo.InvariantCulture, new object[] { objectType }));
				}
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00018188 File Offset: 0x00016388
		private JsonArrayContract EnsureArrayContract(Type objectType, JsonContract contract)
		{
			if (contract == null)
			{
				throw new JsonSerializationException("Could not resolve type '{0}' to a JsonContract.".FormatWith(CultureInfo.InvariantCulture, new object[] { objectType }));
			}
			JsonArrayContract jsonArrayContract = contract as JsonArrayContract;
			if (jsonArrayContract == null)
			{
				throw new JsonSerializationException("Cannot deserialize JSON array into type '{0}'.".FormatWith(CultureInfo.InvariantCulture, new object[] { objectType }));
			}
			return jsonArrayContract;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000181E5 File Offset: 0x000163E5
		private void CheckedRead(JsonReader reader)
		{
			if (!reader.Read())
			{
				throw new JsonSerializationException("Unexpected end when deserializing object.");
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x000181FC File Offset: 0x000163FC
		private object CreateList(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, object existingValue, string reference)
		{
			object obj;
			if (this.HasDefinedType(objectType))
			{
				JsonArrayContract jsonArrayContract = this.EnsureArrayContract(objectType, contract);
				if (existingValue == null)
				{
					obj = this.CreateAndPopulateList(reader, reference, jsonArrayContract);
				}
				else
				{
					obj = this.PopulateList(jsonArrayContract.CreateWrapper(existingValue), reader, reference, jsonArrayContract);
				}
			}
			else
			{
				obj = this.CreateJToken(reader, contract);
			}
			return obj;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001824B File Offset: 0x0001644B
		private bool HasDefinedType(Type type)
		{
			return type != null && type != typeof(object) && !typeof(JToken).IsAssignableFrom(type);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00018274 File Offset: 0x00016474
		private object EnsureType(object value, CultureInfo culture, Type targetType)
		{
			if (targetType == null)
			{
				return value;
			}
			Type objectType = ReflectionUtils.GetObjectType(value);
			if (objectType != targetType)
			{
				try
				{
					return ConvertUtils.ConvertOrCast(value, culture, targetType);
				}
				catch (Exception ex)
				{
					throw new JsonSerializationException("Error converting value {0} to type '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						this.FormatValueForPrint(value),
						targetType
					}), ex);
				}
				return value;
			}
			return value;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x000182DC File Offset: 0x000164DC
		private string FormatValueForPrint(object value)
		{
			if (value == null)
			{
				return "{null}";
			}
			if (value is string)
			{
				return "\"" + value + "\"";
			}
			return value.ToString();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00018308 File Offset: 0x00016508
		private void SetPropertyValue(JsonProperty property, JsonReader reader, object target)
		{
			if (property.Ignored)
			{
				reader.Skip();
				return;
			}
			object obj = null;
			bool flag = false;
			bool flag2 = false;
			ObjectCreationHandling valueOrDefault = property.ObjectCreationHandling.GetValueOrDefault(base.Serializer.ObjectCreationHandling);
			if ((valueOrDefault == ObjectCreationHandling.Auto || valueOrDefault == ObjectCreationHandling.Reuse) && (reader.TokenType == JsonToken.StartArray || reader.TokenType == JsonToken.StartObject) && property.Readable)
			{
				obj = property.ValueProvider.GetValue(target);
				flag2 = true;
				flag = obj != null && !property.PropertyType.IsArray && !ReflectionUtils.InheritsGenericDefinition(property.PropertyType, typeof(ReadOnlyCollection<>)) && !property.PropertyType.IsValueType;
			}
			if (!property.Writable && !flag)
			{
				reader.Skip();
				return;
			}
			if (property.NullValueHandling.GetValueOrDefault(base.Serializer.NullValueHandling) == NullValueHandling.Ignore && reader.TokenType == JsonToken.Null)
			{
				reader.Skip();
				return;
			}
			if (this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(base.Serializer.DefaultValueHandling), DefaultValueHandling.Ignore) && JsonReader.IsPrimitiveToken(reader.TokenType) && MiscellaneousUtils.ValueEquals(reader.Value, property.DefaultValue))
			{
				reader.Skip();
				return;
			}
			object obj2 = (flag ? obj : null);
			object obj3 = this.CreateValueProperty(reader, property, target, flag2, obj2);
			if ((!flag || obj3 != obj) && this.ShouldSetPropertyValue(property, obj3))
			{
				property.ValueProvider.SetValue(target, obj3);
				if (property.SetIsSpecified != null)
				{
					property.SetIsSpecified(target, true);
				}
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001848A File Offset: 0x0001668A
		private bool HasFlag(DefaultValueHandling value, DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00018494 File Offset: 0x00016694
		private bool ShouldSetPropertyValue(JsonProperty property, object value)
		{
			return (property.NullValueHandling.GetValueOrDefault(base.Serializer.NullValueHandling) != NullValueHandling.Ignore || value != null) && (!this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(base.Serializer.DefaultValueHandling), DefaultValueHandling.Ignore) || !MiscellaneousUtils.ValueEquals(value, property.DefaultValue)) && property.Writable;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00018500 File Offset: 0x00016700
		private object CreateAndPopulateDictionary(JsonReader reader, JsonDictionaryContract contract, string id)
		{
			if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || base.Serializer.ConstructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor))
			{
				object obj = contract.DefaultCreator();
				IWrappedDictionary wrappedDictionary = contract.CreateWrapper(obj);
				this.PopulateDictionary(wrappedDictionary, reader, contract, id);
				return wrappedDictionary.UnderlyingDictionary;
			}
			throw new JsonSerializationException("Unable to find a default constructor to use for type {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { contract.UnderlyingType }));
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00018578 File Offset: 0x00016778
		private object PopulateDictionary(IWrappedDictionary dictionary, JsonReader reader, JsonDictionaryContract contract, string id)
		{
			if (id != null)
			{
				base.Serializer.ReferenceResolver.AddReference(this, id, dictionary.UnderlyingDictionary);
			}
			contract.InvokeOnDeserializing(dictionary.UnderlyingDictionary, base.Serializer.Context);
			int depth = reader.Depth;
			JsonToken tokenType;
			for (;;)
			{
				tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.PropertyName:
				{
					object obj = reader.Value;
					try
					{
						try
						{
							obj = this.EnsureType(obj, CultureInfo.InvariantCulture, contract.DictionaryKeyType);
						}
						catch (Exception ex)
						{
							throw new JsonSerializationException("Could not convert string '{0}' to dictionary key type '{1}'. Create a TypeConverter to convert from the string to the key type object.".FormatWith(CultureInfo.InvariantCulture, new object[] { reader.Value, contract.DictionaryKeyType }), ex);
						}
						if (!this.ReadForType(reader, contract.DictionaryValueType, null))
						{
							throw new JsonSerializationException("Unexpected end when deserializing object.");
						}
						dictionary[obj] = this.CreateValueNonProperty(reader, contract.DictionaryValueType, this.GetContractSafe(contract.DictionaryValueType));
						goto IL_0144;
					}
					catch (Exception ex2)
					{
						if (base.IsErrorHandled(dictionary, contract, obj, ex2))
						{
							this.HandleError(reader, depth);
							goto IL_0144;
						}
						throw;
					}
					goto IL_010B;
				}
				case JsonToken.Comment:
					goto IL_0144;
				}
				break;
				IL_0144:
				if (!reader.Read())
				{
					goto Block_5;
				}
			}
			if (tokenType != JsonToken.EndObject)
			{
				throw new JsonSerializationException("Unexpected token when deserializing object: " + reader.TokenType);
			}
			IL_010B:
			contract.InvokeOnDeserialized(dictionary.UnderlyingDictionary, base.Serializer.Context);
			return dictionary.UnderlyingDictionary;
			Block_5:
			throw new JsonSerializationException("Unexpected end when deserializing object.");
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x000187E8 File Offset: 0x000169E8
		private object CreateAndPopulateList(JsonReader reader, string reference, JsonArrayContract contract)
		{
			return CollectionUtils.CreateAndPopulateList(contract.CreatedType, delegate(IList l, bool isTemporaryListReference)
			{
				if (reference != null && isTemporaryListReference)
				{
					throw new JsonSerializationException("Cannot preserve reference to array or readonly list: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { contract.UnderlyingType }));
				}
				if (contract.OnSerializing != null && isTemporaryListReference)
				{
					throw new JsonSerializationException("Cannot call OnSerializing on an array or readonly list: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { contract.UnderlyingType }));
				}
				if (contract.OnError != null && isTemporaryListReference)
				{
					throw new JsonSerializationException("Cannot call OnError on an array or readonly list: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { contract.UnderlyingType }));
				}
				this.PopulateList(contract.CreateWrapper(l), reader, reference, contract);
			});
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00018834 File Offset: 0x00016A34
		private bool ReadForTypeArrayHack(JsonReader reader, Type t)
		{
			bool flag;
			try
			{
				flag = this.ReadForType(reader, t, null);
			}
			catch (JsonReaderException)
			{
				if (reader.TokenType != JsonToken.EndArray)
				{
					throw;
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00018870 File Offset: 0x00016A70
		private object PopulateList(IWrappedCollection wrappedList, JsonReader reader, string reference, JsonArrayContract contract)
		{
			object underlyingCollection = wrappedList.UnderlyingCollection;
			if (wrappedList.IsFixedSize)
			{
				reader.Skip();
				return wrappedList.UnderlyingCollection;
			}
			if (reference != null)
			{
				base.Serializer.ReferenceResolver.AddReference(this, reference, underlyingCollection);
			}
			contract.InvokeOnDeserializing(underlyingCollection, base.Serializer.Context);
			int depth = reader.Depth;
			while (this.ReadForTypeArrayHack(reader, contract.CollectionItemType))
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					if (tokenType == JsonToken.EndArray)
					{
						contract.InvokeOnDeserialized(underlyingCollection, base.Serializer.Context);
						return wrappedList.UnderlyingCollection;
					}
					try
					{
						object obj = this.CreateValueNonProperty(reader, contract.CollectionItemType, this.GetContractSafe(contract.CollectionItemType));
						wrappedList.Add(obj);
					}
					catch (Exception ex)
					{
						if (!base.IsErrorHandled(underlyingCollection, contract, wrappedList.Count, ex))
						{
							throw;
						}
						this.HandleError(reader, depth);
					}
				}
			}
			throw new JsonSerializationException("Unexpected end when deserializing array.");
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00018974 File Offset: 0x00016B74
		private object CreateISerializable(JsonReader reader, JsonISerializableContract contract, string id)
		{
			Type underlyingType = contract.UnderlyingType;
			SerializationInfo serializationInfo = new SerializationInfo(contract.UnderlyingType, this.GetFormatterConverter());
			bool flag = false;
			string text;
			for (;;)
			{
				JsonToken tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.PropertyName:
					text = reader.Value.ToString();
					if (!reader.Read())
					{
						goto Block_3;
					}
					serializationInfo.AddValue(text, JToken.ReadFrom(reader));
					break;
				case JsonToken.Comment:
					break;
				default:
					if (tokenType != JsonToken.EndObject)
					{
						goto Block_2;
					}
					flag = true;
					break;
				}
				if (flag || !reader.Read())
				{
					goto IL_00B0;
				}
			}
			Block_2:
			throw new JsonSerializationException("Unexpected token when deserializing object: " + reader.TokenType);
			Block_3:
			throw new JsonSerializationException("Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, new object[] { text }));
			IL_00B0:
			if (contract.ISerializableCreator == null)
			{
				throw new JsonSerializationException("ISerializable type '{0}' does not have a valid constructor. To correctly implement ISerializable a constructor that takes SerializationInfo and StreamingContext parameters should be present.".FormatWith(CultureInfo.InvariantCulture, new object[] { underlyingType }));
			}
			object obj = contract.ISerializableCreator(new object[]
			{
				serializationInfo,
				base.Serializer.Context
			});
			if (id != null)
			{
				base.Serializer.ReferenceResolver.AddReference(this, id, obj);
			}
			contract.InvokeOnDeserializing(obj, base.Serializer.Context);
			contract.InvokeOnDeserialized(obj, base.Serializer.Context);
			return obj;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00018ACC File Offset: 0x00016CCC
		private object CreateAndPopulateObject(JsonReader reader, JsonObjectContract contract, string id)
		{
			object obj = null;
			if (contract.UnderlyingType.IsInterface || contract.UnderlyingType.IsAbstract)
			{
				throw new JsonSerializationException("Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantated.".FormatWith(CultureInfo.InvariantCulture, new object[] { contract.UnderlyingType }));
			}
			if (contract.OverrideConstructor != null)
			{
				if (contract.OverrideConstructor.GetParameters().Length > 0)
				{
					return this.CreateObjectFromNonDefaultConstructor(reader, contract, contract.OverrideConstructor, id);
				}
				obj = contract.OverrideConstructor.Invoke(null);
			}
			else if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || base.Serializer.ConstructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor))
			{
				obj = contract.DefaultCreator();
			}
			else if (contract.ParametrizedConstructor != null)
			{
				return this.CreateObjectFromNonDefaultConstructor(reader, contract, contract.ParametrizedConstructor, id);
			}
			if (obj == null)
			{
				throw new JsonSerializationException("Unable to find a constructor to use for type {0}. A class should either have a default constructor, one constructor with arguments or a constructor marked with the JsonConstructor attribute.".FormatWith(CultureInfo.InvariantCulture, new object[] { contract.UnderlyingType }));
			}
			this.PopulateObject(obj, reader, contract, id);
			return obj;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00018BE0 File Offset: 0x00016DE0
		private object CreateObjectFromNonDefaultConstructor(JsonReader reader, JsonObjectContract contract, ConstructorInfo constructorInfo, string id)
		{
			ValidationUtils.ArgumentNotNull(constructorInfo, "constructorInfo");
			Type underlyingType = contract.UnderlyingType;
			IDictionary<JsonProperty, object> dictionary = this.ResolvePropertyAndConstructorValues(contract, reader, underlyingType);
			IDictionary<ParameterInfo, object> dictionary2 = constructorInfo.GetParameters().ToDictionary((ParameterInfo p) => p, (ParameterInfo p) => null);
			IDictionary<JsonProperty, object> dictionary3 = new Dictionary<JsonProperty, object>();
			foreach (KeyValuePair<JsonProperty, object> keyValuePair in dictionary)
			{
				ParameterInfo key = dictionary2.ForgivingCaseSensitiveFind((KeyValuePair<ParameterInfo, object> kv) => kv.Key.Name, keyValuePair.Key.UnderlyingName).Key;
				if (key != null)
				{
					dictionary2[key] = keyValuePair.Value;
				}
				else
				{
					dictionary3.Add(keyValuePair);
				}
			}
			object obj = constructorInfo.Invoke(dictionary2.Values.ToArray<object>());
			if (id != null)
			{
				base.Serializer.ReferenceResolver.AddReference(this, id, obj);
			}
			contract.InvokeOnDeserializing(obj, base.Serializer.Context);
			foreach (KeyValuePair<JsonProperty, object> keyValuePair2 in dictionary3)
			{
				JsonProperty key2 = keyValuePair2.Key;
				object value = keyValuePair2.Value;
				if (this.ShouldSetPropertyValue(keyValuePair2.Key, keyValuePair2.Value))
				{
					key2.ValueProvider.SetValue(obj, value);
				}
				else if (!key2.Writable && value != null)
				{
					JsonContract jsonContract = base.Serializer.ContractResolver.ResolveContract(key2.PropertyType);
					if (jsonContract is JsonArrayContract)
					{
						JsonArrayContract jsonArrayContract = jsonContract as JsonArrayContract;
						object value2 = key2.ValueProvider.GetValue(obj);
						if (value2 == null)
						{
							continue;
						}
						IWrappedCollection wrappedCollection = jsonArrayContract.CreateWrapper(value2);
						IWrappedCollection wrappedCollection2 = jsonArrayContract.CreateWrapper(value);
						using (IEnumerator enumerator3 = wrappedCollection2.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								object obj2 = enumerator3.Current;
								wrappedCollection.Add(obj2);
							}
							continue;
						}
					}
					if (jsonContract is JsonDictionaryContract)
					{
						JsonDictionaryContract jsonDictionaryContract = jsonContract as JsonDictionaryContract;
						object value3 = key2.ValueProvider.GetValue(obj);
						if (value3 != null)
						{
							IWrappedDictionary wrappedDictionary = jsonDictionaryContract.CreateWrapper(value3);
							IWrappedDictionary wrappedDictionary2 = jsonDictionaryContract.CreateWrapper(value);
							foreach (object obj3 in wrappedDictionary2)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
								wrappedDictionary.Add(dictionaryEntry.Key, dictionaryEntry.Value);
							}
						}
					}
				}
			}
			contract.InvokeOnDeserialized(obj, base.Serializer.Context);
			return obj;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00018F34 File Offset: 0x00017134
		private IDictionary<JsonProperty, object> ResolvePropertyAndConstructorValues(JsonObjectContract contract, JsonReader reader, Type objectType)
		{
			IDictionary<JsonProperty, object> dictionary = new Dictionary<JsonProperty, object>();
			bool flag = false;
			string text;
			for (;;)
			{
				JsonToken tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.PropertyName:
				{
					text = reader.Value.ToString();
					JsonProperty jsonProperty = contract.ConstructorParameters.GetClosestMatchProperty(text) ?? contract.Properties.GetClosestMatchProperty(text);
					if (jsonProperty != null)
					{
						if (!this.ReadForType(reader, jsonProperty.PropertyType, jsonProperty.Converter))
						{
							goto Block_5;
						}
						if (!jsonProperty.Ignored)
						{
							dictionary[jsonProperty] = this.CreateValueProperty(reader, jsonProperty, null, true, null);
						}
						else
						{
							reader.Skip();
						}
					}
					else
					{
						if (!reader.Read())
						{
							goto Block_7;
						}
						if (base.Serializer.MissingMemberHandling == MissingMemberHandling.Error)
						{
							goto Block_8;
						}
						reader.Skip();
					}
					break;
				}
				case JsonToken.Comment:
					break;
				default:
					if (tokenType != JsonToken.EndObject)
					{
						goto Block_2;
					}
					flag = true;
					break;
				}
				if (flag || !reader.Read())
				{
					return dictionary;
				}
			}
			Block_2:
			throw new JsonSerializationException("Unexpected token when deserializing object: " + reader.TokenType);
			Block_5:
			throw new JsonSerializationException("Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, new object[] { text }));
			Block_7:
			throw new JsonSerializationException("Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, new object[] { text }));
			Block_8:
			throw new JsonSerializationException("Could not find member '{0}' on object of type '{1}'".FormatWith(CultureInfo.InvariantCulture, new object[] { text, objectType.Name }));
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000190A0 File Offset: 0x000172A0
		private bool ReadForType(JsonReader reader, Type t, JsonConverter propertyConverter)
		{
			bool flag = this.GetConverter(this.GetContractSafe(t), propertyConverter) != null;
			if (flag)
			{
				return reader.Read();
			}
			if (t == typeof(byte[]))
			{
				reader.ReadAsBytes();
				return true;
			}
			if (t == typeof(decimal) || t == typeof(decimal?))
			{
				reader.ReadAsDecimal();
				return true;
			}
			if (t == typeof(DateTimeOffset) || t == typeof(DateTimeOffset?))
			{
				reader.ReadAsDateTimeOffset();
				return true;
			}
			while (reader.Read())
			{
				if (reader.TokenType != JsonToken.Comment)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00019144 File Offset: 0x00017344
		private object PopulateObject(object newObject, JsonReader reader, JsonObjectContract contract, string id)
		{
			contract.InvokeOnDeserializing(newObject, base.Serializer.Context);
			Dictionary<JsonProperty, JsonSerializerInternalReader.PropertyPresence> dictionary = contract.Properties.ToDictionary((JsonProperty m) => m, (JsonProperty m) => JsonSerializerInternalReader.PropertyPresence.None);
			if (id != null)
			{
				base.Serializer.ReferenceResolver.AddReference(this, id, newObject);
			}
			int depth = reader.Depth;
			JsonToken tokenType;
			for (;;)
			{
				tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.PropertyName:
				{
					string text = reader.Value.ToString();
					try
					{
						JsonProperty closestMatchProperty = contract.Properties.GetClosestMatchProperty(text);
						if (closestMatchProperty == null)
						{
							if (base.Serializer.MissingMemberHandling == MissingMemberHandling.Error)
							{
								throw new JsonSerializationException("Could not find member '{0}' on object of type '{1}'".FormatWith(CultureInfo.InvariantCulture, new object[]
								{
									text,
									contract.UnderlyingType.Name
								}));
							}
							reader.Skip();
							goto IL_02C8;
						}
						else
						{
							if (!this.ReadForType(reader, closestMatchProperty.PropertyType, closestMatchProperty.Converter))
							{
								throw new JsonSerializationException("Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, new object[] { text }));
							}
							this.SetPropertyPresence(reader, closestMatchProperty, dictionary);
							this.SetPropertyValue(closestMatchProperty, reader, newObject);
							goto IL_02C8;
						}
					}
					catch (Exception ex)
					{
						if (base.IsErrorHandled(newObject, contract, text, ex))
						{
							this.HandleError(reader, depth);
							goto IL_02C8;
						}
						throw;
					}
					goto IL_0176;
				}
				case JsonToken.Comment:
					goto IL_02C8;
				}
				break;
				IL_02C8:
				if (!reader.Read())
				{
					goto Block_8;
				}
			}
			if (tokenType != JsonToken.EndObject)
			{
				throw new JsonSerializationException("Unexpected token when deserializing object: " + reader.TokenType);
			}
			IL_0176:
			foreach (KeyValuePair<JsonProperty, JsonSerializerInternalReader.PropertyPresence> keyValuePair in dictionary)
			{
				JsonProperty key = keyValuePair.Key;
				switch (keyValuePair.Value)
				{
				case JsonSerializerInternalReader.PropertyPresence.None:
					if (key.Required == Required.AllowNull || key.Required == Required.Always)
					{
						throw new JsonSerializationException("Required property '{0}' not found in JSON.".FormatWith(CultureInfo.InvariantCulture, new object[] { key.PropertyName }));
					}
					if (this.HasFlag(key.DefaultValueHandling.GetValueOrDefault(base.Serializer.DefaultValueHandling), DefaultValueHandling.Populate) && key.Writable)
					{
						key.ValueProvider.SetValue(newObject, this.EnsureType(key.DefaultValue, CultureInfo.InvariantCulture, key.PropertyType));
					}
					break;
				case JsonSerializerInternalReader.PropertyPresence.Null:
					if (key.Required == Required.Always)
					{
						throw new JsonSerializationException("Required property '{0}' expects a value but got null.".FormatWith(CultureInfo.InvariantCulture, new object[] { key.PropertyName }));
					}
					break;
				}
			}
			contract.InvokeOnDeserialized(newObject, base.Serializer.Context);
			return newObject;
			Block_8:
			throw new JsonSerializationException("Unexpected end when deserializing object.");
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00019464 File Offset: 0x00017664
		private void SetPropertyPresence(JsonReader reader, JsonProperty property, Dictionary<JsonProperty, JsonSerializerInternalReader.PropertyPresence> requiredProperties)
		{
			if (property != null)
			{
				requiredProperties[property] = ((reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.Undefined) ? JsonSerializerInternalReader.PropertyPresence.Null : JsonSerializerInternalReader.PropertyPresence.Value);
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00019488 File Offset: 0x00017688
		private void HandleError(JsonReader reader, int initialDepth)
		{
			base.ClearErrorContext();
			reader.Skip();
			while (reader.Depth > initialDepth + 1)
			{
				reader.Read();
			}
		}

		// Token: 0x04000230 RID: 560
		private JsonSerializerProxy _internalSerializer;

		// Token: 0x04000231 RID: 561
		private JsonFormatterConverter _formatterConverter;

		// Token: 0x02000091 RID: 145
		internal enum PropertyPresence
		{
			// Token: 0x04000238 RID: 568
			None,
			// Token: 0x04000239 RID: 569
			Null,
			// Token: 0x0400023A RID: 570
			Value
		}
	}
}
