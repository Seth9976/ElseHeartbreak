using System;

namespace TingTing
{
	// Token: 0x02000012 RID: 18
	public class WorldCoordinateException : Exception
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00004FE0 File Offset: 0x000031E0
		public WorldCoordinateException(string pMessage, Exception pInnerException)
			: base(pMessage, pInnerException)
		{
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004FEC File Offset: 0x000031EC
		public WorldCoordinateException(string pMessage)
			: base(pMessage)
		{
		}
	}
}
