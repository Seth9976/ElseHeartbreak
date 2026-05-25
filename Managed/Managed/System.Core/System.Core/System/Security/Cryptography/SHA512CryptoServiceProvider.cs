using System;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.SHA512" /> algorithm. </summary>
	// Token: 0x02000062 RID: 98
	public sealed class SHA512CryptoServiceProvider : SHA512
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA512CryptoServiceProvider" /> class. </summary>
		// Token: 0x06000556 RID: 1366 RVA: 0x00018530 File Offset: 0x00016730
		[SecurityCritical]
		public SHA512CryptoServiceProvider()
		{
			this.hash = new SHA512Managed();
		}

		/// <summary>Initializes, or reinitializes, an instance of a hash algorithm.</summary>
		// Token: 0x06000558 RID: 1368 RVA: 0x00018554 File Offset: 0x00016754
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00018564 File Offset: 0x00016764
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00018578 File Offset: 0x00016778
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA512CryptoServiceProvider.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000185B0 File Offset: 0x000167B0
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000162 RID: 354
		private static byte[] Empty = new byte[0];

		// Token: 0x04000163 RID: 355
		private SHA512 hash;
	}
}
