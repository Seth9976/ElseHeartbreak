using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies whether components in the assembly run in the creator's process or in a system process.</summary>
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(false)]
	public sealed class ApplicationActivationAttribute : Attribute, IConfigurationAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationActivationAttribute" /> class, setting the specified <see cref="T:System.EnterpriseServices.ActivationOption" /> value.</summary>
		/// <param name="opt">One of the <see cref="T:System.EnterpriseServices.ActivationOption" /> values. </param>
		// Token: 0x06000013 RID: 19 RVA: 0x000021A8 File Offset: 0x000003A8
		public ApplicationActivationAttribute(ActivationOption opt)
		{
			this.opt = opt;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021B8 File Offset: 0x000003B8
		[MonoTODO]
		bool IConfigurationAttribute.AfterSaveChanges(Hashtable info)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021C0 File Offset: 0x000003C0
		[MonoTODO]
		bool IConfigurationAttribute.Apply(Hashtable cache)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021C8 File Offset: 0x000003C8
		bool IConfigurationAttribute.IsValidTarget(string s)
		{
			return s == "Application";
		}

		/// <summary>This property is not supported in the current version.</summary>
		/// <returns>This property is not supported in the current version.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000021D8 File Offset: 0x000003D8
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000021E0 File Offset: 0x000003E0
		public string SoapMailbox
		{
			get
			{
				return this.soapMailbox;
			}
			set
			{
				this.soapMailbox = value;
			}
		}

		/// <summary>Gets or sets a value representing a virtual root on the Web for the COM+ application.</summary>
		/// <returns>The virtual root on the Web for the COM+ application.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000021EC File Offset: 0x000003EC
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000021F4 File Offset: 0x000003F4
		public string SoapVRoot
		{
			get
			{
				return this.soapVRoot;
			}
			set
			{
				this.soapVRoot = value;
			}
		}

		/// <summary>Gets the specified <see cref="T:System.EnterpriseServices.ActivationOption" /> value.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.ActivationOption" /> values, either Library or Server.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002200 File Offset: 0x00000400
		public ActivationOption Value
		{
			get
			{
				return this.opt;
			}
		}

		// Token: 0x04000028 RID: 40
		private ActivationOption opt;

		// Token: 0x04000029 RID: 41
		private string soapMailbox;

		// Token: 0x0400002A RID: 42
		private string soapVRoot;
	}
}
