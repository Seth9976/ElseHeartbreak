using System;

namespace System.Data.Common
{
	// Token: 0x020000A8 RID: 168
	internal sealed class SingleDataContainer : DataContainer
	{
		// Token: 0x060007A7 RID: 1959 RVA: 0x00024FB0 File Offset: 0x000231B0
		protected override object GetValue(int index)
		{
			return this._values[index];
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00024FC0 File Offset: 0x000231C0
		protected override void ZeroOut(int index)
		{
			this._values[index] = 0f;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00024FD0 File Offset: 0x000231D0
		protected override void SetValue(int index, object value)
		{
			this._values[index] = (float)base.GetContainerData(value);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00024FE8 File Offset: 0x000231E8
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this._values[index] = record.GetFloatSafe(field);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00024FFC File Offset: 0x000231FC
		protected override void DoCopyValue(DataContainer from, int from_index, int to_index)
		{
			this._values[to_index] = ((SingleDataContainer)from)._values[from_index];
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00025014 File Offset: 0x00023214
		protected override int DoCompareValues(int index1, int index2)
		{
			float num = this._values[index1];
			float num2 = this._values[index2];
			return (num != num2) ? ((num >= num2) ? 1 : (-1)) : 0;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00025050 File Offset: 0x00023250
		protected override void Resize(int size)
		{
			if (this._values == null)
			{
				this._values = new float[size];
				return;
			}
			float[] array = new float[size];
			Array.Copy(this._values, 0, array, 0, this._values.Length);
			this._values = array;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0002509C File Offset: 0x0002329C
		internal override long GetInt64(int index)
		{
			return Convert.ToInt64(this._values[index]);
		}

		// Token: 0x040002F5 RID: 757
		private float[] _values;
	}
}
