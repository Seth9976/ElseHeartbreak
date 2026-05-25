using System;
using System.Collections.ObjectModel;

namespace System.Runtime.Serialization
{
	/// <summary>Represents the options that can be set for an <see cref="T:System.Runtime.Serialization.XsdDataContractExporter" />. </summary>
	// Token: 0x02000018 RID: 24
	public class ExportOptions
	{
		/// <summary>Gets or sets a serialization surrogate. </summary>
		/// <returns>An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> interface that can be used to customize how an XML schema representation is exported for a specific type. </returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002A84 File Offset: 0x00000C84
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002A8C File Offset: 0x00000C8C
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

		/// <summary>Gets the collection of types that may be encountered during serialization or deserialization. </summary>
		/// <returns>A KnownTypes collection that contains types that may be encountered during serialization or deserialization. XML schema representations are exported for all the types specified in this collection by the <see cref="T:System.Runtime.Serialization.XsdDataContractExporter" />.</returns>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002A98 File Offset: 0x00000C98
		public Collection<Type> KnownTypes
		{
			get
			{
				return this.known_types;
			}
		}

		// Token: 0x0400003C RID: 60
		private IDataContractSurrogate surrogate;

		// Token: 0x0400003D RID: 61
		private KnownTypeCollection known_types;
	}
}
