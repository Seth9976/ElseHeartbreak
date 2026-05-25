using System;
using System.Data;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000018 RID: 24
	public class DataSetConverter : JsonConverter
	{
		// Token: 0x060000FE RID: 254 RVA: 0x000053F0 File Offset: 0x000035F0
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			DataSet dataSet = (DataSet)value;
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			DataTableConverter dataTableConverter = new DataTableConverter();
			writer.WriteStartObject();
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.ResolvePropertyName(dataTable.TableName) : dataTable.TableName);
				dataTableConverter.WriteJson(writer, dataTable, serializer);
			}
			writer.WriteEndObject();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005494 File Offset: 0x00003694
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			DataSet dataSet = new DataSet();
			DataTableConverter dataTableConverter = new DataTableConverter();
			reader.Read();
			while (reader.TokenType == JsonToken.PropertyName)
			{
				DataTable dataTable = (DataTable)dataTableConverter.ReadJson(reader, typeof(DataTable), null, serializer);
				dataSet.Tables.Add(dataTable);
				reader.Read();
			}
			return dataSet;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000054ED File Offset: 0x000036ED
		public override bool CanConvert(Type valueType)
		{
			return valueType == typeof(DataSet);
		}
	}
}
