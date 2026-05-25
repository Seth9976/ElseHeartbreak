using System;
using System.Collections.ObjectModel;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000B4 RID: 180
	internal class EnumValues<T> : KeyedCollection<string, EnumValue<T>> where T : struct
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x0001DA12 File Offset: 0x0001BC12
		protected override string GetKeyForItem(EnumValue<T> item)
		{
			return item.Name;
		}
	}
}
