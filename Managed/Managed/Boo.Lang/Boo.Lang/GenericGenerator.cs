using System;
using System.Collections;
using System.Collections.Generic;

namespace Boo.Lang
{
	// Token: 0x02000017 RID: 23
	public abstract class GenericGenerator<T> : IEnumerable, IEnumerable<T>
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002F48 File Offset: 0x00001148
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600005B RID: 91
		public abstract IEnumerator<T> GetEnumerator();

		// Token: 0x0600005C RID: 92 RVA: 0x00002F50 File Offset: 0x00001150
		public override string ToString()
		{
			return string.Format("generator({0})", typeof(T));
		}
	}
}
