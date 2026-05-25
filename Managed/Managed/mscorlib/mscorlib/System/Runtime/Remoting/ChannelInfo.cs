using System;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting
{
	// Token: 0x0200044A RID: 1098
	[Serializable]
	internal class ChannelInfo : IChannelInfo
	{
		// Token: 0x06002E44 RID: 11844 RVA: 0x00099B78 File Offset: 0x00097D78
		public ChannelInfo()
		{
			this.channelData = ChannelServices.GetCurrentChannelInfo();
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x00099B8C File Offset: 0x00097D8C
		public ChannelInfo(object remoteChannelData)
		{
			this.channelData = new object[] { remoteChannelData };
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x00099BA4 File Offset: 0x00097DA4
		// (set) Token: 0x06002E47 RID: 11847 RVA: 0x00099BAC File Offset: 0x00097DAC
		public object[] ChannelData
		{
			get
			{
				return this.channelData;
			}
			set
			{
				this.channelData = value;
			}
		}

		// Token: 0x040013CF RID: 5071
		private object[] channelData;
	}
}
