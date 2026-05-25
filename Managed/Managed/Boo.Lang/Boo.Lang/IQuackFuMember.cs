using System;

namespace Boo.Lang
{
	// Token: 0x0200001F RID: 31
	public interface IQuackFuMember
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000C6 RID: 198
		string Name { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000C7 RID: 199
		QuackFuMemberKind Kind { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000C8 RID: 200
		Type ReturnType { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C9 RID: 201
		string[] ArgumentNames { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000CA RID: 202
		Type[] ArgumentTypes { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000CB RID: 203
		string Info { get; }
	}
}
