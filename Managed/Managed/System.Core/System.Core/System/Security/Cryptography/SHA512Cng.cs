using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Secure Hash Algorithm (SHA) for 512-bit hash values.</summary>
	// Token: 0x02000061 RID: 97
	public sealed class SHA512Cng : SHA512
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA512Cng" /> class. </summary>
		// Token: 0x06000550 RID: 1360 RVA: 0x0001849C File Offset: 0x0001669C
		[SecurityCritical]
		public SHA512Cng()
		{
			this.hash = new SHA512Managed();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x06000552 RID: 1362 RVA: 0x000184C0 File Offset: 0x000166C0
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000184D0 File Offset: 0x000166D0
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000184E4 File Offset: 0x000166E4
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA512Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001851C File Offset: 0x0001671C
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x04000160 RID: 352
		private static byte[] Empty = new byte[0];

		// Token: 0x04000161 RID: 353
		private SHA512 hash;
	}
}
