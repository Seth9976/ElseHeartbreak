using System;

namespace System.Data
{
	/// <summary>Identifies a list of connection string parameters identified by the KeyRestrictions property that are either allowed or not allowed.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005D RID: 93
	[Serializable]
	public enum KeyRestrictionBehavior
	{
		/// <summary>Default. Identifies the only additional connection string parameters that are allowed.</summary>
		// Token: 0x040001D8 RID: 472
		AllowOnly,
		/// <summary>Identifies additional connection string parameters that are not allowed.</summary>
		// Token: 0x040001D9 RID: 473
		PreventUsage
	}
}
