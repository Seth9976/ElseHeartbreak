using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents the date and time data ranging in value from January 1, 1753 to December 31, 9999 to an accuracy of 3.33 milliseconds to be stored in or retrieved from a database. The <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure has a different underlying data structure from its corresponding .NET Framework type, <see cref="T:System.DateTime" />, which can represent any time between 12:00:00 AM 1/1/0001 and 11:59:59 PM 12/31/9999, to the accuracy of 100 nanoseconds. <see cref="T:System.Data.SqlTypes.SqlDateTime" /> actually stores the relative difference to 00:00:00 AM 1/1/1900. Therefore, a conversion from "00:00:00 AM 1/1/1900" to an integer will return 0.</summary>
	// Token: 0x02000107 RID: 263
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlDateTime : IXmlSerializable, IComparable, INullable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="value">A DateTime structure. </param>
		// Token: 0x06000D19 RID: 3353 RVA: 0x0003683C File Offset: 0x00034A3C
		public SqlDateTime(DateTime value)
		{
			this.value = value;
			this.notNull = true;
			SqlDateTime.CheckRange(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters.</summary>
		/// <param name="dayTicks">An integer value that represents the date as ticks. </param>
		/// <param name="timeTicks">An integer value that represents the time as ticks. </param>
		// Token: 0x06000D1A RID: 3354 RVA: 0x00036858 File Offset: 0x00034A58
		public SqlDateTime(int dayTicks, int timeTicks)
		{
			try
			{
				long num = SqlDateTime.SQLTicksToMilliseconds(timeTicks);
				this.value = SqlDateTime.zero_day.AddDays((double)dayTicks).AddMilliseconds((double)num);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw new SqlTypeException(ex.Message);
			}
			this.notNull = true;
			SqlDateTime.CheckRange(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters to initialize the year, month, day.</summary>
		/// <param name="year">An integer representing the year of the of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="month">An integer value representing the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="day">An integer value representing the day number of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D1B RID: 3355 RVA: 0x000368D0 File Offset: 0x00034AD0
		public SqlDateTime(int year, int month, int day)
		{
			try
			{
				this.value = new DateTime(year, month, day);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw new SqlTypeException(ex.Message);
			}
			this.notNull = true;
			SqlDateTime.CheckRange(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters to initialize the year, month, day, hour, minute, and second of the new structure.</summary>
		/// <param name="year">An integer value representing the year of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="month">An integer value representing the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="day">An integer value representing the day of the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="hour">An integer value representing the hour of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="minute">An integer value representing the minute of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="second">An integer value representing the second of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D1C RID: 3356 RVA: 0x00036930 File Offset: 0x00034B30
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second)
		{
			try
			{
				this.value = new DateTime(year, month, day, hour, minute, second);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw new SqlTypeException(ex.Message);
			}
			this.notNull = true;
			SqlDateTime.CheckRange(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters to initialize the year, month, day, hour, minute, second, and millisecond of the new structure.</summary>
		/// <param name="year">An integer value representing the year of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="month">An integer value representing the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="day">An integer value representing the day of the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="hour">An integer value representing the hour of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="minute">An integer value representing the minute of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="second">An integer value representing the second of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="millisecond">An double value representing the millisecond of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D1D RID: 3357 RVA: 0x00036998 File Offset: 0x00034B98
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second, double millisecond)
		{
			try
			{
				long num = (long)(millisecond * 10000.0);
				long num2 = SqlDateTime.SQLTicksToMilliseconds(SqlDateTime.TimeSpanTicksToSQLTicks(num));
				DateTime dateTime = new DateTime(year, month, day, hour, minute, second);
				this.value = dateTime.AddMilliseconds((double)num2);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw new SqlTypeException(ex.Message);
			}
			this.notNull = true;
			SqlDateTime.CheckRange(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure using the supplied parameters to initialize the year, month, day, hour, minute, second, and billisecond of the new structure.</summary>
		/// <param name="year">An integer value representing the year of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="month">An integer value representing the month of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="day">An integer value representing the day of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="hour">An integer value representing the hour of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="minute">An integer value representing the minute of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="second">An integer value representing the second of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="bilisecond">An integer value representing the bilisecond (billionth of a second) of the new <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D1E RID: 3358 RVA: 0x00036A24 File Offset: 0x00034C24
		public SqlDateTime(int year, int month, int day, int hour, int minute, int second, int bilisecond)
		{
			try
			{
				long num = (long)(bilisecond * 10);
				long num2 = SqlDateTime.SQLTicksToMilliseconds(SqlDateTime.TimeSpanTicksToSQLTicks(num));
				DateTime dateTime = new DateTime(year, month, day, hour, minute, second);
				this.value = dateTime.AddMilliseconds((double)num2);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw new SqlTypeException(ex.Message);
			}
			this.notNull = true;
			SqlDateTime.CheckRange(this);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00036AA8 File Offset: 0x00034CA8
		static SqlDateTime()
		{
			DateTime dateTime = new DateTime(9999, 12, 31, 23, 59, 59);
			long num = dateTime.Ticks + 9970000L;
			SqlDateTime.MaxValue.value = new DateTime(num);
			SqlDateTime.MaxValue.notNull = true;
			SqlDateTime.MinValue.value = new DateTime(1753, 1, 1);
			SqlDateTime.MinValue.notNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06000D20 RID: 3360 RVA: 0x00036B48 File Offset: 0x00034D48
		[MonoTODO]
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06000D21 RID: 3361 RVA: 0x00036B50 File Offset: 0x00034D50
		[MonoTODO]
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x06000D22 RID: 3362 RVA: 0x00036B58 File Offset: 0x00034D58
		[MonoTODO]
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00036B60 File Offset: 0x00034D60
		private static int TimeSpanTicksToSQLTicks(long ticks)
		{
			return (int)(ticks * (long)SqlDateTime.SQLTicksPerSecond / 10000000L);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00036B74 File Offset: 0x00034D74
		private static long SQLTicksToMilliseconds(int timeTicks)
		{
			return (long)((double)timeTicks * 1000.0 / (double)SqlDateTime.SQLTicksPerSecond + 0.5);
		}

		/// <summary>Gets the number of ticks representing the date of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>The number of ticks representing the date that is contained in the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The exception that is thrown when the Value property of a <see cref="N:System.Data.SqlTypes" /> structure is set to null.</exception>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x00036B94 File Offset: 0x00034D94
		public int DayTicks
		{
			get
			{
				return (this.Value - SqlDateTime.zero_day).Days;
			}
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false. </returns>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00036BBC File Offset: 0x00034DBC
		public bool IsNull
		{
			get
			{
				return !this.notNull;
			}
		}

		/// <summary>Gets the number of ticks representing the time of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>The number of ticks representing the time of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00036BC8 File Offset: 0x00034DC8
		public int TimeTicks
		{
			get
			{
				return SqlDateTime.TimeSpanTicksToSQLTicks(this.Value.TimeOfDay.Ticks);
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. This property is read-only.</summary>
		/// <returns>The value of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The exception that is thrown when the Value property of a <see cref="N:System.Data.SqlTypes" /> structure is set to null.</exception>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00036BF0 File Offset: 0x00034DF0
		public DateTime Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException("The property contains Null.");
				}
				return this.value;
			}
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00036C10 File Offset: 0x00034E10
		private static void CheckRange(SqlDateTime target)
		{
			if (target.IsNull)
			{
				return;
			}
			if (target.value > SqlDateTime.MaxValue.value || target.value < SqlDateTime.MinValue.value)
			{
				throw new SqlTypeException(string.Format("SqlDateTime overflow. Must be between {0} and {1}. Value was {2}", SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value, target.value));
			}
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing as Visual Basic). </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000D2A RID: 3370 RVA: 0x00036CA8 File Offset: 0x00034EA8
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is SqlDateTime))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Data.SqlTypes.SqlDateTime"));
			}
			return this.CompareTo((SqlDateTime)value);
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to the supplied <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than <see cref="T:System.Data.SqlTypes.SqlDateTime" />. Zero This instance is the same as <see cref="T:System.Data.SqlTypes.SqlDateTime" />. Greater than zero This instance is greater than <see cref="T:System.Data.SqlTypes.SqlDateTime" />-or- <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to be compared.</param>
		// Token: 0x06000D2B RID: 3371 RVA: 0x00036CDC File Offset: 0x00034EDC
		public int CompareTo(SqlDateTime value)
		{
			if (value.IsNull)
			{
				return 1;
			}
			return this.value.CompareTo(value.Value);
		}

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> object.</summary>
		/// <returns>true if the object is an instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06000D2C RID: 3372 RVA: 0x00036D0C File Offset: 0x00034F0C
		public override bool Equals(object value)
		{
			if (!(value is SqlDateTime))
			{
				return false;
			}
			if (this.IsNull)
			{
				return ((SqlDateTime)value).IsNull;
			}
			return !((SqlDateTime)value).IsNull && (bool)(this == (SqlDateTime)value);
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structures to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D2D RID: 3373 RVA: 0x00036D6C File Offset: 0x00034F6C
		public static SqlBoolean Equals(SqlDateTime x, SqlDateTime y)
		{
			return x == y;
		}

		/// <summary>Gets the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000D2E RID: 3374 RVA: 0x00036D78 File Offset: 0x00034F78
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		/// <summary>Adds a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to the specified TimeSpan.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</param>
		/// <param name="t">A Timespan value.</param>
		// Token: 0x06000D2F RID: 3375 RVA: 0x00036D88 File Offset: 0x00034F88
		public static SqlDateTime Add(SqlDateTime x, TimeSpan t)
		{
			return x + t;
		}

		/// <summary>Subtracts the specified Timespan from this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> instance.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value.</param>
		/// <param name="t">A Timespan value.</param>
		// Token: 0x06000D30 RID: 3376 RVA: 0x00036D94 File Offset: 0x00034F94
		public static SqlDateTime Subtract(SqlDateTime x, TimeSpan t)
		{
			return x - t;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D31 RID: 3377 RVA: 0x00036DA0 File Offset: 0x00034FA0
		public static SqlBoolean GreaterThan(SqlDateTime x, SqlDateTime y)
		{
			return x > y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D32 RID: 3378 RVA: 0x00036DAC File Offset: 0x00034FAC
		public static SqlBoolean GreaterThanOrEqual(SqlDateTime x, SqlDateTime y)
		{
			return x >= y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D33 RID: 3379 RVA: 0x00036DB8 File Offset: 0x00034FB8
		public static SqlBoolean LessThan(SqlDateTime x, SqlDateTime y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D34 RID: 3380 RVA: 0x00036DC4 File Offset: 0x00034FC4
		public static SqlBoolean LessThanOrEqual(SqlDateTime x, SqlDateTime y)
		{
			return x <= y;
		}

		/// <summary>Performs a logical comparison of two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D35 RID: 3381 RVA: 0x00036DD0 File Offset: 0x00034FD0
		public static SqlBoolean NotEquals(SqlDateTime x, SqlDateTime y)
		{
			return x != y;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> representation of a date and time to its <see cref="T:System.Data.SqlTypes.SqlDateTime" /> equivalent.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure equal to the date and time represented by the specified string.</returns>
		/// <param name="s">The string to be parsed. </param>
		// Token: 0x06000D36 RID: 3382 RVA: 0x00036DDC File Offset: 0x00034FDC
		public static SqlDateTime Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("Argument cannot be null");
			}
			DateTimeFormatInfo currentInfo = DateTimeFormatInfo.CurrentInfo;
			try
			{
				return new SqlDateTime(DateTime.Parse(s, currentInfo));
			}
			catch (Exception)
			{
			}
			try
			{
				return new SqlDateTime(DateTime.Parse(s, CultureInfo.InvariantCulture));
			}
			catch (Exception)
			{
			}
			throw new FormatException(string.Format("String {0} is not recognized as valid DateTime.", s));
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A SqlString structure whose value is a string representing the date and time that is contained in this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000D37 RID: 3383 RVA: 0x00036E84 File Offset: 0x00035084
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to a <see cref="T:System.String" />.</summary>
		/// <returns>A String representing the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of this <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000D38 RID: 3384 RVA: 0x00036E94 File Offset: 0x00035094
		public override string ToString()
		{
			if (this.IsNull)
			{
				return "Null";
			}
			return this.value.ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000D39 RID: 3385 RVA: 0x00036EB8 File Offset: 0x000350B8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("dateTime", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Adds the period of time indicated by the supplied <see cref="T:System.TimeSpan" /> parameter, <paramref name="t" />, to the supplied <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDateTime" />. If either argument is <see cref="F:System.Data.SqlTypes.SqlDateTime.Null" />, the new <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> is <see cref="F:System.Data.SqlTypes.SqlDateTime.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="t">A <see cref="T:System.TimeSpan" /> structure. </param>
		// Token: 0x06000D3A RID: 3386 RVA: 0x00036ED8 File Offset: 0x000350D8
		public static SqlDateTime operator +(SqlDateTime x, TimeSpan t)
		{
			if (x.IsNull)
			{
				return SqlDateTime.Null;
			}
			return new SqlDateTime(x.Value + t);
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structures to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D3B RID: 3387 RVA: 0x00036F0C File Offset: 0x0003510C
		public static SqlBoolean operator ==(SqlDateTime x, SqlDateTime y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.Value == y.Value);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D3C RID: 3388 RVA: 0x00036F50 File Offset: 0x00035150
		public static SqlBoolean operator >(SqlDateTime x, SqlDateTime y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.Value > y.Value);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D3D RID: 3389 RVA: 0x00036F94 File Offset: 0x00035194
		public static SqlBoolean operator >=(SqlDateTime x, SqlDateTime y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.Value >= y.Value);
		}

		/// <summary>Performs a logical comparison of two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D3E RID: 3390 RVA: 0x00036FD8 File Offset: 0x000351D8
		public static SqlBoolean operator !=(SqlDateTime x, SqlDateTime y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(!(x.Value == y.Value));
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D3F RID: 3391 RVA: 0x00037020 File Offset: 0x00035220
		public static SqlBoolean operator <(SqlDateTime x, SqlDateTime y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.Value < y.Value);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlDateTime" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		// Token: 0x06000D40 RID: 3392 RVA: 0x00037064 File Offset: 0x00035264
		public static SqlBoolean operator <=(SqlDateTime x, SqlDateTime y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.Value <= y.Value);
		}

		/// <summary>Subtracts the supplied <see cref="T:System.TimeSpan" /> structure, <paramref name="t" />, from the supplied <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure representing the results of the subtraction.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure. </param>
		/// <param name="t">A <see cref="T:System.TimeSpan" /> structure. </param>
		// Token: 0x06000D41 RID: 3393 RVA: 0x000370A8 File Offset: 0x000352A8
		public static SqlDateTime operator -(SqlDateTime x, TimeSpan t)
		{
			if (x.IsNull)
			{
				return x;
			}
			return new SqlDateTime(x.Value - t);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to a <see cref="T:System.DateTime" /> structure.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object whose <see cref="P:System.DateTime.Date" /> and <see cref="P:System.DateTime.TimeOfDay" /> properties contain the same date and time values as the <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> property of the supplied <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</param>
		// Token: 0x06000D42 RID: 3394 RVA: 0x000370D8 File Offset: 0x000352D8
		public static explicit operator DateTime(SqlDateTime x)
		{
			return x.Value;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> is equal to the date and time represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter. If the <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the Value of the newly created <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />.</param>
		// Token: 0x06000D43 RID: 3395 RVA: 0x000370E4 File Offset: 0x000352E4
		public static explicit operator SqlDateTime(SqlString x)
		{
			return SqlDateTime.Parse(x.Value);
		}

		/// <summary>Converts a <see cref="T:System.DateTime" /> structure to a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure whose <see cref="P:System.Data.SqlTypes.SqlDateTime.Value" /> is equal to the combined <see cref="P:System.DateTime.Date" /> and <see cref="P:System.DateTime.TimeOfDay" /> properties of the supplied <see cref="T:System.DateTime" /> structure.</returns>
		/// <param name="value">A DateTime structure. </param>
		// Token: 0x06000D44 RID: 3396 RVA: 0x000370F4 File Offset: 0x000352F4
		public static implicit operator SqlDateTime(DateTime value)
		{
			return new SqlDateTime(value);
		}

		// Token: 0x040004E5 RID: 1253
		private DateTime value;

		// Token: 0x040004E6 RID: 1254
		private bool notNull;

		/// <summary>Represents the maximum valid date value for a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		// Token: 0x040004E7 RID: 1255
		public static readonly SqlDateTime MaxValue;

		/// <summary>Represents the minimum valid date value for a <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		// Token: 0x040004E8 RID: 1256
		public static readonly SqlDateTime MinValue;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure.</summary>
		// Token: 0x040004E9 RID: 1257
		public static readonly SqlDateTime Null;

		/// <summary>A constant whose value is the number of ticks equivalent to one hour.</summary>
		// Token: 0x040004EA RID: 1258
		public static readonly int SQLTicksPerHour = 1080000;

		/// <summary>A constant whose value is the number of ticks equivalent to one minute.</summary>
		// Token: 0x040004EB RID: 1259
		public static readonly int SQLTicksPerMinute = 18000;

		/// <summary>A constant whose value is the number of ticks equivalent to one second.</summary>
		// Token: 0x040004EC RID: 1260
		public static readonly int SQLTicksPerSecond = 300;

		// Token: 0x040004ED RID: 1261
		private static readonly DateTime zero_day = new DateTime(1900, 1, 1);
	}
}
