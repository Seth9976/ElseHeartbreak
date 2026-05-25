using System;

namespace System.EnterpriseServices
{
	/// <summary>Describes the chain of callers leading up to the current method call.</summary>
	// Token: 0x0200003B RID: 59
	public sealed class SecurityCallContext
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00002874 File Offset: 0x00000A74
		internal SecurityCallContext()
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000287C File Offset: 0x00000A7C
		internal SecurityCallContext(ISecurityCallContext context)
		{
		}

		/// <summary>Gets a <see cref="T:System.EnterpriseServices.SecurityCallers" /> object that describes the caller.</summary>
		/// <returns>The <see cref="T:System.EnterpriseServices.SecurityCallers" /> object that describes the caller.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no security context. </exception>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002884 File Offset: 0x00000A84
		public SecurityCallers Callers
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.EnterpriseServices.SecurityCallContext" /> object that describes the security call context.</summary>
		/// <returns>The <see cref="T:System.EnterpriseServices.SecurityCallContext" /> object that describes the security call context.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000288C File Offset: 0x00000A8C
		public static SecurityCallContext CurrentCall
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.EnterpriseServices.SecurityIdentity" /> object that describes the direct caller of this method.</summary>
		/// <returns>A <see cref="T:System.EnterpriseServices.SecurityIdentity" /> value.</returns>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002894 File Offset: 0x00000A94
		public SecurityIdentity DirectCaller
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines whether security checks are enabled in the current context.</summary>
		/// <returns>true if security checks are enabled in the current context; otherwise, false.</returns>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000289C File Offset: 0x00000A9C
		public bool IsSecurityEnabled
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the MinAuthenticationLevel value from the ISecurityCallContext collection in COM+.</summary>
		/// <returns>The MinAuthenticationLevel value from the ISecurityCallContext collection in COM+.</returns>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000028A4 File Offset: 0x00000AA4
		public int MinAuthenticationLevel
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the NumCallers value from the ISecurityCallContext collection in COM+.</summary>
		/// <returns>The NumCallers value from the ISecurityCallContext collection in COM+.</returns>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000028AC File Offset: 0x00000AAC
		public int NumCallers
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.EnterpriseServices.SecurityIdentity" /> that describes the original caller.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.SecurityIdentity" /> values.</returns>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000028B4 File Offset: 0x00000AB4
		public SecurityIdentity OriginalCaller
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Verifies that the direct caller is a member of the specified role.</summary>
		/// <returns>true if the direct caller is a member of the specified role; otherwise, false.</returns>
		/// <param name="role">The specified role. </param>
		// Token: 0x060000DF RID: 223 RVA: 0x000028BC File Offset: 0x00000ABC
		[MonoTODO]
		public bool IsCallerInRole(string role)
		{
			throw new NotImplementedException();
		}

		/// <summary>Verifies that the specified user is in the specified role.</summary>
		/// <returns>true if the specified user is a member of the specified role; otherwise, false.</returns>
		/// <param name="user">The specified user. </param>
		/// <param name="role">The specified role. </param>
		// Token: 0x060000E0 RID: 224 RVA: 0x000028C4 File Offset: 0x00000AC4
		[MonoTODO]
		public bool IsUserInRole(string user, string role)
		{
			throw new NotImplementedException();
		}
	}
}
