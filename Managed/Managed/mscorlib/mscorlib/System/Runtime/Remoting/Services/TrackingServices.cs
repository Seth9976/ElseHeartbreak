using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Services
{
	/// <summary>Provides a way to register, unregister, and obtain a list of tracking handlers.</summary>
	// Token: 0x020004ED RID: 1261
	[ComVisible(true)]
	public class TrackingServices
	{
		/// <summary>Registers a new tracking handler with the <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.</summary>
		/// <param name="handler">The tracking handler to register. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="handler" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The handler that is indicated in the <paramref name="handler" /> parameter is already registered with <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600329B RID: 12955 RVA: 0x000A4214 File Offset: 0x000A2414
		public static void RegisterTrackingHandler(ITrackingHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			object syncRoot = TrackingServices._handlers.SyncRoot;
			lock (syncRoot)
			{
				if (TrackingServices._handlers.IndexOf(handler) != -1)
				{
					throw new RemotingException("handler already registered");
				}
				TrackingServices._handlers.Add(handler);
			}
		}

		/// <summary>Unregisters the specified tracking handler from <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.</summary>
		/// <param name="handler">The handler to unregister. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="handler" /> is null. </exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The handler that is indicated in the <paramref name="handler" /> parameter is not registered with <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600329C RID: 12956 RVA: 0x000A4294 File Offset: 0x000A2494
		public static void UnregisterTrackingHandler(ITrackingHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			object syncRoot = TrackingServices._handlers.SyncRoot;
			lock (syncRoot)
			{
				int num = TrackingServices._handlers.IndexOf(handler);
				if (num == -1)
				{
					throw new RemotingException("handler is not registered");
				}
				TrackingServices._handlers.RemoveAt(num);
			}
		}

		/// <summary>Gets an array of the tracking handlers that are currently registered with <see cref="T:System.Runtime.Remoting.Services.TrackingServices" /> in the current <see cref="T:System.AppDomain" />.</summary>
		/// <returns>An array of the tracking handlers that are currently registered with <see cref="T:System.Runtime.Remoting.Services.TrackingServices" /> in the current <see cref="T:System.AppDomain" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x000A4314 File Offset: 0x000A2514
		public static ITrackingHandler[] RegisteredHandlers
		{
			get
			{
				object syncRoot = TrackingServices._handlers.SyncRoot;
				ITrackingHandler[] array;
				lock (syncRoot)
				{
					if (TrackingServices._handlers.Count == 0)
					{
						array = new ITrackingHandler[0];
					}
					else
					{
						array = (ITrackingHandler[])TrackingServices._handlers.ToArray(typeof(ITrackingHandler));
					}
				}
				return array;
			}
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x000A4398 File Offset: 0x000A2598
		internal static void NotifyMarshaledObject(object obj, ObjRef or)
		{
			object syncRoot = TrackingServices._handlers.SyncRoot;
			ITrackingHandler[] array;
			lock (syncRoot)
			{
				if (TrackingServices._handlers.Count == 0)
				{
					return;
				}
				array = (ITrackingHandler[])TrackingServices._handlers.ToArray(typeof(ITrackingHandler));
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i].MarshaledObject(obj, or);
			}
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x000A442C File Offset: 0x000A262C
		internal static void NotifyUnmarshaledObject(object obj, ObjRef or)
		{
			object syncRoot = TrackingServices._handlers.SyncRoot;
			ITrackingHandler[] array;
			lock (syncRoot)
			{
				if (TrackingServices._handlers.Count == 0)
				{
					return;
				}
				array = (ITrackingHandler[])TrackingServices._handlers.ToArray(typeof(ITrackingHandler));
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i].UnmarshaledObject(obj, or);
			}
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x000A44C0 File Offset: 0x000A26C0
		internal static void NotifyDisconnectedObject(object obj)
		{
			object syncRoot = TrackingServices._handlers.SyncRoot;
			ITrackingHandler[] array;
			lock (syncRoot)
			{
				if (TrackingServices._handlers.Count == 0)
				{
					return;
				}
				array = (ITrackingHandler[])TrackingServices._handlers.ToArray(typeof(ITrackingHandler));
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i].DisconnectedObject(obj);
			}
		}

		// Token: 0x04001529 RID: 5417
		private static ArrayList _handlers = new ArrayList();
	}
}
