using System;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x0200002B RID: 43
	internal class TdsAsyncState
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x0000DF30 File Offset: 0x0000C130
		public TdsAsyncState(object userState)
		{
			this._userState = userState;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000DF40 File Offset: 0x0000C140
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000DF48 File Offset: 0x0000C148
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000DF54 File Offset: 0x0000C154
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000DF5C File Offset: 0x0000C15C
		public bool WantResults
		{
			get
			{
				return this._wantResults;
			}
			set
			{
				this._wantResults = value;
			}
		}

		// Token: 0x04000140 RID: 320
		private object _userState;

		// Token: 0x04000141 RID: 321
		private bool _wantResults;
	}
}
