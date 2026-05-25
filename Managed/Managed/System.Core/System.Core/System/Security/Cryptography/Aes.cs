using System;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of the Advanced Encryption Standard (AES) must inherit. </summary>
	// Token: 0x02000055 RID: 85
	public abstract class Aes : SymmetricAlgorithm
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Aes" /> class. </summary>
		// Token: 0x060004E6 RID: 1254 RVA: 0x00015BBC File Offset: 0x00013DBC
		protected Aes()
		{
			this.KeySizeValue = 256;
			this.BlockSizeValue = 128;
			this.FeedbackSizeValue = 128;
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(128, 256, 64);
			this.LegalBlockSizesValue = new KeySizes[1];
			this.LegalBlockSizesValue[0] = new KeySizes(128, 128, 0);
		}

		/// <summary>Creates a cryptographic object that is used to perform the symmetric algorithm.</summary>
		/// <returns>A cryptographic object that is used to perform the symmetric algorithm.</returns>
		// Token: 0x060004E7 RID: 1255 RVA: 0x00015C3C File Offset: 0x00013E3C
		public new static Aes Create()
		{
			return Aes.Create("System.Security.Cryptography.AesManaged, System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
		}

		/// <summary>Creates a cryptographic object that specifies the implementation of AES to use to perform the symmetric algorithm.</summary>
		/// <returns>A cryptographic object that is used to perform the symmetric algorithm.</returns>
		/// <param name="algorithmName">The name of the specific implementation of AES to use.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithmName" /> parameter is null.</exception>
		// Token: 0x060004E8 RID: 1256 RVA: 0x00015C48 File Offset: 0x00013E48
		public new static Aes Create(string algName)
		{
			return (Aes)CryptoConfig.CreateFromName(algName);
		}
	}
}
