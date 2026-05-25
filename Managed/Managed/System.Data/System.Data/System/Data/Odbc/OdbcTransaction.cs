using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Odbc
{
	/// <summary>Represents an SQL transaction to be made at a data source. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000129 RID: 297
	public sealed class OdbcTransaction : DbTransaction, IDisposable
	{
		// Token: 0x060010CE RID: 4302 RVA: 0x0004220C File Offset: 0x0004040C
		internal OdbcTransaction(OdbcConnection conn, IsolationLevel isolationlevel)
		{
			OdbcTransaction.SetAutoCommit(conn, false);
			OdbcIsolationLevel odbcIsolationLevel = OdbcIsolationLevel.ReadCommitted;
			OdbcConnectionAttribute odbcConnectionAttribute = OdbcConnectionAttribute.TransactionIsolation;
			if (isolationlevel != IsolationLevel.Unspecified)
			{
				if (isolationlevel == IsolationLevel.Chaos)
				{
					throw new ArgumentOutOfRangeException("IsolationLevel", string.Format(CultureInfo.CurrentCulture, "The IsolationLevel enumeration value, {0}, is not supported by the .Net Framework Odbc Data Provider.", new object[] { (int)isolationlevel }));
				}
				if (isolationlevel != IsolationLevel.ReadUncommitted)
				{
					if (isolationlevel != IsolationLevel.ReadCommitted)
					{
						if (isolationlevel != IsolationLevel.RepeatableRead)
						{
							if (isolationlevel != IsolationLevel.Serializable)
							{
								if (isolationlevel != IsolationLevel.Snapshot)
								{
									throw new ArgumentOutOfRangeException("IsolationLevel", string.Format(CultureInfo.CurrentCulture, "The IsolationLevel enumeration value, {0}, is invalid.", new object[] { (int)isolationlevel }));
								}
								odbcIsolationLevel = OdbcIsolationLevel.Snapshot;
								odbcConnectionAttribute = OdbcConnectionAttribute.CoptTransactionIsolation;
							}
							else
							{
								odbcIsolationLevel = OdbcIsolationLevel.Serializable;
							}
						}
						else
						{
							odbcIsolationLevel = OdbcIsolationLevel.RepeatableRead;
						}
					}
					else
					{
						odbcIsolationLevel = OdbcIsolationLevel.ReadCommitted;
					}
				}
				else
				{
					odbcIsolationLevel = OdbcIsolationLevel.ReadUncommitted;
				}
			}
			if (isolationlevel != IsolationLevel.Unspecified)
			{
				OdbcReturn odbcReturn = libodbc.SQLSetConnectAttr(conn.hDbc, odbcConnectionAttribute, (IntPtr)((int)odbcIsolationLevel), 0);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw conn.CreateOdbcException(OdbcHandleType.Dbc, conn.hDbc);
				}
			}
			this.isolationlevel = isolationlevel;
			this.connection = conn;
			this.isOpen = true;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00042344 File Offset: 0x00040544
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00042354 File Offset: 0x00040554
		private static void SetAutoCommit(OdbcConnection conn, bool isAuto)
		{
			OdbcReturn odbcReturn = libodbc.SQLSetConnectAttr(conn.hDbc, OdbcConnectionAttribute.AutoCommit, (IntPtr)((!isAuto) ? 0 : 1), -5);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw conn.CreateOdbcException(OdbcHandleType.Dbc, conn.hDbc);
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x000423A0 File Offset: 0x000405A0
		private static IsolationLevel GetIsolationLevel(OdbcConnection conn)
		{
			int num;
			int num2;
			OdbcReturn odbcReturn = libodbc.SQLGetConnectAttr(conn.hDbc, OdbcConnectionAttribute.TransactionIsolation, out num, 0, out num2);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw conn.CreateOdbcException(OdbcHandleType.Dbc, conn.hDbc);
			}
			return OdbcTransaction.MapOdbcIsolationLevel((OdbcIsolationLevel)num);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x000423E4 File Offset: 0x000405E4
		private static IsolationLevel MapOdbcIsolationLevel(OdbcIsolationLevel odbcLevel)
		{
			IsolationLevel isolationLevel = IsolationLevel.Unspecified;
			switch (odbcLevel)
			{
			case OdbcIsolationLevel.ReadUncommitted:
				isolationLevel = IsolationLevel.ReadUncommitted;
				break;
			case OdbcIsolationLevel.ReadCommitted:
				isolationLevel = IsolationLevel.ReadCommitted;
				break;
			default:
				if (odbcLevel == OdbcIsolationLevel.Snapshot)
				{
					isolationLevel = IsolationLevel.Snapshot;
				}
				break;
			case OdbcIsolationLevel.RepeatableRead:
				isolationLevel = IsolationLevel.RepeatableRead;
				break;
			case OdbcIsolationLevel.Serializable:
				isolationLevel = IsolationLevel.Serializable;
				break;
			}
			return isolationLevel;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00042464 File Offset: 0x00040664
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

		/// <summary>Commits the database transaction.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010D4 RID: 4308 RVA: 0x00042490 File Offset: 0x00040690
		public override void Commit()
		{
			if (!this.isOpen)
			{
				throw ExceptionHelper.TransactionNotUsable(base.GetType());
			}
			if (this.connection.transaction != this)
			{
				throw new InvalidOperationException();
			}
			OdbcReturn odbcReturn = libodbc.SQLEndTran(2, this.connection.hDbc, 0);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw this.connection.CreateOdbcException(OdbcHandleType.Dbc, this.connection.hDbc);
			}
			OdbcTransaction.SetAutoCommit(this.connection, true);
			this.connection.transaction = null;
			this.connection = null;
			this.isOpen = false;
		}

		/// <summary>Rolls back a transaction from a pending state.</summary>
		/// <exception cref="T:System.Exception">An error occurred while trying to commit the transaction. </exception>
		/// <exception cref="T:System.InvalidOperationException">The transaction has already been committed or rolled back.-or- The connection is broken. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010D5 RID: 4309 RVA: 0x00042530 File Offset: 0x00040730
		public override void Rollback()
		{
			if (!this.isOpen)
			{
				throw ExceptionHelper.TransactionNotUsable(base.GetType());
			}
			if (this.connection.transaction != this)
			{
				throw new InvalidOperationException();
			}
			OdbcReturn odbcReturn = libodbc.SQLEndTran(2, this.connection.hDbc, 1);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw this.connection.CreateOdbcException(OdbcHandleType.Dbc, this.connection.hDbc);
			}
			OdbcTransaction.SetAutoCommit(this.connection, true);
			this.connection.transaction = null;
			this.connection = null;
			this.isOpen = false;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x000425D0 File Offset: 0x000407D0
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
		}

		/// <summary>Specifies the <see cref="T:System.Data.IsolationLevel" /> for this transaction.</summary>
		/// <returns>The <see cref="T:System.Data.IsolationLevel" /> for this transaction. The default depends on the underlying ODBC driver.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x000425D8 File Offset: 0x000407D8
		public override IsolationLevel IsolationLevel
		{
			get
			{
				if (!this.isOpen)
				{
					throw ExceptionHelper.TransactionNotUsable(base.GetType());
				}
				if (this.isolationlevel == IsolationLevel.Unspecified)
				{
					this.isolationlevel = OdbcTransaction.GetIsolationLevel(this.Connection);
				}
				return this.isolationlevel;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Odbc.OdbcConnection" /> object associated with the transaction, or null if the transaction is no longer valid.</summary>
		/// <returns>The <see cref="T:System.Data.Odbc.OdbcConnection" /> object associated with the transaction.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x00042620 File Offset: 0x00040820
		public new OdbcConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x04000586 RID: 1414
		private bool disposed;

		// Token: 0x04000587 RID: 1415
		private OdbcConnection connection;

		// Token: 0x04000588 RID: 1416
		private IsolationLevel isolationlevel;

		// Token: 0x04000589 RID: 1417
		private bool isOpen;
	}
}
