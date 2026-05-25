using System;

namespace System.Runtime.Serialization
{
	/// <summary>Stores data from a versioned data contract that has been extended by adding new members. </summary>
	// Token: 0x02000019 RID: 25
	public sealed class ExtensionDataObject
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002AA0 File Offset: 0x00000CA0
		internal ExtensionDataObject(object target)
		{
			this.target = target;
		}

		// Token: 0x0400003E RID: 62
		private object target;
	}
}
