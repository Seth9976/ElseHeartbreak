using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RelayLib
{
	// Token: 0x0200000B RID: 11
	public class TableField<T> : ITableField
	{
		// Token: 0x06000037 RID: 55 RVA: 0x0000327C File Offset: 0x0000147C
		public TableField(string pName)
		{
			this.type = typeof(T);
			this.name = pName;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000032EC File Offset: 0x000014EC
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000032F4 File Offset: 0x000014F4
		public string name { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003300 File Offset: 0x00001500
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00003308 File Offset: 0x00001508
		public Type type { get; private set; }

		// Token: 0x0600003D RID: 61 RVA: 0x00003314 File Offset: 0x00001514
		public ITableField GetEmptyCopy()
		{
			return new TableField<T>(this.name);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003324 File Offset: 0x00001524
		public void ClearEntryAtRow(int pRow)
		{
			this.entries[pRow] = null;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003334 File Offset: 0x00001534
		public string GetValueAsString(int pRow)
		{
			if (this.entries[pRow] != null)
			{
				StringWriter stringWriter = new StringWriter();
				JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter);
				jsonTextWriter.Formatting = Formatting.None;
				TableField<T>._serializer.Serialize(jsonTextWriter, this.entries[pRow].data);
				jsonTextWriter.Close();
				return stringWriter.ToString();
			}
			return "NULL_TOKEN";
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000339C File Offset: 0x0000159C
		public void SetValueFromString(int pRow, string pValue)
		{
			if (pValue == "NULL_TOKEN")
			{
				this.entries[pRow] = null;
			}
			else
			{
				if (this.entries[pRow] == null)
				{
					this.entries[pRow] = new ValueEntry<T>();
				}
				JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(pValue));
				this.entries[pRow].data = TableField<T>.jsonSerializer.Deserialize<T>(jsonTextReader);
				jsonTextReader.Close();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000341C File Offset: 0x0000161C
		// (set) Token: 0x06000042 RID: 66 RVA: 0x0000342C File Offset: 0x0000162C
		public int rowCount
		{
			get
			{
				return this.entries.Count;
			}
			set
			{
				if (value == 0)
				{
					this.entries.Clear();
				}
				while (value < this.entries.Count)
				{
					this.entries.RemoveAt(this.entries.Count - 1);
				}
				while (value > this.entries.Count)
				{
					this.entries.Add(null);
				}
			}
		}

		// Token: 0x0400000A RID: 10
		public List<ValueEntry<T>> entries = new List<ValueEntry<T>>();

		// Token: 0x0400000B RID: 11
		private static JsonSerializer _serializer = JsonSerializer.Create(new JsonSerializerSettings());

		// Token: 0x0400000C RID: 12
		private static JsonSerializerSettings settings = new JsonSerializerSettings();

		// Token: 0x0400000D RID: 13
		private static JsonSerializer jsonSerializer = JsonSerializer.Create(TableField<T>.settings);
	}
}
