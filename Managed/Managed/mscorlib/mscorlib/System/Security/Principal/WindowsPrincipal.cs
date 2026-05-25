using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Allows code to check the Windows group membership of a Windows user.</summary>
	// Token: 0x0200066D RID: 1645
	[ComVisible(true)]
	[Serializable]
	public class WindowsPrincipal : IPrincipal
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.WindowsPrincipal" /> class by using the specified <see cref="T:System.Security.Principal.WindowsIdentity" /> object.</summary>
		/// <param name="ntIdentity">The <see cref="T:System.Security.Principal.WindowsIdentity" /> object from which to construct the new instance of <see cref="T:System.Security.Principal.WindowsPrincipal" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ntIdentity" /> is null. </exception>
		// Token: 0x06003E7B RID: 15995 RVA: 0x000D663C File Offset: 0x000D483C
		public WindowsPrincipal(WindowsIdentity ntIdentity)
		{
			if (ntIdentity == null)
			{
				throw new ArgumentNullException("ntIdentity");
			}
			this._identity = ntIdentity;
		}

		/// <summary>Gets the identity of the current principal.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.WindowsIdentity" /> object of the current principal.</returns>
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06003E7C RID: 15996 RVA: 0x000D665C File Offset: 0x000D485C
		public virtual IIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified relative identifier (RID).</summary>
		/// <returns>true if the current principal is a member of the specified Windows user group, that is, in a particular role; otherwise, false.</returns>
		/// <param name="rid">The RID of the Windows user group in which to check for the principal’s membership status. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06003E7D RID: 15997 RVA: 0x000D6664 File Offset: 0x000D4864
		public virtual bool IsInRole(int rid)
		{
			if (WindowsPrincipal.IsPosix)
			{
				return WindowsPrincipal.IsMemberOfGroupId(this.Token, (IntPtr)rid);
			}
			string text;
			switch (rid)
			{
			case 544:
				text = "BUILTIN\\Administrators";
				break;
			case 545:
				text = "BUILTIN\\Users";
				break;
			case 546:
				text = "BUILTIN\\Guests";
				break;
			case 547:
				text = "BUILTIN\\Power Users";
				break;
			case 548:
				text = "BUILTIN\\Account Operators";
				break;
			case 549:
				text = "BUILTIN\\System Operators";
				break;
			case 550:
				text = "BUILTIN\\Print Operators";
				break;
			case 551:
				text = "BUILTIN\\Backup Operators";
				break;
			case 552:
				text = "BUILTIN\\Replicator";
				break;
			default:
				return false;
			}
			return this.IsInRole(text);
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified name.</summary>
		/// <returns>true if the current principal is a member of the specified Windows user group; otherwise, false.</returns>
		/// <param name="role">The name of the Windows user group for which to check membership. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06003E7E RID: 15998 RVA: 0x000D6734 File Offset: 0x000D4934
		public virtual bool IsInRole(string role)
		{
			if (role == null)
			{
				return false;
			}
			if (WindowsPrincipal.IsPosix)
			{
				return WindowsPrincipal.IsMemberOfGroupName(this.Token, role);
			}
			if (this.m_roles == null)
			{
				this.m_roles = WindowsIdentity._GetRoles(this.Token);
			}
			role = role.ToUpperInvariant();
			foreach (string text in this.m_roles)
			{
				if (text != null && role == text.ToUpperInvariant())
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified <see cref="T:System.Security.Principal.WindowsBuiltInRole" />.</summary>
		/// <returns>true if the current principal is a member of the specified Windows user group; otherwise, false.</returns>
		/// <param name="role">One of the <see cref="T:System.Security.Principal.WindowsBuiltInRole" /> values. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="role" /> is not a valid <see cref="T:System.Security.Principal.WindowsBuiltInRole" /> value.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06003E7F RID: 15999 RVA: 0x000D67C0 File Offset: 0x000D49C0
		public virtual bool IsInRole(WindowsBuiltInRole role)
		{
			if (!WindowsPrincipal.IsPosix)
			{
				return this.IsInRole((int)role);
			}
			if (role != WindowsBuiltInRole.Administrator)
			{
				return false;
			}
			string text = "root";
			return this.IsInRole(text);
		}

		/// <summary>Determines whether the current principal belongs to the Windows user group with the specified security identifier (SID).</summary>
		/// <returns>true if the current principal is a member of the specified Windows user group; otherwise, false.</returns>
		/// <param name="sid">A <see cref="T:System.Security.Principal.SecurityIdentifier" />  that uniquely identifies a Windows user group.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sid" /> is null.</exception>
		/// <exception cref="T:System.Security.SecurityException">Windows returned a Win32 error.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06003E80 RID: 16000 RVA: 0x000D6808 File Offset: 0x000D4A08
		[MonoTODO("not implemented")]
		[ComVisible(false)]
		public virtual bool IsInRole(SecurityIdentifier sid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06003E81 RID: 16001 RVA: 0x000D6810 File Offset: 0x000D4A10
		private static bool IsPosix
		{
			get
			{
				int platform = (int)Environment.Platform;
				return platform == 128 || platform == 4 || platform == 6;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06003E82 RID: 16002 RVA: 0x000D683C File Offset: 0x000D4A3C
		private IntPtr Token
		{
			get
			{
				return this._identity.Token;
			}
		}

		// Token: 0x06003E83 RID: 16003
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsMemberOfGroupId(IntPtr user, IntPtr group);

		// Token: 0x06003E84 RID: 16004
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsMemberOfGroupName(IntPtr user, string group);

		// Token: 0x04001B28 RID: 6952
		private WindowsIdentity _identity;

		// Token: 0x04001B29 RID: 6953
		private string[] m_roles;
	}
}
