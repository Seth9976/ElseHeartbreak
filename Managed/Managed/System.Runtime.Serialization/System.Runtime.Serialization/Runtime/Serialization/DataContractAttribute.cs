using System;

namespace System.Runtime.Serialization
{
	/// <summary>Specifies that the type defines or implements a data contract and is serializable by a serializer, such as the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />. To make their type serializable, type authors must define a data contract for their type.</summary>
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
	public sealed class DataContractAttribute : Attribute
	{
		/// <summary>Gets or sets the name of the data contract for the type.</summary>
		/// <returns>The local name of a data contract. The default is the name of the class that the attribute is applied to. </returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002284 File Offset: 0x00000484
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000228C File Offset: 0x0000048C
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the namespace for the data contract for the type.</summary>
		/// <returns>The namespace of the contract. </returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002298 File Offset: 0x00000498
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000022A0 File Offset: 0x000004A0
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to preserve object reference data.</summary>
		/// <returns>true to keep object reference data using standard XML; otherwise, false. The default is false.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000022AC File Offset: 0x000004AC
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000022B4 File Offset: 0x000004B4
		public bool IsReference { get; set; }

		// Token: 0x04000028 RID: 40
		private string name;

		// Token: 0x04000029 RID: 41
		private string ns;
	}
}
