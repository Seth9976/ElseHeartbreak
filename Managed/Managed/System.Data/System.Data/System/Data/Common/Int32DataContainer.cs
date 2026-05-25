using System;

namespace System.Data.Common
{
	// Token: 0x020000A4 RID: 164
	internal sealed class Int32DataContainer : DataContainer
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x00024BBC File Offset: 0x00022DBC
		protected override object GetValue(int index)
		{
			return this._values[index];
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00024BCC File Offset: 0x00022DCC
		protected override void ZeroOut(int index)
		{
			this._values[index] = 0;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00024BD8 File Offset: 0x00022DD8
		protected override void SetValue(int index, object value)
		{
			this._values[index] = (int)base.GetContainerData(value);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00024BF0 File Offset: 0x00022DF0
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this._values[index] = record.GetInt32Safe(field);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00024C04 File Offset: 0x00022E04
		protected override void DoCopyValue(DataContainer from, int from_index, int to_index)
		{
			this._values[to_index] = ((Int32DataContainer)from)._values[from_index];
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00024C1C File Offset: 0x00022E1C
		protected override int DoCompareValues(int index1, int index2)
		{
			int num = this._values[index1];
			int num2 = this._values[index2];
			return (num != num2) ? ((num >= num2) ? 1 : (-1)) : 0;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00024C58 File Offset: 0x00022E58
		protected override void Resize(int size)
		{
			if (this._values == null)
			{
				this._values = new int[size];
				return;
			}
			int[] array = new int[size];
			Array.Copy(this._values, 0, array, 0, this._values.Length);
			this._values = array;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00024CA4 File Offset: 0x00022EA4
		internal override long GetInt64(int index)
		{
			return (long)this._values[index];
		}

		// Token: 0x040002F1 RID: 753
		private int[] _values;
	}
}
