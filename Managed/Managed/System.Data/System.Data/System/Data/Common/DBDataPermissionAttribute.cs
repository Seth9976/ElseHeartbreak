using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Data.Common
{
	/// <summary>Associates a security action with a custom security attribute.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000BE RID: 190
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public abstract class DBDataPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DBDataPermissionAttribute" />.</summary>
		/// <param name="action">One of the security action values representing an action that can be performed by declarative security.</param>
		// Token: 0x06000912 RID: 2322 RVA: 0x0002D7A0 File Offset: 0x0002B9A0
		protected DBDataPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets a value indicating whether a blank password is allowed.</summary>
		/// <returns>true if a blank password is allowed; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0002D7AC File Offset: 0x0002B9AC
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x0002D7B4 File Offset: 0x0002B9B4
		public bool AllowBlankPassword
		{
			get
			{
				return this.allowBlankPassword;
			}
			set
			{
				this.allowBlankPassword = value;
			}
		}

		/// <summary>Gets or sets connection string parameters that are allowed or disallowed.</summary>
		/// <returns>One or more connection string parameters that are allowed or disallowed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x0002D7C0 File Offset: 0x0002B9C0
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x0002D7DC File Offset: 0x0002B9DC
		public string KeyRestrictions
		{
			get
			{
				if (this.keyRestrictions == null)
				{
					return string.Empty;
				}
				return this.keyRestrictions;
			}
			set
			{
				this.keyRestrictions = value;
			}
		}

		/// <summary>Gets or sets a permitted connection string.</summary>
		/// <returns>A permitted connection string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x0002D7E8 File Offset: 0x0002B9E8
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x0002D804 File Offset: 0x0002BA04
		public string ConnectionString
		{
			get
			{
				if (this.connectionString == null)
				{
					return string.Empty;
				}
				return this.connectionString;
			}
			set
			{
				this.connectionString = value;
			}
		}

		/// <summary>Identifies whether the list of connection string parameters identified by the <see cref="P:System.Data.Common.DBDataPermissionAttribute.KeyRestrictions" /> property are the only connection string parameters allowed.</summary>
		/// <returns>One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0002D810 File Offset: 0x0002BA10
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0002D818 File Offset: 0x0002BA18
		public KeyRestrictionBehavior KeyRestrictionBehavior
		{
			get
			{
				return this.keyRestrictionBehavior;
			}
			set
			{
				ExceptionHelper.CheckEnumValue(typeof(KeyRestrictionBehavior), value);
				this.keyRestrictionBehavior = value;
			}
		}

		/// <summary>Identifies whether the attribute should serialize the connection string.</summary>
		/// <returns>true if the attribute should serialize the connection string; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600091B RID: 2331 RVA: 0x0002D838 File Offset: 0x0002BA38
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeConnectionString()
		{
			return false;
		}

		/// <summary>Identifies whether the attribute should serialize the set of key restrictions.</summary>
		/// <returns>true if the attribute should serialize the set of key restrictions; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600091C RID: 2332 RVA: 0x0002D83C File Offset: 0x0002BA3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeKeyRestrictions()
		{
			return false;
		}

		// Token: 0x0400032C RID: 812
		private bool allowBlankPassword;

		// Token: 0x0400032D RID: 813
		private string keyRestrictions;

		// Token: 0x0400032E RID: 814
		private KeyRestrictionBehavior keyRestrictionBehavior;

		// Token: 0x0400032F RID: 815
		private string connectionString;
	}
}
