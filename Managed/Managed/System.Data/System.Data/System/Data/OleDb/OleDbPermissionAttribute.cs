using System;
using System.ComponentModel;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	/// <summary>Associates a security action with a custom security attribute.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000FA RID: 250
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class OleDbPermissionAttribute : DBDataPermissionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbPermissionAttribute" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values representing an action that can be performed by using declarative security. </param>
		// Token: 0x06000C2B RID: 3115 RVA: 0x00034338 File Offset: 0x00032538
		public OleDbPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets a comma-delimited string that contains a list of supported providers.</summary>
		/// <returns>A comma-delimited list of providers allowed by the security policy.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00034344 File Offset: 0x00032544
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x00034360 File Offset: 0x00032560
		[Obsolete]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Returns an <see cref="T:System.Data.OleDb.OleDbPermission" /> object that is configured according to the attribute properties.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbPermission" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000C2E RID: 3118 RVA: 0x0003436C File Offset: 0x0003256C
		public override IPermission CreatePermission()
		{
			return new OleDbPermission(this);
		}

		// Token: 0x04000477 RID: 1143
		private string _provider;
	}
}
