using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Net
{
	/// <summary>Provides common methods for sending data to and receiving data from a resource identified by a URI.</summary>
	// Token: 0x02000410 RID: 1040
	[ComVisible(true)]
	public class WebClient : global::System.ComponentModel.Component
	{
		// Token: 0x060024BD RID: 9405 RVA: 0x0006E7E4 File Offset: 0x0006C9E4
		static WebClient()
		{
			int num = 0;
			int i = 48;
			while (i <= 57)
			{
				WebClient.hexBytes[num] = (byte)i;
				i++;
				num++;
			}
			int j = 97;
			while (j <= 102)
			{
				WebClient.hexBytes[num] = (byte)j;
				j++;
				num++;
			}
		}

		/// <summary>Occurs when an asynchronous data download operation completes.</summary>
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x060024BE RID: 9406 RVA: 0x0006E84C File Offset: 0x0006CA4C
		// (remove) Token: 0x060024BF RID: 9407 RVA: 0x0006E868 File Offset: 0x0006CA68
		public event DownloadDataCompletedEventHandler DownloadDataCompleted;

		/// <summary>Occurs when an asynchronous file download operation completes.</summary>
		// Token: 0x14000054 RID: 84
		// (add) Token: 0x060024C0 RID: 9408 RVA: 0x0006E884 File Offset: 0x0006CA84
		// (remove) Token: 0x060024C1 RID: 9409 RVA: 0x0006E8A0 File Offset: 0x0006CAA0
		public event global::System.ComponentModel.AsyncCompletedEventHandler DownloadFileCompleted;

		/// <summary>Occurs when an asynchronous download operation successfully transfers some or all of the data.</summary>
		// Token: 0x14000055 RID: 85
		// (add) Token: 0x060024C2 RID: 9410 RVA: 0x0006E8BC File Offset: 0x0006CABC
		// (remove) Token: 0x060024C3 RID: 9411 RVA: 0x0006E8D8 File Offset: 0x0006CAD8
		public event DownloadProgressChangedEventHandler DownloadProgressChanged;

		/// <summary>Occurs when an asynchronous resource-download operation completes.</summary>
		// Token: 0x14000056 RID: 86
		// (add) Token: 0x060024C4 RID: 9412 RVA: 0x0006E8F4 File Offset: 0x0006CAF4
		// (remove) Token: 0x060024C5 RID: 9413 RVA: 0x0006E910 File Offset: 0x0006CB10
		public event DownloadStringCompletedEventHandler DownloadStringCompleted;

		/// <summary>Occurs when an asynchronous operation to open a stream containing a resource completes.</summary>
		// Token: 0x14000057 RID: 87
		// (add) Token: 0x060024C6 RID: 9414 RVA: 0x0006E92C File Offset: 0x0006CB2C
		// (remove) Token: 0x060024C7 RID: 9415 RVA: 0x0006E948 File Offset: 0x0006CB48
		public event OpenReadCompletedEventHandler OpenReadCompleted;

		/// <summary>Occurs when an asynchronous operation to open a stream to write data to a resource completes.</summary>
		// Token: 0x14000058 RID: 88
		// (add) Token: 0x060024C8 RID: 9416 RVA: 0x0006E964 File Offset: 0x0006CB64
		// (remove) Token: 0x060024C9 RID: 9417 RVA: 0x0006E980 File Offset: 0x0006CB80
		public event OpenWriteCompletedEventHandler OpenWriteCompleted;

		/// <summary>Occurs when an asynchronous data-upload operation completes.</summary>
		// Token: 0x14000059 RID: 89
		// (add) Token: 0x060024CA RID: 9418 RVA: 0x0006E99C File Offset: 0x0006CB9C
		// (remove) Token: 0x060024CB RID: 9419 RVA: 0x0006E9B8 File Offset: 0x0006CBB8
		public event UploadDataCompletedEventHandler UploadDataCompleted;

		/// <summary>Occurs when an asynchronous file-upload operation completes.</summary>
		// Token: 0x1400005A RID: 90
		// (add) Token: 0x060024CC RID: 9420 RVA: 0x0006E9D4 File Offset: 0x0006CBD4
		// (remove) Token: 0x060024CD RID: 9421 RVA: 0x0006E9F0 File Offset: 0x0006CBF0
		public event UploadFileCompletedEventHandler UploadFileCompleted;

		/// <summary>Occurs when an asynchronous upload operation successfully transfers some or all of the data.</summary>
		// Token: 0x1400005B RID: 91
		// (add) Token: 0x060024CE RID: 9422 RVA: 0x0006EA0C File Offset: 0x0006CC0C
		// (remove) Token: 0x060024CF RID: 9423 RVA: 0x0006EA28 File Offset: 0x0006CC28
		public event UploadProgressChangedEventHandler UploadProgressChanged;

		/// <summary>Occurs when an asynchronous string-upload operation completes.</summary>
		// Token: 0x1400005C RID: 92
		// (add) Token: 0x060024D0 RID: 9424 RVA: 0x0006EA44 File Offset: 0x0006CC44
		// (remove) Token: 0x060024D1 RID: 9425 RVA: 0x0006EA60 File Offset: 0x0006CC60
		public event UploadStringCompletedEventHandler UploadStringCompleted;

		/// <summary>Occurs when an asynchronous upload of a name/value collection completes.</summary>
		// Token: 0x1400005D RID: 93
		// (add) Token: 0x060024D2 RID: 9426 RVA: 0x0006EA7C File Offset: 0x0006CC7C
		// (remove) Token: 0x060024D3 RID: 9427 RVA: 0x0006EA98 File Offset: 0x0006CC98
		public event UploadValuesCompletedEventHandler UploadValuesCompleted;

		/// <summary>Gets or sets the base URI for requests made by a <see cref="T:System.Net.WebClient" />.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the base URI for requests made by a <see cref="T:System.Net.WebClient" /> or <see cref="F:System.String.Empty" /> if no base address has been specified.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Net.WebClient.BaseAddress" /> is set to an invalid URI. The inner exception may contain information that will help you locate the error.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x0006EAB4 File Offset: 0x0006CCB4
		// (set) Token: 0x060024D5 RID: 9429 RVA: 0x0006EAF0 File Offset: 0x0006CCF0
		public string BaseAddress
		{
			get
			{
				if (this.baseString == null && this.baseAddress == null)
				{
					return string.Empty;
				}
				this.baseString = this.baseAddress.ToString();
				return this.baseString;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					this.baseAddress = null;
				}
				else
				{
					this.baseAddress = new global::System.Uri(value);
				}
			}
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0006EB1C File Offset: 0x0006CD1C
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets or sets the application's cache policy for any resources obtained by this WebClient instance using <see cref="T:System.Net.WebRequest" /> objects.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCachePolicy" /> object that represents the application's caching requirements.</returns>
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060024D7 RID: 9431 RVA: 0x0006EB24 File Offset: 0x0006CD24
		// (set) Token: 0x060024D8 RID: 9432 RVA: 0x0006EB2C File Offset: 0x0006CD2C
		[global::System.MonoTODO]
		public global::System.Net.Cache.RequestCachePolicy CachePolicy
		{
			get
			{
				throw WebClient.GetMustImplement();
			}
			set
			{
				throw WebClient.GetMustImplement();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>true if the default credentials are used; otherwise false. The default value is false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="USERNAME" />
		/// </PermissionSet>
		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x0006EB34 File Offset: 0x0006CD34
		// (set) Token: 0x060024DA RID: 9434 RVA: 0x0006EB3C File Offset: 0x0006CD3C
		[global::System.MonoTODO]
		public bool UseDefaultCredentials
		{
			get
			{
				throw WebClient.GetMustImplement();
			}
			set
			{
				throw WebClient.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the network credentials that are sent to the host and used to authenticate the request.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> containing the authentication credentials for the request. The default is null.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060024DB RID: 9435 RVA: 0x0006EB44 File Offset: 0x0006CD44
		// (set) Token: 0x060024DC RID: 9436 RVA: 0x0006EB4C File Offset: 0x0006CD4C
		public ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
			}
		}

		/// <summary>Gets or sets a collection of header name/value pairs associated with the request.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> containing header name/value pairs associated with this request.</returns>
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060024DD RID: 9437 RVA: 0x0006EB58 File Offset: 0x0006CD58
		// (set) Token: 0x060024DE RID: 9438 RVA: 0x0006EB78 File Offset: 0x0006CD78
		public WebHeaderCollection Headers
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = new WebHeaderCollection();
				}
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		/// <summary>Gets or sets a collection of query name/value pairs associated with the request.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains query name/value pairs associated with the request. If no pairs are associated with the request, the value is an empty <see cref="T:System.Collections.Specialized.NameValueCollection" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x0006EB84 File Offset: 0x0006CD84
		// (set) Token: 0x060024E0 RID: 9440 RVA: 0x0006EBA4 File Offset: 0x0006CDA4
		public global::System.Collections.Specialized.NameValueCollection QueryString
		{
			get
			{
				if (this.queryString == null)
				{
					this.queryString = new global::System.Collections.Specialized.NameValueCollection();
				}
				return this.queryString;
			}
			set
			{
				this.queryString = value;
			}
		}

		/// <summary>Gets a collection of header name/value pairs associated with the response.</summary>
		/// <returns>A <see cref="T:System.Net.WebHeaderCollection" /> containing header name/value pairs associated with the response, or null if no response has been received.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x0006EBB0 File Offset: 0x0006CDB0
		public WebHeaderCollection ResponseHeaders
		{
			get
			{
				return this.responseHeaders;
			}
		}

		/// <summary>Gets and sets the <see cref="T:System.Text.Encoding" /> used to upload and download strings.</summary>
		/// <returns>A <see cref="T:System.Text.Encoding" /> that is used to encode strings. The default value of this property is the encoding returned by <see cref="P:System.Text.Encoding.Default" />.</returns>
		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x0006EBB8 File Offset: 0x0006CDB8
		// (set) Token: 0x060024E3 RID: 9443 RVA: 0x0006EBC0 File Offset: 0x0006CDC0
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Encoding");
				}
				this.encoding = value;
			}
		}

		/// <summary>Gets or sets the proxy used by this <see cref="T:System.Net.WebClient" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> instance used to send requests.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Net.WebClient.Proxy" /> is set to null. </exception>
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x0006EBDC File Offset: 0x0006CDDC
		// (set) Token: 0x060024E5 RID: 9445 RVA: 0x0006EBE4 File Offset: 0x0006CDE4
		public IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				this.proxy = value;
			}
		}

		/// <summary>Gets whether a Web request is in progress.</summary>
		/// <returns>true if the Web request is still in progress; otherwise false.</returns>
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060024E6 RID: 9446 RVA: 0x0006EBF0 File Offset: 0x0006CDF0
		public bool IsBusy
		{
			get
			{
				return this.is_busy;
			}
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x0006EBF8 File Offset: 0x0006CDF8
		private void CheckBusy()
		{
			if (this.IsBusy)
			{
				throw new NotSupportedException("WebClient does not support conccurent I/O operations.");
			}
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x0006EC10 File Offset: 0x0006CE10
		private void SetBusy()
		{
			lock (this)
			{
				this.CheckBusy();
				this.is_busy = true;
			}
		}

		/// <summary>Downloads the resource with the specified URI as a <see cref="T:System.Byte" /> array.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <param name="address">The URI from which to download data. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading data. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024E9 RID: 9449 RVA: 0x0006EC5C File Offset: 0x0006CE5C
		public byte[] DownloadData(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.DownloadData(this.CreateUri(address));
		}

		/// <summary>Downloads the resource with the specified URI as a <see cref="T:System.Byte" /> array.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the downloaded resource.</returns>
		/// <param name="address">The URI represented by the <see cref="T:System.Uri" />  object, from which to download data.</param>
		// Token: 0x060024EA RID: 9450 RVA: 0x0006EC7C File Offset: 0x0006CE7C
		public byte[] DownloadData(global::System.Uri address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			byte[] array;
			try
			{
				this.SetBusy();
				this.async = false;
				array = this.DownloadDataCore(address, null);
			}
			finally
			{
				this.is_busy = false;
			}
			return array;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x0006ECE8 File Offset: 0x0006CEE8
		private byte[] DownloadDataCore(global::System.Uri address, object userToken)
		{
			WebRequest webRequest = null;
			byte[] array;
			try
			{
				webRequest = this.SetupRequest(address);
				WebResponse webResponse = this.GetWebResponse(webRequest);
				Stream responseStream = webResponse.GetResponseStream();
				array = this.ReadAll(responseStream, (int)webResponse.ContentLength, userToken);
			}
			catch (ThreadInterruptedException)
			{
				if (webRequest != null)
				{
					webRequest.Abort();
				}
				throw;
			}
			catch (WebException ex)
			{
				throw;
			}
			catch (Exception ex2)
			{
				throw new WebException("An error occurred performing a WebClient request.", ex2);
			}
			return array;
		}

		/// <summary>Downloads the resource with the specified URI to a local file.</summary>
		/// <param name="address">The URI from which to download data. </param>
		/// <param name="fileName">The name of the local file that is to receive the data. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="filename" /> is null or <see cref="F:System.String.Empty" />.-or-The file does not exist.-or- An error occurred while downloading data. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024EC RID: 9452 RVA: 0x0006EDA8 File Offset: 0x0006CFA8
		public void DownloadFile(string address, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			this.DownloadFile(this.CreateUri(address), fileName);
		}

		/// <summary>Downloads the resource with the specified URI to a local file.</summary>
		/// <param name="address">The URI specified as a <see cref="T:System.String" />, from which to download data. </param>
		/// <param name="fileName">The name of the local file that is to receive the data. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="filename" /> is null or <see cref="F:System.String.Empty" />.-or- The file does not exist. -or- An error occurred while downloading data. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		// Token: 0x060024ED RID: 9453 RVA: 0x0006EDCC File Offset: 0x0006CFCC
		public void DownloadFile(global::System.Uri address, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			try
			{
				this.SetBusy();
				this.async = false;
				this.DownloadFileCore(address, fileName, null);
			}
			catch (WebException ex)
			{
				throw;
			}
			catch (Exception ex2)
			{
				throw new WebException("An error occurred performing a WebClient request.", ex2);
			}
			finally
			{
				this.is_busy = false;
			}
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x0006EE8C File Offset: 0x0006D08C
		private void DownloadFileCore(global::System.Uri address, string fileName, object userToken)
		{
			WebRequest webRequest = null;
			using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
			{
				try
				{
					webRequest = this.SetupRequest(address);
					WebResponse webResponse = this.GetWebResponse(webRequest);
					Stream responseStream = webResponse.GetResponseStream();
					int num = (int)webResponse.ContentLength;
					int num2 = ((num > -1 && num <= 32768) ? num : 32768);
					byte[] array = new byte[num2];
					long num3 = 0L;
					int num4;
					while ((num4 = responseStream.Read(array, 0, num2)) != 0)
					{
						if (this.async)
						{
							num3 += (long)num4;
							this.OnDownloadProgressChanged(new DownloadProgressChangedEventArgs(num3, webResponse.ContentLength, userToken));
						}
						fileStream.Write(array, 0, num4);
					}
				}
				catch (ThreadInterruptedException)
				{
					if (webRequest != null)
					{
						webRequest.Abort();
					}
					throw;
				}
			}
		}

		/// <summary>Opens a readable stream for the data downloaded from a resource with the URI specified as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to read data from a resource.</returns>
		/// <param name="address">The URI specified as a <see cref="T:System.String" /> from which to download data. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, <paramref name="address" /> is invalid.-or- An error occurred while downloading data. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024EF RID: 9455 RVA: 0x0006EFA0 File Offset: 0x0006D1A0
		public Stream OpenRead(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenRead(this.CreateUri(address));
		}

		/// <summary>Opens a readable stream for the data downloaded from a resource with the URI specified as a <see cref="T:System.Uri" /></summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to read data from a resource.</returns>
		/// <param name="address">The URI specified as a <see cref="T:System.Uri" /> from which to download data. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, <paramref name="address" /> is invalid.-or- An error occurred while downloading data. </exception>
		// Token: 0x060024F0 RID: 9456 RVA: 0x0006EFC0 File Offset: 0x0006D1C0
		public Stream OpenRead(global::System.Uri address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			Stream responseStream;
			try
			{
				this.SetBusy();
				this.async = false;
				WebRequest webRequest = this.SetupRequest(address);
				WebResponse webResponse = this.GetWebResponse(webRequest);
				responseStream = webResponse.GetResponseStream();
			}
			catch (WebException ex)
			{
				throw;
			}
			catch (Exception ex2)
			{
				throw new WebException("An error occurred performing a WebClient request.", ex2);
			}
			finally
			{
				this.is_busy = false;
			}
			return responseStream;
		}

		/// <summary>Opens a stream for writing data to the specified resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024F1 RID: 9457 RVA: 0x0006F088 File Offset: 0x0006D288
		public Stream OpenWrite(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenWrite(this.CreateUri(address));
		}

		/// <summary>Opens a stream for writing data to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024F2 RID: 9458 RVA: 0x0006F0A8 File Offset: 0x0006D2A8
		public Stream OpenWrite(string address, string method)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.OpenWrite(this.CreateUri(address), method);
		}

		/// <summary>Opens a stream for writing data to the specified resource.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		// Token: 0x060024F3 RID: 9459 RVA: 0x0006F0CC File Offset: 0x0006D2CC
		public Stream OpenWrite(global::System.Uri address)
		{
			return this.OpenWrite(address, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource, by using the specified method.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> used to write data to the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		// Token: 0x060024F4 RID: 9460 RVA: 0x0006F0D8 File Offset: 0x0006D2D8
		public Stream OpenWrite(global::System.Uri address, string method)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			Stream requestStream;
			try
			{
				this.SetBusy();
				this.async = false;
				WebRequest webRequest = this.SetupRequest(address, method, true);
				requestStream = webRequest.GetRequestStream();
			}
			catch (WebException ex)
			{
				throw;
			}
			catch (Exception ex2)
			{
				throw new WebException("An error occurred performing a WebClient request.", ex2);
			}
			finally
			{
				this.is_busy = false;
			}
			return requestStream;
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x0006F194 File Offset: 0x0006D394
		private string DetermineMethod(global::System.Uri address, string method, bool is_upload)
		{
			if (method != null)
			{
				return method;
			}
			if (address.Scheme == global::System.Uri.UriSchemeFtp)
			{
				return (!is_upload) ? "RETR" : "STOR";
			}
			return (!is_upload) ? "GET" : "POST";
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="data">The data buffer to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null. -or-An error occurred while sending the data.-or- There was no response from the server hosting the resource. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024F6 RID: 9462 RVA: 0x0006F1EC File Offset: 0x0006D3EC
		public byte[] UploadData(string address, byte[] data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadData(this.CreateUri(address), data);
		}

		/// <summary>Uploads a data buffer to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="method">The HTTP method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- An error occurred while uploading the data.-or- There was no response from the server hosting the resource. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024F7 RID: 9463 RVA: 0x0006F210 File Offset: 0x0006D410
		public byte[] UploadData(string address, string method, byte[] data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadData(this.CreateUri(address), method, data);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="data">The data buffer to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null. -or-An error occurred while sending the data.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x060024F8 RID: 9464 RVA: 0x0006F240 File Offset: 0x0006D440
		public byte[] UploadData(global::System.Uri address, byte[] data)
		{
			return this.UploadData(address, null, data);
		}

		/// <summary>Uploads a data buffer to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="method">The HTTP method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- An error occurred while uploading the data.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x060024F9 RID: 9465 RVA: 0x0006F24C File Offset: 0x0006D44C
		public byte[] UploadData(global::System.Uri address, string method, byte[] data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] array;
			try
			{
				this.SetBusy();
				this.async = false;
				array = this.UploadDataCore(address, method, data, null);
			}
			catch (WebException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new WebException("An error occurred performing a WebClient request.", ex);
			}
			finally
			{
				this.is_busy = false;
			}
			return array;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x0006F314 File Offset: 0x0006D514
		private byte[] UploadDataCore(global::System.Uri address, string method, byte[] data, object userToken)
		{
			WebRequest webRequest = this.SetupRequest(address, method, true);
			byte[] array;
			try
			{
				int num = data.Length;
				webRequest.ContentLength = (long)num;
				using (Stream requestStream = webRequest.GetRequestStream())
				{
					requestStream.Write(data, 0, num);
				}
				WebResponse webResponse = this.GetWebResponse(webRequest);
				Stream responseStream = webResponse.GetResponseStream();
				array = this.ReadAll(responseStream, (int)webResponse.ContentLength, userToken);
			}
			catch (ThreadInterruptedException)
			{
				if (webRequest != null)
				{
					webRequest.Abort();
				}
				throw;
			}
			return array;
		}

		/// <summary>Uploads the specified local file to a resource with the specified URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the file. For example, ftp://localhost/samplefile.txt.</param>
		/// <param name="fileName">The file to send to the resource. For example, "samplefile.txt".</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.-or- An error occurred while uploading the file.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024FB RID: 9467 RVA: 0x0006F3D0 File Offset: 0x0006D5D0
		public byte[] UploadFile(string address, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadFile(this.CreateUri(address), fileName);
		}

		/// <summary>Uploads the specified local file to a resource with the specified URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the file. For example, ftp://localhost/samplefile.txt.</param>
		/// <param name="fileName">The file to send to the resource. For example, "samplefile.txt".</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.-or- An error occurred while uploading the file.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x060024FC RID: 9468 RVA: 0x0006F3F4 File Offset: 0x0006D5F4
		public byte[] UploadFile(global::System.Uri address, string fileName)
		{
			return this.UploadFile(address, null, fileName);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the file.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.-or- An error occurred while uploading the file.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060024FD RID: 9469 RVA: 0x0006F400 File Offset: 0x0006D600
		public byte[] UploadFile(string address, string method, string fileName)
		{
			return this.UploadFile(this.CreateUri(address), method, fileName);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the file.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid characters, or does not exist.-or- An error occurred while uploading the file.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x060024FE RID: 9470 RVA: 0x0006F414 File Offset: 0x0006D614
		public byte[] UploadFile(global::System.Uri address, string method, string fileName)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			byte[] array;
			try
			{
				this.SetBusy();
				this.async = false;
				array = this.UploadFileCore(address, method, fileName, null);
			}
			catch (WebException ex)
			{
				throw;
			}
			catch (Exception ex2)
			{
				throw new WebException("An error occurred performing a WebClient request.", ex2);
			}
			finally
			{
				this.is_busy = false;
			}
			return array;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x0006F4DC File Offset: 0x0006D6DC
		private byte[] UploadFileCore(global::System.Uri address, string method, string fileName, object userToken)
		{
			string text = this.Headers["Content-Type"];
			if (text != null)
			{
				string text2 = text.ToLower();
				if (text2.StartsWith("multipart/"))
				{
					throw new WebException("Content-Type cannot be set to a multipart type for this request.");
				}
			}
			else
			{
				text = "application/octet-stream";
			}
			string text3 = "------------" + DateTime.Now.Ticks.ToString("x");
			this.Headers["Content-Type"] = string.Format("multipart/form-data; boundary={0}", text3);
			Stream stream = null;
			Stream stream2 = null;
			byte[] array = null;
			fileName = Path.GetFullPath(fileName);
			WebRequest webRequest = null;
			try
			{
				stream2 = File.OpenRead(fileName);
				webRequest = this.SetupRequest(address, method, true);
				stream = webRequest.GetRequestStream();
				byte[] bytes = Encoding.ASCII.GetBytes("--" + text3 + "\r\n");
				stream.Write(bytes, 0, bytes.Length);
				string text4 = string.Format("Content-Disposition: form-data; name=\"file\"; filename=\"{0}\"\r\nContent-Type: {1}\r\n\r\n", Path.GetFileName(fileName), text);
				byte[] bytes2 = Encoding.UTF8.GetBytes(text4);
				stream.Write(bytes2, 0, bytes2.Length);
				byte[] array2 = new byte[4096];
				int num;
				while ((num = stream2.Read(array2, 0, 4096)) != 0)
				{
					stream.Write(array2, 0, num);
				}
				stream.WriteByte(13);
				stream.WriteByte(10);
				stream.Write(bytes, 0, bytes.Length);
				stream.Close();
				stream = null;
				WebResponse webResponse = this.GetWebResponse(webRequest);
				Stream responseStream = webResponse.GetResponseStream();
				array = this.ReadAll(responseStream, (int)webResponse.ContentLength, userToken);
			}
			catch (ThreadInterruptedException)
			{
				if (webRequest != null)
				{
					webRequest.Abort();
				}
				throw;
			}
			finally
			{
				if (stream2 != null)
				{
					stream2.Close();
				}
				if (stream != null)
				{
					stream.Close();
				}
			}
			return array;
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the collection. </param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- There was no response from the server hosting the resource.-or- An error occurred while opening the stream.-or- The Content-type header is not null or "application/x-www-form-urlencoded". </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002500 RID: 9472 RVA: 0x0006F6E0 File Offset: 0x0006D8E0
		public byte[] UploadValues(string address, global::System.Collections.Specialized.NameValueCollection data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadValues(this.CreateUri(address), data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the collection. </param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header value is not null and is not application/x-www-form-urlencoded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002501 RID: 9473 RVA: 0x0006F704 File Offset: 0x0006D904
		public byte[] UploadValues(string address, string method, global::System.Collections.Specialized.NameValueCollection data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.UploadValues(this.CreateUri(address), method, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the collection. </param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- There was no response from the server hosting the resource.-or- An error occurred while opening the stream.-or- The Content-type header is not null or "application/x-www-form-urlencoded". </exception>
		// Token: 0x06002502 RID: 9474 RVA: 0x0006F734 File Offset: 0x0006D934
		public byte[] UploadValues(global::System.Uri address, global::System.Collections.Specialized.NameValueCollection data)
		{
			return this.UploadValues(address, null, data);
		}

		/// <summary>Uploads the specified name/value collection to the resource identified by the specified URI, using the specified method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the body of the response from the resource.</returns>
		/// <param name="address">The URI of the resource to receive the collection. </param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" />, and <paramref name="address" /> is invalid.-or- <paramref name="data" /> is null.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header value is not null and is not application/x-www-form-urlencoded. </exception>
		// Token: 0x06002503 RID: 9475 RVA: 0x0006F740 File Offset: 0x0006D940
		public byte[] UploadValues(global::System.Uri address, string method, global::System.Collections.Specialized.NameValueCollection data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] array;
			try
			{
				this.SetBusy();
				this.async = false;
				array = this.UploadValuesCore(address, method, data, null);
			}
			catch (WebException ex)
			{
				throw;
			}
			catch (Exception ex2)
			{
				throw new WebException("An error occurred performing a WebClient request.", ex2);
			}
			finally
			{
				this.is_busy = false;
			}
			return array;
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x0006F808 File Offset: 0x0006DA08
		private byte[] UploadValuesCore(global::System.Uri uri, string method, global::System.Collections.Specialized.NameValueCollection data, object userToken)
		{
			string text = this.Headers["Content-Type"];
			if (text != null && string.Compare(text, WebClient.urlEncodedCType, true) != 0)
			{
				throw new WebException("Content-Type header cannot be changed from its default value for this request.");
			}
			this.Headers["Content-Type"] = WebClient.urlEncodedCType;
			WebRequest webRequest = this.SetupRequest(uri, method, true);
			byte[] array2;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				foreach (object obj in data)
				{
					string text2 = (string)obj;
					byte[] array = Encoding.UTF8.GetBytes(text2);
					WebClient.UrlEncodeAndWrite(memoryStream, array);
					memoryStream.WriteByte(61);
					array = Encoding.UTF8.GetBytes(data[text2]);
					WebClient.UrlEncodeAndWrite(memoryStream, array);
					memoryStream.WriteByte(38);
				}
				int num = (int)memoryStream.Length;
				if (num > 0)
				{
					memoryStream.SetLength((long)(--num));
				}
				byte[] buffer = memoryStream.GetBuffer();
				webRequest.ContentLength = (long)num;
				using (Stream requestStream = webRequest.GetRequestStream())
				{
					requestStream.Write(buffer, 0, num);
				}
				memoryStream.Close();
				WebResponse webResponse = this.GetWebResponse(webRequest);
				Stream responseStream = webResponse.GetResponseStream();
				array2 = this.ReadAll(responseStream, (int)webResponse.ContentLength, userToken);
			}
			catch (ThreadInterruptedException)
			{
				webRequest.Abort();
				throw;
			}
			return array2;
		}

		/// <summary>Downloads the requested resource as a <see cref="T:System.String" />. The resource to download is specified as a <see cref="T:System.String" /> containing the URI.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the requested resource.</returns>
		/// <param name="address">A <see cref="T:System.String" /> containing the URI to download.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002505 RID: 9477 RVA: 0x0006F9D8 File Offset: 0x0006DBD8
		public string DownloadString(string address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.encoding.GetString(this.DownloadData(this.CreateUri(address)));
		}

		/// <summary>Downloads the requested resource as a <see cref="T:System.String" />. The resource to download is specified as a <see cref="T:System.Uri" />.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the requested resource.</returns>
		/// <param name="address">A <see cref="T:System.Uri" /> object containing the URI to download.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.NotSupportedException">The method has been called simultaneously on multiple threads.</exception>
		// Token: 0x06002506 RID: 9478 RVA: 0x0006FA04 File Offset: 0x0006DC04
		public string DownloadString(global::System.Uri address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			return this.encoding.GetString(this.DownloadData(this.CreateUri(address)));
		}

		/// <summary>Uploads the specified string to the specified resource, using the POST method.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the string. For Http resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002507 RID: 9479 RVA: 0x0006FA38 File Offset: 0x0006DC38
		public string UploadString(string address, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] array = this.UploadData(address, this.encoding.GetBytes(data));
			return this.encoding.GetString(array);
		}

		/// <summary>Uploads the specified string to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the file. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method. </param>
		/// <param name="method">The HTTP method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.-or-<paramref name="method" /> cannot be used to send content.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002508 RID: 9480 RVA: 0x0006FA88 File Offset: 0x0006DC88
		public string UploadString(string address, string method, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] array = this.UploadData(address, method, this.encoding.GetBytes(data));
			return this.encoding.GetString(array);
		}

		/// <summary>Uploads the specified string to the specified resource, using the POST method.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the string. For Http resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x06002509 RID: 9481 RVA: 0x0006FAD8 File Offset: 0x0006DCD8
		public string UploadString(global::System.Uri address, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] array = this.UploadData(address, this.encoding.GetBytes(data));
			return this.encoding.GetString(array);
		}

		/// <summary>Uploads the specified string to the specified resource, using the specified method.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the response sent by the server.</returns>
		/// <param name="address">The URI of the resource to receive the file. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method. </param>
		/// <param name="method">The HTTP method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.-or-<paramref name="method" /> cannot be used to send content.</exception>
		// Token: 0x0600250A RID: 9482 RVA: 0x0006FB30 File Offset: 0x0006DD30
		public string UploadString(global::System.Uri address, string method, string data)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			byte[] array = this.UploadData(address, method, this.encoding.GetBytes(data));
			return this.encoding.GetString(array);
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x0006FB88 File Offset: 0x0006DD88
		private global::System.Uri CreateUri(string address)
		{
			return this.MakeUri(address);
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x0006FB94 File Offset: 0x0006DD94
		private global::System.Uri CreateUri(global::System.Uri address)
		{
			string query = address.Query;
			if (string.IsNullOrEmpty(query))
			{
				query = this.GetQueryString(true);
			}
			if (this.baseAddress == null && query == null)
			{
				return address;
			}
			if (this.baseAddress == null)
			{
				return new global::System.Uri(address.ToString() + query, query != null);
			}
			if (query == null)
			{
				return new global::System.Uri(this.baseAddress, address.ToString());
			}
			return new global::System.Uri(this.baseAddress, address.ToString() + query, query != null);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x0006FC34 File Offset: 0x0006DE34
		private string GetQueryString(bool add_qmark)
		{
			if (this.queryString == null || this.queryString.Count == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (add_qmark)
			{
				stringBuilder.Append('?');
			}
			foreach (object obj in this.queryString)
			{
				string text = (string)obj;
				stringBuilder.AppendFormat("{0}={1}&", text, this.UrlEncode(this.queryString[text]));
			}
			if (stringBuilder.Length != 0)
			{
				stringBuilder.Length--;
			}
			if (stringBuilder.Length == 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x0006FD1C File Offset: 0x0006DF1C
		private global::System.Uri MakeUri(string path)
		{
			string text = this.GetQueryString(true);
			if (this.baseAddress == null && text == null)
			{
				try
				{
					return new global::System.Uri(path);
				}
				catch (ArgumentNullException)
				{
					if (Environment.UnityWebSecurityEnabled)
					{
						throw;
					}
					path = Path.GetFullPath(path);
					return new global::System.Uri("file://" + path);
				}
				catch (global::System.UriFormatException)
				{
					if (Environment.UnityWebSecurityEnabled)
					{
						throw;
					}
					path = Path.GetFullPath(path);
					return new global::System.Uri("file://" + path);
				}
			}
			if (this.baseAddress == null)
			{
				return new global::System.Uri(path + text, text != null);
			}
			if (text == null)
			{
				return new global::System.Uri(this.baseAddress, path);
			}
			return new global::System.Uri(this.baseAddress, path + text, text != null);
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x0006FE40 File Offset: 0x0006E040
		private WebRequest SetupRequest(global::System.Uri uri)
		{
			WebRequest webRequest = this.GetWebRequest(uri);
			if (this.Proxy != null)
			{
				webRequest.Proxy = this.Proxy;
			}
			webRequest.Credentials = this.credentials;
			if (this.headers != null && this.headers.Count != 0 && webRequest is HttpWebRequest)
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)webRequest;
				string text = this.headers["Expect"];
				string text2 = this.headers["Content-Type"];
				string text3 = this.headers["Accept"];
				string text4 = this.headers["Connection"];
				string text5 = this.headers["User-Agent"];
				string text6 = this.headers["Referer"];
				this.headers.RemoveInternal("Expect");
				this.headers.RemoveInternal("Content-Type");
				this.headers.RemoveInternal("Accept");
				this.headers.RemoveInternal("Connection");
				this.headers.RemoveInternal("Referer");
				this.headers.RemoveInternal("User-Agent");
				webRequest.Headers = this.headers;
				if (text != null && text.Length > 0)
				{
					httpWebRequest.Expect = text;
				}
				if (text3 != null && text3.Length > 0)
				{
					httpWebRequest.Accept = text3;
				}
				if (text2 != null && text2.Length > 0)
				{
					httpWebRequest.ContentType = text2;
				}
				if (text4 != null && text4.Length > 0)
				{
					httpWebRequest.Connection = text4;
				}
				if (text5 != null && text5.Length > 0)
				{
					httpWebRequest.UserAgent = text5;
				}
				if (text6 != null && text6.Length > 0)
				{
					httpWebRequest.Referer = text6;
				}
			}
			this.responseHeaders = null;
			return webRequest;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x00070028 File Offset: 0x0006E228
		private WebRequest SetupRequest(global::System.Uri uri, string method, bool is_upload)
		{
			WebRequest webRequest = this.SetupRequest(uri);
			webRequest.Method = this.DetermineMethod(uri, method, is_upload);
			return webRequest;
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00070050 File Offset: 0x0006E250
		private byte[] ReadAll(Stream stream, int length, object userToken)
		{
			MemoryStream memoryStream = null;
			bool flag = length == -1;
			int num = ((!flag) ? length : 8192);
			if (flag)
			{
				memoryStream = new MemoryStream();
			}
			int num2 = 0;
			byte[] array = new byte[num];
			int num3;
			while ((num3 = stream.Read(array, num2, num)) != 0)
			{
				if (flag)
				{
					memoryStream.Write(array, 0, num3);
				}
				else
				{
					num2 += num3;
					num -= num3;
				}
				if (this.async)
				{
					this.OnDownloadProgressChanged(new DownloadProgressChangedEventArgs((long)num3, (long)length, userToken));
				}
			}
			if (flag)
			{
				return memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000700F0 File Offset: 0x0006E2F0
		private string UrlEncode(string str)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char c = str[i];
				if (c == ' ')
				{
					stringBuilder.Append('+');
				}
				else if ((c < '0' && c != '-' && c != '.') || (c < 'A' && c > '9') || (c > 'Z' && c < 'a' && c != '_') || c > 'z')
				{
					stringBuilder.Append('%');
					int num = (int)(c >> 4);
					stringBuilder.Append((char)WebClient.hexBytes[num]);
					num = (int)(c & '\u000f');
					stringBuilder.Append((char)WebClient.hexBytes[num]);
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000701CC File Offset: 0x0006E3CC
		private static void UrlEncodeAndWrite(Stream stream, byte[] bytes)
		{
			if (bytes == null)
			{
				return;
			}
			int num = bytes.Length;
			if (num == 0)
			{
				return;
			}
			for (int i = 0; i < num; i++)
			{
				char c = (char)bytes[i];
				if (c == ' ')
				{
					stream.WriteByte(43);
				}
				else if ((c < '0' && c != '-' && c != '.') || (c < 'A' && c > '9') || (c > 'Z' && c < 'a' && c != '_') || c > 'z')
				{
					stream.WriteByte(37);
					int num2 = (int)(c >> 4);
					stream.WriteByte(WebClient.hexBytes[num2]);
					num2 = (int)(c & '\u000f');
					stream.WriteByte(WebClient.hexBytes[num2]);
				}
				else
				{
					stream.WriteByte((byte)c);
				}
			}
		}

		/// <summary>Cancels a pending asynchronous operation.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002514 RID: 9492 RVA: 0x00070298 File Offset: 0x0006E498
		public void CancelAsync()
		{
			lock (this)
			{
				if (this.async_thread != null)
				{
					Thread thread = this.async_thread;
					this.CompleteAsync();
					thread.Interrupt();
				}
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000702F8 File Offset: 0x0006E4F8
		private void CompleteAsync()
		{
			lock (this)
			{
				this.is_busy = false;
				this.async_thread = null;
			}
		}

		/// <summary>Downloads the specified resource as a <see cref="T:System.Byte" /> array. This method does not block the calling thread.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x06002516 RID: 9494 RVA: 0x00070344 File Offset: 0x0006E544
		public void DownloadDataAsync(global::System.Uri address)
		{
			this.DownloadDataAsync(address, null);
		}

		/// <summary>Downloads the specified resource as a <see cref="T:System.Byte" /> array. This method does not block the calling thread.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x06002517 RID: 9495 RVA: 0x00070350 File Offset: 0x0006E550
		public void DownloadDataAsync(global::System.Uri address, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array2 = (object[])state;
					try
					{
						byte[] array3 = this.DownloadDataCore((global::System.Uri)array2[0], array2[1]);
						this.OnDownloadDataCompleted(new DownloadDataCompletedEventArgs(array3, null, false, array2[1]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnDownloadDataCompleted(new DownloadDataCompletedEventArgs(null, null, true, array2[1]));
						throw;
					}
					catch (Exception ex)
					{
						this.OnDownloadDataCompleted(new DownloadDataCompletedEventArgs(null, ex, false, array2[1]));
					}
				});
				object[] array = new object[] { address, userToken };
				this.async_thread.Start(array);
			}
		}

		/// <summary>Downloads, to a local file, the resource with the specified URI. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to download. </param>
		/// <param name="fileName">The name of the file to be placed on the local computer. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.InvalidOperationException">The local file specified by <paramref name="fileName" /> is in use by another thread.</exception>
		// Token: 0x06002518 RID: 9496 RVA: 0x000703E4 File Offset: 0x0006E5E4
		public void DownloadFileAsync(global::System.Uri address, string fileName)
		{
			this.DownloadFileAsync(address, fileName, null);
		}

		/// <summary>Downloads, to a local file, the resource with the specified URI. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to download. </param>
		/// <param name="fileName">The name of the file to be placed on the local computer. </param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		/// <exception cref="T:System.InvalidOperationException">The local file specified by <paramref name="fileName" /> is in use by another thread.</exception>
		// Token: 0x06002519 RID: 9497 RVA: 0x000703F0 File Offset: 0x0006E5F0
		public void DownloadFileAsync(global::System.Uri address, string fileName, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array2 = (object[])state;
					try
					{
						this.DownloadFileCore((global::System.Uri)array2[0], (string)array2[1], array2[2]);
						this.OnDownloadFileCompleted(new global::System.ComponentModel.AsyncCompletedEventArgs(null, false, array2[2]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnDownloadFileCompleted(new global::System.ComponentModel.AsyncCompletedEventArgs(null, true, array2[2]));
					}
					catch (Exception ex)
					{
						this.OnDownloadFileCompleted(new global::System.ComponentModel.AsyncCompletedEventArgs(ex, false, array2[2]));
					}
				});
				object[] array = new object[] { address, fileName, userToken };
				this.async_thread.Start(array);
			}
		}

		/// <summary>Downloads the resource specified as a <see cref="T:System.Uri" />. This method does not block the calling thread.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x0600251A RID: 9498 RVA: 0x00070498 File Offset: 0x0006E698
		public void DownloadStringAsync(global::System.Uri address)
		{
			this.DownloadStringAsync(address, null);
		}

		/// <summary>Downloads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">A <see cref="T:System.Uri" /> containing the URI to download.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while downloading the resource. </exception>
		// Token: 0x0600251B RID: 9499 RVA: 0x000704A4 File Offset: 0x0006E6A4
		public void DownloadStringAsync(global::System.Uri address, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array2 = (object[])state;
					try
					{
						string @string = this.encoding.GetString(this.DownloadDataCore((global::System.Uri)array2[0], array2[1]));
						this.OnDownloadStringCompleted(new DownloadStringCompletedEventArgs(@string, null, false, array2[1]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnDownloadStringCompleted(new DownloadStringCompletedEventArgs(null, null, true, array2[1]));
					}
					catch (Exception ex)
					{
						this.OnDownloadStringCompleted(new DownloadStringCompletedEventArgs(null, ex, false, array2[1]));
					}
				});
				object[] array = new object[] { address, userToken };
				this.async_thread.Start(array);
			}
		}

		/// <summary>Opens a readable stream containing the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to retrieve.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and address is invalid.-or- An error occurred while downloading the resource. -or- An error occurred while opening the stream.</exception>
		// Token: 0x0600251C RID: 9500 RVA: 0x00070538 File Offset: 0x0006E738
		public void OpenReadAsync(global::System.Uri address)
		{
			this.OpenReadAsync(address, null);
		}

		/// <summary>Opens a readable stream containing the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to retrieve.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and address is invalid.-or- An error occurred while downloading the resource. -or- An error occurred while opening the stream.</exception>
		// Token: 0x0600251D RID: 9501 RVA: 0x00070544 File Offset: 0x0006E744
		public void OpenReadAsync(global::System.Uri address, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array2 = (object[])state;
					WebRequest webRequest = null;
					try
					{
						webRequest = this.SetupRequest((global::System.Uri)array2[0]);
						WebResponse webResponse = this.GetWebResponse(webRequest);
						Stream responseStream = webResponse.GetResponseStream();
						this.OnOpenReadCompleted(new OpenReadCompletedEventArgs(responseStream, null, false, array2[1]));
					}
					catch (ThreadInterruptedException)
					{
						if (webRequest != null)
						{
							webRequest.Abort();
						}
						this.OnOpenReadCompleted(new OpenReadCompletedEventArgs(null, null, true, array2[1]));
					}
					catch (Exception ex)
					{
						this.OnOpenReadCompleted(new OpenReadCompletedEventArgs(null, ex, false, array2[1]));
					}
				});
				object[] array = new object[] { address, userToken };
				this.async_thread.Start(array);
			}
		}

		/// <summary>Opens a stream for writing data to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data. </param>
		// Token: 0x0600251E RID: 9502 RVA: 0x000705D8 File Offset: 0x0006E7D8
		public void OpenWriteAsync(global::System.Uri address)
		{
			this.OpenWriteAsync(address, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		// Token: 0x0600251F RID: 9503 RVA: 0x000705E4 File Offset: 0x0006E7E4
		public void OpenWriteAsync(global::System.Uri address, string method)
		{
			this.OpenWriteAsync(address, method, null);
		}

		/// <summary>Opens a stream for writing data to the specified resource, using the specified method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream. </exception>
		// Token: 0x06002520 RID: 9504 RVA: 0x000705F0 File Offset: 0x0006E7F0
		public void OpenWriteAsync(global::System.Uri address, string method, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array2 = (object[])state;
					WebRequest webRequest = null;
					try
					{
						webRequest = this.SetupRequest((global::System.Uri)array2[0], (string)array2[1], true);
						Stream requestStream = webRequest.GetRequestStream();
						this.OnOpenWriteCompleted(new OpenWriteCompletedEventArgs(requestStream, null, false, array2[2]));
					}
					catch (ThreadInterruptedException)
					{
						if (webRequest != null)
						{
							webRequest.Abort();
						}
						this.OnOpenWriteCompleted(new OpenWriteCompletedEventArgs(null, null, true, array2[2]));
					}
					catch (Exception ex)
					{
						this.OnOpenWriteCompleted(new OpenWriteCompletedEventArgs(null, ex, false, array2[2]));
					}
				});
				object[] array = new object[] { address, method, userToken };
				this.async_thread.Start(array);
			}
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data. </param>
		/// <param name="data">The data buffer to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x06002521 RID: 9505 RVA: 0x00070688 File Offset: 0x0006E888
		public void UploadDataAsync(global::System.Uri address, byte[] data)
		{
			this.UploadDataAsync(address, null, data);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI, using the specified method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x06002522 RID: 9506 RVA: 0x00070694 File Offset: 0x0006E894
		public void UploadDataAsync(global::System.Uri address, string method, byte[] data)
		{
			this.UploadDataAsync(address, method, data, null);
		}

		/// <summary>Uploads a data buffer to a resource identified by a URI, using the specified method and identifying token.</summary>
		/// <param name="address">The URI of the resource to receive the data.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The data buffer to send to the resource.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource. </exception>
		// Token: 0x06002523 RID: 9507 RVA: 0x000706A0 File Offset: 0x0006E8A0
		public void UploadDataAsync(global::System.Uri address, string method, byte[] data, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array2 = (object[])state;
					try
					{
						byte[] array3 = this.UploadDataCore((global::System.Uri)array2[0], (string)array2[1], (byte[])array2[2], array2[3]);
						this.OnUploadDataCompleted(new UploadDataCompletedEventArgs(array3, null, false, array2[3]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnUploadDataCompleted(new UploadDataCompletedEventArgs(null, null, true, array2[3]));
					}
					catch (Exception ex)
					{
						this.OnUploadDataCompleted(new UploadDataCompletedEventArgs(null, ex, false, array2[3]));
					}
				});
				object[] array = new object[] { address, method, data, userToken };
				this.async_thread.Start(array);
			}
		}

		/// <summary>Uploads the specified local file to the specified resource, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="fileName">The file to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x06002524 RID: 9508 RVA: 0x00070750 File Offset: 0x0006E950
		public void UploadFileAsync(global::System.Uri address, string fileName)
		{
			this.UploadFileAsync(address, null, fileName);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="method">The HTTP method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource. </param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x06002525 RID: 9509 RVA: 0x0007075C File Offset: 0x0006E95C
		public void UploadFileAsync(global::System.Uri address, string method, string fileName)
		{
			this.UploadFileAsync(address, method, fileName, null);
		}

		/// <summary>Uploads the specified local file to the specified resource, using the POST method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the data to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="fileName">The file to send to the resource.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- <paramref name="fileName" /> is null, is <see cref="F:System.String.Empty" />, contains invalid character, or the specified path to the file does not exist.-or- An error occurred while opening the stream.-or- There was no response from the server hosting the resource.-or- The Content-type header begins with multipart. </exception>
		// Token: 0x06002526 RID: 9510 RVA: 0x00070768 File Offset: 0x0006E968
		public void UploadFileAsync(global::System.Uri address, string method, string fileName, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			lock (this)
			{
				this.SetBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array2 = (object[])state;
					try
					{
						byte[] array3 = this.UploadFileCore((global::System.Uri)array2[0], (string)array2[1], (string)array2[2], array2[3]);
						this.OnUploadFileCompleted(new UploadFileCompletedEventArgs(array3, null, false, array2[3]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnUploadFileCompleted(new UploadFileCompletedEventArgs(null, null, true, array2[3]));
					}
					catch (Exception ex)
					{
						this.OnUploadFileCompleted(new UploadFileCompletedEventArgs(null, ex, false, array2[3]));
					}
				});
				object[] array = new object[] { address, method, fileName, userToken };
				this.async_thread.Start(array);
			}
		}

		/// <summary>Uploads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page. </param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x06002527 RID: 9511 RVA: 0x00070818 File Offset: 0x0006EA18
		public void UploadStringAsync(global::System.Uri address, string data)
		{
			this.UploadStringAsync(address, null, data);
		}

		/// <summary>Uploads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x06002528 RID: 9512 RVA: 0x00070824 File Offset: 0x0006EA24
		public void UploadStringAsync(global::System.Uri address, string method, string data)
		{
			this.UploadStringAsync(address, method, data, null);
		}

		/// <summary>Uploads the specified string to the specified resource. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the file. For HTTP resources, this URI must identify a resource that can accept a request sent with the POST method, such as a script or ASP page.</param>
		/// <param name="method">The HTTP method used to send the file to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The string to be uploaded.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x06002529 RID: 9513 RVA: 0x00070830 File Offset: 0x0006EA30
		public void UploadStringAsync(global::System.Uri address, string method, string data, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			lock (this)
			{
				this.CheckBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array2 = (object[])state;
					try
					{
						string text = this.UploadString((global::System.Uri)array2[0], (string)array2[1], (string)array2[2]);
						this.OnUploadStringCompleted(new UploadStringCompletedEventArgs(text, null, false, array2[3]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnUploadStringCompleted(new UploadStringCompletedEventArgs(null, null, true, array2[3]));
					}
					catch (Exception ex)
					{
						this.OnUploadStringCompleted(new UploadStringCompletedEventArgs(null, ex, false, array2[3]));
					}
				});
				object[] array = new object[] { address, method, data, userToken };
				this.async_thread.Start(array);
			}
		}

		/// <summary>Uploads the data in the specified name/value collection to the resource identified by the specified URI. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the collection. This URI must identify a resource that can accept a request sent with the default method. See remarks.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.</exception>
		// Token: 0x0600252A RID: 9514 RVA: 0x000708E0 File Offset: 0x0006EAE0
		public void UploadValuesAsync(global::System.Uri address, global::System.Collections.Specialized.NameValueCollection values)
		{
			this.UploadValuesAsync(address, null, values);
		}

		/// <summary>Uploads the data in the specified name/value collection to the resource identified by the specified URI, using the specified method. This method does not block the calling thread.</summary>
		/// <param name="address">The URI of the resource to receive the collection. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method.</param>
		/// <param name="method">The method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null. -or- <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.-or-<paramref name="method" /> cannot be used to send content.</exception>
		// Token: 0x0600252B RID: 9515 RVA: 0x000708EC File Offset: 0x0006EAEC
		public void UploadValuesAsync(global::System.Uri address, string method, global::System.Collections.Specialized.NameValueCollection values)
		{
			this.UploadValuesAsync(address, method, values, null);
		}

		/// <summary>Uploads the data in the specified name/value collection to the resource identified by the specified URI, using the specified method. This method does not block the calling thread, and allows the caller to pass an object to the method that is invoked when the operation completes.</summary>
		/// <param name="address">The URI of the resource to receive the collection. This URI must identify a resource that can accept a request sent with the <paramref name="method" /> method.</param>
		/// <param name="method">The HTTP method used to send the string to the resource. If null, the default is POST for http and STOR for ftp.</param>
		/// <param name="data">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to send to the resource.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null. -or- <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.Net.WebException">The URI formed by combining <see cref="P:System.Net.WebClient.BaseAddress" /> and <paramref name="address" /> is invalid.-or- There was no response from the server hosting the resource.-or-<paramref name="method" /> cannot be used to send content.</exception>
		// Token: 0x0600252C RID: 9516 RVA: 0x000708F8 File Offset: 0x0006EAF8
		public void UploadValuesAsync(global::System.Uri address, string method, global::System.Collections.Specialized.NameValueCollection values, object userToken)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			lock (this)
			{
				this.CheckBusy();
				this.async = true;
				this.async_thread = new Thread(delegate(object state)
				{
					object[] array2 = (object[])state;
					try
					{
						byte[] array3 = this.UploadValuesCore((global::System.Uri)array2[0], (string)array2[1], (global::System.Collections.Specialized.NameValueCollection)array2[2], array2[3]);
						this.OnUploadValuesCompleted(new UploadValuesCompletedEventArgs(array3, null, false, array2[3]));
					}
					catch (ThreadInterruptedException)
					{
						this.OnUploadValuesCompleted(new UploadValuesCompletedEventArgs(null, null, true, array2[3]));
					}
					catch (Exception ex)
					{
						this.OnUploadValuesCompleted(new UploadValuesCompletedEventArgs(null, ex, false, array2[3]));
					}
				});
				object[] array = new object[] { address, method, values, userToken };
				this.async_thread.Start(array);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadDataCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.DownloadDataCompletedEventArgs" /> object that contains event data.</param>
		// Token: 0x0600252D RID: 9517 RVA: 0x000709A8 File Offset: 0x0006EBA8
		protected virtual void OnDownloadDataCompleted(DownloadDataCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.DownloadDataCompleted != null)
			{
				this.DownloadDataCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadFileCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x0600252E RID: 9518 RVA: 0x000709C8 File Offset: 0x0006EBC8
		protected virtual void OnDownloadFileCompleted(global::System.ComponentModel.AsyncCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.DownloadFileCompleted != null)
			{
				this.DownloadFileCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadProgressChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.DownloadProgressChangedEventArgs" /> object containing event data.</param>
		// Token: 0x0600252F RID: 9519 RVA: 0x000709E8 File Offset: 0x0006EBE8
		protected virtual void OnDownloadProgressChanged(DownloadProgressChangedEventArgs e)
		{
			if (this.DownloadProgressChanged != null)
			{
				this.DownloadProgressChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.DownloadStringCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.DownloadStringCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06002530 RID: 9520 RVA: 0x00070A04 File Offset: 0x0006EC04
		protected virtual void OnDownloadStringCompleted(DownloadStringCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.DownloadStringCompleted != null)
			{
				this.DownloadStringCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.OpenReadCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.OpenReadCompletedEventArgs" />  object containing event data.</param>
		// Token: 0x06002531 RID: 9521 RVA: 0x00070A24 File Offset: 0x0006EC24
		protected virtual void OnOpenReadCompleted(OpenReadCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.OpenReadCompleted != null)
			{
				this.OpenReadCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.OpenWriteCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.OpenWriteCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06002532 RID: 9522 RVA: 0x00070A44 File Offset: 0x0006EC44
		protected virtual void OnOpenWriteCompleted(OpenWriteCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.OpenWriteCompleted != null)
			{
				this.OpenWriteCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadDataCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.UploadDataCompletedEventArgs" />  object containing event data.</param>
		// Token: 0x06002533 RID: 9523 RVA: 0x00070A64 File Offset: 0x0006EC64
		protected virtual void OnUploadDataCompleted(UploadDataCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.UploadDataCompleted != null)
			{
				this.UploadDataCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadFileCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Net.UploadFileCompletedEventArgs" /> object containing event data.</param>
		// Token: 0x06002534 RID: 9524 RVA: 0x00070A84 File Offset: 0x0006EC84
		protected virtual void OnUploadFileCompleted(UploadFileCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.UploadFileCompleted != null)
			{
				this.UploadFileCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadProgressChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Net.UploadProgressChangedEventArgs" /> object containing event data.</param>
		// Token: 0x06002535 RID: 9525 RVA: 0x00070AA4 File Offset: 0x0006ECA4
		protected virtual void OnUploadProgressChanged(UploadProgressChangedEventArgs e)
		{
			if (this.UploadProgressChanged != null)
			{
				this.UploadProgressChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadStringCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Net.UploadStringCompletedEventArgs" />  object containing event data.</param>
		// Token: 0x06002536 RID: 9526 RVA: 0x00070AC0 File Offset: 0x0006ECC0
		protected virtual void OnUploadStringCompleted(UploadStringCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.UploadStringCompleted != null)
			{
				this.UploadStringCompleted(this, args);
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.WebClient.UploadValuesCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.UploadValuesCompletedEventArgs" />  object containing event data.</param>
		// Token: 0x06002537 RID: 9527 RVA: 0x00070AE0 File Offset: 0x0006ECE0
		protected virtual void OnUploadValuesCompleted(UploadValuesCompletedEventArgs args)
		{
			this.CompleteAsync();
			if (this.UploadValuesCompleted != null)
			{
				this.UploadValuesCompleted(this, args);
			}
		}

		/// <summary>Returns the <see cref="T:System.Net.WebResponse" /> for the specified <see cref="T:System.Net.WebRequest" /> using the specified <see cref="T:System.IAsyncResult" />.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> containing the response for the specified <see cref="T:System.Net.WebRequest" />.</returns>
		/// <param name="request">A <see cref="T:System.Net.WebRequest" /> that is used to obtain the response.</param>
		/// <param name="result">An <see cref="T:System.IAsyncResult" /> object obtained from a previous call to <see cref="M:System.Net.WebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> .</param>
		// Token: 0x06002538 RID: 9528 RVA: 0x00070B00 File Offset: 0x0006ED00
		protected virtual WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
		{
			WebResponse webResponse = request.EndGetResponse(result);
			this.responseHeaders = webResponse.Headers;
			return webResponse;
		}

		/// <summary>Returns a <see cref="T:System.Net.WebRequest" /> object for the specified resource.</summary>
		/// <returns>A new <see cref="T:System.Net.WebRequest" /> object for the specified resource.</returns>
		/// <param name="address">A <see cref="T:System.Uri" /> that identifies the resource to request.</param>
		// Token: 0x06002539 RID: 9529 RVA: 0x00070B24 File Offset: 0x0006ED24
		protected virtual WebRequest GetWebRequest(global::System.Uri address)
		{
			return WebRequest.Create(address);
		}

		/// <summary>Returns the <see cref="T:System.Net.WebResponse" /> for the specified <see cref="T:System.Net.WebRequest" />.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> containing the response for the specified <see cref="T:System.Net.WebRequest" />.</returns>
		/// <param name="request">A <see cref="T:System.Net.WebRequest" /> that is used to obtain the response. </param>
		// Token: 0x0600253A RID: 9530 RVA: 0x00070B2C File Offset: 0x0006ED2C
		protected virtual WebResponse GetWebResponse(WebRequest request)
		{
			WebResponse response = request.GetResponse();
			this.responseHeaders = response.Headers;
			return response;
		}

		// Token: 0x040016E9 RID: 5865
		private static readonly string urlEncodedCType = "application/x-www-form-urlencoded";

		// Token: 0x040016EA RID: 5866
		private static byte[] hexBytes = new byte[16];

		// Token: 0x040016EB RID: 5867
		private ICredentials credentials;

		// Token: 0x040016EC RID: 5868
		private WebHeaderCollection headers;

		// Token: 0x040016ED RID: 5869
		private WebHeaderCollection responseHeaders;

		// Token: 0x040016EE RID: 5870
		private global::System.Uri baseAddress;

		// Token: 0x040016EF RID: 5871
		private string baseString;

		// Token: 0x040016F0 RID: 5872
		private global::System.Collections.Specialized.NameValueCollection queryString;

		// Token: 0x040016F1 RID: 5873
		private bool is_busy;

		// Token: 0x040016F2 RID: 5874
		private bool async;

		// Token: 0x040016F3 RID: 5875
		private Thread async_thread;

		// Token: 0x040016F4 RID: 5876
		private Encoding encoding = Encoding.Default;

		// Token: 0x040016F5 RID: 5877
		private IWebProxy proxy;
	}
}
