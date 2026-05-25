using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200015A RID: 346
	public sealed class ComputeShader : Object
	{
		// Token: 0x06000EF8 RID: 3832
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int FindKernel(string name);

		// Token: 0x06000EF9 RID: 3833
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(string name, float val);

		// Token: 0x06000EFA RID: 3834
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetInt(string name, int val);

		// Token: 0x06000EFB RID: 3835 RVA: 0x0001EB00 File Offset: 0x0001CD00
		public void SetVector(string name, Vector4 val)
		{
			ComputeShader.INTERNAL_CALL_SetVector(this, name, ref val);
		}

		// Token: 0x06000EFC RID: 3836
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetVector(ComputeShader self, string name, ref Vector4 val);

		// Token: 0x06000EFD RID: 3837 RVA: 0x0001EB0C File Offset: 0x0001CD0C
		public void SetFloats(string name, params float[] values)
		{
			this.Internal_SetFloats(name, values);
		}

		// Token: 0x06000EFE RID: 3838
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetFloats(string name, float[] values);

		// Token: 0x06000EFF RID: 3839 RVA: 0x0001EB18 File Offset: 0x0001CD18
		public void SetInts(string name, params int[] values)
		{
			this.Internal_SetInts(name, values);
		}

		// Token: 0x06000F00 RID: 3840
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_SetInts(string name, int[] values);

		// Token: 0x06000F01 RID: 3841
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(int kernelIndex, string name, Texture texture);

		// Token: 0x06000F02 RID: 3842
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBuffer(int kernelIndex, string name, ComputeBuffer buffer);

		// Token: 0x06000F03 RID: 3843
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Dispatch(int kernelIndex, int threadsX, int threadsY, int threadsZ);
	}
}
