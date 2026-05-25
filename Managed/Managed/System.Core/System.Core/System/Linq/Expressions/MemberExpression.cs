using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	/// <summary>Represents accessing a field or property.</summary>
	// Token: 0x02000047 RID: 71
	public sealed class MemberExpression : Expression
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x00013B24 File Offset: 0x00011D24
		internal MemberExpression(Expression expression, MemberInfo member, Type type)
			: base(ExpressionType.MemberAccess, type)
		{
			this.expression = expression;
			this.member = member;
		}

		/// <summary>Gets the containing object of the field or property.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the containing object of the field or property.</returns>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00013B40 File Offset: 0x00011D40
		public Expression Expression
		{
			get
			{
				return this.expression;
			}
		}

		/// <summary>Gets the field or property to be accessed.</summary>
		/// <returns>The <see cref="T:System.Reflection.MemberInfo" /> that represents the field or property to be accessed.</returns>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00013B48 File Offset: 0x00011D48
		public MemberInfo Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00013B50 File Offset: 0x00011D50
		internal override void Emit(EmitContext ec)
		{
			this.member.OnFieldOrProperty(delegate(FieldInfo field)
			{
				this.EmitFieldAccess(ec, field);
			}, delegate(PropertyInfo prop)
			{
				this.EmitPropertyAccess(ec, prop);
			});
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00013B94 File Offset: 0x00011D94
		private void EmitPropertyAccess(EmitContext ec, PropertyInfo property)
		{
			MethodInfo getMethod = property.GetGetMethod(true);
			if (!getMethod.IsStatic)
			{
				ec.EmitLoadSubject(this.expression);
			}
			ec.EmitCall(getMethod);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00013BC8 File Offset: 0x00011DC8
		private void EmitFieldAccess(EmitContext ec, FieldInfo field)
		{
			if (!field.IsStatic)
			{
				ec.EmitLoadSubject(this.expression);
				ec.ig.Emit(OpCodes.Ldfld, field);
			}
			else
			{
				ec.ig.Emit(OpCodes.Ldsfld, field);
			}
		}

		// Token: 0x0400010B RID: 267
		private Expression expression;

		// Token: 0x0400010C RID: 268
		private MemberInfo member;
	}
}
