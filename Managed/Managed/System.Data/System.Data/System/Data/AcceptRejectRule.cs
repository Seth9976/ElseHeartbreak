using System;

namespace System.Data
{
	/// <summary>Determines the action that occurs when the <see cref="M:System.Data.DataSet.AcceptChanges" /> or <see cref="M:System.Data.DataTable.RejectChanges" /> method is invoked on a <see cref="T:System.Data.DataTable" /> with a <see cref="T:System.Data.ForeignKeyConstraint" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200000C RID: 12
	public enum AcceptRejectRule
	{
		/// <summary>No action occurs (default).</summary>
		// Token: 0x04000060 RID: 96
		None,
		/// <summary>Changes are cascaded across the relationship.</summary>
		// Token: 0x04000061 RID: 97
		Cascade
	}
}
