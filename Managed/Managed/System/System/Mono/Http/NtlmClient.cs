using System;
using System.Collections;
using System.Net;

namespace Mono.Http
{
	// Token: 0x02000020 RID: 32
	internal class NtlmClient : global::System.Net.IAuthenticationModule
	{
		// Token: 0x06000128 RID: 296 RVA: 0x0000A210 File Offset: 0x00008410
		public global::System.Net.Authorization Authenticate(string challenge, global::System.Net.WebRequest webRequest, global::System.Net.ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			string text = challenge.Trim();
			int num = text.ToLower().IndexOf("ntlm");
			if (num == -1)
			{
				return null;
			}
			num = text.IndexOfAny(new char[] { ' ', '\t' });
			if (num != -1)
			{
				text = text.Substring(num).Trim();
			}
			else
			{
				text = null;
			}
			global::System.Net.HttpWebRequest httpWebRequest = webRequest as global::System.Net.HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			Hashtable hashtable = Mono.Http.NtlmClient.cache;
			global::System.Net.Authorization authorization;
			lock (hashtable)
			{
				NtlmSession ntlmSession = (NtlmSession)Mono.Http.NtlmClient.cache[httpWebRequest.RequestUri];
				if (ntlmSession == null)
				{
					ntlmSession = new NtlmSession();
					Mono.Http.NtlmClient.cache.Add(httpWebRequest.RequestUri, ntlmSession);
				}
				authorization = ntlmSession.Authenticate(text, webRequest, credentials);
			}
			return authorization;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000A30C File Offset: 0x0000850C
		public global::System.Net.Authorization PreAuthenticate(global::System.Net.WebRequest webRequest, global::System.Net.ICredentials credentials)
		{
			return null;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000A310 File Offset: 0x00008510
		public string AuthenticationType
		{
			get
			{
				return "NTLM";
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000A318 File Offset: 0x00008518
		public bool CanPreAuthenticate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400005E RID: 94
		private static Hashtable cache = new Hashtable();
	}
}
