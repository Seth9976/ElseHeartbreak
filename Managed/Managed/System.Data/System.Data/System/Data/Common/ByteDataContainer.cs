using System;

namespace System.Data.Common
{
	// Token: 0x020000A0 RID: 160
	internal sealed class ByteDataContainer : DataContainer
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x0002482C File Offset: 0x00022A2C
		protected override object GetValue(int index)
		{
			return this._values[index];
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0002483C File Offset: 0x00022A3C
		protected override void ZeroOut(int index)
		{
			this._values[index] = 0;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00024848 File Offset: 0x00022A48
		protected override void SetValue(int index, object value)
		{
			this._values[index] = (byte)base.GetContainerData(value);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00024860 File Offset: 0x00022A60
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this._values[index] = record.GetByteSafe(field);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00024874 File Offset: 0x00022A74
		protected override void DoCopyValue(DataContainer from, int from_index, int to_index)
		{
			this._values[to_index] = ((ByteDataContainer)from)._values[from_index];
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0002488C File Offset: 0x00022A8C
		protected override int DoCompareValues(int index1, int index2)
		{
			int num = (int)this._values[index1];
			int num2 = (int)this._values[index2];
			return num - num2;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000248B0 File Offset: 0x00022AB0
		protected override void Resize(int size)
		{
			if (this._values == null)
			{
				this._values = new byte[size];
				return;
			}
			byte[] array = new byte[size];
			Array.Copy(this._values, 0, array, 0, this._values.Length);
			this._values = array;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000248FC File Offset: 0x00022AFC
		internal override long GetInt64(int index)
		{
			return (long)this._values[index];
		}

		// Token: 0x040002ED RID: 749
		private byte[] _values;
	}
}
