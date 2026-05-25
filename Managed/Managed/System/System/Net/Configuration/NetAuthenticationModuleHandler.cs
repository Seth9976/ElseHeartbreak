using System;
using System.Configuration;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x020002DA RID: 730
	internal class NetAuthenticationModuleHandler : global::System.Configuration.IConfigurationSectionHandler
	{
		// Token: 0x06001901 RID: 6401 RVA: 0x00044D38 File Offset: 0x00042F38
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
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
						AuthenticationManager.Clear();
					}
					else
					{
						string text = HandlersUtil.ExtractAttributeValue("type", xmlNode);
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
						}
						if (name == "add")
						{
							AuthenticationManager.Register(NetAuthenticationModuleHandler.CreateInstance(text, xmlNode));
						}
						else if (name == "remove")
						{
							AuthenticationManager.Unregister(NetAuthenticationModuleHandler.CreateInstance(text, xmlNode));
						}
						else
						{
							HandlersUtil.ThrowException("Unexpected element", xmlNode);
						}
					}
				}
			}
			return AuthenticationManager.RegisteredModules;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00044ECC File Offset: 0x000430CC
		private static IAuthenticationModule CreateInstance(string typeName, XmlNode node)
		{
			IAuthenticationModule authenticationModule = null;
			try
			{
				Type type = Type.GetType(typeName, true);
				authenticationModule = (IAuthenticationModule)Activator.CreateInstance(type);
			}
			catch (Exception ex)
			{
				HandlersUtil.ThrowException(ex.Message, node);
			}
			return authenticationModule;
		}
	}
}
