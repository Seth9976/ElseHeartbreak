using System;

namespace System.Runtime.Serialization
{
	/// <summary>Provides a data structure to store extra data encountered by the <see cref="T:System.Runtime.Serialization.XmlObjectSerializer" /> during deserialization of a type marked with the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute. </summary>
	// Token: 0x0200001B RID: 27
	public interface IExtensibleDataObject
	{
		/// <summary>Gets or sets the structure that contains extra data.</summary>
		/// <returns>An <see cref="T:System.Runtime.Serialization.ExtensionDataObject" /> that contains data that is not recognized as belonging to the data contract.</returns>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000069 RID: 105
		// (set) Token: 0x0600006A RID: 106
		ExtensionDataObject ExtensionData { get; set; }
	}
}
