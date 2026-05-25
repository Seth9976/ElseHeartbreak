using System;

namespace System.Linq.Expressions
{
	/// <summary>Describes the node types for the nodes of an expression tree.</summary>
	// Token: 0x0200003E RID: 62
	public enum ExpressionType
	{
		/// <summary>A node that represents arithmetic addition without overflow checking.</summary>
		// Token: 0x040000D2 RID: 210
		Add,
		/// <summary>A node that represents arithmetic addition with overflow checking.</summary>
		// Token: 0x040000D3 RID: 211
		AddChecked,
		/// <summary>A node that represents a bitwise AND operation.</summary>
		// Token: 0x040000D4 RID: 212
		And,
		/// <summary>A node that represents a short-circuiting conditional AND operation.</summary>
		// Token: 0x040000D5 RID: 213
		AndAlso,
		/// <summary>A node that represents getting the length of a one-dimensional array.</summary>
		// Token: 0x040000D6 RID: 214
		ArrayLength,
		/// <summary>A node that represents indexing into a one-dimensional array.</summary>
		// Token: 0x040000D7 RID: 215
		ArrayIndex,
		/// <summary>A node that represents a method call.</summary>
		// Token: 0x040000D8 RID: 216
		Call,
		/// <summary>A node that represents a null coalescing operation.</summary>
		// Token: 0x040000D9 RID: 217
		Coalesce,
		/// <summary>A node that represents a conditional operation.</summary>
		// Token: 0x040000DA RID: 218
		Conditional,
		/// <summary>A node that represents an expression that has a constant value.</summary>
		// Token: 0x040000DB RID: 219
		Constant,
		/// <summary>A node that represents a cast or conversion operation. If the operation is a numeric conversion, it overflows silently if the converted value does not fit the target type.</summary>
		// Token: 0x040000DC RID: 220
		Convert,
		/// <summary>A node that represents a cast or conversion operation. If the operation is a numeric conversion, an exception is thrown if the converted value does not fit the target type.</summary>
		// Token: 0x040000DD RID: 221
		ConvertChecked,
		/// <summary>A node that represents arithmetic division.</summary>
		// Token: 0x040000DE RID: 222
		Divide,
		/// <summary>A node that represents an equality comparison.</summary>
		// Token: 0x040000DF RID: 223
		Equal,
		/// <summary>A node that represents a bitwise XOR operation.</summary>
		// Token: 0x040000E0 RID: 224
		ExclusiveOr,
		/// <summary>A node that represents a "greater than" numeric comparison.</summary>
		// Token: 0x040000E1 RID: 225
		GreaterThan,
		/// <summary>A node that represents a "greater than or equal" numeric comparison.</summary>
		// Token: 0x040000E2 RID: 226
		GreaterThanOrEqual,
		/// <summary>A node that represents applying a delegate or lambda expression to a list of argument expressions.</summary>
		// Token: 0x040000E3 RID: 227
		Invoke,
		/// <summary>A node that represents a lambda expression.</summary>
		// Token: 0x040000E4 RID: 228
		Lambda,
		/// <summary>A node that represents a bitwise left-shift operation.</summary>
		// Token: 0x040000E5 RID: 229
		LeftShift,
		/// <summary>A node that represents a "less than" numeric comparison.</summary>
		// Token: 0x040000E6 RID: 230
		LessThan,
		/// <summary>A node that represents a "less than or equal" numeric comparison.</summary>
		// Token: 0x040000E7 RID: 231
		LessThanOrEqual,
		/// <summary>A node that represents creating a new <see cref="T:System.Collections.IEnumerable" /> object and initializing it from a list of elements.</summary>
		// Token: 0x040000E8 RID: 232
		ListInit,
		/// <summary>A node that represents reading from a field or property.</summary>
		// Token: 0x040000E9 RID: 233
		MemberAccess,
		/// <summary>A node that represents creating a new object and initializing one or more of its members.</summary>
		// Token: 0x040000EA RID: 234
		MemberInit,
		/// <summary>A node that represents an arithmetic remainder operation.</summary>
		// Token: 0x040000EB RID: 235
		Modulo,
		/// <summary>A node that represents arithmetic multiplication without overflow checking.</summary>
		// Token: 0x040000EC RID: 236
		Multiply,
		/// <summary>A node that represents arithmetic multiplication with overflow checking.</summary>
		// Token: 0x040000ED RID: 237
		MultiplyChecked,
		/// <summary>A node that represents an arithmetic negation operation.</summary>
		// Token: 0x040000EE RID: 238
		Negate,
		/// <summary>A node that represents a unary plus operation. The result of a predefined unary plus operation is simply the value of the operand, but user-defined implementations may have non-trivial results.</summary>
		// Token: 0x040000EF RID: 239
		UnaryPlus,
		/// <summary>A node that represents an arithmetic negation operation that has overflow checking.</summary>
		// Token: 0x040000F0 RID: 240
		NegateChecked,
		/// <summary>A node that represents calling a constructor to create a new object.</summary>
		// Token: 0x040000F1 RID: 241
		New,
		/// <summary>A node that represents creating a new one-dimensional array and initializing it from a list of elements.</summary>
		// Token: 0x040000F2 RID: 242
		NewArrayInit,
		/// <summary>A node that represents creating a new array where the bounds for each dimension are specified.</summary>
		// Token: 0x040000F3 RID: 243
		NewArrayBounds,
		/// <summary>A node that represents a bitwise complement operation.</summary>
		// Token: 0x040000F4 RID: 244
		Not,
		/// <summary>A node that represents an inequality comparison.</summary>
		// Token: 0x040000F5 RID: 245
		NotEqual,
		/// <summary>A node that represents a bitwise OR operation.</summary>
		// Token: 0x040000F6 RID: 246
		Or,
		/// <summary>A node that represents a short-circuiting conditional OR operation.</summary>
		// Token: 0x040000F7 RID: 247
		OrElse,
		/// <summary>A node that represents a reference to a parameter defined in the context of the expression.</summary>
		// Token: 0x040000F8 RID: 248
		Parameter,
		/// <summary>A node that represents raising a number to a power.</summary>
		// Token: 0x040000F9 RID: 249
		Power,
		/// <summary>A node that represents an expression that has a constant value of type <see cref="T:System.Linq.Expressions.Expression" />. A <see cref="F:System.Linq.Expressions.ExpressionType.Quote" /> node can contain references to parameters defined in the context of the expression it represents.</summary>
		// Token: 0x040000FA RID: 250
		Quote,
		/// <summary>A node that represents a bitwise right-shift operation.</summary>
		// Token: 0x040000FB RID: 251
		RightShift,
		/// <summary>A node that represents arithmetic subtraction without overflow checking.</summary>
		// Token: 0x040000FC RID: 252
		Subtract,
		/// <summary>A node that represents arithmetic subtraction with overflow checking.</summary>
		// Token: 0x040000FD RID: 253
		SubtractChecked,
		/// <summary>A node that represents an explicit reference or boxing conversion where null is supplied if the conversion fails.</summary>
		// Token: 0x040000FE RID: 254
		TypeAs,
		/// <summary>A node that represents a type test.</summary>
		// Token: 0x040000FF RID: 255
		TypeIs
	}
}
