using System;
using System.Configuration;
using System.Net.Cache;
using System.Xml;

namespace System.Net.Configuration
{
	/// <summary>Represents the default FTP cache policy for network resources. This class cannot be inherited.</summary>
	// Token: 0x020002D4 RID: 724
	public sealed class FtpCachePolicyElement : ConfigurationElement
	{
		// Token: 0x060018D5 RID: 6357 RVA: 0x00044874 File Offset: 0x00042A74
		static FtpCachePolicyElement()
		{
			FtpCachePolicyElement.properties.Add(FtpCachePolicyElement.policyLevelProp);
		}

		/// <summary>Gets or sets FTP caching behavior for the local machine.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> value that specifies the cache behavior.</returns>
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x000448BC File Offset: 0x00042ABC
		// (set) Token: 0x060018D7 RID: 6359 RVA: 0x000448D0 File Offset: 0x00042AD0
		[ConfigurationProperty("policyLevel", DefaultValue = "Default")]
		public global::System.Net.Cache.RequestCacheLevel PolicyLevel
		{
			get
			{
				return (global::System.Net.Cache.RequestCacheLevel)((int)base[FtpCachePolicyElement.policyLevelProp]);
			}
			set
			{
				base[FtpCachePolicyElement.policyLevelProp] = value;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x000448E4 File Offset: 0x00042AE4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FtpCachePolicyElement.properties;
			}
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x000448EC File Offset: 0x00042AEC
		[global::System.MonoTODO]
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x000448F4 File Offset: 0x00042AF4
		[global::System.MonoTODO]
		protected override void Reset(ConfigurationElement parentElement)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000FC4 RID: 4036
		private static ConfigurationProperty policyLevelProp = new ConfigurationProperty("policyLevel", typeof(global::System.Net.Cache.RequestCacheLevel), global::System.Net.Cache.RequestCacheLevel.Default);

		// Token: 0x04000FC5 RID: 4037
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
