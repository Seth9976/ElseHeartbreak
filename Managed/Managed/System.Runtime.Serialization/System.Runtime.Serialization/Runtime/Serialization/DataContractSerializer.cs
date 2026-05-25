using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace System.Runtime.Serialization
{
	/// <summary>Serializes and deserializes an instance of a type into an XML stream or document using a supplied data contract. This class cannot be inherited. </summary>
	// Token: 0x02000015 RID: 21
	public sealed class DataContractSerializer : XmlObjectSerializer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		// Token: 0x06000032 RID: 50 RVA: 0x000022C0 File Offset: 0x000004C0
		public DataContractSerializer(Type type)
			: this(type, Type.EmptyTypes)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type, and a collection of known types that may be present in the object graph.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="knownTypes">An <see cref="T:System.Collections.Generic.IEnumerable`1" />  of <see cref="T:System.Type" /> that contains the types that may be present in the object graph.</param>
		// Token: 0x06000033 RID: 51 RVA: 0x000022D0 File Offset: 0x000004D0
		public DataContractSerializer(Type type, IEnumerable<Type> knownTypes)
		{
			this.max_items = 65536;
			base..ctor();
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.type = type;
			this.known_types = new KnownTypeCollection();
			this.PopulateTypes(knownTypes);
			this.known_types.TryRegister(type);
			XmlQualifiedName qname = this.known_types.GetQName(type);
			this.FillDictionaryString(qname.Name, qname.Namespace);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type using the supplied XML root element and namespace.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">The name of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">The namespace of the XML element that encloses the content to serialize or deserialize.</param>
		// Token: 0x06000034 RID: 52 RVA: 0x00002344 File Offset: 0x00000544
		public DataContractSerializer(Type type, string rootName, string rootNamespace)
			: this(type, rootName, rootNamespace, Type.EmptyTypes)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type using the XML root element and namespace specified through the parameters of type <see cref="T:System.Xml.XmlDictionaryString" />.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the root element name of the content.</param>
		/// <param name="rootNamespace">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the namespace of the root element.</param>
		// Token: 0x06000035 RID: 53 RVA: 0x00002354 File Offset: 0x00000554
		public DataContractSerializer(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace)
			: this(type, rootName, rootNamespace, Type.EmptyTypes)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type. This method also specifies the root XML element and namespace in two string parameters as well as a list of known types that may be present in the object graph.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">The root element name of the content.</param>
		/// <param name="rootNamespace">The namespace of the root element.</param>
		/// <param name="knownTypes">An <see cref="T:System.Collections.Generic.IEnumerable`1" />  of <see cref="T:System.Type" /> that contains the types that may be present in the object graph.</param>
		// Token: 0x06000036 RID: 54 RVA: 0x00002364 File Offset: 0x00000564
		public DataContractSerializer(Type type, string rootName, string rootNamespace, IEnumerable<Type> knownTypes)
		{
			this.max_items = 65536;
			base..ctor();
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (rootName == null)
			{
				throw new ArgumentNullException("rootName");
			}
			if (rootNamespace == null)
			{
				throw new ArgumentNullException("rootNamespace");
			}
			this.type = type;
			this.PopulateTypes(knownTypes);
			this.FillDictionaryString(rootName, rootNamespace);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type. This method also specifies the root XML element and namespace in two <see cref="T:System.Xml.XmlDictionaryString" /> parameters as well as a list of known types that may be present in the object graph.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the root element name of the content.</param>
		/// <param name="rootNamespace">An <see cref="T:System.Xml.XmlDictionaryString" /> that contains the namespace of the root element.</param>
		/// <param name="knownTypes">A <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Type" /> that contains the known types that may be present in the object graph.</param>
		// Token: 0x06000037 RID: 55 RVA: 0x000023CC File Offset: 0x000005CC
		public DataContractSerializer(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace, IEnumerable<Type> knownTypes)
		{
			this.max_items = 65536;
			base..ctor();
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (rootName == null)
			{
				throw new ArgumentNullException("rootName");
			}
			if (rootNamespace == null)
			{
				throw new ArgumentNullException("rootNamespace");
			}
			this.type = type;
			this.PopulateTypes(knownTypes);
			this.root_name = rootName;
			this.root_ns = rootNamespace;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type. This method also specifies a list of known types that may be present in the object graph, the maximum number of graph items to serialize, parameters to ignore unexpected data, whether to use non-standard XML constructs to preserve object reference data in the graph, and a surrogate for custom serialization.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="knownTypes">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Type" /> that contains the known types that may be present in the object graph.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize. The default is the value returned by the <see cref="F:System.Int32.MaxValue" /> property.</param>
		/// <param name="ignoreExtensionDataObject">true to ignore the data supplied by an extension of the type upon serialization and deserialization; otherwise, false.</param>
		/// <param name="preserveObjectReferences">true to use non-standard XML constructs to preserve object reference data; otherwise, false.</param>
		/// <param name="dataContractSurrogate">An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> to customize the serialization process.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of items exceeds the maximum value.</exception>
		// Token: 0x06000038 RID: 56 RVA: 0x0000243C File Offset: 0x0000063C
		public DataContractSerializer(Type type, IEnumerable<Type> knownTypes, int maxObjectsInGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate)
			: this(type, knownTypes)
		{
			this.Initialize(maxObjectsInGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type. This method also specifies a list of known types that may be present in the object graph, the maximum number of graph items to serialize, parameters to ignore unexpected data, whether to use non-standard XML constructs to preserve object reference data in the graph, a surrogate for custom serialization, and the XML element and namespace that contain the content.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">The XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">The namespace of the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="knownTypes">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Type" /> that contains the known types that may be present in the object graph.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize.</param>
		/// <param name="ignoreExtensionDataObject">true to ignore the data supplied by an extension of the type upon serialization and deserialization; otherwise, false.</param>
		/// <param name="preserveObjectReferences">true to use non-standard XML constructs to preserve object reference data; otherwise, false.</param>
		/// <param name="dataContractSurrogate">An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> to customize the serialization process.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of items exceeds the maximum value.</exception>
		// Token: 0x06000039 RID: 57 RVA: 0x00002454 File Offset: 0x00000654
		public DataContractSerializer(Type type, string rootName, string rootNamespace, IEnumerable<Type> knownTypes, int maxObjectsInGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate)
			: this(type, rootName, rootNamespace, knownTypes)
		{
			this.Initialize(maxObjectsInGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> class to serialize or deserialize an object of the specified type. This method also specifies a list of known types that may be present in the object graph, the maximum number of graph items to serialize, parameters to ignore unexpected data, whether to use non-standard XML constructs to preserve object reference data in the graph, a surrogate for custom serialization, and parameters of <see cref="T:System.Xml.XmlDictionaryString" /> that specify the XML element and namespace that contain the content.</summary>
		/// <param name="type">The type of the instances that are serialized or deserialized.</param>
		/// <param name="rootName">The <see cref="T:System.Xml.XmlDictionaryString" /> that specifies the XML element that encloses the content to serialize or deserialize.</param>
		/// <param name="rootNamespace">The <see cref="T:System.Xml.XmlDictionaryString" /> that specifies the XML namespace of the root.</param>
		/// <param name="knownTypes">A <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Type" /> that contains the known types that may be present in the object graph.</param>
		/// <param name="maxItemsInObjectGraph">The maximum number of items in the graph to serialize or deserialize.</param>
		/// <param name="ignoreExtensionDataObject">true to ignore the data supplied by an extension of the type upon serialization and deserialization; otherwise, false.</param>
		/// <param name="preserveObjectReferences">true to use non-standard XML constructs to preserve object reference data; otherwise, false.</param>
		/// <param name="dataContractSurrogate">An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> to customize the serialization process.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of items exceeds the maximum value.</exception>
		// Token: 0x0600003A RID: 58 RVA: 0x00002470 File Offset: 0x00000670
		public DataContractSerializer(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace, IEnumerable<Type> knownTypes, int maxObjectsInGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate)
			: this(type, rootName, rootNamespace, knownTypes)
		{
			this.Initialize(maxObjectsInGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000248C File Offset: 0x0000068C
		private void PopulateTypes(IEnumerable<Type> knownTypes)
		{
			if (this.known_types == null)
			{
				this.known_types = new KnownTypeCollection();
			}
			if (knownTypes != null)
			{
				foreach (Type type in knownTypes)
				{
					this.known_types.TryRegister(type);
				}
			}
			Type elementType = this.type;
			if (this.type.HasElementType)
			{
				elementType = this.type.GetElementType();
			}
			foreach (KnownTypeAttribute knownTypeAttribute in elementType.GetCustomAttributes(typeof(KnownTypeAttribute), true))
			{
				this.known_types.TryRegister(knownTypeAttribute.Type);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002578 File Offset: 0x00000778
		private void FillDictionaryString(string name, string ns)
		{
			XmlDictionary xmlDictionary = new XmlDictionary();
			this.root_name = xmlDictionary.Add(name);
			this.root_ns = xmlDictionary.Add(ns);
			this.names_filled = true;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000025AC File Offset: 0x000007AC
		private void Initialize(int maxObjectsInGraph, bool ignoreExtensionDataObject, bool preserveObjectReferences, IDataContractSurrogate dataContractSurrogate)
		{
			if (maxObjectsInGraph < 0)
			{
				throw new ArgumentOutOfRangeException("maxObjectsInGraph must not be negative.");
			}
			this.max_items = maxObjectsInGraph;
			this.ignore_ext = ignoreExtensionDataObject;
			this.preserve_refs = preserveObjectReferences;
			this.surrogate = dataContractSurrogate;
			this.PopulateTypes(Type.EmptyTypes);
		}

		/// <summary>Gets a value that specifies whether to ignore data supplied by an extension of the class when the class is being serialized or deserialized.</summary>
		/// <returns>true to omit the extension data; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000025F4 File Offset: 0x000007F4
		public bool IgnoreExtensionDataObject
		{
			get
			{
				return this.ignore_ext;
			}
		}

		/// <summary>Gets a collection of types that may be present in the object graph serialized using this instance of the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> that contains the expected types passed in as known types to the <see cref="T:System.Runtime.Serialization.DataContractSerializer" /> constructor.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000025FC File Offset: 0x000007FC
		public ReadOnlyCollection<Type> KnownTypes
		{
			get
			{
				return this.known_runtime_types;
			}
		}

		/// <summary>Gets a surrogate type that can extend the serialization or deserialization process.</summary>
		/// <returns>An implementation of the <see cref="T:System.Runtime.Serialization.IDataContractSurrogate" /> class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002604 File Offset: 0x00000804
		public IDataContractSurrogate DataContractSurrogate
		{
			get
			{
				return this.surrogate;
			}
		}

		/// <summary>Gets the maximum number of items in an object graph to serialize or deserialize.</summary>
		/// <returns>The maximum number of items to serialize or deserialize. The default is <see cref="F:System.Int32.MaxValue" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of items exceeds the maximum value.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000260C File Offset: 0x0000080C
		public int MaxItemsInObjectGraph
		{
			get
			{
				return this.max_items;
			}
		}

		/// <summary>Gets a value that specifies whether to use non-standard XML constructs to preserve object reference data.</summary>
		/// <returns>true to keep the references; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002614 File Offset: 0x00000814
		public bool PreserveObjectReferences
		{
			get
			{
				return this.preserve_refs;
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Xml.XmlDictionaryReader" /> is positioned on an object that can be deserialized.</summary>
		/// <returns>true if the reader is at the start element of the stream to read; otherwise, false.</returns>
		/// <param name="reader">An <see cref="T:System.Xml.XmlDictionaryReader" /> used to read the XML stream.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000043 RID: 67 RVA: 0x0000261C File Offset: 0x0000081C
		[MonoTODO]
		public override bool IsStartObject(XmlDictionaryReader reader)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the <see cref="T:System.Xml.XmlReader" /> is positioned on an object that can be deserialized.</summary>
		/// <returns>true if the reader is at the start element of the stream to read; otherwise, false.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> used to read the XML stream.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000044 RID: 68 RVA: 0x00002624 File Offset: 0x00000824
		public override bool IsStartObject(XmlReader reader)
		{
			return this.IsStartObject(XmlDictionaryReader.CreateDictionaryReader(reader));
		}

		/// <summary>Reads the XML stream with an <see cref="T:System.Xml.XmlReader" /> and returns the deserialized object.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> used to read the XML stream.</param>
		// Token: 0x06000045 RID: 69 RVA: 0x00002634 File Offset: 0x00000834
		public override object ReadObject(XmlReader reader)
		{
			return this.ReadObject(XmlDictionaryReader.CreateDictionaryReader(reader));
		}

		/// <summary>Reads the XML stream with an <see cref="T:System.Xml.XmlReader" /> and returns the deserialized object, and also specifies whether a check is made to verify the object name before reading its value.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> used to read the XML stream.</param>
		/// <param name="verifyObjectName">true to check whether the name of the object corresponds to the root name value supplied in the constructor; otherwise, false.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="verifyObjectName" /> parameter is set to true, and the element name and namespace do not correspond to the values set in the constructor. </exception>
		// Token: 0x06000046 RID: 70 RVA: 0x00002644 File Offset: 0x00000844
		public override object ReadObject(XmlReader reader, bool verifyObjectName)
		{
			return this.ReadObject(XmlDictionaryReader.CreateDictionaryReader(reader), verifyObjectName);
		}

		/// <summary>Reads the XML stream with an <see cref="T:System.Xml.XmlDictionaryReader" /> and returns the deserialized object, and also specifies whether a check is made to verify the object name before reading its value. </summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlDictionaryReader" /> used to read the XML stream.</param>
		/// <param name="verifyObjectName">true to check whether the name of the object corresponds to the root name value supplied in the constructor; otherwise, false. </param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <paramref name="verifyObjectName" /> parameter is set to true, and the element name and namespace do not correspond to the values set in the constructor. </exception>
		// Token: 0x06000047 RID: 71 RVA: 0x00002654 File Offset: 0x00000854
		[MonoTODO]
		public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
		{
			int count = this.known_types.Count;
			this.known_types.Add(this.type);
			bool isEmptyElement = reader.IsEmptyElement;
			object obj = XmlFormatterDeserializer.Deserialize(reader, this.type, this.known_types, this.surrogate, this.root_name.Value, this.root_ns.Value, verifyObjectName);
			while (this.known_types.Count > count)
			{
				this.known_types.RemoveAt(count);
			}
			return obj;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000026D8 File Offset: 0x000008D8
		private void ReadRootStartElement(XmlReader reader, Type type)
		{
			SerializationMap serializationMap = this.known_types.FindUserMap(type);
			XmlQualifiedName xmlQualifiedName = ((serializationMap == null) ? KnownTypeCollection.GetPredefinedTypeName(type) : serializationMap.XmlName);
			reader.MoveToContent();
			reader.ReadStartElement(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
			reader.Read();
		}

		/// <summary>Writes all the object data (starting XML element, content, and closing element) to an XML document or stream with an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the XML document or stream.</param>
		/// <param name="graph">The object that contains the data to write to the stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">The type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">There is a problem with the instance being written.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">The maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000049 RID: 73 RVA: 0x0000272C File Offset: 0x0000092C
		public override void WriteObject(XmlWriter writer, object graph)
		{
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(writer);
			this.WriteObject(xmlDictionaryWriter, graph);
		}

		/// <summary>Writes the XML content using an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the stream.</param>
		/// <param name="graph">The object to write to the stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">The type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">There is a problem with the instance being written.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">The maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600004A RID: 74 RVA: 0x00002748 File Offset: 0x00000948
		[MonoTODO("support arrays; support Serializable; support SharedType; use DataContractSurrogate")]
		public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
		{
			if (graph == null)
			{
				return;
			}
			int count = this.known_types.Count;
			XmlFormatterSerializer.Serialize(writer, graph, this.known_types, this.ignore_ext, this.max_items, this.root_ns.Value);
			while (this.known_types.Count > count)
			{
				this.known_types.RemoveAt(count);
			}
		}

		/// <summary>Writes the XML content using an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the stream.</param>
		/// <param name="graph">The object to write to the stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">The type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">There is a problem with the instance being written.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">The maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600004B RID: 75 RVA: 0x000027B0 File Offset: 0x000009B0
		public override void WriteObjectContent(XmlWriter writer, object graph)
		{
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateDictionaryWriter(writer);
			this.WriteObjectContent(xmlDictionaryWriter, graph);
		}

		/// <summary>Writes the opening XML element using an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the XML start element.</param>
		/// <param name="graph">The object to write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600004C RID: 76 RVA: 0x000027CC File Offset: 0x000009CC
		public override void WriteStartObject(XmlWriter writer, object graph)
		{
			this.WriteStartObject(XmlDictionaryWriter.CreateDictionaryWriter(writer), graph);
		}

		/// <summary>Writes the opening XML element using an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML start element.</param>
		/// <param name="graph">The object to write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600004D RID: 77 RVA: 0x000027DC File Offset: 0x000009DC
		public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
		{
			Type type = this.type;
			if (this.root_name.Value == string.Empty)
			{
				throw new InvalidDataContractException("Type '" + this.type.ToString() + "' cannot have a DataContract attribute Name set to null or empty string.");
			}
			if (graph == null)
			{
				if (this.names_filled)
				{
					writer.WriteStartElement(this.root_name.Value, this.root_ns.Value);
				}
				else
				{
					writer.WriteStartElement(this.root_name, this.root_ns);
				}
				writer.WriteAttributeString("i", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			XmlQualifiedName qname = this.known_types.GetQName(type);
			XmlQualifiedName qname2 = this.known_types.GetQName(graph.GetType());
			this.known_types.Add(graph.GetType());
			if (this.names_filled)
			{
				writer.WriteStartElement(this.root_name.Value, this.root_ns.Value);
			}
			else
			{
				writer.WriteStartElement(this.root_name, this.root_ns);
			}
			if (this.root_ns.Value != qname.Namespace && qname.Namespace != "http://schemas.microsoft.com/2003/10/Serialization/")
			{
				writer.WriteXmlnsAttribute(null, qname.Namespace);
			}
			if (qname == qname2)
			{
				if (qname.Namespace != "http://schemas.microsoft.com/2003/10/Serialization/" && !type.IsEnum)
				{
					writer.WriteXmlnsAttribute("i", "http://www.w3.org/2001/XMLSchema-instance");
				}
				return;
			}
			this.known_types.Add(type);
			XmlQualifiedName xmlQualifiedName = KnownTypeCollection.GetPredefinedTypeName(graph.GetType());
			if (xmlQualifiedName == XmlQualifiedName.Empty)
			{
				xmlQualifiedName = qname2;
			}
			else
			{
				xmlQualifiedName = new XmlQualifiedName(xmlQualifiedName.Name, "http://www.w3.org/2001/XMLSchema");
			}
			writer.WriteStartAttribute("i", "type", "http://www.w3.org/2001/XMLSchema-instance");
			writer.WriteQualifiedName(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
			writer.WriteEndAttribute();
		}

		/// <summary>Writes the closing XML element using an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">The type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">There is a problem with the instance being written.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">The maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600004E RID: 78 RVA: 0x000029E0 File Offset: 0x00000BE0
		public override void WriteEndObject(XmlDictionaryWriter writer)
		{
			writer.WriteEndElement();
		}

		/// <summary>Writes the closing XML element using an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">The type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">There is a problem with the instance being written.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">The maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600004F RID: 79 RVA: 0x000029E8 File Offset: 0x00000BE8
		public override void WriteEndObject(XmlWriter writer)
		{
			this.WriteEndObject(XmlDictionaryWriter.CreateDictionaryWriter(writer));
		}

		// Token: 0x0400002B RID: 43
		private const string xmlns = "http://www.w3.org/2000/xmlns/";

		// Token: 0x0400002C RID: 44
		private Type type;

		// Token: 0x0400002D RID: 45
		private bool ignore_ext;

		// Token: 0x0400002E RID: 46
		private bool preserve_refs;

		// Token: 0x0400002F RID: 47
		private StreamingContext context;

		// Token: 0x04000030 RID: 48
		private ReadOnlyCollection<Type> known_runtime_types;

		// Token: 0x04000031 RID: 49
		private KnownTypeCollection known_types;

		// Token: 0x04000032 RID: 50
		private IDataContractSurrogate surrogate;

		// Token: 0x04000033 RID: 51
		private int max_items;

		// Token: 0x04000034 RID: 52
		private bool names_filled;

		// Token: 0x04000035 RID: 53
		private XmlDictionaryString root_name;

		// Token: 0x04000036 RID: 54
		private XmlDictionaryString root_ns;
	}
}
