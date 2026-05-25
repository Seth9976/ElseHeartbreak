using System;

namespace System.Xml.Linq
{
	/// <summary>Specifies serialization options.</summary>
	// Token: 0x0200000C RID: 12
	[Flags]
	public enum SaveOptions
	{
		/// <summary>Format (indent) the XML while serializing.</summary>
		// Token: 0x04000024 RID: 36
		None = 0,
		/// <summary>Preserve all insignificant white space while serializing.</summary>
		// Token: 0x04000025 RID: 37
		DisableFormatting = 1
	}
}
