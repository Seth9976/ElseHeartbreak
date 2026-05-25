using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a CNG (Cryptography Next Generation) implementation of the MD5 (Message Digest 5) 128-bit hashing algorithm.</summary>
	// Token: 0x0200005B RID: 91
	public sealed class MD5Cng : MD5
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.MD5Cng" /> class. </summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The FIPS Windows security setting is enabled.</exception>
		// Token: 0x0600052C RID: 1324 RVA: 0x00018124 File Offset: 0x00016324
		[SecurityCritical]
		public MD5Cng()
		{
			this.hash = new MD5CryptoServiceProvider();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x0600052E RID: 1326 RVA: 0x00018148 File Offset: 0x00016348
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00018158 File Offset: 0x00016358
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001816C File Offset: 0x0001636C
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(MD5Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000181A4 File Offset: 0x000163A4
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000154 RID: 340
		private static byte[] Empty = new byte[0];

		// Token: 0x04000155 RID: 341
		private MD5 hash;
	}
}
