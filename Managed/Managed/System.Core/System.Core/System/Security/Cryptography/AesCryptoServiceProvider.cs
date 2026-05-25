using System;
using System.Security.Permissions;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Performs symmetric encryption and decryption using the Cryptographic Application Programming Interfaces (CAPI) implementation of the Advanced Encryption Standard (AES) algorithm.</summary>
	// Token: 0x02000057 RID: 87
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class AesCryptoServiceProvider : Aes
	{
		/// <summary>Generates a random initialization vector (IV) to use for the algorithm.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The initialization vector (IV) could not be generated. </exception>
		// Token: 0x060004F8 RID: 1272 RVA: 0x00015D20 File Offset: 0x00013F20
		public override void GenerateIV()
		{
			this.IVValue = KeyBuilder.IV(this.BlockSizeValue >> 3);
		}

		/// <summary>Generates a random key to use for the algorithm. </summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key could not be generated.</exception>
		// Token: 0x060004F9 RID: 1273 RVA: 0x00015D38 File Offset: 0x00013F38
		public override void GenerateKey()
		{
			this.KeyValue = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		/// <summary>Creates a symmetric AES decryptor object using the specified key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES decryptor object.</returns>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="key" /> parameter is null or invalid.</exception>
		// Token: 0x060004FA RID: 1274 RVA: 0x00015D50 File Offset: 0x00013F50
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return new AesTransform(this, false, rgbKey, rgbIV);
		}

		/// <summary>Creates a symmetric encryptor object using the specified key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES encryptor object.</returns>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="iv" /> parameter is null.</exception>
		// Token: 0x060004FB RID: 1275 RVA: 0x00015D5C File Offset: 0x00013F5C
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return new AesTransform(this, true, rgbKey, rgbIV);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00015D68 File Offset: 0x00013F68
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x00015D70 File Offset: 0x00013F70
		public override byte[] IV
		{
			get
			{
				return base.IV;
			}
			set
			{
				base.IV = value;
			}
		}

		/// <summary>Gets or sets the symmetric key that is used for encryption and decryption.</summary>
		/// <returns>The symmetric key that is used for encryption and decryption.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for the key is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The size of the key is invalid.</exception>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x00015D7C File Offset: 0x00013F7C
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x00015D84 File Offset: 0x00013F84
		public override byte[] Key
		{
			get
			{
				return base.Key;
			}
			set
			{
				base.Key = value;
			}
		}

		/// <summary>Gets or sets the size, in bits, of the secret key. </summary>
		/// <returns>The size, in bits, of the key.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x00015D90 File Offset: 0x00013F90
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x00015D98 File Offset: 0x00013F98
		public override int KeySize
		{
			get
			{
				return base.KeySize;
			}
			set
			{
				base.KeySize = value;
			}
		}

		/// <summary>Creates a symmetric AES decryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES decryptor object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The current key is invalid or missing.</exception>
		// Token: 0x06000502 RID: 1282 RVA: 0x00015DA4 File Offset: 0x00013FA4
		public override ICryptoTransform CreateDecryptor()
		{
			return this.CreateDecryptor(this.Key, this.IV);
		}

		/// <summary>Creates a symmetric AES encryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES encryptor object.</returns>
		// Token: 0x06000503 RID: 1283 RVA: 0x00015DB8 File Offset: 0x00013FB8
		public override ICryptoTransform CreateEncryptor()
		{
			return this.CreateEncryptor(this.Key, this.IV);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00015DCC File Offset: 0x00013FCC
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
