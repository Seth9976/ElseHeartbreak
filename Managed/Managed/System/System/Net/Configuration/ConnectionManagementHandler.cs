using System;
using System.Configuration;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x020002CF RID: 719
	internal class ConnectionManagementHandler : global::System.Configuration.IConfigurationSectionHandler
	{
		// Token: 0x060018BC RID: 6332 RVA: 0x00043EF4 File Offset: 0x000420F4
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			ConnectionManagementData connectionManagementData = new ConnectionManagementData(parent);
			if (section.Attributes != null && section.Attributes.Count != 0)
			{
				HandlersUtil.ThrowException("Unrecognized attribute", section);
			}
			XmlNodeList childNodes = section.ChildNodes;
			foreach (object obj in childNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType != XmlNodeType.Element)
					{
						HandlersUtil.ThrowException("Only elements allowed", xmlNode);
					}
					string name = xmlNode.Name;
					if (name == "clear")
					{
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
						}
						connectionManagementData.Clear();
					}
					else
					{
						string text = HandlersUtil.ExtractAttributeValue("address", xmlNode);
						if (name == "add")
						{
							string text2 = HandlersUtil.ExtractAttributeValue("maxconnection", xmlNode, true);
							if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
							{
								HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
							}
							connectionManagementData.Add(text, text2);
						}
						else if (name == "remove")
						{
							if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
							{
								HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
							}
							connectionManagementData.Remove(text);
						}
						else
						{
							HandlersUtil.ThrowException("Unexpected element", xmlNode);
						}
					}
				}
			}
			return connectionManagementData;
		}
	}
}
