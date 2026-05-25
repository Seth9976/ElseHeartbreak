using System;
using System.Collections;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000AF RID: 175
	internal interface IWrappedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060007F4 RID: 2036
		object UnderlyingDictionary { get; }
	}
}
