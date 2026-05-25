using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000029 RID: 41
	public class JValue : JToken, IEquatable<JValue>, IFormattable, IComparable, IComparable<JValue>
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00007C94 File Offset: 0x00005E94
		internal JValue(object value, JTokenType type)
		{
			this._value = value;
			this._valueType = type;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007CAA File Offset: 0x00005EAA
		public JValue(JValue other)
			: this(other.Value, other.Type)
		{
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007CBE File Offset: 0x00005EBE
		public JValue(long value)
			: this(value, JTokenType.Integer)
		{
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007CCD File Offset: 0x00005ECD
		[CLSCompliant(false)]
		public JValue(ulong value)
			: this(value, JTokenType.Integer)
		{
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007CDC File Offset: 0x00005EDC
		public JValue(double value)
			: this(value, JTokenType.Float)
		{
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007CEB File Offset: 0x00005EEB
		public JValue(DateTime value)
			: this(value, JTokenType.Date)
		{
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007CFB File Offset: 0x00005EFB
		public JValue(bool value)
			: this(value, JTokenType.Boolean)
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007D0B File Offset: 0x00005F0B
		public JValue(string value)
			: this(value, JTokenType.String)
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007D15 File Offset: 0x00005F15
		public JValue(Guid value)
			: this(value, JTokenType.String)
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007D24 File Offset: 0x00005F24
		public JValue(Uri value)
			: this(value, JTokenType.String)
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007D2E File Offset: 0x00005F2E
		public JValue(TimeSpan value)
			: this(value, JTokenType.String)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00007D40 File Offset: 0x00005F40
		public JValue(object value)
			: this(value, JValue.GetValueType(null, value))
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007D64 File Offset: 0x00005F64
		internal override bool DeepEquals(JToken node)
		{
			JValue jvalue = node as JValue;
			return jvalue != null && JValue.ValuesEquals(this, jvalue);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00007D84 File Offset: 0x00005F84
		public override bool HasValues
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007D88 File Offset: 0x00005F88
		private static int Compare(JTokenType valueType, object objA, object objB)
		{
			if (objA == null && objB == null)
			{
				return 0;
			}
			if (objA != null && objB == null)
			{
				return 1;
			}
			if (objA == null && objB != null)
			{
				return -1;
			}
			switch (valueType)
			{
			case JTokenType.Comment:
			case JTokenType.String:
			case JTokenType.Raw:
			{
				string text = Convert.ToString(objA, CultureInfo.InvariantCulture);
				string text2 = Convert.ToString(objB, CultureInfo.InvariantCulture);
				return text.CompareTo(text2);
			}
			case JTokenType.Integer:
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				if (objA is float || objB is float || objA is double || objB is double)
				{
					return JValue.CompareFloat(objA, objB);
				}
				return Convert.ToInt64(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToInt64(objB, CultureInfo.InvariantCulture));
			case JTokenType.Float:
				return JValue.CompareFloat(objA, objB);
			case JTokenType.Boolean:
			{
				bool flag = Convert.ToBoolean(objA, CultureInfo.InvariantCulture);
				bool flag2 = Convert.ToBoolean(objB, CultureInfo.InvariantCulture);
				return flag.CompareTo(flag2);
			}
			case JTokenType.Date:
			{
				if (objA is DateTime)
				{
					DateTime dateTime = Convert.ToDateTime(objA, CultureInfo.InvariantCulture);
					DateTime dateTime2 = Convert.ToDateTime(objB, CultureInfo.InvariantCulture);
					return dateTime.CompareTo(dateTime2);
				}
				if (!(objB is DateTimeOffset))
				{
					throw new ArgumentException("Object must be of type DateTimeOffset.");
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)objA;
				DateTimeOffset dateTimeOffset2 = (DateTimeOffset)objB;
				return dateTimeOffset.CompareTo(dateTimeOffset2);
			}
			case JTokenType.Bytes:
			{
				if (!(objB is byte[]))
				{
					throw new ArgumentException("Object must be of type byte[].");
				}
				byte[] array = objA as byte[];
				byte[] array2 = objB as byte[];
				if (array == null)
				{
					return -1;
				}
				if (array2 == null)
				{
					return 1;
				}
				return MiscellaneousUtils.ByteArrayCompare(array, array2);
			}
			case JTokenType.Guid:
			{
				if (!(objB is Guid))
				{
					throw new ArgumentException("Object must be of type Guid.");
				}
				Guid guid = (Guid)objA;
				Guid guid2 = (Guid)objB;
				return guid.CompareTo(guid2);
			}
			case JTokenType.Uri:
			{
				if (!(objB is Uri))
				{
					throw new ArgumentException("Object must be of type Uri.");
				}
				Uri uri = (Uri)objA;
				Uri uri2 = (Uri)objB;
				return Comparer<string>.Default.Compare(uri.ToString(), uri2.ToString());
			}
			case JTokenType.TimeSpan:
			{
				if (!(objB is TimeSpan))
				{
					throw new ArgumentException("Object must be of type TimeSpan.");
				}
				TimeSpan timeSpan = (TimeSpan)objA;
				TimeSpan timeSpan2 = (TimeSpan)objB;
				return timeSpan.CompareTo(timeSpan2);
			}
			}
			throw MiscellaneousUtils.CreateArgumentOutOfRangeException("valueType", valueType, "Unexpected value type: {0}".FormatWith(CultureInfo.InvariantCulture, new object[] { valueType }));
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008020 File Offset: 0x00006220
		private static int CompareFloat(object objA, object objB)
		{
			double num = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
			double num2 = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
			if (MathUtils.ApproxEquals(num, num2))
			{
				return 0;
			}
			return num.CompareTo(num2);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008058 File Offset: 0x00006258
		internal override JToken CloneToken()
		{
			return new JValue(this);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008060 File Offset: 0x00006260
		public static JValue CreateComment(string value)
		{
			return new JValue(value, JTokenType.Comment);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00008069 File Offset: 0x00006269
		public static JValue CreateString(string value)
		{
			return new JValue(value, JTokenType.String);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00008074 File Offset: 0x00006274
		private static JTokenType GetValueType(JTokenType? current, object value)
		{
			if (value == null)
			{
				return JTokenType.Null;
			}
			if (value == DBNull.Value)
			{
				return JTokenType.Null;
			}
			if (value is string)
			{
				return JValue.GetStringValueType(current);
			}
			if (value is long || value is int || value is short || value is sbyte || value is ulong || value is uint || value is ushort || value is byte)
			{
				return JTokenType.Integer;
			}
			if (value is Enum)
			{
				return JTokenType.Integer;
			}
			if (value is double || value is float || value is decimal)
			{
				return JTokenType.Float;
			}
			if (value is DateTime)
			{
				return JTokenType.Date;
			}
			if (value is DateTimeOffset)
			{
				return JTokenType.Date;
			}
			if (value is byte[])
			{
				return JTokenType.Bytes;
			}
			if (value is bool)
			{
				return JTokenType.Boolean;
			}
			if (value is Guid)
			{
				return JTokenType.Guid;
			}
			if (value is Uri)
			{
				return JTokenType.Uri;
			}
			if (value is TimeSpan)
			{
				return JTokenType.TimeSpan;
			}
			throw new ArgumentException("Could not determine JSON object type for type {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { value.GetType() }));
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000817C File Offset: 0x0000637C
		private static JTokenType GetStringValueType(JTokenType? current)
		{
			if (current == null)
			{
				return JTokenType.String;
			}
			JTokenType value = current.Value;
			if (value == JTokenType.Comment || value == JTokenType.String || value == JTokenType.Raw)
			{
				return current.Value;
			}
			return JTokenType.String;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x000081B2 File Offset: 0x000063B2
		public override JTokenType Type
		{
			get
			{
				return this._valueType;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000081BA File Offset: 0x000063BA
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x000081C4 File Offset: 0x000063C4
		public new object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				Type type = ((this._value != null) ? this._value.GetType() : null);
				Type type2 = ((value != null) ? value.GetType() : null);
				if (type != type2)
				{
					this._valueType = JValue.GetValueType(new JTokenType?(this._valueType), value);
				}
				this._value = value;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008218 File Offset: 0x00006418
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			JTokenType valueType = this._valueType;
			if (valueType == JTokenType.Comment)
			{
				writer.WriteComment(this._value.ToString());
				return;
			}
			switch (valueType)
			{
			case JTokenType.Null:
				writer.WriteNull();
				return;
			case JTokenType.Undefined:
				writer.WriteUndefined();
				return;
			case JTokenType.Raw:
				writer.WriteRawValue((this._value != null) ? this._value.ToString() : null);
				return;
			}
			JsonConverter matchingConverter;
			if (this._value != null && (matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType())) != null)
			{
				matchingConverter.WriteJson(writer, this._value, new JsonSerializer());
				return;
			}
			switch (this._valueType)
			{
			case JTokenType.Integer:
				writer.WriteValue(Convert.ToInt64(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Float:
				writer.WriteValue(Convert.ToDouble(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.String:
				writer.WriteValue((this._value != null) ? this._value.ToString() : null);
				return;
			case JTokenType.Boolean:
				writer.WriteValue(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Date:
				if (this._value is DateTimeOffset)
				{
					writer.WriteValue((DateTimeOffset)this._value);
					return;
				}
				writer.WriteValue(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Bytes:
				writer.WriteValue((byte[])this._value);
				return;
			case JTokenType.Guid:
			case JTokenType.Uri:
			case JTokenType.TimeSpan:
				writer.WriteValue((this._value != null) ? this._value.ToString() : null);
				return;
			}
			throw MiscellaneousUtils.CreateArgumentOutOfRangeException("TokenType", this._valueType, "Unexpected token type.");
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000083E0 File Offset: 0x000065E0
		internal override int GetDeepHashCode()
		{
			int num = ((this._value != null) ? this._value.GetHashCode() : 0);
			return this._valueType.GetHashCode() ^ num;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008416 File Offset: 0x00006616
		private static bool ValuesEquals(JValue v1, JValue v2)
		{
			return v1 == v2 || (v1._valueType == v2._valueType && JValue.Compare(v1._valueType, v1._value, v2._value) == 0);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008448 File Offset: 0x00006648
		public bool Equals(JValue other)
		{
			return other != null && JValue.ValuesEquals(this, other);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008458 File Offset: 0x00006658
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			JValue jvalue = obj as JValue;
			if (jvalue != null)
			{
				return this.Equals(jvalue);
			}
			return base.Equals(obj);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008483 File Offset: 0x00006683
		public override int GetHashCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			return this._value.GetHashCode();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000849A File Offset: 0x0000669A
		public override string ToString()
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			return this._value.ToString();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000084B5 File Offset: 0x000066B5
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000084C3 File Offset: 0x000066C3
		public string ToString(IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000084D0 File Offset: 0x000066D0
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			IFormattable formattable = this._value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(format, formatProvider);
			}
			return this._value.ToString();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008510 File Offset: 0x00006710
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			object obj2 = ((obj is JValue) ? ((JValue)obj).Value : obj);
			return JValue.Compare(this._valueType, this._value, obj2);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000854B File Offset: 0x0000674B
		public int CompareTo(JValue obj)
		{
			if (obj == null)
			{
				return 1;
			}
			return JValue.Compare(this._valueType, this._value, obj._value);
		}

		// Token: 0x0400008B RID: 139
		private JTokenType _valueType;

		// Token: 0x0400008C RID: 140
		private object _value;
	}
}
