using System;

namespace System.Transactions
{
	/// <summary>Makes a code block transactional. This class cannot be inherited.</summary>
	// Token: 0x02000025 RID: 37
	public sealed class TransactionScope : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class. </summary>
		// Token: 0x0600009B RID: 155 RVA: 0x00002DFC File Offset: 0x00000FFC
		public TransactionScope()
			: this(TransactionScopeOption.Required, TransactionManager.DefaultTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction. </summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		// Token: 0x0600009C RID: 156 RVA: 0x00002E0C File Offset: 0x0000100C
		public TransactionScope(Transaction transaction)
			: this(transaction, TransactionManager.DefaultTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction. </summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		// Token: 0x0600009D RID: 157 RVA: 0x00002E1C File Offset: 0x0000101C
		public TransactionScope(Transaction transaction, TimeSpan timeout)
			: this(transaction, timeout, EnterpriseServicesInteropOption.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value and COM+ interoperability requirements, and sets the specified transaction as the ambient transaction, so that transactional work done inside the scope uses this transaction. </summary>
		/// <param name="transactionToUse">The transaction to be set as the ambient transaction, so that transactional work done inside the scope uses this transaction.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		/// <param name="interopOption">An instance of the <see cref="T:System.Transactions.EnterpriseServicesInteropOption" /> enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
		// Token: 0x0600009E RID: 158 RVA: 0x00002E28 File Offset: 0x00001028
		[MonoTODO("EnterpriseServicesInteropOption not supported.")]
		public TransactionScope(Transaction transaction, TimeSpan timeout, EnterpriseServicesInteropOption opt)
		{
			this.Initialize(TransactionScopeOption.Required, transaction, TransactionScope.defaultOptions, opt, timeout);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		// Token: 0x0600009F RID: 159 RVA: 0x00002E4C File Offset: 0x0000104C
		public TransactionScope(TransactionScopeOption option)
			: this(option, TransactionManager.DefaultTimeout)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified timeout value and requirements.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="scopeTimeout">The <see cref="T:System.TimeSpan" /> after which the transaction scope times out and aborts the transaction.</param>
		// Token: 0x060000A0 RID: 160 RVA: 0x00002E5C File Offset: 0x0000105C
		[MonoTODO("No TimeoutException is thrown")]
		public TransactionScope(TransactionScopeOption option, TimeSpan timeout)
		{
			this.Initialize(option, null, TransactionScope.defaultOptions, EnterpriseServicesInteropOption.None, timeout);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified requirements.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="transactionOptions">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
		// Token: 0x060000A1 RID: 161 RVA: 0x00002E80 File Offset: 0x00001080
		public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions options)
			: this(scopeOption, options, EnterpriseServicesInteropOption.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionScope" /> class with the specified scope and COM+ interoperability requirements, and transaction options.</summary>
		/// <param name="scopeOption">An instance of the <see cref="T:System.Transactions.TransactionScopeOption" /> enumeration that describes the transaction requirements associated with this transaction scope.</param>
		/// <param name="transactionOptions">A <see cref="T:System.Transactions.TransactionOptions" /> structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
		/// <param name="interopOption">An instance of the <see cref="T:System.Transactions.EnterpriseServicesInteropOption" /> enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
		// Token: 0x060000A2 RID: 162 RVA: 0x00002E8C File Offset: 0x0000108C
		[MonoTODO("EnterpriseServicesInteropOption not supported")]
		public TransactionScope(TransactionScopeOption scopeOption, TransactionOptions options, EnterpriseServicesInteropOption opt)
		{
			this.Initialize(scopeOption, null, options, opt, TransactionManager.DefaultTimeout);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002EC4 File Offset: 0x000010C4
		private void Initialize(TransactionScopeOption scopeOption, Transaction tx, TransactionOptions options, EnterpriseServicesInteropOption interop, TimeSpan timeout)
		{
			this.completed = false;
			this.isRoot = false;
			this.nested = 0;
			this.oldTransaction = Transaction.CurrentInternal;
			Transaction.CurrentInternal = (this.transaction = this.InitTransaction(tx, scopeOption));
			if (this.transaction != null)
			{
				this.transaction.InitScope(this);
			}
			if (this.parentScope != null)
			{
				this.parentScope.nested++;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002F44 File Offset: 0x00001144
		private Transaction InitTransaction(Transaction tx, TransactionScopeOption scopeOption)
		{
			if (tx != null)
			{
				return tx;
			}
			if (scopeOption == TransactionScopeOption.Suppress)
			{
				if (Transaction.CurrentInternal != null)
				{
					this.parentScope = Transaction.CurrentInternal.Scope;
				}
				return null;
			}
			if (scopeOption != TransactionScopeOption.Required)
			{
				if (Transaction.CurrentInternal != null)
				{
					this.parentScope = Transaction.CurrentInternal.Scope;
				}
				this.isRoot = true;
				return new Transaction();
			}
			if (Transaction.CurrentInternal == null)
			{
				this.isRoot = true;
				return new Transaction();
			}
			this.parentScope = Transaction.CurrentInternal.Scope;
			return Transaction.CurrentInternal;
		}

		/// <summary>Indicates that all operations within the scope are completed successfully.</summary>
		/// <exception cref="T:System.InvalidOperationException">This method has already been called once.</exception>
		// Token: 0x060000A6 RID: 166 RVA: 0x00002FF0 File Offset: 0x000011F0
		public void Complete()
		{
			if (this.completed)
			{
				throw new InvalidOperationException("The current TransactionScope is already complete. You should dispose the TransactionScope.");
			}
			this.completed = true;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003010 File Offset: 0x00001210
		internal bool IsComplete
		{
			get
			{
				return this.completed;
			}
		}

		/// <summary>Ends the transaction scope.</summary>
		// Token: 0x060000A8 RID: 168 RVA: 0x00003018 File Offset: 0x00001218
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (this.parentScope != null)
			{
				this.parentScope.nested--;
			}
			if (this.nested > 0)
			{
				this.transaction.Rollback();
				throw new InvalidOperationException("TransactionScope nested incorrectly");
			}
			if (Transaction.CurrentInternal != this.transaction)
			{
				if (this.transaction != null)
				{
					this.transaction.Rollback();
				}
				if (Transaction.CurrentInternal != null)
				{
					Transaction.CurrentInternal.Rollback();
				}
				throw new InvalidOperationException("Transaction.Current has changed inside of the TransactionScope");
			}
			if (Transaction.CurrentInternal == this.oldTransaction && this.oldTransaction != null)
			{
				this.oldTransaction.Scope = this.parentScope;
			}
			Transaction.CurrentInternal = this.oldTransaction;
			if (this.transaction == null)
			{
				return;
			}
			this.transaction.Scope = null;
			if (!this.IsComplete)
			{
				this.transaction.Rollback();
				return;
			}
			if (!this.isRoot)
			{
				return;
			}
			this.transaction.CommitInternal();
		}

		// Token: 0x04000052 RID: 82
		private static TransactionOptions defaultOptions = new TransactionOptions(IsolationLevel.Serializable, TransactionManager.DefaultTimeout);

		// Token: 0x04000053 RID: 83
		private Transaction transaction;

		// Token: 0x04000054 RID: 84
		private Transaction oldTransaction;

		// Token: 0x04000055 RID: 85
		private TransactionScope parentScope;

		// Token: 0x04000056 RID: 86
		private int nested;

		// Token: 0x04000057 RID: 87
		private bool disposed;

		// Token: 0x04000058 RID: 88
		private bool completed;

		// Token: 0x04000059 RID: 89
		private bool isRoot;
	}
}
