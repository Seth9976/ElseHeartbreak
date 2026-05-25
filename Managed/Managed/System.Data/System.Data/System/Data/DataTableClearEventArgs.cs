using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="M:System.Data.DataTable.Clear" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000036 RID: 54
	public sealed class DataTableClearEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTableClearEventArgs" /> class.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> whose rows are being cleared.</param>
		// Token: 0x0600041B RID: 1051 RVA: 0x00019D9C File Offset: 0x00017F9C
		public DataTableClearEventArgs(DataTable table)
		{
			this._table = table;
		}

		/// <summary>Gets the table whose rows are being cleared.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> whose rows are being cleared.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00019DAC File Offset: 0x00017FAC
		public DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		/// <summary>Gets the table name whose rows are being cleared.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the table name.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00019DB4 File Offset: 0x00017FB4
		public string TableName
		{
			get
			{
				return this._table.TableName;
			}
		}

		/// <summary>Gets the namespace of the table whose rows are being cleared.</summary>
		/// <returns>A <see cref="T:System.String" /> indicating the namespace name.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00019DC4 File Offset: 0x00017FC4
		public string TableNamespace
		{
			get
			{
				return this._table.Namespace;
			}
		}

		// Token: 0x04000161 RID: 353
		private readonly DataTable _table;
	}
}
