using System;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.SHA256" /> algorithm. </summary>
	// Token: 0x0200005E RID: 94
	public sealed class SHA256CryptoServiceProvider : SHA256
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA256CryptoServiceProvider" /> class. </summary>
		// Token: 0x0600053E RID: 1342 RVA: 0x000182E0 File Offset: 0x000164E0
		[SecurityCritical]
		public SHA256CryptoServiceProvider()
		{
			this.hash = new SHA256Managed();
		}

		/// <summary>Initializes, or reinitializes, an instance of a hash algorithm.</summary>
		// Token: 0x06000540 RID: 1344 RVA: 0x00018304 File Offset: 0x00016504
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00018314 File Offset: 0x00016514
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00018328 File Offset: 0x00016528
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA256CryptoServiceProvider.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00018360 File Offset: 0x00016560
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x0400015A RID: 346
		private static byte[] Empty = new byte[0];

		// Token: 0x0400015B RID: 347
		private SHA256 hash;
	}
}
