using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
	/// <summary>Represents the options that can be set on an <see cref="T:System.Runtime.Serialization.XsdDataContractImporter" />. </summary>
	// Token: 0x0200001D RID: 29
	public class ImportOptions
	{
		/// <summary>Gets or sets a <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> instance that provides the means to check whether particular options for a target language are supported.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> that provides the means to check whether particular options for a target language are supported.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002AE4 File Offset: 0x00000CE4
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002AEC File Offset: 0x00000CEC
		public CodeDomProvider CodeProvider
		{
			get
			{
				return this.code_provider;
			}
			set
			{
				this.code_provider = value;
			}
		}

		/// <summary>Gets or sets a data contract surrogate that can be used to modify the code generated during an import operation. </summary>
		/// <returns>An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> interface that handles schema import. </returns>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002AF8 File Offset: 0x00000CF8
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002B00 File Offset: 0x00000D00
		public IDataContractSurrogate DataContractSurrogate
		{
			get
			{
				return this.surrogate;
			}
			set
			{
				this.surrogate = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether types in generated code should implement the <see cref="T:System.ComponentModel.INotifyPropertyChanged" /> interface.</summary>
		/// <returns>true if the generated code should implement the <see cref="T:System.ComponentModel.INotifyPropertyChanged" /> interface; otherwise, false. The default is false.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002B0C File Offset: 0x00000D0C
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002B14 File Offset: 0x00000D14
		public bool EnableDataBinding
		{
			get
			{
				return this.enable_data_binding;
			}
			set
			{
				this.enable_data_binding = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether generated code will be marked internal or public.</summary>
		/// <returns>true if the code will be marked internal; otherwise, false. The default is false.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002B20 File Offset: 0x00000D20
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002B28 File Offset: 0x00000D28
		public bool GenerateInternal
		{
			get
			{
				return this.generate_internal;
			}
			set
			{
				this.generate_internal = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether generated data contract classes will be marked with the <see cref="T:System.SerializableAttribute" /> attribute in addition to the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute.</summary>
		/// <returns>true to generate classes with the <see cref="T:System.SerializableAttribute" /> applied; otherwise, false. The default is false.</returns>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002B34 File Offset: 0x00000D34
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002B3C File Offset: 0x00000D3C
		public bool GenerateSerializable
		{
			get
			{
				return this.generate_serializable;
			}
			set
			{
				this.generate_serializable = value;
			}
		}

		/// <summary>Gets or sets a value that determines whether all XML schema types, even those that do not conform to a data contract schema, will be imported.</summary>
		/// <returns>true to import all schema types; otherwise, false. The default is false.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002B48 File Offset: 0x00000D48
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002B50 File Offset: 0x00000D50
		public bool ImportXmlType
		{
			get
			{
				return this.import_xml_type;
			}
			set
			{
				this.import_xml_type = value;
			}
		}

		/// <summary>Gets a dictionary that contains the mapping of data contract namespaces to the CLR namespaces that must be used to generate code during an import operation.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IDictionary`2" /> that contains the namespace mappings. </returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002B5C File Offset: 0x00000D5C
		public IDictionary<string, string> Namespaces
		{
			get
			{
				return this.namespaces;
			}
		}

		/// <summary>Gets a collection of types that represents data contract collections that should be referenced when generating code for collections, such as lists or dictionaries of items.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.ICollection`1" /> that contains the referenced collection types.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002B64 File Offset: 0x00000D64
		public ICollection<Type> ReferencedCollectionTypes
		{
			get
			{
				return this.referenced_collection_types;
			}
		}

		/// <summary>Gets a <see cref="T:System.Collections.Generic.IList`1" /> containing types referenced in generated code. </summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> that contains the referenced types. </returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002B6C File Offset: 0x00000D6C
		public ICollection<Type> ReferencedTypes
		{
			get
			{
				return this.referenced_types;
			}
		}

		// Token: 0x0400003F RID: 63
		private IDataContractSurrogate surrogate;

		// Token: 0x04000040 RID: 64
		private ICollection<Type> referenced_collection_types = new List<Type>();

		// Token: 0x04000041 RID: 65
		private ICollection<Type> referenced_types = new List<Type>();

		// Token: 0x04000042 RID: 66
		private bool enable_data_binding;

		// Token: 0x04000043 RID: 67
		private bool generate_internal;

		// Token: 0x04000044 RID: 68
		private bool generate_serializable;

		// Token: 0x04000045 RID: 69
		private bool import_xml_type;

		// Token: 0x04000046 RID: 70
		private IDictionary<string, string> namespaces = new Dictionary<string, string>();

		// Token: 0x04000047 RID: 71
		private CodeDomProvider code_provider;
	}
}
