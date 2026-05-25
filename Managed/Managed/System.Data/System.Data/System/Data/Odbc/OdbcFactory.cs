using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	/// <summary>Represents a set of methods for creating instances of the ODBC provider's implementation of the data source classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000119 RID: 281
	public sealed class OdbcFactory : DbProviderFactory
	{
		// Token: 0x06000FB2 RID: 4018 RVA: 0x0003D638 File Offset: 0x0003B838
		private OdbcFactory()
		{
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0003D640 File Offset: 0x0003B840
		static OdbcFactory()
		{
			object obj = OdbcFactory.lockStatic;
			lock (obj)
			{
				if (OdbcFactory.Instance == null)
				{
					OdbcFactory.Instance = new OdbcFactory();
				}
			}
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnection" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000FB4 RID: 4020 RVA: 0x0003D6A0 File Offset: 0x0003B8A0
		public override DbConnection CreateConnection()
		{
			return new OdbcConnection();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommand" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000FB5 RID: 4021 RVA: 0x0003D6A8 File Offset: 0x0003B8A8
		public override DbCommand CreateCommand()
		{
			return new OdbcCommand();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommandBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003D6B0 File Offset: 0x0003B8B0
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new OdbcCommandBuilder();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003D6B8 File Offset: 0x0003B8B8
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new OdbcConnectionStringBuilder();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbDataAdapter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003D6C0 File Offset: 0x0003B8C0
		public override DbDataAdapter CreateDataAdapter()
		{
			return new OdbcDataAdapter();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbParameter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000FB9 RID: 4025 RVA: 0x0003D6C8 File Offset: 0x0003B8C8
		public override DbParameter CreateParameter()
		{
			return new OdbcParameter();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Security.CodeAccessPermission" /> instance. </summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Security.CodeAccessPermission" />.</returns>
		/// <param name="state">A member of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000FBA RID: 4026 RVA: 0x0003D6D0 File Offset: 0x0003B8D0
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new OdbcPermission(state);
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.Odbc.OdbcFactory" />, which can be used to retrieve strongly-typed data objects.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400053B RID: 1339
		public static readonly OdbcFactory Instance;

		// Token: 0x0400053C RID: 1340
		private static readonly object lockStatic = new object();
	}
}
