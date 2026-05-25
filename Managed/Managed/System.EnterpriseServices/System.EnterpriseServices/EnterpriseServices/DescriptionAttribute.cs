using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Sets the description on an assembly (application), component, method, or interface. This class cannot be inherited.</summary>
	// Token: 0x02000014 RID: 20
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface)]
	public sealed class DescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.DescriptionAttribute" /> class.</summary>
		/// <param name="desc">The description of the assembly (application), component, method, or interface. </param>
		// Token: 0x06000056 RID: 86 RVA: 0x00002474 File Offset: 0x00000674
		public DescriptionAttribute(string desc)
		{
		}
	}
}
