using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace System.Runtime.Serialization
{
	// Token: 0x02000023 RID: 35
	internal abstract class SerializationMap
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x0000454C File Offset: 0x0000274C
		protected SerializationMap(Type type, XmlQualifiedName qname, KnownTypeCollection knownTypes)
		{
			this.KnownTypes = knownTypes;
			this.RuntimeType = type;
			if (qname.Namespace == null)
			{
				qname = new XmlQualifiedName(qname.Name, "http://schemas.datacontract.org/2004/07/" + type.Namespace);
			}
			this.XmlName = qname;
			this.Members = new List<DataMemberInfo>();
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000045B4 File Offset: 0x000027B4
		public virtual bool OutputXsiType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000045B8 File Offset: 0x000027B8
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000045C0 File Offset: 0x000027C0
		public XmlQualifiedName XmlName { get; set; }

		// Token: 0x060000C6 RID: 198 RVA: 0x000045CC File Offset: 0x000027CC
		public CollectionDataContractAttribute GetCollectionDataContractAttribute(Type type)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(CollectionDataContractAttribute), false);
			return (customAttributes.Length != 0) ? ((CollectionDataContractAttribute)customAttributes[0]) : null;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004604 File Offset: 0x00002804
		public DataMemberAttribute GetDataMemberAttribute(MemberInfo mi)
		{
			object[] customAttributes = mi.GetCustomAttributes(typeof(DataMemberAttribute), false);
			if (customAttributes.Length == 0)
			{
				return null;
			}
			return (DataMemberAttribute)customAttributes[0];
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004638 File Offset: 0x00002838
		private bool IsPrimitive(Type type)
		{
			return Type.GetTypeCode(type) != TypeCode.Object || type == typeof(object);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004658 File Offset: 0x00002858
		public virtual XmlSchemaType GetSchemaType(XmlSchemaSet schemas, Dictionary<XmlQualifiedName, XmlSchemaType> generated_schema_types)
		{
			if (this.IsPrimitive(this.RuntimeType))
			{
				return null;
			}
			if (generated_schema_types.ContainsKey(this.XmlName))
			{
				return generated_schema_types[this.XmlName];
			}
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.Name = this.XmlName.Name;
			generated_schema_types[this.XmlName] = xmlSchemaComplexType;
			if (this.RuntimeType.BaseType == typeof(object))
			{
				xmlSchemaComplexType.Particle = this.GetSequence(schemas, generated_schema_types);
			}
			else
			{
				XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = new XmlSchemaComplexContentExtension();
				XmlSchemaComplexContent xmlSchemaComplexContent = new XmlSchemaComplexContent();
				xmlSchemaComplexType.ContentModel = xmlSchemaComplexContent;
				xmlSchemaComplexContent.Content = xmlSchemaComplexContentExtension;
				this.KnownTypes.Add(this.RuntimeType.BaseType);
				SerializationMap serializationMap = this.KnownTypes.FindUserMap(this.RuntimeType.BaseType);
				serializationMap.GetSchemaType(schemas, generated_schema_types);
				xmlSchemaComplexContentExtension.Particle = this.GetSequence(schemas, generated_schema_types);
				xmlSchemaComplexContentExtension.BaseTypeName = this.GetQualifiedName(this.RuntimeType.BaseType);
			}
			XmlSchemaElement schemaElement = this.GetSchemaElement(this.XmlName, xmlSchemaComplexType);
			XmlSchema schema = this.GetSchema(schemas, this.XmlName.Namespace);
			schema.Items.Add(xmlSchemaComplexType);
			schema.Items.Add(schemaElement);
			schemas.Reprocess(schema);
			return xmlSchemaComplexType;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000047A8 File Offset: 0x000029A8
		private XmlSchemaSequence GetSequence(XmlSchemaSet schemas, Dictionary<XmlQualifiedName, XmlSchemaType> generated_schema_types)
		{
			List<DataMemberInfo> members = this.GetMembers();
			XmlSchema schema = this.GetSchema(schemas, this.XmlName.Namespace);
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			foreach (DataMemberInfo dataMemberInfo in members)
			{
				if (dataMemberInfo.MemberType.IsAbstract || !typeof(Delegate).IsAssignableFrom(dataMemberInfo.MemberType))
				{
					XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
					xmlSchemaElement.Name = dataMemberInfo.XmlName;
					this.KnownTypes.Add(dataMemberInfo.MemberType);
					SerializationMap serializationMap = this.KnownTypes.FindUserMap(dataMemberInfo.MemberType);
					if (serializationMap != null)
					{
						XmlSchemaType schemaType = serializationMap.GetSchemaType(schemas, generated_schema_types);
						if (schemaType is XmlSchemaComplexType)
						{
							xmlSchemaElement.IsNillable = true;
						}
					}
					else if (dataMemberInfo.MemberType == typeof(string))
					{
						xmlSchemaElement.IsNillable = true;
					}
					xmlSchemaElement.MinOccurs = 0m;
					xmlSchemaElement.SchemaTypeName = this.GetQualifiedName(dataMemberInfo.MemberType);
					this.AddImport(schema, xmlSchemaElement.SchemaTypeName.Namespace);
					xmlSchemaSequence.Items.Add(xmlSchemaElement);
				}
			}
			schemas.Reprocess(schema);
			return xmlSchemaSequence;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004920 File Offset: 0x00002B20
		private void AddImport(XmlSchema schema, string ns)
		{
			if (ns == "http://www.w3.org/2001/XMLSchema" || schema.TargetNamespace == ns)
			{
				return;
			}
			foreach (XmlSchemaObject xmlSchemaObject in schema.Includes)
			{
				XmlSchemaImport xmlSchemaImport = xmlSchemaObject as XmlSchemaImport;
				if (xmlSchemaImport != null)
				{
					if (xmlSchemaImport.Namespace == ns)
					{
						return;
					}
				}
			}
			XmlSchemaImport xmlSchemaImport2 = new XmlSchemaImport();
			xmlSchemaImport2.Namespace = ns;
			schema.Includes.Add(xmlSchemaImport2);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000049EC File Offset: 0x00002BEC
		public virtual List<DataMemberInfo> GetMembers()
		{
			throw new NotImplementedException(string.Format("Implement me for {0}", this));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004A00 File Offset: 0x00002C00
		protected XmlSchemaElement GetSchemaElement(XmlQualifiedName qname, XmlSchemaType schemaType)
		{
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = qname.Name;
			xmlSchemaElement.SchemaTypeName = qname;
			if (schemaType is XmlSchemaComplexType)
			{
				xmlSchemaElement.IsNillable = true;
			}
			return xmlSchemaElement;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004A3C File Offset: 0x00002C3C
		protected XmlSchema GetSchema(XmlSchemaSet schemas, string ns)
		{
			ICollection collection = schemas.Schemas(ns);
			if (collection.Count > 0)
			{
				if (collection.Count > 1)
				{
					throw new Exception(string.Format("More than 1 schema for namespace '{0}' found.", ns));
				}
				using (IEnumerator enumerator = collection.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						return obj as XmlSchema;
					}
				}
			}
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.TargetNamespace = ns;
			xmlSchema.ElementFormDefault = XmlSchemaForm.Qualified;
			schemas.Add(xmlSchema);
			return xmlSchema;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004AFC File Offset: 0x00002CFC
		protected XmlQualifiedName GetQualifiedName(Type type)
		{
			if (this.qname_table.ContainsKey(type))
			{
				return this.qname_table[type];
			}
			XmlQualifiedName xmlQualifiedName = this.KnownTypes.GetQName(type);
			if (xmlQualifiedName.Namespace == "http://schemas.microsoft.com/2003/10/Serialization/")
			{
				xmlQualifiedName = new XmlQualifiedName(xmlQualifiedName.Name, "http://www.w3.org/2001/XMLSchema");
			}
			this.qname_table[type] = xmlQualifiedName;
			return xmlQualifiedName;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004B68 File Offset: 0x00002D68
		public virtual void Serialize(object graph, XmlFormatterSerializer serializer)
		{
			string text = null;
			if (this.IsReference)
			{
				text = (string)serializer.References[graph];
				if (text != null)
				{
					serializer.Writer.WriteAttributeString("z", "Ref", "http://schemas.microsoft.com/2003/10/Serialization/", text);
					return;
				}
				text = "i" + (serializer.References.Count + 1);
				serializer.References.Add(graph, text);
			}
			else if (serializer.SerializingObjects.Contains(graph))
			{
				throw new SerializationException(string.Format("Circular reference of an object in the object graph was found: '{0}' of type {1}", graph, graph.GetType()));
			}
			serializer.SerializingObjects.Add(graph);
			if (text != null)
			{
				serializer.Writer.WriteAttributeString("z", "Id", "http://schemas.microsoft.com/2003/10/Serialization/", text);
			}
			this.SerializeNonReference(graph, serializer);
			serializer.SerializingObjects.Remove(graph);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004C50 File Offset: 0x00002E50
		public virtual void SerializeNonReference(object graph, XmlFormatterSerializer serializer)
		{
			foreach (DataMemberInfo dataMemberInfo in this.Members)
			{
				FieldInfo fieldInfo = dataMemberInfo.Member as FieldInfo;
				PropertyInfo propertyInfo = ((fieldInfo != null) ? null : ((PropertyInfo)dataMemberInfo.Member));
				Type type = ((fieldInfo == null) ? propertyInfo.PropertyType : fieldInfo.FieldType);
				object obj = ((fieldInfo == null) ? propertyInfo.GetValue(graph, null) : fieldInfo.GetValue(graph));
				serializer.WriteStartElement(dataMemberInfo.XmlName, dataMemberInfo.XmlRootNamespace, dataMemberInfo.XmlNamespace);
				serializer.Serialize(type, obj);
				serializer.WriteEndElement();
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004D30 File Offset: 0x00002F30
		public virtual object DeserializeObject(XmlReader reader, XmlFormatterDeserializer deserializer)
		{
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			reader.MoveToContent();
			object obj;
			if (isEmptyElement)
			{
				obj = this.DeserializeEmptyContent(reader, deserializer);
			}
			else
			{
				obj = this.DeserializeContent(reader, deserializer);
			}
			reader.MoveToContent();
			if (!isEmptyElement && reader.NodeType == XmlNodeType.EndElement)
			{
				reader.ReadEndElement();
			}
			else if (!isEmptyElement && reader.NodeType != XmlNodeType.None)
			{
				IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
				throw new SerializationException(string.Format("Deserializing type '{3}'. Expecting state 'EndElement'. Encountered state '{0}' with name '{1}' with namespace '{2}'.{4}", new object[]
				{
					reader.NodeType,
					reader.Name,
					reader.NamespaceURI,
					this.RuntimeType.FullName,
					(xmlLineInfo == null || !xmlLineInfo.HasLineInfo()) ? string.Empty : string.Format(" {0}({1},{2})", reader.BaseURI, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition)
				}));
			}
			return obj;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004E34 File Offset: 0x00003034
		public virtual object DeserializeEmptyContent(XmlReader reader, XmlFormatterDeserializer deserializer)
		{
			return this.DeserializeContent(reader, deserializer, true);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004E40 File Offset: 0x00003040
		public virtual object DeserializeContent(XmlReader reader, XmlFormatterDeserializer deserializer)
		{
			return this.DeserializeContent(reader, deserializer, false);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004E4C File Offset: 0x0000304C
		private object DeserializeContent(XmlReader reader, XmlFormatterDeserializer deserializer, bool empty)
		{
			object uninitializedObject = FormatterServices.GetUninitializedObject(this.RuntimeType);
			int num = ((reader.NodeType != XmlNodeType.None) ? (reader.Depth - 1) : reader.Depth);
			bool[] array = new bool[this.Members.Count];
			int num2 = -1;
			int num3 = -1;
			while (!empty && reader.NodeType == XmlNodeType.Element && reader.Depth > num)
			{
				DataMemberInfo dataMemberInfo = null;
				int i;
				for (i = 0; i < this.Members.Count; i++)
				{
					if (this.Members[i].Order >= 0)
					{
						break;
					}
					if (reader.LocalName == this.Members[i].XmlName && reader.NamespaceURI == this.Members[i].XmlRootNamespace)
					{
						num2 = i;
						dataMemberInfo = this.Members[i];
						break;
					}
				}
				for (i = Math.Max(i, num3); i < this.Members.Count; i++)
				{
					if (dataMemberInfo != null)
					{
						break;
					}
					if (reader.LocalName == this.Members[i].XmlName && reader.NamespaceURI == this.Members[i].XmlRootNamespace)
					{
						num2 = i;
						num3 = i;
						dataMemberInfo = this.Members[i];
						break;
					}
				}
				if (dataMemberInfo == null)
				{
					reader.Skip();
				}
				else
				{
					this.SetValue(dataMemberInfo, uninitializedObject, deserializer.Deserialize(dataMemberInfo.MemberType, reader));
					array[num2] = true;
					reader.MoveToContent();
				}
			}
			for (int j = 0; j < this.Members.Count; j++)
			{
				if (!array[j] && this.Members[j].IsRequired)
				{
					throw this.MissingRequiredMember(this.Members[j], reader);
				}
			}
			return uninitializedObject;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00005078 File Offset: 0x00003278
		protected Exception MissingRequiredMember(DataMemberInfo dmi, XmlReader reader)
		{
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			return new ArgumentException(string.Format("Data contract member {0} for the type {1} is required, but missing in the input XML.{2}", new XmlQualifiedName(dmi.XmlName, dmi.XmlNamespace), this.RuntimeType, (xmlLineInfo == null || !xmlLineInfo.HasLineInfo()) ? null : string.Format(" {0}({1},{2})", reader.BaseURI, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition)));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000050F0 File Offset: 0x000032F0
		protected void SetValue(DataMemberInfo dmi, object obj, object value)
		{
			try
			{
				if (dmi.Member is PropertyInfo)
				{
					((PropertyInfo)dmi.Member).SetValue(obj, value, null);
				}
				else
				{
					((FieldInfo)dmi.Member).SetValue(obj, value);
				}
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(string.Format("Failed to set value of type {0} for property {1}", (value == null) ? null : value.GetType(), dmi.Member), ex);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005188 File Offset: 0x00003388
		protected DataMemberInfo CreateDataMemberInfo(DataMemberAttribute dma, MemberInfo mi, Type type)
		{
			this.KnownTypes.Add(type);
			XmlQualifiedName qname = this.KnownTypes.GetQName(type);
			string @namespace = this.KnownTypes.GetQName(mi.DeclaringType).Namespace;
			if (KnownTypeCollection.GetPrimitiveTypeFromName(qname.Name) != null)
			{
				return new DataMemberInfo(mi, dma, @namespace, null);
			}
			return new DataMemberInfo(mi, dma, @namespace, qname.Namespace);
		}

		// Token: 0x04000071 RID: 113
		public const BindingFlags AllInstanceFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000072 RID: 114
		public readonly KnownTypeCollection KnownTypes;

		// Token: 0x04000073 RID: 115
		public readonly Type RuntimeType;

		// Token: 0x04000074 RID: 116
		public bool IsReference;

		// Token: 0x04000075 RID: 117
		public List<DataMemberInfo> Members;

		// Token: 0x04000076 RID: 118
		private XmlSchemaSet schema_set;

		// Token: 0x04000077 RID: 119
		private Dictionary<Type, XmlQualifiedName> qname_table = new Dictionary<Type, XmlQualifiedName>();
	}
}
