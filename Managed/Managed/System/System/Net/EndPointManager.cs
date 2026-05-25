using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000301 RID: 769
	internal sealed class EndPointManager
	{
		// Token: 0x06001A5D RID: 6749 RVA: 0x00049CC4 File Offset: 0x00047EC4
		private EndPointManager()
		{
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00049CD8 File Offset: 0x00047ED8
		public static void AddListener(HttpListener listener)
		{
			ArrayList arrayList = new ArrayList();
			try
			{
				Hashtable hashtable = EndPointManager.ip_to_endpoints;
				lock (hashtable)
				{
					foreach (string text in listener.Prefixes)
					{
						EndPointManager.AddPrefixInternal(text, listener);
						arrayList.Add(text);
					}
				}
			}
			catch
			{
				foreach (object obj in arrayList)
				{
					string text2 = (string)obj;
					EndPointManager.RemovePrefix(text2, listener);
				}
				throw;
			}
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00049E00 File Offset: 0x00048000
		public static void AddPrefix(string prefix, HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				EndPointManager.AddPrefixInternal(prefix, listener);
			}
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00049E48 File Offset: 0x00048048
		private static void AddPrefixInternal(string p, HttpListener listener)
		{
			ListenerPrefix listenerPrefix = new ListenerPrefix(p);
			if (listenerPrefix.Path.IndexOf('%') != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			if (listenerPrefix.Path.IndexOf("//") != -1)
			{
				throw new HttpListenerException(400, "Invalid path.");
			}
			EndPointListener eplistener = EndPointManager.GetEPListener(IPAddress.Any, listenerPrefix.Port, listener, listenerPrefix.Secure);
			eplistener.AddPrefix(listenerPrefix, listener);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00049EC8 File Offset: 0x000480C8
		private static EndPointListener GetEPListener(IPAddress addr, int port, HttpListener listener, bool secure)
		{
			Hashtable hashtable;
			if (EndPointManager.ip_to_endpoints.ContainsKey(addr))
			{
				hashtable = (Hashtable)EndPointManager.ip_to_endpoints[addr];
			}
			else
			{
				hashtable = new Hashtable();
				EndPointManager.ip_to_endpoints[addr] = hashtable;
			}
			EndPointListener endPointListener;
			if (hashtable.ContainsKey(port))
			{
				endPointListener = (EndPointListener)hashtable[port];
			}
			else
			{
				endPointListener = new EndPointListener(addr, port, secure);
				hashtable[port] = endPointListener;
			}
			return endPointListener;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00049F50 File Offset: 0x00048150
		public static void RemoveEndPoint(EndPointListener epl, IPEndPoint ep)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				Hashtable hashtable2 = (Hashtable)EndPointManager.ip_to_endpoints[ep.Address];
				hashtable2.Remove(ep.Port);
				if (hashtable2.Count == 0)
				{
					EndPointManager.ip_to_endpoints.Remove(ep.Address);
				}
				epl.Close();
			}
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00049FDC File Offset: 0x000481DC
		public static void RemoveListener(HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				foreach (string text in listener.Prefixes)
				{
					EndPointManager.RemovePrefixInternal(text, listener);
				}
			}
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0004A070 File Offset: 0x00048270
		public static void RemovePrefix(string prefix, HttpListener listener)
		{
			Hashtable hashtable = EndPointManager.ip_to_endpoints;
			lock (hashtable)
			{
				EndPointManager.RemovePrefixInternal(prefix, listener);
			}
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0004A0B8 File Offset: 0x000482B8
		private static void RemovePrefixInternal(string prefix, HttpListener listener)
		{
			ListenerPrefix listenerPrefix = new ListenerPrefix(prefix);
			if (listenerPrefix.Path.IndexOf('%') != -1)
			{
				return;
			}
			if (listenerPrefix.Path.IndexOf("//") != -1)
			{
				return;
			}
			EndPointListener eplistener = EndPointManager.GetEPListener(IPAddress.Any, listenerPrefix.Port, listener, listenerPrefix.Secure);
			eplistener.RemovePrefix(listenerPrefix, listener);
		}

		// Token: 0x04001050 RID: 4176
		private static Hashtable ip_to_endpoints = new Hashtable();
	}
}
