using System;
using System.IO;
using System.Text;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200014C RID: 332
	internal sealed class WWWTranscoder
	{
		// Token: 0x06000DE8 RID: 3560 RVA: 0x0001DF2C File Offset: 0x0001C12C
		private static byte Hex2Byte(byte[] b, int offset)
		{
			byte b2 = 0;
			for (int i = offset; i < offset + 2; i++)
			{
				b2 *= 16;
				int num = (int)b[i];
				if (num >= 48 && num <= 57)
				{
					num -= 48;
				}
				else if (num >= 65 && num <= 75)
				{
					num -= 55;
				}
				else if (num >= 97 && num <= 102)
				{
					num -= 87;
				}
				if (num > 15)
				{
					return 63;
				}
				b2 += (byte)num;
			}
			return b2;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0001DFB4 File Offset: 0x0001C1B4
		private static byte[] Byte2Hex(byte b, byte[] hexChars)
		{
			return new byte[]
			{
				hexChars[b >> 4],
				hexChars[(int)(b & 15)]
			};
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0001DFDC File Offset: 0x0001C1DC
		[ExcludeFromDocs]
		public static string URLEncode(string toEncode)
		{
			Encoding utf = Encoding.UTF8;
			return WWWTranscoder.URLEncode(toEncode, utf);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0001DFF8 File Offset: 0x0001C1F8
		public static string URLEncode(string toEncode, [DefaultValue("Encoding.UTF8")] Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace, WWWTranscoder.urlForbidden, false);
			return WWW.DefaultEncoding.GetString(array, 0, array.Length);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0001E034 File Offset: 0x0001C234
		public static byte[] URLEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace, WWWTranscoder.urlForbidden, false);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0001E04C File Offset: 0x0001C24C
		[ExcludeFromDocs]
		public static string QPEncode(string toEncode)
		{
			Encoding utf = Encoding.UTF8;
			return WWWTranscoder.QPEncode(toEncode, utf);
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0001E068 File Offset: 0x0001C268
		public static string QPEncode(string toEncode, [DefaultValue("Encoding.UTF8")] Encoding e)
		{
			byte[] array = WWWTranscoder.Encode(e.GetBytes(toEncode), WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace, WWWTranscoder.qpForbidden, true);
			return WWW.DefaultEncoding.GetString(array, 0, array.Length);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0001E0A4 File Offset: 0x0001C2A4
		public static byte[] QPEncode(byte[] toEncode)
		{
			return WWWTranscoder.Encode(toEncode, WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace, WWWTranscoder.qpForbidden, true);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0001E0BC File Offset: 0x0001C2BC
		public static byte[] Encode(byte[] input, byte escapeChar, byte space, byte[] forbidden, bool uppercase)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(input.Length * 2))
			{
				for (int i = 0; i < input.Length; i++)
				{
					if (input[i] == 32)
					{
						memoryStream.WriteByte(space);
					}
					else if (input[i] < 32 || input[i] > 126 || WWWTranscoder.ByteArrayContains(forbidden, input[i]))
					{
						memoryStream.WriteByte(escapeChar);
						memoryStream.Write(WWWTranscoder.Byte2Hex(input[i], (!uppercase) ? WWWTranscoder.lcHexChars : WWWTranscoder.ucHexChars), 0, 2);
					}
					else
					{
						memoryStream.WriteByte(input[i]);
					}
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0001E198 File Offset: 0x0001C398
		private static bool ByteArrayContains(byte[] array, byte b)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				if (array[i] == b)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0001E1C8 File Offset: 0x0001C3C8
		[ExcludeFromDocs]
		public static string URLDecode(string toEncode)
		{
			Encoding utf = Encoding.UTF8;
			return WWWTranscoder.URLDecode(toEncode, utf);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0001E1E4 File Offset: 0x0001C3E4
		public static string URLDecode(string toEncode, [DefaultValue("Encoding.UTF8")] Encoding e)
		{
			byte[] array = WWWTranscoder.Decode(WWW.DefaultEncoding.GetBytes(toEncode), WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace);
			return e.GetString(array, 0, array.Length);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0001E218 File Offset: 0x0001C418
		public static byte[] URLDecode(byte[] toEncode)
		{
			return WWWTranscoder.Decode(toEncode, WWWTranscoder.urlEscapeChar, WWWTranscoder.urlSpace);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0001E22C File Offset: 0x0001C42C
		[ExcludeFromDocs]
		public static string QPDecode(string toEncode)
		{
			Encoding utf = Encoding.UTF8;
			return WWWTranscoder.QPDecode(toEncode, utf);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0001E248 File Offset: 0x0001C448
		public static string QPDecode(string toEncode, [DefaultValue("Encoding.UTF8")] Encoding e)
		{
			byte[] array = WWWTranscoder.Decode(WWW.DefaultEncoding.GetBytes(toEncode), WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace);
			return e.GetString(array, 0, array.Length);
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0001E27C File Offset: 0x0001C47C
		public static byte[] QPDecode(byte[] toEncode)
		{
			return WWWTranscoder.Decode(toEncode, WWWTranscoder.qpEscapeChar, WWWTranscoder.qpSpace);
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0001E290 File Offset: 0x0001C490
		public static byte[] Decode(byte[] input, byte escapeChar, byte space)
		{
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream(input.Length))
			{
				for (int i = 0; i < input.Length; i++)
				{
					if (input[i] == space)
					{
						memoryStream.WriteByte(32);
					}
					else if (input[i] == escapeChar && i + 2 < input.Length)
					{
						i++;
						memoryStream.WriteByte(WWWTranscoder.Hex2Byte(input, i++));
					}
					else
					{
						memoryStream.WriteByte(input[i]);
					}
				}
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0001E344 File Offset: 0x0001C544
		[ExcludeFromDocs]
		public static bool SevenBitClean(string s)
		{
			Encoding utf = Encoding.UTF8;
			return WWWTranscoder.SevenBitClean(s, utf);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0001E360 File Offset: 0x0001C560
		public static bool SevenBitClean(string s, [DefaultValue("Encoding.UTF8")] Encoding e)
		{
			return WWWTranscoder.SevenBitClean(e.GetBytes(s));
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0001E370 File Offset: 0x0001C570
		public static bool SevenBitClean(byte[] input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] < 32 || input[i] > 126)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040005C4 RID: 1476
		private static byte[] ucHexChars = WWW.DefaultEncoding.GetBytes("0123456789ABCDEF");

		// Token: 0x040005C5 RID: 1477
		private static byte[] lcHexChars = WWW.DefaultEncoding.GetBytes("0123456789abcdef");

		// Token: 0x040005C6 RID: 1478
		private static byte urlEscapeChar = 37;

		// Token: 0x040005C7 RID: 1479
		private static byte urlSpace = 43;

		// Token: 0x040005C8 RID: 1480
		private static byte[] urlForbidden = WWW.DefaultEncoding.GetBytes("@&;:<>=?\"'/\\!#%+$,{}|^[]`");

		// Token: 0x040005C9 RID: 1481
		private static byte qpEscapeChar = 61;

		// Token: 0x040005CA RID: 1482
		private static byte qpSpace = 95;

		// Token: 0x040005CB RID: 1483
		private static byte[] qpForbidden = WWW.DefaultEncoding.GetBytes("&;=?\"'%+_");
	}
}
