using System;

namespace GameTypes
{
	// Token: 0x02000003 RID: 3
	public static class BitCruncher
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
		public static int PackTwoShorts(int pA, int pB)
		{
			if (pA > 32767 || pA < -32768 || pB > 32767 || pB < -32768)
			{
				throw new BitCruncherException(string.Concat(new object[] { "parameters must fit inside 16bit x:", pA, " y:", pB }));
			}
			int num = (int)((ushort)pA);
			int num2 = (int)((ushort)pB);
			return num | (num2 << 16);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002170 File Offset: 0x00000370
		public static void UnpackTwoShorts(int packed, out int aResult, out int bResult)
		{
			aResult = (int)((short)(packed & 65535));
			bResult = (int)((short)((packed >> 16) & 65535));
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000218C File Offset: 0x0000038C
		public static long PackTwoInts(int pA, int pB)
		{
			long num = (long)((ulong)pA);
			long num2 = (long)((ulong)pB);
			return num | (num2 << 32);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021A8 File Offset: 0x000003A8
		public static void UnpackTwoInts(long packed, out int aResult, out int bResult)
		{
			aResult = (int)(packed & (long)((ulong)(-1)));
			bResult = (int)((packed >> 32) & (long)((ulong)(-1)));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021BC File Offset: 0x000003BC
		public static long PackFourShorts(int pA, int pB, int pC, int pD)
		{
			return BitCruncher.PackTwoInts(BitCruncher.PackTwoShorts(pA, pB), BitCruncher.PackTwoShorts(pC, pD));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021D4 File Offset: 0x000003D4
		public static void UnpackFourShorts(long pSource, out int pA, out int pB, out int pC, out int pD)
		{
			int num;
			int num2;
			BitCruncher.UnpackTwoInts(pSource, out num, out num2);
			BitCruncher.UnpackTwoShorts(num, out pA, out pB);
			BitCruncher.UnpackTwoShorts(num2, out pC, out pD);
		}
	}
}
