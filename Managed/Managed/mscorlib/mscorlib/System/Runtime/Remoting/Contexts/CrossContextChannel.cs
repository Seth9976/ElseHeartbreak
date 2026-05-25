using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000474 RID: 1140
	internal class CrossContextChannel : IMessageSink
	{
		// Token: 0x06002F0A RID: 12042 RVA: 0x0009C02C File Offset: 0x0009A22C
		public IMessage SyncProcessMessage(IMessage msg)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			Context context = null;
			if (Thread.CurrentContext != serverIdentity.Context)
			{
				context = Context.SwitchToContext(serverIdentity.Context);
			}
			IMessage message;
			try
			{
				Context.NotifyGlobalDynamicSinks(true, msg, false, false);
				Thread.CurrentContext.NotifyDynamicSinks(true, msg, false, false);
				message = serverIdentity.Context.GetServerContextSinkChain().SyncProcessMessage(msg);
				Context.NotifyGlobalDynamicSinks(false, msg, false, false);
				Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
			}
			catch (Exception ex)
			{
				message = new ReturnMessage(ex, (IMethodCallMessage)msg);
			}
			finally
			{
				if (context != null)
				{
					Context.SwitchToContext(context);
				}
			}
			return message;
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x0009C100 File Offset: 0x0009A300
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			ServerIdentity serverIdentity = (ServerIdentity)RemotingServices.GetMessageTargetIdentity(msg);
			Context context = null;
			if (Thread.CurrentContext != serverIdentity.Context)
			{
				context = Context.SwitchToContext(serverIdentity.Context);
			}
			IMessageCtrl messageCtrl2;
			try
			{
				Context.NotifyGlobalDynamicSinks(true, msg, false, true);
				Thread.CurrentContext.NotifyDynamicSinks(true, msg, false, false);
				if (replySink != null)
				{
					replySink = new CrossContextChannel.ContextRestoreSink(replySink, context, msg);
				}
				IMessageCtrl messageCtrl = serverIdentity.AsyncObjectProcessMessage(msg, replySink);
				if (replySink == null)
				{
					Context.NotifyGlobalDynamicSinks(false, msg, false, false);
					Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
				}
				messageCtrl2 = messageCtrl;
			}
			catch (Exception ex)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(new ReturnMessage(ex, (IMethodCallMessage)msg));
				}
				messageCtrl2 = null;
			}
			finally
			{
				if (context != null)
				{
					Context.SwitchToContext(context);
				}
			}
			return messageCtrl2;
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002F0C RID: 12044 RVA: 0x0009C1FC File Offset: 0x0009A3FC
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x02000475 RID: 1141
		private class ContextRestoreSink : IMessageSink
		{
			// Token: 0x06002F0D RID: 12045 RVA: 0x0009C200 File Offset: 0x0009A400
			public ContextRestoreSink(IMessageSink next, Context context, IMessage call)
			{
				this._next = next;
				this._context = context;
				this._call = call;
			}

			// Token: 0x06002F0E RID: 12046 RVA: 0x0009C220 File Offset: 0x0009A420
			public IMessage SyncProcessMessage(IMessage msg)
			{
				IMessage message;
				try
				{
					Context.NotifyGlobalDynamicSinks(false, msg, false, false);
					Thread.CurrentContext.NotifyDynamicSinks(false, msg, false, false);
					message = this._next.SyncProcessMessage(msg);
				}
				catch (Exception ex)
				{
					message = new ReturnMessage(ex, (IMethodCallMessage)this._call);
				}
				finally
				{
					if (this._context != null)
					{
						Context.SwitchToContext(this._context);
					}
				}
				return message;
			}

			// Token: 0x06002F0F RID: 12047 RVA: 0x0009C2C8 File Offset: 0x0009A4C8
			public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000873 RID: 2163
			// (get) Token: 0x06002F10 RID: 12048 RVA: 0x0009C2D0 File Offset: 0x0009A4D0
			public IMessageSink NextSink
			{
				get
				{
					return this._next;
				}
			}

			// Token: 0x04001406 RID: 5126
			private IMessageSink _next;

			// Token: 0x04001407 RID: 5127
			private Context _context;

			// Token: 0x04001408 RID: 5128
			private IMessage _call;
		}
	}
}
