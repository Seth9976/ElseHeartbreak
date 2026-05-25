using System;

namespace System.Data.Common
{
	// Token: 0x020000A6 RID: 166
	internal sealed class Int64DataContainer : DataContainer
	{
		// Token: 0x06000795 RID: 1941 RVA: 0x00024DB4 File Offset: 0x00022FB4
		protected override object GetValue(int index)
		{
			return this._values[index];
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00024DC4 File Offset: 0x00022FC4
		protected override void ZeroOut(int index)
		{
			this._values[index] = 0L;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00024DD0 File Offset: 0x00022FD0
		protected override void SetValue(int index, object value)
		{
			this._values[index] = (long)base.GetContainerData(value);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00024DE8 File Offset: 0x00022FE8
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this._values[index] = record.GetInt64Safe(field);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00024DFC File Offset: 0x00022FFC
		protected override void DoCopyValue(DataContainer from, int from_index, int to_index)
		{
			this._values[to_index] = ((Int64DataContainer)from)._values[from_index];
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00024E14 File Offset: 0x00023014
		protected override int DoCompareValues(int index1, int index2)
		{
			long num = this._values[index1];
			long num2 = this._values[index2];
			return (num != num2) ? ((num >= num2) ? 1 : (-1)) : 0;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00024E50 File Offset: 0x00023050
		protected override void Resize(int size)
		{
			if (this._values == null)
			{
				this._values = new long[size];
				return;
			}
			long[] array = new long[size];
			Array.Copy(this._values, 0, array, 0, this._values.Length);
			this._values = array;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00024E9C File Offset: 0x0002309C
		internal override long GetInt64(int index)
		{
			return this._values[index];
		}

		// Token: 0x040002F3 RID: 755
		private long[] _values;
	}
}
