using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000093 RID: 147
	internal class JsonSerializerProxy : JsonSerializer
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000725 RID: 1829 RVA: 0x0001A2B0 File Offset: 0x000184B0
		// (remove) Token: 0x06000726 RID: 1830 RVA: 0x0001A2BE File Offset: 0x000184BE
		public override event EventHandler<ErrorEventArgs> Error
		{
			add
			{
				this._serializer.Error += value;
			}
			remove
			{
				this._serializer.Error -= value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001A2CC File Offset: 0x000184CC
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001A2D9 File Offset: 0x000184D9
		public override IReferenceResolver ReferenceResolver
		{
			get
			{
				return this._serializer.ReferenceResolver;
			}
			set
			{
				this._serializer.ReferenceResolver = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001A2E7 File Offset: 0x000184E7
		public override JsonConverterCollection Converters
		{
			get
			{
				return this._serializer.Converters;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001A2F4 File Offset: 0x000184F4
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0001A301 File Offset: 0x00018501
		public override DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._serializer.DefaultValueHandling;
			}
			set
			{
				this._serializer.DefaultValueHandling = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001A30F File Offset: 0x0001850F
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x0001A31C File Offset: 0x0001851C
		public override IContractResolver ContractResolver
		{
			get
			{
				return this._serializer.ContractResolver;
			}
			set
			{
				this._serializer.ContractResolver = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001A32A File Offset: 0x0001852A
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0001A337 File Offset: 0x00018537
		public override MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._serializer.MissingMemberHandling;
			}
			set
			{
				this._serializer.MissingMemberHandling = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001A345 File Offset: 0x00018545
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0001A352 File Offset: 0x00018552
		public override NullValueHandling NullValueHandling
		{
			get
			{
				return this._serializer.NullValueHandling;
			}
			set
			{
				this._serializer.NullValueHandling = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001A360 File Offset: 0x00018560
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x0001A36D File Offset: 0x0001856D
		public override ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._serializer.ObjectCreationHandling;
			}
			set
			{
				this._serializer.ObjectCreationHandling = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001A37B File Offset: 0x0001857B
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001A388 File Offset: 0x00018588
		public override ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._serializer.ReferenceLoopHandling;
			}
			set
			{
				this._serializer.ReferenceLoopHandling = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001A396 File Offset: 0x00018596
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x0001A3A3 File Offset: 0x000185A3
		public override PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._serializer.PreserveReferencesHandling;
			}
			set
			{
				this._serializer.PreserveReferencesHandling = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001A3B1 File Offset: 0x000185B1
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x0001A3BE File Offset: 0x000185BE
		public override TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._serializer.TypeNameHandling;
			}
			set
			{
				this._serializer.TypeNameHandling = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001A3CC File Offset: 0x000185CC
		// (set) Token: 0x0600073B RID: 1851 RVA: 0x0001A3D9 File Offset: 0x000185D9
		public override FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return this._serializer.TypeNameAssemblyFormat;
			}
			set
			{
				this._serializer.TypeNameAssemblyFormat = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001A3E7 File Offset: 0x000185E7
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0001A3F4 File Offset: 0x000185F4
		public override ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._serializer.ConstructorHandling;
			}
			set
			{
				this._serializer.ConstructorHandling = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001A402 File Offset: 0x00018602
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0001A40F File Offset: 0x0001860F
		public override SerializationBinder Binder
		{
			get
			{
				return this._serializer.Binder;
			}
			set
			{
				this._serializer.Binder = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001A41D File Offset: 0x0001861D
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x0001A42A File Offset: 0x0001862A
		public override StreamingContext Context
		{
			get
			{
				return this._serializer.Context;
			}
			set
			{
				this._serializer.Context = value;
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001A438 File Offset: 0x00018638
		internal JsonSerializerInternalBase GetInternalSerializer()
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader;
			}
			return this._serializerWriter;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001A44F File Offset: 0x0001864F
		public JsonSerializerProxy(JsonSerializerInternalReader serializerReader)
		{
			ValidationUtils.ArgumentNotNull(serializerReader, "serializerReader");
			this._serializerReader = serializerReader;
			this._serializer = serializerReader.Serializer;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001A475 File Offset: 0x00018675
		public JsonSerializerProxy(JsonSerializerInternalWriter serializerWriter)
		{
			ValidationUtils.ArgumentNotNull(serializerWriter, "serializerWriter");
			this._serializerWriter = serializerWriter;
			this._serializer = serializerWriter.Serializer;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001A49B File Offset: 0x0001869B
		internal override object DeserializeInternal(JsonReader reader, Type objectType)
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader.Deserialize(reader, objectType);
			}
			return this._serializer.Deserialize(reader, objectType);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001A4C0 File Offset: 0x000186C0
		internal override void PopulateInternal(JsonReader reader, object target)
		{
			if (this._serializerReader != null)
			{
				this._serializerReader.Populate(reader, target);
				return;
			}
			this._serializer.Populate(reader, target);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001A4E5 File Offset: 0x000186E5
		internal override void SerializeInternal(JsonWriter jsonWriter, object value)
		{
			if (this._serializerWriter != null)
			{
				this._serializerWriter.Serialize(jsonWriter, value);
				return;
			}
			this._serializer.Serialize(jsonWriter, value);
		}

		// Token: 0x0400023D RID: 573
		private readonly JsonSerializerInternalReader _serializerReader;

		// Token: 0x0400023E RID: 574
		private readonly JsonSerializerInternalWriter _serializerWriter;

		// Token: 0x0400023F RID: 575
		private readonly JsonSerializer _serializer;
	}
}
