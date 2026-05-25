using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000055 RID: 85
	internal class MimeEncodedStream
	{
		// Token: 0x06000375 RID: 885 RVA: 0x000102B4 File Offset: 0x0000E4B4
		public MimeEncodedStream(string id, string contentEncoding, string value)
		{
			this.Id = id;
			this.ContentEncoding = contentEncoding;
			this.EncodedString = value;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000376 RID: 886 RVA: 0x000102DC File Offset: 0x0000E4DC
		// (set) Token: 0x06000377 RID: 887 RVA: 0x000102E4 File Offset: 0x0000E4E4
		public string Id { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000378 RID: 888 RVA: 0x000102F0 File Offset: 0x0000E4F0
		// (set) Token: 0x06000379 RID: 889 RVA: 0x000102F8 File Offset: 0x0000E4F8
		public string ContentEncoding { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00010304 File Offset: 0x0000E504
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0001030C File Offset: 0x0000E50C
		public string EncodedString { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00010318 File Offset: 0x0000E518
		public string DecodedBase64String
		{
			get
			{
				return Convert.ToBase64String(Encoding.ASCII.GetBytes(this.EncodedString));
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00010330 File Offset: 0x0000E530
		public TextReader CreateTextReader()
		{
			string contentEncoding = this.ContentEncoding;
			if (contentEncoding != null)
			{
				if (MimeEncodedStream.<>f__switch$map9 == null)
				{
					MimeEncodedStream.<>f__switch$map9 = new Dictionary<string, int>(2)
					{
						{ "7bit", 0 },
						{ "8bit", 0 }
					};
				}
				int num;
				if (MimeEncodedStream.<>f__switch$map9.TryGetValue(contentEncoding, out num))
				{
					if (num == 0)
					{
						return new StringReader(this.EncodedString);
					}
				}
			}
			return new StringReader(this.DecodedBase64String);
		}
	}
}
