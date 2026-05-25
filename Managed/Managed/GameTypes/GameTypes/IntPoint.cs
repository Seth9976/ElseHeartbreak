using System;

namespace GameTypes
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public struct IntPoint : IPoint
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000028E8 File Offset: 0x00000AE8
		public IntPoint(int pX, int pY)
		{
			this.x = pX;
			this.y = pY;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002980 File Offset: 0x00000B80
		public float EuclidianDistanceTo(IntPoint pOtherPoint)
		{
			IntPoint intPoint = pOtherPoint - this;
			return (float)Math.Sqrt((double)(intPoint.x * intPoint.x + intPoint.y * intPoint.y));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029C0 File Offset: 0x00000BC0
		public int ManhattanDistanceTo(IntPoint pOtherPoint)
		{
			IntPoint intPoint = pOtherPoint - this;
			intPoint.x = ((intPoint.x >= 0) ? intPoint.x : (-intPoint.x));
			intPoint.y = ((intPoint.y >= 0) ? intPoint.y : (-intPoint.y));
			return intPoint.x + intPoint.y;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A3C File Offset: 0x00000C3C
		public static IntPoint DirectionToIntPoint(Direction direction)
		{
			switch (direction)
			{
			case Direction.RIGHT:
				return IntPoint.Right;
			case Direction.UP_RIGHT:
				return IntPoint.UpRight;
			default:
				if (direction == Direction.LEFT)
				{
					return IntPoint.Left;
				}
				if (direction == Direction.DOWN_LEFT)
				{
					return IntPoint.DownLeft;
				}
				if (direction == Direction.DOWN)
				{
					return IntPoint.Down;
				}
				if (direction != Direction.DOWN_RIGHT)
				{
					return IntPoint.Zero;
				}
				return IntPoint.DownRight;
			case Direction.UP:
				return IntPoint.Up;
			case Direction.UP_LEFT:
				return IntPoint.UpLeft;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public static Direction Turn(Direction pDirection, int pDegrees)
		{
			int num = (int)IntPoint.DirectionToIntPoint(pDirection).Degrees();
			num += pDegrees;
			return GridMath.DegreesToDirection(num);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public Direction ToDirection()
		{
			if (this.Clamped() == IntPoint.Up)
			{
				return Direction.UP;
			}
			if (this.Clamped() == IntPoint.UpLeft)
			{
				return Direction.UP_LEFT;
			}
			if (this.Clamped() == IntPoint.UpRight)
			{
				return Direction.UP_RIGHT;
			}
			if (this.Clamped() == IntPoint.Right)
			{
				return Direction.RIGHT;
			}
			if (this.Clamped() == IntPoint.Left)
			{
				return Direction.LEFT;
			}
			if (this.Clamped() == IntPoint.Down)
			{
				return Direction.DOWN;
			}
			if (this.Clamped() == IntPoint.DownLeft)
			{
				return Direction.DOWN_LEFT;
			}
			if (this.Clamped() == IntPoint.DownRight)
			{
				return Direction.DOWN_RIGHT;
			}
			return Direction.ZERO;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public IntPoint Clamped()
		{
			int num = 0;
			if (this.x >= 1)
			{
				num = 1;
			}
			if (this.x <= -1)
			{
				num = -1;
			}
			int num2 = 0;
			if (this.y >= 1)
			{
				num2 = 1;
			}
			if (this.y <= -1)
			{
				num2 = -1;
			}
			return new IntPoint(num, num2);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C18 File Offset: 0x00000E18
		public float Degrees()
		{
			float num = (float)(Math.Atan2((double)(-(double)this.y), (double)this.x) * 57.2957763671875) + 90f;
			if (num < 0f)
			{
				num += 360f;
			}
			return num;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002C60 File Offset: 0x00000E60
		public IntPoint RotatedWithDegrees(float pDegrees)
		{
			float num = -pDegrees;
			float num2 = 0.017453292f * num;
			float num3 = (float)Math.Atan2((double)this.y, (double)this.x);
			float num4 = num3 + num2;
			return new IntPoint((int)Math.Round(Math.Cos((double)num4)), (int)Math.Round(Math.Sin((double)num4)));
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public static IntPoint Max
		{
			get
			{
				return new IntPoint(int.MaxValue, int.MaxValue);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002CC4 File Offset: 0x00000EC4
		public static IntPoint Min
		{
			get
			{
				return new IntPoint(int.MinValue, int.MinValue);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public override bool Equals(object obj)
		{
			return obj is IntPoint && (IntPoint)obj == this;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public override int GetHashCode()
		{
			return BitCruncher.PackTwoShorts(this.x, this.y);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002D0C File Offset: 0x00000F0C
		public override string ToString()
		{
			return string.Concat(new object[] { "(", this.x, ",", this.y, ")" });
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002D58 File Offset: 0x00000F58
		public IntPoint scale(float amount)
		{
			return new IntPoint((int)((float)this.x * amount), (int)((float)this.y * amount));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D74 File Offset: 0x00000F74
		public float DistanceTo(IPoint pPoint)
		{
			D.assert(pPoint is IntPoint, "Must a point of the same type!");
			return this.EuclidianDistanceTo((IntPoint)pPoint);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002D98 File Offset: 0x00000F98
		public void Set(int pX, int pY)
		{
			this.x = pX;
			this.y = pY;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public static IntPoint operator -(IntPoint pFirst, IntPoint pSecond)
		{
			return new IntPoint(pFirst.x - pSecond.x, pFirst.y - pSecond.y);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public static IntPoint operator +(IntPoint pFirst, IntPoint pSecond)
		{
			return new IntPoint(pFirst.x + pSecond.x, pFirst.y + pSecond.y);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public static IntPoint operator *(int pFirst, IntPoint pSecond)
		{
			return pSecond * pFirst;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E04 File Offset: 0x00001004
		public static IntPoint operator *(IntPoint pFirst, int pSecond)
		{
			return new IntPoint(pFirst.x * pSecond, pFirst.y * pSecond);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002E20 File Offset: 0x00001020
		public static IntPoint operator /(IntPoint pFirst, int pSecond)
		{
			return new IntPoint(pFirst.x / pSecond, pFirst.y / pSecond);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E3C File Offset: 0x0000103C
		public static bool operator ==(IntPoint pFirst, IntPoint pSecond)
		{
			return pFirst.x == pSecond.x && pFirst.y == pSecond.y;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002E70 File Offset: 0x00001070
		public static bool operator !=(IntPoint pFirst, IntPoint pSecond)
		{
			return !(pFirst == pSecond);
		}

		// Token: 0x04000017 RID: 23
		public int x;

		// Token: 0x04000018 RID: 24
		public int y;

		// Token: 0x04000019 RID: 25
		public static readonly IntPoint Zero = new IntPoint(0, 0);

		// Token: 0x0400001A RID: 26
		public static readonly IntPoint Up = new IntPoint(0, 1);

		// Token: 0x0400001B RID: 27
		public static readonly IntPoint Right = new IntPoint(1, 0);

		// Token: 0x0400001C RID: 28
		public static readonly IntPoint Left = new IntPoint(-1, 0);

		// Token: 0x0400001D RID: 29
		public static readonly IntPoint Down = new IntPoint(0, -1);

		// Token: 0x0400001E RID: 30
		public static readonly IntPoint UpRight = new IntPoint(1, 1);

		// Token: 0x0400001F RID: 31
		public static readonly IntPoint UpLeft = new IntPoint(-1, 1);

		// Token: 0x04000020 RID: 32
		public static readonly IntPoint DownRight = new IntPoint(1, -1);

		// Token: 0x04000021 RID: 33
		public static readonly IntPoint DownLeft = new IntPoint(-1, -1);

		// Token: 0x04000022 RID: 34
		public static readonly IntPoint One = new IntPoint(1, 1);
	}
}
