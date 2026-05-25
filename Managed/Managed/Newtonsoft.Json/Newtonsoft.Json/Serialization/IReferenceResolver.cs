using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000034 RID: 52
	public interface IReferenceResolver
	{
		// Token: 0x06000224 RID: 548
		object ResolveReference(object context, string reference);

		// Token: 0x06000225 RID: 549
		string GetReference(object context, object value);

		// Token: 0x06000226 RID: 550
		bool IsReferenced(object context, object value);

		// Token: 0x06000227 RID: 551
		void AddReference(object context, string reference, object value);
	}
}
