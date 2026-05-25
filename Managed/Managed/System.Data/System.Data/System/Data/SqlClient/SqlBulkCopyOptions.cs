using System;

namespace System.Data.SqlClient
{
	/// <summary>Bitwise flag that specifies one or more options to use with an instance of <see cref="T:System.Data.SqlClient.SqlBulkCopy" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017B RID: 379
	[Flags]
	public enum SqlBulkCopyOptions
	{
		/// <summary>Check constraints while data is being inserted. By default, constraints are not checked.</summary>
		// Token: 0x0400081C RID: 2076
		CheckConstraints = 2,
		/// <summary>Use the default values for all options.</summary>
		// Token: 0x0400081D RID: 2077
		Default = 0,
		/// <summary>When specified, cause the server to fire the insert triggers for the rows being inserted into the database.</summary>
		// Token: 0x0400081E RID: 2078
		FireTriggers = 16,
		/// <summary>Preserve source identity values. When not specified, identity values are assigned by the destination.</summary>
		// Token: 0x0400081F RID: 2079
		KeepIdentity = 1,
		/// <summary>Preserve null values in the destination table regardless of the settings for default values. When not specified, null values are replaced by default values where applicable.</summary>
		// Token: 0x04000820 RID: 2080
		KeepNulls = 8,
		/// <summary>Obtain a bulk update lock for the duration of the bulk copy operation. When not specified, row locks are used.</summary>
		// Token: 0x04000821 RID: 2081
		TableLock = 4,
		/// <summary>When specified, each batch of the bulk-copy operation will occur within a transaction. If you indicate this option and also provide a <see cref="T:System.Data.SqlClient.SqlTransaction" /> object to the constructor, an <see cref="T:System.ArgumentException" /> occurs.</summary>
		// Token: 0x04000822 RID: 2082
		UseInternalTransaction = 32
	}
}
