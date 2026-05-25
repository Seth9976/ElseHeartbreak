using System;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x02000158 RID: 344
	internal class SqlAsyncResult : IAsyncResult
	{
		// Token: 0x060011E4 RID: 4580 RVA: 0x00045F38 File Offset: 0x00044138
		public SqlAsyncResult(AsyncCallback userCallback, SqlAsyncState sqlState)
		{
			this._sqlState = sqlState;
			this._userCallback = userCallback;
			this._waitHandle = new ManualResetEvent(false);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00045F68 File Offset: 0x00044168
		public SqlAsyncResult(AsyncCallback userCallback, object state)
		{
			this._sqlState = new SqlAsyncState(state);
			this._userCallback = userCallback;
			this._waitHandle = new ManualResetEvent(false);
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x00045F90 File Offset: 0x00044190
		public object AsyncState
		{
			get
			{
				return this._sqlState.UserState;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x00045FA0 File Offset: 0x000441A0
		internal SqlAsyncState SqlAsyncState
		{
			get
			{
				return this._sqlState;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x00045FA8 File Offset: 0x000441A8
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this._waitHandle;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060011E9 RID: 4585 RVA: 0x00045FB0 File Offset: 0x000441B0
		public bool IsCompleted
		{
			get
			{
				return this._completed;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x00045FB8 File Offset: 0x000441B8
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedSyncly;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x00045FC0 File Offset: 0x000441C0
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x00045FC8 File Offset: 0x000441C8
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

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00045FD4 File Offset: 0x000441D4
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x00045FDC File Offset: 0x000441DC
		public string EndMethod
		{
			get
			{
				return this._endMethod;
			}
			set
			{
				this._endMethod = value;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00045FE8 File Offset: 0x000441E8
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x00045FF0 File Offset: 0x000441F0
		public bool Ended
		{
			get
			{
				return this._ended;
			}
			set
			{
				this._ended = value;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x00045FFC File Offset: 0x000441FC
		// (set) Token: 0x060011F2 RID: 4594 RVA: 0x00046004 File Offset: 0x00044204
		internal IAsyncResult InternalResult
		{
			get
			{
				return this._internal;
			}
			set
			{
				this._internal = value;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x00046010 File Offset: 0x00044210
		public AsyncCallback BubbleCallback
		{
			get
			{
				return new AsyncCallback(this.Bubbleback);
			}
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00046020 File Offset: 0x00044220
		internal void MarkComplete()
		{
			this._completed = true;
			((ManualResetEvent)this._waitHandle).Set();
			if (this._userCallback != null)
			{
				this._userCallback(this);
			}
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00046054 File Offset: 0x00044254
		public void Bubbleback(IAsyncResult ar)
		{
			this.MarkComplete();
		}

		// Token: 0x0400071C RID: 1820
		private SqlAsyncState _sqlState;

		// Token: 0x0400071D RID: 1821
		private WaitHandle _waitHandle;

		// Token: 0x0400071E RID: 1822
		private bool _completed;

		// Token: 0x0400071F RID: 1823
		private bool _completedSyncly;

		// Token: 0x04000720 RID: 1824
		private bool _ended;

		// Token: 0x04000721 RID: 1825
		private AsyncCallback _userCallback;

		// Token: 0x04000722 RID: 1826
		private object _retValue;

		// Token: 0x04000723 RID: 1827
		private string _endMethod;

		// Token: 0x04000724 RID: 1828
		private IAsyncResult _internal;
	}
}
