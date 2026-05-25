using System;
using System.Text;
using Mono.Security;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents the distinguished name of an X509 certificate. This class cannot be inherited.</summary>
	// Token: 0x0200043D RID: 1085
	[global::System.MonoTODO("Some X500DistinguishedNameFlags options aren't supported, like DoNotUsePlusSign, DoNotUseQuotes and ForceUTF8Encoding")]
	public sealed class X500DistinguishedName : AsnEncodedData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="encodedDistinguishedName">An <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object that represents the distinguished name.</param>
		// Token: 0x060026E3 RID: 9955 RVA: 0x00078D00 File Offset: 0x00076F00
		public X500DistinguishedName(AsnEncodedData encodedDistinguishedName)
		{
			if (encodedDistinguishedName == null)
			{
				throw new ArgumentNullException("encodedDistinguishedName");
			}
			base.RawData = encodedDistinguishedName.RawData;
			if (base.RawData.Length > 0)
			{
				this.DecodeRawData();
			}
			else
			{
				this.name = string.Empty;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using information from the specified byte array.</summary>
		/// <param name="encodedDistinguishedName">A byte array that contains distinguished name information.</param>
		// Token: 0x060026E4 RID: 9956 RVA: 0x00078D54 File Offset: 0x00076F54
		public X500DistinguishedName(byte[] encodedDistinguishedName)
		{
			if (encodedDistinguishedName == null)
			{
				throw new ArgumentNullException("encodedDistinguishedName");
			}
			base.Oid = new Oid();
			base.RawData = encodedDistinguishedName;
			if (encodedDistinguishedName.Length > 0)
			{
				this.DecodeRawData();
			}
			else
			{
				this.name = string.Empty;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using information from the specified string.</summary>
		/// <param name="distinguishedName">A string that represents the distinguished name.</param>
		// Token: 0x060026E5 RID: 9957 RVA: 0x00078DAC File Offset: 0x00076FAC
		public X500DistinguishedName(string distinguishedName)
			: this(distinguishedName, X500DistinguishedNameFlags.Reversed)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using the specified string and <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedNameFlags" /> flag.</summary>
		/// <param name="distinguishedName">A string that represents the distinguished name.</param>
		/// <param name="flag">An <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object that specifies the characteristics of the distinguished name.</param>
		// Token: 0x060026E6 RID: 9958 RVA: 0x00078DB8 File Offset: 0x00076FB8
		public X500DistinguishedName(string distinguishedName, X500DistinguishedNameFlags flag)
		{
			if (distinguishedName == null)
			{
				throw new ArgumentNullException("distinguishedName");
			}
			if (flag != X500DistinguishedNameFlags.None && (flag & (X500DistinguishedNameFlags.Reversed | X500DistinguishedNameFlags.UseSemicolons | X500DistinguishedNameFlags.DoNotUsePlusSign | X500DistinguishedNameFlags.DoNotUseQuotes | X500DistinguishedNameFlags.UseCommas | X500DistinguishedNameFlags.UseNewLines | X500DistinguishedNameFlags.UseUTF8Encoding | X500DistinguishedNameFlags.UseT61Encoding | X500DistinguishedNameFlags.ForceUTF8Encoding)) == X500DistinguishedNameFlags.None)
			{
				throw new ArgumentException("flag");
			}
			base.Oid = new Oid();
			if (distinguishedName.Length == 0)
			{
				byte[] array = new byte[2];
				array[0] = 48;
				base.RawData = array;
				this.DecodeRawData();
			}
			else
			{
				ASN1 asn = X501.FromString(distinguishedName);
				if ((flag & X500DistinguishedNameFlags.Reversed) != X500DistinguishedNameFlags.None)
				{
					ASN1 asn2 = new ASN1(48);
					for (int i = asn.Count - 1; i >= 0; i--)
					{
						asn2.Add(asn[i]);
					}
					asn = asn2;
				}
				base.RawData = asn.GetBytes();
				if (flag == X500DistinguishedNameFlags.None)
				{
					this.name = distinguishedName;
				}
				else
				{
					this.name = this.Decode(flag);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object.</summary>
		/// <param name="distinguishedName">An <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object.</param>
		// Token: 0x060026E7 RID: 9959 RVA: 0x00078E98 File Offset: 0x00077098
		public X500DistinguishedName(X500DistinguishedName distinguishedName)
		{
			if (distinguishedName == null)
			{
				throw new ArgumentNullException("distinguishedName");
			}
			base.Oid = new Oid();
			base.RawData = distinguishedName.RawData;
			this.name = distinguishedName.name;
		}

		/// <summary>Gets the comma-delimited distinguished name from an X500 certificate.</summary>
		/// <returns>The comma-delimited distinguished name of the X509 certificate.</returns>
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060026E8 RID: 9960 RVA: 0x00078EE0 File Offset: 0x000770E0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Decodes a distinguished name using the characteristics specified by the <paramref name="flag" /> parameter.</summary>
		/// <returns>The decoded distinguished name.</returns>
		/// <param name="flag">A flag that specifies the characteristics of the <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate has an invalid name.</exception>
		// Token: 0x060026E9 RID: 9961 RVA: 0x00078EE8 File Offset: 0x000770E8
		public string Decode(X500DistinguishedNameFlags flag)
		{
			if (flag != X500DistinguishedNameFlags.None && (flag & (X500DistinguishedNameFlags.Reversed | X500DistinguishedNameFlags.UseSemicolons | X500DistinguishedNameFlags.DoNotUsePlusSign | X500DistinguishedNameFlags.DoNotUseQuotes | X500DistinguishedNameFlags.UseCommas | X500DistinguishedNameFlags.UseNewLines | X500DistinguishedNameFlags.UseUTF8Encoding | X500DistinguishedNameFlags.UseT61Encoding | X500DistinguishedNameFlags.ForceUTF8Encoding)) == X500DistinguishedNameFlags.None)
			{
				throw new ArgumentException("flag");
			}
			if (base.RawData.Length == 0)
			{
				return string.Empty;
			}
			bool flag2 = (flag & X500DistinguishedNameFlags.Reversed) != X500DistinguishedNameFlags.None;
			bool flag3 = (flag & X500DistinguishedNameFlags.DoNotUseQuotes) == X500DistinguishedNameFlags.None;
			string separator = X500DistinguishedName.GetSeparator(flag);
			ASN1 asn = new ASN1(base.RawData);
			return X501.ToString(asn, flag2, separator, flag3);
		}

		/// <summary>Returns a formatted version of an X500 distinguished name for printing or for output to a text window or to a console.</summary>
		/// <returns>A formatted string that represents the X500 distinguished name.</returns>
		/// <param name="multiLine">true if the return string should contain carriage returns; otherwise, false.</param>
		// Token: 0x060026EA RID: 9962 RVA: 0x00078F54 File Offset: 0x00077154
		public override string Format(bool multiLine)
		{
			if (!multiLine)
			{
				return this.Decode(X500DistinguishedNameFlags.UseCommas);
			}
			string text = this.Decode(X500DistinguishedNameFlags.UseNewLines);
			if (text.Length > 0)
			{
				return text + Environment.NewLine;
			}
			return text;
		}

		// Token: 0x060026EB RID: 9963 RVA: 0x00078F98 File Offset: 0x00077198
		private static string GetSeparator(X500DistinguishedNameFlags flag)
		{
			if ((flag & X500DistinguishedNameFlags.UseSemicolons) != X500DistinguishedNameFlags.None)
			{
				return "; ";
			}
			if ((flag & X500DistinguishedNameFlags.UseCommas) != X500DistinguishedNameFlags.None)
			{
				return ", ";
			}
			if ((flag & X500DistinguishedNameFlags.UseNewLines) != X500DistinguishedNameFlags.None)
			{
				return Environment.NewLine;
			}
			return ", ";
		}

		// Token: 0x060026EC RID: 9964 RVA: 0x00078FE0 File Offset: 0x000771E0
		private void DecodeRawData()
		{
			if (base.RawData == null || base.RawData.Length < 3)
			{
				this.name = string.Empty;
				return;
			}
			ASN1 asn = new ASN1(base.RawData);
			this.name = X501.ToString(asn, true, ", ", true);
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x00079034 File Offset: 0x00077234
		private static string Canonize(string s)
		{
			int i = s.IndexOf('=');
			StringBuilder stringBuilder = new StringBuilder(s.Substring(0, i + 1));
			while (char.IsWhiteSpace(s, ++i))
			{
			}
			s = s.TrimEnd(new char[0]);
			bool flag = false;
			while (i < s.Length)
			{
				if (!flag)
				{
					goto IL_005C;
				}
				flag = char.IsWhiteSpace(s, i);
				if (!flag)
				{
					goto IL_005C;
				}
				IL_007D:
				i++;
				continue;
				IL_005C:
				if (char.IsWhiteSpace(s, i))
				{
					flag = true;
				}
				stringBuilder.Append(char.ToUpperInvariant(s[i]));
				goto IL_007D;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x000790D4 File Offset: 0x000772D4
		internal static bool AreEqual(X500DistinguishedName name1, X500DistinguishedName name2)
		{
			if (name1 == null)
			{
				return name2 == null;
			}
			if (name2 == null)
			{
				return false;
			}
			X500DistinguishedNameFlags x500DistinguishedNameFlags = X500DistinguishedNameFlags.DoNotUseQuotes | X500DistinguishedNameFlags.UseNewLines;
			string[] array = new string[] { Environment.NewLine };
			string[] array2 = name1.Decode(x500DistinguishedNameFlags).Split(array, StringSplitOptions.RemoveEmptyEntries);
			string[] array3 = name2.Decode(x500DistinguishedNameFlags).Split(array, StringSplitOptions.RemoveEmptyEntries);
			if (array2.Length != array3.Length)
			{
				return false;
			}
			for (int i = 0; i < array2.Length; i++)
			{
				if (X500DistinguishedName.Canonize(array2[i]) != X500DistinguishedName.Canonize(array3[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001806 RID: 6150
		private const X500DistinguishedNameFlags AllFlags = X500DistinguishedNameFlags.Reversed | X500DistinguishedNameFlags.UseSemicolons | X500DistinguishedNameFlags.DoNotUsePlusSign | X500DistinguishedNameFlags.DoNotUseQuotes | X500DistinguishedNameFlags.UseCommas | X500DistinguishedNameFlags.UseNewLines | X500DistinguishedNameFlags.UseUTF8Encoding | X500DistinguishedNameFlags.UseT61Encoding | X500DistinguishedNameFlags.ForceUTF8Encoding;

		// Token: 0x04001807 RID: 6151
		private string name;
	}
}
