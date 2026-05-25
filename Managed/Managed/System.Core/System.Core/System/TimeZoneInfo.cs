using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
	/// <summary>Represents any time zone in the world.</summary>
	// Token: 0x02000012 RID: 18
	[Serializable]
	public sealed class TimeZoneInfo : ISerializable, IDeserializationCallback, IEquatable<TimeZoneInfo>
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00006810 File Offset: 0x00004A10
		private TimeZoneInfo(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id == string.Empty)
			{
				throw new ArgumentException("id parameter is an empty string");
			}
			if (baseUtcOffset.Ticks % 600000000L != 0L)
			{
				throw new ArgumentException("baseUtcOffset parameter does not represent a whole number of minutes");
			}
			if (baseUtcOffset > new TimeSpan(14, 0, 0) || baseUtcOffset < new TimeSpan(-14, 0, 0))
			{
				throw new ArgumentOutOfRangeException("baseUtcOffset parameter is greater than 14 hours or less than -14 hours");
			}
			if (adjustmentRules != null && adjustmentRules.Length != 0)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = null;
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule2 in adjustmentRules)
				{
					if (adjustmentRule2 == null)
					{
						throw new InvalidTimeZoneException("one or more elements in adjustmentRules are null");
					}
					if (baseUtcOffset + adjustmentRule2.DaylightDelta < new TimeSpan(-14, 0, 0) || baseUtcOffset + adjustmentRule2.DaylightDelta > new TimeSpan(14, 0, 0))
					{
						throw new InvalidTimeZoneException("Sum of baseUtcOffset and DaylightDelta of one or more object in adjustmentRules array is greater than 14 or less than -14 hours;");
					}
					if (adjustmentRule != null && adjustmentRule.DateStart > adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("adjustment rules specified in adjustmentRules parameter are not in chronological order");
					}
					if (adjustmentRule != null && adjustmentRule.DateEnd > adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("some adjustment rules in the adjustmentRules parameter overlap");
					}
					if (adjustmentRule != null && adjustmentRule.DateEnd == adjustmentRule2.DateStart)
					{
						throw new InvalidTimeZoneException("a date can have multiple adjustment rules applied to it");
					}
					adjustmentRule = adjustmentRule2;
				}
			}
			this.id = id;
			this.baseUtcOffset = baseUtcOffset;
			this.displayName = displayName ?? id;
			this.standardDisplayName = standardDisplayName ?? id;
			this.daylightDisplayName = daylightDisplayName;
			this.disableDaylightSavingTime = disableDaylightSavingTime;
			this.adjustmentRules = adjustmentRules;
		}

		/// <summary>Gets the time difference between the current time zone's standard time and Coordinated Universal Time (UTC).</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object that indicates the time difference between the current time zone's standard time and Coordinated Universal Time (UTC).</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000069E8 File Offset: 0x00004BE8
		public TimeSpan BaseUtcOffset
		{
			get
			{
				return this.baseUtcOffset;
			}
		}

		/// <summary>Gets the localized display name for the current time zone's daylight saving time.</summary>
		/// <returns>The display name for the time zone's localized daylight saving time.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000069F0 File Offset: 0x00004BF0
		public string DaylightName
		{
			get
			{
				if (this.disableDaylightSavingTime)
				{
					return string.Empty;
				}
				return this.daylightDisplayName;
			}
		}

		/// <summary>Gets the localized general display name that represents the time zone.</summary>
		/// <returns>The time zone's localized general display name.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00006A0C File Offset: 0x00004C0C
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		/// <summary>Gets the time zone identifier.</summary>
		/// <returns>The time zone identifier.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00006A14 File Offset: 0x00004C14
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Gets a <see cref="T:System.TimeZoneInfo" /> object that represents the local time zone.</summary>
		/// <returns>A <see cref="T:System.TimeZoneInfo" /> object that represents the local time zone.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006A1C File Offset: 0x00004C1C
		public static TimeZoneInfo Local
		{
			get
			{
				if (TimeZoneInfo.local == null)
				{
					try
					{
						TimeZoneInfo.local = TimeZoneInfo.FindSystemTimeZoneByFileName("Local", "/etc/localtime");
					}
					catch
					{
						try
						{
							TimeZoneInfo.local = TimeZoneInfo.FindSystemTimeZoneByFileName("Local", Path.Combine(TimeZoneInfo.TimeZoneDirectory, "localtime"));
						}
						catch
						{
							throw new TimeZoneNotFoundException();
						}
					}
				}
				return TimeZoneInfo.local;
			}
		}

		/// <summary>Gets the localized display name for the time zone's standard time.</summary>
		/// <returns>The localized display name of the time zone's standard time.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006ABC File Offset: 0x00004CBC
		public string StandardName
		{
			get
			{
				return this.standardDisplayName;
			}
		}

		/// <summary>Gets a value indicating whether the time zone has any daylight saving time rules.</summary>
		/// <returns>true if the time zone supports daylight saving time; otherwise, false.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006AC4 File Offset: 0x00004CC4
		public bool SupportsDaylightSavingTime
		{
			get
			{
				return !this.disableDaylightSavingTime;
			}
		}

		/// <summary>Gets a <see cref="T:System.TimeZoneInfo" /> object that represents the Coordinated Universal Time (UTC) zone.</summary>
		/// <returns>A <see cref="T:System.TimeZoneInfo" /> object that represents the Coordinated Universal Time (UTC) zone.</returns>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public static TimeZoneInfo Utc
		{
			get
			{
				if (TimeZoneInfo.utc == null)
				{
					TimeZoneInfo.utc = TimeZoneInfo.CreateCustomTimeZone("UTC", new TimeSpan(0L), "UTC", "UTC");
				}
				return TimeZoneInfo.utc;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006B04 File Offset: 0x00004D04
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00006B20 File Offset: 0x00004D20
		private static string TimeZoneDirectory
		{
			get
			{
				if (TimeZoneInfo.timeZoneDirectory == null)
				{
					TimeZoneInfo.timeZoneDirectory = "/usr/share/zoneinfo";
				}
				return TimeZoneInfo.timeZoneDirectory;
			}
			set
			{
				TimeZoneInfo.ClearCachedData();
				TimeZoneInfo.timeZoneDirectory = value;
			}
		}

		/// <summary>Clears cached time zone data.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600010B RID: 267 RVA: 0x00006B30 File Offset: 0x00004D30
		public static void ClearCachedData()
		{
			TimeZoneInfo.local = null;
			TimeZoneInfo.utc = null;
			TimeZoneInfo.systemTimeZones = null;
		}

		/// <summary>Converts a time to the time in a particular time zone.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that represents the date and time in the destination time zone.</returns>
		/// <param name="dateTime">The date and time to convert.   </param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="dateTime" /> parameter represents an invalid time.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="destinationTimeZone" /> parameter is null.</exception>
		// Token: 0x0600010C RID: 268 RVA: 0x00006B44 File Offset: 0x00004D44
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, destinationTimeZone);
		}

		/// <summary>Converts a time from one time zone to another.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that represents the date and time in the destination time zone that corresponds to the <paramref name="dateTime" /> parameter in the source time zone.</returns>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="sourceTimeZone">The time zone of <paramref name="dateTime" />.</param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> parameter is <see cref="F:System.DateTimeKind.Local" />, but the <paramref name="sourceTimeZone" /> parameter does not equal <see cref="F:System.DateTimeKind.Local" />.-or-The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> parameter is <see cref="F:System.DateTimeKind.Utc" />, but the <paramref name="sourceTimeZone" /> parameter does not equal <see cref="P:System.TimeZoneInfo.Utc" />.-or-The <paramref name="dateTime" /> parameter is an invalid time (that is, it represents a time that does not exist because of a time zone's adjustment rules).</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceTimeZone" /> parameter is null.-or-The <paramref name="destinationTimeZone" /> parameter is null.</exception>
		// Token: 0x0600010D RID: 269 RVA: 0x00006B54 File Offset: 0x00004D54
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
		{
			if (dateTime.Kind == DateTimeKind.Local && sourceTimeZone != TimeZoneInfo.Local)
			{
				throw new ArgumentException("Kind propery of dateTime is Local but the sourceTimeZone does not equal TimeZoneInfo.Local");
			}
			if (dateTime.Kind == DateTimeKind.Utc && sourceTimeZone != TimeZoneInfo.Utc)
			{
				throw new ArgumentException("Kind propery of dateTime is Utc but the sourceTimeZone does not equal TimeZoneInfo.Utc");
			}
			if (sourceTimeZone.IsInvalidTime(dateTime))
			{
				throw new ArgumentException("dateTime parameter is an invalid time");
			}
			if (sourceTimeZone == null)
			{
				throw new ArgumentNullException("sourceTimeZone");
			}
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			if (dateTime.Kind == DateTimeKind.Local && sourceTimeZone == TimeZoneInfo.Local && destinationTimeZone == TimeZoneInfo.Local)
			{
				return dateTime;
			}
			DateTime dateTime2 = TimeZoneInfo.ConvertTimeToUtc(dateTime);
			if (destinationTimeZone == TimeZoneInfo.Utc)
			{
				return dateTime2;
			}
			return TimeZoneInfo.ConvertTimeFromUtc(dateTime2, destinationTimeZone);
		}

		/// <summary>Converts a time to the time in a particular time zone.</summary>
		/// <returns>A <see cref="T:System.DateTimeOffset" /> value that represents the date and time in the destination time zone.</returns>
		/// <param name="dateTimeOffset">The date and time to convert.   </param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="destinationTimeZone" /> parameter is null.</exception>
		// Token: 0x0600010E RID: 270 RVA: 0x00006C20 File Offset: 0x00004E20
		public static DateTimeOffset ConvertTime(DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts a time to the time in another time zone based on the time zone's identifier.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that represents the date and time in the destination time zone.</returns>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationTimeZoneId" /> is null.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifier was found, but the registry data is corrupted.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The <paramref name="destinationTimeZoneId" /> identifier was not found on the local system.</exception>
		// Token: 0x0600010F RID: 271 RVA: 0x00006C28 File Offset: 0x00004E28
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		/// <summary>Converts a time from one time zone to another based on time zone identifiers.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that represents the date and time in the destination time zone that corresponds to the <paramref name="dateTime" /> parameter in the source time zone.</returns>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="sourceTimeZoneId">The identifier of the source time zone. </param>
		/// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> parameter does not correspond to the source time zone.-or-<paramref name="dateTime" /> is an invalid time in the source time zone.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceTimeZoneId" /> is null.-or-<paramref name="destinationTimeZoneId" /> is null.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifier was found, but the registry data is corrupted.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The <paramref name="sourceTimeZoneId" /> identifier was not found on the local system.-or-The <paramref name="destinationTimeZoneId" /> identifier was not found on the local system.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry keys that hold time zone data.</exception>
		// Token: 0x06000110 RID: 272 RVA: 0x00006C38 File Offset: 0x00004E38
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId), TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		/// <summary>Converts a time to the time in another time zone based on the time zone's identifier.</summary>
		/// <returns>A <see cref="T:System.DateTimeOffset" /> value that represents the date and time in the destination time zone.</returns>
		/// <param name="dateTimeOffset">The date and time to convert.</param>
		/// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationTimeZoneId" /> is null.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifier was found but the registry data is corrupted.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The <paramref name="destinationTimeZoneId" /> identifier was not found on the local system.</exception>
		// Token: 0x06000111 RID: 273 RVA: 0x00006C4C File Offset: 0x00004E4C
		public static DateTimeOffset ConvertTimeBySystemTimeZoneId(DateTimeOffset dateTimeOffset, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTimeOffset, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006C5C File Offset: 0x00004E5C
		private DateTime ConvertTimeFromUtc(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				throw new ArgumentException("Kind property of dateTime is Local");
			}
			if (this == TimeZoneInfo.Utc)
			{
				return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
			}
			if (this == TimeZoneInfo.Local)
			{
				return DateTime.SpecifyKind(dateTime.ToLocalTime(), DateTimeKind.Unspecified);
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
			if (this.IsDaylightSavingTime(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)))
			{
				return DateTime.SpecifyKind(dateTime + this.BaseUtcOffset + applicableRule.DaylightDelta, DateTimeKind.Unspecified);
			}
			return DateTime.SpecifyKind(dateTime + this.BaseUtcOffset, DateTimeKind.Unspecified);
		}

		/// <summary>Converts a Coordinated Universal Time (UTC) to the time in a specified time zone.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that represents the date and time in the destination time zone. Its <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Utc" /> if <paramref name="destinationTimeZone" /> is <see cref="P:System.TimeZoneInfo.Utc" />; otherwise, its <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Unspecified" />.</returns>
		/// <param name="dateTime">The Coordinated Universal Time (UTC).</param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of <paramref name="dateTime" /> is <see cref="F:System.DateTimeKind.Local" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationTimeZone" /> is null.</exception>
		// Token: 0x06000113 RID: 275 RVA: 0x00006CF8 File Offset: 0x00004EF8
		public static DateTime ConvertTimeFromUtc(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			return destinationTimeZone.ConvertTimeFromUtc(dateTime);
		}

		/// <summary>Converts the current date and time to Coordinated Universal Time (UTC).</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that represents the Coordinated Universal Time (UTC) that corresponds to the <paramref name="dateTime" /> parameter. The <see cref="T:System.DateTime" /> value's <see cref="P:System.DateTime.Kind" /> property is always set to <see cref="F:System.DateTimeKind.Utc" />.</returns>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <exception cref="T:System.ArgumentException">TimeZoneInfo.Local.IsInvalidDateTime(<paramref name="dateTime" />) returns true.</exception>
		// Token: 0x06000114 RID: 276 RVA: 0x00006D14 File Offset: 0x00004F14
		public static DateTime ConvertTimeToUtc(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			return DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
		}

		/// <summary>Converts the time in a specified time zone to Coordinated Universal Time (UTC).</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the Coordinated Universal Time (UTC) that corresponds to the <paramref name="dateTime" /> parameter. The <see cref="T:System.DateTime" /> object's <see cref="P:System.DateTime.Kind" /> property is always set to <see cref="F:System.DateTimeKind.Utc" />.</returns>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="sourceTimeZone">The time zone of <paramref name="dateTime" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTime" />.Kind is <see cref="F:System.DateTimeKind.Utc" /> and <paramref name="sourceTimeZone" /> does not equal <see cref="P:System.TimeZoneInfo.Utc" />.-or-<paramref name="dateTime" />.Kind is <see cref="F:System.DateTimeKind.Local" /> and <paramref name="sourceTimeZone" /> does not equal <see cref="P:System.TimeZoneInfo.Local" />.-or-<paramref name="sourceTimeZone" />.IsInvalidDateTime(<paramref name="dateTime" />) returns true.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceTimeZone" /> is null.</exception>
		// Token: 0x06000115 RID: 277 RVA: 0x00006D34 File Offset: 0x00004F34
		public static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone)
		{
			if (sourceTimeZone == null)
			{
				throw new ArgumentNullException("sourceTimeZone");
			}
			if (dateTime.Kind == DateTimeKind.Utc && sourceTimeZone != TimeZoneInfo.Utc)
			{
				throw new ArgumentException("Kind propery of dateTime is Utc but the sourceTimeZone does not equal TimeZoneInfo.Utc");
			}
			if (dateTime.Kind == DateTimeKind.Local && sourceTimeZone != TimeZoneInfo.Local)
			{
				throw new ArgumentException("Kind propery of dateTime is Local but the sourceTimeZone does not equal TimeZoneInfo.Local");
			}
			if (sourceTimeZone.IsInvalidTime(dateTime))
			{
				throw new ArgumentException("dateTime parameter is an invalid time");
			}
			if (dateTime.Kind == DateTimeKind.Utc && sourceTimeZone == TimeZoneInfo.Utc)
			{
				return dateTime;
			}
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			if (dateTime.Kind == DateTimeKind.Local)
			{
				return TimeZoneInfo.ConvertTimeToUtc(dateTime);
			}
			if (sourceTimeZone.IsAmbiguousTime(dateTime) || !sourceTimeZone.IsDaylightSavingTime(dateTime))
			{
				return DateTime.SpecifyKind(dateTime - sourceTimeZone.BaseUtcOffset, DateTimeKind.Utc);
			}
			TimeZoneInfo.AdjustmentRule applicableRule = sourceTimeZone.GetApplicableRule(dateTime);
			return DateTime.SpecifyKind(dateTime - sourceTimeZone.BaseUtcOffset - applicableRule.DaylightDelta, DateTimeKind.Utc);
		}

		/// <summary>Creates a custom time zone with a specified identifier, an offset from Coordinated Universal Time (UTC), a display name, and a standard time display name.</summary>
		/// <returns>A <see cref="T:System.TimeZoneInfo" /> object that represents the new time zone.</returns>
		/// <param name="id">The time zone's identifier.</param>
		/// <param name="baseUtcOffset">A <see cref="T:System.TimeSpan" /> object that represents the time difference between this time zone and Coordinated Universal Time (UTC).</param>
		/// <param name="displayName">The display name of the new time zone.   </param>
		/// <param name="standardDisplayName">The name of the new time zone's standard time.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="id" /> parameter is an empty string ("").-or-The <paramref name="baseUtcOffset" /> parameter does not represent a whole number of minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="baseUtcOffset" /> parameter is greater than 14 hours or less than -14 hours.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000116 RID: 278 RVA: 0x00006E3C File Offset: 0x0000503C
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName)
		{
			return TimeZoneInfo.CreateCustomTimeZone(id, baseUtcOffset, displayName, standardDisplayName, null, null, true);
		}

		/// <summary>Creates a custom time zone with a specified identifier, an offset from Coordinated Universal Time (UTC), a display name, a standard time name, a daylight saving time name, and daylight saving time rules.</summary>
		/// <returns>A <see cref="T:System.TimeZoneInfo" /> object that represents the new time zone.</returns>
		/// <param name="id">The time zone's identifier.</param>
		/// <param name="baseUtcOffset">A <see cref="T:System.TimeSpan" /> object that represents the time difference between this time zone and Coordinated Universal Time (UTC).</param>
		/// <param name="displayName">The display name of the new time zone.   </param>
		/// <param name="standardDisplayName">The new time zone's standard time name.</param>
		/// <param name="daylightDisplayName">The daylight saving time name of the new time zone.   </param>
		/// <param name="adjustmentRules">An array of <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects that augment the base UTC offset for a particular period. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="id" /> parameter is an empty string ("").-or-The <paramref name="baseUtcOffset" /> parameter does not represent a whole number of minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="baseUtcOffset" /> parameter is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter overlap.-or-The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter are not in chronological order.-or-One or more elements in <paramref name="adjustmentRules" /> are null.-or-A date can have multiple adjustment rules applied to it.-or-The sum of the <paramref name="baseUtcOffset" /> parameter and the <see cref="P:System.TimeZoneInfo.AdjustmentRule.DaylightDelta" /> value of one or more objects in the <paramref name="adjustmentRules" /> array is greater than 14 hours or less than -14 hours.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000117 RID: 279 RVA: 0x00006E4C File Offset: 0x0000504C
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules)
		{
			return TimeZoneInfo.CreateCustomTimeZone(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
		}

		/// <summary>Creates a custom time zone with a specified identifier, an offset from Coordinated Universal Time (UTC), a display name, a standard time name, a daylight saving time name, daylight saving time rules, and a value that indicates whether the returned object reflects daylight saving time information.</summary>
		/// <returns>A <see cref="T:System.TimeZoneInfo" /> object that represents the new time zone. If the <paramref name="disableDaylightSavingTime" /> parameter is true, the returned object has no daylight saving time data.</returns>
		/// <param name="id">The time zone's identifier.</param>
		/// <param name="baseUtcOffset">A <see cref="T:System.TimeSpan" /> object that represents the time difference between this time zone and Coordinated Universal Time (UTC).</param>
		/// <param name="displayName">The display name of the new time zone.   </param>
		/// <param name="standardDisplayName">The standard time name of the new time zone.</param>
		/// <param name="daylightDisplayName">The daylight saving time name of the new time zone.   </param>
		/// <param name="adjustmentRules">An array of <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects that augment the base UTC offset for a particular period.</param>
		/// <param name="disableDaylightSavingTime">true to discard any daylight saving time-related information present in <paramref name="adjustmentRules" /> with the new object; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="id" /> parameter is an empty string ("").-or-The <paramref name="baseUtcOffset" /> parameter does not represent a whole number of minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="baseUtcOffset" /> parameter is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter overlap.-or-The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter are not in chronological order.-or-One or more elements in <paramref name="adjustmentRules" /> are null.-or-A date can have multiple adjustment rules applied to it.-or-The sum of the <paramref name="baseUtcOffset" /> parameter and the <see cref="P:System.TimeZoneInfo.AdjustmentRule.DaylightDelta" /> value of one or more objects in the <paramref name="adjustmentRules" /> array is greater than 14 hours or less than -14 hours.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000118 RID: 280 RVA: 0x00006E5C File Offset: 0x0000505C
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, disableDaylightSavingTime);
		}

		/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo" /> object and another <see cref="T:System.TimeZoneInfo" /> object are equal.</summary>
		/// <returns>true if the two <see cref="T:System.TimeZoneInfo" /> objects are equal; otherwise, false.</returns>
		/// <param name="other">A second <see cref="T:System.TimeZoneInfo" /> object to compare with the current <see cref="T:System.TimeZoneInfo" /> object.  </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000119 RID: 281 RVA: 0x00006E70 File Offset: 0x00005070
		public bool Equals(TimeZoneInfo other)
		{
			return other != null && other.Id == this.Id && this.HasSameRules(other);
		}

		/// <summary>Retrieves a <see cref="T:System.TimeZoneInfo" /> object from the registry based on its identifier.</summary>
		/// <returns>A <see cref="T:System.TimeZoneInfo" /> object whose identifier is the value of the <paramref name="id" /> parameter.</returns>
		/// <param name="id">The time zone identifier, which corresponds to the <see cref="P:System.TimeZoneInfo.Id" /> property.      </param>
		/// <exception cref="T:System.OutOfMemoryException">The system does not have enough memory to hold information about the time zone.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is null.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The time zone identifier specified by <paramref name="id" /> was not found. This means that a registry key whose name matches <paramref name="id" /> does not exist, or that the key exists but does not contain any time zone data.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifier was found, but the registry data is corrupted.</exception>
		// Token: 0x0600011A RID: 282 RVA: 0x00006EA8 File Offset: 0x000050A8
		public static TimeZoneInfo FindSystemTimeZoneById(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			string text = Path.Combine(TimeZoneInfo.TimeZoneDirectory, id);
			return TimeZoneInfo.FindSystemTimeZoneByFileName(id, text);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006EDC File Offset: 0x000050DC
		private static TimeZoneInfo FindSystemTimeZoneByFileName(string id, string filepath)
		{
			if (!File.Exists(filepath))
			{
				throw new TimeZoneNotFoundException();
			}
			byte[] array = new byte[16384];
			int num;
			using (FileStream fileStream = File.OpenRead(filepath))
			{
				num = fileStream.Read(array, 0, 16384);
			}
			if (!TimeZoneInfo.ValidTZFile(array, num))
			{
				throw new InvalidTimeZoneException("TZ file too big for the buffer");
			}
			TimeZoneInfo timeZoneInfo;
			try
			{
				timeZoneInfo = TimeZoneInfo.ParseTZBuffer(id, array, num);
			}
			catch (Exception ex)
			{
				throw new InvalidTimeZoneException(ex.Message);
			}
			return timeZoneInfo;
		}

		/// <summary>Deserializes a string to re-create an original serialized <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <returns>A <see cref="T:System.TimeZoneInfo" /> object.</returns>
		/// <param name="source">The string representation of the serialized <see cref="T:System.TimeZoneInfo" /> object.   </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> parameter is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> parameter is a null string.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The source parameter cannot be deserialized back into a <see cref="T:System.TimeZoneInfo" /> object.</exception>
		// Token: 0x0600011C RID: 284 RVA: 0x00006FA0 File Offset: 0x000051A0
		public static TimeZoneInfo FromSerializedString(string source)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves an array of <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects that apply to the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <returns>An array of <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects for this time zone.</returns>
		/// <exception cref="T:System.OutOfMemoryException">The system does not have enough memory to make an in-memory copy of the adjustment rules.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600011D RID: 285 RVA: 0x00006FA8 File Offset: 0x000051A8
		public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
		{
			if (this.disableDaylightSavingTime)
			{
				return new TimeZoneInfo.AdjustmentRule[0];
			}
			return (TimeZoneInfo.AdjustmentRule[])this.adjustmentRules.Clone();
		}

		/// <summary>Returns information about the possible dates and times that an ambiguous date and time can be mapped to.</summary>
		/// <returns>An array of <see cref="T:System.TimeSpan" /> objects that represents possible Coordinated Universal Time (UTC) offsets that a particular date and time can be mapped to.</returns>
		/// <param name="dateTime">A date and time.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTime" /> is not an ambiguous time.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600011E RID: 286 RVA: 0x00006FD8 File Offset: 0x000051D8
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTime dateTime)
		{
			if (!this.IsAmbiguousTime(dateTime))
			{
				throw new ArgumentException("dateTime is not an ambiguous time");
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
			return new TimeSpan[]
			{
				this.baseUtcOffset,
				this.baseUtcOffset + applicableRule.DaylightDelta
			};
		}

		/// <summary>Returns information about the possible dates and times that an ambiguous date and time can be mapped to.</summary>
		/// <returns>An array of <see cref="T:System.TimeSpan" /> objects that represents possible Coordinated Universal Time (UTC) offsets that a particular date and time can be mapped to.</returns>
		/// <param name="dateTimeOffset">A date and time.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTime" /> is not an ambiguous time.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600011F RID: 287 RVA: 0x0000703C File Offset: 0x0000523C
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTimeOffset dateTimeOffset)
		{
			if (!this.IsAmbiguousTime(dateTimeOffset))
			{
				throw new ArgumentException("dateTimeOffset is not an ambiguous time");
			}
			throw new NotImplementedException();
		}

		/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
		/// <returns>A 32-bit signed integer that serves as the hash code for this <see cref="T:System.TimeZoneInfo" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000120 RID: 288 RVA: 0x0000705C File Offset: 0x0000525C
		public override int GetHashCode()
		{
			int num = this.Id.GetHashCode();
			foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in this.GetAdjustmentRules())
			{
				num ^= adjustmentRule.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000070A0 File Offset: 0x000052A0
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a sorted collection of all the time zones about which information is available on the local system.</summary>
		/// <returns>A read-only collection of <see cref="T:System.TimeZoneInfo" /> objects.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to store all time zone information.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to read from the registry keys that contain time zone information.</exception>
		// Token: 0x06000122 RID: 290 RVA: 0x000070A8 File Offset: 0x000052A8
		public static ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
		{
			if (TimeZoneInfo.systemTimeZones == null)
			{
				TimeZoneInfo.systemTimeZones = new List<TimeZoneInfo>();
				string[] array = new string[]
				{
					"Africa", "America", "Antarctica", "Arctic", "Asia", "Atlantic", "Brazil", "Canada", "Chile", "Europe",
					"Indian", "Mexico", "Mideast", "Pacific", "US"
				};
				foreach (string text in array)
				{
					try
					{
						foreach (string text2 in Directory.GetFiles(Path.Combine(TimeZoneInfo.TimeZoneDirectory, text)))
						{
							try
							{
								string text3 = string.Format("{0}/{1}", text, Path.GetFileName(text2));
								TimeZoneInfo.systemTimeZones.Add(TimeZoneInfo.FindSystemTimeZoneById(text3));
							}
							catch (ArgumentNullException)
							{
							}
							catch (TimeZoneNotFoundException)
							{
							}
							catch (InvalidTimeZoneException)
							{
							}
							catch (Exception)
							{
								throw;
							}
						}
					}
					catch
					{
					}
				}
			}
			return new ReadOnlyCollection<TimeZoneInfo>(TimeZoneInfo.systemTimeZones);
		}

		/// <summary>Calculates the offset or difference between the time in this time zone and Coordinated Universal Time (UTC) for a particular date and time.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object that indicates the time difference between the two time zones.</returns>
		/// <param name="dateTime">The date and time to determine the offset for.   </param>
		// Token: 0x06000123 RID: 291 RVA: 0x00007270 File Offset: 0x00005470
		public TimeSpan GetUtcOffset(DateTime dateTime)
		{
			if (this.IsDaylightSavingTime(dateTime))
			{
				TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
				return this.BaseUtcOffset + applicableRule.DaylightDelta;
			}
			return this.BaseUtcOffset;
		}

		/// <summary>Calculates the offset or difference between the time in this time zone and Coordinated Universal Time (UTC) for a particular date and time.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object that indicates the time difference between Coordinated Universal Time (UTC) and the current time zone.</returns>
		/// <param name="dateTimeOffset">The date and time to determine the offset for.</param>
		// Token: 0x06000124 RID: 292 RVA: 0x000072AC File Offset: 0x000054AC
		public TimeSpan GetUtcOffset(DateTimeOffset dateTimeOffset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether the current object and another <see cref="T:System.TimeZoneInfo" /> object have the same adjustment rules.</summary>
		/// <returns>true if the two time zones have identical adjustment rules and an identical base offset; otherwise, false.</returns>
		/// <param name="other">A second <see cref="T:System.TimeZoneInfo" /> object to compare with the current <see cref="T:System.TimeZoneInfo" /> object.   </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="other" /> parameter is null.</exception>
		// Token: 0x06000125 RID: 293 RVA: 0x000072B4 File Offset: 0x000054B4
		public bool HasSameRules(TimeZoneInfo other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.adjustmentRules == null != (other.adjustmentRules == null))
			{
				return false;
			}
			if (this.adjustmentRules == null)
			{
				return true;
			}
			if (this.BaseUtcOffset != other.BaseUtcOffset)
			{
				return false;
			}
			if (this.adjustmentRules.Length != other.adjustmentRules.Length)
			{
				return false;
			}
			for (int i = 0; i < this.adjustmentRules.Length; i++)
			{
				if (!this.adjustmentRules[i].Equals(other.adjustmentRules[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether a particular date and time in a particular time zone is ambiguous and can be mapped to two or more Coordinated Universal Time (UTC) times.</summary>
		/// <returns>true if the <paramref name="dateTime" /> parameter is ambiguous; otherwise, false.</returns>
		/// <param name="dateTime">A date and time value.   </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> value is <see cref="F:System.DateTimeKind.Local" /> and <paramref name="dateTime" /> is an invalid time.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000126 RID: 294 RVA: 0x00007360 File Offset: 0x00005560
		public bool IsAmbiguousTime(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Local && this.IsInvalidTime(dateTime))
			{
				throw new ArgumentException("Kind is Local and time is Invalid");
			}
			if (this == TimeZoneInfo.Utc)
			{
				return false;
			}
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				dateTime = this.ConvertTimeFromUtc(dateTime);
			}
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Local)
			{
				dateTime = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, this);
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
			DateTime dateTime2 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionEnd, dateTime.Year);
			return dateTime > dateTime2 - applicableRule.DaylightDelta && dateTime <= dateTime2;
		}

		/// <summary>Determines whether a particular date and time in a particular time zone is ambiguous and can be mapped to two or more Coordinated Universal Time (UTC) times.</summary>
		/// <returns>true if the <paramref name="dateTimeOffset" /> parameter is ambiguous in the current time zone; otherwise, false.</returns>
		/// <param name="dateTimeOffset">A date and time.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000127 RID: 295 RVA: 0x0000741C File Offset: 0x0000561C
		public bool IsAmbiguousTime(DateTimeOffset dateTimeOffset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether a specified date and time falls in the range of daylight saving time for the time zone of the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <returns>true if the <paramref name="dateTime" /> parameter is a daylight saving time; otherwise, false.</returns>
		/// <param name="dateTime">A date and time value.   </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> value is <see cref="F:System.DateTimeKind.Local" /> and <paramref name="dateTime" /> is an invalid time.</exception>
		// Token: 0x06000128 RID: 296 RVA: 0x00007424 File Offset: 0x00005624
		public bool IsDaylightSavingTime(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Local && this.IsInvalidTime(dateTime))
			{
				throw new ArgumentException("dateTime is invalid and Kind is Local");
			}
			if (this == TimeZoneInfo.Utc)
			{
				return false;
			}
			if (!this.SupportsDaylightSavingTime)
			{
				return false;
			}
			if ((dateTime.Kind == DateTimeKind.Local || dateTime.Kind == DateTimeKind.Unspecified) && this == TimeZoneInfo.Local)
			{
				return dateTime.IsDaylightSavingTime();
			}
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Utc)
			{
				return this.IsDaylightSavingTime(DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc));
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime.Date);
			if (applicableRule == null)
			{
				return false;
			}
			DateTime dateTime2 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionStart, dateTime.Year);
			DateTime dateTime3 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionEnd, dateTime.Year + ((applicableRule.DaylightTransitionStart.Month >= applicableRule.DaylightTransitionEnd.Month) ? 1 : 0));
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				dateTime2 -= this.BaseUtcOffset;
				dateTime3 -= this.BaseUtcOffset + applicableRule.DaylightDelta;
			}
			return dateTime >= dateTime2 && dateTime < dateTime3;
		}

		/// <summary>Indicates whether a specified date and time falls in the range of daylight saving time for the time zone of the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <returns>true if the <paramref name="dateTimeOffset" /> parameter is a daylight saving time; otherwise, false.</returns>
		/// <param name="dateTimeOffset">A date and time value.</param>
		// Token: 0x06000129 RID: 297 RVA: 0x00007578 File Offset: 0x00005778
		public bool IsDaylightSavingTime(DateTimeOffset dateTimeOffset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether a particular date and time is invalid.</summary>
		/// <returns>true if <paramref name="dateTime" /> is invalid; otherwise, false.</returns>
		/// <param name="dateTime">A date and time value.   </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600012A RID: 298 RVA: 0x00007580 File Offset: 0x00005780
		public bool IsInvalidTime(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return false;
			}
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Local)
			{
				return false;
			}
			TimeZoneInfo.AdjustmentRule applicableRule = this.GetApplicableRule(dateTime);
			DateTime dateTime2 = TimeZoneInfo.TransitionPoint(applicableRule.DaylightTransitionStart, dateTime.Year);
			return dateTime >= dateTime2 && dateTime < dateTime2 + applicableRule.DaylightDelta;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000075F8 File Offset: 0x000057F8
		public void OnDeserialization(object sender)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts the current <see cref="T:System.TimeZoneInfo" /> object to a serialized string.</summary>
		/// <returns>A string that represents the current <see cref="T:System.TimeZoneInfo" /> object.</returns>
		// Token: 0x0600012C RID: 300 RVA: 0x00007600 File Offset: 0x00005800
		public string ToSerializedString()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the current <see cref="T:System.TimeZoneInfo" /> object's display name.</summary>
		/// <returns>The value of the <see cref="P:System.TimeZoneInfo.DisplayName" /> property of the current <see cref="T:System.TimeZoneInfo" /> object.</returns>
		// Token: 0x0600012D RID: 301 RVA: 0x00007608 File Offset: 0x00005808
		public override string ToString()
		{
			return this.DisplayName;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007610 File Offset: 0x00005810
		private TimeZoneInfo.AdjustmentRule GetApplicableRule(DateTime dateTime)
		{
			DateTime dateTime2 = dateTime;
			if (dateTime.Kind == DateTimeKind.Local && this != TimeZoneInfo.Local)
			{
				dateTime2 = dateTime2.ToUniversalTime() + this.BaseUtcOffset;
			}
			if (dateTime.Kind == DateTimeKind.Utc && this != TimeZoneInfo.Utc)
			{
				dateTime2 += this.BaseUtcOffset;
			}
			foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in this.adjustmentRules)
			{
				if (adjustmentRule.DateStart > dateTime2.Date)
				{
					return null;
				}
				if (!(adjustmentRule.DateEnd < dateTime2.Date))
				{
					return adjustmentRule;
				}
			}
			return null;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000076C8 File Offset: 0x000058C8
		private static DateTime TransitionPoint(TimeZoneInfo.TransitionTime transition, int year)
		{
			if (transition.IsFixedDateRule)
			{
				return new DateTime(year, transition.Month, transition.Day) + transition.TimeOfDay.TimeOfDay;
			}
			DateTime dateTime = new DateTime(year, transition.Month, 1);
			DayOfWeek dayOfWeek = dateTime.DayOfWeek;
			int num = 1 + (transition.Week - 1) * 7 + (transition.DayOfWeek - dayOfWeek) % 7;
			if (num > DateTime.DaysInMonth(year, transition.Month))
			{
				num -= 7;
			}
			return new DateTime(year, transition.Month, num) + transition.TimeOfDay.TimeOfDay;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007778 File Offset: 0x00005978
		private static bool ValidTZFile(byte[] buffer, int length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 4; i++)
			{
				stringBuilder.Append((char)buffer[i]);
			}
			return !(stringBuilder.ToString() != "TZif") && length < 16384;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000077D0 File Offset: 0x000059D0
		private static int SwapInt32(int i)
		{
			return ((i >> 24) & 255) | ((i >> 8) & 65280) | ((i << 8) & 16711680) | (i << 24);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000077F8 File Offset: 0x000059F8
		private static int ReadBigEndianInt32(byte[] buffer, int start)
		{
			int num = BitConverter.ToInt32(buffer, start);
			if (!BitConverter.IsLittleEndian)
			{
				return num;
			}
			return TimeZoneInfo.SwapInt32(num);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00007820 File Offset: 0x00005A20
		private static TimeZoneInfo ParseTZBuffer(string id, byte[] buffer, int length)
		{
			int num = TimeZoneInfo.ReadBigEndianInt32(buffer, 20);
			int num2 = TimeZoneInfo.ReadBigEndianInt32(buffer, 24);
			int num3 = TimeZoneInfo.ReadBigEndianInt32(buffer, 28);
			int num4 = TimeZoneInfo.ReadBigEndianInt32(buffer, 32);
			int num5 = TimeZoneInfo.ReadBigEndianInt32(buffer, 36);
			int num6 = TimeZoneInfo.ReadBigEndianInt32(buffer, 40);
			if (length < 44 + num4 * 5 + num5 * 6 + num6 + num3 * 8 + num2 + num)
			{
				throw new InvalidTimeZoneException();
			}
			Dictionary<int, string> dictionary = TimeZoneInfo.ParseAbbreviations(buffer, 44 + 4 * num4 + num4 + 6 * num5, num6);
			Dictionary<int, TimeZoneInfo.TimeType> dictionary2 = TimeZoneInfo.ParseTimesTypes(buffer, 44 + 4 * num4 + num4, num5, dictionary);
			List<KeyValuePair<DateTime, TimeZoneInfo.TimeType>> list = TimeZoneInfo.ParseTransitions(buffer, 44, num4, dictionary2);
			if (dictionary2.Count == 0)
			{
				throw new InvalidTimeZoneException();
			}
			if (dictionary2.Count == 1 && dictionary2[0].IsDst)
			{
				throw new InvalidTimeZoneException();
			}
			TimeSpan timeSpan = new TimeSpan(0L);
			TimeSpan timeSpan2 = new TimeSpan(0L);
			string text = null;
			string text2 = null;
			bool flag = false;
			DateTime dateTime = DateTime.MinValue;
			List<TimeZoneInfo.AdjustmentRule> list2 = new List<TimeZoneInfo.AdjustmentRule>();
			for (int i = 0; i < list.Count; i++)
			{
				KeyValuePair<DateTime, TimeZoneInfo.TimeType> keyValuePair = list[i];
				DateTime key = keyValuePair.Key;
				TimeZoneInfo.TimeType value = keyValuePair.Value;
				if (!value.IsDst)
				{
					if (text != value.Name || timeSpan.TotalSeconds != (double)value.Offset)
					{
						text = value.Name;
						text2 = null;
						timeSpan = new TimeSpan(0, 0, value.Offset);
						list2 = new List<TimeZoneInfo.AdjustmentRule>();
						flag = false;
					}
					if (flag)
					{
						dateTime += timeSpan;
						DateTime dateTime2 = key + timeSpan + timeSpan2;
						if (dateTime2.Date == new DateTime(dateTime2.Year, 1, 1) && dateTime2.Year > dateTime.Year)
						{
							dateTime2 -= new TimeSpan(24, 0, 0);
						}
						DateTime dateTime3;
						if (dateTime.Month < 7)
						{
							dateTime3 = new DateTime(dateTime.Year, 1, 1);
						}
						else
						{
							dateTime3 = new DateTime(dateTime.Year, 7, 1);
						}
						DateTime dateTime4;
						if (dateTime2.Month >= 7)
						{
							dateTime4 = new DateTime(dateTime2.Year, 12, 31);
						}
						else
						{
							dateTime4 = new DateTime(dateTime2.Year, 6, 30);
						}
						TimeZoneInfo.TransitionTime transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1) + dateTime.TimeOfDay, dateTime.Month, dateTime.Day);
						TimeZoneInfo.TransitionTime transitionTime2 = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1) + dateTime2.TimeOfDay, dateTime2.Month, dateTime2.Day);
						if (transitionTime != transitionTime2)
						{
							list2.Add(TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateTime3, dateTime4, timeSpan2, transitionTime, transitionTime2));
						}
					}
					flag = false;
				}
				else
				{
					if (text2 != value.Name || timeSpan2.TotalSeconds != (double)value.Offset - timeSpan.TotalSeconds)
					{
						text2 = value.Name;
						timeSpan2 = new TimeSpan(0, 0, value.Offset) - timeSpan;
					}
					dateTime = key;
					flag = true;
				}
			}
			if (list2.Count == 0)
			{
				TimeZoneInfo.TimeType timeType = dictionary2[0];
				if (text == null)
				{
					text = timeType.Name;
					timeSpan = new TimeSpan(0, 0, timeType.Offset);
				}
				return TimeZoneInfo.CreateCustomTimeZone(id, timeSpan, id, text);
			}
			return TimeZoneInfo.CreateCustomTimeZone(id, timeSpan, id, text, text2, TimeZoneInfo.ValidateRules(list2).ToArray());
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007BB4 File Offset: 0x00005DB4
		private static List<TimeZoneInfo.AdjustmentRule> ValidateRules(List<TimeZoneInfo.AdjustmentRule> adjustmentRules)
		{
			TimeZoneInfo.AdjustmentRule adjustmentRule = null;
			foreach (TimeZoneInfo.AdjustmentRule adjustmentRule2 in adjustmentRules.ToArray())
			{
				if (adjustmentRule != null && adjustmentRule.DateEnd > adjustmentRule2.DateStart)
				{
					adjustmentRules.Remove(adjustmentRule2);
				}
				adjustmentRule = adjustmentRule2;
			}
			return adjustmentRules;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007C0C File Offset: 0x00005E0C
		private static Dictionary<int, string> ParseAbbreviations(byte[] buffer, int index, int count)
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				char c = (char)buffer[index + i];
				if (c != '\0')
				{
					stringBuilder.Append(c);
				}
				else
				{
					dictionary.Add(num, stringBuilder.ToString());
					for (int j = 1; j < stringBuilder.Length; j++)
					{
						dictionary.Add(num + j, stringBuilder.ToString(j, stringBuilder.Length - j));
					}
					num = i + 1;
					stringBuilder = new StringBuilder();
				}
			}
			return dictionary;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007CA4 File Offset: 0x00005EA4
		private static Dictionary<int, TimeZoneInfo.TimeType> ParseTimesTypes(byte[] buffer, int index, int count, Dictionary<int, string> abbreviations)
		{
			Dictionary<int, TimeZoneInfo.TimeType> dictionary = new Dictionary<int, TimeZoneInfo.TimeType>(count);
			for (int i = 0; i < count; i++)
			{
				int num = TimeZoneInfo.ReadBigEndianInt32(buffer, index + 6 * i);
				byte b = buffer[index + 6 * i + 4];
				byte b2 = buffer[index + 6 * i + 5];
				dictionary.Add(i, new TimeZoneInfo.TimeType(num, b != 0, abbreviations[(int)b2]));
			}
			return dictionary;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007D08 File Offset: 0x00005F08
		private static List<KeyValuePair<DateTime, TimeZoneInfo.TimeType>> ParseTransitions(byte[] buffer, int index, int count, Dictionary<int, TimeZoneInfo.TimeType> time_types)
		{
			List<KeyValuePair<DateTime, TimeZoneInfo.TimeType>> list = new List<KeyValuePair<DateTime, TimeZoneInfo.TimeType>>(count);
			for (int i = 0; i < count; i++)
			{
				int num = TimeZoneInfo.ReadBigEndianInt32(buffer, index + 4 * i);
				DateTime dateTime = TimeZoneInfo.DateTimeFromUnixTime((long)num);
				byte b = buffer[index + 4 * count + i];
				list.Add(new KeyValuePair<DateTime, TimeZoneInfo.TimeType>(dateTime, time_types[(int)b]));
			}
			return list;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007D64 File Offset: 0x00005F64
		private static DateTime DateTimeFromUnixTime(long unix_time)
		{
			DateTime dateTime = new DateTime(1970, 1, 1);
			return dateTime.AddSeconds((double)unix_time);
		}

		// Token: 0x04000043 RID: 67
		private const int BUFFER_SIZE = 16384;

		// Token: 0x04000044 RID: 68
		private TimeSpan baseUtcOffset;

		// Token: 0x04000045 RID: 69
		private string daylightDisplayName;

		// Token: 0x04000046 RID: 70
		private string displayName;

		// Token: 0x04000047 RID: 71
		private string id;

		// Token: 0x04000048 RID: 72
		private static TimeZoneInfo local;

		// Token: 0x04000049 RID: 73
		private string standardDisplayName;

		// Token: 0x0400004A RID: 74
		private bool disableDaylightSavingTime;

		// Token: 0x0400004B RID: 75
		private static TimeZoneInfo utc;

		// Token: 0x0400004C RID: 76
		private static string timeZoneDirectory;

		// Token: 0x0400004D RID: 77
		private TimeZoneInfo.AdjustmentRule[] adjustmentRules;

		// Token: 0x0400004E RID: 78
		private static List<TimeZoneInfo> systemTimeZones;

		/// <summary>Provides information about a time zone adjustment, such as the transition to and from daylight saving time.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x02000013 RID: 19
		[Serializable]
		public sealed class AdjustmentRule : ISerializable, IDeserializationCallback, IEquatable<TimeZoneInfo.AdjustmentRule>
		{
			// Token: 0x06000139 RID: 313 RVA: 0x00007D88 File Offset: 0x00005F88
			private AdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				if (dateStart.Kind != DateTimeKind.Unspecified || dateEnd.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException("the Kind property of dateStart or dateEnd parameter does not equal DateTimeKind.Unspecified");
				}
				if (daylightTransitionStart == daylightTransitionEnd)
				{
					throw new ArgumentException("daylightTransitionStart parameter cannot equal daylightTransitionEnd parameter");
				}
				if (dateStart.Ticks % 864000000000L != 0L || dateEnd.Ticks % 864000000000L != 0L)
				{
					throw new ArgumentException("dateStart or dateEnd parameter includes a time of day value");
				}
				if (dateEnd < dateStart)
				{
					throw new ArgumentOutOfRangeException("dateEnd is earlier than dateStart");
				}
				if (daylightDelta > new TimeSpan(14, 0, 0) || daylightDelta < new TimeSpan(-14, 0, 0))
				{
					throw new ArgumentOutOfRangeException("daylightDelta is less than -14 or greater than 14 hours");
				}
				if (daylightDelta.Ticks % 10000000L != 0L)
				{
					throw new ArgumentOutOfRangeException("daylightDelta parameter does not represent a whole number of seconds");
				}
				this.dateStart = dateStart;
				this.dateEnd = dateEnd;
				this.daylightDelta = daylightDelta;
				this.daylightTransitionStart = daylightTransitionStart;
				this.daylightTransitionEnd = daylightTransitionEnd;
			}

			/// <summary>Gets the date when the adjustment rule ceases to be in effect.</summary>
			/// <returns>A <see cref="T:System.DateTime" /> value that indicates the end date of the adjustment rule.</returns>
			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600013A RID: 314 RVA: 0x00007E9C File Offset: 0x0000609C
			public DateTime DateEnd
			{
				get
				{
					return this.dateEnd;
				}
			}

			/// <summary>Gets the date when the adjustment rule takes effect.</summary>
			/// <returns>A <see cref="T:System.DateTime" /> value that indicates when the adjustment rule takes effect.</returns>
			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600013B RID: 315 RVA: 0x00007EA4 File Offset: 0x000060A4
			public DateTime DateStart
			{
				get
				{
					return this.dateStart;
				}
			}

			/// <summary>Gets the amount of time that is required to form the time zone's daylight saving time. This amount of time is added to the time zone's offset from Coordinated Universal Time (UTC).</summary>
			/// <returns>A <see cref="T:System.TimeSpan" /> object that indicates the amount of time to add to the standard time changes as a result of the adjustment rule.</returns>
			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600013C RID: 316 RVA: 0x00007EAC File Offset: 0x000060AC
			public TimeSpan DaylightDelta
			{
				get
				{
					return this.daylightDelta;
				}
			}

			/// <summary>Gets information about the annual transition from daylight saving time back to standard time.</summary>
			/// <returns>A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that defines the annual transition from daylight saving time back to the time zone's standard time.</returns>
			// Token: 0x17000018 RID: 24
			// (get) Token: 0x0600013D RID: 317 RVA: 0x00007EB4 File Offset: 0x000060B4
			public TimeZoneInfo.TransitionTime DaylightTransitionEnd
			{
				get
				{
					return this.daylightTransitionEnd;
				}
			}

			/// <summary>Gets information about the annual transition from standard time to daylight saving time.</summary>
			/// <returns>A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that defines the annual transition from a time zone's standard time to daylight saving time.</returns>
			// Token: 0x17000019 RID: 25
			// (get) Token: 0x0600013E RID: 318 RVA: 0x00007EBC File Offset: 0x000060BC
			public TimeZoneInfo.TransitionTime DaylightTransitionStart
			{
				get
				{
					return this.daylightTransitionStart;
				}
			}

			/// <summary>Creates a new adjustment rule for a particular time zone.</summary>
			/// <returns>A <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object that represents the new adjustment rule.</returns>
			/// <param name="dateStart">The effective date of the adjustment rule. If the value of the <paramref name="dateStart" /> parameter is DateTime.MinValue.Date, this is the first adjustment rule in effect for a time zone.   </param>
			/// <param name="dateEnd">The last date that the adjustment rule is in force. If the value of the <paramref name="dateEnd" /> parameter is DateTime.MaxValue.Date, the adjustment rule has no end date.</param>
			/// <param name="daylightDelta">The time change that results from the adjustment. This value is added to the time zone's <see cref="P:System.TimeZoneInfo.BaseUtcOffset" /> property to obtain the correct daylight offset from Coordinated Universal Time (UTC). This value can range from -14 to 14. </param>
			/// <param name="daylightTransitionStart">A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that defines the start of daylight saving time.</param>
			/// <param name="daylightTransitionEnd">A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that defines the end of daylight saving time.   </param>
			/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateStart" /> or <paramref name="dateEnd" /> parameter does not equal <see cref="F:System.DateTimeKind.Unspecified" />.-or-The <paramref name="daylightTransitionStart" /> parameter is equal to the <paramref name="daylightTransitionEnd" /> parameter.-or-The <paramref name="dateStart" /> or <paramref name="dateEnd" /> parameter includes a time of day value.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="dateEnd" /> is earlier than <paramref name="dateStart" />.-or-<paramref name="daylightDelta" /> is less than -14 or greater than 14.-or-The <see cref="P:System.TimeSpan.Milliseconds" /> property of the <paramref name="daylightDelta" /> parameter is not equal to 0.-or-The <see cref="P:System.TimeSpan.Ticks" /> property of the <paramref name="daylightDelta" /> parameter does not equal a whole number of seconds.</exception>
			// Token: 0x0600013F RID: 319 RVA: 0x00007EC4 File Offset: 0x000060C4
			public static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				return new TimeZoneInfo.AdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
			}

			/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object is equal to a second <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object.</summary>
			/// <returns>true if both <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects have equal values; otherwise, false.</returns>
			/// <param name="other">A second <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object.</param>
			/// <filterpriority>2</filterpriority>
			// Token: 0x06000140 RID: 320 RVA: 0x00007ED4 File Offset: 0x000060D4
			public bool Equals(TimeZoneInfo.AdjustmentRule other)
			{
				return this.dateStart == other.dateStart && this.dateEnd == other.dateEnd && this.daylightDelta == other.daylightDelta && this.daylightTransitionStart == other.daylightTransitionStart && this.daylightTransitionEnd == other.daylightTransitionEnd;
			}

			/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
			/// <returns>A 32-bit signed integer that serves as the hash code for the current <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object.</returns>
			/// <filterpriority>2</filterpriority>
			// Token: 0x06000141 RID: 321 RVA: 0x00007F50 File Offset: 0x00006150
			public override int GetHashCode()
			{
				return this.dateStart.GetHashCode() ^ this.dateEnd.GetHashCode() ^ this.daylightDelta.GetHashCode() ^ this.daylightTransitionStart.GetHashCode() ^ this.daylightTransitionEnd.GetHashCode();
			}

			// Token: 0x06000142 RID: 322 RVA: 0x00007F98 File Offset: 0x00006198
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000143 RID: 323 RVA: 0x00007FA0 File Offset: 0x000061A0
			public void OnDeserialization(object sender)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400004F RID: 79
			private DateTime dateEnd;

			// Token: 0x04000050 RID: 80
			private DateTime dateStart;

			// Token: 0x04000051 RID: 81
			private TimeSpan daylightDelta;

			// Token: 0x04000052 RID: 82
			private TimeZoneInfo.TransitionTime daylightTransitionEnd;

			// Token: 0x04000053 RID: 83
			private TimeZoneInfo.TransitionTime daylightTransitionStart;
		}

		// Token: 0x02000014 RID: 20
		private struct TimeType
		{
			// Token: 0x06000144 RID: 324 RVA: 0x00007FA8 File Offset: 0x000061A8
			public TimeType(int offset, bool is_dst, string abbrev)
			{
				this.Offset = offset;
				this.IsDst = is_dst;
				this.Name = abbrev;
			}

			// Token: 0x06000145 RID: 325 RVA: 0x00007FC0 File Offset: 0x000061C0
			public override string ToString()
			{
				return string.Concat(new object[] { "offset: ", this.Offset, "s, is_dst: ", this.IsDst, ", zone name: ", this.Name });
			}

			// Token: 0x04000054 RID: 84
			public readonly int Offset;

			// Token: 0x04000055 RID: 85
			public readonly bool IsDst;

			// Token: 0x04000056 RID: 86
			public string Name;
		}

		/// <summary>Provides information about a specific time change, such as the change from daylight saving time to standard time or vice versa, in a particular time zone.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x02000015 RID: 21
		[Serializable]
		public struct TransitionTime : ISerializable, IDeserializationCallback, IEquatable<TimeZoneInfo.TransitionTime>
		{
			// Token: 0x06000146 RID: 326 RVA: 0x00008018 File Offset: 0x00006218
			private TransitionTime(DateTime timeOfDay, int month, int day)
			{
				this = new TimeZoneInfo.TransitionTime(timeOfDay, month);
				if (day < 1 || day > 31)
				{
					throw new ArgumentOutOfRangeException("day parameter is less than 1 or greater than 31");
				}
				this.day = day;
				this.isFixedDateRule = true;
			}

			// Token: 0x06000147 RID: 327 RVA: 0x00008058 File Offset: 0x00006258
			private TransitionTime(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
			{
				this = new TimeZoneInfo.TransitionTime(timeOfDay, month);
				if (week < 1 || week > 5)
				{
					throw new ArgumentOutOfRangeException("week parameter is less than 1 or greater than 5");
				}
				if (dayOfWeek != DayOfWeek.Sunday && dayOfWeek != DayOfWeek.Monday && dayOfWeek != DayOfWeek.Tuesday && dayOfWeek != DayOfWeek.Wednesday && dayOfWeek != DayOfWeek.Thursday && dayOfWeek != DayOfWeek.Friday && dayOfWeek != DayOfWeek.Saturday)
				{
					throw new ArgumentOutOfRangeException("dayOfWeek parameter is not a member od DayOfWeek enumeration");
				}
				this.week = week;
				this.dayOfWeek = dayOfWeek;
				this.isFixedDateRule = false;
			}

			// Token: 0x06000148 RID: 328 RVA: 0x000080E0 File Offset: 0x000062E0
			private TransitionTime(DateTime timeOfDay, int month)
			{
				if (timeOfDay.Year != 1 || timeOfDay.Month != 1 || timeOfDay.Day != 1)
				{
					throw new ArgumentException("timeOfDay parameter has a non-default date component");
				}
				if (timeOfDay.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException("timeOfDay parameter Kind's property is not DateTimeKind.Unspecified");
				}
				if (timeOfDay.Ticks % 10000L != 0L)
				{
					throw new ArgumentException("timeOfDay parameter does not represent a whole number of milliseconds");
				}
				if (month < 1 || month > 12)
				{
					throw new ArgumentOutOfRangeException("month parameter is less than 1 or greater than 12");
				}
				this.timeOfDay = timeOfDay;
				this.month = month;
				this.week = -1;
				this.dayOfWeek = (DayOfWeek)(-1);
				this.day = -1;
				this.isFixedDateRule = false;
			}

			/// <summary>Gets the hour, minute, and second at which the time change occurs.</summary>
			/// <returns>A <see cref="T:System.DateTime" /> value that indicates the time of day at which the time change occurs.</returns>
			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000149 RID: 329 RVA: 0x00008198 File Offset: 0x00006398
			public DateTime TimeOfDay
			{
				get
				{
					return this.timeOfDay;
				}
			}

			/// <summary>Gets the month in which the time change occurs.</summary>
			/// <returns>The month in which the time change occurs.</returns>
			// Token: 0x1700001B RID: 27
			// (get) Token: 0x0600014A RID: 330 RVA: 0x000081A0 File Offset: 0x000063A0
			public int Month
			{
				get
				{
					return this.month;
				}
			}

			/// <summary>Gets the day on which the time change occurs.</summary>
			/// <returns>The day on which the time change occurs.</returns>
			// Token: 0x1700001C RID: 28
			// (get) Token: 0x0600014B RID: 331 RVA: 0x000081A8 File Offset: 0x000063A8
			public int Day
			{
				get
				{
					return this.day;
				}
			}

			/// <summary>Gets the week of the month in which a time change occurs.</summary>
			/// <returns>The week of the month in which the time change occurs.</returns>
			// Token: 0x1700001D RID: 29
			// (get) Token: 0x0600014C RID: 332 RVA: 0x000081B0 File Offset: 0x000063B0
			public int Week
			{
				get
				{
					return this.week;
				}
			}

			/// <summary>Gets the day of the week on which the time change occurs.</summary>
			/// <returns>The day of the week on which the time change occurs.</returns>
			// Token: 0x1700001E RID: 30
			// (get) Token: 0x0600014D RID: 333 RVA: 0x000081B8 File Offset: 0x000063B8
			public DayOfWeek DayOfWeek
			{
				get
				{
					return this.dayOfWeek;
				}
			}

			/// <summary>Gets a value indicating whether the time change occurs at a fixed date and time (such as November 1) or a floating date and time (such as the last Sunday of October).</summary>
			/// <returns>true if the time change rule is fixed-date; false if the time change rule is floating-date.</returns>
			// Token: 0x1700001F RID: 31
			// (get) Token: 0x0600014E RID: 334 RVA: 0x000081C0 File Offset: 0x000063C0
			public bool IsFixedDateRule
			{
				get
				{
					return this.isFixedDateRule;
				}
			}

			/// <summary>Defines a time change that uses a fixed-date rule.</summary>
			/// <returns>A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that contains data about the time change.</returns>
			/// <param name="timeOfDay">The time at which the time change occurs.</param>
			/// <param name="month">The month in which the time change occurs.</param>
			/// <param name="day">The day of the month on which the time change occurs.</param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="timeOfDay" /> parameter has a non-default date component.-or-The <paramref name="timeOfDay" /> parameter's <see cref="P:System.DateTime.Kind" /> property is not <see cref="F:System.DateTimeKind.Unspecified" />.-or-The <paramref name="timeOfDay" /> parameter does not represent a whole number of milliseconds.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="month" /> parameter is less than 1 or greater than 12.-or-The <paramref name="day" /> parameter is less than 1 or greater than 31.</exception>
			// Token: 0x0600014F RID: 335 RVA: 0x000081C8 File Offset: 0x000063C8
			public static TimeZoneInfo.TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day)
			{
				return new TimeZoneInfo.TransitionTime(timeOfDay, month, day);
			}

			/// <summary>Defines a time change that uses a floating-date rule.</summary>
			/// <returns>A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that contains data about the time change.</returns>
			/// <param name="timeOfDay">The time at which the time change occurs.</param>
			/// <param name="month">The month in which the time change occurs.</param>
			/// <param name="week">The week of the month in which the time change occurs.</param>
			/// <param name="dayOfWeek">The day of the week on which the time change occurs.</param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="timeOfDay" /> parameter has a non-default date component.-or-The <paramref name="timeOfDay" /> parameter does not represent a whole number of milliseconds.-or-The <paramref name="timeOfDay" /> parameter's <see cref="P:System.DateTime.Kind" /> property is not <see cref="F:System.DateTimeKind.Unspecified" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="month" /> is less than 1 or greater than 12.-or-<paramref name="week" /> is less than 1 or greater than 5.-or-The <paramref name="dayOfWeek" /> parameter is not a member of the <see cref="T:System.DayOfWeek" /> enumeration.</exception>
			// Token: 0x06000150 RID: 336 RVA: 0x000081D4 File Offset: 0x000063D4
			public static TimeZoneInfo.TransitionTime CreateFloatingDateRule(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
			{
				return new TimeZoneInfo.TransitionTime(timeOfDay, month, week, dayOfWeek);
			}

			// Token: 0x06000151 RID: 337 RVA: 0x000081E0 File Offset: 0x000063E0
			public void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new NotImplementedException();
			}

			/// <summary>Determines whether an object has identical values to the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</summary>
			/// <returns>true if the two objects are equal; otherwise, false.</returns>
			/// <param name="obj">An object to compare with the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.   </param>
			/// <filterpriority>2</filterpriority>
			// Token: 0x06000152 RID: 338 RVA: 0x000081E8 File Offset: 0x000063E8
			public override bool Equals(object other)
			{
				return other is TimeZoneInfo.TransitionTime && this == (TimeZoneInfo.TransitionTime)other;
			}

			/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object has identical values to a second <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</summary>
			/// <returns>true if the two objects have identical property values; otherwise, false.</returns>
			/// <param name="other">The second <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.   </param>
			/// <filterpriority>2</filterpriority>
			// Token: 0x06000153 RID: 339 RVA: 0x00008208 File Offset: 0x00006408
			public bool Equals(TimeZoneInfo.TransitionTime other)
			{
				return this == other;
			}

			/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
			/// <returns>A 32-bit signed integer that serves as the hash code for this <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</returns>
			/// <filterpriority>2</filterpriority>
			// Token: 0x06000154 RID: 340 RVA: 0x00008218 File Offset: 0x00006418
			public override int GetHashCode()
			{
				return this.day ^ (int)this.dayOfWeek ^ this.month ^ (int)this.timeOfDay.Ticks ^ this.week;
			}

			// Token: 0x06000155 RID: 341 RVA: 0x00008250 File Offset: 0x00006450
			public void OnDeserialization(object sender)
			{
				throw new NotImplementedException();
			}

			/// <summary>Determines whether two specified <see cref="T:System.TimeZoneInfo.TransitionTime" /> objects are equal.</summary>
			/// <returns>true if <paramref name="left" /> and <paramref name="right" /> have identical values; otherwise, false.</returns>
			/// <param name="left">The first <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</param>
			/// <param name="right">The second <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</param>
			// Token: 0x06000156 RID: 342 RVA: 0x00008258 File Offset: 0x00006458
			public static bool operator ==(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return t1.day == t2.day && t1.dayOfWeek == t2.dayOfWeek && t1.isFixedDateRule == t2.isFixedDateRule && t1.month == t2.month && t1.timeOfDay == t2.timeOfDay && t1.week == t2.week;
			}

			/// <summary>Determines whether two specified <see cref="T:System.TimeZoneInfo.TransitionTime" /> objects are not equal.</summary>
			/// <returns>true if <paramref name="left" /> and <paramref name="right" /> have any different member values; otherwise, false.</returns>
			/// <param name="left">The first <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</param>
			/// <param name="right">The second <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</param>
			// Token: 0x06000157 RID: 343 RVA: 0x000082DC File Offset: 0x000064DC
			public static bool operator !=(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return !(t1 == t2);
			}

			// Token: 0x04000057 RID: 87
			private DateTime timeOfDay;

			// Token: 0x04000058 RID: 88
			private int month;

			// Token: 0x04000059 RID: 89
			private int day;

			// Token: 0x0400005A RID: 90
			private int week;

			// Token: 0x0400005B RID: 91
			private DayOfWeek dayOfWeek;

			// Token: 0x0400005C RID: 92
			private bool isFixedDateRule;
		}
	}
}
