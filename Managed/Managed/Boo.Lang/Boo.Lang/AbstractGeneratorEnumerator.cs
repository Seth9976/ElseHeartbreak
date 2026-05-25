using System;
using System.Collections;

namespace Boo.Lang
{
	// Token: 0x02000003 RID: 3
	public abstract class AbstractGeneratorEnumerator : IEnumerator
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000212C File Offset: 0x0000032C
		public AbstractGeneratorEnumerator()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002134 File Offset: 0x00000334
		public object Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000213C File Offset: 0x0000033C
		public void Reset()
		{
			this._state = 0;
		}

		// Token: 0x06000007 RID: 7
		public abstract bool MoveNext();

		// Token: 0x06000008 RID: 8 RVA: 0x00002148 File Offset: 0x00000348
		protected bool Yield(int state, object value)
		{
			this._state = state;
			this._current = value;
			return true;
		}

		// Token: 0x04000001 RID: 1
		protected object _current;

		// Token: 0x04000002 RID: 2
		protected int _state;
	}
}
