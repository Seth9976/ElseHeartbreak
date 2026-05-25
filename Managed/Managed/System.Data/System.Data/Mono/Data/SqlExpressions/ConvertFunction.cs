using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x020001A2 RID: 418
	internal class ConvertFunction : UnaryExpression
	{
		// Token: 0x06001568 RID: 5480 RVA: 0x00060000 File Offset: 0x0005E200
		public ConvertFunction(IExpression e, string targetType)
			: base(e)
		{
			try
			{
				this.targetType = Type.GetType(targetType, true);
			}
			catch (TypeLoadException)
			{
				throw new EvaluateException(string.Format("Invalid type name '{0}'.", targetType));
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0006005C File Offset: 0x0005E25C
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			if (!(obj is ConvertFunction))
			{
				return false;
			}
			ConvertFunction convertFunction = (ConvertFunction)obj;
			return convertFunction.targetType == this.targetType;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x000600A0 File Offset: 0x0005E2A0
		public override int GetHashCode()
		{
			return this.targetType.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x000600B4 File Offset: 0x0005E2B4
		public override object Eval(DataRow row)
		{
			object obj = this.expr.Eval(row);
			if (obj == null)
			{
				return DBNull.Value;
			}
			if (obj == DBNull.Value || obj.GetType() == this.targetType)
			{
				return obj;
			}
			if (this.targetType == typeof(string))
			{
				return obj.ToString();
			}
			if (this.targetType == typeof(TimeSpan))
			{
				if (obj is string)
				{
					return TimeSpan.Parse((string)obj);
				}
				this.ThrowInvalidCastException(obj);
			}
			if (obj is TimeSpan)
			{
				this.ThrowInvalidCastException(obj);
			}
			if (obj is char && this.targetType != typeof(int) && this.targetType != typeof(uint))
			{
				this.ThrowInvalidCastException(obj);
			}
			if (this.targetType == typeof(char) && !(obj is int) && !(obj is uint))
			{
				this.ThrowInvalidCastException(obj);
			}
			if (obj is bool && (this.targetType == typeof(float) || this.targetType == typeof(double) || this.targetType == typeof(decimal)))
			{
				this.ThrowInvalidCastException(obj);
			}
			if (this.targetType == typeof(bool) && (obj is float || obj is double || obj is decimal))
			{
				this.ThrowInvalidCastException(obj);
			}
			return Convert.ChangeType(obj, this.targetType);
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00060268 File Offset: 0x0005E468
		private void ThrowInvalidCastException(object val)
		{
			throw new InvalidCastException(string.Format("Type '{0}' cannot be converted to '{1}'.", val.GetType(), this.targetType));
		}

		// Token: 0x04000892 RID: 2194
		private Type targetType;
	}
}
