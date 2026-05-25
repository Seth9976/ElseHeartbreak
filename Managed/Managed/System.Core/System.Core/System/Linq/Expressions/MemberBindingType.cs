using System;

namespace System.Linq.Expressions
{
	/// <summary>Describes the binding types that are used in <see cref="T:System.Linq.Expressions.MemberInitExpression" /> objects.</summary>
	// Token: 0x02000046 RID: 70
	public enum MemberBindingType
	{
		/// <summary>A binding that represents initializing a member with the value of an expression.</summary>
		// Token: 0x04000108 RID: 264
		Assignment,
		/// <summary>A binding that represents recursively initializing members of a member.</summary>
		// Token: 0x04000109 RID: 265
		MemberBinding,
		/// <summary>A binding that represents initializing a member of type <see cref="T:System.Collections.IList" /> or <see cref="T:System.Collections.Generic.ICollection`1" /> from a list of elements.</summary>
		// Token: 0x0400010A RID: 266
		ListBinding
	}
}
