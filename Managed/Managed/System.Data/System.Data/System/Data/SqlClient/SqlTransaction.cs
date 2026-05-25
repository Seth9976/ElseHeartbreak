using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Represents a Transact-SQL transaction to be made in a SQL Server database. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000176 RID: 374
	public sealed class SqlTransaction : DbTransaction, IDisposable, IDbTransaction
	{
		// Token: 0x06001418 RID: 5144 RVA: 0x000543E4 File Offset: 0x000525E4
		internal SqlTransaction(SqlConnection connection, IsolationLevel isolevel)
		{
			this.connection = connection;
			this.isolationLevel = isolevel;
			this.isOpen = true;
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlConnection" /> object associated with the transaction, or null if the transaction is no longer valid.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlConnection" /> object associated with the transaction.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x00054404 File Offset: 0x00052604
		public new SqlConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x0005440C File Offset: 0x0005260C
		internal bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction. The default is ReadCommitted.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x00054414 File Offset: 0x00052614
		public override IsolationLevel IsolationLevel
		{
			get
			{
				if (!this.isOpen)
				{
					throw ExceptionHelper.TransactionNotUsable(base.GetType());
				}
				return this.isolationLevel;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00054434 File Offset: 0x00052634
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		/// <summary>Commits the database transaction.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600141D RID: 5149 RVA: 0x0005443C File Offset: 0x0005263C
		public override void Commit()
		{
			if (!this.isOpen)
			{
				throw ExceptionHelper.TransactionNotUsable(base.GetType());
			}
			this.connection.Tds.Execute("COMMIT TRANSACTION");
			this.connection.Transaction = null;
			this.connection = null;
			this.isOpen = false;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00054490 File Offset: 0x00052690
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing && this.isOpen)
				{
					this.Rollback();
				}
				this.disposed = true;
			}
		}

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600141F RID: 5151 RVA: 0x000544BC File Offset: 0x000526BC
		public override void Rollback()
		{
			this.Rollback(string.Empty);
		}

		/// <summary>Rolls back a transaction from a pending state, and specifies the transaction or savepoint name.</summary>
		/// <param name="transactionName">The name of the transaction to roll back, or the savepoint to which to roll back. </param>
		/// <exception cref="T:System.ArgumentException">No transaction name was specified. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001420 RID: 5152 RVA: 0x000544CC File Offset: 0x000526CC
		public void Rollback(string transactionName)
		{
			if (this.disposed)
			{
				return;
			}
			if (!this.isOpen)
			{
				throw ExceptionHelper.TransactionNotUsable(base.GetType());
			}
			this.connection.Tds.Execute(string.Format("IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION {0}", transactionName));
			this.isOpen = false;
			this.connection.Transaction = null;
			this.connection = null;
		}

		/// <summary>Creates a savepoint in the transaction that can be used to roll back a part of the transaction, and specifies the savepoint name.</summary>
		/// <param name="savePointName">The name of the savepoint. </param>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001421 RID: 5153 RVA: 0x00054534 File Offset: 0x00052734
		public void Save(string savePointName)
		{
			if (!this.isOpen)
			{
				throw ExceptionHelper.TransactionNotUsable(base.GetType());
			}
			this.connection.Tds.Execute(string.Format("SAVE TRANSACTION {0}", savePointName));
		}

		// Token: 0x04000805 RID: 2053
		private bool disposed;

		// Token: 0x04000806 RID: 2054
		private SqlConnection connection;

		// Token: 0x04000807 RID: 2055
		private IsolationLevel isolationLevel;

		// Token: 0x04000808 RID: 2056
		private bool isOpen;
	}
}
