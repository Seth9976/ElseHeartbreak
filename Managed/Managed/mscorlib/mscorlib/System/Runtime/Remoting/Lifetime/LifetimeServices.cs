using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	/// <summary>Controls the.NET remoting lifetime services.</summary>
	// Token: 0x0200048B RID: 1163
	[ComVisible(true)]
	public sealed class LifetimeServices
	{
		/// <summary>Gets or sets the time interval between each activation of the lease manager to clean up expired leases.</summary>
		/// <returns>The default amount of time the lease manager sleeps after checking for expired leases.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002F74 RID: 12148 RVA: 0x0009D1F8 File Offset: 0x0009B3F8
		// (set) Token: 0x06002F75 RID: 12149 RVA: 0x0009D200 File Offset: 0x0009B400
		public static TimeSpan LeaseManagerPollTime
		{
			get
			{
				return LifetimeServices._leaseManagerPollTime;
			}
			set
			{
				LifetimeServices._leaseManagerPollTime = value;
				LifetimeServices._leaseManager.SetPollTime(value);
			}
		}

		/// <summary>Gets or sets the initial lease time span for an <see cref="T:System.AppDomain" />.</summary>
		/// <returns>The initial lease <see cref="T:System.TimeSpan" /> for objects that can have leases in the <see cref="T:System.AppDomain" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002F76 RID: 12150 RVA: 0x0009D214 File Offset: 0x0009B414
		// (set) Token: 0x06002F77 RID: 12151 RVA: 0x0009D21C File Offset: 0x0009B41C
		public static TimeSpan LeaseTime
		{
			get
			{
				return LifetimeServices._leaseTime;
			}
			set
			{
				LifetimeServices._leaseTime = value;
			}
		}

		/// <summary>Gets or sets the amount of time by which the lease is extended every time a call comes in on the server object.</summary>
		/// <returns>The <see cref="T:System.TimeSpan" /> by which a lifetime lease in the current <see cref="T:System.AppDomain" /> is extended after each call.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002F78 RID: 12152 RVA: 0x0009D224 File Offset: 0x0009B424
		// (set) Token: 0x06002F79 RID: 12153 RVA: 0x0009D22C File Offset: 0x0009B42C
		public static TimeSpan RenewOnCallTime
		{
			get
			{
				return LifetimeServices._renewOnCallTime;
			}
			set
			{
				LifetimeServices._renewOnCallTime = value;
			}
		}

		/// <summary>Gets or sets the amount of time the lease manager waits for a sponsor to return with a lease renewal time.</summary>
		/// <returns>The initial sponsorship time-out.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002F7A RID: 12154 RVA: 0x0009D234 File Offset: 0x0009B434
		// (set) Token: 0x06002F7B RID: 12155 RVA: 0x0009D23C File Offset: 0x0009B43C
		public static TimeSpan SponsorshipTimeout
		{
			get
			{
				return LifetimeServices._sponsorshipTimeout;
			}
			set
			{
				LifetimeServices._sponsorshipTimeout = value;
			}
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x0009D244 File Offset: 0x0009B444
		internal static void TrackLifetime(ServerIdentity identity)
		{
			LifetimeServices._leaseManager.TrackLifetime(identity);
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x0009D254 File Offset: 0x0009B454
		internal static void StopTrackingLifetime(ServerIdentity identity)
		{
			LifetimeServices._leaseManager.StopTrackingLifetime(identity);
		}

		// Token: 0x0400142D RID: 5165
		private static TimeSpan _leaseManagerPollTime = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400142E RID: 5166
		private static TimeSpan _leaseTime = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400142F RID: 5167
		private static TimeSpan _renewOnCallTime = TimeSpan.FromMinutes(2.0);

		// Token: 0x04001430 RID: 5168
		private static TimeSpan _sponsorshipTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x04001431 RID: 5169
		private static LeaseManager _leaseManager = new LeaseManager();
	}
}
