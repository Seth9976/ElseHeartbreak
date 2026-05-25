using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C0 RID: 192
	internal static class ReflectionUtils
	{
		// Token: 0x06000890 RID: 2192 RVA: 0x0001F1D4 File Offset: 0x0001D3D4
		public static bool IsVirtual(this PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			MethodInfo methodInfo = propertyInfo.GetGetMethod();
			if (methodInfo != null && methodInfo.IsVirtual)
			{
				return true;
			}
			methodInfo = propertyInfo.GetSetMethod();
			return methodInfo != null && methodInfo.IsVirtual;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001F215 File Offset: 0x0001D415
		public static Type GetObjectType(object v)
		{
			if (v == null)
			{
				return null;
			}
			return v.GetType();
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001F222 File Offset: 0x0001D422
		public static string GetTypeName(Type t, FormatterAssemblyStyle assemblyFormat)
		{
			return ReflectionUtils.GetTypeName(t, assemblyFormat, null);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001F22C File Offset: 0x0001D42C
		public static string GetTypeName(Type t, FormatterAssemblyStyle assemblyFormat, SerializationBinder binder)
		{
			string assemblyQualifiedName = t.AssemblyQualifiedName;
			switch (assemblyFormat)
			{
			case FormatterAssemblyStyle.Simple:
				return ReflectionUtils.RemoveAssemblyDetails(assemblyQualifiedName);
			case FormatterAssemblyStyle.Full:
				return t.AssemblyQualifiedName;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001F268 File Offset: 0x0001D468
		private static string RemoveAssemblyDetails(string fullyQualifiedTypeName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			foreach (char c in fullyQualifiedTypeName)
			{
				char c2 = c;
				if (c2 != ',')
				{
					switch (c2)
					{
					case '[':
						flag = false;
						flag2 = false;
						stringBuilder.Append(c);
						goto IL_0077;
					case ']':
						flag = false;
						flag2 = false;
						stringBuilder.Append(c);
						goto IL_0077;
					}
					if (!flag2)
					{
						stringBuilder.Append(c);
					}
				}
				else if (!flag)
				{
					flag = true;
					stringBuilder.Append(c);
				}
				else
				{
					flag2 = true;
				}
				IL_0077:;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001F300 File Offset: 0x0001D500
		public static bool IsInstantiatableType(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return !t.IsAbstract && !t.IsInterface && !t.IsArray && !t.IsGenericTypeDefinition && t != typeof(void) && ReflectionUtils.HasDefaultConstructor(t);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001F352 File Offset: 0x0001D552
		public static bool HasDefaultConstructor(Type t)
		{
			return ReflectionUtils.HasDefaultConstructor(t, false);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001F35B File Offset: 0x0001D55B
		public static bool HasDefaultConstructor(Type t, bool nonPublic)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return t.IsValueType || ReflectionUtils.GetDefaultConstructor(t, nonPublic) != null;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001F37F File Offset: 0x0001D57F
		public static ConstructorInfo GetDefaultConstructor(Type t)
		{
			return ReflectionUtils.GetDefaultConstructor(t, false);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001F388 File Offset: 0x0001D588
		public static ConstructorInfo GetDefaultConstructor(Type t, bool nonPublic)
		{
			BindingFlags bindingFlags = BindingFlags.Public;
			if (nonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			return t.GetConstructor(bindingFlags | BindingFlags.Instance, null, new Type[0], null);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001F3B1 File Offset: 0x0001D5B1
		public static bool IsNullable(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return !t.IsValueType || ReflectionUtils.IsNullableType(t);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001F3CE File Offset: 0x0001D5CE
		public static bool IsNullableType(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001F3F7 File Offset: 0x0001D5F7
		public static Type EnsureNotNullableType(Type t)
		{
			if (!ReflectionUtils.IsNullableType(t))
			{
				return t;
			}
			return Nullable.GetUnderlyingType(t);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001F40C File Offset: 0x0001D60C
		public static bool IsUnitializedValue(object value)
		{
			if (value == null)
			{
				return true;
			}
			object obj = ReflectionUtils.CreateUnitializedValue(value.GetType());
			return value.Equals(obj);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001F434 File Offset: 0x0001D634
		public static object CreateUnitializedValue(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsGenericTypeDefinition)
			{
				throw new ArgumentException("Type {0} is a generic type definition and cannot be instantiated.".FormatWith(CultureInfo.InvariantCulture, new object[] { type }), "type");
			}
			if (type.IsClass || type.IsInterface || type == typeof(void))
			{
				return null;
			}
			if (type.IsValueType)
			{
				return Activator.CreateInstance(type);
			}
			throw new ArgumentException("Type {0} cannot be instantiated.".FormatWith(CultureInfo.InvariantCulture, new object[] { type }), "type");
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001F4CD File Offset: 0x0001D6CD
		public static bool IsPropertyIndexed(PropertyInfo property)
		{
			ValidationUtils.ArgumentNotNull(property, "property");
			return !CollectionUtils.IsNullOrEmpty<ParameterInfo>(property.GetIndexParameters());
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001F4E8 File Offset: 0x0001D6E8
		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition)
		{
			Type type2;
			return ReflectionUtils.ImplementsGenericDefinition(type, genericInterfaceDefinition, out type2);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001F500 File Offset: 0x0001D700
		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(genericInterfaceDefinition, "genericInterfaceDefinition");
			if (!genericInterfaceDefinition.IsInterface || !genericInterfaceDefinition.IsGenericTypeDefinition)
			{
				throw new ArgumentNullException("'{0}' is not a generic interface definition.".FormatWith(CultureInfo.InvariantCulture, new object[] { genericInterfaceDefinition }));
			}
			if (type.IsInterface && type.IsGenericType)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (genericInterfaceDefinition == genericTypeDefinition)
				{
					implementingType = type;
					return true;
				}
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType)
				{
					Type genericTypeDefinition2 = type2.GetGenericTypeDefinition();
					if (genericInterfaceDefinition == genericTypeDefinition2)
					{
						implementingType = type2;
						return true;
					}
				}
			}
			implementingType = null;
			return false;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001F5B8 File Offset: 0x0001D7B8
		public static bool AssignableToTypeName(this Type type, string fullTypeName, out Type match)
		{
			for (Type type2 = type; type2 != null; type2 = type2.BaseType)
			{
				if (string.Equals(type2.FullName, fullTypeName, StringComparison.Ordinal))
				{
					match = type2;
					return true;
				}
			}
			foreach (Type type3 in type.GetInterfaces())
			{
				if (string.Equals(type3.Name, fullTypeName, StringComparison.Ordinal))
				{
					match = type;
					return true;
				}
			}
			match = null;
			return false;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001F624 File Offset: 0x0001D824
		public static bool AssignableToTypeName(this Type type, string fullTypeName)
		{
			Type type2;
			return type.AssignableToTypeName(fullTypeName, out type2);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001F63C File Offset: 0x0001D83C
		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition)
		{
			Type type2;
			return ReflectionUtils.InheritsGenericDefinition(type, genericClassDefinition, out type2);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001F654 File Offset: 0x0001D854
		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition, out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(genericClassDefinition, "genericClassDefinition");
			if (!genericClassDefinition.IsClass || !genericClassDefinition.IsGenericTypeDefinition)
			{
				throw new ArgumentNullException("'{0}' is not a generic class definition.".FormatWith(CultureInfo.InvariantCulture, new object[] { genericClassDefinition }));
			}
			return ReflectionUtils.InheritsGenericDefinitionInternal(type, genericClassDefinition, out implementingType);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001F6B0 File Offset: 0x0001D8B0
		private static bool InheritsGenericDefinitionInternal(Type currentType, Type genericClassDefinition, out Type implementingType)
		{
			if (currentType.IsGenericType)
			{
				Type genericTypeDefinition = currentType.GetGenericTypeDefinition();
				if (genericClassDefinition == genericTypeDefinition)
				{
					implementingType = currentType;
					return true;
				}
			}
			if (currentType.BaseType == null)
			{
				implementingType = null;
				return false;
			}
			return ReflectionUtils.InheritsGenericDefinitionInternal(currentType.BaseType, genericClassDefinition, out implementingType);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001F6F0 File Offset: 0x0001D8F0
		public static Type GetCollectionItemType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			Type type2;
			if (ReflectionUtils.ImplementsGenericDefinition(type, typeof(IEnumerable<>), out type2))
			{
				if (type2.IsGenericTypeDefinition)
				{
					throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, new object[] { type }));
				}
				return type2.GetGenericArguments()[0];
			}
			else
			{
				if (typeof(IEnumerable).IsAssignableFrom(type))
				{
					return null;
				}
				throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, new object[] { type }));
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001F794 File Offset: 0x0001D994
		public static void GetDictionaryKeyValueTypes(Type dictionaryType, out Type keyType, out Type valueType)
		{
			ValidationUtils.ArgumentNotNull(dictionaryType, "type");
			Type type;
			if (ReflectionUtils.ImplementsGenericDefinition(dictionaryType, typeof(IDictionary<, >), out type))
			{
				if (type.IsGenericTypeDefinition)
				{
					throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, new object[] { dictionaryType }));
				}
				Type[] genericArguments = type.GetGenericArguments();
				keyType = genericArguments[0];
				valueType = genericArguments[1];
				return;
			}
			else
			{
				if (typeof(IDictionary).IsAssignableFrom(dictionaryType))
				{
					keyType = null;
					valueType = null;
					return;
				}
				throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, new object[] { dictionaryType }));
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001F834 File Offset: 0x0001DA34
		public static Type GetDictionaryValueType(Type dictionaryType)
		{
			Type type;
			Type type2;
			ReflectionUtils.GetDictionaryKeyValueTypes(dictionaryType, out type, out type2);
			return type2;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001F84C File Offset: 0x0001DA4C
		public static Type GetDictionaryKeyType(Type dictionaryType)
		{
			Type type;
			Type type2;
			ReflectionUtils.GetDictionaryKeyValueTypes(dictionaryType, out type, out type2);
			return type;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001F864 File Offset: 0x0001DA64
		public static bool ItemsUnitializedValue<T>(IList<T> list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			Type collectionItemType = ReflectionUtils.GetCollectionItemType(list.GetType());
			if (collectionItemType.IsValueType)
			{
				object obj = ReflectionUtils.CreateUnitializedValue(collectionItemType);
				for (int i = 0; i < list.Count; i++)
				{
					T t = list[i];
					if (!t.Equals(obj))
					{
						return false;
					}
				}
			}
			else
			{
				if (!collectionItemType.IsClass)
				{
					throw new Exception("Type {0} is neither a ValueType or a Class.".FormatWith(CultureInfo.InvariantCulture, new object[] { collectionItemType }));
				}
				for (int j = 0; j < list.Count; j++)
				{
					object obj2 = list[j];
					if (obj2 != null)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001F91C File Offset: 0x0001DB1C
		public static Type GetMemberUnderlyingType(MemberInfo member)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			MemberTypes memberType = member.MemberType;
			switch (memberType)
			{
			case MemberTypes.Event:
				return ((EventInfo)member).EventHandlerType;
			case MemberTypes.Constructor | MemberTypes.Event:
				break;
			case MemberTypes.Field:
				return ((FieldInfo)member).FieldType;
			default:
				if (memberType == MemberTypes.Property)
				{
					return ((PropertyInfo)member).PropertyType;
				}
				break;
			}
			throw new ArgumentException("MemberInfo must be of type FieldInfo, PropertyInfo or EventInfo", "member");
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001F98C File Offset: 0x0001DB8C
		public static bool IsIndexedProperty(MemberInfo member)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			PropertyInfo propertyInfo = member as PropertyInfo;
			return propertyInfo != null && ReflectionUtils.IsIndexedProperty(propertyInfo);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001F9B6 File Offset: 0x0001DBB6
		public static bool IsIndexedProperty(PropertyInfo property)
		{
			ValidationUtils.ArgumentNotNull(property, "property");
			return property.GetIndexParameters().Length > 0;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0001F9D0 File Offset: 0x0001DBD0
		public static object GetMemberValue(MemberInfo member, object target)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			ValidationUtils.ArgumentNotNull(target, "target");
			MemberTypes memberType = member.MemberType;
			if (memberType != MemberTypes.Field)
			{
				if (memberType == MemberTypes.Property)
				{
					try
					{
						return ((PropertyInfo)member).GetValue(target, null);
					}
					catch (TargetParameterCountException ex)
					{
						throw new ArgumentException("MemberInfo '{0}' has index parameters".FormatWith(CultureInfo.InvariantCulture, new object[] { member.Name }), ex);
					}
				}
				throw new ArgumentException("MemberInfo '{0}' is not of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					CultureInfo.InvariantCulture,
					member.Name
				}), "member");
			}
			return ((FieldInfo)member).GetValue(target);
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001FA94 File Offset: 0x0001DC94
		public static void SetMemberValue(MemberInfo member, object target, object value)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			ValidationUtils.ArgumentNotNull(target, "target");
			MemberTypes memberType = member.MemberType;
			if (memberType == MemberTypes.Field)
			{
				((FieldInfo)member).SetValue(target, value);
				return;
			}
			if (memberType != MemberTypes.Property)
			{
				throw new ArgumentException("MemberInfo '{0}' must be of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, new object[] { member.Name }), "member");
			}
			((PropertyInfo)member).SetValue(target, value, null);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001FB10 File Offset: 0x0001DD10
		public static bool CanReadMemberValue(MemberInfo member, bool nonPublic)
		{
			MemberTypes memberType = member.MemberType;
			if (memberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				return nonPublic || fieldInfo.IsPublic;
			}
			if (memberType != MemberTypes.Property)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)member;
			return propertyInfo.CanRead && (nonPublic || propertyInfo.GetGetMethod(nonPublic) != null);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001FB6C File Offset: 0x0001DD6C
		public static bool CanSetMemberValue(MemberInfo member, bool nonPublic, bool canSetReadOnly)
		{
			MemberTypes memberType = member.MemberType;
			if (memberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				return (!fieldInfo.IsInitOnly || canSetReadOnly) && (nonPublic || fieldInfo.IsPublic);
			}
			if (memberType != MemberTypes.Property)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)member;
			return propertyInfo.CanWrite && (nonPublic || propertyInfo.GetSetMethod(nonPublic) != null);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001FBD5 File Offset: 0x0001DDD5
		public static List<MemberInfo> GetFieldsAndProperties<T>(BindingFlags bindingAttr)
		{
			return ReflectionUtils.GetFieldsAndProperties(typeof(T), bindingAttr);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001FD48 File Offset: 0x0001DF48
		public static List<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags bindingAttr)
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.AddRange(ReflectionUtils.GetFields(type, bindingAttr));
			list.AddRange(ReflectionUtils.GetProperties(type, bindingAttr));
			List<MemberInfo> list2 = new List<MemberInfo>(list.Count);
			var enumerable = from m in list
				group m by m.Name into g
				select new
				{
					Count = g.Count<MemberInfo>(),
					Members = g.Cast<MemberInfo>()
				};
			foreach (var <>f__AnonymousType in enumerable)
			{
				if (<>f__AnonymousType.Count == 1)
				{
					list2.Add(<>f__AnonymousType.Members.First<MemberInfo>());
				}
				else
				{
					IEnumerable<MemberInfo> enumerable2 = <>f__AnonymousType.Members.Where((MemberInfo m) => !ReflectionUtils.IsOverridenGenericMember(m, bindingAttr) || m.Name == "Item");
					list2.AddRange(enumerable2);
				}
			}
			return list2;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001FE68 File Offset: 0x0001E068
		private static bool IsOverridenGenericMember(MemberInfo memberInfo, BindingFlags bindingAttr)
		{
			if (memberInfo.MemberType != MemberTypes.Field && memberInfo.MemberType != MemberTypes.Property)
			{
				throw new ArgumentException("Member must be a field or property.");
			}
			Type declaringType = memberInfo.DeclaringType;
			if (!declaringType.IsGenericType)
			{
				return false;
			}
			Type genericTypeDefinition = declaringType.GetGenericTypeDefinition();
			if (genericTypeDefinition == null)
			{
				return false;
			}
			MemberInfo[] member = genericTypeDefinition.GetMember(memberInfo.Name, bindingAttr);
			if (member.Length == 0)
			{
				return false;
			}
			Type memberUnderlyingType = ReflectionUtils.GetMemberUnderlyingType(member[0]);
			return memberUnderlyingType.IsGenericParameter;
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001FED9 File Offset: 0x0001E0D9
		public static T GetAttribute<T>(ICustomAttributeProvider attributeProvider) where T : Attribute
		{
			return ReflectionUtils.GetAttribute<T>(attributeProvider, true);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001FEE4 File Offset: 0x0001E0E4
		public static T GetAttribute<T>(ICustomAttributeProvider attributeProvider, bool inherit) where T : Attribute
		{
			T[] attributes = ReflectionUtils.GetAttributes<T>(attributeProvider, inherit);
			return CollectionUtils.GetSingleItem<T>(attributes, true);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001FF00 File Offset: 0x0001E100
		public static T[] GetAttributes<T>(ICustomAttributeProvider attributeProvider, bool inherit) where T : Attribute
		{
			ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
			if (attributeProvider is Type)
			{
				return (T[])((Type)attributeProvider).GetCustomAttributes(typeof(T), inherit);
			}
			if (attributeProvider is Assembly)
			{
				return (T[])Attribute.GetCustomAttributes((Assembly)attributeProvider, typeof(T), inherit);
			}
			if (attributeProvider is MemberInfo)
			{
				return (T[])Attribute.GetCustomAttributes((MemberInfo)attributeProvider, typeof(T), inherit);
			}
			if (attributeProvider is Module)
			{
				return (T[])Attribute.GetCustomAttributes((Module)attributeProvider, typeof(T), inherit);
			}
			if (attributeProvider is ParameterInfo)
			{
				return (T[])Attribute.GetCustomAttributes((ParameterInfo)attributeProvider, typeof(T), inherit);
			}
			return (T[])attributeProvider.GetCustomAttributes(typeof(T), inherit);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001FFE2 File Offset: 0x0001E1E2
		public static string GetNameAndAssessmblyName(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return t.FullName + ", " + t.Assembly.GetName().Name;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00020010 File Offset: 0x0001E210
		public static Type MakeGenericType(Type genericTypeDefinition, params Type[] innerTypes)
		{
			ValidationUtils.ArgumentNotNull(genericTypeDefinition, "genericTypeDefinition");
			ValidationUtils.ArgumentNotNullOrEmpty<Type>(innerTypes, "innerTypes");
			ValidationUtils.ArgumentConditionTrue(genericTypeDefinition.IsGenericTypeDefinition, "genericTypeDefinition", "Type {0} is not a generic type definition.".FormatWith(CultureInfo.InvariantCulture, new object[] { genericTypeDefinition }));
			return genericTypeDefinition.MakeGenericType(innerTypes);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00020068 File Offset: 0x0001E268
		public static object CreateGeneric(Type genericTypeDefinition, Type innerType, params object[] args)
		{
			return ReflectionUtils.CreateGeneric(genericTypeDefinition, new Type[] { innerType }, args);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00020096 File Offset: 0x0001E296
		public static object CreateGeneric(Type genericTypeDefinition, IList<Type> innerTypes, params object[] args)
		{
			return ReflectionUtils.CreateGeneric(genericTypeDefinition, innerTypes, (Type t, IList<object> a) => ReflectionUtils.CreateInstance(t, a.ToArray<object>()), args);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x000200C0 File Offset: 0x0001E2C0
		public static object CreateGeneric(Type genericTypeDefinition, IList<Type> innerTypes, Func<Type, IList<object>, object> instanceCreator, params object[] args)
		{
			ValidationUtils.ArgumentNotNull(genericTypeDefinition, "genericTypeDefinition");
			ValidationUtils.ArgumentNotNullOrEmpty<Type>(innerTypes, "innerTypes");
			ValidationUtils.ArgumentNotNull(instanceCreator, "createInstance");
			Type type = ReflectionUtils.MakeGenericType(genericTypeDefinition, innerTypes.ToArray<Type>());
			return instanceCreator(type, args);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00020103 File Offset: 0x0001E303
		public static bool IsCompatibleValue(object value, Type type)
		{
			if (value == null)
			{
				return ReflectionUtils.IsNullable(type);
			}
			return type.IsAssignableFrom(value.GetType());
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00020120 File Offset: 0x0001E320
		public static object CreateInstance(Type type, params object[] args)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			return Activator.CreateInstance(type, args);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00020134 File Offset: 0x0001E334
		public static void SplitFullyQualifiedTypeName(string fullyQualifiedTypeName, out string typeName, out string assemblyName)
		{
			int? assemblyDelimiterIndex = ReflectionUtils.GetAssemblyDelimiterIndex(fullyQualifiedTypeName);
			if (assemblyDelimiterIndex != null)
			{
				typeName = fullyQualifiedTypeName.Substring(0, assemblyDelimiterIndex.Value).Trim();
				assemblyName = fullyQualifiedTypeName.Substring(assemblyDelimiterIndex.Value + 1, fullyQualifiedTypeName.Length - assemblyDelimiterIndex.Value - 1).Trim();
				return;
			}
			typeName = fullyQualifiedTypeName;
			assemblyName = null;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00020194 File Offset: 0x0001E394
		private static int? GetAssemblyDelimiterIndex(string fullyQualifiedTypeName)
		{
			int num = 0;
			for (int i = 0; i < fullyQualifiedTypeName.Length; i++)
			{
				char c = fullyQualifiedTypeName[i];
				char c2 = c;
				if (c2 != ',')
				{
					switch (c2)
					{
					case '[':
						num++;
						break;
					case ']':
						num--;
						break;
					}
				}
				else if (num == 0)
				{
					return new int?(i);
				}
			}
			return null;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00020204 File Offset: 0x0001E404
		public static MemberInfo GetMemberInfoFromType(Type targetType, MemberInfo memberInfo)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			MemberTypes memberType = memberInfo.MemberType;
			if (memberType == MemberTypes.Property)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				Type[] array = (from p in propertyInfo.GetIndexParameters()
					select p.ParameterType).ToArray<Type>();
				return targetType.GetProperty(propertyInfo.Name, bindingFlags, null, propertyInfo.PropertyType, array, null);
			}
			return targetType.GetMember(memberInfo.Name, memberInfo.MemberType, bindingFlags).SingleOrDefault<MemberInfo>();
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00020284 File Offset: 0x0001E484
		public static IEnumerable<FieldInfo> GetFields(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, "targetType");
			List<MemberInfo> list = new List<MemberInfo>(targetType.GetFields(bindingAttr));
			ReflectionUtils.GetChildPrivateFields(list, targetType, bindingAttr);
			return list.Cast<FieldInfo>();
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x000202C0 File Offset: 0x0001E4C0
		private static void GetChildPrivateFields(IList<MemberInfo> initialFields, Type targetType, BindingFlags bindingAttr)
		{
			if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
			{
				BindingFlags bindingFlags = bindingAttr.RemoveFlag(BindingFlags.Public);
				while ((targetType = targetType.BaseType) != null)
				{
					IEnumerable<MemberInfo> enumerable = (from f in targetType.GetFields(bindingFlags)
						where f.IsPrivate
						select f).Cast<MemberInfo>();
					initialFields.AddRange(enumerable);
				}
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00020320 File Offset: 0x0001E520
		public static IEnumerable<PropertyInfo> GetProperties(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, "targetType");
			List<PropertyInfo> list = new List<PropertyInfo>(targetType.GetProperties(bindingAttr));
			ReflectionUtils.GetChildPrivateProperties(list, targetType, bindingAttr);
			for (int i = 0; i < list.Count; i++)
			{
				PropertyInfo propertyInfo = list[i];
				if (propertyInfo.DeclaringType != targetType)
				{
					PropertyInfo propertyInfo2 = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(propertyInfo.DeclaringType, propertyInfo);
					list[i] = propertyInfo2;
				}
			}
			return list;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0002038A File Offset: 0x0001E58A
		public static BindingFlags RemoveFlag(this BindingFlags bindingAttr, BindingFlags flag)
		{
			if ((bindingAttr & flag) != flag)
			{
				return bindingAttr;
			}
			return bindingAttr ^ flag;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x000203B8 File Offset: 0x0001E5B8
		private static void GetChildPrivateProperties(IList<PropertyInfo> initialProperties, Type targetType, BindingFlags bindingAttr)
		{
			if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
			{
				BindingFlags bindingFlags = bindingAttr.RemoveFlag(BindingFlags.Public);
				while ((targetType = targetType.BaseType) != null)
				{
					PropertyInfo[] properties = targetType.GetProperties(bindingFlags);
					for (int i = 0; i < properties.Length; i++)
					{
						PropertyInfo propertyInfo = properties[i];
						PropertyInfo nonPublicProperty = propertyInfo;
						int num = initialProperties.IndexOf((PropertyInfo p) => p.Name == nonPublicProperty.Name);
						if (num == -1)
						{
							initialProperties.Add(nonPublicProperty);
						}
						else
						{
							initialProperties[num] = nonPublicProperty;
						}
					}
				}
			}
		}
	}
}
