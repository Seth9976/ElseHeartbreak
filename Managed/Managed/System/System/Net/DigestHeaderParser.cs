using System;

namespace System.Net
{
	// Token: 0x020002F9 RID: 761
	internal class DigestHeaderParser
	{
		// Token: 0x06001A06 RID: 6662 RVA: 0x0004807C File Offset: 0x0004627C
		public DigestHeaderParser(string header)
		{
			this.header = header.Trim();
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x000480E8 File Offset: 0x000462E8
		public string Realm
		{
			get
			{
				return this.values[0];
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x000480F4 File Offset: 0x000462F4
		public string Opaque
		{
			get
			{
				return this.values[1];
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x00048100 File Offset: 0x00046300
		public string Nonce
		{
			get
			{
				return this.values[2];
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x0004810C File Offset: 0x0004630C
		public string Algorithm
		{
			get
			{
				return this.values[3];
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x00048118 File Offset: 0x00046318
		public string QOP
		{
			get
			{
				return this.values[4];
			}
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00048124 File Offset: 0x00046324
		public bool Parse()
		{
			if (!this.header.ToLower().StartsWith("digest "))
			{
				return false;
			}
			this.pos = 6;
			this.length = this.header.Length;
			while (this.pos < this.length)
			{
				string text;
				string text2;
				if (!this.GetKeywordAndValue(out text, out text2))
				{
					return false;
				}
				this.SkipWhitespace();
				if (this.pos < this.length && this.header[this.pos] == ',')
				{
					this.pos++;
				}
				int num = Array.IndexOf<string>(DigestHeaderParser.keywords, text);
				if (num != -1)
				{
					if (this.values[num] != null)
					{
						return false;
					}
					this.values[num] = text2;
				}
			}
			return this.Realm != null && this.Nonce != null;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00048214 File Offset: 0x00046414
		private void SkipWhitespace()
		{
			char c = ' ';
			while (this.pos < this.length && (c == ' ' || c == '\t' || c == '\r' || c == '\n'))
			{
				c = this.header[this.pos++];
			}
			this.pos--;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00048288 File Offset: 0x00046488
		private string GetKey()
		{
			this.SkipWhitespace();
			int num = this.pos;
			while (this.pos < this.length && this.header[this.pos] != '=')
			{
				this.pos++;
			}
			return this.header.Substring(num, this.pos - num).Trim().ToLower();
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x00048300 File Offset: 0x00046500
		private bool GetKeywordAndValue(out string key, out string value)
		{
			key = null;
			value = null;
			key = this.GetKey();
			if (this.pos >= this.length)
			{
				return false;
			}
			this.SkipWhitespace();
			if (this.pos + 1 >= this.length || this.header[this.pos++] != '=')
			{
				return false;
			}
			this.SkipWhitespace();
			if (this.pos + 1 >= this.length)
			{
				return false;
			}
			bool flag = false;
			if (this.header[this.pos] == '"')
			{
				this.pos++;
				flag = true;
			}
			int num = this.pos;
			if (flag)
			{
				this.pos = this.header.IndexOf('"', this.pos);
				if (this.pos == -1)
				{
					return false;
				}
			}
			else
			{
				do
				{
					char c = this.header[this.pos];
					if (c == ',' || c == ' ' || c == '\t' || c == '\r' || c == '\n')
					{
						break;
					}
				}
				while (++this.pos < this.length);
				if (this.pos >= this.length && num == this.pos)
				{
					return false;
				}
			}
			value = this.header.Substring(num, this.pos - num);
			this.pos += 2;
			return true;
		}

		// Token: 0x0400103A RID: 4154
		private string header;

		// Token: 0x0400103B RID: 4155
		private int length;

		// Token: 0x0400103C RID: 4156
		private int pos;

		// Token: 0x0400103D RID: 4157
		private static string[] keywords = new string[] { "realm", "opaque", "nonce", "algorithm", "qop" };

		// Token: 0x0400103E RID: 4158
		private string[] values = new string[DigestHeaderParser.keywords.Length];
	}
}
