using System;

namespace System.Data.Common
{
	// Token: 0x0200009F RID: 159
	internal sealed class CharDataContainer : DataContainer
	{
		// Token: 0x06000756 RID: 1878 RVA: 0x0002472C File Offset: 0x0002292C
		protected override object GetValue(int index)
		{
			return this._values[index];
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0002473C File Offset: 0x0002293C
		protected override void ZeroOut(int index)
		{
			this._values[index] = '\0';
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00024748 File Offset: 0x00022948
		protected override void SetValue(int index, object value)
		{
			this._values[index] = (char)base.GetContainerData(value);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00024760 File Offset: 0x00022960
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this._values[index] = record.GetCharSafe(field);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00024774 File Offset: 0x00022974
		protected override void DoCopyValue(DataContainer from, int from_index, int to_index)
		{
			this._values[to_index] = ((CharDataContainer)from)._values[from_index];
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0002478C File Offset: 0x0002298C
		protected override int DoCompareValues(int index1, int index2)
		{
			char c = this._values[index1];
			char c2 = this._values[index2];
			return (c != c2) ? ((c >= c2) ? 1 : (-1)) : 0;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000247C8 File Offset: 0x000229C8
		protected override void Resize(int size)
		{
			if (this._values == null)
			{
				this._values = new char[size];
				return;
			}
			char[] array = new char[size];
			Array.Copy(this._values, 0, array, 0, this._values.Length);
			this._values = array;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00024814 File Offset: 0x00022A14
		internal override long GetInt64(int index)
		{
			return Convert.ToInt64(this._values[index]);
		}

		// Token: 0x040002EC RID: 748
		private char[] _values;
	}
}
