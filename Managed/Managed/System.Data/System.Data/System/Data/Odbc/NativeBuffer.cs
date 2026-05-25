using System;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x02000118 RID: 280
	internal sealed class NativeBuffer : IDisposable
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x0003D514 File Offset: 0x0003B714
		// (set) Token: 0x06000FA9 RID: 4009 RVA: 0x0003D51C File Offset: 0x0003B71C
		public IntPtr Handle
		{
			get
			{
				return this._ptr;
			}
			set
			{
				this._ptr = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0003D528 File Offset: 0x0003B728
		public int Size
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0003D530 File Offset: 0x0003B730
		public void AllocBuffer(int length)
		{
			this.FreeBuffer();
			this._ptr = Marshal.AllocCoTaskMem(length);
			this._length = length;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0003D54C File Offset: 0x0003B74C
		public void FreeBuffer()
		{
			if (this._ptr == IntPtr.Zero)
			{
				return;
			}
			Marshal.FreeCoTaskMem(this._ptr);
			this._length = 0;
			this._ptr = IntPtr.Zero;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0003D584 File Offset: 0x0003B784
		public void EnsureAlloc(int length)
		{
			if (this.Size == length && this._ptr != IntPtr.Zero)
			{
				return;
			}
			this.AllocBuffer(length);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0003D5B0 File Offset: 0x0003B7B0
		public void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.FreeBuffer();
			this._ptr = IntPtr.Zero;
			this.disposed = true;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0003D5E4 File Offset: 0x0003B7E4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0003D5F4 File Offset: 0x0003B7F4
		~NativeBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0003D630 File Offset: 0x0003B830
		public static implicit operator IntPtr(NativeBuffer buf)
		{
			return buf.Handle;
		}

		// Token: 0x04000538 RID: 1336
		private IntPtr _ptr;

		// Token: 0x04000539 RID: 1337
		private int _length;

		// Token: 0x0400053A RID: 1338
		private bool disposed;
	}
}
