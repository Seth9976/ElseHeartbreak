using System;
using System.Runtime.Serialization;

namespace Boo.Lang.Runtime
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class AssertionFailedException : RuntimeException
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x00004194 File Offset: 0x00002394
		public AssertionFailedException(string message)
			: base(message)
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000041A0 File Offset: 0x000023A0
		protected AssertionFailedException(SerializationInfo si, StreamingContext sc)
			: base(si, sc)
		{
		}
	}
}
