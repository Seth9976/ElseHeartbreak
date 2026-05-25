using System;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Transactions
{
	/// <summary>Describes a committable transaction.</summary>
	// Token: 0x0200000A RID: 10
	[Serializable]
	public sealed class CommittableTransaction : Transaction, IDisposable, IAsyncResult, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.CommittableTransaction" /> class. </summary>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt to create a transaction under Windows 98, Windows 98 Second Edition or Windows Millennium Edition.</exception>
		// Token: 0x0600000C RID: 12 RVA: 0x00002160 File Offset: 0x00000360
		public CommittableTransaction()
			: this(default(TransactionOptions))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.CommittableTransaction" /> class with the specified <paramref name="timeout" /> value.</summary>
		/// <param name="timeout">The maximum amount of time the transaction can exist, before it is aborted.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt to create a transaction under Windows 98, Windows 98 Second Edition or Windows Millennium Edition.</exception>
		// Token: 0x0600000D RID: 13 RVA: 0x0000217C File Offset: 0x0000037C
		public CommittableTransaction(TimeSpan timeout)
		{
			this.options = default(TransactionOptions);
			this.options.Timeout = timeout;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.CommittableTransaction" /> class with the specified transaction options.</summary>
		/// <param name="options">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use for the new transaction.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt to create a transaction under Windows 98, Windows 98 Second Edition or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is invalid.</exception>
		// Token: 0x0600000E RID: 14 RVA: 0x000021AC File Offset: 0x000003AC
		public CommittableTransaction(TransactionOptions options)
		{
			this.options = options;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021BC File Offset: 0x000003BC
		[MonoTODO("Not implemented")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the object provided as the last parameter of the <see cref="M:System.Transactions.CommittableTransaction.BeginCommit(System.AsyncCallback,System.Object)" /> method call.</summary>
		/// <returns>The object provided as the last parameter of the <see cref="M:System.Transactions.CommittableTransaction.BeginCommit(System.AsyncCallback,System.Object)" /> method call.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021C4 File Offset: 0x000003C4
		object IAsyncResult.AsyncState
		{
			get
			{
				return this.user_defined_state;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is used to wait for an asynchronous operation to complete.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021CC File Offset: 0x000003CC
		WaitHandle IAsyncResult.AsyncWaitHandle
		{
			get
			{
				return this.asyncResult.AsyncWaitHandle;
			}
		}

		/// <summary>Gets an indication of whether the asynchronous commit operation completed synchronously. </summary>
		/// <returns>true if the asynchronous commit operation completed synchronously; otherwise, false. This property always returns false even if the operation completed synchronously.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021DC File Offset: 0x000003DC
		bool IAsyncResult.CompletedSynchronously
		{
			get
			{
				return this.asyncResult.CompletedSynchronously;
			}
		}

		/// <summary>Gets an indication whether the asynchronous commit operation has completed.</summary>
		/// <returns>true if the operation is complete; otherwise, false. </returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000021EC File Offset: 0x000003EC
		bool IAsyncResult.IsCompleted
		{
			get
			{
				return this.asyncResult.IsCompleted;
			}
		}

		/// <summary>Begins an attempt to commit the transaction asynchronously.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> interface that can be used by the caller to check the status of the asynchronous operation, or to wait for the operation to complete.</returns>
		/// <param name="asyncCallback">The <see cref="T:System.AsyncCallback" /> delegate that is invoked when the transaction completes. This parameter can be null, in which case the application is not notified of the transaction's completion. Instead, the application must use the <see cref="T:System.IAsyncResult" /> interface to check for completion and wait accordingly, or call <see cref="M:System.Transactions.CommittableTransaction.EndCommit(System.IAsyncResult)" /> to wait for completion.</param>
		/// <param name="asyncState">An object, which might contain arbitrary state information, associated with the asynchronous commitment. This object is passed to the callback, and is not interpreted by <see cref="N:System.Transactions" />. A null reference is permitted.</param>
		// Token: 0x06000014 RID: 20 RVA: 0x000021FC File Offset: 0x000003FC
		public IAsyncResult BeginCommit(AsyncCallback callback, object user_defined_state)
		{
			this.callback = callback;
			this.user_defined_state = user_defined_state;
			AsyncCallback asyncCallback = null;
			if (callback != null)
			{
				asyncCallback = new AsyncCallback(this.CommitCallback);
			}
			this.asyncResult = base.BeginCommitInternal(asyncCallback);
			return this;
		}

		/// <summary>Ends an attempt to commit the transaction asynchronously.</summary>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> object associated with the asynchronous commitment.</param>
		/// <exception cref="T:System.Transactions.TransactionAbortedException">
		///   <see cref="M:System.Transactions.CommittableTransaction.BeginCommit(System.AsyncCallback,System.Object)" /> is called and the transaction rolls back for the first time.</exception>
		// Token: 0x06000015 RID: 21 RVA: 0x0000223C File Offset: 0x0000043C
		public void EndCommit(IAsyncResult ar)
		{
			if (ar != this)
			{
				throw new ArgumentException("The IAsyncResult parameter must be the same parameter as returned by BeginCommit.", "asyncResult");
			}
			base.EndCommitInternal(this.asyncResult);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002264 File Offset: 0x00000464
		private void CommitCallback(IAsyncResult ar)
		{
			if (this.asyncResult == null && ar.CompletedSynchronously)
			{
				this.asyncResult = ar;
			}
			this.callback(this);
		}

		/// <summary>Attempts to commit the transaction.</summary>
		/// <exception cref="T:System.Transactions.TransactionInDoubtException">
		///   <see cref="M:System.Transactions.CommittableTransaction.Commit" /> is called on a transaction and the transaction becomes <see cref="F:System.Transactions.TransactionStatus.InDoubt" />.</exception>
		/// <exception cref="T:System.Transactions.TransactionAbortedException">
		///   <see cref="M:System.Transactions.CommittableTransaction.Commit" /> is called and the transaction rolls back for the first time.</exception>
		// Token: 0x06000017 RID: 23 RVA: 0x00002290 File Offset: 0x00000490
		public void Commit()
		{
			base.CommitInternal();
		}

		// Token: 0x0400001F RID: 31
		private TransactionOptions options;

		// Token: 0x04000020 RID: 32
		private AsyncCallback callback;

		// Token: 0x04000021 RID: 33
		private object user_defined_state;

		// Token: 0x04000022 RID: 34
		private IAsyncResult asyncResult;
	}
}
