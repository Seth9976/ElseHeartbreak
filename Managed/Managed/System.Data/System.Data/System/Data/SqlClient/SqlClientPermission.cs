using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	/// <summary>Enables the .NET Framework Data Provider for SQL Server to help make sure that a user has a security level sufficient to access a data source. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015A RID: 346
	[Serializable]
	public sealed class SqlClientPermission : DBDataPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> class.</summary>
		// Token: 0x060011F7 RID: 4599 RVA: 0x000460EC File Offset: 0x000442EC
		[Obsolete("Use SqlClientPermission(PermissionState.None)", true)]
		public SqlClientPermission()
			: this(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x060011F8 RID: 4600 RVA: 0x000460F8 File Offset: 0x000442F8
		public SqlClientPermission(PermissionState state)
			: base(state)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed. </param>
		// Token: 0x060011F9 RID: 4601 RVA: 0x00046104 File Offset: 0x00044304
		[Obsolete("Use SqlClientPermission(PermissionState.None)", true)]
		public SqlClientPermission(PermissionState state, bool allowBlankPassword)
			: base(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00046114 File Offset: 0x00044314
		internal SqlClientPermission(DBDataPermission permission)
			: base(permission)
		{
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00046120 File Offset: 0x00044320
		internal SqlClientPermission(DBDataPermissionAttribute attribute)
			: base(attribute)
		{
		}

		/// <summary>Returns the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> as an <see cref="T:System.Security.IPermission" />.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060011FC RID: 4604 RVA: 0x0004612C File Offset: 0x0004432C
		public override IPermission Copy()
		{
			return new SqlClientPermission(this);
		}

		/// <summary>Adds a new connection string and a set of restricted keywords to the <see cref="T:System.Data.SqlClient.SqlClientPermission" /> object.</summary>
		/// <param name="connectionString">The connection string.</param>
		/// <param name="restrictions">The key restrictions.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> enumerations.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060011FD RID: 4605 RVA: 0x00046134 File Offset: 0x00044334
		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			base.Add(connectionString, restrictions, behavior);
		}
	}
}
