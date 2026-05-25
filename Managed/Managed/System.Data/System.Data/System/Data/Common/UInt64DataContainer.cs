using System;

namespace System.Data.Common
{
	// Token: 0x020000A7 RID: 167
	internal sealed class UInt64DataContainer : DataContainer
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x00024EB0 File Offset: 0x000230B0
		protected override object GetValue(int index)
		{
			return this._values[index];
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00024EC0 File Offset: 0x000230C0
		protected override void ZeroOut(int index)
		{
			this._values[index] = 0UL;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00024ECC File Offset: 0x000230CC
		protected override void SetValue(int index, object value)
		{
			this._values[index] = (ulong)base.GetContainerData(value);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00024EE4 File Offset: 0x000230E4
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this._values[index] = (ulong)record.GetInt64Safe(field);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00024EF8 File Offset: 0x000230F8
		protected override void DoCopyValue(DataContainer from, int from_index, int to_index)
		{
			this._values[to_index] = ((UInt64DataContainer)from)._values[from_index];
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00024F10 File Offset: 0x00023110
		protected override int DoCompareValues(int index1, int index2)
		{
			ulong num = this._values[index1];
			ulong num2 = this._values[index2];
			return (num != num2) ? ((num >= num2) ? 1 : (-1)) : 0;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00024F4C File Offset: 0x0002314C
		protected override void Resize(int size)
		{
			if (this._values == null)
			{
				this._values = new ulong[size];
				return;
			}
			ulong[] array = new ulong[size];
			Array.Copy(this._values, 0, array, 0, this._values.Length);
			this._values = array;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00024F98 File Offset: 0x00023198
		internal override long GetInt64(int index)
		{
			return Convert.ToInt64(this._values[index]);
		}

		// Token: 0x040002F4 RID: 756
		private ulong[] _values;
	}
}
