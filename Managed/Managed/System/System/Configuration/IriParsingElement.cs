using System;

namespace System.Configuration
{
	/// <summary>Provides the configuration setting for International Resource Identifier (IRI) processing in the <see cref="T:System.Uri" /> class.</summary>
	// Token: 0x020001E4 RID: 484
	public sealed class IriParsingElement : ConfigurationElement
	{
		// Token: 0x060010C3 RID: 4291 RVA: 0x0002D48C File Offset: 0x0002B68C
		static IriParsingElement()
		{
			IriParsingElement.properties.Add(IriParsingElement.enabled_prop);
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.IriParsingElement" /> configuration setting.</summary>
		/// <returns>A Boolean that indicates if International Resource Identifier (IRI) processing is enabled. </returns>
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0002D4C8 File Offset: 0x0002B6C8
		// (set) Token: 0x060010C5 RID: 4293 RVA: 0x0002D4DC File Offset: 0x0002B6DC
		[ConfigurationProperty("enabled", DefaultValue = false, Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public bool Enabled
		{
			get
			{
				return (bool)base[IriParsingElement.enabled_prop];
			}
			set
			{
				base[IriParsingElement.enabled_prop] = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x0002D4F0 File Offset: 0x0002B6F0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return IriParsingElement.properties;
			}
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0002D4F8 File Offset: 0x0002B6F8
		public override bool Equals(object o)
		{
			IriParsingElement iriParsingElement = o as IriParsingElement;
			return iriParsingElement != null && iriParsingElement.Enabled == this.Enabled;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0002D524 File Offset: 0x0002B724
		public override int GetHashCode()
		{
			return Convert.ToInt32(this.Enabled) ^ 127;
		}

		// Token: 0x040004CD RID: 1229
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040004CE RID: 1230
		private static ConfigurationProperty enabled_prop = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
	}
}
