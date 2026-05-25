using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the maximum length for response headers. This class cannot be inherited.</summary>
	// Token: 0x020002D6 RID: 726
	public sealed class HttpWebRequestElement : ConfigurationElement
	{
		// Token: 0x060018E9 RID: 6377 RVA: 0x00044AA0 File Offset: 0x00042CA0
		static HttpWebRequestElement()
		{
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.maximumErrorResponseLengthProp);
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.maximumResponseHeadersLengthProp);
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.maximumUnauthorizedUploadLengthProp);
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.useUnsafeHeaderParsingProp);
		}

		/// <summary>Gets or sets the maximum allowed length of an error response.</summary>
		/// <returns>A 32-bit signed integer containing the maximum length in kilobytes (1024 bytes) of the error response. The default value is 64.</returns>
		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x00044B74 File Offset: 0x00042D74
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x00044B88 File Offset: 0x00042D88
		[ConfigurationProperty("maximumErrorResponseLength", DefaultValue = "64")]
		public int MaximumErrorResponseLength
		{
			get
			{
				return (int)base[HttpWebRequestElement.maximumErrorResponseLengthProp];
			}
			set
			{
				base[HttpWebRequestElement.maximumErrorResponseLengthProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum allowed length of the response headers.</summary>
		/// <returns>A 32-bit signed integer containing the maximum length in kilobytes (1024 bytes) of the response headers. The default value is 64.</returns>
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x00044B9C File Offset: 0x00042D9C
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x00044BB0 File Offset: 0x00042DB0
		[ConfigurationProperty("maximumResponseHeadersLength", DefaultValue = "64")]
		public int MaximumResponseHeadersLength
		{
			get
			{
				return (int)base[HttpWebRequestElement.maximumResponseHeadersLengthProp];
			}
			set
			{
				base[HttpWebRequestElement.maximumResponseHeadersLengthProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum length of an upload in response to an unauthorized error code.</summary>
		/// <returns>A 32-bit signed integer containing the maximum length (in bytes) of an upload in respons to an unauthorized error code. A value of -1 indicates that no size limit will be imposed on the upload. The default value is -1.</returns>
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x00044BC4 File Offset: 0x00042DC4
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x00044BD8 File Offset: 0x00042DD8
		[ConfigurationProperty("maximumUnauthorizedUploadLength", DefaultValue = "-1")]
		public int MaximumUnauthorizedUploadLength
		{
			get
			{
				return (int)base[HttpWebRequestElement.maximumUnauthorizedUploadLengthProp];
			}
			set
			{
				base[HttpWebRequestElement.maximumUnauthorizedUploadLengthProp] = value;
			}
		}

		/// <summary>Setting this property ignores validation errors that occur during HTTP parsing.</summary>
		/// <returns>Boolean that indicates whether this property has been set. </returns>
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x00044BEC File Offset: 0x00042DEC
		// (set) Token: 0x060018F1 RID: 6385 RVA: 0x00044C00 File Offset: 0x00042E00
		[ConfigurationProperty("useUnsafeHeaderParsing", DefaultValue = "False")]
		public bool UseUnsafeHeaderParsing
		{
			get
			{
				return (bool)base[HttpWebRequestElement.useUnsafeHeaderParsingProp];
			}
			set
			{
				base[HttpWebRequestElement.useUnsafeHeaderParsingProp] = value;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x00044C14 File Offset: 0x00042E14
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpWebRequestElement.properties;
			}
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00044C1C File Offset: 0x00042E1C
		[global::System.MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x04000FCB RID: 4043
		private static ConfigurationProperty maximumErrorResponseLengthProp = new ConfigurationProperty("maximumErrorResponseLength", typeof(int), 64);

		// Token: 0x04000FCC RID: 4044
		private static ConfigurationProperty maximumResponseHeadersLengthProp = new ConfigurationProperty("maximumResponseHeadersLength", typeof(int), 64);

		// Token: 0x04000FCD RID: 4045
		private static ConfigurationProperty maximumUnauthorizedUploadLengthProp = new ConfigurationProperty("maximumUnauthorizedUploadLength", typeof(int), -1);

		// Token: 0x04000FCE RID: 4046
		private static ConfigurationProperty useUnsafeHeaderParsingProp = new ConfigurationProperty("useUnsafeHeaderParsing", typeof(bool), false);

		// Token: 0x04000FCF RID: 4047
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
