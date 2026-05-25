using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x0200049C RID: 1180
	internal class CapturingGroup : Group, IComparable
	{
		// Token: 0x06002A73 RID: 10867 RVA: 0x00092634 File Offset: 0x00090834
		public CapturingGroup()
		{
			this.gid = 0;
			this.name = null;
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06002A74 RID: 10868 RVA: 0x0009264C File Offset: 0x0009084C
		// (set) Token: 0x06002A75 RID: 10869 RVA: 0x00092654 File Offset: 0x00090854
		public int Index
		{
			get
			{
				return this.gid;
			}
			set
			{
				this.gid = value;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06002A76 RID: 10870 RVA: 0x00092660 File Offset: 0x00090860
		// (set) Token: 0x06002A77 RID: 10871 RVA: 0x00092668 File Offset: 0x00090868
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06002A78 RID: 10872 RVA: 0x00092674 File Offset: 0x00090874
		public bool IsNamed
		{
			get
			{
				return this.name != null;
			}
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x00092684 File Offset: 0x00090884
		public override void Compile(ICompiler cmp, bool reverse)
		{
			cmp.EmitOpen(this.gid);
			base.Compile(cmp, reverse);
			cmp.EmitClose(this.gid);
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000926B4 File Offset: 0x000908B4
		public override bool IsComplex()
		{
			return true;
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000926B8 File Offset: 0x000908B8
		public int CompareTo(object other)
		{
			return this.gid - ((CapturingGroup)other).gid;
		}

		// Token: 0x04001AFE RID: 6910
		private int gid;

		// Token: 0x04001AFF RID: 6911
		private string name;
	}
}
