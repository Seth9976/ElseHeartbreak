using System;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace System.Net
{
	// Token: 0x02000308 RID: 776
	internal class FtpDataStream : Stream, IDisposable
	{
		// Token: 0x06001ABE RID: 6846 RVA: 0x0004B5BC File Offset: 0x000497BC
		internal FtpDataStream(FtpWebRequest request, Stream stream, bool isRead)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.request = request;
			this.networkStream = stream;
			this.isRead = isRead;
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x0004B5F8 File Offset: 0x000497F8
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x0004B608 File Offset: 0x00049808
		public override bool CanRead
		{
			get
			{
				return this.isRead;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x0004B610 File Offset: 0x00049810
		public override bool CanWrite
		{
			get
			{
				return !this.isRead;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x0004B61C File Offset: 0x0004981C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x0004B620 File Offset: 0x00049820
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x0004B628 File Offset: 0x00049828
		// (set) Token: 0x06001AC5 RID: 6853 RVA: 0x0004B630 File Offset: 0x00049830
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001AC6 RID: 6854 RVA: 0x0004B638 File Offset: 0x00049838
		internal Stream NetworkStream
		{
			get
			{
				this.CheckDisposed();
				return this.networkStream;
			}
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0004B648 File Offset: 0x00049848
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0004B654 File Offset: 0x00049854
		public override void Flush()
		{
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x0004B658 File Offset: 0x00049858
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0004B660 File Offset: 0x00049860
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0004B668 File Offset: 0x00049868
		private int ReadInternal(byte[] buffer, int offset, int size)
		{
			int num = 0;
			this.request.CheckIfAborted();
			try
			{
				num = this.networkStream.Read(buffer, offset, size);
			}
			catch (IOException)
			{
				throw new ProtocolViolationException("Server commited a protocol violation");
			}
			this.totalRead += num;
			if (num == 0)
			{
				this.networkStream = null;
				this.request.CloseDataConnection();
				this.request.SetTransferCompleted();
			}
			return num;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0004B6F8 File Offset: 0x000498F8
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			this.CheckDisposed();
			if (!this.isRead)
			{
				throw new NotSupportedException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("offset+size");
			}
			FtpDataStream.ReadDelegate readDelegate = new FtpDataStream.ReadDelegate(this.ReadInternal);
			return readDelegate.BeginInvoke(buffer, offset, size, cb, state);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0004B780 File Offset: 0x00049980
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid asyncResult", "asyncResult");
			}
			FtpDataStream.ReadDelegate readDelegate = asyncResult2.AsyncDelegate as FtpDataStream.ReadDelegate;
			if (readDelegate == null)
			{
				throw new ArgumentException("Invalid asyncResult", "asyncResult");
			}
			return readDelegate.EndInvoke(asyncResult);
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0004B7E4 File Offset: 0x000499E4
		public override int Read(byte[] buffer, int offset, int size)
		{
			this.request.CheckIfAborted();
			IAsyncResult asyncResult = this.BeginRead(buffer, offset, size, null, null);
			if (!asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne(this.request.ReadWriteTimeout, false))
			{
				throw new WebException("Read timed out.", WebExceptionStatus.Timeout);
			}
			return this.EndRead(asyncResult);
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0004B844 File Offset: 0x00049A44
		private void WriteInternal(byte[] buffer, int offset, int size)
		{
			this.request.CheckIfAborted();
			try
			{
				this.networkStream.Write(buffer, offset, size);
			}
			catch (IOException)
			{
				throw new ProtocolViolationException();
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0004B898 File Offset: 0x00049A98
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback cb, object state)
		{
			this.CheckDisposed();
			if (this.isRead)
			{
				throw new NotSupportedException();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (size < 0 || size > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("offset+size");
			}
			FtpDataStream.WriteDelegate writeDelegate = new FtpDataStream.WriteDelegate(this.WriteInternal);
			return writeDelegate.BeginInvoke(buffer, offset, size, cb, state);
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0004B920 File Offset: 0x00049B20
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			AsyncResult asyncResult2 = asyncResult as AsyncResult;
			if (asyncResult2 == null)
			{
				throw new ArgumentException("Invalid asyncResult.", "asyncResult");
			}
			FtpDataStream.WriteDelegate writeDelegate = asyncResult2.AsyncDelegate as FtpDataStream.WriteDelegate;
			if (writeDelegate == null)
			{
				throw new ArgumentException("Invalid asyncResult.", "asyncResult");
			}
			writeDelegate.EndInvoke(asyncResult);
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0004B984 File Offset: 0x00049B84
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.request.CheckIfAborted();
			IAsyncResult asyncResult = this.BeginWrite(buffer, offset, size, null, null);
			if (!asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne(this.request.ReadWriteTimeout, false))
			{
				throw new WebException("Read timed out.", WebExceptionStatus.Timeout);
			}
			this.EndWrite(asyncResult);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0004B9E4 File Offset: 0x00049BE4
		~FtpDataStream()
		{
			this.Dispose(false);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0004BA20 File Offset: 0x00049C20
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			if (this.networkStream != null)
			{
				this.request.CloseDataConnection();
				this.request.SetTransferCompleted();
				this.request = null;
				this.networkStream = null;
			}
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0004BA70 File Offset: 0x00049C70
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x04001076 RID: 4214
		private FtpWebRequest request;

		// Token: 0x04001077 RID: 4215
		private Stream networkStream;

		// Token: 0x04001078 RID: 4216
		private bool disposed;

		// Token: 0x04001079 RID: 4217
		private bool isRead;

		// Token: 0x0400107A RID: 4218
		private int totalRead;

		// Token: 0x020004E8 RID: 1256
		// (Invoke) Token: 0x06002C4C RID: 11340
		private delegate void WriteDelegate(byte[] buffer, int offset, int size);

		// Token: 0x020004E9 RID: 1257
		// (Invoke) Token: 0x06002C50 RID: 11344
		private delegate int ReadDelegate(byte[] buffer, int offset, int size);
	}
}
