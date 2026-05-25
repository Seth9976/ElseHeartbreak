using System;
using System.Collections;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace System.Net
{
	/// <summary>Contains HTTP proxy settings for the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x0200041E RID: 1054
	[Serializable]
	public class WebProxy : ISerializable, IWebProxy
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.WebProxy" /> class.</summary>
		// Token: 0x06002607 RID: 9735 RVA: 0x00076830 File Offset: 0x00074A30
		public WebProxy()
			: this(null, false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI.</summary>
		/// <param name="Address">The URI of the proxy server. </param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI. </exception>
		// Token: 0x06002608 RID: 9736 RVA: 0x0007683C File Offset: 0x00074A3C
		public WebProxy(string address)
			: this(WebProxy.ToUri(address), false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class from the specified <see cref="T:System.Uri" /> instance.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server. </param>
		// Token: 0x06002609 RID: 9737 RVA: 0x00076850 File Offset: 0x00074A50
		public WebProxy(global::System.Uri address)
			: this(address, false, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI and bypass setting.</summary>
		/// <param name="Address">The URI of the proxy server. </param>
		/// <param name="BypassOnLocal">true to bypass the proxy for local addresses; otherwise, false. </param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI. </exception>
		// Token: 0x0600260A RID: 9738 RVA: 0x0007685C File Offset: 0x00074A5C
		public WebProxy(string address, bool bypassOnLocal)
			: this(WebProxy.ToUri(address), bypassOnLocal, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified host and port number.</summary>
		/// <param name="Host">The name of the proxy host. </param>
		/// <param name="Port">The port number on <paramref name="Host" /> to use. </param>
		/// <exception cref="T:System.UriFormatException">The URI formed by combining <paramref name="Host" /> and <paramref name="Port" /> is not a valid URI. </exception>
		// Token: 0x0600260B RID: 9739 RVA: 0x00076870 File Offset: 0x00074A70
		public WebProxy(string host, int port)
			: this(new global::System.Uri(string.Concat(new object[] { "http://", host, ":", port })))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the <see cref="T:System.Uri" /> instance and bypass setting.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server. </param>
		/// <param name="BypassOnLocal">true to bypass the proxy for local addresses; otherwise, false. </param>
		// Token: 0x0600260C RID: 9740 RVA: 0x000768A8 File Offset: 0x00074AA8
		public WebProxy(global::System.Uri address, bool bypassOnLocal)
			: this(address, bypassOnLocal, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI, bypass setting, and list of URIs to bypass.</summary>
		/// <param name="Address">The URI of the proxy server. </param>
		/// <param name="BypassOnLocal">true to bypass the proxy for local addresses; otherwise, false. </param>
		/// <param name="BypassList">An array of regular expression strings that contain the URIs of the servers to bypass. </param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI. </exception>
		// Token: 0x0600260D RID: 9741 RVA: 0x000768B4 File Offset: 0x00074AB4
		public WebProxy(string address, bool bypassOnLocal, string[] bypassList)
			: this(WebProxy.ToUri(address), bypassOnLocal, bypassList, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified <see cref="T:System.Uri" /> instance, bypass setting, and list of URIs to bypass.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server. </param>
		/// <param name="BypassOnLocal">true to bypass the proxy for local addresses; otherwise, false. </param>
		/// <param name="BypassList">An array of regular expression strings that contains the URIs of the servers to bypass. </param>
		// Token: 0x0600260E RID: 9742 RVA: 0x000768C8 File Offset: 0x00074AC8
		public WebProxy(global::System.Uri address, bool bypassOnLocal, string[] bypassList)
			: this(address, bypassOnLocal, bypassList, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified URI, bypass setting, list of URIs to bypass, and credentials.</summary>
		/// <param name="Address">The URI of the proxy server. </param>
		/// <param name="BypassOnLocal">true to bypass the proxy for local addresses; otherwise, false. </param>
		/// <param name="BypassList">An array of regular expression strings that contains the URIs of the servers to bypass. </param>
		/// <param name="Credentials">An <see cref="T:System.Net.ICredentials" /> instance to submit to the proxy server for authentication. </param>
		/// <exception cref="T:System.UriFormatException">
		///   <paramref name="Address" /> is an invalid URI. </exception>
		// Token: 0x0600260F RID: 9743 RVA: 0x000768D4 File Offset: 0x00074AD4
		public WebProxy(string address, bool bypassOnLocal, string[] bypassList, ICredentials credentials)
			: this(WebProxy.ToUri(address), bypassOnLocal, bypassList, credentials)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebProxy" /> class with the specified <see cref="T:System.Uri" /> instance, bypass setting, list of URIs to bypass, and credentials.</summary>
		/// <param name="Address">A <see cref="T:System.Uri" /> instance that contains the address of the proxy server. </param>
		/// <param name="BypassOnLocal">true to bypass the proxy for local addresses; otherwise, false. </param>
		/// <param name="BypassList">An array of regular expression strings that contains the URIs of the servers to bypass. </param>
		/// <param name="Credentials">An <see cref="T:System.Net.ICredentials" /> instance to submit to the proxy server for authentication. </param>
		// Token: 0x06002610 RID: 9744 RVA: 0x000768E8 File Offset: 0x00074AE8
		public WebProxy(global::System.Uri address, bool bypassOnLocal, string[] bypassList, ICredentials credentials)
		{
			this.address = address;
			this.bypassOnLocal = bypassOnLocal;
			if (bypassList != null)
			{
				this.bypassList = new ArrayList(bypassList);
			}
			this.credentials = credentials;
			this.CheckBypassList();
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Net.WebProxy" /> class using previously serialized content.</summary>
		/// <param name="serializationInfo">The serialization data. </param>
		/// <param name="streamingContext">The context for the serialized data. </param>
		// Token: 0x06002611 RID: 9745 RVA: 0x0007692C File Offset: 0x00074B2C
		protected WebProxy(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.address = (global::System.Uri)serializationInfo.GetValue("_ProxyAddress", typeof(global::System.Uri));
			this.bypassOnLocal = serializationInfo.GetBoolean("_BypassOnLocal");
			this.bypassList = (ArrayList)serializationInfo.GetValue("_BypassList", typeof(ArrayList));
			this.useDefaultCredentials = serializationInfo.GetBoolean("_UseDefaultCredentials");
			this.credentials = null;
			this.CheckBypassList();
		}

		/// <summary>Creates the serialization data and context that are used by the system to serialize a <see cref="T:System.Net.WebProxy" /> object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that indicates the destination for this serialization. </param>
		// Token: 0x06002612 RID: 9746 RVA: 0x000769B0 File Offset: 0x00074BB0
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Gets or sets the address of the proxy server.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that contains the address of the proxy server.</returns>
		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x000769BC File Offset: 0x00074BBC
		// (set) Token: 0x06002614 RID: 9748 RVA: 0x000769C4 File Offset: 0x00074BC4
		public global::System.Uri Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}

		/// <summary>Gets a list of addresses that do not use the proxy server.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> that contains a list of <see cref="P:System.Net.WebProxy.BypassList" /> arrays that represents URIs that do not use the proxy server when accessed.</returns>
		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x000769D0 File Offset: 0x00074BD0
		public ArrayList BypassArrayList
		{
			get
			{
				if (this.bypassList == null)
				{
					this.bypassList = new ArrayList();
				}
				return this.bypassList;
			}
		}

		/// <summary>Gets or sets an array of addresses that do not use the proxy server.</summary>
		/// <returns>An array that contains a list of regular expressions that describe URIs that do not use the proxy server when accessed.</returns>
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06002616 RID: 9750 RVA: 0x000769F0 File Offset: 0x00074BF0
		// (set) Token: 0x06002617 RID: 9751 RVA: 0x00076A0C File Offset: 0x00074C0C
		public string[] BypassList
		{
			get
			{
				return (string[])this.BypassArrayList.ToArray(typeof(string));
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.bypassList = new ArrayList(value);
				this.CheckBypassList();
			}
		}

		/// <summary>Gets or sets a value that indicates whether to bypass the proxy server for local addresses.</summary>
		/// <returns>true to bypass the proxy server for local addresses; otherwise, false. The default value is false.</returns>
		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06002618 RID: 9752 RVA: 0x00076A2C File Offset: 0x00074C2C
		// (set) Token: 0x06002619 RID: 9753 RVA: 0x00076A34 File Offset: 0x00074C34
		public bool BypassProxyOnLocal
		{
			get
			{
				return this.bypassOnLocal;
			}
			set
			{
				this.bypassOnLocal = value;
			}
		}

		/// <summary>Gets or sets the credentials to submit to the proxy server for authentication.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance that contains the credentials to submit to the proxy server for authentication.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property when the <see cref="P:System.Net.WebProxy.UseDefaultCredentials" />  property was set to true. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x00076A40 File Offset: 0x00074C40
		// (set) Token: 0x0600261B RID: 9755 RVA: 0x00076A48 File Offset: 0x00074C48
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

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>true if the default credentials are used; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">You attempted to set this property when the <see cref="P:System.Net.WebProxy.Credentials" /> property contains credentials other than the default credentials. For more information, see the Remarks section.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="USERNAME" />
		/// </PermissionSet>
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x00076A54 File Offset: 0x00074C54
		// (set) Token: 0x0600261D RID: 9757 RVA: 0x00076A5C File Offset: 0x00074C5C
		[global::System.MonoTODO("Does not affect Credentials, since CredentialCache.DefaultCredentials is not implemented.")]
		public bool UseDefaultCredentials
		{
			get
			{
				return this.useDefaultCredentials;
			}
			set
			{
				this.useDefaultCredentials = value;
			}
		}

		/// <summary>Reads the Internet Explorer nondynamic proxy settings.</summary>
		/// <returns>A <see cref="T:System.Net.WebProxy" /> instance that contains the nondynamic proxy settings from Internet Explorer 5.5 and later.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600261E RID: 9758 RVA: 0x00076A68 File Offset: 0x00074C68
		[Obsolete("This method has been deprecated", false)]
		[global::System.MonoTODO("Can we get this info under windows from the system?")]
		public static WebProxy GetDefaultProxy()
		{
			IWebProxy select = GlobalProxySelection.Select;
			if (select is WebProxy)
			{
				return (WebProxy)select;
			}
			return new WebProxy();
		}

		/// <summary>Returns the proxied URI for a request.</summary>
		/// <returns>The <see cref="T:System.Uri" /> instance of the Internet resource, if the resource is on the bypass list; otherwise, the <see cref="T:System.Uri" /> instance of the proxy.</returns>
		/// <param name="destination">The <see cref="T:System.Uri" /> instance of the requested Internet resource. </param>
		// Token: 0x0600261F RID: 9759 RVA: 0x00076A94 File Offset: 0x00074C94
		public global::System.Uri GetProxy(global::System.Uri destination)
		{
			if (this.IsBypassed(destination))
			{
				return destination;
			}
			return this.address;
		}

		/// <summary>Indicates whether to use the proxy server for the specified host.</summary>
		/// <returns>true if the proxy server should not be used for <paramref name="host" />; otherwise, false.</returns>
		/// <param name="host">The <see cref="T:System.Uri" /> instance of the host to check for proxy use. </param>
		// Token: 0x06002620 RID: 9760 RVA: 0x00076AAC File Offset: 0x00074CAC
		public bool IsBypassed(global::System.Uri host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (host.IsLoopback && this.bypassOnLocal)
			{
				return true;
			}
			if (this.address == null)
			{
				return true;
			}
			string host2 = host.Host;
			if (this.bypassOnLocal && host2.IndexOf('.') == -1)
			{
				return true;
			}
			if (!this.bypassOnLocal)
			{
				if (string.Compare(host2, "localhost", true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
				if (string.Compare(host2, "loopback", true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
				IPAddress ipaddress = null;
				if (IPAddress.TryParse(host2, out ipaddress) && IPAddress.IsLoopback(ipaddress))
				{
					return true;
				}
			}
			if (this.bypassList == null || this.bypassList.Count == 0)
			{
				return false;
			}
			bool flag;
			try
			{
				string text = host.Scheme + "://" + host.Authority;
				int i;
				for (i = 0; i < this.bypassList.Count; i++)
				{
					global::System.Text.RegularExpressions.Regex regex = new global::System.Text.RegularExpressions.Regex((string)this.bypassList[i], global::System.Text.RegularExpressions.RegexOptions.IgnoreCase | global::System.Text.RegularExpressions.RegexOptions.Singleline);
					if (regex.IsMatch(text))
					{
						break;
					}
				}
				if (i == this.bypassList.Count)
				{
					flag = false;
				}
				else
				{
					while (i < this.bypassList.Count)
					{
						new global::System.Text.RegularExpressions.Regex((string)this.bypassList[i]);
						i++;
					}
					flag = true;
				}
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data that is needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06002621 RID: 9761 RVA: 0x00076C74 File Offset: 0x00074E74
		protected virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			serializationInfo.AddValue("_BypassOnLocal", this.bypassOnLocal);
			serializationInfo.AddValue("_ProxyAddress", this.address);
			serializationInfo.AddValue("_BypassList", this.bypassList);
			serializationInfo.AddValue("_UseDefaultCredentials", this.UseDefaultCredentials);
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x00076CC8 File Offset: 0x00074EC8
		private void CheckBypassList()
		{
			if (this.bypassList == null)
			{
				return;
			}
			for (int i = 0; i < this.bypassList.Count; i++)
			{
				new global::System.Text.RegularExpressions.Regex((string)this.bypassList[i]);
			}
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x00076D14 File Offset: 0x00074F14
		private static global::System.Uri ToUri(string address)
		{
			if (address == null)
			{
				return null;
			}
			if (address.IndexOf("://") == -1)
			{
				address = "http://" + address;
			}
			return new global::System.Uri(address);
		}

		// Token: 0x04001776 RID: 6006
		private global::System.Uri address;

		// Token: 0x04001777 RID: 6007
		private bool bypassOnLocal;

		// Token: 0x04001778 RID: 6008
		private ArrayList bypassList;

		// Token: 0x04001779 RID: 6009
		private ICredentials credentials;

		// Token: 0x0400177A RID: 6010
		private bool useDefaultCredentials;
	}
}
