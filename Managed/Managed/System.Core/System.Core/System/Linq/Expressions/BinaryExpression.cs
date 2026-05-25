using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	/// <summary>Represents an expression that has a binary operator.</summary>
	// Token: 0x02000035 RID: 53
	public sealed class BinaryExpression : Expression
	{
		// Token: 0x06000374 RID: 884 RVA: 0x0000F868 File Offset: 0x0000DA68
		internal BinaryExpression(ExpressionType node_type, Type type, Expression left, Expression right)
			: base(node_type, type)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000F884 File Offset: 0x0000DA84
		internal BinaryExpression(ExpressionType node_type, Type type, Expression left, Expression right, MethodInfo method)
			: base(node_type, type)
		{
			this.left = left;
			this.right = right;
			this.method = method;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		internal BinaryExpression(ExpressionType node_type, Type type, Expression left, Expression right, bool lift_to_null, bool is_lifted, MethodInfo method, LambdaExpression conversion)
			: base(node_type, type)
		{
			this.left = left;
			this.right = right;
			this.method = method;
			this.conversion = conversion;
			this.lift_to_null = lift_to_null;
			this.is_lifted = is_lifted;
		}

		/// <summary>Gets the left operand of the binary operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the left operand of the binary operation.</returns>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000F8E4 File Offset: 0x0000DAE4
		public Expression Left
		{
			get
			{
				return this.left;
			}
		}

		/// <summary>Gets the right operand of the binary operation.</summary>
		/// <returns>An <see cref="T:System.Linq.Expressions.Expression" /> that represents the right operand of the binary operation.</returns>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000F8EC File Offset: 0x0000DAEC
		public Expression Right
		{
			get
			{
				return this.right;
			}
		}

		/// <summary>Gets the implementing method for the binary operation.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodInfo" /> that represents the implementing method.</returns>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000F8F4 File Offset: 0x0000DAF4
		public MethodInfo Method
		{
			get
			{
				return this.method;
			}
		}

		/// <summary>Gets a value that indicates whether the expression tree node represents a lifted call to an operator.</summary>
		/// <returns>true if the node represents a lifted call; otherwise, false.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000F8FC File Offset: 0x0000DAFC
		public bool IsLifted
		{
			get
			{
				return this.is_lifted;
			}
		}

		/// <summary>Gets a value that indicates whether the expression tree node represents a lifted call to an operator whose return type is lifted to a nullable type.</summary>
		/// <returns>true if the operator's return type is lifted to a nullable type; otherwise, false.</returns>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000F904 File Offset: 0x0000DB04
		public bool IsLiftedToNull
		{
			get
			{
				return this.lift_to_null;
			}
		}

		/// <summary>Gets the type conversion function that is used by a coalescing operation.</summary>
		/// <returns>A <see cref="T:System.Linq.Expressions.LambdaExpression" /> that represents a type conversion function.</returns>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000F90C File Offset: 0x0000DB0C
		public LambdaExpression Conversion
		{
			get
			{
				return this.conversion;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000F914 File Offset: 0x0000DB14
		private void EmitArrayAccess(EmitContext ec)
		{
			this.left.Emit(ec);
			this.right.Emit(ec);
			ec.ig.Emit(OpCodes.Ldelem, base.Type);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000F950 File Offset: 0x0000DB50
		private void EmitLogicalBinary(EmitContext ec)
		{
			ExpressionType nodeType = base.NodeType;
			if (nodeType != ExpressionType.And)
			{
				if (nodeType != ExpressionType.AndAlso)
				{
					if (nodeType == ExpressionType.Or)
					{
						goto IL_002A;
					}
					if (nodeType != ExpressionType.OrElse)
					{
						return;
					}
				}
				if (!this.IsLifted)
				{
					this.EmitLogicalShortCircuit(ec);
				}
				else
				{
					this.EmitLiftedLogicalShortCircuit(ec);
				}
				return;
			}
			IL_002A:
			if (!this.IsLifted)
			{
				this.EmitLogical(ec);
			}
			else if (base.Type == typeof(bool?))
			{
				this.EmitLiftedLogical(ec);
			}
			else
			{
				this.EmitLiftedArithmeticBinary(ec);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		private void EmitLogical(EmitContext ec)
		{
			this.EmitNonLiftedBinary(ec);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000F9FC File Offset: 0x0000DBFC
		private void EmitLiftedLogical(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			bool flag = base.NodeType == ExpressionType.And;
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			LocalBuilder localBuilder2 = ec.EmitStored(this.right);
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			Label label3 = ig.DefineLabel();
			ec.EmitNullableGetValueOrDefault(localBuilder);
			ig.Emit(OpCodes.Brtrue, label);
			ec.EmitNullableGetValueOrDefault(localBuilder2);
			ig.Emit(OpCodes.Brtrue, label2);
			ec.EmitNullableHasValue(localBuilder);
			ig.Emit(OpCodes.Brfalse, label);
			ig.MarkLabel(label2);
			ec.EmitLoad((!flag) ? localBuilder2 : localBuilder);
			ig.Emit(OpCodes.Br, label3);
			ig.MarkLabel(label);
			ec.EmitLoad((!flag) ? localBuilder : localBuilder2);
			ig.MarkLabel(label3);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
		private void EmitLogicalShortCircuit(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			bool flag = base.NodeType == ExpressionType.AndAlso;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			ec.Emit(this.left);
			ig.Emit((!flag) ? OpCodes.Brtrue : OpCodes.Brfalse, label);
			ec.Emit(this.right);
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			ig.Emit((!flag) ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
			ig.MarkLabel(label2);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000FB6C File Offset: 0x0000DD6C
		private MethodInfo GetFalseOperator()
		{
			return Expression.GetFalseOperator(this.left.Type.GetNotNullableType());
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000FB84 File Offset: 0x0000DD84
		private MethodInfo GetTrueOperator()
		{
			return Expression.GetTrueOperator(this.left.Type.GetNotNullableType());
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000FB9C File Offset: 0x0000DD9C
		private void EmitUserDefinedLogicalShortCircuit(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			bool flag = base.NodeType == ExpressionType.AndAlso;
			Label label = ig.DefineLabel();
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			ec.EmitLoad(localBuilder);
			ig.Emit(OpCodes.Dup);
			ec.EmitCall((!flag) ? this.GetTrueOperator() : this.GetFalseOperator());
			ig.Emit(OpCodes.Brtrue, label);
			ec.Emit(this.right);
			ec.EmitCall(this.method);
			ig.MarkLabel(label);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000FC28 File Offset: 0x0000DE28
		private void EmitLiftedLogicalShortCircuit(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			bool flag = base.NodeType == ExpressionType.AndAlso;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			Label label3 = ig.DefineLabel();
			Label label4 = ig.DefineLabel();
			Label label5 = ig.DefineLabel();
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			ec.EmitNullableHasValue(localBuilder);
			ig.Emit(OpCodes.Brfalse, label);
			ec.EmitNullableGetValueOrDefault(localBuilder);
			ig.Emit(OpCodes.Ldc_I4_0);
			ig.Emit(OpCodes.Ceq);
			ig.Emit((!flag) ? OpCodes.Brfalse : OpCodes.Brtrue, label2);
			ig.MarkLabel(label);
			LocalBuilder localBuilder2 = ec.EmitStored(this.right);
			ec.EmitNullableHasValue(localBuilder2);
			ig.Emit(OpCodes.Brfalse_S, label3);
			ec.EmitNullableGetValueOrDefault(localBuilder2);
			ig.Emit(OpCodes.Ldc_I4_0);
			ig.Emit(OpCodes.Ceq);
			ig.Emit((!flag) ? OpCodes.Brfalse : OpCodes.Brtrue, label2);
			ec.EmitNullableHasValue(localBuilder);
			ig.Emit(OpCodes.Brfalse, label3);
			ig.Emit((!flag) ? OpCodes.Ldc_I4_0 : OpCodes.Ldc_I4_1);
			ig.Emit(OpCodes.Br_S, label4);
			ig.MarkLabel(label2);
			ig.Emit((!flag) ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
			ig.MarkLabel(label4);
			ec.EmitNullableNew(base.Type);
			ig.Emit(OpCodes.Br, label5);
			ig.MarkLabel(label3);
			LocalBuilder localBuilder3 = ig.DeclareLocal(base.Type);
			ec.EmitNullableInitialize(localBuilder3);
			ig.MarkLabel(label5);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000FDD4 File Offset: 0x0000DFD4
		private void EmitCoalesce(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			bool flag = localBuilder.LocalType.IsNullable();
			if (flag)
			{
				ec.EmitNullableHasValue(localBuilder);
			}
			else
			{
				ec.EmitLoad(localBuilder);
			}
			ig.Emit(OpCodes.Brfalse, label2);
			if (flag && !base.Type.IsNullable())
			{
				ec.EmitNullableGetValue(localBuilder);
			}
			else
			{
				ec.EmitLoad(localBuilder);
			}
			ig.Emit(OpCodes.Br, label);
			ig.MarkLabel(label2);
			ec.Emit(this.right);
			ig.MarkLabel(label);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000FE88 File Offset: 0x0000E088
		private void EmitConvertedCoalesce(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			if (localBuilder.LocalType.IsNullable())
			{
				ec.EmitNullableHasValue(localBuilder);
			}
			else
			{
				ec.EmitLoad(localBuilder);
			}
			ig.Emit(OpCodes.Brfalse, label2);
			ec.Emit(this.conversion);
			ec.EmitLoad(localBuilder);
			ig.Emit(OpCodes.Callvirt, this.conversion.Type.GetInvokeMethod());
			ig.Emit(OpCodes.Br, label);
			ig.MarkLabel(label2);
			ec.Emit(this.right);
			ig.MarkLabel(label);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000FF3C File Offset: 0x0000E13C
		private static bool IsInt32OrInt64(Type type)
		{
			return type == typeof(int) || type == typeof(long);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000FF6C File Offset: 0x0000E16C
		private static bool IsSingleOrDouble(Type type)
		{
			return type == typeof(float) || type == typeof(double);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000FF9C File Offset: 0x0000E19C
		private void EmitBinaryOperator(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			bool flag = Expression.IsUnsigned(this.left.Type);
			ExpressionType nodeType = base.NodeType;
			switch (nodeType)
			{
			case ExpressionType.Divide:
				ig.Emit((!flag) ? OpCodes.Div : OpCodes.Div_Un);
				break;
			case ExpressionType.Equal:
				ig.Emit(OpCodes.Ceq);
				break;
			case ExpressionType.ExclusiveOr:
				ig.Emit(OpCodes.Xor);
				break;
			case ExpressionType.GreaterThan:
				ig.Emit((!flag) ? OpCodes.Cgt : OpCodes.Cgt_Un);
				break;
			case ExpressionType.GreaterThanOrEqual:
				if (flag || BinaryExpression.IsSingleOrDouble(this.left.Type))
				{
					ig.Emit(OpCodes.Clt_Un);
				}
				else
				{
					ig.Emit(OpCodes.Clt);
				}
				ig.Emit(OpCodes.Ldc_I4_0);
				ig.Emit(OpCodes.Ceq);
				break;
			default:
				switch (nodeType)
				{
				case ExpressionType.Add:
					ig.Emit(OpCodes.Add);
					break;
				case ExpressionType.AddChecked:
					if (BinaryExpression.IsInt32OrInt64(this.left.Type))
					{
						ig.Emit(OpCodes.Add_Ovf);
					}
					else
					{
						ig.Emit((!flag) ? OpCodes.Add : OpCodes.Add_Ovf_Un);
					}
					break;
				case ExpressionType.And:
					ig.Emit(OpCodes.And);
					break;
				default:
					throw new InvalidOperationException(string.Format("Internal error: BinaryExpression contains non-Binary nodetype {0}", base.NodeType));
				}
				break;
			case ExpressionType.LeftShift:
			case ExpressionType.RightShift:
				ig.Emit(OpCodes.Ldc_I4, (this.left.Type != typeof(int)) ? 63 : 31);
				ig.Emit(OpCodes.And);
				if (base.NodeType == ExpressionType.RightShift)
				{
					ig.Emit((!flag) ? OpCodes.Shr : OpCodes.Shr_Un);
				}
				else
				{
					ig.Emit(OpCodes.Shl);
				}
				break;
			case ExpressionType.LessThan:
				ig.Emit((!flag) ? OpCodes.Clt : OpCodes.Clt_Un);
				break;
			case ExpressionType.LessThanOrEqual:
				if (flag || BinaryExpression.IsSingleOrDouble(this.left.Type))
				{
					ig.Emit(OpCodes.Cgt_Un);
				}
				else
				{
					ig.Emit(OpCodes.Cgt);
				}
				ig.Emit(OpCodes.Ldc_I4_0);
				ig.Emit(OpCodes.Ceq);
				break;
			case ExpressionType.Modulo:
				ig.Emit((!flag) ? OpCodes.Rem : OpCodes.Rem_Un);
				break;
			case ExpressionType.Multiply:
				ig.Emit(OpCodes.Mul);
				break;
			case ExpressionType.MultiplyChecked:
				if (BinaryExpression.IsInt32OrInt64(this.left.Type))
				{
					ig.Emit(OpCodes.Mul_Ovf);
				}
				else
				{
					ig.Emit((!flag) ? OpCodes.Mul : OpCodes.Mul_Ovf_Un);
				}
				break;
			case ExpressionType.NotEqual:
				ig.Emit(OpCodes.Ceq);
				ig.Emit(OpCodes.Ldc_I4_0);
				ig.Emit(OpCodes.Ceq);
				break;
			case ExpressionType.Or:
				ig.Emit(OpCodes.Or);
				break;
			case ExpressionType.Power:
				ig.Emit(OpCodes.Call, typeof(Math).GetMethod("Pow"));
				break;
			case ExpressionType.Subtract:
				ig.Emit(OpCodes.Sub);
				break;
			case ExpressionType.SubtractChecked:
				if (BinaryExpression.IsInt32OrInt64(this.left.Type))
				{
					ig.Emit(OpCodes.Sub_Ovf);
				}
				else
				{
					ig.Emit((!flag) ? OpCodes.Sub : OpCodes.Sub_Ovf_Un);
				}
				break;
			}
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000103A4 File Offset: 0x0000E5A4
		private bool IsLeftLiftedBinary()
		{
			return this.left.Type.IsNullable() && !this.right.Type.IsNullable();
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000103DC File Offset: 0x0000E5DC
		private void EmitLeftLiftedToNullBinary(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			ec.EmitNullableHasValue(localBuilder);
			ig.Emit(OpCodes.Brfalse, label);
			ec.EmitNullableGetValueOrDefault(localBuilder);
			ec.Emit(this.right);
			this.EmitBinaryOperator(ec);
			ec.EmitNullableNew(base.Type);
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			LocalBuilder localBuilder2 = ig.DeclareLocal(base.Type);
			ec.EmitNullableInitialize(localBuilder2);
			ig.MarkLabel(label2);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00010474 File Offset: 0x0000E674
		private void EmitLiftedArithmeticBinary(EmitContext ec)
		{
			if (this.IsLeftLiftedBinary())
			{
				this.EmitLeftLiftedToNullBinary(ec);
			}
			else
			{
				this.EmitLiftedToNullBinary(ec);
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00010494 File Offset: 0x0000E694
		private void EmitLiftedToNullBinary(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			LocalBuilder localBuilder2 = ec.EmitStored(this.right);
			LocalBuilder localBuilder3 = ig.DeclareLocal(base.Type);
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			ec.EmitNullableHasValue(localBuilder);
			ec.EmitNullableHasValue(localBuilder2);
			ig.Emit(OpCodes.And);
			ig.Emit(OpCodes.Brtrue, label);
			ec.EmitNullableInitialize(localBuilder3);
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			ec.EmitNullableGetValueOrDefault(localBuilder);
			ec.EmitNullableGetValueOrDefault(localBuilder2);
			this.EmitBinaryOperator(ec);
			ec.EmitNullableNew(localBuilder3.LocalType);
			ig.MarkLabel(label2);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001054C File Offset: 0x0000E74C
		private void EmitLiftedRelationalBinary(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			LocalBuilder localBuilder2 = ec.EmitStored(this.right);
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			ec.EmitNullableGetValueOrDefault(localBuilder);
			ec.EmitNullableGetValueOrDefault(localBuilder2);
			ExpressionType expressionType = base.NodeType;
			if (expressionType != ExpressionType.Equal && expressionType != ExpressionType.NotEqual)
			{
				this.EmitBinaryOperator(ec);
				ig.Emit(OpCodes.Brfalse, label);
			}
			else
			{
				ig.Emit(OpCodes.Bne_Un, label);
			}
			ec.EmitNullableHasValue(localBuilder);
			ec.EmitNullableHasValue(localBuilder2);
			expressionType = base.NodeType;
			if (expressionType != ExpressionType.Equal)
			{
				if (expressionType != ExpressionType.NotEqual)
				{
					ig.Emit(OpCodes.And);
				}
				else
				{
					ig.Emit(OpCodes.Ceq);
					ig.Emit(OpCodes.Ldc_I4_0);
					ig.Emit(OpCodes.Ceq);
				}
			}
			else
			{
				ig.Emit(OpCodes.Ceq);
			}
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			ig.Emit((base.NodeType != ExpressionType.NotEqual) ? OpCodes.Ldc_I4_0 : OpCodes.Ldc_I4_1);
			ig.MarkLabel(label2);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00010690 File Offset: 0x0000E890
		private void EmitArithmeticBinary(EmitContext ec)
		{
			if (!this.IsLifted)
			{
				this.EmitNonLiftedBinary(ec);
			}
			else
			{
				this.EmitLiftedArithmeticBinary(ec);
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000106B0 File Offset: 0x0000E8B0
		private void EmitNonLiftedBinary(EmitContext ec)
		{
			ec.Emit(this.left);
			ec.Emit(this.right);
			this.EmitBinaryOperator(ec);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000106D4 File Offset: 0x0000E8D4
		private void EmitRelationalBinary(EmitContext ec)
		{
			if (!this.IsLifted)
			{
				this.EmitNonLiftedBinary(ec);
			}
			else if (this.IsLiftedToNull)
			{
				this.EmitLiftedToNullBinary(ec);
			}
			else
			{
				this.EmitLiftedRelationalBinary(ec);
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00010718 File Offset: 0x0000E918
		private void EmitLiftedUserDefinedOperator(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			Label label3 = ig.DefineLabel();
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			LocalBuilder localBuilder2 = ec.EmitStored(this.right);
			ec.EmitNullableHasValue(localBuilder);
			ec.EmitNullableHasValue(localBuilder2);
			ExpressionType nodeType = base.NodeType;
			if (nodeType != ExpressionType.Equal)
			{
				if (nodeType != ExpressionType.NotEqual)
				{
					ig.Emit(OpCodes.And);
					ig.Emit(OpCodes.Brfalse, label2);
				}
				else
				{
					ig.Emit(OpCodes.Bne_Un, label);
					ec.EmitNullableHasValue(localBuilder);
					ig.Emit(OpCodes.Brfalse, label2);
				}
			}
			else
			{
				ig.Emit(OpCodes.Bne_Un, label2);
				ec.EmitNullableHasValue(localBuilder);
				ig.Emit(OpCodes.Brfalse, label);
			}
			ec.EmitNullableGetValueOrDefault(localBuilder);
			ec.EmitNullableGetValueOrDefault(localBuilder2);
			ec.EmitCall(this.method);
			ig.Emit(OpCodes.Br, label3);
			ig.MarkLabel(label);
			ig.Emit(OpCodes.Ldc_I4_1);
			ig.Emit(OpCodes.Br, label3);
			ig.MarkLabel(label2);
			ig.Emit(OpCodes.Ldc_I4_0);
			ig.Emit(OpCodes.Br, label3);
			ig.MarkLabel(label3);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00010860 File Offset: 0x0000EA60
		private void EmitLiftedToNullUserDefinedOperator(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			LocalBuilder localBuilder2 = ec.EmitStored(this.right);
			ec.EmitNullableHasValue(localBuilder);
			ec.EmitNullableHasValue(localBuilder2);
			ig.Emit(OpCodes.And);
			ig.Emit(OpCodes.Brfalse, label);
			ec.EmitNullableGetValueOrDefault(localBuilder);
			ec.EmitNullableGetValueOrDefault(localBuilder2);
			ec.EmitCall(this.method);
			ec.EmitNullableNew(base.Type);
			ig.Emit(OpCodes.Br, label2);
			ig.MarkLabel(label);
			LocalBuilder localBuilder3 = ig.DeclareLocal(base.Type);
			ec.EmitNullableInitialize(localBuilder3);
			ig.MarkLabel(label2);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001091C File Offset: 0x0000EB1C
		private void EmitUserDefinedLiftedLogicalShortCircuit(EmitContext ec)
		{
			ILGenerator ig = ec.ig;
			bool flag = base.NodeType == ExpressionType.AndAlso;
			Label label = ig.DefineLabel();
			Label label2 = ig.DefineLabel();
			Label label3 = ig.DefineLabel();
			Label label4 = ig.DefineLabel();
			LocalBuilder localBuilder = ec.EmitStored(this.left);
			ec.EmitNullableHasValue(localBuilder);
			ig.Emit(OpCodes.Brfalse, (!flag) ? label : label3);
			ec.EmitNullableGetValueOrDefault(localBuilder);
			ec.EmitCall((!flag) ? this.GetTrueOperator() : this.GetFalseOperator());
			ig.Emit(OpCodes.Brtrue, label2);
			ig.MarkLabel(label);
			LocalBuilder localBuilder2 = ec.EmitStored(this.right);
			ec.EmitNullableHasValue(localBuilder2);
			ig.Emit(OpCodes.Brfalse, label3);
			ec.EmitNullableGetValueOrDefault(localBuilder);
			ec.EmitNullableGetValueOrDefault(localBuilder2);
			ec.EmitCall(this.method);
			ec.EmitNullableNew(base.Type);
			ig.Emit(OpCodes.Br, label4);
			ig.MarkLabel(label2);
			ec.EmitLoad(localBuilder);
			ig.Emit(OpCodes.Br, label4);
			ig.MarkLabel(label3);
			LocalBuilder localBuilder3 = ig.DeclareLocal(base.Type);
			ec.EmitNullableInitialize(localBuilder3);
			ig.MarkLabel(label4);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00010A5C File Offset: 0x0000EC5C
		private void EmitUserDefinedOperator(EmitContext ec)
		{
			if (!this.IsLifted)
			{
				ExpressionType expressionType = base.NodeType;
				if (expressionType != ExpressionType.AndAlso && expressionType != ExpressionType.OrElse)
				{
					this.left.Emit(ec);
					this.right.Emit(ec);
					ec.EmitCall(this.method);
				}
				else
				{
					this.EmitUserDefinedLogicalShortCircuit(ec);
				}
			}
			else if (this.IsLiftedToNull)
			{
				ExpressionType expressionType = base.NodeType;
				if (expressionType != ExpressionType.AndAlso && expressionType != ExpressionType.OrElse)
				{
					this.EmitLiftedToNullUserDefinedOperator(ec);
				}
				else
				{
					this.EmitUserDefinedLiftedLogicalShortCircuit(ec);
				}
			}
			else
			{
				this.EmitLiftedUserDefinedOperator(ec);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00010B14 File Offset: 0x0000ED14
		internal override void Emit(EmitContext ec)
		{
			if (this.method != null)
			{
				this.EmitUserDefinedOperator(ec);
				return;
			}
			switch (base.NodeType)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
			case ExpressionType.Divide:
			case ExpressionType.ExclusiveOr:
			case ExpressionType.LeftShift:
			case ExpressionType.Modulo:
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
			case ExpressionType.Power:
			case ExpressionType.RightShift:
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				this.EmitArithmeticBinary(ec);
				return;
			case ExpressionType.And:
			case ExpressionType.AndAlso:
			case ExpressionType.Or:
			case ExpressionType.OrElse:
				this.EmitLogicalBinary(ec);
				return;
			case ExpressionType.ArrayIndex:
				this.EmitArrayAccess(ec);
				return;
			case ExpressionType.Coalesce:
				if (this.conversion != null)
				{
					this.EmitConvertedCoalesce(ec);
				}
				else
				{
					this.EmitCoalesce(ec);
				}
				return;
			case ExpressionType.Equal:
			case ExpressionType.GreaterThan:
			case ExpressionType.GreaterThanOrEqual:
			case ExpressionType.LessThan:
			case ExpressionType.LessThanOrEqual:
			case ExpressionType.NotEqual:
				this.EmitRelationalBinary(ec);
				return;
			}
			throw new NotSupportedException(base.NodeType.ToString());
		}

		// Token: 0x040000B3 RID: 179
		private Expression left;

		// Token: 0x040000B4 RID: 180
		private Expression right;

		// Token: 0x040000B5 RID: 181
		private LambdaExpression conversion;

		// Token: 0x040000B6 RID: 182
		private MethodInfo method;

		// Token: 0x040000B7 RID: 183
		private bool lift_to_null;

		// Token: 0x040000B8 RID: 184
		private bool is_lifted;
	}
}
