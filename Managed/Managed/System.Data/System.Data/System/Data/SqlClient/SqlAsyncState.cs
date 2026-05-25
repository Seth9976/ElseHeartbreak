using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000157 RID: 343
	internal class SqlAsyncState
	{
		// Token: 0x060011E1 RID: 4577 RVA: 0x00045F14 File Offset: 0x00044114
		public SqlAsyncState(object userState)
		{
			this._userState = userState;
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00045F24 File Offset: 0x00044124
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x00045F2C File Offset: 0x0004412C
		public object UserState
		{
			get
			{
				return this._userState;
			}
			set
			{
				this._userState = value;
			}
		}

		// Token: 0x0400071B RID: 1819
		private object _userState;
	}
}
