using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000026 RID: 38
	public interface IJEnumerable<T> : IEnumerable<T>, IEnumerable where T : JToken
	{
		// Token: 0x1700002B RID: 43
		IJEnumerable<JToken> this[object key] { get; }
	}
}
