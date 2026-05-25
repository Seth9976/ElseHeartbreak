using System;
using System.Collections;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200045E RID: 1118
	internal class ServerDispatchSinkProvider : IServerChannelSinkProvider, IServerFormatterSinkProvider
	{
		// Token: 0x06002E91 RID: 11921 RVA: 0x0009AAE4 File Offset: 0x00098CE4
		public ServerDispatchSinkProvider()
		{
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x0009AAEC File Offset: 0x00098CEC
		public ServerDispatchSinkProvider(IDictionary properties, ICollection providerData)
		{
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x0009AAF4 File Offset: 0x00098CF4
		// (set) Token: 0x06002E94 RID: 11924 RVA: 0x0009AAF8 File Offset: 0x00098CF8
		public IServerChannelSinkProvider Next
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x0009AB00 File Offset: 0x00098D00
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			return new ServerDispatchSink();
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x0009AB08 File Offset: 0x00098D08
		public void GetChannelData(IChannelDataStore channelData)
		{
		}
	}
}
