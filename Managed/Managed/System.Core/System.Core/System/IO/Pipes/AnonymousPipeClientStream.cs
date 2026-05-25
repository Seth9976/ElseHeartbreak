using System;
using System.Globalization;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	/// <summary>Exposes the client side of an anonymous pipe stream, which supports both synchronous and asynchronous read and write operations.</summary>
	// Token: 0x02000068 RID: 104
	[MonoTODO("Anonymous pipes are not working even on win32, due to some access authorization issue")]
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class AnonymousPipeClientStream : PipeStream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> class with the specified string representation of the pipe handle.</summary>
		/// <param name="pipeHandleAsString">A string that represents the pipe handle.</param>
		/// <exception cref="T:System.IO.IOException">
		///   <paramref name="pipeHandleAsString" /> is not a valid pipe handle.</exception>
		// Token: 0x06000587 RID: 1415 RVA: 0x00018F04 File Offset: 0x00017104
		public AnonymousPipeClientStream(string pipeHandleAsString)
			: this(PipeDirection.In, pipeHandleAsString)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> class with the specified pipe direction and a string representation of the pipe handle.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="pipeHandleAsString">A string that represents the pipe handle.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="pipeHandleAsString" /> is an invalid handle.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeHandleAsString" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		// Token: 0x06000588 RID: 1416 RVA: 0x00018F10 File Offset: 0x00017110
		public AnonymousPipeClientStream(PipeDirection direction, string pipeHandleAsString)
			: this(direction, AnonymousPipeClientStream.ToSafePipeHandle(pipeHandleAsString))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> class from the specified handle.</summary>
		/// <param name="direction">One of the enumeration values that determines the direction of the pipe.Anonymous pipes can only be in one direction, so <paramref name="direction" /> cannot be set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</param>
		/// <param name="safePipeHandle">A safe handle for the pipe that this <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object will encapsulate.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="safePipeHandle " />is not a valid handle.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="safePipeHandle" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="direction" /> is set to <see cref="F:System.IO.Pipes.PipeDirection.InOut" />.</exception>
		/// <exception cref="T:System.IO.IOException">An I/O error, such as a disk error, has occurred.-or-The stream has been closed.</exception>
		// Token: 0x06000589 RID: 1417 RVA: 0x00018F20 File Offset: 0x00017120
		public AnonymousPipeClientStream(PipeDirection direction, SafePipeHandle safePipeHandle)
			: base(direction, 1024)
		{
			base.InitializeHandle(safePipeHandle, false, false);
			base.IsConnected = true;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00018F40 File Offset: 0x00017140
		private static SafePipeHandle ToSafePipeHandle(string pipeHandleAsString)
		{
			if (pipeHandleAsString == null)
			{
				throw new ArgumentNullException("pipeHandleAsString");
			}
			return new SafePipeHandle(new IntPtr(long.Parse(pipeHandleAsString, NumberFormatInfo.InvariantInfo)), false);
		}

		/// <summary>Sets the reading mode for the <see cref="T:System.IO.Pipes.AnonymousPipeClientStream" /> object.</summary>
		/// <returns>Always <see cref="F:System.IO.Pipes.PipeTransmissionMode.Byte" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The transmission mode is not valid. For anonymous pipes, only <see cref="F:System.IO.Pipes.PipeTransmissionMode.Byte" /> is supported.</exception>
		/// <exception cref="T:System.NotSupportedException">The transmission mode is <see cref="F:System.IO.Pipes.PipeTransmissionMode.Message" />.</exception>
		/// <exception cref="T:System.IO.IOException">The connection is broken or another I/O error occurs.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		// Token: 0x17000080 RID: 128
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x00018F6C File Offset: 0x0001716C
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

		/// <summary>Gets the pipe transmission mode supported by the current pipe.</summary>
		/// <returns>Always <see cref="F:System.IO.Pipes.PipeTransmissionMode.Byte" />.</returns>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00018F7C File Offset: 0x0001717C
		public override PipeTransmissionMode TransmissionMode
		{
			get
			{
				return PipeTransmissionMode.Byte;
			}
		}
	}
}
