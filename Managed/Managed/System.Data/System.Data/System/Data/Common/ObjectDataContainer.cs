using System;

namespace System.Data.Common
{
	// Token: 0x020000AA RID: 170
	internal class ObjectDataContainer : DataContainer
	{
		// Token: 0x060007B9 RID: 1977 RVA: 0x000251BC File Offset: 0x000233BC
		protected override object GetValue(int index)
		{
			return this._values[index];
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000251C8 File Offset: 0x000233C8
		protected override void ZeroOut(int index)
		{
			this._values[index] = null;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x000251D4 File Offset: 0x000233D4
		protected override void SetValue(int index, object value)
		{
			this._values[index] = value;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x000251E0 File Offset: 0x000233E0
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this._values[index] = record.GetValue(field);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000251F4 File Offset: 0x000233F4
		protected override void DoCopyValue(DataContainer from, int from_index, int to_index)
		{
			this._values[to_index] = ((ObjectDataContainer)from)._values[from_index];
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0002520C File Offset: 0x0002340C
		protected override int DoCompareValues(int index1, int index2)
		{
			object obj = this._values[index1];
			object obj2 = this._values[index2];
			if (obj == obj2)
			{
				return 0;
			}
			if (obj is IComparable)
			{
				try
				{
					return ((IComparable)obj).CompareTo(obj2);
				}
				catch
				{
					if (obj2 is IComparable)
					{
						obj2 = Convert.ChangeType(obj2, Type.GetTypeCode(obj.GetType()));
						return ((IComparable)obj).CompareTo(obj2);
					}
				}
			}
			return string.Compare(obj.ToString(), obj2.ToString());
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000252BC File Offset: 0x000234BC
		protected override void Resize(int size)
		{
			if (this._values == null)
			{
				this._values = new object[size];
				return;
			}
			object[] array = new object[size];
			Array.Copy(this._values, 0, array, 0, this._values.Length);
			this._values = array;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00025308 File Offset: 0x00023508
		internal override long GetInt64(int index)
		{
			return Convert.ToInt64(this._values[index]);
		}

		// Token: 0x040002F7 RID: 759
		private object[] _values;
	}
}
