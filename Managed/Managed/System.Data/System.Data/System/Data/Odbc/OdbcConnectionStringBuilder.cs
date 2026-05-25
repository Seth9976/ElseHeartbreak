using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Provides a simple way to create and manage the contents of connection strings used by the <see cref="T:System.Data.Odbc.OdbcConnection" /> class.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000120 RID: 288
	[DefaultProperty("Driver")]
	[TypeConverter("System.Data.Odbc.OdbcConnectionStringBuilder+OdbcConnectionStringBuilderConverter, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public sealed class OdbcConnectionStringBuilder : DbConnectionStringBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> class.</summary>
		// Token: 0x06001026 RID: 4134 RVA: 0x0003ECC8 File Offset: 0x0003CEC8
		public OdbcConnectionStringBuilder()
			: base(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> class. The provided connection string provides the data for the instance's internal connection information.</summary>
		/// <param name="connectionString">The basis for the object's internal connection information. Parsed into key/value pairs.</param>
		/// <exception cref="T:System.ArgumentException">The connection string is incorrectly formatted (perhaps missing the required "=" within a key/value pair).</exception>
		// Token: 0x06001027 RID: 4135 RVA: 0x0003ECD4 File Offset: 0x0003CED4
		public OdbcConnectionStringBuilder(string connectionString)
			: base(true)
		{
			if (connectionString == null)
			{
				base.ConnectionString = string.Empty;
				return;
			}
			base.ConnectionString = connectionString;
		}

		/// <summary>Gets or sets the value associated with the specified key. In C#, this property is the indexer.</summary>
		/// <returns>The value associated with the specified key.</returns>
		/// <param name="keyword">The key of the item to get or set.</param>
		/// <exception cref="T:System.ArgumentException">The connection string is incorrectly formatted (perhaps missing the required "=" within a key/value pair).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002B2 RID: 690
		public override object this[string keyword]
		{
			get
			{
				if (keyword == null)
				{
					throw new ArgumentNullException("keyword");
				}
				if (string.Compare(keyword, "Driver", StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return this.Driver;
				}
				if (string.Compare(keyword, "Dsn", StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return this.Dsn;
				}
				return base[keyword];
			}
			set
			{
				if (value == null)
				{
					this.Remove(keyword);
					return;
				}
				if (keyword == null)
				{
					throw new ArgumentNullException("keyword");
				}
				string text = value.ToString();
				if (string.Compare(keyword, "Driver", StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					this.Driver = text;
					return;
				}
				if (string.Compare(keyword, "Dsn", StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					this.dsn = text;
				}
				else if (value.ToString().IndexOf(';') != -1)
				{
					text = "{" + text + "}";
				}
				base[keyword] = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0003EDF4 File Offset: 0x0003CFF4
		public override ICollection Keys
		{
			get
			{
				List<string> list = new List<string>();
				list.Add("Dsn");
				list.Add("Driver");
				ICollection keys = base.Keys;
				foreach (object obj in keys)
				{
					string text = (string)obj;
					if (string.Compare(text, "Driver", StringComparison.InvariantCultureIgnoreCase) != 0)
					{
						if (string.Compare(text, "Dsn", StringComparison.InvariantCultureIgnoreCase) != 0)
						{
							list.Add(text);
						}
					}
				}
				string[] array = new string[list.Count];
				list.CopyTo(array);
				return array;
			}
		}

		/// <summary>Gets or sets the name of the ODBC driver associated with the connection.</summary>
		/// <returns>The value of the <see cref="P:System.Data.Odbc.OdbcConnectionStringBuilder.Driver" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0003EEC8 File Offset: 0x0003D0C8
		// (set) Token: 0x0600102C RID: 4140 RVA: 0x0003EEE4 File Offset: 0x0003D0E4
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Driver")]
		public string Driver
		{
			get
			{
				if (this.driver == null)
				{
					return string.Empty;
				}
				return this.driver;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Driver");
				}
				this.driver = value;
				if (value.Length > 0)
				{
					int num = value.IndexOf('{');
					int num2 = value.IndexOf('}');
					if (num == -1 || num2 == -1)
					{
						value = "{" + value + "}";
					}
					else if (num > 0 || num2 < value.Length - 1)
					{
						value = "{" + value + "}";
					}
				}
				base["Driver"] = value;
			}
		}

		/// <summary>Gets or sets the name of the data source name (DSN) associated with the connection.</summary>
		/// <returns>The value of the <see cref="P:System.Data.Odbc.OdbcConnectionStringBuilder.Dsn" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0003EF80 File Offset: 0x0003D180
		// (set) Token: 0x0600102E RID: 4142 RVA: 0x0003EF9C File Offset: 0x0003D19C
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Dsn")]
		public string Dsn
		{
			get
			{
				if (this.dsn == null)
				{
					return string.Empty;
				}
				return this.dsn;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Dsn");
				}
				this.dsn = value;
				base["Dsn"] = this.dsn;
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> contains an element that has the specified key; otherwise false.</returns>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (Nothing in Visual Basic).</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600102F RID: 4143 RVA: 0x0003EFC8 File Offset: 0x0003D1C8
		public override bool ContainsKey(string keyword)
		{
			if (keyword == null)
			{
				throw new ArgumentNullException("keyword");
			}
			return string.Compare(keyword, "Driver", StringComparison.InvariantCultureIgnoreCase) == 0 || string.Compare(keyword, "Dsn", StringComparison.InvariantCultureIgnoreCase) == 0 || base.ContainsKey(keyword);
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> instance.</summary>
		/// <returns>true if the key existed within the connection string and was removed; false if the key did not exist.</returns>
		/// <param name="keyword">The key of the key/value pair to be removed from the connection string in this <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (Nothing in Visual Basic).</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001030 RID: 4144 RVA: 0x0003F014 File Offset: 0x0003D214
		public override bool Remove(string keyword)
		{
			if (keyword == null)
			{
				throw new ArgumentNullException("keyword");
			}
			if (string.Compare(keyword, "Driver", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				this.driver = string.Empty;
			}
			else if (string.Compare(keyword, "Dsn", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				this.dsn = string.Empty;
			}
			return base.Remove(keyword);
		}

		/// <summary>Clears the contents of the <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" /> instance.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001031 RID: 4145 RVA: 0x0003F078 File Offset: 0x0003D278
		public override void Clear()
		{
			this.driver = null;
			this.dsn = null;
			base.Clear();
		}

		/// <summary>Retrieves a value corresponding to the supplied key from this <see cref="T:System.Data.Odbc.OdbcConnectionStringBuilder" />.</summary>
		/// <returns>true if <paramref name="keyword" /> was found within the connection string; otherwise false.</returns>
		/// <param name="keyword">The key of the item to retrieve.</param>
		/// <param name="value">The value corresponding to <paramref name="keyword." /></param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001032 RID: 4146 RVA: 0x0003F090 File Offset: 0x0003D290
		public override bool TryGetValue(string keyword, out object value)
		{
			if (keyword == null)
			{
				throw new ArgumentNullException("keyword");
			}
			bool flag = base.TryGetValue(keyword, out value);
			if (flag)
			{
				return flag;
			}
			if (string.Compare(keyword, "Driver", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				value = string.Empty;
				return true;
			}
			if (string.Compare(keyword, "Dsn", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				value = string.Empty;
				return true;
			}
			return false;
		}

		// Token: 0x0400055C RID: 1372
		private string driver;

		// Token: 0x0400055D RID: 1373
		private string dsn;
	}
}
