using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace System.Runtime.Serialization
{
	// Token: 0x0200002A RID: 42
	internal class DictionaryTypeMap : SerializationMap, ICollectionTypeMap
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00005A18 File Offset: 0x00003C18
		public DictionaryTypeMap(Type type, CollectionDataContractAttribute a, KnownTypeCollection knownTypes)
			: base(type, XmlQualifiedName.Empty, knownTypes)
		{
			this.a = a;
			this.key_type = typeof(object);
			this.value_type = typeof(object);
			Type genericDictionaryInterface = DictionaryTypeMap.GetGenericDictionaryInterface(this.RuntimeType);
			if (genericDictionaryInterface != null)
			{
				InterfaceMapping interfaceMap = this.RuntimeType.GetInterfaceMap(genericDictionaryInterface);
				for (int i = 0; i < interfaceMap.InterfaceMethods.Length; i++)
				{
					if (interfaceMap.InterfaceMethods[i].Name == "Add")
					{
						this.add_method = interfaceMap.TargetMethods[i];
						break;
					}
				}
				Type[] genericArguments = genericDictionaryInterface.GetGenericArguments();
				this.key_type = genericArguments[0];
				this.value_type = genericArguments[1];
				if (this.add_method == null)
				{
					this.add_method = type.GetMethod("Add", genericArguments);
				}
			}
			base.XmlName = this.GetDictionaryQName();
			this.item_qname = this.GetItemQName();
			this.key_qname = this.GetKeyQName();
			this.value_qname = this.GetValueQName();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005B64 File Offset: 0x00003D64
		private static Type GetGenericDictionaryInterface(Type type)
		{
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(IDictionary<, >))
				{
					return type2;
				}
			}
			return null;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005BB4 File Offset: 0x00003DB4
		private string ContractNamespace
		{
			get
			{
				return (this.a == null || string.IsNullOrEmpty(this.a.Namespace)) ? "http://schemas.microsoft.com/2003/10/Serialization/Arrays" : this.a.Namespace;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005BEC File Offset: 0x00003DEC
		public Type KeyType
		{
			get
			{
				return this.key_type;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005BF4 File Offset: 0x00003DF4
		public Type ValueType
		{
			get
			{
				return this.value_type;
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005BFC File Offset: 0x00003DFC
		internal virtual XmlQualifiedName GetDictionaryQName()
		{
			if (this.a != null && !string.IsNullOrEmpty(this.a.Name))
			{
				return new XmlQualifiedName(this.a.Name, this.ContractNamespace);
			}
			return new XmlQualifiedName("ArrayOf" + this.GetItemQName().Name, "http://schemas.microsoft.com/2003/10/Serialization/Arrays");
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005C60 File Offset: 0x00003E60
		internal virtual XmlQualifiedName GetItemQName()
		{
			if (this.a != null && !string.IsNullOrEmpty(this.a.ItemName))
			{
				return new XmlQualifiedName(this.a.ItemName, this.ContractNamespace);
			}
			return new XmlQualifiedName("KeyValueOf" + this.KnownTypes.GetQName(this.key_type).Name + this.KnownTypes.GetQName(this.value_type).Name, "http://schemas.microsoft.com/2003/10/Serialization/Arrays");
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005CE4 File Offset: 0x00003EE4
		internal virtual XmlQualifiedName GetKeyQName()
		{
			if (this.a != null && !string.IsNullOrEmpty(this.a.KeyName))
			{
				return new XmlQualifiedName(this.a.KeyName, this.ContractNamespace);
			}
			return DictionaryTypeMap.kvpair_key_qname;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005D30 File Offset: 0x00003F30
		internal virtual XmlQualifiedName GetValueQName()
		{
			if (this.a != null && !string.IsNullOrEmpty(this.a.ValueName))
			{
				return new XmlQualifiedName(this.a.ValueName, this.ContractNamespace);
			}
			return DictionaryTypeMap.kvpair_value_qname;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005D7C File Offset: 0x00003F7C
		internal virtual string CurrentNamespace
		{
			get
			{
				string text = this.item_qname.Namespace;
				if (text == "http://schemas.microsoft.com/2003/10/Serialization/")
				{
					text = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
				}
				return text;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005DAC File Offset: 0x00003FAC
		public override void SerializeNonReference(object graph, XmlFormatterSerializer serializer)
		{
			if (this.add_method != null)
			{
				if (this.pair_type == null)
				{
					this.pair_type = typeof(KeyValuePair<, >).MakeGenericType(this.add_method.DeclaringType.GetGenericArguments());
					this.pair_key_property = this.pair_type.GetProperty("Key");
					this.pair_value_property = this.pair_type.GetProperty("Value");
				}
				foreach (object obj in ((IEnumerable)graph))
				{
					serializer.WriteStartElement(this.item_qname.Name, this.item_qname.Namespace, this.CurrentNamespace);
					serializer.WriteStartElement(this.key_qname.Name, this.key_qname.Namespace, this.CurrentNamespace);
					serializer.Serialize(this.pair_key_property.PropertyType, this.pair_key_property.GetValue(obj, null));
					serializer.WriteEndElement();
					serializer.WriteStartElement(this.value_qname.Name, this.value_qname.Namespace, this.CurrentNamespace);
					serializer.Serialize(this.pair_value_property.PropertyType, this.pair_value_property.GetValue(obj, null));
					serializer.WriteEndElement();
					serializer.WriteEndElement();
				}
			}
			else
			{
				foreach (object obj2 in ((IEnumerable)graph))
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
					serializer.WriteStartElement(this.item_qname.Name, this.item_qname.Namespace, this.CurrentNamespace);
					serializer.WriteStartElement(this.key_qname.Name, this.key_qname.Namespace, this.CurrentNamespace);
					serializer.Serialize(this.key_type, dictionaryEntry.Key);
					serializer.WriteEndElement();
					serializer.WriteStartElement(this.value_qname.Name, this.value_qname.Namespace, this.CurrentNamespace);
					serializer.Serialize(this.value_type, dictionaryEntry.Value);
					serializer.WriteEndElement();
					serializer.WriteEndElement();
				}
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000602C File Offset: 0x0000422C
		private object CreateInstance()
		{
			if (!this.RuntimeType.IsInterface)
			{
				return Activator.CreateInstance(this.RuntimeType, true);
			}
			if (this.RuntimeType.IsGenericType && Array.IndexOf<Type>(this.RuntimeType.GetGenericTypeDefinition().GetInterfaces(), typeof(IDictionary<, >)) >= 0)
			{
				Type[] genericArguments = this.RuntimeType.GetGenericArguments();
				return Activator.CreateInstance(typeof(Dictionary<, >).MakeGenericType(new Type[]
				{
					genericArguments[0],
					genericArguments[1]
				}));
			}
			return new Hashtable();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000060C4 File Offset: 0x000042C4
		public override object DeserializeEmptyContent(XmlReader reader, XmlFormatterDeserializer deserializer)
		{
			return this.DeserializeContent(reader, deserializer);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000060D0 File Offset: 0x000042D0
		public override object DeserializeContent(XmlReader reader, XmlFormatterDeserializer deserializer)
		{
			object obj = this.CreateInstance();
			int num = ((reader.NodeType != XmlNodeType.None) ? (reader.Depth - 1) : reader.Depth);
			while (reader.NodeType == XmlNodeType.Element && reader.Depth > num)
			{
				if (reader.IsEmptyElement)
				{
					throw new XmlException(string.Format("Unexpected empty element for dictionary entry: name {0}", reader.Name));
				}
				reader.ReadStartElement();
				reader.MoveToContent();
				object obj2 = deserializer.Deserialize(this.key_type, reader);
				reader.MoveToContent();
				object obj3 = deserializer.Deserialize(this.value_type, reader);
				reader.ReadEndElement();
				if (obj is IDictionary)
				{
					((IDictionary)obj).Add(obj2, obj3);
				}
				else
				{
					if (this.add_method == null)
					{
						throw new NotImplementedException(string.Format("Type {0} is not supported", this.RuntimeType));
					}
					this.add_method.Invoke(obj, new object[] { obj2, obj3 });
				}
			}
			return obj;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000061D8 File Offset: 0x000043D8
		public override List<DataMemberInfo> GetMembers()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000061E0 File Offset: 0x000043E0
		public override XmlSchemaType GetSchemaType(XmlSchemaSet schemas, Dictionary<XmlQualifiedName, XmlSchemaType> generated_schema_types)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400007C RID: 124
		private Type key_type;

		// Token: 0x0400007D RID: 125
		private Type value_type;

		// Token: 0x0400007E RID: 126
		private XmlQualifiedName dict_qname;

		// Token: 0x0400007F RID: 127
		private XmlQualifiedName item_qname;

		// Token: 0x04000080 RID: 128
		private XmlQualifiedName key_qname;

		// Token: 0x04000081 RID: 129
		private XmlQualifiedName value_qname;

		// Token: 0x04000082 RID: 130
		private MethodInfo add_method;

		// Token: 0x04000083 RID: 131
		private CollectionDataContractAttribute a;

		// Token: 0x04000084 RID: 132
		private static readonly XmlQualifiedName kvpair_key_qname = new XmlQualifiedName("Key", "http://schemas.microsoft.com/2003/10/Serialization/Arrays");

		// Token: 0x04000085 RID: 133
		private static readonly XmlQualifiedName kvpair_value_qname = new XmlQualifiedName("Value", "http://schemas.microsoft.com/2003/10/Serialization/Arrays");

		// Token: 0x04000086 RID: 134
		private Type pair_type;

		// Token: 0x04000087 RID: 135
		private PropertyInfo pair_key_property;

		// Token: 0x04000088 RID: 136
		private PropertyInfo pair_value_property;
	}
}
