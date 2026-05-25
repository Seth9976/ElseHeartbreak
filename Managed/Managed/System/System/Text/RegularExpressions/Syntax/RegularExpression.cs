using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x0200049B RID: 1179
	internal class RegularExpression : Group
	{
		// Token: 0x06002A6F RID: 10863 RVA: 0x00092578 File Offset: 0x00090778
		public RegularExpression()
		{
			this.group_count = 0;
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06002A70 RID: 10864 RVA: 0x00092588 File Offset: 0x00090788
		// (set) Token: 0x06002A71 RID: 10865 RVA: 0x00092590 File Offset: 0x00090790
		public int GroupCount
		{
			get
			{
				return this.group_count;
			}
			set
			{
				this.group_count = value;
			}
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x0009259C File Offset: 0x0009079C
		public override void Compile(ICompiler cmp, bool reverse)
		{
			int num;
			int num2;
			this.GetWidth(out num, out num2);
			cmp.EmitInfo(this.group_count, num, num2);
			AnchorInfo anchorInfo = this.GetAnchorInfo(reverse);
			LinkRef linkRef = cmp.NewLink();
			cmp.EmitAnchor(reverse, anchorInfo.Offset, linkRef);
			if (anchorInfo.IsPosition)
			{
				cmp.EmitPosition(anchorInfo.Position);
			}
			else if (anchorInfo.IsSubstring)
			{
				cmp.EmitString(anchorInfo.Substring, anchorInfo.IgnoreCase, reverse);
			}
			cmp.EmitTrue();
			cmp.ResolveLink(linkRef);
			base.Compile(cmp, reverse);
			cmp.EmitTrue();
		}

		// Token: 0x04001AFD RID: 6909
		private int group_count;
	}
}
