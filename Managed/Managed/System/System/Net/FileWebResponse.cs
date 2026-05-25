using System;
using System.IO;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>Provides a file system implementation of the <see cref="T:System.Net.WebResponse" /> class.</summary>
	// Token: 0x02000306 RID: 774
	[Serializable]
	public class FileWebResponse : WebResponse, IDisposable, ISerializable
	{
		// Token: 0x06001A9E RID: 6814 RVA: 0x0004B07C File Offset: 0x0004927C
		internal FileWebResponse(global::System.Uri responseUri, FileStream fileStream)
		{
			try
			{
				this.responseUri = responseUri;
				this.fileStream = fileStream;
				this.contentLength = fileStream.Length;
				this.webHeaders = new WebHeaderCollection();
				this.webHeaders.Add("Content-Length", Convert.ToString(this.contentLength));
				this.webHeaders.Add("Content-Type", "application/octet-stream");
			}
			catch (Exception ex)
			{
				throw new WebException(ex.Message, ex);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.FileWebResponse" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information required to serialize the new <see cref="T:System.Net.FileWebResponse" /> instance. </param>
		/// <param name="streamingContext">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class that contains the source of the serialized stream associated with the new <see cref="T:System.Net.FileWebResponse" /> instance. </param>
		// Token: 0x06001A9F RID: 6815 RVA: 0x0004B118 File Offset: 0x00049318
		[Obsolete("Serialization is obsoleted for this type", false)]
		protected FileWebResponse(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.responseUri = (global::System.Uri)serializationInfo.GetValue("responseUri", typeof(global::System.Uri));
			this.contentLength = serializationInfo.GetInt64("contentLength");
			this.webHeaders = (WebHeaderCollection)serializationInfo.GetValue("webHeaders", typeof(WebHeaderCollection));
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.FileWebResponse" />.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> , which will hold the serialized data for the <see cref="T:System.Net.FileWebResponse" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> containing the destination of the serialized stream associated with the new <see cref="T:System.Net.FileWebResponse" />. </param>
		// Token: 0x06001AA0 RID: 6816 RVA: 0x0004B180 File Offset: 0x00049380
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.FileWebResponse" />.</summary>
		// Token: 0x06001AA1 RID: 6817 RVA: 0x0004B18C File Offset: 0x0004938C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets the length of the content in the file system resource.</summary>
		/// <returns>The number of bytes returned from the file system resource.</returns>
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x0004B19C File Offset: 0x0004939C
		public override long ContentLength
		{
			get
			{
				this.CheckDisposed();
				return this.contentLength;
			}
		}

		/// <summary>Gets the content type of the file system resource.</summary>
		/// <returns>The value "binary/octet-stream".</returns>
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x0004B1AC File Offset: 0x000493AC
		public override string ContentType
		{
			get
			{
				this.CheckDisposed();
				return "application/octet-stream";
			}
		}

		/// <summary>Gets a collection of header name/value pairs associated with the response.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> that contains the header name/value pairs associated with the response.</returns>
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x0004B1BC File Offset: 0x000493BC
		public override WebHeaderCollection Headers
		{
			get
			{
				this.CheckDisposed();
				return this.webHeaders;
			}
		}

		/// <summary>Gets the URI of the file system resource that provided the response.</summary>
		/// <returns>A <see cref="T:System.Uri" /> that contains the URI of the file system resource that provided the response.</returns>
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x0004B1CC File Offset: 0x000493CC
		public override global::System.Uri ResponseUri
		{
			get
			{
				this.CheckDisposed();
				return this.responseUri;
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06001AA6 RID: 6822 RVA: 0x0004B1DC File Offset: 0x000493DC
		protected override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("responseUri", this.responseUri, typeof(global::System.Uri));
			serializationInfo.AddValue("contentLength", this.contentLength);
			serializationInfo.AddValue("webHeaders", this.webHeaders, typeof(WebHeaderCollection));
		}

		/// <summary>Returns the data stream from the file system resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> for reading data from the file system resource.</returns>
		// Token: 0x06001AA7 RID: 6823 RVA: 0x0004B234 File Offset: 0x00049434
		public override Stream GetResponseStream()
		{
			this.CheckDisposed();
			return this.fileStream;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0004B244 File Offset: 0x00049444
		~FileWebResponse()
		{
			this.Dispose(false);
		}

		/// <summary>Closes the response stream.</summary>
		// Token: 0x06001AA9 RID: 6825 RVA: 0x0004B280 File Offset: 0x00049480
		public override void Close()
		{
			((IDisposable)this).Dispose();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.FileWebResponse" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001AAA RID: 6826 RVA: 0x0004B288 File Offset: 0x00049488
		private void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (disposing)
			{
				this.responseUri = null;
				this.webHeaders = null;
			}
			FileStream fileStream = this.fileStream;
			this.fileStream = null;
			if (fileStream != null)
			{
				fileStream.Close();
			}
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0004B2D8 File Offset: 0x000494D8
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x04001068 RID: 4200
		private global::System.Uri responseUri;

		// Token: 0x04001069 RID: 4201
		private FileStream fileStream;

		// Token: 0x0400106A RID: 4202
		private long contentLength;

		// Token: 0x0400106B RID: 4203
		private WebHeaderCollection webHeaders;

		// Token: 0x0400106C RID: 4204
		private bool disposed;
	}
}
