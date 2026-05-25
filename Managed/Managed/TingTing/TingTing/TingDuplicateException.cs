using System;

namespace TingTing
{
	// Token: 0x0200000A RID: 10
	public class TingDuplicateException : Exception
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public TingDuplicateException(string pMessage, Exception pInnerException)
			: base(pMessage, pInnerException)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public TingDuplicateException(string pMessage)
			: base(pMessage)
		{
		}
	}
}
