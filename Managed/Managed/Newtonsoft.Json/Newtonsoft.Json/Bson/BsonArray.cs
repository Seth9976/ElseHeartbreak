using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200000B RID: 11
	internal class BsonArray : BsonToken, IEnumerable<BsonToken>, IEnumerable
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003B86 File Offset: 0x00001D86
		public void Add(BsonToken token)
		{
			this._children.Add(token);
			token.Parent = this;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003B9B File Offset: 0x00001D9B
		public override BsonType Type
		{
			get
			{
				return BsonType.Array;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003B9E File Offset: 0x00001D9E
		public IEnumerator<BsonToken> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003BB0 File Offset: 0x00001DB0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000043 RID: 67
		private readonly List<BsonToken> _children = new List<BsonToken>();
	}
}
