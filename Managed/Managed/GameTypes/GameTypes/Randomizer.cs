using System;
using System.Collections.Generic;

namespace GameTypes
{
	// Token: 0x0200000E RID: 14
	public class Randomizer
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000030F0 File Offset: 0x000012F0
		public static float GetValue(float pMin, float pMax)
		{
			if (pMin > pMax)
			{
				throw new ArgumentException("pMin > pMax");
			}
			if (Randomizer._random == null)
			{
				int num = DateTime.Now.Millisecond * DateTime.Now.Second * DateTime.Now.Minute;
				Randomizer._random = new Random(num);
			}
			return pMin + (pMax - pMin) * (float)Randomizer._random.NextDouble();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003160 File Offset: 0x00001360
		public static int GetIntValue(int pMin, int pMax)
		{
			if (pMin > pMax)
			{
				throw new ArgumentException("pMin > pMax");
			}
			if (Randomizer._random == null)
			{
				int num = DateTime.Now.Millisecond * DateTime.Now.Second * DateTime.Now.Minute;
				Randomizer._random = new Random(num);
			}
			return Randomizer._random.Next(pMin, pMax);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000031CC File Offset: 0x000013CC
		public static bool OneIn(int pX)
		{
			return Randomizer.GetIntValue(0, pX) == 0;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000031D8 File Offset: 0x000013D8
		public static T RandNth<T>(IList<T> pList)
		{
			return pList[Randomizer.GetIntValue(0, pList.Count)];
		}

		// Token: 0x0400002C RID: 44
		private static Random _random;
	}
}
