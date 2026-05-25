using System;

namespace System.Runtime.Serialization
{
	/// <summary>When applied to a collection type, enables custom specification of the collection item elements. This attribute can be applied only to types that are recognized by the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> as valid, serializable collections.</summary>
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
	public sealed class CollectionDataContractAttribute : Attribute
	{
		/// <summary>Gets or sets the data contract name for the collection type.</summary>
		/// <returns>The data contract name for the collection type.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000021D8 File Offset: 0x000003D8
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000021E0 File Offset: 0x000003E0
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

		/// <summary>Gets or sets the namespace for the data contract.</summary>
		/// <returns>The namespace of the data contract.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000021EC File Offset: 0x000003EC
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000021F4 File Offset: 0x000003F4
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

		/// <summary>Gets or sets a custom name for a collection element.</summary>
		/// <returns>The name to apply to collection elements.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002200 File Offset: 0x00000400
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002208 File Offset: 0x00000408
		public string ItemName
		{
			get
			{
				return this.item_name;
			}
			set
			{
				this.item_name = value;
			}
		}

		/// <summary>Gets or sets the custom name for a dictionary key name.</summary>
		/// <returns>The name to use instead of the default dictionary key name.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002214 File Offset: 0x00000414
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000221C File Offset: 0x0000041C
		public string KeyName
		{
			get
			{
				return this.key_name;
			}
			set
			{
				this.key_name = value;
			}
		}

		/// <summary>Gets or sets the custom name for a dictionary value name.</summary>
		/// <returns>The name to use instead of the default dictionary value name.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002228 File Offset: 0x00000428
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002230 File Offset: 0x00000430
		public string ValueName
		{
			get
			{
				return this.value_name;
			}
			set
			{
				this.value_name = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to preserve object reference data.</summary>
		/// <returns>true to keep object reference data; otherwise, false. The default is false.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000223C File Offset: 0x0000043C
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002244 File Offset: 0x00000444
		public bool IsReference { get; set; }

		// Token: 0x0400001F RID: 31
		private string name;

		// Token: 0x04000020 RID: 32
		private string ns;

		// Token: 0x04000021 RID: 33
		private string item_name;

		// Token: 0x04000022 RID: 34
		private string key_name;

		// Token: 0x04000023 RID: 35
		private string value_name;

		// Token: 0x04000024 RID: 36
		private bool is_reference;
	}
}
