using System;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x02000020 RID: 32
	public class TdsInternalInfoMessageEventArgs : EventArgs
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public TdsInternalInfoMessageEventArgs(TdsInternalErrorCollection errors)
		{
			this.errors = errors;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		public TdsInternalInfoMessageEventArgs(TdsInternalError error)
		{
			this.errors = new TdsInternalErrorCollection();
			this.errors.Add(error);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		public TdsInternalErrorCollection Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
		public byte Class
		{
			get
			{
				return this.errors[0].Class;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000DDFC File Offset: 0x0000BFFC
		public int LineNumber
		{
			get
			{
				return this.errors[0].LineNumber;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000DE10 File Offset: 0x0000C010
		public string Message
		{
			get
			{
				return this.errors[0].Message;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000DE24 File Offset: 0x0000C024
		public int Number
		{
			get
			{
				return this.errors[0].Number;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000DE38 File Offset: 0x0000C038
		public string Procedure
		{
			get
			{
				return this.errors[0].Procedure;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000DE4C File Offset: 0x0000C04C
		public string Server
		{
			get
			{
				return this.errors[0].Server;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000DE60 File Offset: 0x0000C060
		public string Source
		{
			get
			{
				return this.errors[0].Source;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000DE74 File Offset: 0x0000C074
		public byte State
		{
			get
			{
				return this.errors[0].State;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000DE88 File Offset: 0x0000C088
		public int Add(byte theClass, int lineNumber, string message, int number, string procedure, string server, string source, byte state)
		{
			return this.errors.Add(new TdsInternalError(theClass, lineNumber, message, number, procedure, server, source, state));
		}

		// Token: 0x0400010B RID: 267
		private TdsInternalErrorCollection errors;
	}
}
