using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004B8 RID: 1208
	internal class ServerObjectReplySink : IMessageSink
	{
		// Token: 0x06003103 RID: 12547 RVA: 0x000A0FAC File Offset: 0x0009F1AC
		public ServerObjectReplySink(ServerIdentity identity, IMessageSink replySink)
		{
			this._replySink = replySink;
			this._identity = identity;
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x000A0FC4 File Offset: 0x0009F1C4
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this._identity.NotifyServerDynamicSinks(false, msg, true, true);
			return this._replySink.SyncProcessMessage(msg);
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000A0FE4 File Offset: 0x0009F1E4
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000A0FEC File Offset: 0x0009F1EC
		public IMessageSink NextSink
		{
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x040014BD RID: 5309
		private IMessageSink _replySink;

		// Token: 0x040014BE RID: 5310
		private ServerIdentity _identity;
	}
}
