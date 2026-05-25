using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007A RID: 122
	public class DefaultContractResolver : IContractResolver
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00013DDE File Offset: 0x00011FDE
		internal static IContractResolver Instance
		{
			get
			{
				return DefaultContractResolver._instance;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00013DE5 File Offset: 0x00011FE5
		public bool DynamicCodeGeneration
		{
			get
			{
				return JsonTypeReflector.DynamicCodeGeneration;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00013DEC File Offset: 0x00011FEC
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x00013DF4 File Offset: 0x00011FF4
		public BindingFlags DefaultMembersSearchFlags { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00013DFD File Offset: 0x00011FFD
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x00013E05 File Offset: 0x00012005
		public bool SerializeCompilerGeneratedMembers { get; set; }

		// Token: 0x060005D7 RID: 1495 RVA: 0x00013E0E File Offset: 0x0001200E
		public DefaultContractResolver()
			: this(false)
		{
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00013E17 File Offset: 0x00012017
		public DefaultContractResolver(bool shareCache)
		{
			this.DefaultMembersSearchFlags = BindingFlags.Instance | BindingFlags.Public;
			this._sharedCache = shareCache;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00013E2E File Offset: 0x0001202E
		private Dictionary<ResolverContractKey, JsonContract> GetCache()
		{
			if (this._sharedCache)
			{
				return DefaultContractResolver._sharedContractCache;
			}
			return this._instanceContractCache;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00013E44 File Offset: 0x00012044
		private void UpdateCache(Dictionary<ResolverContractKey, JsonContract> cache)
		{
			if (this._sharedCache)
			{
				DefaultContractResolver._sharedContractCache = cache;
				return;
			}
			this._instanceContractCache = cache;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00013E5C File Offset: 0x0001205C
		public virtual JsonContract ResolveContract(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ResolverContractKey resolverContractKey = new ResolverContractKey(base.GetType(), type);
			Dictionary<ResolverContractKey, JsonContract> dictionary = this.GetCache();
			JsonContract jsonContract;
			if (dictionary == null || !dictionary.TryGetValue(resolverContractKey, out jsonContract))
			{
				jsonContract = this.CreateContract(type);
				lock (DefaultContractResolver._typeContractCacheLock)
				{
					dictionary = this.GetCache();
					Dictionary<ResolverContractKey, JsonContract> dictionary2 = ((dictionary != null) ? new Dictionary<ResolverContractKey, JsonContract>(dictionary) : new Dictionary<ResolverContractKey, JsonContract>());
					dictionary2[resolverContractKey] = jsonContract;
					this.UpdateCache(dictionary2);
				}
			}
			return jsonContract;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00013F0C File Offset: 0x0001210C
		protected virtual List<MemberInfo> GetSerializableMembers(Type objectType)
		{
			DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(objectType);
			List<MemberInfo> list = (from m in ReflectionUtils.GetFieldsAndProperties(objectType, this.DefaultMembersSearchFlags)
				where !ReflectionUtils.IsIndexedProperty(m)
				select m).ToList<MemberInfo>();
			List<MemberInfo> list2 = (from m in ReflectionUtils.GetFieldsAndProperties(objectType, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
				where !ReflectionUtils.IsIndexedProperty(m)
				select m).ToList<MemberInfo>();
			List<MemberInfo> list3 = new List<MemberInfo>();
			foreach (MemberInfo memberInfo in list2)
			{
				if (this.SerializeCompilerGeneratedMembers || !memberInfo.IsDefined(typeof(CompilerGeneratedAttribute), true))
				{
					if (list.Contains(memberInfo))
					{
						list3.Add(memberInfo);
					}
					else if (JsonTypeReflector.GetAttribute<JsonPropertyAttribute>(memberInfo) != null)
					{
						list3.Add(memberInfo);
					}
					else if (dataContractAttribute != null && JsonTypeReflector.GetAttribute<DataMemberAttribute>(memberInfo) != null)
					{
						list3.Add(memberInfo);
					}
				}
			}
			Type type;
			if (objectType.AssignableToTypeName("System.Data.Objects.DataClasses.EntityObject", out type))
			{
				list3 = list3.Where(new Func<MemberInfo, bool>(this.ShouldSerializeEntityMember)).ToList<MemberInfo>();
			}
			return list3;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00014048 File Offset: 0x00012248
		private bool ShouldSerializeEntityMember(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			return propertyInfo == null || !propertyInfo.PropertyType.IsGenericType || !(propertyInfo.PropertyType.GetGenericTypeDefinition().FullName == "System.Data.Objects.DataClasses.EntityReference`1");
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000140A0 File Offset: 0x000122A0
		protected virtual JsonObjectContract CreateObjectContract(Type objectType)
		{
			JsonObjectContract jsonObjectContract = new JsonObjectContract(objectType);
			this.InitializeContract(jsonObjectContract);
			jsonObjectContract.MemberSerialization = JsonTypeReflector.GetObjectMemberSerialization(objectType);
			jsonObjectContract.Properties.AddRange(this.CreateProperties(jsonObjectContract.UnderlyingType, jsonObjectContract.MemberSerialization));
			if (objectType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any((ConstructorInfo c) => c.IsDefined(typeof(JsonConstructorAttribute), true)))
			{
				ConstructorInfo attributeConstructor = this.GetAttributeConstructor(objectType);
				if (attributeConstructor != null)
				{
					jsonObjectContract.OverrideConstructor = attributeConstructor;
					jsonObjectContract.ConstructorParameters.AddRange(this.CreateConstructorParameters(attributeConstructor, jsonObjectContract.Properties));
				}
			}
			else if (jsonObjectContract.DefaultCreator == null || jsonObjectContract.DefaultCreatorNonPublic)
			{
				ConstructorInfo parametrizedConstructor = this.GetParametrizedConstructor(objectType);
				if (parametrizedConstructor != null)
				{
					jsonObjectContract.ParametrizedConstructor = parametrizedConstructor;
					jsonObjectContract.ConstructorParameters.AddRange(this.CreateConstructorParameters(parametrizedConstructor, jsonObjectContract.Properties));
				}
			}
			return jsonObjectContract;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001418C File Offset: 0x0001238C
		private ConstructorInfo GetAttributeConstructor(Type objectType)
		{
			IList<ConstructorInfo> list = (from c in objectType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				where c.IsDefined(typeof(JsonConstructorAttribute), true)
				select c).ToList<ConstructorInfo>();
			if (list.Count > 1)
			{
				throw new Exception("Multiple constructors with the JsonConstructorAttribute.");
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000141F0 File Offset: 0x000123F0
		private ConstructorInfo GetParametrizedConstructor(Type objectType)
		{
			IList<ConstructorInfo> constructors = objectType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
			if (constructors.Count == 1)
			{
				return constructors[0];
			}
			return null;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00014218 File Offset: 0x00012418
		protected virtual IList<JsonProperty> CreateConstructorParameters(ConstructorInfo constructor, JsonPropertyCollection memberProperties)
		{
			ParameterInfo[] parameters = constructor.GetParameters();
			JsonPropertyCollection jsonPropertyCollection = new JsonPropertyCollection(constructor.DeclaringType);
			foreach (ParameterInfo parameterInfo in parameters)
			{
				JsonProperty jsonProperty = memberProperties.GetClosestMatchProperty(parameterInfo.Name);
				if (jsonProperty != null && jsonProperty.PropertyType != parameterInfo.ParameterType)
				{
					jsonProperty = null;
				}
				JsonProperty jsonProperty2 = this.CreatePropertyFromConstructorParameter(jsonProperty, parameterInfo);
				if (jsonProperty2 != null)
				{
					jsonPropertyCollection.AddProperty(jsonProperty2);
				}
			}
			return jsonPropertyCollection;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001428C File Offset: 0x0001248C
		protected virtual JsonProperty CreatePropertyFromConstructorParameter(JsonProperty matchingMemberProperty, ParameterInfo parameterInfo)
		{
			JsonProperty jsonProperty = new JsonProperty();
			jsonProperty.PropertyType = parameterInfo.ParameterType;
			bool flag;
			bool flag2;
			this.SetPropertySettingsFromAttributes(jsonProperty, parameterInfo, parameterInfo.Name, parameterInfo.Member.DeclaringType, MemberSerialization.OptOut, out flag, out flag2);
			jsonProperty.Readable = false;
			jsonProperty.Writable = true;
			if (matchingMemberProperty != null)
			{
				jsonProperty.PropertyName = ((jsonProperty.PropertyName != parameterInfo.Name) ? jsonProperty.PropertyName : matchingMemberProperty.PropertyName);
				jsonProperty.Converter = jsonProperty.Converter ?? matchingMemberProperty.Converter;
				jsonProperty.MemberConverter = jsonProperty.MemberConverter ?? matchingMemberProperty.MemberConverter;
				jsonProperty.DefaultValue = jsonProperty.DefaultValue ?? matchingMemberProperty.DefaultValue;
				jsonProperty.Required = ((jsonProperty.Required != Required.Default) ? jsonProperty.Required : matchingMemberProperty.Required);
				JsonProperty jsonProperty2 = jsonProperty;
				bool? isReference = jsonProperty.IsReference;
				jsonProperty2.IsReference = ((isReference != null) ? new bool?(isReference.GetValueOrDefault()) : matchingMemberProperty.IsReference);
				JsonProperty jsonProperty3 = jsonProperty;
				NullValueHandling? nullValueHandling = jsonProperty.NullValueHandling;
				jsonProperty3.NullValueHandling = ((nullValueHandling != null) ? new NullValueHandling?(nullValueHandling.GetValueOrDefault()) : matchingMemberProperty.NullValueHandling);
				JsonProperty jsonProperty4 = jsonProperty;
				DefaultValueHandling? defaultValueHandling = jsonProperty.DefaultValueHandling;
				jsonProperty4.DefaultValueHandling = ((defaultValueHandling != null) ? new DefaultValueHandling?(defaultValueHandling.GetValueOrDefault()) : matchingMemberProperty.DefaultValueHandling);
				JsonProperty jsonProperty5 = jsonProperty;
				ReferenceLoopHandling? referenceLoopHandling = jsonProperty.ReferenceLoopHandling;
				jsonProperty5.ReferenceLoopHandling = ((referenceLoopHandling != null) ? new ReferenceLoopHandling?(referenceLoopHandling.GetValueOrDefault()) : matchingMemberProperty.ReferenceLoopHandling);
				JsonProperty jsonProperty6 = jsonProperty;
				ObjectCreationHandling? objectCreationHandling = jsonProperty.ObjectCreationHandling;
				jsonProperty6.ObjectCreationHandling = ((objectCreationHandling != null) ? new ObjectCreationHandling?(objectCreationHandling.GetValueOrDefault()) : matchingMemberProperty.ObjectCreationHandling);
				JsonProperty jsonProperty7 = jsonProperty;
				TypeNameHandling? typeNameHandling = jsonProperty.TypeNameHandling;
				jsonProperty7.TypeNameHandling = ((typeNameHandling != null) ? new TypeNameHandling?(typeNameHandling.GetValueOrDefault()) : matchingMemberProperty.TypeNameHandling);
			}
			return jsonProperty;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00014464 File Offset: 0x00012664
		protected virtual JsonConverter ResolveContractConverter(Type objectType)
		{
			return JsonTypeReflector.GetJsonConverter(objectType, objectType);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001446D File Offset: 0x0001266D
		private Func<object> GetDefaultCreator(Type createdType)
		{
			return JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(createdType);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001447C File Offset: 0x0001267C
		private void InitializeContract(JsonContract contract)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(contract.UnderlyingType);
			if (jsonContainerAttribute != null)
			{
				contract.IsReference = jsonContainerAttribute._isReference;
			}
			else
			{
				DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(contract.UnderlyingType);
				if (dataContractAttribute != null && dataContractAttribute.IsReference)
				{
					contract.IsReference = new bool?(true);
				}
			}
			contract.Converter = this.ResolveContractConverter(contract.UnderlyingType);
			contract.InternalConverter = JsonSerializer.GetMatchingConverter(DefaultContractResolver.BuiltInConverters, contract.UnderlyingType);
			if (ReflectionUtils.HasDefaultConstructor(contract.CreatedType, true) || contract.CreatedType.IsValueType)
			{
				contract.DefaultCreator = this.GetDefaultCreator(contract.CreatedType);
				contract.DefaultCreatorNonPublic = !contract.CreatedType.IsValueType && ReflectionUtils.GetDefaultConstructor(contract.CreatedType) == null;
			}
			this.ResolveCallbackMethods(contract, contract.UnderlyingType);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00014550 File Offset: 0x00012750
		private void ResolveCallbackMethods(JsonContract contract, Type t)
		{
			if (t.BaseType != null)
			{
				this.ResolveCallbackMethods(contract, t.BaseType);
			}
			MethodInfo methodInfo;
			MethodInfo methodInfo2;
			MethodInfo methodInfo3;
			MethodInfo methodInfo4;
			MethodInfo methodInfo5;
			this.GetCallbackMethodsForType(t, out methodInfo, out methodInfo2, out methodInfo3, out methodInfo4, out methodInfo5);
			if (methodInfo != null)
			{
				contract.OnSerializing = methodInfo;
			}
			if (methodInfo2 != null)
			{
				contract.OnSerialized = methodInfo2;
			}
			if (methodInfo3 != null)
			{
				contract.OnDeserializing = methodInfo3;
			}
			if (methodInfo4 != null)
			{
				contract.OnDeserialized = methodInfo4;
			}
			if (methodInfo5 != null)
			{
				contract.OnError = methodInfo5;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000145B8 File Offset: 0x000127B8
		private void GetCallbackMethodsForType(Type type, out MethodInfo onSerializing, out MethodInfo onSerialized, out MethodInfo onDeserializing, out MethodInfo onDeserialized, out MethodInfo onError)
		{
			onSerializing = null;
			onSerialized = null;
			onDeserializing = null;
			onDeserialized = null;
			onError = null;
			foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!methodInfo.ContainsGenericParameters)
				{
					Type type2 = null;
					ParameterInfo[] parameters = methodInfo.GetParameters();
					if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnSerializingAttribute), onSerializing, ref type2))
					{
						onSerializing = methodInfo;
					}
					if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnSerializedAttribute), onSerialized, ref type2))
					{
						onSerialized = methodInfo;
					}
					if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnDeserializingAttribute), onDeserializing, ref type2))
					{
						onDeserializing = methodInfo;
					}
					if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnDeserializedAttribute), onDeserialized, ref type2))
					{
						onDeserialized = methodInfo;
					}
					if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnErrorAttribute), onError, ref type2))
					{
						onError = methodInfo;
					}
				}
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001469C File Offset: 0x0001289C
		protected virtual JsonDictionaryContract CreateDictionaryContract(Type objectType)
		{
			JsonDictionaryContract jsonDictionaryContract = new JsonDictionaryContract(objectType);
			this.InitializeContract(jsonDictionaryContract);
			jsonDictionaryContract.PropertyNameResolver = new Func<string, string>(this.ResolvePropertyName);
			return jsonDictionaryContract;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000146CC File Offset: 0x000128CC
		protected virtual JsonArrayContract CreateArrayContract(Type objectType)
		{
			JsonArrayContract jsonArrayContract = new JsonArrayContract(objectType);
			this.InitializeContract(jsonArrayContract);
			return jsonArrayContract;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000146E8 File Offset: 0x000128E8
		protected virtual JsonPrimitiveContract CreatePrimitiveContract(Type objectType)
		{
			JsonPrimitiveContract jsonPrimitiveContract = new JsonPrimitiveContract(objectType);
			this.InitializeContract(jsonPrimitiveContract);
			return jsonPrimitiveContract;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00014704 File Offset: 0x00012904
		protected virtual JsonLinqContract CreateLinqContract(Type objectType)
		{
			JsonLinqContract jsonLinqContract = new JsonLinqContract(objectType);
			this.InitializeContract(jsonLinqContract);
			return jsonLinqContract;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00014738 File Offset: 0x00012938
		protected virtual JsonISerializableContract CreateISerializableContract(Type objectType)
		{
			JsonISerializableContract jsonISerializableContract = new JsonISerializableContract(objectType);
			this.InitializeContract(jsonISerializableContract);
			ConstructorInfo constructor = objectType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[]
			{
				typeof(SerializationInfo),
				typeof(StreamingContext)
			}, null);
			if (constructor != null)
			{
				MethodCall<object, object> methodCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(constructor);
				jsonISerializableContract.ISerializableCreator = (object[] args) => methodCall(null, args);
			}
			return jsonISerializableContract;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000147B0 File Offset: 0x000129B0
		protected virtual JsonStringContract CreateStringContract(Type objectType)
		{
			JsonStringContract jsonStringContract = new JsonStringContract(objectType);
			this.InitializeContract(jsonStringContract);
			return jsonStringContract;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000147CC File Offset: 0x000129CC
		protected virtual JsonContract CreateContract(Type objectType)
		{
			Type type = ReflectionUtils.EnsureNotNullableType(objectType);
			if (JsonConvert.IsJsonPrimitiveType(type))
			{
				return this.CreatePrimitiveContract(type);
			}
			if (JsonTypeReflector.GetJsonObjectAttribute(type) != null)
			{
				return this.CreateObjectContract(type);
			}
			if (JsonTypeReflector.GetJsonArrayAttribute(type) != null)
			{
				return this.CreateArrayContract(type);
			}
			if (type == typeof(JToken) || type.IsSubclassOf(typeof(JToken)))
			{
				return this.CreateLinqContract(type);
			}
			if (CollectionUtils.IsDictionaryType(type))
			{
				return this.CreateDictionaryContract(type);
			}
			if (typeof(IEnumerable).IsAssignableFrom(type))
			{
				return this.CreateArrayContract(type);
			}
			if (DefaultContractResolver.CanConvertToString(type))
			{
				return this.CreateStringContract(type);
			}
			if (typeof(ISerializable).IsAssignableFrom(type))
			{
				return this.CreateISerializableContract(type);
			}
			return this.CreateObjectContract(type);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00014894 File Offset: 0x00012A94
		internal static bool CanConvertToString(Type type)
		{
			TypeConverter converter = ConvertUtils.GetConverter(type);
			return (converter != null && !(converter is ComponentConverter) && !(converter is ReferenceConverter) && converter.GetType() != typeof(TypeConverter) && converter.CanConvertTo(typeof(string))) || (type == typeof(Type) || type.IsSubclassOf(typeof(Type)));
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00014904 File Offset: 0x00012B04
		private static bool IsValidCallback(MethodInfo method, ParameterInfo[] parameters, Type attributeType, MethodInfo currentCallback, ref Type prevAttributeType)
		{
			if (!method.IsDefined(attributeType, false))
			{
				return false;
			}
			if (currentCallback != null)
			{
				throw new Exception("Invalid attribute. Both '{0}' and '{1}' in type '{2}' have '{3}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					method,
					currentCallback,
					DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
					attributeType
				}));
			}
			if (prevAttributeType != null)
			{
				throw new Exception("Invalid Callback. Method '{3}' in type '{2}' has both '{0}' and '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					prevAttributeType,
					attributeType,
					DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
					method
				}));
			}
			if (method.IsVirtual)
			{
				throw new Exception("Virtual Method '{0}' of type '{1}' cannot be marked with '{2}' attribute.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					method,
					DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
					attributeType
				}));
			}
			if (method.ReturnType != typeof(void))
			{
				throw new Exception("Serialization Callback '{1}' in type '{0}' must return void.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
					method
				}));
			}
			if (attributeType == typeof(OnErrorAttribute))
			{
				if (parameters == null || parameters.Length != 2 || parameters[0].ParameterType != typeof(StreamingContext) || parameters[1].ParameterType != typeof(ErrorContext))
				{
					throw new Exception("Serialization Error Callback '{1}' in type '{0}' must have two parameters of type '{2}' and '{3}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
						method,
						typeof(StreamingContext),
						typeof(ErrorContext)
					}));
				}
			}
			else if (parameters == null || parameters.Length != 1 || parameters[0].ParameterType != typeof(StreamingContext))
			{
				throw new Exception("Serialization Callback '{1}' in type '{0}' must have a single parameter of type '{2}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
					method,
					typeof(StreamingContext)
				}));
			}
			prevAttributeType = attributeType;
			return true;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00014B08 File Offset: 0x00012D08
		internal static string GetClrTypeFullName(Type type)
		{
			if (type.IsGenericTypeDefinition || !type.ContainsGenericParameters)
			{
				return type.FullName;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { type.Namespace, type.Name });
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00014B80 File Offset: 0x00012D80
		protected virtual IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			List<MemberInfo> serializableMembers = this.GetSerializableMembers(type);
			if (serializableMembers == null)
			{
				throw new JsonSerializationException("Null collection of seralizable members returned.");
			}
			JsonPropertyCollection jsonPropertyCollection = new JsonPropertyCollection(type);
			foreach (MemberInfo memberInfo in serializableMembers)
			{
				JsonProperty jsonProperty = this.CreateProperty(memberInfo, memberSerialization);
				if (jsonProperty != null)
				{
					jsonPropertyCollection.AddProperty(jsonProperty);
				}
			}
			return jsonPropertyCollection.OrderBy(delegate(JsonProperty p)
			{
				int? order = p.Order;
				if (order == null)
				{
					return -1;
				}
				return order.GetValueOrDefault();
			}).ToList<JsonProperty>();
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00014C24 File Offset: 0x00012E24
		protected virtual IValueProvider CreateMemberValueProvider(MemberInfo member)
		{
			if (this.DynamicCodeGeneration)
			{
				return new DynamicValueProvider(member);
			}
			return new ReflectionValueProvider(member);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00014C3C File Offset: 0x00012E3C
		protected virtual JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty jsonProperty = new JsonProperty();
			jsonProperty.PropertyType = ReflectionUtils.GetMemberUnderlyingType(member);
			jsonProperty.ValueProvider = this.CreateMemberValueProvider(member);
			bool flag;
			bool flag2;
			this.SetPropertySettingsFromAttributes(jsonProperty, member, member.Name, member.DeclaringType, memberSerialization, out flag, out flag2);
			jsonProperty.Readable = ReflectionUtils.CanReadMemberValue(member, flag);
			jsonProperty.Writable = ReflectionUtils.CanSetMemberValue(member, flag, flag2);
			jsonProperty.ShouldSerialize = this.CreateShouldSerializeTest(member);
			this.SetIsSpecifiedActions(jsonProperty, member, flag);
			return jsonProperty;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00014CB4 File Offset: 0x00012EB4
		private void SetPropertySettingsFromAttributes(JsonProperty property, ICustomAttributeProvider attributeProvider, string name, Type declaringType, MemberSerialization memberSerialization, out bool allowNonPublicAccess, out bool hasExplicitAttribute)
		{
			hasExplicitAttribute = false;
			DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(declaringType);
			DataMemberAttribute dataMemberAttribute;
			if (dataContractAttribute != null && attributeProvider is MemberInfo)
			{
				dataMemberAttribute = JsonTypeReflector.GetDataMemberAttribute((MemberInfo)attributeProvider);
			}
			else
			{
				dataMemberAttribute = null;
			}
			JsonPropertyAttribute attribute = JsonTypeReflector.GetAttribute<JsonPropertyAttribute>(attributeProvider);
			if (attribute != null)
			{
				hasExplicitAttribute = true;
			}
			bool flag = JsonTypeReflector.GetAttribute<JsonIgnoreAttribute>(attributeProvider) != null;
			string text;
			if (attribute != null && attribute.PropertyName != null)
			{
				text = attribute.PropertyName;
			}
			else if (dataMemberAttribute != null && dataMemberAttribute.Name != null)
			{
				text = dataMemberAttribute.Name;
			}
			else
			{
				text = name;
			}
			property.PropertyName = this.ResolvePropertyName(text);
			property.UnderlyingName = name;
			if (attribute != null)
			{
				property.Required = attribute.Required;
				property.Order = attribute._order;
			}
			else if (dataMemberAttribute != null)
			{
				property.Required = (dataMemberAttribute.IsRequired ? Required.AllowNull : Required.Default);
				property.Order = ((dataMemberAttribute.Order != -1) ? new int?(dataMemberAttribute.Order) : null);
			}
			else
			{
				property.Required = Required.Default;
			}
			property.Ignored = flag || (memberSerialization == MemberSerialization.OptIn && attribute == null && dataMemberAttribute == null);
			property.Converter = JsonTypeReflector.GetJsonConverter(attributeProvider, property.PropertyType);
			property.MemberConverter = JsonTypeReflector.GetJsonConverter(attributeProvider, property.PropertyType);
			DefaultValueAttribute attribute2 = JsonTypeReflector.GetAttribute<DefaultValueAttribute>(attributeProvider);
			property.DefaultValue = ((attribute2 != null) ? attribute2.Value : null);
			property.NullValueHandling = ((attribute != null) ? attribute._nullValueHandling : null);
			property.DefaultValueHandling = ((attribute != null) ? attribute._defaultValueHandling : null);
			property.ReferenceLoopHandling = ((attribute != null) ? attribute._referenceLoopHandling : null);
			property.ObjectCreationHandling = ((attribute != null) ? attribute._objectCreationHandling : null);
			property.TypeNameHandling = ((attribute != null) ? attribute._typeNameHandling : null);
			property.IsReference = ((attribute != null) ? attribute._isReference : null);
			allowNonPublicAccess = false;
			if ((this.DefaultMembersSearchFlags & BindingFlags.NonPublic) == BindingFlags.NonPublic)
			{
				allowNonPublicAccess = true;
			}
			if (attribute != null)
			{
				allowNonPublicAccess = true;
			}
			if (dataMemberAttribute != null)
			{
				allowNonPublicAccess = true;
				hasExplicitAttribute = true;
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00014EEC File Offset: 0x000130EC
		private Predicate<object> CreateShouldSerializeTest(MemberInfo member)
		{
			MethodInfo method = member.DeclaringType.GetMethod("ShouldSerialize" + member.Name, new Type[0]);
			if (method == null || method.ReturnType != typeof(bool))
			{
				return null;
			}
			MethodCall<object, object> shouldSerializeCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return (object o) => (bool)shouldSerializeCall(o, new object[0]);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00014F70 File Offset: 0x00013170
		private void SetIsSpecifiedActions(JsonProperty property, MemberInfo member, bool allowNonPublicAccess)
		{
			MemberInfo memberInfo = member.DeclaringType.GetProperty(member.Name + "Specified");
			if (memberInfo == null)
			{
				memberInfo = member.DeclaringType.GetField(member.Name + "Specified");
			}
			if (memberInfo == null || ReflectionUtils.GetMemberUnderlyingType(memberInfo) != typeof(bool))
			{
				return;
			}
			Func<object, object> specifiedPropertyGet = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(memberInfo);
			property.GetIsSpecified = (object o) => (bool)specifiedPropertyGet(o);
			if (ReflectionUtils.CanSetMemberValue(memberInfo, allowNonPublicAccess, false))
			{
				property.SetIsSpecified = JsonTypeReflector.ReflectionDelegateFactory.CreateSet<object>(memberInfo);
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00015012 File Offset: 0x00013212
		protected internal virtual string ResolvePropertyName(string propertyName)
		{
			return propertyName;
		}

		// Token: 0x0400018E RID: 398
		private static readonly IContractResolver _instance = new DefaultContractResolver(true);

		// Token: 0x0400018F RID: 399
		private static readonly IList<JsonConverter> BuiltInConverters = new List<JsonConverter>
		{
			new EntityKeyMemberConverter(),
			new BinaryConverter(),
			new KeyValuePairConverter(),
			new XmlNodeConverter(),
			new DataSetConverter(),
			new DataTableConverter(),
			new BsonObjectIdConverter()
		};

		// Token: 0x04000190 RID: 400
		private static Dictionary<ResolverContractKey, JsonContract> _sharedContractCache;

		// Token: 0x04000191 RID: 401
		private static readonly object _typeContractCacheLock = new object();

		// Token: 0x04000192 RID: 402
		private Dictionary<ResolverContractKey, JsonContract> _instanceContractCache;

		// Token: 0x04000193 RID: 403
		private readonly bool _sharedCache;
	}
}
