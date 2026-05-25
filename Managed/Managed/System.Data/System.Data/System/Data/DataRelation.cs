using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Represents a parent/child relationship between two <see cref="T:System.Data.DataTable" /> objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000024 RID: 36
	[Editor("Microsoft.VSDesigner.Data.Design.DataRelationEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[TypeConverter(typeof(RelationshipConverter))]
	[DefaultProperty("RelationName")]
	public class DataRelation
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified <see cref="T:System.Data.DataRelation" /> name, and parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="relationName">The name of the <see cref="T:System.Data.DataRelation" />. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentColumn">The parent <see cref="T:System.Data.DataColumn" /> in the relationship. </param>
		/// <param name="childColumn">The child <see cref="T:System.Data.DataColumn" /> in the relationship. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects contains null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types -Or- The tables do not belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x060001A3 RID: 419 RVA: 0x0000C25C File Offset: 0x0000A45C
		public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn)
			: this(relationName, parentColumn, childColumn, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified <see cref="T:System.Data.DataRelation" /> name and matched arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="relationName">The name of the relation. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects contains null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The <see cref="T:System.Data.DataColumn" /> objects have different data types -Or- One or both of the arrays are not composed of distinct columns from the same table.-Or- The tables do not belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x060001A4 RID: 420 RVA: 0x0000C268 File Offset: 0x0000A468
		public DataRelation(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns)
			: this(relationName, parentColumns, childColumns, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified name, parent and child <see cref="T:System.Data.DataColumn" /> objects, and a value that indicates whether to create constraints.</summary>
		/// <param name="relationName">The name of the relation. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentColumn">The parent <see cref="T:System.Data.DataColumn" /> in the relation. </param>
		/// <param name="childColumn">The child <see cref="T:System.Data.DataColumn" /> in the relation. </param>
		/// <param name="createConstraints">A value that indicates whether constraints are created. true, if constraints are created. Otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects contains null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types -Or- The tables do not belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x060001A5 RID: 421 RVA: 0x0000C274 File Offset: 0x0000A474
		public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn, bool createConstraints)
			: this(relationName, new DataColumn[] { parentColumn }, new DataColumn[] { childColumn }, createConstraints)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified name, matched arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects, and value that indicates whether to create constraints.</summary>
		/// <param name="relationName">The name of the relation. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="createConstraints">A value that indicates whether to create constraints. true, if constraints are created. Otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types -Or- The tables do not belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x060001A6 RID: 422 RVA: 0x0000C294 File Offset: 0x0000A494
		public DataRelation(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			this.createConstraints = true;
			this._parentTableNameSpace = string.Empty;
			this._childTableNameSpace = string.Empty;
			base..ctor();
			this.extendedProperties = new PropertyCollection();
			this.relationName = ((relationName != null) ? relationName : string.Empty);
			if (parentColumns == null)
			{
				throw new ArgumentNullException("parentColumns");
			}
			this.parentColumns = parentColumns;
			if (childColumns == null)
			{
				throw new ArgumentNullException("childColumns");
			}
			this.childColumns = childColumns;
			this.createConstraints = createConstraints;
			if (parentColumns.Length != childColumns.Length)
			{
				throw new ArgumentException("ParentColumns and ChildColumns should be the same length");
			}
			DataTable table = parentColumns[0].Table;
			DataTable table2 = childColumns[0].Table;
			if (table.DataSet != table2.DataSet)
			{
				throw new InvalidConstraintException();
			}
			foreach (DataColumn dataColumn in parentColumns)
			{
				if (dataColumn.Table != table)
				{
					throw new InvalidConstraintException();
				}
			}
			foreach (DataColumn dataColumn2 in childColumns)
			{
				if (dataColumn2.Table != table2)
				{
					throw new InvalidConstraintException();
				}
			}
			for (int k = 0; k < this.ChildColumns.Length; k++)
			{
				if (!parentColumns[k].DataTypeMatches(childColumns[k]))
				{
					throw new InvalidConstraintException("Parent Columns and Child Columns don't have matching column types");
				}
			}
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio environment.</summary>
		/// <param name="relationName">The name of the relation. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the parent table of the relation. </param>
		/// <param name="childTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the child table of the relation. </param>
		/// <param name="parentColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the parent <see cref="T:System.Data.DataTable" /> of the relation. </param>
		/// <param name="childColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the child <see cref="T:System.Data.DataTable" /> of the relation. </param>
		/// <param name="nested">A value that indicates whether relationships are nested. </param>
		// Token: 0x060001A7 RID: 423 RVA: 0x0000C400 File Offset: 0x0000A600
		[Browsable(false)]
		public DataRelation(string relationName, string parentTableName, string childTableName, string[] parentColumnNames, string[] childColumnNames, bool nested)
		{
			this.createConstraints = true;
			this._parentTableNameSpace = string.Empty;
			this._childTableNameSpace = string.Empty;
			base..ctor();
			this._relationName = relationName;
			this._parentTableName = parentTableName;
			this._childTableName = childTableName;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._nested = nested;
			this.InitInProgress = true;
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio environment.</summary>
		/// <param name="relationName">The name of the <see cref="T:System.Data.DataRelation" />. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the parent table of the relation.</param>
		/// <param name="parentTableNamespace">The name of the parent table namespace.</param>
		/// <param name="childTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the child table of the relation. </param>
		/// <param name="childTableNamespace">The name of the child table namespace.</param>
		/// <param name="parentColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the parent <see cref="T:System.Data.DataTable" /> of the relation.</param>
		/// <param name="childColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the child <see cref="T:System.Data.DataTable" /> of the relation.</param>
		/// <param name="nested">A value that indicates whether relationships are nested.</param>
		// Token: 0x060001A8 RID: 424 RVA: 0x0000C464 File Offset: 0x0000A664
		[Browsable(false)]
		public DataRelation(string relationName, string parentTableName, string parentTableNameSpace, string childTableName, string childTableNameSpace, string[] parentColumnNames, string[] childColumnNames, bool nested)
		{
			this.createConstraints = true;
			this._parentTableNameSpace = string.Empty;
			this._childTableNameSpace = string.Empty;
			base..ctor();
			this._relationName = relationName;
			this._parentTableName = parentTableName;
			this._parentTableNameSpace = parentTableNameSpace;
			this._childTableName = childTableName;
			this._childTableNameSpace = childTableNameSpace;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._nested = nested;
			this.InitInProgress = true;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
		internal bool InitInProgress
		{
			get
			{
				return this.initInProgress;
			}
			set
			{
				this.initInProgress = value;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		internal void FinishInit(DataSet ds)
		{
			if (!ds.Tables.Contains(this._parentTableName) || !ds.Tables.Contains(this._childTableName))
			{
				throw new InvalidOperationException();
			}
			if (this._parentColumnNames.Length != this._childColumnNames.Length)
			{
				throw new InvalidOperationException();
			}
			DataTable dataTable = ds.Tables[this._parentTableName];
			DataTable dataTable2 = ds.Tables[this._childTableName];
			this.parentColumns = new DataColumn[this._parentColumnNames.Length];
			this.childColumns = new DataColumn[this._childColumnNames.Length];
			for (int i = 0; i < this._parentColumnNames.Length; i++)
			{
				if (!dataTable.Columns.Contains(this._parentColumnNames[i]))
				{
					throw new InvalidOperationException();
				}
				this.parentColumns[i] = dataTable.Columns[this._parentColumnNames[i]];
				if (!dataTable2.Columns.Contains(this._childColumnNames[i]))
				{
					throw new InvalidOperationException();
				}
				this.childColumns[i] = dataTable2.Columns[this._childColumnNames[i]];
			}
			this.RelationName = this._relationName;
			this.Nested = this._nested;
			this.initFinished = true;
			this.extendedProperties = new PropertyCollection();
			this.InitInProgress = false;
			if (this._parentTableNameSpace != string.Empty)
			{
				dataTable.Namespace = this._parentTableNameSpace;
			}
			if (this._childTableNameSpace != string.Empty)
			{
				dataTable2.Namespace = this._childTableNameSpace;
			}
		}

		/// <summary>Gets the child <see cref="T:System.Data.DataColumn" /> objects of this relation.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000C690 File Offset: 0x0000A890
		[DataCategory("Data")]
		public virtual DataColumn[] ChildColumns
		{
			get
			{
				return this.childColumns;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.ForeignKeyConstraint" /> for the relation.</summary>
		/// <returns>A <see cref="T:System.Data.ForeignKeyConstraint" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000C698 File Offset: 0x0000A898
		public virtual ForeignKeyConstraint ChildKeyConstraint
		{
			get
			{
				return this.childKeyConstraint;
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
		internal void SetChildKeyConstraint(ForeignKeyConstraint foreignKeyConstraint)
		{
			this.childKeyConstraint = foreignKeyConstraint;
		}

		/// <summary>Gets the child table of this relation.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that is the child table of the relation.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000C6AC File Offset: 0x0000A8AC
		public virtual DataTable ChildTable
		{
			get
			{
				return this.childColumns[0].Table;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataSet" /> to which the <see cref="T:System.Data.DataRelation" /> belongs.</summary>
		/// <returns>A <see cref="T:System.Data.DataSet" /> to which the <see cref="T:System.Data.DataRelation" /> belongs.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual DataSet DataSet
		{
			get
			{
				return this.childColumns[0].Table.DataSet;
			}
		}

		/// <summary>Gets the collection that stores customized properties.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> that contains customized properties.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000C6D0 File Offset: 0x0000A8D0
		[DataCategory("Data")]
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				if (this.extendedProperties == null)
				{
					this.extendedProperties = new PropertyCollection();
				}
				return this.extendedProperties;
			}
		}

		/// <summary>Gets or sets a value that indicates whether <see cref="T:System.Data.DataRelation" /> objects are nested.</summary>
		/// <returns>true, if <see cref="T:System.Data.DataRelation" /> objects are nested; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000C6F0 File Offset: 0x0000A8F0
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000C6F8 File Offset: 0x0000A8F8
		[DataCategory("Data")]
		[DefaultValue(false)]
		public virtual bool Nested
		{
			get
			{
				return this.nested;
			}
			set
			{
				this.nested = value;
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Data.DataColumn" /> objects that are the parent columns of this <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that are the parent columns of this <see cref="T:System.Data.DataRelation" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x0000C704 File Offset: 0x0000A904
		[DataCategory("Data")]
		public virtual DataColumn[] ParentColumns
		{
			get
			{
				return this.parentColumns;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.UniqueConstraint" /> that guarantees that values in the parent column of a <see cref="T:System.Data.DataRelation" /> are unique.</summary>
		/// <returns>A <see cref="T:System.Data.UniqueConstraint" /> that makes sure that values in a parent column are unique.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000C70C File Offset: 0x0000A90C
		public virtual UniqueConstraint ParentKeyConstraint
		{
			get
			{
				return this.parentKeyConstraint;
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000C714 File Offset: 0x0000A914
		internal void SetParentKeyConstraint(UniqueConstraint uniqueConstraint)
		{
			this.parentKeyConstraint = uniqueConstraint;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000C720 File Offset: 0x0000A920
		internal void SetDataSet(DataSet ds)
		{
			this.dataSet = ds;
		}

		/// <summary>Gets the parent <see cref="T:System.Data.DataTable" /> of this <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that is the parent table of this relation.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000C72C File Offset: 0x0000A92C
		public virtual DataTable ParentTable
		{
			get
			{
				return this.parentColumns[0].Table;
			}
		}

		/// <summary>Gets or sets the name used to retrieve a <see cref="T:System.Data.DataRelation" /> from the <see cref="T:System.Data.DataRelationCollection" />.</summary>
		/// <returns>The name of the a <see cref="T:System.Data.DataRelation" />.</returns>
		/// <exception cref="T:System.ArgumentException">null or empty string ("") was passed into a <see cref="T:System.Data.DataColumn" /> that is a <see cref="T:System.Data.DataRelation" />. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The <see cref="T:System.Data.DataRelation" /> belongs to a collection that already contains a <see cref="T:System.Data.DataRelation" /> with the same name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000C73C File Offset: 0x0000A93C
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000C744 File Offset: 0x0000A944
		[DataCategory("Data")]
		[DefaultValue("")]
		public virtual string RelationName
		{
			get
			{
				return this.relationName;
			}
			set
			{
				this.relationName = value;
			}
		}

		/// <summary>This method supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <exception cref="T:System.Data.DataException">The parent and child tables belong to different <see cref="T:System.Data.DataSet" /> objects.-Or- One or more pairs of parent and child <see cref="T:System.Data.DataColumn" /> objects have mismatched data types.-Or- The parent and child <see cref="T:System.Data.DataColumn" /> objects are identical. </exception>
		// Token: 0x060001BB RID: 443 RVA: 0x0000C750 File Offset: 0x0000A950
		protected void CheckStateForProperty()
		{
			DataTable table = this.parentColumns[0].Table;
			DataTable table2 = this.childColumns[0].Table;
			if (table.DataSet != table2.DataSet)
			{
				throw new DataException();
			}
			bool flag = false;
			for (int i = 0; i < this.parentColumns.Length; i++)
			{
				if (!this.parentColumns[i].DataType.Equals(this.childColumns[i].DataType))
				{
					throw new DataException();
				}
				if (this.parentColumns[i] != this.childColumns[i])
				{
					flag = false;
				}
			}
			if (flag)
			{
				throw new DataException();
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="pcevent">Parameter reference.</param>
		// Token: 0x060001BC RID: 444 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
		protected internal void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			if (this.onPropertyChangingDelegate != null)
			{
				this.onPropertyChangingDelegate(this, pcevent);
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="name">Parameter reference.</param>
		// Token: 0x060001BD RID: 445 RVA: 0x0000C814 File Offset: 0x0000AA14
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		/// <summary>Gets the <see cref="P:System.Data.DataRelation.RelationName" />, if one exists.</summary>
		/// <returns>The value of the <see cref="P:System.Data.DataRelation.RelationName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001BE RID: 446 RVA: 0x0000C824 File Offset: 0x0000AA24
		public override string ToString()
		{
			return this.relationName;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000C82C File Offset: 0x0000AA2C
		internal void UpdateConstraints()
		{
			if (this.initFinished || !this.createConstraints)
			{
				return;
			}
			ForeignKeyConstraint foreignKeyConstraint = this.FindForeignKey(this.ChildTable.Constraints);
			UniqueConstraint uniqueConstraint = this.FindUniqueConstraint(this.ParentTable.Constraints);
			if (uniqueConstraint == null)
			{
				uniqueConstraint = new UniqueConstraint(this.ParentColumns, false);
				this.ParentTable.Constraints.Add(uniqueConstraint);
			}
			if (foreignKeyConstraint == null)
			{
				foreignKeyConstraint = new ForeignKeyConstraint(this.RelationName, this.ParentColumns, this.ChildColumns);
				this.ChildTable.Constraints.Add(foreignKeyConstraint);
			}
			this.SetParentKeyConstraint(uniqueConstraint);
			this.SetChildKeyConstraint(foreignKeyConstraint);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000C8DC File Offset: 0x0000AADC
		private static bool CompareDataColumns(DataColumn[] dc1, DataColumn[] dc2)
		{
			if (dc1.Length != dc2.Length)
			{
				return false;
			}
			for (int i = 0; i < dc1.Length; i++)
			{
				if (dc1[i] != dc2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000C918 File Offset: 0x0000AB18
		private ForeignKeyConstraint FindForeignKey(ConstraintCollection cl)
		{
			foreach (object obj in cl)
			{
				Constraint constraint = (Constraint)obj;
				if (constraint is ForeignKeyConstraint)
				{
					ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraint;
					if (DataRelation.CompareDataColumns(this.ChildColumns, foreignKeyConstraint.Columns) && DataRelation.CompareDataColumns(this.ParentColumns, foreignKeyConstraint.RelatedColumns))
					{
						return foreignKeyConstraint;
					}
				}
			}
			return null;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		private UniqueConstraint FindUniqueConstraint(ConstraintCollection cl)
		{
			foreach (object obj in cl)
			{
				Constraint constraint = (Constraint)obj;
				if (constraint is UniqueConstraint)
				{
					UniqueConstraint uniqueConstraint = (UniqueConstraint)constraint;
					if (DataRelation.CompareDataColumns(this.ParentColumns, uniqueConstraint.Columns))
					{
						return uniqueConstraint;
					}
				}
			}
			return null;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000CA70 File Offset: 0x0000AC70
		internal bool Contains(DataColumn column)
		{
			foreach (DataColumn dataColumn in this.ParentColumns)
			{
				if (dataColumn == column)
				{
					return true;
				}
			}
			foreach (DataColumn dataColumn2 in this.ChildColumns)
			{
				if (dataColumn2 == column)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040000CF RID: 207
		private DataSet dataSet;

		// Token: 0x040000D0 RID: 208
		private string relationName;

		// Token: 0x040000D1 RID: 209
		private UniqueConstraint parentKeyConstraint;

		// Token: 0x040000D2 RID: 210
		private ForeignKeyConstraint childKeyConstraint;

		// Token: 0x040000D3 RID: 211
		private DataColumn[] parentColumns;

		// Token: 0x040000D4 RID: 212
		private DataColumn[] childColumns;

		// Token: 0x040000D5 RID: 213
		private bool nested;

		// Token: 0x040000D6 RID: 214
		internal bool createConstraints;

		// Token: 0x040000D7 RID: 215
		private bool initFinished;

		// Token: 0x040000D8 RID: 216
		private PropertyCollection extendedProperties;

		// Token: 0x040000D9 RID: 217
		private PropertyChangedEventHandler onPropertyChangingDelegate;

		// Token: 0x040000DA RID: 218
		private string _relationName;

		// Token: 0x040000DB RID: 219
		private string _parentTableName;

		// Token: 0x040000DC RID: 220
		private string _childTableName;

		// Token: 0x040000DD RID: 221
		private string[] _parentColumnNames;

		// Token: 0x040000DE RID: 222
		private string[] _childColumnNames;

		// Token: 0x040000DF RID: 223
		private bool _nested;

		// Token: 0x040000E0 RID: 224
		private bool initInProgress;

		// Token: 0x040000E1 RID: 225
		private string _parentTableNameSpace;

		// Token: 0x040000E2 RID: 226
		private string _childTableNameSpace;
	}
}
