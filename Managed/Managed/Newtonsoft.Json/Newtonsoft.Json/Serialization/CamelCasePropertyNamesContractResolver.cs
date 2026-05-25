using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007B RID: 123
	public class CamelCasePropertyNamesContractResolver : DefaultContractResolver
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x00015093 File Offset: 0x00013293
		public CamelCasePropertyNamesContractResolver()
			: base(true)
		{
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001509C File Offset: 0x0001329C
		protected internal override string ResolvePropertyName(string propertyName)
		{
			return StringUtils.ToCamelCase(propertyName);
		}
	}
}
