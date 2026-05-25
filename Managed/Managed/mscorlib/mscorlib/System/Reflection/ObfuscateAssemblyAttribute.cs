using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Instructs obfuscation tools to use their standard obfuscation rules for the appropriate assembly type.</summary>
	// Token: 0x020002AD RID: 685
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class ObfuscateAssemblyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ObfuscateAssemblyAttribute" /> class, specifying whether the assembly to be obfuscated is public or private.</summary>
		/// <param name="assemblyIsPrivate">true if the assembly is used within the scope of one application; otherwise, false.</param>
		// Token: 0x060022E9 RID: 8937 RVA: 0x0007DA7C File Offset: 0x0007BC7C
		public ObfuscateAssemblyAttribute(bool assemblyIsPrivate)
		{
			this.strip = true;
			this.is_private = assemblyIsPrivate;
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value indicating whether the assembly was marked private.</summary>
		/// <returns>true if the assembly was marked private; otherwise, false. </returns>
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x0007DA94 File Offset: 0x0007BC94
		public bool AssemblyIsPrivate
		{
			get
			{
				return this.is_private;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should remove the attribute after processing.</summary>
		/// <returns>true if the obfuscation tool should remove the attribute after processing; otherwise, false. The default value for this property is true.</returns>
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x0007DA9C File Offset: 0x0007BC9C
		// (set) Token: 0x060022EC RID: 8940 RVA: 0x0007DAA4 File Offset: 0x0007BCA4
		public bool StripAfterObfuscation
		{
			get
			{
				return this.strip;
			}
			set
			{
				this.strip = value;
			}
		}

		// Token: 0x04000D04 RID: 3332
		private bool is_private;

		// Token: 0x04000D05 RID: 3333
		private bool strip;
	}
}
