using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x02000197 RID: 407
	internal class ArithmeticOperation : BinaryOpExpression
	{
		// Token: 0x06001545 RID: 5445 RVA: 0x0005F650 File Offset: 0x0005D850
		public ArithmeticOperation(Operation op, IExpression e1, IExpression e2)
			: base(op, e1, e2)
		{
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0005F65C File Offset: 0x0005D85C
		public override object Eval(DataRow row)
		{
			object obj = this.expr1.Eval(row);
			if (obj == DBNull.Value || obj == null)
			{
				return obj;
			}
			object obj2 = this.expr2.Eval(row);
			if (obj2 == DBNull.Value || obj2 == null)
			{
				return obj2;
			}
			if (this.op == Operation.ADD && (obj is string || obj2 is string))
			{
				return obj.ToString() + obj2.ToString();
			}
			IConvertible convertible = (IConvertible)obj;
			IConvertible convertible2 = (IConvertible)obj2;
			switch (this.op)
			{
			case Operation.ADD:
				return Numeric.Add(convertible, convertible2);
			case Operation.SUB:
				return Numeric.Subtract(convertible, convertible2);
			case Operation.MUL:
				return Numeric.Multiply(convertible, convertible2);
			case Operation.DIV:
				return Numeric.Divide(convertible, convertible2);
			case Operation.MOD:
				return Numeric.Modulo(convertible, convertible2);
			default:
				return 0;
			}
		}
	}
}
