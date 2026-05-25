using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200000E RID: 14
	internal class BsonRegex : BsonToken
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003C24 File Offset: 0x00001E24
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003C2C File Offset: 0x00001E2C
		public BsonString Pattern { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003C35 File Offset: 0x00001E35
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003C3D File Offset: 0x00001E3D
		public BsonString Options { get; set; }

		// Token: 0x0600006B RID: 107 RVA: 0x00003C46 File Offset: 0x00001E46
		public BsonRegex(string pattern, string options)
		{
			this.Pattern = new BsonString(pattern, false);
			this.Options = new BsonString(options, false);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003C68 File Offset: 0x00001E68
		public override BsonType Type
		{
			get
			{
				return BsonType.Regex;
			}
		}
	}
}
