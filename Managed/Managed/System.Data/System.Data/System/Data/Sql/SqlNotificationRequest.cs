using System;

namespace System.Data.Sql
{
	/// <summary>Represents a request for notification for a given command. </summary>
	// Token: 0x02000143 RID: 323
	public sealed class SqlNotificationRequest
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.Sql.SqlNotificationRequest" /> class with default values.</summary>
		// Token: 0x06001163 RID: 4451 RVA: 0x00044440 File Offset: 0x00042640
		public SqlNotificationRequest()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Data.Sql.SqlNotificationRequest" /> class with a user-defined string that identifies a particular notification request, the name of a predefined SQL Server 2005 Service Broker service name, and the time-out period, measured in seconds.</summary>
		/// <param name="userData">A string that contains an application-specific identifier for this notification. It is not used by the notifications infrastructure, but it allows you to associate notifications with the application state. The value indicated in this parameter is included in the Service Broker queue message. </param>
		/// <param name="options">A string that contains the Service Broker service name where notification messages are posted, and it must include a database name or a Service Broker instance GUID that restricts the scope of the service name lookup to a particular database.For more information about the format of the <paramref name="options" /> parameter, see <see cref="P:System.Data.Sql.SqlNotificationRequest.Options" />.</param>
		/// <param name="timeout">The time, in seconds, to wait for a notification message. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="options" /> parameter is NULL. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="options" /> or <paramref name="userData" /> parameter is longer than uint16.MaxValue or the value in the <paramref name="timeout" /> parameter is less than zero. </exception>
		// Token: 0x06001164 RID: 4452 RVA: 0x00044448 File Offset: 0x00042648
		public SqlNotificationRequest(string userData, string options, int timeout)
		{
			this.UserData = userData;
			this.Options = options;
			this.Timeout = timeout;
		}

		/// <summary>Gets or sets an application-specific identifier for this notification.</summary>
		/// <returns>A string value of the application-specific identifier for this notification.</returns>
		/// <exception cref="T:System.ArgumentException">The value is longer than uint16.MaxValue. </exception>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00044470 File Offset: 0x00042670
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x00044478 File Offset: 0x00042678
		public string UserData
		{
			get
			{
				return this.userData;
			}
			set
			{
				if (value != null && value.Length > 65535)
				{
					throw new ArgumentOutOfRangeException("UserData");
				}
				this.userData = value;
			}
		}

		/// <summary>Gets or sets the SQL Server Service Broker service name where notification messages are posted.</summary>
		/// <returns>string that contains the SQL Server 2005 Service Broker service name where notification messages are posted and the database or service broker instance GUID to scope the server name lookup.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is NULL. </exception>
		/// <exception cref="T:System.ArgumentException">The value is longer than uint16.MaxValue. </exception>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x000444B0 File Offset: 0x000426B0
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x000444B8 File Offset: 0x000426B8
		public string Options
		{
			get
			{
				return this.options;
			}
			set
			{
				if (value != null && value.Length > 65535)
				{
					throw new ArgumentOutOfRangeException("Service");
				}
				this.options = value;
			}
		}

		/// <summary>Gets or sets a value that specifies how long SQL Server waits for a change to occur before the operation times out.</summary>
		/// <returns>A signed integer value that specifies, in seconds, how long SQL Server waits for a change to occur before the operation times out.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than zero. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x000444F0 File Offset: 0x000426F0
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x000444F8 File Offset: 0x000426F8
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("Timeout");
				}
				this.timeout = value;
			}
		}

		// Token: 0x04000670 RID: 1648
		private string userData;

		// Token: 0x04000671 RID: 1649
		private string options;

		// Token: 0x04000672 RID: 1650
		private int timeout;
	}
}
