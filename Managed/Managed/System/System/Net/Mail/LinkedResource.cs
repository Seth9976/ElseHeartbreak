using System;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an embedded external resource in an email attachment, such as an image in an HTML attachment.</summary>
	// Token: 0x0200033B RID: 827
	public class LinkedResource : AttachmentBase
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> using the specified file name.</summary>
		/// <param name="fileName">The file name holding the content for this embedded resource.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		// Token: 0x06001D4F RID: 7503 RVA: 0x00058E1C File Offset: 0x0005701C
		public LinkedResource(string fileName)
			: base(fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the specified file name and content type.</summary>
		/// <param name="fileName">The file name that holds the content for this embedded resource.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		// Token: 0x06001D50 RID: 7504 RVA: 0x00058E34 File Offset: 0x00057034
		public LinkedResource(string fileName, global::System.Net.Mime.ContentType contentType)
			: base(fileName, contentType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the specified file name and media type.</summary>
		/// <param name="fileName">The file name that holds the content for this embedded resource.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		// Token: 0x06001D51 RID: 7505 RVA: 0x00058E4C File Offset: 0x0005704C
		public LinkedResource(string fileName, string mediaType)
			: base(fileName, mediaType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> using the supplied <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this embedded resource.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is null.</exception>
		// Token: 0x06001D52 RID: 7506 RVA: 0x00058E64 File Offset: 0x00057064
		public LinkedResource(Stream contentStream)
			: base(contentStream)
		{
			if (contentStream == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the values supplied by <see cref="T:System.IO.Stream" /> and <see cref="T:System.Net.Mime.ContentType" />.</summary>
		/// <param name="contentStream">A stream that contains the content for this embedded resource.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is not a valid value.</exception>
		// Token: 0x06001D53 RID: 7507 RVA: 0x00058E7C File Offset: 0x0005707C
		public LinkedResource(Stream contentStream, global::System.Net.Mime.ContentType contentType)
			: base(contentStream, contentType)
		{
			if (contentStream == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Net.Mail.LinkedResource" /> with the specified <see cref="T:System.IO.Stream" /> and media type.</summary>
		/// <param name="contentStream">A stream that contains the content for this embedded resource.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentStream" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="mediaType" /> is not a valid value.</exception>
		// Token: 0x06001D54 RID: 7508 RVA: 0x00058E94 File Offset: 0x00057094
		public LinkedResource(Stream contentStream, string mediaType)
			: base(contentStream, mediaType)
		{
			if (contentStream == null)
			{
				throw new ArgumentNullException();
			}
		}

		/// <summary>Gets or sets a URI that the resource must match.</summary>
		/// <returns>If <see cref="P:System.Net.Mail.LinkedResource.ContentLink" /> is a relative URI, the recipient of the message must resolve it.</returns>
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x00058EAC File Offset: 0x000570AC
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x00058EB4 File Offset: 0x000570B4
		public global::System.Uri ContentLink
		{
			get
			{
				return this.contentLink;
			}
			set
			{
				this.contentLink = value;
			}
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.LinkedResource" /> object from a string to be included in an email attachment as an embedded resource. The default media type is plain text, and the default content type is ASCII.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResource" /> object that contains the embedded resource to be included in the email attachment.</returns>
		/// <param name="content">A string that contains the embedded resource to be included in the email attachment.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified content string is null.</exception>
		// Token: 0x06001D57 RID: 7511 RVA: 0x00058EC0 File Offset: 0x000570C0
		public static LinkedResource CreateLinkedResourceFromString(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException();
			}
			MemoryStream memoryStream = new MemoryStream(Encoding.Default.GetBytes(content));
			return new LinkedResource(memoryStream)
			{
				TransferEncoding = global::System.Net.Mime.TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.LinkedResource" /> object from a string to be included in an email attachment as an embedded resource, with the specified content type, and media type as plain text.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResource" /> object that contains the embedded resource to be included in the email attachment.</returns>
		/// <param name="content">A string that contains the embedded resource to be included in the email attachment.</param>
		/// <param name="contentType">The type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified content string is null.</exception>
		// Token: 0x06001D58 RID: 7512 RVA: 0x00058EFC File Offset: 0x000570FC
		public static LinkedResource CreateLinkedResourceFromString(string content, global::System.Net.Mime.ContentType contentType)
		{
			if (content == null)
			{
				throw new ArgumentNullException();
			}
			MemoryStream memoryStream = new MemoryStream(Encoding.Default.GetBytes(content));
			return new LinkedResource(memoryStream, contentType)
			{
				TransferEncoding = global::System.Net.Mime.TransferEncoding.QuotedPrintable
			};
		}

		/// <summary>Creates a <see cref="T:System.Net.Mail.LinkedResource" /> object from a string to be included in an email attachment as an embedded resource, with the specified content type, and media type.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.LinkedResource" /> object that contains the embedded resource to be included in the email attachment.</returns>
		/// <param name="content">A string that contains the embedded resource to be included in the email attachment.</param>
		/// <param name="contentEncoding">The type of the content.</param>
		/// <param name="mediaType">The MIME media type of the content.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified content string is null.</exception>
		// Token: 0x06001D59 RID: 7513 RVA: 0x00058F38 File Offset: 0x00057138
		public static LinkedResource CreateLinkedResourceFromString(string content, Encoding contentEncoding, string mediaType)
		{
			if (content == null)
			{
				throw new ArgumentNullException();
			}
			MemoryStream memoryStream = new MemoryStream(contentEncoding.GetBytes(content));
			return new LinkedResource(memoryStream, mediaType)
			{
				TransferEncoding = global::System.Net.Mime.TransferEncoding.QuotedPrintable
			};
		}

		// Token: 0x0400123B RID: 4667
		private global::System.Uri contentLink;
	}
}
