using System;

namespace System.Data
{
	/// <summary>Specifies how query command results are applied to the row being updated.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000086 RID: 134
	public enum UpdateRowSource
	{
		/// <summary>Any returned parameters or rows are ignored.</summary>
		// Token: 0x04000255 RID: 597
		None,
		/// <summary>Output parameters are mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x04000256 RID: 598
		OutputParameters,
		/// <summary>The data in the first returned row is mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x04000257 RID: 599
		FirstReturnedRecord,
		/// <summary>Both the output parameters and the first returned row are mapped to the changed row in the <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x04000258 RID: 600
		Both
	}
}
