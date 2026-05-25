using System;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x0200000E RID: 14
	public class Tds80 : Tds70
	{
		// Token: 0x060000EC RID: 236 RVA: 0x0000B270 File Offset: 0x00009470
		public Tds80(string server, int port)
			: this(server, port, 512, 15)
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000B284 File Offset: 0x00009484
		public Tds80(string server, int port, int packetSize, int timeout)
			: base(server, port, packetSize, timeout, Tds80.Version)
		{
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000B2A4 File Offset: 0x000094A4
		protected override byte[] ClientVersion
		{
			get
			{
				return new byte[] { 0, 0, 0, 113 };
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000B2B4 File Offset: 0x000094B4
		public override bool Connect(TdsConnectionParameters connectionParameters)
		{
			return base.Connect(connectionParameters);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000B2C0 File Offset: 0x000094C0
		protected override void ProcessColumnInfo()
		{
			if (base.TdsVersion < TdsVersion.tds80)
			{
				base.ProcessColumnInfo();
				return;
			}
			int tdsShort = (int)base.Comm.GetTdsShort();
			int i = 0;
			while (i < tdsShort)
			{
				byte[] array = new byte[4];
				for (int j = 0; j < 4; j++)
				{
					array[j] = base.Comm.GetByte();
				}
				bool flag = (array[2] & 1) > 0;
				bool flag2 = (array[2] & 12) > 0;
				bool flag3 = (array[2] & 16) > 0;
				bool flag4 = (array[2] & 16) > 0;
				TdsColumnType tdsColumnType = (TdsColumnType)(base.Comm.GetByte() & byte.MaxValue);
				if ((byte)tdsColumnType == 239)
				{
					tdsColumnType = TdsColumnType.NChar;
				}
				TdsColumnType tdsColumnType2 = tdsColumnType;
				if (base.IsLargeType(tdsColumnType) && tdsColumnType != TdsColumnType.NChar)
				{
					tdsColumnType -= 128;
				}
				string text = null;
				int num = 0;
				int num2 = 0;
				int num3;
				if (base.IsBlobType(tdsColumnType))
				{
					num3 = base.Comm.GetTdsInt();
				}
				else if (Tds.IsFixedSizeColumn(tdsColumnType))
				{
					num3 = Tds.LookupBufferSize(tdsColumnType);
				}
				else if (base.IsLargeType(tdsColumnType2))
				{
					num3 = (int)base.Comm.GetTdsShort();
				}
				else
				{
					num3 = (int)(base.Comm.GetByte() & byte.MaxValue);
				}
				if (tdsColumnType2 == TdsColumnType.BigChar || tdsColumnType2 == TdsColumnType.BigNVarChar || tdsColumnType2 == TdsColumnType.BigVarChar || tdsColumnType2 == TdsColumnType.NChar || tdsColumnType2 == TdsColumnType.NVarChar || tdsColumnType2 == TdsColumnType.Text || tdsColumnType2 == TdsColumnType.NText)
				{
					byte[] bytes = base.Comm.GetBytes(5, true);
					num = TdsCollation.LCID(bytes);
					num2 = TdsCollation.SortId(bytes);
				}
				if (base.IsBlobType(tdsColumnType))
				{
					text = base.Comm.GetString((int)base.Comm.GetTdsShort());
				}
				byte b = 0;
				byte b2 = 0;
				TdsColumnType tdsColumnType3 = tdsColumnType;
				switch (tdsColumnType3)
				{
				case TdsColumnType.NVarChar:
					goto IL_0215;
				default:
					if (tdsColumnType3 == TdsColumnType.NText || tdsColumnType3 == TdsColumnType.NChar)
					{
						goto IL_0215;
					}
					break;
				case TdsColumnType.Decimal:
				case TdsColumnType.Numeric:
					b = base.Comm.GetByte();
					b2 = base.Comm.GetByte();
					break;
				}
				IL_023F:
				string @string = base.Comm.GetString((int)base.Comm.GetByte());
				TdsDataColumn tdsDataColumn = new TdsDataColumn();
				base.Columns.Add(tdsDataColumn);
				tdsDataColumn.ColumnType = new TdsColumnType?(tdsColumnType);
				tdsDataColumn.ColumnName = @string;
				tdsDataColumn.IsAutoIncrement = new bool?(flag3);
				tdsDataColumn.IsIdentity = new bool?(flag4);
				tdsDataColumn.ColumnSize = new int?(num3);
				tdsDataColumn.NumericPrecision = new short?((short)b);
				tdsDataColumn.NumericScale = new short?((short)b2);
				tdsDataColumn.IsReadOnly = new bool?(!flag2);
				tdsDataColumn.AllowDBNull = new bool?(flag);
				tdsDataColumn.BaseTableName = text;
				tdsDataColumn.LCID = new int?(num);
				tdsDataColumn.SortOrder = new int?(num2);
				i++;
				continue;
				IL_0215:
				num3 /= 2;
				goto IL_023F;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000B5E8 File Offset: 0x000097E8
		protected override void ProcessOutputParam()
		{
			if (base.TdsVersion < TdsVersion.tds80)
			{
				base.ProcessOutputParam();
				return;
			}
			base.GetSubPacketLength();
			base.Comm.Skip((long)((long)(base.Comm.GetByte() & byte.MaxValue) << 1));
			base.Comm.Skip(1L);
			base.Comm.Skip(4L);
			TdsColumnType @byte = (TdsColumnType)base.Comm.GetByte();
			object columnValue = base.GetColumnValue(new TdsColumnType?(@byte), true);
			base.OutputParameters.Add(columnValue);
		}

		// Token: 0x0400007A RID: 122
		public static readonly TdsVersion Version = TdsVersion.tds80;
	}
}
