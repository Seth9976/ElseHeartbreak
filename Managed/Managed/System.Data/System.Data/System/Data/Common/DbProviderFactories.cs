using System;
using System.Configuration;
using System.Reflection;
using System.Threading;

namespace System.Data.Common
{
	/// <summary>Represents a set of static methods for creating one or more instances of <see cref="T:System.Data.Common.DbProviderFactory" /> classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CC RID: 204
	public static class DbProviderFactories
	{
		/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified <see cref="T:System.Data.DataRow" />.</returns>
		/// <param name="providerRow">
		///   <see cref="T:System.Data.DataRow" /> containing the provider's configuration information.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060009F2 RID: 2546 RVA: 0x0002EED0 File Offset: 0x0002D0D0
		public static DbProviderFactory GetFactory(DataRow providerRow)
		{
			string text = (string)providerRow["AssemblyQualifiedName"];
			Type type = Type.GetType(text, false, true);
			if (type != null && type.IsSubclassOf(typeof(DbProviderFactory)))
			{
				FieldInfo field = type.GetField("Instance", BindingFlags.Static | BindingFlags.Public);
				if (field != null)
				{
					return field.GetValue(null) as DbProviderFactory;
				}
			}
			throw new ConfigurationErrorsException("Failed to find or load the registered .Net Framework Data Provider.");
		}

		/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified provider name.</returns>
		/// <param name="providerInvariantName">Invariant name of a provider.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060009F3 RID: 2547 RVA: 0x0002EF40 File Offset: 0x0002D140
		public static DbProviderFactory GetFactory(string providerInvariantName)
		{
			DataTable factoryClasses = DbProviderFactories.GetFactoryClasses();
			if (factoryClasses != null)
			{
				DataRow dataRow = factoryClasses.Rows.Find(providerInvariantName);
				if (dataRow != null)
				{
					return DbProviderFactories.GetFactory(dataRow);
				}
			}
			throw new ConfigurationErrorsException(string.Format("Failed to find or load the registered .Net Framework Data Provider '{0}'.", providerInvariantName));
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that contains information about all installed providers that implement <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <returns>Returns a <see cref="T:System.Data.DataTable" /> containing <see cref="T:System.Data.DataRow" /> objects that contain the following data. Column ordinalColumn nameDescription0NameHuman-readable name for the data provider.1DescriptionHuman-readable description of the data provider.2InvariantNameName that can be used programmatically to refer to the data provider.3AssemblyQualifiedNameFully qualified name of the factory class, which contains enough information to instantiate the object.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060009F4 RID: 2548 RVA: 0x0002EF84 File Offset: 0x0002D184
		public static DataTable GetFactoryClasses()
		{
			DataSet dataSet = DbProviderFactories.GetConfigEntries();
			DataTable dataTable = ((dataSet == null) ? null : dataSet.Tables["DbProviderFactories"]);
			if (dataTable != null)
			{
				dataTable = dataTable.Copy();
			}
			return dataTable;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002EFC4 File Offset: 0x0002D1C4
		internal static DataSet GetConfigEntries()
		{
			if (DbProviderFactories.configEntries != null)
			{
				return DbProviderFactories.configEntries as DataSet;
			}
			DataSet dataSet = (DataSet)ConfigurationManager.GetSection("system.data");
			Interlocked.CompareExchange(ref DbProviderFactories.configEntries, dataSet, null);
			return DbProviderFactories.configEntries as DataSet;
		}

		// Token: 0x0400036D RID: 877
		internal const string CONFIG_SECTION_NAME = "system.data";

		// Token: 0x0400036E RID: 878
		internal const string CONFIG_SEC_TABLE_NAME = "DbProviderFactories";

		// Token: 0x0400036F RID: 879
		private static object configEntries;
	}
}
