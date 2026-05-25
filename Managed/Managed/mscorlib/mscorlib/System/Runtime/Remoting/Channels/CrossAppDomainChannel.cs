using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000469 RID: 1129
	[Serializable]
	internal class CrossAppDomainChannel : IChannel, IChannelReceiver, IChannelSender
	{
		// Token: 0x06002EBC RID: 11964 RVA: 0x0009AD14 File Offset: 0x00098F14
		internal static void RegisterCrossAppDomainChannel()
		{
			object obj = CrossAppDomainChannel.s_lock;
			lock (obj)
			{
				CrossAppDomainChannel crossAppDomainChannel = new CrossAppDomainChannel();
				ChannelServices.RegisterChannel(crossAppDomainChannel);
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x0009AD64 File Offset: 0x00098F64
		public virtual string ChannelName
		{
			get
			{
				return "MONOCAD";
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002EBE RID: 11966 RVA: 0x0009AD6C File Offset: 0x00098F6C
		public virtual int ChannelPriority
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x0009AD70 File Offset: 0x00098F70
		public string Parse(string url, out string objectURI)
		{
			objectURI = url;
			return null;
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002EC0 RID: 11968 RVA: 0x0009AD78 File Offset: 0x00098F78
		public virtual object ChannelData
		{
			get
			{
				return new CrossAppDomainData(Thread.GetDomainID());
			}
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x0009AD84 File Offset: 0x00098F84
		public virtual string[] GetUrlsForUri(string objectURI)
		{
			throw new NotSupportedException("CrossAppdomain channel dont support UrlsForUri");
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x0009AD90 File Offset: 0x00098F90
		public virtual void StartListening(object data)
		{
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x0009AD94 File Offset: 0x00098F94
		public virtual void StopListening(object data)
		{
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x0009AD98 File Offset: 0x00098F98
		public virtual IMessageSink CreateMessageSink(string url, object data, out string uri)
		{
			uri = null;
			if (data != null)
			{
				CrossAppDomainData crossAppDomainData = data as CrossAppDomainData;
				if (crossAppDomainData != null && crossAppDomainData.ProcessID == RemotingConfiguration.ProcessId)
				{
					return CrossAppDomainSink.GetSink(crossAppDomainData.DomainID);
				}
			}
			if (url != null && url.StartsWith("MONOCAD"))
			{
				throw new NotSupportedException("Can't create a named channel via crossappdomain");
			}
			return null;
		}

		// Token: 0x040013E8 RID: 5096
		private const string _strName = "MONOCAD";

		// Token: 0x040013E9 RID: 5097
		private const string _strBaseURI = "MONOCADURI";

		// Token: 0x040013EA RID: 5098
		private static object s_lock = new object();
	}
}
