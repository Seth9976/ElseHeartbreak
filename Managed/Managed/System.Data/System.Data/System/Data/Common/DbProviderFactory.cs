using System;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Common
{
	/// <summary>Represents a set of methods for creating instances of a provider's implementation of the data source classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CD RID: 205
	public abstract class DbProviderFactory
	{
		// Token: 0x060009F7 RID: 2551 RVA: 0x0002F018 File Offset: 0x0002D218
		private NotImplementedException CreateNotImplementedException()
		{
			return new NotImplementedException();
		}

		/// <summary>Specifies whether the specific <see cref="T:System.Data.Common.DbProviderFactory" /> supports the <see cref="T:System.Data.Common.DbDataSourceEnumerator" /> class.</summary>
		/// <returns>true if the instance of the <see cref="T:System.Data.Common.DbProviderFactory" /> supports the <see cref="T:System.Data.Common.DbDataSourceEnumerator" /> class; otherwise false.</returns>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0002F020 File Offset: 0x0002D220
		public virtual bool CanCreateDataSourceEnumerator
		{
			get
			{
				throw this.CreateNotImplementedException();
			}
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbCommand" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009F9 RID: 2553 RVA: 0x0002F028 File Offset: 0x0002D228
		public virtual DbCommand CreateCommand()
		{
			throw this.CreateNotImplementedException();
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009FA RID: 2554 RVA: 0x0002F030 File Offset: 0x0002D230
		public virtual DbCommandBuilder CreateCommandBuilder()
		{
			throw this.CreateNotImplementedException();
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbConnection" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009FB RID: 2555 RVA: 0x0002F038 File Offset: 0x0002D238
		public virtual DbConnection CreateConnection()
		{
			throw this.CreateNotImplementedException();
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbDataAdapter" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009FC RID: 2556 RVA: 0x0002F040 File Offset: 0x0002D240
		public virtual DbDataAdapter CreateDataAdapter()
		{
			throw this.CreateNotImplementedException();
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbDataSourceEnumerator" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbDataSourceEnumerator" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009FD RID: 2557 RVA: 0x0002F048 File Offset: 0x0002D248
		public virtual DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			throw this.CreateNotImplementedException();
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbParameter" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009FE RID: 2558 RVA: 0x0002F050 File Offset: 0x0002D250
		public virtual DbParameter CreateParameter()
		{
			throw this.CreateNotImplementedException();
		}

		/// <summary>Returns a new instance of the provider's class that implements the provider's version of the <see cref="T:System.Security.CodeAccessPermission" /> class.</summary>
		/// <returns>A <see cref="T:System.Security.CodeAccessPermission" /> object for the specified <see cref="T:System.Security.Permissions.PermissionState" />.</returns>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060009FF RID: 2559 RVA: 0x0002F058 File Offset: 0x0002D258
		public virtual CodeAccessPermission CreatePermission(PermissionState state)
		{
			throw this.CreateNotImplementedException();
		}

		/// <summary>Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000A00 RID: 2560 RVA: 0x0002F060 File Offset: 0x0002D260
		public virtual DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			throw this.CreateNotImplementedException();
		}
	}
}
