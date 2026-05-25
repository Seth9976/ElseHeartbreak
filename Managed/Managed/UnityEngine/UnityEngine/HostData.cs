using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200012C RID: 300
	[StructLayout(LayoutKind.Sequential)]
	public sealed class HostData
	{
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0001CC0C File Offset: 0x0001AE0C
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x0001CC1C File Offset: 0x0001AE1C
		public bool useNat
		{
			get
			{
				return this.m_Nat != 0;
			}
			set
			{
				this.m_Nat = ((!value) ? 0 : 1);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0001CC34 File Offset: 0x0001AE34
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0001CC3C File Offset: 0x0001AE3C
		public string gameType
		{
			get
			{
				return this.m_GameType;
			}
			set
			{
				this.m_GameType = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0001CC48 File Offset: 0x0001AE48
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0001CC50 File Offset: 0x0001AE50
		public string gameName
		{
			get
			{
				return this.m_GameName;
			}
			set
			{
				this.m_GameName = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0001CC5C File Offset: 0x0001AE5C
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x0001CC64 File Offset: 0x0001AE64
		public int connectedPlayers
		{
			get
			{
				return this.m_ConnectedPlayers;
			}
			set
			{
				this.m_ConnectedPlayers = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0001CC70 File Offset: 0x0001AE70
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x0001CC78 File Offset: 0x0001AE78
		public int playerLimit
		{
			get
			{
				return this.m_PlayerLimit;
			}
			set
			{
				this.m_PlayerLimit = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0001CC84 File Offset: 0x0001AE84
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x0001CC8C File Offset: 0x0001AE8C
		public string[] ip
		{
			get
			{
				return this.m_IP;
			}
			set
			{
				this.m_IP = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0001CC98 File Offset: 0x0001AE98
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x0001CCA0 File Offset: 0x0001AEA0
		public int port
		{
			get
			{
				return this.m_Port;
			}
			set
			{
				this.m_Port = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0001CCAC File Offset: 0x0001AEAC
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x0001CCBC File Offset: 0x0001AEBC
		public bool passwordProtected
		{
			get
			{
				return this.m_PasswordProtected != 0;
			}
			set
			{
				this.m_PasswordProtected = ((!value) ? 0 : 1);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0001CCD4 File Offset: 0x0001AED4
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x0001CCDC File Offset: 0x0001AEDC
		public string comment
		{
			get
			{
				return this.m_Comment;
			}
			set
			{
				this.m_Comment = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0001CCE8 File Offset: 0x0001AEE8
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0001CCF0 File Offset: 0x0001AEF0
		public string guid
		{
			get
			{
				return this.m_GUID;
			}
			set
			{
				this.m_GUID = value;
			}
		}

		// Token: 0x04000555 RID: 1365
		private int m_Nat;

		// Token: 0x04000556 RID: 1366
		private string m_GameType;

		// Token: 0x04000557 RID: 1367
		private string m_GameName;

		// Token: 0x04000558 RID: 1368
		private int m_ConnectedPlayers;

		// Token: 0x04000559 RID: 1369
		private int m_PlayerLimit;

		// Token: 0x0400055A RID: 1370
		private string[] m_IP;

		// Token: 0x0400055B RID: 1371
		private int m_Port;

		// Token: 0x0400055C RID: 1372
		private int m_PasswordProtected;

		// Token: 0x0400055D RID: 1373
		private string m_Comment;

		// Token: 0x0400055E RID: 1374
		private string m_GUID;
	}
}
