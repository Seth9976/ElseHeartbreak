using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200002D RID: 45
	internal struct EnumMemberInfo
	{
		// Token: 0x06000109 RID: 265 RVA: 0x000066B8 File Offset: 0x000048B8
		public EnumMemberInfo(string name, object value)
		{
			this.XmlName = name;
			this.Value = value;
		}

		// Token: 0x0400008B RID: 139
		public readonly string XmlName;

		// Token: 0x0400008C RID: 140
		public readonly object Value;
	}
}
