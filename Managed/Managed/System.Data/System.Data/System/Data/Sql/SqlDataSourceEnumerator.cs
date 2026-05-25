using System;
using System.Data.Common;

namespace System.Data.Sql
{
	/// <summary>Provides a mechanism for enumerating all available instances of SQL Server within the local network. </summary>
	// Token: 0x02000142 RID: 322
	public sealed class SqlDataSourceEnumerator : DbDataSourceEnumerator
	{
		// Token: 0x06001160 RID: 4448 RVA: 0x00044428 File Offset: 0x00042628
		private SqlDataSourceEnumerator()
		{
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" />, which can be used to retrieve information about available SQL Server instances.</summary>
		/// <returns>A <see cref="T:System.Data.Sql.SqlDataSourceEnumerator" />.</returns>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x00044430 File Offset: 0x00042630
		[MonoTODO]
		public static SqlDataSourceEnumerator Instance
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Retrieves a <see cref="T:System.Data.DataTable" /> containing information about all visible SQL Server 2000 or SQL Server 2005 instances.</summary>
		/// <returns>Returns a <see cref="T:System.Data.DataTable" /> containing information about the visible SQL Server instances.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001162 RID: 4450 RVA: 0x00044438 File Offset: 0x00042638
		[MonoTODO]
		public override DataTable GetDataSources()
		{
			throw new NotImplementedException();
		}
	}
}
