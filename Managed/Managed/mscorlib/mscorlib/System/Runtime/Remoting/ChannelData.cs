using System;
using System.Collections;

namespace System.Runtime.Remoting
{
	// Token: 0x02000425 RID: 1061
	internal class ChannelData
	{
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002D37 RID: 11575 RVA: 0x000962FC File Offset: 0x000944FC
		internal ArrayList ServerProviders
		{
			get
			{
				if (this._serverProviders == null)
				{
					this._serverProviders = new ArrayList();
				}
				return this._serverProviders;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06002D38 RID: 11576 RVA: 0x0009631C File Offset: 0x0009451C
		public ArrayList ClientProviders
		{
			get
			{
				if (this._clientProviders == null)
				{
					this._clientProviders = new ArrayList();
				}
				return this._clientProviders;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x0009633C File Offset: 0x0009453C
		public Hashtable CustomProperties
		{
			get
			{
				if (this._customProperties == null)
				{
					this._customProperties = new Hashtable();
				}
				return this._customProperties;
			}
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x0009635C File Offset: 0x0009455C
		public void CopyFrom(ChannelData other)
		{
			if (this.Ref == null)
			{
				this.Ref = other.Ref;
			}
			if (this.Id == null)
			{
				this.Id = other.Id;
			}
			if (this.Type == null)
			{
				this.Type = other.Type;
			}
			if (this.DelayLoadAsClientChannel == null)
			{
				this.DelayLoadAsClientChannel = other.DelayLoadAsClientChannel;
			}
			if (other._customProperties != null)
			{
				foreach (object obj in other._customProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (!this.CustomProperties.ContainsKey(dictionaryEntry.Key))
					{
						this.CustomProperties[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
			}
			if (this._serverProviders == null && other._serverProviders != null)
			{
				foreach (object obj2 in other._serverProviders)
				{
					ProviderData providerData = (ProviderData)obj2;
					ProviderData providerData2 = new ProviderData();
					providerData2.CopyFrom(providerData);
					this.ServerProviders.Add(providerData2);
				}
			}
			if (this._clientProviders == null && other._clientProviders != null)
			{
				foreach (object obj3 in other._clientProviders)
				{
					ProviderData providerData3 = (ProviderData)obj3;
					ProviderData providerData4 = new ProviderData();
					providerData4.CopyFrom(providerData3);
					this.ClientProviders.Add(providerData4);
				}
			}
		}

		// Token: 0x04001386 RID: 4998
		internal string Ref;

		// Token: 0x04001387 RID: 4999
		internal string Type;

		// Token: 0x04001388 RID: 5000
		internal string Id;

		// Token: 0x04001389 RID: 5001
		internal string DelayLoadAsClientChannel;

		// Token: 0x0400138A RID: 5002
		private ArrayList _serverProviders = new ArrayList();

		// Token: 0x0400138B RID: 5003
		private ArrayList _clientProviders = new ArrayList();

		// Token: 0x0400138C RID: 5004
		private Hashtable _customProperties = new Hashtable();
	}
}
