using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents an SQL transaction to be made at a data source. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FE RID: 254
	public sealed class OleDbTransaction : DbTransaction, IDisposable, IDbTransaction
	{
		// Token: 0x06000C38 RID: 3128 RVA: 0x00034608 File Offset: 0x00032808
		internal OleDbTransaction(OleDbConnection connection, int depth)
			: this(connection, depth, IsolationLevel.ReadCommitted)
		{
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00034618 File Offset: 0x00032818
		internal OleDbTransaction(OleDbConnection connection)
			: this(connection, 1)
		{
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00034624 File Offset: 0x00032824
		internal OleDbTransaction(OleDbConnection connection, int depth, IsolationLevel isolevel)
		{
			this.connection = connection;
			this.gdaTransaction = libgda.gda_transaction_new(depth.ToString());
			if (isolevel != IsolationLevel.ReadUncommitted)
			{
				if (isolevel != IsolationLevel.ReadCommitted)
				{
					if (isolevel != IsolationLevel.RepeatableRead)
					{
						if (isolevel == IsolationLevel.Serializable)
						{
							libgda.gda_transaction_set_isolation_level(this.gdaTransaction, GdaTransactionIsolation.Serializable);
						}
					}
					else
					{
						libgda.gda_transaction_set_isolation_level(this.gdaTransaction, GdaTransactionIsolation.RepeatableRead);
					}
				}
				else
				{
					libgda.gda_transaction_set_isolation_level(this.gdaTransaction, GdaTransactionIsolation.ReadCommitted);
				}
			}
			else
			{
				libgda.gda_transaction_set_isolation_level(this.gdaTransaction, GdaTransactionIsolation.ReadUncommitted);
			}
			libgda.gda_connection_begin_transaction(connection.GdaConnection, this.gdaTransaction);
			this.isOpen = true;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000346E0 File Offset: 0x000328E0
		internal OleDbTransaction(OleDbConnection connection, IsolationLevel isolevel)
			: this(connection, 1, isolevel)
		{
		}

		/// <summary>Gets the <see cref="T:System.Data.OleDb.OleDbConnection" /> object associated with the transaction, or null if the transaction is no longer valid.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbConnection" /> object associated with the transaction.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x000346EC File Offset: 0x000328EC
		public new OleDbConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x000346F4 File Offset: 0x000328F4
		protected override DbConnection DbConnection
		{
			get
			{
				return this.connection;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction. The default is ReadCommitted.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000346FC File Offset: 0x000328FC
		public override IsolationLevel IsolationLevel
		{
			get
			{
				if (!this.isOpen)
				{
					throw ExceptionHelper.TransactionNotUsable(base.GetType());
				}
				switch (libgda.gda_transaction_get_isolation_level(this.gdaTransaction))
				{
				case GdaTransactionIsolation.ReadCommitted:
					return IsolationLevel.ReadCommitted;
				case GdaTransactionIsolation.ReadUncommitted:
					return IsolationLevel.ReadUncommitted;
				case GdaTransactionIsolation.RepeatableRead:
					return IsolationLevel.RepeatableRead;
				case GdaTransactionIsolation.Serializable:
					return IsolationLevel.Serializable;
				default:
					return IsolationLevel.Unspecified;
				}
			}
		}

		/// <summary>Initiates a nested database transaction.</summary>
		/// <returns>A nested database transaction.</returns>
		/// <exception cref="T:System.InvalidOperationException">Nested transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C3F RID: 3135 RVA: 0x00034764 File Offset: 0x00032964
		public OleDbTransaction Begin()
		{
			if (!this.isOpen)
			{
				throw ExceptionHelper.TransactionNotUsable(base.GetType());
			}
			return new OleDbTransaction(this.connection, this.depth + 1);
		}

		/// <summary>Initiates a nested database transaction and specifies the isolation level to use for the new transaction.</summary>
		/// <returns>A nested database transaction.</returns>
		/// <param name="isolevel">The isolation level to use for the transaction. </param>
		/// <exception cref="T:System.InvalidOperationException">Nested transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C40 RID: 3136 RVA: 0x0003479C File Offset: 0x0003299C
		public OleDbTransaction Begin(IsolationLevel isolevel)
		{
			if (!this.isOpen)
			{
				throw ExceptionHelper.TransactionNotUsable(base.GetType());
			}
			return new OleDbTransaction(this.connection, this.depth + 1, isolevel);
		}

		/// <summary>Commits the database transaction.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C41 RID: 3137 RVA: 0x000347CC File Offset: 0x000329CC
		public override void Commit()
		{
			if (!this.isOpen)
			{
				throw ExceptionHelper.TransactionNotUsable(base.GetType());
			}
			if (!libgda.gda_connection_commit_transaction(this.connection.GdaConnection, this.gdaTransaction))
			{
				throw new InvalidOperationException();
			}
			this.connection = null;
			this.isOpen = false;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00034820 File Offset: 0x00032A20
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
			base.Dispose(disposing);
		}

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000C43 RID: 3139 RVA: 0x00034860 File Offset: 0x00032A60
		public override void Rollback()
		{
			if (!this.isOpen)
			{
				throw ExceptionHelper.TransactionNotUsable(base.GetType());
			}
			if (!libgda.gda_connection_rollback_transaction(this.connection.GdaConnection, this.gdaTransaction))
			{
				throw new InvalidOperationException();
			}
			this.connection = null;
			this.isOpen = false;
		}

		// Token: 0x0400049B RID: 1179
		private bool disposed;

		// Token: 0x0400049C RID: 1180
		private OleDbConnection connection;

		// Token: 0x0400049D RID: 1181
		private IntPtr gdaTransaction;

		// Token: 0x0400049E RID: 1182
		private int depth;

		// Token: 0x0400049F RID: 1183
		private bool isOpen;
	}
}
