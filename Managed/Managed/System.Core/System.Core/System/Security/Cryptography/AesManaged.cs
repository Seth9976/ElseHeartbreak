using System;
using System.Security.Permissions;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Provides a managed implementation of the Advanced Encryption Standard (AES) symmetric algorithm. </summary>
	// Token: 0x02000056 RID: 86
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class AesManaged : Aes
	{
		/// <summary>Generates a random initialization vector (IV) to use for the symmetric algorithm.</summary>
		// Token: 0x060004EA RID: 1258 RVA: 0x00015C60 File Offset: 0x00013E60
		public override void GenerateIV()
		{
			this.IVValue = KeyBuilder.IV(this.BlockSizeValue >> 3);
		}

		/// <summary>Generates a random key to use for the symmetric algorithm. </summary>
		// Token: 0x060004EB RID: 1259 RVA: 0x00015C78 File Offset: 0x00013E78
		public override void GenerateKey()
		{
			this.KeyValue = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		/// <summary>Creates a symmetric decryptor object using the specified key and initialization vector (IV).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		// Token: 0x060004EC RID: 1260 RVA: 0x00015C90 File Offset: 0x00013E90
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return new AesTransform(this, false, rgbKey, rgbIV);
		}

		/// <summary>Creates a symmetric encryptor object using the specified key and initialization vector (IV).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		// Token: 0x060004ED RID: 1261 RVA: 0x00015C9C File Offset: 0x00013E9C
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			return new AesTransform(this, true, rgbKey, rgbIV);
		}

		/// <summary>Gets or sets the initialization vector (IV) to use for the symmetric algorithm. </summary>
		/// <returns>The initialization vector to use for the symmetric algorithm</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00015CA8 File Offset: 0x00013EA8
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00015CB0 File Offset: 0x00013EB0
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

		/// <summary>Gets or sets the secret key used for the symmetric algorithm.</summary>
		/// <returns>The key for the symmetric algorithm.</returns>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00015CBC File Offset: 0x00013EBC
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x00015CC4 File Offset: 0x00013EC4
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

		/// <summary>Gets or sets the size, in bits, of the secret key used for the symmetric algorithm. </summary>
		/// <returns>The size, in bits, of the key used by the symmetric algorithm.</returns>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00015CD0 File Offset: 0x00013ED0
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x00015CD8 File Offset: 0x00013ED8
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

		/// <summary>Creates a symmetric decryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric decryptor object.</returns>
		// Token: 0x060004F4 RID: 1268 RVA: 0x00015CE4 File Offset: 0x00013EE4
		public override ICryptoTransform CreateDecryptor()
		{
			return this.CreateDecryptor(this.Key, this.IV);
		}

		/// <summary>Creates a symmetric encryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric encryptor object.</returns>
		// Token: 0x060004F5 RID: 1269 RVA: 0x00015CF8 File Offset: 0x00013EF8
		public override ICryptoTransform CreateEncryptor()
		{
			return this.CreateEncryptor(this.Key, this.IV);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00015D0C File Offset: 0x00013F0C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
