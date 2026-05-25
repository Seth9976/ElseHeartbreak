using System;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	/// <summary>Exposes a <see cref="T:System.IO.Stream" /> around a named pipe, which supports both synchronous and asynchronous read and write operations.</summary>
	// Token: 0x0200006A RID: 106
	[MonoTODO("working only on win32 right now")]
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class NamedPipeClientStream : PipeStream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe name.</summary>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".</exception>
		// Token: 0x06000599 RID: 1433 RVA: 0x0001912C File Offset: 0x0001732C
		public NamedPipeClientStream(string pipeName)
			: this(".", pipeName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> is null.-or-<paramref name="serverName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> is a zero-length string.-or-<paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".</exception>
		// Token: 0x0600059A RID: 1434 RVA: 0x0001913C File Offset: 0x0001733C
		public NamedPipeClientStream(string serverName, string pipeName)
			: this(serverName, pipeName, PipeDirection.InOut)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe direction.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that determines the direction of the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> is null.-or-<paramref name="serverName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> is a zero-length string.-or-<paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".-or-<paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.</exception>
		// Token: 0x0600059B RID: 1435 RVA: 0x00019148 File Offset: 0x00017348
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction)
			: this(serverName, pipeName, direction, PipeOptions.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe direction and pipe options.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that determines the direction of the pipe.</param>
		/// <param name="options">One of the <see cref="T:System.IO.Pipes.PipeOptions" /> values that determines how to open or create the pipe.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> is null.-or-<paramref name="serverName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> is a zero-length string.-or-<paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".-or-<paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-<paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.</exception>
		// Token: 0x0600059C RID: 1436 RVA: 0x00019154 File Offset: 0x00017354
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options)
			: this(serverName, pipeName, direction, options, TokenImpersonationLevel.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe direction, pipe options, and security impersonation level.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that determines the direction of the pipe.</param>
		/// <param name="options">One of the <see cref="T:System.IO.Pipes.PipeOptions" /> values that determines how to open or create the pipe.</param>
		/// <param name="impersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values that determines the security impersonation level.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> is null.-or-<paramref name="serverName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> is a zero-length string.-or-<paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".-or-<paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-<paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.-or-<paramref name="impersonationLevel" /> is not a valid <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> value.</exception>
		// Token: 0x0600059D RID: 1437 RVA: 0x00019164 File Offset: 0x00017364
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options, TokenImpersonationLevel impersonationLevel)
			: this(serverName, pipeName, direction, options, impersonationLevel, HandleInheritability.None)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe direction, pipe options, security impersonation level, and inheritability mode.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that determines the direction of the pipe.</param>
		/// <param name="options">One of the <see cref="T:System.IO.Pipes.PipeOptions" /> values that determines how to open or create the pipe.</param>
		/// <param name="impersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values that determines the security impersonation level.</param>
		/// <param name="inheritability">One of the <see cref="T:System.IO.HandleInheritability" /> values that determines whether the underlying handle will be inheritable by child processes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> is null.-or-<paramref name="serverName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> is a zero-length string.-or-<paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".-or-<paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-<paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.-or-<paramref name="impersonationLevel" /> is not a valid <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> value.-or-<paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> value.</exception>
		// Token: 0x0600059E RID: 1438 RVA: 0x00019174 File Offset: 0x00017374
		public NamedPipeClientStream(string serverName, string pipeName, PipeDirection direction, PipeOptions options, TokenImpersonationLevel impersonationLevel, HandleInheritability inheritability)
			: this(serverName, pipeName, PipeStream.ToAccessRights(direction), options, impersonationLevel, inheritability)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class for the specified pipe handle with the specified pipe direction.</summary>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that determines the direction of the pipe.</param>
		/// <param name="isAsync">true to indicate that the handle was opened asynchronously; otherwise, false.</param>
		/// <param name="isConnected">true to indicate that the pipe is connected; otherwise, false.</param>
		/// <param name="safePipeHandle">A <see cref="T:Microsoft.Win32.SafeHandles.SafePipeHandle" /> object for the pipe that this <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> object will encapsulate.</param>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="safePipeHandle" /> is not a valid handle.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="safePipeHandle" /> is not a valid pipe handle.</exception>
		// Token: 0x0600059F RID: 1439 RVA: 0x00019198 File Offset: 0x00017398
		public NamedPipeClientStream(PipeDirection direction, bool isAsync, bool isConnected, SafePipeHandle safePipeHandle)
			: base(direction, 1024)
		{
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32NamedPipeClient(this, safePipeHandle);
			}
			else
			{
				this.impl = new UnixNamedPipeClient(this, safePipeHandle);
			}
			base.IsConnected = isConnected;
			base.InitializeHandle(safePipeHandle, true, isAsync);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> class with the specified pipe and server names, and the specified pipe options, security impersonation level, and inheritability mode.</summary>
		/// <param name="serverName">The name of the remote computer to connect to, or "." to specify the local computer.</param>
		/// <param name="pipeName">The name of the pipe.</param>
		/// <param name="desiredAccessRights">One of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values that specifies the desired access rights of the pipe.</param>
		/// <param name="options">One of the <see cref="T:System.IO.Pipes.PipeOptions" /> values that determines how to open or create the pipe.</param>
		/// <param name="impersonationLevel">One of the <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> values that determines the security impersonation level.</param>
		/// <param name="inheritability">One of the <see cref="T:System.IO.HandleInheritability" /> values that determines whether the underlying handle will be inheritable by child processes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeName" /> is null.-or-<paramref name="serverName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeName" /> is a zero-length string.-or-<paramref name="serverName" /> is a zero-length string.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="pipeName" /> is set to "anonymous".-or-<paramref name="options" /> is not a valid <see cref="T:System.IO.Pipes.PipeOptions" /> value.-or-<paramref name="impersonationLevel" /> is not a valid <see cref="T:System.Security.Principal.TokenImpersonationLevel" /> value.-or-<paramref name="inheritability" /> is not a valid <see cref="T:System.IO.HandleInheritability" /> value.</exception>
		// Token: 0x060005A0 RID: 1440 RVA: 0x000191F0 File Offset: 0x000173F0
		public NamedPipeClientStream(string serverName, string pipeName, PipeAccessRights desiredAccessRights, PipeOptions options, TokenImpersonationLevel impersonationLevel, HandleInheritability inheritability)
			: base(PipeStream.ToDirection(desiredAccessRights), 1024)
		{
			if (impersonationLevel != TokenImpersonationLevel.None || inheritability != HandleInheritability.None)
			{
				throw base.ThrowACLException();
			}
			if (PipeStream.IsWindows)
			{
				this.impl = new Win32NamedPipeClient(this, serverName, pipeName, desiredAccessRights, options, inheritability);
			}
			else
			{
				this.impl = new UnixNamedPipeClient(this, serverName, pipeName, desiredAccessRights, options, inheritability);
			}
		}

		/// <summary>Connects to a waiting server.</summary>
		/// <exception cref="T:System.InvalidOperationException">The client is already connected.</exception>
		// Token: 0x060005A1 RID: 1441 RVA: 0x00019258 File Offset: 0x00017458
		public void Connect()
		{
			this.impl.Connect();
			base.InitializeHandle(this.impl.Handle, false, this.impl.IsAsync);
			base.IsConnected = true;
		}

		/// <summary>Connects to a waiting server within the specified time-out period.</summary>
		/// <param name="timeout">The number of milliseconds to wait for the server to respond before the connection times out.</param>
		/// <exception cref="T:System.TimeoutException">Could not connect to the server within the specified <paramref name="timeout" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The client is already connected.</exception>
		/// <exception cref="T:System.IO.IOException">The server is connected to another client and the time-out period has expired.</exception>
		// Token: 0x060005A2 RID: 1442 RVA: 0x00019294 File Offset: 0x00017494
		public void Connect(int timeout)
		{
			this.impl.Connect(timeout);
			base.InitializeHandle(this.impl.Handle, false, this.impl.IsAsync);
			base.IsConnected = true;
		}

		/// <summary>Gets the number of server instances that share the same pipe name.</summary>
		/// <returns>The number of server instances that share the same pipe name.</returns>
		/// <exception cref="T:System.InvalidOperationException">The pipe handle has not been set.-or-The current <see cref="T:System.IO.Pipes.NamedPipeClientStream" /> object has not yet connected to a <see cref="T:System.IO.Pipes.NamedPipeServerStream" /> object.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or an I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The underlying pipe handle is closed.</exception>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x000192D4 File Offset: 0x000174D4
		public int NumberOfServerInstances
		{
			get
			{
				this.CheckPipePropertyOperations();
				return this.impl.NumberOfServerInstances;
			}
		}

		// Token: 0x04000178 RID: 376
		private INamedPipeClient impl;
	}
}
