using System;

namespace System.Data.Common
{
	/// <summary>The base class for a transaction. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000CF RID: 207
	public abstract class DbTransaction : MarshalByRefObject, IDisposable, IDbTransaction
	{
		/// <summary>Gets the <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction, or a null reference if the transaction is no longer valid.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</returns>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002F088 File Offset: 0x0002D288
		IDbConnection IDbTransaction.Connection
		{
			get
			{
				return this.Connection;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0002F090 File Offset: 0x0002D290
		public DbConnection Connection
		{
			get
			{
				return this.DbConnection;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DbConnection" /> object associated with the transaction.</returns>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000A06 RID: 2566
		protected abstract DbConnection DbConnection { get; }

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000A07 RID: 2567
		public abstract IsolationLevel IsolationLevel { get; }

		/// <summary>Commits the database transaction.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A08 RID: 2568
		public abstract void Commit();

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A09 RID: 2569
		public abstract void Rollback();

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbTransaction" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000A0A RID: 2570 RVA: 0x0002F098 File Offset: 0x0002D298
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbTransaction" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">If true, this method releases all resources held by any managed objects that this <see cref="T:System.Data.Common.DbTransaction" /> references.</param>
		// Token: 0x06000A0B RID: 2571 RVA: 0x0002F0A4 File Offset: 0x0002D2A4
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
