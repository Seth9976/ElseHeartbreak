using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Mono.Data.Tds.Protocol;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a numeric value between - 10^38 +1 and 10^38 - 1, with fixed precision and scale. </summary>
	// Token: 0x02000108 RID: 264
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlDecimal : IXmlSerializable, IComparable, INullable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="value">The <see cref="T:System.Decimal" /> value to be stored as a <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D45 RID: 3397 RVA: 0x000370FC File Offset: 0x000352FC
		public SqlDecimal(decimal value)
		{
			int[] bits = decimal.GetBits(value);
			this.precision = SqlDecimal.MaxPrecision;
			this.scale = (byte)((uint)bits[3] >> 16);
			if (this.scale > SqlDecimal.MaxScale || (bits[3] & 2130771967) != 0)
			{
				throw new ArgumentException(Locale.GetText("Invalid scale"));
			}
			this.value = new int[4];
			this.value[0] = bits[0];
			this.value[1] = bits[1];
			this.value[2] = bits[2];
			this.value[3] = 0;
			this.positive = value >= 0m;
			this.notNull = true;
			this.precision = this.GetPrecision(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied double parameter.</summary>
		/// <param name="dVal">A double, representing the value for the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D46 RID: 3398 RVA: 0x000371B4 File Offset: 0x000353B4
		public SqlDecimal(double dVal)
		{
			this = new SqlDecimal((decimal)dVal);
			SqlDecimal sqlDecimal = this;
			int num = (int)(17 - this.precision);
			if (num > 0)
			{
				sqlDecimal = SqlDecimal.AdjustScale(this, num, false);
			}
			else
			{
				sqlDecimal = SqlDecimal.Round(this, 17);
			}
			this.notNull = sqlDecimal.notNull;
			this.positive = sqlDecimal.positive;
			this.precision = sqlDecimal.precision;
			this.scale = sqlDecimal.scale;
			this.value = sqlDecimal.value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied integer value.</summary>
		/// <param name="value">The supplied integer value which will the used as the value of the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D47 RID: 3399 RVA: 0x00037248 File Offset: 0x00035448
		public SqlDecimal(int value)
		{
			this = new SqlDecimal(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied long integer value.</summary>
		/// <param name="value">The supplied long integer value which will the used as the value of the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D48 RID: 3400 RVA: 0x00037258 File Offset: 0x00035458
		public SqlDecimal(long value)
		{
			this = new SqlDecimal(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied parameters.</summary>
		/// <param name="bPrecision">The maximum number of digits that can be used to represent the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="bScale">The number of decimal places to which the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property will be resolved for the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="fPositive">A Boolean value that indicates whether the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure represents a positive or negative number. </param>
		/// <param name="bits">The 128-bit unsigned integer that provides the value of the new <see cref="T:System.Data.SqlTypes.SqlDecimal" />. </param>
		// Token: 0x06000D49 RID: 3401 RVA: 0x00037268 File Offset: 0x00035468
		public SqlDecimal(byte bPrecision, byte bScale, bool fPositive, int[] bits)
		{
			this = new SqlDecimal(bPrecision, bScale, fPositive, bits[0], bits[1], bits[2], bits[3]);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure using the supplied parameters.</summary>
		/// <param name="bPrecision">The maximum number of digits that can be used to represent the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="bScale">The number of decimal places to which the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property will be resolved for the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="fPositive">A Boolean value that indicates whether the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure represents a positive or negative number. </param>
		/// <param name="data1">An 32-bit unsigned integer which will be combined with data2, data3, and data4 to make up the 128-bit unsigned integer that represents the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures value. </param>
		/// <param name="data2">An 32-bit unsigned integer which will be combined with data1, data3, and data4 to make up the 128-bit unsigned integer that represents the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures value. </param>
		/// <param name="data3">An 32-bit unsigned integer which will be combined with data1, data2, and data4 to make up the 128-bit unsigned integer that represents the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures value. </param>
		/// <param name="data4">An 32-bit unsigned integer which will be combined with data1, data2, and data3 to make up the 128-bit unsigned integer that represents the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures value. </param>
		// Token: 0x06000D4A RID: 3402 RVA: 0x00037290 File Offset: 0x00035490
		public SqlDecimal(byte bPrecision, byte bScale, bool fPositive, int data1, int data2, int data3, int data4)
		{
			this.precision = bPrecision;
			this.scale = bScale;
			this.positive = fPositive;
			this.value = new int[4];
			this.value[0] = data1;
			this.value[1] = data2;
			this.value[2] = data3;
			this.value[3] = data4;
			this.notNull = true;
			if (this.precision < this.scale)
			{
				throw new SqlTypeException(Locale.GetText("Invalid presicion/scale combination."));
			}
			if (this.precision > 38)
			{
				throw new SqlTypeException(Locale.GetText("Invalid precision/scale combination."));
			}
			if (this.ToDouble() > Math.Pow(10.0, 38.0) - 1.0 || this.ToDouble() < -Math.Pow(10.0, 38.0))
			{
				throw new OverflowException("Can't convert to SqlDecimal, Out of range ");
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06000D4C RID: 3404 RVA: 0x000373FC File Offset: 0x000355FC
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06000D4D RID: 3405 RVA: 0x00037400 File Offset: 0x00035600
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				return;
			}
			switch (reader.ReadState)
			{
			case ReadState.Error:
			case ReadState.EndOfFile:
			case ReadState.Closed:
				return;
			default:
				reader.MoveToContent();
				if (reader.EOF)
				{
					return;
				}
				reader.Read();
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					return;
				}
				if (reader.Value.Length > 0)
				{
					if (string.Compare("Null", reader.Value) == 0)
					{
						this.notNull = false;
						return;
					}
					SqlDecimal sqlDecimal = new SqlDecimal(decimal.Parse(reader.Value));
					this.value = sqlDecimal.Data;
					this.notNull = true;
					this.scale = sqlDecimal.Scale;
					this.precision = sqlDecimal.Precision;
					this.positive = sqlDecimal.IsPositive;
				}
				return;
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x06000D4E RID: 3406 RVA: 0x000374D8 File Offset: 0x000356D8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteString(this.Value.ToString());
		}

		/// <summary>Get the binary representation of the value of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure as an array of bytes.</summary>
		/// <returns>An array of bytes that contains the binary representation of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's value.</returns>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x000374FC File Offset: 0x000356FC
		public byte[] BinData
		{
			get
			{
				byte[] array = new byte[this.value.Length * 4];
				int num = 0;
				for (int i = 0; i < this.value.Length; i++)
				{
					array[num++] = (byte)(255 & this.value[i]);
					array[num++] = (byte)(255 & (this.value[i] >> 8));
					array[num++] = (byte)(255 & (this.value[i] >> 16));
					array[num++] = (byte)(255 & (this.value[i] >> 24));
				}
				return array;
			}
		}

		/// <summary>Gets the binary representation of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure as an array of integers.</summary>
		/// <returns>An array of integers that contains the binary representation of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</returns>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x00037598 File Offset: 0x00035798
		public int[] Data
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				return new int[]
				{
					this.value[0],
					this.value[1],
					this.value[2],
					this.value[3]
				};
			}
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure is null.</summary>
		/// <returns>true if this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure is null. Otherwise, false. </returns>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x000375EC File Offset: 0x000357EC
		public bool IsNull
		{
			get
			{
				return !this.notNull;
			}
		}

		/// <summary>Indicates whether the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure is greater than zero.</summary>
		/// <returns>true if the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> is assigned to null. Otherwise, false.</returns>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x000375F8 File Offset: 0x000357F8
		public bool IsPositive
		{
			get
			{
				return this.positive;
			}
		}

		/// <summary>Gets the maximum number of digits used to represent the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the Value of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</returns>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x00037600 File Offset: 0x00035800
		public byte Precision
		{
			get
			{
				return this.precision;
			}
		}

		/// <summary>Gets the number of decimal places to which <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which the Value property is resolved.</returns>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00037608 File Offset: 0x00035808
		public byte Scale
		{
			get
			{
				return this.scale;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. This property is read-only.</summary>
		/// <returns>A number in the range -79,228,162,514,264,337,593,543,950,335 through 79,228,162,514,162,514,264,337,593,543,950,335.</returns>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00037610 File Offset: 0x00035810
		public decimal Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException();
				}
				if (this.value[3] > 0)
				{
					throw new OverflowException();
				}
				return new decimal(this.value[0], this.value[1], this.value[2], !this.positive, this.scale);
			}
		}

		/// <summary>The Abs method gets the absolute value of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the unsigned number representing the absolute value of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="n">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D56 RID: 3414 RVA: 0x00037670 File Offset: 0x00035870
		public static SqlDecimal Abs(SqlDecimal n)
		{
			if (!n.notNull)
			{
				return n;
			}
			return new SqlDecimal(n.Precision, n.Scale, true, n.Data);
		}

		/// <summary>Calculates the sum of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operators.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the sum.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D57 RID: 3415 RVA: 0x000376A8 File Offset: 0x000358A8
		public static SqlDecimal Add(SqlDecimal x, SqlDecimal y)
		{
			return x + y;
		}

		/// <summary>The scale of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand will be adjusted to the number of digits indicated by the digits parameter. Depending on the value of the fRound parameter, the value will either be rounded to the appropriate number of digits or truncated.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the adjusted number.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be adjusted. </param>
		/// <param name="digits">The number of digits in the adjusted structure. </param>
		/// <param name="fRound">If this parameter is true, the new Value will be rounded, if false, the value will be truncated. </param>
		// Token: 0x06000D58 RID: 3416 RVA: 0x000376B4 File Offset: 0x000358B4
		public static SqlDecimal AdjustScale(SqlDecimal n, int digits, bool fRound)
		{
			byte b = n.Precision;
			if (n.IsNull)
			{
				throw new SqlNullValueException();
			}
			if (digits == 0)
			{
				return n;
			}
			byte b2;
			if (digits > 0)
			{
				b = (byte)((int)b + digits);
				b2 = (byte)((int)n.scale + digits);
				for (int i = 0; i < digits; i++)
				{
					n *= 10L;
				}
			}
			else
			{
				if ((int)n.Scale < Math.Abs(digits))
				{
					throw new SqlTruncateException();
				}
				if (fRound)
				{
					n = SqlDecimal.Round(n, digits + (int)n.scale);
				}
				else
				{
					n = SqlDecimal.Round(SqlDecimal.Truncate(n, digits + (int)n.scale), digits + (int)n.scale);
				}
				b2 = n.scale;
			}
			return new SqlDecimal(b, b2, n.positive, n.Data);
		}

		/// <summary>Returns the smallest whole number greater than or equal to the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> representing the smallest whole number greater than or equal to the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure for which the ceiling value is to be calculated. </param>
		// Token: 0x06000D59 RID: 3417 RVA: 0x00037794 File Offset: 0x00035994
		public static SqlDecimal Ceiling(SqlDecimal n)
		{
			if (!n.notNull)
			{
				return n;
			}
			return SqlDecimal.AdjustScale(n, (int)(-(int)n.Scale), true);
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> instance to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000D5A RID: 3418 RVA: 0x000377B4 File Offset: 0x000359B4
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is SqlDecimal))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Data.SqlTypes.SqlDecimal"));
			}
			return this.CompareTo((SqlDecimal)value);
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlDecimal" /> object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> to be compared. </param>
		// Token: 0x06000D5B RID: 3419 RVA: 0x000377E8 File Offset: 0x000359E8
		public int CompareTo(SqlDecimal value)
		{
			if (value.IsNull)
			{
				return 1;
			}
			return this.Value.CompareTo(value.Value);
		}

		/// <summary>Adjusts the value of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand to the indicated precision and scale.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose Value has been adjusted to the precision and scale indicated in the parameters.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose value is to be adjusted. </param>
		/// <param name="precision">The precision for the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="scale">The scale for the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D5C RID: 3420 RVA: 0x00037818 File Offset: 0x00035A18
		public static SqlDecimal ConvertToPrecScale(SqlDecimal n, int precision, int scale)
		{
			int num = (int)n.Precision;
			int num2 = (int)n.Scale;
			n = SqlDecimal.AdjustScale(n, scale - (int)n.scale, true);
			if ((int)n.Scale >= num2 && precision < (int)n.Precision)
			{
				throw new SqlTruncateException();
			}
			return new SqlDecimal((byte)precision, n.scale, n.IsPositive, n.Data);
		}

		/// <summary>The division operator calculates the results of dividing the first <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D5D RID: 3421 RVA: 0x00037888 File Offset: 0x00035A88
		public static SqlDecimal Divide(SqlDecimal x, SqlDecimal y)
		{
			return x / y;
		}

		/// <summary>Compares the supplied <see cref="T:System.Object" /> parameter to the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> instance.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> and the two are equal. Otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared.</param>
		// Token: 0x06000D5E RID: 3422 RVA: 0x00037894 File Offset: 0x00035A94
		public override bool Equals(object value)
		{
			if (!(value is SqlDecimal))
			{
				return false;
			}
			if (this.IsNull)
			{
				return ((SqlDecimal)value).IsNull;
			}
			return !((SqlDecimal)value).IsNull && (bool)(this == (SqlDecimal)value);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operands to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false. If either instance is null, the value of the SqlDecimal will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D5F RID: 3423 RVA: 0x000378F4 File Offset: 0x00035AF4
		public static SqlBoolean Equals(SqlDecimal x, SqlDecimal y)
		{
			return x == y;
		}

		/// <summary>Rounds a specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> number to the next lower whole number.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure that contains the whole number part of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure for which the floor value is to be calculated. </param>
		// Token: 0x06000D60 RID: 3424 RVA: 0x00037900 File Offset: 0x00035B00
		public static SqlDecimal Floor(SqlDecimal n)
		{
			return SqlDecimal.AdjustScale(n, (int)(-(int)n.Scale), false);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00037914 File Offset: 0x00035B14
		internal static SqlDecimal FromTdsBigDecimal(TdsBigDecimal x)
		{
			if (x == null)
			{
				return SqlDecimal.Null;
			}
			return new SqlDecimal(x.Precision, x.Scale, !x.IsNegative, x.Data);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000D62 RID: 3426 RVA: 0x00037950 File Offset: 0x00035B50
		public override int GetHashCode()
		{
			int num = 10;
			num = 91 * num + this.Data[0];
			num = 91 * num + this.Data[1];
			num = 91 * num + this.Data[2];
			num = 91 * num + this.Data[3];
			num = 91 * num + (int)this.Scale;
			return 91 * num + (int)this.Precision;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D63 RID: 3427 RVA: 0x000379B4 File Offset: 0x00035BB4
		public static SqlBoolean GreaterThan(SqlDecimal x, SqlDecimal y)
		{
			return x > y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D64 RID: 3428 RVA: 0x000379C0 File Offset: 0x00035BC0
		public static SqlBoolean GreaterThanOrEqual(SqlDecimal x, SqlDecimal y)
		{
			return x >= y;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D65 RID: 3429 RVA: 0x000379CC File Offset: 0x00035BCC
		public static SqlBoolean LessThan(SqlDecimal x, SqlDecimal y)
		{
			return x < y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D66 RID: 3430 RVA: 0x000379D8 File Offset: 0x00035BD8
		public static SqlBoolean LessThanOrEqual(SqlDecimal x, SqlDecimal y)
		{
			return x <= y;
		}

		/// <summary>The multiplication operator computes the product of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D67 RID: 3431 RVA: 0x000379E4 File Offset: 0x00035BE4
		public static SqlDecimal Multiply(SqlDecimal x, SqlDecimal y)
		{
			return x * y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D68 RID: 3432 RVA: 0x000379F0 File Offset: 0x00035BF0
		public static SqlBoolean NotEquals(SqlDecimal x, SqlDecimal y)
		{
			return x != y;
		}

		/// <summary>Converts the <see cref="T:System.String" /> representation of a number to its <see cref="T:System.Data.SqlTypes.SqlDecimal" /> equivalent.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> equivalent to the value that is contained in the specified <see cref="T:System.String" />.</returns>
		/// <param name="s">The String to be parsed. </param>
		// Token: 0x06000D69 RID: 3433 RVA: 0x000379FC File Offset: 0x00035BFC
		public static SqlDecimal Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException(Locale.GetText("string s"));
			}
			return new SqlDecimal(decimal.Parse(s));
		}

		/// <summary>Raises the value of the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to the specified exponential power.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure that contains the results.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be raised to a power. </param>
		/// <param name="exp">A double value that indicates the power to which the number should be raised. </param>
		// Token: 0x06000D6A RID: 3434 RVA: 0x00037A20 File Offset: 0x00035C20
		public static SqlDecimal Power(SqlDecimal n, double exp)
		{
			if (n.IsNull)
			{
				return SqlDecimal.Null;
			}
			return new SqlDecimal(Math.Pow(n.ToDouble(), exp));
		}

		/// <summary>Gets the number nearest the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's value with the specified precision.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure that contains the results of the rounding operation.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be rounded. </param>
		/// <param name="position">The number of significant fractional digits (precision) in the return value. </param>
		// Token: 0x06000D6B RID: 3435 RVA: 0x00037A54 File Offset: 0x00035C54
		public static SqlDecimal Round(SqlDecimal n, int position)
		{
			if (n.IsNull)
			{
				throw new SqlNullValueException();
			}
			decimal num = n.Value;
			num = decimal.Round(num, position);
			return new SqlDecimal(num);
		}

		/// <summary>Gets a value that indicates the sign of a <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property.</summary>
		/// <returns>A number that indicates the sign of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose sign is to be evaluated. </param>
		// Token: 0x06000D6C RID: 3436 RVA: 0x00037A8C File Offset: 0x00035C8C
		public static SqlInt32 Sign(SqlDecimal n)
		{
			if (n.IsNull)
			{
				return SqlInt32.Null;
			}
			return (!n.IsPositive) ? (-1) : 1;
		}

		/// <summary>Calculates the results of subtracting the second <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand from the first.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose Value property contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D6D RID: 3437 RVA: 0x00037AC4 File Offset: 0x00035CC4
		public static SqlDecimal Subtract(SqlDecimal x, SqlDecimal y)
		{
			return x - y;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00037AD0 File Offset: 0x00035CD0
		private byte GetPrecision(decimal value)
		{
			string text = value.ToString();
			byte b = 0;
			foreach (char c in text)
			{
				if (c >= '0' && c <= '9')
				{
					b += 1;
				}
			}
			return b;
		}

		/// <summary>Returns the a double equal to the contents of the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of this instance.</summary>
		/// <returns>The decimal representation of the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property.</returns>
		// Token: 0x06000D6F RID: 3439 RVA: 0x00037B24 File Offset: 0x00035D24
		public double ToDouble()
		{
			double num = this.Data[0];
			num += this.Data[1] * Math.Pow(2.0, 32.0);
			num += this.Data[2] * Math.Pow(2.0, 64.0);
			num += this.Data[3] * Math.Pow(2.0, 96.0);
			return num / Math.Pow(10.0, (double)this.scale);
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>true if the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> is non-zero; false if zero; otherwise Null.</returns>
		// Token: 0x06000D70 RID: 3440 RVA: 0x00037BC4 File Offset: 0x00035DC4
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose Value equals the Value of this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. If the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's Value is true, the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's Value will be 1. Otherwise, the <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's Value will be 0.</returns>
		// Token: 0x06000D71 RID: 3441 RVA: 0x00037BD4 File Offset: 0x00035DD4
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x06000D72 RID: 3442 RVA: 0x00037BE4 File Offset: 0x00035DE4
		public SqlDouble ToSqlDouble()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x06000D73 RID: 3443 RVA: 0x00037BF4 File Offset: 0x00035DF4
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x06000D74 RID: 3444 RVA: 0x00037C04 File Offset: 0x00035E04
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x06000D75 RID: 3445 RVA: 0x00037C14 File Offset: 0x00035E14
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x06000D76 RID: 3446 RVA: 0x00037C24 File Offset: 0x00035E24
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure with the same value as this instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		// Token: 0x06000D77 RID: 3447 RVA: 0x00037C34 File Offset: 0x00035E34
		public SqlSingle ToSqlSingle()
		{
			return this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> structure whose value is a string representing the value contained in this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</returns>
		// Token: 0x06000D78 RID: 3448 RVA: 0x00037C44 File Offset: 0x00035E44
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to <see cref="T:System.String" />.</summary>
		/// <returns>A new <see cref="T:System.String" /> object that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property.</returns>
		// Token: 0x06000D79 RID: 3449 RVA: 0x00037C54 File Offset: 0x00035E54
		public override string ToString()
		{
			if (this.IsNull)
			{
				return "Null";
			}
			ulong num = (ulong)this.Data[0];
			num += (ulong)((ulong)((long)this.Data[1]) << 32);
			ulong num2 = (ulong)this.Data[2];
			num2 += (ulong)((ulong)((long)this.Data[3]) << 32);
			uint num3 = 0U;
			StringBuilder stringBuilder = new StringBuilder();
			int num4 = 0;
			while (num != 0UL || num2 != 0UL)
			{
				SqlDecimal.Div128By32(ref num2, ref num, 10U, ref num3);
				stringBuilder.Insert(0, num3.ToString());
				num4++;
			}
			while (stringBuilder.Length > (int)this.Precision)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			if (this.Scale > 0)
			{
				stringBuilder.Insert(stringBuilder.Length - (int)this.Scale, ".");
			}
			if (!this.positive)
			{
				stringBuilder.Insert(0, '-');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00037D4C File Offset: 0x00035F4C
		private static int Div128By32(ref ulong hi, ref ulong lo, uint divider)
		{
			uint num = 0U;
			return SqlDecimal.Div128By32(ref hi, ref lo, divider, ref num);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00037D68 File Offset: 0x00035F68
		private static int Div128By32(ref ulong hi, ref ulong lo, uint divider, ref uint rest)
		{
			ulong num = (ulong)((uint)(hi >> 32));
			ulong num2 = num / (ulong)divider;
			num -= num2 * (ulong)divider;
			num <<= 32;
			num |= (ulong)((uint)hi);
			ulong num3 = num / (ulong)divider;
			num -= num3 * (ulong)divider;
			num <<= 32;
			hi = (num2 << 32) | (ulong)((uint)num3);
			num |= (ulong)((uint)(lo >> 32));
			num2 = num / (ulong)divider;
			num -= num2 * (ulong)divider;
			num <<= 32;
			num |= (ulong)((uint)lo);
			num3 = num / (ulong)divider;
			num -= num3 * (ulong)divider;
			lo = (num2 << 32) | (ulong)((uint)num3);
			rest = (uint)num;
			num <<= 1;
			return (num <= (ulong)divider && (num != (ulong)divider || (num3 & 1UL) != 1UL)) ? 0 : 1;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00037E1C File Offset: 0x0003601C
		[MonoTODO("Find out what is the right way to set scale and precision")]
		private static SqlDecimal DecimalDiv(SqlDecimal x, SqlDecimal y)
		{
			ulong num = 0UL;
			ulong num2 = 0UL;
			int num3 = 0;
			int num4 = 0;
			bool flag = !(x.positive ^ y.positive);
			byte b = ((x.Precision < y.Precision) ? y.Precision : x.Precision);
			SqlDecimal.DecimalDivSub(ref x, ref y, ref num, ref num2, ref num4);
			num3 = (int)(x.Scale - y.Scale);
			SqlDecimal.Rescale128(ref num, ref num2, ref num3, num4, 0, 38, 1);
			uint num5 = 0U;
			while ((int)b < num3)
			{
				SqlDecimal.Div128By32(ref num2, ref num, 10U, ref num5);
				num3--;
			}
			if (num5 >= 5U)
			{
				num += 1UL;
			}
			while (num2 * Math.Pow(2.0, 64.0) + num - Math.Pow(10.0, (double)b) > 0.0)
			{
				b += 1;
			}
			while ((int)b + num3 > (int)SqlDecimal.MaxScale)
			{
				SqlDecimal.Div128By32(ref num2, ref num, 10U, ref num5);
				num3--;
				if (num5 >= 5U)
				{
					num += 1UL;
				}
			}
			int num6 = (int)num;
			int num7 = (int)(num >> 32);
			int num8 = (int)num2;
			int num9 = (int)(num2 >> 32);
			return new SqlDecimal(b, (byte)num3, flag, num6, num7, num8, num9);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00037F78 File Offset: 0x00036178
		private static void Rescale128(ref ulong clo, ref ulong chi, ref int scale, int texp, int minScale, int maxScale, int roundFlag)
		{
			int i = 0;
			int num = 0;
			i = scale;
			if (texp > 0)
			{
				while (texp > 0 && i <= maxScale)
				{
					uint num2 = (uint)chi;
					while (texp > 0 && ((clo & 1UL) == 0UL || num2 > 0U))
					{
						if (--texp == 0)
						{
							num = (int)(clo & 1UL);
						}
						SqlDecimal.RShift128(ref clo, ref chi);
						num2 = (uint)(chi >> 32);
					}
					int num3;
					if (texp > 9)
					{
						num3 = 9;
					}
					else
					{
						num3 = texp;
					}
					if (i + num3 > maxScale)
					{
						num3 = maxScale - i;
					}
					if (num3 == 0)
					{
						break;
					}
					texp -= num3;
					i += num3;
					uint num4 = SqlDecimal.constantsDecadeInt32Factors[num3] >> num3;
					SqlDecimal.Mult128By32(ref clo, ref chi, num4, 0);
				}
				while (texp > 0)
				{
					if (--texp == 0)
					{
						num = (int)(clo & 1UL);
					}
					SqlDecimal.RShift128(ref clo, ref chi);
				}
			}
			while (i > maxScale)
			{
				int num3 = scale - maxScale;
				if (num3 > 9)
				{
					num3 = 9;
				}
				i -= num3;
				num = SqlDecimal.Div128By32(ref clo, ref chi, SqlDecimal.constantsDecadeInt32Factors[num3]);
			}
			while (i < minScale)
			{
				if (roundFlag == 0)
				{
					num = 0;
				}
				int num3 = minScale - i;
				if (num3 > 9)
				{
					num3 = 9;
				}
				i += num3;
				SqlDecimal.Mult128By32(ref clo, ref chi, SqlDecimal.constantsDecadeInt32Factors[num3], num);
				num = 0;
			}
			scale = i;
			SqlDecimal.Normalize128(ref clo, ref chi, ref i, roundFlag, num);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000380E4 File Offset: 0x000362E4
		private static void Normalize128(ref ulong clo, ref ulong chi, ref int scale, int roundFlag, int roundBit)
		{
			int num = scale;
			scale = num;
			if (roundFlag != 0 && roundBit != 0)
			{
				SqlDecimal.RoundUp128(ref clo, ref chi);
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0003810C File Offset: 0x0003630C
		private static void RoundUp128(ref ulong lo, ref ulong hi)
		{
			if ((lo += 1UL) == 0UL)
			{
				hi += 1UL;
			}
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00038130 File Offset: 0x00036330
		private static void DecimalDivSub(ref SqlDecimal x, ref SqlDecimal y, ref ulong clo, ref ulong chi, ref int exp)
		{
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = 0U;
			uint num4 = 0U;
			ulong num5 = (ulong)(((long)x.Data[3] << 32) | (long)x.Data[2]);
			ulong num6 = (ulong)(((long)x.Data[1] << 32) | (long)x.Data[0]);
			ulong num7 = 0UL;
			num = (uint)y.Data[0];
			num2 = (uint)y.Data[1];
			num3 = (uint)y.Data[2];
			num4 = (uint)y.Data[3];
			if (num == 0U && num2 == 0U && num3 == 0U && num4 == 0U)
			{
				throw new DivideByZeroException();
			}
			if (num6 == 0UL && num5 == 0UL)
			{
				clo = (chi = 0UL);
				return;
			}
			int num8 = 0;
			while ((num5 & 9223372036854775808UL) == 0UL)
			{
				SqlDecimal.LShift128(ref num6, ref num5);
				num8++;
			}
			int num9 = 0;
			while (((ulong)num4 & (ulong)(-2147483648)) == 0UL)
			{
				SqlDecimal.LShift128(ref num, ref num2, ref num3, ref num4);
				num9++;
			}
			ulong num10 = ((ulong)num4 << 32) | (ulong)num3;
			ulong num11 = ((ulong)num2 << 32) | (ulong)num;
			ulong num12 = 0UL;
			int num13;
			if (num5 > num10 || (num5 == num10 && num6 >= num11))
			{
				SqlDecimal.Sub192(num7, num6, num5, num12, num11, num10, ref num7, ref num6, ref num5);
				num13 = 1;
			}
			else
			{
				num13 = 0;
			}
			SqlDecimal.Div192By128To128(num7, num6, num5, num, num2, num3, num4, ref clo, ref chi);
			exp = 128 + num8 - num9;
			if (num13 != 0)
			{
				SqlDecimal.RShift128(ref clo, ref chi);
				chi += 9223372036854775808UL;
				exp--;
			}
			while (exp > 0 && (clo & 1UL) == 0UL)
			{
				SqlDecimal.RShift128(ref clo, ref chi);
				exp--;
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00038300 File Offset: 0x00036500
		private static void RShift128(ref ulong lo, ref ulong hi)
		{
			lo >>= 1;
			if ((hi & 1UL) != 0UL)
			{
				lo |= 9223372036854775808UL;
			}
			hi >>= 1;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00038334 File Offset: 0x00036534
		private static void LShift128(ref ulong lo, ref ulong hi)
		{
			hi <<= 1;
			if ((lo & 9223372036854775808UL) != 0UL)
			{
				hi += 1UL;
			}
			lo <<= 1;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00038368 File Offset: 0x00036568
		private static void LShift128(ref uint lo, ref uint mi, ref uint mi2, ref uint hi)
		{
			hi <<= 1;
			if (((ulong)mi2 & (ulong)(-2147483648)) != 0UL)
			{
				hi += 1U;
			}
			mi2 <<= 1;
			if (((ulong)mi & (ulong)(-2147483648)) != 0UL)
			{
				mi2 += 1U;
			}
			mi <<= 1;
			if (((ulong)lo & (ulong)(-2147483648)) != 0UL)
			{
				mi += 1U;
			}
			lo <<= 1;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x000383CC File Offset: 0x000365CC
		private static void Div192By128To128(ulong xlo, ulong xmi, ulong xhi, uint ylo, uint ymi, uint ymi2, uint yhi, ref ulong clo, ref ulong chi)
		{
			ulong num = xlo;
			ulong num2 = xmi;
			ulong num3 = xhi;
			uint num4 = SqlDecimal.Div192By128To32WithRest(ref num, ref num2, ref num3, ylo, ymi, ymi2, yhi);
			num3 = (num3 << 32) | (num2 >> 32);
			num2 = (num2 << 32) | (num >> 32);
			num <<= 32;
			chi = ((ulong)num4 << 32) | (ulong)SqlDecimal.Div192By128To32WithRest(ref num, ref num2, ref num3, ylo, ymi, ymi2, yhi);
			num3 = (num3 << 32) | (num2 >> 32);
			num2 = (num2 << 32) | (num >> 32);
			num <<= 32;
			num4 = SqlDecimal.Div192By128To32WithRest(ref num, ref num2, ref num3, ylo, ymi, ymi2, yhi);
			uint num5;
			if (num3 >= (ulong)yhi)
			{
				num5 = uint.MaxValue;
			}
			else
			{
				num3 <<= 32;
				num5 = (uint)(num3 / (ulong)yhi);
			}
			clo = ((ulong)num4 << 32) | (ulong)num5;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00038480 File Offset: 0x00036680
		private static uint Div192By128To32WithRest(ref ulong xlo, ref ulong xmi, ref ulong xhi, uint ylo, uint ymi, uint ymi2, uint yhi)
		{
			ulong num = 0UL;
			ulong num2 = 0UL;
			ulong num3 = xlo;
			ulong num4 = xmi;
			ulong num5 = xhi;
			uint num6;
			if (num5 >= (ulong)yhi << 32)
			{
				num6 = uint.MaxValue;
			}
			else
			{
				num6 = (uint)(num5 / (ulong)yhi);
			}
			SqlDecimal.Mult128By32To128(ylo, ymi, ymi2, yhi, num6, ref num, ref num2);
			SqlDecimal.Sub192(num3, num4, num5, 0UL, num, num2, ref num3, ref num4, ref num5);
			while (num5 < 0UL)
			{
				num6 -= 1U;
				SqlDecimal.Add192(num3, num4, num5, 0UL, ((ulong)ymi << 32) | (ulong)ylo, (ulong)(yhi | ymi2), ref num3, ref num4, ref num5);
			}
			xlo = num3;
			xmi = num4;
			xhi = num5;
			return num6;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0003851C File Offset: 0x0003671C
		private static void Mult128By32(ref ulong clo, ref ulong chi, uint factor, int roundBit)
		{
			ulong num = (ulong)((uint)clo) * (ulong)factor;
			if (roundBit != 0)
			{
				num += (ulong)(factor / 2U);
			}
			uint num2 = (uint)num;
			num >>= 32;
			num += (clo >> 32) * (ulong)factor;
			uint num3 = (uint)num;
			clo = ((ulong)num3 << 32) | (ulong)num2;
			num >>= 32;
			num += (ulong)((uint)chi) * (ulong)factor;
			num2 = (uint)num;
			num >>= 32;
			num += (chi >> 32) * (ulong)factor;
			num3 = (uint)num;
			chi = ((ulong)num3 << 32) | (ulong)num2;
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00038594 File Offset: 0x00036794
		private static void Mult128By32To128(uint xlo, uint xmi, uint xmi2, uint xhi, uint factor, ref ulong clo, ref ulong chi)
		{
			ulong num = (ulong)xlo * (ulong)factor;
			uint num2 = (uint)num;
			num >>= 32;
			num += (ulong)xmi * (ulong)factor;
			uint num3 = (uint)num;
			num >>= 32;
			num += (ulong)xmi2 * (ulong)factor;
			uint num4 = (uint)num;
			num >>= 32;
			num += (ulong)xhi * (ulong)factor;
			clo = ((ulong)num3 << 32) | (ulong)num2;
			chi = num | (ulong)num4;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x000385F0 File Offset: 0x000367F0
		private static void Add192(ulong xlo, ulong xmi, ulong xhi, ulong ylo, ulong ymi, ulong yhi, ref ulong clo, ref ulong cmi, ref ulong chi)
		{
			xlo += ylo;
			if (xlo < ylo)
			{
				xmi += 1UL;
				if (xmi == 0UL)
				{
					xhi += 1UL;
				}
			}
			xmi += ymi;
			if (xmi < ymi)
			{
				xmi += 1UL;
			}
			xhi += yhi;
			clo = xlo;
			cmi = xmi;
			chi = xhi;
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00038644 File Offset: 0x00036844
		private static void Sub192(ulong xlo, ulong xmi, ulong xhi, ulong ylo, ulong ymi, ulong yhi, ref ulong lo, ref ulong mi, ref ulong hi)
		{
			ulong num = xlo - ylo;
			ulong num2 = xmi - ymi;
			ulong num3 = xhi - yhi;
			if (xlo < ylo)
			{
				if (num2 == 0UL)
				{
					num3 -= 1UL;
				}
				num2 -= 1UL;
			}
			if (xmi < ymi)
			{
				num3 -= 1UL;
			}
			lo = num;
			mi = num2;
			hi = num3;
		}

		/// <summary>Truncates the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's value to the that you want position.</summary>
		/// <returns>Supply a negative value for the <paramref name="position" /> parameter in order to truncate the value to the corresponding position to the left of the decimal point.</returns>
		/// <param name="n">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be truncated. </param>
		/// <param name="position">The decimal position to which the number will be truncated. </param>
		// Token: 0x06000D8A RID: 3466 RVA: 0x00038698 File Offset: 0x00036898
		public static SqlDecimal Truncate(SqlDecimal n, int position)
		{
			int num = (int)n.scale - position;
			if (num == 0)
			{
				return n;
			}
			int[] array = n.Data;
			decimal num2 = new decimal(array[0], array[1], array[2], !n.positive, 0);
			decimal num3 = 10m;
			int i = 0;
			while (i < num)
			{
				num2 -= num2 % num3;
				i++;
				num3 *= 10m;
			}
			array = decimal.GetBits(num2);
			array[3] = 0;
			return new SqlDecimal(n.precision, n.scale, n.positive, array);
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000D8B RID: 3467 RVA: 0x0003873C File Offset: 0x0003693C
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			if (schemaSet != null && schemaSet.Count == 0)
			{
				XmlSchema xmlSchema = new XmlSchema();
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				xmlSchemaComplexType.Name = "decimal";
				xmlSchema.Items.Add(xmlSchemaComplexType);
				schemaSet.Add(xmlSchema);
			}
			return new XmlQualifiedName("decimal", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Calculates the sum of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operators.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the sum.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D8C RID: 3468 RVA: 0x00038798 File Offset: 0x00036998
		public static SqlDecimal operator +(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDecimal.Null;
			}
			if (x.IsPositive && !y.IsPositive)
			{
				y = new SqlDecimal(y.Precision, y.Scale, !y.IsPositive, y.Data);
				return x - y;
			}
			if (!x.IsPositive && y.IsPositive)
			{
				x = new SqlDecimal(x.Precision, x.Scale, !x.IsPositive, x.Data);
				return y - x;
			}
			if (!x.IsPositive && !y.IsPositive)
			{
				x = new SqlDecimal(x.Precision, x.Scale, !x.IsPositive, x.Data);
				y = new SqlDecimal(y.Precision, y.Scale, !y.IsPositive, y.Data);
				x += y;
				return new SqlDecimal(x.Precision, x.Scale, !x.IsPositive, x.Data);
			}
			if (x.scale > y.scale)
			{
				y = SqlDecimal.AdjustScale(y, (int)(x.scale - y.scale), false);
			}
			else if (y.scale > x.scale)
			{
				x = SqlDecimal.AdjustScale(x, (int)(y.scale - x.scale), false);
			}
			byte b = (byte)((int)Math.Max(x.Scale, y.Scale) + Math.Max((int)(x.Precision - x.Scale), (int)(y.Precision - y.Scale)) + 1);
			if (b > SqlDecimal.MaxPrecision)
			{
				b = SqlDecimal.MaxPrecision;
			}
			int[] data = x.Data;
			int[] data2 = y.Data;
			int[] array = new int[4];
			ulong num = 0UL;
			for (int i = 0; i < 4; i++)
			{
				ulong num2 = (ulong)data[i] + (ulong)data2[i] + num;
				array[i] = (int)(num2 & (ulong)(-1));
				num = num2 >> 32;
			}
			if (num > 0UL)
			{
				throw new OverflowException();
			}
			return new SqlDecimal(b, x.Scale, x.IsPositive, array);
		}

		/// <summary>The division operator calculates the results of dividing the first <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand by the second.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the results of the division.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D8D RID: 3469 RVA: 0x00038A04 File Offset: 0x00036C04
		public static SqlDecimal operator /(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDecimal.Null;
			}
			return SqlDecimal.DecimalDiv(x, y);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operands to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D8E RID: 3470 RVA: 0x00038A2C File Offset: 0x00036C2C
		public static SqlBoolean operator ==(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			if (x.IsPositive != y.IsPositive)
			{
				return SqlBoolean.False;
			}
			if (x.Scale > y.Scale)
			{
				y = SqlDecimal.AdjustScale(y, (int)(x.Scale - y.Scale), false);
			}
			else if (y.Scale > x.Scale)
			{
				x = SqlDecimal.AdjustScale(y, (int)(y.Scale - x.Scale), false);
			}
			for (int i = 0; i < 4; i++)
			{
				if (x.Data[i] != y.Data[i])
				{
					return SqlBoolean.False;
				}
			}
			return SqlBoolean.True;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D8F RID: 3471 RVA: 0x00038B00 File Offset: 0x00036D00
		public static SqlBoolean operator >(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			if (x.IsPositive != y.IsPositive)
			{
				return new SqlBoolean(x.IsPositive);
			}
			if (x.Scale > y.Scale)
			{
				y = SqlDecimal.AdjustScale(y, (int)(x.Scale - y.Scale), false);
			}
			else if (y.Scale > x.Scale)
			{
				x = SqlDecimal.AdjustScale(x, (int)(y.Scale - x.Scale), false);
			}
			for (int i = 3; i >= 0; i--)
			{
				if (x.Data[i] != 0 || y.Data[i] != 0)
				{
					return new SqlBoolean(x.Data[i] > y.Data[i]);
				}
			}
			return new SqlBoolean(false);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D90 RID: 3472 RVA: 0x00038BFC File Offset: 0x00036DFC
		public static SqlBoolean operator >=(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			if (x.IsPositive != y.IsPositive)
			{
				return new SqlBoolean(x.IsPositive);
			}
			if (x.Scale > y.Scale)
			{
				y = SqlDecimal.AdjustScale(y, (int)(x.Scale - y.Scale), true);
			}
			else if (y.Scale > x.Scale)
			{
				x = SqlDecimal.AdjustScale(x, (int)(y.Scale - x.Scale), true);
			}
			for (int i = 3; i >= 0; i--)
			{
				if (x.Data[i] != 0 || y.Data[i] != 0)
				{
					return new SqlBoolean(x.Data[i] >= y.Data[i]);
				}
			}
			return new SqlBoolean(true);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D91 RID: 3473 RVA: 0x00038CF8 File Offset: 0x00036EF8
		public static SqlBoolean operator !=(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			if (x.IsPositive != y.IsPositive)
			{
				return SqlBoolean.True;
			}
			if (x.Scale > y.Scale)
			{
				x = SqlDecimal.AdjustScale(x, (int)(y.Scale - x.Scale), true);
			}
			else if (y.Scale > x.Scale)
			{
				y = SqlDecimal.AdjustScale(y, (int)(x.Scale - y.Scale), true);
			}
			for (int i = 0; i < 4; i++)
			{
				if (x.Data[i] != y.Data[i])
				{
					return SqlBoolean.True;
				}
			}
			return SqlBoolean.False;
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structures to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D92 RID: 3474 RVA: 0x00038DCC File Offset: 0x00036FCC
		public static SqlBoolean operator <(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			if (x.IsPositive != y.IsPositive)
			{
				return new SqlBoolean(y.IsPositive);
			}
			if (x.Scale > y.Scale)
			{
				y = SqlDecimal.AdjustScale(y, (int)(x.Scale - y.Scale), true);
			}
			else if (y.Scale > x.Scale)
			{
				x = SqlDecimal.AdjustScale(x, (int)(y.Scale - x.Scale), true);
			}
			for (int i = 3; i >= 0; i--)
			{
				if (x.Data[i] != 0 || y.Data[i] != 0)
				{
					return new SqlBoolean(x.Data[i] < y.Data[i]);
				}
			}
			return new SqlBoolean(false);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDecimal" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D93 RID: 3475 RVA: 0x00038EC8 File Offset: 0x000370C8
		public static SqlBoolean operator <=(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			if (x.IsPositive != y.IsPositive)
			{
				return new SqlBoolean(y.IsPositive);
			}
			if (x.Scale > y.Scale)
			{
				y = SqlDecimal.AdjustScale(y, (int)(x.Scale - y.Scale), true);
			}
			else if (y.Scale > x.Scale)
			{
				x = SqlDecimal.AdjustScale(x, (int)(y.Scale - x.Scale), true);
			}
			for (int i = 3; i >= 0; i--)
			{
				if (x.Data[i] != 0 || y.Data[i] != 0)
				{
					return new SqlBoolean(x.Data[i] <= y.Data[i]);
				}
			}
			return new SqlBoolean(true);
		}

		/// <summary>The multiplication operator computes the product of the two <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameters.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property contains the product of the multiplication.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D94 RID: 3476 RVA: 0x00038FC4 File Offset: 0x000371C4
		public static SqlDecimal operator *(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDecimal.Null;
			}
			byte b = x.Precision + y.Precision + 1;
			byte b2 = x.Scale + y.Scale;
			if (b > SqlDecimal.MaxPrecision)
			{
				b = SqlDecimal.MaxPrecision;
			}
			int[] data = x.Data;
			int[] data2 = y.Data;
			int[] array = new int[4];
			ulong num = 0UL;
			for (int i = 0; i < 4; i++)
			{
				ulong num2 = 0UL;
				for (int j = i; j <= i; j++)
				{
					num2 += (ulong)data[j] * (ulong)data2[i - j];
				}
				array[i] = (int)((num2 + num) & (ulong)(-1));
				num = num2 >> 32;
			}
			if (num > 0UL)
			{
				throw new OverflowException();
			}
			return new SqlDecimal(b, b2, x.IsPositive == y.IsPositive, array);
		}

		/// <summary>Calculates the results of subtracting the second <see cref="T:System.Data.SqlTypes.SqlDecimal" /> operand from the first.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose Value property contains the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure. </param>
		// Token: 0x06000D95 RID: 3477 RVA: 0x000390C4 File Offset: 0x000372C4
		public static SqlDecimal operator -(SqlDecimal x, SqlDecimal y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlDecimal.Null;
			}
			if (x.IsPositive && !y.IsPositive)
			{
				y = new SqlDecimal(y.Precision, y.Scale, !y.IsPositive, y.Data);
				return x + y;
			}
			if (!x.IsPositive && y.IsPositive)
			{
				x = new SqlDecimal(x.Precision, x.Scale, !x.IsPositive, x.Data);
				x += y;
				return new SqlDecimal(x.Precision, x.Scale, false, x.Data);
			}
			if (!x.IsPositive && !y.IsPositive)
			{
				y = new SqlDecimal(y.Precision, y.Scale, !y.IsPositive, y.Data);
				x = new SqlDecimal(x.Precision, x.Scale, !x.IsPositive, x.Data);
				return y - x;
			}
			if (x.scale > y.scale)
			{
				y = SqlDecimal.AdjustScale(y, (int)(x.scale - y.scale), false);
			}
			else if (y.scale > x.scale)
			{
				x = SqlDecimal.AdjustScale(x, (int)(y.scale - x.scale), false);
			}
			byte b = (byte)((int)Math.Max(x.Scale, y.Scale) + Math.Max((int)(x.Precision - x.Scale), (int)(y.Precision - y.Scale)));
			int[] array;
			int[] array2;
			if (x >= y)
			{
				array = x.Data;
				array2 = y.Data;
			}
			else
			{
				array = y.Data;
				array2 = x.Data;
			}
			int num = 0;
			int[] array3 = new int[4];
			for (int i = 0; i < 4; i++)
			{
				ulong num2 = (ulong)array[i] - (ulong)array2[i] + (ulong)((long)num);
				num = 0;
				if (array2[i] > array[i])
				{
					num = -1;
				}
				array3[i] = (int)num2;
			}
			if (num > 0)
			{
				throw new OverflowException();
			}
			return new SqlDecimal(b, x.Scale, (x >= y).Value, array3);
		}

		/// <summary>The unary minus operator negates the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose value contains the results of the negation.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be negated. </param>
		// Token: 0x06000D96 RID: 3478 RVA: 0x0003934C File Offset: 0x0003754C
		public static SqlDecimal operator -(SqlDecimal x)
		{
			return new SqlDecimal(x.Precision, x.Scale, !x.IsPositive, x.Data);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be converted. </param>
		// Token: 0x06000D97 RID: 3479 RVA: 0x00039380 File Offset: 0x00037580
		public static explicit operator SqlDecimal(SqlBoolean x)
		{
			if (x.IsNull)
			{
				return SqlDecimal.Null;
			}
			return new SqlDecimal(x.ByteValue);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to <see cref="T:System.Decimal" />.</summary>
		/// <returns>A new Decimal structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure to be converted. </param>
		// Token: 0x06000D98 RID: 3480 RVA: 0x000393A8 File Offset: 0x000375A8
		public static explicit operator decimal(SqlDecimal x)
		{
			return x.Value;
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to be converted. </param>
		// Token: 0x06000D99 RID: 3481 RVA: 0x000393B4 File Offset: 0x000375B4
		public static explicit operator SqlDecimal(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlDecimal.Null;
			}
			return new SqlDecimal(x.Value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to be converted. </param>
		// Token: 0x06000D9A RID: 3482 RVA: 0x000393D8 File Offset: 0x000375D8
		public static explicit operator SqlDecimal(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlDecimal.Null;
			}
			return new SqlDecimal((double)x.Value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> parameter to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> equals the value represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlString" /> object to be converted. </param>
		// Token: 0x06000D9B RID: 3483 RVA: 0x000393FC File Offset: 0x000375FC
		public static explicit operator SqlDecimal(SqlString x)
		{
			return SqlDecimal.Parse(x.Value);
		}

		/// <summary>Converts the <see cref="T:System.Double" /> parameter to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose value equals the value of the <see cref="T:System.Double" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Double" /> structure to be converted.</param>
		// Token: 0x06000D9C RID: 3484 RVA: 0x0003940C File Offset: 0x0003760C
		public static explicit operator SqlDecimal(double x)
		{
			return new SqlDecimal(x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Int64" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the value of the <see cref="T:System.Int64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Int64" /> structure to be converted.</param>
		// Token: 0x06000D9D RID: 3485 RVA: 0x00039414 File Offset: 0x00037614
		public static implicit operator SqlDecimal(long x)
		{
			return new SqlDecimal(x);
		}

		/// <summary>Converts the <see cref="T:System.Decimal" /> value to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the value of the Decimal parameter.</returns>
		/// <param name="x">The <see cref="T:System.Decimal" /> value to be converted. </param>
		// Token: 0x06000D9E RID: 3486 RVA: 0x0003941C File Offset: 0x0003761C
		public static implicit operator SqlDecimal(decimal x)
		{
			return new SqlDecimal(x);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to be converted. </param>
		// Token: 0x06000D9F RID: 3487 RVA: 0x00039424 File Offset: 0x00037624
		public static implicit operator SqlDecimal(SqlByte x)
		{
			if (x.IsNull)
			{
				return SqlDecimal.Null;
			}
			return new SqlDecimal(x.Value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" /></summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to be converted. </param>
		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003944C File Offset: 0x0003764C
		public static implicit operator SqlDecimal(SqlInt16 x)
		{
			if (x.IsNull)
			{
				return SqlDecimal.Null;
			}
			return new SqlDecimal(x.Value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property is equal to the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure to be converted. </param>
		// Token: 0x06000DA1 RID: 3489 RVA: 0x00039474 File Offset: 0x00037674
		public static implicit operator SqlDecimal(SqlInt32 x)
		{
			if (x.IsNull)
			{
				return SqlDecimal.Null;
			}
			return new SqlDecimal(x.Value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to SqlDecimal.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to be converted. </param>
		// Token: 0x06000DA2 RID: 3490 RVA: 0x0003949C File Offset: 0x0003769C
		public static implicit operator SqlDecimal(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlDecimal.Null;
			}
			return new SqlDecimal(x.Value);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlMoney" /> operand to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> equals the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to be converted. </param>
		// Token: 0x06000DA3 RID: 3491 RVA: 0x000394C4 File Offset: 0x000376C4
		public static implicit operator SqlDecimal(SqlMoney x)
		{
			if (x.IsNull)
			{
				return SqlDecimal.Null;
			}
			return new SqlDecimal(x.Value);
		}

		// Token: 0x040004EE RID: 1262
		private const int SCALE_SHIFT = 16;

		// Token: 0x040004EF RID: 1263
		private const int SIGN_SHIFT = 31;

		// Token: 0x040004F0 RID: 1264
		private const int RESERVED_SS32_BITS = 2130771967;

		// Token: 0x040004F1 RID: 1265
		private const ulong LIT_GUINT64_HIGHBIT = 9223372036854775808UL;

		// Token: 0x040004F2 RID: 1266
		private const ulong LIT_GUINT32_HIGHBIT = 2147483648UL;

		// Token: 0x040004F3 RID: 1267
		private const byte DECIMAL_MAX_INTFACTORS = 9;

		// Token: 0x040004F4 RID: 1268
		private int[] value;

		// Token: 0x040004F5 RID: 1269
		private byte precision;

		// Token: 0x040004F6 RID: 1270
		private byte scale;

		// Token: 0x040004F7 RID: 1271
		private bool positive;

		// Token: 0x040004F8 RID: 1272
		private bool notNull;

		// Token: 0x040004F9 RID: 1273
		private static uint[] constantsDecadeInt32Factors = new uint[] { 1U, 10U, 100U, 1000U, 10000U, 100000U, 1000000U, 10000000U, 100000000U, 1000000000U };

		/// <summary>A constant representing the largest possible value for the <see cref="P:System.Data.SqlTypes.SqlDecimal.Precision" /> property.</summary>
		// Token: 0x040004FA RID: 1274
		public static readonly byte MaxPrecision = 38;

		/// <summary>A constant representing the maximum value for the <see cref="P:System.Data.SqlTypes.SqlDecimal.Scale" /> property.</summary>
		// Token: 0x040004FB RID: 1275
		public static readonly byte MaxScale = 38;

		/// <summary>A constant representing the maximum value of a <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</summary>
		// Token: 0x040004FC RID: 1276
		public static readonly SqlDecimal MaxValue = new SqlDecimal(SqlDecimal.MaxPrecision, 0, true, -1, 160047679, 1518781562, 1262177448);

		/// <summary>A constant representing the minimum value for a <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure.</summary>
		// Token: 0x040004FD RID: 1277
		public static readonly SqlDecimal MinValue = new SqlDecimal(SqlDecimal.MaxPrecision, 0, false, -1, 160047679, 1518781562, 1262177448);

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlDecimal" />class.</summary>
		// Token: 0x040004FE RID: 1278
		public static readonly SqlDecimal Null;
	}
}
