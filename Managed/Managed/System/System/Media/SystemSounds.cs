using System;

namespace System.Media
{
	/// <summary>Retrieves sounds associated with a set of Windows operating system sound-event types. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002B6 RID: 694
	public sealed class SystemSounds
	{
		// Token: 0x06001813 RID: 6163 RVA: 0x00042354 File Offset: 0x00040554
		private SystemSounds()
		{
		}

		/// <summary>Gets the sound associated with the Asterisk program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" />.</returns>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x0004235C File Offset: 0x0004055C
		public static SystemSound Asterisk
		{
			get
			{
				return new SystemSound("Asterisk");
			}
		}

		/// <summary>Gets the sound associated with the Beep program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" />.</returns>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x00042368 File Offset: 0x00040568
		public static SystemSound Beep
		{
			get
			{
				return new SystemSound("Beep");
			}
		}

		/// <summary>Gets the sound associated with the Exclamation program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" />.</returns>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x00042374 File Offset: 0x00040574
		public static SystemSound Exclamation
		{
			get
			{
				return new SystemSound("Exclamation");
			}
		}

		/// <summary>Gets the sound associated with the Hand program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" />.</returns>
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00042380 File Offset: 0x00040580
		public static SystemSound Hand
		{
			get
			{
				return new SystemSound("Hand");
			}
		}

		/// <summary>Gets the sound associated with the Question program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" />.</returns>
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x0004238C File Offset: 0x0004058C
		public static SystemSound Question
		{
			get
			{
				return new SystemSound("Question");
			}
		}
	}
}
