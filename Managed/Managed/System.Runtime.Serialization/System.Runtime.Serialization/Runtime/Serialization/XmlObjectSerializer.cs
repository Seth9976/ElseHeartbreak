using System;
using System.IO;
using System.Xml;

namespace System.Runtime.Serialization
{
	/// <summary>Provides the base class used to serialize objects as XML streams or documents. This class is abstract.</summary>
	/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
	/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized. </exception>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000032 RID: 50
	public abstract class XmlObjectSerializer
	{
		/// <summary>Gets a value that specifies whether the <see cref="T:System.Xml.XmlReader" /> is positioned over an XML element that can be read.</summary>
		/// <returns>true if the reader is positioned over the starting element; otherwise, false.</returns>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" /> used to read the XML stream or document.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000121 RID: 289 RVA: 0x00006F7C File Offset: 0x0000517C
		public virtual bool IsStartObject(XmlReader reader)
		{
			return this.IsStartObject(XmlDictionaryReader.CreateDictionaryReader(reader));
		}

		/// <summary>Gets a value that specifies whether the <see cref="T:System.Xml.XmlDictionaryReader" /> is positioned over an XML element that can be read.</summary>
		/// <returns>true if the reader can read the data; otherwise, false.</returns>
		/// <param name="reader">An <see cref="T:System.Xml.XmlDictionaryReader" /> used to read the XML stream or document.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000122 RID: 290
		public abstract bool IsStartObject(XmlDictionaryReader reader);

		/// <summary>Reads the XML stream or document with a <see cref="T:System.IO.Stream" /> and returns the deserialized object.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> used to read the XML stream or document.</param>
		// Token: 0x06000123 RID: 291 RVA: 0x00006F8C File Offset: 0x0000518C
		public virtual object ReadObject(Stream stream)
		{
			return this.ReadObject(XmlReader.Create(stream));
		}

		/// <summary>Reads the XML document or stream with an <see cref="T:System.Xml.XmlReader" /> and returns the deserialized object.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" /> used to read the XML stream or document.</param>
		// Token: 0x06000124 RID: 292 RVA: 0x00006F9C File Offset: 0x0000519C
		public virtual object ReadObject(XmlReader reader)
		{
			return this.ReadObject(XmlDictionaryReader.CreateDictionaryReader(reader));
		}

		/// <summary>Reads the XML document or stream with an <see cref="T:System.Xml.XmlDictionaryReader" /> and returns the deserialized object.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="reader">An <see cref="T:System.Xml.XmlDictionaryReader" /> used to read the XML document.</param>
		// Token: 0x06000125 RID: 293 RVA: 0x00006FAC File Offset: 0x000051AC
		public virtual object ReadObject(XmlDictionaryReader reader)
		{
			return this.ReadObject(reader, true);
		}

		/// <summary>Reads the XML document or stream with an <see cref="T:System.Xml.XmlReader" /> and returns the deserialized object; it also enables you to specify whether the serializer can read the data before attempting to read it.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" /> used to read the XML document or stream.</param>
		/// <param name="verifyObjectName">true to check whether the enclosing XML element name and namespace correspond to the root name and root namespace; false to skip the verification.</param>
		// Token: 0x06000126 RID: 294 RVA: 0x00006FB8 File Offset: 0x000051B8
		public virtual object ReadObject(XmlReader reader, bool readContentOnly)
		{
			return this.ReadObject(XmlDictionaryReader.CreateDictionaryReader(reader), readContentOnly);
		}

		/// <summary>Reads the XML stream or document with an <see cref="T:System.Xml.XmlDictionaryReader" /> and returns the deserialized object; it also enables you to specify whether the serializer can read the data before attempting to read it.</summary>
		/// <returns>The deserialized object.</returns>
		/// <param name="reader">An <see cref="T:System.Xml.XmlDictionaryReader" /> used to read the XML document.</param>
		/// <param name="verifyObjectName">true to check whether the enclosing XML element name and namespace correspond to the root name and root namespace; otherwise, false to skip the verification.</param>
		// Token: 0x06000127 RID: 295
		[MonoTODO]
		public abstract object ReadObject(XmlDictionaryReader reader, bool readContentOnly);

		/// <summary>Writes the complete content (start, content, and end) of the object to the XML document or stream with the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> used to write the XML document or stream.</param>
		/// <param name="graph">The object that contains the data to write to the stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000128 RID: 296 RVA: 0x00006FC8 File Offset: 0x000051C8
		public virtual void WriteObject(Stream stream, object graph)
		{
			using (XmlWriter xmlWriter = XmlDictionaryWriter.CreateTextWriter(stream))
			{
				this.WriteObject(xmlWriter, graph);
			}
		}

		/// <summary>Writes the complete content (start, content, and end) of the object to the XML document or stream with the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> used to write the XML document or stream.</param>
		/// <param name="graph">The object that contains the content to write.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x06000129 RID: 297 RVA: 0x00007014 File Offset: 0x00005214
		public virtual void WriteObject(XmlWriter writer, object graph)
		{
			this.WriteObject(XmlDictionaryWriter.CreateDictionaryWriter(writer), graph);
		}

		/// <summary>Writes the start of the object's data as an opening XML element using the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> used to write the XML document.</param>
		/// <param name="graph">The object to serialize.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600012A RID: 298 RVA: 0x00007024 File Offset: 0x00005224
		public virtual void WriteStartObject(XmlWriter writer, object graph)
		{
			this.WriteStartObject(XmlDictionaryWriter.CreateDictionaryWriter(writer), graph);
		}

		/// <summary>Writes the complete content (start, content, and end) of the object to the XML document or stream with the specified <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the content to the XML document or stream.</param>
		/// <param name="graph">The object that contains the content to write.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		// Token: 0x0600012B RID: 299 RVA: 0x00007034 File Offset: 0x00005234
		public virtual void WriteObject(XmlDictionaryWriter writer, object graph)
		{
			this.WriteStartObject(writer, graph);
			this.WriteObjectContent(writer, graph);
			this.WriteEndObject(writer);
		}

		/// <summary>Writes the start of the object's data as an opening XML element using the specified <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML document.</param>
		/// <param name="graph">The object to serialize.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600012C RID: 300
		public abstract void WriteStartObject(XmlDictionaryWriter writer, object graph);

		/// <summary>Writes only the content of the object to the XML document or stream with the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> used to write the XML document or stream.</param>
		/// <param name="graph">The object that contains the content to write.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600012D RID: 301 RVA: 0x00007050 File Offset: 0x00005250
		public virtual void WriteObjectContent(XmlWriter writer, object graph)
		{
			this.WriteObjectContent(XmlDictionaryWriter.CreateDictionaryWriter(writer), graph);
		}

		/// <summary>Writes only the content of the object to the XML document or stream using the specified <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML document or stream.</param>
		/// <param name="graph">The object that contains the content to write.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600012E RID: 302
		public abstract void WriteObjectContent(XmlDictionaryWriter writer, object graph);

		/// <summary>Writes the end of the object data as a closing XML element to the XML document or stream with an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> used to write the XML document or stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600012F RID: 303 RVA: 0x00007060 File Offset: 0x00005260
		public virtual void WriteEndObject(XmlWriter writer)
		{
			this.WriteEndObject(XmlDictionaryWriter.CreateDictionaryWriter(writer));
		}

		/// <summary>Writes the end of the object data as a closing XML element to the XML document or stream with an <see cref="T:System.Xml.XmlDictionaryWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlDictionaryWriter" /> used to write the XML document or stream.</param>
		/// <exception cref="T:System.Runtime.Serialization.InvalidDataContractException">the type being serialized does not conform to data contract rules. For example, the <see cref="T:System.Runtime.Serialization.DataContractAttribute" /> attribute has not been applied to the type.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">there is a problem with the instance being serialized.</exception>
		/// <exception cref="T:System.ServiceModel.QuotaExceededException">the maximum number of objects to serialize has been exceeded. Check the <see cref="P:System.Runtime.Serialization.DataContractSerializer.MaxItemsInObjectGraph" /> property.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000130 RID: 304
		public abstract void WriteEndObject(XmlDictionaryWriter writer);

		// Token: 0x040000A1 RID: 161
		private IDataContractSurrogate surrogate;

		// Token: 0x040000A2 RID: 162
		private SerializationBinder binder;

		// Token: 0x040000A3 RID: 163
		private ISurrogateSelector selector;

		// Token: 0x040000A4 RID: 164
		private int max_items = 65536;
	}
}
