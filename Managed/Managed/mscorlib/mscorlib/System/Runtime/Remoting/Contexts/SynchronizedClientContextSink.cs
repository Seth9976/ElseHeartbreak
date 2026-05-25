using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000481 RID: 1153
	internal class SynchronizedClientContextSink : IMessageSink
	{
		// Token: 0x06002F32 RID: 12082 RVA: 0x0009C64C File Offset: 0x0009A84C
		public SynchronizedClientContextSink(IMessageSink next, SynchronizationAttribute att)
		{
			this._att = att;
			this._next = next;
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x0009C664 File Offset: 0x0009A864
		public IMessageSink NextSink
		{
			get
			{
				return this._next;
			}
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x0009C66C File Offset: 0x0009A86C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._att.IsReEntrant)
			{
				this._att.ReleaseLock();
				replySink = new SynchronizedContextReplySink(replySink, this._att, true);
			}
			return this._next.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x0009C6B0 File Offset: 0x0009A8B0
		public IMessage SyncProcessMessage(IMessage msg)
		{
			if (this._att.IsReEntrant)
			{
				this._att.ReleaseLock();
			}
			IMessage message;
			try
			{
				message = this._next.SyncProcessMessage(msg);
			}
			finally
			{
				if (this._att.IsReEntrant)
				{
					this._att.AcquireLock();
				}
			}
			return message;
		}

		// Token: 0x04001413 RID: 5139
		private IMessageSink _next;

		// Token: 0x04001414 RID: 5140
		private SynchronizationAttribute _att;
	}
}
