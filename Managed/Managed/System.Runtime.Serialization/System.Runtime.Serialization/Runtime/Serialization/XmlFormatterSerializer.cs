using System;
using System.Collections;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x02000031 RID: 49
	internal class XmlFormatterSerializer
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00006D00 File Offset: 0x00004F00
		public XmlFormatterSerializer(XmlDictionaryWriter writer, KnownTypeCollection types, bool ignoreUnknown, int maxItems, string root_ns)
		{
			this.writer = writer;
			this.types = types;
			this.ignore_unknown = ignoreUnknown;
			this.max_items = maxItems;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006D3C File Offset: 0x00004F3C
		public static void Serialize(XmlDictionaryWriter writer, object graph, KnownTypeCollection types, bool ignoreUnknown, int maxItems, string root_ns)
		{
			new XmlFormatterSerializer(writer, types, ignoreUnknown, maxItems, root_ns).Serialize((graph == null) ? null : graph.GetType(), graph);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006D70 File Offset: 0x00004F70
		public ArrayList SerializingObjects
		{
			get
			{
				return this.objects;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00006D78 File Offset: 0x00004F78
		public IDictionary References
		{
			get
			{
				return this.references;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006D80 File Offset: 0x00004F80
		public XmlDictionaryWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006D88 File Offset: 0x00004F88
		public void Serialize(Type type, object graph)
		{
			if (graph == null)
			{
				this.writer.WriteAttributeString("nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
			}
			else
			{
				Type type2 = graph.GetType();
				SerializationMap serializationMap = this.types.FindUserMap(type2);
				if (serializationMap == null)
				{
					type2 = this.types.GetSerializedType(type2);
					serializationMap = this.types.FindUserMap(type2);
				}
				if (serializationMap == null)
				{
					this.types.Add(type2);
					serializationMap = this.types.FindUserMap(type2);
				}
				if (type2 != type && (serializationMap == null || serializationMap.OutputXsiType))
				{
					XmlQualifiedName xmlName = this.types.GetXmlName(type2);
					string text = xmlName.Name;
					string text2 = xmlName.Namespace;
					if (xmlName == XmlQualifiedName.Empty)
					{
						text = XmlConvert.EncodeLocalName(type2.Name);
						text2 = "http://schemas.datacontract.org/2004/07/" + type2.Namespace;
					}
					else if (xmlName.Namespace == "http://schemas.microsoft.com/2003/10/Serialization/")
					{
						text2 = "http://www.w3.org/2001/XMLSchema";
					}
					if (this.writer.LookupPrefix(text2) == null)
					{
						this.writer.WriteXmlnsAttribute(null, text2);
					}
					this.writer.WriteStartAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
					this.writer.WriteQualifiedName(text, text2);
					this.writer.WriteEndAttribute();
				}
				XmlQualifiedName predefinedTypeName = KnownTypeCollection.GetPredefinedTypeName(type2);
				if (predefinedTypeName != XmlQualifiedName.Empty)
				{
					this.SerializePrimitive(type, graph, predefinedTypeName);
				}
				else
				{
					serializationMap.Serialize(graph, this);
				}
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006F10 File Offset: 0x00005110
		public void SerializePrimitive(Type type, object graph, XmlQualifiedName qname)
		{
			this.writer.WriteString(KnownTypeCollection.PredefinedTypeObjectToString(graph));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006F24 File Offset: 0x00005124
		public void WriteStartElement(string rootName, string rootNamespace, string currentNamespace)
		{
			this.writer.WriteStartElement(rootName, rootNamespace);
			if (!string.IsNullOrEmpty(currentNamespace) && currentNamespace != rootNamespace)
			{
				this.writer.WriteXmlnsAttribute(null, currentNamespace);
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006F58 File Offset: 0x00005158
		public void WriteEndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x04000098 RID: 152
		private XmlDictionaryWriter writer;

		// Token: 0x04000099 RID: 153
		private object graph;

		// Token: 0x0400009A RID: 154
		private KnownTypeCollection types;

		// Token: 0x0400009B RID: 155
		private bool save_id;

		// Token: 0x0400009C RID: 156
		private bool ignore_unknown;

		// Token: 0x0400009D RID: 157
		private IDataContractSurrogate surrogate;

		// Token: 0x0400009E RID: 158
		private int max_items;

		// Token: 0x0400009F RID: 159
		private ArrayList objects = new ArrayList();

		// Token: 0x040000A0 RID: 160
		private Hashtable references = new Hashtable();
	}
}
