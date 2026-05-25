using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000066 RID: 102
	public struct JEnumerable<T> : IJEnumerable<T>, IEnumerable<T>, IEnumerable where T : JToken
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x00011005 File Offset: 0x0000F205
		public JEnumerable(IEnumerable<T> enumerable)
		{
			ValidationUtils.ArgumentNotNull(enumerable, "enumerable");
			this._enumerable = enumerable;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00011019 File Offset: 0x0000F219
		public IEnumerator<T> GetEnumerator()
		{
			return this._enumerable.GetEnumerator();
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00011026 File Offset: 0x0000F226
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170000F2 RID: 242
		public IJEnumerable<JToken> this[object key]
		{
			get
			{
				return new JEnumerable<JToken>(this._enumerable.Values(key));
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00011046 File Offset: 0x0000F246
		public override bool Equals(object obj)
		{
			return obj is JEnumerable<T> && this._enumerable.Equals(((JEnumerable<T>)obj)._enumerable);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00011068 File Offset: 0x0000F268
		public override int GetHashCode()
		{
			return this._enumerable.GetHashCode();
		}

		// Token: 0x04000143 RID: 323
		public static readonly JEnumerable<T> Empty = new JEnumerable<T>(Enumerable.Empty<T>());

		// Token: 0x04000144 RID: 324
		private IEnumerable<T> _enumerable;
	}
}
