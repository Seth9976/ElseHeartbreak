using System;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	/// <summary>The <see cref="T:System.Data.SqlClient.SqlDependency" /> object represents a query notification dependency between an application and an instance of SQL Server 2005. An application can create a <see cref="T:System.Data.SqlClient.SqlDependency" /> object and register to receive notifications via the <see cref="T:System.Data.SqlClient.OnChangeEventHandler" /> event handler.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200016A RID: 362
	public sealed class SqlDependency
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class with the default settings.</summary>
		// Token: 0x06001366 RID: 4966 RVA: 0x0005166C File Offset: 0x0004F86C
		[MonoTODO]
		public SqlDependency()
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class and associates it with the <see cref="T:System.Data.SqlClient.SqlCommand" /> parameter.</summary>
		/// <param name="command">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object to associate with this <see cref="T:System.Data.SqlClient.SqlDependency" /> object. The constructor will set up a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object and bind it to the command. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="command" /> parameter is NULL. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object already has a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object assigned to its <see cref="P:System.Data.SqlClient.SqlCommand.Notification" /> property, and that <see cref="T:System.Data.Sql.SqlNotificationRequest" /> is not associated with this dependency. </exception>
		// Token: 0x06001367 RID: 4967 RVA: 0x00051694 File Offset: 0x0004F894
		[MonoTODO]
		public SqlDependency(SqlCommand command)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class, associates it with the <see cref="T:System.Data.SqlClient.SqlCommand" /> parameter, and specifies notification options and a time-out value.</summary>
		/// <param name="command">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object to associate with this <see cref="T:System.Data.SqlClient.SqlDependency" /> object. The constructor sets up a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object and bind it to the command.</param>
		/// <param name="options">The notification request options to be used by this dependency. null to use the default service. </param>
		/// <param name="timeout">The time-out for this notification in seconds. The default is 0, indicating that the server's time-out should be used.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="command" /> parameter is NULL. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The time-out value is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object already has a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object assigned to its <see cref="P:System.Data.SqlClient.SqlCommand.Notification" /> property and that <see cref="T:System.Data.Sql.SqlNotificationRequest" /> is not associated with this dependency.An attempt was made to create a SqlDependency instance from within SQLCLR.</exception>
		// Token: 0x06001368 RID: 4968 RVA: 0x000516BC File Offset: 0x0004F8BC
		[MonoTODO]
		public SqlDependency(SqlCommand command, string options, int timeout)
		{
		}

		/// <summary>Occurs when a notification is received for any of the commands associated with this <see cref="T:System.Data.SqlClient.SqlDependency" /> object.</summary>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001369 RID: 4969 RVA: 0x000516E4 File Offset: 0x0004F8E4
		// (remove) Token: 0x0600136A RID: 4970 RVA: 0x00051700 File Offset: 0x0004F900
		public event OnChangeEventHandler OnChange;

		/// <summary>Gets a value that uniquely identifies this instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class.</summary>
		/// <returns>A string representation of a GUID that is generated for each instance of the <see cref="T:System.Data.SqlClient.SqlDependency" /> class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0005171C File Offset: 0x0004F91C
		public string Id
		{
			get
			{
				return this.uniqueId;
			}
		}

		/// <summary>Gets a value that indicates whether one of the result sets associated with the dependency has changed.</summary>
		/// <returns>A Boolean value indicating whether one of the result sets has changed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x00051724 File Offset: 0x0004F924
		[MonoTODO]
		public bool HasChanges
		{
			get
			{
				return true;
			}
		}

		/// <summary>Associates a <see cref="T:System.Data.SqlClient.SqlCommand" /> object with this <see cref="T:System.Data.SqlClient.SqlDependency" /> instance.</summary>
		/// <param name="command">A <see cref="T:System.Data.SqlClient.SqlCommand" /> object containing a statement that is valid for notifications. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="command" /> parameter is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlCommand" /> object already has a <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object assigned to its <see cref="P:System.Data.SqlClient.SqlCommand.Notification" /> property, and that <see cref="T:System.Data.Sql.SqlNotificationRequest" /> is not associated with this dependency. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600136D RID: 4973 RVA: 0x00051728 File Offset: 0x0004F928
		[MonoTODO]
		public void AddCommandDependency(SqlCommand command)
		{
		}

		/// <summary>Starts the listener for receiving dependency change notifications from the instance of SQL Server specified by the connection string.</summary>
		/// <returns>true if the listener initialized successfully; false if a compatible listener already exists.</returns>
		/// <param name="connectionString">The connection string for the instance of SQL Server from which to obtain change notifications.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="connectionString" /> parameter is NULL. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="connectionString" /> parameter is the same as a previous call to this method, but the parameters are different.The method was called from within SQLCLR.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Data.SqlClient.SqlClientPermission" /> code access security (CAS) permission.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">A subsequent call to the method has been made with an equivalent <paramref name="connectionString" /> parameter with a different user, or a user that does not default to the same schema.Also, any underlying SqlClient exceptions.</exception>
		// Token: 0x0600136E RID: 4974 RVA: 0x0005172C File Offset: 0x0004F92C
		[MonoTODO]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
		public static bool Start(string connectionString)
		{
			return true;
		}

		/// <summary>Starts the listener for receiving dependency change notifications from the instance of SQL Server specified by the connection string using the specified SQL Server Service Broker queue.</summary>
		/// <returns>true if the listener initialized successfully; false if a compatible listener already exists.</returns>
		/// <param name="connectionString">The connection string for the instance of SQL Server from which to obtain change notifications.</param>
		/// <param name="queue">An existing SQL Server Service Broker queue to be used. If null, the default queue is used.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="connectionString" /> parameter is NULL. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="connectionString" /> parameter is the same as a previous call to this method, but the parameters are different.The method was called from within SQLCLR.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Data.SqlClient.SqlClientPermission" /> code access security (CAS) permission.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">A subsequent call to the method has been made with an equivalent <paramref name="connectionString" /> parameter but a different user, or a user that does not default to the same schema.Also, any underlying SqlClient exceptions.</exception>
		// Token: 0x0600136F RID: 4975 RVA: 0x00051730 File Offset: 0x0004F930
		[MonoTODO]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
		public static bool Start(string connectionString, string queue)
		{
			return true;
		}

		/// <summary>Stops a listener for a connection specified in a previous <see cref="Overload:System.Data.SqlClient.SqlDependency.Start" /> call.</summary>
		/// <returns>true if the listener was completely stopped; false if the <see cref="T:System.AppDomain" /> was unbound from the listener, but there are is at least one other <see cref="T:System.AppDomain" /> using the same listener.</returns>
		/// <param name="connectionString">Connection string for the instance of SQL Server that was used in a previous <see cref="M:System.Data.SqlClient.SqlDependency.Start(System.String)" /> call.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="connectionString" /> parameter is NULL. </exception>
		/// <exception cref="T:System.InvalidOperationException">The method was called from within SQLCLR.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Data.SqlClient.SqlClientPermission" /> code access security (CAS) permission.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">An underlying SqlClient exception occurred.</exception>
		// Token: 0x06001370 RID: 4976 RVA: 0x00051734 File Offset: 0x0004F934
		[MonoTODO]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
		public static bool Stop(string connectionString)
		{
			return true;
		}

		/// <summary>Stops a listener for a connection specified in a previous <see cref="Overload:System.Data.SqlClient.SqlDependency.Start" /> call.</summary>
		/// <returns>true if the listener was completely stopped; false if the <see cref="T:System.AppDomain" /> was unbound from the listener, but there is at least one other <see cref="T:System.AppDomain" /> using the same listener.</returns>
		/// <param name="connectionString">Connection string for the instance of SQL Server that was used in a previous <see cref="M:System.Data.SqlClient.SqlDependency.Start(System.String,System.String)" /> call.</param>
		/// <param name="queue">The SQL Server Service Broker queue that was used in a previous <see cref="M:System.Data.SqlClient.SqlDependency.Start(System.String,System.String)" /> call.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="connectionString" /> parameter is NULL. </exception>
		/// <exception cref="T:System.InvalidOperationException">The method was called from within SQLCLR.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required <see cref="T:System.Data.SqlClient.SqlClientPermission" /> code access security (CAS) permission.</exception>
		/// <exception cref="T:System.Data.SqlClient.SqlException">And underlying SqlClient exception occurred.</exception>
		// Token: 0x06001371 RID: 4977 RVA: 0x00051738 File Offset: 0x0004F938
		[MonoTODO]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
		public static bool Stop(string connectionString, string queue)
		{
			return true;
		}

		// Token: 0x040007E0 RID: 2016
		private string uniqueId = Guid.NewGuid().ToString();
	}
}
