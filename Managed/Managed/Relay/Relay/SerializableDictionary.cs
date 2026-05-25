using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

// Token: 0x02000009 RID: 9
[XmlRoot("dictionary")]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
{
	// Token: 0x0600002C RID: 44 RVA: 0x000030E8 File Offset: 0x000012E8
	public XmlSchema GetSchema()
	{
		return null;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000030EC File Offset: 0x000012EC
	public void ReadXml(XmlReader reader)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(TKey));
		XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(TValue));
		bool isEmptyElement = reader.IsEmptyElement;
		reader.Read();
		if (isEmptyElement)
		{
			return;
		}
		while (reader.NodeType != XmlNodeType.EndElement)
		{
			reader.ReadStartElement("item");
			reader.ReadStartElement("key");
			TKey tkey = (TKey)((object)xmlSerializer.Deserialize(reader));
			reader.ReadEndElement();
			reader.ReadStartElement("value");
			TValue tvalue = (TValue)((object)xmlSerializer2.Deserialize(reader));
			reader.ReadEndElement();
			base.Add(tkey, tvalue);
			reader.ReadEndElement();
			reader.MoveToContent();
		}
		reader.ReadEndElement();
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000031A4 File Offset: 0x000013A4
	public void WriteXml(XmlWriter writer)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(TKey));
		XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(TValue));
		foreach (TKey tkey in base.Keys)
		{
			writer.WriteStartElement("item");
			writer.WriteStartElement("key");
			xmlSerializer.Serialize(writer, tkey);
			writer.WriteEndElement();
			writer.WriteStartElement("value");
			TValue tvalue = base[tkey];
			xmlSerializer2.Serialize(writer, tvalue);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}
	}
}
