using System;

namespace System.Runtime.Serialization
{
	/// <summary>When applied to the member of a type, specifies that the member is part of a data contract and is serializable by the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />. </summary>
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class DataMemberAttribute : Attribute
	{
		/// <summary>Gets or sets a value that specifies whether to serialize the default value for a field or property being serialized. </summary>
		/// <returns>true if the default value for a member should be generated in the serialization stream; otherwise, false. The default is true.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002A10 File Offset: 0x00000C10
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002A18 File Offset: 0x00000C18
		public bool EmitDefaultValue
		{
			get
			{
				return this.emit_default;
			}
			set
			{
				this.emit_default = value;
			}
		}

		/// <summary>Gets or sets a value that instructs the serialization engine that the member must be present when reading or deserializing.</summary>
		/// <returns>true, if the member is required; otherwise, false.</returns>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">the member is not present.</exception>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002A24 File Offset: 0x00000C24
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002A2C File Offset: 0x00000C2C
		public bool IsRequired
		{
			get
			{
				return this.is_required;
			}
			set
			{
				this.is_required = value;
			}
		}

		/// <summary>Gets or sets a data member name. </summary>
		/// <returns>The name of the data member. The default is the name of the target that the attribute is applied to. </returns>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002A38 File Offset: 0x00000C38
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002A40 File Offset: 0x00000C40
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

		/// <summary>Gets or sets the order of serialization and deserialization of a member.</summary>
		/// <returns>The numeric order of serialization or deserialization.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002A4C File Offset: 0x00000C4C
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002A54 File Offset: 0x00000C54
		public int Order
		{
			get
			{
				return this.order;
			}
			set
			{
				this.order = value;
			}
		}

		// Token: 0x04000037 RID: 55
		private bool is_required;

		// Token: 0x04000038 RID: 56
		private bool emit_default = true;

		// Token: 0x04000039 RID: 57
		private string name;

		// Token: 0x0400003A RID: 58
		private int order = -1;
	}
}
