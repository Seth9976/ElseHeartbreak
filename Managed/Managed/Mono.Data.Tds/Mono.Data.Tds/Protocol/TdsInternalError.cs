using System;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x0200001C RID: 28
	public sealed class TdsInternalError
	{
		// Token: 0x0600019A RID: 410 RVA: 0x0000DB98 File Offset: 0x0000BD98
		public TdsInternalError(byte theClass, int lineNumber, string message, int number, string procedure, string server, string source, byte state)
		{
			this.theClass = theClass;
			this.lineNumber = lineNumber;
			this.message = message;
			this.number = number;
			this.procedure = procedure;
			this.server = server;
			this.source = source;
			this.state = state;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000DBF0 File Offset: 0x0000BDF0
		public byte Class
		{
			get
			{
				return this.theClass;
			}
			set
			{
				this.theClass = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000DBFC File Offset: 0x0000BDFC
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000DC04 File Offset: 0x0000BE04
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
			set
			{
				this.lineNumber = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000DC10 File Offset: 0x0000BE10
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000DC18 File Offset: 0x0000BE18
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000DC24 File Offset: 0x0000BE24
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x0000DC2C File Offset: 0x0000BE2C
		public int Number
		{
			get
			{
				return this.number;
			}
			set
			{
				this.number = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000DC38 File Offset: 0x0000BE38
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000DC40 File Offset: 0x0000BE40
		public string Procedure
		{
			get
			{
				return this.procedure;
			}
			set
			{
				this.procedure = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000DC4C File Offset: 0x0000BE4C
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000DC54 File Offset: 0x0000BE54
		public string Server
		{
			get
			{
				return this.server;
			}
			set
			{
				this.server = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000DC60 File Offset: 0x0000BE60
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000DC68 File Offset: 0x0000BE68
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				this.source = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000DC74 File Offset: 0x0000BE74
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000DC7C File Offset: 0x0000BE7C
		public byte State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x040000FB RID: 251
		private byte theClass;

		// Token: 0x040000FC RID: 252
		private int lineNumber;

		// Token: 0x040000FD RID: 253
		private string message;

		// Token: 0x040000FE RID: 254
		private int number;

		// Token: 0x040000FF RID: 255
		private string procedure;

		// Token: 0x04000100 RID: 256
		private string server;

		// Token: 0x04000101 RID: 257
		private string source;

		// Token: 0x04000102 RID: 258
		private byte state;
	}
}
