using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
	// Token: 0x02000021 RID: 33
	internal sealed class KnownTypeCollection : Collection<Type>
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00002C28 File Offset: 0x00000E28
		static KnownTypeCollection()
		{
			string text = "http://schemas.microsoft.com/2003/10/Serialization/";
			KnownTypeCollection.any_type = new XmlQualifiedName("anyType", text);
			KnownTypeCollection.any_uri_type = new XmlQualifiedName("anyURI", text);
			KnownTypeCollection.bool_type = new XmlQualifiedName("boolean", text);
			KnownTypeCollection.base64_type = new XmlQualifiedName("base64Binary", text);
			KnownTypeCollection.date_type = new XmlQualifiedName("dateTime", text);
			KnownTypeCollection.duration_type = new XmlQualifiedName("duration", text);
			KnownTypeCollection.qname_type = new XmlQualifiedName("QName", text);
			KnownTypeCollection.decimal_type = new XmlQualifiedName("decimal", text);
			KnownTypeCollection.double_type = new XmlQualifiedName("double", text);
			KnownTypeCollection.float_type = new XmlQualifiedName("float", text);
			KnownTypeCollection.byte_type = new XmlQualifiedName("byte", text);
			KnownTypeCollection.short_type = new XmlQualifiedName("short", text);
			KnownTypeCollection.int_type = new XmlQualifiedName("int", text);
			KnownTypeCollection.long_type = new XmlQualifiedName("long", text);
			KnownTypeCollection.ubyte_type = new XmlQualifiedName("unsignedByte", text);
			KnownTypeCollection.ushort_type = new XmlQualifiedName("unsignedShort", text);
			KnownTypeCollection.uint_type = new XmlQualifiedName("unsignedInt", text);
			KnownTypeCollection.ulong_type = new XmlQualifiedName("unsignedLong", text);
			KnownTypeCollection.string_type = new XmlQualifiedName("string", text);
			KnownTypeCollection.guid_type = new XmlQualifiedName("guid", text);
			KnownTypeCollection.char_type = new XmlQualifiedName("char", text);
			KnownTypeCollection.dbnull_type = new XmlQualifiedName("DBNull", "http://schemas.microsoft.com/2003/10/Serialization/System");
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002DA0 File Offset: 0x00000FA0
		internal XmlQualifiedName GetXmlName(Type type)
		{
			SerializationMap serializationMap = this.FindUserMap(type);
			if (serializationMap != null)
			{
				return serializationMap.XmlName;
			}
			return KnownTypeCollection.GetPredefinedTypeName(type);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002DC8 File Offset: 0x00000FC8
		internal static XmlQualifiedName GetPredefinedTypeName(Type type)
		{
			XmlQualifiedName primitiveTypeName = KnownTypeCollection.GetPrimitiveTypeName(type);
			if (primitiveTypeName != XmlQualifiedName.Empty)
			{
				return primitiveTypeName;
			}
			if (type == typeof(DBNull))
			{
				return KnownTypeCollection.dbnull_type;
			}
			return XmlQualifiedName.Empty;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002E0C File Offset: 0x0000100C
		internal static XmlQualifiedName GetPrimitiveTypeName(Type type)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				return KnownTypeCollection.GetPrimitiveTypeName(type.GetGenericArguments()[0]);
			}
			if (type.IsEnum)
			{
				return XmlQualifiedName.Empty;
			}
			switch (Type.GetTypeCode(type))
			{
			default:
				if (type == typeof(object))
				{
					return KnownTypeCollection.any_type;
				}
				if (type == typeof(Guid))
				{
					return KnownTypeCollection.guid_type;
				}
				if (type == typeof(TimeSpan))
				{
					return KnownTypeCollection.duration_type;
				}
				if (type == typeof(byte[]))
				{
					return KnownTypeCollection.base64_type;
				}
				if (type == typeof(Uri))
				{
					return KnownTypeCollection.any_uri_type;
				}
				return XmlQualifiedName.Empty;
			case TypeCode.Boolean:
				return KnownTypeCollection.bool_type;
			case TypeCode.Char:
				return KnownTypeCollection.char_type;
			case TypeCode.SByte:
				return KnownTypeCollection.byte_type;
			case TypeCode.Byte:
				return KnownTypeCollection.ubyte_type;
			case TypeCode.Int16:
				return KnownTypeCollection.short_type;
			case TypeCode.UInt16:
				return KnownTypeCollection.ushort_type;
			case TypeCode.Int32:
				return KnownTypeCollection.int_type;
			case TypeCode.UInt32:
				return KnownTypeCollection.uint_type;
			case TypeCode.Int64:
				return KnownTypeCollection.long_type;
			case TypeCode.UInt64:
				return KnownTypeCollection.ulong_type;
			case TypeCode.Single:
				return KnownTypeCollection.float_type;
			case TypeCode.Double:
				return KnownTypeCollection.double_type;
			case TypeCode.Decimal:
				return KnownTypeCollection.decimal_type;
			case TypeCode.DateTime:
				return KnownTypeCollection.date_type;
			case TypeCode.String:
				return KnownTypeCollection.string_type;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002F84 File Offset: 0x00001184
		internal static string PredefinedTypeObjectToString(object obj)
		{
			Type type = obj.GetType();
			switch (Type.GetTypeCode(type))
			{
			default:
				if (type == typeof(object))
				{
					return string.Empty;
				}
				if (type == typeof(Guid))
				{
					return XmlConvert.ToString((Guid)obj);
				}
				if (type == typeof(TimeSpan))
				{
					return XmlConvert.ToString((TimeSpan)obj);
				}
				if (type == typeof(byte[]))
				{
					return Convert.ToBase64String((byte[])obj);
				}
				if (type == typeof(Uri))
				{
					return ((Uri)obj).ToString();
				}
				throw new Exception("Internal error: missing predefined type serialization for type " + type.FullName);
			case TypeCode.DBNull:
				return string.Empty;
			case TypeCode.Boolean:
				return XmlConvert.ToString((bool)obj);
			case TypeCode.Char:
				return XmlConvert.ToString((uint)((char)obj));
			case TypeCode.SByte:
				return XmlConvert.ToString((sbyte)obj);
			case TypeCode.Byte:
				return XmlConvert.ToString((int)((byte)obj));
			case TypeCode.Int16:
				return XmlConvert.ToString((short)obj);
			case TypeCode.UInt16:
				return XmlConvert.ToString((int)((ushort)obj));
			case TypeCode.Int32:
				return XmlConvert.ToString((int)obj);
			case TypeCode.UInt32:
				return XmlConvert.ToString((uint)obj);
			case TypeCode.Int64:
				return XmlConvert.ToString((long)obj);
			case TypeCode.UInt64:
				return XmlConvert.ToString((ulong)obj);
			case TypeCode.Single:
				return XmlConvert.ToString((float)obj);
			case TypeCode.Double:
				return XmlConvert.ToString((double)obj);
			case TypeCode.Decimal:
				return XmlConvert.ToString((decimal)obj);
			case TypeCode.DateTime:
				return XmlConvert.ToString((DateTime)obj, XmlDateTimeSerializationMode.RoundtripKind);
			case TypeCode.String:
				return (string)obj;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003148 File Offset: 0x00001348
		internal static Type GetPrimitiveTypeFromName(string name)
		{
			switch (name)
			{
			case "anyURI":
				return typeof(Uri);
			case "boolean":
				return typeof(bool);
			case "base64Binary":
				return typeof(byte[]);
			case "dateTime":
				return typeof(DateTime);
			case "duration":
				return typeof(TimeSpan);
			case "QName":
				return typeof(XmlQualifiedName);
			case "decimal":
				return typeof(decimal);
			case "double":
				return typeof(double);
			case "float":
				return typeof(float);
			case "byte":
				return typeof(sbyte);
			case "short":
				return typeof(short);
			case "int":
				return typeof(int);
			case "long":
				return typeof(long);
			case "unsignedByte":
				return typeof(byte);
			case "unsignedShort":
				return typeof(ushort);
			case "unsignedInt":
				return typeof(uint);
			case "unsignedLong":
				return typeof(ulong);
			case "string":
				return typeof(string);
			case "anyType":
				return typeof(object);
			case "guid":
				return typeof(Guid);
			case "char":
				return typeof(char);
			}
			return null;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000033D8 File Offset: 0x000015D8
		internal static object PredefinedTypeStringToObject(string s, string name, XmlReader reader)
		{
			switch (name)
			{
			case "anyURI":
				return new Uri(s, UriKind.RelativeOrAbsolute);
			case "boolean":
				return XmlConvert.ToBoolean(s);
			case "base64Binary":
				return Convert.FromBase64String(s);
			case "dateTime":
				return XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.RoundtripKind);
			case "duration":
				return XmlConvert.ToTimeSpan(s);
			case "QName":
			{
				int num2 = s.IndexOf(':');
				string text = ((num2 >= 0) ? s.Substring(num2 + 1) : s);
				return (num2 >= 0) ? new XmlQualifiedName(text, reader.LookupNamespace(s.Substring(0, num2))) : new XmlQualifiedName(text);
			}
			case "decimal":
				return XmlConvert.ToDecimal(s);
			case "double":
				return XmlConvert.ToDouble(s);
			case "float":
				return XmlConvert.ToSingle(s);
			case "byte":
				return XmlConvert.ToSByte(s);
			case "short":
				return XmlConvert.ToInt16(s);
			case "int":
				return XmlConvert.ToInt32(s);
			case "long":
				return XmlConvert.ToInt64(s);
			case "unsignedByte":
				return XmlConvert.ToByte(s);
			case "unsignedShort":
				return XmlConvert.ToUInt16(s);
			case "unsignedInt":
				return XmlConvert.ToUInt32(s);
			case "unsignedLong":
				return XmlConvert.ToUInt64(s);
			case "string":
				return s;
			case "guid":
				return XmlConvert.ToGuid(s);
			case "anyType":
				return s;
			case "char":
				return (char)XmlConvert.ToUInt32(s);
			}
			throw new Exception("Unanticipated primitive type: " + name);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000036AC File Offset: 0x000018AC
		protected override void ClearItems()
		{
			base.Clear();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000036B4 File Offset: 0x000018B4
		protected override void InsertItem(int index, Type type)
		{
			if (this.TryRegister(type))
			{
				base.InsertItem(index, type);
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000036CC File Offset: 0x000018CC
		protected override void RemoveItem(int index)
		{
			Type type = base[index];
			List<SerializationMap> list = new List<SerializationMap>();
			foreach (SerializationMap serializationMap in this.contracts)
			{
				if (serializationMap.RuntimeType == type)
				{
					list.Add(serializationMap);
				}
			}
			foreach (SerializationMap serializationMap2 in list)
			{
				this.contracts.Remove(serializationMap2);
				base.RemoveItem(index);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000037B0 File Offset: 0x000019B0
		protected override void SetItem(int index, Type type)
		{
			if (index == this.Count)
			{
				this.InsertItem(index, type);
			}
			else
			{
				this.RemoveItem(index);
				if (this.TryRegister(type))
				{
					base.InsertItem(index - 1, type);
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000037F4 File Offset: 0x000019F4
		internal SerializationMap FindUserMap(XmlQualifiedName qname)
		{
			for (int i = 0; i < this.contracts.Count; i++)
			{
				if (qname == this.contracts[i].XmlName)
				{
					return this.contracts[i];
				}
			}
			return null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003848 File Offset: 0x00001A48
		internal Type GetSerializedType(Type type)
		{
			if (KnownTypeCollection.GetCollectionElementType(type) == null)
			{
				return type;
			}
			XmlQualifiedName qname = this.GetQName(type);
			SerializationMap serializationMap = this.FindUserMap(qname);
			if (serializationMap != null)
			{
				return serializationMap.RuntimeType;
			}
			return type;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003884 File Offset: 0x00001A84
		internal SerializationMap FindUserMap(Type type)
		{
			for (int i = 0; i < this.contracts.Count; i++)
			{
				if (type == this.contracts[i].RuntimeType)
				{
					return this.contracts[i];
				}
			}
			return null;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000038D4 File Offset: 0x00001AD4
		internal XmlQualifiedName GetQName(Type type)
		{
			if (this.IsPrimitiveNotEnum(type))
			{
				return KnownTypeCollection.GetPrimitiveTypeName(type);
			}
			SerializationMap serializationMap = this.FindUserMap(type);
			if (serializationMap != null)
			{
				return serializationMap.XmlName;
			}
			if (type.IsEnum)
			{
				return this.GetEnumQName(type);
			}
			XmlQualifiedName xmlQualifiedName = this.GetContractQName(type);
			if (xmlQualifiedName != null)
			{
				return xmlQualifiedName;
			}
			if (type.GetInterface("System.Xml.Serialization.IXmlSerializable") != null)
			{
				return this.GetSerializableQName(type);
			}
			xmlQualifiedName = this.GetCollectionContractQName(type);
			if (xmlQualifiedName != null)
			{
				return xmlQualifiedName;
			}
			Type collectionElementType = KnownTypeCollection.GetCollectionElementType(type);
			if (collectionElementType != null)
			{
				return this.GetCollectionQName(collectionElementType);
			}
			if (this.GetAttribute<SerializableAttribute>(type) != null)
			{
				return this.GetSerializableQName(type);
			}
			return XmlQualifiedName.Empty;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003990 File Offset: 0x00001B90
		private XmlQualifiedName GetContractQName(Type type)
		{
			DataContractAttribute attribute = this.GetAttribute<DataContractAttribute>(type);
			return (attribute != null) ? KnownTypeCollection.GetContractQName(type, attribute.Name, attribute.Namespace) : null;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000039C4 File Offset: 0x00001BC4
		private XmlQualifiedName GetCollectionContractQName(Type type)
		{
			CollectionDataContractAttribute attribute = this.GetAttribute<CollectionDataContractAttribute>(type);
			return (attribute != null) ? KnownTypeCollection.GetContractQName(type, attribute.Name, attribute.Namespace) : null;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000039F8 File Offset: 0x00001BF8
		internal static XmlQualifiedName GetContractQName(Type type, string name, string ns)
		{
			if (name == null)
			{
				name = ((type.Namespace != null && type.Namespace.Length != 0) ? type.FullName.Substring(type.Namespace.Length + 1).Replace('+', '.') : type.Name);
				if (type.IsGenericType)
				{
					name = name.Substring(0, name.IndexOf('`')) + "Of";
					foreach (Type type2 in type.GetGenericArguments())
					{
						name += type2.Name;
					}
				}
			}
			if (ns == null)
			{
				ns = "http://schemas.datacontract.org/2004/07/" + type.Namespace;
			}
			return new XmlQualifiedName(name, ns);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003AC8 File Offset: 0x00001CC8
		private XmlQualifiedName GetEnumQName(Type type)
		{
			string text = null;
			string text2 = null;
			if (!type.IsEnum)
			{
				return null;
			}
			DataContractAttribute attribute = this.GetAttribute<DataContractAttribute>(type);
			if (attribute != null)
			{
				text2 = attribute.Namespace;
				text = attribute.Name;
			}
			if (text2 == null)
			{
				text2 = "http://schemas.datacontract.org/2004/07/" + type.Namespace;
			}
			if (text == null)
			{
				text = ((type.Namespace != null) ? type.FullName.Substring(type.Namespace.Length + 1).Replace('+', '.') : type.Name);
			}
			return new XmlQualifiedName(text, text2);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003B60 File Offset: 0x00001D60
		private XmlQualifiedName GetCollectionQName(Type element)
		{
			XmlQualifiedName qname = this.GetQName(element);
			string text = qname.Namespace;
			if (qname.Namespace == "http://schemas.microsoft.com/2003/10/Serialization/")
			{
				text = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
			}
			return new XmlQualifiedName("ArrayOf" + XmlConvert.EncodeLocalName(qname.Name), text);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003BB4 File Offset: 0x00001DB4
		private XmlQualifiedName GetSerializableQName(Type type)
		{
			string text = type.Name;
			if (type.IsGenericType)
			{
				text = text.Substring(0, text.IndexOf('`')) + "Of";
				foreach (Type type2 in type.GetGenericArguments())
				{
					text += this.GetQName(type2).Name;
				}
			}
			string text2 = "http://schemas.datacontract.org/2004/07/" + type.Namespace;
			XmlRootAttribute attribute = this.GetAttribute<XmlRootAttribute>(type);
			if (attribute != null)
			{
				text = attribute.ElementName;
				text2 = attribute.Namespace;
			}
			return new XmlQualifiedName(XmlConvert.EncodeLocalName(text), text2);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003C60 File Offset: 0x00001E60
		internal bool IsPrimitiveNotEnum(Type type)
		{
			return !type.IsEnum && (Type.GetTypeCode(type) != TypeCode.Object || (type == typeof(Guid) || type == typeof(object) || type == typeof(TimeSpan) || type == typeof(byte[]) || type == typeof(Uri)) || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && this.IsPrimitiveNotEnum(type.GetGenericArguments()[0])));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003D0C File Offset: 0x00001F0C
		internal bool TryRegister(Type type)
		{
			if (this.IsPrimitiveNotEnum(type))
			{
				return false;
			}
			if (this.FindUserMap(type) != null)
			{
				return false;
			}
			if (this.RegisterEnum(type) != null)
			{
				return true;
			}
			if (this.RegisterContract(type) != null)
			{
				return true;
			}
			if (this.RegisterIXmlSerializable(type) != null)
			{
				return true;
			}
			if (this.RegisterDictionary(type) != null)
			{
				return true;
			}
			if (this.RegisterCollectionContract(type) != null)
			{
				return true;
			}
			if (this.RegisterCollection(type) != null)
			{
				return true;
			}
			if (this.GetAttribute<SerializableAttribute>(type) != null)
			{
				this.RegisterSerializable(type);
				return true;
			}
			this.RegisterDefaultTypeMap(type);
			return true;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003DA8 File Offset: 0x00001FA8
		internal static Type GetCollectionElementType(Type type)
		{
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			Type[] interfaces = type.GetInterfaces();
			foreach (Type type2 in interfaces)
			{
				if (type2.IsGenericType && type2.GetGenericTypeDefinition().Equals(typeof(ICollection<>)))
				{
					return type2.GetGenericArguments()[0];
				}
			}
			foreach (Type type3 in interfaces)
			{
				if (type3 == typeof(IList))
				{
					return typeof(object);
				}
			}
			return null;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003E58 File Offset: 0x00002058
		internal T GetAttribute<T>(MemberInfo mi) where T : Attribute
		{
			object[] customAttributes = mi.GetCustomAttributes(typeof(T), false);
			return (customAttributes.Length != 0) ? ((T)((object)customAttributes[0])) : ((T)((object)null));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003E94 File Offset: 0x00002094
		private CollectionContractTypeMap RegisterCollectionContract(Type type)
		{
			CollectionDataContractAttribute attribute = this.GetAttribute<CollectionDataContractAttribute>(type);
			if (attribute == null)
			{
				return null;
			}
			Type collectionElementType = KnownTypeCollection.GetCollectionElementType(type);
			if (collectionElementType == null)
			{
				throw new InvalidOperationException(string.Format("Type '{0}' is marked as collection contract, but it is not a collection", type));
			}
			this.TryRegister(collectionElementType);
			XmlQualifiedName collectionContractQName = this.GetCollectionContractQName(type);
			this.CheckStandardQName(collectionContractQName);
			if (this.FindUserMap(collectionContractQName) != null)
			{
				throw new InvalidOperationException(string.Format("Failed to add type {0} to known type collection. There already is a registered type for XML name {1}", type, collectionContractQName));
			}
			CollectionContractTypeMap collectionContractTypeMap = new CollectionContractTypeMap(type, attribute, collectionElementType, collectionContractQName, this);
			this.contracts.Add(collectionContractTypeMap);
			return collectionContractTypeMap;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003F1C File Offset: 0x0000211C
		private CollectionTypeMap RegisterCollection(Type type)
		{
			Type collectionElementType = KnownTypeCollection.GetCollectionElementType(type);
			if (collectionElementType == null)
			{
				return null;
			}
			this.TryRegister(collectionElementType);
			XmlQualifiedName collectionQName = this.GetCollectionQName(collectionElementType);
			SerializationMap serializationMap = this.FindUserMap(collectionQName);
			if (serializationMap == null)
			{
				CollectionTypeMap collectionTypeMap = new CollectionTypeMap(type, collectionElementType, collectionQName, this);
				this.contracts.Add(collectionTypeMap);
				return collectionTypeMap;
			}
			CollectionTypeMap collectionTypeMap2 = serializationMap as CollectionTypeMap;
			if (collectionTypeMap2 == null || collectionTypeMap2.RuntimeType != type)
			{
				throw new InvalidOperationException(string.Format("Failed to add type {0} to known type collection. There already is a registered type for XML name {1}", type, collectionQName));
			}
			return collectionTypeMap2;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003FA0 File Offset: 0x000021A0
		private static bool TypeImplementsIDictionary(Type type)
		{
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2 == typeof(IDictionary) || (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(IDictionary<, >)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004000 File Offset: 0x00002200
		private DictionaryTypeMap RegisterDictionary(Type type)
		{
			if (!KnownTypeCollection.TypeImplementsIDictionary(type))
			{
				return null;
			}
			CollectionDataContractAttribute attribute = this.GetAttribute<CollectionDataContractAttribute>(type);
			DictionaryTypeMap dictionaryTypeMap = new DictionaryTypeMap(type, attribute, this);
			if (this.FindUserMap(dictionaryTypeMap.XmlName) != null)
			{
				throw new InvalidOperationException(string.Format("Failed to add type {0} to known type collection. There already is a registered type for XML name {1}", type, dictionaryTypeMap.XmlName));
			}
			this.contracts.Add(dictionaryTypeMap);
			this.TryRegister(dictionaryTypeMap.KeyType);
			this.TryRegister(dictionaryTypeMap.ValueType);
			return dictionaryTypeMap;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000407C File Offset: 0x0000227C
		private SerializationMap RegisterSerializable(Type type)
		{
			XmlQualifiedName serializableQName = this.GetSerializableQName(type);
			if (this.FindUserMap(serializableQName) != null)
			{
				throw new InvalidOperationException(string.Format("There is already a registered type for XML name {0}", serializableQName));
			}
			SharedTypeMap sharedTypeMap = new SharedTypeMap(type, serializableQName, this);
			this.contracts.Add(sharedTypeMap);
			return sharedTypeMap;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000040C4 File Offset: 0x000022C4
		private SerializationMap RegisterIXmlSerializable(Type type)
		{
			if (type.GetInterface("System.Xml.Serialization.IXmlSerializable") == null)
			{
				return null;
			}
			XmlQualifiedName serializableQName = this.GetSerializableQName(type);
			if (this.FindUserMap(serializableQName) != null)
			{
				throw new InvalidOperationException(string.Format("There is already a registered type for XML name {0}", serializableQName));
			}
			XmlSerializableMap xmlSerializableMap = new XmlSerializableMap(type, serializableQName, this);
			this.contracts.Add(xmlSerializableMap);
			return xmlSerializableMap;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004120 File Offset: 0x00002320
		private void CheckStandardQName(XmlQualifiedName qname)
		{
			string @namespace = qname.Namespace;
			if (@namespace != null)
			{
				if (KnownTypeCollection.<>f__switch$map2 == null)
				{
					KnownTypeCollection.<>f__switch$map2 = new Dictionary<string, int>(4)
					{
						{ "http://www.w3.org/2001/XMLSchema", 0 },
						{ "http://www.w3.org/2001/XMLSchema-instance", 0 },
						{ "http://schemas.microsoft.com/2003/10/Serialization/", 0 },
						{ "http://schemas.microsoft.com/2003/10/Serialization/Arrays", 0 }
					};
				}
				int num;
				if (KnownTypeCollection.<>f__switch$map2.TryGetValue(@namespace, out num))
				{
					if (num == 0)
					{
						throw new InvalidOperationException(string.Format("Namespace {0} is reserved and cannot be used for user serialization", qname.Namespace));
					}
				}
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000041B4 File Offset: 0x000023B4
		private SharedContractMap RegisterContract(Type type)
		{
			XmlQualifiedName contractQName = this.GetContractQName(type);
			if (contractQName == null)
			{
				return null;
			}
			this.CheckStandardQName(contractQName);
			if (this.FindUserMap(contractQName) != null)
			{
				throw new InvalidOperationException(string.Format("There is already a registered type for XML name {0}", contractQName));
			}
			SharedContractMap sharedContractMap = new SharedContractMap(type, contractQName, this);
			this.contracts.Add(sharedContractMap);
			sharedContractMap.Initialize();
			foreach (KnownTypeAttribute knownTypeAttribute in type.GetCustomAttributes(typeof(KnownTypeAttribute), true))
			{
				this.TryRegister(knownTypeAttribute.Type);
			}
			return sharedContractMap;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004258 File Offset: 0x00002458
		private DefaultTypeMap RegisterDefaultTypeMap(Type type)
		{
			DefaultTypeMap defaultTypeMap = new DefaultTypeMap(type, this);
			this.contracts.Add(defaultTypeMap);
			return defaultTypeMap;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000427C File Offset: 0x0000247C
		private EnumMap RegisterEnum(Type type)
		{
			XmlQualifiedName enumQName = this.GetEnumQName(type);
			if (enumQName == null)
			{
				return null;
			}
			if (this.FindUserMap(enumQName) != null)
			{
				throw new InvalidOperationException(string.Format("There is already a registered type for XML name {0}", enumQName));
			}
			EnumMap enumMap = new EnumMap(type, enumQName, this);
			this.contracts.Add(enumMap);
			return enumMap;
		}

		// Token: 0x0400004A RID: 74
		internal const string MSSimpleNamespace = "http://schemas.microsoft.com/2003/10/Serialization/";

		// Token: 0x0400004B RID: 75
		internal const string MSArraysNamespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";

		// Token: 0x0400004C RID: 76
		internal const string DefaultClrNamespaceBase = "http://schemas.datacontract.org/2004/07/";

		// Token: 0x0400004D RID: 77
		private static XmlQualifiedName any_type;

		// Token: 0x0400004E RID: 78
		private static XmlQualifiedName bool_type;

		// Token: 0x0400004F RID: 79
		private static XmlQualifiedName byte_type;

		// Token: 0x04000050 RID: 80
		private static XmlQualifiedName date_type;

		// Token: 0x04000051 RID: 81
		private static XmlQualifiedName decimal_type;

		// Token: 0x04000052 RID: 82
		private static XmlQualifiedName double_type;

		// Token: 0x04000053 RID: 83
		private static XmlQualifiedName float_type;

		// Token: 0x04000054 RID: 84
		private static XmlQualifiedName string_type;

		// Token: 0x04000055 RID: 85
		private static XmlQualifiedName short_type;

		// Token: 0x04000056 RID: 86
		private static XmlQualifiedName int_type;

		// Token: 0x04000057 RID: 87
		private static XmlQualifiedName long_type;

		// Token: 0x04000058 RID: 88
		private static XmlQualifiedName ubyte_type;

		// Token: 0x04000059 RID: 89
		private static XmlQualifiedName ushort_type;

		// Token: 0x0400005A RID: 90
		private static XmlQualifiedName uint_type;

		// Token: 0x0400005B RID: 91
		private static XmlQualifiedName ulong_type;

		// Token: 0x0400005C RID: 92
		private static XmlQualifiedName any_uri_type;

		// Token: 0x0400005D RID: 93
		private static XmlQualifiedName base64_type;

		// Token: 0x0400005E RID: 94
		private static XmlQualifiedName duration_type;

		// Token: 0x0400005F RID: 95
		private static XmlQualifiedName qname_type;

		// Token: 0x04000060 RID: 96
		private static XmlQualifiedName char_type;

		// Token: 0x04000061 RID: 97
		private static XmlQualifiedName guid_type;

		// Token: 0x04000062 RID: 98
		private static XmlQualifiedName dbnull_type;

		// Token: 0x04000063 RID: 99
		private List<SerializationMap> contracts = new List<SerializationMap>();
	}
}
