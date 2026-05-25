using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200004B RID: 75
	internal interface IXmlDeclaration : IXmlNode
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002E5 RID: 741
		string Version { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002E6 RID: 742
		// (set) Token: 0x060002E7 RID: 743
		string Encoding { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002E8 RID: 744
		// (set) Token: 0x060002E9 RID: 745
		string Standalone { get; set; }
	}
}
