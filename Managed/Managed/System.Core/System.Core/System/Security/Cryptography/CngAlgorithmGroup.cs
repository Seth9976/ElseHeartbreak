using System;

namespace System.Security.Cryptography
{
	/// <summary>Encapsulates the name of an encryption algorithm group. </summary>
	// Token: 0x0200005A RID: 90
	[Serializable]
	public sealed class CngAlgorithmGroup : IEquatable<CngAlgorithmGroup>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> class.</summary>
		/// <param name="algorithmGroup">The name of the algorithm group to initialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithmGroup" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="algorithmGroup" /> parameter length is 0 (zero).</exception>
		// Token: 0x0600051F RID: 1311 RVA: 0x00017F40 File Offset: 0x00016140
		public CngAlgorithmGroup(string algorithmGroup)
		{
			if (algorithmGroup == null)
			{
				throw new ArgumentNullException("algorithmGroup");
			}
			if (algorithmGroup.Length == 0)
			{
				throw new ArgumentException("algorithmGroup");
			}
			this.group = algorithmGroup;
		}

		/// <summary>Gets the name of the algorithm group that the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object specifies.</summary>
		/// <returns>The embedded algorithm group name.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00017F84 File Offset: 0x00016184
		public string AlgorithmGroup
		{
			get
			{
				return this.group;
			}
		}

		/// <summary>Compares the specified <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object to the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</summary>
		/// <returns>true if the <paramref name="other" /> parameter specifies the same algorithm group as the current object; otherwise, false.</returns>
		/// <param name="other">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</param>
		// Token: 0x06000521 RID: 1313 RVA: 0x00017F8C File Offset: 0x0001618C
		public bool Equals(CngAlgorithmGroup other)
		{
			return this == other;
		}

		/// <summary>Compares the specified object to the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter is a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> that specifies the same algorithm group as the current object; otherwise, false.</returns>
		/// <param name="obj">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</param>
		// Token: 0x06000522 RID: 1314 RVA: 0x00017F98 File Offset: 0x00016198
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngAlgorithmGroup);
		}

		/// <summary>Generates a hash value for the algorithm group name that is embedded in the current <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object.</summary>
		/// <returns>The hash value of the embedded algorithm group name.</returns>
		// Token: 0x06000523 RID: 1315 RVA: 0x00017FA8 File Offset: 0x000161A8
		public override int GetHashCode()
		{
			return this.group.GetHashCode();
		}

		/// <summary>Gets the name of the algorithm group that the current <see cref="T:System.Security.Cryptography.CngAlgorithm" /> object specifies.</summary>
		/// <returns>The embedded algorithm group name.</returns>
		// Token: 0x06000524 RID: 1316 RVA: 0x00017FB8 File Offset: 0x000161B8
		public override string ToString()
		{
			return this.group;
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Diffie-Hellman family of algorithms.</summary>
		/// <returns>An object that specifies the Diffie-Hellman family of algorithms.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00017FC0 File Offset: 0x000161C0
		public static CngAlgorithmGroup DiffieHellman
		{
			get
			{
				if (CngAlgorithmGroup.dh == null)
				{
					CngAlgorithmGroup.dh = new CngAlgorithmGroup("DH");
				}
				return CngAlgorithmGroup.dh;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Digital Signature Algorithm (DSA) family of algorithms.</summary>
		/// <returns>An object that specifies the DSA family of algorithms.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00017FF4 File Offset: 0x000161F4
		public static CngAlgorithmGroup Dsa
		{
			get
			{
				if (CngAlgorithmGroup.dsa == null)
				{
					CngAlgorithmGroup.dsa = new CngAlgorithmGroup("DSA");
				}
				return CngAlgorithmGroup.dsa;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Elliptic Curve Diffie-Hellman (ECDH) family of algorithms.</summary>
		/// <returns>An object that specifies the ECDH family of algorithms.</returns>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00018028 File Offset: 0x00016228
		public static CngAlgorithmGroup ECDiffieHellman
		{
			get
			{
				if (CngAlgorithmGroup.ecdh == null)
				{
					CngAlgorithmGroup.ecdh = new CngAlgorithmGroup("ECDH");
				}
				return CngAlgorithmGroup.ecdh;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Elliptic Curve Digital Signature Algorithm (ECDSA) family of algorithms.</summary>
		/// <returns>An object that specifies the ECDSA family of algorithms.</returns>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001805C File Offset: 0x0001625C
		public static CngAlgorithmGroup ECDsa
		{
			get
			{
				if (CngAlgorithmGroup.ecdsa == null)
				{
					CngAlgorithmGroup.ecdsa = new CngAlgorithmGroup("ECDSA");
				}
				return CngAlgorithmGroup.ecdsa;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> object that specifies the Rivest-Shamir-Adleman (RSA) family of algorithms.</summary>
		/// <returns>An object that specifies the RSA family of algorithms.</returns>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00018090 File Offset: 0x00016290
		public static CngAlgorithmGroup Rsa
		{
			get
			{
				if (CngAlgorithmGroup.rsa == null)
				{
					CngAlgorithmGroup.rsa = new CngAlgorithmGroup("RSA");
				}
				return CngAlgorithmGroup.rsa;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> objects specify the same algorithm group.</summary>
		/// <returns>true if the two objects specify the same algorithm group; otherwise, false.</returns>
		/// <param name="left">An object that specifies an algorithm group.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		// Token: 0x0600052A RID: 1322 RVA: 0x000180C4 File Offset: 0x000162C4
		public static bool operator ==(CngAlgorithmGroup left, CngAlgorithmGroup right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.group == right.group;
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngAlgorithmGroup" /> objects do not specify the same algorithm group.</summary>
		/// <returns>true if the two objects do not specify the same algorithm group; otherwise, false. </returns>
		/// <param name="left">An object that specifies an algorithm group.</param>
		/// <param name="right">A second object, to be compared to the object that is identified by the <paramref name="left" /> parameter.</param>
		// Token: 0x0600052B RID: 1323 RVA: 0x000180F8 File Offset: 0x000162F8
		public static bool operator !=(CngAlgorithmGroup left, CngAlgorithmGroup right)
		{
			if (left == null)
			{
				return right != null;
			}
			return right == null || left.group != right.group;
		}

		// Token: 0x0400014E RID: 334
		private string group;

		// Token: 0x0400014F RID: 335
		private static CngAlgorithmGroup dh;

		// Token: 0x04000150 RID: 336
		private static CngAlgorithmGroup dsa;

		// Token: 0x04000151 RID: 337
		private static CngAlgorithmGroup ecdh;

		// Token: 0x04000152 RID: 338
		private static CngAlgorithmGroup ecdsa;

		// Token: 0x04000153 RID: 339
		private static CngAlgorithmGroup rsa;
	}
}
