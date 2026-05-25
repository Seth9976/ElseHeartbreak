using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x0200007B RID: 123
	internal class UnixAnonymousPipeServer : UnixAnonymousPipe, IPipe, IAnonymousPipeServer
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x00019AD0 File Offset: 0x00017CD0
		public UnixAnonymousPipeServer(AnonymousPipeServerStream owner, PipeDirection direction, HandleInheritability inheritability, int bufferSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00019AE0 File Offset: 0x00017CE0
		public UnixAnonymousPipeServer(AnonymousPipeServerStream owner, SafePipeHandle serverHandle, SafePipeHandle clientHandle)
		{
			this.server_handle = serverHandle;
			this.client_handle = clientHandle;
			throw new NotImplementedException();
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00019AFC File Offset: 0x00017CFC
		public override SafePipeHandle Handle
		{
			get
			{
				return this.server_handle;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00019B04 File Offset: 0x00017D04
		public SafePipeHandle ClientHandle
		{
			get
			{
				return this.client_handle;
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00019B0C File Offset: 0x00017D0C
		public void DisposeLocalCopyOfClientHandle()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040001A9 RID: 425
		private SafePipeHandle server_handle;

		// Token: 0x040001AA RID: 426
		private SafePipeHandle client_handle;
	}
}
