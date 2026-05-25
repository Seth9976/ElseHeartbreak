using System;

namespace UnityScript.Lang
{
	// Token: 0x0200000B RID: 11
	public static class UnityBuiltins
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00002A70 File Offset: 0x00000C70
		public static object eval(string code)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002A78 File Offset: 0x00000C78
		public static int parseInt(string value)
		{
			return int.Parse(value);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002A80 File Offset: 0x00000C80
		public static int parseInt(float value)
		{
			return checked((int)value);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002A84 File Offset: 0x00000C84
		public static int parseInt(double value)
		{
			return checked((int)value);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002A88 File Offset: 0x00000C88
		public static int parseInt(int value)
		{
			return value;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002A8C File Offset: 0x00000C8C
		public static float parseFloat(string value)
		{
			return float.Parse(value);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002A94 File Offset: 0x00000C94
		public static float parseFloat(float value)
		{
			return value;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002A98 File Offset: 0x00000C98
		public static float parseFloat(double value)
		{
			return (float)value;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002A9C File Offset: 0x00000C9C
		public static float parseFloat(int value)
		{
			return (float)value;
		}
	}
}
