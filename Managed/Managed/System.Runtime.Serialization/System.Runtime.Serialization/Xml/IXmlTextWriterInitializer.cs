using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	/// <summary>Specifies implementation requirements for XML text writers that derive from this interface.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200003E RID: 62
	public interface IXmlTextWriterInitializer
	{
		/// <summary>Specifies initialization requirements for XML text writers that implement this method.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="encoding">The character encoding of the stream.</param>
		/// <param name="ownsStream">If true, stream is closed by the writer when done; otherwise false.</param>
		// Token: 0x0600016D RID: 365
		void SetOutput(Stream stream, Encoding encoding, bool ownsStream);
	}
}
