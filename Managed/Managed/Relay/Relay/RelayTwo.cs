using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GameTypes;
using Newtonsoft.Json;

namespace RelayLib
{
	// Token: 0x02000007 RID: 7
	public class RelayTwo
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000267C File Offset: 0x0000087C
		public RelayTwo()
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002690 File Offset: 0x00000890
		public RelayTwo(string pFilename)
		{
			this.LoadAll(pFilename);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026AC File Offset: 0x000008AC
		public TableTwo CreateTable(string pTableName)
		{
			if (this.tables.ContainsKey(pTableName))
			{
				throw new RelayException("Database already contains a table with name " + pTableName);
			}
			TableTwo tableTwo = new TableTwo(pTableName);
			this.tables.Add(pTableName, tableTwo);
			return tableTwo;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000026F0 File Offset: 0x000008F0
		public TableTwo GetTable(string pTableName)
		{
			if (!this.tables.ContainsKey(pTableName))
			{
				throw new RelayException("Can't find table with name " + pTableName);
			}
			return this.tables[pTableName];
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000272C File Offset: 0x0000092C
		private IEnumerable<float> Save(string pFilename)
		{
			FileStream f = new FileStream(pFilename, FileMode.Create);
			StreamWriter tw = new StreamWriter(f);
			int tableCount = this.tables.Count;
			int tableIndex = 0;
			foreach (TableTwo t in this.tables.Values)
			{
				tw.WriteLine(t.name);
				tw.WriteLine(t.Count());
				tw.WriteLine(JsonConvert.SerializeObject(t.fieldNames, Formatting.None));
				tw.WriteLine(JsonConvert.SerializeObject(t.fieldDataTypeNames, Formatting.None));
				foreach (TableRow r in t)
				{
					tw.WriteLine(JsonConvert.SerializeObject(r.GetSerializableTableRow(), Formatting.None));
				}
				yield return (float)tableIndex / (float)tableCount;
			}
			tw.Flush();
			tw.Close();
			tw.Dispose();
			f.Dispose();
			yield break;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002760 File Offset: 0x00000960
		public void SaveAll(string pFilename)
		{
			foreach (float num in this.Save(pFilename))
			{
				float num2 = num;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027C0 File Offset: 0x000009C0
		public void SaveTableSubsetSeparately(string pTableName, string pSaveFilePath)
		{
			RelayTwo relayTwo = this.Subset(pTableName, (TableRow o) => true);
			relayTwo.SaveAll(pSaveFilePath);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027FC File Offset: 0x000009FC
		public void LoadAll(string pFilename)
		{
			foreach (float num in this.Load(pFilename))
			{
				float num2 = num;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000285C File Offset: 0x00000A5C
		public IEnumerable<float> Load(string pFilename)
		{
			FileStream f = new FileStream(pFilename, FileMode.Open);
			StreamReader sw = new StreamReader(f);
			while (!sw.EndOfStream)
			{
				string tableName = sw.ReadLine();
				int tableCount = Convert.ToInt32(sw.ReadLine());
				string[] fieldNames = JsonConvert.DeserializeObject<string[]>(sw.ReadLine());
				string[] typeNames = JsonConvert.DeserializeObject<string[]>(sw.ReadLine());
				TableTwo newTable = new TableTwo(tableName);
				this.AddFieldsToTable(newTable, fieldNames, typeNames);
				for (int i = 0; i < tableCount; i++)
				{
					SerializableTableRow r = JsonConvert.DeserializeObject<SerializableTableRow>(sw.ReadLine());
					if (r.row >= newTable.capacity)
					{
						newTable.SetCapacity(r.row + 1);
					}
					r.InsertToTable(newTable);
				}
				this.tables.Add(newTable.name, newTable);
				yield return (float)sw.BaseStream.Position / (float)sw.BaseStream.Length;
			}
			f.Flush();
			f.Close();
			f.Dispose();
			yield break;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002890 File Offset: 0x00000A90
		private void AddFieldsToTable(TableTwo pTable, string[] pFieldNames, string[] pDataTypeNames)
		{
			D.assert(pFieldNames.Length == pDataTypeNames.Length, "field definitions does not match");
			for (int i = 0; i < pFieldNames.Length; i++)
			{
				Type typeFromHandle = typeof(TableField<>);
				string text = pDataTypeNames[i];
				Type[] array = null;
				foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					array = new Type[] { assembly.GetType(text) };
					if (array[0] != null)
					{
						break;
					}
				}
				D.isNull(array[0], text + " was not found ");
				Type type = typeFromHandle.MakeGenericType(array);
				ITableField tableField = (ITableField)Activator.CreateInstance(type, new object[] { pFieldNames[i] });
				pTable.AddField(tableField);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002960 File Offset: 0x00000B60
		public void AppendTables(RelayTwo pRelay)
		{
			foreach (string text in pRelay.tables.Keys)
			{
				TableTwo tableTwo = pRelay.tables[text];
				if (this.tables.ContainsKey(text))
				{
					TableTwo tableTwo2 = this.tables[text];
					foreach (ITableField tableField in tableTwo.fields)
					{
						if (!tableTwo2.fieldNames.Contains(tableField.name))
						{
							tableTwo2.AddField(tableField.GetEmptyCopy());
						}
					}
					foreach (TableRow tableRow in tableTwo)
					{
						TableRow tableRow2 = tableTwo2.CreateRow();
						foreach (ITableField tableField2 in tableTwo.fields)
						{
							ITableField field = tableTwo2.GetField(tableField2.name);
							field.SetValueFromString(tableRow2.row, tableField2.GetValueAsString(tableRow.row));
						}
					}
				}
				else
				{
					this.tables[tableTwo.name] = tableTwo;
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002B5C File Offset: 0x00000D5C
		public void AppendTables(string pFilename)
		{
			RelayTwo relayTwo = new RelayTwo(pFilename);
			this.AppendTables(relayTwo);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002B78 File Offset: 0x00000D78
		public RelayTwo Subset(string pTableName, Func<TableRow, bool> pPredicate)
		{
			RelayTwo relayTwo = new RelayTwo();
			TableTwo tableTwo = relayTwo.CreateTable(pTableName);
			TableTwo tableTwo2 = this.tables[pTableName];
			foreach (ITableField tableField in tableTwo2.fields)
			{
				tableTwo.AddField(tableField.GetEmptyCopy());
			}
			tableTwo.SetCapacity(tableTwo2.capacity);
			foreach (TableRow tableRow in tableTwo2.Where(pPredicate))
			{
				SerializableTableRow serializableTableRow = tableRow.GetSerializableTableRow();
				serializableTableRow.InsertToTable(tableTwo);
			}
			return relayTwo;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C74 File Offset: 0x00000E74
		public void MergeWith(RelayTwo pSource)
		{
			foreach (TableTwo tableTwo in this.tables.Values)
			{
				if (pSource.tables.ContainsKey(tableTwo.name))
				{
					TableTwo tableTwo2 = pSource.tables[tableTwo.name];
					foreach (ITableField tableField in tableTwo2.fields)
					{
						if (!tableTwo.fieldNames.Contains(tableField.name))
						{
							tableTwo.AddField(tableField.GetEmptyCopy());
						}
					}
					foreach (ITableField tableField2 in tableTwo.fields)
					{
						if (!tableTwo2.fieldNames.Contains(tableField2.name))
						{
							tableTwo2.AddField(tableField2.GetEmptyCopy());
						}
					}
					if (tableTwo2.capacity > tableTwo.capacity)
					{
						tableTwo.SetCapacity(tableTwo2.capacity);
					}
					foreach (TableRow tableRow in tableTwo2)
					{
						SerializableTableRow serializableTableRow = tableRow.GetSerializableTableRow();
						if (tableTwo.ContainsRow(serializableTableRow.row))
						{
							throw new RelayTwo.RelayMergeException(string.Concat(new object[] { "table ", tableTwo.name, " does already contain row ", serializableTableRow.row }));
						}
						serializableTableRow.InsertToTable(tableTwo);
					}
				}
			}
			foreach (TableTwo tableTwo3 in pSource.tables.Values)
			{
				if (!this.tables.ContainsKey(tableTwo3.name))
				{
					TableTwo tableTwo4 = this.CreateTable(tableTwo3.name);
					foreach (ITableField tableField3 in tableTwo3.fields)
					{
						tableTwo4.AddField(tableField3.GetEmptyCopy());
					}
					tableTwo4.SetCapacity(tableTwo3.capacity);
					foreach (TableRow tableRow2 in tableTwo3)
					{
						SerializableTableRow serializableTableRow2 = tableRow2.GetSerializableTableRow();
						serializableTableRow2.InsertToTable(tableTwo4);
					}
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003000 File Offset: 0x00001200
		public override bool Equals(object obj)
		{
			RelayTwo relayTwo = obj as RelayTwo;
			if (relayTwo == null || relayTwo.tables.Count != this.tables.Count)
			{
				return false;
			}
			foreach (TableTwo tableTwo in this.tables.Values)
			{
				if (!tableTwo.Equals(relayTwo.tables[tableTwo.name]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000030B8 File Offset: 0x000012B8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000030C0 File Offset: 0x000012C0
		public static string ValueToString(object pValue)
		{
			return JsonConvert.SerializeObject(pValue);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000030C8 File Offset: 0x000012C8
		public static T StringToValue<T>(string pString)
		{
			return JsonConvert.DeserializeObject<T>(pString);
		}

		// Token: 0x04000008 RID: 8
		public SerializableDictionary<string, TableTwo> tables = new SerializableDictionary<string, TableTwo>();

		// Token: 0x02000008 RID: 8
		public class RelayMergeException : Exception
		{
			// Token: 0x0600002A RID: 42 RVA: 0x000030D4 File Offset: 0x000012D4
			public RelayMergeException(string pMessage)
				: base(pMessage)
			{
			}
		}
	}
}
