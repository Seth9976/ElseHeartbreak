using System;
using System.Data.Common;
using System.Data.Sql;
using System.Security;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	/// <summary>Represents a set of methods for creating instances of the <see cref="N:System.Data.SqlClient" /> provider's implementation of the data source classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000179 RID: 377
	public sealed class SqlClientFactory : DbProviderFactory
	{
		// Token: 0x0600142E RID: 5166 RVA: 0x00054880 File Offset: 0x00052A80
		private SqlClientFactory()
		{
		}

		/// <summary>Returns true if a <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" /> can be created; otherwise false .</summary>
		/// <returns>true if a <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" /> can be created; otherwise false.</returns>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x00054894 File Offset: 0x00052A94
		public override bool CanCreateDataSourceEnumerator
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbCommand" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001431 RID: 5169 RVA: 0x00054898 File Offset: 0x00052A98
		public override DbCommand CreateCommand()
		{
			return new SqlCommand();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbCommandBuilder" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001432 RID: 5170 RVA: 0x000548A0 File Offset: 0x00052AA0
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new SqlCommandBuilder();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbConnection" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001433 RID: 5171 RVA: 0x000548A8 File Offset: 0x00052AA8
		public override DbConnection CreateConnection()
		{
			return new SqlConnection();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001434 RID: 5172 RVA: 0x000548B0 File Offset: 0x00052AB0
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SqlConnectionStringBuilder();
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbDataAdapter" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001435 RID: 5173 RVA: 0x000548B8 File Offset: 0x00052AB8
		public override DbDataAdapter CreateDataAdapter()
		{
			return new SqlDataAdapter();
		}

		/// <summary>Returns a new <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" />.</summary>
		/// <returns>A <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001436 RID: 5174 RVA: 0x000548C0 File Offset: 0x00052AC0
		public override DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return SqlDataSourceEnumerator.Instance;
		}

		/// <summary>Returns a strongly typed <see cref="T:System.Data.Common.DbParameter" /> instance.</summary>
		/// <returns>A new strongly typed instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001437 RID: 5175 RVA: 0x000548C8 File Offset: 0x00052AC8
		public override DbParameter CreateParameter()
		{
			return new SqlParameter();
		}

		/// <summary>Returns a new <see cref="T:System.Security.CodeAccessPermission" />.</summary>
		/// <returns>A strongly typed instance of <see cref="T:System.Security.CodeAccessPermission" />.</returns>
		/// <param name="state">A member of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001438 RID: 5176 RVA: 0x000548D0 File Offset: 0x00052AD0
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new SqlClientPermission(state);
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.SqlClient.SqlClientFactory" />. This can be used to retrieve strongly typed data objects.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400080E RID: 2062
		public static readonly SqlClientFactory Instance = new SqlClientFactory();
	}
}
