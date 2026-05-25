using System;
using System.Data;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000019 RID: 25
	public class DataTableConverter : JsonConverter
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00005504 File Offset: 0x00003704
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			DataTable dataTable = (DataTable)value;
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			writer.WriteStartArray();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				writer.WriteStartObject();
				foreach (object obj2 in dataRow.Table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj2;
					if (serializer.NullValueHandling != NullValueHandling.Ignore || (dataRow[dataColumn] != null && dataRow[dataColumn] != DBNull.Value))
					{
						writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.ResolvePropertyName(dataColumn.ColumnName) : dataColumn.ColumnName);
						serializer.Serialize(writer, dataRow[dataColumn]);
					}
				}
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005628 File Offset: 0x00003828
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			DataTable dataTable;
			if (reader.TokenType == JsonToken.PropertyName)
			{
				dataTable = new DataTable((string)reader.Value);
				reader.Read();
			}
			else
			{
				dataTable = new DataTable();
			}
			reader.Read();
			while (reader.TokenType == JsonToken.StartObject)
			{
				DataRow dataRow = dataTable.NewRow();
				reader.Read();
				while (reader.TokenType == JsonToken.PropertyName)
				{
					string text = (string)reader.Value;
					reader.Read();
					if (!dataTable.Columns.Contains(text))
					{
						Type columnDataType = DataTableConverter.GetColumnDataType(reader.TokenType);
						dataTable.Columns.Add(new DataColumn(text, columnDataType));
					}
					dataRow[text] = reader.Value ?? DBNull.Value;
					reader.Read();
				}
				dataRow.EndEdit();
				dataTable.Rows.Add(dataRow);
				reader.Read();
			}
			return dataTable;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005708 File Offset: 0x00003908
		private static Type GetColumnDataType(JsonToken tokenType)
		{
			switch (tokenType)
			{
			case JsonToken.Integer:
				return typeof(long);
			case JsonToken.Float:
				return typeof(double);
			case JsonToken.String:
			case JsonToken.Null:
			case JsonToken.Undefined:
				return typeof(string);
			case JsonToken.Boolean:
				return typeof(bool);
			case JsonToken.Date:
				return typeof(DateTime);
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005785 File Offset: 0x00003985
		public override bool CanConvert(Type valueType)
		{
			return valueType == typeof(DataTable);
		}
	}
}
