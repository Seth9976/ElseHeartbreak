using System;

namespace System.Data
{
	/// <summary>Represents a transaction to be performed at a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000054 RID: 84
	public interface IDbTransaction : IDisposable
	{
		/// <summary>Commits the database transaction.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005E0 RID: 1504
		void Commit();

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005E1 RID: 1505
		void Rollback();

		/// <summary>Specifies the Connection object to associate with the transaction.</summary>
		/// <returns>The Connection object to associate with the transaction.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005E2 RID: 1506
		IDbConnection Connection { get; }

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction. The default is ReadCommitted.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005E3 RID: 1507
		IsolationLevel IsolationLevel { get; }
	}
}
