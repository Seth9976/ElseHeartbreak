using System;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	/// <summary>Exposes a stream around an anonymous pipe, which supports both synchronous and asynchronous read and write operations.</summary>
	// Token: 0x02000069 RID: 105
	[MonoTODO("Anonymous pipes are not working even on win32, due to some access authorization issue")]
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class AnonymousPipeServerStream : PipeStream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class.</summary>
		// Token: 0x0600058D RID: 1421 RVA: 0x00018F80 File Offset: 0x00017180
		public AnonymousPipeServerStream()
			: this(PipeDirection.Out)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class with the specified pipe direction.</summary>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x0600058E RID: 1422 RVA: 0x00018F8C File Offset: 0x0001718C
		public AnonymousPipeServerStream(PipeDirection direction)
			: this(direction, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class with the specified pipe direction and inheritability mode.</summary>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="inheritability">One of the <see cref="T:System.IO.HandleInheritability" /> values that determines whether the underlying handle can be inherited by child processes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inheritably" /> is not set to either <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" />.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x0600058F RID: 1423 RVA: 0x00018F98 File Offset: 0x00017198
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability)
			: this(direction, inheritability, 1024)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class with the specified pipe direction, inheritability mode, and buffer size.</summary>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="inheritability">One of the <see cref="T:System.IO.HandleInheritability" /> values that determines whether the underlying handle can be inherited by child processes.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than or equal to 0, which specifies the buffer size.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inheritably" /> is not set to either <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" />.-or-<paramref name="bufferSize" /> is less than 0.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x06000590 RID: 1424 RVA: 0x00018FA8 File Offset: 0x000171A8
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability, int bufferSize)
			: this(direction, inheritability, bufferSize, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class with the specified pipe direction, inheritability mode, buffer size, and pipe security.</summary>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="inheritability">One of the <see cref="T:System.IO.HandleInheritability" /> values that determines whether the underlying handle can be inherited by child processes.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than 0, which specifies the buffer size.</param>
		/// <param name="pipeSecurity">A <see cref="T:System.IO.Pipes.PipeSecurity" /> object that determines the access control and audit security for the pipe.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inheritably" /> is not set to either <see cref="F:System.IO.HandleInheritability.None" /> or <see cref="F:System.IO.HandleInheritability.Inheritable" />.-or-<paramref name="bufferSize" /> is less than 0.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x06000591 RID: 1425 RVA: 0x00018FB4 File Offset: 0x000171B4
		public AnonymousPipeServerStream(PipeDirection direction, HandleInheritability inheritability, int bufferSize, PipeSecurity pipeSecurity)
			: base(direction, bufferSize)
		{
			if (pipeSecurity != null)
			{
				throw base.ThrowACLException();
			}
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException("Anonymous pipe direction can only be either in or out.");
			}
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32AnonymousPipeServer(this, direction, inheritability, bufferSize);
			}
			else
			{
				this.impl = new UnixAnonymousPipeServer(this, direction, inheritability, bufferSize);
			}
			base.InitializeHandle(this.impl.Handle, false, false);
			base.IsConnected = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> class from the specified pipe handles.</summary>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="serverSafePipeHandle">A <see cref="T:Microsoft.Win32.SafeHandles.SafePipeHandle" /> for the pipe that this <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object will encapsulate.</param>
		/// <param name="clientSafePipeHandle">A <see cref="T:Microsoft.Win32.SafeHandles.SafePipeHandle" /> for the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="serverSafePipeHandle" /> or <paramref name="cleintSafePipeHandle" /> is an invalid handle.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="serverSafePipeHandle" /> or <paramref name="cleintSafePipeHandle" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="serverSafePipeHandle" /> or <paramref name="cleintSafePipeHandle" /> is an invalid pipe handle.-or-An I/O error, such as a disk error, has occurred.-or-The stream has been closed.</exception>
		// Token: 0x06000592 RID: 1426 RVA: 0x00019030 File Offset: 0x00017230
		[MonoTODO]
		public AnonymousPipeServerStream(PipeDirection direction, SafePipeHandle serverSafePipeHandle, SafePipeHandle clientSafePipeHandle)
			: base(direction, 1024)
		{
			if (serverSafePipeHandle == null)
			{
				throw new ArgumentNullException("serverSafePipeHandle");
			}
			if (clientSafePipeHandle == null)
			{
				throw new ArgumentNullException("clientSafePipeHandle");
			}
			if (direction == PipeDirection.InOut)
			{
				throw new NotSupportedException("Anonymous pipe direction can only be either in or out.");
			}
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32AnonymousPipeServer(this, serverSafePipeHandle, clientSafePipeHandle);
			}
			else
			{
				this.impl = new UnixAnonymousPipeServer(this, serverSafePipeHandle, clientSafePipeHandle);
			}
			base.InitializeHandle(serverSafePipeHandle, true, false);
			base.IsConnected = true;
			this.ClientSafePipeHandle = clientSafePipeHandle;
		}

		/// <summary>Gets the safe handle for the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object that is currently connected to the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.SafeHandles.SafePipeHandle" /> object for the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object that is currently connected to the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object.</returns>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x000190C0 File Offset: 0x000172C0
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x000190C8 File Offset: 0x000172C8
		[MonoTODO]
		public SafePipeHandle ClientSafePipeHandle { get; private set; }

		/// <summary>Sets the reading mode for the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object.</summary>
		/// <returns>One of the <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> values that indicates how the <see cref="T:System.IO.Pipes.AnonymousPipeServerStream" /> object reads from the pipe.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">For anonymous pipes, transmission mode must be <see cref="F:System.IO.Pipes.PipeTransmissionMode.Byte" />.</exception>
		/// <exception cref="T:System.NotSupportedException">Anonymous pipes do not support <see cref="F:System.IO.Pipes.PipeTransmissionMode.Message" />.</exception>
		/// <exception cref="T:System.IO.IOException">The connection is broken or another I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		// Token: 0x17000083 RID: 131
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x000190D4 File Offset: 0x000172D4
		public override PipeTransmissionMode ReadMode
		{
			set
			{
				if (value == PipeTransmissionMode.Message)
				{
					throw new NotSupportedException();
				}
			}
		}

		/// <summary>Gets the pipe transmission mode that is supported by the current pipe.</summary>
		/// <returns>Always <see cref="F:System.IO.Pipes.PipeTransmissionMode.Byte" />.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x000190E4 File Offset: 0x000172E4
		public override PipeTransmissionMode TransmissionMode
		{
			get
			{
				return PipeTransmissionMode.Byte;
			}
		}

		/// <summary>Closes the local copy of the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object's handle.</summary>
		// Token: 0x06000597 RID: 1431 RVA: 0x000190E8 File Offset: 0x000172E8
		[MonoTODO]
		public void DisposeLocalCopyOfClientHandle()
		{
			this.impl.DisposeLocalCopyOfClientHandle();
		}

		/// <summary>Gets the connected <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object's handle as a string.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the connected <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object's handle.</returns>
		// Token: 0x06000598 RID: 1432 RVA: 0x000190F8 File Offset: 0x000172F8
		public string GetClientHandleAsString()
		{
			return this.impl.Handle.DangerousGetHandle().ToInt64().ToString(NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x04000176 RID: 374
		private IAnonymousPipeServer impl;
	}
}
