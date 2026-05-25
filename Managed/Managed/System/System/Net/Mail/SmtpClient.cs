using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net.Configuration;
using System.Net.Mime;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace System.Net.Mail
{
	/// <summary>Allows applications to send e-mail by using the Simple Mail Transfer Protocol (SMTP).</summary>
	// Token: 0x02000341 RID: 833
	public class SmtpClient
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class by using configuration file settings. </summary>
		// Token: 0x06001D91 RID: 7569 RVA: 0x000596A0 File Offset: 0x000578A0
		public SmtpClient()
			: this(null, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class that sends e-mail by using the specified SMTP server. </summary>
		/// <param name="host">A <see cref="T:System.String" /> that contains the name or IP address of the host computer used for SMTP transactions.</param>
		// Token: 0x06001D92 RID: 7570 RVA: 0x000596AC File Offset: 0x000578AC
		public SmtpClient(string host)
			: this(host, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.SmtpClient" /> class that sends e-mail by using the specified SMTP server and port.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that contains the name or IP address of the host used for SMTP transactions.</param>
		/// <param name="port">An <see cref="T:System.Int32" /> greater than zero that contains the port to be used on <paramref name="host" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> cannot be less than zero.</exception>
		// Token: 0x06001D93 RID: 7571 RVA: 0x000596B8 File Offset: 0x000578B8
		public SmtpClient(string host, int port)
		{
			global::System.Net.Configuration.SmtpSection smtpSection = (global::System.Net.Configuration.SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
			if (smtpSection != null)
			{
				this.host = smtpSection.Network.Host;
				this.port = smtpSection.Network.Port;
				if (smtpSection.Network.UserName != null)
				{
					string text = string.Empty;
					if (smtpSection.Network.Password != null)
					{
						text = smtpSection.Network.Password;
					}
					this.Credentials = new CCredentialsByHost(smtpSection.Network.UserName, text);
				}
				if (smtpSection.From != null)
				{
					this.defaultFrom = new MailAddress(smtpSection.From);
				}
			}
			if (!string.IsNullOrEmpty(host))
			{
				this.host = host;
			}
			if (port != 0)
			{
				this.port = port;
			}
		}

		/// <summary>Occurs when an asynchronous e-mail send operation completes.</summary>
		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06001D94 RID: 7572 RVA: 0x000597C0 File Offset: 0x000579C0
		// (remove) Token: 0x06001D95 RID: 7573 RVA: 0x000597DC File Offset: 0x000579DC
		public event SendCompletedEventHandler SendCompleted;

		/// <summary>Specify which certificates should be used to establish the Secure Sockets Layer (SSL) connection.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />, holding one or more client certificates. The default value is derived from the mail configuration attributes in a configuration file.</returns>
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x000597F8 File Offset: 0x000579F8
		[global::System.MonoTODO("Client certificates not used")]
		public global::System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.clientCertificates == null)
				{
					this.clientCertificates = new global::System.Security.Cryptography.X509Certificates.X509CertificateCollection();
				}
				return this.clientCertificates;
			}
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) to use for authentication when using extended protection.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the SPN to use for extended protection. The default value for this SPN is of the form "SMTPSVC/&lt;host&gt;" where &lt;host&gt; is the hostname of the SMTP mail server. </returns>
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x00059818 File Offset: 0x00057A18
		// (set) Token: 0x06001D98 RID: 7576 RVA: 0x00059820 File Offset: 0x00057A20
		private string TargetName { get; set; }

		/// <summary>Gets or sets the credentials used to authenticate the sender.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentialsByHost" /> that represents the credentials to use for authentication; or null if no credentials have been specified.</returns>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x0005982C File Offset: 0x00057A2C
		// (set) Token: 0x06001D9A RID: 7578 RVA: 0x00059834 File Offset: 0x00057A34
		public ICredentialsByHost Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.CheckState();
				this.credentials = value;
			}
		}

		/// <summary>Specifies how outgoing email messages will be handled.</summary>
		/// <returns>An <see cref="T:System.Net.Mail.SmtpDeliveryMethod" /> that indicates how email messages are delivered.</returns>
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x00059844 File Offset: 0x00057A44
		// (set) Token: 0x06001D9C RID: 7580 RVA: 0x0005984C File Offset: 0x00057A4C
		public SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return this.deliveryMethod;
			}
			set
			{
				this.CheckState();
				this.deliveryMethod = value;
			}
		}

		/// <summary>Specify whether the <see cref="T:System.Net.Mail.SmtpClient" /> uses Secure Sockets Layer (SSL) to encrypt the connection.</summary>
		/// <returns>true if the <see cref="T:System.Net.Mail.SmtpClient" /> uses SSL; otherwise, false. The default is false.</returns>
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001D9D RID: 7581 RVA: 0x0005985C File Offset: 0x00057A5C
		// (set) Token: 0x06001D9E RID: 7582 RVA: 0x00059864 File Offset: 0x00057A64
		public bool EnableSsl
		{
			get
			{
				return this.enableSsl;
			}
			set
			{
				this.CheckState();
				this.enableSsl = value;
			}
		}

		/// <summary>Gets or sets the name or IP address of the host used for SMTP transactions.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name or IP address of the computer to use for SMTP transactions.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is equal to <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001D9F RID: 7583 RVA: 0x00059874 File Offset: 0x00057A74
		// (set) Token: 0x06001DA0 RID: 7584 RVA: 0x0005987C File Offset: 0x00057A7C
		public string Host
		{
			get
			{
				return this.host;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException("An empty string is not allowed.", "value");
				}
				this.CheckState();
				this.host = value;
			}
		}

		/// <summary>Gets or sets the folder where applications save mail messages to be processed by the local SMTP server.</summary>
		/// <returns>A <see cref="T:System.String" /> that specifies the pickup directory for mail messages.</returns>
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x000598B8 File Offset: 0x00057AB8
		// (set) Token: 0x06001DA2 RID: 7586 RVA: 0x000598C0 File Offset: 0x00057AC0
		public string PickupDirectoryLocation
		{
			get
			{
				return this.pickupDirectoryLocation;
			}
			set
			{
				this.pickupDirectoryLocation = value;
			}
		}

		/// <summary>Gets or sets the port used for SMTP transactions.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the port number on the SMTP host. The default value is 25.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x000598CC File Offset: 0x00057ACC
		// (set) Token: 0x06001DA4 RID: 7588 RVA: 0x000598D4 File Offset: 0x00057AD4
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.CheckState();
				this.port = value;
			}
		}

		/// <summary>Gets the network connection used to transmit the e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.ServicePoint" /> that connects to the <see cref="P:System.Net.Mail.SmtpClient.Host" /> property used for SMTP.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null or the empty string ("").-or-<see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero.</exception>
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x000598F8 File Offset: 0x00057AF8
		[global::System.MonoTODO]
		public ServicePoint ServicePoint
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that specifies the amount of time after which a synchronous <see cref="Overload:System.Net.Mail.SmtpClient.Send" /> call times out.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that specifies the time-out value in milliseconds. The default value is 100,000 (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation was less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an email is being sent.</exception>
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x00059900 File Offset: 0x00057B00
		// (set) Token: 0x06001DA7 RID: 7591 RVA: 0x00059908 File Offset: 0x00057B08
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.CheckState();
				this.timeout = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> are sent with requests.</summary>
		/// <returns>true if the default credentials are used; otherwise false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">You cannot change the value of this property when an e-mail is being sent.</exception>
		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x0005992C File Offset: 0x00057B2C
		// (set) Token: 0x06001DA9 RID: 7593 RVA: 0x00059930 File Offset: 0x00057B30
		public bool UseDefaultCredentials
		{
			get
			{
				return false;
			}
			[global::System.MonoNotSupported("no DefaultCredential support in Mono")]
			set
			{
				if (value)
				{
					throw new NotImplementedException("Default credentials are not supported");
				}
				this.CheckState();
			}
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x0005994C File Offset: 0x00057B4C
		private void CheckState()
		{
			if (this.messageInProcess != null)
			{
				throw new InvalidOperationException("Cannot set Timeout while Sending a message");
			}
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x00059964 File Offset: 0x00057B64
		private static string EncodeAddress(MailAddress address)
		{
			string text = global::System.Net.Mime.ContentType.EncodeSubjectRFC2047(address.DisplayName, Encoding.UTF8);
			return string.Concat(new string[] { "\"", text, "\" <", address.Address, ">" });
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x000599B4 File Offset: 0x00057BB4
		private static string EncodeAddresses(MailAddressCollection addresses)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (MailAddress mailAddress in addresses)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(SmtpClient.EncodeAddress(mailAddress));
				flag = false;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x00059A3C File Offset: 0x00057C3C
		private string EncodeSubjectRFC2047(MailMessage message)
		{
			return global::System.Net.Mime.ContentType.EncodeSubjectRFC2047(message.Subject, message.SubjectEncoding);
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x00059A50 File Offset: 0x00057C50
		private string EncodeBody(MailMessage message)
		{
			string body = message.Body;
			Encoding bodyEncoding = message.BodyEncoding;
			global::System.Net.Mime.TransferEncoding contentTransferEncoding = message.ContentTransferEncoding;
			if (contentTransferEncoding == global::System.Net.Mime.TransferEncoding.Base64)
			{
				return Convert.ToBase64String(bodyEncoding.GetBytes(body), Base64FormattingOptions.InsertLineBreaks);
			}
			if (contentTransferEncoding != global::System.Net.Mime.TransferEncoding.SevenBit)
			{
				return this.ToQuotedPrintable(body, bodyEncoding);
			}
			return body;
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00059AA0 File Offset: 0x00057CA0
		private string EncodeBody(AlternateView av)
		{
			byte[] array = new byte[av.ContentStream.Length];
			av.ContentStream.Read(array, 0, array.Length);
			global::System.Net.Mime.TransferEncoding transferEncoding = av.TransferEncoding;
			if (transferEncoding == global::System.Net.Mime.TransferEncoding.Base64)
			{
				return Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks);
			}
			if (transferEncoding != global::System.Net.Mime.TransferEncoding.SevenBit)
			{
				return this.ToQuotedPrintable(array);
			}
			return Encoding.ASCII.GetString(array);
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x00059B08 File Offset: 0x00057D08
		private void EndSection(string section)
		{
			this.SendData(string.Format("--{0}--", section));
			this.SendData(string.Empty);
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x00059B28 File Offset: 0x00057D28
		private string GenerateBoundary()
		{
			string text = SmtpClient.GenerateBoundary(this.boundaryIndex);
			this.boundaryIndex++;
			return text;
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00059B50 File Offset: 0x00057D50
		private static string GenerateBoundary(int index)
		{
			return string.Format("--boundary_{0}_{1}", index, Guid.NewGuid().ToString("D"));
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x00059B80 File Offset: 0x00057D80
		private bool IsError(SmtpClient.SmtpResponse status)
		{
			return status.StatusCode >= (SmtpStatusCode)400;
		}

		/// <summary>Raises the <see cref="E:System.Net.Mail.SmtpClient.SendCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> that contains event data.</param>
		// Token: 0x06001DB4 RID: 7604 RVA: 0x00059B94 File Offset: 0x00057D94
		protected void OnSendCompleted(global::System.ComponentModel.AsyncCompletedEventArgs e)
		{
			try
			{
				if (this.SendCompleted != null)
				{
					this.SendCompleted(this, e);
				}
			}
			finally
			{
				this.worker = null;
				this.user_async_state = null;
			}
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x00059BEC File Offset: 0x00057DEC
		private void CheckCancellation()
		{
			if (this.worker != null && this.worker.CancellationPending)
			{
				throw new SmtpClient.CancellationException();
			}
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00059C10 File Offset: 0x00057E10
		private SmtpClient.SmtpResponse Read()
		{
			byte[] array = new byte[512];
			int num = 0;
			bool flag = false;
			do
			{
				this.CheckCancellation();
				int num2 = this.stream.Read(array, num, array.Length - num);
				if (num2 <= 0)
				{
					break;
				}
				int num3 = num + num2 - 1;
				if (num3 > 4 && (array[num3] == 10 || array[num3] == 13))
				{
					int num4 = num3 - 3;
					while (num4 >= 0 && array[num4] != 10 && array[num4] != 13)
					{
						num4--;
					}
					flag = array[num4 + 4] == 32;
				}
				num += num2;
				if (num == array.Length)
				{
					byte[] array2 = new byte[array.Length * 2];
					Array.Copy(array, 0, array2, 0, array.Length);
					array = array2;
				}
			}
			while (!flag);
			if (num > 0)
			{
				Encoding encoding = new ASCIIEncoding();
				string @string = encoding.GetString(array, 0, num - 1);
				return SmtpClient.SmtpResponse.Parse(@string);
			}
			throw new IOException("Connection closed");
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x00059D20 File Offset: 0x00057F20
		private void ResetExtensions()
		{
			this.authMechs = SmtpClient.AuthMechs.None;
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x00059D2C File Offset: 0x00057F2C
		private void ParseExtensions(string extens)
		{
			char[] array = new char[] { ' ' };
			string[] array2 = extens.Split(new char[] { '\n' });
			foreach (string text in array2)
			{
				if (text.Length >= 4)
				{
					string text2 = text.Substring(4);
					if (text2.StartsWith("AUTH ", StringComparison.Ordinal))
					{
						string[] array4 = text2.Split(array);
						for (int j = 1; j < array4.Length; j++)
						{
							string text3 = array4[j].Trim();
							string text4 = text3;
							switch (text4)
							{
							case "CRAM-MD5":
								this.authMechs |= SmtpClient.AuthMechs.CramMD5;
								break;
							case "DIGEST-MD5":
								this.authMechs |= SmtpClient.AuthMechs.DigestMD5;
								break;
							case "GSSAPI":
								this.authMechs |= SmtpClient.AuthMechs.GssAPI;
								break;
							case "KERBEROS_V4":
								this.authMechs |= SmtpClient.AuthMechs.Kerberos4;
								break;
							case "LOGIN":
								this.authMechs |= SmtpClient.AuthMechs.Login;
								break;
							case "PLAIN":
								this.authMechs |= SmtpClient.AuthMechs.Plain;
								break;
							}
						}
					}
				}
			}
		}

		/// <summary>Sends the specified message to an SMTP server for delivery.</summary>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Net.Mail.MailMessage.From" /> is null.-or-<see cref="P:System.Net.Mail.MailMessage.To" /> is null.-or- <paramref name="message" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">There are no recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, and <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.-or- <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null.-or-<see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").-or- <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.-or-Authentication failed.-or-The operation timed out.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException">The message could not be delivered to one or more of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		// Token: 0x06001DB9 RID: 7609 RVA: 0x00059EF0 File Offset: 0x000580F0
		public void Send(MailMessage message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (this.deliveryMethod == SmtpDeliveryMethod.Network && (this.Host == null || this.Host.Trim().Length == 0))
			{
				throw new InvalidOperationException("The SMTP host was not specified");
			}
			if (this.deliveryMethod == SmtpDeliveryMethod.PickupDirectoryFromIis)
			{
				throw new NotSupportedException("IIS delivery is not supported");
			}
			if (this.port == 0)
			{
				this.port = 25;
			}
			this.mutex.WaitOne();
			try
			{
				this.messageInProcess = message;
				if (this.deliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
				{
					this.SendToFile(message);
				}
				else
				{
					this.SendInternal(message);
				}
			}
			catch (SmtpClient.CancellationException)
			{
			}
			catch (SmtpException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new SmtpException("Message could not be sent.", ex);
			}
			finally
			{
				this.mutex.ReleaseMutex();
				this.messageInProcess = null;
			}
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0005A03C File Offset: 0x0005823C
		private void SendInternal(MailMessage message)
		{
			this.CheckCancellation();
			try
			{
				this.client = new global::System.Net.Sockets.TcpClient(this.host, this.port);
				this.stream = this.client.GetStream();
				this.writer = new StreamWriter(this.stream);
				this.reader = new StreamReader(this.stream);
				this.SendCore(message);
			}
			finally
			{
				if (this.writer != null)
				{
					this.writer.Close();
				}
				if (this.reader != null)
				{
					this.reader.Close();
				}
				if (this.stream != null)
				{
					this.stream.Close();
				}
				if (this.client != null)
				{
					this.client.Close();
				}
			}
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x0005A11C File Offset: 0x0005831C
		private void SendToFile(MailMessage message)
		{
			if (!Path.IsPathRooted(this.pickupDirectoryLocation))
			{
				throw new SmtpException("Only absolute directories are allowed for pickup directory.");
			}
			string text = Path.Combine(this.pickupDirectoryLocation, Guid.NewGuid() + ".eml");
			try
			{
				this.writer = new StreamWriter(text);
				MailAddress from = message.From;
				if (from == null)
				{
					from = this.defaultFrom;
				}
				this.SendHeader("Date", DateTime.Now.ToString("ddd, dd MMM yyyy HH':'mm':'ss zzz", DateTimeFormatInfo.InvariantInfo));
				this.SendHeader("From", from.ToString());
				this.SendHeader("To", message.To.ToString());
				if (message.CC.Count > 0)
				{
					this.SendHeader("Cc", message.CC.ToString());
				}
				this.SendHeader("Subject", this.EncodeSubjectRFC2047(message));
				foreach (string text2 in message.Headers.AllKeys)
				{
					this.SendHeader(text2, message.Headers[text2]);
				}
				this.AddPriorityHeader(message);
				this.boundaryIndex = 0;
				if (message.Attachments.Count > 0)
				{
					this.SendWithAttachments(message);
				}
				else
				{
					this.SendWithoutAttachments(message, null, false);
				}
			}
			finally
			{
				if (this.writer != null)
				{
					this.writer.Close();
				}
				this.writer = null;
			}
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0005A2B8 File Offset: 0x000584B8
		private void SendCore(MailMessage message)
		{
			SmtpClient.SmtpResponse smtpResponse = this.Read();
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			smtpResponse = this.SendCommand("EHLO " + Dns.GetHostName());
			if (this.IsError(smtpResponse))
			{
				smtpResponse = this.SendCommand("HELO " + Dns.GetHostName());
				if (this.IsError(smtpResponse))
				{
					throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
				}
			}
			else
			{
				string description = smtpResponse.Description;
				if (description != null)
				{
					this.ParseExtensions(description);
				}
			}
			if (this.enableSsl)
			{
				this.InitiateSecureConnection();
				this.ResetExtensions();
				this.writer = new StreamWriter(this.stream);
				this.reader = new StreamReader(this.stream);
				smtpResponse = this.SendCommand("EHLO " + Dns.GetHostName());
				if (this.IsError(smtpResponse))
				{
					smtpResponse = this.SendCommand("HELO " + Dns.GetHostName());
					if (this.IsError(smtpResponse))
					{
						throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
					}
				}
				else
				{
					string description2 = smtpResponse.Description;
					if (description2 != null)
					{
						this.ParseExtensions(description2);
					}
				}
			}
			if (this.authMechs != SmtpClient.AuthMechs.None)
			{
				this.Authenticate();
			}
			MailAddress from = message.From;
			if (from == null)
			{
				from = this.defaultFrom;
			}
			smtpResponse = this.SendCommand("MAIL FROM:<" + from.Address + '>');
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			List<SmtpFailedRecipientException> list = new List<SmtpFailedRecipientException>();
			for (int i = 0; i < message.To.Count; i++)
			{
				smtpResponse = this.SendCommand("RCPT TO:<" + message.To[i].Address + '>');
				if (this.IsError(smtpResponse))
				{
					list.Add(new SmtpFailedRecipientException(smtpResponse.StatusCode, message.To[i].Address));
				}
			}
			for (int j = 0; j < message.CC.Count; j++)
			{
				smtpResponse = this.SendCommand("RCPT TO:<" + message.CC[j].Address + '>');
				if (this.IsError(smtpResponse))
				{
					list.Add(new SmtpFailedRecipientException(smtpResponse.StatusCode, message.CC[j].Address));
				}
			}
			for (int k = 0; k < message.Bcc.Count; k++)
			{
				smtpResponse = this.SendCommand("RCPT TO:<" + message.Bcc[k].Address + '>');
				if (this.IsError(smtpResponse))
				{
					list.Add(new SmtpFailedRecipientException(smtpResponse.StatusCode, message.Bcc[k].Address));
				}
			}
			if (list.Count > 0)
			{
				throw new SmtpFailedRecipientsException("failed recipients", list.ToArray());
			}
			smtpResponse = this.SendCommand("DATA");
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			string text = DateTime.Now.ToString("ddd, dd MMM yyyy HH':'mm':'ss zzz", DateTimeFormatInfo.InvariantInfo);
			text = text.Remove(text.Length - 3, 1);
			this.SendHeader("Date", text);
			this.SendHeader("From", SmtpClient.EncodeAddress(from));
			this.SendHeader("To", SmtpClient.EncodeAddresses(message.To));
			if (message.CC.Count > 0)
			{
				this.SendHeader("Cc", SmtpClient.EncodeAddresses(message.CC));
			}
			this.SendHeader("Subject", this.EncodeSubjectRFC2047(message));
			string text2 = "normal";
			switch (message.Priority)
			{
			case MailPriority.Normal:
				text2 = "normal";
				break;
			case MailPriority.Low:
				text2 = "non-urgent";
				break;
			case MailPriority.High:
				text2 = "urgent";
				break;
			}
			this.SendHeader("Priority", text2);
			if (message.Sender != null)
			{
				this.SendHeader("Sender", SmtpClient.EncodeAddress(message.Sender));
			}
			if (message.ReplyToList.Count > 0)
			{
				this.SendHeader("Reply-To", SmtpClient.EncodeAddresses(message.ReplyToList));
			}
			foreach (string text3 in message.Headers.AllKeys)
			{
				this.SendHeader(text3, message.Headers[text3]);
			}
			this.AddPriorityHeader(message);
			this.boundaryIndex = 0;
			if (message.Attachments.Count > 0)
			{
				this.SendWithAttachments(message);
			}
			else
			{
				this.SendWithoutAttachments(message, null, false);
			}
			this.SendDot();
			smtpResponse = this.Read();
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			try
			{
				smtpResponse = this.SendCommand("QUIT");
			}
			catch (IOException)
			{
			}
		}

		/// <summary>Sends the specified e-mail message to an SMTP server for delivery. The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the addresses that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="recipient" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.-or-<paramref name="recipient" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.-or- <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null.-or-<see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").-or- <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.-or-Authentication failed.-or-The operation timed out.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException">The message could not be delivered to one or more of the recipients in <paramref name="recipients" />. </exception>
		// Token: 0x06001DBD RID: 7613 RVA: 0x0005A840 File Offset: 0x00058A40
		public void Send(string from, string to, string subject, string body)
		{
			this.Send(new MailMessage(from, to, subject, body));
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0005A854 File Offset: 0x00058A54
		private void SendDot()
		{
			this.writer.Write(".\r\n");
			this.writer.Flush();
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0005A874 File Offset: 0x00058A74
		private void SendData(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				this.writer.Write("\r\n");
				this.writer.Flush();
				return;
			}
			StringReader stringReader = new StringReader(data);
			bool flag = this.deliveryMethod == SmtpDeliveryMethod.Network;
			string text;
			while ((text = stringReader.ReadLine()) != null)
			{
				this.CheckCancellation();
				if (flag)
				{
					int i;
					for (i = 0; i < text.Length; i++)
					{
						if (text[i] != '.')
						{
							break;
						}
					}
					if (i > 0 && i == text.Length)
					{
						text += ".";
					}
				}
				this.writer.Write(text);
				this.writer.Write("\r\n");
			}
			this.writer.Flush();
		}

		/// <summary>Sends the specified e-mail message to an SMTP server for delivery. This method does not block the calling thread and allows the caller to pass an object to the method that is invoked when the operation completes. </summary>
		/// <param name="message">A <see cref="T:System.Net.Mail.MailMessage" /> that contains the message to send.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Net.Mail.MailMessage.From" /> is null.-or-<see cref="P:System.Net.Mail.MailMessage.To" /> is null.-or- <paramref name="message" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">There are no recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, and <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.-or- <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null.-or-<see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").-or- <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.-or-Authentication failed.-or-The operation timed out.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException">The <paramref name="message" /> could not be delivered to one or more of the recipients in <see cref="P:System.Net.Mail.MailMessage.To" />, <see cref="P:System.Net.Mail.MailMessage.CC" />, or <see cref="P:System.Net.Mail.MailMessage.Bcc" />.</exception>
		// Token: 0x06001DC0 RID: 7616 RVA: 0x0005A948 File Offset: 0x00058B48
		public void SendAsync(MailMessage message, object userToken)
		{
			if (this.worker != null)
			{
				throw new InvalidOperationException("Another SendAsync operation is in progress");
			}
			this.worker = new global::System.ComponentModel.BackgroundWorker();
			this.worker.DoWork += delegate(object o, global::System.ComponentModel.DoWorkEventArgs ea)
			{
				try
				{
					this.user_async_state = ea.Argument;
					this.Send(message);
				}
				catch (Exception ex)
				{
					ea.Result = ex;
					throw ex;
				}
			};
			this.worker.WorkerSupportsCancellation = true;
			this.worker.RunWorkerCompleted += delegate(object o, global::System.ComponentModel.RunWorkerCompletedEventArgs ea)
			{
				this.OnSendCompleted(new global::System.ComponentModel.AsyncCompletedEventArgs(ea.Error, ea.Cancelled, this.user_async_state));
			};
			this.worker.RunWorkerAsync(userToken);
		}

		/// <summary>Sends an e-mail message to an SMTP server for delivery. The message sender, recipients, subject, and message body are specified using <see cref="T:System.String" /> objects. This method does not block the calling thread and allows the caller to pass an object to the method that is invoked when the operation completes.</summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address information of the message sender.</param>
		/// <param name="recipients">A <see cref="T:System.String" /> that contains the address that the message is sent to.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject line for the message.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <param name="userToken">A user-defined object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="recipient" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" />.-or-<paramref name="recipient" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.Mail.SmtpClient" /> has a <see cref="Overload:System.Net.Mail.SmtpClient.SendAsync" /> call in progress.-or- <see cref="P:System.Net.Mail.SmtpClient.Host" /> is null.-or-<see cref="P:System.Net.Mail.SmtpClient.Host" /> is equal to the empty string ("").-or- <see cref="P:System.Net.Mail.SmtpClient.Port" /> is zero.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpException">The connection to the SMTP server failed.-or-Authentication failed.-or-The operation timed out.</exception>
		/// <exception cref="T:System.Net.Mail.SmtpFailedRecipientsException">The message could not be delivered to one or more of the recipients in <paramref name="recipients" />. </exception>
		// Token: 0x06001DC1 RID: 7617 RVA: 0x0005A9D0 File Offset: 0x00058BD0
		public void SendAsync(string from, string to, string subject, string body, object userToken)
		{
			this.SendAsync(new MailMessage(from, to, subject, body), userToken);
		}

		/// <summary>Cancels an asynchronous operation to send an e-mail message.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06001DC2 RID: 7618 RVA: 0x0005A9E4 File Offset: 0x00058BE4
		public void SendAsyncCancel()
		{
			if (this.worker == null)
			{
				throw new InvalidOperationException("SendAsync operation is not in progress");
			}
			this.worker.CancelAsync();
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0005AA08 File Offset: 0x00058C08
		private void AddPriorityHeader(MailMessage message)
		{
			MailPriority priority = message.Priority;
			if (priority != MailPriority.Low)
			{
				if (priority == MailPriority.High)
				{
					this.SendHeader("Priority", "Urgent");
					this.SendHeader("Importance", "high");
					this.SendHeader("X-Priority", "1");
				}
			}
			else
			{
				this.SendHeader("Priority", "Non-Urgent");
				this.SendHeader("Importance", "low");
				this.SendHeader("X-Priority", "5");
			}
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x0005AA9C File Offset: 0x00058C9C
		private void SendSimpleBody(MailMessage message)
		{
			this.SendHeader("Content-Type", message.BodyContentType.ToString());
			if (message.ContentTransferEncoding != global::System.Net.Mime.TransferEncoding.SevenBit)
			{
				this.SendHeader("Content-Transfer-Encoding", SmtpClient.GetTransferEncodingName(message.ContentTransferEncoding));
			}
			this.SendData(string.Empty);
			this.SendData(this.EncodeBody(message));
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x0005AAFC File Offset: 0x00058CFC
		private void SendBodylessSingleAlternate(AlternateView av)
		{
			this.SendHeader("Content-Type", av.ContentType.ToString());
			if (av.TransferEncoding != global::System.Net.Mime.TransferEncoding.SevenBit)
			{
				this.SendHeader("Content-Transfer-Encoding", SmtpClient.GetTransferEncodingName(av.TransferEncoding));
			}
			this.SendData(string.Empty);
			this.SendData(this.EncodeBody(av));
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x0005AB5C File Offset: 0x00058D5C
		private void SendWithoutAttachments(MailMessage message, string boundary, bool attachmentExists)
		{
			if (message.Body == null && message.AlternateViews.Count == 1)
			{
				this.SendBodylessSingleAlternate(message.AlternateViews[0]);
			}
			else if (message.AlternateViews.Count > 0)
			{
				this.SendBodyWithAlternateViews(message, boundary, attachmentExists);
			}
			else
			{
				this.SendSimpleBody(message);
			}
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x0005ABC4 File Offset: 0x00058DC4
		private void SendWithAttachments(MailMessage message)
		{
			string text = this.GenerateBoundary();
			this.SendHeader("Content-Type", new global::System.Net.Mime.ContentType
			{
				Boundary = text,
				MediaType = "multipart/mixed",
				CharSet = null
			}.ToString());
			this.SendData(string.Empty);
			Attachment attachment = null;
			if (message.AlternateViews.Count > 0)
			{
				this.SendWithoutAttachments(message, text, true);
			}
			else
			{
				attachment = Attachment.CreateAttachmentFromString(message.Body, null, message.BodyEncoding, (!message.IsBodyHtml) ? "text/plain" : "text/html");
				message.Attachments.Insert(0, attachment);
			}
			try
			{
				this.SendAttachments(message, attachment, text);
			}
			finally
			{
				if (attachment != null)
				{
					message.Attachments.Remove(attachment);
				}
			}
			this.EndSection(text);
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x0005ACB4 File Offset: 0x00058EB4
		private void SendBodyWithAlternateViews(MailMessage message, string boundary, bool attachmentExists)
		{
			AlternateViewCollection alternateViews = message.AlternateViews;
			string text = this.GenerateBoundary();
			global::System.Net.Mime.ContentType contentType = new global::System.Net.Mime.ContentType();
			contentType.Boundary = text;
			contentType.MediaType = "multipart/alternative";
			if (!attachmentExists)
			{
				this.SendHeader("Content-Type", contentType.ToString());
				this.SendData(string.Empty);
			}
			AlternateView alternateView = null;
			if (message.Body != null)
			{
				alternateView = AlternateView.CreateAlternateViewFromString(message.Body, message.BodyEncoding, (!message.IsBodyHtml) ? "text/plain" : "text/html");
				alternateViews.Insert(0, alternateView);
				this.StartSection(boundary, contentType);
			}
			try
			{
				foreach (AlternateView alternateView2 in alternateViews)
				{
					string text2 = null;
					if (alternateView2.LinkedResources.Count > 0)
					{
						text2 = this.GenerateBoundary();
						global::System.Net.Mime.ContentType contentType2 = new global::System.Net.Mime.ContentType("multipart/related");
						contentType2.Boundary = text2;
						contentType2.Parameters["type"] = alternateView2.ContentType.ToString();
						this.StartSection(text, contentType2);
						this.StartSection(text2, alternateView2.ContentType, alternateView2.TransferEncoding);
					}
					else
					{
						global::System.Net.Mime.ContentType contentType2 = new global::System.Net.Mime.ContentType(alternateView2.ContentType.ToString());
						this.StartSection(text, contentType2, alternateView2.TransferEncoding);
					}
					global::System.Net.Mime.TransferEncoding transferEncoding = alternateView2.TransferEncoding;
					switch (transferEncoding + 1)
					{
					case global::System.Net.Mime.TransferEncoding.QuotedPrintable:
					case (global::System.Net.Mime.TransferEncoding)3:
					{
						byte[] array = new byte[alternateView2.ContentStream.Length];
						alternateView2.ContentStream.Read(array, 0, array.Length);
						this.SendData(Encoding.ASCII.GetString(array));
						break;
					}
					case global::System.Net.Mime.TransferEncoding.Base64:
					{
						byte[] array2 = new byte[alternateView2.ContentStream.Length];
						alternateView2.ContentStream.Read(array2, 0, array2.Length);
						this.SendData(this.ToQuotedPrintable(array2));
						break;
					}
					case global::System.Net.Mime.TransferEncoding.SevenBit:
					{
						byte[] array = new byte[alternateView2.ContentStream.Length];
						alternateView2.ContentStream.Read(array, 0, array.Length);
						this.SendData(Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks));
						break;
					}
					}
					if (alternateView2.LinkedResources.Count > 0)
					{
						this.SendLinkedResources(message, alternateView2.LinkedResources, text2);
						this.EndSection(text2);
					}
					if (!attachmentExists)
					{
						this.SendData(string.Empty);
					}
				}
			}
			finally
			{
				if (alternateView != null)
				{
					alternateViews.Remove(alternateView);
				}
			}
			this.EndSection(text);
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0005AF84 File Offset: 0x00059184
		private void SendLinkedResources(MailMessage message, LinkedResourceCollection resources, string boundary)
		{
			foreach (LinkedResource linkedResource in resources)
			{
				this.StartSection(boundary, linkedResource.ContentType, linkedResource.TransferEncoding, linkedResource);
				global::System.Net.Mime.TransferEncoding transferEncoding = linkedResource.TransferEncoding;
				switch (transferEncoding + 1)
				{
				case global::System.Net.Mime.TransferEncoding.QuotedPrintable:
				case (global::System.Net.Mime.TransferEncoding)3:
				{
					byte[] array = new byte[linkedResource.ContentStream.Length];
					linkedResource.ContentStream.Read(array, 0, array.Length);
					this.SendData(Encoding.ASCII.GetString(array));
					break;
				}
				case global::System.Net.Mime.TransferEncoding.Base64:
				{
					byte[] array2 = new byte[linkedResource.ContentStream.Length];
					linkedResource.ContentStream.Read(array2, 0, array2.Length);
					this.SendData(this.ToQuotedPrintable(array2));
					break;
				}
				case global::System.Net.Mime.TransferEncoding.SevenBit:
				{
					byte[] array = new byte[linkedResource.ContentStream.Length];
					linkedResource.ContentStream.Read(array, 0, array.Length);
					this.SendData(Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks));
					break;
				}
				}
			}
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x0005B0B8 File Offset: 0x000592B8
		private void SendAttachments(MailMessage message, Attachment body, string boundary)
		{
			foreach (Attachment attachment in message.Attachments)
			{
				global::System.Net.Mime.ContentType contentType = new global::System.Net.Mime.ContentType(attachment.ContentType.ToString());
				if (attachment.Name != null)
				{
					contentType.Name = attachment.Name;
					if (attachment.NameEncoding != null)
					{
						contentType.CharSet = attachment.NameEncoding.HeaderName;
					}
					attachment.ContentDisposition.FileName = attachment.Name;
				}
				this.StartSection(boundary, contentType, attachment.TransferEncoding, (attachment != body) ? attachment.ContentDisposition : null);
				byte[] array = new byte[attachment.ContentStream.Length];
				attachment.ContentStream.Read(array, 0, array.Length);
				global::System.Net.Mime.TransferEncoding transferEncoding = attachment.TransferEncoding;
				switch (transferEncoding + 1)
				{
				case global::System.Net.Mime.TransferEncoding.QuotedPrintable:
				case (global::System.Net.Mime.TransferEncoding)3:
					this.SendData(Encoding.ASCII.GetString(array));
					break;
				case global::System.Net.Mime.TransferEncoding.Base64:
					this.SendData(this.ToQuotedPrintable(array));
					break;
				case global::System.Net.Mime.TransferEncoding.SevenBit:
					this.SendData(Convert.ToBase64String(array, Base64FormattingOptions.InsertLineBreaks));
					break;
				}
				this.SendData(string.Empty);
			}
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x0005B218 File Offset: 0x00059418
		private SmtpClient.SmtpResponse SendCommand(string command)
		{
			this.writer.Write(command);
			this.writer.Write("\r\n");
			this.writer.Flush();
			return this.Read();
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x0005B254 File Offset: 0x00059454
		private void SendHeader(string name, string value)
		{
			this.SendData(string.Format("{0}: {1}", name, value));
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x0005B268 File Offset: 0x00059468
		private void StartSection(string section, global::System.Net.Mime.ContentType sectionContentType)
		{
			this.SendData(string.Format("--{0}", section));
			this.SendHeader("content-type", sectionContentType.ToString());
			this.SendData(string.Empty);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x0005B2A4 File Offset: 0x000594A4
		private void StartSection(string section, global::System.Net.Mime.ContentType sectionContentType, global::System.Net.Mime.TransferEncoding transferEncoding)
		{
			this.SendData(string.Format("--{0}", section));
			this.SendHeader("content-type", sectionContentType.ToString());
			this.SendHeader("content-transfer-encoding", SmtpClient.GetTransferEncodingName(transferEncoding));
			this.SendData(string.Empty);
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x0005B2F0 File Offset: 0x000594F0
		private void StartSection(string section, global::System.Net.Mime.ContentType sectionContentType, global::System.Net.Mime.TransferEncoding transferEncoding, LinkedResource lr)
		{
			this.SendData(string.Format("--{0}", section));
			this.SendHeader("content-type", sectionContentType.ToString());
			this.SendHeader("content-transfer-encoding", SmtpClient.GetTransferEncodingName(transferEncoding));
			if (lr.ContentId != null && lr.ContentId.Length > 0)
			{
				this.SendHeader("content-ID", "<" + lr.ContentId + ">");
			}
			this.SendData(string.Empty);
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0005B37C File Offset: 0x0005957C
		private void StartSection(string section, global::System.Net.Mime.ContentType sectionContentType, global::System.Net.Mime.TransferEncoding transferEncoding, global::System.Net.Mime.ContentDisposition contentDisposition)
		{
			this.SendData(string.Format("--{0}", section));
			this.SendHeader("content-type", sectionContentType.ToString());
			this.SendHeader("content-transfer-encoding", SmtpClient.GetTransferEncodingName(transferEncoding));
			if (contentDisposition != null)
			{
				this.SendHeader("content-disposition", contentDisposition.ToString());
			}
			this.SendData(string.Empty);
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0005B3E0 File Offset: 0x000595E0
		private string ToQuotedPrintable(string input, Encoding enc)
		{
			byte[] bytes = enc.GetBytes(input);
			return this.ToQuotedPrintable(bytes);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0005B3FC File Offset: 0x000595FC
		private string ToQuotedPrintable(byte[] bytes)
		{
			StringWriter stringWriter = new StringWriter();
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder("=", 3);
			byte b = 61;
			char c = '\0';
			int i = 0;
			while (i < bytes.Length)
			{
				byte b2 = bytes[i];
				int num2;
				if (b2 > 127 || b2 == b)
				{
					stringBuilder.Length = 1;
					stringBuilder.Append(Convert.ToString(b2, 16).ToUpperInvariant());
					num2 = 3;
					goto IL_008E;
				}
				c = Convert.ToChar(b2);
				if (c != '\r' && c != '\n')
				{
					num2 = 1;
					goto IL_008E;
				}
				stringWriter.Write(c);
				num = 0;
				IL_00C7:
				i++;
				continue;
				IL_008E:
				num += num2;
				if (num > 75)
				{
					stringWriter.Write("=\r\n");
					num = num2;
				}
				if (num2 == 1)
				{
					stringWriter.Write(c);
					goto IL_00C7;
				}
				stringWriter.Write(stringBuilder.ToString());
				goto IL_00C7;
			}
			return stringWriter.ToString();
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x0005B4E8 File Offset: 0x000596E8
		private static string GetTransferEncodingName(global::System.Net.Mime.TransferEncoding encoding)
		{
			switch (encoding)
			{
			case global::System.Net.Mime.TransferEncoding.QuotedPrintable:
				return "quoted-printable";
			case global::System.Net.Mime.TransferEncoding.Base64:
				return "base64";
			case global::System.Net.Mime.TransferEncoding.SevenBit:
				return "7bit";
			default:
				return "unknown";
			}
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0005B528 File Offset: 0x00059728
		private void InitiateSecureConnection()
		{
			SmtpClient.SmtpResponse smtpResponse = this.SendCommand("STARTTLS");
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(SmtpStatusCode.GeneralFailure, "Server does not support secure connections.");
			}
			global::System.Net.Security.SslStream sslStream = new global::System.Net.Security.SslStream(this.stream, false, this.callback, null);
			this.CheckCancellation();
			sslStream.AuthenticateAsClient(this.Host, this.ClientCertificates, global::System.Security.Authentication.SslProtocols.Default, false);
			this.stream = sslStream;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x0005B594 File Offset: 0x00059794
		private void Authenticate()
		{
			string text;
			string text2;
			if (this.UseDefaultCredentials)
			{
				text = CredentialCache.DefaultCredentials.GetCredential(new global::System.Uri("smtp://" + this.host), "basic").UserName;
				text2 = CredentialCache.DefaultCredentials.GetCredential(new global::System.Uri("smtp://" + this.host), "basic").Password;
			}
			else
			{
				if (this.Credentials == null)
				{
					return;
				}
				text = this.Credentials.GetCredential(this.host, this.port, "smtp").UserName;
				text2 = this.Credentials.GetCredential(this.host, this.port, "smtp").Password;
			}
			this.Authenticate(text, text2);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x0005B668 File Offset: 0x00059868
		private void Authenticate(string Username, string Password)
		{
			SmtpClient.SmtpResponse smtpResponse = this.SendCommand("AUTH LOGIN");
			if (smtpResponse.StatusCode != (SmtpStatusCode)334)
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			smtpResponse = this.SendCommand(Convert.ToBase64String(Encoding.ASCII.GetBytes(Username)));
			if (smtpResponse.StatusCode != (SmtpStatusCode)334)
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
			smtpResponse = this.SendCommand(Convert.ToBase64String(Encoding.ASCII.GetBytes(Password)));
			if (this.IsError(smtpResponse))
			{
				throw new SmtpException(smtpResponse.StatusCode, smtpResponse.Description);
			}
		}

		// Token: 0x04001257 RID: 4695
		private string host;

		// Token: 0x04001258 RID: 4696
		private int port;

		// Token: 0x04001259 RID: 4697
		private int timeout = 100000;

		// Token: 0x0400125A RID: 4698
		private ICredentialsByHost credentials;

		// Token: 0x0400125B RID: 4699
		private string pickupDirectoryLocation;

		// Token: 0x0400125C RID: 4700
		private SmtpDeliveryMethod deliveryMethod;

		// Token: 0x0400125D RID: 4701
		private bool enableSsl;

		// Token: 0x0400125E RID: 4702
		private global::System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates;

		// Token: 0x0400125F RID: 4703
		private global::System.Net.Sockets.TcpClient client;

		// Token: 0x04001260 RID: 4704
		private Stream stream;

		// Token: 0x04001261 RID: 4705
		private StreamWriter writer;

		// Token: 0x04001262 RID: 4706
		private StreamReader reader;

		// Token: 0x04001263 RID: 4707
		private int boundaryIndex;

		// Token: 0x04001264 RID: 4708
		private MailAddress defaultFrom;

		// Token: 0x04001265 RID: 4709
		private MailMessage messageInProcess;

		// Token: 0x04001266 RID: 4710
		private global::System.ComponentModel.BackgroundWorker worker;

		// Token: 0x04001267 RID: 4711
		private object user_async_state;

		// Token: 0x04001268 RID: 4712
		private SmtpClient.AuthMechs authMechs;

		// Token: 0x04001269 RID: 4713
		private Mutex mutex = new Mutex();

		// Token: 0x0400126A RID: 4714
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

		// Token: 0x02000342 RID: 834
		[Flags]
		private enum AuthMechs
		{
			// Token: 0x04001270 RID: 4720
			None = 0,
			// Token: 0x04001271 RID: 4721
			CramMD5 = 1,
			// Token: 0x04001272 RID: 4722
			DigestMD5 = 2,
			// Token: 0x04001273 RID: 4723
			GssAPI = 4,
			// Token: 0x04001274 RID: 4724
			Kerberos4 = 8,
			// Token: 0x04001275 RID: 4725
			Login = 16,
			// Token: 0x04001276 RID: 4726
			Plain = 32
		}

		// Token: 0x02000343 RID: 835
		private class CancellationException : Exception
		{
		}

		// Token: 0x02000344 RID: 836
		private struct HeaderName
		{
			// Token: 0x04001277 RID: 4727
			public const string ContentTransferEncoding = "Content-Transfer-Encoding";

			// Token: 0x04001278 RID: 4728
			public const string ContentType = "Content-Type";

			// Token: 0x04001279 RID: 4729
			public const string Bcc = "Bcc";

			// Token: 0x0400127A RID: 4730
			public const string Cc = "Cc";

			// Token: 0x0400127B RID: 4731
			public const string From = "From";

			// Token: 0x0400127C RID: 4732
			public const string Subject = "Subject";

			// Token: 0x0400127D RID: 4733
			public const string To = "To";

			// Token: 0x0400127E RID: 4734
			public const string MimeVersion = "MIME-Version";

			// Token: 0x0400127F RID: 4735
			public const string MessageId = "Message-ID";

			// Token: 0x04001280 RID: 4736
			public const string Priority = "Priority";

			// Token: 0x04001281 RID: 4737
			public const string Importance = "Importance";

			// Token: 0x04001282 RID: 4738
			public const string XPriority = "X-Priority";

			// Token: 0x04001283 RID: 4739
			public const string Date = "Date";
		}

		// Token: 0x02000345 RID: 837
		private struct SmtpResponse
		{
			// Token: 0x06001DD9 RID: 7641 RVA: 0x0005B768 File Offset: 0x00059968
			public static SmtpClient.SmtpResponse Parse(string line)
			{
				SmtpClient.SmtpResponse smtpResponse = default(SmtpClient.SmtpResponse);
				if (line.Length < 4)
				{
					throw new SmtpException("Response is to short " + line.Length + ".");
				}
				if (line[3] != ' ' && line[3] != '-')
				{
					throw new SmtpException("Response format is wrong.(" + line + ")");
				}
				smtpResponse.StatusCode = (SmtpStatusCode)int.Parse(line.Substring(0, 3));
				smtpResponse.Description = line;
				return smtpResponse;
			}

			// Token: 0x04001284 RID: 4740
			public SmtpStatusCode StatusCode;

			// Token: 0x04001285 RID: 4741
			public string Description;
		}
	}
}
