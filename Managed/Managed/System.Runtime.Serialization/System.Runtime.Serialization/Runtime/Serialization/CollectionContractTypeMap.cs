using System;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x02000027 RID: 39
	internal class CollectionContractTypeMap : CollectionTypeMap
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00005528 File Offset: 0x00003728
		public CollectionContractTypeMap(Type type, CollectionDataContractAttribute a, Type elementType, XmlQualifiedName qname, KnownTypeCollection knownTypes)
			: base(type, elementType, qname, knownTypes)
		{
			this.IsReference = a.IsReference;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005544 File Offset: 0x00003744
		internal override string CurrentNamespace
		{
			get
			{
				return base.XmlName.Namespace;
			}
		}
	}
}
