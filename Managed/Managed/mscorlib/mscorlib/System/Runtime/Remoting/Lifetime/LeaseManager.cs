using System;
using System.Collections;
using System.Threading;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000488 RID: 1160
	internal class LeaseManager
	{
		// Token: 0x06002F67 RID: 12135 RVA: 0x0009CEC0 File Offset: 0x0009B0C0
		public void SetPollTime(TimeSpan timeSpan)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				if (this._timer != null)
				{
					this._timer.Change(timeSpan, timeSpan);
				}
			}
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x0009CF20 File Offset: 0x0009B120
		public void TrackLifetime(ServerIdentity identity)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				identity.Lease.Activate();
				this._objects.Add(identity);
				if (this._timer == null)
				{
					this.StartManager();
				}
			}
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x0009CF90 File Offset: 0x0009B190
		public void StopTrackingLifetime(ServerIdentity identity)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				this._objects.Remove(identity);
			}
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x0009CFE4 File Offset: 0x0009B1E4
		public void StartManager()
		{
			this._timer = new Timer(new TimerCallback(this.ManageLeases), null, LifetimeServices.LeaseManagerPollTime, LifetimeServices.LeaseManagerPollTime);
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x0009D014 File Offset: 0x0009B214
		public void StopManager()
		{
			Timer timer = this._timer;
			this._timer = null;
			timer.Dispose();
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x0009D038 File Offset: 0x0009B238
		public void ManageLeases(object state)
		{
			object syncRoot = this._objects.SyncRoot;
			lock (syncRoot)
			{
				int i = 0;
				while (i < this._objects.Count)
				{
					ServerIdentity serverIdentity = (ServerIdentity)this._objects[i];
					serverIdentity.Lease.UpdateState();
					if (serverIdentity.Lease.CurrentState == LeaseState.Expired)
					{
						this._objects.RemoveAt(i);
						serverIdentity.OnLifetimeExpired();
					}
					else
					{
						i++;
					}
				}
				if (this._objects.Count == 0)
				{
					this.StopManager();
				}
			}
		}

		// Token: 0x04001424 RID: 5156
		private ArrayList _objects = new ArrayList();

		// Token: 0x04001425 RID: 5157
		private Timer _timer;
	}
}
