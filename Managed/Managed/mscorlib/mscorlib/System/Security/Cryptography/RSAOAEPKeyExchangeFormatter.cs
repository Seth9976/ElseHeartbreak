using System;
using System.Runtime.InteropServices;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Creates Optimal Asymmetric Encryption Padding (OAEP) key exchange data using <see cref="T:System.Security.Cryptography.RSA" />.</summary>
	// Token: 0x020005D3 RID: 1491
	[ComVisible(true)]
	public class RSAOAEPKeyExchangeFormatter : AsymmetricKeyExchangeFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeFormatter" /> class.</summary>
		// Token: 0x0600391E RID: 14622 RVA: 0x000C3484 File Offset: 0x000C1684
		public RSAOAEPKeyExchangeFormatter()
		{
			this.rsa = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.RSAOAEPKeyExchangeFormatter" /> class with the specified key.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the public key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key " />is null.</exception>
		// Token: 0x0600391F RID: 14623 RVA: 0x000C3494 File Offset: 0x000C1694
		public RSAOAEPKeyExchangeFormatter(AsymmetricAlgorithm key)
		{
			this.SetKey(key);
		}

		/// <summary>Gets or sets the parameter used to create padding in the key exchange creation process.</summary>
		/// <returns>The parameter value.</returns>
		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06003920 RID: 14624 RVA: 0x000C34A4 File Offset: 0x000C16A4
		// (set) Token: 0x06003921 RID: 14625 RVA: 0x000C34AC File Offset: 0x000C16AC
		public byte[] Parameter
		{
			get
			{
				return this.param;
			}
			set
			{
				this.param = value;
			}
		}

		/// <summary>Gets the parameters for the Optimal Asymmetric Encryption Padding (OAEP) key exchange.</summary>
		/// <returns>An XML string containing the parameters of the OAEP key exchange operation.</returns>
		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06003922 RID: 14626 RVA: 0x000C34B8 File Offset: 0x000C16B8
		public override string Parameters
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets or sets the random number generator algorithm to use in the creation of the key exchange.</summary>
		/// <returns>The instance of a random number generator algorithm to use.</returns>
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06003923 RID: 14627 RVA: 0x000C34BC File Offset: 0x000C16BC
		// (set) Token: 0x06003924 RID: 14628 RVA: 0x000C34C4 File Offset: 0x000C16C4
		public RandomNumberGenerator Rng
		{
			get
			{
				return this.random;
			}
			set
			{
				this.random = value;
			}
		}

		/// <summary>Creates the encrypted key exchange data from the specified input data.</summary>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		/// <param name="rgbData">The secret information to be passed in the key exchange. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The key is missing.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003925 RID: 14629 RVA: 0x000C34D0 File Offset: 0x000C16D0
		public override byte[] CreateKeyExchange(byte[] rgbData)
		{
			if (this.random == null)
			{
				this.random = RandomNumberGenerator.Create();
			}
			if (this.rsa == null)
			{
				string text = Locale.GetText("No RSA key specified");
				throw new CryptographicUnexpectedOperationException(text);
			}
			SHA1 sha = SHA1.Create();
			return PKCS1.Encrypt_OAEP(this.rsa, sha, this.random, rgbData);
		}

		/// <summary>Creates the encrypted key exchange data from the specified input data.</summary>
		/// <returns>The encrypted key exchange data to be sent to the intended recipient.</returns>
		/// <param name="rgbData">The secret information to be passed in the key exchange. </param>
		/// <param name="symAlgType">This parameter is not used in the current version. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.KeyContainerPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003926 RID: 14630 RVA: 0x000C352C File Offset: 0x000C172C
		public override byte[] CreateKeyExchange(byte[] rgbData, Type symAlgType)
		{
			return this.CreateKeyExchange(rgbData);
		}

		/// <summary>Sets the public key to use for encrypting the key exchange data.</summary>
		/// <param name="key">The instance of the <see cref="T:System.Security.Cryptography.RSA" /> algorithm that holds the public key. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key " />is null.</exception>
		// Token: 0x06003927 RID: 14631 RVA: 0x000C3538 File Offset: 0x000C1738
		public override void SetKey(AsymmetricAlgorithm key)
		{
			this.rsa = (RSA)key;
		}

		// Token: 0x040018BB RID: 6331
		private RSA rsa;

		// Token: 0x040018BC RID: 6332
		private RandomNumberGenerator random;

		// Token: 0x040018BD RID: 6333
		private byte[] param;
	}
}
