using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000491 RID: 1169
	internal class ClientContextTerminatorSink : IMessageSink
	{
		// Token: 0x06002FA3 RID: 12195 RVA: 0x0009D7A4 File Offset: 0x0009B9A4
		public ClientContextTerminatorSink(Context ctx)
		{
			this._context = ctx;
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x0009D7B4 File Offset: 0x0009B9B4
		public IMessage SyncProcessMessage(IMessage msg)
		{
			Context.NotifyGlobalDynamicSinks(true, msg, true, false);
			this._context.NotifyDynamicSinks(true, msg, true, false);
			IMessage message;
			if (msg is IConstructionCallMessage)
			{
				message = ActivationServices.RemoteActivate((IConstructionCallMessage)msg);
			}
			else
			{
				Identity messageTargetIdentity = RemotingServices.GetMessageTargetIdentity(msg);
				message = messageTargetIdentity.ChannelSink.SyncProcessMessage(msg);
			}
			Context.NotifyGlobalDynamicSinks(false, msg, true, false);
			this._context.NotifyDynamicSinks(false, msg, true, false);
			return message;
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x0009D824 File Offset: 0x0009BA24
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._context.HasDynamicSinks || Context.HasGlobalDynamicSinks)
			{
				Context.NotifyGlobalDynamicSinks(true, msg, true, true);
				this._context.NotifyDynamicSinks(true, msg, true, true);
				if (replySink != null)
				{
					replySink = new ClientContextReplySink(this._context, replySink);
				}
			}
			Identity messageTargetIdentity = RemotingServices.GetMessageTargetIdentity(msg);
			IMessageCtrl messageCtrl = messageTargetIdentity.ChannelSink.AsyncProcessMessage(msg, replySink);
			if (replySink == null && (this._context.HasDynamicSinks || Context.HasGlobalDynamicSinks))
			{
				Context.NotifyGlobalDynamicSinks(false, msg, true, true);
				this._context.NotifyDynamicSinks(false, msg, true, true);
			}
			return messageCtrl;
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x0009D8C8 File Offset: 0x0009BAC8
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04001449 RID: 5193
		private Context _context;
	}
}
