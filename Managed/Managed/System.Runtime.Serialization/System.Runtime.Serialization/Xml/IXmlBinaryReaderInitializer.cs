using System;
using System.IO;

namespace System.Xml
{
	/// <summary>Provides methods for reinitializing a binary reader to read a new document.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000038 RID: 56
	public interface IXmlBinaryReaderInitializer
	{
		/// <summary>Reinitializes the binary reader using the given input stream.</summary>
		/// <param name="stream">The stream from which to read.</param>
		/// <param name="dictionary">
		///   <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">
		///   <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="session">
		///   <see cref="T:System.Xml.XmlBinaryReaderSession" /> to use.</param>
		/// <param name="onClose">Delegate to call when the reader is closed.</param>
		// Token: 0x06000162 RID: 354
		void SetInput(Stream stream, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quota, XmlBinaryReaderSession session, OnXmlDictionaryReaderClose onClose);

		/// <summary>Reinitializes the binary reader using the given input buffer.</summary>
		/// <param name="buffer">The buffer from which to read.</param>
		/// <param name="offset">Starting position from which to read in <paramref name="buffer" />.</param>
		/// <param name="count">Number of bytes that can be read from <paramref name="buffer" />.</param>
		/// <param name="dictionary">
		///   <see cref="T:System.Xml.XmlDictionary" /> to use.</param>
		/// <param name="quotas">
		///   <see cref="T:System.Xml.XmlDictionaryReaderQuotas" /> to apply.</param>
		/// <param name="session">
		///   <see cref="T:System.Xml.XmlBinaryReaderSession" /> to use.</param>
		/// <param name="onClose">Delegate to call when the reader is closed.</param>
		// Token: 0x06000163 RID: 355
		void SetInput(byte[] buffer, int offset, int count, IXmlDictionary dictionary, XmlDictionaryReaderQuotas quota, XmlBinaryReaderSession session, OnXmlDictionaryReaderClose onClose);
	}
}
