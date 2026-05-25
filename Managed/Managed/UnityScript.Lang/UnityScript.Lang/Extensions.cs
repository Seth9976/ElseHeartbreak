using System;

namespace UnityScript.Lang
{
	// Token: 0x02000009 RID: 9
	public static class Extensions
	{
		// Token: 0x0600004C RID: 76 RVA: 0x0000297C File Offset: 0x00000B7C
		public static bool operator ==(this char lhs, string rhs)
		{
			bool flag;
			if (flag = 1 == rhs.Length)
			{
				flag = lhs == rhs.get_Chars(0);
			}
			return flag;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002998 File Offset: 0x00000B98
		public static bool operator ==(this string lhs, char rhs)
		{
			return rhs == lhs;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029A4 File Offset: 0x00000BA4
		public static bool operator !=(this char lhs, string rhs)
		{
			return (1 != rhs.Length) ?? (lhs != rhs.get_Chars(0));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029D4 File Offset: 0x00000BD4
		public static bool operator !=(this string lhs, char rhs)
		{
			return rhs != lhs;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000029E0 File Offset: 0x00000BE0
		public static int length
		{
			get
			{
				return a.Length;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000029E8 File Offset: 0x00000BE8
		public static int length
		{
			get
			{
				return s.Length;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000029F0 File Offset: 0x00000BF0
		public static implicit operator bool(this Enum e)
		{
			return e.ToInt32(null) != 0;
		}
	}
}
