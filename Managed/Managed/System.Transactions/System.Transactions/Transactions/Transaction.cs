using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Transactions
{
	/// <summary>Represents a transaction.</summary>
	// Token: 0x0200001A RID: 26
	[Serializable]
	public class Transaction : IDisposable, ISerializable
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002390 File Offset: 0x00000590
		internal Transaction()
		{
			this.info = new TransactionInformation();
			this.level = IsolationLevel.Serializable;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000023CC File Offset: 0x000005CC
		internal Transaction(Transaction other)
		{
			this.level = other.level;
			this.info = other.info;
			this.dependents = other.dependents;
		}

		/// <summary>Indicates that the transaction is completed.</summary>
		/// <exception cref="T:System.ObjectDisposedException">An attempt to subscribe this event on a transaction that has been disposed. </exception>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600003A RID: 58 RVA: 0x00002424 File Offset: 0x00000624
		// (remove) Token: 0x0600003B RID: 59 RVA: 0x00002440 File Offset: 0x00000640
		public event TransactionCompletedEventHandler TransactionCompleted;

		/// <summary>Gets a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize this transaction. </summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" /> ) for this serialization. </param>
		// Token: 0x0600003C RID: 60 RVA: 0x0000245C File Offset: 0x0000065C
		[MonoTODO]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the ambient transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> that describes the current transaction.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002464 File Offset: 0x00000664
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002470 File Offset: 0x00000670
		public static Transaction Current
		{
			get
			{
				Transaction.EnsureIncompleteCurrentScope();
				return Transaction.CurrentInternal;
			}
			set
			{
				Transaction.EnsureIncompleteCurrentScope();
				Transaction.CurrentInternal = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002480 File Offset: 0x00000680
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002488 File Offset: 0x00000688
		internal static Transaction CurrentInternal
		{
			get
			{
				return Transaction.ambient;
			}
			set
			{
				Transaction.ambient = value;
			}
		}

		/// <summary>Gets the isolation level of the transaction.</summary>
		/// <returns>One of the <see cref="T:System.Transactions.IsolationLevel" /> values that indicates the isolation level of the transaction.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002490 File Offset: 0x00000690
		public IsolationLevel IsolationLevel
		{
			get
			{
				Transaction.EnsureIncompleteCurrentScope();
				return this.level;
			}
		}

		/// <summary>Retrieves additional information about a transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.TransactionInformation" /> that contains additional information about the transaction.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000024A0 File Offset: 0x000006A0
		public TransactionInformation TransactionInformation
		{
			get
			{
				Transaction.EnsureIncompleteCurrentScope();
				return this.info;
			}
		}

		/// <summary>Creates a clone of the transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> that is a copy of the current transaction object.</returns>
		// Token: 0x06000043 RID: 67 RVA: 0x000024B0 File Offset: 0x000006B0
		public Transaction Clone()
		{
			return new Transaction(this);
		}

		/// <summary>Releases the resources that are held by the object.</summary>
		// Token: 0x06000044 RID: 68 RVA: 0x000024B8 File Offset: 0x000006B8
		public void Dispose()
		{
			if (this.TransactionInformation.Status == TransactionStatus.Active)
			{
				this.Rollback();
			}
		}

		/// <summary>Creates a dependent clone of the transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.DependentTransaction" /> that represents the dependent clone.</returns>
		/// <param name="cloneOption">A <see cref="T:System.Transactions.DependentCloneOption" /> that controls what kind of dependent transaction to create.</param>
		// Token: 0x06000045 RID: 69 RVA: 0x000024D0 File Offset: 0x000006D0
		[MonoTODO]
		public DependentTransaction DependentClone(DependentCloneOption option)
		{
			DependentTransaction dependentTransaction = new DependentTransaction(this, option);
			this.dependents.Add(dependentTransaction);
			return dependentTransaction;
		}

		/// <summary>Enlists a durable resource manager that supports two phase commit to participate in a transaction.</summary>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> object that describes the enlistment.</returns>
		/// <param name="resourceManagerIdentifier">A unique identifier for a resource manager, which should persist across resource manager failure or reboot.</param>
		/// <param name="enlistmentNotification">An object that implements the <see cref="T:System.Transactions.IEnlistmentNotification" /> interface to receive two phase commit notifications. </param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		// Token: 0x06000046 RID: 70 RVA: 0x000024F4 File Offset: 0x000006F4
		[MonoTODO("Only SinglePhase commit supported for durable resource managers.")]
		[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"/>\n")]
		public Enlistment EnlistDurable(Guid manager, IEnlistmentNotification notification, EnlistmentOptions options)
		{
			throw new NotImplementedException("Only SinglePhase commit supported for durable resource managers.");
		}

		/// <summary>Enlists a durable resource manager that supports single phase commit optimization to participate in a transaction.</summary>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> object that describes the enlistment.</returns>
		/// <param name="resourceManagerIdentifier">A unique identifier for a resource manager, which should persist across resource manager failure or reboot.</param>
		/// <param name="singlePhaseNotification">An object that implements the <see cref="T:System.Transactions.ISinglePhaseNotification" /> interface that must be able to receive single phase commit and two phase commit notifications.</param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		// Token: 0x06000047 RID: 71 RVA: 0x00002500 File Offset: 0x00000700
		[MonoTODO("Only Local Transaction Manager supported. Cannot have more than 1 durable resource per transaction. Only EnlistmentOptions.None supported yet.")]
		[PermissionSet((SecurityAction)14, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"/>\n")]
		public Enlistment EnlistDurable(Guid manager, ISinglePhaseNotification notification, EnlistmentOptions options)
		{
			if (this.durables.Count == 1)
			{
				throw new NotImplementedException("Only LTM supported. Cannot have more than 1 durable resource per transaction.");
			}
			Transaction.EnsureIncompleteCurrentScope();
			if (options != EnlistmentOptions.None)
			{
				throw new NotImplementedException("Implement me");
			}
			this.durables.Add(notification);
			return new Enlistment();
		}

		/// <summary>Enlists a resource manager that has an internal transaction using a promotable single phase enlistment (PSPE). </summary>
		/// <returns>A <see cref="T:System.Transactions.SinglePhaseEnlistment" /> interface implementation that describes the enlistment.</returns>
		/// <param name="promotableSinglePhaseNotification">A <see cref="T:System.Transactions.IPromotableSinglePhaseNotification" /> interface implemented by the participant.</param>
		// Token: 0x06000048 RID: 72 RVA: 0x00002550 File Offset: 0x00000750
		[MonoTODO]
		public bool EnlistPromotableSinglePhase(IPromotableSinglePhaseNotification notification)
		{
			throw new NotImplementedException();
		}

		/// <summary>Enlists a volatile resource manager that supports two phase commit to participate in a transaction.</summary>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> object that describes the enlistment.</returns>
		/// <param name="enlistmentNotification">An object that implements the <see cref="T:System.Transactions.IEnlistmentNotification" /> interface to receive two phase commit notifications. </param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		// Token: 0x06000049 RID: 73 RVA: 0x00002558 File Offset: 0x00000758
		[MonoTODO("EnlistmentOptions being ignored")]
		public Enlistment EnlistVolatile(IEnlistmentNotification notification, EnlistmentOptions options)
		{
			return this.EnlistVolatileInternal(notification, options);
		}

		/// <summary>Enlists a volatile resource manager that supports single phase commit optimization to participate in a transaction.</summary>
		/// <returns>An <see cref="T:System.Transactions.Enlistment" /> object that describes the enlistment.</returns>
		/// <param name="singlePhaseNotification">An object that implements the <see cref="T:System.Transactions.ISinglePhaseNotification" /> interface that must be able to receive single phase commit and two phase commit notifications.</param>
		/// <param name="enlistmentOptions">
		///   <see cref="F:System.Transactions.EnlistmentOptions.EnlistDuringPrepareRequired" /> if the resource manager wants to perform additional work during the prepare phase.</param>
		// Token: 0x0600004A RID: 74 RVA: 0x00002564 File Offset: 0x00000764
		[MonoTODO("EnlistmentOptions being ignored")]
		public Enlistment EnlistVolatile(ISinglePhaseNotification notification, EnlistmentOptions options)
		{
			return this.EnlistVolatileInternal(notification, options);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002570 File Offset: 0x00000770
		private Enlistment EnlistVolatileInternal(IEnlistmentNotification notification, EnlistmentOptions options)
		{
			Transaction.EnsureIncompleteCurrentScope();
			this.volatiles.Add(notification);
			return new Enlistment();
		}

		/// <summary>Determines whether this transaction and the specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> and this transaction are identical; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x0600004C RID: 76 RVA: 0x00002588 File Offset: 0x00000788
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Transaction);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002598 File Offset: 0x00000798
		private bool Equals(Transaction t)
		{
			return object.ReferenceEquals(t, this) || (!object.ReferenceEquals(t, null) && this.level == t.level && this.info == t.info);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600004E RID: 78 RVA: 0x000025E4 File Offset: 0x000007E4
		public override int GetHashCode()
		{
			return (int)(this.level ^ (IsolationLevel)this.info.GetHashCode() ^ (IsolationLevel)this.dependents.GetHashCode());
		}

		/// <summary>Rolls back (aborts) the transaction.</summary>
		// Token: 0x0600004F RID: 79 RVA: 0x00002604 File Offset: 0x00000804
		public void Rollback()
		{
			this.Rollback(null);
		}

		/// <summary>Rolls back (aborts) the transaction.</summary>
		/// <param name="e">An explanation of why a rollback occurred.</param>
		// Token: 0x06000050 RID: 80 RVA: 0x00002610 File Offset: 0x00000810
		public void Rollback(Exception ex)
		{
			Transaction.EnsureIncompleteCurrentScope();
			this.Rollback(ex, null);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002620 File Offset: 0x00000820
		internal void Rollback(Exception ex, IEnlistmentNotification enlisted)
		{
			if (this.aborted)
			{
				return;
			}
			if (this.info.Status == TransactionStatus.Committed)
			{
				throw new TransactionException("Transaction has already been committed. Cannot accept any new work.");
			}
			this.innerException = ex;
			Enlistment enlistment = new Enlistment();
			foreach (IEnlistmentNotification enlistmentNotification in this.volatiles)
			{
				if (enlistmentNotification != enlisted)
				{
					enlistmentNotification.Rollback(enlistment);
				}
			}
			if (this.durables.Count > 0 && this.durables[0] != enlisted)
			{
				this.durables[0].Rollback(enlistment);
			}
			this.Aborted = true;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002700 File Offset: 0x00000900
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002708 File Offset: 0x00000908
		private bool Aborted
		{
			get
			{
				return this.aborted;
			}
			set
			{
				this.aborted = value;
				if (this.aborted)
				{
					this.info.Status = TransactionStatus.Aborted;
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002728 File Offset: 0x00000928
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002730 File Offset: 0x00000930
		internal TransactionScope Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000273C File Offset: 0x0000093C
		protected IAsyncResult BeginCommitInternal(AsyncCallback callback)
		{
			if (this.committed || this.committing)
			{
				throw new InvalidOperationException("Commit has already been called for this transaction.");
			}
			this.committing = true;
			this.asyncCommit = new Transaction.AsyncCommit(this.DoCommit);
			return this.asyncCommit.BeginInvoke(callback, null);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002790 File Offset: 0x00000990
		protected void EndCommitInternal(IAsyncResult ar)
		{
			this.asyncCommit.EndInvoke(ar);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000027A0 File Offset: 0x000009A0
		internal void CommitInternal()
		{
			if (this.committed || this.committing)
			{
				throw new InvalidOperationException("Commit has already been called for this transaction.");
			}
			this.committing = true;
			this.DoCommit();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000027DC File Offset: 0x000009DC
		private void DoCommit()
		{
			if (this.Scope != null)
			{
				this.Rollback(null, null);
				this.CheckAborted();
			}
			if (this.volatiles.Count == 1 && this.durables.Count == 0)
			{
				ISinglePhaseNotification singlePhaseNotification = this.volatiles[0] as ISinglePhaseNotification;
				if (singlePhaseNotification != null)
				{
					this.DoSingleCommit(singlePhaseNotification);
					this.Complete();
					return;
				}
			}
			if (this.volatiles.Count > 0)
			{
				this.DoPreparePhase();
			}
			if (this.durables.Count > 0)
			{
				this.DoSingleCommit(this.durables[0]);
			}
			if (this.volatiles.Count > 0)
			{
				this.DoCommitPhase();
			}
			this.Complete();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000028A0 File Offset: 0x00000AA0
		private void Complete()
		{
			this.committing = false;
			this.committed = true;
			if (!this.aborted)
			{
				this.info.Status = TransactionStatus.Committed;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000028C8 File Offset: 0x00000AC8
		internal void InitScope(TransactionScope scope)
		{
			this.CheckAborted();
			if (this.committed)
			{
				throw new InvalidOperationException("Commit has already been called on this transaction.");
			}
			this.Scope = scope;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000028F0 File Offset: 0x00000AF0
		private void DoPreparePhase()
		{
			foreach (IEnlistmentNotification enlistmentNotification in this.volatiles)
			{
				PreparingEnlistment preparingEnlistment = new PreparingEnlistment(this, enlistmentNotification);
				enlistmentNotification.Prepare(preparingEnlistment);
				if (!preparingEnlistment.IsPrepared)
				{
					this.Aborted = true;
					break;
				}
			}
			this.CheckAborted();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000297C File Offset: 0x00000B7C
		private void DoCommitPhase()
		{
			foreach (IEnlistmentNotification enlistmentNotification in this.volatiles)
			{
				Enlistment enlistment = new Enlistment();
				enlistmentNotification.Commit(enlistment);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000029E8 File Offset: 0x00000BE8
		private void DoSingleCommit(ISinglePhaseNotification single)
		{
			if (single == null)
			{
				return;
			}
			SinglePhaseEnlistment singlePhaseEnlistment = new SinglePhaseEnlistment(this, single);
			single.SinglePhaseCommit(singlePhaseEnlistment);
			this.CheckAborted();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002A14 File Offset: 0x00000C14
		private void CheckAborted()
		{
			if (this.aborted)
			{
				throw new TransactionAbortedException("Transaction has aborted", this.innerException);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002A34 File Offset: 0x00000C34
		private static void EnsureIncompleteCurrentScope()
		{
			if (Transaction.CurrentInternal == null)
			{
				return;
			}
			if (Transaction.CurrentInternal.Scope != null && Transaction.CurrentInternal.Scope.IsComplete)
			{
				throw new InvalidOperationException("The current TransactionScope is already complete");
			}
		}

		/// <summary>Tests whether two specified <see cref="T:System.Transactions.Transaction" /> instances are equivalent.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> are equal; otherwise, false.</returns>
		/// <param name="x">The <see cref="T:System.Transactions.Transaction" /> instance that is to the left of the equality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.Transaction" /> instance that is to the right of the equality operator.</param>
		// Token: 0x06000061 RID: 97 RVA: 0x00002A80 File Offset: 0x00000C80
		public static bool operator ==(Transaction x, Transaction y)
		{
			if (object.ReferenceEquals(x, null))
			{
				return object.ReferenceEquals(y, null);
			}
			return x.Equals(y);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Transactions.Transaction" /> instances are not equal.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> are not equal; otherwise, false.</returns>
		/// <param name="x">The <see cref="T:System.Transactions.Transaction" /> instance that is to the left of the inequality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.Transaction" /> instance that is to the right of the inequality operator.</param>
		// Token: 0x06000062 RID: 98 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public static bool operator !=(Transaction x, Transaction y)
		{
			return !(x == y);
		}

		// Token: 0x0400003C RID: 60
		[ThreadStatic]
		private static Transaction ambient;

		// Token: 0x0400003D RID: 61
		private IsolationLevel level;

		// Token: 0x0400003E RID: 62
		private TransactionInformation info;

		// Token: 0x0400003F RID: 63
		private ArrayList dependents = new ArrayList();

		// Token: 0x04000040 RID: 64
		private List<IEnlistmentNotification> volatiles = new List<IEnlistmentNotification>();

		// Token: 0x04000041 RID: 65
		private List<ISinglePhaseNotification> durables = new List<ISinglePhaseNotification>();

		// Token: 0x04000042 RID: 66
		private Transaction.AsyncCommit asyncCommit;

		// Token: 0x04000043 RID: 67
		private bool committing;

		// Token: 0x04000044 RID: 68
		private bool committed;

		// Token: 0x04000045 RID: 69
		private bool aborted;

		// Token: 0x04000046 RID: 70
		private TransactionScope scope;

		// Token: 0x04000047 RID: 71
		private Exception innerException;

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x060000AA RID: 170
		private delegate void AsyncCommit();
	}
}
