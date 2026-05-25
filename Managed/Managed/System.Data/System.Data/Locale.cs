using System;

// Token: 0x0200000B RID: 11
internal sealed class Locale
{
	// Token: 0x0600002D RID: 45 RVA: 0x00003984 File Offset: 0x00001B84
	private Locale()
	{
	}

	// Token: 0x0600002E RID: 46 RVA: 0x0000398C File Offset: 0x00001B8C
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00003990 File Offset: 0x00001B90
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
