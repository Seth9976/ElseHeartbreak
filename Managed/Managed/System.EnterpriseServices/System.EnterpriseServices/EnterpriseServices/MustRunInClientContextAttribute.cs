using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Forces the attributed object to be created in the context of the creator, if possible. This class cannot be inherited.</summary>
	// Token: 0x0200002E RID: 46
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class MustRunInClientContextAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.MustRunInClientContextAttribute" /> class, requiring creation of the object in the context of the creator.</summary>
		// Token: 0x06000096 RID: 150 RVA: 0x000025D4 File Offset: 0x000007D4
		public MustRunInClientContextAttribute()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.MustRunInClientContextAttribute" /> class, optionally not creating the object in the context of the creator.</summary>
		/// <param name="val">true to create the object in the context of the creator; otherwise, false. </param>
		// Token: 0x06000097 RID: 151 RVA: 0x000025E0 File Offset: 0x000007E0
		public MustRunInClientContextAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets a value that indicates whether the attributed object is to be created in the context of the creator.</summary>
		/// <returns>true if the object is to be created in the context of the creator; otherwise, false. The default is true.</returns>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000025F0 File Offset: 0x000007F0
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x04000061 RID: 97
		private bool val;
	}
}
