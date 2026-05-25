using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x020004A4 RID: 1188
	internal class Literal : Expression
	{
		// Token: 0x06002AA9 RID: 10921 RVA: 0x00092E3C File Offset: 0x0009103C
		public Literal(string str, bool ignore)
		{
			this.str = str;
			this.ignore = ignore;
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002AAA RID: 10922 RVA: 0x00092E54 File Offset: 0x00091054
		// (set) Token: 0x06002AAB RID: 10923 RVA: 0x00092E5C File Offset: 0x0009105C
		public string String
		{
			get
			{
				return this.str;
			}
			set
			{
				this.str = value;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06002AAC RID: 10924 RVA: 0x00092E68 File Offset: 0x00091068
		// (set) Token: 0x06002AAD RID: 10925 RVA: 0x00092E70 File Offset: 0x00091070
		public bool IgnoreCase
		{
			get
			{
				return this.ignore;
			}
			set
			{
				this.ignore = value;
			}
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x00092E7C File Offset: 0x0009107C
		public static void CompileLiteral(string str, ICompiler cmp, bool ignore, bool reverse)
		{
			if (str.Length == 0)
			{
				return;
			}
			if (str.Length == 1)
			{
				cmp.EmitCharacter(str[0], false, ignore, reverse);
			}
			else
			{
				cmp.EmitString(str, ignore, reverse);
			}
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x00092EC0 File Offset: 0x000910C0
		public override void Compile(ICompiler cmp, bool reverse)
		{
			Literal.CompileLiteral(this.str, cmp, this.ignore, reverse);
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x00092ED8 File Offset: 0x000910D8
		public override void GetWidth(out int min, out int max)
		{
			min = (max = this.str.Length);
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x00092EF8 File Offset: 0x000910F8
		public override AnchorInfo GetAnchorInfo(bool reverse)
		{
			return new AnchorInfo(this, 0, this.str.Length, this.str, this.ignore);
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x00092F18 File Offset: 0x00091118
		public override bool IsComplex()
		{
			return false;
		}

		// Token: 0x04001B09 RID: 6921
		private string str;

		// Token: 0x04001B0A RID: 6922
		private bool ignore;
	}
}
