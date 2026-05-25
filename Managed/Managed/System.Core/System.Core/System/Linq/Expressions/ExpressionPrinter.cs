using System;
using System.Collections.ObjectModel;
using System.Text;

namespace System.Linq.Expressions
{
	// Token: 0x0200003D RID: 61
	internal class ExpressionPrinter : ExpressionVisitor
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x00011F18 File Offset: 0x00010118
		private ExpressionPrinter(StringBuilder builder)
		{
			this.builder = builder;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00011F28 File Offset: 0x00010128
		private ExpressionPrinter()
			: this(new StringBuilder())
		{
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00011F38 File Offset: 0x00010138
		public static string ToString(Expression expression)
		{
			ExpressionPrinter expressionPrinter = new ExpressionPrinter();
			expressionPrinter.Visit(expression);
			return expressionPrinter.builder.ToString();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00011F60 File Offset: 0x00010160
		public static string ToString(ElementInit init)
		{
			ExpressionPrinter expressionPrinter = new ExpressionPrinter();
			expressionPrinter.VisitElementInitializer(init);
			return expressionPrinter.builder.ToString();
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00011F88 File Offset: 0x00010188
		public static string ToString(MemberBinding binding)
		{
			ExpressionPrinter expressionPrinter = new ExpressionPrinter();
			expressionPrinter.VisitBinding(binding);
			return expressionPrinter.builder.ToString();
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00011FB0 File Offset: 0x000101B0
		private void Print(string str)
		{
			this.builder.Append(str);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00011FC0 File Offset: 0x000101C0
		private void Print(object obj)
		{
			this.builder.Append(obj);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00011FD0 File Offset: 0x000101D0
		private void Print(string str, params object[] objs)
		{
			this.builder.AppendFormat(str, objs);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00011FE0 File Offset: 0x000101E0
		protected override void VisitElementInitializer(ElementInit initializer)
		{
			this.Print(initializer.AddMethod);
			this.Print("(");
			this.VisitExpressionList(initializer.Arguments);
			this.Print(")");
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001201C File Offset: 0x0001021C
		protected override void VisitUnary(UnaryExpression unary)
		{
			ExpressionType nodeType = unary.NodeType;
			if (nodeType != ExpressionType.Convert && nodeType != ExpressionType.ConvertChecked)
			{
				if (nodeType == ExpressionType.Negate)
				{
					this.Print("-");
					this.Visit(unary.Operand);
					return;
				}
				if (nodeType == ExpressionType.UnaryPlus)
				{
					this.Print("+");
					this.Visit(unary.Operand);
					return;
				}
				if (nodeType != ExpressionType.ArrayLength && nodeType != ExpressionType.Not)
				{
					if (nodeType == ExpressionType.Quote)
					{
						this.Visit(unary.Operand);
						return;
					}
					if (nodeType != ExpressionType.TypeAs)
					{
						throw new NotImplementedException();
					}
					this.Print("(");
					this.Visit(unary.Operand);
					this.Print(" As {0})", new object[] { unary.Type.Name });
					return;
				}
			}
			this.Print("{0}(", new object[] { unary.NodeType });
			this.Visit(unary.Operand);
			this.Print(")");
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00012124 File Offset: 0x00010324
		private static string OperatorToString(BinaryExpression binary)
		{
			switch (binary.NodeType)
			{
			case ExpressionType.Add:
			case ExpressionType.AddChecked:
				return "+";
			case ExpressionType.And:
				return (!ExpressionPrinter.IsBoolean(binary)) ? "&" : "And";
			case ExpressionType.AndAlso:
				return "&&";
			case ExpressionType.Coalesce:
				return "??";
			case ExpressionType.Divide:
				return "/";
			case ExpressionType.Equal:
				return "=";
			case ExpressionType.ExclusiveOr:
				return "^";
			case ExpressionType.GreaterThan:
				return ">";
			case ExpressionType.GreaterThanOrEqual:
				return ">=";
			case ExpressionType.LeftShift:
				return "<<";
			case ExpressionType.LessThan:
				return "<";
			case ExpressionType.LessThanOrEqual:
				return "<=";
			case ExpressionType.Modulo:
				return "%";
			case ExpressionType.Multiply:
			case ExpressionType.MultiplyChecked:
				return "*";
			case ExpressionType.NotEqual:
				return "!=";
			case ExpressionType.Or:
				return (!ExpressionPrinter.IsBoolean(binary)) ? "|" : "Or";
			case ExpressionType.OrElse:
				return "||";
			case ExpressionType.Power:
				return "^";
			case ExpressionType.RightShift:
				return ">>";
			case ExpressionType.Subtract:
			case ExpressionType.SubtractChecked:
				return "-";
			}
			return null;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00012298 File Offset: 0x00010498
		private static bool IsBoolean(Expression expression)
		{
			return expression.Type == typeof(bool) || expression.Type == typeof(bool?);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000122D0 File Offset: 0x000104D0
		private void PrintArrayIndex(BinaryExpression index)
		{
			this.Visit(index.Left);
			this.Print("[");
			this.Visit(index.Right);
			this.Print("]");
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001230C File Offset: 0x0001050C
		protected override void VisitBinary(BinaryExpression binary)
		{
			ExpressionType nodeType = binary.NodeType;
			if (nodeType != ExpressionType.ArrayIndex)
			{
				this.Print("(");
				this.Visit(binary.Left);
				this.Print(" {0} ", new object[] { ExpressionPrinter.OperatorToString(binary) });
				this.Visit(binary.Right);
				this.Print(")");
				return;
			}
			this.PrintArrayIndex(binary);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001237C File Offset: 0x0001057C
		protected override void VisitTypeIs(TypeBinaryExpression type)
		{
			ExpressionType nodeType = type.NodeType;
			if (nodeType != ExpressionType.TypeIs)
			{
				throw new NotImplementedException();
			}
			this.Print("(");
			this.Visit(type.Expression);
			this.Print(" Is {0})", new object[] { type.TypeOperand.Name });
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000123DC File Offset: 0x000105DC
		protected override void VisitConstant(ConstantExpression constant)
		{
			object value = constant.Value;
			if (value == null)
			{
				this.Print("null");
			}
			else if (value is string)
			{
				this.Print("\"");
				this.Print(value);
				this.Print("\"");
			}
			else if (!ExpressionPrinter.HasStringRepresentation(value))
			{
				this.Print("value(");
				this.Print(value);
				this.Print(")");
			}
			else
			{
				this.Print(value);
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00012468 File Offset: 0x00010668
		private static bool HasStringRepresentation(object obj)
		{
			return obj.ToString() != obj.GetType().ToString();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001248C File Offset: 0x0001068C
		protected override void VisitConditional(ConditionalExpression conditional)
		{
			this.Print("IIF(");
			this.Visit(conditional.Test);
			this.Print(", ");
			this.Visit(conditional.IfTrue);
			this.Print(", ");
			this.Visit(conditional.IfFalse);
			this.Print(")");
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000124EC File Offset: 0x000106EC
		protected override void VisitParameter(ParameterExpression parameter)
		{
			this.Print(parameter.Name ?? "<param>");
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00012508 File Offset: 0x00010708
		protected override void VisitMemberAccess(MemberExpression access)
		{
			if (access.Expression == null)
			{
				this.Print(access.Member.DeclaringType.Name);
			}
			else
			{
				this.Visit(access.Expression);
			}
			this.Print(".{0}", new object[] { access.Member.Name });
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00012568 File Offset: 0x00010768
		protected override void VisitMethodCall(MethodCallExpression call)
		{
			if (call.Object != null)
			{
				this.Visit(call.Object);
				this.Print(".");
			}
			this.Print(call.Method.Name);
			this.Print("(");
			this.VisitExpressionList(call.Arguments);
			this.Print(")");
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000125CC File Offset: 0x000107CC
		protected override void VisitMemberAssignment(MemberAssignment assignment)
		{
			this.Print("{0} = ", new object[] { assignment.Member.Name });
			this.Visit(assignment.Expression);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00012604 File Offset: 0x00010804
		protected override void VisitMemberMemberBinding(MemberMemberBinding binding)
		{
			this.Print(binding.Member.Name);
			this.Print(" = {");
			this.VisitList<MemberBinding>(binding.Bindings, new Action<MemberBinding>(this.VisitBinding));
			this.Print("}");
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00012654 File Offset: 0x00010854
		protected override void VisitMemberListBinding(MemberListBinding binding)
		{
			this.Print(binding.Member.Name);
			this.Print(" = {");
			this.VisitList<ElementInit>(binding.Initializers, new Action<ElementInit>(this.VisitElementInitializer));
			this.Print("}");
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000126A4 File Offset: 0x000108A4
		protected override void VisitList<T>(ReadOnlyCollection<T> list, Action<T> visitor)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					this.Print(", ");
				}
				visitor(list[i]);
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000126E8 File Offset: 0x000108E8
		protected override void VisitLambda(LambdaExpression lambda)
		{
			if (lambda.Parameters.Count != 1)
			{
				this.Print("(");
				this.VisitList<ParameterExpression>(lambda.Parameters, new Action<ParameterExpression>(this.Visit));
				this.Print(")");
			}
			else
			{
				this.Visit(lambda.Parameters[0]);
			}
			this.Print(" => ");
			this.Visit(lambda.Body);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00012764 File Offset: 0x00010964
		protected override void VisitNew(NewExpression nex)
		{
			this.Print("new {0}(", new object[] { nex.Type.Name });
			if (nex.Members != null && nex.Members.Count > 0)
			{
				for (int i = 0; i < nex.Members.Count; i++)
				{
					if (i > 0)
					{
						this.Print(", ");
					}
					this.Print("{0} = ", new object[] { nex.Members[i].Name });
					this.Visit(nex.Arguments[i]);
				}
			}
			else
			{
				this.VisitExpressionList(nex.Arguments);
			}
			this.Print(")");
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00012830 File Offset: 0x00010A30
		protected override void VisitMemberInit(MemberInitExpression init)
		{
			this.Visit(init.NewExpression);
			this.Print(" {");
			this.VisitList<MemberBinding>(init.Bindings, new Action<MemberBinding>(this.VisitBinding));
			this.Print("}");
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00012878 File Offset: 0x00010A78
		protected override void VisitListInit(ListInitExpression init)
		{
			this.Visit(init.NewExpression);
			this.Print(" {");
			this.VisitList<ElementInit>(init.Initializers, new Action<ElementInit>(this.VisitElementInitializer));
			this.Print("}");
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000128C0 File Offset: 0x00010AC0
		protected override void VisitNewArray(NewArrayExpression newArray)
		{
			this.Print("new ");
			ExpressionType nodeType = newArray.NodeType;
			if (nodeType == ExpressionType.NewArrayInit)
			{
				this.Print("[] {");
				this.VisitExpressionList(newArray.Expressions);
				this.Print("}");
				return;
			}
			if (nodeType != ExpressionType.NewArrayBounds)
			{
				throw new NotSupportedException();
			}
			this.Print(newArray.Type);
			this.Print("(");
			this.VisitExpressionList(newArray.Expressions);
			this.Print(")");
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001294C File Offset: 0x00010B4C
		protected override void VisitInvocation(InvocationExpression invocation)
		{
			this.Print("Invoke(");
			this.Visit(invocation.Expression);
			if (invocation.Arguments.Count != 0)
			{
				this.Print(", ");
				this.VisitExpressionList(invocation.Arguments);
			}
			this.Print(")");
		}

		// Token: 0x040000CF RID: 207
		private const string ListSeparator = ", ";

		// Token: 0x040000D0 RID: 208
		private StringBuilder builder;
	}
}
