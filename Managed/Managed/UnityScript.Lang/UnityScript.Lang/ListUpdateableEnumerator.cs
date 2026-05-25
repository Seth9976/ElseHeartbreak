using System;
using System.Collections;

namespace UnityScript.Lang
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	internal class ListUpdateableEnumerator : IEnumerator
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002A00 File Offset: 0x00000C00
		public ListUpdateableEnumerator(IList list)
		{
			this._current = -1;
			this._list = list;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A18 File Offset: 0x00000C18
		public override void Reset()
		{
			this._current = -1;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A24 File Offset: 0x00000C24
		public override bool MoveNext()
		{
			checked
			{
				this._current++;
				return this._current < this._list.Count;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002A48 File Offset: 0x00000C48
		public override object Current
		{
			get
			{
				return this._list[this._current];
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A5C File Offset: 0x00000C5C
		public void Update(object newValue)
		{
			this._list[this._current] = newValue;
		}

		// Token: 0x04000007 RID: 7
		protected IList _list;

		// Token: 0x04000008 RID: 8
		protected int _current;
	}
}
