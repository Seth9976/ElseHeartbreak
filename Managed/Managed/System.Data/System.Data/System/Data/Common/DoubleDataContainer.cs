using System;

namespace System.Data.Common
{
	// Token: 0x020000A9 RID: 169
	internal sealed class DoubleDataContainer : DataContainer
	{
		// Token: 0x060007B0 RID: 1968 RVA: 0x000250B4 File Offset: 0x000232B4
		protected override object GetValue(int index)
		{
			return this._values[index];
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000250C4 File Offset: 0x000232C4
		protected override void ZeroOut(int index)
		{
			this._values[index] = 0.0;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000250D8 File Offset: 0x000232D8
		protected override void SetValue(int index, object value)
		{
			this._values[index] = (double)base.GetContainerData(value);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000250F0 File Offset: 0x000232F0
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this._values[index] = record.GetDoubleSafe(field);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00025104 File Offset: 0x00023304
		protected override void DoCopyValue(DataContainer from, int from_index, int to_index)
		{
			this._values[to_index] = ((DoubleDataContainer)from)._values[from_index];
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0002511C File Offset: 0x0002331C
		protected override int DoCompareValues(int index1, int index2)
		{
			double num = this._values[index1];
			double num2 = this._values[index2];
			return (num != num2) ? ((num >= num2) ? 1 : (-1)) : 0;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00025158 File Offset: 0x00023358
		protected override void Resize(int size)
		{
			if (this._values == null)
			{
				this._values = new double[size];
				return;
			}
			double[] array = new double[size];
			Array.Copy(this._values, 0, array, 0, this._values.Length);
			this._values = array;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x000251A4 File Offset: 0x000233A4
		internal override long GetInt64(int index)
		{
			return Convert.ToInt64(this._values[index]);
		}

		// Token: 0x040002F6 RID: 758
		private double[] _values;
	}
}
