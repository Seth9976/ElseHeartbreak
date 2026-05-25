using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies access controls to an assembly containing <see cref="T:System.EnterpriseServices.ServicedComponent" /> classes.</summary>
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(false)]
	public sealed class ApplicationAccessControlAttribute : Attribute, IConfigurationAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationAccessControlAttribute" /> class, enabling the COM+ security configuration.</summary>
		// Token: 0x06000006 RID: 6 RVA: 0x0000211C File Offset: 0x0000031C
		public ApplicationAccessControlAttribute()
		{
			this.val = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationAccessControlAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ApplicationAccessControlAttribute.Value" /> property indicating whether to enable COM security configuration.</summary>
		/// <param name="val">true to allow configuration of security; otherwise, false. </param>
		// Token: 0x06000007 RID: 7 RVA: 0x0000212C File Offset: 0x0000032C
		public ApplicationAccessControlAttribute(bool val)
		{
			this.val = val;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000213C File Offset: 0x0000033C
		bool IConfigurationAttribute.AfterSaveChanges(Hashtable info)
		{
			return false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002140 File Offset: 0x00000340
		[MonoTODO]
		bool IConfigurationAttribute.Apply(Hashtable cache)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002148 File Offset: 0x00000348
		bool IConfigurationAttribute.IsValidTarget(string s)
		{
			return s == "Application";
		}

		/// <summary>Gets or sets the access checking level to process level or to component level.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.AccessChecksLevelOption" /> values.</returns>
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002158 File Offset: 0x00000358
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002160 File Offset: 0x00000360
		public AccessChecksLevelOption AccessChecksLevel
		{
			get
			{
				return this.accessChecksLevel;
			}
			set
			{
				this.accessChecksLevel = value;
			}
		}

		/// <summary>Gets or sets the remote procedure call (RPC) authentication level.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.AuthenticationOption" /> values.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000216C File Offset: 0x0000036C
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002174 File Offset: 0x00000374
		public AuthenticationOption Authentication
		{
			get
			{
				return this.authentication;
			}
			set
			{
				this.authentication = value;
			}
		}

		/// <summary>Gets or sets the impersonation level that is allowed for calling targets of this application.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.ImpersonationLevelOption" /> values.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002180 File Offset: 0x00000380
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002188 File Offset: 0x00000388
		public ImpersonationLevelOption ImpersonationLevel
		{
			get
			{
				return this.impersonation;
			}
			set
			{
				this.impersonation = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to enable COM+ security configuration.</summary>
		/// <returns>true if COM+ security configuration is enabled; otherwise, false.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002194 File Offset: 0x00000394
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000219C File Offset: 0x0000039C
		public bool Value
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
			}
		}

		// Token: 0x04000024 RID: 36
		private AccessChecksLevelOption accessChecksLevel;

		// Token: 0x04000025 RID: 37
		private AuthenticationOption authentication;

		// Token: 0x04000026 RID: 38
		private ImpersonationLevelOption impersonation;

		// Token: 0x04000027 RID: 39
		private bool val;
	}
}
