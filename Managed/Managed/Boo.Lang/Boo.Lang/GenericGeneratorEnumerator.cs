using System;
using System.Collections;
using System.Collections.Generic;

namespace Boo.Lang
{
	// Token: 0x02000018 RID: 24
	public abstract class GenericGeneratorEnumerator<T> : IEnumerator, IDisposable, IEnumerator<T>
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002F68 File Offset: 0x00001168
		public GenericGeneratorEnumerator()
		{
			this._state = 0;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002F78 File Offset: 0x00001178
		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002F88 File Offset: 0x00001188
		public T Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F90 File Offset: 0x00001190
		public virtual void Dispose()
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F94 File Offset: 0x00001194
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000062 RID: 98
		public abstract bool MoveNext();

		// Token: 0x06000063 RID: 99 RVA: 0x00002F9C File Offset: 0x0000119C
		protected bool Yield(int state, T value)
		{
			this._state = state;
			this._current = value;
			return true;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002FB0 File Offset: 0x000011B0
		protected bool YieldDefault(int state)
		{
			this._state = state;
			this._current = default(T);
			return true;
		}

		// Token: 0x04000012 RID: 18
		protected T _current;

		// Token: 0x04000013 RID: 19
		public int _state;
	}
}
