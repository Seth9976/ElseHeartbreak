using System;
using System.Collections;
using System.Xml;

// Token: 0x0200008D RID: 141
internal class XmlHelper
{
	// Token: 0x060006AC RID: 1708 RVA: 0x00021640 File Offset: 0x0001F840
	internal static string Decode(string xmlName)
	{
		string text = (string)XmlHelper.localSchemaNameCache[xmlName];
		if (text == null)
		{
			text = XmlConvert.DecodeName(xmlName);
			XmlHelper.localSchemaNameCache[xmlName] = text;
		}
		return text;
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x00021678 File Offset: 0x0001F878
	internal static string Encode(string schemaName)
	{
		string text = (string)XmlHelper.localXmlNameCache[schemaName];
		if (text == null)
		{
			text = XmlConvert.EncodeLocalName(schemaName);
			XmlHelper.localXmlNameCache[schemaName] = text;
		}
		return text;
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x000216B0 File Offset: 0x0001F8B0
	internal static void ClearCache()
	{
		XmlHelper.localSchemaNameCache.Clear();
		XmlHelper.localXmlNameCache.Clear();
	}

	// Token: 0x04000272 RID: 626
	private static Hashtable localSchemaNameCache = new Hashtable();

	// Token: 0x04000273 RID: 627
	private static Hashtable localXmlNameCache = new Hashtable();
}
