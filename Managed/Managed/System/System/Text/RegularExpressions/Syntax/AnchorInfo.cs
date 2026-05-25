using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x020004A9 RID: 1193
	internal class AnchorInfo
	{
		// Token: 0x06002AD3 RID: 10963 RVA: 0x00093644 File Offset: 0x00091844
		public AnchorInfo(Expression expr, int width)
		{
			this.expr = expr;
			this.offset = 0;
			this.width = width;
			this.str = null;
			this.ignore = false;
			this.pos = Position.Any;
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x00093684 File Offset: 0x00091884
		public AnchorInfo(Expression expr, int offset, int width, string str, bool ignore)
		{
			this.expr = expr;
			this.offset = offset;
			this.width = width;
			this.str = ((!ignore) ? str : str.ToLower());
			this.ignore = ignore;
			this.pos = Position.Any;
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000936D8 File Offset: 0x000918D8
		public AnchorInfo(Expression expr, int offset, int width, Position pos)
		{
			this.expr = expr;
			this.offset = offset;
			this.width = width;
			this.pos = pos;
			this.str = null;
			this.ignore = false;
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x0009370C File Offset: 0x0009190C
		public Expression Expression
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x00093714 File Offset: 0x00091914
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002AD8 RID: 10968 RVA: 0x0009371C File Offset: 0x0009191C
		public int Width
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x00093724 File Offset: 0x00091924
		public int Length
		{
			get
			{
				return (this.str == null) ? 0 : this.str.Length;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x00093744 File Offset: 0x00091944
		public bool IsUnknownWidth
		{
			get
			{
				return this.width < 0;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x00093750 File Offset: 0x00091950
		public bool IsComplete
		{
			get
			{
				return this.Length == this.Width;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06002ADC RID: 10972 RVA: 0x00093760 File Offset: 0x00091960
		public string Substring
		{
			get
			{
				return this.str;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x00093768 File Offset: 0x00091968
		public bool IgnoreCase
		{
			get
			{
				return this.ignore;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x00093770 File Offset: 0x00091970
		public Position Position
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06002ADF RID: 10975 RVA: 0x00093778 File Offset: 0x00091978
		public bool IsSubstring
		{
			get
			{
				return this.str != null;
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06002AE0 RID: 10976 RVA: 0x00093788 File Offset: 0x00091988
		public bool IsPosition
		{
			get
			{
				return this.pos != Position.Any;
			}
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x00093798 File Offset: 0x00091998
		public Interval GetInterval()
		{
			return this.GetInterval(0);
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000937A4 File Offset: 0x000919A4
		public Interval GetInterval(int start)
		{
			if (!this.IsSubstring)
			{
				return Interval.Empty;
			}
			return new Interval(start + this.Offset, start + this.Offset + this.Length - 1);
		}

		// Token: 0x04001B17 RID: 6935
		private Expression expr;

		// Token: 0x04001B18 RID: 6936
		private Position pos;

		// Token: 0x04001B19 RID: 6937
		private int offset;

		// Token: 0x04001B1A RID: 6938
		private string str;

		// Token: 0x04001B1B RID: 6939
		private int width;

		// Token: 0x04001B1C RID: 6940
		private bool ignore;
	}
}
