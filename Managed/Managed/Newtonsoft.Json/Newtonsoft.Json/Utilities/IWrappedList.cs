using System;
using System.Collections;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000BB RID: 187
	internal interface IWrappedList : IList, ICollection, IEnumerable
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000866 RID: 2150
		object UnderlyingList { get; }
	}
}
