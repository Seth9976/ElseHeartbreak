using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	/// <summary>Provides the base class from which the classes that represent bindings that are used to initialize members of a newly created object derive.</summary>
	// Token: 0x02000045 RID: 69
	public abstract class MemberBinding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Linq.Expressions.MemberBinding" /> class.</summary>
		/// <param name="type">The <see cref="T:System.Linq.Expressions.MemberBindingType" /> that discriminates the type of binding that is represented.</param>
		/// <param name="member">The <see cref="T:System.Reflection.MemberInfo" /> that represents a field or property to be initialized.</param>
		// Token: 0x0600045E RID: 1118 RVA: 0x00013A14 File Offset: 0x00011C14
		protected MemberBinding(MemberBindingType binding_type, MemberInfo member)
		{
			this.binding_type = binding_type;
			this.member = member;
		}

		/// <summary>Gets the type of binding that is represented.</summary>
		/// <returns>One of the <see cref="T:System.Linq.Expressions.MemberBindingType" /> values.</returns>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00013A2C File Offset: 0x00011C2C
		public MemberBindingType BindingType
		{
			get
			{
				return this.binding_type;
			}
		}

		/// <summary>Gets the field or property to be initialized.</summary>
		/// <returns>The <see cref="T:System.Reflection.MemberInfo" /> that represents the field or property to be initialized.</returns>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00013A34 File Offset: 0x00011C34
		public MemberInfo Member
		{
			get
			{
				return this.member;
			}
		}

		/// <summary>Returns a textual representation of the <see cref="T:System.Linq.Expressions.MemberBinding" />.</summary>
		/// <returns>A textual representation of the <see cref="T:System.Linq.Expressions.MemberBinding" />.</returns>
		// Token: 0x06000461 RID: 1121 RVA: 0x00013A3C File Offset: 0x00011C3C
		public override string ToString()
		{
			return ExpressionPrinter.ToString(this);
		}

		// Token: 0x06000462 RID: 1122
		internal abstract void Emit(EmitContext ec, LocalBuilder local);

		// Token: 0x06000463 RID: 1123 RVA: 0x00013A44 File Offset: 0x00011C44
		internal LocalBuilder EmitLoadMember(EmitContext ec, LocalBuilder local)
		{
			ec.EmitLoadSubject(local);
			return this.member.OnFieldOrProperty((FieldInfo field) => this.EmitLoadField(ec, field), (PropertyInfo prop) => this.EmitLoadProperty(ec, prop));
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00013A94 File Offset: 0x00011C94
		private LocalBuilder EmitLoadProperty(EmitContext ec, PropertyInfo property)
		{
			MethodInfo getMethod = property.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new NotSupportedException();
			}
			LocalBuilder localBuilder = ec.ig.DeclareLocal(property.PropertyType);
			ec.EmitCall(getMethod);
			ec.ig.Emit(OpCodes.Stloc, localBuilder);
			return localBuilder;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00013AE0 File Offset: 0x00011CE0
		private LocalBuilder EmitLoadField(EmitContext ec, FieldInfo field)
		{
			LocalBuilder localBuilder = ec.ig.DeclareLocal(field.FieldType);
			ec.ig.Emit(OpCodes.Ldfld, field);
			ec.ig.Emit(OpCodes.Stloc, localBuilder);
			return localBuilder;
		}

		// Token: 0x04000105 RID: 261
		private MemberBindingType binding_type;

		// Token: 0x04000106 RID: 262
		private MemberInfo member;
	}
}
