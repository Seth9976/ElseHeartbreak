using System;
using System.Collections.Generic;
using Mono.Security;
using Mono.Security.Cryptography;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a certificate's public key information. This class cannot be inherited.</summary>
	// Token: 0x0200043A RID: 1082
	public sealed class PublicKey
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.PublicKey" /> class using an object identifier (OID) object of the public key, an ASN.1-encoded representation of the public key parameters, and an ASN.1-encoded representation of the public key value. </summary>
		/// <param name="oid">An object identifier (OID) object that represents the public key.</param>
		/// <param name="parameters">An ASN.1-encoded representation of the public key parameters.</param>
		/// <param name="keyValue">An ASN.1-encoded representation of the public key value.</param>
		// Token: 0x060026DA RID: 9946 RVA: 0x0007879C File Offset: 0x0007699C
		public PublicKey(Oid oid, AsnEncodedData parameters, AsnEncodedData keyValue)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			if (keyValue == null)
			{
				throw new ArgumentNullException("keyValue");
			}
			this._oid = new Oid(oid);
			this._params = new AsnEncodedData(parameters);
			this._keyValue = new AsnEncodedData(keyValue);
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x00078808 File Offset: 0x00076A08
		internal PublicKey(X509Certificate certificate)
		{
			bool flag = true;
			if (certificate.KeyAlgorithm == "1.2.840.113549.1.1.1")
			{
				RSACryptoServiceProvider rsacryptoServiceProvider = certificate.RSA as RSACryptoServiceProvider;
				if (rsacryptoServiceProvider != null && rsacryptoServiceProvider.PublicOnly)
				{
					this._key = certificate.RSA;
					flag = false;
				}
				else
				{
					RSAManaged rsamanaged = certificate.RSA as RSAManaged;
					if (rsamanaged != null && rsamanaged.PublicOnly)
					{
						this._key = certificate.RSA;
						flag = false;
					}
				}
				if (flag)
				{
					RSAParameters rsaparameters = certificate.RSA.ExportParameters(false);
					this._key = RSA.Create();
					(this._key as RSA).ImportParameters(rsaparameters);
				}
			}
			else
			{
				DSACryptoServiceProvider dsacryptoServiceProvider = certificate.DSA as DSACryptoServiceProvider;
				if (dsacryptoServiceProvider != null && dsacryptoServiceProvider.PublicOnly)
				{
					this._key = certificate.DSA;
					flag = false;
				}
				if (flag)
				{
					DSAParameters dsaparameters = certificate.DSA.ExportParameters(false);
					this._key = DSA.Create();
					(this._key as DSA).ImportParameters(dsaparameters);
				}
			}
			this._oid = new Oid(certificate.KeyAlgorithm);
			this._keyValue = new AsnEncodedData(this._oid, certificate.PublicKey);
			this._params = new AsnEncodedData(this._oid, certificate.KeyAlgorithmParameters);
		}

		/// <summary>Gets the ASN.1-encoded representation of the public key value.</summary>
		/// <returns>The ASN.1-encoded representation of the public key value.</returns>
		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x00078960 File Offset: 0x00076B60
		public AsnEncodedData EncodedKeyValue
		{
			get
			{
				return this._keyValue;
			}
		}

		/// <summary>Gets the ASN.1-encoded representation of the public key parameters.</summary>
		/// <returns>The ASN.1-encoded representation of the public key parameters.</returns>
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x00078968 File Offset: 0x00076B68
		public AsnEncodedData EncodedParameters
		{
			get
			{
				return this._params;
			}
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> or <see cref="T:System.Security.Cryptography.DSACryptoServiceProvider" /> object representing the public key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object representing the public key.</returns>
		/// <exception cref="T:System.NotSupportedException">The key algorithm is not supported.</exception>
		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x00078970 File Offset: 0x00076B70
		public AsymmetricAlgorithm Key
		{
			get
			{
				if (this._key == null)
				{
					string value = this._oid.Value;
					if (value != null)
					{
						if (PublicKey.<>f__switch$map16 == null)
						{
							PublicKey.<>f__switch$map16 = new Dictionary<string, int>(2)
							{
								{ "1.2.840.113549.1.1.1", 0 },
								{ "1.2.840.10040.4.1", 1 }
							};
						}
						int num;
						if (PublicKey.<>f__switch$map16.TryGetValue(value, out num))
						{
							if (num == 0)
							{
								this._key = PublicKey.DecodeRSA(this._keyValue.RawData);
								goto IL_00D7;
							}
							if (num == 1)
							{
								this._key = PublicKey.DecodeDSA(this._keyValue.RawData, this._params.RawData);
								goto IL_00D7;
							}
						}
					}
					string text = global::Locale.GetText("Cannot decode public key from unknown OID '{0}'.", new object[] { this._oid.Value });
					throw new NotSupportedException(text);
				}
				IL_00D7:
				return this._key;
			}
		}

		/// <summary>Gets an object identifier (OID) object of the public key.</summary>
		/// <returns>An object identifier (OID) object of the public key.</returns>
		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x00078A5C File Offset: 0x00076C5C
		public Oid Oid
		{
			get
			{
				return this._oid;
			}
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x00078A64 File Offset: 0x00076C64
		private static byte[] GetUnsignedBigInteger(byte[] integer)
		{
			if (integer[0] != 0)
			{
				return integer;
			}
			int num = integer.Length - 1;
			byte[] array = new byte[num];
			Buffer.BlockCopy(integer, 1, array, 0, num);
			return array;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x00078A94 File Offset: 0x00076C94
		internal static DSA DecodeDSA(byte[] rawPublicKey, byte[] rawParameters)
		{
			DSAParameters dsaparameters = default(DSAParameters);
			try
			{
				ASN1 asn = new ASN1(rawPublicKey);
				if (asn.Tag != 2)
				{
					throw new CryptographicException(global::Locale.GetText("Missing DSA Y integer."));
				}
				dsaparameters.Y = PublicKey.GetUnsignedBigInteger(asn.Value);
				ASN1 asn2 = new ASN1(rawParameters);
				if (asn2 == null || asn2.Tag != 48 || asn2.Count < 3)
				{
					throw new CryptographicException(global::Locale.GetText("Missing DSA parameters."));
				}
				if (asn2[0].Tag != 2 || asn2[1].Tag != 2 || asn2[2].Tag != 2)
				{
					throw new CryptographicException(global::Locale.GetText("Invalid DSA parameters."));
				}
				dsaparameters.P = PublicKey.GetUnsignedBigInteger(asn2[0].Value);
				dsaparameters.Q = PublicKey.GetUnsignedBigInteger(asn2[1].Value);
				dsaparameters.G = PublicKey.GetUnsignedBigInteger(asn2[2].Value);
			}
			catch (Exception ex)
			{
				string text = global::Locale.GetText("Error decoding the ASN.1 structure.");
				throw new CryptographicException(text, ex);
			}
			DSA dsa = new DSACryptoServiceProvider(dsaparameters.Y.Length << 3);
			dsa.ImportParameters(dsaparameters);
			return dsa;
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x00078BFC File Offset: 0x00076DFC
		internal static RSA DecodeRSA(byte[] rawPublicKey)
		{
			RSAParameters rsaparameters = default(RSAParameters);
			try
			{
				ASN1 asn = new ASN1(rawPublicKey);
				if (asn.Count == 0)
				{
					throw new CryptographicException(global::Locale.GetText("Missing RSA modulus and exponent."));
				}
				ASN1 asn2 = asn[0];
				if (asn2 == null || asn2.Tag != 2)
				{
					throw new CryptographicException(global::Locale.GetText("Missing RSA modulus."));
				}
				ASN1 asn3 = asn[1];
				if (asn3.Tag != 2)
				{
					throw new CryptographicException(global::Locale.GetText("Missing RSA public exponent."));
				}
				rsaparameters.Modulus = PublicKey.GetUnsignedBigInteger(asn2.Value);
				rsaparameters.Exponent = asn3.Value;
			}
			catch (Exception ex)
			{
				string text = global::Locale.GetText("Error decoding the ASN.1 structure.");
				throw new CryptographicException(text, ex);
			}
			int num = rsaparameters.Modulus.Length << 3;
			RSA rsa = new RSACryptoServiceProvider(num);
			rsa.ImportParameters(rsaparameters);
			return rsa;
		}

		// Token: 0x040017F3 RID: 6131
		private const string rsaOid = "1.2.840.113549.1.1.1";

		// Token: 0x040017F4 RID: 6132
		private const string dsaOid = "1.2.840.10040.4.1";

		// Token: 0x040017F5 RID: 6133
		private AsymmetricAlgorithm _key;

		// Token: 0x040017F6 RID: 6134
		private AsnEncodedData _keyValue;

		// Token: 0x040017F7 RID: 6135
		private AsnEncodedData _params;

		// Token: 0x040017F8 RID: 6136
		private Oid _oid;
	}
}
