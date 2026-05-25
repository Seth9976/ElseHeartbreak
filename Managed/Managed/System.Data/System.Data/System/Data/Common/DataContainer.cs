using System;
using System.Collections;
using System.Reflection;

namespace System.Data.Common
{
	// Token: 0x0200009D RID: 157
	internal abstract class DataContainer
	{
		// Token: 0x06000735 RID: 1845
		protected abstract object GetValue(int index);

		// Token: 0x06000736 RID: 1846
		internal abstract long GetInt64(int index);

		// Token: 0x06000737 RID: 1847
		protected abstract void ZeroOut(int index);

		// Token: 0x06000738 RID: 1848
		protected abstract void SetValue(int index, object value);

		// Token: 0x06000739 RID: 1849
		protected abstract void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field);

		// Token: 0x0600073A RID: 1850
		protected abstract void DoCopyValue(DataContainer from, int from_index, int to_index);

		// Token: 0x0600073B RID: 1851
		protected abstract int DoCompareValues(int index1, int index2);

		// Token: 0x0600073C RID: 1852
		protected abstract void Resize(int length);

		// Token: 0x17000152 RID: 338
		internal object this[int index]
		{
			get
			{
				return (!this.IsNull(index)) ? this.GetValue(index) : DBNull.Value;
			}
			set
			{
				if (value == null)
				{
					this.CopyValue(this.Column.Table.DefaultValuesRowIndex, index);
					return;
				}
				bool flag = value == DBNull.Value;
				if (flag)
				{
					this.ZeroOut(index);
				}
				else
				{
					this.SetValue(index, value);
				}
				this.null_values[index] = flag;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x000241CC File Offset: 0x000223CC
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x000241EC File Offset: 0x000223EC
		internal int Capacity
		{
			get
			{
				return (this.null_values == null) ? 0 : this.null_values.Count;
			}
			set
			{
				int capacity = this.Capacity;
				if (value == capacity)
				{
					return;
				}
				if (this.null_values == null)
				{
					this.null_values = new BitArray(value);
				}
				else
				{
					this.null_values.Length = value;
				}
				this.Resize(value);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x00024238 File Offset: 0x00022438
		internal Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x00024240 File Offset: 0x00022440
		protected DataColumn Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00024248 File Offset: 0x00022448
		internal static DataContainer Create(Type type, DataColumn column)
		{
			DataContainer dataContainer;
			switch (Type.GetTypeCode(type))
			{
			case TypeCode.Boolean:
				dataContainer = new BitDataContainer();
				goto IL_0104;
			case TypeCode.Char:
				dataContainer = new CharDataContainer();
				goto IL_0104;
			case TypeCode.SByte:
				dataContainer = new SByteDataContainer();
				goto IL_0104;
			case TypeCode.Byte:
				dataContainer = new ByteDataContainer();
				goto IL_0104;
			case TypeCode.Int16:
				dataContainer = new Int16DataContainer();
				goto IL_0104;
			case TypeCode.UInt16:
				dataContainer = new UInt16DataContainer();
				goto IL_0104;
			case TypeCode.Int32:
				dataContainer = new Int32DataContainer();
				goto IL_0104;
			case TypeCode.UInt32:
				dataContainer = new UInt32DataContainer();
				goto IL_0104;
			case TypeCode.Int64:
				dataContainer = new Int64DataContainer();
				goto IL_0104;
			case TypeCode.UInt64:
				dataContainer = new UInt64DataContainer();
				goto IL_0104;
			case TypeCode.Single:
				dataContainer = new SingleDataContainer();
				goto IL_0104;
			case TypeCode.Double:
				dataContainer = new DoubleDataContainer();
				goto IL_0104;
			case TypeCode.Decimal:
				dataContainer = new DecimalDataContainer();
				goto IL_0104;
			case TypeCode.DateTime:
				dataContainer = new DateTimeDataContainer();
				goto IL_0104;
			case TypeCode.String:
				dataContainer = new StringDataContainer();
				goto IL_0104;
			}
			dataContainer = new ObjectDataContainer();
			IL_0104:
			dataContainer._type = type;
			dataContainer._column = column;
			return dataContainer;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00024368 File Offset: 0x00022568
		internal static object GetExplicitValue(object value)
		{
			Type type = value.GetType();
			MethodInfo method = type.GetMethod("op_Explicit", new Type[] { type });
			if (method != null)
			{
				return method.Invoke(value, new object[] { value });
			}
			return null;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000243AC File Offset: 0x000225AC
		internal object GetContainerData(object value)
		{
			if (this._type.IsInstanceOfType(value))
			{
				return value;
			}
			if (value is IConvertible)
			{
				switch (Type.GetTypeCode(this._type))
				{
				case TypeCode.Boolean:
					return Convert.ToBoolean(value);
				case TypeCode.Char:
					return Convert.ToChar(value);
				case TypeCode.SByte:
					return Convert.ToSByte(value);
				case TypeCode.Byte:
					return Convert.ToByte(value);
				case TypeCode.Int16:
					return Convert.ToInt16(value);
				case TypeCode.UInt16:
					return Convert.ToUInt16(value);
				case TypeCode.Int32:
					return Convert.ToInt32(value);
				case TypeCode.UInt32:
					return Convert.ToUInt32(value);
				case TypeCode.Int64:
					return Convert.ToInt64(value);
				case TypeCode.UInt64:
					return Convert.ToUInt64(value);
				case TypeCode.Single:
					return Convert.ToSingle(value);
				case TypeCode.Double:
					return Convert.ToDouble(value);
				case TypeCode.Decimal:
					return Convert.ToDecimal(value);
				case TypeCode.DateTime:
					return Convert.ToDateTime(value);
				case TypeCode.String:
					return Convert.ToString(value);
				}
				throw new InvalidCastException();
			}
			object explicitValue;
			if ((explicitValue = DataContainer.GetExplicitValue(value)) != null)
			{
				return explicitValue;
			}
			throw new InvalidCastException();
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000244FC File Offset: 0x000226FC
		internal bool IsNull(int index)
		{
			return this.null_values == null || this.null_values[index];
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00024518 File Offset: 0x00022718
		internal void FillValues(int fromIndex)
		{
			for (int i = 0; i < this.Capacity; i++)
			{
				this.CopyValue(fromIndex, i);
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00024544 File Offset: 0x00022744
		internal void CopyValue(int from_index, int to_index)
		{
			this.CopyValue(this, from_index, to_index);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00024550 File Offset: 0x00022750
		internal void CopyValue(DataContainer from, int from_index, int to_index)
		{
			this.DoCopyValue(from, from_index, to_index);
			this.null_values[to_index] = from.null_values[from_index];
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00024574 File Offset: 0x00022774
		internal void SetItemFromDataRecord(int index, IDataRecord record, int field)
		{
			if (record.IsDBNull(field))
			{
				this[index] = DBNull.Value;
			}
			else if (record is ISafeDataRecord)
			{
				this.SetValueFromSafeDataRecord(index, (ISafeDataRecord)record, field);
			}
			else
			{
				this[index] = record.GetValue(field);
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000245CC File Offset: 0x000227CC
		internal int CompareValues(int index1, int index2)
		{
			bool flag = this.IsNull(index1);
			bool flag2 = this.IsNull(index2);
			if (flag == flag2)
			{
				return (!flag) ? this.DoCompareValues(index1, index2) : 0;
			}
			return (!flag) ? 1 : (-1);
		}

		// Token: 0x040002E8 RID: 744
		private BitArray null_values;

		// Token: 0x040002E9 RID: 745
		private Type _type;

		// Token: 0x040002EA RID: 746
		private DataColumn _column;
	}
}
