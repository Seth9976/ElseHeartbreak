using System;
using System.Data;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x02000193 RID: 403
	internal class Literal : BaseExpression
	{
		// Token: 0x0600152C RID: 5420 RVA: 0x0005EE60 File Offset: 0x0005D060
		public Literal(object val)
		{
			this.val = val;
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0005EE70 File Offset: 0x0005D070
		public override bool Equals(object obj)
		{
			if (!base.Equals(obj))
			{
				return false;
			}
			if (!(obj is Literal))
			{
				return false;
			}
			Literal literal = (Literal)obj;
			if (literal.val != null)
			{
				if (!literal.val.Equals(this.val))
				{
					return false;
				}
			}
			else if (this.val != null)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0005EED8 File Offset: 0x0005D0D8
		public override int GetHashCode()
		{
			return this.val.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0005EEEC File Offset: 0x0005D0EC
		public override object Eval(DataRow row)
		{
			return this.val;
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0005EEF4 File Offset: 0x0005D0F4
		public override bool DependsOn(DataColumn other)
		{
			return false;
		}

		// Token: 0x04000881 RID: 2177
		private object val;
	}
}
