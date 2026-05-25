using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Describes the version of data in a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200003E RID: 62
	[Editor("Microsoft.VSDesigner.Data.Design.DataViewRowStateEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Flags]
	public enum DataViewRowState
	{
		/// <summary>None.</summary>
		// Token: 0x04000191 RID: 401
		None = 0,
		/// <summary>An unchanged row.</summary>
		// Token: 0x04000192 RID: 402
		Unchanged = 2,
		/// <summary>A new row.</summary>
		// Token: 0x04000193 RID: 403
		Added = 4,
		/// <summary>A deleted row.</summary>
		// Token: 0x04000194 RID: 404
		Deleted = 8,
		/// <summary>A current version of original data that has been modified (see ModifiedOriginal).</summary>
		// Token: 0x04000195 RID: 405
		ModifiedCurrent = 16,
		/// <summary>Current rows including unchanged, new, and modified rows.</summary>
		// Token: 0x04000196 RID: 406
		CurrentRows = 22,
		/// <summary>The original version of the data that was modified. (Although the data has since been modified, it is available as ModifiedCurrent).</summary>
		// Token: 0x04000197 RID: 407
		ModifiedOriginal = 32,
		/// <summary>Original rows including unchanged and deleted rows.</summary>
		// Token: 0x04000198 RID: 408
		OriginalRows = 42
	}
}
