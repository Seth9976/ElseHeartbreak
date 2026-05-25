using System;

namespace GameTypes
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public struct GameTime
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000021FC File Offset: 0x000003FC
		public GameTime(GameTime pGameTime)
		{
			this.days = pGameTime.days;
			this.hours = pGameTime.hours;
			this.minutes = pGameTime.hours;
			this.seconds = pGameTime.seconds;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002240 File Offset: 0x00000440
		public GameTime(float pTotalSeconds)
		{
			this.days = (int)(pTotalSeconds / 86400f);
			float num = pTotalSeconds - (float)(this.days * 86400);
			this.hours = (int)(num / 3600f);
			num -= (float)this.hours * 3600f;
			this.minutes = (int)(num / 60f);
			num -= (float)this.minutes * 60f;
			this.seconds = num;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022B0 File Offset: 0x000004B0
		public GameTime(int pHours, int pMinutes)
		{
			this.days = 0;
			this.hours = pHours;
			this.minutes = pMinutes;
			this.seconds = 0f;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022E0 File Offset: 0x000004E0
		public GameTime(int pDays, int pHours, int pMinutes, float pSeconds)
		{
			this.days = pDays;
			this.hours = pHours;
			this.minutes = pMinutes;
			this.seconds = pSeconds;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002300 File Offset: 0x00000500
		public void Tick(float pDeltaTime)
		{
			this.seconds += pDeltaTime;
			while (this.seconds >= 60f)
			{
				this.seconds -= 60f;
				this.minutes++;
			}
			while (this.minutes >= 60)
			{
				this.minutes -= 60;
				this.hours++;
			}
			while (this.hours >= 24)
			{
				this.hours -= 24;
				this.days++;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023B0 File Offset: 0x000005B0
		public void TurnForwardToTime(int pHour, int pMinute)
		{
			string text = this.ToString();
			if (pHour <= this.hours)
			{
				this.days++;
			}
			if (pMinute <= this.minutes)
			{
				this.hours++;
			}
			this.hours = pHour;
			this.minutes = pMinute;
			D.Log("Turning forward time from " + text + " to " + this.ToString());
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002430 File Offset: 0x00000630
		public override string ToString()
		{
			string text = this.hours.ToString();
			if (text.Length == 1)
			{
				text = "0" + text;
			}
			string text2 = this.minutes.ToString();
			if (text2.Length == 1)
			{
				text2 = "0" + text2;
			}
			string text3 = Math.Floor((double)this.seconds).ToString();
			if (text3.Length == 1)
			{
				text3 = "0" + text3;
			}
			return string.Format("Day {0}, {1}:{2}:{3}", new object[] { this.days, text, text2, text3 });
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024DC File Offset: 0x000006DC
		public string ToStringWithoutDayAndSeconds()
		{
			string text = this.hours.ToString();
			if (text.Length == 1)
			{
				text = "0" + text;
			}
			string text2 = this.minutes.ToString();
			if (text2.Length == 1)
			{
				text2 = "0" + text2;
			}
			return string.Format("{0}:{1}", text, text2);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002540 File Offset: 0x00000740
		public float normalizedDayTime
		{
			get
			{
				float num = (float)((int)this.seconds + this.minutes * 60 + this.hours * 3600);
				return num / 86400f;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002578 File Offset: 0x00000778
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000025A8 File Offset: 0x000007A8
		public float totalSeconds
		{
			get
			{
				return this.seconds + (float)(this.minutes * 60 + this.hours * 3600 + this.days * 86400);
			}
			set
			{
				this.days = (int)(value / 86400f);
				float num = value - (float)(86400 * this.days);
				this.hours = (int)(num / 3600f);
				num -= (float)(3600 * this.hours);
				this.minutes = (int)(num / 60f);
				num -= (float)(60 * this.minutes);
				this.seconds = num;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002618 File Offset: 0x00000818
		public GameTime Now()
		{
			return new GameTime
			{
				days = this.days,
				hours = this.hours,
				minutes = this.minutes,
				seconds = this.seconds
			};
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002664 File Offset: 0x00000864
		public override bool Equals(object obj)
		{
			return obj is GameTime && (GameTime)obj == this;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002684 File Offset: 0x00000884
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002698 File Offset: 0x00000898
		public bool isDaytime
		{
			get
			{
				return this.hours >= 6 && this.hours < 18;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000026B4 File Offset: 0x000008B4
		public bool IsWithinMinuteBounds(GameTime startTime, GameTime endTime)
		{
			GameTime gameTime = new GameTime(startTime.hours, startTime.minutes);
			GameTime gameTime2 = new GameTime(endTime.hours, endTime.minutes);
			GameTime gameTime3 = new GameTime(this.hours, this.minutes);
			if (gameTime < gameTime2)
			{
				return gameTime.totalSeconds <= gameTime3.totalSeconds && gameTime3.totalSeconds < gameTime2.totalSeconds;
			}
			bool flag = gameTime3.totalSeconds <= gameTime2.totalSeconds;
			bool flag2 = gameTime3.totalSeconds >= gameTime.totalSeconds;
			return flag || flag2;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002768 File Offset: 0x00000968
		public static GameTime operator +(GameTime g1, GameTime g2)
		{
			GameTime gameTime = default(GameTime);
			float num = g1.totalSeconds + g2.totalSeconds;
			gameTime.totalSeconds = num;
			return gameTime;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002798 File Offset: 0x00000998
		public static GameTime operator -(GameTime g1, GameTime g2)
		{
			GameTime gameTime = default(GameTime);
			float num = g1.totalSeconds - g2.totalSeconds;
			gameTime.totalSeconds = num;
			return gameTime;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000027C8 File Offset: 0x000009C8
		public static bool operator >(GameTime g1, GameTime g2)
		{
			return g1.totalSeconds > g2.totalSeconds;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000027DC File Offset: 0x000009DC
		public static bool operator >=(GameTime g1, GameTime g2)
		{
			return g1.totalSeconds >= g2.totalSeconds;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027F4 File Offset: 0x000009F4
		public static bool operator <(GameTime g1, GameTime g2)
		{
			return g1.totalSeconds < g2.totalSeconds;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002808 File Offset: 0x00000A08
		public static bool operator <=(GameTime g1, GameTime g2)
		{
			return g1.totalSeconds <= g2.totalSeconds;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002820 File Offset: 0x00000A20
		public static bool operator ==(GameTime g1, GameTime g2)
		{
			return (int)g1.totalSeconds == (int)g2.totalSeconds;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002834 File Offset: 0x00000A34
		public static bool operator !=(GameTime g1, GameTime g2)
		{
			return !(g1 == g2);
		}

		// Token: 0x04000001 RID: 1
		private const int SECONDS_PER_MINUTE = 60;

		// Token: 0x04000002 RID: 2
		private const int SECONDS_PER_HOUR = 3600;

		// Token: 0x04000003 RID: 3
		private const int SECONDS_PER_DAY = 86400;

		// Token: 0x04000004 RID: 4
		public int days;

		// Token: 0x04000005 RID: 5
		public int hours;

		// Token: 0x04000006 RID: 6
		public int minutes;

		// Token: 0x04000007 RID: 7
		public float seconds;
	}
}
