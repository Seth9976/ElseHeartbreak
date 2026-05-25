using System;
using System.Threading;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x0200002C RID: 44
	internal class TdsAsyncResult : IAsyncResult
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x0000DF68 File Offset: 0x0000C168
		public TdsAsyncResult(AsyncCallback userCallback, TdsAsyncState tdsState)
		{
			this._tdsState = tdsState;
			this._userCallback = userCallback;
			this._waitHandle = new ManualResetEvent(false);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000DF98 File Offset: 0x0000C198
		public TdsAsyncResult(AsyncCallback userCallback, object state)
		{
			this._tdsState = new TdsAsyncState(state);
			this._userCallback = userCallback;
			this._waitHandle = new ManualResetEvent(false);
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
		public object AsyncState
		{
			get
			{
				return this._tdsState.UserState;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000DFD0 File Offset: 0x0000C1D0
		internal TdsAsyncState TdsAsyncState
		{
			get
			{
				return this._tdsState;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this._waitHandle;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
		public bool IsCompleted
		{
			get
			{
				return this._completed;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000DFE8 File Offset: 0x0000C1E8
		public bool IsCompletedWithException
		{
			get
			{
				return this._exception != null;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000E000 File Offset: 0x0000C200
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedSyncly;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000E008 File Offset: 0x0000C208
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x0000E010 File Offset: 0x0000C210
		internal object ReturnValue
		{
			get
			{
				return this._retValue;
			}
			set
			{
				this._retValue = value;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000E01C File Offset: 0x0000C21C
		internal void MarkComplete()
		{
			this._completed = true;
			this._exception = null;
			((ManualResetEvent)this._waitHandle).Set();
			if (this._userCallback != null)
			{
				this._userCallback(this);
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000E060 File Offset: 0x0000C260
		internal void MarkComplete(Exception e)
		{
			this._completed = true;
			this._exception = e;
			((ManualResetEvent)this._waitHandle).Set();
			if (this._userCallback != null)
			{
				this._userCallback(this);
			}
		}

		// Token: 0x04000142 RID: 322
		private TdsAsyncState _tdsState;

		// Token: 0x04000143 RID: 323
		private WaitHandle _waitHandle;

		// Token: 0x04000144 RID: 324
		private bool _completed;

		// Token: 0x04000145 RID: 325
		private bool _completedSyncly;

		// Token: 0x04000146 RID: 326
		private AsyncCallback _userCallback;

		// Token: 0x04000147 RID: 327
		private object _retValue;

		// Token: 0x04000148 RID: 328
		private Exception _exception;
	}
}
