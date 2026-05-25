using System;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x0200018C RID: 396
	internal class Numeric
	{
		// Token: 0x06001509 RID: 5385 RVA: 0x0005E610 File Offset: 0x0005C810
		internal static bool IsNumeric(object o)
		{
			if (o is IConvertible)
			{
				TypeCode typeCode = ((IConvertible)o).GetTypeCode();
				if (TypeCode.Char < typeCode && typeCode <= TypeCode.Decimal)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0005E648 File Offset: 0x0005C848
		internal static IConvertible Unify(IConvertible o)
		{
			switch (o.GetTypeCode())
			{
			case TypeCode.Char:
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
				return (IConvertible)Convert.ChangeType(o, TypeCode.Int32);
			case TypeCode.UInt32:
				return (IConvertible)Convert.ChangeType(o, TypeCode.Int64);
			case TypeCode.UInt64:
				return (IConvertible)Convert.ChangeType(o, TypeCode.Decimal);
			case TypeCode.Single:
				return (IConvertible)Convert.ChangeType(o, TypeCode.Double);
			}
			return o;
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0005E6CC File Offset: 0x0005C8CC
		internal static TypeCode ToSameType(ref IConvertible o1, ref IConvertible o2)
		{
			TypeCode typeCode = o1.GetTypeCode();
			TypeCode typeCode2 = o2.GetTypeCode();
			if (typeCode == typeCode2)
			{
				return typeCode;
			}
			if (typeCode == TypeCode.DBNull || typeCode2 == TypeCode.DBNull)
			{
				return TypeCode.DBNull;
			}
			if (typeCode < typeCode2)
			{
				o1 = (IConvertible)Convert.ChangeType(o1, typeCode2);
				return typeCode2;
			}
			o2 = (IConvertible)Convert.ChangeType(o2, typeCode);
			return typeCode;
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0005E72C File Offset: 0x0005C92C
		internal static IConvertible Add(IConvertible o1, IConvertible o2)
		{
			switch (Numeric.ToSameType(ref o1, ref o2))
			{
			case TypeCode.Int32:
				return (long)((int)o1 + (int)o2);
			case TypeCode.Int64:
				return (long)o1 + (long)o2;
			case TypeCode.Double:
				return (double)o1 + (double)o2;
			case TypeCode.Decimal:
				return (decimal)o1 + (decimal)o2;
			}
			return DBNull.Value;
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0005E7C4 File Offset: 0x0005C9C4
		internal static IConvertible Subtract(IConvertible o1, IConvertible o2)
		{
			switch (Numeric.ToSameType(ref o1, ref o2))
			{
			case TypeCode.Int32:
				return (int)o1 - (int)o2;
			case TypeCode.Int64:
				return (long)o1 - (long)o2;
			case TypeCode.Double:
				return (double)o1 - (double)o2;
			case TypeCode.Decimal:
				return (decimal)o1 - (decimal)o2;
			}
			return DBNull.Value;
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0005E85C File Offset: 0x0005CA5C
		internal static IConvertible Multiply(IConvertible o1, IConvertible o2)
		{
			switch (Numeric.ToSameType(ref o1, ref o2))
			{
			case TypeCode.Int32:
				return (int)o1 * (int)o2;
			case TypeCode.Int64:
				return (long)o1 * (long)o2;
			case TypeCode.Double:
				return (double)o1 * (double)o2;
			case TypeCode.Decimal:
				return (decimal)o1 * (decimal)o2;
			}
			return DBNull.Value;
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0005E8F4 File Offset: 0x0005CAF4
		internal static IConvertible Divide(IConvertible o1, IConvertible o2)
		{
			switch (Numeric.ToSameType(ref o1, ref o2))
			{
			case TypeCode.Int32:
				return (int)o1 / (int)o2;
			case TypeCode.Int64:
				return (long)o1 / (long)o2;
			case TypeCode.Double:
				return (double)o1 / (double)o2;
			case TypeCode.Decimal:
				return (decimal)o1 / (decimal)o2;
			}
			return DBNull.Value;
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0005E98C File Offset: 0x0005CB8C
		internal static IConvertible Modulo(IConvertible o1, IConvertible o2)
		{
			switch (Numeric.ToSameType(ref o1, ref o2))
			{
			case TypeCode.Int32:
				return (int)o1 % (int)o2;
			case TypeCode.Int64:
				return (long)o1 % (long)o2;
			case TypeCode.Double:
				return (double)o1 % (double)o2;
			case TypeCode.Decimal:
				return (decimal)o1 % (decimal)o2;
			}
			return DBNull.Value;
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0005EA24 File Offset: 0x0005CC24
		internal static IConvertible Negative(IConvertible o)
		{
			switch (o.GetTypeCode())
			{
			case TypeCode.Int32:
				return -(int)o;
			case TypeCode.Int64:
				return -(long)o;
			case TypeCode.Double:
				return -(double)o;
			case TypeCode.Decimal:
				return -(decimal)o;
			}
			return DBNull.Value;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0005EAA0 File Offset: 0x0005CCA0
		internal static IConvertible Min(IConvertible o1, IConvertible o2)
		{
			switch (Numeric.ToSameType(ref o1, ref o2))
			{
			case TypeCode.Int32:
				return Math.Min((int)o1, (int)o2);
			case TypeCode.Int64:
				return Math.Min((long)o1, (long)o2);
			case TypeCode.Double:
				return Math.Min((double)o1, (double)o2);
			case TypeCode.Decimal:
				return Math.Min((decimal)o1, (decimal)o2);
			case TypeCode.String:
			{
				int num = string.Compare((string)o1, (string)o2);
				if (num <= 0)
				{
					return o1;
				}
				return o2;
			}
			}
			return DBNull.Value;
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0005EB6C File Offset: 0x0005CD6C
		internal static IConvertible Max(IConvertible o1, IConvertible o2)
		{
			switch (Numeric.ToSameType(ref o1, ref o2))
			{
			case TypeCode.Int32:
				return Math.Max((int)o1, (int)o2);
			case TypeCode.Int64:
				return Math.Max((long)o1, (long)o2);
			case TypeCode.Double:
				return Math.Max((double)o1, (double)o2);
			case TypeCode.Decimal:
				return Math.Max((decimal)o1, (decimal)o2);
			case TypeCode.String:
			{
				int num = string.Compare((string)o1, (string)o2);
				if (num >= 0)
				{
					return o1;
				}
				return o2;
			}
			}
			return DBNull.Value;
		}
	}
}
