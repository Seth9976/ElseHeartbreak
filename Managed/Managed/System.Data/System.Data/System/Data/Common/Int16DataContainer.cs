using System;

namespace System.Data.Common
{
	// Token: 0x020000A2 RID: 162
	internal sealed class Int16DataContainer : DataContainer
	{
		// Token: 0x06000771 RID: 1905 RVA: 0x000249F4 File Offset: 0x00022BF4
		protected override object GetValue(int index)
		{
			return this._values[index];
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00024A04 File Offset: 0x00022C04
		protected override void ZeroOut(int index)
		{
			this._values[index] = 0;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00024A10 File Offset: 0x00022C10
		protected override void SetValue(int index, object value)
		{
			this._values[index] = (short)base.GetContainerData(value);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00024A28 File Offset: 0x00022C28
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this._values[index] = record.GetInt16Safe(field);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00024A3C File Offset: 0x00022C3C
		protected override void DoCopyValue(DataContainer from, int from_index, int to_index)
		{
			this._values[to_index] = ((Int16DataContainer)from)._values[from_index];
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00024A54 File Offset: 0x00022C54
		protected override int DoCompareValues(int index1, int index2)
		{
			int num = (int)this._values[index1];
			int num2 = (int)this._values[index2];
			return num - num2;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00024A78 File Offset: 0x00022C78
		protected override void Resize(int size)
		{
			if (this._values == null)
			{
				this._values = new short[size];
				return;
			}
			short[] array = new short[size];
			Array.Copy(this._values, 0, array, 0, this._values.Length);
			this._values = array;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00024AC4 File Offset: 0x00022CC4
		internal override long GetInt64(int index)
		{
			return (long)this._values[index];
		}

		// Token: 0x040002EF RID: 751
		private short[] _values;
	}
}
