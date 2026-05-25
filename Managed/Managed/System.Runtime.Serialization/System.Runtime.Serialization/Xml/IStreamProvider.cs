using System;
using System.IO;

namespace System.Xml
{
	/// <summary>An interface that can be implemented by classes providing streams.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000037 RID: 55
	public interface IStreamProvider
	{
		/// <summary>Gets a stream.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" />.</returns>
		// Token: 0x06000160 RID: 352
		Stream GetStream();

		/// <summary>Releases a stream to output.</summary>
		/// <param name="stream">The stream being released.</param>
		// Token: 0x06000161 RID: 353
		void ReleaseStream(Stream stream);
	}
}
