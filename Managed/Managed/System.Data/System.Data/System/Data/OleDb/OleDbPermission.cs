using System;
using System.ComponentModel;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	/// <summary>Enables the .NET Framework Data Provider for OLE DB to help make sure that a user has a security level sufficient to access an OLE DB data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F9 RID: 249
	[Serializable]
	public sealed class OleDbPermission : DBDataPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbPermission" /> class.</summary>
		// Token: 0x06000C23 RID: 3107 RVA: 0x000342C8 File Offset: 0x000324C8
		[Obsolete("use OleDbPermission(PermissionState.None)", true)]
		public OleDbPermission()
			: base(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x06000C24 RID: 3108 RVA: 0x000342D4 File Offset: 0x000324D4
		public OleDbPermission(PermissionState state)
			: base(state)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbPermission" /> class.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed. </param>
		// Token: 0x06000C25 RID: 3109 RVA: 0x000342E0 File Offset: 0x000324E0
		[Obsolete("use OleDbPermission(PermissionState.None)", true)]
		public OleDbPermission(PermissionState state, bool allowBlankPassword)
			: base(state)
		{
			base.AllowBlankPassword = allowBlankPassword;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000342F0 File Offset: 0x000324F0
		internal OleDbPermission(DBDataPermission permission)
			: base(permission)
		{
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000342FC File Offset: 0x000324FC
		internal OleDbPermission(DBDataPermissionAttribute attribute)
			: base(attribute)
		{
		}

		/// <summary>This property has been marked as obsolete. Setting this property will have no effect.</summary>
		/// <returns>This property has been marked as obsolete. Setting this property will have no effect.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00034308 File Offset: 0x00032508
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x00034324 File Offset: 0x00032524
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Obsolete]
		public string Provider
		{
			get
			{
				if (this._provider == null)
				{
					return string.Empty;
				}
				return this._provider;
			}
			set
			{
				this._provider = value;
			}
		}

		/// <summary>Returns the <see cref="T:System.Data.OleDb.OleDbPermission" /> as an <see cref="T:System.Security.IPermission" />.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000C2A RID: 3114 RVA: 0x00034330 File Offset: 0x00032530
		public override IPermission Copy()
		{
			return new OleDbPermission(this);
		}

		// Token: 0x04000476 RID: 1142
		private string _provider;
	}
}
