using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x020004A6 RID: 1190
	internal class Reference : Expression
	{
		// Token: 0x06002ABA RID: 10938 RVA: 0x00092FB0 File Offset: 0x000911B0
		public Reference(bool ignore)
		{
			this.ignore = ignore;
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06002ABB RID: 10939 RVA: 0x00092FC0 File Offset: 0x000911C0
		// (set) Token: 0x06002ABC RID: 10940 RVA: 0x00092FC8 File Offset: 0x000911C8
		public CapturingGroup CapturingGroup
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x00092FD4 File Offset: 0x000911D4
		// (set) Token: 0x06002ABE RID: 10942 RVA: 0x00092FDC File Offset: 0x000911DC
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

		// Token: 0x06002ABF RID: 10943 RVA: 0x00092FE8 File Offset: 0x000911E8
		public override void Compile(ICompiler cmp, bool reverse)
		{
			cmp.EmitReference(this.group.Index, this.ignore, reverse);
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x00093004 File Offset: 0x00091204
		public override void GetWidth(out int min, out int max)
		{
			min = 0;
			max = int.MaxValue;
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x00093010 File Offset: 0x00091210
		public override bool IsComplex()
		{
			return true;
		}

		// Token: 0x04001B0C RID: 6924
		private CapturingGroup group;

		// Token: 0x04001B0D RID: 6925
		private bool ignore;
	}
}
