using System;

namespace System.EnterpriseServices
{
	/// <summary>Contains information that regards an identity in a COM+ call chain.</summary>
	// Token: 0x0200003D RID: 61
	public sealed class SecurityIdentity
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x000028F4 File Offset: 0x00000AF4
		[MonoTODO]
		internal SecurityIdentity()
		{
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000028FC File Offset: 0x00000AFC
		[MonoTODO]
		internal SecurityIdentity(ISecurityIdentityColl collection)
		{
		}

		/// <summary>Gets the name of the user described by this identity.</summary>
		/// <returns>The name of the user described by this identity.</returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002904 File Offset: 0x00000B04
		public string AccountName
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the authentication level of the user described by this identity.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.AuthenticationOption" /> values.</returns>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000290C File Offset: 0x00000B0C
		public AuthenticationOption AuthenticationLevel
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the authentication service described by this identity.</summary>
		/// <returns>The authentication service described by this identity.</returns>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002914 File Offset: 0x00000B14
		public int AuthenticationService
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the impersonation level of the user described by this identity.</summary>
		/// <returns>A <see cref="T:System.EnterpriseServices.ImpersonationLevelOption" /> value.</returns>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000291C File Offset: 0x00000B1C
		public ImpersonationLevelOption ImpersonationLevel
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
