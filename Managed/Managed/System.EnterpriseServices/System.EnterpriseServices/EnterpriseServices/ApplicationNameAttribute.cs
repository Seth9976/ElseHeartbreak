using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the name of the COM+ application to be used for the install of the components in the assembly. This class cannot be inherited.</summary>
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(false)]
	public sealed class ApplicationNameAttribute : Attribute, IConfigurationAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationNameAttribute" /> class, specifying the name of the COM+ application to be used for the install of the components.</summary>
		/// <param name="name">The name of the COM+ application. </param>
		// Token: 0x06000021 RID: 33 RVA: 0x0000223C File Offset: 0x0000043C
		public ApplicationNameAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000224C File Offset: 0x0000044C
		bool IConfigurationAttribute.AfterSaveChanges(Hashtable info)
		{
			return false;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002250 File Offset: 0x00000450
		[MonoTODO]
		bool IConfigurationAttribute.Apply(Hashtable cache)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002258 File Offset: 0x00000458
		bool IConfigurationAttribute.IsValidTarget(string s)
		{
			return s == "Application";
		}

		/// <summary>Gets a value indicating the name of the COM+ application that contains the components in the assembly.</summary>
		/// <returns>The name of the COM+ application.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002268 File Offset: 0x00000468
		public string Value
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400002C RID: 44
		private string name;
	}
}
