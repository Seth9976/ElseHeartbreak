using System;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x0200042E RID: 1070
	internal class ClientActivatedIdentity : ServerIdentity
	{
		// Token: 0x06002D94 RID: 11668 RVA: 0x00097CCC File Offset: 0x00095ECC
		public ClientActivatedIdentity(string objectUri, Type objectType)
			: base(objectUri, null, objectType)
		{
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x00097CD8 File Offset: 0x00095ED8
		public MarshalByRefObject GetServerObject()
		{
			return this._serverObject;
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x00097CE0 File Offset: 0x00095EE0
		public MarshalByRefObject GetClientProxy()
		{
			return this._targetThis;
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x00097CE8 File Offset: 0x00095EE8
		public void SetClientProxy(MarshalByRefObject obj)
		{
			this._targetThis = obj;
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x00097CF4 File Offset: 0x00095EF4
		public override void OnLifetimeExpired()
		{
			base.OnLifetimeExpired();
			RemotingServices.DisposeIdentity(this);
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x00097D04 File Offset: 0x00095F04
		public override IMessage SyncObjectProcessMessage(IMessage msg)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain((!flag) ? this._serverObject : this._targetThis, flag);
			}
			return this._serverSink.SyncProcessMessage(msg);
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x00097D60 File Offset: 0x00095F60
		public override IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink)
		{
			if (this._serverSink == null)
			{
				bool flag = this._targetThis != null;
				this._serverSink = this._context.CreateServerObjectSinkChain((!flag) ? this._serverObject : this._targetThis, flag);
			}
			return this._serverSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x040013A1 RID: 5025
		private MarshalByRefObject _targetThis;
	}
}
