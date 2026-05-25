using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Odbc
{
	/// <summary>Enables the .NET Framework Data Provider for ODBC to help make sure that a user has a security level sufficient to access an ODBC data source. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200013F RID: 319
	[Serializable]
	public sealed class OdbcPermission : DBDataPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcPermission" /> class.</summary>
		// Token: 0x06001130 RID: 4400 RVA: 0x000433CC File Offset: 0x000415CC
		[Obsolete("use OdbcPermission(PermissionState.None)", true)]
		public OdbcPermission()
			: base(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcPermission" /> class with one of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x06001131 RID: 4401 RVA: 0x000433D8 File Offset: 0x000415D8
		public OdbcPermission(PermissionState state)
			: base(state)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcPermission" /> class.</summary>
		/// <param name="state">One of the System.Security.Permissions.PermissionState values. </param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed. </param>
		// Token: 0x06001132 RID: 4402 RVA: 0x000433E4 File Offset: 0x000415E4
		[Obsolete("use OdbcPermission(PermissionState.None)", true)]
		public OdbcPermission(PermissionState state, bool allowBlankPassword)
			: base(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000433F4 File Offset: 0x000415F4
		internal OdbcPermission(DBDataPermission permission)
			: base(permission)
		{
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00043400 File Offset: 0x00041600
		internal OdbcPermission(DBDataPermissionAttribute attribute)
			: base(attribute)
		{
		}

		/// <summary>Returns the <see cref="T:System.Data.Odbc.OdbcPermission" /> as an <see cref="T:System.Security.IPermission" />.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001135 RID: 4405 RVA: 0x0004340C File Offset: 0x0004160C
		public override IPermission Copy()
		{
			return new OdbcPermission(this);
		}

		/// <summary>Adds access for the specified connection string to the existing state of the permission.</summary>
		/// <param name="connectionString">A permitted connection string. </param>
		/// <param name="restrictions">String that identifies connection string parameters that are allowed or disallowed. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> values. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001136 RID: 4406 RVA: 0x00043414 File Offset: 0x00041614
		public override void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			base.Add(connectionString, restrictions, behavior);
		}
	}
}
