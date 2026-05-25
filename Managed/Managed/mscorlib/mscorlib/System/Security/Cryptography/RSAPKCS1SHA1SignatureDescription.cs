using System;

namespace System.Security.Cryptography
{
	// Token: 0x020005E6 RID: 1510
	internal class RSAPKCS1SHA1SignatureDescription : SignatureDescription
	{
		// Token: 0x06003995 RID: 14741 RVA: 0x000C5A68 File Offset: 0x000C3C68
		public RSAPKCS1SHA1SignatureDescription()
		{
			base.DeformatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureDeformatter";
			base.DigestAlgorithm = "System.Security.Cryptography.SHA1CryptoServiceProvider";
			base.FormatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureFormatter";
			base.KeyAlgorithm = "System.Security.Cryptography.RSACryptoServiceProvider";
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000C5AA8 File Offset: 0x000C3CA8
		public override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			return base.CreateDeformatter(key);
		}
	}
}
