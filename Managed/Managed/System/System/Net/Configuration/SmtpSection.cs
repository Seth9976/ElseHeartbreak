using System;
using System.Configuration;
using System.Net.Mail;

namespace System.Net.Configuration
{
	/// <summary>Represents the SMTP section in the System.Net configuration file.</summary>
	// Token: 0x020002E6 RID: 742
	public sealed class SmtpSection : ConfigurationSection
	{
		/// <summary>Gets or sets the SMTP delivery method. The default delivery method is <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" />.</summary>
		/// <returns>A string that represents the SMTP delivery method.</returns>
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001956 RID: 6486 RVA: 0x00045AE0 File Offset: 0x00043CE0
		// (set) Token: 0x06001957 RID: 6487 RVA: 0x00045AF4 File Offset: 0x00043CF4
		[ConfigurationProperty("deliveryMethod", DefaultValue = "Network")]
		public global::System.Net.Mail.SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return (global::System.Net.Mail.SmtpDeliveryMethod)((int)base["deliveryMethod"]);
			}
			set
			{
				base["deliveryMethod"] = value;
			}
		}

		/// <summary>Gets or sets the default value that indicates who the email message is from.</summary>
		/// <returns>A string that represents the default value indicating who a mail message is from.</returns>
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001958 RID: 6488 RVA: 0x00045B08 File Offset: 0x00043D08
		// (set) Token: 0x06001959 RID: 6489 RVA: 0x00045B1C File Offset: 0x00043D1C
		[ConfigurationProperty("from")]
		public string From
		{
			get
			{
				return (string)base["from"];
			}
			set
			{
				base["from"] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Net.Configuration.SmtpNetworkElement" />.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpNetworkElement" /> object.</returns>
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x00045B2C File Offset: 0x00043D2C
		[ConfigurationProperty("network")]
		public SmtpNetworkElement Network
		{
			get
			{
				return (SmtpNetworkElement)base["network"];
			}
		}

		/// <summary>Gets the pickup directory that will be used by the SMPT client.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpSpecifiedPickupDirectoryElement" /> object that specifies the pickup directory folder.</returns>
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x00045B40 File Offset: 0x00043D40
		[ConfigurationProperty("specifiedPickupDirectory")]
		public SmtpSpecifiedPickupDirectoryElement SpecifiedPickupDirectory
		{
			get
			{
				return (SmtpSpecifiedPickupDirectoryElement)base["specifiedPickupDirectory"];
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x00045B54 File Offset: 0x00043D54
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}
	}
}
