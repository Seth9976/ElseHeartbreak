using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the application ID (as a GUID) for this assembly. This class cannot be inherited.</summary>
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(false)]
	public sealed class ApplicationIDAttribute : Attribute, IConfigurationAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationIDAttribute" /> class specifying the GUID representing the application ID for the COM+ application.</summary>
		/// <param name="guid">The GUID associated with the COM+ application. </param>
		// Token: 0x0600001C RID: 28 RVA: 0x00002208 File Offset: 0x00000408
		public ApplicationIDAttribute(string guid)
		{
			this.guid = new Guid(guid);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000221C File Offset: 0x0000041C
		bool IConfigurationAttribute.AfterSaveChanges(Hashtable info)
		{
			return false;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002220 File Offset: 0x00000420
		bool IConfigurationAttribute.Apply(Hashtable cache)
		{
			return false;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002224 File Offset: 0x00000424
		bool IConfigurationAttribute.IsValidTarget(string s)
		{
			return s == "Application";
		}

		/// <summary>Gets the GUID of the COM+ application.</summary>
		/// <returns>The GUID representing the COM+ application.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002234 File Offset: 0x00000434
		public Guid Value
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x0400002B RID: 43
		private Guid guid;
	}
}
