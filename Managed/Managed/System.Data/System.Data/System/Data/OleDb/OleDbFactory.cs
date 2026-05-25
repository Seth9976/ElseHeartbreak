using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	/// <summary>Represents a set of methods for creating instances of the OLEDB provider's implementation of the data source classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F3 RID: 243
	public sealed class OleDbFactory : DbProviderFactory
	{
		// Token: 0x06000BC2 RID: 3010 RVA: 0x0003370C File Offset: 0x0003190C
		private OleDbFactory()
		{
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommand" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BC4 RID: 3012 RVA: 0x00033720 File Offset: 0x00031920
		public override DbCommand CreateCommand()
		{
			return new OleDbCommand();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbCommandBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BC5 RID: 3013 RVA: 0x00033728 File Offset: 0x00031928
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new OleDbCommandBuilder();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnection" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000BC6 RID: 3014 RVA: 0x00033730 File Offset: 0x00031930
		public override DbConnection CreateConnection()
		{
			return new OleDbConnection();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000BC7 RID: 3015 RVA: 0x00033738 File Offset: 0x00031938
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return null;
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbDataAdapter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BC8 RID: 3016 RVA: 0x0003373C File Offset: 0x0003193C
		public override DbDataAdapter CreateDataAdapter()
		{
			return new OleDbDataAdapter();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Data.Common.DbParameter" /> instance.</summary>
		/// <returns>A new strongly-typed instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BC9 RID: 3017 RVA: 0x00033744 File Offset: 0x00031944
		public override DbParameter CreateParameter()
		{
			return new OleDbParameter();
		}

		/// <summary>Returns a strongly-typed <see cref="T:System.Security.CodeAccessPermission" /> instance.</summary>
		/// <returns>A strongly-typed instance of <see cref="T:System.Security.CodeAccessPermission" />.</returns>
		/// <param name="state">A member of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BCA RID: 3018 RVA: 0x0003374C File Offset: 0x0003194C
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new OleDbPermission(state);
		}

		/// <summary>Gets an instance of the <see cref="T:System.Data.OleDb.OleDbFactory" />. This can be used to retrieve strongly-typed data objects.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0400043D RID: 1085
		public static readonly OleDbFactory Instance = new OleDbFactory();
	}
}
