using System;
using System.Threading;

namespace RelayLib
{
	// Token: 0x02000011 RID: 17
	public class ValueEntry<T>
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000045A8 File Offset: 0x000027A8
		public ValueEntry()
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000045CC File Offset: 0x000027CC
		internal ValueEntry(T pValue, ValueEntry<T>.DataChangeHandler pOnDataChanged)
		{
			this._value = pValue;
			this.onDataChanged = pOnDataChanged;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000087 RID: 135 RVA: 0x000045FC File Offset: 0x000027FC
		// (remove) Token: 0x06000088 RID: 136 RVA: 0x00004638 File Offset: 0x00002838
		public event ValueEntry<T>.DataChangeHandler onDataChanged
		{
			add
			{
				ValueEntry<T>.DataChangeHandler dataChangeHandler = this.onDataChanged;
				ValueEntry<T>.DataChangeHandler dataChangeHandler2;
				do
				{
					dataChangeHandler2 = dataChangeHandler;
					dataChangeHandler = Interlocked.CompareExchange<ValueEntry<T>.DataChangeHandler>(ref this.onDataChanged, (ValueEntry<T>.DataChangeHandler)Delegate.Combine(dataChangeHandler2, value), dataChangeHandler);
				}
				while (dataChangeHandler != dataChangeHandler2);
			}
			remove
			{
				ValueEntry<T>.DataChangeHandler dataChangeHandler = this.onDataChanged;
				ValueEntry<T>.DataChangeHandler dataChangeHandler2;
				do
				{
					dataChangeHandler2 = dataChangeHandler;
					dataChangeHandler = Interlocked.CompareExchange<ValueEntry<T>.DataChangeHandler>(ref this.onDataChanged, (ValueEntry<T>.DataChangeHandler)Delegate.Remove(dataChangeHandler2, value), dataChangeHandler);
				}
				while (dataChangeHandler != dataChangeHandler2);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000470C File Offset: 0x0000290C
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00004674 File Offset: 0x00002874
		public T data
		{
			get
			{
				return this._value;
			}
			set
			{
				if (this.onDataChanged == null)
				{
					this._value = value;
				}
				else
				{
					if (this._value == null && value == null)
					{
						return;
					}
					if ((this._value != null && !this._value.Equals(value)) || value != null)
					{
						T value2 = this._value;
						this._value = value;
						this.onDataChanged(value2, this._value);
					}
				}
			}
		}

		// Token: 0x0400001F RID: 31
		private T _value = default(T);

		// Token: 0x02000012 RID: 18
		// (Invoke) Token: 0x0600008C RID: 140
		public delegate void DataChangeHandler(T pOldValue, T pNewValue);
	}
}
