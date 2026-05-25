using System;

namespace System.Data
{
	/// <summary>Indicates the action that occurs when a <see cref="T:System.Data.ForeignKeyConstraint" /> is enforced.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006E RID: 110
	public enum Rule
	{
		/// <summary>No action taken on related rows.</summary>
		// Token: 0x04000201 RID: 513
		None,
		/// <summary>Delete or update related rows. This is the default.</summary>
		// Token: 0x04000202 RID: 514
		Cascade,
		/// <summary>Set values in related rows to DBNull.</summary>
		// Token: 0x04000203 RID: 515
		SetNull,
		/// <summary>Set values in related rows to the value contained in the <see cref="P:System.Data.DataColumn.DefaultValue" /> property.</summary>
		// Token: 0x04000204 RID: 516
		SetDefault
	}
}
