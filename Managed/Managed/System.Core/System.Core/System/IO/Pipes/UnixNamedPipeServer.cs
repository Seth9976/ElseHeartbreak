using System;
using Microsoft.Win32.SafeHandles;
using Mono.Unix.Native;

namespace System.IO.Pipes
{
	// Token: 0x0200007E RID: 126
	internal class UnixNamedPipeServer : UnixNamedPipe, IPipe, INamedPipeServer
	{
		// Token: 0x0600061F RID: 1567 RVA: 0x00019DB0 File Offset: 0x00017FB0
		public UnixNamedPipeServer(NamedPipeServerStream owner, SafePipeHandle safePipeHandle)
		{
			this.handle = safePipeHandle;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00019DC0 File Offset: 0x00017FC0
		public UnixNamedPipeServer(NamedPipeServerStream owner, string pipeName, int maxNumberOfServerInstances, PipeTransmissionMode transmissionMode, PipeAccessRights rights, PipeOptions options, int inBufferSize, int outBufferSize, HandleInheritability inheritability)
		{
			string text = Path.Combine("/var/tmp/", pipeName);
			base.EnsureTargetFile(text);
			string text2 = base.RightsToAccess(rights);
			base.ValidateOptions(options, owner.TransmissionMode);
			FileStream fileStream = new FileStream(text, FileMode.Open, base.RightsToFileAccess(rights), FileShare.ReadWrite);
			this.handle = new SafePipeHandle(fileStream.Handle, false);
			owner.Stream = fileStream;
			this.should_close_handle = true;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00019E30 File Offset: 0x00018030
		public override SafePipeHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00019E38 File Offset: 0x00018038
		public void Disconnect()
		{
			if (this.should_close_handle)
			{
				Stdlib.fclose(this.handle.DangerousGetHandle());
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00019E58 File Offset: 0x00018058
		public void WaitForConnection()
		{
		}

		// Token: 0x040001AF RID: 431
		private SafePipeHandle handle;

		// Token: 0x040001B0 RID: 432
		private bool should_close_handle;
	}
}
