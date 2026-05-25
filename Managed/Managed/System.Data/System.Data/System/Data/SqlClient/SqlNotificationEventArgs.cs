using System;

namespace System.Data.SqlClient
{
	/// <summary>Represents the set of arguments passed to the notification event handler.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017F RID: 383
	public class SqlNotificationEventArgs : EventArgs
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlNotificationEventArgs" /> object. </summary>
		/// <param name="type">
		///   <see cref="T:System.Data.SqlClient.SqlNotificationType" /> value that indicates whether this notification is generated because of an actual change, or by the subscription. </param>
		/// <param name="info">
		///   <see cref="T:System.Data.SqlClient.SqlNotificationInfo" /> value that indicates the reason for the notification event. This may occur because the data in the store actually changed, or the notification became invalid (for example, it timed out). </param>
		/// <param name="source">
		///   <see cref="T:System.Data.SqlClient.SqlNotificationSource" /> value that indicates the source that generated the notification. </param>
		// Token: 0x06001472 RID: 5234 RVA: 0x00055ECC File Offset: 0x000540CC
		public SqlNotificationEventArgs(SqlNotificationType type, SqlNotificationInfo info, SqlNotificationSource source)
		{
			this.type = type;
			this.info = info;
			this.source = source;
		}

		/// <summary>Gets a value that indicates whether this notification is generated because of an actual change, or by the subscription.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlNotificationType" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x00055EEC File Offset: 0x000540EC
		public SqlNotificationType Type
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets a value that indicates the reason for the notification event.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlNotificationInfo" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00055EF4 File Offset: 0x000540F4
		public SqlNotificationInfo Info
		{
			get
			{
				return this.info;
			}
		}

		/// <summary>Gets a value that indicates the source that generated the notification.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlNotificationSource" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x00055EFC File Offset: 0x000540FC
		public SqlNotificationSource Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x04000829 RID: 2089
		private SqlNotificationType type;

		// Token: 0x0400082A RID: 2090
		private SqlNotificationInfo info;

		// Token: 0x0400082B RID: 2091
		private SqlNotificationSource source;
	}
}
