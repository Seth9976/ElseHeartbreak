using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000049 RID: 73
	internal interface IXmlElement : IXmlNode
	{
		// Token: 0x060002E0 RID: 736
		void SetAttributeNode(IXmlNode attribute);

		// Token: 0x060002E1 RID: 737
		string GetPrefixOfNamespace(string namespaceURI);
	}
}
