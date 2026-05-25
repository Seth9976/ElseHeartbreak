using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000096 RID: 150
	internal static class JsonTypeReflector
	{
		// Token: 0x0600074A RID: 1866 RVA: 0x0001A513 File Offset: 0x00018713
		public static JsonContainerAttribute GetJsonContainerAttribute(Type type)
		{
			return CachedAttributeGetter<JsonContainerAttribute>.GetAttribute(type);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001A51B File Offset: 0x0001871B
		public static JsonObjectAttribute GetJsonObjectAttribute(Type type)
		{
			return JsonTypeReflector.GetJsonContainerAttribute(type) as JsonObjectAttribute;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001A528 File Offset: 0x00018728
		public static JsonArrayAttribute GetJsonArrayAttribute(Type type)
		{
			return JsonTypeReflector.GetJsonContainerAttribute(type) as JsonArrayAttribute;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001A538 File Offset: 0x00018738
		public static DataContractAttribute GetDataContractAttribute(Type type)
		{
			DataContractAttribute dataContractAttribute = null;
			Type type2 = type;
			while (dataContractAttribute == null && type2 != null)
			{
				dataContractAttribute = CachedAttributeGetter<DataContractAttribute>.GetAttribute(type2);
				type2 = type2.BaseType;
			}
			return dataContractAttribute;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001A560 File Offset: 0x00018760
		public static DataMemberAttribute GetDataMemberAttribute(MemberInfo memberInfo)
		{
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				return CachedAttributeGetter<DataMemberAttribute>.GetAttribute(memberInfo);
			}
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			DataMemberAttribute dataMemberAttribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo);
			if (dataMemberAttribute == null && propertyInfo.IsVirtual())
			{
				Type type = propertyInfo.DeclaringType;
				while (dataMemberAttribute == null && type != null)
				{
					PropertyInfo propertyInfo2 = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(type, propertyInfo);
					if (propertyInfo2 != null && propertyInfo2.IsVirtual())
					{
						dataMemberAttribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo2);
					}
					type = type.BaseType;
				}
			}
			return dataMemberAttribute;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001A5CC File Offset: 0x000187CC
		public static MemberSerialization GetObjectMemberSerialization(Type objectType)
		{
			JsonObjectAttribute jsonObjectAttribute = JsonTypeReflector.GetJsonObjectAttribute(objectType);
			if (jsonObjectAttribute != null)
			{
				return jsonObjectAttribute.MemberSerialization;
			}
			DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(objectType);
			if (dataContractAttribute != null)
			{
				return MemberSerialization.OptIn;
			}
			return MemberSerialization.OptOut;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001A5F7 File Offset: 0x000187F7
		private static Type GetJsonConverterType(ICustomAttributeProvider attributeProvider)
		{
			return JsonTypeReflector.JsonConverterTypeCache.Get(attributeProvider);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001A604 File Offset: 0x00018804
		private static Type GetJsonConverterTypeFromAttribute(ICustomAttributeProvider attributeProvider)
		{
			JsonConverterAttribute attribute = JsonTypeReflector.GetAttribute<JsonConverterAttribute>(attributeProvider);
			if (attribute == null)
			{
				return null;
			}
			return attribute.ConverterType;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001A624 File Offset: 0x00018824
		public static JsonConverter GetJsonConverter(ICustomAttributeProvider attributeProvider, Type targetConvertedType)
		{
			Type jsonConverterType = JsonTypeReflector.GetJsonConverterType(attributeProvider);
			if (jsonConverterType == null)
			{
				return null;
			}
			JsonConverter jsonConverter = JsonConverterAttribute.CreateJsonConverterInstance(jsonConverterType);
			if (!jsonConverter.CanConvert(targetConvertedType))
			{
				throw new JsonSerializationException("JsonConverter {0} on {1} is not compatible with member type {2}.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					jsonConverter.GetType().Name,
					attributeProvider,
					targetConvertedType.Name
				}));
			}
			return jsonConverter;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001A686 File Offset: 0x00018886
		public static TypeConverter GetTypeConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001A68E File Offset: 0x0001888E
		private static Type GetAssociatedMetadataType(Type type)
		{
			return JsonTypeReflector.AssociatedMetadataTypesCache.Get(type);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001A69C File Offset: 0x0001889C
		private static Type GetAssociateMetadataTypeFromAttribute(Type type)
		{
			Type metadataTypeAttributeType = JsonTypeReflector.GetMetadataTypeAttributeType();
			if (metadataTypeAttributeType == null)
			{
				return null;
			}
			object obj = type.GetCustomAttributes(metadataTypeAttributeType, true).SingleOrDefault<object>();
			if (obj == null)
			{
				return null;
			}
			IMetadataTypeAttribute metadataTypeAttribute = (JsonTypeReflector.DynamicCodeGeneration ? DynamicWrapper.CreateWrapper<IMetadataTypeAttribute>(obj) : new LateBoundMetadataTypeAttribute(obj));
			return metadataTypeAttribute.MetadataClassType;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001A6E4 File Offset: 0x000188E4
		private static Type GetMetadataTypeAttributeType()
		{
			if (JsonTypeReflector._cachedMetadataTypeAttributeType == null)
			{
				Type type = Type.GetType("System.ComponentModel.DataAnnotations.MetadataTypeAttribute, System.ComponentModel.DataAnnotations, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
				if (type == null)
				{
					return null;
				}
				JsonTypeReflector._cachedMetadataTypeAttributeType = type;
			}
			return JsonTypeReflector._cachedMetadataTypeAttributeType;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001A718 File Offset: 0x00018918
		private static T GetAttribute<T>(Type type) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(type);
			T t;
			if (associatedMetadataType != null)
			{
				t = ReflectionUtils.GetAttribute<T>(associatedMetadataType, true);
				if (t != null)
				{
					return t;
				}
			}
			t = ReflectionUtils.GetAttribute<T>(type, true);
			if (t != null)
			{
				return t;
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				t = ReflectionUtils.GetAttribute<T>(type2, true);
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001A794 File Offset: 0x00018994
		private static T GetAttribute<T>(MemberInfo memberInfo) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(memberInfo.DeclaringType);
			T t;
			if (associatedMetadataType != null)
			{
				MemberInfo memberInfoFromType = ReflectionUtils.GetMemberInfoFromType(associatedMetadataType, memberInfo);
				if (memberInfoFromType != null)
				{
					t = ReflectionUtils.GetAttribute<T>(memberInfoFromType, true);
					if (t != null)
					{
						return t;
					}
				}
			}
			t = ReflectionUtils.GetAttribute<T>(memberInfo, true);
			if (t != null)
			{
				return t;
			}
			foreach (Type type in memberInfo.DeclaringType.GetInterfaces())
			{
				MemberInfo memberInfoFromType2 = ReflectionUtils.GetMemberInfoFromType(type, memberInfo);
				if (memberInfoFromType2 != null)
				{
					t = ReflectionUtils.GetAttribute<T>(memberInfoFromType2, true);
					if (t != null)
					{
						return t;
					}
				}
			}
			return default(T);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001A838 File Offset: 0x00018A38
		public static T GetAttribute<T>(ICustomAttributeProvider attributeProvider) where T : Attribute
		{
			Type type = attributeProvider as Type;
			if (type != null)
			{
				return JsonTypeReflector.GetAttribute<T>(type);
			}
			MemberInfo memberInfo = attributeProvider as MemberInfo;
			if (memberInfo != null)
			{
				return JsonTypeReflector.GetAttribute<T>(memberInfo);
			}
			return ReflectionUtils.GetAttribute<T>(attributeProvider, true);
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001A870 File Offset: 0x00018A70
		public static bool DynamicCodeGeneration
		{
			get
			{
				if (JsonTypeReflector._dynamicCodeGeneration == null)
				{
					try
					{
						new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Demand();
						new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess).Demand();
						new SecurityPermission(SecurityPermissionFlag.SkipVerification).Demand();
						new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
						new SecurityPermission(PermissionState.Unrestricted).Demand();
						JsonTypeReflector._dynamicCodeGeneration = new bool?(true);
					}
					catch (Exception)
					{
						JsonTypeReflector._dynamicCodeGeneration = new bool?(false);
					}
				}
				return JsonTypeReflector._dynamicCodeGeneration.Value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001A8F8 File Offset: 0x00018AF8
		public static ReflectionDelegateFactory ReflectionDelegateFactory
		{
			get
			{
				if (JsonTypeReflector.DynamicCodeGeneration)
				{
					return DynamicReflectionDelegateFactory.Instance;
				}
				return LateBoundReflectionDelegateFactory.Instance;
			}
		}

		// Token: 0x04000240 RID: 576
		public const string IdPropertyName = "$id";

		// Token: 0x04000241 RID: 577
		public const string RefPropertyName = "$ref";

		// Token: 0x04000242 RID: 578
		public const string TypePropertyName = "$type";

		// Token: 0x04000243 RID: 579
		public const string ValuePropertyName = "$value";

		// Token: 0x04000244 RID: 580
		public const string ArrayValuesPropertyName = "$values";

		// Token: 0x04000245 RID: 581
		public const string ShouldSerializePrefix = "ShouldSerialize";

		// Token: 0x04000246 RID: 582
		public const string SpecifiedPostfix = "Specified";

		// Token: 0x04000247 RID: 583
		private const string MetadataTypeAttributeTypeName = "System.ComponentModel.DataAnnotations.MetadataTypeAttribute, System.ComponentModel.DataAnnotations, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

		// Token: 0x04000248 RID: 584
		private static readonly ThreadSafeStore<ICustomAttributeProvider, Type> JsonConverterTypeCache = new ThreadSafeStore<ICustomAttributeProvider, Type>(new Func<ICustomAttributeProvider, Type>(JsonTypeReflector.GetJsonConverterTypeFromAttribute));

		// Token: 0x04000249 RID: 585
		private static readonly ThreadSafeStore<Type, Type> AssociatedMetadataTypesCache = new ThreadSafeStore<Type, Type>(new Func<Type, Type>(JsonTypeReflector.GetAssociateMetadataTypeFromAttribute));

		// Token: 0x0400024A RID: 586
		private static Type _cachedMetadataTypeAttributeType;

		// Token: 0x0400024B RID: 587
		private static bool? _dynamicCodeGeneration;
	}
}
