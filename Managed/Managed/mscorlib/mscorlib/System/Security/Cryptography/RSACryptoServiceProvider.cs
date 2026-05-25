using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Performs asymmetric encryption and decryption using the implementation of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm provided by the cryptographic service provider (CSP). This class cannot be inherited.</summary>
	// Token: 0x020005D1 RID: 1489
	[ComVisible(true)]
	public sealed class RSACryptoServiceProvider : RSA, ICspAsymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class using the default key.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
		// Token: 0x060038F6 RID: 14582 RVA: 0x000C2D0C File Offset: 0x000C0F0C
		public RSACryptoServiceProvider()
		{
			this.Common(1024, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified parameters.</summary>
		/// <param name="parameters">The parameters to be passed to the cryptographic service provider (CSP). </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired. </exception>
		// Token: 0x060038F7 RID: 14583 RVA: 0x000C2D28 File Offset: 0x000C0F28
		public RSACryptoServiceProvider(CspParameters parameters)
		{
			this.Common(1024, parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified key size.</summary>
		/// <param name="dwKeySize">The size of the key to use in bits. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
		// Token: 0x060038F8 RID: 14584 RVA: 0x000C2D44 File Offset: 0x000C0F44
		public RSACryptoServiceProvider(int dwKeySize)
		{
			this.Common(dwKeySize, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> class with the specified key size and parameters.</summary>
		/// <param name="dwKeySize">The size of the key to use in bits. </param>
		/// <param name="parameters">The parameters to be passed to the cryptographic service provider (CSP). </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The CSP cannot be acquired.-or- The key cannot be created. </exception>
		// Token: 0x060038F9 RID: 14585 RVA: 0x000C2D5C File Offset: 0x000C0F5C
		public RSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
		{
			this.Common(dwKeySize, parameters);
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x000C2D78 File Offset: 0x000C0F78
		private void Common(int dwKeySize, CspParameters p)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);
			base.KeySize = dwKeySize;
			this.rsa = new RSAManaged(this.KeySize);
			this.rsa.KeyGenerated += this.OnKeyGenerated;
			this.persistKey = p != null;
			if (p == null)
			{
				p = new CspParameters(1);
				if (RSACryptoServiceProvider.useMachineKeyStore)
				{
					p.Flags |= CspProviderFlags.UseMachineKeyStore;
				}
				this.store = new KeyPairPersistence(p);
			}
			else
			{
				this.store = new KeyPairPersistence(p);
				this.store.Load();
				if (this.store.KeyValue != null)
				{
					this.persisted = true;
					this.FromXmlString(this.store.KeyValue);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the key should be persisted in the computer's key store instead of the user profile store.</summary>
		/// <returns>true if the key should be persisted in the computer key store; otherwise, false.</returns>
		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x000C2E5C File Offset: 0x000C105C
		// (set) Token: 0x060038FD RID: 14589 RVA: 0x000C2E64 File Offset: 0x000C1064
		public static bool UseMachineKeyStore
		{
			get
			{
				return RSACryptoServiceProvider.useMachineKeyStore;
			}
			set
			{
				RSACryptoServiceProvider.useMachineKeyStore = value;
			}
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x000C2E6C File Offset: 0x000C106C
		~RSACryptoServiceProvider()
		{
			this.Dispose(false);
		}

		/// <summary>Gets the name of the key exchange algorithm available with this implementation of <see cref="T:System.Security.Cryptography.RSA" />.</summary>
		/// <returns>The name of the key exchange algorithm if it exists; otherwise, null.</returns>
		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060038FF RID: 14591 RVA: 0x000C2EA8 File Offset: 0x000C10A8
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA-PKCS1-KeyEx";
			}
		}

		/// <summary>Gets the size of the current key.</summary>
		/// <returns>The size of the key in bits.</returns>
		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06003900 RID: 14592 RVA: 0x000C2EB0 File Offset: 0x000C10B0
		public override int KeySize
		{
			get
			{
				if (this.rsa == null)
				{
					return this.KeySizeValue;
				}
				return this.rsa.KeySize;
			}
		}

		/// <summary>Gets or sets a value indicating whether the key should be persisted in the cryptographic service provider (CSP).</summary>
		/// <returns>true if the key should be persisted in the CSP; otherwise, false.</returns>
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06003901 RID: 14593 RVA: 0x000C2ED0 File Offset: 0x000C10D0
		// (set) Token: 0x06003902 RID: 14594 RVA: 0x000C2ED8 File Offset: 0x000C10D8
		public bool PersistKeyInCsp
		{
			get
			{
				return this.persistKey;
			}
			set
			{
				this.persistKey = value;
				if (this.persistKey)
				{
					this.OnKeyGenerated(this.rsa, null);
				}
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object contains only a public key.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object contains only a public key; otherwise, false.</returns>
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x000C2EFC File Offset: 0x000C10FC
		[ComVisible(false)]
		public bool PublicOnly
		{
			get
			{
				return this.rsa.PublicOnly;
			}
		}

		/// <summary>Gets the name of the signature algorithm available with this implementation of <see cref="T:System.Security.Cryptography.RSA" />.</summary>
		/// <returns>The name of the signature algorithm.</returns>
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06003904 RID: 14596 RVA: 0x000C2F0C File Offset: 0x000C110C
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
			}
		}

		/// <summary>Decrypts data with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		/// <returns>The decrypted data, which is the original plain text before encryption.</returns>
		/// <param name="rgb">The data to be decrypted. </param>
		/// <param name="fOAEP">true to perform direct <see cref="T:System.Security.Cryptography.RSA" /> decryption using OAEP padding (only available on a computer running Microsoft Windows XP or later); otherwise, false to use PKCS#1 v1.5 padding. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- The <paramref name="fOAEP" /> parameter is true and the length of the <paramref name="rgb" /> parameter is greater than <see cref="P:System.Security.Cryptography.RSACryptoServiceProvider.KeySize" />.-or- The <paramref name="fOAEP" /> parameter is true and OAEP is not supported. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgb " />is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003905 RID: 14597 RVA: 0x000C2F14 File Offset: 0x000C1114
		public byte[] Decrypt(byte[] rgb, bool fOAEP)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("rsa");
			}
			AsymmetricKeyExchangeDeformatter asymmetricKeyExchangeDeformatter;
			if (fOAEP)
			{
				asymmetricKeyExchangeDeformatter = new RSAOAEPKeyExchangeDeformatter(this.rsa);
			}
			else
			{
				asymmetricKeyExchangeDeformatter = new RSAPKCS1KeyExchangeDeformatter(this.rsa);
			}
			return asymmetricKeyExchangeDeformatter.DecryptKeyExchange(rgb);
		}

		/// <summary>This method is not supported in the current version.</summary>
		/// <returns>The decrypted data, which is the original plain text before encryption.</returns>
		/// <param name="rgb">The data to be decrypted. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported in the current version. </exception>
		// Token: 0x06003906 RID: 14598 RVA: 0x000C2F64 File Offset: 0x000C1164
		public override byte[] DecryptValue(byte[] rgb)
		{
			if (!this.rsa.IsCrtPossible)
			{
				throw new CryptographicException("Incomplete private key - missing CRT.");
			}
			return this.rsa.DecryptValue(rgb);
		}

		/// <summary>Encrypts data with the <see cref="T:System.Security.Cryptography.RSA" /> algorithm.</summary>
		/// <returns>The encrypted data.</returns>
		/// <param name="rgb">The data to be encrypted. </param>
		/// <param name="fOAEP">true to perform direct <see cref="T:System.Security.Cryptography.RSA" /> encryption using Optimal Asymmetric Encryption Padding (OAEP), which is only available on a computer running Microsoft Windows XP or later; false to use PKCS#1 v1.5 padding. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- The length of the <paramref name="rgb" /> parameter is greater than the maximum allowed length.-or- The <paramref name="fOAEP" /> parameter is true and OAEP padding is not supported. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rgb " />is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003907 RID: 14599 RVA: 0x000C2F90 File Offset: 0x000C1190
		public byte[] Encrypt(byte[] rgb, bool fOAEP)
		{
			AsymmetricKeyExchangeFormatter asymmetricKeyExchangeFormatter;
			if (fOAEP)
			{
				asymmetricKeyExchangeFormatter = new RSAOAEPKeyExchangeFormatter(this.rsa);
			}
			else
			{
				asymmetricKeyExchangeFormatter = new RSAPKCS1KeyExchangeFormatter(this.rsa);
			}
			return asymmetricKeyExchangeFormatter.CreateKeyExchange(rgb);
		}

		/// <summary>This method is not supported in the current version.</summary>
		/// <returns>The encrypted data.</returns>
		/// <param name="rgb">The data to be encrypted. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported in the current version. </exception>
		// Token: 0x06003908 RID: 14600 RVA: 0x000C2FCC File Offset: 0x000C11CC
		public override byte[] EncryptValue(byte[] rgb)
		{
			return this.rsa.EncryptValue(rgb);
		}

		/// <summary>Exports the <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <returns>The parameters for <see cref="T:System.Security.Cryptography.RSA" />.</returns>
		/// <param name="includePrivateParameters">true to include private parameters; otherwise, false. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key cannot be exported. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003909 RID: 14601 RVA: 0x000C2FDC File Offset: 0x000C11DC
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (includePrivateParameters && !this.privateKeyExportable)
			{
				throw new CryptographicException("cannot export private key");
			}
			return this.rsa.ExportParameters(includePrivateParameters);
		}

		/// <summary>Imports the specified <see cref="T:System.Security.Cryptography.RSAParameters" />.</summary>
		/// <param name="parameters">The parameters for <see cref="T:System.Security.Cryptography.RSA" />. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- The <paramref name="parameters" /> parameter has missing fields. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600390A RID: 14602 RVA: 0x000C3014 File Offset: 0x000C1214
		public override void ImportParameters(RSAParameters parameters)
		{
			this.rsa.ImportParameters(parameters);
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000C3024 File Offset: 0x000C1224
		private HashAlgorithm GetHash(object halg)
		{
			if (halg == null)
			{
				throw new ArgumentNullException("halg");
			}
			HashAlgorithm hashAlgorithm;
			if (halg is string)
			{
				hashAlgorithm = HashAlgorithm.Create((string)halg);
			}
			else if (halg is HashAlgorithm)
			{
				hashAlgorithm = (HashAlgorithm)halg;
			}
			else
			{
				if (!(halg is Type))
				{
					throw new ArgumentException("halg");
				}
				hashAlgorithm = (HashAlgorithm)Activator.CreateInstance((Type)halg);
			}
			return hashAlgorithm;
		}

		/// <summary>Computes the hash value of the specified byte array using the specified hash algorithm, and signs the resulting hash value.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified data.</returns>
		/// <param name="buffer">The input data for which to compute the hash. </param>
		/// <param name="halg">The hash algorithm to use to create the hash value. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600390C RID: 14604 RVA: 0x000C30A4 File Offset: 0x000C12A4
		public byte[] SignData(byte[] buffer, object halg)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			return this.SignData(buffer, 0, buffer.Length, halg);
		}

		/// <summary>Computes the hash value of the specified input stream using the specified hash algorithm, and signs the resulting hash value.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified data.</returns>
		/// <param name="inputStream">The input data for which to compute the hash. </param>
		/// <param name="halg">The hash algorithm to use to create the hash value. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600390D RID: 14605 RVA: 0x000C30C4 File Offset: 0x000C12C4
		public byte[] SignData(Stream inputStream, object halg)
		{
			HashAlgorithm hash = this.GetHash(halg);
			byte[] array = hash.ComputeHash(inputStream);
			return PKCS1.Sign_v15(this, hash, array);
		}

		/// <summary>Computes the hash value of a subset of the specified byte array using the specified hash algorithm, and signs the resulting hash value.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified data.</returns>
		/// <param name="buffer">The input data for which to compute the hash. </param>
		/// <param name="offset">The offset into the array from which to begin using data. </param>
		/// <param name="count">The number of bytes in the array to use as data. </param>
		/// <param name="halg">The hash algorithm to use to create the hash value. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600390E RID: 14606 RVA: 0x000C30EC File Offset: 0x000C12EC
		public byte[] SignData(byte[] buffer, int offset, int count, object halg)
		{
			HashAlgorithm hash = this.GetHash(halg);
			byte[] array = hash.ComputeHash(buffer, offset, count);
			return PKCS1.Sign_v15(this, hash, array);
		}

		// Token: 0x0600390F RID: 14607 RVA: 0x000C3114 File Offset: 0x000C1314
		private string GetHashNameFromOID(string oid)
		{
			if (oid != null)
			{
				if (RSACryptoServiceProvider.<>f__switch$map2D == null)
				{
					RSACryptoServiceProvider.<>f__switch$map2D = new Dictionary<string, int>(2)
					{
						{ "1.3.14.3.2.26", 0 },
						{ "1.2.840.113549.2.5", 1 }
					};
				}
				int num;
				if (RSACryptoServiceProvider.<>f__switch$map2D.TryGetValue(oid, out num))
				{
					if (num == 0)
					{
						return "SHA1";
					}
					if (num == 1)
					{
						return "MD5";
					}
				}
			}
			throw new NotSupportedException(oid + " is an unsupported hash algorithm for RSA signing");
		}

		/// <summary>Computes the signature for the specified hash value by encrypting it with the private key.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.RSA" /> signature for the specified hash value.</returns>
		/// <param name="rgbHash">The hash value of the data to be signed. </param>
		/// <param name="str">The hash algorithm identifier (OID) used to create the hash value of the data. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- There is no private key. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003910 RID: 14608 RVA: 0x000C3198 File Offset: 0x000C1398
		public byte[] SignHash(byte[] rgbHash, string str)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			string text = ((str != null) ? this.GetHashNameFromOID(str) : "SHA1");
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(text);
			return PKCS1.Sign_v15(this, hashAlgorithm, rgbHash);
		}

		/// <summary>Verifies that a digital signature is valid by determining the hash value in the signature using the provided public key and comparing it to the hash value of the provided data.</summary>
		/// <returns>true if the signature is valid; otherwise, false.</returns>
		/// <param name="buffer">The data that was signed. </param>
		/// <param name="halg">The name of the hash algorithm used to create the hash value of the data. </param>
		/// <param name="signature">The signature data to use for verification. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="halg" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="halg" /> parameter is not a valid type. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003911 RID: 14609 RVA: 0x000C31E0 File Offset: 0x000C13E0
		public bool VerifyData(byte[] buffer, object halg, byte[] signature)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			HashAlgorithm hash = this.GetHash(halg);
			byte[] array = hash.ComputeHash(buffer);
			return PKCS1.Verify_v15(this, hash, array, signature);
		}

		/// <summary>Verifies that a digital signature is valid by determining the hash value in the signature using the provided public key and comparing it to the provided hash value.</summary>
		/// <returns>true if the signature is valid; otherwise, false.</returns>
		/// <param name="rgbHash">The hash value of the signed data. </param>
		/// <param name="str">The hash algorithm identifier (OID) used to create the hash value of the data. </param>
		/// <param name="rgbSignature">The signature data to use for verification. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is null.-or- The <paramref name="rgbSignature" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.-or- The signature cannot be verified. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003912 RID: 14610 RVA: 0x000C3228 File Offset: 0x000C1428
		public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			string text = ((str != null) ? this.GetHashNameFromOID(str) : "SHA1");
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(text);
			return PKCS1.Verify_v15(this, hashAlgorithm, rgbHash, rgbSignature);
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000C3280 File Offset: 0x000C1480
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.persisted && !this.persistKey)
				{
					this.store.Remove();
				}
				if (this.rsa != null)
				{
					this.rsa.Clear();
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000C32D8 File Offset: 0x000C14D8
		private void OnKeyGenerated(object sender, EventArgs e)
		{
			if (this.persistKey && !this.persisted)
			{
				this.store.KeyValue = this.ToXmlString(!this.rsa.PublicOnly);
				this.store.Save();
				this.persisted = true;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair. </summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CspKeyContainerInfo" /> object that describes additional information about a cryptographic key pair.</returns>
		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06003915 RID: 14613 RVA: 0x000C332C File Offset: 0x000C152C
		[ComVisible(false)]
		[MonoTODO("Always return null")]
		public CspKeyContainerInfo CspKeyContainerInfo
		{
			get
			{
				return null;
			}
		}

		/// <summary>Exports a blob containing the key information associated with an <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object.  </summary>
		/// <returns>A byte array containing the key information associated with an <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> object.</returns>
		/// <param name="includePrivateParameters">true to include the private key; otherwise, false.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003916 RID: 14614 RVA: 0x000C3330 File Offset: 0x000C1530
		[ComVisible(false)]
		public byte[] ExportCspBlob(bool includePrivateParameters)
		{
			byte[] array;
			if (includePrivateParameters)
			{
				array = CryptoConvert.ToCapiPrivateKeyBlob(this);
			}
			else
			{
				array = CryptoConvert.ToCapiPublicKeyBlob(this);
			}
			array[5] = 164;
			return array;
		}

		/// <summary>Imports a blob that represents RSA key information.  </summary>
		/// <param name="keyBlob">A byte array that represents an RSA key blob.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003917 RID: 14615 RVA: 0x000C3364 File Offset: 0x000C1564
		[ComVisible(false)]
		public void ImportCspBlob(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			RSA rsa = CryptoConvert.FromCapiKeyBlob(keyBlob);
			if (rsa is RSACryptoServiceProvider)
			{
				RSAParameters rsaparameters = rsa.ExportParameters(!(rsa as RSACryptoServiceProvider).PublicOnly);
				this.ImportParameters(rsaparameters);
			}
			else
			{
				try
				{
					RSAParameters rsaparameters2 = rsa.ExportParameters(true);
					this.ImportParameters(rsaparameters2);
				}
				catch
				{
					RSAParameters rsaparameters3 = rsa.ExportParameters(false);
					this.ImportParameters(rsaparameters3);
				}
			}
		}

		// Token: 0x040018B1 RID: 6321
		private const int PROV_RSA_FULL = 1;

		// Token: 0x040018B2 RID: 6322
		private KeyPairPersistence store;

		// Token: 0x040018B3 RID: 6323
		private bool persistKey;

		// Token: 0x040018B4 RID: 6324
		private bool persisted;

		// Token: 0x040018B5 RID: 6325
		private bool privateKeyExportable = true;

		// Token: 0x040018B6 RID: 6326
		private bool m_disposed;

		// Token: 0x040018B7 RID: 6327
		private RSAManaged rsa;

		// Token: 0x040018B8 RID: 6328
		private static bool useMachineKeyStore;
	}
}
