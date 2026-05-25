using System;
using System.Runtime.Serialization;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x0200001F RID: 31
	public class TdsInternalException : SystemException
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		internal TdsInternalException()
			: base("a TDS Exception has occurred.")
		{
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000DD1C File Offset: 0x0000BF1C
		internal TdsInternalException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000DD28 File Offset: 0x0000BF28
		internal TdsInternalException(byte theClass, int lineNumber, string message, int number, string procedure, string server, string source, byte state)
			: base(message)
		{
			this.theClass = theClass;
			this.lineNumber = lineNumber;
			this.number = number;
			this.procedure = procedure;
			this.server = server;
			this.source = source;
			this.state = state;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000DD68 File Offset: 0x0000BF68
		public byte Class
		{
			get
			{
				return this.theClass;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000DD70 File Offset: 0x0000BF70
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000DD78 File Offset: 0x0000BF78
		public override string Message
		{
			get
			{
				return base.Message;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000DD80 File Offset: 0x0000BF80
		public int Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000DD88 File Offset: 0x0000BF88
		public string Procedure
		{
			get
			{
				return this.procedure;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000DD90 File Offset: 0x0000BF90
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000DD98 File Offset: 0x0000BF98
		public override string Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000DDA0 File Offset: 0x0000BFA0
		public byte State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		[MonoTODO]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000104 RID: 260
		private byte theClass;

		// Token: 0x04000105 RID: 261
		private int lineNumber;

		// Token: 0x04000106 RID: 262
		private int number;

		// Token: 0x04000107 RID: 263
		private string procedure;

		// Token: 0x04000108 RID: 264
		private string server;

		// Token: 0x04000109 RID: 265
		private string source;

		// Token: 0x0400010A RID: 266
		private byte state;
	}
}
