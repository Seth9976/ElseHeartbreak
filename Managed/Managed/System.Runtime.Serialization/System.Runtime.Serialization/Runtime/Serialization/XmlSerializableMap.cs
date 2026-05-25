using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
	// Token: 0x02000024 RID: 36
	internal class XmlSerializableMap : SerializationMap
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000051F0 File Offset: 0x000033F0
		public XmlSerializableMap(Type type, XmlQualifiedName qname, KnownTypeCollection knownTypes)
			: base(type, qname, knownTypes)
		{
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000051FC File Offset: 0x000033FC
		public override void Serialize(object graph, XmlFormatterSerializer serializer)
		{
			IXmlSerializable xmlSerializable = graph as IXmlSerializable;
			if (xmlSerializable == null)
			{
				throw new SerializationException();
			}
			xmlSerializable.WriteXml(serializer.Writer);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005228 File Offset: 0x00003428
		public override object DeserializeObject(XmlReader reader, XmlFormatterDeserializer deserializer)
		{
			IXmlSerializable xmlSerializable = (IXmlSerializable)FormatterServices.GetUninitializedObject(this.RuntimeType);
			xmlSerializable.ReadXml(reader);
			return xmlSerializable;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005250 File Offset: 0x00003450
		public override XmlSchemaType GetSchemaType(XmlSchemaSet schemas, Dictionary<XmlQualifiedName, XmlSchemaType> generated_schema_types)
		{
			return null;
		}
	}
}
