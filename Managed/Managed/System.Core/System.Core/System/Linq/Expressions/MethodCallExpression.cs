using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	/// <summary>Represents calling a method.</summary>
	// Token: 0x0200004B RID: 75
	public sealed class MethodCallExpression : Expression
	{
		// Token: 0x06000476 RID: 1142 RVA: 0x00013D8C File Offset: 0x00011F8C
		internal MethodCallExpression(MethodInfo method, ReadOnlyCollection<Expression> arguments)
			: base(ExpressionType.Call, method.ReturnType)
		{
			this.method = method;
			this.arguments = arguments;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00013DAC File Offset: 0x00011FAC
		internal MethodCallExpression(Expression obj, MethodInfo method, ReadOnlyCollection<Expression> arguments)
			: base(ExpressionType.Call, method.ReturnType)
		{
			this.obj = obj;
			this.method = method;
			this.arguments = arguments;
		}

		/// <summary>Gets the receiving object of the method.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the receiving object of the method.</returns>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00013DDC File Offset: 0x00011FDC
		public Expression Object
		{
			get
			{
				return this.obj;
			}
		}

		/// <summary>Gets the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> that represents the called method.</returns>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00013DE4 File Offset: 0x00011FE4
		public MethodInfo Method
		{
			get
			{
				return this.method;
			}
		}

		/// <summary>Gets the arguments to the called method.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.Expression" /> objects which represent the arguments to the called method.</returns>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00013DEC File Offset: 0x00011FEC
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00013DF4 File Offset: 0x00011FF4
		internal override void Emit(EmitContext ec)
		{
			ec.EmitCall(this.obj, this.arguments, this.method);
		}

		// Token: 0x04000111 RID: 273
		private Expression obj;

		// Token: 0x04000112 RID: 274
		private MethodInfo method;

		// Token: 0x04000113 RID: 275
		private ReadOnlyCollection<Expression> arguments;
	}
}
