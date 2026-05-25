using System;

namespace System.Data.Common
{
	// Token: 0x020000A3 RID: 163
	internal sealed class UInt16DataContainer : DataContainer
	{
		// Token: 0x0600077A RID: 1914 RVA: 0x00024AD8 File Offset: 0x00022CD8
		protected override object GetValue(int index)
		{
			return this._values[index];
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00024AE8 File Offset: 0x00022CE8
		protected override void ZeroOut(int index)
		{
			this._values[index] = 0;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00024AF4 File Offset: 0x00022CF4
		protected override void SetValue(int index, object value)
		{
			this._values[index] = (ushort)base.GetContainerData(value);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00024B0C File Offset: 0x00022D0C
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this._values[index] = (ushort)record.GetInt16Safe(field);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00024B20 File Offset: 0x00022D20
		protected override void DoCopyValue(DataContainer from, int from_index, int to_index)
		{
			this._values[to_index] = ((UInt16DataContainer)from)._values[from_index];
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00024B38 File Offset: 0x00022D38
		protected override int DoCompareValues(int index1, int index2)
		{
			int num = (int)this._values[index1];
			int num2 = (int)this._values[index2];
			return num - num2;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00024B5C File Offset: 0x00022D5C
		protected override void Resize(int size)
		{
			if (this._values == null)
			{
				this._values = new ushort[size];
				return;
			}
			ushort[] array = new ushort[size];
			Array.Copy(this._values, 0, array, 0, this._values.Length);
			this._values = array;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00024BA8 File Offset: 0x00022DA8
		internal override long GetInt64(int index)
		{
			return (long)this._values[index];
		}

		// Token: 0x040002F0 RID: 752
		private ushort[] _values;
	}
}
