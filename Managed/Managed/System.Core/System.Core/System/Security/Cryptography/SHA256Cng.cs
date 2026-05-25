using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Secure Hash Algorithm (SHA) for 256-bit hash values.</summary>
	// Token: 0x0200005D RID: 93
	public sealed class SHA256Cng : SHA256
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA256Cng" /> class. </summary>
		// Token: 0x06000538 RID: 1336 RVA: 0x0001824C File Offset: 0x0001644C
		[SecurityCritical]
		public SHA256Cng()
		{
			this.hash = new SHA256Managed();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x0600053A RID: 1338 RVA: 0x00018270 File Offset: 0x00016470
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00018280 File Offset: 0x00016480
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00018294 File Offset: 0x00016494
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA256Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000182CC File Offset: 0x000164CC
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000158 RID: 344
		private static byte[] Empty = new byte[0];

		// Token: 0x04000159 RID: 345
		private SHA256 hash;
	}
}
