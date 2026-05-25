using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;

namespace System.Runtime.Remoting
{
	// Token: 0x0200042D RID: 1069
	internal abstract class ServerIdentity : Identity
	{
		// Token: 0x06002D88 RID: 11656 RVA: 0x00097AEC File Offset: 0x00095CEC
		public ServerIdentity(string objectUri, Context context, Type objectType)
			: base(objectUri)
		{
			this._objectType = objectType;
			this._context = context;
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002D89 RID: 11657 RVA: 0x00097B04 File Offset: 0x00095D04
		public Type ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x00097B0C File Offset: 0x00095D0C
		public void StartTrackingLifetime(ILease lease)
		{
			if (lease != null && lease.CurrentState == LeaseState.Null)
			{
				lease = null;
			}
			if (lease != null)
			{
				if (!(lease is Lease))
				{
					lease = new Lease();
				}
				this._lease = (Lease)lease;
				LifetimeServices.TrackLifetime(this);
			}
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x00097B58 File Offset: 0x00095D58
		public virtual void OnLifetimeExpired()
		{
			this.DisposeServerObject();
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x00097B60 File Offset: 0x00095D60
		public override ObjRef CreateObjRef(Type requestedType)
		{
			if (this._objRef != null)
			{
				this._objRef.UpdateChannelInfo();
				return this._objRef;
			}
			if (requestedType == null)
			{
				requestedType = this._objectType;
			}
			this._objRef = new ObjRef();
			this._objRef.TypeInfo = new TypeInfo(requestedType);
			this._objRef.URI = this._objectUri;
			if (this._envoySink != null && !(this._envoySink is EnvoyTerminatorSink))
			{
				this._objRef.EnvoyInfo = new EnvoyInfo(this._envoySink);
			}
			return this._objRef;
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x00097BFC File Offset: 0x00095DFC
		public void AttachServerObject(MarshalByRefObject serverObject, Context context)
		{
			this.DisposeServerObject();
			this._context = context;
			this._serverObject = serverObject;
			if (RemotingServices.IsTransparentProxy(serverObject))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(serverObject);
				if (realProxy.ObjectIdentity == null)
				{
					realProxy.ObjectIdentity = this;
				}
			}
			else
			{
				if (this._objectType.IsContextful)
				{
					this._envoySink = context.CreateEnvoySink(serverObject);
				}
				this._serverObject.ObjectIdentity = this;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x00097C70 File Offset: 0x00095E70
		public Lease Lease
		{
			get
			{
				return this._lease;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002D8F RID: 11663 RVA: 0x00097C78 File Offset: 0x00095E78
		// (set) Token: 0x06002D90 RID: 11664 RVA: 0x00097C80 File Offset: 0x00095E80
		public Context Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x06002D91 RID: 11665
		public abstract IMessage SyncObjectProcessMessage(IMessage msg);

		// Token: 0x06002D92 RID: 11666
		public abstract IMessageCtrl AsyncObjectProcessMessage(IMessage msg, IMessageSink replySink);

		// Token: 0x06002D93 RID: 11667 RVA: 0x00097C8C File Offset: 0x00095E8C
		protected void DisposeServerObject()
		{
			if (this._serverObject != null)
			{
				MarshalByRefObject serverObject = this._serverObject;
				this._serverObject.ObjectIdentity = null;
				this._serverObject = null;
				this._serverSink = null;
				TrackingServices.NotifyDisconnectedObject(serverObject);
			}
		}

		// Token: 0x0400139C RID: 5020
		protected Type _objectType;

		// Token: 0x0400139D RID: 5021
		protected MarshalByRefObject _serverObject;

		// Token: 0x0400139E RID: 5022
		protected IMessageSink _serverSink;

		// Token: 0x0400139F RID: 5023
		protected Context _context;

		// Token: 0x040013A0 RID: 5024
		protected Lease _lease;
	}
}
