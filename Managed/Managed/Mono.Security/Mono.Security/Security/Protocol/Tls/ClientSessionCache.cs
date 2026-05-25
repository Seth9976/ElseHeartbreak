using System;
using System.Collections;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000086 RID: 134
	internal class ClientSessionCache
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x0001D788 File Offset: 0x0001B988
		public static void Add(string host, byte[] id)
		{
			object obj = ClientSessionCache.locker;
			lock (obj)
			{
				string text = BitConverter.ToString(id);
				ClientSessionInfo clientSessionInfo = (ClientSessionInfo)ClientSessionCache.cache[text];
				if (clientSessionInfo == null)
				{
					ClientSessionCache.cache.Add(text, new ClientSessionInfo(host, id));
				}
				else if (clientSessionInfo.HostName == host)
				{
					clientSessionInfo.KeepAlive();
				}
				else
				{
					clientSessionInfo.Dispose();
					ClientSessionCache.cache.Remove(text);
					ClientSessionCache.cache.Add(text, new ClientSessionInfo(host, id));
				}
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001D840 File Offset: 0x0001BA40
		public static byte[] FromHost(string host)
		{
			object obj = ClientSessionCache.locker;
			byte[] array;
			lock (obj)
			{
				foreach (object obj2 in ClientSessionCache.cache.Values)
				{
					ClientSessionInfo clientSessionInfo = (ClientSessionInfo)obj2;
					if (clientSessionInfo.HostName == host && clientSessionInfo.Valid)
					{
						clientSessionInfo.KeepAlive();
						return clientSessionInfo.Id;
					}
				}
				array = null;
			}
			return array;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001D918 File Offset: 0x0001BB18
		private static ClientSessionInfo FromContext(Context context, bool checkValidity)
		{
			if (context == null)
			{
				return null;
			}
			byte[] sessionId = context.SessionId;
			if (sessionId == null || sessionId.Length == 0)
			{
				return null;
			}
			string text = BitConverter.ToString(sessionId);
			ClientSessionInfo clientSessionInfo = (ClientSessionInfo)ClientSessionCache.cache[text];
			if (clientSessionInfo == null)
			{
				return null;
			}
			if (context.ClientSettings.TargetHost != clientSessionInfo.HostName)
			{
				return null;
			}
			if (checkValidity && !clientSessionInfo.Valid)
			{
				clientSessionInfo.Dispose();
				ClientSessionCache.cache.Remove(text);
				return null;
			}
			return clientSessionInfo;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
		public static bool SetContextInCache(Context context)
		{
			object obj = ClientSessionCache.locker;
			bool flag;
			lock (obj)
			{
				ClientSessionInfo clientSessionInfo = ClientSessionCache.FromContext(context, false);
				if (clientSessionInfo == null)
				{
					flag = false;
				}
				else
				{
					clientSessionInfo.GetContext(context);
					clientSessionInfo.KeepAlive();
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001DA14 File Offset: 0x0001BC14
		public static bool SetContextFromCache(Context context)
		{
			object obj = ClientSessionCache.locker;
			bool flag;
			lock (obj)
			{
				ClientSessionInfo clientSessionInfo = ClientSessionCache.FromContext(context, true);
				if (clientSessionInfo == null)
				{
					flag = false;
				}
				else
				{
					clientSessionInfo.SetContext(context);
					clientSessionInfo.KeepAlive();
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x0400025C RID: 604
		private static Hashtable cache = new Hashtable();

		// Token: 0x0400025D RID: 605
		private static object locker = new object();
	}
}
