using System;

namespace System.Security.Cryptography
{
	/// <summary>Encapsulates the name of an encryption algorithm. </summary>
	// Token: 0x02000059 RID: 89
	[Serializable]
	public sealed class CngAlgorithm : IEquatable<CngAlgorithm>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngAlgorithm" /> class.</summary>
		/// <param name="algorithm">The name of the algorithm to initialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithm" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="algorithm" /> parameter length is 0 (zero).</exception>
		// Token: 0x0600050C RID: 1292 RVA: 0x00017C0C File Offset: 0x00015E0C
		public CngAlgorithm(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (algorithm.Length == 0)
			{
				throw new ArgumentException("algorithm");
			}
			this.algo = algorithm;
		}

		/// <summary>Gets the algorithm name that the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object specifies.</summary>
		/// <returns>The embedded algorithm name.</returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00017C50 File Offset: 0x00015E50
		public string Algorithm
		{
			get
			{
				return this.algo;
			}
		}

		/// <summary>Compares the specified <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object to the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object. </summary>
		/// <returns>true if the <paramref name="other" /> parameter specifies the same algorithm as the current object; otherwise, false.</returns>
		/// <param name="other">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object.</param>
		// Token: 0x0600050E RID: 1294 RVA: 0x00017C58 File Offset: 0x00015E58
		public bool Equals(CngAlgorithm other)
		{
			return !(other == null) && this.algo == other.algo;
		}

		/// <summary>Compares the specified object to the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter is a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> that specifies the same algorithm as the current object; otherwise, false.</returns>
		/// <param name="obj">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object.</param>
		// Token: 0x0600050F RID: 1295 RVA: 0x00017C7C File Offset: 0x00015E7C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngAlgorithm);
		}

		/// <summary>Generates a hash value for the algorithm name that is embedded in the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object.</summary>
		/// <returns>The hash value of the embedded algorithm name.</returns>
		// Token: 0x06000510 RID: 1296 RVA: 0x00017C8C File Offset: 0x00015E8C
		public override int GetHashCode()
		{
			return this.algo.GetHashCode();
		}

		/// <summary>Gets the name of the algorithm that the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object specifies.</summary>
		/// <returns>The embedded algorithm name.</returns>
		// Token: 0x06000511 RID: 1297 RVA: 0x00017C9C File Offset: 0x00015E9C
		public override string ToString()
		{
			return this.algo;
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Diffie-Hellman (ECDH) key exchange algorithm that uses the P-256 curve.</summary>
		/// <returns>An object that specifies an ECDH algorithm that uses the P-256 curve.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00017CA4 File Offset: 0x00015EA4
		public static CngAlgorithm ECDiffieHellmanP256
		{
			get
			{
				if (CngAlgorithm.dh256 == null)
				{
					CngAlgorithm.dh256 = new CngAlgorithm("ECDH_P256");
				}
				return CngAlgorithm.dh256;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Diffie-Hellman (ECDH) key exchange algorithm that uses the P-384 curve.</summary>
		/// <returns>An object that specifies an ECDH algorithm that uses the P-384 curve.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00017CD8 File Offset: 0x00015ED8
		public static CngAlgorithm ECDiffieHellmanP384
		{
			get
			{
				if (CngAlgorithm.dh384 == null)
				{
					CngAlgorithm.dh384 = new CngAlgorithm("ECDH_P384");
				}
				return CngAlgorithm.dh384;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Diffie-Hellman (ECDH) key exchange algorithm that uses the P-521 curve.</summary>
		/// <returns>An object that specifies an ECDH algorithm that uses the P-521 curve.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00017D0C File Offset: 0x00015F0C
		public static CngAlgorithm ECDiffieHellmanP521
		{
			get
			{
				if (CngAlgorithm.dh521 == null)
				{
					CngAlgorithm.dh521 = new CngAlgorithm("ECDH_P521");
				}
				return CngAlgorithm.dh521;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Digital Signature Algorithm (ECDSA) that uses the P-256 curve.</summary>
		/// <returns>An object that specifies an ECDSA algorithm that uses the P-256 curve.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00017D40 File Offset: 0x00015F40
		public static CngAlgorithm ECDsaP256
		{
			get
			{
				if (CngAlgorithm.dsa256 == null)
				{
					CngAlgorithm.dsa256 = new CngAlgorithm("ECDSA_P256");
				}
				return CngAlgorithm.dsa256;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Digital Signature Algorithm (ECDSA) that uses the P-384 curve.</summary>
		/// <returns>An object that specifies an ECDSA algorithm that uses the P-384 curve.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00017D74 File Offset: 0x00015F74
		public static CngAlgorithm ECDsaP384
		{
			get
			{
				if (CngAlgorithm.dsa384 == null)
				{
					CngAlgorithm.dsa384 = new CngAlgorithm("ECDSA_P384");
				}
				return CngAlgorithm.dsa384;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies an Elliptic Curve Digital Signature Algorithm (ECDSA) that uses the P-521 curve.</summary>
		/// <returns>An object that specifies an ECDSA algorithm that uses the P-521 curve.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00017DA8 File Offset: 0x00015FA8
		public static CngAlgorithm ECDsaP521
		{
			get
			{
				if (CngAlgorithm.dsa521 == null)
				{
					CngAlgorithm.dsa521 = new CngAlgorithm("ECDSA_P521");
				}
				return CngAlgorithm.dsa521;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Message Digest 5 (MD5) hash algorithm.</summary>
		/// <returns>An object that specifies the MD5 algorithm.</returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00017DDC File Offset: 0x00015FDC
		public static CngAlgorithm MD5
		{
			get
			{
				if (CngAlgorithm.md5 == null)
				{
					CngAlgorithm.md5 = new CngAlgorithm("MD5");
				}
				return CngAlgorithm.md5;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Secure Hash Algorithm 1 (SHA-1) algorithm.</summary>
		/// <returns>An object that specifies the SHA-1 algorithm.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00017E10 File Offset: 0x00016010
		public static CngAlgorithm Sha1
		{
			get
			{
				if (CngAlgorithm.sha1 == null)
				{
					CngAlgorithm.sha1 = new CngAlgorithm("SHA1");
				}
				return CngAlgorithm.sha1;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Secure Hash Algorithm 256 (SHA-256) algorithm.</summary>
		/// <returns>An object that specifies the SHA-256 algorithm.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00017E44 File Offset: 0x00016044
		public static CngAlgorithm Sha256
		{
			get
			{
				if (CngAlgorithm.sha256 == null)
				{
					CngAlgorithm.sha256 = new CngAlgorithm("SHA256");
				}
				return CngAlgorithm.sha256;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Secure Hash Algorithm 384 (SHA-384) algorithm.</summary>
		/// <returns>An object that specifies the SHA-384 algorithm.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00017E78 File Offset: 0x00016078
		public static CngAlgorithm Sha384
		{
			get
			{
				if (CngAlgorithm.sha384 == null)
				{
					CngAlgorithm.sha384 = new CngAlgorithm("SHA384");
				}
				return CngAlgorithm.sha384;
			}
		}

		/// <summary>Gets a new <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object that specifies the Secure Hash Algorithm 512 (SHA-512) algorithm.</summary>
		/// <returns>An object that specifies the SHA-512 algorithm.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00017EAC File Offset: 0x000160AC
		public static CngAlgorithm Sha512
		{
			get
			{
				if (CngAlgorithm.sha512 == null)
				{
					CngAlgorithm.sha512 = new CngAlgorithm("SHA512");
				}
				return CngAlgorithm.sha512;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngAlgorithm" /> objects specify the same algorithm name.</summary>
		/// <returns>true if the two objects specify the same algorithm name; otherwise, false.</returns>
		/// <param name="left">An object that specifies an algorithm name.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		// Token: 0x0600051D RID: 1309 RVA: 0x00017EE0 File Offset: 0x000160E0
		public static bool operator ==(CngAlgorithm left, CngAlgorithm right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.algo == right.algo;
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngAlgorithm" /> objects do not specify the same algorithm.</summary>
		/// <returns>true if the two objects do not specify the same algorithm name; otherwise, false.</returns>
		/// <param name="left">An object that specifies an algorithm name.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		// Token: 0x0600051E RID: 1310 RVA: 0x00017F14 File Offset: 0x00016114
		public static bool operator !=(CngAlgorithm left, CngAlgorithm right)
		{
			if (left == null)
			{
				return right != null;
			}
			return right == null || left.algo != right.algo;
		}

		// Token: 0x04000142 RID: 322
		private string algo;

		// Token: 0x04000143 RID: 323
		private static CngAlgorithm dh256;

		// Token: 0x04000144 RID: 324
		private static CngAlgorithm dh384;

		// Token: 0x04000145 RID: 325
		private static CngAlgorithm dh521;

		// Token: 0x04000146 RID: 326
		private static CngAlgorithm dsa256;

		// Token: 0x04000147 RID: 327
		private static CngAlgorithm dsa384;

		// Token: 0x04000148 RID: 328
		private static CngAlgorithm dsa521;

		// Token: 0x04000149 RID: 329
		private static CngAlgorithm md5;

		// Token: 0x0400014A RID: 330
		private static CngAlgorithm sha1;

		// Token: 0x0400014B RID: 331
		private static CngAlgorithm sha256;

		// Token: 0x0400014C RID: 332
		private static CngAlgorithm sha384;

		// Token: 0x0400014D RID: 333
		private static CngAlgorithm sha512;
	}
}
