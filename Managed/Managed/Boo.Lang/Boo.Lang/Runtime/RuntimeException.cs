using System;
using System.Runtime.Serialization;

namespace Boo.Lang.Runtime
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public class RuntimeException : Exception
	{
		// Token: 0x060001EB RID: 491 RVA: 0x00006600 File Offset: 0x00004800
		public RuntimeException(string message)
			: base(message)
		{
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000660C File Offset: 0x0000480C
		protected RuntimeException(SerializationInfo si, StreamingContext sc)
			: base(si, sc)
		{
		}
	}
}
