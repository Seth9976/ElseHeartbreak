using System;

namespace Mono.Data.Tds
{
	// Token: 0x02000004 RID: 4
	internal static class TdsCollation
	{
		// Token: 0x06000004 RID: 4 RVA: 0x00002104 File Offset: 0x00000304
		public static int LCID(byte[] collation)
		{
			if (collation == null)
			{
				return -1;
			}
			return (int)collation[0] | ((int)collation[1] << 8) | ((int)(collation[2] & 15) << 16);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002124 File Offset: 0x00000324
		public static int CollationFlags(byte[] collation)
		{
			if (collation == null)
			{
				return -1;
			}
			return (int)(collation[2] & 240) | ((int)(collation[3] & 15) << 4);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002140 File Offset: 0x00000340
		public static int Version(byte[] collation)
		{
			if (collation == null)
			{
				return -1;
			}
			return (int)(collation[3] & 240);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002154 File Offset: 0x00000354
		public static int SortId(byte[] collation)
		{
			if (collation == null)
			{
				return -1;
			}
			return (int)collation[4];
		}
	}
}
