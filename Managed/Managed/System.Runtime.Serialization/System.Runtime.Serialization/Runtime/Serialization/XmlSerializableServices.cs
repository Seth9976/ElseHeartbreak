using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace System.Runtime.Serialization
{
	/// <summary>Contains methods for reading and writing XML. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000033 RID: 51
	public static class XmlSerializableServices
	{
		/// <summary>Generates a default schema type given the specified type name and adds it to the specified schema set.</summary>
		/// <param name="schemas">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to add the generated schema type to.</param>
		/// <param name="typeQName">An <see cref="T:System.Xml.XmlQualifiedName" /> that specifies the type name to assign the schema to. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemas" /> or <paramref name="typeQName" /> argument is null.</exception>
		// Token: 0x06000132 RID: 306 RVA: 0x0000707C File Offset: 0x0000527C
		[MonoTODO]
		public static void AddDefaultSchema(XmlSchemaSet schemas, XmlQualifiedName typeQName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a set of XML nodes from the specified reader and returns the result.</summary>
		/// <returns>An array of type <see cref="T:System.Xml.XmlNode" />. </returns>
		/// <param name="xmlReader">An <see cref="T:System.Xml.XmlReader" /> used for reading.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="xmlReader" /> argument is null.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">While reading, a null node was encountered.</exception>
		// Token: 0x06000133 RID: 307 RVA: 0x00007084 File Offset: 0x00005284
		public static XmlNode[] ReadNodes(XmlReader xmlReader)
		{
			if (xmlReader.NodeType != XmlNodeType.Element || xmlReader.IsEmptyElement)
			{
				return new XmlNode[0];
			}
			int depth = xmlReader.Depth;
			xmlReader.Read();
			if (xmlReader.NodeType == XmlNodeType.EndElement)
			{
				return new XmlNode[0];
			}
			List<XmlNode> list = new List<XmlNode>();
			XmlDocument xmlDocument = new XmlDocument();
			while ((xmlReader.Depth > depth) & !xmlReader.EOF)
			{
				list.Add(xmlDocument.ReadNode(xmlReader));
			}
			return list.ToArray();
		}

		/// <summary>Writes the supplied nodes using the specified writer.</summary>
		/// <param name="xmlWriter">An <see cref="T:System.Xml.XmlWriter" /> used for writing.</param>
		/// <param name="nodes">An array of type <see cref="T:System.Xml.XmlNode" /> to write.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="xmlWriter" /> argument is null.</exception>
		// Token: 0x06000134 RID: 308 RVA: 0x00007110 File Offset: 0x00005310
		public static void WriteNodes(XmlWriter xmlWriter, XmlNode[] nodes)
		{
			foreach (XmlNode xmlNode in nodes)
			{
				xmlNode.WriteTo(xmlWriter);
			}
		}

		// Token: 0x040000A5 RID: 165
		private static Dictionary<XmlQualifiedName, XmlSchemaSet> defaultSchemas = new Dictionary<XmlQualifiedName, XmlSchemaSet>();
	}
}
