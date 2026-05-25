using System;

namespace GameTypes
{
	// Token: 0x02000005 RID: 5
	public class GridMath
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002848 File Offset: 0x00000A48
		public static Direction RadiansToDirection(float pRadians)
		{
			int num = (int)Math.Round((double)(pRadians * 1.2732395f), 0);
			return GridMath.DirectionIndexToDirection(num & 7);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002870 File Offset: 0x00000A70
		public static Direction DegreesToDirection(int pDegrees)
		{
			return GridMath.RadiansToDirection((float)pDegrees * 0.017453292f);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002880 File Offset: 0x00000A80
		internal static Direction DirectionIndexToDirection(int direction)
		{
			switch (direction)
			{
			case 0:
				return Direction.UP;
			case 1:
				return Direction.UP_RIGHT;
			case 2:
				return Direction.RIGHT;
			case 3:
				return Direction.DOWN_RIGHT;
			case 4:
				return Direction.DOWN;
			case 5:
				return Direction.DOWN_LEFT;
			case 6:
				return Direction.LEFT;
			case 7:
				return Direction.UP_LEFT;
			default:
				D.isNull(null, "direction error: " + direction);
				return Direction.ZERO;
			}
		}

		// Token: 0x04000008 RID: 8
		public const float TWO_PI = 6.2831855f;

		// Token: 0x04000009 RID: 9
		public const float TWO_PI_INVERTED = 0.15915494f;

		// Token: 0x0400000A RID: 10
		public const float EIGHTH = 1.2732395f;

		// Token: 0x0400000B RID: 11
		public const float DEGREES_TO_RADIANS = 0.017453292f;

		// Token: 0x0400000C RID: 12
		public const float RADIANS_TO_DEGREES = 57.295776f;
	}
}
