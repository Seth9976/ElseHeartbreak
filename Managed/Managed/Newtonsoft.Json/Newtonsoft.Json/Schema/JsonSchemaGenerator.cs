using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x0200008A RID: 138
	public class JsonSchemaGenerator
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00016CB1 File Offset: 0x00014EB1
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x00016CB9 File Offset: 0x00014EB9
		public UndefinedSchemaIdHandling UndefinedSchemaIdHandling { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00016CC2 File Offset: 0x00014EC2
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x00016CD8 File Offset: 0x00014ED8
		public IContractResolver ContractResolver
		{
			get
			{
				if (this._contractResolver == null)
				{
					return DefaultContractResolver.Instance;
				}
				return this._contractResolver;
			}
			set
			{
				this._contractResolver = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00016CE1 File Offset: 0x00014EE1
		private JsonSchema CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00016CE9 File Offset: 0x00014EE9
		private void Push(JsonSchemaGenerator.TypeSchema typeSchema)
		{
			this._currentSchema = typeSchema.Schema;
			this._stack.Add(typeSchema);
			this._resolver.LoadedSchemas.Add(typeSchema.Schema);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00016D1C File Offset: 0x00014F1C
		private JsonSchemaGenerator.TypeSchema Pop()
		{
			JsonSchemaGenerator.TypeSchema typeSchema = this._stack[this._stack.Count - 1];
			this._stack.RemoveAt(this._stack.Count - 1);
			JsonSchemaGenerator.TypeSchema typeSchema2 = this._stack.LastOrDefault<JsonSchemaGenerator.TypeSchema>();
			if (typeSchema2 != null)
			{
				this._currentSchema = typeSchema2.Schema;
			}
			else
			{
				this._currentSchema = null;
			}
			return typeSchema;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00016D7F File Offset: 0x00014F7F
		public JsonSchema Generate(Type type)
		{
			return this.Generate(type, new JsonSchemaResolver(), false);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00016D8E File Offset: 0x00014F8E
		public JsonSchema Generate(Type type, JsonSchemaResolver resolver)
		{
			return this.Generate(type, resolver, false);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00016D99 File Offset: 0x00014F99
		public JsonSchema Generate(Type type, bool rootSchemaNullable)
		{
			return this.Generate(type, new JsonSchemaResolver(), rootSchemaNullable);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00016DA8 File Offset: 0x00014FA8
		public JsonSchema Generate(Type type, JsonSchemaResolver resolver, bool rootSchemaNullable)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			this._resolver = resolver;
			return this.GenerateInternal(type, (!rootSchemaNullable) ? Required.Always : Required.Default, false);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00016DD8 File Offset: 0x00014FD8
		private string GetTitle(Type type)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(type);
			if (jsonContainerAttribute != null && !string.IsNullOrEmpty(jsonContainerAttribute.Title))
			{
				return jsonContainerAttribute.Title;
			}
			return null;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00016E04 File Offset: 0x00015004
		private string GetDescription(Type type)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(type);
			if (jsonContainerAttribute != null && !string.IsNullOrEmpty(jsonContainerAttribute.Description))
			{
				return jsonContainerAttribute.Description;
			}
			DescriptionAttribute attribute = ReflectionUtils.GetAttribute<DescriptionAttribute>(type);
			if (attribute != null)
			{
				return attribute.Description;
			}
			return null;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00016E44 File Offset: 0x00015044
		private string GetTypeId(Type type, bool explicitOnly)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(type);
			if (jsonContainerAttribute != null && !string.IsNullOrEmpty(jsonContainerAttribute.Id))
			{
				return jsonContainerAttribute.Id;
			}
			if (explicitOnly)
			{
				return null;
			}
			switch (this.UndefinedSchemaIdHandling)
			{
			case UndefinedSchemaIdHandling.UseTypeName:
				return type.FullName;
			case UndefinedSchemaIdHandling.UseAssemblyQualifiedName:
				return type.AssemblyQualifiedName;
			default:
				return null;
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00016EB4 File Offset: 0x000150B4
		private JsonSchema GenerateInternal(Type type, Required valueRequired, bool required)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			string typeId = this.GetTypeId(type, false);
			string typeId2 = this.GetTypeId(type, true);
			if (!string.IsNullOrEmpty(typeId))
			{
				JsonSchema schema = this._resolver.GetSchema(typeId);
				if (schema != null)
				{
					if (valueRequired != Required.Always && !JsonSchemaGenerator.HasFlag(schema.Type, JsonSchemaType.Null))
					{
						schema.Type |= JsonSchemaType.Null;
					}
					if (required && schema.Required != true)
					{
						schema.Required = new bool?(true);
					}
					return schema;
				}
			}
			if (this._stack.Any((JsonSchemaGenerator.TypeSchema tc) => tc.Type == type))
			{
				throw new Exception("Unresolved circular reference for type '{0}'. Explicitly define an Id for the type using a JsonObject/JsonArray attribute or automatically generate a type Id using the UndefinedSchemaIdHandling property.".FormatWith(CultureInfo.InvariantCulture, new object[] { type }));
			}
			JsonContract jsonContract = this.ContractResolver.ResolveContract(type);
			JsonConverter jsonConverter;
			if ((jsonConverter = jsonContract.Converter) != null || (jsonConverter = jsonContract.InternalConverter) != null)
			{
				JsonSchema schema2 = jsonConverter.GetSchema();
				if (schema2 != null)
				{
					return schema2;
				}
			}
			this.Push(new JsonSchemaGenerator.TypeSchema(type, new JsonSchema()));
			if (typeId2 != null)
			{
				this.CurrentSchema.Id = typeId2;
			}
			if (required)
			{
				this.CurrentSchema.Required = new bool?(true);
			}
			this.CurrentSchema.Title = this.GetTitle(type);
			this.CurrentSchema.Description = this.GetDescription(type);
			if (jsonConverter != null)
			{
				this.CurrentSchema.Type = new JsonSchemaType?(JsonSchemaType.Any);
			}
			else if (jsonContract is JsonDictionaryContract)
			{
				this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Object, valueRequired));
				Type type2;
				Type type3;
				ReflectionUtils.GetDictionaryKeyValueTypes(type, out type2, out type3);
				if (type2 != null && typeof(IConvertible).IsAssignableFrom(type2))
				{
					this.CurrentSchema.AdditionalProperties = this.GenerateInternal(type3, Required.Default, false);
				}
			}
			else if (jsonContract is JsonArrayContract)
			{
				this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Array, valueRequired));
				this.CurrentSchema.Id = this.GetTypeId(type, false);
				JsonArrayAttribute jsonArrayAttribute = JsonTypeReflector.GetJsonContainerAttribute(type) as JsonArrayAttribute;
				bool flag = jsonArrayAttribute == null || jsonArrayAttribute.AllowNullItems;
				Type collectionItemType = ReflectionUtils.GetCollectionItemType(type);
				if (collectionItemType != null)
				{
					this.CurrentSchema.Items = new List<JsonSchema>();
					this.CurrentSchema.Items.Add(this.GenerateInternal(collectionItemType, (!flag) ? Required.Always : Required.Default, false));
				}
			}
			else
			{
				if (jsonContract is JsonPrimitiveContract)
				{
					this.CurrentSchema.Type = new JsonSchemaType?(this.GetJsonSchemaType(type, valueRequired));
					if (!(this.CurrentSchema.Type == JsonSchemaType.Integer) || !type.IsEnum || type.IsDefined(typeof(FlagsAttribute), true))
					{
						goto IL_0511;
					}
					this.CurrentSchema.Enum = new List<JToken>();
					this.CurrentSchema.Options = new Dictionary<JToken, string>();
					EnumValues<long> namesAndValues = EnumUtils.GetNamesAndValues<long>(type);
					using (IEnumerator<EnumValue<long>> enumerator = namesAndValues.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							EnumValue<long> enumValue = enumerator.Current;
							JToken jtoken = JToken.FromObject(enumValue.Value);
							this.CurrentSchema.Enum.Add(jtoken);
							this.CurrentSchema.Options.Add(jtoken, enumValue.Name);
						}
						goto IL_0511;
					}
				}
				if (jsonContract is JsonObjectContract)
				{
					this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Object, valueRequired));
					this.CurrentSchema.Id = this.GetTypeId(type, false);
					this.GenerateObjectSchema(type, (JsonObjectContract)jsonContract);
				}
				else if (jsonContract is JsonISerializableContract)
				{
					this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Object, valueRequired));
					this.CurrentSchema.Id = this.GetTypeId(type, false);
					this.GenerateISerializableContract(type, (JsonISerializableContract)jsonContract);
				}
				else if (jsonContract is JsonStringContract)
				{
					JsonSchemaType jsonSchemaType = ((!ReflectionUtils.IsNullable(jsonContract.UnderlyingType)) ? JsonSchemaType.String : this.AddNullType(JsonSchemaType.String, valueRequired));
					this.CurrentSchema.Type = new JsonSchemaType?(jsonSchemaType);
				}
				else
				{
					if (!(jsonContract is JsonLinqContract))
					{
						throw new Exception("Unexpected contract type: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { jsonContract }));
					}
					this.CurrentSchema.Type = new JsonSchemaType?(JsonSchemaType.Any);
				}
			}
			IL_0511:
			return this.Pop().Schema;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000173F0 File Offset: 0x000155F0
		private JsonSchemaType AddNullType(JsonSchemaType type, Required valueRequired)
		{
			if (valueRequired != Required.Always)
			{
				return type | JsonSchemaType.Null;
			}
			return type;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000173FC File Offset: 0x000155FC
		private bool HasFlag(DefaultValueHandling value, DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00017404 File Offset: 0x00015604
		private void GenerateObjectSchema(Type type, JsonObjectContract contract)
		{
			this.CurrentSchema.Properties = new Dictionary<string, JsonSchema>();
			foreach (JsonProperty jsonProperty in contract.Properties)
			{
				if (!jsonProperty.Ignored)
				{
					bool flag = jsonProperty.NullValueHandling == NullValueHandling.Ignore || this.HasFlag(jsonProperty.DefaultValueHandling.GetValueOrDefault(), DefaultValueHandling.Ignore) || jsonProperty.ShouldSerialize != null || jsonProperty.GetIsSpecified != null;
					JsonSchema jsonSchema = this.GenerateInternal(jsonProperty.PropertyType, jsonProperty.Required, !flag);
					if (jsonProperty.DefaultValue != null)
					{
						jsonSchema.Default = JToken.FromObject(jsonProperty.DefaultValue);
					}
					this.CurrentSchema.Properties.Add(jsonProperty.PropertyName, jsonSchema);
				}
			}
			if (type.IsSealed)
			{
				this.CurrentSchema.AllowAdditionalProperties = false;
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00017514 File Offset: 0x00015714
		private void GenerateISerializableContract(Type type, JsonISerializableContract contract)
		{
			this.CurrentSchema.AllowAdditionalProperties = true;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00017524 File Offset: 0x00015724
		internal static bool HasFlag(JsonSchemaType? value, JsonSchemaType flag)
		{
			if (value == null)
			{
				return true;
			}
			bool flag2 = (value & flag) == flag;
			return flag2 || (value == JsonSchemaType.Float && flag == JsonSchemaType.Integer);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000175A4 File Offset: 0x000157A4
		private JsonSchemaType GetJsonSchemaType(Type type, Required valueRequired)
		{
			JsonSchemaType jsonSchemaType = JsonSchemaType.None;
			if (valueRequired != Required.Always && ReflectionUtils.IsNullable(type))
			{
				jsonSchemaType = JsonSchemaType.Null;
				if (ReflectionUtils.IsNullableType(type))
				{
					type = Nullable.GetUnderlyingType(type);
				}
			}
			TypeCode typeCode = Type.GetTypeCode(type);
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.Object:
				return jsonSchemaType | JsonSchemaType.String;
			case TypeCode.DBNull:
				return jsonSchemaType | JsonSchemaType.Null;
			case TypeCode.Boolean:
				return jsonSchemaType | JsonSchemaType.Boolean;
			case TypeCode.Char:
				return jsonSchemaType | JsonSchemaType.String;
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
				return jsonSchemaType | JsonSchemaType.Integer;
			case TypeCode.Single:
			case TypeCode.Double:
			case TypeCode.Decimal:
				return jsonSchemaType | JsonSchemaType.Float;
			case TypeCode.DateTime:
				return jsonSchemaType | JsonSchemaType.String;
			case TypeCode.String:
				return jsonSchemaType | JsonSchemaType.String;
			}
			throw new Exception("Unexpected type code '{0}' for type '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[] { typeCode, type }));
		}

		// Token: 0x04000217 RID: 535
		private IContractResolver _contractResolver;

		// Token: 0x04000218 RID: 536
		private JsonSchemaResolver _resolver;

		// Token: 0x04000219 RID: 537
		private IList<JsonSchemaGenerator.TypeSchema> _stack = new List<JsonSchemaGenerator.TypeSchema>();

		// Token: 0x0400021A RID: 538
		private JsonSchema _currentSchema;

		// Token: 0x0200008B RID: 139
		private class TypeSchema
		{
			// Token: 0x1700016B RID: 363
			// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001768C File Offset: 0x0001588C
			// (set) Token: 0x060006C8 RID: 1736 RVA: 0x00017694 File Offset: 0x00015894
			public Type Type { get; private set; }

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0001769D File Offset: 0x0001589D
			// (set) Token: 0x060006CA RID: 1738 RVA: 0x000176A5 File Offset: 0x000158A5
			public JsonSchema Schema { get; private set; }

			// Token: 0x060006CB RID: 1739 RVA: 0x000176AE File Offset: 0x000158AE
			public TypeSchema(Type type, JsonSchema schema)
			{
				ValidationUtils.ArgumentNotNull(type, "type");
				ValidationUtils.ArgumentNotNull(schema, "schema");
				this.Type = type;
				this.Schema = schema;
			}
		}
	}
}
