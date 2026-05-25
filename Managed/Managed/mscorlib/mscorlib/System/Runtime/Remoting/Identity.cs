using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting
{
	// Token: 0x0200041C RID: 1052
	internal abstract class Identity
	{
		// Token: 0x06002CBF RID: 11455 RVA: 0x00093F68 File Offset: 0x00092168
		public Identity(string objectUri)
		{
			this._objectUri = objectUri;
		}

		// Token: 0x06002CC0 RID: 11456
		public abstract ObjRef CreateObjRef(Type requestedType);

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x00093F78 File Offset: 0x00092178
		public bool IsFromThisAppDomain
		{
			get
			{
				return this._channelSink == null;
			}
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002CC2 RID: 11458 RVA: 0x00093F84 File Offset: 0x00092184
		// (set) Token: 0x06002CC3 RID: 11459 RVA: 0x00093F8C File Offset: 0x0009218C
		public IMessageSink ChannelSink
		{
			get
			{
				return this._channelSink;
			}
			set
			{
				this._channelSink = value;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x00093F98 File Offset: 0x00092198
		public IMessageSink EnvoySink
		{
			get
			{
				return this._envoySink;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x00093FA0 File Offset: 0x000921A0
		// (set) Token: 0x06002CC6 RID: 11462 RVA: 0x00093FA8 File Offset: 0x000921A8
		public string ObjectUri
		{
			get
			{
				return this._objectUri;
			}
			set
			{
				this._objectUri = value;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x00093FB4 File Offset: 0x000921B4
		public bool IsConnected
		{
			get
			{
				return this._objectUri != null;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x00093FC4 File Offset: 0x000921C4
		// (set) Token: 0x06002CC9 RID: 11465 RVA: 0x00093FCC File Offset: 0x000921CC
		public bool Disposed
		{
			get
			{
				return this._disposed;
			}
			set
			{
				this._disposed = value;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002CCA RID: 11466 RVA: 0x00093FD8 File Offset: 0x000921D8
		public DynamicPropertyCollection ClientDynamicProperties
		{
			get
			{
				if (this._clientDynamicProperties == null)
				{
					this._clientDynamicProperties = new DynamicPropertyCollection();
				}
				return this._clientDynamicProperties;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x00093FF8 File Offset: 0x000921F8
		public DynamicPropertyCollection ServerDynamicProperties
		{
			get
			{
				if (this._serverDynamicProperties == null)
				{
					this._serverDynamicProperties = new DynamicPropertyCollection();
				}
				return this._serverDynamicProperties;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002CCC RID: 11468 RVA: 0x00094018 File Offset: 0x00092218
		public bool HasClientDynamicSinks
		{
			get
			{
				return this._clientDynamicProperties != null && this._clientDynamicProperties.HasProperties;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002CCD RID: 11469 RVA: 0x00094034 File Offset: 0x00092234
		public bool HasServerDynamicSinks
		{
			get
			{
				return this._serverDynamicProperties != null && this._serverDynamicProperties.HasProperties;
			}
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x00094050 File Offset: 0x00092250
		public void NotifyClientDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this._clientDynamicProperties != null && this._clientDynamicProperties.HasProperties)
			{
				this._clientDynamicProperties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x00094080 File Offset: 0x00092280
		public void NotifyServerDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this._serverDynamicProperties != null && this._serverDynamicProperties.HasProperties)
			{
				this._serverDynamicProperties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x0400135C RID: 4956
		protected string _objectUri;

		// Token: 0x0400135D RID: 4957
		protected IMessageSink _channelSink;

		// Token: 0x0400135E RID: 4958
		protected IMessageSink _envoySink;

		// Token: 0x0400135F RID: 4959
		private DynamicPropertyCollection _clientDynamicProperties;

		// Token: 0x04001360 RID: 4960
		private DynamicPropertyCollection _serverDynamicProperties;

		// Token: 0x04001361 RID: 4961
		protected ObjRef _objRef;

		// Token: 0x04001362 RID: 4962
		private bool _disposed;
	}
}
