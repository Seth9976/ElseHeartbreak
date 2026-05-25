using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x0200009E RID: 158
	internal sealed class BitDataContainer : DataContainer
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x0002461C File Offset: 0x0002281C
		protected override object GetValue(int index)
		{
			return this._values[index];
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00024630 File Offset: 0x00022830
		protected override void ZeroOut(int index)
		{
			this._values[index] = false;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00024640 File Offset: 0x00022840
		protected override void SetValue(int index, object value)
		{
			this._values[index] = (bool)base.GetContainerData(value);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0002465C File Offset: 0x0002285C
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this._values[index] = record.GetBooleanSafe(field);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00024674 File Offset: 0x00022874
		protected override void DoCopyValue(DataContainer from, int from_index, int to_index)
		{
			this._values[to_index] = ((BitDataContainer)from)._values[from_index];
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00024694 File Offset: 0x00022894
		protected override int DoCompareValues(int index1, int index2)
		{
			bool flag = this._values[index1];
			bool flag2 = this._values[index2];
			return (flag != flag2) ? ((!flag) ? (-1) : 1) : 0;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000246D8 File Offset: 0x000228D8
		protected override void Resize(int size)
		{
			if (this._values == null)
			{
				this._values = new BitArray(size);
			}
			else
			{
				this._values.Length = size;
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00024710 File Offset: 0x00022910
		internal override long GetInt64(int index)
		{
			return Convert.ToInt64(this._values[index]);
		}

		// Token: 0x040002EB RID: 747
		private BitArray _values;
	}
}
