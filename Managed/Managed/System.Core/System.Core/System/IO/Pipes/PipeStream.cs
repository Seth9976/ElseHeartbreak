using System;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	/// <summary>Exposes a <see cref="T:System.IO.Stream" /> object around a pipe, which supports both anonymous and named pipes.</summary>
	// Token: 0x02000077 RID: 119
	[PermissionSet((SecurityAction)15, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\"/>\n")]
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public abstract class PipeStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeStream" /> class using the specified <see cref="T:System.IO.Pipes.PipeDirection" /> value and buffer size.</summary>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that indicates the direction of the pipe object.</param>
		/// <param name="bufferSize">A positive <see cref="T:System.Int32" /> value greater than or equal to 0 that indicates the buffer size.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-<paramref name="bufferSize" /> is less than 0.</exception>
		// Token: 0x060005D8 RID: 1496 RVA: 0x00019674 File Offset: 0x00017874
		protected PipeStream(PipeDirection direction, int bufferSize)
			: this(direction, PipeTransmissionMode.Byte, bufferSize)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeStream" /> class using the specified <see cref="T:System.IO.Pipes.PipeDirection" />, <see cref="T:System.IO.Pipes.PipeTransmissionMode" />, and buffer size.</summary>
		/// <param name="direction">One of the <see cref="T:System.IO.Pipes.PipeDirection" /> values that indicates the direction of the pipe object.</param>
		/// <param name="transmissionMode">One of the <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> values that indicates the transmission mode of the pipe object.</param>
		/// <param name="outBufferSize">A positive <see cref="T:System.Int32" /> value greater than or equal to 0 that indicates the buffer size.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="direction" /> is not a valid <see cref="T:System.IO.Pipes.PipeDirection" /> value.-or-<paramref name="transmissionMode" /> is not a valid <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> value.-or-<paramref name="bufferSize" /> is less than 0.</exception>
		// Token: 0x060005D9 RID: 1497 RVA: 0x00019680 File Offset: 0x00017880
		protected PipeStream(PipeDirection direction, PipeTransmissionMode transmissionMode, int outBufferSize)
		{
			this.direction = direction;
			this.transmission_mode = transmissionMode;
			this.read_trans_mode = transmissionMode;
			if (outBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize must be greater than 0");
			}
			this.buffer_size = outBufferSize;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x000196C4 File Offset: 0x000178C4
		internal static bool IsWindows
		{
			get
			{
				return Win32Marshal.IsWindows;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000196CC File Offset: 0x000178CC
		internal Exception ThrowACLException()
		{
			return new NotImplementedException("ACL is not supported in Mono");
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000196D8 File Offset: 0x000178D8
		internal static PipeAccessRights ToAccessRights(PipeDirection direction)
		{
			switch (direction)
			{
			case PipeDirection.In:
				return PipeAccessRights.ReadData;
			case PipeDirection.Out:
				return PipeAccessRights.WriteData;
			case PipeDirection.InOut:
				return PipeAccessRights.ReadData | PipeAccessRights.WriteData;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001970C File Offset: 0x0001790C
		internal static PipeDirection ToDirection(PipeAccessRights rights)
		{
			bool flag = (rights & PipeAccessRights.ReadData) != (PipeAccessRights)0;
			bool flag2 = (rights & PipeAccessRights.WriteData) != (PipeAccessRights)0;
			if (flag)
			{
				if (flag2)
				{
					return PipeDirection.InOut;
				}
				return PipeDirection.In;
			}
			else
			{
				if (flag2)
				{
					return PipeDirection.Out;
				}
				throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports read operations.</summary>
		/// <returns>true if the stream supports read operations; otherwise, false.</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0001974C File Offset: 0x0001794C
		public override bool CanRead
		{
			get
			{
				return (this.direction & PipeDirection.In) != (PipeDirection)0;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports seek operations.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0001975C File Offset: 0x0001795C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current stream supports write operations.</summary>
		/// <returns>true if the stream supports write operations; otherwise, false.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00019760 File Offset: 0x00017960
		public override bool CanWrite
		{
			get
			{
				return (this.direction & PipeDirection.Out) != (PipeDirection)0;
			}
		}

		/// <summary>Gets the size, in bytes, of the inbound buffer for a pipe.</summary>
		/// <returns>An integer value that represents the inbound buffer size, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The pipe is waiting to connect.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00019770 File Offset: 0x00017970
		public virtual int InBufferSize
		{
			get
			{
				return this.buffer_size;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.IO.Pipes.PipeStream" /> object was opened asynchronously or synchronously.</summary>
		/// <returns>true if the <see cref="T:System.IO.Pipes.PipeStream" /> object was opened asynchronously; otherwise, false.</returns>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00019778 File Offset: 0x00017978
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x00019780 File Offset: 0x00017980
		public bool IsAsync { get; private set; }

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.IO.Pipes.PipeStream" /> object is connected.</summary>
		/// <returns>true if the <see cref="T:System.IO.Pipes.PipeStream" /> object is connected; otherwise, false.</returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0001978C File Offset: 0x0001798C
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x00019794 File Offset: 0x00017994
		public bool IsConnected { get; protected set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000197A0 File Offset: 0x000179A0
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x0001981C File Offset: 0x00017A1C
		internal Stream Stream
		{
			get
			{
				if (!this.IsConnected)
				{
					throw new InvalidOperationException("Pipe is not connected");
				}
				if (this.stream == null)
				{
					this.stream = new FileStream(this.handle.DangerousGetHandle(), (!this.CanRead) ? FileAccess.Write : ((!this.CanWrite) ? FileAccess.Read : FileAccess.ReadWrite), true, this.buffer_size, this.IsAsync);
				}
				return this.stream;
			}
			set
			{
				this.stream = value;
			}
		}

		/// <summary>Gets a value indicating whether a handle to a <see cref="T:System.IO.Pipes.PipeStream" /> object is exposed.</summary>
		/// <returns>true if a handle to the <see cref="T:System.IO.Pipes.PipeStream" /> object is exposed; otherwise, false.</returns>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00019828 File Offset: 0x00017A28
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00019830 File Offset: 0x00017A30
		private protected bool IsHandleExposed { protected get; private set; }

		/// <summary>Gets a value indicating whether there is more data in the message returned from the most recent read operation.</summary>
		/// <returns>true if there are no more characters to read in the message; otherwise, false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The pipe is not connected.-or-The pipe handle has not been set.-or-The pipe's <see cref="P:System.IO.Pipes.PipeStream.ReadMode" /> property value is not <see cref="F:System.IO.Pipes.PipeTransmissionMode.Message" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0001983C File Offset: 0x00017A3C
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00019844 File Offset: 0x00017A44
		[MonoTODO]
		public bool IsMessageComplete { get; private set; }

		/// <summary>Gets the size, in bytes, of the outbound buffer for a pipe.</summary>
		/// <returns>The outbound buffer size, in bytes.</returns>
		/// <exception cref="T:System.InvalidOperationException">The pipe is waiting to connect.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00019850 File Offset: 0x00017A50
		[MonoTODO]
		public virtual int OutBufferSize
		{
			get
			{
				return this.buffer_size;
			}
		}

		/// <summary>Gets or sets the reading mode for a <see cref="T:System.IO.Pipes.PipeStream" /> object.</summary>
		/// <returns>One of the <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> values that indicates how the <see cref="T:System.IO.Pipes.PipeStream" /> object reads from the pipe.</returns>
		/// <exception cref="T:System.NotSupportedException">The supplied value is not a valid <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> value.</exception>
		/// <exception cref="T:System.InvalidOperationException">The handle has not been set.-or-The pipe is waiting to connect with a named client.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or an I/O error occurred with a named client.</exception>
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00019858 File Offset: 0x00017A58
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x00019868 File Offset: 0x00017A68
		public virtual PipeTransmissionMode ReadMode
		{
			get
			{
				this.CheckPipePropertyOperations();
				return this.read_trans_mode;
			}
			set
			{
				this.CheckPipePropertyOperations();
				this.read_trans_mode = value;
			}
		}

		/// <summary>Gets the safe handle for the local end of the pipe that the current <see cref="T:System.IO.Pipes.PipeStream" /> object encapsulates.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.SafeHandles.SafePipeHandle" /> object for the pipe that is encapsulated by the current <see cref="T:System.IO.Pipes.PipeStream" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The pipe handle has not been set.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00019878 File Offset: 0x00017A78
		public SafePipeHandle SafePipeHandle
		{
			get
			{
				this.CheckPipePropertyOperations();
				return this.handle;
			}
		}

		/// <summary>Gets the pipe transmission mode supported by the current pipe.</summary>
		/// <returns>One of the <see cref="T:System.IO.Pipes.PipeTransmissionMode" /> values that indicates the transmission mode supported by the current pipe.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The handle has not been set.-or-The pipe is waiting to connect in an anonymous client/server operation or with a named client. </exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00019888 File Offset: 0x00017A88
		public virtual PipeTransmissionMode TransmissionMode
		{
			get
			{
				this.CheckPipePropertyOperations();
				return this.transmission_mode;
			}
		}

		/// <summary>Verifies that the pipe is in a proper state for getting or setting properties.</summary>
		// Token: 0x060005F1 RID: 1521 RVA: 0x00019898 File Offset: 0x00017A98
		[MonoTODO]
		protected internal virtual void CheckPipePropertyOperations()
		{
		}

		/// <summary>Verifies that the pipe is in a connected state for read operations.</summary>
		// Token: 0x060005F2 RID: 1522 RVA: 0x0001989C File Offset: 0x00017A9C
		[MonoTODO]
		protected internal void CheckReadOperations()
		{
			if (!this.IsConnected)
			{
				throw new InvalidOperationException("Pipe is not connected");
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException("The pipe stream does not support read operations");
			}
		}

		/// <summary>Verifies that the pipe is in a connected state for write operations.</summary>
		// Token: 0x060005F3 RID: 1523 RVA: 0x000198D8 File Offset: 0x00017AD8
		[MonoTODO]
		protected internal void CheckWriteOperations()
		{
			if (!this.IsConnected)
			{
				throw new InvalidOperationException("Pipe us not connected");
			}
			if (!this.CanWrite)
			{
				throw new NotSupportedException("The pipe stream does not support write operations");
			}
		}

		/// <summary>Initializes a <see cref="T:System.IO.Pipes.PipeStream" /> object from the specified <see cref="T:Microsoft.Win32.SafeHandles.SafePipeHandle" /> object.</summary>
		/// <param name="handle">The <see cref="T:Microsoft.Win32.SafeHandles.SafePipeHandle" /> object of the pipe to initialize.</param>
		/// <param name="isExposed">true to expose the handle; otherwise, false.</param>
		/// <param name="isAsync">true to indicate that the handle was opened asynchronously; otherwise, false.</param>
		/// <exception cref="T:System.IO.IOException">A handle cannot be bound to the pipe.</exception>
		// Token: 0x060005F4 RID: 1524 RVA: 0x00019914 File Offset: 0x00017B14
		protected void InitializeHandle(SafePipeHandle handle, bool isExposed, bool isAsync)
		{
			this.handle = handle;
			this.IsHandleExposed = isExposed;
			this.IsAsync = isAsync;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.Pipes.PipeStream" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060005F5 RID: 1525 RVA: 0x0001992C File Offset: 0x00017B2C
		protected override void Dispose(bool disposing)
		{
			if (this.handle != null && disposing)
			{
				this.handle.Dispose();
			}
		}

		/// <summary>Gets the length of a stream, in bytes.</summary>
		/// <returns>0 in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x0001994C File Offset: 0x00017B4C
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the current position of the current stream.</summary>
		/// <returns>0 in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00019954 File Offset: 0x00017B54
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00019958 File Offset: 0x00017B58
		public override long Position
		{
			get
			{
				return 0L;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Sets the length of the current stream to the specified value.</summary>
		/// <param name="value">The new length of the stream.</param>
		// Token: 0x060005F9 RID: 1529 RVA: 0x00019960 File Offset: 0x00017B60
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		/// <summary>Sets the current position of the current stream to the specified value.</summary>
		/// <returns>The new position in the stream.</returns>
		/// <param name="offset">The point, relative to <paramref name="origin" />, to begin seeking from.</param>
		/// <param name="origin">Specifies the beginning, the end, or the current position as a reference point for <paramref name="offset" />, using a value of type <see cref="T:System.IO.SeekOrigin" />.</param>
		// Token: 0x060005FA RID: 1530 RVA: 0x00019968 File Offset: 0x00017B68
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a <see cref="T:System.IO.Pipes.PipeSecurity" /> object that encapsulates the access control list (ACL) entries for the pipe described by the current <see cref="T:System.IO.Pipes.PipeStream" /> object.</summary>
		/// <returns>A <see cref="T:System.IO.Pipes.PipeSecurity" /> object that encapsulates the access control list (ACL) entries for the pipe described by the current <see cref="T:System.IO.Pipes.PipeStream" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The underlying call to set security information failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying call to set security information failed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying call to set security information failed.</exception>
		// Token: 0x060005FB RID: 1531 RVA: 0x00019970 File Offset: 0x00017B70
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeSecurity GetAccessControl()
		{
			throw this.ThrowACLException();
		}

		/// <summary>Applies the access control list (ACL) entries specified by a <see cref="T:System.IO.Pipes.PipeSecurity" /> object to the pipe specified by the current <see cref="T:System.IO.Pipes.PipeStream" /> object.</summary>
		/// <param name="pipeSecurity">A <see cref="T:System.IO.Pipes.PipeSecurity" /> object that specifies an access control list (ACL) entry to apply to the current pipe.</param>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pipeSecurity" /> is null.</exception>
		/// <exception cref="T:System.UnauthorizedAccessException">The underlying call to set security information failed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The underlying call to set security information failed.</exception>
		/// <exception cref="T:System.NotSupportedException">The underlying call to set security information failed.</exception>
		// Token: 0x060005FC RID: 1532 RVA: 0x00019978 File Offset: 0x00017B78
		[MonoNotSupported("ACL is not supported in Mono")]
		public void SetAccessControl(PipeSecurity pipeSecurity)
		{
			throw this.ThrowACLException();
		}

		/// <summary>Waits for the other end of the pipe to read all sent bytes.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support write operations.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x060005FD RID: 1533 RVA: 0x00019980 File Offset: 0x00017B80
		public void WaitForPipeDrain()
		{
		}

		/// <summary>Reads a block of bytes from a stream and writes the data to a specified buffer.</summary>
		/// <returns>The total number of bytes that are read into <paramref name="buffer" />. This might be less than the number of bytes requested if that number of bytes is not currently available, or 0 if the end of the stream is reached.</returns>
		/// <param name="buffer">When this method returns, contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
		/// <param name="offset">The byte offset in the <paramref name="buffer" /> array at which the bytes that are read will be placed.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or-<paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is greater than the number of bytes available in <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support read operations.</exception>
		/// <exception cref="T:System.InvalidOperationException">The pipe is disconnected, waiting to connect, or the handle has not been set.</exception>
		/// <exception cref="T:System.IO.IOException">Any I/O error occurred.</exception>
		// Token: 0x060005FE RID: 1534 RVA: 0x00019984 File Offset: 0x00017B84
		[MonoTODO]
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckReadOperations();
			return this.Stream.Read(buffer, offset, count);
		}

		/// <summary>Reads a byte from a pipe.</summary>
		/// <returns>The byte, cast to <see cref="T:System.Int32" />, or -1 indicates the end of the stream (the pipe has been closed).</returns>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support read operations.</exception>
		/// <exception cref="T:System.InvalidOperationException">The pipe is disconnected, waiting to connect, or the handle has not been set.</exception>
		/// <exception cref="T:System.IO.IOException">Any I/O error occurred.</exception>
		// Token: 0x060005FF RID: 1535 RVA: 0x000199A8 File Offset: 0x00017BA8
		[MonoTODO]
		public override int ReadByte()
		{
			this.CheckReadOperations();
			return this.Stream.ReadByte();
		}

		/// <summary>Writes a block of bytes to the current stream using data from a buffer.</summary>
		/// <param name="buffer">The buffer that contains data to write to the pipe.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The maximum number of bytes to write to the current stream.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or-<paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is greater than the number of bytes available in <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support write operations.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x06000600 RID: 1536 RVA: 0x000199BC File Offset: 0x00017BBC
		[MonoTODO]
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckWriteOperations();
			this.Stream.Write(buffer, offset, count);
		}

		/// <summary>Writes a byte to the current stream.</summary>
		/// <param name="value">The byte to write to the stream.</param>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support write operations.</exception>
		/// <exception cref="T:System.InvalidOperationException">The pipe is disconnected, waiting to connect, or the handle has not been set.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x06000601 RID: 1537 RVA: 0x000199E0 File Offset: 0x00017BE0
		[MonoTODO]
		public override void WriteByte(byte value)
		{
			this.CheckWriteOperations();
			this.Stream.WriteByte(value);
		}

		/// <summary>Clears the buffer for the current stream and causes any buffered data to be written to the underlying device.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support write operations.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x06000602 RID: 1538 RVA: 0x000199F4 File Offset: 0x00017BF4
		[MonoTODO]
		public override void Flush()
		{
			this.CheckWriteOperations();
			this.Stream.Flush();
		}

		/// <summary>Begins an asynchronous read operation.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous read.</returns>
		/// <param name="buffer">The buffer to read data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin reading.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="callback">The method to call when the asynchronous read operation is completed.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous read request from other requests.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or-<paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is greater than the number of bytes available in <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support read operations.</exception>
		/// <exception cref="T:System.InvalidOperationException">The pipe is disconnected, waiting to connect, or the handle has not been set.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x06000603 RID: 1539 RVA: 0x00019A08 File Offset: 0x00017C08
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.read_delegate == null)
			{
				this.read_delegate = new Func<byte[], int, int, int>(this.Read);
			}
			return this.read_delegate.BeginInvoke(buffer, offset, count, callback, state);
		}

		/// <summary>Begins an asynchronous write operation.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that references the asynchronous write operation.</returns>
		/// <param name="buffer">The buffer that contains the data to write to the current stream.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="callback">The method to call when the asynchronous write operation is completed.</param>
		/// <param name="state">A user-provided object that distinguishes this particular asynchronous write request from other requests.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is less than 0.-or-<paramref name="count" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="count" /> is greater than the number of bytes available in <paramref name="buffer" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The pipe is closed.</exception>
		/// <exception cref="T:System.NotSupportedException">The pipe does not support write operations.</exception>
		/// <exception cref="T:System.InvalidOperationException">The pipe is disconnected, waiting to connect, or the handle has not been set.</exception>
		/// <exception cref="T:System.IO.IOException">The pipe is broken or another I/O error occurred.</exception>
		// Token: 0x06000604 RID: 1540 RVA: 0x00019A48 File Offset: 0x00017C48
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.write_delegate == null)
			{
				this.write_delegate = new Action<byte[], int, int>(this.Write);
			}
			return this.write_delegate.BeginInvoke(buffer, offset, count, callback, state);
		}

		/// <summary>Ends a pending asynchronous read request.</summary>
		/// <returns>The number of bytes that were read. A return value of 0 indicates the end of the stream (the pipe has been closed).</returns>
		/// <param name="asyncResult">The reference to the pending asynchronous request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Pipes.PipeStream.BeginRead(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		// Token: 0x06000605 RID: 1541 RVA: 0x00019A88 File Offset: 0x00017C88
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.read_delegate.EndInvoke(asyncResult);
		}

		/// <summary>Ends a pending asynchronous write request.</summary>
		/// <param name="asyncResult">The reference to the pending asynchronous request.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> did not originate from a <see cref="M:System.IO.Pipes.PipeStream.BeginWrite(System.Byte[],System.Int32,System.Int32,System.AsyncCallback,System.Object)" /> method on the current stream. </exception>
		/// <exception cref="T:System.IO.IOException">The stream is closed or an internal error has occurred.</exception>
		// Token: 0x06000606 RID: 1542 RVA: 0x00019A98 File Offset: 0x00017C98
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.write_delegate.EndInvoke(asyncResult);
		}

		// Token: 0x04000198 RID: 408
		internal const int DefaultBufferSize = 1024;

		// Token: 0x04000199 RID: 409
		private PipeDirection direction;

		// Token: 0x0400019A RID: 410
		private PipeTransmissionMode transmission_mode;

		// Token: 0x0400019B RID: 411
		private PipeTransmissionMode read_trans_mode;

		// Token: 0x0400019C RID: 412
		private int buffer_size;

		// Token: 0x0400019D RID: 413
		private SafePipeHandle handle;

		// Token: 0x0400019E RID: 414
		private Stream stream;

		// Token: 0x0400019F RID: 415
		private Func<byte[], int, int, int> read_delegate;

		// Token: 0x040001A0 RID: 416
		private Action<byte[], int, int> write_delegate;
	}
}
