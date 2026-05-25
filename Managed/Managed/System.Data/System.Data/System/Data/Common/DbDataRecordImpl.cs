using System;

namespace System.Data.Common
{
	// Token: 0x020000C2 RID: 194
	internal class DbDataRecordImpl : DbDataRecord
	{
		// Token: 0x06000983 RID: 2435 RVA: 0x0002E3DC File Offset: 0x0002C5DC
		internal DbDataRecordImpl(SchemaInfo[] schema, object[] values)
		{
			this.schema = schema;
			this.values = values;
			this.fieldCount = values.Length;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0002E3FC File Offset: 0x0002C5FC
		public override int FieldCount
		{
			get
			{
				return this.fieldCount;
			}
		}

		// Token: 0x170001B2 RID: 434
		public override object this[string name]
		{
			get
			{
				return this[this.GetOrdinal(name)];
			}
		}

		// Token: 0x170001B3 RID: 435
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002E420 File Offset: 0x0002C620
		public override bool GetBoolean(int i)
		{
			return (bool)this.GetValue(i);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002E430 File Offset: 0x0002C630
		public override byte GetByte(int i)
		{
			return (byte)this.GetValue(i);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0002E440 File Offset: 0x0002C640
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			object value = this.GetValue(i);
			if (!(value is byte[]))
			{
				throw new InvalidCastException("Type is " + value.GetType().ToString());
			}
			if (buffer == null)
			{
				return (long)((byte[])value).Length;
			}
			Array.Copy((byte[])value, (int)dataIndex, buffer, bufferIndex, length);
			return (long)((byte[])value).Length - dataIndex;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0002E4A8 File Offset: 0x0002C6A8
		public override char GetChar(int i)
		{
			return (char)this.GetValue(i);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002E4B8 File Offset: 0x0002C6B8
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			object value = this.GetValue(i);
			char[] array;
			if (value is char[])
			{
				array = (char[])value;
			}
			else
			{
				if (!(value is string))
				{
					throw new InvalidCastException("Type is " + value.GetType().ToString());
				}
				array = ((string)value).ToCharArray();
			}
			if (buffer == null)
			{
				return (long)array.Length;
			}
			Array.Copy(array, (int)dataIndex, buffer, bufferIndex, length);
			return (long)array.Length - dataIndex;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0002E53C File Offset: 0x0002C73C
		public override string GetDataTypeName(int i)
		{
			return this.schema[i].DataTypeName;
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0002E54C File Offset: 0x0002C74C
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this.GetValue(i);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0002E55C File Offset: 0x0002C75C
		[MonoTODO]
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002E564 File Offset: 0x0002C764
		public override decimal GetDecimal(int i)
		{
			return (decimal)this.GetValue(i);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002E574 File Offset: 0x0002C774
		public override double GetDouble(int i)
		{
			return (double)this.GetValue(i);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002E584 File Offset: 0x0002C784
		public override Type GetFieldType(int i)
		{
			return this.schema[i].FieldType;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002E594 File Offset: 0x0002C794
		public override float GetFloat(int i)
		{
			return (float)this.GetValue(i);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0002E5A4 File Offset: 0x0002C7A4
		public override Guid GetGuid(int i)
		{
			return (Guid)this.GetValue(i);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002E5B4 File Offset: 0x0002C7B4
		public override short GetInt16(int i)
		{
			return (short)this.GetValue(i);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0002E5C4 File Offset: 0x0002C7C4
		public override int GetInt32(int i)
		{
			return (int)this.GetValue(i);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0002E5D4 File Offset: 0x0002C7D4
		public override long GetInt64(int i)
		{
			return (long)this.GetValue(i);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0002E5E4 File Offset: 0x0002C7E4
		public override string GetName(int i)
		{
			return this.schema[i].ColumnName;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0002E5F4 File Offset: 0x0002C7F4
		public override int GetOrdinal(string name)
		{
			for (int i = 0; i < this.FieldCount; i++)
			{
				if (this.schema[i].ColumnName == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0002E634 File Offset: 0x0002C834
		public override string GetString(int i)
		{
			return (string)this.GetValue(i);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0002E644 File Offset: 0x0002C844
		public override object GetValue(int i)
		{
			if (i < 0 || i > this.fieldCount)
			{
				throw new IndexOutOfRangeException();
			}
			return this.values[i];
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0002E668 File Offset: 0x0002C868
		public override int GetValues(object[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int num = ((values.Length <= this.values.Length) ? values.Length : this.values.Length);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.values[i];
			}
			return num;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0002E6C8 File Offset: 0x0002C8C8
		public override bool IsDBNull(int i)
		{
			return this.GetValue(i) == DBNull.Value;
		}

		// Token: 0x04000334 RID: 820
		private readonly SchemaInfo[] schema;

		// Token: 0x04000335 RID: 821
		private readonly object[] values;

		// Token: 0x04000336 RID: 822
		private readonly int fieldCount;
	}
}
