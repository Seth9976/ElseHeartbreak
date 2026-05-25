using System;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Xml;

namespace System.Runtime.Serialization
{
	/// <summary>Serializes and deserializes an instance of a type into XML stream or document using the supplied .NET Framework types. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000022 RID: 34
	public sealed class NetDataContractSerializer : XmlObjectSerializer, IFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class. </summary>
		// Token: 0x060000A9 RID: 169 RVA: 0x000042D4 File Offset: 0x000024D4
		public NetDataContractSerializer()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class with the supplied streaming context data. </summary>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains context data.</param>
		// Token: 0x060000AA RID: 170 RVA: 0x000042E8 File Offset: 0x000024E8
		public NetDataContractSerializer(StreamingContext context)
		{
			this.context = context;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class with the supplied XML root element and namespace.</summary>
		/// <param name="rootName">The name of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">The namespace of the XML element that encloses the content to serialize or deserialize.</param>
		// Token: 0x060000AB RID: 171 RVA: 0x00004304 File Offset: 0x00002504
		public NetDataContractSerializer(string rootName, string rootNamespace)
		{
			this.FillDictionaryString(rootName, rootNamespace);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class with two parameters of type <see cref="T:System.Xml.XmlDictionaryString" /> that contain the root element and namespace used to specify the content.</summary>
		/// <param name="rootName">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the name of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the namespace of the XML element that encloses the content to serialize or deserialize.</param>
		// Token: 0x060000AC RID: 172 RVA: 0x00004320 File Offset: 0x00002520
		public NetDataContractSerializer(XmlDictionaryString rootName, XmlDictionaryString rootNamespace)
		{
			if (rootName == null)
			{
				throw new ArgumentNullException("rootName");
			}
			if (rootNamespace == null)
			{
				throw new ArgumentNullException("rootNamespace");
			}
			this.root_name = rootName;
			this.root_ns = rootNamespace;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class with the supplied context data; in addition, specifies the maximum number of items in the object to be serialized, and parameters to specify whether extra data is ignored, the assembly loading method, and a surrogate selector.</summary>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains context data.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize. </param>
		/// <param name="ignoreExtensionDataObject">true to ignore the data supplied by an extension of the type; otherwise, false.</param>
		/// <param name="assemblyFormat">A <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> enumeration value that specifies a method for locating and loading assemblies.</param>
		/// <param name="surrogateSelector">An implementation of the <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxItemsInObjectGraph" /> value is less than 0.</exception>
		// Token: 0x060000AD RID: 173 RVA: 0x00004370 File Offset: 0x00002570
		public NetDataContractSerializer(StreamingContext context, int maxItemsInObjectGraph, bool ignoreExtensibleDataObject, FormatterAssemblyStyle assemblyFormat, ISurrogateSelector surrogateSelector)
		{
			this.context = context;
			this.max_items = maxItemsInObjectGraph;
			this.ignore_extensions = ignoreExtensibleDataObject;
			this.ass_style = assemblyFormat;
			this.selector = surrogateSelector;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class with the supplied context data and root name and namespace; in addition, specifies the maximum number of items in the object to be serialized, and parameters to specify whether extra data is ignored, the assembly loading method, and a surrogate selector.</summary>
		/// <param name="rootName">The name of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">The namespace of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains context data.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize. </param>
		/// <param name="ignoreExtensionDataObject">true to ignore the data supplied by an extension of the type; otherwise, false.</param>
		/// <param name="assemblyFormat">A <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> enumeration value that specifies a method for locating and loading assemblies.</param>
		/// <param name="surrogateSelector">An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> to handle the legacy type.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxItemsInObjectGraph" /> value is less than 0.</exception>
		// Token: 0x060000AE RID: 174 RVA: 0x000043B4 File Offset: 0x000025B4
		public NetDataContractSerializer(string rootName, string rootNamespace, StreamingContext context, int maxItemsInObjectGraph, bool ignoreExtensibleDataObject, FormatterAssemblyStyle assemblyFormat, ISurrogateSelector surrogateSelector)
			: this(context, maxItemsInObjectGraph, ignoreExtensibleDataObject, assemblyFormat, surrogateSelector)
		{
			this.FillDictionaryString(rootName, rootNamespace);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.NetDataContractSerializer" /> class with the supplied context data, and root name and namespace (as <see cref="T:System.Xml.XmlDictionaryString" />  parameters); in addition, specifies the maximum number of items in the object to be serialized, and parameters to specify whether extra data found is ignored, assembly loading method, and a surrogate selector.</summary>
		/// <param name="rootName">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the root element of the content.</param>
		/// <param name="rootNamespace">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the namespace of the root element.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains context data.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize. </param>
		/// <param name="ignoreExtensionDataObject">true to ignore the data supplied by an extension of the type; otherwise, false.</param>
		/// <param name="assemblyFormat">A <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> enumeration value that specifies a method for locating and loading assemblies.</param>
		/// <param name="surrogateSelector">An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> to handle the legacy type.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxItemsInObjectGraph" /> value is less than 0.</exception>
		// Token: 0x060000AF RID: 175 RVA: 0x000043D0 File Offset: 0x000025D0
		public NetDataContractSerializer(XmlDictionaryString rootName, XmlDictionaryString rootNamespace, StreamingContext context, int maxItemsInObjectGraph, bool ignoreExtensibleDataObject, FormatterAssemblyStyle assemblyFormat, ISurrogateSelector surrogateSelector)
			: this(context, maxItemsInObjectGraph, ignoreExtensibleDataObject, assemblyFormat, surrogateSelector)
		{
			if (rootName == null)
			{
				throw new ArgumentNullException("rootName");
			}
			if (rootNamespace == null)
			{
				throw new ArgumentNullException("rootNamespace");
			}
			this.root_name = rootName;
			this.root_ns = rootNamespace;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000441C File Offset: 0x0000261C
		private void FillDictionaryString(string rootName, string rootNamespace)
		{
			if (rootName == null)
			{
				throw new ArgumentNullException("rootName");
			}
			if (rootNamespace == null)
			{
				throw new ArgumentNullException("rootNamespace");
			}
			XmlDictionary xmlDictionary = new XmlDictionary();
			this.root_name = xmlDictionary.Add(rootName);
			this.root_ns = xmlDictionary.Add(rootNamespace);
		}

		/// <summary>Gets a value that specifies a method for locating and loading assemblies.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> enumeration value that specifies a method for locating and loading assemblies.</returns>
		/// <exception cref="T:System.ArgumentException">The value being set does not correspond to any of the <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000446C File Offset: 0x0000266C
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00004474 File Offset: 0x00002674
		public FormatterAssemblyStyle AssemblyFormat
		{
			get
			{
				return this.ass_style;
			}
			set
			{
				this.ass_style = value;
			}
		}

		/// <summary>Gets or sets an object that controls class loading.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.SerializationBinder" /> used with the current formatter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004480 File Offset: 0x00002680
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00004488 File Offset: 0x00002688
		public SerializationBinder Binder
		{
			get
			{
				return this.binder;
			}
			set
			{
				this.binder = value;
			}
		}

		/// <summary>Gets a value that specifies whether data supplied by an extension of the object is ignored.</summary>
		/// <returns>true to ignore the data supplied by an extension of the type; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004494 File Offset: 0x00002694
		public bool IgnoreExtensionDataObject
		{
			get
			{
				return this.ignore_extensions;
			}
		}

		/// <summary>Gets or sets an object that assists the formatter when selecting a surrogate for serialization.</summary>
		/// <returns>An <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> for selecting a surrogate.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000449C File Offset: 0x0000269C
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000044A4 File Offset: 0x000026A4
		public ISurrogateSelector SurrogateSelector
		{
			get
			{
				return this.selector;
			}
			set
			{
				this.selector = value;
			}
		}

		/// <summary>Gets or sets the object that enables the passing of context data that is useful while serializing or deserializing.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the context data.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000044B0 File Offset: 0x000026B0
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000044B8 File Offset: 0x000026B8
		public StreamingContext Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		/// <summary>Gets the maximum number of items allowed in the object to be serialized.</summary>
		/// <returns>The maximum number of items allowed in the object. The default is <see cref="F:System.Int32.MaxValue" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000044C4 File Offset: 0x000026C4
		public int MaxItemsInObjectGraph
		{
			get
			{
				return this.max_items;
			}
		}

		/// <summary>Deserializes an XML document or stream into an object.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the XML to deserialize.</param>
		// Token: 0x060000BB RID: 187 RVA: 0x000044CC File Offset: 0x000026CC
		public object Deserialize(Stream stream)
		{
			return this.ReadObject(stream);
		}

		/// <summary>Determines whether the <see cref="T:System.Xml.XmlDictionaryReader" /> is positioned on an object that can be deserialized using the specified reader.</summary>
		/// <returns>true, if the reader is at the start element of the stream to read; otherwise, false.</returns>
		/// <param name="reader">An <see cref="T:System.Xml.XmlDictionaryReader" /> that contains the XML to read.</param>
		/// <exception cref="T:System.ArgumentNullException">the <paramref name="reader" /> is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000BC RID: 188 RVA: 0x000044D8 File Offset: 0x000026D8
		[MonoTODO]
		public override bool IsStartObject(XmlDictionaryReader reader)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads the XML stream or document with an <see cref="T:System.Xml.XmlDictionaryReader" /> and returns the deserialized object; also checks whether the object data conforms to the name and namespace used to create the serializer.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlDictionaryReader" /> used to read the XML stream or document.</param>
		/// <param name="verifyObjectName">true to check whether the enclosing XML element name and namespace correspond to the root name and root namespace used to construct the serializer; false to skip the verification.</param>
		/// <exception cref="T:System.ArgumentNullException">the <paramref name="reader" /> is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000BD RID: 189 RVA: 0x000044E0 File Offset: 0x000026E0
		public override object ReadObject(XmlDictionaryReader reader, bool readContentOnly)
		{
			throw new NotImplementedException();
		}

		/// <summary>Serializes the specified object graph using the specified writer.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> to serialize with.</param>
		/// <param name="graph">The object to serialize. All child objects of this root object are automatically serialized.</param>
		// Token: 0x060000BE RID: 190 RVA: 0x000044E8 File Offset: 0x000026E8
		public void Serialize(Stream stream, object graph)
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(stream))
			{
				this.WriteObject(xmlWriter, graph);
			}
		}

		/// <summary>Writes the XML content using an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML content.</param>
		/// <param name="graph">The object to serialize. All child objects of this root object are automatically serialized.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of object to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000BF RID: 191 RVA: 0x00004534 File Offset: 0x00002734
		[MonoTODO("support arrays; support Serializable; support SharedType; use DataContractSurrogate")]
		public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes the opening XML element using an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML element.</param>
		/// <param name="graph">The object to serialize. All child objects of this root object are automatically serialized.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of object to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000C0 RID: 192 RVA: 0x0000453C File Offset: 0x0000273C
		public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes the closing XML element using an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML document or stream.</param>
		/// <exception cref="T:System.ArgumentNullException">the <paramref name="writer" /> is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060000C1 RID: 193 RVA: 0x00004544 File Offset: 0x00002744
		public override void WriteEndObject(XmlDictionaryWriter writer)
		{
			writer.WriteEndElement();
		}

		// Token: 0x04000067 RID: 103
		private const string xmlns = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000068 RID: 104
		private const string default_ns = "http://schemas.datacontract.org/2004/07/";

		// Token: 0x04000069 RID: 105
		private StreamingContext context;

		// Token: 0x0400006A RID: 106
		private SerializationBinder binder;

		// Token: 0x0400006B RID: 107
		private ISurrogateSelector selector;

		// Token: 0x0400006C RID: 108
		private int max_items = 65536;

		// Token: 0x0400006D RID: 109
		private bool ignore_extensions;

		// Token: 0x0400006E RID: 110
		private FormatterAssemblyStyle ass_style;

		// Token: 0x0400006F RID: 111
		private XmlDictionaryString root_name;

		// Token: 0x04000070 RID: 112
		private XmlDictionaryString root_ns;
	}
}
