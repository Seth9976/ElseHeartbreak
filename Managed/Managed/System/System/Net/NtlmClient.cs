using System;
using Mono.Http;

namespace System.Net
{
	// Token: 0x020003D8 RID: 984
	internal class NtlmClient : IAuthenticationModule
	{
		// Token: 0x060021A7 RID: 8615 RVA: 0x00062944 File Offset: 0x00060B44
		public NtlmClient()
		{
			this.authObject = new Mono.Http.NtlmClient();
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x00062958 File Offset: 0x00060B58
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (this.authObject == null)
			{
				return null;
			}
			return this.authObject.Authenticate(challenge, webRequest, credentials);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x00062978 File Offset: 0x00060B78
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return null;
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x0006297C File Offset: 0x00060B7C
		public string AuthenticationType
		{
			get
			{
				return "NTLM";
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x00062984 File Offset: 0x00060B84
		public bool CanPreAuthenticate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040014E6 RID: 5350
		private IAuthenticationModule authObject;
	}
}
