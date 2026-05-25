using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents the format to view an email message.</summary>
	// Token: 0x02000334 RID: 820
	public class AlternateView : AttachmentBase
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified file name.</summary>
		/// <param name="fileName">The name of the file that contains the content for this alternate view.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred, such as a disk error.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified file handle, such as when access is Write or ReadWrite and the file handle is set for read-only access.</exception>
		// Token: 0x06001D14 RID: 7444 RVA: 0x000564C4 File Offset: 0x000546C4
		public AlternateView(string fileName)
			: base(fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified file name and content type.</summary>
		/// <param name="fileName">The name of the file that contains the content for this alternate view.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred, such as a disk error.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified file handle, such as when access is Write or ReadWrite and the file handle is set for read-only access.</exception>
		// Token: 0x06001D15 RID: 7445 RVA: 0x000564E4 File Offset: 0x000546E4
		public AlternateView(string fileName, global::System.Net.Mime.ContentType contentType)
			: base(fileName, contentType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified file name and media type.</summary>
		/// <param name="fileName">The name of the file that contains the content for this alternate view.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred, such as a disk error.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified file handle, such as when access is Write or ReadWrite and the file handle is set for read-only access.</exception>
		// Token: 0x06001D16 RID: 7446 RVA: 0x00056508 File Offset: 0x00054708
		public AlternateView(string fileName, string mediaType)
			: base(fileName, mediaType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this view.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is null.</exception>
		// Token: 0x06001D17 RID: 7447 RVA: 0x0005652C File Offset: 0x0005472C
		public AlternateView(Stream contentStream)
			: base(contentStream)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified <see cref="T:System.IO.Stream" /> and media type.</summary>
		/// <param name="contentStream">A stream that contains the content for this attachment.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		// Token: 0x06001D18 RID: 7448 RVA: 0x00056540 File Offset: 0x00054740
		public AlternateView(Stream contentStream, string mediaType)
			: base(contentStream, mediaType)
		{
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.AlternateView" /> with the specified <see cref="T:System.IO.Stream" /> and <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this attachment.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		// Token: 0x06001D19 RID: 7449 RVA: 0x00056558 File Offset: 0x00054758
		public AlternateView(Stream contentStream, global::System.Net.Mime.ContentType contentType)
			: base(contentStream, contentType)
		{
		}

		/// <summary>Gets or sets the Base URI to use for resolving relative URIs in the <see cref="T:System.Net.Mail.AlternateView" />.</summary>
		/// <returns>A <see cref="T:System.Uri" />.</returns>
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00056570 File Offset: 0x00054770
		// (set) Token: 0x06001D1B RID: 7451 RVA: 0x00056578 File Offset: 0x00054778
		public global::System.Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
			}
		}

		/// <summary>Gets the set of embedded resources referred to by this attachment.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResourceCollection" /> object that stores the collection of linked resources to be sent as part of an e-mail message.</returns>
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x00056584 File Offset: 0x00054784
		public LinkedResourceCollection LinkedResources
		{
			get
			{
				return this.linkedResources;
			}
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.AlternateView" /> of an email message using the content specified in a <see cref="System.String" />.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.AlternateView" /> object that represents an alternate view of an email message.</returns>
		/// <param name="content">The <see cref="T:System.String" /> that contains the content of the email message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="content" /> is null.</exception>
		// Token: 0x06001D1D RID: 7453 RVA: 0x0005658C File Offset: 0x0005478C
		public static AlternateView CreateAlternateViewFromString(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException();
			}
			MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(content));
			return new AlternateView(memoryStream)
			{
				TransferEncoding = global::System.Net.Mime.TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates an <see cref="T:System.Net.Mail.AlternateView" /> of an email message using the content specified in a <see cref="System.String" /> and the specified MIME media type of the content.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.AlternateView" /> object that represents an alternate view of an email message.</returns>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="contentType">A <see cref="T:System.Net.Mime.ContentType" /> that describes the data in <paramref name="string" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="content" /> is null.</exception>
		// Token: 0x06001D1E RID: 7454 RVA: 0x000565C8 File Offset: 0x000547C8
		public static AlternateView CreateAlternateViewFromString(string content, global::System.Net.Mime.ContentType contentType)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			Encoding encoding = ((contentType.CharSet == null) ? Encoding.UTF8 : Encoding.GetEncoding(contentType.CharSet));
			MemoryStream memoryStream = new MemoryStream(encoding.GetBytes(content));
			return new AlternateView(memoryStream, contentType)
			{
				TransferEncoding = global::System.Net.Mime.TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates an <see cref="T:System.Net.Mail.AlternateView" /> of an email message using the content specified in a <see cref="System.String" />, the specified text encoding, and MIME media type of the content.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.AlternateView" /> object that represents an alternate view of an email message.</returns>
		/// <param name="content">A <see cref="T:System.String" /> that contains the content for this attachment.</param>
		/// <param name="contentEncoding">An <see cref="T:System.Text.Encoding" />. This value can be null.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="content" /> is null.</exception>
		// Token: 0x06001D1F RID: 7455 RVA: 0x00056624 File Offset: 0x00054824
		public static AlternateView CreateAlternateViewFromString(string content, Encoding encoding, string mediaType)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (encoding == null)
			{
				encoding = Encoding.UTF8;
			}
			MemoryStream memoryStream = new MemoryStream(encoding.GetBytes(content));
			return new AlternateView(memoryStream, new global::System.Net.Mime.ContentType
			{
				MediaType = mediaType,
				CharSet = encoding.HeaderName
			})
			{
				TransferEncoding = global::System.Net.Mime.TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Mail.AlternateView" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001D20 RID: 7456 RVA: 0x00056688 File Offset: 0x00054888
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (LinkedResource linkedResource in this.linkedResources)
				{
					linkedResource.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400122C RID: 4652
		private global::System.Uri baseUri;

		// Token: 0x0400122D RID: 4653
		private LinkedResourceCollection linkedResources = new LinkedResourceCollection();
	}
}
