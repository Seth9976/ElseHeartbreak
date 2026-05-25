using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Performs a cryptographic transformation of data. This class cannot be inherited.</summary>
	// Token: 0x0200059B RID: 1435
	[ComVisible(true)]
	public sealed class CryptoAPITransform : IDisposable, ICryptoTransform
	{
		// Token: 0x06003754 RID: 14164 RVA: 0x000B2BC8 File Offset: 0x000B0DC8
		internal CryptoAPITransform()
		{
			this.m_disposed = false;
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		// Token: 0x06003755 RID: 14165 RVA: 0x000B2BD8 File Offset: 0x000B0DD8
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets a value indicating whether the current transform can be reused.</summary>
		/// <returns>Always true.</returns>
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06003756 RID: 14166 RVA: 0x000B2BE8 File Offset: 0x000B0DE8
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether multiple blocks can be transformed.</summary>
		/// <returns>true if multiple blocks can be transformed; otherwise, false.</returns>
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06003757 RID: 14167 RVA: 0x000B2BEC File Offset: 0x000B0DEC
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the input block size.</summary>
		/// <returns>The input block size in bytes.</returns>
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06003758 RID: 14168 RVA: 0x000B2BF0 File Offset: 0x000B0DF0
		public int InputBlockSize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the key handle.</summary>
		/// <returns>The key handle.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000B2BF4 File Offset: 0x000B0DF4
		public IntPtr KeyHandle
		{
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
			get
			{
				return IntPtr.Zero;
			}
		}

		/// <summary>Gets the output block size.</summary>
		/// <returns>The output block size in bytes.</returns>
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x0600375A RID: 14170 RVA: 0x000B2BFC File Offset: 0x000B0DFC
		public int OutputBlockSize
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.CryptoAPITransform" /> method.</summary>
		// Token: 0x0600375B RID: 14171 RVA: 0x000B2C00 File Offset: 0x000B0E00
		public void Clear()
		{
			this.Dispose(false);
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x000B2C0C File Offset: 0x000B0E0C
		private void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
				}
				this.m_disposed = true;
			}
		}

		/// <summary>Computes the transformation for the specified region of the input byte array and copies the resulting transformation to the specified region of the output byte array.</summary>
		/// <returns>The number of bytes written.</returns>
		/// <param name="inputBuffer">The input on which to perform the operation on. </param>
		/// <param name="inputOffset">The offset into the input byte array from which to begin using data from. </param>
		/// <param name="inputCount">The number of bytes in the input byte array to use as data. </param>
		/// <param name="outputBuffer">The output to which to write the data to. </param>
		/// <param name="outputOffset">The offset into the output byte array from which to begin writing data from. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is null.-or- The <paramref name="outputBuffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The length of the input buffer is less than the sum of the input offset and the input count. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="inputOffset" /> is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x0600375D RID: 14173 RVA: 0x000B2C28 File Offset: 0x000B0E28
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			return 0;
		}

		/// <summary>Computes the transformation for the specified region of the specified byte array.</summary>
		/// <returns>The computed transformation.</returns>
		/// <param name="inputBuffer">The input on which to perform the operation on. </param>
		/// <param name="inputOffset">The offset into the byte array from which to begin using data from. </param>
		/// <param name="inputCount">The number of bytes in the byte array to use as data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputBuffer" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="inputOffset" /> parameter is less than zero.-or- The <paramref name="inputCount" /> parameter is less than zero.-or- The length of the input buffer is less than the sum of the input offset and the input count. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="F:System.Security.Cryptography.PaddingMode.PKCS7" /> padding is invalid. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="inputOffset" /> parameter is out of range. This parameter requires a non-negative number.</exception>
		// Token: 0x0600375E RID: 14174 RVA: 0x000B2C2C File Offset: 0x000B0E2C
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			return null;
		}

		/// <summary>Resets the internal state of <see cref="T:System.Security.Cryptography.CryptoAPITransform" /> so that it can be used again to do a different encryption or decryption.</summary>
		// Token: 0x0600375F RID: 14175 RVA: 0x000B2C30 File Offset: 0x000B0E30
		[ComVisible(false)]
		public void Reset()
		{
		}

		// Token: 0x04001778 RID: 6008
		private bool m_disposed;
	}
}
