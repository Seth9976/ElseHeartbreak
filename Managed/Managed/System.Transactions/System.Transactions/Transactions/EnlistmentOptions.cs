using System;

namespace System.Transactions
{
	/// <summary>Determines whether the object should be enlisted during the prepare phase.</summary>
	// Token: 0x0200000E RID: 14
	[Flags]
	public enum EnlistmentOptions
	{
		/// <summary>The object does not require enlistment during the initial phase of the commitment process.</summary>
		// Token: 0x04000029 RID: 41
		None = 0,
		/// <summary>The object must enlist during the initial phase of the commitment process.</summary>
		// Token: 0x0400002A RID: 42
		EnlistDuringPrepareRequired = 1
	}
}
