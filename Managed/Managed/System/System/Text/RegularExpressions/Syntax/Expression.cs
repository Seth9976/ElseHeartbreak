using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x02000498 RID: 1176
	internal abstract class Expression
	{
		// Token: 0x06002A5F RID: 10847
		public abstract void Compile(ICompiler cmp, bool reverse);

		// Token: 0x06002A60 RID: 10848
		public abstract void GetWidth(out int min, out int max);

		// Token: 0x06002A61 RID: 10849 RVA: 0x00092030 File Offset: 0x00090230
		public int GetFixedWidth()
		{
			int num;
			int num2;
			this.GetWidth(out num, out num2);
			if (num == num2)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x00092054 File Offset: 0x00090254
		public virtual AnchorInfo GetAnchorInfo(bool reverse)
		{
			return new AnchorInfo(this, this.GetFixedWidth());
		}

		// Token: 0x06002A63 RID: 10851
		public abstract bool IsComplex();
	}
}
