using System;

namespace Boo.Lang
{
	// Token: 0x0200001D RID: 29
	public interface IQuackFu
	{
		// Token: 0x060000C3 RID: 195
		object QuackGet(string name, object[] parameters);

		// Token: 0x060000C4 RID: 196
		object QuackSet(string name, object[] parameters, object value);

		// Token: 0x060000C5 RID: 197
		object QuackInvoke(string name, params object[] args);
	}
}
