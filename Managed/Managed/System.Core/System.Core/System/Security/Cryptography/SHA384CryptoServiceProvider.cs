using System;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.SHA384" /> algorithm. </summary>
	// Token: 0x02000060 RID: 96
	public sealed class SHA384CryptoServiceProvider : SHA384
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA384CryptoServiceProvider" /> class. </summary>
		// Token: 0x0600054A RID: 1354 RVA: 0x00018408 File Offset: 0x00016608
		[SecurityCritical]
		public SHA384CryptoServiceProvider()
		{
			this.hash = new SHA384Managed();
		}

		/// <summary>Initializes, or reinitializes, an instance of a hash algorithm.</summary>
		// Token: 0x0600054C RID: 1356 RVA: 0x0001842C File Offset: 0x0001662C
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001843C File Offset: 0x0001663C
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00018450 File Offset: 0x00016650
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA384CryptoServiceProvider.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00018488 File Offset: 0x00016688
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x0400015E RID: 350
		private static byte[] Empty = new byte[0];

		// Token: 0x0400015F RID: 351
		private SHA384 hash;
	}
}
