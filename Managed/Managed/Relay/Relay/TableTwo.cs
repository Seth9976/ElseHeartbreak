using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameTypes;

namespace RelayLib
{
	// Token: 0x0200000E RID: 14
	public class TableTwo : IEnumerable<TableRow>, IEnumerable
	{
		// Token: 0x0600004D RID: 77 RVA: 0x0000362C File Offset: 0x0000182C
		public TableTwo(string pName)
		{
			this.name = pName;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003654 File Offset: 0x00001854
		IEnumerator IEnumerable.GetEnumerator()
		{
			int[] rows = this.GetIndexesOfPopulatedRows();
			foreach (int i in rows)
			{
				yield return new TableRow(this, i);
			}
			yield break;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003670 File Offset: 0x00001870
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00003678 File Offset: 0x00001878
		public string name { get; private set; }

		// Token: 0x06000051 RID: 81 RVA: 0x00003684 File Offset: 0x00001884
		public void EnsureField<T>(string pFieldName)
		{
			if (!this._fields.ContainsKey(pFieldName))
			{
				this.AddField<T>(pFieldName);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000036A0 File Offset: 0x000018A0
		public ValueEntry<T> GetValueEntryEnsureDefault<T>(int pRow, string pFieldName, T pDefaultValue)
		{
			this.EnsureField<T>(pFieldName);
			if (!this.ContainsRow(pRow))
			{
				throw new RelayException(string.Concat(new object[] { "The row ", pRow, " does not exist in table ", this.name }));
			}
			TableField<T> tableField = this._fields[pFieldName] as TableField<T>;
			if (tableField == null)
			{
				throw new RelayException(string.Concat(new string[]
				{
					"Can't access field '",
					pFieldName,
					"' using the type '",
					typeof(T).Name,
					"', use '",
					this._fields[pFieldName].type.Name,
					"' instead"
				}));
			}
			if (tableField.entries[pRow] == null)
			{
				tableField.entries[pRow] = new ValueEntry<T>();
				tableField.entries[pRow].data = pDefaultValue;
			}
			return (this._fields[pFieldName] as TableField<T>).entries[pRow];
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000037BC File Offset: 0x000019BC
		public void AddField<T>(string pFieldName)
		{
			D.assert(!this._fields.ContainsKey(pFieldName), "field does already exist!");
			TableField<T> tableField = new TableField<T>(pFieldName);
			this.AddField(tableField);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000037F0 File Offset: 0x000019F0
		internal void AddField(ITableField pField)
		{
			pField.rowCount = this.capacity;
			this._fields.Add(pField.name, pField);
			this.EnsureFieldEquality();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003824 File Offset: 0x00001A24
		public void SetValue<T>(int pRow, string pFieldName, T pValue)
		{
			if (!this._fields.ContainsKey(pFieldName))
			{
				throw new RelayException("The field '" + pFieldName + "' does not exist");
			}
			TableField<T> tableField = this._fields[pFieldName] as TableField<T>;
			if (tableField == null)
			{
				throw new RelayException(string.Concat(new string[]
				{
					"Can't access field '",
					pFieldName,
					"' using the type '",
					typeof(T).Name,
					"', use '",
					this._fields[pFieldName].type.Name,
					"' instead"
				}));
			}
			if (tableField.entries[pRow] == null)
			{
				tableField.entries[pRow] = new ValueEntry<T>();
			}
			tableField.entries[pRow].data = pValue;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003904 File Offset: 0x00001B04
		public T GetValue<T>(int pRow, string pFieldName)
		{
			return this.GetValueEntry<T>(pRow, pFieldName).data;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003914 File Offset: 0x00001B14
		public ValueEntry<T> GetValueEntry<T>(int pRow, string pFieldName)
		{
			if (!this.ContainsRow(pRow))
			{
				throw new RelayException(string.Concat(new object[] { "The row ", pRow, " does not exist in table ", this.name }));
			}
			if (!this._fields.ContainsKey(pFieldName))
			{
				throw new RelayException("The field " + pFieldName + " does not exist in table " + this.name);
			}
			TableField<T> tableField = this._fields[pFieldName] as TableField<T>;
			if (tableField == null)
			{
				throw new RelayException(string.Concat(new string[]
				{
					"Can't access field '",
					pFieldName,
					"' using the type '",
					typeof(T).Name,
					"', use '",
					this._fields[pFieldName].type.Name,
					"' instead"
				}));
			}
			if (tableField.entries[pRow] == null)
			{
				throw new RelayException(string.Concat(new object[] { "Can't get value since cell is null: row ", pRow, " field ", pFieldName }));
			}
			return (this._fields[pFieldName] as TableField<T>).entries[pRow];
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003A60 File Offset: 0x00001C60
		public string GetStringValue(int pRow, string pFieldName)
		{
			if (!this._fields.ContainsKey(pFieldName))
			{
				throw new RelayException("The field '" + pFieldName + "' does not exist in table " + this.name);
			}
			return this._fields[pFieldName].GetValueAsString(pRow);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003AAC File Offset: 0x00001CAC
		public TableRow CreateRow()
		{
			int num = this.GetOneFreeIndex();
			if (num == -1)
			{
				num = this._capacity;
				this.SetCapacity(this._capacity + 1);
			}
			this._usedRows[num] = true;
			TableRow tableRow = new TableRow(this, num);
			this.EnsureFieldEquality();
			return tableRow;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003AFC File Offset: 0x00001CFC
		private int GetOneFreeIndex()
		{
			for (int i = 0; i < this.capacity; i++)
			{
				if (!this._usedRows[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003B34 File Offset: 0x00001D34
		public void RemoveRowAt(int pRow)
		{
			if (!this.ContainsRow(pRow))
			{
				throw new RelayException(string.Concat(new object[] { "Can't remove row ", pRow, " because it doesn't exist in table ", this.name }));
			}
			if (pRow == this._capacity - 1)
			{
				this.RemoveLastRow();
			}
			else
			{
				this._usedRows[pRow] = false;
				foreach (ITableField tableField in this._fields.Values)
				{
					tableField.ClearEntryAtRow(pRow);
				}
			}
			this.EnsureFieldEquality();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003C10 File Offset: 0x00001E10
		public void RemoveLastRow()
		{
			this.SetCapacity(this._capacity - 1);
			while (this._capacity > 0 && !this._usedRows[this.capacity - 1])
			{
				this.RemoveLastRow();
			}
			this.EnsureFieldEquality();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003C60 File Offset: 0x00001E60
		private void EnsureFieldEquality()
		{
			if (this._fields.Values.Count == 0)
			{
				return;
			}
			int[] array = this._fields.Values.Select((ITableField f) => f.rowCount).ToArray<int>();
			foreach (int num in array)
			{
				D.assert(num == array[0], "All fields must have the same row count");
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003CE0 File Offset: 0x00001EE0
		public int[] GetIndexesOfPopulatedRows()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < this._capacity; i++)
			{
				if (this._usedRows[i])
				{
					list.Add(i);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003D28 File Offset: 0x00001F28
		public int UsedRowCount()
		{
			int num = 0;
			for (int i = 0; i < this._capacity; i++)
			{
				if (this._usedRows[i])
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003D64 File Offset: 0x00001F64
		public TableRow[] GetRows()
		{
			int[] indexesOfPopulatedRows = this.GetIndexesOfPopulatedRows();
			TableRow[] array = new TableRow[indexesOfPopulatedRows.Length];
			int num = 0;
			foreach (int num2 in indexesOfPopulatedRows)
			{
				array[num++] = this.GetRow(num2);
			}
			return array;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003DBC File Offset: 0x00001FBC
		public int capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003DC4 File Offset: 0x00001FC4
		public string[] fieldNames
		{
			get
			{
				if (this.fields.Count<ITableField>() == 0)
				{
					return new string[0];
				}
				IEnumerable<string> enumerable = from ITableField f in this.fields
					select f.name;
				return enumerable.ToArray<string>();
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003E1C File Offset: 0x0000201C
		public string[] fieldDataTypeNames
		{
			get
			{
				if (this.fields.Count<ITableField>() == 0)
				{
					return new string[0];
				}
				IEnumerable<string> enumerable = from ITableField f in this.fields
					select f.type.FullName;
				return enumerable.ToArray<string>();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003E74 File Offset: 0x00002074
		public IEnumerable<ITableField> fields
		{
			get
			{
				return this._fields.Values;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003E84 File Offset: 0x00002084
		public IEnumerator<TableRow> GetEnumerator()
		{
			int[] rows = this.GetIndexesOfPopulatedRows();
			foreach (int i in rows)
			{
				yield return new TableRow(this, i);
			}
			yield break;
		}

		// Token: 0x17000012 RID: 18
		public TableRow this[int pRow]
		{
			get
			{
				if (!this.ContainsRow(pRow))
				{
					throw new RelayException(string.Concat(new object[] { "Row ", pRow, " does not exist in table ", this.name }));
				}
				return new TableRow(this, pRow);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003EF4 File Offset: 0x000020F4
		public ITableField GetField(string pFieldName)
		{
			return this._fields[pFieldName];
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003F04 File Offset: 0x00002104
		public TableRow GetRow(int pRow)
		{
			return this[pRow];
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003F10 File Offset: 0x00002110
		public int Count()
		{
			return this.GetIndexesOfPopulatedRows().Length;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003F1C File Offset: 0x0000211C
		public void SetCapacity(int pNewCapacity)
		{
			foreach (ITableField tableField in this._fields.Values)
			{
				tableField.rowCount = pNewCapacity;
			}
			this._capacity = pNewCapacity;
			while (this._usedRows.Count < this._capacity)
			{
				this._usedRows.Add(false);
			}
			while (this._usedRows.Count > this._capacity)
			{
				this._usedRows.RemoveAt(this._usedRows.Count - 1);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003FEC File Offset: 0x000021EC
		public override bool Equals(object obj)
		{
			TableTwo tableTwo = obj as TableTwo;
			if (tableTwo == null || tableTwo.name != this.name || tableTwo.capacity != this.capacity || !tableTwo.fieldNames.SequenceEqual(this.fieldNames) || !tableTwo.fieldDataTypeNames.SequenceEqual(this.fieldDataTypeNames))
			{
				return false;
			}
			for (int i = 0; i < this.capacity; i++)
			{
				bool flag = this.ContainsRow(i);
				if (flag != tableTwo.ContainsRow(i))
				{
					return false;
				}
				if (flag && !this[i].valuesAsStrings.SequenceEqual(tableTwo[i].valuesAsStrings))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000040BC File Offset: 0x000022BC
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000040C4 File Offset: 0x000022C4
		internal bool ContainsRow(int pRowIndex)
		{
			return pRowIndex > -1 && pRowIndex < this._capacity && this._usedRows[pRowIndex];
		}

		// Token: 0x04000014 RID: 20
		public const string NULL_TOKEN = "NULL_TOKEN";

		// Token: 0x04000015 RID: 21
		private int _capacity;

		// Token: 0x04000016 RID: 22
		public List<bool> _usedRows = new List<bool>();

		// Token: 0x04000017 RID: 23
		private Dictionary<string, ITableField> _fields = new Dictionary<string, ITableField>();
	}
}
