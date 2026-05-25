using System;
using System.Collections;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x02000010 RID: 16
	public class TdsBulkCopy
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x0000B6B8 File Offset: 0x000098B8
		public TdsBulkCopy(Tds tds)
		{
			this.tds = tds;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000B6C8 File Offset: 0x000098C8
		public bool SendColumnMetaData(string colMetaData)
		{
			this.tds.Comm.StartPacket(TdsPacketType.Query);
			this.tds.Comm.Append(colMetaData);
			this.tds.ExecBulkCopyMetaData(30, false);
			return true;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000B708 File Offset: 0x00009908
		public bool BulkCopyStart(TdsMetaParameterCollection parameters)
		{
			this.tds.Comm.StartPacket(TdsPacketType.Bulk);
			this.tds.Comm.Append(129);
			short num = 0;
			foreach (object obj in ((IEnumerable)parameters))
			{
				TdsMetaParameter tdsMetaParameter = (TdsMetaParameter)obj;
				if (tdsMetaParameter.Value == null)
				{
					num += 1;
				}
			}
			this.tds.Comm.Append(num);
			if (parameters != null)
			{
				foreach (object obj2 in ((IEnumerable)parameters))
				{
					TdsMetaParameter tdsMetaParameter2 = (TdsMetaParameter)obj2;
					if (tdsMetaParameter2.Value == null)
					{
						this.tds.Comm.Append(0);
						this.tds.Comm.Append(10);
						this.WriteParameterInfo(tdsMetaParameter2);
						this.tds.Comm.Append((byte)tdsMetaParameter2.ParameterName.Length);
						this.tds.Comm.Append(tdsMetaParameter2.ParameterName);
					}
				}
			}
			return true;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000B888 File Offset: 0x00009A88
		public bool BulkCopyData(object o, int size, bool isNewRow)
		{
			if (isNewRow)
			{
				this.tds.Comm.Append(209);
			}
			if (size > 0)
			{
				this.tds.Comm.Append((short)size);
			}
			this.tds.Comm.Append(o);
			return true;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000B8DC File Offset: 0x00009ADC
		public bool BulkCopyEnd()
		{
			this.tds.Comm.Append(253);
			this.tds.ExecBulkCopy(30, false);
			return true;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000B910 File Offset: 0x00009B10
		private void WriteParameterInfo(TdsMetaParameter param)
		{
			param.IsNullable = true;
			TdsColumnType metaType = param.GetMetaType();
			param.IsNullable = false;
			this.tds.Comm.Append((byte)metaType);
			int num;
			if (param.Size == 0)
			{
				num = param.GetActualSize();
			}
			else
			{
				num = param.Size;
			}
			if (metaType == TdsColumnType.BigNVarChar)
			{
				num <<= 1;
			}
			if (this.tds.IsLargeType(metaType))
			{
				this.tds.Comm.Append((short)num);
			}
			else if (this.tds.IsBlobType(metaType))
			{
				this.tds.Comm.Append(num);
			}
			else
			{
				this.tds.Comm.Append((byte)num);
			}
			if (param.TypeName == "decimal" || param.TypeName == "numeric")
			{
				this.tds.Comm.Append((param.Precision == 0) ? 29 : param.Precision);
				this.tds.Comm.Append(param.Scale);
			}
		}

		// Token: 0x0400007F RID: 127
		private Tds tds;
	}
}
