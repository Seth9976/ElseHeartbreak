using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace System.Runtime.Serialization
{
	// Token: 0x02000029 RID: 41
	internal class CollectionTypeMap : SerializationMap, ICollectionTypeMap
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00005554 File Offset: 0x00003754
		public CollectionTypeMap(Type type, Type elementType, XmlQualifiedName qname, KnownTypeCollection knownTypes)
			: base(type, qname, knownTypes)
		{
			this.element_type = elementType;
			this.element_qname = this.KnownTypes.GetQName(this.element_type);
			Type genericCollectionInterface = CollectionTypeMap.GetGenericCollectionInterface(this.RuntimeType);
			if (genericCollectionInterface != null)
			{
				if (this.RuntimeType.IsInterface)
				{
					this.add_method = this.RuntimeType.GetMethod("Add", genericCollectionInterface.GetGenericArguments());
				}
				else
				{
					InterfaceMapping interfaceMap = this.RuntimeType.GetInterfaceMap(genericCollectionInterface);
					for (int i = 0; i < interfaceMap.InterfaceMethods.Length; i++)
					{
						if (interfaceMap.InterfaceMethods[i].Name == "Add")
						{
							this.add_method = interfaceMap.TargetMethods[i];
							break;
						}
					}
					if (this.add_method == null)
					{
						this.add_method = type.GetMethod("Add", genericCollectionInterface.GetGenericArguments());
					}
				}
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00005648 File Offset: 0x00003848
		private static Type GetGenericCollectionInterface(Type type)
		{
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(ICollection<>))
				{
					return type2;
				}
			}
			return null;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00005698 File Offset: 0x00003898
		public override bool OutputXsiType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000569C File Offset: 0x0000389C
		internal virtual string CurrentNamespace
		{
			get
			{
				string text = this.element_qname.Namespace;
				if (text == "http://schemas.microsoft.com/2003/10/Serialization/")
				{
					text = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
				}
				return text;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000056CC File Offset: 0x000038CC
		public override void SerializeNonReference(object graph, XmlFormatterSerializer serializer)
		{
			foreach (object obj in ((IEnumerable)graph))
			{
				serializer.WriteStartElement(this.element_qname.Name, base.XmlName.Namespace, this.CurrentNamespace);
				serializer.Serialize(this.element_type, obj);
				serializer.WriteEndElement();
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005764 File Offset: 0x00003964
		private object CreateInstance()
		{
			if (this.RuntimeType.IsArray)
			{
				return new ArrayList();
			}
			if (!this.RuntimeType.IsInterface)
			{
				return Activator.CreateInstance(this.RuntimeType, true);
			}
			Type genericCollectionInterface = CollectionTypeMap.GetGenericCollectionInterface(this.RuntimeType);
			if (genericCollectionInterface != null)
			{
				return Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { this.RuntimeType.GetGenericArguments()[0] }));
			}
			return new ArrayList();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000057E8 File Offset: 0x000039E8
		public override object DeserializeEmptyContent(XmlReader reader, XmlFormatterDeserializer deserializer)
		{
			return this.CreateInstance();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000057F0 File Offset: 0x000039F0
		public override object DeserializeContent(XmlReader reader, XmlFormatterDeserializer deserializer)
		{
			object obj = this.CreateInstance();
			int num = ((reader.NodeType != XmlNodeType.None) ? (reader.Depth - 1) : reader.Depth);
			while (reader.NodeType == XmlNodeType.Element && reader.Depth > num)
			{
				object obj2 = deserializer.Deserialize(this.element_type, reader);
				if (obj is IList)
				{
					((IList)obj).Add(obj2);
				}
				else
				{
					if (this.add_method == null)
					{
						throw new NotImplementedException(string.Format("Type {0} is not supported", this.RuntimeType));
					}
					this.add_method.Invoke(obj, new object[] { obj2 });
				}
				reader.MoveToContent();
			}
			if (this.RuntimeType.IsArray)
			{
				return ((ArrayList)obj).ToArray(this.element_type);
			}
			return obj;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000058D4 File Offset: 0x00003AD4
		public override List<DataMemberInfo> GetMembers()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000058DC File Offset: 0x00003ADC
		public override XmlSchemaType GetSchemaType(XmlSchemaSet schemas, Dictionary<XmlQualifiedName, XmlSchemaType> generated_schema_types)
		{
			if (generated_schema_types.ContainsKey(base.XmlName))
			{
				return null;
			}
			if (generated_schema_types.ContainsKey(base.XmlName))
			{
				return generated_schema_types[base.XmlName];
			}
			XmlQualifiedName qualifiedName = base.GetQualifiedName(this.element_type);
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.Name = base.XmlName.Name;
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.MinOccurs = 0m;
			xmlSchemaElement.MaxOccursString = "unbounded";
			xmlSchemaElement.Name = qualifiedName.Name;
			this.KnownTypes.Add(this.element_type);
			SerializationMap serializationMap = this.KnownTypes.FindUserMap(this.element_type);
			if (serializationMap != null)
			{
				serializationMap.GetSchemaType(schemas, generated_schema_types);
				xmlSchemaElement.IsNillable = true;
			}
			xmlSchemaElement.SchemaTypeName = qualifiedName;
			xmlSchemaSequence.Items.Add(xmlSchemaElement);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schema = base.GetSchema(schemas, base.XmlName.Namespace);
			schema.Items.Add(xmlSchemaComplexType);
			schema.Items.Add(base.GetSchemaElement(base.XmlName, xmlSchemaComplexType));
			schemas.Reprocess(schema);
			generated_schema_types[base.XmlName] = xmlSchemaComplexType;
			return xmlSchemaComplexType;
		}

		// Token: 0x04000079 RID: 121
		private Type element_type;

		// Token: 0x0400007A RID: 122
		internal XmlQualifiedName element_qname;

		// Token: 0x0400007B RID: 123
		private MethodInfo add_method;
	}
}
