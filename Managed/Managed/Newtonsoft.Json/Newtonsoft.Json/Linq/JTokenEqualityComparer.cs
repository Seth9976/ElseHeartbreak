using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200003F RID: 63
	public class JTokenEqualityComparer : IEqualityComparer<JToken>
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0000A488 File Offset: 0x00008688
		public bool Equals(JToken x, JToken y)
		{
			return JToken.DeepEquals(x, y);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000A491 File Offset: 0x00008691
		public int GetHashCode(JToken obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetDeepHashCode();
		}
	}
}
