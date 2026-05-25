using System;
using System.Collections;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000487 RID: 1159
	internal class Lease : MarshalByRefObject, ILease
	{
		// Token: 0x06002F55 RID: 12117 RVA: 0x0009CA34 File Offset: 0x0009AC34
		public Lease()
		{
			this._currentState = LeaseState.Initial;
			this._initialLeaseTime = LifetimeServices.LeaseTime;
			this._renewOnCallTime = LifetimeServices.RenewOnCallTime;
			this._sponsorshipTimeout = LifetimeServices.SponsorshipTimeout;
			this._leaseExpireTime = DateTime.Now + this._initialLeaseTime;
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x0009CA88 File Offset: 0x0009AC88
		public TimeSpan CurrentLeaseTime
		{
			get
			{
				return this._leaseExpireTime - DateTime.Now;
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002F57 RID: 12119 RVA: 0x0009CA9C File Offset: 0x0009AC9C
		public LeaseState CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x0009CAA4 File Offset: 0x0009ACA4
		public void Activate()
		{
			this._currentState = LeaseState.Active;
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002F59 RID: 12121 RVA: 0x0009CAB0 File Offset: 0x0009ACB0
		// (set) Token: 0x06002F5A RID: 12122 RVA: 0x0009CAB8 File Offset: 0x0009ACB8
		public TimeSpan InitialLeaseTime
		{
			get
			{
				return this._initialLeaseTime;
			}
			set
			{
				if (this._currentState != LeaseState.Initial)
				{
					throw new RemotingException("InitialLeaseTime property can only be set when the lease is in initial state; state is " + this._currentState + ".");
				}
				this._initialLeaseTime = value;
				this._leaseExpireTime = DateTime.Now + this._initialLeaseTime;
				if (value == TimeSpan.Zero)
				{
					this._currentState = LeaseState.Null;
				}
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002F5B RID: 12123 RVA: 0x0009CB28 File Offset: 0x0009AD28
		// (set) Token: 0x06002F5C RID: 12124 RVA: 0x0009CB30 File Offset: 0x0009AD30
		public TimeSpan RenewOnCallTime
		{
			get
			{
				return this._renewOnCallTime;
			}
			set
			{
				if (this._currentState != LeaseState.Initial)
				{
					throw new RemotingException("RenewOnCallTime property can only be set when the lease is in initial state; state is " + this._currentState + ".");
				}
				this._renewOnCallTime = value;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002F5D RID: 12125 RVA: 0x0009CB68 File Offset: 0x0009AD68
		// (set) Token: 0x06002F5E RID: 12126 RVA: 0x0009CB70 File Offset: 0x0009AD70
		public TimeSpan SponsorshipTimeout
		{
			get
			{
				return this._sponsorshipTimeout;
			}
			set
			{
				if (this._currentState != LeaseState.Initial)
				{
					throw new RemotingException("SponsorshipTimeout property can only be set when the lease is in initial state; state is " + this._currentState + ".");
				}
				this._sponsorshipTimeout = value;
			}
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x0009CBA8 File Offset: 0x0009ADA8
		public void Register(ISponsor obj)
		{
			this.Register(obj, TimeSpan.Zero);
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x0009CBB8 File Offset: 0x0009ADB8
		public void Register(ISponsor obj, TimeSpan renewalTime)
		{
			lock (this)
			{
				if (this._sponsors == null)
				{
					this._sponsors = new ArrayList();
				}
				this._sponsors.Add(obj);
			}
			if (renewalTime != TimeSpan.Zero)
			{
				this.Renew(renewalTime);
			}
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x0009CC30 File Offset: 0x0009AE30
		public TimeSpan Renew(TimeSpan renewalTime)
		{
			DateTime dateTime = DateTime.Now + renewalTime;
			if (dateTime > this._leaseExpireTime)
			{
				this._leaseExpireTime = dateTime;
			}
			return this.CurrentLeaseTime;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x0009CC68 File Offset: 0x0009AE68
		public void Unregister(ISponsor obj)
		{
			lock (this)
			{
				if (this._sponsors != null)
				{
					for (int i = 0; i < this._sponsors.Count; i++)
					{
						if (object.ReferenceEquals(this._sponsors[i], obj))
						{
							this._sponsors.RemoveAt(i);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x0009CCFC File Offset: 0x0009AEFC
		internal void UpdateState()
		{
			if (this._currentState != LeaseState.Active)
			{
				return;
			}
			if (this.CurrentLeaseTime > TimeSpan.Zero)
			{
				return;
			}
			if (this._sponsors != null)
			{
				this._currentState = LeaseState.Renewing;
				lock (this)
				{
					this._renewingSponsors = new Queue(this._sponsors);
				}
				this.CheckNextSponsor();
			}
			else
			{
				this._currentState = LeaseState.Expired;
			}
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x0009CD94 File Offset: 0x0009AF94
		private void CheckNextSponsor()
		{
			if (this._renewingSponsors.Count == 0)
			{
				this._currentState = LeaseState.Expired;
				this._renewingSponsors = null;
				return;
			}
			ISponsor sponsor = (ISponsor)this._renewingSponsors.Peek();
			this._renewalDelegate = new Lease.RenewalDelegate(sponsor.Renewal);
			IAsyncResult asyncResult = this._renewalDelegate.BeginInvoke(this, null, null);
			ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, new WaitOrTimerCallback(this.ProcessSponsorResponse), asyncResult, this._sponsorshipTimeout, true);
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x0009CE14 File Offset: 0x0009B014
		private void ProcessSponsorResponse(object state, bool timedOut)
		{
			if (!timedOut)
			{
				try
				{
					IAsyncResult asyncResult = (IAsyncResult)state;
					TimeSpan timeSpan = this._renewalDelegate.EndInvoke(asyncResult);
					if (timeSpan != TimeSpan.Zero)
					{
						this.Renew(timeSpan);
						this._currentState = LeaseState.Active;
						this._renewingSponsors = null;
						return;
					}
				}
				catch
				{
				}
			}
			this.Unregister((ISponsor)this._renewingSponsors.Dequeue());
			this.CheckNextSponsor();
		}

		// Token: 0x0400141C RID: 5148
		private DateTime _leaseExpireTime;

		// Token: 0x0400141D RID: 5149
		private LeaseState _currentState;

		// Token: 0x0400141E RID: 5150
		private TimeSpan _initialLeaseTime;

		// Token: 0x0400141F RID: 5151
		private TimeSpan _renewOnCallTime;

		// Token: 0x04001420 RID: 5152
		private TimeSpan _sponsorshipTimeout;

		// Token: 0x04001421 RID: 5153
		private ArrayList _sponsors;

		// Token: 0x04001422 RID: 5154
		private Queue _renewingSponsors;

		// Token: 0x04001423 RID: 5155
		private Lease.RenewalDelegate _renewalDelegate;

		// Token: 0x020006E2 RID: 1762
		// (Invoke) Token: 0x0600437C RID: 17276
		private delegate TimeSpan RenewalDelegate(ILease lease);
	}
}
