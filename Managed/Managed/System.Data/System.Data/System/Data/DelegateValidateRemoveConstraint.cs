using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020001A8 RID: 424
	// (Invoke) Token: 0x06001585 RID: 5509
	[Editor]
	[Serializable]
	internal delegate void DelegateValidateRemoveConstraint(ConstraintCollection sender, Constraint constraintToRemove, ref bool fail, ref string failReason);
}
