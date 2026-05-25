using System;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	/// <summary>Represents a named parameter expression.</summary>
	// Token: 0x0200004E RID: 78
	public sealed class ParameterExpression : Expression
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x000140B8 File Offset: 0x000122B8
		internal ParameterExpression(Type type, string name)
			: base(ExpressionType.Parameter, type)
		{
			this.name = name;
		}

		/// <summary>Gets the name of the parameter.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the parameter.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x000140CC File Offset: 0x000122CC
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000140D4 File Offset: 0x000122D4
		private void EmitLocalParameter(EmitContext ec, int position)
		{
			ec.ig.Emit(OpCodes.Ldarg, position);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000140E8 File Offset: 0x000122E8
		private void EmitHoistedLocal(EmitContext ec, int level, int position)
		{
			ec.EmitScope();
			for (int i = 0; i < level; i++)
			{
				ec.EmitParentScope();
			}
			ec.EmitLoadLocals();
			ec.ig.Emit(OpCodes.Ldc_I4, position);
			ec.ig.Emit(OpCodes.Ldelem, typeof(object));
			ec.EmitLoadStrongBoxValue(base.Type);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00014150 File Offset: 0x00012350
		internal override void Emit(EmitContext ec)
		{
			int num = -1;
			if (ec.IsLocalParameter(this, ref num))
			{
				this.EmitLocalParameter(ec, num);
				return;
			}
			int num2 = 0;
			if (ec.IsHoistedLocal(this, ref num2, ref num))
			{
				this.EmitHoistedLocal(ec, num2, num);
				return;
			}
			throw new InvalidOperationException("Parameter out of scope");
		}

		// Token: 0x04000118 RID: 280
		private string name;
	}
}
