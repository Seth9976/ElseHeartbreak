using System;

namespace UnityEngine
{
	// Token: 0x02000139 RID: 313
	public interface ISerializationCallbackReceiver
	{
		// Token: 0x06000D25 RID: 3365
		void OnBeforeSerialize();

		// Token: 0x06000D26 RID: 3366
		void OnAfterDeserialize();
	}
}
