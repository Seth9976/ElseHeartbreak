using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x020004A0 RID: 1184
	internal abstract class Assertion : CompositeExpression
	{
		// Token: 0x06002A8F RID: 10895 RVA: 0x000929FC File Offset: 0x00090BFC
		public Assertion()
		{
			base.Expressions.Add(null);
			base.Expressions.Add(null);
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x00092A28 File Offset: 0x00090C28
		// (set) Token: 0x06002A91 RID: 10897 RVA: 0x00092A38 File Offset: 0x00090C38
		public Expression TrueExpression
		{
			get
			{
				return base.Expressions[0];
			}
			set
			{
				base.Expressions[0] = value;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x00092A48 File Offset: 0x00090C48
		// (set) Token: 0x06002A93 RID: 10899 RVA: 0x00092A58 File Offset: 0x00090C58
		public Expression FalseExpression
		{
			get
			{
				return base.Expressions[1];
			}
			set
			{
				base.Expressions[1] = value;
			}
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x00092A68 File Offset: 0x00090C68
		public override void GetWidth(out int min, out int max)
		{
			base.GetWidth(out min, out max, 2);
			if (this.TrueExpression == null || this.FalseExpression == null)
			{
				min = 0;
			}
		}
	}
}
