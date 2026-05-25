using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Mono.Data.SqlExpressions;

namespace System.Data
{
	/// <summary>Represents one table of in-memory data.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000034 RID: 52
	[DefaultEvent("RowChanging")]
	[ToolboxItem(false)]
	[Editor("Microsoft.VSDesigner.Data.Design.DataTableEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[XmlSchemaProvider("GetDataTableSchema")]
	[DesignTimeVisible(false)]
	[DefaultProperty("TableName")]
	[Serializable]
	public class DataTable : MarshalByValueComponent, IXmlSerializable, IListSource, ISupportInitialize, ISerializable, ISupportInitializeNotification
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTable" /> class with no arguments.</summary>
		// Token: 0x0600032A RID: 810 RVA: 0x000147D8 File Offset: 0x000129D8
		public DataTable()
		{
			this.dataSet = null;
			this._columnCollection = new DataColumnCollection(this);
			this._constraintCollection = new ConstraintCollection(this);
			this._extendedProperties = new PropertyCollection();
			this._tableName = string.Empty;
			this._nameSpace = null;
			this._caseSensitive = false;
			this._displayExpression = null;
			this._primaryKeyConstraint = null;
			this._site = null;
			this._rows = new DataRowCollection(this);
			this._indexes = new ArrayList();
			this._recordCache = new RecordCache(this);
			this._minimumCapacity = 50;
			this._childRelations = new DataRelationCollection.DataTableRelationCollection(this);
			this._parentRelations = new DataRelationCollection.DataTableRelationCollection(this);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTable" /> class with the specified table name.</summary>
		/// <param name="tableName">The name to give the table. If <paramref name="tableName" /> is null or an empty string, a default name is given when added to the <see cref="T:System.Data.DataTableCollection" />. </param>
		// Token: 0x0600032B RID: 811 RVA: 0x000148A4 File Offset: 0x00012AA4
		public DataTable(string tableName)
			: this()
		{
			this._tableName = tableName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTable" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object. </param>
		/// <param name="context">The source and destination of a given serialized stream. </param>
		// Token: 0x0600032C RID: 812 RVA: 0x000148B4 File Offset: 0x00012AB4
		protected DataTable(SerializationInfo info, StreamingContext context)
			: this()
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			while (enumerator.MoveNext())
			{
				if (enumerator.ObjectType == typeof(SerializationFormat))
				{
					serializationFormat = (SerializationFormat)((int)enumerator.Value);
					break;
				}
			}
			if (serializationFormat == SerializationFormat.Xml)
			{
				string @string = info.GetString("XmlSchema");
				string string2 = info.GetString("XmlDiffGram");
				DataSet dataSet = new DataSet();
				dataSet.ReadXmlSchema(new StringReader(@string));
				dataSet.Tables[0].CopyProperties(this);
				dataSet = new DataSet();
				dataSet.Tables.Add(this);
				dataSet.ReadXml(new StringReader(string2), XmlReadMode.DiffGram);
				dataSet.Tables.Remove(this);
			}
			else
			{
				this.BinaryDeserializeTable(info);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTable" /> class using the specified table name and namespace.</summary>
		/// <param name="tableName">The name to give the table. If <paramref name="tableName" /> is null or an empty string, a default name is given when added to the <see cref="T:System.Data.DataTableCollection" />. </param>
		/// <param name="tableNamespace">The namespace for the XML representation of the data stored in the DataTable. </param>
		// Token: 0x0600032D RID: 813 RVA: 0x00014988 File Offset: 0x00012B88
		public DataTable(string tableName, string tbNamespace)
			: this(tableName)
		{
			this._nameSpace = tbNamespace;
		}

		/// <summary>Occurs after a value has been changed for the specified <see cref="T:System.Data.DataColumn" /> in a <see cref="T:System.Data.DataRow" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600032F RID: 815 RVA: 0x000149B8 File Offset: 0x00012BB8
		// (remove) Token: 0x06000330 RID: 816 RVA: 0x000149D4 File Offset: 0x00012BD4
		[DataCategory("Data")]
		public event DataColumnChangeEventHandler ColumnChanged;

		/// <summary>Occurs when a value is being changed for the specified <see cref="T:System.Data.DataColumn" /> in a <see cref="T:System.Data.DataRow" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000331 RID: 817 RVA: 0x000149F0 File Offset: 0x00012BF0
		// (remove) Token: 0x06000332 RID: 818 RVA: 0x00014A0C File Offset: 0x00012C0C
		[DataCategory("Data")]
		public event DataColumnChangeEventHandler ColumnChanging;

		/// <summary>Occurs after a <see cref="T:System.Data.DataRow" /> has been changed successfully.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000333 RID: 819 RVA: 0x00014A28 File Offset: 0x00012C28
		// (remove) Token: 0x06000334 RID: 820 RVA: 0x00014A44 File Offset: 0x00012C44
		[DataCategory("Data")]
		public event DataRowChangeEventHandler RowChanged;

		/// <summary>Occurs when a <see cref="T:System.Data.DataRow" /> is changing.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000335 RID: 821 RVA: 0x00014A60 File Offset: 0x00012C60
		// (remove) Token: 0x06000336 RID: 822 RVA: 0x00014A7C File Offset: 0x00012C7C
		[DataCategory("Data")]
		public event DataRowChangeEventHandler RowChanging;

		/// <summary>Occurs after a row in the table has been deleted.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000337 RID: 823 RVA: 0x00014A98 File Offset: 0x00012C98
		// (remove) Token: 0x06000338 RID: 824 RVA: 0x00014AB4 File Offset: 0x00012CB4
		[DataCategory("Data")]
		public event DataRowChangeEventHandler RowDeleted;

		/// <summary>Occurs before a row in the table is about to be deleted.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000339 RID: 825 RVA: 0x00014AD0 File Offset: 0x00012CD0
		// (remove) Token: 0x0600033A RID: 826 RVA: 0x00014AEC File Offset: 0x00012CEC
		[DataCategory("Data")]
		public event DataRowChangeEventHandler RowDeleting;

		/// <summary>Occurs after the <see cref="T:System.Data.DataTable" /> is initialized.</summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600033B RID: 827 RVA: 0x00014B08 File Offset: 0x00012D08
		// (remove) Token: 0x0600033C RID: 828 RVA: 0x00014B24 File Offset: 0x00012D24
		public event EventHandler Initialized;

		/// <summary>Occurs after a <see cref="T:System.Data.DataTable" /> is cleared.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x0600033D RID: 829 RVA: 0x00014B40 File Offset: 0x00012D40
		// (remove) Token: 0x0600033E RID: 830 RVA: 0x00014B5C File Offset: 0x00012D5C
		[DataCategory("Data")]
		public event DataTableClearEventHandler TableCleared;

		/// <summary>Occurs when a <see cref="T:System.Data.DataTable" /> is cleared.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600033F RID: 831 RVA: 0x00014B78 File Offset: 0x00012D78
		// (remove) Token: 0x06000340 RID: 832 RVA: 0x00014B94 File Offset: 0x00012D94
		[DataCategory("Data")]
		public event DataTableClearEventHandler TableClearing;

		/// <summary>Occurs when a new <see cref="T:System.Data.DataRow" /> is inserted.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000341 RID: 833 RVA: 0x00014BB0 File Offset: 0x00012DB0
		// (remove) Token: 0x06000342 RID: 834 RVA: 0x00014BCC File Offset: 0x00012DCC
		public event DataTableNewRowEventHandler TableNewRow;

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00014BE8 File Offset: 0x00012DE8
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00014BEC File Offset: 0x00012DEC
		IList IListSource.GetList()
		{
			return this.DefaultView;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00014C04 File Offset: 0x00012E04
		[MonoNotSupported("")]
		XmlSchema IXmlSerializable.GetSchema()
		{
			return this.GetSchema();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00014C0C File Offset: 0x00012E0C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXml_internal(reader, true);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00014C18 File Offset: 0x00012E18
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			DataSet dataSet = this.dataSet;
			bool flag = true;
			if (this.dataSet == null)
			{
				dataSet = new DataSet();
				dataSet.Tables.Add(this);
				flag = false;
			}
			XmlSchemaWriter.WriteXmlSchema(writer, new DataTable[] { this }, null, this.TableName, dataSet.DataSetName, (!this.LocaleSpecified) ? ((!dataSet.LocaleSpecified) ? null : dataSet.Locale) : this.Locale);
			dataSet.WriteIndividualTableContent(writer, this, XmlWriteMode.DiffGram);
			writer.Flush();
			if (!flag)
			{
				this.dataSet.Tables.Remove(this);
			}
		}

		/// <summary>Indicates whether string comparisons within the table are case-sensitive.</summary>
		/// <returns>true if the comparison is case-sensitive; otherwise false. The default is set to the parent <see cref="T:System.Data.DataSet" /> object's <see cref="P:System.Data.DataSet.CaseSensitive" /> property, or false if the <see cref="T:System.Data.DataTable" /> was created independently of a <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00014CC0 File Offset: 0x00012EC0
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00014CF8 File Offset: 0x00012EF8
		public bool CaseSensitive
		{
			get
			{
				if (this._virginCaseSensitive && this.dataSet != null)
				{
					return this.dataSet.CaseSensitive;
				}
				return this._caseSensitive;
			}
			set
			{
				if (this._childRelations.Count > 0 || this._parentRelations.Count > 0)
				{
					throw new ArgumentException("Cannot change CaseSensitive or Locale property. This change would lead to at least one DataRelation or Constraint to have different Locale or CaseSensitive settings between its related tables.");
				}
				this._virginCaseSensitive = false;
				this._caseSensitive = value;
				this.ResetCaseSensitiveIndexes();
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00014D48 File Offset: 0x00012F48
		internal ArrayList Indexes
		{
			get
			{
				return this._indexes;
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00014D50 File Offset: 0x00012F50
		internal void ChangedDataColumn(DataRow dr, DataColumn dc, object pv)
		{
			DataColumnChangeEventArgs dataColumnChangeEventArgs = new DataColumnChangeEventArgs(dr, dc, pv);
			this.OnColumnChanged(dataColumnChangeEventArgs);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00014D70 File Offset: 0x00012F70
		internal void ChangingDataColumn(DataRow dr, DataColumn dc, object pv)
		{
			DataColumnChangeEventArgs dataColumnChangeEventArgs = new DataColumnChangeEventArgs(dr, dc, pv);
			this.OnColumnChanging(dataColumnChangeEventArgs);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00014D90 File Offset: 0x00012F90
		internal void DeletedDataRow(DataRow dr, DataRowAction action)
		{
			DataRowChangeEventArgs dataRowChangeEventArgs = new DataRowChangeEventArgs(dr, action);
			this.OnRowDeleted(dataRowChangeEventArgs);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00014DAC File Offset: 0x00012FAC
		internal void DeletingDataRow(DataRow dr, DataRowAction action)
		{
			DataRowChangeEventArgs dataRowChangeEventArgs = new DataRowChangeEventArgs(dr, action);
			this.OnRowDeleting(dataRowChangeEventArgs);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00014DC8 File Offset: 0x00012FC8
		internal void ChangedDataRow(DataRow dr, DataRowAction action)
		{
			DataRowChangeEventArgs dataRowChangeEventArgs = new DataRowChangeEventArgs(dr, action);
			this.OnRowChanged(dataRowChangeEventArgs);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00014DE4 File Offset: 0x00012FE4
		internal void ChangingDataRow(DataRow dr, DataRowAction action)
		{
			DataRowChangeEventArgs dataRowChangeEventArgs = new DataRowChangeEventArgs(dr, action);
			this.OnRowChanging(dataRowChangeEventArgs);
		}

		/// <summary>Gets the collection of child relations for this <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataRelationCollection" /> that contains the child relations for the table. An empty collection is returned if no <see cref="T:System.Data.DataRelation" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00014E00 File Offset: 0x00013000
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataRelationCollection ChildRelations
		{
			get
			{
				return this._childRelations;
			}
		}

		/// <summary>Gets the collection of columns that belong to this table.</summary>
		/// <returns>A <see cref="T:System.Data.DataColumnCollection" /> that contains the collection of <see cref="T:System.Data.DataColumn" /> objects for the table. An empty collection is returned if no <see cref="T:System.Data.DataColumn" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00014E08 File Offset: 0x00013008
		[DataCategory("Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataColumnCollection Columns
		{
			get
			{
				return this._columnCollection;
			}
		}

		/// <summary>Gets the collection of constraints maintained by this table.</summary>
		/// <returns>A <see cref="T:System.Data.ConstraintCollection" /> that contains the collection of <see cref="T:System.Data.Constraint" /> objects for the table. An empty collection is returned if no <see cref="T:System.Data.Constraint" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00014E10 File Offset: 0x00013010
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00014E18 File Offset: 0x00013018
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DataCategory("Data")]
		public ConstraintCollection Constraints
		{
			get
			{
				return this._constraintCollection;
			}
			internal set
			{
				this._constraintCollection = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataSet" /> to which this table belongs.</summary>
		/// <returns>The <see cref="T:System.Data.DataSet" /> to which this table belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00014E24 File Offset: 0x00013024
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataSet DataSet
		{
			get
			{
				return this.dataSet;
			}
		}

		/// <summary>Gets a customized view of the table that may include a filtered view, or a cursor position.</summary>
		/// <returns>The <see cref="T:System.Data.DataView" /> associated with the <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00014E2C File Offset: 0x0001302C
		[Browsable(false)]
		public DataView DefaultView
		{
			get
			{
				if (this._defaultView == null)
				{
					lock (this)
					{
						if (this._defaultView == null)
						{
							if (this.dataSet != null)
							{
								this._defaultView = this.dataSet.DefaultViewManager.CreateDataView(this);
							}
							else
							{
								this._defaultView = new DataView(this);
							}
						}
					}
				}
				return this._defaultView;
			}
		}

		/// <summary>Gets or sets the expression that returns a value used to represent this table in the user interface. The DisplayExpression property lets you display the name of this table in a user interface.</summary>
		/// <returns>A display string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00014EB8 File Offset: 0x000130B8
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00014ED8 File Offset: 0x000130D8
		[DataCategory("Data")]
		[DefaultValue("")]
		public string DisplayExpression
		{
			get
			{
				return (this._displayExpression != null) ? this._displayExpression : string.Empty;
			}
			set
			{
				this._displayExpression = value;
			}
		}

		/// <summary>Gets the collection of customized user information.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> that contains custom user information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00014EE4 File Offset: 0x000130E4
		[DataCategory("Data")]
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				return this._extendedProperties;
			}
		}

		/// <summary>Gets a value indicating whether there are errors in any of the rows in any of the tables of the <see cref="T:System.Data.DataSet" /> to which the table belongs.</summary>
		/// <returns>true if errors exist; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00014EEC File Offset: 0x000130EC
		[Browsable(false)]
		public bool HasErrors
		{
			get
			{
				for (int i = 0; i < this._rows.Count; i++)
				{
					if (this._rows[i].HasErrors)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Gets or sets the locale information used to compare strings within the table.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that contains data about the user's machine locale. The default is the <see cref="T:System.Data.DataSet" /> object's <see cref="T:System.Globalization.CultureInfo" /> (returned by the <see cref="P:System.Data.DataSet.Locale" /> property) to which the <see cref="T:System.Data.DataTable" /> belongs; if the table doesn't belong to a <see cref="T:System.Data.DataSet" />, the default is the current system <see cref="T:System.Globalization.CultureInfo" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00014F30 File Offset: 0x00013130
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00014F6C File Offset: 0x0001316C
		public CultureInfo Locale
		{
			get
			{
				if (this._locale != null)
				{
					return this._locale;
				}
				if (this.DataSet != null)
				{
					return this.DataSet.Locale;
				}
				return CultureInfo.CurrentCulture;
			}
			set
			{
				if (this._childRelations.Count > 0 || this._parentRelations.Count > 0)
				{
					throw new ArgumentException("Cannot change CaseSensitive or Locale property. This change would lead to at least one DataRelation or Constraint to have different Locale or CaseSensitive settings between its related tables.");
				}
				if (this._locale == null || !this._locale.Equals(value))
				{
					this._locale = value;
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00014FCC File Offset: 0x000131CC
		internal bool LocaleSpecified
		{
			get
			{
				return this._locale != null;
			}
		}

		/// <summary>Gets or sets the initial starting size for this table.</summary>
		/// <returns>The initial starting size in rows of this table. The default is 50.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00014FDC File Offset: 0x000131DC
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00014FE4 File Offset: 0x000131E4
		[DefaultValue(50)]
		[DataCategory("Data")]
		public int MinimumCapacity
		{
			get
			{
				return this._minimumCapacity;
			}
			set
			{
				this._minimumCapacity = value;
			}
		}

		/// <summary>Gets or sets the namespace for the XML representation of the data stored in the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00014FF0 File Offset: 0x000131F0
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0001502C File Offset: 0x0001322C
		[DataCategory("Data")]
		public string Namespace
		{
			get
			{
				if (this._nameSpace != null)
				{
					return this._nameSpace;
				}
				if (this.DataSet != null)
				{
					return this.DataSet.Namespace;
				}
				return string.Empty;
			}
			set
			{
				this._nameSpace = value;
			}
		}

		/// <summary>Gets the collection of parent relations for this <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataRelationCollection" /> that contains the parent relations for the table. An empty collection is returned if no <see cref="T:System.Data.DataRelation" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00015038 File Offset: 0x00013238
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataRelationCollection ParentRelations
		{
			get
			{
				return this._parentRelations;
			}
		}

		/// <summary>Gets or sets the namespace for the XML representation of the data stored in the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>The prefix of the <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00015040 File Offset: 0x00013240
		// (set) Token: 0x06000364 RID: 868 RVA: 0x00015060 File Offset: 0x00013260
		[DataCategory("Data")]
		[DefaultValue("")]
		public string Prefix
		{
			get
			{
				return (this._prefix != null) ? this._prefix : string.Empty;
			}
			set
			{
				for (int i = 0; i < value.Length; i++)
				{
					if (!char.IsLetterOrDigit(value[i]) && value[i] != '_' && value[i] != ':')
					{
						throw new DataException("Prefix '" + value + "' is not valid, because it contains special characters.");
					}
				}
				this._prefix = value;
			}
		}

		/// <summary>Gets or sets an array of columns that function as primary keys for the data table.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects.</returns>
		/// <exception cref="T:System.Data.DataException">The key is a foreign key. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000150D0 File Offset: 0x000132D0
		// (set) Token: 0x06000366 RID: 870 RVA: 0x000150F0 File Offset: 0x000132F0
		[TypeConverter("System.Data.PrimaryKeyTypeConverter, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[DataCategory("Data")]
		[Editor("Microsoft.VSDesigner.Data.Design.PrimaryKeyEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public DataColumn[] PrimaryKey
		{
			get
			{
				if (this._primaryKeyConstraint == null)
				{
					return new DataColumn[0];
				}
				return this._primaryKeyConstraint.Columns;
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					if (this._primaryKeyConstraint != null)
					{
						this._primaryKeyConstraint.SetIsPrimaryKey(false);
						this.Constraints.Remove(this._primaryKeyConstraint);
						this._primaryKeyConstraint = null;
					}
					return;
				}
				if (this.InitInProgress)
				{
					this._latestPrimaryKeyCols = value;
					return;
				}
				if (this._primaryKeyConstraint != null && DataColumn.AreColumnSetsTheSame(value, this._primaryKeyConstraint.Columns))
				{
					return;
				}
				UniqueConstraint uniqueConstraint = UniqueConstraint.GetUniqueConstraintForColumnSet(this.Constraints, value);
				if (uniqueConstraint == null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						DataColumn dataColumn = value[i];
						if (dataColumn.Table == null)
						{
							break;
						}
						if (this.Columns.IndexOf(dataColumn) < 0)
						{
							throw new ArgumentException("PrimaryKey columns do not belong to this table.");
						}
					}
					uniqueConstraint = new UniqueConstraint(value, false);
					this.Constraints.Add(uniqueConstraint);
				}
				if (this._primaryKeyConstraint != null)
				{
					this._primaryKeyConstraint.SetIsPrimaryKey(false);
					this.Constraints.Remove(this._primaryKeyConstraint);
					this._primaryKeyConstraint = null;
				}
				UniqueConstraint.SetAsPrimaryKey(this.Constraints, uniqueConstraint);
				this._primaryKeyConstraint = uniqueConstraint;
				for (int j = 0; j < uniqueConstraint.Columns.Length; j++)
				{
					uniqueConstraint.Columns[j].AllowDBNull = false;
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0001524C File Offset: 0x0001344C
		internal UniqueConstraint PrimaryKeyConstraint
		{
			get
			{
				return this._primaryKeyConstraint;
			}
		}

		/// <summary>Gets the collection of rows that belong to this table.</summary>
		/// <returns>A <see cref="T:System.Data.DataRowCollection" /> that contains <see cref="T:System.Data.DataRow" /> objects; otherwise a null value if no <see cref="T:System.Data.DataRow" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00015254 File Offset: 0x00013454
		[Browsable(false)]
		public DataRowCollection Rows
		{
			get
			{
				return this._rows;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0001525C File Offset: 0x0001345C
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00015264 File Offset: 0x00013464
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ISite Site
		{
			get
			{
				return this._site;
			}
			set
			{
				this._site = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.DataTable" />.</returns>
		/// <exception cref="T:System.ArgumentException">null or empty string ("") is passed in and this table belongs to a collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The table belongs to a collection that already has a table with the same name. (Comparison is case-sensitive).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00015270 File Offset: 0x00013470
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00015290 File Offset: 0x00013490
		[DefaultValue("")]
		[DataCategory("Data")]
		[RefreshProperties(RefreshProperties.All)]
		public string TableName
		{
			get
			{
				return (this._tableName != null) ? this._tableName : string.Empty;
			}
			set
			{
				this._tableName = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0001529C File Offset: 0x0001349C
		internal RecordCache RecordCache
		{
			get
			{
				return this._recordCache;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000152A4 File Offset: 0x000134A4
		private DataRowBuilder RowBuilder
		{
			get
			{
				if (this._rowBuilder == null)
				{
					this._rowBuilder = new DataRowBuilder(this, -1, 0);
				}
				else
				{
					this._rowBuilder._rowId = -1;
				}
				return this._rowBuilder;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000152E4 File Offset: 0x000134E4
		// (set) Token: 0x06000370 RID: 880 RVA: 0x000152EC File Offset: 0x000134EC
		internal bool EnforceConstraints
		{
			get
			{
				return this.enforceConstraints;
			}
			set
			{
				if (value == this.enforceConstraints)
				{
					return;
				}
				if (value)
				{
					this.ResetIndexes();
					foreach (object obj in this.Constraints)
					{
						Constraint constraint = (Constraint)obj;
						constraint.AssertConstraint();
					}
					this.AssertNotNullConstraints();
					if (this.HasErrors)
					{
						Constraint.ThrowConstraintException();
					}
				}
				this.enforceConstraints = value;
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00015390 File Offset: 0x00013590
		internal void AssertNotNullConstraints()
		{
			if (this._duringDataLoad && !this._nullConstraintViolationDuringDataLoad)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < this.Columns.Count; i++)
			{
				DataColumn dataColumn = this.Columns[i];
				if (!dataColumn.AllowDBNull)
				{
					for (int j = 0; j < this.Rows.Count; j++)
					{
						if (this.Rows[j].HasVersion(DataRowVersion.Default) && this.Rows[j].IsNull(dataColumn))
						{
							flag = true;
							string text = string.Format("Column '{0}' does not allow DBNull.Value.", dataColumn.ColumnName);
							this.Rows[j].SetColumnError(i, text);
							this.Rows[j].RowError = text;
						}
					}
				}
			}
			this._nullConstraintViolationDuringDataLoad = flag;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00015480 File Offset: 0x00013680
		internal bool RowsExist(DataColumn[] columns, DataColumn[] relatedColumns, DataRow row)
		{
			int num = row.IndexFromVersion(DataRowVersion.Default);
			int num2 = this.RecordCache.NewRecord();
			bool flag;
			try
			{
				for (int i = 0; i < relatedColumns.Length; i++)
				{
					columns[i].DataContainer.CopyValue(relatedColumns[i].DataContainer, num, num2);
				}
				flag = this.RowsExist(columns, num2);
			}
			finally
			{
				this.RecordCache.DisposeRecord(num2);
			}
			return flag;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00015510 File Offset: 0x00013710
		private bool RowsExist(DataColumn[] columns, int index)
		{
			Index index2 = this.FindIndex(columns);
			if (index2 != null)
			{
				return index2.Find(index) != -1;
			}
			foreach (object obj in this.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.RowState != DataRowState.Deleted)
				{
					int num = dataRow.IndexFromVersion((dataRow.RowState != DataRowState.Modified) ? DataRowVersion.Current : DataRowVersion.Original);
					bool flag = true;
					foreach (DataColumn dataColumn in columns)
					{
						if (dataColumn.DataContainer.CompareValues(num, index) != 0)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Commits all the changes made to this table since the last time <see cref="M:System.Data.DataTable.AcceptChanges" /> was called.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000374 RID: 884 RVA: 0x00015620 File Offset: 0x00013820
		public void AcceptChanges()
		{
			int i = 0;
			while (i < this.Rows.Count)
			{
				DataRow dataRow = this.Rows[i];
				dataRow.AcceptChanges();
				if (dataRow.RowState != DataRowState.Detached)
				{
					i++;
				}
			}
			this._rows.OnListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, -1, -1));
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Data.DataTable" /> that is used on a form or used by another component. The initialization occurs at run time. </summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000375 RID: 885 RVA: 0x0001567C File Offset: 0x0001387C
		public virtual void BeginInit()
		{
			this.InitInProgress = true;
			this.tableInitialized = false;
		}

		/// <summary>Turns off notifications, index maintenance, and constraints while loading data.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000376 RID: 886 RVA: 0x0001568C File Offset: 0x0001388C
		public void BeginLoadData()
		{
			if (this._duringDataLoad)
			{
				return;
			}
			this._duringDataLoad = true;
			this._nullConstraintViolationDuringDataLoad = false;
			if (this.dataSet != null)
			{
				this.dataSetPrevEnforceConstraints = this.dataSet.EnforceConstraints;
				this.dataSet.EnforceConstraints = false;
			}
			else
			{
				this.EnforceConstraints = false;
			}
		}

		/// <summary>Clears the <see cref="T:System.Data.DataTable" /> of all data.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000377 RID: 887 RVA: 0x000156E8 File Offset: 0x000138E8
		public void Clear()
		{
			this._rows.Clear();
		}

		/// <summary>Clones the structure of the <see cref="T:System.Data.DataTable" />, including all <see cref="T:System.Data.DataTable" /> schemas and constraints.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> with the same schema as the current <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000378 RID: 888 RVA: 0x000156F8 File Offset: 0x000138F8
		public virtual DataTable Clone()
		{
			DataTable dataTable = (DataTable)Activator.CreateInstance(base.GetType(), true);
			this.CopyProperties(dataTable);
			return dataTable;
		}

		/// <summary>Computes the given expression on the current rows that pass the filter criteria.</summary>
		/// <returns>An <see cref="T:System.Object" />, set to the result of the computation. If the expression evaluates to null, the return value will be <see cref="F:System.DBNull.Value" />.</returns>
		/// <param name="expression">The expression to compute. </param>
		/// <param name="filter">The filter to limit the rows that evaluate in the expression. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000379 RID: 889 RVA: 0x00015720 File Offset: 0x00013920
		public object Compute(string expression, string filter)
		{
			DataRow[] array = this.Select(filter);
			if (array == null || array.Length == 0)
			{
				return DBNull.Value;
			}
			Parser parser = new Parser(array);
			IExpression expression2 = parser.Compile(expression);
			return expression2.Eval(array[0]);
		}

		/// <summary>Copies both the structure and data for this <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> with the same structure (table schemas and constraints) and data as this <see cref="T:System.Data.DataTable" />.If these classes have been derived, the copy will also be of the same derived classes.Both the <see cref="M:System.Data.DataTable.Copy" /> and the <see cref="M:System.Data.DataTable.Clone" /> methods create a new DataTable with the same structure as the original DataTable. The new DataTable created by the <see cref="M:System.Data.DataTable.Copy" /> method has the same set of DataRows as the original table, but the new DataTable created by the <see cref="M:System.Data.DataTable.Clone" /> method does not contain any DataRows.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600037A RID: 890 RVA: 0x00015764 File Offset: 0x00013964
		public DataTable Copy()
		{
			DataTable dataTable = this.Clone();
			dataTable._duringDataLoad = true;
			foreach (object obj in this.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRow dataRow2 = dataTable.NewNotInitializedRow();
				dataTable.Rows.AddInternal(dataRow2);
				this.CopyRow(dataRow, dataRow2);
			}
			dataTable._duringDataLoad = false;
			dataTable.ResetIndexes();
			return dataTable;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00015808 File Offset: 0x00013A08
		internal void CopyRow(DataRow fromRow, DataRow toRow)
		{
			if (fromRow.HasErrors)
			{
				fromRow.CopyErrors(toRow);
			}
			if (fromRow.HasVersion(DataRowVersion.Original))
			{
				toRow.Original = toRow.Table.RecordCache.CopyRecord(this, fromRow.Original, -1);
			}
			if (fromRow.HasVersion(DataRowVersion.Current))
			{
				if (fromRow.Original != fromRow.Current)
				{
					toRow.Current = toRow.Table.RecordCache.CopyRecord(this, fromRow.Current, -1);
				}
				else
				{
					toRow.Current = toRow.Original;
				}
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000158A8 File Offset: 0x00013AA8
		private void CopyProperties(DataTable Copy)
		{
			Copy.CaseSensitive = this.CaseSensitive;
			Copy._virginCaseSensitive = this._virginCaseSensitive;
			Copy.DisplayExpression = this.DisplayExpression;
			if (this.ExtendedProperties.Count > 0)
			{
				Array array = Array.CreateInstance(typeof(object), this.ExtendedProperties.Count);
				this.ExtendedProperties.Keys.CopyTo(array, 0);
				for (int i = 0; i < this.ExtendedProperties.Count; i++)
				{
					Copy.ExtendedProperties.Add(array.GetValue(i), this.ExtendedProperties[array.GetValue(i)]);
				}
			}
			Copy._locale = this._locale;
			Copy.MinimumCapacity = this.MinimumCapacity;
			Copy.Namespace = this.Namespace;
			Copy.Prefix = this.Prefix;
			Copy.Site = this.Site;
			Copy.TableName = this.TableName;
			bool flag = Copy.Columns.Count == 0;
			foreach (object obj in this.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (flag || !Copy.Columns.Contains(dataColumn.ColumnName))
				{
					Copy.Columns.Add(dataColumn.Clone());
				}
			}
			this.CopyConstraints(Copy);
			if (this.PrimaryKey.Length > 0)
			{
				DataColumn[] array2 = new DataColumn[this.PrimaryKey.Length];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = Copy.Columns[this.PrimaryKey[j].ColumnName];
				}
				Copy.PrimaryKey = array2;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00015AA0 File Offset: 0x00013CA0
		private void CopyConstraints(DataTable copy)
		{
			for (int i = 0; i < this.Constraints.Count; i++)
			{
				if (this.Constraints[i] is UniqueConstraint)
				{
					if (!copy.Constraints.Contains(this.Constraints[i].ConstraintName))
					{
						UniqueConstraint uniqueConstraint = (UniqueConstraint)this.Constraints[i];
						DataColumn[] array = new DataColumn[uniqueConstraint.Columns.Length];
						for (int j = 0; j < array.Length; j++)
						{
							array[j] = copy.Columns[uniqueConstraint.Columns[j].ColumnName];
						}
						UniqueConstraint uniqueConstraint2 = new UniqueConstraint(uniqueConstraint.ConstraintName, array, uniqueConstraint.IsPrimaryKey);
						copy.Constraints.Add(uniqueConstraint2);
					}
				}
			}
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Data.DataTable" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600037E RID: 894 RVA: 0x00015B7C File Offset: 0x00013D7C
		public virtual void EndInit()
		{
			this.InitInProgress = false;
			this.DataTableInitialized();
			this.FinishInit();
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00015B94 File Offset: 0x00013D94
		// (set) Token: 0x06000380 RID: 896 RVA: 0x00015B9C File Offset: 0x00013D9C
		internal bool InitInProgress
		{
			get
			{
				return this.fInitInProgress;
			}
			set
			{
				this.fInitInProgress = value;
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00015BA8 File Offset: 0x00013DA8
		internal void FinishInit()
		{
			UniqueConstraint primaryKeyConstraint = this._primaryKeyConstraint;
			this.Columns.PostAddRange();
			this._constraintCollection.PostAddRange();
			if (this._primaryKeyConstraint == primaryKeyConstraint)
			{
				this.PrimaryKey = this._latestPrimaryKeyCols;
			}
		}

		/// <summary>Turns on notifications, index maintenance, and constraints after loading data.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000382 RID: 898 RVA: 0x00015BEC File Offset: 0x00013DEC
		public void EndLoadData()
		{
			if (this._duringDataLoad)
			{
				if (this.dataSet != null)
				{
					this.dataSet.InternalEnforceConstraints(this.dataSetPrevEnforceConstraints, true);
				}
				else
				{
					this.EnforceConstraints = true;
				}
				this._duringDataLoad = false;
			}
		}

		/// <summary>Gets a copy of the <see cref="T:System.Data.DataTable" /> that contains all changes made to it since it was loaded or <see cref="M:System.Data.DataTable.AcceptChanges" /> was last called.</summary>
		/// <returns>A copy of the changes from this <see cref="T:System.Data.DataTable" />, or null if no changes are found.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000383 RID: 899 RVA: 0x00015C2C File Offset: 0x00013E2C
		public DataTable GetChanges()
		{
			return this.GetChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
		}

		/// <summary>Gets a copy of the <see cref="T:System.Data.DataTable" /> containing all changes made to it since it was last loaded, or since <see cref="M:System.Data.DataTable.AcceptChanges" /> was called, filtered by <see cref="T:System.Data.DataRowState" />.</summary>
		/// <returns>A filtered copy of the <see cref="T:System.Data.DataTable" /> that can have actions performed on it, and later be merged back in the <see cref="T:System.Data.DataTable" /> using <see cref="M:System.Data.DataSet.Merge(System.Data.DataSet)" />. If no rows of the desired <see cref="T:System.Data.DataRowState" /> are found, the method returns null.</returns>
		/// <param name="rowStates">One of the <see cref="T:System.Data.DataRowState" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000384 RID: 900 RVA: 0x00015C38 File Offset: 0x00013E38
		public DataTable GetChanges(DataRowState rowStates)
		{
			DataTable dataTable = null;
			foreach (object obj in this.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow.IsRowChanged(rowStates))
				{
					if (dataTable == null)
					{
						dataTable = this.Clone();
					}
					DataRow dataRow2 = dataTable.NewNotInitializedRow();
					dataRow.CopyValuesToRow(dataRow2);
					dataRow2.XmlRowID = dataRow.XmlRowID;
					dataTable.Rows.AddInternal(dataRow2);
				}
			}
			return dataTable;
		}

		/// <summary>Gets an array of <see cref="T:System.Data.DataRow" /> objects that contain errors.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects that have errors.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000385 RID: 901 RVA: 0x00015CEC File Offset: 0x00013EEC
		public DataRow[] GetErrors()
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this._rows.Count; i++)
			{
				if (this._rows[i].HasErrors)
				{
					arrayList.Add(this._rows[i]);
				}
			}
			DataRow[] array = this.NewRowArray(arrayList.Count);
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00015D5C File Offset: 0x00013F5C
		protected virtual DataTable CreateInstance()
		{
			return Activator.CreateInstance(base.GetType(), true) as DataTable;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00015D70 File Offset: 0x00013F70
		protected virtual Type GetRowType()
		{
			return typeof(DataRow);
		}

		/// <summary>Copies a <see cref="T:System.Data.DataRow" /> into a <see cref="T:System.Data.DataTable" />, preserving any property settings, as well as original and current values.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to be imported. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000388 RID: 904 RVA: 0x00015D7C File Offset: 0x00013F7C
		public void ImportRow(DataRow row)
		{
			if (row.RowState == DataRowState.Detached)
			{
				return;
			}
			DataRow dataRow = this.NewNotInitializedRow();
			int num = -1;
			if (row.HasVersion(DataRowVersion.Original))
			{
				num = row.IndexFromVersion(DataRowVersion.Original);
				dataRow.Original = this.RecordCache.NewRecord();
				this.RecordCache.CopyRecord(row.Table, num, dataRow.Original);
			}
			if (row.HasVersion(DataRowVersion.Current))
			{
				int num2 = row.IndexFromVersion(DataRowVersion.Current);
				if (num2 == num)
				{
					dataRow.Current = dataRow.Original;
				}
				else
				{
					dataRow.Current = this.RecordCache.NewRecord();
					this.RecordCache.CopyRecord(row.Table, num2, dataRow.Current);
				}
			}
			if (row.RowState != DataRowState.Deleted)
			{
				dataRow.Validate();
			}
			else
			{
				this.AddRowToIndexes(dataRow);
			}
			this.Rows.AddInternal(dataRow);
			if (row.HasErrors)
			{
				row.CopyErrors(dataRow);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00015E80 File Offset: 0x00014080
		internal int DefaultValuesRowIndex
		{
			get
			{
				return this._defaultValuesRowIndex;
			}
		}

		/// <summary>Populates a serialization information object with the data needed to serialize the <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized data associated with the <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Data.DataTable" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x0600038A RID: 906 RVA: 0x00015E88 File Offset: 0x00014088
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this.RemotingFormat == SerializationFormat.Xml)
			{
				DataSet dataSet;
				if (this.dataSet != null)
				{
					dataSet = this.dataSet;
				}
				else
				{
					dataSet = new DataSet("tmpDataSet");
					dataSet.Tables.Add(this);
				}
				StringWriter stringWriter = new StringWriter();
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				xmlTextWriter.Formatting = Formatting.Indented;
				dataSet.WriteIndividualTableContent(xmlTextWriter, this, XmlWriteMode.DiffGram);
				xmlTextWriter.Close();
				StringWriter stringWriter2 = new StringWriter();
				DataTableCollection dataTableCollection = new DataTableCollection(dataSet);
				dataTableCollection.Add(this);
				XmlSchemaWriter.WriteXmlSchema(dataSet, new XmlTextWriter(stringWriter2), dataTableCollection, null);
				stringWriter2.Close();
				info.AddValue("XmlSchema", stringWriter2.ToString(), typeof(string));
				info.AddValue("XmlDiffGram", stringWriter.ToString(), typeof(string));
			}
			else
			{
				this.BinarySerializeProperty(info);
				if (this.dataSet == null)
				{
					for (int i = 0; i < this.Columns.Count; i++)
					{
						info.AddValue("DataTable.DataColumn_" + i + ".Expression", this.Columns[i].Expression);
					}
					this.BinarySerialize(info, "DataTable_0.");
				}
			}
		}

		/// <summary>Finds and updates a specific row. If no matching row is found, a new row is created using the given values.</summary>
		/// <returns>The new <see cref="T:System.Data.DataRow" />.</returns>
		/// <param name="values">An array of values used to create the new row. </param>
		/// <param name="fAcceptChanges">true to accept changes; otherwise false. </param>
		/// <exception cref="T:System.ArgumentException">The array is larger than the number of columns in the table. </exception>
		/// <exception cref="T:System.InvalidCastException">A value doesn't match its respective column type. </exception>
		/// <exception cref="T:System.Data.ConstraintException">Adding the row invalidates a constraint. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">Attempting to put a null in a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is false. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600038B RID: 907 RVA: 0x00015FC4 File Offset: 0x000141C4
		public DataRow LoadDataRow(object[] values, bool fAcceptChanges)
		{
			DataRow dataRow;
			if (this.PrimaryKey.Length == 0)
			{
				dataRow = this.Rows.Add(values);
			}
			else
			{
				this.EnsureDefaultValueRowIndex();
				int num = this.CreateRecord(values);
				int num2 = this._primaryKeyConstraint.Index.Find(num);
				if (num2 < 0)
				{
					dataRow = this.NewRowFromBuilder(this.RowBuilder);
					dataRow.Proposed = num;
					this.Rows.AddInternal(dataRow);
					if (!this._duringDataLoad)
					{
						this.AddRowToIndexes(dataRow);
					}
				}
				else
				{
					dataRow = this.RecordCache[num2];
					dataRow.BeginEdit();
					dataRow.ImportRecord(num);
					dataRow.EndEdit();
				}
			}
			if (fAcceptChanges)
			{
				dataRow.AcceptChanges();
			}
			return dataRow;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00016080 File Offset: 0x00014280
		internal DataRow LoadDataRow(IDataRecord record, int[] mapping, int length, bool fAcceptChanges)
		{
			DataRow dataRow = null;
			int num = this.RecordCache.NewRecord();
			try
			{
				this.RecordCache.ReadIDataRecord(num, record, mapping, length);
				if (this.PrimaryKey.Length != 0)
				{
					bool flag = true;
					foreach (DataColumn dataColumn in this.PrimaryKey)
					{
						if (dataColumn.Ordinal >= mapping.Length)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						int num2 = this._primaryKeyConstraint.Index.Find(num);
						if (num2 != -1)
						{
							dataRow = this.RecordCache[num2];
						}
					}
				}
				if (dataRow == null)
				{
					dataRow = this.NewNotInitializedRow();
					dataRow.Proposed = num;
					this.Rows.AddInternal(dataRow);
				}
				else
				{
					dataRow.BeginEdit();
					dataRow.ImportRecord(num);
					dataRow.EndEdit();
				}
				if (fAcceptChanges)
				{
					dataRow.AcceptChanges();
				}
			}
			catch
			{
				this.RecordCache.DisposeRecord(num);
				throw;
			}
			return dataRow;
		}

		/// <summary>Creates a new <see cref="T:System.Data.DataRow" /> with the same schema as the table.</summary>
		/// <returns>A <see cref="T:System.Data.DataRow" /> with the same schema as the <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600038D RID: 909 RVA: 0x000161A0 File Offset: 0x000143A0
		public DataRow NewRow()
		{
			this.EnsureDefaultValueRowIndex();
			DataRow dataRow = this.NewRowFromBuilder(this.RowBuilder);
			dataRow.Proposed = this.CreateRecord(null);
			this.NewRowAdded(dataRow);
			return dataRow;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000161D8 File Offset: 0x000143D8
		internal int CreateRecord(object[] values)
		{
			int num = ((values == null) ? 0 : values.Length);
			if (num > this.Columns.Count)
			{
				throw new ArgumentException("Input array is longer than the number of columns in this table.");
			}
			int num2 = this.RecordCache.NewRecord();
			int num3;
			try
			{
				for (int i = 0; i < num; i++)
				{
					if (values[i] == null)
					{
						this.Columns[i].SetDefaultValue(num2);
					}
					else
					{
						this.Columns[i][num2] = values[i];
					}
				}
				for (int j = num; j < this.Columns.Count; j++)
				{
					this.Columns[j].SetDefaultValue(num2);
				}
				num3 = num2;
			}
			catch
			{
				this.RecordCache.DisposeRecord(num2);
				throw;
			}
			return num3;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000162D8 File Offset: 0x000144D8
		private void EnsureDefaultValueRowIndex()
		{
			if (this._defaultValuesRowIndex == -1)
			{
				this._defaultValuesRowIndex = this.RecordCache.NewRecord();
				for (int i = 0; i < this.Columns.Count; i++)
				{
					DataColumn dataColumn = this.Columns[i];
					dataColumn.DataContainer[this._defaultValuesRowIndex] = dataColumn.DefaultValue;
				}
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00016344 File Offset: 0x00014544
		protected internal DataRow[] NewRowArray(int size)
		{
			if (size == 0 && this.empty_rows != null)
			{
				return this.empty_rows;
			}
			Type rowType = this.GetRowType();
			DataRow[] array = ((rowType != typeof(DataRow)) ? ((DataRow[])Array.CreateInstance(rowType, size)) : new DataRow[size]);
			if (size == 0)
			{
				this.empty_rows = array;
			}
			return array;
		}

		/// <summary>Creates a new row from an existing row.</summary>
		/// <returns>A <see cref="T:System.Data.DataRow" /> derived class.</returns>
		/// <param name="builder">A <see cref="T:System.Data.DataRowBuilder" /> object. </param>
		// Token: 0x06000391 RID: 913 RVA: 0x000163A8 File Offset: 0x000145A8
		protected virtual DataRow NewRowFromBuilder(DataRowBuilder builder)
		{
			return new DataRow(builder);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000163B0 File Offset: 0x000145B0
		internal DataRow NewNotInitializedRow()
		{
			this.EnsureDefaultValueRowIndex();
			return this.NewRowFromBuilder(this.RowBuilder);
		}

		/// <summary>Rolls back all changes that have been made to the table since it was loaded, or the last time <see cref="M:System.Data.DataTable.AcceptChanges" /> was called.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000393 RID: 915 RVA: 0x000163C4 File Offset: 0x000145C4
		public void RejectChanges()
		{
			for (int i = this._rows.Count - 1; i >= 0; i--)
			{
				DataRow dataRow = this._rows[i];
				if (dataRow.RowState != DataRowState.Unchanged)
				{
					this._rows[i].RejectChanges();
				}
			}
		}

		/// <summary>Resets the <see cref="T:System.Data.DataTable" /> to its original state.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000394 RID: 916 RVA: 0x0001641C File Offset: 0x0001461C
		public virtual void Reset()
		{
			this.Clear();
			while (this.ParentRelations.Count > 0)
			{
				if (this.dataSet.Relations.Contains(this.ParentRelations[this.ParentRelations.Count - 1].RelationName))
				{
					this.dataSet.Relations.Remove(this.ParentRelations[this.ParentRelations.Count - 1]);
				}
			}
			while (this.ChildRelations.Count > 0)
			{
				if (this.dataSet.Relations.Contains(this.ChildRelations[this.ChildRelations.Count - 1].RelationName))
				{
					this.dataSet.Relations.Remove(this.ChildRelations[this.ChildRelations.Count - 1]);
				}
			}
			this.Constraints.Clear();
			this.Columns.Clear();
		}

		/// <summary>Gets an array of all <see cref="T:System.Data.DataRow" /> objects.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000395 RID: 917 RVA: 0x00016528 File Offset: 0x00014728
		public DataRow[] Select()
		{
			return this.Select(string.Empty, string.Empty, DataViewRowState.CurrentRows);
		}

		/// <summary>Gets an array of all <see cref="T:System.Data.DataRow" /> objects that match the filter criteria in order of primary key (or lacking one, order of addition.) </summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects.</returns>
		/// <param name="filterExpression">The criteria to use to filter the rows. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000396 RID: 918 RVA: 0x0001653C File Offset: 0x0001473C
		public DataRow[] Select(string filterExpression)
		{
			return this.Select(filterExpression, string.Empty, DataViewRowState.CurrentRows);
		}

		/// <summary>Gets an array of all <see cref="T:System.Data.DataRow" /> objects that match the filter criteria, in the specified sort order.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects matching the filter expression.</returns>
		/// <param name="filterExpression">The criteria to use to filter the rows. </param>
		/// <param name="sort">A string specifying the column and sort direction. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000397 RID: 919 RVA: 0x0001654C File Offset: 0x0001474C
		public DataRow[] Select(string filterExpression, string sort)
		{
			return this.Select(filterExpression, sort, DataViewRowState.CurrentRows);
		}

		/// <summary>Gets an array of all <see cref="T:System.Data.DataRow" /> objects that match the filter in the order of the sort that match the specified state.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects.</returns>
		/// <param name="filterExpression">The criteria to use to filter the rows. </param>
		/// <param name="sort">A string specifying the column and sort direction. </param>
		/// <param name="recordStates">One of the <see cref="T:System.Data.DataViewRowState" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000398 RID: 920 RVA: 0x00016558 File Offset: 0x00014758
		public DataRow[] Select(string filterExpression, string sort, DataViewRowState recordStates)
		{
			if (filterExpression == null)
			{
				filterExpression = string.Empty;
			}
			IExpression expression = null;
			if (filterExpression != string.Empty)
			{
				Parser parser = new Parser();
				expression = parser.Compile(filterExpression);
			}
			DataColumn[] array = DataTable._emptyColumnArray;
			ListSortDirection[] array2 = null;
			if (sort != null && !sort.Equals(string.Empty))
			{
				array = DataTable.ParseSortString(this, sort, out array2, false);
			}
			if (this.Rows.Count == 0)
			{
				return this.NewRowArray(0);
			}
			if (array.Length == 0 && expression != null)
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this.Columns.Count; i++)
				{
					if (expression.DependsOn(this.Columns[i]))
					{
						arrayList.Add(this.Columns[i]);
					}
				}
				array = (DataColumn[])arrayList.ToArray(typeof(DataColumn));
			}
			bool flag = true;
			if (filterExpression != string.Empty)
			{
				flag = false;
			}
			Index index = this.GetIndex(array, array2, recordStates, expression, false, flag);
			int[] all = index.GetAll();
			DataRow[] array3 = this.NewRowArray(index.Size);
			for (int j = 0; j < array3.Length; j++)
			{
				array3[j] = this.RecordCache[all[j]];
			}
			return array3;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000166C0 File Offset: 0x000148C0
		private void AddIndex(Index index)
		{
			if (this._indexes == null)
			{
				this._indexes = new ArrayList();
			}
			this._indexes.Add(index);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000166E8 File Offset: 0x000148E8
		internal Index GetIndex(DataColumn[] columns, ListSortDirection[] sort, DataViewRowState rowState, IExpression filter, bool reset)
		{
			return this.GetIndex(columns, sort, rowState, filter, reset, true);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000166F8 File Offset: 0x000148F8
		internal Index GetIndex(DataColumn[] columns, ListSortDirection[] sort, DataViewRowState rowState, IExpression filter, bool reset, bool addIndex)
		{
			Index index = this.FindIndex(columns, sort, rowState, filter);
			if (index == null)
			{
				index = new Index(new Key(this, columns, sort, rowState, filter));
				if (addIndex)
				{
					this.AddIndex(index);
				}
			}
			else if (reset)
			{
				index.Reset();
			}
			return index;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001674C File Offset: 0x0001494C
		internal Index FindIndex(DataColumn[] columns)
		{
			return this.FindIndex(columns, null, DataViewRowState.None, null);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00016758 File Offset: 0x00014958
		internal Index FindIndex(DataColumn[] columns, ListSortDirection[] sort, DataViewRowState rowState, IExpression filter)
		{
			if (this.Indexes != null)
			{
				foreach (object obj in this.Indexes)
				{
					Index index = (Index)obj;
					if (index.Key.Equals(columns, sort, rowState, filter))
					{
						return index;
					}
				}
			}
			return null;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000167EC File Offset: 0x000149EC
		internal void ResetIndexes()
		{
			foreach (object obj in this.Indexes)
			{
				Index index = (Index)obj;
				index.Reset();
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001685C File Offset: 0x00014A5C
		internal void ResetCaseSensitiveIndexes()
		{
			foreach (object obj in this.Indexes)
			{
				Index index = (Index)obj;
				bool flag = false;
				foreach (DataColumn dataColumn in index.Key.Columns)
				{
					if (dataColumn.DataType == typeof(string))
					{
						flag = true;
						break;
					}
				}
				if (!flag && index.Key.HasFilter)
				{
					foreach (object obj2 in this.Columns)
					{
						DataColumn dataColumn2 = (DataColumn)obj2;
						if (dataColumn2.DataType == DbTypes.TypeOfString && index.Key.DependsOn(dataColumn2))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					index.Reset();
				}
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000169BC File Offset: 0x00014BBC
		internal void DropIndex(Index index)
		{
			if (index != null && index.RefCount == 0)
			{
				this._indexes.Remove(index);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000169DC File Offset: 0x00014BDC
		internal void DropReferencedIndexes(DataColumn column)
		{
			if (this._indexes != null)
			{
				for (int i = this._indexes.Count - 1; i >= 0; i--)
				{
					Index index = (Index)this._indexes[i];
					if (index.Key.DependsOn(column))
					{
						this._indexes.Remove(index);
					}
				}
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00016A44 File Offset: 0x00014C44
		internal void AddRowToIndexes(DataRow row)
		{
			if (this._indexes != null)
			{
				for (int i = 0; i < this._indexes.Count; i++)
				{
					((Index)this._indexes[i]).Add(row);
				}
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00016A90 File Offset: 0x00014C90
		internal void DeleteRowFromIndexes(DataRow row)
		{
			if (this._indexes != null)
			{
				foreach (object obj in this._indexes)
				{
					Index index = (Index)obj;
					index.Delete(row);
				}
			}
		}

		/// <summary>Gets the <see cref="P:System.Data.DataTable.TableName" /> and <see cref="P:System.Data.DataTable.DisplayExpression" />, if there is one as a concatenated string.</summary>
		/// <returns>A string consisting of the <see cref="P:System.Data.DataTable.TableName" /> and the <see cref="P:System.Data.DataTable.DisplayExpression" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060003A4 RID: 932 RVA: 0x00016B0C File Offset: 0x00014D0C
		public override string ToString()
		{
			string text = this.TableName;
			if (this.DisplayExpression != null && this.DisplayExpression != string.Empty)
			{
				text = text + " + " + this.DisplayExpression;
			}
			return text;
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.ColumnChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataColumnChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x060003A5 RID: 933 RVA: 0x00016B54 File Offset: 0x00014D54
		protected virtual void OnColumnChanged(DataColumnChangeEventArgs e)
		{
			if (this.ColumnChanged != null)
			{
				this.ColumnChanged(this, e);
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00016B70 File Offset: 0x00014D70
		internal void RaiseOnColumnChanged(DataColumnChangeEventArgs e)
		{
			this.OnColumnChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.ColumnChanging" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataColumnChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x060003A7 RID: 935 RVA: 0x00016B7C File Offset: 0x00014D7C
		protected virtual void OnColumnChanging(DataColumnChangeEventArgs e)
		{
			if (this.ColumnChanging != null)
			{
				this.ColumnChanging(this, e);
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00016B98 File Offset: 0x00014D98
		internal void RaiseOnColumnChanging(DataColumnChangeEventArgs e)
		{
			this.OnColumnChanging(e);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" /> event.</summary>
		/// <param name="pcevent">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x060003A9 RID: 937 RVA: 0x00016BA4 File Offset: 0x00014DA4
		[MonoTODO]
		protected internal virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the <see cref="T:System.Data.DataTable" /> that a <see cref="T:System.Data.DataColumn" /> is being removed.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> being removed. </param>
		// Token: 0x060003AA RID: 938 RVA: 0x00016BAC File Offset: 0x00014DAC
		protected internal virtual void OnRemoveColumn(DataColumn column)
		{
			this.DropReferencedIndexes(column);
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.RowChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x060003AB RID: 939 RVA: 0x00016BB8 File Offset: 0x00014DB8
		protected virtual void OnRowChanged(DataRowChangeEventArgs e)
		{
			if (this.RowChanged != null)
			{
				this.RowChanged(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.RowChanging" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x060003AC RID: 940 RVA: 0x00016BD4 File Offset: 0x00014DD4
		protected virtual void OnRowChanging(DataRowChangeEventArgs e)
		{
			if (this.RowChanging != null)
			{
				this.RowChanging(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.RowDeleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x060003AD RID: 941 RVA: 0x00016BF0 File Offset: 0x00014DF0
		protected virtual void OnRowDeleted(DataRowChangeEventArgs e)
		{
			if (this.RowDeleted != null)
			{
				this.RowDeleted(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.RowDeleting" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataRowChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x060003AE RID: 942 RVA: 0x00016C0C File Offset: 0x00014E0C
		protected virtual void OnRowDeleting(DataRowChangeEventArgs e)
		{
			if (this.RowDeleting != null)
			{
				this.RowDeleting(this, e);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00016C28 File Offset: 0x00014E28
		internal static DataColumn[] ParseSortString(DataTable table, string sort, out ListSortDirection[] sortDirections, bool rejectNoResult)
		{
			DataColumn[] array = DataTable._emptyColumnArray;
			sortDirections = null;
			ArrayList arrayList = null;
			ArrayList arrayList2 = null;
			if (sort != null && !sort.Equals(string.Empty))
			{
				arrayList = new ArrayList();
				arrayList2 = new ArrayList();
				string[] array2 = sort.Trim().Split(new char[] { ',' });
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i].Trim();
					Match match = DataTable.SortRegex.Match(text);
					Group group = match.Groups["ColName"];
					if (!group.Success)
					{
						throw new IndexOutOfRangeException("Could not find column: " + text);
					}
					string value = group.Value;
					DataColumn dataColumn = table.Columns[value];
					if (dataColumn == null)
					{
						try
						{
							dataColumn = table.Columns[int.Parse(value)];
						}
						catch (FormatException)
						{
							throw new IndexOutOfRangeException("Cannot find column " + value);
						}
					}
					arrayList.Add(dataColumn);
					group = match.Groups["Order"];
					if (!group.Success || string.Compare(group.Value, "ASC", true, CultureInfo.InvariantCulture) == 0)
					{
						arrayList2.Add(ListSortDirection.Ascending);
					}
					else
					{
						arrayList2.Add(ListSortDirection.Descending);
					}
				}
				array = (DataColumn[])arrayList.ToArray(typeof(DataColumn));
				sortDirections = new ListSortDirection[arrayList2.Count];
				for (int j = 0; j < sortDirections.Length; j++)
				{
					sortDirections[j] = (ListSortDirection)((int)arrayList2[j]);
				}
			}
			if (rejectNoResult)
			{
				if (array == null)
				{
					throw new SystemException("sort expression result is null");
				}
				if (array.Length == 0)
				{
					throw new SystemException("sort expression result is 0");
				}
			}
			return array;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00016E28 File Offset: 0x00015028
		private void UpdatePropertyDescriptorsCache()
		{
			PropertyDescriptor[] array = new PropertyDescriptor[this.Columns.Count + this.ChildRelations.Count];
			int num = 0;
			foreach (object obj in this.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				array[num++] = new DataColumnPropertyDescriptor(dataColumn);
			}
			foreach (object obj2 in this.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj2;
				array[num++] = new DataRelationPropertyDescriptor(dataRelation);
			}
			this._propertyDescriptorsCache = new PropertyDescriptorCollection(array);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00016F38 File Offset: 0x00015138
		internal PropertyDescriptorCollection GetPropertyDescriptorCollection()
		{
			if (this._propertyDescriptorsCache == null)
			{
				this.UpdatePropertyDescriptorsCache();
			}
			return this._propertyDescriptorsCache;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00016F54 File Offset: 0x00015154
		internal void ResetPropertyDescriptorsCache()
		{
			this._propertyDescriptorsCache = null;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00016F60 File Offset: 0x00015160
		internal void SetRowsID()
		{
			int num = 0;
			foreach (object obj in this.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow.XmlRowID = num;
				num++;
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00016FD8 File Offset: 0x000151D8
		[MonoTODO]
		protected virtual XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		/// <summary>This method returns an <see cref="T:System.Xml.Schema.XmlSchemaSet" /> instance containing the Web Services Description Language (WSDL) that describes the <see cref="T:System.Data.DataTable" /> for Web Services.</summary>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> instance.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B5 RID: 949 RVA: 0x00016FE0 File Offset: 0x000151E0
		public static XmlSchemaComplexType GetDataTableSchema(XmlSchemaSet schemaSet)
		{
			return new XmlSchemaComplexType();
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="stream">An object that derives from <see cref="T:System.IO.Stream" /></param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B6 RID: 950 RVA: 0x00016FE8 File Offset: 0x000151E8
		public XmlReadMode ReadXml(Stream stream)
		{
			return this.ReadXml(new XmlTextReader(stream, null));
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataTable" /> from the specified file.</summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="fileName">The name of the file from which to read the data. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B7 RID: 951 RVA: 0x00016FF8 File Offset: 0x000151F8
		public XmlReadMode ReadXml(string fileName)
		{
			XmlReader xmlReader = new XmlTextReader(fileName);
			XmlReadMode xmlReadMode;
			try
			{
				xmlReadMode = this.ReadXml(xmlReader);
			}
			finally
			{
				xmlReader.Close();
			}
			return xmlReadMode;
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> that will be used to read the data.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B8 RID: 952 RVA: 0x00017044 File Offset: 0x00015244
		public XmlReadMode ReadXml(TextReader reader)
		{
			return this.ReadXml(new XmlTextReader(reader));
		}

		/// <summary>Reads XML Schema and Data into the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.Xml.XmlReader" />. </summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> that will be used to read the data. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003B9 RID: 953 RVA: 0x00017054 File Offset: 0x00015254
		public XmlReadMode ReadXml(XmlReader reader)
		{
			return this.ReadXml_internal(reader, false);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00017060 File Offset: 0x00015260
		public XmlReadMode ReadXml_internal(XmlReader reader, bool serializable)
		{
			bool flag = true;
			bool flag2 = false;
			XmlReadMode xmlReadMode = XmlReadMode.ReadSchema;
			DataSet dataSet = null;
			DataSet dataSet2 = new DataSet();
			reader.MoveToContent();
			if ((this.Columns.Count > 0 && reader.LocalName != "diffgram") || serializable)
			{
				xmlReadMode = dataSet2.ReadXml(reader);
			}
			else
			{
				if (this.Columns.Count > 0 && reader.LocalName == "diffgram")
				{
					try
					{
						if (this.TableName == string.Empty)
						{
							flag2 = true;
						}
						if (this.DataSet == null)
						{
							flag = false;
							dataSet2.Tables.Add(this);
							xmlReadMode = dataSet2.ReadXml(reader);
						}
						else
						{
							xmlReadMode = this.DataSet.ReadXml(reader);
						}
					}
					catch (DataException)
					{
						xmlReadMode = XmlReadMode.DiffGram;
						if (flag2)
						{
							this.TableName = string.Empty;
						}
					}
					finally
					{
						if (!flag)
						{
							dataSet2.Tables.Remove(this);
						}
					}
					return xmlReadMode;
				}
				xmlReadMode = dataSet2.ReadXml(reader, XmlReadMode.ReadSchema);
			}
			if (xmlReadMode == XmlReadMode.InferSchema)
			{
				xmlReadMode = XmlReadMode.IgnoreSchema;
			}
			if (this.DataSet == null)
			{
				flag = false;
				dataSet = new DataSet();
				if (this.TableName == string.Empty)
				{
					flag2 = true;
				}
				dataSet.Tables.Add(this);
			}
			this.DenyXmlResolving(this, dataSet2, xmlReadMode, flag2, flag);
			if (this.Columns.Count > 0 && this.TableName != dataSet2.Tables[0].TableName)
			{
				if (!flag)
				{
					dataSet.Tables.Remove(this);
				}
				if (flag2 && !flag)
				{
					this.TableName = string.Empty;
				}
				return xmlReadMode;
			}
			this.TableName = dataSet2.Tables[0].TableName;
			if (!flag)
			{
				if (this.Columns.Count > 0)
				{
					dataSet.Merge(dataSet2, true, MissingSchemaAction.Ignore);
				}
				else
				{
					dataSet.Merge(dataSet2, true, MissingSchemaAction.AddWithKey);
				}
				if (this.ChildRelations.Count == 0)
				{
					dataSet.Tables.Remove(this);
				}
				else
				{
					dataSet.DataSetName = dataSet2.DataSetName;
				}
			}
			else if (this.Columns.Count > 0)
			{
				this.DataSet.Merge(dataSet2, true, MissingSchemaAction.Ignore);
			}
			else
			{
				this.DataSet.Merge(dataSet2, true, MissingSchemaAction.AddWithKey);
			}
			return xmlReadMode;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000172FC File Offset: 0x000154FC
		private void DenyXmlResolving(DataTable table, DataSet ds, XmlReadMode mode, bool isTableNameBlank, bool isPartOfDataSet)
		{
			if (ds.Tables.Count == 0 && table.Columns.Count == 0)
			{
				throw new InvalidOperationException("DataTable does not support schema inference from XML");
			}
			if (table.Columns.Count == 0 && ds.Tables[0].TableName != table.TableName && !isTableNameBlank)
			{
				throw new ArgumentException(string.Format("DataTable '{0}' does not match to any DataTable in source", table.TableName));
			}
			if (table.Columns.Count > 0 && ds.Tables[0].TableName != table.TableName && !isTableNameBlank && mode == XmlReadMode.ReadSchema && !isPartOfDataSet)
			{
				throw new ArgumentException(string.Format("DataTable '{0}' does not match to any DataTable in source", table.TableName));
			}
			if (isPartOfDataSet && table.Columns.Count > 0 && mode == XmlReadMode.ReadSchema && table.TableName != ds.Tables[0].TableName)
			{
				throw new ArgumentException(string.Format("DataTable '{0}' does not match to any DataTable in source", table.TableName));
			}
		}

		/// <summary>Reads an XML schema into the <see cref="T:System.Data.DataTable" /> using the specified stream.</summary>
		/// <param name="stream">The stream used to read the schema. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003BC RID: 956 RVA: 0x00017438 File Offset: 0x00015638
		public void ReadXmlSchema(Stream stream)
		{
			this.ReadXmlSchema(new XmlTextReader(stream));
		}

		/// <summary>Reads an XML schema into the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> used to read the schema information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003BD RID: 957 RVA: 0x00017448 File Offset: 0x00015648
		public void ReadXmlSchema(TextReader reader)
		{
			this.ReadXmlSchema(new XmlTextReader(reader));
		}

		/// <summary>Reads an XML schema into the <see cref="T:System.Data.DataTable" /> from the specified file.</summary>
		/// <param name="fileName">The name of the file from which to read the schema information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003BE RID: 958 RVA: 0x00017458 File Offset: 0x00015658
		public void ReadXmlSchema(string fileName)
		{
			XmlTextReader xmlTextReader = null;
			try
			{
				xmlTextReader = new XmlTextReader(fileName);
				this.ReadXmlSchema(xmlTextReader);
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
			}
		}

		/// <summary>Reads an XML schema into the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> used to read the schema information. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003BF RID: 959 RVA: 0x000174A4 File Offset: 0x000156A4
		public void ReadXmlSchema(XmlReader reader)
		{
			if (this.Columns.Count > 0)
			{
				return;
			}
			DataSet dataSet = new DataSet();
			new XmlSchemaDataImporter(dataSet, reader, false).Process();
			DataTable dataTable = null;
			if (this.TableName == string.Empty)
			{
				if (dataSet.Tables.Count > 0)
				{
					dataTable = dataSet.Tables[0];
				}
			}
			else
			{
				dataTable = dataSet.Tables[this.TableName];
				if (dataTable == null)
				{
					throw new ArgumentException(string.Format("DataTable '{0}' does not match to any DataTable in source.", this.TableName));
				}
			}
			if (dataTable != null)
			{
				dataTable.CopyProperties(this);
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001754C File Offset: 0x0001574C
		[MonoNotSupported("")]
		protected virtual void ReadXmlSerializable(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00017554 File Offset: 0x00015754
		private XmlWriterSettings GetWriterSettings()
		{
			return new XmlWriterSettings
			{
				Indent = true,
				OmitXmlDeclaration = true
			};
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The stream to which the data will be written. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003C2 RID: 962 RVA: 0x00017578 File Offset: 0x00015778
		public void WriteXml(Stream stream)
		{
			this.WriteXml(stream, XmlWriteMode.IgnoreSchema, false);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> with which to write the content. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003C3 RID: 963 RVA: 0x00017584 File Offset: 0x00015784
		public void WriteXml(TextWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema, false);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write the contents. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003C4 RID: 964 RVA: 0x00017590 File Offset: 0x00015790
		public void WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema, false);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified file.</summary>
		/// <param name="fileName">The file to which to write the XML data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003C5 RID: 965 RVA: 0x0001759C File Offset: 0x0001579C
		public void WriteXml(string fileName)
		{
			this.WriteXml(fileName, XmlWriteMode.IgnoreSchema, false);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> to the specified file using the specified <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="stream">The stream to which the data will be written. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003C6 RID: 966 RVA: 0x000175A8 File Offset: 0x000157A8
		public void WriteXml(Stream stream, XmlWriteMode mode)
		{
			this.WriteXml(stream, mode, false);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> used to write the document. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003C7 RID: 967 RVA: 0x000175B4 File Offset: 0x000157B4
		public void WriteXml(TextWriter writer, XmlWriteMode mode)
		{
			this.WriteXml(writer, mode, false);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.Xml.XmlWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the document. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003C8 RID: 968 RVA: 0x000175C0 File Offset: 0x000157C0
		public void WriteXml(XmlWriter writer, XmlWriteMode mode)
		{
			this.WriteXml(writer, mode, false);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> using the specified file and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="fileName">The name of the file to which the data will be written. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003C9 RID: 969 RVA: 0x000175CC File Offset: 0x000157CC
		public void WriteXml(string fileName, XmlWriteMode mode)
		{
			this.WriteXml(fileName, mode, false);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified <see cref="T:System.IO.Stream" />. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="stream">The stream to which the data will be written. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003CA RID: 970 RVA: 0x000175D8 File Offset: 0x000157D8
		public void WriteXml(Stream stream, bool writeHierarchy)
		{
			this.WriteXml(stream, XmlWriteMode.IgnoreSchema, writeHierarchy);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified file. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="fileName">The file to which to write the XML data.</param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003CB RID: 971 RVA: 0x000175E4 File Offset: 0x000157E4
		public void WriteXml(string fileName, bool writeHierarchy)
		{
			this.WriteXml(fileName, XmlWriteMode.IgnoreSchema, writeHierarchy);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified <see cref="T:System.IO.TextWriter" />. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> with which to write the content. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003CC RID: 972 RVA: 0x000175F0 File Offset: 0x000157F0
		public void WriteXml(TextWriter writer, bool writeHierarchy)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema, writeHierarchy);
		}

		/// <summary>Writes the current contents of the <see cref="T:System.Data.DataTable" /> as XML using the specified <see cref="T:System.Xml.XmlWriter" />. </summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write the contents. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003CD RID: 973 RVA: 0x000175FC File Offset: 0x000157FC
		public void WriteXml(XmlWriter writer, bool writeHierarchy)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema, writeHierarchy);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> to the specified file using the specified <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="stream">The stream to which the data will be written. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003CE RID: 974 RVA: 0x00017608 File Offset: 0x00015808
		public void WriteXml(Stream stream, XmlWriteMode mode, bool writeHierarchy)
		{
			this.WriteXml(XmlWriter.Create(stream, this.GetWriterSettings()), mode, writeHierarchy);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> using the specified file and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="fileName">The name of the file to which the data will be written. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003CF RID: 975 RVA: 0x00017620 File Offset: 0x00015820
		public void WriteXml(string fileName, XmlWriteMode mode, bool writeHierarchy)
		{
			XmlWriter xmlWriter = null;
			try
			{
				xmlWriter = XmlWriter.Create(fileName, this.GetWriterSettings());
				this.WriteXml(xmlWriter, mode, writeHierarchy);
			}
			finally
			{
				if (xmlWriter != null)
				{
					xmlWriter.Close();
				}
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> used to write the document. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D0 RID: 976 RVA: 0x00017674 File Offset: 0x00015874
		public void WriteXml(TextWriter writer, XmlWriteMode mode, bool writeHierarchy)
		{
			this.WriteXml(XmlWriter.Create(writer, this.GetWriterSettings()), mode, writeHierarchy);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataTable" /> using the specified <see cref="T:System.Xml.XmlWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema. To save the data for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the document. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <param name="writeHierarchy">If true, write the contents of the current table and all its descendants. If false (the default value), write the data for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D1 RID: 977 RVA: 0x0001768C File Offset: 0x0001588C
		public void WriteXml(XmlWriter writer, XmlWriteMode mode, bool writeHierarchy)
		{
			List<DataTable> list = new List<DataTable>();
			if (!writeHierarchy)
			{
				list.Add(this);
			}
			else
			{
				this.FindAllChildren(list, this);
			}
			List<DataRelation> list2 = new List<DataRelation>();
			if (this.DataSet != null)
			{
				foreach (object obj in this.DataSet.Relations)
				{
					DataRelation dataRelation = (DataRelation)obj;
					if (list.Contains(dataRelation.ParentTable) && list.Contains(dataRelation.ChildTable))
					{
						list2.Add(dataRelation);
					}
				}
			}
			string text = null;
			if (mode == XmlWriteMode.WriteSchema)
			{
				text = this.TableName;
			}
			string text2;
			if (this.DataSet != null)
			{
				text2 = this.DataSet.DataSetName;
			}
			else if (this.DataSet == null && mode == XmlWriteMode.WriteSchema)
			{
				text2 = "NewDataSet";
			}
			else
			{
				text2 = "DocumentElement";
			}
			XmlTableWriter.WriteTables(writer, mode, list, list2, text, text2);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000177B8 File Offset: 0x000159B8
		private void FindAllChildren(List<DataTable> list, DataTable root)
		{
			if (!list.Contains(root))
			{
				list.Add(root);
				foreach (object obj in root.ChildRelations)
				{
					DataRelation dataRelation = (DataRelation)obj;
					this.FindAllChildren(list, dataRelation.ChildTable);
				}
			}
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema to the specified stream.</summary>
		/// <param name="stream">The stream to which the XML schema will be written. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003D3 RID: 979 RVA: 0x00017840 File Offset: 0x00015A40
		public void WriteXmlSchema(Stream stream)
		{
			if (this.TableName == string.Empty)
			{
				throw new InvalidOperationException("Cannot serialize the DataTable. DataTable name is not set.");
			}
			XmlWriterSettings writerSettings = this.GetWriterSettings();
			writerSettings.OmitXmlDeclaration = false;
			this.WriteXmlSchema(XmlWriter.Create(stream, writerSettings));
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> with which to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003D4 RID: 980 RVA: 0x00017888 File Offset: 0x00015A88
		public void WriteXmlSchema(TextWriter writer)
		{
			if (this.TableName == string.Empty)
			{
				throw new InvalidOperationException("Cannot serialize the DataTable. DataTable name is not set.");
			}
			XmlWriterSettings writerSettings = this.GetWriterSettings();
			writerSettings.OmitXmlDeclaration = false;
			this.WriteXmlSchema(XmlWriter.Create(writer, writerSettings));
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema using the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to use. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003D5 RID: 981 RVA: 0x000178D0 File Offset: 0x00015AD0
		public void WriteXmlSchema(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, false);
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema to the specified file.</summary>
		/// <param name="fileName">The name of the file to use. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060003D6 RID: 982 RVA: 0x000178DC File Offset: 0x00015ADC
		public void WriteXmlSchema(string fileName)
		{
			if (fileName == string.Empty)
			{
				throw new ArgumentException("Empty path name is not legal.");
			}
			if (this.TableName == string.Empty)
			{
				throw new InvalidOperationException("Cannot serialize the DataTable. DataTable name is not set.");
			}
			XmlTextWriter xmlTextWriter = null;
			try
			{
				XmlWriterSettings writerSettings = this.GetWriterSettings();
				writerSettings.OmitXmlDeclaration = false;
				xmlTextWriter = new XmlTextWriter(fileName, null);
				this.WriteXmlSchema(xmlTextWriter);
			}
			finally
			{
				if (xmlTextWriter != null)
				{
					xmlTextWriter.Close();
				}
			}
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema to the specified stream. To save the schema for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="stream">The stream to which the XML schema will be written. </param>
		/// <param name="writeHierarchy">If true, write the schema of the current table and all its descendants. If false (the default value), write the schema for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D7 RID: 983 RVA: 0x00017974 File Offset: 0x00015B74
		public void WriteXmlSchema(Stream stream, bool writeHierarchy)
		{
			if (this.TableName == string.Empty)
			{
				throw new InvalidOperationException("Cannot serialize the DataTable. DataTable name is not set.");
			}
			XmlWriterSettings writerSettings = this.GetWriterSettings();
			writerSettings.OmitXmlDeclaration = false;
			this.WriteXmlSchema(XmlWriter.Create(stream, writerSettings), writeHierarchy);
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema using the specified <see cref="T:System.IO.TextWriter" />. To save the schema for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> with which to write. </param>
		/// <param name="writeHierarchy">If true, write the schema of the current table and all its descendants. If false (the default value), write the schema for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D8 RID: 984 RVA: 0x000179C0 File Offset: 0x00015BC0
		public void WriteXmlSchema(TextWriter writer, bool writeHierarchy)
		{
			if (this.TableName == string.Empty)
			{
				throw new InvalidOperationException("Cannot serialize the DataTable. DataTable name is not set.");
			}
			XmlWriterSettings writerSettings = this.GetWriterSettings();
			writerSettings.OmitXmlDeclaration = false;
			this.WriteXmlSchema(XmlWriter.Create(writer, writerSettings), writeHierarchy);
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema using the specified <see cref="T:System.Xml.XmlWriter" />. To save the schema for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> used to write the document. </param>
		/// <param name="writeHierarchy">If true, write the schema of the current table and all its descendants. If false (the default value), write the schema for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003D9 RID: 985 RVA: 0x00017A0C File Offset: 0x00015C0C
		public void WriteXmlSchema(XmlWriter writer, bool writeHierarchy)
		{
			if (this.TableName == string.Empty)
			{
				throw new InvalidOperationException("Cannot serialize the DataTable. DataTable name is not set.");
			}
			DataSet dataSet = this.DataSet;
			DataSet dataSet2 = null;
			try
			{
				if (dataSet == null)
				{
					dataSet = (dataSet2 = new DataSet());
					dataSet.Tables.Add(this);
				}
				writer.WriteStartDocument();
				DataRelation[] array = null;
				DataTable[] array2;
				if (writeHierarchy && this.ChildRelations.Count > 0)
				{
					array = new DataRelation[this.ChildRelations.Count];
					for (int i = 0; i < this.ChildRelations.Count; i++)
					{
						array[i] = this.ChildRelations[i];
					}
					array2 = new DataTable[dataSet.Tables.Count];
					for (int j = 0; j < dataSet.Tables.Count; j++)
					{
						array2[j] = dataSet.Tables[j];
					}
				}
				else
				{
					array2 = new DataTable[] { this };
				}
				string text;
				if (dataSet.Namespace == string.Empty)
				{
					text = this.TableName;
				}
				else
				{
					text = dataSet.Namespace + "_x003A_" + this.TableName;
				}
				XmlSchemaWriter.WriteXmlSchema(writer, array2, array, text, dataSet.DataSetName, (!this.LocaleSpecified) ? ((!dataSet.LocaleSpecified) ? null : dataSet.Locale) : this.Locale);
			}
			finally
			{
				if (dataSet2 != null)
				{
					dataSet.Tables.Remove(this);
				}
			}
		}

		/// <summary>Writes the current data structure of the <see cref="T:System.Data.DataTable" /> as an XML schema to the specified file. To save the schema for the table and all its descendants, set the <paramref name="writeHierarchy" /> parameter to true.</summary>
		/// <param name="fileName">The name of the file to use. </param>
		/// <param name="writeHierarchy">If true, write the schema of the current table and all its descendants. If false (the default value), write the schema for the current table only.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060003DA RID: 986 RVA: 0x00017BBC File Offset: 0x00015DBC
		public void WriteXmlSchema(string fileName, bool writeHierarchy)
		{
			if (fileName == string.Empty)
			{
				throw new ArgumentException("Empty path name is not legal.");
			}
			if (this.TableName == string.Empty)
			{
				throw new InvalidOperationException("Cannot serialize the DataTable. DataTable name is not set.");
			}
			XmlTextWriter xmlTextWriter = null;
			try
			{
				XmlWriterSettings writerSettings = this.GetWriterSettings();
				writerSettings.OmitXmlDeclaration = false;
				xmlTextWriter = new XmlTextWriter(fileName, null);
				this.WriteXmlSchema(xmlTextWriter, writeHierarchy);
			}
			finally
			{
				if (xmlTextWriter != null)
				{
					xmlTextWriter.Close();
				}
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataTable" /> is initialized.</summary>
		/// <returns>true to indicate the component has completed initialization; otherwise false. </returns>
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00017C54 File Offset: 0x00015E54
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return this.tableInitialized;
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00017C5C File Offset: 0x00015E5C
		private void OnTableInitialized(EventArgs e)
		{
			if (this.Initialized != null)
			{
				this.Initialized(this, e);
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00017C78 File Offset: 0x00015E78
		private void DataTableInitialized()
		{
			this.tableInitialized = true;
			this.OnTableInitialized(new EventArgs());
		}

		/// <summary>Gets or sets the serialization format.</summary>
		/// <returns>A <see cref="T:System.Data.SerializationFormat" /> enumeration specifying either Binary or Xml serialization.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00017C8C File Offset: 0x00015E8C
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00017CBC File Offset: 0x00015EBC
		[DefaultValue(SerializationFormat.Xml)]
		public SerializationFormat RemotingFormat
		{
			get
			{
				if (this.dataSet != null)
				{
					this.remotingFormat = this.dataSet.RemotingFormat;
				}
				return this.remotingFormat;
			}
			set
			{
				if (this.dataSet != null)
				{
					throw new ArgumentException("Cannot have different remoting format property value for DataSet and DataTable");
				}
				this.remotingFormat = value;
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00017CDC File Offset: 0x00015EDC
		internal void DeserializeConstraints(ArrayList arrayList)
		{
			bool flag = false;
			for (int i = 0; i < arrayList.Count; i++)
			{
				ArrayList arrayList2 = arrayList[i] as ArrayList;
				if (arrayList2 != null)
				{
					if ((string)arrayList2[0] == "F")
					{
						int[] array = arrayList2[2] as int[];
						if (array != null)
						{
							ArrayList arrayList3 = new ArrayList();
							DataTable dataTable = this.dataSet.Tables[array[0]];
							for (int j = 0; j < array.Length - 1; j++)
							{
								arrayList3.Add(dataTable.Columns[array[j + 1]]);
							}
							array = arrayList2[3] as int[];
							if (array != null)
							{
								ArrayList arrayList4 = new ArrayList();
								dataTable = this.dataSet.Tables[array[0]];
								for (int k = 0; k < array.Length - 1; k++)
								{
									arrayList4.Add(dataTable.Columns[array[k + 1]]);
								}
								ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint((string)arrayList2[1], (DataColumn[])arrayList3.ToArray(typeof(DataColumn)), (DataColumn[])arrayList4.ToArray(typeof(DataColumn)));
								Array array2 = (Array)arrayList2[4];
								foreignKeyConstraint.AcceptRejectRule = (AcceptRejectRule)((int)array2.GetValue(0));
								foreignKeyConstraint.UpdateRule = (Rule)((int)array2.GetValue(1));
								foreignKeyConstraint.DeleteRule = (Rule)((int)array2.GetValue(2));
								foreignKeyConstraint.SetExtendedProperties((PropertyCollection)arrayList2[5]);
								this.Constraints.Add(foreignKeyConstraint);
								flag = true;
							}
						}
					}
					else if (!flag && (string)arrayList2[0] == "U")
					{
						ArrayList arrayList5 = new ArrayList();
						int[] array3 = arrayList2[2] as int[];
						if (array3 != null)
						{
							for (int l = 0; l < array3.Length; l++)
							{
								arrayList5.Add(this.Columns[array3[l]]);
							}
							UniqueConstraint uniqueConstraint = new UniqueConstraint((string)arrayList2[1], (DataColumn[])arrayList5.ToArray(typeof(DataColumn)), (bool)arrayList2[3]);
							if (this.Constraints.IndexOf(uniqueConstraint) == -1 && this.Constraints.IndexOf((string)arrayList2[1]) == -1)
							{
								uniqueConstraint.SetExtendedProperties((PropertyCollection)arrayList2[4]);
								this.Constraints.Add(uniqueConstraint);
							}
						}
					}
					else
					{
						flag = false;
					}
				}
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00017FBC File Offset: 0x000161BC
		private DataRowState GetCurrentRowState(BitArray rowStateBitArray, int i)
		{
			DataRowState dataRowState;
			if (!rowStateBitArray[i] && !rowStateBitArray[i + 1] && rowStateBitArray[i + 2])
			{
				dataRowState = DataRowState.Detached;
			}
			else if (!rowStateBitArray[i] && !rowStateBitArray[i + 1] && !rowStateBitArray[i + 2])
			{
				dataRowState = DataRowState.Unchanged;
			}
			else if (!rowStateBitArray[i] && rowStateBitArray[i + 1] && !rowStateBitArray[i + 2])
			{
				dataRowState = DataRowState.Added;
			}
			else if (rowStateBitArray[i] && rowStateBitArray[i + 1] && !rowStateBitArray[i + 2])
			{
				dataRowState = DataRowState.Deleted;
			}
			else
			{
				dataRowState = DataRowState.Modified;
			}
			return dataRowState;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001808C File Offset: 0x0001628C
		internal void DeserializeRecords(ArrayList arrayList, ArrayList nullBits, BitArray rowStateBitArray)
		{
			if (arrayList == null || arrayList.Count < 1)
			{
				return;
			}
			int length = ((Array)arrayList[0]).Length;
			object[] array = new object[arrayList.Count];
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				DataRowState currentRowState = this.GetCurrentRowState(rowStateBitArray, num * 3);
				for (int j = 0; j < arrayList.Count; j++)
				{
					Array array2 = (Array)arrayList[j];
					BitArray bitArray = (BitArray)nullBits[j];
					if (!bitArray[i])
					{
						array[j] = array2.GetValue(i);
					}
					else
					{
						array[j] = null;
					}
				}
				this.LoadDataRow(array, false);
				if (currentRowState == DataRowState.Modified)
				{
					this.Rows[num].AcceptChanges();
					i++;
					for (int k = 0; k < arrayList.Count; k++)
					{
						Array array3 = (Array)arrayList[k];
						BitArray bitArray = (BitArray)nullBits[k];
						if (!bitArray[i])
						{
							this.Rows[num][k] = array3.GetValue(i);
						}
						else
						{
							this.Rows[num][k] = null;
						}
					}
				}
				else if (currentRowState == DataRowState.Unchanged)
				{
					this.Rows[num].AcceptChanges();
				}
				else if (currentRowState == DataRowState.Deleted)
				{
					this.Rows[num].AcceptChanges();
					this.Rows[num].Delete();
				}
				num++;
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00018240 File Offset: 0x00016440
		private void BinaryDeserializeTable(SerializationInfo info)
		{
			this.TableName = info.GetString("DataTable.TableName");
			this.Namespace = info.GetString("DataTable.Namespace");
			this.Prefix = info.GetString("DataTable.Prefix");
			this.CaseSensitive = info.GetBoolean("DataTable.CaseSensitive");
			this.Locale = new CultureInfo(info.GetInt32("DataTable.LocaleLCID"));
			this._extendedProperties = (PropertyCollection)info.GetValue("DataTable.ExtendedProperties", typeof(PropertyCollection));
			this.MinimumCapacity = info.GetInt32("DataTable.MinimumCapacity");
			int @int = info.GetInt32("DataTable.Columns.Count");
			for (int i = 0; i < @int; i++)
			{
				this.Columns.Add();
				string text = "DataTable.DataColumn_" + i + ".";
				this.Columns[i].ColumnName = info.GetString(text + "ColumnName");
				this.Columns[i].Namespace = info.GetString(text + "Namespace");
				this.Columns[i].Caption = info.GetString(text + "Caption");
				this.Columns[i].Prefix = info.GetString(text + "Prefix");
				this.Columns[i].DataType = (Type)info.GetValue(text + "DataType", typeof(Type));
				this.Columns[i].DefaultValue = info.GetValue(text + "DefaultValue", typeof(object));
				this.Columns[i].AllowDBNull = info.GetBoolean(text + "AllowDBNull");
				this.Columns[i].AutoIncrement = info.GetBoolean(text + "AutoIncrement");
				this.Columns[i].AutoIncrementStep = info.GetInt64(text + "AutoIncrementStep");
				this.Columns[i].AutoIncrementSeed = info.GetInt64(text + "AutoIncrementSeed");
				this.Columns[i].ReadOnly = info.GetBoolean(text + "ReadOnly");
				this.Columns[i].MaxLength = info.GetInt32(text + "MaxLength");
				this.Columns[i].ExtendedProperties = (PropertyCollection)info.GetValue(text + "ExtendedProperties", typeof(PropertyCollection));
				if (this.Columns[i].DataType == typeof(DataSetDateTime))
				{
					this.Columns[i].DateTimeMode = (DataSetDateTime)((int)info.GetValue(text + "DateTimeMode", typeof(DataSetDateTime)));
				}
				this.Columns[i].ColumnMapping = (MappingType)((int)info.GetValue(text + "ColumnMapping", typeof(MappingType)));
				try
				{
					this.Columns[i].Expression = info.GetString(text + "Expression");
					text = "DataTable_0.";
					ArrayList arrayList = (ArrayList)info.GetValue(text + "Constraints", typeof(ArrayList));
					if (this.Constraints == null)
					{
						this.Constraints = new ConstraintCollection(this);
					}
					this.DeserializeConstraints(arrayList);
				}
				catch (SerializationException)
				{
				}
			}
			try
			{
				string text2 = "DataTable_0.";
				ArrayList arrayList2 = (ArrayList)info.GetValue(text2 + "NullBits", typeof(ArrayList));
				ArrayList arrayList = (ArrayList)info.GetValue(text2 + "Records", typeof(ArrayList));
				BitArray bitArray = (BitArray)info.GetValue(text2 + "RowStates", typeof(BitArray));
				Hashtable hashtable = (Hashtable)info.GetValue(text2 + "RowErrors", typeof(Hashtable));
				this.DeserializeRecords(arrayList, arrayList2, bitArray);
			}
			catch (SerializationException)
			{
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000186CC File Offset: 0x000168CC
		internal void BinarySerializeProperty(SerializationInfo info)
		{
			Version version = new Version(2, 0);
			info.AddValue("DataTable.RemotingVersion", version);
			info.AddValue("DataTable.RemotingFormat", this.RemotingFormat);
			info.AddValue("DataTable.TableName", this.TableName);
			info.AddValue("DataTable.Namespace", this.Namespace);
			info.AddValue("DataTable.Prefix", this.Prefix);
			info.AddValue("DataTable.CaseSensitive", this.CaseSensitive);
			info.AddValue("DataTable.caseSensitiveAmbient", true);
			info.AddValue("DataTable.NestedInDataSet", true);
			info.AddValue("DataTable.RepeatableElement", false);
			info.AddValue("DataTable.LocaleLCID", this.Locale.LCID);
			info.AddValue("DataTable.MinimumCapacity", this.MinimumCapacity);
			info.AddValue("DataTable.Columns.Count", this.Columns.Count);
			info.AddValue("DataTable.ExtendedProperties", this._extendedProperties);
			for (int i = 0; i < this.Columns.Count; i++)
			{
				info.AddValue("DataTable.DataColumn_" + i + ".ColumnName", this.Columns[i].ColumnName);
				info.AddValue("DataTable.DataColumn_" + i + ".Namespace", this.Columns[i].Namespace);
				info.AddValue("DataTable.DataColumn_" + i + ".Caption", this.Columns[i].Caption);
				info.AddValue("DataTable.DataColumn_" + i + ".Prefix", this.Columns[i].Prefix);
				info.AddValue("DataTable.DataColumn_" + i + ".DataType", this.Columns[i].DataType, typeof(Type));
				info.AddValue("DataTable.DataColumn_" + i + ".DefaultValue", this.Columns[i].DefaultValue, typeof(DBNull));
				info.AddValue("DataTable.DataColumn_" + i + ".AllowDBNull", this.Columns[i].AllowDBNull);
				info.AddValue("DataTable.DataColumn_" + i + ".AutoIncrement", this.Columns[i].AutoIncrement);
				info.AddValue("DataTable.DataColumn_" + i + ".AutoIncrementStep", this.Columns[i].AutoIncrementStep);
				info.AddValue("DataTable.DataColumn_" + i + ".AutoIncrementSeed", this.Columns[i].AutoIncrementSeed);
				info.AddValue("DataTable.DataColumn_" + i + ".ReadOnly", this.Columns[i].ReadOnly);
				info.AddValue("DataTable.DataColumn_" + i + ".MaxLength", this.Columns[i].MaxLength);
				info.AddValue("DataTable.DataColumn_" + i + ".ExtendedProperties", this.Columns[i].ExtendedProperties);
				info.AddValue("DataTable.DataColumn_" + i + ".DateTimeMode", this.Columns[i].DateTimeMode);
				info.AddValue("DataTable.DataColumn_" + i + ".ColumnMapping", this.Columns[i].ColumnMapping, typeof(MappingType));
				info.AddValue("DataTable.DataColumn_" + i + ".SimpleType", null, typeof(string));
				info.AddValue("DataTable.DataColumn_" + i + ".AutoIncrementCurrent", this.Columns[i].AutoIncrementValue());
				info.AddValue("DataTable.DataColumn_" + i + ".XmlDataType", null, typeof(string));
			}
			info.AddValue("DataTable.TypeName", null, typeof(string));
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00018B20 File Offset: 0x00016D20
		internal void SerializeConstraints(SerializationInfo info, string prefix)
		{
			ArrayList arrayList = new ArrayList();
			int i = 0;
			while (i < this.Constraints.Count)
			{
				ArrayList arrayList2 = new ArrayList();
				if (this.Constraints[i] is UniqueConstraint)
				{
					arrayList2.Add("U");
					UniqueConstraint uniqueConstraint = (UniqueConstraint)this.Constraints[i];
					arrayList2.Add(uniqueConstraint.ConstraintName);
					DataColumn[] columns = uniqueConstraint.Columns;
					int[] array = new int[columns.Length];
					for (int j = 0; j < columns.Length; j++)
					{
						array[j] = uniqueConstraint.Table.Columns.IndexOf(uniqueConstraint.Columns[j]);
					}
					arrayList2.Add(array);
					arrayList2.Add(uniqueConstraint.IsPrimaryKey);
					arrayList2.Add(uniqueConstraint.ExtendedProperties);
					goto IL_022C;
				}
				if (this.Constraints[i] is ForeignKeyConstraint)
				{
					arrayList2.Add("F");
					ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)this.Constraints[i];
					arrayList2.Add(foreignKeyConstraint.ConstraintName);
					int[] array2 = new int[foreignKeyConstraint.RelatedColumns.Length + 1];
					array2[0] = this.DataSet.Tables.IndexOf(foreignKeyConstraint.RelatedTable);
					for (int k = 0; k < foreignKeyConstraint.Columns.Length; k++)
					{
						array2[k + 1] = foreignKeyConstraint.RelatedColumns[k].Ordinal;
					}
					arrayList2.Add(array2);
					array2 = new int[foreignKeyConstraint.Columns.Length + 1];
					array2[0] = this.DataSet.Tables.IndexOf(foreignKeyConstraint.Table);
					for (int l = 0; l < foreignKeyConstraint.Columns.Length; l++)
					{
						array2[l + 1] = foreignKeyConstraint.Columns[l].Ordinal;
					}
					arrayList2.Add(array2);
					arrayList2.Add(new int[]
					{
						(int)foreignKeyConstraint.AcceptRejectRule,
						(int)foreignKeyConstraint.UpdateRule,
						(int)foreignKeyConstraint.DeleteRule
					});
					arrayList2.Add(foreignKeyConstraint.ExtendedProperties);
					goto IL_022C;
				}
				IL_0234:
				i++;
				continue;
				IL_022C:
				arrayList.Add(arrayList2);
				goto IL_0234;
			}
			info.AddValue(prefix, arrayList, typeof(ArrayList));
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00018D88 File Offset: 0x00016F88
		internal void BinarySerialize(SerializationInfo info, string prefix)
		{
			int count = this.Columns.Count;
			int count2 = this.Rows.Count;
			int num = this.Rows.Count;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			BitArray bitArray = new BitArray(count2 * 3);
			for (int i = 0; i < this.Rows.Count; i++)
			{
				if (this.Rows[i].RowState == DataRowState.Modified)
				{
					num++;
				}
			}
			this.SerializeConstraints(info, prefix + "Constraints");
			for (int j = 0; j < count; j++)
			{
				if (count2 != 0)
				{
					BitArray bitArray2 = new BitArray(count2);
					DataColumn dataColumn = this.Columns[j];
					Array array = Array.CreateInstance(dataColumn.DataType, num);
					int k = 0;
					int num2 = 0;
					while (k < this.Rows.Count)
					{
						DataRow dataRow = this.Rows[k];
						DataRowVersion dataRowVersion;
						if (dataRow.RowState == DataRowState.Modified)
						{
							dataRowVersion = DataRowVersion.Default;
							bitArray2.Length++;
							if (!dataRow.IsNull(dataColumn, dataRowVersion))
							{
								bitArray2[num2] = false;
								array.SetValue(dataRow[j, dataRowVersion], num2);
							}
							else
							{
								bitArray2[num2] = true;
							}
							num2++;
							dataRowVersion = DataRowVersion.Current;
						}
						else if (dataRow.RowState == DataRowState.Deleted)
						{
							dataRowVersion = DataRowVersion.Original;
						}
						else
						{
							dataRowVersion = DataRowVersion.Default;
						}
						if (!dataRow.IsNull(dataColumn, dataRowVersion))
						{
							bitArray2[num2] = false;
							array.SetValue(dataRow[j, dataRowVersion], num2);
						}
						else
						{
							bitArray2[num2] = true;
						}
						k++;
						num2++;
					}
					arrayList.Add(array);
					arrayList2.Add(bitArray2);
				}
			}
			for (int l = 0; l < this.Rows.Count; l++)
			{
				int num3 = l * 3;
				DataRowState rowState = this.Rows[l].RowState;
				if (rowState == DataRowState.Detached)
				{
					bitArray[num3] = false;
					bitArray[num3 + 1] = false;
					bitArray[num3 + 2] = true;
				}
				else if (rowState == DataRowState.Unchanged)
				{
					bitArray[num3] = false;
					bitArray[num3 + 1] = false;
					bitArray[num3 + 2] = false;
				}
				else if (rowState == DataRowState.Added)
				{
					bitArray[num3] = false;
					bitArray[num3 + 1] = true;
					bitArray[num3 + 2] = false;
				}
				else if (rowState == DataRowState.Deleted)
				{
					bitArray[num3] = true;
					bitArray[num3 + 1] = true;
					bitArray[num3 + 2] = false;
				}
				else
				{
					bitArray[num3] = true;
					bitArray[num3 + 1] = false;
					bitArray[num3 + 2] = false;
				}
			}
			info.AddValue(prefix + "Rows.Count", this.Rows.Count);
			info.AddValue(prefix + "Records.Count", num);
			info.AddValue(prefix + "Records", arrayList, typeof(ArrayList));
			info.AddValue(prefix + "NullBits", arrayList2, typeof(ArrayList));
			info.AddValue(prefix + "RowStates", bitArray, typeof(BitArray));
			Hashtable hashtable = new Hashtable();
			info.AddValue(prefix + "RowErrors", hashtable, typeof(Hashtable));
			Hashtable hashtable2 = new Hashtable();
			info.AddValue(prefix + "ColumnErrors", hashtable2, typeof(Hashtable));
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTableReader" /> corresponding to the data within this <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTableReader" /> containing one result set, corresponding to the source <see cref="T:System.Data.DataTable" /> instance.</returns>
		// Token: 0x060003E7 RID: 999 RVA: 0x00019168 File Offset: 0x00017368
		public DataTableReader CreateDataReader()
		{
			return new DataTableReader(this);
		}

		/// <summary>Fills a <see cref="T:System.Data.DataTable" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />. If the <see cref="T:System.Data.DataTable" /> already contains rows, the incoming data from the data source is merged with the existing rows.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides a result set.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003E8 RID: 1000 RVA: 0x00019170 File Offset: 0x00017370
		public void Load(IDataReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("Value cannot be null. Parameter name: reader");
			}
			this.Load(reader, LoadOption.PreserveChanges);
		}

		/// <summary>Fills a <see cref="T:System.Data.DataTable" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />. If the DataTable already contains rows, the incoming data from the data source is merged with the existing rows according to the value of the <paramref name="loadOption" /> parameter.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets.</param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> are combined with incoming rows that share the same primary key. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003E9 RID: 1001 RVA: 0x0001918C File Offset: 0x0001738C
		public void Load(IDataReader reader, LoadOption loadOption)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("Value cannot be null. Parameter name: reader");
			}
			bool flag = this.EnforceConstraints;
			try
			{
				this.EnforceConstraints = false;
				int[] array = DataAdapter.BuildSchema(reader, this, SchemaType.Mapped, MissingSchemaAction.AddWithKey, MissingMappingAction.Passthrough, new DataTableMappingCollection());
				DbDataAdapter.FillFromReader(this, reader, 0, 0, array, loadOption);
			}
			finally
			{
				this.EnforceConstraints = flag;
			}
		}

		/// <summary>Fills a <see cref="T:System.Data.DataTable" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" /> using an error-handling delegate.</summary>
		/// <param name="reader">A <see cref="T:System.Data.IDataReader" /> that provides a result set.</param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> are combined with incoming rows that share the same primary key. </param>
		/// <param name="errorHandler">A <see cref="T:System.Data.FillErrorEventHandler" /> delegate to call when an error occurs while loading data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003EA RID: 1002 RVA: 0x00019200 File Offset: 0x00017400
		public virtual void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler errorHandler)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("Value cannot be null. Parameter name: reader");
			}
			bool flag = this.EnforceConstraints;
			try
			{
				this.EnforceConstraints = false;
				int[] array = DataAdapter.BuildSchema(reader, this, SchemaType.Mapped, MissingSchemaAction.AddWithKey, MissingMappingAction.Passthrough, new DataTableMappingCollection());
				DbDataAdapter.FillFromReader(this, reader, 0, 0, array, loadOption, errorHandler);
			}
			finally
			{
				this.EnforceConstraints = flag;
			}
		}

		/// <summary>Finds and updates a specific row. If no matching row is found, a new row is created using the given values.</summary>
		/// <returns>The new <see cref="T:System.Data.DataRow" />.</returns>
		/// <param name="values">An array of values used to create the new row. </param>
		/// <param name="loadOption">Used to determine how the array values are applied to the corresponding values in an existing row. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003EB RID: 1003 RVA: 0x00019274 File Offset: 0x00017474
		public DataRow LoadDataRow(object[] values, LoadOption loadOption)
		{
			DataRow dataRow = null;
			if (this.PrimaryKey.Length > 0)
			{
				object[] array = new object[this.PrimaryKey.Length];
				for (int i = 0; i < this.PrimaryKey.Length; i++)
				{
					array[i] = values[this.PrimaryKey[i].Ordinal];
				}
				dataRow = this.Rows.Find(array, DataViewRowState.OriginalRows);
				if (dataRow == null)
				{
					dataRow = this.Rows.Find(array);
				}
			}
			if (dataRow == null || (dataRow.RowState == DataRowState.Deleted && loadOption == LoadOption.Upsert))
			{
				dataRow = this.NewNotInitializedRow();
				dataRow.ImportRecord(this.CreateRecord(values));
				dataRow.Validate();
				if (loadOption == LoadOption.OverwriteChanges || loadOption == LoadOption.PreserveChanges)
				{
					this.Rows.AddInternal(dataRow, DataRowAction.ChangeCurrentAndOriginal);
				}
				else
				{
					this.Rows.AddInternal(dataRow);
				}
				return dataRow;
			}
			dataRow.Load(values, loadOption);
			return dataRow;
		}

		/// <summary>Merge the specified <see cref="T:System.Data.DataTable" /> with the current <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> to be merged with the current <see cref="T:System.Data.DataTable" />.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003EC RID: 1004 RVA: 0x00019358 File Offset: 0x00017558
		public void Merge(DataTable table)
		{
			this.Merge(table, false, MissingSchemaAction.Add);
		}

		/// <summary>Merge the specified <see cref="T:System.Data.DataTable" /> with the current DataTable, indicating whether to preserve changes in the current DataTable.</summary>
		/// <param name="table">The DataTable to be merged with the current DataTable.</param>
		/// <param name="preserveChanges">true, to preserve changes in the current DataTable; otherwise false. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003ED RID: 1005 RVA: 0x00019364 File Offset: 0x00017564
		public void Merge(DataTable table, bool preserveChanges)
		{
			this.Merge(table, preserveChanges, MissingSchemaAction.Add);
		}

		/// <summary>Merge the specified <see cref="T:System.Data.DataTable" /> with the current DataTable, indicating whether to preserve changes and how to handle missing schema in the current DataTable.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> to be merged with the current <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="preserveChanges">true, to preserve changes in the current <see cref="T:System.Data.DataTable" />; otherwise false.</param>
		/// <param name="missingSchemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060003EE RID: 1006 RVA: 0x00019370 File Offset: 0x00017570
		public void Merge(DataTable table, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			MergeManager.Merge(this, table, preserveChanges, missingSchemaAction);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001937C File Offset: 0x0001757C
		internal int CompareRecords(int x, int y)
		{
			for (int i = 0; i < this.Columns.Count; i++)
			{
				int num = this.Columns[i].DataContainer.CompareValues(x, y);
				if (num != 0)
				{
					return num;
				}
			}
			return 0;
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.TableCleared" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataTableClearEventArgs" /> that contains the event data. </param>
		// Token: 0x060003F0 RID: 1008 RVA: 0x000193C8 File Offset: 0x000175C8
		protected virtual void OnTableCleared(DataTableClearEventArgs e)
		{
			if (this.TableCleared != null)
			{
				this.TableCleared(this, e);
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000193E4 File Offset: 0x000175E4
		internal void DataTableCleared()
		{
			this.OnTableCleared(new DataTableClearEventArgs(this));
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.TableClearing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataTableClearEventArgs" /> that contains the event data. </param>
		// Token: 0x060003F2 RID: 1010 RVA: 0x000193F4 File Offset: 0x000175F4
		protected virtual void OnTableClearing(DataTableClearEventArgs e)
		{
			if (this.TableClearing != null)
			{
				this.TableClearing(this, e);
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00019410 File Offset: 0x00017610
		internal void DataTableClearing()
		{
			this.OnTableClearing(new DataTableClearEventArgs(this));
		}

		/// <summary>Raises the <see cref="E:System.Data.DataTable.TableNewRow" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Data.DataTableNewRowEventArgs" /> that contains the event data. </param>
		// Token: 0x060003F4 RID: 1012 RVA: 0x00019420 File Offset: 0x00017620
		protected virtual void OnTableNewRow(DataTableNewRowEventArgs e)
		{
			if (this.TableNewRow != null)
			{
				this.TableNewRow(this, e);
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001943C File Offset: 0x0001763C
		private void NewRowAdded(DataRow dr)
		{
			this.OnTableNewRow(new DataTableNewRowEventArgs(dr));
		}

		// Token: 0x0400012D RID: 301
		internal DataSet dataSet;

		// Token: 0x0400012E RID: 302
		private bool _caseSensitive;

		// Token: 0x0400012F RID: 303
		private DataColumnCollection _columnCollection;

		// Token: 0x04000130 RID: 304
		private ConstraintCollection _constraintCollection;

		// Token: 0x04000131 RID: 305
		private DataView _defaultView;

		// Token: 0x04000132 RID: 306
		private string _displayExpression;

		// Token: 0x04000133 RID: 307
		private PropertyCollection _extendedProperties;

		// Token: 0x04000134 RID: 308
		private bool _hasErrors;

		// Token: 0x04000135 RID: 309
		private CultureInfo _locale;

		// Token: 0x04000136 RID: 310
		private int _minimumCapacity;

		// Token: 0x04000137 RID: 311
		private string _nameSpace;

		// Token: 0x04000138 RID: 312
		private DataRelationCollection _childRelations;

		// Token: 0x04000139 RID: 313
		private DataRelationCollection _parentRelations;

		// Token: 0x0400013A RID: 314
		private string _prefix;

		// Token: 0x0400013B RID: 315
		private UniqueConstraint _primaryKeyConstraint;

		// Token: 0x0400013C RID: 316
		private DataRowCollection _rows;

		// Token: 0x0400013D RID: 317
		private ISite _site;

		// Token: 0x0400013E RID: 318
		private string _tableName;

		// Token: 0x0400013F RID: 319
		private bool _containsListCollection;

		// Token: 0x04000140 RID: 320
		private string _encodedTableName;

		// Token: 0x04000141 RID: 321
		internal bool _duringDataLoad;

		// Token: 0x04000142 RID: 322
		internal bool _nullConstraintViolationDuringDataLoad;

		// Token: 0x04000143 RID: 323
		private bool dataSetPrevEnforceConstraints;

		// Token: 0x04000144 RID: 324
		private bool dataTablePrevEnforceConstraints;

		// Token: 0x04000145 RID: 325
		private bool enforceConstraints = true;

		// Token: 0x04000146 RID: 326
		private DataRowBuilder _rowBuilder;

		// Token: 0x04000147 RID: 327
		private ArrayList _indexes;

		// Token: 0x04000148 RID: 328
		private RecordCache _recordCache;

		// Token: 0x04000149 RID: 329
		private int _defaultValuesRowIndex = -1;

		// Token: 0x0400014A RID: 330
		protected internal bool fInitInProgress;

		// Token: 0x0400014B RID: 331
		private bool _virginCaseSensitive = true;

		// Token: 0x0400014C RID: 332
		private PropertyDescriptorCollection _propertyDescriptorsCache;

		// Token: 0x0400014D RID: 333
		private static DataColumn[] _emptyColumnArray = new DataColumn[0];

		// Token: 0x0400014E RID: 334
		private static Regex SortRegex = new Regex("^((\\[(?<ColName>.+)\\])|(?<ColName>\\S+))([ ]+(?<Order>ASC|DESC))?$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

		// Token: 0x0400014F RID: 335
		private DataColumn[] _latestPrimaryKeyCols;

		// Token: 0x04000150 RID: 336
		private DataRow[] empty_rows;

		// Token: 0x04000151 RID: 337
		private bool tableInitialized = true;

		// Token: 0x04000152 RID: 338
		private SerializationFormat remotingFormat;
	}
}
