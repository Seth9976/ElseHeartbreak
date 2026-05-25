using System;

namespace System.Xml
{
	// Token: 0x02000054 RID: 84
	internal abstract class DummyStateXmlReader : XmlReader
	{
		// Token: 0x0600035D RID: 861 RVA: 0x00010210 File Offset: 0x0000E410
		protected DummyStateXmlReader(string baseUri, XmlNameTable nameTable, ReadState readState)
		{
			this.base_uri = baseUri;
			this.name_table = nameTable;
			this.read_state = readState;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00010230 File Offset: 0x0000E430
		public override string BaseURI
		{
			get
			{
				return this.base_uri;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00010238 File Offset: 0x0000E438
		public override bool EOF
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001023C File Offset: 0x0000E43C
		public override void Close()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00010244 File Offset: 0x0000E444
		public override bool Read()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0001024C File Offset: 0x0000E44C
		public override int AttributeCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00010250 File Offset: 0x0000E450
		public override bool IsEmptyElement
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00010254 File Offset: 0x0000E454
		public override string LocalName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0001025C File Offset: 0x0000E45C
		public override string NamespaceURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00010264 File Offset: 0x0000E464
		public override XmlNameTable NameTable
		{
			get
			{
				return this.name_table;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0001026C File Offset: 0x0000E46C
		public override string Prefix
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00010274 File Offset: 0x0000E474
		public override ReadState ReadState
		{
			get
			{
				return this.read_state;
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001027C File Offset: 0x0000E47C
		public override bool MoveToElement()
		{
			return false;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00010280 File Offset: 0x0000E480
		public override string GetAttribute(int index)
		{
			return null;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00010284 File Offset: 0x0000E484
		public override string GetAttribute(string name)
		{
			return null;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00010288 File Offset: 0x0000E488
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return null;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001028C File Offset: 0x0000E48C
		public override void MoveToAttribute(int index)
		{
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00010294 File Offset: 0x0000E494
		public override bool MoveToAttribute(string name)
		{
			return false;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00010298 File Offset: 0x0000E498
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return false;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001029C File Offset: 0x0000E49C
		public override bool MoveToFirstAttribute()
		{
			return false;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000102A0 File Offset: 0x0000E4A0
		public override bool MoveToNextAttribute()
		{
			return false;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000102A4 File Offset: 0x0000E4A4
		public override string LookupNamespace(string prefix)
		{
			return null;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000102A8 File Offset: 0x0000E4A8
		public override bool ReadAttributeValue()
		{
			return false;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000102AC File Offset: 0x0000E4AC
		public override void ResolveEntity()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04000167 RID: 359
		private string base_uri;

		// Token: 0x04000168 RID: 360
		private XmlNameTable name_table;

		// Token: 0x04000169 RID: 361
		private ReadState read_state;
	}
}
