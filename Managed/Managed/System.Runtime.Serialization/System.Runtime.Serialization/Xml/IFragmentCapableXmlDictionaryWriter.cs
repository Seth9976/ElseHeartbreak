using System;
using System.IO;

namespace System.Xml
{
	/// <summary>Contains properties and methods that when implemented by a <see cref="T:System.Xml.XmlDictionaryWriter" />, allows processing of XML fragments.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000036 RID: 54
	public interface IFragmentCapableXmlDictionaryWriter
	{
		/// <summary>Gets a value that indicates whether this <see cref="T:System.Xml.XmlDictionaryWriter" /> can process XML fragments. </summary>
		/// <returns>true if this <see cref="T:System.Xml.XmlDictionaryWriter" /> can process XML fragments; otherwise, false.</returns>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600015C RID: 348
		bool CanFragment { get; }

		/// <summary>Starts the processing of an XML fragment.</summary>
		/// <param name="stream">The stream to write to.</param>
		/// <param name="generateSelfContainedTextFragment">If true, any namespaces declared outside the fragment is declared again if used inside of it; if false the namespaces are not declared again.</param>
		// Token: 0x0600015D RID: 349
		void StartFragment(Stream stream, bool generateSelfContainedTextFragment);

		/// <summary>Writes an XML fragment to the underlying stream of the writer.</summary>
		/// <param name="buffer">The buffer to write to.</param>
		/// <param name="offset">The starting position from which to write in <paramref name="buffer" />.</param>
		/// <param name="count">The number of bytes to be written to the <paramref name="buffer" />.</param>
		// Token: 0x0600015E RID: 350
		void WriteFragment(byte[] buffer, int offset, int count);

		/// <summary>Ends the processing of an XML fragment.</summary>
		// Token: 0x0600015F RID: 351
		void EndFragment();
	}
}
