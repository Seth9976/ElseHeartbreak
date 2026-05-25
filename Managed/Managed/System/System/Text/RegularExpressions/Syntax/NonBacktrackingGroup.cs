using System;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x0200049E RID: 1182
	internal class NonBacktrackingGroup : Group
	{
		// Token: 0x06002A81 RID: 10881 RVA: 0x00092788 File Offset: 0x00090988
		public override void Compile(ICompiler cmp, bool reverse)
		{
			LinkRef linkRef = cmp.NewLink();
			cmp.EmitSub(linkRef);
			base.Compile(cmp, reverse);
			cmp.EmitTrue();
			cmp.ResolveLink(linkRef);
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000927B8 File Offset: 0x000909B8
		public override bool IsComplex()
		{
			return true;
		}
	}
}
