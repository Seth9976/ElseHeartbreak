using System;
using System.Configuration;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x020002D0 RID: 720
	internal class HandlersUtil
	{
		// Token: 0x060018BD RID: 6333 RVA: 0x000440BC File Offset: 0x000422BC
		private HandlersUtil()
		{
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000440C4 File Offset: 0x000422C4
		internal static string ExtractAttributeValue(string attKey, XmlNode node)
		{
			return HandlersUtil.ExtractAttributeValue(attKey, node, false);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000440D0 File Offset: 0x000422D0
		internal static string ExtractAttributeValue(string attKey, XmlNode node, bool optional)
		{
			if (node.Attributes == null)
			{
				if (optional)
				{
					return null;
				}
				HandlersUtil.ThrowException("Required attribute not found: " + attKey, node);
			}
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(attKey);
			if (xmlNode == null)
			{
				if (optional)
				{
					return null;
				}
				HandlersUtil.ThrowException("Required attribute not found: " + attKey, node);
			}
			string value = xmlNode.Value;
			if (value == string.Empty)
			{
				string text = ((!optional) ? "Required" : "Optional");
				HandlersUtil.ThrowException(text + " attribute is empty: " + attKey, node);
			}
			return value;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00044170 File Offset: 0x00042370
		internal static void ThrowException(string msg, XmlNode node)
		{
			if (node != null && node.Name != string.Empty)
			{
				msg = msg + " (node name: " + node.Name + ") ";
			}
			throw new global::System.Configuration.ConfigurationException(msg, node);
		}
	}
}
