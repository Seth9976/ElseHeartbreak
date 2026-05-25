using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000031 RID: 49
	public interface IValueProvider
	{
		// Token: 0x0600021A RID: 538
		void SetValue(object target, object value);

		// Token: 0x0600021B RID: 539
		object GetValue(object target);
	}
}
