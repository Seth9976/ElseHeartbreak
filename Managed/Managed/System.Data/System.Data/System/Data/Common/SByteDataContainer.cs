using System;

namespace System.Data.Common
{
	// Token: 0x020000A1 RID: 161
	internal sealed class SByteDataContainer : DataContainer
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x00024910 File Offset: 0x00022B10
		protected override object GetValue(int index)
		{
			return this._values[index];
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00024920 File Offset: 0x00022B20
		protected override void ZeroOut(int index)
		{
			this._values[index] = 0;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0002492C File Offset: 0x00022B2C
		protected override void SetValue(int index, object value)
		{
			this._values[index] = (sbyte)base.GetContainerData(value);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00024944 File Offset: 0x00022B44
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this._values[index] = (sbyte)record.GetByteSafe(field);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00024958 File Offset: 0x00022B58
		protected override void DoCopyValue(DataContainer from, int from_index, int to_index)
		{
			this._values[to_index] = ((SByteDataContainer)from)._values[from_index];
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00024970 File Offset: 0x00022B70
		protected override int DoCompareValues(int index1, int index2)
		{
			int num = (int)this._values[index1];
			int num2 = (int)this._values[index2];
			return num - num2;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00024994 File Offset: 0x00022B94
		protected override void Resize(int size)
		{
			if (this._values == null)
			{
				this._values = new sbyte[size];
				return;
			}
			sbyte[] array = new sbyte[size];
			Array.Copy(this._values, 0, array, 0, this._values.Length);
			this._values = array;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000249E0 File Offset: 0x00022BE0
		internal override long GetInt64(int index)
		{
			return (long)this._values[index];
		}

		// Token: 0x040002EE RID: 750
		private sbyte[] _values;
	}
}
