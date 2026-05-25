using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004B7 RID: 1207
	internal class ServerObjectTerminatorSink : IMessageSink
	{
		// Token: 0x060030FF RID: 12543 RVA: 0x000A0EFC File Offset: 0x0009F0FC
		public ServerObjectTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x000A0F0C File Offset: 0x0009F10C
		public IMessage SyncProcessMessage(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			serverIdentity.NotifyServerDynamicSinks(true, msg, false, false);
			IMessage message = this._nextSink.SyncProcessMessage(msg);
			serverIdentity.NotifyServerDynamicSinks(false, msg, false, false);
			return message;
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000A0F48 File Offset: 0x0009F148
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			if (serverIdentity.HasServerDynamicSinks)
			{
				serverIdentity.NotifyServerDynamicSinks(true, msg, false, true);
				if (replySink != null)
				{
					replySink = new ServerObjectReplySink(serverIdentity, replySink);
				}
			}
			IMessageCtrl messageCtrl = this._nextSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null)
			{
				serverIdentity.NotifyServerDynamicSinks(false, msg, true, true);
			}
			return messageCtrl;
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x000A0FA4 File Offset: 0x0009F1A4
		public IMessageSink NextSink
		{
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x040014BC RID: 5308
		private IMessageSink _nextSink;
	}
}
