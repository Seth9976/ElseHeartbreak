using System;
using System.Net;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x02000014 RID: 20
	public class TdsConnectionParameters
	{
		// Token: 0x0600013B RID: 315 RVA: 0x0000CE8C File Offset: 0x0000B08C
		public TdsConnectionParameters()
		{
			this.Reset();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000CE9C File Offset: 0x0000B09C
		public void Reset()
		{
			this.ApplicationName = "Mono";
			this.Database = string.Empty;
			this.Charset = string.Empty;
			this.Hostname = Dns.GetHostName();
			this.Language = string.Empty;
			this.LibraryName = "Mono";
			this.Password = string.Empty;
			this.ProgName = "Mono";
			this.User = string.Empty;
			this.DomainLogin = false;
			this.DefaultDomain = string.Empty;
			this.AttachDBFileName = string.Empty;
		}

		// Token: 0x040000BF RID: 191
		public string ApplicationName;

		// Token: 0x040000C0 RID: 192
		public string Database;

		// Token: 0x040000C1 RID: 193
		public string Charset;

		// Token: 0x040000C2 RID: 194
		public string Hostname;

		// Token: 0x040000C3 RID: 195
		public string Language;

		// Token: 0x040000C4 RID: 196
		public string LibraryName;

		// Token: 0x040000C5 RID: 197
		public string Password;

		// Token: 0x040000C6 RID: 198
		public string ProgName;

		// Token: 0x040000C7 RID: 199
		public string User;

		// Token: 0x040000C8 RID: 200
		public bool DomainLogin;

		// Token: 0x040000C9 RID: 201
		public string DefaultDomain;

		// Token: 0x040000CA RID: 202
		public string AttachDBFileName;
	}
}
