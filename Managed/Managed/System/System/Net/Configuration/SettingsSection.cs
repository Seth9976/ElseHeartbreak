using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for sockets, IPv6, response headers, and service points. This class cannot be inherited.</summary>
	// Token: 0x020002E4 RID: 740
	public sealed class SettingsSection : ConfigurationSection
	{
		// Token: 0x06001940 RID: 6464 RVA: 0x00045880 File Offset: 0x00043A80
		static SettingsSection()
		{
			SettingsSection.properties.Add(SettingsSection.httpWebRequestProp);
			SettingsSection.properties.Add(SettingsSection.ipv6Prop);
			SettingsSection.properties.Add(SettingsSection.performanceCountersProp);
			SettingsSection.properties.Add(SettingsSection.servicePointManagerProp);
			SettingsSection.properties.Add(SettingsSection.socketProp);
			SettingsSection.properties.Add(SettingsSection.webProxyScriptProp);
		}

		/// <summary>Gets the configuration element that controls the maximum response header length.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.HttpWebRequestElement" /> object.</returns>
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x00045988 File Offset: 0x00043B88
		[ConfigurationProperty("httpWebRequest")]
		public HttpWebRequestElement HttpWebRequest
		{
			get
			{
				return (HttpWebRequestElement)base[SettingsSection.httpWebRequestProp];
			}
		}

		/// <summary>Gets the configuration element that enables Internet Protocol version 6 (IPv6).</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.Ipv6Element" />.</returns>
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x0004599C File Offset: 0x00043B9C
		[ConfigurationProperty("ipv6")]
		public Ipv6Element Ipv6
		{
			get
			{
				return (Ipv6Element)base[SettingsSection.ipv6Prop];
			}
		}

		/// <summary>Gets the configuration element that controls whether performance counters are enabled.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.PerformanceCountersElement" />.</returns>
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x000459B0 File Offset: 0x00043BB0
		[ConfigurationProperty("performanceCounters")]
		public PerformanceCountersElement PerformanceCounters
		{
			get
			{
				return (PerformanceCountersElement)base[SettingsSection.performanceCountersProp];
			}
		}

		/// <summary>Gets the configuration element that controls settings for connections to remote host computers.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ServicePointManagerElement" /> object.</returns>
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x000459C4 File Offset: 0x00043BC4
		[ConfigurationProperty("servicePointManager")]
		public ServicePointManagerElement ServicePointManager
		{
			get
			{
				return (ServicePointManagerElement)base[SettingsSection.servicePointManagerProp];
			}
		}

		/// <summary>Gets the configuration element that controls settings for sockets.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SocketElement" /> object.</returns>
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x000459D8 File Offset: 0x00043BD8
		[ConfigurationProperty("socket")]
		public SocketElement Socket
		{
			get
			{
				return (SocketElement)base[SettingsSection.socketProp];
			}
		}

		/// <summary>Gets the configuration element that controls the execution timeout and download timeout of Web proxy scripts.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebProxyScriptElement" /> object.</returns>
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x000459EC File Offset: 0x00043BEC
		[ConfigurationProperty("webProxyScript")]
		public WebProxyScriptElement WebProxyScript
		{
			get
			{
				return (WebProxyScriptElement)base[SettingsSection.webProxyScriptProp];
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001947 RID: 6471 RVA: 0x00045A00 File Offset: 0x00043C00
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SettingsSection.properties;
			}
		}

		// Token: 0x04000FF6 RID: 4086
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000FF7 RID: 4087
		private static ConfigurationProperty httpWebRequestProp = new ConfigurationProperty("httpWebRequest", typeof(HttpWebRequestElement));

		// Token: 0x04000FF8 RID: 4088
		private static ConfigurationProperty ipv6Prop = new ConfigurationProperty("ipv6", typeof(Ipv6Element));

		// Token: 0x04000FF9 RID: 4089
		private static ConfigurationProperty performanceCountersProp = new ConfigurationProperty("performanceCounters", typeof(PerformanceCountersElement));

		// Token: 0x04000FFA RID: 4090
		private static ConfigurationProperty servicePointManagerProp = new ConfigurationProperty("servicePointManager", typeof(ServicePointManagerElement));

		// Token: 0x04000FFB RID: 4091
		private static ConfigurationProperty webProxyScriptProp = new ConfigurationProperty("webProxyScript", typeof(WebProxyScriptElement));

		// Token: 0x04000FFC RID: 4092
		private static ConfigurationProperty socketProp = new ConfigurationProperty("socket", typeof(SocketElement));
	}
}
