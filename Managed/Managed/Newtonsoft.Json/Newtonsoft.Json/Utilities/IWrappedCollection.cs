using System;
using System.Collections;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000AC RID: 172
	internal interface IWrappedCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060007D6 RID: 2006
		object UnderlyingCollection { get; }
	}
}
