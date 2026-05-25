using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Cache;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Net
{
	/// <summary>Implements a File Transfer Protocol (FTP) client.</summary>
	// Token: 0x0200030B RID: 779
	public sealed class FtpWebRequest : WebRequest
	{
		// Token: 0x06001AD8 RID: 6872 RVA: 0x0004BAA0 File Offset: 0x00049CA0
		internal FtpWebRequest(global::System.Uri uri)
		{
			this.requestUri = uri;
			this.proxy = GlobalProxySelection.Select;
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x0004BBAC File Offset: 0x00049DAC
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets the certificates used for establishing an encrypted connection to the FTP server.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> object that contains the client certificates.</returns>
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0004BBB4 File Offset: 0x00049DB4
		// (set) Token: 0x06001ADC RID: 6876 RVA: 0x0004BBBC File Offset: 0x00049DBC
		[global::System.MonoTODO]
		public global::System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates
		{
			get
			{
				throw FtpWebRequest.GetMustImplement();
			}
			set
			{
				throw FtpWebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the name of the connection group that contains the service point used to send the current request.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains a connection group name.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001ADD RID: 6877 RVA: 0x0004BBC4 File Offset: 0x00049DC4
		// (set) Token: 0x06001ADE RID: 6878 RVA: 0x0004BBCC File Offset: 0x00049DCC
		[global::System.MonoTODO]
		public override string ConnectionGroupName
		{
			get
			{
				throw FtpWebRequest.GetMustImplement();
			}
			set
			{
				throw FtpWebRequest.GetMustImplement();
			}
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Content type information is not supported for FTP.</exception>
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x0004BBD4 File Offset: 0x00049DD4
		// (set) Token: 0x06001AE0 RID: 6880 RVA: 0x0004BBDC File Offset: 0x00049DDC
		public override string ContentType
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that is ignored by the <see cref="T:System.Net.FtpWebRequest" /> class.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that should be ignored.</returns>
		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001AE1 RID: 6881 RVA: 0x0004BBE4 File Offset: 0x00049DE4
		// (set) Token: 0x06001AE2 RID: 6882 RVA: 0x0004BBE8 File Offset: 0x00049DE8
		public override long ContentLength
		{
			get
			{
				return 0L;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets a byte offset into the file being downloaded by this request.</summary>
		/// <returns>An <see cref="T:System.Int64" /> instance that specifies the file offset, in bytes. The default value is zero.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for this property is less than zero. </exception>
		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001AE3 RID: 6883 RVA: 0x0004BBEC File Offset: 0x00049DEC
		// (set) Token: 0x06001AE4 RID: 6884 RVA: 0x0004BBF4 File Offset: 0x00049DF4
		public long ContentOffset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.CheckRequestStarted();
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.offset = value;
			}
		}

		/// <summary>Gets or sets the credentials used to communicate with the FTP server.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance; otherwise, null if the property has not been set.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">An <see cref="T:System.Net.ICredentials" /> of a type other than <see cref="T:System.Net.NetworkCredential" /> was specified for a set operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x0004BC14 File Offset: 0x00049E14
		// (set) Token: 0x06001AE6 RID: 6886 RVA: 0x0004BC1C File Offset: 0x00049E1C
		public override ICredentials Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.CheckRequestStarted();
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (!(value is NetworkCredential))
				{
					throw new ArgumentException();
				}
				this.credentials = value as NetworkCredential;
			}
		}

		/// <summary>Defines the default cache policy for all FTP requests.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCachePolicy" /> that defines the cache policy for FTP requests.</returns>
		/// <exception cref="T:System.ArgumentNullException">The caller tried to set this property to null.</exception>
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x0004BC50 File Offset: 0x00049E50
		// (set) Token: 0x06001AE8 RID: 6888 RVA: 0x0004BC58 File Offset: 0x00049E58
		[global::System.MonoTODO]
		public new static global::System.Net.Cache.RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				throw FtpWebRequest.GetMustImplement();
			}
			set
			{
				throw FtpWebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> that specifies that an SSL connection should be used.</summary>
		/// <returns>true if control and data transmissions are encrypted; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection to the FTP server has already been established.</exception>
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x0004BC60 File Offset: 0x00049E60
		// (set) Token: 0x06001AEA RID: 6890 RVA: 0x0004BC68 File Offset: 0x00049E68
		public bool EnableSsl
		{
			get
			{
				return this.enableSsl;
			}
			set
			{
				this.CheckRequestStarted();
				this.enableSsl = value;
			}
		}

		/// <summary>Gets an empty <see cref="T:System.Net.WebHeaderCollection" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Net.WebHeaderCollection" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x0004BC78 File Offset: 0x00049E78
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x0004BC80 File Offset: 0x00049E80
		[global::System.MonoTODO]
		public override WebHeaderCollection Headers
		{
			get
			{
				throw FtpWebRequest.GetMustImplement();
			}
			set
			{
				throw FtpWebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the control connection to the FTP server is closed after the request completes.</summary>
		/// <returns>true if the connection to the server should not be destroyed; otherwise, false. The default value is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x0004BC88 File Offset: 0x00049E88
		// (set) Token: 0x06001AEE RID: 6894 RVA: 0x0004BC90 File Offset: 0x00049E90
		[global::System.MonoTODO("We don't support KeepAlive = true")]
		public bool KeepAlive
		{
			get
			{
				return this.keepAlive;
			}
			set
			{
				this.CheckRequestStarted();
			}
		}

		/// <summary>Gets or sets the command to send to the FTP server.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the FTP command to send to the server. The default value is <see cref="F:System.Net.WebRequestMethods.Ftp.DownloadFile" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <exception cref="T:System.ArgumentException">The method is invalid.- or -The method is not supported.- or -Multiple methods were specified.</exception>
		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x0004BC98 File Offset: 0x00049E98
		// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x0004BCA0 File Offset: 0x00049EA0
		public override string Method
		{
			get
			{
				return this.method;
			}
			set
			{
				this.CheckRequestStarted();
				if (value == null)
				{
					throw new ArgumentNullException("Method string cannot be null");
				}
				if (value.Length == 0 || Array.BinarySearch<string>(FtpWebRequest.supportedCommands, value) < 0)
				{
					throw new ArgumentException("Method not supported", "value");
				}
				this.method = value;
			}
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Preauthentication is not supported for FTP.</exception>
		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x0004BCF8 File Offset: 0x00049EF8
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x0004BD00 File Offset: 0x00049F00
		public override bool PreAuthenticate
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the proxy used to communicate with the FTP server.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> instance responsible for communicating with the FTP server.</returns>
		/// <exception cref="T:System.ArgumentNullException">This property cannot be set to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0004BD08 File Offset: 0x00049F08
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x0004BD10 File Offset: 0x00049F10
		public override IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				this.CheckRequestStarted();
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.proxy = value;
			}
		}

		/// <summary>Gets or sets a time-out when reading from or writing to a stream.</summary>
		/// <returns>The number of milliseconds before the reading or writing times out. The default value is 300,000 milliseconds (5 minutes).</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has already been sent. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x0004BD2C File Offset: 0x00049F2C
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x0004BD34 File Offset: 0x00049F34
		public int ReadWriteTimeout
		{
			get
			{
				return this.rwTimeout;
			}
			set
			{
				this.CheckRequestStarted();
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.rwTimeout = value;
			}
		}

		/// <summary>Gets or sets the new name of a file being renamed.</summary>
		/// <returns>The new name of the file being renamed.</returns>
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x0004BD50 File Offset: 0x00049F50
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x0004BD58 File Offset: 0x00049F58
		public string RenameTo
		{
			get
			{
				return this.renameTo;
			}
			set
			{
				this.CheckRequestStarted();
				if (value == null || value.Length == 0)
				{
					throw new ArgumentException("RenameTo value can't be null or empty", "RenameTo");
				}
				this.renameTo = value;
			}
		}

		/// <summary>Gets the URI requested by this instance.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that identifies a resource that is accessed using the File Transfer Protocol.</returns>
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x0004BD94 File Offset: 0x00049F94
		public override global::System.Uri RequestUri
		{
			get
			{
				return this.requestUri;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.ServicePoint" /> object used to connect to the FTP server.</summary>
		/// <returns>A <see cref="T:System.Net.ServicePoint" /> object that can be used to customize connection behavior.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x0004BD9C File Offset: 0x00049F9C
		public ServicePoint ServicePoint
		{
			get
			{
				return this.GetServicePoint();
			}
		}

		/// <summary>Gets or sets the behavior of a client application's data transfer process.</summary>
		/// <returns>false if the client application's data transfer process listens for a connection on the data port; otherwise, true if the client should initiate a connection on the data port. The default value is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0004BDA4 File Offset: 0x00049FA4
		// (set) Token: 0x06001AFC RID: 6908 RVA: 0x0004BDAC File Offset: 0x00049FAC
		public bool UsePassive
		{
			get
			{
				return this.usePassive;
			}
			set
			{
				this.CheckRequestStarted();
				this.usePassive = value;
			}
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Default credentials are not supported for FTP.</exception>
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001AFD RID: 6909 RVA: 0x0004BDBC File Offset: 0x00049FBC
		// (set) Token: 0x06001AFE RID: 6910 RVA: 0x0004BDC4 File Offset: 0x00049FC4
		[global::System.MonoTODO]
		public override bool UseDefaultCredentials
		{
			get
			{
				throw FtpWebRequest.GetMustImplement();
			}
			set
			{
				throw FtpWebRequest.GetMustImplement();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies the data type for file transfers.</summary>
		/// <returns>true to indicate to the server that the data to be transferred is binary; false to indicate that the data is text. The default value is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress.</exception>
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0004BDCC File Offset: 0x00049FCC
		// (set) Token: 0x06001B00 RID: 6912 RVA: 0x0004BDD4 File Offset: 0x00049FD4
		public bool UseBinary
		{
			get
			{
				return this.binary;
			}
			set
			{
				this.CheckRequestStarted();
				this.binary = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds to wait for a request.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of milliseconds to wait before a request times out. The default value is <see cref="F:System.Threading.Timeout.Infinite" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than zero and is not <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x0004BDE4 File Offset: 0x00049FE4
		// (set) Token: 0x06001B02 RID: 6914 RVA: 0x0004BDEC File Offset: 0x00049FEC
		public override int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.CheckRequestStarted();
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.timeout = value;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x0004BE08 File Offset: 0x0004A008
		private string DataType
		{
			get
			{
				return (!this.binary) ? "A" : "I";
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x0004BE24 File Offset: 0x0004A024
		// (set) Token: 0x06001B05 RID: 6917 RVA: 0x0004BE74 File Offset: 0x0004A074
		private FtpWebRequest.RequestState State
		{
			get
			{
				object obj = this.locker;
				FtpWebRequest.RequestState requestState;
				lock (obj)
				{
					requestState = this.requestState;
				}
				return requestState;
			}
			set
			{
				object obj = this.locker;
				lock (obj)
				{
					this.CheckIfAborted();
					this.CheckFinalState();
					this.requestState = value;
				}
			}
		}

		/// <summary>Terminates an asynchronous FTP operation.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B06 RID: 6918 RVA: 0x0004BECC File Offset: 0x0004A0CC
		public override void Abort()
		{
			object obj = this.locker;
			lock (obj)
			{
				if (this.State == FtpWebRequest.RequestState.TransferInProgress)
				{
					this.SendCommand(false, "ABOR", new string[0]);
				}
				if (!this.InFinalState())
				{
					this.State = FtpWebRequest.RequestState.Aborted;
					this.ftpResponse = new FtpWebResponse(this, this.requestUri, this.method, FtpStatusCode.FileActionAborted, "Aborted by request");
				}
			}
		}

		/// <summary>Begins sending a request and receiving a response from an FTP server asynchronously.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that indicates the status of the operation.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="callback" /> delegate when the operation completes. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.FtpWebRequest.GetResponse" /> or <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> has already been called for this instance. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B07 RID: 6919 RVA: 0x0004BF64 File Offset: 0x0004A164
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			if (this.asyncResult != null && !this.asyncResult.IsCompleted)
			{
				throw new InvalidOperationException("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress");
			}
			this.CheckIfAborted();
			this.asyncResult = new FtpAsyncResult(callback, state);
			object obj = this.locker;
			lock (obj)
			{
				if (this.InFinalState())
				{
					this.asyncResult.SetCompleted(true, this.ftpResponse);
				}
				else
				{
					if (this.State == FtpWebRequest.RequestState.Before)
					{
						this.State = FtpWebRequest.RequestState.Scheduled;
					}
					Thread thread = new Thread(new ThreadStart(this.ProcessRequest));
					thread.Start();
				}
			}
			return this.asyncResult;
		}

		/// <summary>Ends a pending asynchronous operation started with <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> reference that contains an <see cref="T:System.Net.FtpWebResponse" /> instance. This object contains the FTP server's response to the request.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> that was returned when the operation started. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was already called for the operation identified by <paramref name="asyncResult" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B08 RID: 6920 RVA: 0x0004C034 File Offset: 0x0004A234
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("AsyncResult cannot be null!");
			}
			if (!(asyncResult is FtpAsyncResult) || asyncResult != this.asyncResult)
			{
				throw new ArgumentException("AsyncResult is from another request!");
			}
			FtpAsyncResult ftpAsyncResult = (FtpAsyncResult)asyncResult;
			if (!ftpAsyncResult.WaitUntilComplete(this.timeout, false))
			{
				this.Abort();
				throw new WebException("Transfer timed out.", WebExceptionStatus.Timeout);
			}
			this.CheckIfAborted();
			asyncResult = null;
			if (ftpAsyncResult.GotException)
			{
				throw ftpAsyncResult.Exception;
			}
			return ftpAsyncResult.Response;
		}

		/// <summary>Returns the FTP server response.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> reference that contains an <see cref="T:System.Net.FtpWebResponse" /> instance. This object contains the FTP server's response to the request.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.FtpWebRequest.GetResponse" /> or <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> has already been called for this instance.- or -An HTTP proxy is enabled, and you attempted to use an FTP command other than <see cref="F:System.Net.WebRequestMethods.Ftp.DownloadFile" />, <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectory" />, or <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectoryDetails" />.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="P:System.Net.FtpWebRequest.EnableSsl" /> is set to true, but the server does not support this feature.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B09 RID: 6921 RVA: 0x0004C0C4 File Offset: 0x0004A2C4
		public override WebResponse GetResponse()
		{
			IAsyncResult asyncResult = this.BeginGetResponse(null, null);
			return this.EndGetResponse(asyncResult);
		}

		/// <summary>Begins asynchronously opening a request's content stream for writing.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that indicates the status of the operation.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="callback" /> delegate when the operation completes. </param>
		/// <exception cref="T:System.InvalidOperationException">A previous call to this method or <see cref="M:System.Net.FtpWebRequest.GetRequestStream" /> has not yet completed. </exception>
		/// <exception cref="T:System.Net.WebException">A connection to the FTP server could not be established. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.FtpWebRequest.Method" /> property is not set to <see cref="F:System.Net.WebRequestMethods.Ftp.UploadFile" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B0A RID: 6922 RVA: 0x0004C0E4 File Offset: 0x0004A2E4
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			if (this.method != "STOR" && this.method != "STOU" && this.method != "APPE")
			{
				throw new ProtocolViolationException();
			}
			object obj = this.locker;
			lock (obj)
			{
				this.CheckIfAborted();
				if (this.State != FtpWebRequest.RequestState.Before)
				{
					throw new InvalidOperationException("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress");
				}
				this.State = FtpWebRequest.RequestState.Scheduled;
			}
			this.asyncResult = new FtpAsyncResult(callback, state);
			Thread thread = new Thread(new ThreadStart(this.ProcessRequest));
			thread.Start();
			return this.asyncResult;
		}

		/// <summary>Ends a pending asynchronous operation started with <see cref="M:System.Net.FtpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />.</summary>
		/// <returns>A writable <see cref="T:System.IO.Stream" /> instance associated with this instance.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> object that was returned when the operation started. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling <see cref="M:System.Net.FtpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was already called for the operation identified by <paramref name="asyncResult" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B0B RID: 6923 RVA: 0x0004C1BC File Offset: 0x0004A3BC
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (!(asyncResult is FtpAsyncResult))
			{
				throw new ArgumentException("asyncResult");
			}
			if (this.State == FtpWebRequest.RequestState.Aborted)
			{
				throw new WebException("Request aborted", WebExceptionStatus.RequestCanceled);
			}
			if (asyncResult != this.asyncResult)
			{
				throw new ArgumentException("AsyncResult is from another request!");
			}
			FtpAsyncResult ftpAsyncResult = (FtpAsyncResult)asyncResult;
			if (!ftpAsyncResult.WaitUntilComplete(this.timeout, false))
			{
				this.Abort();
				throw new WebException("Request timed out");
			}
			if (ftpAsyncResult.GotException)
			{
				throw ftpAsyncResult.Exception;
			}
			return ftpAsyncResult.Stream;
		}

		/// <summary>Retrieves the stream used to upload data to an FTP server.</summary>
		/// <returns>A writable <see cref="T:System.IO.Stream" /> instance used to store data to be sent to the server by the current request.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.FtpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" /> has been called and has not completed. - or -An HTTP proxy is enabled, and you attempted to use an FTP command other than <see cref="F:System.Net.WebRequestMethods.Ftp.DownloadFile" />, <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectory" />, or <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectoryDetails" />.</exception>
		/// <exception cref="T:System.Net.WebException">A connection to the FTP server could not be established. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.FtpWebRequest.Method" /> property is not set to <see cref="F:System.Net.WebRequestMethods.Ftp.UploadFile" /> or <see cref="F:System.Net.WebRequestMethods.Ftp.AppendFile" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B0C RID: 6924 RVA: 0x0004C264 File Offset: 0x0004A464
		public override Stream GetRequestStream()
		{
			IAsyncResult asyncResult = this.BeginGetRequestStream(null, null);
			return this.EndGetRequestStream(asyncResult);
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0004C284 File Offset: 0x0004A484
		private ServicePoint GetServicePoint()
		{
			if (this.servicePoint == null)
			{
				this.servicePoint = ServicePointManager.FindServicePoint(this.requestUri, this.proxy);
			}
			return this.servicePoint;
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0004C2BC File Offset: 0x0004A4BC
		private void ResolveHost()
		{
			this.CheckIfAborted();
			this.hostEntry = this.GetServicePoint().HostEntry;
			if (this.hostEntry == null)
			{
				this.ftpResponse.UpdateStatus(new FtpStatus(FtpStatusCode.ActionAbortedLocalProcessingError, "Cannot resolve server name"));
				throw new WebException("The remote server name could not be resolved: " + this.requestUri, null, WebExceptionStatus.NameResolutionFailure, this.ftpResponse);
			}
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0004C324 File Offset: 0x0004A524
		private void ProcessRequest()
		{
			if (this.State == FtpWebRequest.RequestState.Scheduled)
			{
				this.ftpResponse = new FtpWebResponse(this, this.requestUri, this.method, this.keepAlive);
				try
				{
					this.ProcessMethod();
					this.asyncResult.SetCompleted(false, this.ftpResponse);
				}
				catch (Exception ex)
				{
					this.State = FtpWebRequest.RequestState.Error;
					this.SetCompleteWithError(ex);
				}
			}
			else
			{
				if (this.InProgress())
				{
					FtpStatus responseStatus = this.GetResponseStatus();
					this.ftpResponse.UpdateStatus(responseStatus);
					if (this.ftpResponse.IsFinal())
					{
						this.State = FtpWebRequest.RequestState.Finished;
					}
				}
				this.asyncResult.SetCompleted(false, this.ftpResponse);
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0004C3F4 File Offset: 0x0004A5F4
		private void SetType()
		{
			if (this.binary)
			{
				FtpStatus ftpStatus = this.SendCommand("TYPE", new string[] { this.DataType });
				if (ftpStatus.StatusCode < FtpStatusCode.CommandOK || ftpStatus.StatusCode >= (FtpStatusCode)300)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
			}
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0004C450 File Offset: 0x0004A650
		private string GetRemoteFolderPath(global::System.Uri uri)
		{
			string text = global::System.Uri.UnescapeDataString(uri.LocalPath);
			string text2;
			if (this.initial_path == null || this.initial_path == "/")
			{
				text2 = text;
			}
			else
			{
				if (text[0] == '/')
				{
					text = text.Substring(1);
				}
				global::System.Uri uri2 = new global::System.Uri("ftp://dummy-host" + this.initial_path);
				text2 = new global::System.Uri(uri2, text).LocalPath;
			}
			int num = text2.LastIndexOf('/');
			if (num == -1)
			{
				return null;
			}
			return text2.Substring(0, num + 1);
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x0004C4E8 File Offset: 0x0004A6E8
		private void CWDAndSetFileName(global::System.Uri uri)
		{
			string remoteFolderPath = this.GetRemoteFolderPath(uri);
			if (remoteFolderPath != null)
			{
				FtpStatus ftpStatus = this.SendCommand("CWD", new string[] { remoteFolderPath });
				if (ftpStatus.StatusCode < FtpStatusCode.CommandOK || ftpStatus.StatusCode >= (FtpStatusCode)300)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				int num = uri.LocalPath.LastIndexOf('/');
				if (num >= 0)
				{
					this.file_name = global::System.Uri.UnescapeDataString(uri.LocalPath.Substring(num + 1));
				}
			}
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x0004C570 File Offset: 0x0004A770
		private void ProcessMethod()
		{
			this.State = FtpWebRequest.RequestState.Connecting;
			this.ResolveHost();
			this.OpenControlConnection();
			this.CWDAndSetFileName(this.requestUri);
			this.SetType();
			string text = this.method;
			if (text != null)
			{
				if (FtpWebRequest.<>f__switch$mapA == null)
				{
					FtpWebRequest.<>f__switch$mapA = new Dictionary<string, int>(12)
					{
						{ "RETR", 0 },
						{ "NLST", 0 },
						{ "LIST", 0 },
						{ "APPE", 1 },
						{ "STOR", 1 },
						{ "STOU", 1 },
						{ "SIZE", 2 },
						{ "MDTM", 2 },
						{ "PWD", 2 },
						{ "MKD", 2 },
						{ "RENAME", 2 },
						{ "DELE", 2 }
					};
				}
				int num;
				if (FtpWebRequest.<>f__switch$mapA.TryGetValue(text, out num))
				{
					switch (num)
					{
					case 0:
						this.DownloadData();
						break;
					case 1:
						this.UploadData();
						break;
					case 2:
						this.ProcessSimpleMethod();
						break;
					default:
						goto IL_0124;
					}
					this.CheckIfAborted();
					return;
				}
			}
			IL_0124:
			throw new Exception(string.Format("Support for command {0} not implemented yet", this.method));
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0004C6C0 File Offset: 0x0004A8C0
		private void CloseControlConnection()
		{
			if (this.controlStream != null)
			{
				this.SendCommand("QUIT", new string[0]);
				this.controlStream.Close();
				this.controlStream = null;
			}
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0004C6F4 File Offset: 0x0004A8F4
		internal void CloseDataConnection()
		{
			if (this.origDataStream != null)
			{
				this.origDataStream.Close();
				this.origDataStream = null;
			}
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0004C714 File Offset: 0x0004A914
		private void CloseConnection()
		{
			this.CloseControlConnection();
			this.CloseDataConnection();
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0004C724 File Offset: 0x0004A924
		private void ProcessSimpleMethod()
		{
			this.State = FtpWebRequest.RequestState.TransferInProgress;
			if (this.method == "PWD")
			{
				this.method = "PWD";
			}
			if (this.method == "RENAME")
			{
				this.method = "RNFR";
			}
			FtpStatus ftpStatus = this.SendCommand(this.method, new string[] { this.file_name });
			this.ftpResponse.Stream = Stream.Null;
			string statusDescription = ftpStatus.StatusDescription;
			string text = this.method;
			switch (text)
			{
			case "SIZE":
			{
				if (ftpStatus.StatusCode != FtpStatusCode.FileStatus)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				int num2 = 4;
				int num3 = 0;
				while (num2 < statusDescription.Length && char.IsDigit(statusDescription[num2]))
				{
					num2++;
					num3++;
				}
				if (num3 == 0)
				{
					throw new WebException("Bad format for server response in " + this.method);
				}
				long num4;
				if (!long.TryParse(statusDescription.Substring(4, num3), out num4))
				{
					throw new WebException("Bad format for server response in " + this.method);
				}
				this.ftpResponse.contentLength = num4;
				break;
			}
			case "MDTM":
				if (ftpStatus.StatusCode != FtpStatusCode.FileStatus)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				this.ftpResponse.LastModified = DateTime.ParseExact(statusDescription.Substring(4), "yyyyMMddHHmmss", null);
				break;
			case "MKD":
				if (ftpStatus.StatusCode != FtpStatusCode.PathnameCreated)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				break;
			case "CWD":
				this.method = "PWD";
				if (ftpStatus.StatusCode != FtpStatusCode.FileActionOK)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				ftpStatus = this.SendCommand(this.method, new string[0]);
				if (ftpStatus.StatusCode != FtpStatusCode.PathnameCreated)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				break;
			case "RNFR":
				this.method = "RENAME";
				if (ftpStatus.StatusCode != FtpStatusCode.FileCommandPending)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				ftpStatus = this.SendCommand("RNTO", new string[] { (this.renameTo == null) ? string.Empty : this.renameTo });
				if (ftpStatus.StatusCode != FtpStatusCode.FileActionOK)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				break;
			case "DELE":
				if (ftpStatus.StatusCode != FtpStatusCode.FileActionOK)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				break;
			}
			this.State = FtpWebRequest.RequestState.Finished;
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x0004CA34 File Offset: 0x0004AC34
		private void UploadData()
		{
			this.State = FtpWebRequest.RequestState.OpeningData;
			this.OpenDataConnection();
			this.State = FtpWebRequest.RequestState.TransferInProgress;
			this.requestStream = new FtpDataStream(this, this.dataStream, false);
			this.asyncResult.Stream = this.requestStream;
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x0004CA7C File Offset: 0x0004AC7C
		private void DownloadData()
		{
			this.State = FtpWebRequest.RequestState.OpeningData;
			this.OpenDataConnection();
			this.State = FtpWebRequest.RequestState.TransferInProgress;
			this.ftpResponse.Stream = new FtpDataStream(this, this.dataStream, true);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x0004CAB8 File Offset: 0x0004ACB8
		private void CheckRequestStarted()
		{
			if (this.State != FtpWebRequest.RequestState.Before)
			{
				throw new InvalidOperationException("There is a request currently in progress");
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0004CAD0 File Offset: 0x0004ACD0
		private void OpenControlConnection()
		{
			Exception ex = null;
			global::System.Net.Sockets.Socket socket = null;
			foreach (IPAddress ipaddress in this.hostEntry.AddressList)
			{
				socket = new global::System.Net.Sockets.Socket(ipaddress.AddressFamily, global::System.Net.Sockets.SocketType.Stream, global::System.Net.Sockets.ProtocolType.Tcp);
				IPEndPoint ipendPoint = new IPEndPoint(ipaddress, this.requestUri.Port);
				if (!this.ServicePoint.CallEndPointDelegate(socket, ipendPoint))
				{
					socket.Close();
					socket = null;
				}
				else
				{
					try
					{
						socket.Connect(ipendPoint);
						this.localEndPoint = (IPEndPoint)socket.LocalEndPoint;
						break;
					}
					catch (global::System.Net.Sockets.SocketException ex2)
					{
						ex = ex2;
						socket.Close();
						socket = null;
					}
				}
			}
			if (socket == null)
			{
				throw new WebException("Unable to connect to remote server", ex, WebExceptionStatus.UnknownError, this.ftpResponse);
			}
			this.controlStream = new global::System.Net.Sockets.NetworkStream(socket);
			this.controlReader = new StreamReader(this.controlStream, Encoding.ASCII);
			this.State = FtpWebRequest.RequestState.Authenticating;
			this.Authenticate();
			FtpStatus ftpStatus = this.SendCommand("OPTS", new string[] { "utf8", "on" });
			ftpStatus = this.SendCommand("PWD", new string[0]);
			this.initial_path = FtpWebRequest.GetInitialPath(ftpStatus);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0004CC28 File Offset: 0x0004AE28
		private static string GetInitialPath(FtpStatus status)
		{
			int statusCode = (int)status.StatusCode;
			if (statusCode < 200 || statusCode > 300 || status.StatusDescription.Length <= 4)
			{
				throw new WebException("Error getting current directory: " + status.StatusDescription, null, WebExceptionStatus.UnknownError, null);
			}
			string text = status.StatusDescription.Substring(4);
			if (text[0] == '"')
			{
				int num = text.IndexOf('"', 1);
				if (num == -1)
				{
					throw new WebException("Error getting current directory: PWD -> " + status.StatusDescription, null, WebExceptionStatus.UnknownError, null);
				}
				text = text.Substring(1, num - 1);
			}
			if (!text.EndsWith("/"))
			{
				text += "/";
			}
			return text;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0004CCEC File Offset: 0x0004AEEC
		private global::System.Net.Sockets.Socket SetupPassiveConnection(string statusDescription)
		{
			if (statusDescription.Length < 4)
			{
				throw new WebException("Cannot open passive data connection");
			}
			int num = 3;
			while (num < statusDescription.Length && !char.IsDigit(statusDescription[num]))
			{
				num++;
			}
			if (num >= statusDescription.Length)
			{
				throw new WebException("Cannot open passive data connection");
			}
			string[] array = statusDescription.Substring(num).Split(new char[] { ',' }, 6);
			if (array.Length != 6)
			{
				throw new WebException("Cannot open passive data connection");
			}
			int num2 = array[5].Length - 1;
			while (num2 >= 0 && !char.IsDigit(array[5][num2]))
			{
				num2--;
			}
			if (num2 < 0)
			{
				throw new WebException("Cannot open passive data connection");
			}
			array[5] = array[5].Substring(0, num2 + 1);
			IPAddress ipaddress;
			try
			{
				ipaddress = IPAddress.Parse(string.Join(".", array, 0, 4));
			}
			catch (FormatException)
			{
				throw new WebException("Cannot open passive data connection");
			}
			int num3;
			int num4;
			if (!int.TryParse(array[4], out num3) || !int.TryParse(array[5], out num4))
			{
				throw new WebException("Cannot open passive data connection");
			}
			int num5 = (num3 << 8) + num4;
			if (num5 < 0 || num5 > 65535)
			{
				throw new WebException("Cannot open passive data connection");
			}
			IPEndPoint ipendPoint = new IPEndPoint(ipaddress, num5);
			global::System.Net.Sockets.Socket socket = new global::System.Net.Sockets.Socket(ipendPoint.AddressFamily, global::System.Net.Sockets.SocketType.Stream, global::System.Net.Sockets.ProtocolType.Tcp);
			try
			{
				socket.Connect(ipendPoint);
			}
			catch (global::System.Net.Sockets.SocketException)
			{
				socket.Close();
				throw new WebException("Cannot open passive data connection");
			}
			return socket;
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x0004CEC0 File Offset: 0x0004B0C0
		private Exception CreateExceptionFromResponse(FtpStatus status)
		{
			FtpWebResponse ftpWebResponse = new FtpWebResponse(this, this.requestUri, this.method, status);
			return new WebException("Server returned an error: " + status.StatusDescription, null, WebExceptionStatus.ProtocolError, ftpWebResponse);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x0004CEFC File Offset: 0x0004B0FC
		internal void SetTransferCompleted()
		{
			if (this.InFinalState())
			{
				return;
			}
			this.State = FtpWebRequest.RequestState.Finished;
			FtpStatus responseStatus = this.GetResponseStatus();
			this.ftpResponse.UpdateStatus(responseStatus);
			if (!this.keepAlive)
			{
				this.CloseConnection();
			}
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x0004CF40 File Offset: 0x0004B140
		internal void OperationCompleted()
		{
			if (!this.keepAlive)
			{
				this.CloseConnection();
			}
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0004CF54 File Offset: 0x0004B154
		private void SetCompleteWithError(Exception exc)
		{
			if (this.asyncResult != null)
			{
				this.asyncResult.SetCompleted(false, exc);
			}
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0004CF70 File Offset: 0x0004B170
		private global::System.Net.Sockets.Socket InitDataConnection()
		{
			if (this.usePassive)
			{
				FtpStatus ftpStatus = this.SendCommand("PASV", new string[0]);
				if (ftpStatus.StatusCode != FtpStatusCode.EnteringPassive)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				return this.SetupPassiveConnection(ftpStatus.StatusDescription);
			}
			else
			{
				global::System.Net.Sockets.Socket socket = new global::System.Net.Sockets.Socket(global::System.Net.Sockets.AddressFamily.InterNetwork, global::System.Net.Sockets.SocketType.Stream, global::System.Net.Sockets.ProtocolType.Tcp);
				try
				{
					socket.Bind(new IPEndPoint(this.localEndPoint.Address, 0));
					socket.Listen(1);
				}
				catch (global::System.Net.Sockets.SocketException ex)
				{
					socket.Close();
					throw new WebException("Couldn't open listening socket on client", ex);
				}
				IPEndPoint ipendPoint = (IPEndPoint)socket.LocalEndPoint;
				string text = ipendPoint.Address.ToString().Replace('.', ',');
				int num = ipendPoint.Port >> 8;
				int num2 = ipendPoint.Port % 256;
				string text2 = string.Concat(new object[] { text, ",", num, ",", num2 });
				FtpStatus ftpStatus = this.SendCommand("PORT", new string[] { text2 });
				if (ftpStatus.StatusCode != FtpStatusCode.CommandOK)
				{
					socket.Close();
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				return socket;
			}
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0004D0C8 File Offset: 0x0004B2C8
		private void OpenDataConnection()
		{
			global::System.Net.Sockets.Socket socket = this.InitDataConnection();
			FtpStatus ftpStatus;
			if (this.offset > 0L)
			{
				ftpStatus = this.SendCommand("REST", new string[] { this.offset.ToString() });
				if (ftpStatus.StatusCode != FtpStatusCode.FileCommandPending)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
			}
			if (this.method != "NLST" && this.method != "LIST" && this.method != "STOU")
			{
				ftpStatus = this.SendCommand(this.method, new string[] { this.file_name });
			}
			else
			{
				ftpStatus = this.SendCommand(this.method, new string[0]);
			}
			if (ftpStatus.StatusCode != FtpStatusCode.OpeningData && ftpStatus.StatusCode != FtpStatusCode.DataAlreadyOpen)
			{
				throw this.CreateExceptionFromResponse(ftpStatus);
			}
			if (this.usePassive)
			{
				this.origDataStream = new global::System.Net.Sockets.NetworkStream(socket, true);
				this.dataStream = this.origDataStream;
				if (this.EnableSsl)
				{
					this.ChangeToSSLSocket(ref this.dataStream);
				}
			}
			else
			{
				global::System.Net.Sockets.Socket socket2 = null;
				try
				{
					socket2 = socket.Accept();
				}
				catch (global::System.Net.Sockets.SocketException)
				{
					socket.Close();
					if (socket2 != null)
					{
						socket2.Close();
					}
					throw new ProtocolViolationException("Server commited a protocol violation.");
				}
				socket.Close();
				this.origDataStream = new global::System.Net.Sockets.NetworkStream(socket, true);
				this.dataStream = this.origDataStream;
				if (this.EnableSsl)
				{
					this.ChangeToSSLSocket(ref this.dataStream);
				}
			}
			this.ftpResponse.UpdateStatus(ftpStatus);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0004D28C File Offset: 0x0004B48C
		private void Authenticate()
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			if (this.credentials != null)
			{
				text = this.credentials.UserName;
				text2 = this.credentials.Password;
				text3 = this.credentials.Domain;
			}
			if (text == null)
			{
				text = "anonymous";
			}
			if (text2 == null)
			{
				text2 = "@anonymous";
			}
			if (!string.IsNullOrEmpty(text3))
			{
				text = text3 + '\\' + text;
			}
			FtpStatus ftpStatus = this.GetResponseStatus();
			this.ftpResponse.BannerMessage = ftpStatus.StatusDescription;
			if (this.EnableSsl)
			{
				this.InitiateSecureConnection(ref this.controlStream);
				this.controlReader = new StreamReader(this.controlStream, Encoding.ASCII);
				ftpStatus = this.SendCommand("PBSZ", new string[] { "0" });
				int num = (int)ftpStatus.StatusCode;
				if (num < 200 || num >= 300)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				ftpStatus = this.SendCommand("PROT", new string[] { "P" });
				num = (int)ftpStatus.StatusCode;
				if (num < 200 || num >= 300)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				ftpStatus = new FtpStatus(FtpStatusCode.SendUserCommand, string.Empty);
			}
			if (ftpStatus.StatusCode != FtpStatusCode.SendUserCommand)
			{
				throw this.CreateExceptionFromResponse(ftpStatus);
			}
			ftpStatus = this.SendCommand("USER", new string[] { text });
			FtpStatusCode statusCode = ftpStatus.StatusCode;
			if (statusCode != FtpStatusCode.LoggedInProceed)
			{
				if (statusCode != FtpStatusCode.SendPasswordCommand)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
				ftpStatus = this.SendCommand("PASS", new string[] { text2 });
				if (ftpStatus.StatusCode != FtpStatusCode.LoggedInProceed)
				{
					throw this.CreateExceptionFromResponse(ftpStatus);
				}
			}
			this.ftpResponse.WelcomeMessage = ftpStatus.StatusDescription;
			this.ftpResponse.UpdateStatus(ftpStatus);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0004D48C File Offset: 0x0004B68C
		private FtpStatus SendCommand(string command, params string[] parameters)
		{
			return this.SendCommand(true, command, parameters);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0004D498 File Offset: 0x0004B698
		private FtpStatus SendCommand(bool waitResponse, string command, params string[] parameters)
		{
			string text = command;
			if (parameters.Length > 0)
			{
				text = text + " " + string.Join(" ", parameters);
			}
			text += "\r\n";
			byte[] bytes = Encoding.ASCII.GetBytes(text);
			try
			{
				this.controlStream.Write(bytes, 0, bytes.Length);
			}
			catch (IOException)
			{
				return new FtpStatus(FtpStatusCode.ServiceNotAvailable, "Write failed");
			}
			if (!waitResponse)
			{
				return null;
			}
			FtpStatus responseStatus = this.GetResponseStatus();
			if (this.ftpResponse != null)
			{
				this.ftpResponse.UpdateStatus(responseStatus);
			}
			return responseStatus;
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0004D554 File Offset: 0x0004B754
		internal static FtpStatus ServiceNotAvailable()
		{
			return new FtpStatus(FtpStatusCode.ServiceNotAvailable, global::Locale.GetText("Invalid response from server"));
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0004D56C File Offset: 0x0004B76C
		internal FtpStatus GetResponseStatus()
		{
			string text = null;
			try
			{
				text = this.controlReader.ReadLine();
			}
			catch (IOException)
			{
			}
			if (text == null || text.Length < 3)
			{
				return FtpWebRequest.ServiceNotAvailable();
			}
			int num;
			if (!int.TryParse(text.Substring(0, 3), out num))
			{
				return FtpWebRequest.ServiceNotAvailable();
			}
			if (text.Length > 3 && text[3] == '-')
			{
				string text2 = null;
				string text3 = num.ToString() + ' ';
				for (;;)
				{
					text2 = null;
					try
					{
						text2 = this.controlReader.ReadLine();
					}
					catch (IOException)
					{
					}
					if (text2 == null)
					{
						break;
					}
					text = text + Environment.NewLine + text2;
					if (text2.StartsWith(text3, StringComparison.Ordinal))
					{
						goto Block_8;
					}
				}
				return FtpWebRequest.ServiceNotAvailable();
				Block_8:;
			}
			return new FtpStatus((FtpStatusCode)num, text);
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x0004D680 File Offset: 0x0004B880
		private void InitiateSecureConnection(ref Stream stream)
		{
			FtpStatus ftpStatus = this.SendCommand("AUTH", new string[] { "TLS" });
			if (ftpStatus.StatusCode != FtpStatusCode.ServerWantsSecureSession)
			{
				throw this.CreateExceptionFromResponse(ftpStatus);
			}
			this.ChangeToSSLSocket(ref stream);
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0004D6C8 File Offset: 0x0004B8C8
		internal bool ChangeToSSLSocket(ref Stream stream)
		{
			global::System.Net.Security.SslStream sslStream = new global::System.Net.Security.SslStream(stream, true, this.callback, null);
			sslStream.AuthenticateAsClient(this.requestUri.Host, null, global::System.Security.Authentication.SslProtocols.Default, false);
			stream = sslStream;
			return true;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0004D704 File Offset: 0x0004B904
		private bool InFinalState()
		{
			return this.State == FtpWebRequest.RequestState.Aborted || this.State == FtpWebRequest.RequestState.Error || this.State == FtpWebRequest.RequestState.Finished;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x0004D738 File Offset: 0x0004B938
		private bool InProgress()
		{
			return this.State != FtpWebRequest.RequestState.Before && !this.InFinalState();
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0004D754 File Offset: 0x0004B954
		internal void CheckIfAborted()
		{
			if (this.State == FtpWebRequest.RequestState.Aborted)
			{
				throw new WebException("Request aborted", WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x0004D770 File Offset: 0x0004B970
		private void CheckFinalState()
		{
			if (this.InFinalState())
			{
				throw new InvalidOperationException("Cannot change final state");
			}
		}

		// Token: 0x040010A1 RID: 4257
		private const string ChangeDir = "CWD";

		// Token: 0x040010A2 RID: 4258
		private const string UserCommand = "USER";

		// Token: 0x040010A3 RID: 4259
		private const string PasswordCommand = "PASS";

		// Token: 0x040010A4 RID: 4260
		private const string TypeCommand = "TYPE";

		// Token: 0x040010A5 RID: 4261
		private const string PassiveCommand = "PASV";

		// Token: 0x040010A6 RID: 4262
		private const string PortCommand = "PORT";

		// Token: 0x040010A7 RID: 4263
		private const string AbortCommand = "ABOR";

		// Token: 0x040010A8 RID: 4264
		private const string AuthCommand = "AUTH";

		// Token: 0x040010A9 RID: 4265
		private const string RestCommand = "REST";

		// Token: 0x040010AA RID: 4266
		private const string RenameFromCommand = "RNFR";

		// Token: 0x040010AB RID: 4267
		private const string RenameToCommand = "RNTO";

		// Token: 0x040010AC RID: 4268
		private const string QuitCommand = "QUIT";

		// Token: 0x040010AD RID: 4269
		private const string EOL = "\r\n";

		// Token: 0x040010AE RID: 4270
		private global::System.Uri requestUri;

		// Token: 0x040010AF RID: 4271
		private string file_name;

		// Token: 0x040010B0 RID: 4272
		private ServicePoint servicePoint;

		// Token: 0x040010B1 RID: 4273
		private Stream origDataStream;

		// Token: 0x040010B2 RID: 4274
		private Stream dataStream;

		// Token: 0x040010B3 RID: 4275
		private Stream controlStream;

		// Token: 0x040010B4 RID: 4276
		private StreamReader controlReader;

		// Token: 0x040010B5 RID: 4277
		private NetworkCredential credentials;

		// Token: 0x040010B6 RID: 4278
		private IPHostEntry hostEntry;

		// Token: 0x040010B7 RID: 4279
		private IPEndPoint localEndPoint;

		// Token: 0x040010B8 RID: 4280
		private IWebProxy proxy;

		// Token: 0x040010B9 RID: 4281
		private int timeout = 100000;

		// Token: 0x040010BA RID: 4282
		private int rwTimeout = 300000;

		// Token: 0x040010BB RID: 4283
		private long offset;

		// Token: 0x040010BC RID: 4284
		private bool binary = true;

		// Token: 0x040010BD RID: 4285
		private bool enableSsl;

		// Token: 0x040010BE RID: 4286
		private bool usePassive = true;

		// Token: 0x040010BF RID: 4287
		private bool keepAlive;

		// Token: 0x040010C0 RID: 4288
		private string method = "RETR";

		// Token: 0x040010C1 RID: 4289
		private string renameTo;

		// Token: 0x040010C2 RID: 4290
		private object locker = new object();

		// Token: 0x040010C3 RID: 4291
		private FtpWebRequest.RequestState requestState;

		// Token: 0x040010C4 RID: 4292
		private FtpAsyncResult asyncResult;

		// Token: 0x040010C5 RID: 4293
		private FtpWebResponse ftpResponse;

		// Token: 0x040010C6 RID: 4294
		private Stream requestStream;

		// Token: 0x040010C7 RID: 4295
		private string initial_path;

		// Token: 0x040010C8 RID: 4296
		private static readonly string[] supportedCommands = new string[]
		{
			"APPE", "DELE", "LIST", "MDTM", "MKD", "NLST", "PWD", "RENAME", "RETR", "RMD",
			"SIZE", "STOR", "STOU"
		};

		// Token: 0x040010C9 RID: 4297
		private global::System.Net.Security.RemoteCertificateValidationCallback callback = delegate(object sender, X509Certificate certificate, global::System.Security.Cryptography.X509Certificates.X509Chain chain, global::System.Net.Security.SslPolicyErrors sslPolicyErrors)
		{
			if (ServicePointManager.ServerCertificateValidationCallback != null)
			{
				return ServicePointManager.ServerCertificateValidationCallback(sender, certificate, chain, sslPolicyErrors);
			}
			if (sslPolicyErrors != global::System.Net.Security.SslPolicyErrors.None)
			{
				throw new InvalidOperationException("SSL authentication error: " + sslPolicyErrors);
			}
			return true;
		};

		// Token: 0x0200030C RID: 780
		private enum RequestState
		{
			// Token: 0x040010CE RID: 4302
			Before,
			// Token: 0x040010CF RID: 4303
			Scheduled,
			// Token: 0x040010D0 RID: 4304
			Connecting,
			// Token: 0x040010D1 RID: 4305
			Authenticating,
			// Token: 0x040010D2 RID: 4306
			OpeningData,
			// Token: 0x040010D3 RID: 4307
			TransferInProgress,
			// Token: 0x040010D4 RID: 4308
			Finished,
			// Token: 0x040010D5 RID: 4309
			Aborted,
			// Token: 0x040010D6 RID: 4310
			Error
		}
	}
}
