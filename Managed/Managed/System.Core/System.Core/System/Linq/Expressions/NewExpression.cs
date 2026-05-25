using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	/// <summary>Represents a constructor call.</summary>
	// Token: 0x0200004D RID: 77
	public sealed class NewExpression : Expression
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x00013F9C File Offset: 0x0001219C
		internal NewExpression(Type type, ReadOnlyCollection<Expression> arguments)
			: base(ExpressionType.New, type)
		{
			this.arguments = arguments;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00013FB0 File Offset: 0x000121B0
		internal NewExpression(ConstructorInfo constructor, ReadOnlyCollection<Expression> arguments, ReadOnlyCollection<MemberInfo> members)
			: base(ExpressionType.New, constructor.DeclaringType)
		{
			this.constructor = constructor;
			this.arguments = arguments;
			this.members = members;
		}

		/// <summary>Gets the called constructor.</summary>
		/// <returns>The <see cref="T:System.Reflection.ConstructorInfo" /> that represents the called constructor.</returns>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00013FD8 File Offset: 0x000121D8
		public ConstructorInfo Constructor
		{
			get
			{
				return this.constructor;
			}
		}

		/// <summary>Gets the arguments to the constructor.</summary>
		/// <returns>A collection of <see cref="T:System.Linq.Expressions.Expression" /> objects that represent the arguments to the constructor.</returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00013FE0 File Offset: 0x000121E0
		public ReadOnlyCollection<Expression> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		/// <summary>Gets the members that can retrieve the values of the fields that were initialized with constructor arguments.</summary>
		/// <returns>A collection of <see cref="T:System.Reflection.MemberInfo" /> objects that represent the members that can retrieve the values of the fields that were initialized with constructor arguments.</returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00013FE8 File Offset: 0x000121E8
		public ReadOnlyCollection<MemberInfo> Members
		{
			get
			{
				return this.members;
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00013FF0 File Offset: 0x000121F0
		internal override void Emit(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Type type = base.Type;
			LocalBuilder localBuilder = null;
			if (type.IsValueType)
			{
				localBuilder = ig.DeclareLocal(type);
				ig.Emit(OpCodes.Ldloca, localBuilder);
				if (this.constructor == null)
				{
					ig.Emit(OpCodes.Initobj, type);
					ig.Emit(OpCodes.Ldloc, localBuilder);
					return;
				}
			}
			ec.EmitCollection<Expression>(this.arguments);
			if (type.IsValueType)
			{
				ig.Emit(OpCodes.Call, this.constructor);
				ig.Emit(OpCodes.Ldloc, localBuilder);
			}
			else
			{
				ig.Emit(OpCodes.Newobj, this.constructor ?? NewExpression.GetDefaultConstructor(type));
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x000140A8 File Offset: 0x000122A8
		private static ConstructorInfo GetDefaultConstructor(Type type)
		{
			return type.GetConstructor(Type.EmptyTypes);
		}

		// Token: 0x04000115 RID: 277
		private ConstructorInfo constructor;

		// Token: 0x04000116 RID: 278
		private ReadOnlyCollection<Expression> arguments;

		// Token: 0x04000117 RID: 279
		private ReadOnlyCollection<MemberInfo> members;
	}
}
