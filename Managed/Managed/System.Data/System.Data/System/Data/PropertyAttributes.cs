using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Specifies the attributes of a property.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000069 RID: 105
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Flags]
	[Obsolete]
	public enum PropertyAttributes
	{
		/// <summary>The property is not supported by the provider.</summary>
		// Token: 0x040001FA RID: 506
		NotSupported = 0,
		/// <summary>The user must specify a value for this property before the data source is initialized.</summary>
		// Token: 0x040001FB RID: 507
		Required = 1,
		/// <summary>The user does not need to specify a value for this property before the data source is initialized.</summary>
		// Token: 0x040001FC RID: 508
		Optional = 2,
		/// <summary>The user can read the property.</summary>
		// Token: 0x040001FD RID: 509
		Read = 512,
		/// <summary>The user can write to the property.</summary>
		// Token: 0x040001FE RID: 510
		Write = 1024
	}
}
