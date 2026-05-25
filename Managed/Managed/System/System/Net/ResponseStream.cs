using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Net
{
	// Token: 0x020003DC RID: 988
	internal class ResponseStream : Stream
	{
		// Token: 0x060021C5 RID: 8645 RVA: 0x00062D7C File Offset: 0x00060F7C
		internal ResponseStream(Stream stream, HttpListenerResponse response, bool ignore_errors)
		{
			this.response = response;
			this.ignore_errors = ignore_errors;
			this.stream = stream;
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060021C7 RID: 8647 RVA: 0x00062DB4 File Offset: 0x00060FB4
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x00062DB8 File Offset: 0x00060FB8
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x00062DBC File Offset: 0x00060FBC
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x00062DC0 File Offset: 0x00060FC0
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x00062DC8 File Offset: 0x00060FC8
		// (set) Token: 0x060021CC RID: 8652 RVA: 0x00062DD0 File Offset: 0x00060FD0
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

		// Token: 0x060021CD RID: 8653 RVA: 0x00062DD8 File Offset: 0x00060FD8
		public override void Close()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				MemoryStream headers = this.GetHeaders(true);
				bool sendChunked = this.response.SendChunked;
				if (headers != null)
				{
					long position = headers.Position;
					if (sendChunked && !this.trailer_sent)
					{
						byte[] array = ResponseStream.GetChunkSizeBytes(0, true);
						headers.Position = headers.Length;
						headers.Write(array, 0, array.Length);
					}
					this.InternalWrite(headers.GetBuffer(), (int)position, (int)(headers.Length - position));
					this.trailer_sent = true;
				}
				else if (sendChunked && !this.trailer_sent)
				{
					byte[] array = ResponseStream.GetChunkSizeBytes(0, true);
					this.InternalWrite(array, 0, array.Length);
					this.trailer_sent = true;
				}
				this.response.Close();
			}
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x00062EA4 File Offset: 0x000610A4
		private MemoryStream GetHeaders(bool closing)
		{
			if (this.response.HeadersSent)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			this.response.SendHeaders(closing, memoryStream);
			return memoryStream;
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x00062ED8 File Offset: 0x000610D8
		public override void Flush()
		{
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x00062EDC File Offset: 0x000610DC
		private static byte[] GetChunkSizeBytes(int size, bool final)
		{
			string text = string.Format("{0:x}\r\n{1}", size, (!final) ? string.Empty : "\r\n");
			return Encoding.ASCII.GetBytes(text);
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x00062F1C File Offset: 0x0006111C
		internal void InternalWrite(byte[] buffer, int offset, int count)
		{
			if (this.ignore_errors)
			{
				try
				{
					this.stream.Write(buffer, offset, count);
				}
				catch
				{
				}
			}
			else
			{
				this.stream.Write(buffer, offset, count);
			}
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x00062F7C File Offset: 0x0006117C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			MemoryStream headers = this.GetHeaders(false);
			bool sendChunked = this.response.SendChunked;
			if (headers != null)
			{
				long position = headers.Position;
				headers.Position = headers.Length;
				if (sendChunked)
				{
					byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
					headers.Write(array, 0, array.Length);
				}
				int num = Math.Min(count, 16384 - (int)headers.Position + (int)position);
				headers.Write(buffer, offset, num);
				count -= num;
				offset += num;
				this.InternalWrite(headers.GetBuffer(), (int)position, (int)(headers.Length - position));
				headers.SetLength(0L);
				headers.Capacity = 0;
			}
			else if (sendChunked)
			{
				byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
				this.InternalWrite(array, 0, array.Length);
			}
			if (count > 0)
			{
				this.InternalWrite(buffer, offset, count);
			}
			if (sendChunked)
			{
				this.InternalWrite(ResponseStream.crlf, 0, 2);
			}
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x00063084 File Offset: 0x00061284
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			MemoryStream headers = this.GetHeaders(false);
			bool sendChunked = this.response.SendChunked;
			if (headers != null)
			{
				long position = headers.Position;
				headers.Position = headers.Length;
				if (sendChunked)
				{
					byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
					headers.Write(array, 0, array.Length);
				}
				headers.Write(buffer, offset, count);
				buffer = headers.GetBuffer();
				offset = (int)position;
				count = (int)(headers.Position - position);
			}
			else if (sendChunked)
			{
				byte[] array = ResponseStream.GetChunkSizeBytes(count, false);
				this.InternalWrite(array, 0, array.Length);
			}
			return this.stream.BeginWrite(buffer, offset, count, cback, state);
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x00063148 File Offset: 0x00061348
		public override void EndWrite(IAsyncResult ares)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
			if (this.ignore_errors)
			{
				try
				{
					this.stream.EndWrite(ares);
					if (this.response.SendChunked)
					{
						this.stream.Write(ResponseStream.crlf, 0, 2);
					}
				}
				catch
				{
				}
			}
			else
			{
				this.stream.EndWrite(ares);
				if (this.response.SendChunked)
				{
					this.stream.Write(ResponseStream.crlf, 0, 2);
				}
			}
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x00063204 File Offset: 0x00061404
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x0006320C File Offset: 0x0006140C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback cback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x00063214 File Offset: 0x00061414
		public override int EndRead(IAsyncResult ares)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0006321C File Offset: 0x0006141C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x00063224 File Offset: 0x00061424
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040014ED RID: 5357
		private HttpListenerResponse response;

		// Token: 0x040014EE RID: 5358
		private bool ignore_errors;

		// Token: 0x040014EF RID: 5359
		private bool disposed;

		// Token: 0x040014F0 RID: 5360
		private bool trailer_sent;

		// Token: 0x040014F1 RID: 5361
		private Stream stream;

		// Token: 0x040014F2 RID: 5362
		private static byte[] crlf = new byte[] { 13, 10 };
	}
}
