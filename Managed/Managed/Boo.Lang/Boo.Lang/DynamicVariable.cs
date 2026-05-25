using System;

namespace Boo.Lang
{
	// Token: 0x0200000A RID: 10
	public class DynamicVariable<T>
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000029B8 File Offset: 0x00000BB8
		public DynamicVariable()
		{
			this._current = default(T);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029DC File Offset: 0x00000BDC
		public DynamicVariable(T initialValue)
		{
			this._current = initialValue;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000029EC File Offset: 0x00000BEC
		public T Value
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029F4 File Offset: 0x00000BF4
		[Obsolete("Use With(T, System.Action) and access the variable value directly from the closure")]
		public void With(T value, Action<T> code)
		{
			this.With(value, delegate
			{
				code.Invoke(value);
			});
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A30 File Offset: 0x00000C30
		public void With(T value, Procedure code)
		{
			T current = this._current;
			this._current = value;
			try
			{
				code();
			}
			finally
			{
				this._current = current;
			}
		}

		// Token: 0x04000006 RID: 6
		private T _current;
	}
}
