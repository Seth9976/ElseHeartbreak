using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace UnityEngine
{
	// Token: 0x0200015C RID: 348
	public sealed class ComputeBuffer : IDisposable
	{
		// Token: 0x06000F04 RID: 3844 RVA: 0x0001EB24 File Offset: 0x0001CD24
		public ComputeBuffer(int count, int stride)
			: this(count, stride, ComputeBufferType.Default)
		{
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0001EB30 File Offset: 0x0001CD30
		public ComputeBuffer(int count, int stride, ComputeBufferType type)
		{
			this.m_Ptr = IntPtr.Zero;
			ComputeBuffer.InitBuffer(this, count, stride, type);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0001EB4C File Offset: 0x0001CD4C
		~ComputeBuffer()
		{
			this.Dispose(false);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0001EB88 File Offset: 0x0001CD88
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0001EB98 File Offset: 0x0001CD98
		private void Dispose(bool disposing)
		{
			ComputeBuffer.DestroyBuffer(this);
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06000F09 RID: 3849
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitBuffer(ComputeBuffer buf, int count, int stride, ComputeBufferType type);

		// Token: 0x06000F0A RID: 3850
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyBuffer(ComputeBuffer buf);

		// Token: 0x06000F0B RID: 3851 RVA: 0x0001EBAC File Offset: 0x0001CDAC
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000F0C RID: 3852
		public extern int count
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000F0D RID: 3853
		public extern int stride
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0001EBB4 File Offset: 0x0001CDB4
		[SecuritySafeCritical]
		public void SetData(Array data)
		{
			this.InternalSetData(data, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000F0F RID: 3855
		[SecurityCritical]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetData(Array data, int elemSize);

		// Token: 0x06000F10 RID: 3856 RVA: 0x0001EBD8 File Offset: 0x0001CDD8
		[SecuritySafeCritical]
		public void GetData(Array data)
		{
			this.InternalGetData(data, Marshal.SizeOf(data.GetType().GetElementType()));
		}

		// Token: 0x06000F11 RID: 3857
		[WrapperlessIcall]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalGetData(Array data, int elemSize);

		// Token: 0x06000F12 RID: 3858
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CopyCount(ComputeBuffer src, ComputeBuffer dst, int dstOffset);

		// Token: 0x040005E7 RID: 1511
		internal IntPtr m_Ptr;
	}
}
