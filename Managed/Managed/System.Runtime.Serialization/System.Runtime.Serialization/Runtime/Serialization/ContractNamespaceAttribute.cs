using System;

namespace System.Runtime.Serialization
{
	/// <summary>Specifies the CLR namespace and XML namespace of the data contract. </summary>
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, Inherited = false, AllowMultiple = true)]
	public sealed class ContractNamespaceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.ContractNamespaceAttribute" /> class using the supplied namespace. </summary>
		/// <param name="contractNamespace">The namespace of the contract.</param>
		// Token: 0x06000027 RID: 39 RVA: 0x00002250 File Offset: 0x00000450
		public ContractNamespaceAttribute(string ns)
		{
			this.contract_ns = ns;
		}

		/// <summary>Gets or sets the CLR namespace of the data contract type. </summary>
		/// <returns>The CLR-legal namespace of a type. </returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002260 File Offset: 0x00000460
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002268 File Offset: 0x00000468
		public string ClrNamespace
		{
			get
			{
				return this.clr_ns;
			}
			set
			{
				this.clr_ns = value;
			}
		}

		/// <summary>Gets the namespace of the data contract members.</summary>
		/// <returns>The namespace of the data contract members.</returns>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002274 File Offset: 0x00000474
		public string ContractNamespace
		{
			get
			{
				return this.contract_ns;
			}
		}

		// Token: 0x04000026 RID: 38
		private string clr_ns;

		// Token: 0x04000027 RID: 39
		private string contract_ns;
	}
}
