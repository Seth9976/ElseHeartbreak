using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Secure Hash Algorithm (SHA).</summary>
	// Token: 0x0200005C RID: 92
	public sealed class SHA1Cng : SHA1
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA1Cng" /> class. </summary>
		// Token: 0x06000532 RID: 1330 RVA: 0x000181B8 File Offset: 0x000163B8
		[SecurityCritical]
		public SHA1Cng()
		{
			this.hash = new SHA1Managed();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x06000534 RID: 1332 RVA: 0x000181DC File Offset: 0x000163DC
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000181EC File Offset: 0x000163EC
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00018200 File Offset: 0x00016400
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA1Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00018238 File Offset: 0x00016438
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000156 RID: 342
		private static byte[] Empty = new byte[0];

		// Token: 0x04000157 RID: 343
		private SHA1 hash;
	}
}
