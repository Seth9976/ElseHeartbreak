using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using Mono.Data.SqlExpressions;

namespace System.Data
{
	/// <summary>Represents the schema of a column in a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200001C RID: 28
	[DefaultProperty("ColumnName")]
	[Editor("Microsoft.VSDesigner.Data.Design.DataColumnEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	public class DataColumn : MarshalByValueComponent
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.DataColumn" /> class as type string.</summary>
		// Token: 0x0600010E RID: 270 RVA: 0x00009888 File Offset: 0x00007A88
		public DataColumn()
			: this(string.Empty, typeof(string), string.Empty, MappingType.Element)
		{
		}

		/// <summary>Inititalizes a new instance of the <see cref="T:System.Data.DataColumn" /> class, as type string, using the specified column name.</summary>
		/// <param name="columnName">A string that represents the name of the column to be created. If set to null or an empty string (""), a default name will be specified when added to the columns collection. </param>
		// Token: 0x0600010F RID: 271 RVA: 0x000098A8 File Offset: 0x00007AA8
		public DataColumn(string columnName)
			: this(columnName, typeof(string), string.Empty, MappingType.Element)
		{
		}

		/// <summary>Inititalizes a new instance of the <see cref="T:System.Data.DataColumn" /> class using the specified column name and data type.</summary>
		/// <param name="columnName">A string that represents the name of the column to be created. If set to null or an empty string (""), a default name will be specified when added to the columns collection. </param>
		/// <param name="dataType">A supported <see cref="P:System.Data.DataColumn.DataType" />. </param>
		/// <exception cref="T:System.ArgumentNullException">No <paramref name="dataType" /> was specified. </exception>
		// Token: 0x06000110 RID: 272 RVA: 0x000098C4 File Offset: 0x00007AC4
		public DataColumn(string columnName, Type dataType)
			: this(columnName, dataType, string.Empty, MappingType.Element)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumn" /> class using the specified name, data type, and expression.</summary>
		/// <param name="columnName">A string that represents the name of the column to be created. If set to null or an empty string (""), a default name will be specified when added to the columns collection. </param>
		/// <param name="dataType">A supported <see cref="P:System.Data.DataColumn.DataType" />. </param>
		/// <param name="expr">The expression used to create this column. For more information, see the <see cref="P:System.Data.DataColumn.Expression" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">No <paramref name="dataType" /> was specified. </exception>
		// Token: 0x06000111 RID: 273 RVA: 0x000098D4 File Offset: 0x00007AD4
		public DataColumn(string columnName, Type dataType, string expr)
			: this(columnName, dataType, expr, MappingType.Element)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumn" /> class using the specified name, data type, expression, and value that determines whether the column is an attribute.</summary>
		/// <param name="columnName">A string that represents the name of the column to be created. If set to null or an empty string (""), a default name will be specified when added to the columns collection. </param>
		/// <param name="dataType">A supported <see cref="P:System.Data.DataColumn.DataType" />. </param>
		/// <param name="expr">The expression used to create this column. For more information, see the <see cref="P:System.Data.DataColumn.Expression" /> property. </param>
		/// <param name="type">One of the <see cref="T:System.Data.MappingType" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">No <paramref name="dataType" /> was specified. </exception>
		// Token: 0x06000112 RID: 274 RVA: 0x000098E0 File Offset: 0x00007AE0
		public DataColumn(string columnName, Type dataType, string expr, MappingType type)
		{
			this.ColumnName = ((columnName != null) ? columnName : string.Empty);
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			this.DataType = dataType;
			this.Expression = ((expr != null) ? expr : string.Empty);
			this.ColumnMapping = type;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000114 RID: 276 RVA: 0x000099AC File Offset: 0x00007BAC
		// (remove) Token: 0x06000115 RID: 277 RVA: 0x000099C0 File Offset: 0x00007BC0
		internal event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				this._eventHandlers.AddHandler(DataColumn._propertyChangedKey, value);
			}
			remove
			{
				this._eventHandlers.RemoveHandler(DataColumn._propertyChangedKey, value);
			}
		}

		// Token: 0x1700000C RID: 12
		internal object this[int index]
		{
			get
			{
				return this.DataContainer[index];
			}
			set
			{
				if (value == null)
				{
					if (this.AutoIncrement)
					{
						goto IL_0064;
					}
				}
				try
				{
					this.DataContainer[index] = value;
				}
				catch (Exception ex)
				{
					throw new ArgumentException(string.Format("{0}. Couldn't store <{1}> in Column named '{2}'. Expected type is {3}.", new object[]
					{
						ex.Message,
						value,
						this.ColumnName,
						this.DataType.Name
					}), ex);
				}
				IL_0064:
				if (this.AutoIncrement && !this.DataContainer.IsNull(index))
				{
					long num = Convert.ToInt64(value);
					this.UpdateAutoIncrementValue(num);
				}
			}
		}

		/// <summary>Gets or sets the DateTimeMode for the column.</summary>
		/// <returns>The <see cref="T:System.Data.DataSetDateTime" /> for the specified column.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00009A9C File Offset: 0x00007C9C
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00009AA4 File Offset: 0x00007CA4
		[DefaultValue(DataSetDateTime.UnspecifiedLocal)]
		[RefreshProperties(RefreshProperties.All)]
		public DataSetDateTime DateTimeMode
		{
			get
			{
				return this._datetimeMode;
			}
			set
			{
				if (this.DataType != typeof(DateTime))
				{
					throw new InvalidOperationException("The DateTimeMode can be set only on DataColumns of type DateTime.");
				}
				if (!Enum.IsDefined(typeof(DataSetDateTime), value))
				{
					throw new InvalidEnumArgumentException(string.Format(CultureInfo.InvariantCulture, "The {0} enumeration value, {1}, is invalid", new object[]
					{
						typeof(DataSetDateTime).Name,
						value
					}));
				}
				if (this._datetimeMode == value)
				{
					return;
				}
				if (this._table == null || this._table.Rows.Count == 0)
				{
					this._datetimeMode = value;
					return;
				}
				if ((this._datetimeMode == DataSetDateTime.Unspecified || this._datetimeMode == DataSetDateTime.UnspecifiedLocal) && (value == DataSetDateTime.Unspecified || value == DataSetDateTime.UnspecifiedLocal))
				{
					this._datetimeMode = value;
					return;
				}
				throw new InvalidOperationException(string.Format("Cannot change DateTimeMode from '{0}' to '{1}' once the table has data.", this._datetimeMode, value));
			}
		}

		/// <summary>Gets or sets a value that indicates whether null values are allowed in this column for rows that belong to the table.</summary>
		/// <returns>true if null values values are allowed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00009BA4 File Offset: 0x00007DA4
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00009BAC File Offset: 0x00007DAC
		[DefaultValue(true)]
		[DataCategory("Data")]
		public bool AllowDBNull
		{
			get
			{
				return this._allowDBNull;
			}
			set
			{
				if (!value && this._table != null)
				{
					for (int i = 0; i < this._table.Rows.Count; i++)
					{
						DataRow dataRow = this._table.Rows[i];
						DataRowVersion dataRowVersion = ((!dataRow.HasVersion(DataRowVersion.Default)) ? DataRowVersion.Original : DataRowVersion.Default);
						if (dataRow.IsNull(this, dataRowVersion))
						{
							throw new DataException("Column '" + this.ColumnName + "' has null values in it.");
						}
					}
				}
				this._allowDBNull = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the column automatically increments the value of the column for new rows added to the table.</summary>
		/// <returns>true if the value of the column increments automatically; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">The column is a computed column. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00009C4C File Offset: 0x00007E4C
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00009C54 File Offset: 0x00007E54
		[DefaultValue(false)]
		[DataCategory("Data")]
		[RefreshProperties(RefreshProperties.All)]
		public bool AutoIncrement
		{
			get
			{
				return this._autoIncrement;
			}
			set
			{
				if (value)
				{
					if (this.Expression != string.Empty)
					{
						throw new ArgumentException("Can not Auto Increment a computed column.");
					}
					if (this.DefaultValue != DBNull.Value)
					{
						throw new ArgumentException("Can not set AutoIncrement while default value exists for this column.");
					}
					if (!DataColumn.CanAutoIncrement(this.DataType))
					{
						this.DataType = typeof(int);
					}
				}
				if (this._table != null)
				{
					this._table.Columns.UpdateAutoIncrement(this, value);
				}
				this._autoIncrement = value;
			}
		}

		/// <summary>Gets or sets the starting value for a column that has its <see cref="P:System.Data.DataColumn.AutoIncrement" /> property set to true.</summary>
		/// <returns>The starting value for the <see cref="P:System.Data.DataColumn.AutoIncrement" /> feature.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00009CE8 File Offset: 0x00007EE8
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00009CF0 File Offset: 0x00007EF0
		[DefaultValue(0)]
		[DataCategory("Data")]
		public long AutoIncrementSeed
		{
			get
			{
				return this._autoIncrementSeed;
			}
			set
			{
				this._autoIncrementSeed = value;
				this._nextAutoIncrementValue = this._autoIncrementSeed;
			}
		}

		/// <summary>Gets or sets the increment used by a column with its <see cref="P:System.Data.DataColumn.AutoIncrement" /> property set to true.</summary>
		/// <returns>The number by which the value of the column is automatically incremented. The default is 1.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is zero. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00009D08 File Offset: 0x00007F08
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00009D10 File Offset: 0x00007F10
		[DefaultValue(1)]
		[DataCategory("Data")]
		public long AutoIncrementStep
		{
			get
			{
				return this._autoIncrementStep;
			}
			set
			{
				this._autoIncrementStep = value;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00009D1C File Offset: 0x00007F1C
		internal void UpdateAutoIncrementValue(long value64)
		{
			if (this._autoIncrementStep > 0L)
			{
				if (value64 >= this._nextAutoIncrementValue)
				{
					this._nextAutoIncrementValue = value64;
					this.AutoIncrementValue();
				}
			}
			else if (value64 <= this._nextAutoIncrementValue)
			{
				this.AutoIncrementValue();
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00009D68 File Offset: 0x00007F68
		internal long AutoIncrementValue()
		{
			long nextAutoIncrementValue = this._nextAutoIncrementValue;
			this._nextAutoIncrementValue += this.AutoIncrementStep;
			return nextAutoIncrementValue;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00009D90 File Offset: 0x00007F90
		internal long GetAutoIncrementValue()
		{
			return this._nextAutoIncrementValue;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00009D98 File Offset: 0x00007F98
		internal void SetDefaultValue(int index)
		{
			if (this.AutoIncrement)
			{
				this[index] = this._nextAutoIncrementValue;
			}
			else
			{
				this.DataContainer.CopyValue(this.Table.DefaultValuesRowIndex, index);
			}
		}

		/// <summary>Gets or sets the caption for the column.</summary>
		/// <returns>The caption of the column. If not set, returns the <see cref="P:System.Data.DataColumn.ColumnName" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00009DE0 File Offset: 0x00007FE0
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00009E00 File Offset: 0x00008000
		[DataCategory("Data")]
		public string Caption
		{
			get
			{
				return (this._caption != null) ? this._caption : this.ColumnName;
			}
			set
			{
				this._caption = ((value != null) ? value : string.Empty);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.MappingType" /> of the column.</summary>
		/// <returns>One of the <see cref="T:System.Data.MappingType" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00009E1C File Offset: 0x0000801C
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00009E24 File Offset: 0x00008024
		[DefaultValue(MappingType.Element)]
		public virtual MappingType ColumnMapping
		{
			get
			{
				return this._columnMapping;
			}
			set
			{
				this._columnMapping = value;
			}
		}

		/// <summary>Gets or sets the name of the column in the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The name of the column.</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to null or an empty string and the column belongs to a collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">A column with the same name already exists in the collection. The name comparison is not case sensitive. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00009E30 File Offset: 0x00008030
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00009E38 File Offset: 0x00008038
		[DataCategory("Data")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		public string ColumnName
		{
			get
			{
				return this._columnName;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				CultureInfo cultureInfo = ((this.Table == null) ? CultureInfo.CurrentCulture : this.Table.Locale);
				if (string.Compare(value, this._columnName, true, cultureInfo) != 0)
				{
					if (this.Table != null)
					{
						if (value.Length == 0)
						{
							throw new ArgumentException("ColumnName is required when it is part of a DataTable.");
						}
						this.Table.Columns.RegisterName(value, this);
						if (this._columnName.Length > 0)
						{
							this.Table.Columns.UnregisterName(this._columnName);
						}
					}
					this.RaisePropertyChanging("ColumnName");
					this._columnName = value;
					if (this.Table != null)
					{
						this.Table.ResetPropertyDescriptorsCache();
					}
				}
				else if (string.Compare(value, this._columnName, false, cultureInfo) != 0)
				{
					this.RaisePropertyChanging("ColumnName");
					this._columnName = value;
					if (this.Table != null)
					{
						this.Table.ResetPropertyDescriptorsCache();
					}
				}
			}
		}

		/// <summary>Gets or sets the type of data stored in the column.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the column data type.</returns>
		/// <exception cref="T:System.ArgumentException">The column already has data stored. - or -<see cref="P:System.Data.DataColumn.AutoIncrement" /> is true, but the value is set to a type a unsupported by <see cref="P:System.Data.DataColumn.AutoIncrement" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00009F48 File Offset: 0x00008148
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00009F58 File Offset: 0x00008158
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(ColumnTypeConverter))]
		[DataCategory("Data")]
		[DefaultValue(typeof(string))]
		public Type DataType
		{
			get
			{
				return this.DataContainer.Type;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				if (this._dataContainer != null)
				{
					if (value == this._dataContainer.Type)
					{
						return;
					}
					if (this._dataContainer.Capacity > 0)
					{
						throw new ArgumentException("The column already has data stored.");
					}
				}
				if (this.GetParentRelation() != null || this.GetChildRelation() != null)
				{
					throw new InvalidConstraintException("Cannot change datatype when column is part of a relation");
				}
				Type type = ((this._dataContainer == null) ? null : this._dataContainer.Type);
				if (this._dataContainer != null && this._dataContainer.Type == typeof(DateTime))
				{
					this._datetimeMode = DataSetDateTime.UnspecifiedLocal;
				}
				this._dataContainer = DataContainer.Create(value, this);
				if (this.AutoIncrement && !DataColumn.CanAutoIncrement(value))
				{
					this.AutoIncrement = false;
				}
				if (this.DefaultValue != DataColumn.GetDefaultValueForType(type))
				{
					this.SetDefaultValue(this.DefaultValue, true);
				}
				else
				{
					this._defaultValue = DataColumn.GetDefaultValueForType(this.DataType);
				}
			}
		}

		/// <summary>Gets or sets the default value for the column when you are creating new rows.</summary>
		/// <returns>A value appropriate to the column's <see cref="P:System.Data.DataColumn.DataType" />.</returns>
		/// <exception cref="T:System.InvalidCastException">When you are adding a row, the default value is not an instance of the column's data type. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000A070 File Offset: 0x00008270
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000A078 File Offset: 0x00008278
		[DataCategory("Data")]
		[TypeConverter(typeof(DefaultValueTypeConverter))]
		public object DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
			set
			{
				if (this.AutoIncrement)
				{
					throw new ArgumentException("Can not set default value while AutoIncrement is true on this column.");
				}
				this.SetDefaultValue(value, false);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000A098 File Offset: 0x00008298
		private void SetDefaultValue(object value, bool forcedTypeCheck)
		{
			if (forcedTypeCheck || !this._defaultValue.Equals(value))
			{
				if (value == null || value == DBNull.Value)
				{
					this._defaultValue = DataColumn.GetDefaultValueForType(this.DataType);
				}
				else if (this.DataType.IsInstanceOfType(value))
				{
					this._defaultValue = value;
				}
				else
				{
					try
					{
						this._defaultValue = Convert.ChangeType(value, this.DataType);
					}
					catch (InvalidCastException)
					{
						string text = string.Format("Default Value of type '{0}' is not compatible with column type '{1}'", value.GetType(), this.DataType);
						throw new DataException(text);
					}
				}
			}
			if (this.Table != null && this.Table.DefaultValuesRowIndex != -1)
			{
				this.DataContainer[this.Table.DefaultValuesRowIndex] = this._defaultValue;
			}
		}

		/// <summary>Gets or sets the expression used to filter rows, calculate the values in a column, or create an aggregate column.</summary>
		/// <returns>An expression to calculate the value of a column, or create an aggregate column. The return type of an expression is determined by the <see cref="P:System.Data.DataColumn.DataType" /> of the column.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Data.DataColumn.AutoIncrement" /> or <see cref="P:System.Data.DataColumn.Unique" /> property is set to true. </exception>
		/// <exception cref="T:System.FormatException">When you are using the CONVERT function, the expression evaluates to a string, but the string does not contain a representation that can be converted to the type parameter. </exception>
		/// <exception cref="T:System.InvalidCastException">When you are using the CONVERT function, the requested cast is not possible. See the Conversion function in the following section for detailed information about possible casts. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">When you use the SUBSTRING function, the start argument is out of range.-Or- When you use the SUBSTRING function, the length argument is out of range. </exception>
		/// <exception cref="T:System.Exception">When you use the LEN function or the TRIM function, the expression does not evaluate to a string. This includes expressions that evaluate to <see cref="T:System.Char" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000A190 File Offset: 0x00008390
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000A198 File Offset: 0x00008398
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		[DataCategory("Data")]
		public string Expression
		{
			get
			{
				return this._expression;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value != string.Empty)
				{
					if (this.AutoIncrement || this.Unique)
					{
						throw new ArgumentException("Cannot create an expression on a column that has AutoIncrement or Unique.");
					}
					if (this.Table != null)
					{
						for (int i = 0; i < this.Table.Constraints.Count; i++)
						{
							if (this.Table.Constraints[i].IsColumnContained(this))
							{
								throw new ArgumentException(string.Format("Cannot set Expression property on column {0}, because it is a part of a constraint.", this.ColumnName));
							}
						}
					}
					Parser parser = new Parser();
					IExpression expression = parser.Compile(value);
					if (this.Table != null)
					{
						if (expression.DependsOn(this))
						{
							throw new ArgumentException("Cannot set Expression property due to circular reference in the expression.");
						}
						if (this.Table.Rows.Count == 0)
						{
							expression.Eval(this.Table.NewRow());
						}
						else
						{
							expression.Eval(this.Table.Rows[0]);
						}
					}
					this.ReadOnly = true;
					this._compiledExpression = expression;
				}
				else
				{
					this._compiledExpression = null;
					if (this.Table != null)
					{
						int defaultValuesRowIndex = this.Table.DefaultValuesRowIndex;
						if (defaultValuesRowIndex != -1)
						{
							this.DataContainer.FillValues(defaultValuesRowIndex);
						}
					}
				}
				this._expression = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000A300 File Offset: 0x00008500
		internal IExpression CompiledExpression
		{
			get
			{
				return this._compiledExpression;
			}
		}

		/// <summary>Gets the collection of custom user information associated with a <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> of custom information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000A308 File Offset: 0x00008508
		// (set) Token: 0x06000135 RID: 309 RVA: 0x0000A310 File Offset: 0x00008510
		[DataCategory("Data")]
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				return this._extendedProperties;
			}
			internal set
			{
				this._extendedProperties = value;
			}
		}

		/// <summary>Gets or sets the maximum length of a text column.</summary>
		/// <returns>The maximum length of the column in characters. If the column has no maximum length, the value is –1 (default).</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000A31C File Offset: 0x0000851C
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000A324 File Offset: 0x00008524
		[DefaultValue(-1)]
		[DataCategory("Data")]
		public int MaxLength
		{
			get
			{
				return this._maxLength;
			}
			set
			{
				if (value >= 0 && this._columnMapping == MappingType.SimpleContent)
				{
					throw new ArgumentException(string.Format("Cannot set MaxLength property on '{0}' column which is mapped to SimpleContent.", this.ColumnName));
				}
				this._maxLength = value;
			}
		}

		/// <summary>Gets or sets the namespace of the <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Data.DataColumn" />.</returns>
		/// <exception cref="T:System.ArgumentException">The namespace already has data. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000A364 File Offset: 0x00008564
		// (set) Token: 0x06000139 RID: 313 RVA: 0x0000A3AC File Offset: 0x000085AC
		[DataCategory("Data")]
		public string Namespace
		{
			get
			{
				if (this._nameSpace != null)
				{
					return this._nameSpace;
				}
				if (this.Table != null && this._columnMapping != MappingType.Attribute)
				{
					return this.Table.Namespace;
				}
				return string.Empty;
			}
			set
			{
				this._nameSpace = value;
			}
		}

		/// <summary>Gets the position of the column in the <see cref="T:System.Data.DataColumnCollection" /> collection.</summary>
		/// <returns>The position of the column. Gets -1 if the column is not a member of a collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000A3B8 File Offset: 0x000085B8
		// (set) Token: 0x0600013B RID: 315 RVA: 0x0000A3C0 File Offset: 0x000085C0
		[Browsable(false)]
		[DataCategory("Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Ordinal
		{
			get
			{
				return this._ordinal;
			}
			internal set
			{
				this._ordinal = value;
			}
		}

		/// <summary>Changes the ordinal or position of the <see cref="T:System.Data.DataColumn" /> to the specified ordinal or position.</summary>
		/// <param name="ordinal">The specified ordinal.</param>
		// Token: 0x0600013C RID: 316 RVA: 0x0000A3CC File Offset: 0x000085CC
		public void SetOrdinal(int ordinal)
		{
			if (this._ordinal == -1)
			{
				throw new ArgumentException("Column must belong to a table.");
			}
			this._table.Columns.MoveColumn(this._ordinal, ordinal);
			this._ordinal = ordinal;
		}

		/// <summary>Gets or sets an XML prefix that aliases the namespace of the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>The XML prefix for the <see cref="T:System.Data.DataTable" /> namespace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000A404 File Offset: 0x00008604
		// (set) Token: 0x0600013E RID: 318 RVA: 0x0000A40C File Offset: 0x0000860C
		[DataCategory("Data")]
		[DefaultValue("")]
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
			set
			{
				this._prefix = ((value != null) ? value : string.Empty);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the column allows for changes as soon as a row has been added to the table.</summary>
		/// <returns>true if the column is read only; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">The property is set to false on a computed column. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000A428 File Offset: 0x00008628
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000A430 File Offset: 0x00008630
		[DataCategory("Data")]
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return this._readOnly;
			}
			set
			{
				this._readOnly = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> to which the column belongs to.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> that the <see cref="T:System.Data.DataColumn" /> belongs to.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000A43C File Offset: 0x0000863C
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000A444 File Offset: 0x00008644
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DataCategory("Data")]
		public DataTable Table
		{
			get
			{
				return this._table;
			}
			internal set
			{
				this._table = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the values in each row of the column must be unique.</summary>
		/// <returns>true if the value must be unique; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.ArgumentException">The column is a calculated column. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000A450 File Offset: 0x00008650
		// (set) Token: 0x06000144 RID: 324 RVA: 0x0000A458 File Offset: 0x00008658
		[DefaultValue(false)]
		[DataCategory("Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Unique
		{
			get
			{
				return this._unique;
			}
			set
			{
				if (this._unique == value)
				{
					return;
				}
				this._unique = value;
				if (this._table == null)
				{
					return;
				}
				try
				{
					if (value)
					{
						if (this.Expression != null && this.Expression != string.Empty)
						{
							throw new ArgumentException("Cannot change Unique property for the expression column.");
						}
						this._table.Constraints.Add(null, this, false);
					}
					else
					{
						UniqueConstraint uniqueConstraintForColumnSet = UniqueConstraint.GetUniqueConstraintForColumnSet(this._table.Constraints, new DataColumn[] { this });
						this._table.Constraints.Remove(uniqueConstraintForColumnSet);
					}
				}
				catch (Exception ex)
				{
					this._unique = !value;
					throw ex;
				}
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000A530 File Offset: 0x00008730
		internal DataContainer DataContainer
		{
			get
			{
				return this._dataContainer;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000A538 File Offset: 0x00008738
		internal static bool CanAutoIncrement(Type type)
		{
			TypeCode typeCode = Type.GetTypeCode(type);
			switch (typeCode)
			{
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				break;
			default:
				if (typeCode != TypeCode.Decimal)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000A578 File Offset: 0x00008778
		[MonoTODO]
		internal DataColumn Clone()
		{
			DataColumn dataColumn = new DataColumn();
			dataColumn._allowDBNull = this._allowDBNull;
			dataColumn._autoIncrement = this._autoIncrement;
			dataColumn._autoIncrementSeed = this._autoIncrementSeed;
			dataColumn._autoIncrementStep = this._autoIncrementStep;
			dataColumn._caption = this._caption;
			dataColumn._columnMapping = this._columnMapping;
			dataColumn._columnName = this._columnName;
			dataColumn.DataType = this.DataType;
			dataColumn._defaultValue = this._defaultValue;
			dataColumn.Expression = this._expression;
			dataColumn._maxLength = this._maxLength;
			dataColumn._nameSpace = this._nameSpace;
			dataColumn._prefix = this._prefix;
			dataColumn._readOnly = this._readOnly;
			if (this.DataType == typeof(DateTime))
			{
				dataColumn.DateTimeMode = this._datetimeMode;
			}
			dataColumn._extendedProperties = this._extendedProperties;
			return dataColumn;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000A664 File Offset: 0x00008864
		internal void SetUnique()
		{
			this._unique = true;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000A670 File Offset: 0x00008870
		[MonoTODO]
		internal void AssertCanAddToCollection()
		{
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x0600014A RID: 330 RVA: 0x0000A674 File Offset: 0x00008874
		[MonoTODO]
		protected internal void CheckNotAllowNull()
		{
			throw new NotImplementedException();
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x0600014B RID: 331 RVA: 0x0000A67C File Offset: 0x0000887C
		[MonoTODO]
		protected void CheckUnique()
		{
			throw new NotImplementedException();
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="pcevent">Parameter reference.</param>
		// Token: 0x0600014C RID: 332 RVA: 0x0000A684 File Offset: 0x00008884
		protected internal virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			PropertyChangedEventHandler propertyChangedEventHandler = this._eventHandlers[DataColumn._propertyChangedKey] as PropertyChangedEventHandler;
			if (propertyChangedEventHandler != null)
			{
				propertyChangedEventHandler(this, pcevent);
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="name">Parameter reference.</param>
		// Token: 0x0600014D RID: 333 RVA: 0x0000A6B8 File Offset: 0x000088B8
		protected internal void RaisePropertyChanging(string name)
		{
			PropertyChangedEventArgs propertyChangedEventArgs = new PropertyChangedEventArgs(name);
			this.OnPropertyChanging(propertyChangedEventArgs);
		}

		/// <summary>Gets the <see cref="P:System.Data.DataColumn.Expression" /> of the column, if one exists.</summary>
		/// <returns>The <see cref="P:System.Data.DataColumn.Expression" /> value, if the property is set; otherwise, the <see cref="P:System.Data.DataColumn.ColumnName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600014E RID: 334 RVA: 0x0000A6D4 File Offset: 0x000088D4
		public override string ToString()
		{
			if (this._expression != string.Empty)
			{
				return this.ColumnName + " + " + this._expression;
			}
			return this.ColumnName;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A714 File Offset: 0x00008914
		internal void SetTable(DataTable table)
		{
			if (this._table != null)
			{
				throw new ArgumentException("The column already belongs to a different table");
			}
			this._table = table;
			if (this._unique)
			{
				UniqueConstraint uniqueConstraint = new UniqueConstraint(this);
				this._table.Constraints.Add(uniqueConstraint);
			}
			this.DataContainer.Capacity = this._table.RecordCache.CurrentCapacity;
			int defaultValuesRowIndex = this._table.DefaultValuesRowIndex;
			if (defaultValuesRowIndex != -1)
			{
				this.DataContainer[defaultValuesRowIndex] = this._defaultValue;
				this.DataContainer.FillValues(defaultValuesRowIndex);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000A7B0 File Offset: 0x000089B0
		internal static bool AreColumnSetsTheSame(DataColumn[] columnSet, DataColumn[] compareSet)
		{
			if (columnSet == null && compareSet == null)
			{
				return true;
			}
			if (columnSet == null || compareSet == null)
			{
				return false;
			}
			if (columnSet.Length != compareSet.Length)
			{
				return false;
			}
			foreach (DataColumn dataColumn in columnSet)
			{
				bool flag = false;
				foreach (DataColumn dataColumn2 in compareSet)
				{
					if (dataColumn == dataColumn2)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000A838 File Offset: 0x00008A38
		internal int CompareValues(int index1, int index2)
		{
			return this.DataContainer.CompareValues(index1, index2);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000A848 File Offset: 0x00008A48
		private DataRelation GetParentRelation()
		{
			if (this._table == null)
			{
				return null;
			}
			foreach (object obj in this._table.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Contains(this))
				{
					return dataRelation;
				}
			}
			return null;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000A8D8 File Offset: 0x00008AD8
		private DataRelation GetChildRelation()
		{
			if (this._table == null)
			{
				return null;
			}
			foreach (object obj in this._table.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Contains(this))
				{
					return dataRelation;
				}
			}
			return null;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000A968 File Offset: 0x00008B68
		internal void ResetColumnInfo()
		{
			this._ordinal = -1;
			this._table = null;
			if (this._compiledExpression != null)
			{
				this._compiledExpression.ResetExpression();
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A99C File Offset: 0x00008B9C
		internal bool DataTypeMatches(DataColumn col)
		{
			return this.DataType == col.DataType && (this.DataType != typeof(DateTime) || this.DateTimeMode == col.DateTimeMode || (this.DateTimeMode != DataSetDateTime.Local && this.DateTimeMode != DataSetDateTime.Utc && col.DateTimeMode != DataSetDateTime.Local && col.DateTimeMode != DataSetDateTime.Utc));
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000AA1C File Offset: 0x00008C1C
		internal static object GetDefaultValueForType(Type type)
		{
			if (type == null)
			{
				return DBNull.Value;
			}
			if (type.Namespace == "System.Data.SqlTypes" && type.Assembly == typeof(DataColumn).Assembly)
			{
				if (type == typeof(SqlBinary))
				{
					return SqlBinary.Null;
				}
				if (type == typeof(SqlBoolean))
				{
					return SqlBoolean.Null;
				}
				if (type == typeof(SqlByte))
				{
					return SqlByte.Null;
				}
				if (type == typeof(SqlBytes))
				{
					return SqlBytes.Null;
				}
				if (type == typeof(SqlChars))
				{
					return SqlChars.Null;
				}
				if (type == typeof(SqlDateTime))
				{
					return SqlDateTime.Null;
				}
				if (type == typeof(SqlDecimal))
				{
					return SqlDecimal.Null;
				}
				if (type == typeof(SqlDouble))
				{
					return SqlDouble.Null;
				}
				if (type == typeof(SqlGuid))
				{
					return SqlGuid.Null;
				}
				if (type == typeof(SqlInt16))
				{
					return SqlInt16.Null;
				}
				if (type == typeof(SqlInt32))
				{
					return SqlInt32.Null;
				}
				if (type == typeof(SqlInt64))
				{
					return SqlInt64.Null;
				}
				if (type == typeof(SqlMoney))
				{
					return SqlMoney.Null;
				}
				if (type == typeof(SqlSingle))
				{
					return SqlSingle.Null;
				}
				if (type == typeof(SqlString))
				{
					return SqlString.Null;
				}
				if (type == typeof(SqlXml))
				{
					return SqlXml.Null;
				}
			}
			return DBNull.Value;
		}

		// Token: 0x040000A2 RID: 162
		private EventHandlerList _eventHandlers = new EventHandlerList();

		// Token: 0x040000A3 RID: 163
		private static readonly object _propertyChangedKey = new object();

		// Token: 0x040000A4 RID: 164
		private bool _allowDBNull = true;

		// Token: 0x040000A5 RID: 165
		private bool _autoIncrement;

		// Token: 0x040000A6 RID: 166
		private long _autoIncrementSeed;

		// Token: 0x040000A7 RID: 167
		private long _autoIncrementStep = 1L;

		// Token: 0x040000A8 RID: 168
		private long _nextAutoIncrementValue;

		// Token: 0x040000A9 RID: 169
		private string _caption;

		// Token: 0x040000AA RID: 170
		private MappingType _columnMapping;

		// Token: 0x040000AB RID: 171
		private string _columnName = string.Empty;

		// Token: 0x040000AC RID: 172
		private object _defaultValue = DataColumn.GetDefaultValueForType(null);

		// Token: 0x040000AD RID: 173
		private string _expression;

		// Token: 0x040000AE RID: 174
		private IExpression _compiledExpression;

		// Token: 0x040000AF RID: 175
		private PropertyCollection _extendedProperties = new PropertyCollection();

		// Token: 0x040000B0 RID: 176
		private int _maxLength = -1;

		// Token: 0x040000B1 RID: 177
		private string _nameSpace;

		// Token: 0x040000B2 RID: 178
		private int _ordinal = -1;

		// Token: 0x040000B3 RID: 179
		private string _prefix = string.Empty;

		// Token: 0x040000B4 RID: 180
		private bool _readOnly;

		// Token: 0x040000B5 RID: 181
		private DataTable _table;

		// Token: 0x040000B6 RID: 182
		private bool _unique;

		// Token: 0x040000B7 RID: 183
		private DataContainer _dataContainer;

		// Token: 0x040000B8 RID: 184
		private DataSetDateTime _datetimeMode = DataSetDateTime.UnspecifiedLocal;
	}
}
