using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000056 RID: 86
	public class XmlNodeConverter : JsonConverter
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000B1B8 File Offset: 0x000093B8
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000B1C0 File Offset: 0x000093C0
		public string DeserializeRootElementName { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000B1C9 File Offset: 0x000093C9
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000B1D1 File Offset: 0x000093D1
		public bool WriteArrayAttribute { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000B1DA File Offset: 0x000093DA
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000B1E2 File Offset: 0x000093E2
		public bool OmitRootObject { get; set; }

		// Token: 0x0600033F RID: 831 RVA: 0x0000B1EC File Offset: 0x000093EC
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			IXmlNode xmlNode = this.WrapXml(value);
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
			this.PushParentNamespaces(xmlNode, xmlNamespaceManager);
			if (!this.OmitRootObject)
			{
				writer.WriteStartObject();
			}
			this.SerializeNode(writer, xmlNode, xmlNamespaceManager, !this.OmitRootObject);
			if (!this.OmitRootObject)
			{
				writer.WriteEndObject();
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000B242 File Offset: 0x00009442
		private IXmlNode WrapXml(object value)
		{
			if (value is XObject)
			{
				return XContainerWrapper.WrapNode((XObject)value);
			}
			if (value is XmlNode)
			{
				return new XmlNodeWrapper((XmlNode)value);
			}
			throw new ArgumentException("Value must be an XML object.", "value");
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000B27C File Offset: 0x0000947C
		private void PushParentNamespaces(IXmlNode node, XmlNamespaceManager manager)
		{
			List<IXmlNode> list = null;
			IXmlNode xmlNode = node;
			while ((xmlNode = xmlNode.ParentNode) != null)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					if (list == null)
					{
						list = new List<IXmlNode>();
					}
					list.Add(xmlNode);
				}
			}
			if (list != null)
			{
				list.Reverse();
				foreach (IXmlNode xmlNode2 in list)
				{
					manager.PushScope();
					foreach (IXmlNode xmlNode3 in xmlNode2.Attributes)
					{
						if (xmlNode3.NamespaceURI == "http://www.w3.org/2000/xmlns/" && xmlNode3.LocalName != "xmlns")
						{
							manager.AddNamespace(xmlNode3.LocalName, xmlNode3.Value);
						}
					}
				}
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000B370 File Offset: 0x00009570
		private string ResolveFullName(IXmlNode node, XmlNamespaceManager manager)
		{
			string text = ((node.NamespaceURI == null || (node.LocalName == "xmlns" && node.NamespaceURI == "http://www.w3.org/2000/xmlns/")) ? null : manager.LookupPrefix(node.NamespaceURI));
			if (!string.IsNullOrEmpty(text))
			{
				return text + ":" + node.LocalName;
			}
			return node.LocalName;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000B3DC File Offset: 0x000095DC
		private string GetPropertyName(IXmlNode node, XmlNamespaceManager manager)
		{
			switch (node.NodeType)
			{
			case XmlNodeType.Element:
				return this.ResolveFullName(node, manager);
			case XmlNodeType.Attribute:
				if (node.NamespaceURI == "http://james.newtonking.com/projects/json")
				{
					return "$" + node.LocalName;
				}
				return "@" + this.ResolveFullName(node, manager);
			case XmlNodeType.Text:
				return "#text";
			case XmlNodeType.CDATA:
				return "#cdata-section";
			case XmlNodeType.ProcessingInstruction:
				return "?" + this.ResolveFullName(node, manager);
			case XmlNodeType.Comment:
				return "#comment";
			case XmlNodeType.Whitespace:
				return "#whitespace";
			case XmlNodeType.SignificantWhitespace:
				return "#significant-whitespace";
			case XmlNodeType.XmlDeclaration:
				return "?xml";
			}
			throw new JsonSerializationException("Unexpected XmlNodeType when getting node name: " + node.NodeType);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000B4F4 File Offset: 0x000096F4
		private bool IsArray(IXmlNode node)
		{
			IXmlNode xmlNode;
			if (node.Attributes == null)
			{
				xmlNode = null;
			}
			else
			{
				xmlNode = node.Attributes.SingleOrDefault((IXmlNode a) => a.LocalName == "Array" && a.NamespaceURI == "http://james.newtonking.com/projects/json");
			}
			IXmlNode xmlNode2 = xmlNode;
			return xmlNode2 != null && XmlConvert.ToBoolean(xmlNode2.Value);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000B548 File Offset: 0x00009748
		private void SerializeGroupedNodes(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager, bool writePropertyName)
		{
			Dictionary<string, List<IXmlNode>> dictionary = new Dictionary<string, List<IXmlNode>>();
			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				IXmlNode xmlNode = node.ChildNodes[i];
				string propertyName = this.GetPropertyName(xmlNode, manager);
				List<IXmlNode> list;
				if (!dictionary.TryGetValue(propertyName, out list))
				{
					list = new List<IXmlNode>();
					dictionary.Add(propertyName, list);
				}
				list.Add(xmlNode);
			}
			foreach (KeyValuePair<string, List<IXmlNode>> keyValuePair in dictionary)
			{
				List<IXmlNode> value = keyValuePair.Value;
				if (value.Count == 1 && !this.IsArray(value[0]))
				{
					this.SerializeNode(writer, value[0], manager, writePropertyName);
				}
				else
				{
					string key = keyValuePair.Key;
					if (writePropertyName)
					{
						writer.WritePropertyName(key);
					}
					writer.WriteStartArray();
					for (int j = 0; j < value.Count; j++)
					{
						this.SerializeNode(writer, value[j], manager, false);
					}
					writer.WriteEndArray();
				}
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000B698 File Offset: 0x00009898
		private void SerializeNode(JsonWriter writer, IXmlNode node, XmlNamespaceManager manager, bool writePropertyName)
		{
			switch (node.NodeType)
			{
			case XmlNodeType.Element:
			{
				if (this.IsArray(node))
				{
					if (node.ChildNodes.All((IXmlNode n) => n.LocalName == node.LocalName) && node.ChildNodes.Count > 0)
					{
						this.SerializeGroupedNodes(writer, node, manager, false);
						return;
					}
				}
				foreach (IXmlNode xmlNode in node.Attributes)
				{
					if (xmlNode.NamespaceURI == "http://www.w3.org/2000/xmlns/")
					{
						string text = ((xmlNode.LocalName != "xmlns") ? xmlNode.LocalName : string.Empty);
						manager.AddNamespace(text, xmlNode.Value);
					}
				}
				if (writePropertyName)
				{
					writer.WritePropertyName(this.GetPropertyName(node, manager));
				}
				if (this.ValueAttributes(node.Attributes).Count<IXmlNode>() == 0 && node.ChildNodes.Count == 1 && node.ChildNodes[0].NodeType == XmlNodeType.Text)
				{
					writer.WriteValue(node.ChildNodes[0].Value);
					return;
				}
				if (node.ChildNodes.Count == 0 && CollectionUtils.IsNullOrEmpty<IXmlNode>(node.Attributes))
				{
					writer.WriteNull();
					return;
				}
				writer.WriteStartObject();
				for (int i = 0; i < node.Attributes.Count; i++)
				{
					this.SerializeNode(writer, node.Attributes[i], manager, true);
				}
				this.SerializeGroupedNodes(writer, node, manager, true);
				writer.WriteEndObject();
				return;
			}
			case XmlNodeType.Attribute:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				if (node.NamespaceURI == "http://www.w3.org/2000/xmlns/" && node.Value == "http://james.newtonking.com/projects/json")
				{
					return;
				}
				if (node.NamespaceURI == "http://james.newtonking.com/projects/json" && node.LocalName == "Array")
				{
					return;
				}
				if (writePropertyName)
				{
					writer.WritePropertyName(this.GetPropertyName(node, manager));
				}
				writer.WriteValue(node.Value);
				return;
			case XmlNodeType.Comment:
				if (writePropertyName)
				{
					writer.WriteComment(node.Value);
					return;
				}
				return;
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
				this.SerializeGroupedNodes(writer, node, manager, writePropertyName);
				return;
			case XmlNodeType.XmlDeclaration:
			{
				IXmlDeclaration xmlDeclaration = (IXmlDeclaration)node;
				writer.WritePropertyName(this.GetPropertyName(node, manager));
				writer.WriteStartObject();
				if (!string.IsNullOrEmpty(xmlDeclaration.Version))
				{
					writer.WritePropertyName("@version");
					writer.WriteValue(xmlDeclaration.Version);
				}
				if (!string.IsNullOrEmpty(xmlDeclaration.Encoding))
				{
					writer.WritePropertyName("@encoding");
					writer.WriteValue(xmlDeclaration.Encoding);
				}
				if (!string.IsNullOrEmpty(xmlDeclaration.Standalone))
				{
					writer.WritePropertyName("@standalone");
					writer.WriteValue(xmlDeclaration.Standalone);
				}
				writer.WriteEndObject();
				return;
			}
			}
			throw new JsonSerializationException("Unexpected XmlNodeType when serializing nodes: " + node.NodeType);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000BA68 File Offset: 0x00009C68
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
			IXmlDocument xmlDocument = null;
			IXmlNode xmlNode = null;
			if (typeof(XObject).IsAssignableFrom(objectType))
			{
				if (objectType != typeof(XDocument) && objectType != typeof(XElement))
				{
					throw new JsonSerializationException("XmlNodeConverter only supports deserializing XDocument or XElement.");
				}
				XDocument xdocument = new XDocument();
				xmlDocument = new XDocumentWrapper(xdocument);
				xmlNode = xmlDocument;
			}
			if (typeof(XmlNode).IsAssignableFrom(objectType))
			{
				if (objectType != typeof(XmlDocument))
				{
					throw new JsonSerializationException("XmlNodeConverter only supports deserializing XmlDocuments");
				}
				XmlDocument xmlDocument2 = new XmlDocument();
				xmlDocument = new XmlDocumentWrapper(xmlDocument2);
				xmlNode = xmlDocument;
			}
			if (xmlDocument == null || xmlNode == null)
			{
				throw new JsonSerializationException("Unexpected type when converting XML: " + objectType);
			}
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw new JsonSerializationException("XmlNodeConverter can only convert JSON that begins with an object.");
			}
			if (!string.IsNullOrEmpty(this.DeserializeRootElementName))
			{
				this.ReadElement(reader, xmlDocument, xmlNode, this.DeserializeRootElementName, xmlNamespaceManager);
			}
			else
			{
				reader.Read();
				this.DeserializeNode(reader, xmlDocument, xmlNamespaceManager, xmlNode);
			}
			if (objectType == typeof(XElement))
			{
				XElement xelement = (XElement)xmlDocument.DocumentElement.WrappedNode;
				xelement.Remove();
				return xelement;
			}
			return xmlDocument.WrappedNode;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000BB90 File Offset: 0x00009D90
		private void DeserializeValue(JsonReader reader, IXmlDocument document, XmlNamespaceManager manager, string propertyName, IXmlNode currentNode)
		{
			if (propertyName != null)
			{
				if (propertyName == "#text")
				{
					currentNode.AppendChild(document.CreateTextNode(reader.Value.ToString()));
					return;
				}
				if (propertyName == "#cdata-section")
				{
					currentNode.AppendChild(document.CreateCDataSection(reader.Value.ToString()));
					return;
				}
				if (propertyName == "#whitespace")
				{
					currentNode.AppendChild(document.CreateWhitespace(reader.Value.ToString()));
					return;
				}
				if (propertyName == "#significant-whitespace")
				{
					currentNode.AppendChild(document.CreateSignificantWhitespace(reader.Value.ToString()));
					return;
				}
			}
			if (!string.IsNullOrEmpty(propertyName) && propertyName[0] == '?')
			{
				this.CreateInstruction(reader, document, currentNode, propertyName);
				return;
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				this.ReadArrayElements(reader, document, propertyName, currentNode, manager);
				return;
			}
			this.ReadElement(reader, document, currentNode, propertyName, manager);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000BC8C File Offset: 0x00009E8C
		private void ReadElement(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName, XmlNamespaceManager manager)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new JsonSerializationException("XmlNodeConverter cannot convert JSON with an empty property name to XML.");
			}
			Dictionary<string, string> dictionary = this.ReadAttributeElements(reader, manager);
			string prefix = MiscellaneousUtils.GetPrefix(propertyName);
			IXmlElement xmlElement = this.CreateElement(propertyName, document, prefix, manager);
			currentNode.AppendChild(xmlElement);
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				string prefix2 = MiscellaneousUtils.GetPrefix(keyValuePair.Key);
				IXmlNode xmlNode = ((!string.IsNullOrEmpty(prefix2)) ? document.CreateAttribute(keyValuePair.Key, manager.LookupNamespace(prefix2), keyValuePair.Value) : document.CreateAttribute(keyValuePair.Key, keyValuePair.Value));
				xmlElement.SetAttributeNode(xmlNode);
			}
			if (reader.TokenType == JsonToken.String || reader.TokenType == JsonToken.Integer || reader.TokenType == JsonToken.Float || reader.TokenType == JsonToken.Boolean || reader.TokenType == JsonToken.Date)
			{
				xmlElement.AppendChild(document.CreateTextNode(this.ConvertTokenToXmlValue(reader)));
				return;
			}
			if (reader.TokenType == JsonToken.Null)
			{
				return;
			}
			if (reader.TokenType != JsonToken.EndObject)
			{
				manager.PushScope();
				this.DeserializeNode(reader, document, manager, xmlElement);
				manager.PopScope();
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000BDD8 File Offset: 0x00009FD8
		private string ConvertTokenToXmlValue(JsonReader reader)
		{
			if (reader.TokenType == JsonToken.String)
			{
				return reader.Value.ToString();
			}
			if (reader.TokenType == JsonToken.Integer)
			{
				return XmlConvert.ToString(Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture));
			}
			if (reader.TokenType == JsonToken.Float)
			{
				return XmlConvert.ToString(Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture));
			}
			if (reader.TokenType == JsonToken.Boolean)
			{
				return XmlConvert.ToString(Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture));
			}
			if (reader.TokenType == JsonToken.Date)
			{
				DateTime dateTime = Convert.ToDateTime(reader.Value, CultureInfo.InvariantCulture);
				return XmlConvert.ToString(dateTime, DateTimeUtils.ToSerializationMode(dateTime.Kind));
			}
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			throw new Exception("Cannot get an XML string value from token type '{0}'.".FormatWith(CultureInfo.InvariantCulture, new object[] { reader.TokenType }));
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000BED8 File Offset: 0x0000A0D8
		private void ReadArrayElements(JsonReader reader, IXmlDocument document, string propertyName, IXmlNode currentNode, XmlNamespaceManager manager)
		{
			string prefix = MiscellaneousUtils.GetPrefix(propertyName);
			IXmlElement xmlElement = this.CreateElement(propertyName, document, prefix, manager);
			currentNode.AppendChild(xmlElement);
			int num = 0;
			while (reader.Read() && reader.TokenType != JsonToken.EndArray)
			{
				this.DeserializeValue(reader, document, manager, propertyName, xmlElement);
				num++;
			}
			if (this.WriteArrayAttribute)
			{
				this.AddJsonArrayAttribute(xmlElement, document);
			}
			if (num == 1 && this.WriteArrayAttribute)
			{
				IXmlElement xmlElement2 = xmlElement.ChildNodes.CastValid<IXmlElement>().Single((IXmlElement n) => n.LocalName == propertyName);
				this.AddJsonArrayAttribute(xmlElement2, document);
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000BF98 File Offset: 0x0000A198
		private void AddJsonArrayAttribute(IXmlElement element, IXmlDocument document)
		{
			element.SetAttributeNode(document.CreateAttribute("json:Array", "http://james.newtonking.com/projects/json", "true"));
			if (element is XElementWrapper && element.GetPrefixOfNamespace("http://james.newtonking.com/projects/json") == null)
			{
				element.SetAttributeNode(document.CreateAttribute("xmlns:json", "http://www.w3.org/2000/xmlns/", "http://james.newtonking.com/projects/json"));
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		private Dictionary<string, string> ReadAttributeElements(JsonReader reader, XmlNamespaceManager manager)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			bool flag = false;
			bool flag2 = false;
			if (reader.TokenType != JsonToken.String && reader.TokenType != JsonToken.Null && reader.TokenType != JsonToken.Boolean && reader.TokenType != JsonToken.Integer && reader.TokenType != JsonToken.Float && reader.TokenType != JsonToken.Date && reader.TokenType != JsonToken.StartConstructor)
			{
				while (!flag && !flag2 && reader.Read())
				{
					JsonToken tokenType = reader.TokenType;
					if (tokenType != JsonToken.PropertyName)
					{
						if (tokenType != JsonToken.EndObject)
						{
							throw new JsonSerializationException("Unexpected JsonToken: " + reader.TokenType);
						}
						flag2 = true;
					}
					else
					{
						string text = reader.Value.ToString();
						if (!string.IsNullOrEmpty(text))
						{
							char c = text[0];
							char c2 = c;
							if (c2 != '$')
							{
								if (c2 == '@')
								{
									text = text.Substring(1);
									reader.Read();
									string text2 = this.ConvertTokenToXmlValue(reader);
									dictionary.Add(text, text2);
									string text3;
									if (this.IsNamespaceAttribute(text, out text3))
									{
										manager.AddNamespace(text3, text2);
									}
								}
								else
								{
									flag = true;
								}
							}
							else
							{
								text = text.Substring(1);
								reader.Read();
								string text2 = reader.Value.ToString();
								string text4 = manager.LookupPrefix("http://james.newtonking.com/projects/json");
								if (text4 == null)
								{
									int? num = null;
									while (manager.LookupNamespace("json" + num) != null)
									{
										num = new int?(num.GetValueOrDefault() + 1);
									}
									text4 = "json" + num;
									dictionary.Add("xmlns:" + text4, "http://james.newtonking.com/projects/json");
									manager.AddNamespace(text4, "http://james.newtonking.com/projects/json");
								}
								dictionary.Add(text4 + ":" + text, text2);
							}
						}
						else
						{
							flag = true;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		private void CreateInstruction(JsonReader reader, IXmlDocument document, IXmlNode currentNode, string propertyName)
		{
			if (propertyName == "?xml")
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				while (reader.Read() && reader.TokenType != JsonToken.EndObject)
				{
					string text4;
					if ((text4 = reader.Value.ToString()) != null)
					{
						if (text4 == "@version")
						{
							reader.Read();
							text = reader.Value.ToString();
							continue;
						}
						if (text4 == "@encoding")
						{
							reader.Read();
							text2 = reader.Value.ToString();
							continue;
						}
						if (text4 == "@standalone")
						{
							reader.Read();
							text3 = reader.Value.ToString();
							continue;
						}
					}
					throw new JsonSerializationException("Unexpected property name encountered while deserializing XmlDeclaration: " + reader.Value);
				}
				IXmlNode xmlNode = document.CreateXmlDeclaration(text, text2, text3);
				currentNode.AppendChild(xmlNode);
				return;
			}
			IXmlNode xmlNode2 = document.CreateProcessingInstruction(propertyName.Substring(1), reader.Value.ToString());
			currentNode.AppendChild(xmlNode2);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000C2DE File Offset: 0x0000A4DE
		private IXmlElement CreateElement(string elementName, IXmlDocument document, string elementPrefix, XmlNamespaceManager manager)
		{
			if (string.IsNullOrEmpty(elementPrefix))
			{
				return document.CreateElement(elementName);
			}
			return document.CreateElement(elementName, manager.LookupNamespace(elementPrefix));
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000C31C File Offset: 0x0000A51C
		private void DeserializeNode(JsonReader reader, IXmlDocument document, XmlNamespaceManager manager, IXmlNode currentNode)
		{
			JsonToken tokenType;
			for (;;)
			{
				tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.StartConstructor:
				{
					string text = reader.Value.ToString();
					while (reader.Read())
					{
						if (reader.TokenType == JsonToken.EndConstructor)
						{
							break;
						}
						this.DeserializeValue(reader, document, manager, text, currentNode);
					}
					goto IL_0162;
				}
				case JsonToken.PropertyName:
				{
					if (currentNode.NodeType == XmlNodeType.Document && document.DocumentElement != null)
					{
						goto Block_3;
					}
					string propertyName = reader.Value.ToString();
					reader.Read();
					if (reader.TokenType != JsonToken.StartArray)
					{
						this.DeserializeValue(reader, document, manager, propertyName, currentNode);
						goto IL_0162;
					}
					int num = 0;
					while (reader.Read() && reader.TokenType != JsonToken.EndArray)
					{
						this.DeserializeValue(reader, document, manager, propertyName, currentNode);
						num++;
					}
					if (num == 1 && this.WriteArrayAttribute)
					{
						IXmlElement xmlElement = currentNode.ChildNodes.CastValid<IXmlElement>().Single((IXmlElement n) => n.LocalName == propertyName);
						this.AddJsonArrayAttribute(xmlElement, document);
						goto IL_0162;
					}
					goto IL_0162;
				}
				case JsonToken.Comment:
					currentNode.AppendChild(document.CreateComment((string)reader.Value));
					goto IL_0162;
				}
				break;
				IL_0162:
				if (reader.TokenType != JsonToken.PropertyName && !reader.Read())
				{
					return;
				}
			}
			switch (tokenType)
			{
			case JsonToken.EndObject:
			case JsonToken.EndArray:
				return;
			default:
				throw new JsonSerializationException("Unexpected JsonToken when deserializing node: " + reader.TokenType);
			}
			Block_3:
			throw new JsonSerializationException("JSON root object has multiple properties. The root object must have a single property in order to create a valid XML document. Consider specifing a DeserializeRootElementName.");
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		private bool IsNamespaceAttribute(string attributeName, out string prefix)
		{
			if (attributeName.StartsWith("xmlns", StringComparison.Ordinal))
			{
				if (attributeName.Length == 5)
				{
					prefix = string.Empty;
					return true;
				}
				if (attributeName[5] == ':')
				{
					prefix = attributeName.Substring(6, attributeName.Length - 6);
					return true;
				}
			}
			prefix = null;
			return false;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000C505 File Offset: 0x0000A705
		private IEnumerable<IXmlNode> ValueAttributes(IEnumerable<IXmlNode> c)
		{
			return c.Where((IXmlNode a) => a.NamespaceURI != "http://james.newtonking.com/projects/json");
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000C52A File Offset: 0x0000A72A
		public override bool CanConvert(Type valueType)
		{
			return typeof(XObject).IsAssignableFrom(valueType) || typeof(XmlNode).IsAssignableFrom(valueType);
		}

		// Token: 0x040000F8 RID: 248
		private const string TextName = "#text";

		// Token: 0x040000F9 RID: 249
		private const string CommentName = "#comment";

		// Token: 0x040000FA RID: 250
		private const string CDataName = "#cdata-section";

		// Token: 0x040000FB RID: 251
		private const string WhitespaceName = "#whitespace";

		// Token: 0x040000FC RID: 252
		private const string SignificantWhitespaceName = "#significant-whitespace";

		// Token: 0x040000FD RID: 253
		private const string DeclarationName = "?xml";

		// Token: 0x040000FE RID: 254
		private const string JsonNamespaceUri = "http://james.newtonking.com/projects/json";
	}
}
