using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000083 RID: 131
	internal class Win32NamedPipeClient : Win32NamedPipe, IPipe, INamedPipeClient
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x00019FD4 File Offset: 0x000181D4
		public Win32NamedPipeClient(NamedPipeClientStream owner, SafePipeHandle safePipeHandle)
		{
			this.handle = safePipeHandle;
			this.owner = owner;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00019FEC File Offset: 0x000181EC
		public Win32NamedPipeClient(NamedPipeClientStream owner, string serverName, string pipeName, PipeAccessRights desiredAccessRights, PipeOptions options, HandleInheritability inheritability)
		{
			Win32NamedPipeClient <>f__this = this;
			this.name = string.Format("\\\\{0}\\pipe\\{1}", serverName, pipeName);
			SecurityAttributesHack att = new SecurityAttributesHack(inheritability == HandleInheritability.Inheritable);
			this.is_async = (options & PipeOptions.Asynchronous) != PipeOptions.None;
			this.opener = delegate
			{
				IntPtr intPtr = Win32Marshal.CreateFile(<>f__this.name, desiredAccessRights, FileShare.None, ref att, 3, 0, IntPtr.Zero);
				if (intPtr == new IntPtr(-1L))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				return new SafePipeHandle(intPtr, true);
			};
			this.owner = owner;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001A060 File Offset: 0x00018260
		public override SafePipeHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001A068 File Offset: 0x00018268
		public bool IsAsync
		{
			get
			{
				return this.is_async;
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001A070 File Offset: 0x00018270
		public void Connect()
		{
			if (this.owner.IsConnected)
			{
				throw new InvalidOperationException("The named pipe is already connected");
			}
			this.handle = this.opener();
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001A0AC File Offset: 0x000182AC
		public void Connect(int timeout)
		{
			if (this.owner.IsConnected)
			{
				throw new InvalidOperationException("The named pipe is already connected");
			}
			if (!Win32Marshal.WaitNamedPipe(this.name, timeout))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			this.Connect();
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0001A0EC File Offset: 0x000182EC
		public int NumberOfServerInstances
		{
			get
			{
				byte[] array = null;
				int num;
				int num2;
				int num3;
				int num4;
				if (!Win32Marshal.GetNamedPipeHandleState(this.Handle, out num, out num2, out num3, out num4, array, 0))
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				return num2;
			}
		}

		// Token: 0x040001B5 RID: 437
		private NamedPipeClientStream owner;

		// Token: 0x040001B6 RID: 438
		private Func<SafePipeHandle> opener;

		// Token: 0x040001B7 RID: 439
		private bool is_async;

		// Token: 0x040001B8 RID: 440
		private string name;

		// Token: 0x040001B9 RID: 441
		private SafePipeHandle handle;
	}
}
