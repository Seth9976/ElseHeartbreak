using System;

namespace System.Data.Common
{
	// Token: 0x020000A5 RID: 165
	internal sealed class UInt32DataContainer : DataContainer
	{
		// Token: 0x0600078C RID: 1932 RVA: 0x00024CB8 File Offset: 0x00022EB8
		protected override object GetValue(int index)
		{
			return this._values[index];
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00024CC8 File Offset: 0x00022EC8
		protected override void ZeroOut(int index)
		{
			this._values[index] = 0U;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00024CD4 File Offset: 0x00022ED4
		protected override void SetValue(int index, object value)
		{
			this._values[index] = (uint)base.GetContainerData(value);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00024CEC File Offset: 0x00022EEC
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this._values[index] = (uint)record.GetInt32Safe(field);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00024D00 File Offset: 0x00022F00
		protected override void DoCopyValue(DataContainer from, int from_index, int to_index)
		{
			this._values[to_index] = ((UInt32DataContainer)from)._values[from_index];
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00024D18 File Offset: 0x00022F18
		protected override int DoCompareValues(int index1, int index2)
		{
			uint num = this._values[index1];
			uint num2 = this._values[index2];
			return (num != num2) ? ((num >= num2) ? 1 : (-1)) : 0;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00024D54 File Offset: 0x00022F54
		protected override void Resize(int size)
		{
			if (this._values == null)
			{
				this._values = new uint[size];
				return;
			}
			uint[] array = new uint[size];
			Array.Copy(this._values, 0, array, 0, this._values.Length);
			this._values = array;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00024DA0 File Offset: 0x00022FA0
		internal override long GetInt64(int index)
		{
			return (long)((ulong)this._values[index]);
		}

		// Token: 0x040002F2 RID: 754
		private uint[] _values;
	}
}
