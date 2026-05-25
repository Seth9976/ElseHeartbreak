using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Secure Hash Algorithm (SHA) for 384-bit hash values.</summary>
	// Token: 0x0200005F RID: 95
	public sealed class SHA384Cng : SHA384
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA384Cng" /> class. </summary>
		// Token: 0x06000544 RID: 1348 RVA: 0x00018374 File Offset: 0x00016574
		[SecurityCritical]
		public SHA384Cng()
		{
			this.hash = new SHA384Managed();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x06000546 RID: 1350 RVA: 0x00018398 File Offset: 0x00016598
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000183A8 File Offset: 0x000165A8
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000183BC File Offset: 0x000165BC
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA384Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000183F4 File Offset: 0x000165F4
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x0400015C RID: 348
		private static byte[] Empty = new byte[0];

		// Token: 0x0400015D RID: 349
		private SHA384 hash;
	}
}
