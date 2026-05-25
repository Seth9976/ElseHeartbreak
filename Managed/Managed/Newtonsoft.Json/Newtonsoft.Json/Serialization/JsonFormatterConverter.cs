using System;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200002C RID: 44
	internal class JsonFormatterConverter : IFormatterConverter
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x000085F0 File Offset: 0x000067F0
		public JsonFormatterConverter(JsonSerializer serializer)
		{
			ValidationUtils.ArgumentNotNull(serializer, "serializer");
			this._serializer = serializer;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000860C File Offset: 0x0000680C
		private T GetTokenValue<T>(object value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JValue jvalue = (JValue)value;
			return (T)((object)global::System.Convert.ChangeType(jvalue.Value, typeof(T), CultureInfo.InvariantCulture));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000864C File Offset: 0x0000684C
		public object Convert(object value, Type type)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException("Value is not a JToken.", "value");
			}
			return this._serializer.Deserialize(jtoken.CreateReader(), type);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008690 File Offset: 0x00006890
		public object Convert(object value, TypeCode typeCode)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value is JValue)
			{
				value = ((JValue)value).Value;
			}
			return global::System.Convert.ChangeType(value, typeCode, CultureInfo.InvariantCulture);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000086BE File Offset: 0x000068BE
		public bool ToBoolean(object value)
		{
			return this.GetTokenValue<bool>(value);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000086C7 File Offset: 0x000068C7
		public byte ToByte(object value)
		{
			return this.GetTokenValue<byte>(value);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000086D0 File Offset: 0x000068D0
		public char ToChar(object value)
		{
			return this.GetTokenValue<char>(value);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000086D9 File Offset: 0x000068D9
		public DateTime ToDateTime(object value)
		{
			return this.GetTokenValue<DateTime>(value);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000086E2 File Offset: 0x000068E2
		public decimal ToDecimal(object value)
		{
			return this.GetTokenValue<decimal>(value);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000086EB File Offset: 0x000068EB
		public double ToDouble(object value)
		{
			return this.GetTokenValue<double>(value);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000086F4 File Offset: 0x000068F4
		public short ToInt16(object value)
		{
			return this.GetTokenValue<short>(value);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000086FD File Offset: 0x000068FD
		public int ToInt32(object value)
		{
			return this.GetTokenValue<int>(value);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008706 File Offset: 0x00006906
		public long ToInt64(object value)
		{
			return this.GetTokenValue<long>(value);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000870F File Offset: 0x0000690F
		public sbyte ToSByte(object value)
		{
			return this.GetTokenValue<sbyte>(value);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008718 File Offset: 0x00006918
		public float ToSingle(object value)
		{
			return this.GetTokenValue<float>(value);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008721 File Offset: 0x00006921
		public string ToString(object value)
		{
			return this.GetTokenValue<string>(value);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000872A File Offset: 0x0000692A
		public ushort ToUInt16(object value)
		{
			return this.GetTokenValue<ushort>(value);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008733 File Offset: 0x00006933
		public uint ToUInt32(object value)
		{
			return this.GetTokenValue<uint>(value);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000873C File Offset: 0x0000693C
		public ulong ToUInt64(object value)
		{
			return this.GetTokenValue<ulong>(value);
		}

		// Token: 0x04000091 RID: 145
		private readonly JsonSerializer _serializer;
	}
}
