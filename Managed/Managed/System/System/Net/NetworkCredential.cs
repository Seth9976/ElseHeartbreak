using System;

namespace System.Net
{
	/// <summary>Provides credentials for password-based authentication schemes such as basic, digest, NTLM, and Kerberos authentication.</summary>
	// Token: 0x02000359 RID: 857
	public class NetworkCredential : ICredentials, ICredentialsByHost
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class.</summary>
		// Token: 0x06001E3D RID: 7741 RVA: 0x0005CABC File Offset: 0x0005ACBC
		public NetworkCredential()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name and password.</summary>
		/// <param name="userName">The user name associated with the credentials. </param>
		/// <param name="password">The password for the user name associated with the credentials. </param>
		// Token: 0x06001E3E RID: 7742 RVA: 0x0005CAC4 File Offset: 0x0005ACC4
		public NetworkCredential(string userName, string password)
		{
			this.userName = userName;
			this.password = password;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkCredential" /> class with the specified user name, password, and domain.</summary>
		/// <param name="userName">The user name associated with the credentials. </param>
		/// <param name="password">The password for the user name associated with the credentials. </param>
		/// <param name="domain">The domain associated with these credentials. </param>
		// Token: 0x06001E3F RID: 7743 RVA: 0x0005CADC File Offset: 0x0005ACDC
		public NetworkCredential(string userName, string password, string domain)
		{
			this.userName = userName;
			this.password = password;
			this.domain = domain;
		}

		/// <summary>Gets or sets the domain or computer name that verifies the credentials.</summary>
		/// <returns>The name of the domain associated with the credentials.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x0005CAFC File Offset: 0x0005ACFC
		// (set) Token: 0x06001E41 RID: 7745 RVA: 0x0005CB1C File Offset: 0x0005AD1C
		public string Domain
		{
			get
			{
				return (this.domain != null) ? this.domain : string.Empty;
			}
			set
			{
				this.domain = value;
			}
		}

		/// <summary>Gets or sets the user name associated with the credentials.</summary>
		/// <returns>The user name associated with the credentials.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0005CB28 File Offset: 0x0005AD28
		// (set) Token: 0x06001E43 RID: 7747 RVA: 0x0005CB48 File Offset: 0x0005AD48
		public string UserName
		{
			get
			{
				return (this.userName != null) ? this.userName : string.Empty;
			}
			set
			{
				this.userName = value;
			}
		}

		/// <summary>Gets or sets the password for the user name associated with the credentials.</summary>
		/// <returns>The password associated with the credentials. If this <see cref="T:System.Net.NetworkCredential" /> instance was constructed with a null password, then the <see cref="P:System.Net.NetworkCredential.Password" /> property will return an empty string.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0005CB54 File Offset: 0x0005AD54
		// (set) Token: 0x06001E45 RID: 7749 RVA: 0x0005CB74 File Offset: 0x0005AD74
		public string Password
		{
			get
			{
				return (this.password != null) ? this.password : string.Empty;
			}
			set
			{
				this.password = value;
			}
		}

		/// <summary>Returns an instance of the <see cref="T:System.Net.NetworkCredential" /> class for the specified Uniform Resource Identifier (URI) and authentication type.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> object.</returns>
		/// <param name="uri">The URI that the client provides authentication for. </param>
		/// <param name="authType">The type of authentication requested, as defined in the <see cref="P:System.Net.IAuthenticationModule.AuthenticationType" /> property. </param>
		// Token: 0x06001E46 RID: 7750 RVA: 0x0005CB80 File Offset: 0x0005AD80
		public NetworkCredential GetCredential(global::System.Uri uri, string authType)
		{
			return this;
		}

		/// <summary>Returns an instance of the <see cref="T:System.Net.NetworkCredential" /> class for the specified host, port, and authentication type.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> for the specified host, port, and authentication protocol, or null if there are no credentials available for the specified host, port, and authentication protocol.</returns>
		/// <param name="host">The host computer that authenticates the client.</param>
		/// <param name="port">The port on the <paramref name="host" /> that the client communicates with.</param>
		/// <param name="authenticationType">The type of authentication requested, as defined in the <see cref="P:System.Net.IAuthenticationModule.AuthenticationType" /> property. </param>
		// Token: 0x06001E47 RID: 7751 RVA: 0x0005CB84 File Offset: 0x0005AD84
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			return this;
		}

		// Token: 0x040012D4 RID: 4820
		private string userName;

		// Token: 0x040012D5 RID: 4821
		private string password;

		// Token: 0x040012D6 RID: 4822
		private string domain;
	}
}
