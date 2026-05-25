using System;

namespace System.Configuration
{
	/// <summary>Provides the configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</summary>
	// Token: 0x020001E2 RID: 482
	public sealed class IdnElement : ConfigurationElement
	{
		// Token: 0x060010BA RID: 4282 RVA: 0x0002D3D4 File Offset: 0x0002B5D4
		static IdnElement()
		{
			IdnElement.properties.Add(IdnElement.enabled_prop);
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.IdnElement" /> configuration setting. </summary>
		/// <returns>A <see cref="T:System.UriIdnScope" /> that contains the current configuration setting for IDN processing.</returns>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0002D410 File Offset: 0x0002B610
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x0002D424 File Offset: 0x0002B624
		[ConfigurationProperty("enabled", DefaultValue = global::System.UriIdnScope.None, Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public global::System.UriIdnScope Enabled
		{
			get
			{
				return (global::System.UriIdnScope)((int)base[IdnElement.enabled_prop]);
			}
			set
			{
				base[IdnElement.enabled_prop] = value;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0002D438 File Offset: 0x0002B638
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IdnElement.properties;
			}
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0002D440 File Offset: 0x0002B640
		public override bool Equals(object o)
		{
			IdnElement idnElement = o as IdnElement;
			return idnElement != null && idnElement.Enabled == this.Enabled;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0002D46C File Offset: 0x0002B66C
		public override int GetHashCode()
		{
			return (int)(this.Enabled ^ (global::System.UriIdnScope)127);
		}

		// Token: 0x040004CB RID: 1227
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040004CC RID: 1228
		private static ConfigurationProperty enabled_prop = new ConfigurationProperty("enabled", typeof(global::System.UriIdnScope), global::System.UriIdnScope.None, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
