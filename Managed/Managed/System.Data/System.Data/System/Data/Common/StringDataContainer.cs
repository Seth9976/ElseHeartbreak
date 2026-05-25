using System;

namespace System.Data.Common
{
	// Token: 0x020000AD RID: 173
	internal sealed class StringDataContainer : ObjectDataContainer
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x00025380 File Offset: 0x00023580
		private void SetValue(int index, string value)
		{
			if (value != null && base.Column.MaxLength >= 0 && base.Column.MaxLength < value.Length)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Cannot set column '",
					base.Column.ColumnName,
					"' to '",
					value,
					"'. The value violates the MaxLength limit of this column."
				}));
			}
			base.SetValue(index, value);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00025400 File Offset: 0x00023600
		protected override void SetValue(int index, object value)
		{
			this.SetValue(index, (string)base.GetContainerData(value));
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00025418 File Offset: 0x00023618
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			this.SetValue(index, record.GetStringSafe(field));
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00025428 File Offset: 0x00023628
		protected override int DoCompareValues(int index1, int index2)
		{
			DataTable table = base.Column.Table;
			return string.Compare((string)base[index1], (string)base[index2], !table.CaseSensitive, table.Locale);
		}
	}
}
