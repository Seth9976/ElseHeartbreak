using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data
{
	/// <summary>Represents an in-memory cache of data.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000031 RID: 49
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.DataSetToolboxItem, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[XmlRoot("DataSet")]
	[XmlSchemaProvider("GetDataSetSchema")]
	[Designer("Microsoft.VSDesigner.Data.VS.DataSetDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("DataSetName")]
	[Serializable]
	public class DataSet : MarshalByValueComponent, IXmlSerializable, IListSource, ISupportInitialize, ISerializable, ISupportInitializeNotification
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataSet" /> class.</summary>
		// Token: 0x060002A1 RID: 673 RVA: 0x00011854 File Offset: 0x0000FA54
		public DataSet()
			: this("NewDataSet")
		{
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.DataSet" /> class with the given name.</summary>
		/// <param name="dataSetName">The name of the <see cref="T:System.Data.DataSet" />.</param>
		// Token: 0x060002A2 RID: 674 RVA: 0x00011864 File Offset: 0x0000FA64
		public DataSet(string dataSetName)
		{
			this.dataSetName = dataSetName;
			this.tableCollection = new DataTableCollection(this);
			this.relationCollection = new DataRelationCollection.DataSetRelationCollection(this);
			this.properties = new PropertyCollection();
			this.prefix = string.Empty;
		}

		/// <param name="info">The data needed to serialize or deserialize an object. </param>
		/// <param name="context">The source and destination of a given serialized stream. </param>
		// Token: 0x060002A3 RID: 675 RVA: 0x000118C8 File Offset: 0x0000FAC8
		protected DataSet(SerializationInfo info, StreamingContext context)
			: this()
		{
			if (this.IsBinarySerialized(info, context))
			{
				this.BinaryDeserialize(info);
				return;
			}
			string text = info.GetValue("XmlSchema", typeof(string)) as string;
			XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(text));
			this.ReadXmlSchema(xmlTextReader);
			xmlTextReader.Close();
			this.GetSerializationData(info, context);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0001192C File Offset: 0x0000FB2C
		protected DataSet(SerializationInfo info, StreamingContext context, bool constructSchema)
			: this()
		{
			if (this.DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.ExcludeSchema)
			{
				this.InitializeDerivedDataSet();
			}
			if (this.IsBinarySerialized(info, context))
			{
				this.BinaryDeserialize(info);
				return;
			}
			if (constructSchema)
			{
				string text = info.GetValue("XmlSchema", typeof(string)) as string;
				XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(text));
				this.ReadXmlSchema(xmlTextReader);
				xmlTextReader.Close();
				this.GetSerializationData(info, context);
			}
		}

		/// <summary>Occurs when a target and source <see cref="T:System.Data.DataRow" /> have the same primary key value, and <see cref="P:System.Data.DataSet.EnforceConstraints" /> is set to true.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060002A5 RID: 677 RVA: 0x000119AC File Offset: 0x0000FBAC
		// (remove) Token: 0x060002A6 RID: 678 RVA: 0x000119C8 File Offset: 0x0000FBC8
		[DataCategory("Action")]
		public event MergeFailedEventHandler MergeFailed;

		/// <summary>Occurs after the <see cref="T:System.Data.DataSet" /> is initialized.</summary>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060002A7 RID: 679 RVA: 0x000119E4 File Offset: 0x0000FBE4
		// (remove) Token: 0x060002A8 RID: 680 RVA: 0x00011A00 File Offset: 0x0000FC00
		public event EventHandler Initialized;

		// Token: 0x060002A9 RID: 681 RVA: 0x00011A1C File Offset: 0x0000FC1C
		IList IListSource.GetList()
		{
			return this.DefaultViewManager;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00011A24 File Offset: 0x0000FC24
		bool IListSource.ContainsListCollection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00011A28 File Offset: 0x0000FC28
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			this.ReadXmlSerializable(reader);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00011A34 File Offset: 0x0000FC34
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.DoWriteXmlSchema(writer);
			this.WriteXml(writer, XmlWriteMode.DiffGram);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00011A48 File Offset: 0x0000FC48
		XmlSchema IXmlSerializable.GetSchema()
		{
			if (base.GetType() == typeof(DataSet))
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, null);
			this.WriteXmlSchema(xmlTextWriter);
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}

		/// <summary>Gets or sets a value indicating whether string comparisons within <see cref="T:System.Data.DataTable" /> objects are case-sensitive.</summary>
		/// <returns>true if string comparisons are case-sensitive; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00011A98 File Offset: 0x0000FC98
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		[DefaultValue(false)]
		[DataCategory("Data")]
		public bool CaseSensitive
		{
			get
			{
				return this.caseSensitive;
			}
			set
			{
				this.caseSensitive = value;
				if (!this.caseSensitive)
				{
					foreach (object obj in this.Tables)
					{
						DataTable dataTable = (DataTable)obj;
						dataTable.ResetCaseSensitiveIndexes();
						foreach (object obj2 in dataTable.Constraints)
						{
							Constraint constraint = (Constraint)obj2;
							constraint.AssertConstraint();
						}
					}
				}
				else
				{
					foreach (object obj3 in this.Tables)
					{
						DataTable dataTable2 = (DataTable)obj3;
						dataTable2.ResetCaseSensitiveIndexes();
					}
				}
			}
		}

		/// <summary>Gets or sets the name of the current <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x00011BF8 File Offset: 0x0000FDF8
		[DefaultValue("")]
		[DataCategory("Data")]
		public string DataSetName
		{
			get
			{
				return this.dataSetName;
			}
			set
			{
				this.dataSetName = value;
			}
		}

		/// <summary>Gets a custom view of the data contained in the <see cref="T:System.Data.DataSet" /> to allow filtering, searching, and navigating using a custom <see cref="T:System.Data.DataViewManager" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewManager" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00011C04 File Offset: 0x0000FE04
		[Browsable(false)]
		public DataViewManager DefaultViewManager
		{
			get
			{
				if (this.defaultView == null)
				{
					this.defaultView = new DataViewManager(this);
				}
				return this.defaultView;
			}
		}

		/// <summary>Gets or sets a value indicating whether constraint rules are followed when attempting any update operation.</summary>
		/// <returns>true if rules are enforced; otherwise false. The default is true.</returns>
		/// <exception cref="T:System.Data.ConstraintException">One or more constraints cannot be enforced. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00011C24 File Offset: 0x0000FE24
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x00011C2C File Offset: 0x0000FE2C
		[DefaultValue(true)]
		public bool EnforceConstraints
		{
			get
			{
				return this.enforceConstraints;
			}
			set
			{
				this.InternalEnforceConstraints(value, true);
			}
		}

		/// <summary>Gets the collection of customized user information associated with the DataSet.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> with all custom user information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00011C38 File Offset: 0x0000FE38
		[Browsable(false)]
		[DataCategory("Data")]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets a value indicating whether there are errors in any of the <see cref="T:System.Data.DataTable" /> objects within this <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>true if any table contains an error;otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x00011C40 File Offset: 0x0000FE40
		[Browsable(false)]
		public bool HasErrors
		{
			get
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					if (this.Tables[i].HasErrors)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Gets or sets the locale information used to compare strings within the table.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that contains data about the user's machine locale. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00011C84 File Offset: 0x0000FE84
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		[DataCategory("Data")]
		public CultureInfo Locale
		{
			get
			{
				return (this.locale == null) ? Thread.CurrentThread.CurrentCulture : this.locale;
			}
			set
			{
				if (this.locale == null || !this.locale.Equals(value))
				{
					this.locale = value;
				}
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00011CDC File Offset: 0x0000FEDC
		internal bool LocaleSpecified
		{
			get
			{
				return this.locale != null;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00011CEC File Offset: 0x0000FEEC
		internal TableAdapterSchemaInfo TableAdapterSchemaData
		{
			get
			{
				return this.tableAdapterSchemaInfo;
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00011CF4 File Offset: 0x0000FEF4
		internal void InternalEnforceConstraints(bool value, bool resetIndexes)
		{
			if (value == this.enforceConstraints)
			{
				return;
			}
			if (value)
			{
				if (resetIndexes)
				{
					foreach (object obj in this.Tables)
					{
						DataTable dataTable = (DataTable)obj;
						dataTable.ResetIndexes();
					}
				}
				bool flag = false;
				foreach (object obj2 in this.Tables)
				{
					DataTable dataTable2 = (DataTable)obj2;
					foreach (object obj3 in dataTable2.Constraints)
					{
						Constraint constraint = (Constraint)obj3;
						constraint.AssertConstraint();
					}
					dataTable2.AssertNotNullConstraints();
					if (!flag && dataTable2.HasErrors)
					{
						flag = true;
					}
				}
				if (flag)
				{
					Constraint.ThrowConstraintException();
				}
			}
			this.enforceConstraints = value;
		}

		/// <summary>Merges an array of <see cref="T:System.Data.DataRow" /> objects into the current <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="rows">The array of DataRow objects to be merged into the DataSet. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002BC RID: 700 RVA: 0x00011E70 File Offset: 0x00010070
		public void Merge(DataRow[] rows)
		{
			this.Merge(rows, false, MissingSchemaAction.Add);
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema into the current DataSet.</summary>
		/// <param name="dataSet">The DataSet whose data and schema will be merged. </param>
		/// <exception cref="T:System.Data.ConstraintException">One or more constraints cannot be enabled. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002BD RID: 701 RVA: 0x00011E7C File Offset: 0x0001007C
		public void Merge(DataSet dataSet)
		{
			this.Merge(dataSet, false, MissingSchemaAction.Add);
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataTable" /> and its schema into the current <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> whose data and schema will be merged. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002BE RID: 702 RVA: 0x00011E88 File Offset: 0x00010088
		public void Merge(DataTable table)
		{
			this.Merge(table, false, MissingSchemaAction.Add);
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema into the current DataSet, preserving or discarding any changes in this DataSet according to the given argument.</summary>
		/// <param name="dataSet">The DataSet whose data and schema will be merged. </param>
		/// <param name="preserveChanges">true to preserve changes in the current DataSet; otherwise false. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002BF RID: 703 RVA: 0x00011E94 File Offset: 0x00010094
		public void Merge(DataSet dataSet, bool preserveChanges)
		{
			this.Merge(dataSet, preserveChanges, MissingSchemaAction.Add);
		}

		/// <summary>Merges an array of <see cref="T:System.Data.DataRow" /> objects into the current <see cref="T:System.Data.DataSet" />, preserving or discarding changes in the DataSet and handling an incompatible schema according to the given arguments.</summary>
		/// <param name="rows">The array of <see cref="T:System.Data.DataRow" /> objects to be merged into the DataSet. </param>
		/// <param name="preserveChanges">true to preserve changes in the DataSet; otherwise false. </param>
		/// <param name="missingSchemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002C0 RID: 704 RVA: 0x00011EA0 File Offset: 0x000100A0
		public void Merge(DataRow[] rows, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			if (rows == null)
			{
				throw new ArgumentNullException("rows");
			}
			if (!DataSet.IsLegalSchemaAction(missingSchemaAction))
			{
				throw new ArgumentOutOfRangeException("missingSchemaAction");
			}
			MergeManager.Merge(this, rows, preserveChanges, missingSchemaAction);
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema with the current DataSet, preserving or discarding changes in the current DataSet and handling an incompatible schema according to the given arguments.</summary>
		/// <param name="dataSet">The DataSet whose data and schema will be merged. </param>
		/// <param name="preserveChanges">true to preserve changes in the current DataSet; otherwise false. </param>
		/// <param name="missingSchemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002C1 RID: 705 RVA: 0x00011EE0 File Offset: 0x000100E0
		public void Merge(DataSet dataSet, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			if (!DataSet.IsLegalSchemaAction(missingSchemaAction))
			{
				throw new ArgumentOutOfRangeException("missingSchemaAction");
			}
			MergeManager.Merge(this, dataSet, preserveChanges, missingSchemaAction);
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataTable" /> and its schema into the current DataSet, preserving or discarding changes in the DataSet and handling an incompatible schema according to the given arguments.</summary>
		/// <param name="table">The DataTable whose data and schema will be merged. </param>
		/// <param name="preserveChanges">One of the <see cref="T:System.Data.MissingSchemaAction" /> values. </param>
		/// <param name="missingSchemaAction">true to preserve changes in the DataSet; otherwise false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002C2 RID: 706 RVA: 0x00011F20 File Offset: 0x00010120
		public void Merge(DataTable table, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			if (!DataSet.IsLegalSchemaAction(missingSchemaAction))
			{
				throw new ArgumentOutOfRangeException("missingSchemaAction");
			}
			MergeManager.Merge(this, table, preserveChanges, missingSchemaAction);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00011F60 File Offset: 0x00010160
		private static bool IsLegalSchemaAction(MissingSchemaAction missingSchemaAction)
		{
			return missingSchemaAction == MissingSchemaAction.Add || missingSchemaAction == MissingSchemaAction.AddWithKey || missingSchemaAction == MissingSchemaAction.Error || missingSchemaAction == MissingSchemaAction.Ignore;
		}

		/// <summary>Gets or sets the namespace of the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Data.DataSet" />.</returns>
		/// <exception cref="T:System.ArgumentException">The namespace already has data. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00011F84 File Offset: 0x00010184
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00011F8C File Offset: 0x0001018C
		[DataCategory("Data")]
		[DefaultValue("")]
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (value != this._namespace)
				{
					this.RaisePropertyChanging("Namespace");
				}
				this._namespace = value;
			}
		}

		/// <summary>Gets or sets an XML prefix that aliases the namespace of the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The XML prefix for the <see cref="T:System.Data.DataSet" /> namespace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00011FCC File Offset: 0x000101CC
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00011FD4 File Offset: 0x000101D4
		[DataCategory("Data")]
		[DefaultValue("")]
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				for (int i = 0; i < value.Length; i++)
				{
					if (!char.IsLetterOrDigit(value[i]) && value[i] != '_' && value[i] != ':')
					{
						throw new DataException("Prefix '" + value + "' is not valid, because it contains special characters.");
					}
				}
				if (value != this.prefix)
				{
					this.RaisePropertyChanging("Prefix");
				}
				this.prefix = value;
			}
		}

		/// <summary>Get the collection of relations that link tables and allow navigation from parent tables to child tables.</summary>
		/// <returns>A <see cref="T:System.Data.DataRelationCollection" /> that contains a collection of <see cref="T:System.Data.DataRelation" /> objects. An empty collection is returned if no <see cref="T:System.Data.DataRelation" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0001206C File Offset: 0x0001026C
		[DataCategory("Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataRelationCollection Relations
		{
			get
			{
				return this.relationCollection;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00012074 File Offset: 0x00010274
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0001207C File Offset: 0x0001027C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
			}
		}

		/// <summary>Gets the collection of tables contained in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The <see cref="T:System.Data.DataTableCollection" /> contained by this <see cref="T:System.Data.DataSet" />. An empty collection is returned if no <see cref="T:System.Data.DataTable" /> objects exist.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00012088 File Offset: 0x00010288
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DataCategory("Data")]
		public DataTableCollection Tables
		{
			get
			{
				return this.tableCollection;
			}
		}

		/// <summary>Commits all the changes made to this <see cref="T:System.Data.DataSet" /> since it was loaded or since the last time <see cref="M:System.Data.DataSet.AcceptChanges" /> was called.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002CC RID: 716 RVA: 0x00012090 File Offset: 0x00010290
		public void AcceptChanges()
		{
			foreach (object obj in this.tableCollection)
			{
				DataTable dataTable = (DataTable)obj;
				dataTable.AcceptChanges();
			}
		}

		/// <summary>Clears the <see cref="T:System.Data.DataSet" /> of any data by removing all rows in all tables.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002CD RID: 717 RVA: 0x00012100 File Offset: 0x00010300
		public void Clear()
		{
			if (this._xmlDataDocument != null)
			{
				throw new NotSupportedException("Clear function on dataset and datatable is not supported when XmlDataDocument is bound to the DataSet.");
			}
			bool flag = this.EnforceConstraints;
			this.EnforceConstraints = false;
			for (int i = 0; i < this.tableCollection.Count; i++)
			{
				this.tableCollection[i].Clear();
			}
			this.EnforceConstraints = flag;
		}

		/// <summary>Copies the structure of the <see cref="T:System.Data.DataSet" />, including all <see cref="T:System.Data.DataTable" /> schemas, relations, and constraints. Does not copy any data.</summary>
		/// <returns>A new <see cref="T:System.Data.DataSet" /> with the same schema as the current <see cref="T:System.Data.DataSet" />, but none of the data.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002CE RID: 718 RVA: 0x00012168 File Offset: 0x00010368
		public virtual DataSet Clone()
		{
			DataSet dataSet = (DataSet)Activator.CreateInstance(base.GetType(), true);
			this.CopyProperties(dataSet);
			foreach (object obj in this.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				if (!dataSet.Tables.Contains(dataTable.TableName))
				{
					dataSet.Tables.Add(dataTable.Clone());
				}
			}
			this.CopyRelations(dataSet);
			return dataSet;
		}

		/// <summary>Copies both the structure and data for this <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataSet" /> with the same structure (table schemas, relations, and constraints) and data as this <see cref="T:System.Data.DataSet" />.Note:If these classes have been subclassed, the copy will also be of the same subclasses.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002CF RID: 719 RVA: 0x00012218 File Offset: 0x00010418
		public DataSet Copy()
		{
			DataSet dataSet = (DataSet)Activator.CreateInstance(base.GetType(), true);
			this.CopyProperties(dataSet);
			foreach (object obj in this.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				if (!dataSet.Tables.Contains(dataTable.TableName))
				{
					dataSet.Tables.Add(dataTable.Copy());
				}
				else
				{
					foreach (object obj2 in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj2;
						dataSet.Tables[dataTable.TableName].ImportRow(dataRow);
					}
				}
			}
			this.CopyRelations(dataSet);
			return dataSet;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00012348 File Offset: 0x00010548
		private void CopyProperties(DataSet Copy)
		{
			Copy.CaseSensitive = this.CaseSensitive;
			Copy.DataSetName = this.DataSetName;
			Copy.EnforceConstraints = this.EnforceConstraints;
			if (this.ExtendedProperties.Count > 0)
			{
				Array array = Array.CreateInstance(typeof(object), this.ExtendedProperties.Count);
				this.ExtendedProperties.Keys.CopyTo(array, 0);
				for (int i = 0; i < this.ExtendedProperties.Count; i++)
				{
					Copy.ExtendedProperties.Add(array.GetValue(i), this.ExtendedProperties[array.GetValue(i)]);
				}
			}
			Copy.locale = this.locale;
			Copy.Namespace = this.Namespace;
			Copy.Prefix = this.Prefix;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001241C File Offset: 0x0001061C
		private void CopyRelations(DataSet Copy)
		{
			foreach (object obj in this.Relations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (!Copy.Relations.Contains(dataRelation.RelationName))
				{
					string tableName = dataRelation.ParentTable.TableName;
					string tableName2 = dataRelation.ChildTable.TableName;
					DataColumn[] array = new DataColumn[dataRelation.ParentColumns.Length];
					DataColumn[] array2 = new DataColumn[dataRelation.ChildColumns.Length];
					int num = 0;
					foreach (DataColumn dataColumn in dataRelation.ParentColumns)
					{
						array[num] = Copy.Tables[tableName].Columns[dataColumn.ColumnName];
						num++;
					}
					num = 0;
					foreach (DataColumn dataColumn2 in dataRelation.ChildColumns)
					{
						array2[num] = Copy.Tables[tableName2].Columns[dataColumn2.ColumnName];
						num++;
					}
					DataRelation dataRelation2 = new DataRelation(dataRelation.RelationName, array, array2, false);
					Copy.Relations.Add(dataRelation2);
				}
			}
			foreach (object obj2 in this.Tables)
			{
				DataTable dataTable = (DataTable)obj2;
				foreach (object obj3 in dataTable.Constraints)
				{
					Constraint constraint = (Constraint)obj3;
					if (constraint is ForeignKeyConstraint && !Copy.Tables[dataTable.TableName].Constraints.Contains(constraint.ConstraintName))
					{
						ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraint;
						DataTable dataTable2 = Copy.Tables[foreignKeyConstraint.RelatedTable.TableName];
						DataTable dataTable3 = Copy.Tables[dataTable.TableName];
						DataColumn[] array3 = new DataColumn[foreignKeyConstraint.RelatedColumns.Length];
						DataColumn[] array4 = new DataColumn[foreignKeyConstraint.Columns.Length];
						for (int k = 0; k < array3.Length; k++)
						{
							array3[k] = dataTable2.Columns[foreignKeyConstraint.RelatedColumns[k].ColumnName];
						}
						for (int l = 0; l < array4.Length; l++)
						{
							array4[l] = dataTable3.Columns[foreignKeyConstraint.Columns[l].ColumnName];
						}
						dataTable3.Constraints.Add(foreignKeyConstraint.ConstraintName, array3, array4);
					}
				}
			}
		}

		/// <summary>Gets a copy of the <see cref="T:System.Data.DataSet" /> that contains all changes made to it since it was loaded or since <see cref="M:System.Data.DataSet.AcceptChanges" /> was last called.</summary>
		/// <returns>A copy of the changes from this <see cref="T:System.Data.DataSet" /> that can have actions performed on it and later be merged back in using <see cref="M:System.Data.DataSet.Merge(System.Data.DataSet)" />. If no changed rows are found, the method returns null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002D2 RID: 722 RVA: 0x00012778 File Offset: 0x00010978
		public DataSet GetChanges()
		{
			return this.GetChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
		}

		/// <summary>Gets a copy of the <see cref="T:System.Data.DataSet" /> containing all changes made to it since it was last loaded, or since <see cref="M:System.Data.DataSet.AcceptChanges" /> was called, filtered by <see cref="T:System.Data.DataRowState" />.</summary>
		/// <returns>A filtered copy of the <see cref="T:System.Data.DataSet" /> that can have actions performed on it, and subsequently be merged back in using <see cref="M:System.Data.DataSet.Merge(System.Data.DataSet)" />. If no rows of the desired <see cref="T:System.Data.DataRowState" /> are found, the method returns null.</returns>
		/// <param name="rowStates">One of the <see cref="T:System.Data.DataRowState" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002D3 RID: 723 RVA: 0x00012784 File Offset: 0x00010984
		public DataSet GetChanges(DataRowState rowStates)
		{
			if (!this.HasChanges(rowStates))
			{
				return null;
			}
			DataSet dataSet = this.Clone();
			bool flag = dataSet.EnforceConstraints;
			dataSet.EnforceConstraints = false;
			Hashtable hashtable = new Hashtable();
			for (int i = 0; i < this.Tables.Count; i++)
			{
				DataTable dataTable = this.Tables[i];
				DataTable dataTable2 = dataSet.Tables[dataTable.TableName];
				for (int j = 0; j < dataTable.Rows.Count; j++)
				{
					DataRow dataRow = dataTable.Rows[j];
					if (dataRow.IsRowChanged(rowStates) && !hashtable.Contains(dataRow))
					{
						this.AddChangedRow(hashtable, dataTable2, dataRow);
					}
				}
			}
			dataSet.EnforceConstraints = flag;
			return dataSet;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0001285C File Offset: 0x00010A5C
		private void AddChangedRow(Hashtable addedRows, DataTable copyTable, DataRow row)
		{
			if (addedRows.ContainsKey(row))
			{
				return;
			}
			foreach (object obj in row.Table.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				DataRow dataRow = ((row.RowState == DataRowState.Deleted) ? row.GetParentRow(dataRelation, DataRowVersion.Original) : row.GetParentRow(dataRelation));
				if (dataRow != null)
				{
					DataTable dataTable = copyTable.DataSet.Tables[dataRow.Table.TableName];
					this.AddChangedRow(addedRows, dataTable, dataRow);
				}
			}
			DataRow dataRow2 = copyTable.NewNotInitializedRow();
			copyTable.Rows.AddInternal(dataRow2);
			row.CopyValuesToRow(dataRow2);
			dataRow2.XmlRowID = row.XmlRowID;
			addedRows.Add(row, row);
		}

		/// <summary>Returns the XML representation of the data stored in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A string that is a representation of the data stored in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002D5 RID: 725 RVA: 0x00012960 File Offset: 0x00010B60
		public string GetXml()
		{
			StringWriter stringWriter = new StringWriter();
			this.WriteXml(stringWriter, XmlWriteMode.IgnoreSchema);
			return stringWriter.ToString();
		}

		/// <summary>Returns the XML Schema for the XML representation of the data stored in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>String that is the XML Schema for the XML representation of the data stored in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002D6 RID: 726 RVA: 0x00012984 File Offset: 0x00010B84
		public string GetXmlSchema()
		{
			StringWriter stringWriter = new StringWriter();
			this.WriteXmlSchema(stringWriter);
			return stringWriter.ToString();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.DataSet" /> has changes, including new, deleted, or modified rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataSet" /> has changes; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002D7 RID: 727 RVA: 0x000129A4 File Offset: 0x00010BA4
		public bool HasChanges()
		{
			return this.HasChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.DataSet" /> has changes, including new, deleted, or modified rows, filtered by <see cref="T:System.Data.DataRowState" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataSet" /> has changes; otherwise false.</returns>
		/// <param name="rowStates">One of the <see cref="T:System.Data.DataRowState" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002D8 RID: 728 RVA: 0x000129B0 File Offset: 0x00010BB0
		public bool HasChanges(DataRowState rowStates)
		{
			if (((long)rowStates & (long)((ulong)(-32))) != 0L)
			{
				throw new ArgumentOutOfRangeException("rowStates");
			}
			DataTableCollection tables = this.Tables;
			for (int i = 0; i < tables.Count; i++)
			{
				DataTable dataTable = tables[i];
				DataRowCollection rows = dataTable.Rows;
				for (int j = 0; j < rows.Count; j++)
				{
					DataRow dataRow = rows[j];
					if ((dataRow.RowState & rowStates) != (DataRowState)0)
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Applies the XML schema from the specified <see cref="T:System.Xml.XmlReader" /> to the <see cref="T:System.Data.DataSet" />. </summary>
		/// <param name="reader">The XMLReader from which to read the schema. </param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002D9 RID: 729 RVA: 0x00012A3C File Offset: 0x00010C3C
		public void InferXmlSchema(XmlReader reader, string[] nsArray)
		{
			if (reader == null)
			{
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			this.InferXmlSchema(xmlDocument, nsArray);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00012A68 File Offset: 0x00010C68
		private void InferXmlSchema(XmlDocument doc, string[] nsArray)
		{
			XmlDataInferenceLoader.Infer(this, doc, XmlReadMode.InferSchema, nsArray);
		}

		/// <summary>Applies the XML schema from the specified <see cref="T:System.IO.Stream" /> to the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="stream">The Stream from which to read the schema. </param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002DB RID: 731 RVA: 0x00012A74 File Offset: 0x00010C74
		public void InferXmlSchema(Stream stream, string[] nsArray)
		{
			this.InferXmlSchema(new XmlTextReader(stream), nsArray);
		}

		/// <summary>Applies the XML schema from the specified <see cref="T:System.IO.TextReader" /> to the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The TextReader from which to read the schema. </param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002DC RID: 732 RVA: 0x00012A84 File Offset: 0x00010C84
		public void InferXmlSchema(TextReader reader, string[] nsArray)
		{
			this.InferXmlSchema(new XmlTextReader(reader), nsArray);
		}

		/// <summary>Applies the XML schema from the specified file to the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="fileName">The name of the file (including the path) from which to read the schema. </param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002DD RID: 733 RVA: 0x00012A94 File Offset: 0x00010C94
		public void InferXmlSchema(string fileName, string[] nsArray)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			try
			{
				this.InferXmlSchema(xmlTextReader, nsArray);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		/// <summary>Rolls back all the changes made to the <see cref="T:System.Data.DataSet" /> since it was created, or since the last time <see cref="M:System.Data.DataSet.AcceptChanges" /> was called.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002DE RID: 734 RVA: 0x00012AD8 File Offset: 0x00010CD8
		public virtual void RejectChanges()
		{
			bool flag = this.EnforceConstraints;
			this.EnforceConstraints = false;
			for (int i = 0; i < this.Tables.Count; i++)
			{
				this.Tables[i].RejectChanges();
			}
			this.EnforceConstraints = flag;
		}

		/// <summary>Resets the <see cref="T:System.Data.DataSet" /> to its original state. Subclasses should override <see cref="M:System.Data.DataSet.Reset" /> to restore a <see cref="T:System.Data.DataSet" /> to its original state.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060002DF RID: 735 RVA: 0x00012B28 File Offset: 0x00010D28
		public virtual void Reset()
		{
			for (int i = 0; i < this.Tables.Count; i++)
			{
				ConstraintCollection constraints = this.Tables[i].Constraints;
				for (int j = 0; j < constraints.Count; j++)
				{
					if (constraints[j] is ForeignKeyConstraint)
					{
						constraints.Remove(constraints[j]);
					}
				}
			}
			this.Clear();
			this.Relations.Clear();
			this.Tables.Clear();
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object used to write to a file. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002E0 RID: 736 RVA: 0x00012BB4 File Offset: 0x00010DB4
		public void WriteXml(Stream stream)
		{
			this.WriteXml(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented
			});
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> to the specified file.</summary>
		/// <param name="fileName">The file name (including the path) to which to write. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002E1 RID: 737 RVA: 0x00012BD8 File Offset: 0x00010DD8
		public void WriteXml(string fileName)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartDocument(true);
			try
			{
				this.WriteXml(xmlTextWriter);
			}
			finally
			{
				xmlTextWriter.WriteEndDocument();
				xmlTextWriter.Close();
			}
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> object with which to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002E2 RID: 738 RVA: 0x00012C30 File Offset: 0x00010E30
		public void WriteXml(TextWriter writer)
		{
			this.WriteXml(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented
			});
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002E3 RID: 739 RVA: 0x00012C54 File Offset: 0x00010E54
		public void WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> to the specified file using the specified <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="fileName">The file name (including the path) to which to write. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002E4 RID: 740 RVA: 0x00012C60 File Offset: 0x00010E60
		public void WriteXml(string fileName, XmlWriteMode mode)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartDocument(true);
			try
			{
				this.WriteXml(xmlTextWriter, mode);
			}
			finally
			{
				xmlTextWriter.WriteEndDocument();
				xmlTextWriter.Close();
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object used to write to a file. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002E5 RID: 741 RVA: 0x00012CBC File Offset: 0x00010EBC
		public void WriteXml(Stream stream, XmlWriteMode mode)
		{
			this.WriteXml(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented
			}, mode);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> object used to write the document. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002E6 RID: 742 RVA: 0x00012CE0 File Offset: 0x00010EE0
		public void WriteXml(TextWriter writer, XmlWriteMode mode)
		{
			this.WriteXml(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented
			}, mode);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to WriteSchema.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002E7 RID: 743 RVA: 0x00012D04 File Offset: 0x00010F04
		public void WriteXml(XmlWriter writer, XmlWriteMode mode)
		{
			if (mode == XmlWriteMode.DiffGram)
			{
				this.SetRowsID();
				DataSet.WriteDiffGramElement(writer);
			}
			bool flag = mode != XmlWriteMode.DiffGram;
			int num = 0;
			while (num < this.tableCollection.Count && !flag)
			{
				flag = this.tableCollection[num].Rows.Count > 0;
				num++;
			}
			if (flag)
			{
				DataSet.WriteStartElement(writer, mode, this.Namespace, this.Prefix, XmlHelper.Encode(this.DataSetName));
				if (mode == XmlWriteMode.WriteSchema)
				{
					this.DoWriteXmlSchema(writer);
				}
				this.WriteTables(writer, mode, this.Tables, DataRowVersion.Default);
				writer.WriteEndElement();
			}
			if (mode == XmlWriteMode.DiffGram && this.HasChanges(DataRowState.Deleted | DataRowState.Modified))
			{
				DataSet changes = this.GetChanges(DataRowState.Deleted | DataRowState.Modified);
				DataSet.WriteStartElement(writer, XmlWriteMode.DiffGram, "urn:schemas-microsoft-com:xml-diffgram-v1", "diffgr", "before");
				this.WriteTables(writer, mode, changes.Tables, DataRowVersion.Original);
				writer.WriteEndElement();
			}
			if (mode == XmlWriteMode.DiffGram)
			{
				writer.WriteEndElement();
			}
			writer.Flush();
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to using the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object used to write to a file. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002E8 RID: 744 RVA: 0x00012E14 File Offset: 0x00011014
		public void WriteXmlSchema(Stream stream)
		{
			this.WriteXmlSchema(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented
			});
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to a file.</summary>
		/// <param name="fileName">The file name (including the path) to which to write. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002E9 RID: 745 RVA: 0x00012E38 File Offset: 0x00011038
		public void WriteXmlSchema(string fileName)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			try
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument(true);
				this.WriteXmlSchema(xmlTextWriter);
			}
			finally
			{
				xmlTextWriter.WriteEndDocument();
				xmlTextWriter.Close();
			}
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to a <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> object with which to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002EA RID: 746 RVA: 0x00012E90 File Offset: 0x00011090
		public void WriteXmlSchema(TextWriter writer)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(writer);
			try
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				this.WriteXmlSchema(xmlTextWriter);
			}
			finally
			{
				xmlTextWriter.Close();
			}
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to an <see cref="T:System.Xml.XmlWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060002EB RID: 747 RVA: 0x00012EDC File Offset: 0x000110DC
		public void WriteXmlSchema(XmlWriter writer)
		{
			this.DoWriteXmlSchema(writer);
		}

		/// <summary>Reads the XML schema from the specified <see cref="T:System.IO.Stream" /> into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002EC RID: 748 RVA: 0x00012EE8 File Offset: 0x000110E8
		public void ReadXmlSchema(Stream stream)
		{
			XmlReader xmlReader = new XmlTextReader(stream, null);
			this.ReadXmlSchema(xmlReader);
		}

		/// <summary>Reads the XML schema from the specified file into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="fileName">The file name (including the path) from which to read. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002ED RID: 749 RVA: 0x00012F04 File Offset: 0x00011104
		public void ReadXmlSchema(string fileName)
		{
			XmlReader xmlReader = new XmlTextReader(fileName);
			try
			{
				this.ReadXmlSchema(xmlReader);
			}
			finally
			{
				xmlReader.Close();
			}
		}

		/// <summary>Reads the XML schema from the specified <see cref="T:System.IO.TextReader" /> into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002EE RID: 750 RVA: 0x00012F48 File Offset: 0x00011148
		public void ReadXmlSchema(TextReader reader)
		{
			XmlReader xmlReader = new XmlTextReader(reader);
			this.ReadXmlSchema(xmlReader);
		}

		/// <summary>Reads the XML schema from the specified <see cref="T:System.Xml.XmlReader" /> into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002EF RID: 751 RVA: 0x00012F64 File Offset: 0x00011164
		public void ReadXmlSchema(XmlReader reader)
		{
			XmlSchemaDataImporter xmlSchemaDataImporter = new XmlSchemaDataImporter(this, reader, true);
			xmlSchemaDataImporter.Process();
			this.tableAdapterSchemaInfo = xmlSchemaDataImporter.CurrentAdapter;
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="stream">An object that derives from <see cref="T:System.IO.Stream" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002F0 RID: 752 RVA: 0x00012F8C File Offset: 0x0001118C
		public XmlReadMode ReadXml(Stream stream)
		{
			return this.ReadXml(new XmlTextReader(stream));
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified file.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="fileName">The filename (including the path) from which to read. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002F1 RID: 753 RVA: 0x00012F9C File Offset: 0x0001119C
		public XmlReadMode ReadXml(string fileName)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			XmlReadMode xmlReadMode;
			try
			{
				xmlReadMode = this.ReadXml(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Close();
			}
			return xmlReadMode;
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		/// <param name="reader">The TextReader from which to read the schema and data. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002F2 RID: 754 RVA: 0x00012FE8 File Offset: 0x000111E8
		public XmlReadMode ReadXml(TextReader reader)
		{
			return this.ReadXml(new XmlTextReader(reader));
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002F3 RID: 755 RVA: 0x00012FF8 File Offset: 0x000111F8
		public XmlReadMode ReadXml(XmlReader reader)
		{
			return this.ReadXml(reader, XmlReadMode.Auto);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" /> and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which to read. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002F4 RID: 756 RVA: 0x00013004 File Offset: 0x00011204
		public XmlReadMode ReadXml(Stream stream, XmlReadMode mode)
		{
			return this.ReadXml(new XmlTextReader(stream), mode);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified file and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="fileName">The filename (including the path) from which to read. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002F5 RID: 757 RVA: 0x00013014 File Offset: 0x00011214
		public XmlReadMode ReadXml(string fileName, XmlReadMode mode)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			XmlReadMode xmlReadMode;
			try
			{
				xmlReadMode = this.ReadXml(xmlTextReader, mode);
			}
			finally
			{
				xmlTextReader.Close();
			}
			return xmlReadMode;
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextReader" /> and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> from which to read. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002F6 RID: 758 RVA: 0x00013060 File Offset: 0x00011260
		public XmlReadMode ReadXml(TextReader reader, XmlReadMode mode)
		{
			return this.ReadXml(new XmlTextReader(reader), mode);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlReader" /> and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <returns>The XmlReadMode used to read the data.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read. </param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002F7 RID: 759 RVA: 0x00013070 File Offset: 0x00011270
		public XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode)
		{
			if (reader == null)
			{
				return mode;
			}
			switch (reader.ReadState)
			{
			case ReadState.Error:
			case ReadState.EndOfFile:
			case ReadState.Closed:
				return mode;
			default:
			{
				reader.MoveToContent();
				if (reader.EOF)
				{
					return mode;
				}
				if (reader is XmlTextReader)
				{
					((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.None;
				}
				XmlDiffLoader xmlDiffLoader = null;
				if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
				{
					switch (mode)
					{
					case XmlReadMode.Auto:
					case XmlReadMode.DiffGram:
						if (xmlDiffLoader == null)
						{
							xmlDiffLoader = new XmlDiffLoader(this);
						}
						xmlDiffLoader.Load(reader);
						return XmlReadMode.DiffGram;
					case XmlReadMode.Fragment:
						reader.Skip();
						goto IL_00D3;
					}
					reader.Skip();
					return mode;
				}
				IL_00D3:
				if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
				{
					switch (mode)
					{
					case XmlReadMode.Auto:
						if (this.Tables.Count == 0)
						{
							this.ReadXmlSchema(reader);
							return XmlReadMode.ReadSchema;
						}
						reader.Skip();
						return XmlReadMode.IgnoreSchema;
					case XmlReadMode.IgnoreSchema:
					case XmlReadMode.InferSchema:
						reader.Skip();
						return mode;
					case XmlReadMode.Fragment:
						this.ReadXmlSchema(reader);
						goto IL_0162;
					}
					this.ReadXmlSchema(reader);
					return mode;
				}
				IL_0162:
				if (reader.EOF)
				{
					return mode;
				}
				int num = ((reader.NodeType != XmlNodeType.Element) ? (-1) : reader.Depth);
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
				if (reader.HasAttributes)
				{
					for (int i = 0; i < reader.AttributeCount; i++)
					{
						reader.MoveToAttribute(i);
						if (reader.NamespaceURI == "http://www.w3.org/2000/xmlns/")
						{
							xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
						}
						else
						{
							XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
							xmlAttribute.Prefix = reader.Prefix;
							xmlAttribute.Value = reader.GetAttribute(i);
						}
					}
				}
				reader.Read();
				XmlReadMode xmlReadMode = mode;
				bool flag = false;
				while (reader.Depth != num && reader.NodeType != XmlNodeType.EndElement)
				{
					if (reader.NodeType != XmlNodeType.Element)
					{
						if (!reader.Read())
						{
							IL_0364:
							if (reader.NodeType == XmlNodeType.EndElement)
							{
								reader.Read();
							}
							reader.MoveToContent();
							if (mode == XmlReadMode.DiffGram)
							{
								return xmlReadMode;
							}
							xmlDocument.AppendChild(xmlElement);
							if (!flag && xmlReadMode != XmlReadMode.ReadSchema && mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.Fragment && (this.Tables.Count == 0 || mode == XmlReadMode.InferSchema))
							{
								this.InferXmlSchema(xmlDocument, null);
								if (mode == XmlReadMode.Auto)
								{
									xmlReadMode = XmlReadMode.InferSchema;
								}
							}
							reader = new XmlNodeReader(xmlDocument);
							XmlDataReader.ReadXml(this, reader, mode);
							return (xmlReadMode != XmlReadMode.Auto) ? xmlReadMode : XmlReadMode.IgnoreSchema;
						}
					}
					else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
					{
						if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema)
						{
							this.ReadXmlSchema(reader);
							xmlReadMode = XmlReadMode.ReadSchema;
							flag = true;
						}
						else
						{
							reader.Skip();
						}
					}
					else if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
					{
						if (mode == XmlReadMode.DiffGram || mode == XmlReadMode.IgnoreSchema || mode == XmlReadMode.Auto)
						{
							if (xmlDiffLoader == null)
							{
								xmlDiffLoader = new XmlDiffLoader(this);
							}
							xmlDiffLoader.Load(reader);
							xmlReadMode = XmlReadMode.DiffGram;
						}
						else
						{
							reader.Skip();
						}
					}
					else
					{
						XmlNode xmlNode = xmlDocument.ReadNode(reader);
						xmlElement.AppendChild(xmlNode);
					}
				}
				goto IL_0364;
			}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00013474 File Offset: 0x00011674
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0001347C File Offset: 0x0001167C
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

		/// <summary>Begins the initialization of a <see cref="T:System.Data.DataSet" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002FA RID: 762 RVA: 0x00013488 File Offset: 0x00011688
		public void BeginInit()
		{
			this.InitInProgress = true;
			this.dataSetInitialized = false;
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Data.DataSet" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060002FB RID: 763 RVA: 0x00013498 File Offset: 0x00011698
		public void EndInit()
		{
			this.Tables.PostAddRange();
			for (int i = 0; i < this.Tables.Count; i++)
			{
				if (this.Tables[i].InitInProgress)
				{
					this.Tables[i].FinishInit();
				}
			}
			this.Relations.PostAddRange();
			this.InitInProgress = false;
			this.dataSetInitialized = true;
			this.DataSetInitialized();
		}

		/// <summary>Populates a serialization information object with the data needed to serialize the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized data associated with the <see cref="T:System.Data.DataSet" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream associated with the <see cref="T:System.Data.DataSet" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
		// Token: 0x060002FC RID: 764 RVA: 0x00013518 File Offset: 0x00011718
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this.RemotingFormat == SerializationFormat.Xml)
			{
				info.AddValue("SchemaSerializationMode.DataSet", this.SchemaSerializationMode);
				StringWriter stringWriter = new StringWriter();
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				this.DoWriteXmlSchema(xmlTextWriter);
				xmlTextWriter.Flush();
				info.AddValue("XmlSchema", stringWriter.ToString());
				stringWriter = new StringWriter();
				xmlTextWriter = new XmlTextWriter(stringWriter);
				this.WriteXml(xmlTextWriter, XmlWriteMode.DiffGram);
				xmlTextWriter.Flush();
				info.AddValue("XmlDiffGram", stringWriter.ToString());
			}
			else
			{
				this.BinarySerialize(info);
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000135AC File Offset: 0x000117AC
		protected void GetSerializationData(SerializationInfo info, StreamingContext context)
		{
			string text = info.GetValue("XmlDiffGram", typeof(string)) as string;
			XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(text));
			this.ReadXml(xmlTextReader, XmlReadMode.DiffGram);
			xmlTextReader.Close();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000135F0 File Offset: 0x000117F0
		protected virtual XmlSchema GetSchemaSerializable()
		{
			return null;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000135F4 File Offset: 0x000117F4
		protected virtual void ReadXmlSerializable(XmlReader reader)
		{
			this.ReadXml(reader, XmlReadMode.DiffGram);
		}

		/// <summary>Gets a value indicating whether <see cref="P:System.Data.DataSet.Relations" /> property should be persisted.</summary>
		/// <returns>true if the property value has been changed from its default; otherwise false.</returns>
		// Token: 0x06000300 RID: 768 RVA: 0x00013600 File Offset: 0x00011800
		protected virtual bool ShouldSerializeRelations()
		{
			return true;
		}

		/// <summary>Gets a value indicating whether <see cref="P:System.Data.DataSet.Tables" /> property should be persisted.</summary>
		/// <returns>true if the property value has been changed from its default; otherwise false.</returns>
		// Token: 0x06000301 RID: 769 RVA: 0x00013604 File Offset: 0x00011804
		protected virtual bool ShouldSerializeTables()
		{
			return true;
		}

		/// <summary>Raises the <see cref="M:System.Data.DataSet.OnPropertyChanging(System.ComponentModel.PropertyChangedEventArgs)" /> event.</summary>
		/// <param name="pcevent">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06000302 RID: 770 RVA: 0x00013608 File Offset: 0x00011808
		[MonoTODO]
		protected internal virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			throw new NotImplementedException();
		}

		/// <summary>Occurs when a <see cref="T:System.Data.DataRelation" /> object is removed from a <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> being removed. </param>
		// Token: 0x06000303 RID: 771 RVA: 0x00013610 File Offset: 0x00011810
		[MonoTODO]
		protected virtual void OnRemoveRelation(DataRelation relation)
		{
			throw new NotImplementedException();
		}

		/// <summary>Occurs when a <see cref="T:System.Data.DataTable" /> is removed from a <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> being removed. </param>
		// Token: 0x06000304 RID: 772 RVA: 0x00013618 File Offset: 0x00011818
		[MonoTODO]
		protected virtual void OnRemoveTable(DataTable table)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00013620 File Offset: 0x00011820
		internal virtual void OnMergeFailed(MergeFailedEventArgs e)
		{
			if (this.MergeFailed != null)
			{
				this.MergeFailed(this, e);
				return;
			}
			throw new DataException(e.Conflict);
		}

		/// <summary>Sends a notification that the specified <see cref="T:System.Data.DataSet" /> property is about to change.</summary>
		/// <param name="name">The name of the property that is about to change. </param>
		// Token: 0x06000306 RID: 774 RVA: 0x0001364C File Offset: 0x0001184C
		[MonoTODO]
		protected internal void RaisePropertyChanging(string name)
		{
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00013650 File Offset: 0x00011850
		internal static string WriteObjectXml(object o)
		{
			switch (Type.GetTypeCode(o.GetType()))
			{
			case TypeCode.Boolean:
				return XmlConvert.ToString((bool)o);
			case TypeCode.Char:
				return XmlConvert.ToString((char)o);
			case TypeCode.SByte:
				return XmlConvert.ToString((sbyte)o);
			case TypeCode.Byte:
				return XmlConvert.ToString((byte)o);
			case TypeCode.Int16:
				return XmlConvert.ToString((short)o);
			case TypeCode.UInt16:
				return XmlConvert.ToString((ushort)o);
			case TypeCode.Int32:
				return XmlConvert.ToString((int)o);
			case TypeCode.UInt32:
				return XmlConvert.ToString((uint)o);
			case TypeCode.Int64:
				return XmlConvert.ToString((long)o);
			case TypeCode.UInt64:
				return XmlConvert.ToString((ulong)o);
			case TypeCode.Single:
				return XmlConvert.ToString((float)o);
			case TypeCode.Double:
				return XmlConvert.ToString((double)o);
			case TypeCode.Decimal:
				return XmlConvert.ToString((decimal)o);
			case TypeCode.DateTime:
				return XmlConvert.ToString((DateTime)o, XmlDateTimeSerializationMode.Unspecified);
			default:
				if (o is TimeSpan)
				{
					return XmlConvert.ToString((TimeSpan)o);
				}
				if (o is Guid)
				{
					return XmlConvert.ToString((Guid)o);
				}
				if (o is byte[])
				{
					return Convert.ToBase64String((byte[])o);
				}
				return o.ToString();
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000137A4 File Offset: 0x000119A4
		private void WriteTables(XmlWriter writer, XmlWriteMode mode, DataTableCollection tableCollection, DataRowVersion version)
		{
			foreach (object obj in tableCollection)
			{
				DataTable dataTable = (DataTable)obj;
				DataSet.WriteTable(writer, dataTable, mode, version);
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00013814 File Offset: 0x00011A14
		internal static void WriteTable(XmlWriter writer, DataTable table, XmlWriteMode mode, DataRowVersion version)
		{
			DataRow[] array = table.NewRowArray(table.Rows.Count);
			table.Rows.CopyTo(array, 0);
			DataSet.WriteTable(writer, array, mode, version, true);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001384C File Offset: 0x00011A4C
		internal static void WriteTable(XmlWriter writer, DataRow[] rows, XmlWriteMode mode, DataRowVersion version, bool skipIfNested)
		{
			if (rows.Length == 0)
			{
				return;
			}
			DataTable table = rows[0].Table;
			if (table.TableName == null || table.TableName == string.Empty)
			{
				throw new InvalidOperationException("Cannot serialize the DataTable. DataTable name is not set.");
			}
			DataColumn dataColumn = null;
			ArrayList arrayList;
			ArrayList arrayList2;
			DataSet.SplitColumns(table, out arrayList, out arrayList2, out dataColumn);
			int count = table.ParentRelations.Count;
			int i = 0;
			while (i < rows.Length)
			{
				DataRow dataRow = rows[i];
				if (!skipIfNested)
				{
					goto IL_00D6;
				}
				bool flag = false;
				for (int j = 0; j < table.ParentRelations.Count; j++)
				{
					DataRelation dataRelation = table.ParentRelations[j];
					if (dataRelation.Nested)
					{
						if (dataRow.GetParentRow(dataRelation) != null)
						{
							flag = true;
						}
					}
				}
				if (!flag)
				{
					goto IL_00D6;
				}
				IL_02C5:
				i++;
				continue;
				IL_00D6:
				if (!dataRow.HasVersion(version) || (mode == XmlWriteMode.DiffGram && dataRow.RowState == DataRowState.Unchanged && version == DataRowVersion.Original))
				{
					goto IL_02C5;
				}
				bool flag2 = true;
				foreach (object obj in table.Columns)
				{
					DataColumn dataColumn2 = (DataColumn)obj;
					if (dataRow[dataColumn2.ColumnName, version] != DBNull.Value)
					{
						flag2 = false;
						break;
					}
				}
				if (flag2)
				{
					writer.WriteElementString(XmlHelper.Encode(table.TableName), string.Empty);
					goto IL_02C5;
				}
				DataSet.WriteTableElement(writer, mode, table, dataRow, version);
				foreach (object obj2 in arrayList)
				{
					DataColumn dataColumn3 = (DataColumn)obj2;
					DataSet.WriteColumnAsAttribute(writer, mode, dataColumn3, dataRow, version);
				}
				if (dataColumn != null)
				{
					writer.WriteString(DataSet.WriteObjectXml(dataRow[dataColumn, version]));
				}
				else
				{
					foreach (object obj3 in arrayList2)
					{
						DataColumn dataColumn4 = (DataColumn)obj3;
						DataSet.WriteColumnAsElement(writer, mode, dataColumn4, dataRow, version);
					}
				}
				foreach (object obj4 in table.ChildRelations)
				{
					DataRelation dataRelation2 = (DataRelation)obj4;
					if (dataRelation2.Nested)
					{
						DataSet.WriteTable(writer, dataRow.GetChildRows(dataRelation2), mode, version, false);
					}
				}
				writer.WriteEndElement();
				goto IL_02C5;
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00013B94 File Offset: 0x00011D94
		internal static void WriteColumnAsElement(XmlWriter writer, XmlWriteMode mode, DataColumn col, DataRow row, DataRowVersion version)
		{
			string text = null;
			object obj = row[col, version];
			if (obj == null || obj == DBNull.Value)
			{
				return;
			}
			if (col.Namespace != string.Empty)
			{
				text = col.Namespace;
			}
			DataSet.WriteStartElement(writer, mode, text, col.Prefix, XmlHelper.Encode(col.ColumnName));
			if (typeof(IXmlSerializable).IsAssignableFrom(col.DataType) || col.DataType == typeof(object))
			{
				if (!(obj is IXmlSerializable))
				{
					throw new InvalidOperationException();
				}
				((IXmlSerializable)obj).WriteXml(writer);
			}
			else
			{
				writer.WriteString(DataSet.WriteObjectXml(obj));
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00013C5C File Offset: 0x00011E5C
		internal static void WriteColumnAsAttribute(XmlWriter writer, XmlWriteMode mode, DataColumn col, DataRow row, DataRowVersion version)
		{
			if (!row.IsNull(col))
			{
				DataSet.WriteAttributeString(writer, mode, col.Namespace, col.Prefix, XmlHelper.Encode(col.ColumnName), DataSet.WriteObjectXml(row[col, version]));
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00013CA4 File Offset: 0x00011EA4
		internal static void WriteTableElement(XmlWriter writer, XmlWriteMode mode, DataTable table, DataRow row, DataRowVersion version)
		{
			string text = ((table.Namespace.Length <= 0 && table.DataSet != null) ? table.DataSet.Namespace : table.Namespace);
			DataSet.WriteStartElement(writer, mode, text, table.Prefix, XmlHelper.Encode(table.TableName));
			if (mode == XmlWriteMode.DiffGram)
			{
				DataSet.WriteAttributeString(writer, mode, "urn:schemas-microsoft-com:xml-diffgram-v1", "diffgr", "id", table.TableName + (row.XmlRowID + 1));
				DataSet.WriteAttributeString(writer, mode, "urn:schemas-microsoft-com:xml-msdata", "msdata", "rowOrder", XmlConvert.ToString(row.XmlRowID));
				string text2 = null;
				if (row.RowState == DataRowState.Modified)
				{
					text2 = "modified";
				}
				else if (row.RowState == DataRowState.Added)
				{
					text2 = "inserted";
				}
				if (version != DataRowVersion.Original && text2 != null)
				{
					DataSet.WriteAttributeString(writer, mode, "urn:schemas-microsoft-com:xml-diffgram-v1", "diffgr", "hasChanges", text2);
				}
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00013DA8 File Offset: 0x00011FA8
		internal static void WriteStartElement(XmlWriter writer, XmlWriteMode mode, string nspc, string prefix, string name)
		{
			writer.WriteStartElement(prefix, name, nspc);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00013DB4 File Offset: 0x00011FB4
		internal static void WriteAttributeString(XmlWriter writer, XmlWriteMode mode, string nspc, string prefix, string name, string stringValue)
		{
			if (mode != XmlWriteMode.DiffGram)
			{
				writer.WriteAttributeString(name, stringValue);
			}
			else
			{
				writer.WriteAttributeString(prefix, name, nspc, stringValue);
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00013DF0 File Offset: 0x00011FF0
		internal void WriteIndividualTableContent(XmlWriter writer, DataTable table, XmlWriteMode mode)
		{
			if (mode == XmlWriteMode.DiffGram)
			{
				table.SetRowsID();
				DataSet.WriteDiffGramElement(writer);
			}
			DataSet.WriteStartElement(writer, mode, this.Namespace, this.Prefix, XmlHelper.Encode(this.DataSetName));
			DataSet.WriteTable(writer, table, mode, DataRowVersion.Default);
			if (mode == XmlWriteMode.DiffGram)
			{
				writer.WriteEndElement();
				if (this.HasChanges(DataRowState.Deleted | DataRowState.Modified))
				{
					DataSet changes = this.GetChanges(DataRowState.Deleted | DataRowState.Modified);
					DataSet.WriteStartElement(writer, XmlWriteMode.DiffGram, "urn:schemas-microsoft-com:xml-diffgram-v1", "diffgr", "before");
					DataSet.WriteTable(writer, changes.Tables[table.TableName], mode, DataRowVersion.Original);
					writer.WriteEndElement();
				}
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00013EA0 File Offset: 0x000120A0
		private void DoWriteXmlSchema(XmlWriter writer)
		{
			if (writer.WriteState == WriteState.Start)
			{
				writer.WriteStartDocument();
			}
			XmlSchemaWriter.WriteXmlSchema(this, writer);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00013EBC File Offset: 0x000120BC
		internal static void SplitColumns(DataTable table, out ArrayList atts, out ArrayList elements, out DataColumn simple)
		{
			atts = new ArrayList();
			elements = new ArrayList();
			simple = null;
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				switch (dataColumn.ColumnMapping)
				{
				case MappingType.Element:
					elements.Add(dataColumn);
					break;
				case MappingType.Attribute:
					atts.Add(dataColumn);
					break;
				case MappingType.SimpleContent:
					if (simple != null)
					{
						throw new InvalidOperationException("There may only be one simple content element");
					}
					simple = dataColumn;
					break;
				}
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00013F90 File Offset: 0x00012190
		internal static void WriteDiffGramElement(XmlWriter writer)
		{
			DataSet.WriteStartElement(writer, XmlWriteMode.DiffGram, "urn:schemas-microsoft-com:xml-diffgram-v1", "diffgr", "diffgram");
			DataSet.WriteAttributeString(writer, XmlWriteMode.DiffGram, null, "xmlns", "msdata", "urn:schemas-microsoft-com:xml-msdata");
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00013FC0 File Offset: 0x000121C0
		private void SetRowsID()
		{
			foreach (object obj in this.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				dataTable.SetRowsID();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.SerializationFormat" /> for the <see cref="T:System.Data.DataSet" /> used during remoting.</summary>
		/// <returns>A <see cref="T:System.Data.SerializationFormat" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00014030 File Offset: 0x00012230
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00014038 File Offset: 0x00012238
		[DefaultValue(SerializationFormat.Xml)]
		public SerializationFormat RemotingFormat
		{
			get
			{
				return this.remotingFormat;
			}
			set
			{
				this.remotingFormat = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataSet" /> is initialized.</summary>
		/// <returns>true to indicate the component has completed initialization; otherwise false.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00014044 File Offset: 0x00012244
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return this.dataSetInitialized;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>Gets or sets a <see cref="T:System.Data.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0001404C File Offset: 0x0001224C
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00014050 File Offset: 0x00012250
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual SchemaSerializationMode SchemaSerializationMode
		{
			get
			{
				return SchemaSerializationMode.IncludeSchema;
			}
			set
			{
				if (value != SchemaSerializationMode.IncludeSchema)
				{
					throw new InvalidOperationException("Only IncludeSchema Mode can be set for Untyped DataSet");
				}
			}
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTableReader" /> with one result set per <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTableReader" /> containing one or more result sets, corresponding to the <see cref="T:System.Data.DataTable" /> instances contained within the source <see cref="T:System.Data.DataSet" />. The returned result sets are in the order specified by the <paramref name="dataTables" /> parameter.</returns>
		/// <param name="dataTables">An array of DataTables providing the order of the result sets to be returned in the <see cref="T:System.Data.DataTableReader" />.</param>
		// Token: 0x0600031A RID: 794 RVA: 0x00014064 File Offset: 0x00012264
		public DataTableReader CreateDataReader(params DataTable[] dataTables)
		{
			return new DataTableReader(dataTables);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTableReader" /> with one result set per <see cref="T:System.Data.DataTable" />, in the same sequence as the tables appear in the <see cref="P:System.Data.DataSet.Tables" /> collection.</summary>
		/// <returns>A <see cref="T:System.Data.DataTableReader" /> containing one or more result sets, corresponding to the <see cref="T:System.Data.DataTable" /> instances contained within the source <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x0600031B RID: 795 RVA: 0x0001406C File Offset: 0x0001226C
		public DataTableReader CreateDataReader()
		{
			return new DataTableReader((DataTable[])this.Tables.ToArray(typeof(DataTable)));
		}

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600031C RID: 796 RVA: 0x00014090 File Offset: 0x00012290
		public static XmlSchemaComplexType GetDataSetSchema(XmlSchemaSet schemaSet)
		{
			return new XmlSchemaComplexType();
		}

		/// <summary>Fills a <see cref="T:System.Data.DataSet" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />, using an array of <see cref="T:System.Data.DataTable" /> instances to supply the schema and namespace information.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets. </param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> instances within the <see cref="T:System.Data.DataSet" /> will be combined with incoming rows that share the same primary key. </param>
		/// <param name="tables">An array of <see cref="T:System.Data.DataTable" /> instances, from which the <see cref="M:System.Data.DataSet.Load(System.Data.IDataReader,System.Data.LoadOption,System.Data.DataTable[])" /> method retrieves name and namespace information. Each of these tables must be a member of the <see cref="T:System.Data.DataTableCollection" /> contained by this <see cref="T:System.Data.DataSet" />.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600031D RID: 797 RVA: 0x00014098 File Offset: 0x00012298
		public void Load(IDataReader reader, LoadOption loadOption, params DataTable[] tables)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("Value cannot be null. Parameter name: reader");
			}
			foreach (DataTable dataTable in tables)
			{
				if (dataTable.DataSet == null || dataTable.DataSet != this)
				{
					throw new ArgumentException("Table " + dataTable.TableName + " does not belong to this DataSet.");
				}
				dataTable.Load(reader, loadOption);
				reader.NextResult();
			}
		}

		/// <summary>Fills a <see cref="T:System.Data.DataSet" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />, using an array of strings to supply the names for the tables within the DataSet.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets.</param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> instances within the DataSet will be combined with incoming rows that share the same primary key. </param>
		/// <param name="tables">An array of strings, from which the Load method retrieves table name information.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600031E RID: 798 RVA: 0x00014114 File Offset: 0x00012314
		public void Load(IDataReader reader, LoadOption loadOption, params string[] tables)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("Value cannot be null. Parameter name: reader");
			}
			foreach (string text in tables)
			{
				DataTable dataTable = this.Tables[text];
				if (dataTable == null)
				{
					dataTable = new DataTable(text);
					this.Tables.Add(dataTable);
				}
				dataTable.Load(reader, loadOption);
				reader.NextResult();
			}
		}

		/// <summary>Fills a <see cref="T:System.Data.DataSet" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />, using an array of <see cref="T:System.Data.DataTable" /> instances to supply the schema and namespace information.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets.</param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> instances within the <see cref="T:System.Data.DataSet" /> will be combined with incoming rows that share the same primary key. </param>
		/// <param name="errorHandler">A <see cref="T:System.Data.FillErrorEventHandler" /> delegate to call when an error occurs while loading data.</param>
		/// <param name="tables">An array of <see cref="T:System.Data.DataTable" /> instances, from which the <see cref="M:System.Data.DataSet.Load(System.Data.IDataReader,System.Data.LoadOption,System.Data.FillErrorEventHandler,System.Data.DataTable[])" /> method retrieves name and namespace information.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600031F RID: 799 RVA: 0x00014184 File Offset: 0x00012384
		public virtual void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler errorHandler, params DataTable[] tables)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("Value cannot be null. Parameter name: reader");
			}
			foreach (DataTable dataTable in tables)
			{
				if (dataTable.DataSet == null || dataTable.DataSet != this)
				{
					throw new ArgumentException("Table " + dataTable.TableName + " does not belong to this DataSet.");
				}
				dataTable.Load(reader, loadOption, errorHandler);
				reader.NextResult();
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00014200 File Offset: 0x00012400
		private void BinarySerialize(SerializationInfo si)
		{
			Version version = new Version(2, 0);
			si.AddValue("DataSet.RemotingVersion", version, typeof(Version));
			si.AddValue("DataSet.RemotingFormat", this.RemotingFormat, typeof(SerializationFormat));
			si.AddValue("DataSet.DataSetName", this.DataSetName);
			si.AddValue("DataSet.Namespace", this.Namespace);
			si.AddValue("DataSet.Prefix", this.Prefix);
			si.AddValue("DataSet.CaseSensitive", this.CaseSensitive);
			si.AddValue("DataSet.LocaleLCID", this.Locale.LCID);
			si.AddValue("DataSet.EnforceConstraints", this.EnforceConstraints);
			si.AddValue("DataSet.ExtendedProperties", this.properties, typeof(PropertyCollection));
			this.Tables.BinarySerialize_Schema(si);
			this.Tables.BinarySerialize_Data(si);
			this.Relations.BinarySerialize(si);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000142F8 File Offset: 0x000124F8
		private void BinaryDeserialize(SerializationInfo info)
		{
			this.DataSetName = info.GetString("DataSet.DataSetName");
			this.Namespace = info.GetString("DataSet.Namespace");
			this.CaseSensitive = info.GetBoolean("DataSet.CaseSensitive");
			this.Locale = new CultureInfo(info.GetInt32("DataSet.LocaleLCID"));
			this.EnforceConstraints = info.GetBoolean("DataSet.EnforceConstraints");
			this.Prefix = info.GetString("DataSet.Prefix");
			this.properties = (PropertyCollection)info.GetValue("DataSet.ExtendedProperties", typeof(PropertyCollection));
			int @int = info.GetInt32("DataSet.Tables.Count");
			ArrayList arrayList2;
			for (int i = 0; i < @int; i++)
			{
				byte[] array = (byte[])info.GetValue("DataSet.Tables_" + i, typeof(byte[]));
				MemoryStream memoryStream = new MemoryStream(array);
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				DataTable dataTable = (DataTable)binaryFormatter.Deserialize(memoryStream);
				memoryStream.Close();
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					dataTable.Columns[j].Expression = info.GetString(string.Concat(new object[] { "DataTable_", i, ".DataColumn_", j, ".Expression" }));
				}
				ArrayList arrayList = (ArrayList)info.GetValue("DataTable_" + i + ".NullBits", typeof(ArrayList));
				arrayList2 = (ArrayList)info.GetValue("DataTable_" + i + ".Records", typeof(ArrayList));
				BitArray bitArray = (BitArray)info.GetValue("DataTable_" + i + ".RowStates", typeof(BitArray));
				dataTable.DeserializeRecords(arrayList2, arrayList, bitArray);
				this.Tables.Add(dataTable);
			}
			for (int k = 0; k < @int; k++)
			{
				DataTable dataTable = this.Tables[k];
				dataTable.dataSet = this;
				arrayList2 = (ArrayList)info.GetValue("DataTable_" + k + ".Constraints", typeof(ArrayList));
				if (dataTable.Constraints == null)
				{
					dataTable.Constraints = new ConstraintCollection(dataTable);
				}
				dataTable.DeserializeConstraints(arrayList2);
			}
			arrayList2 = (ArrayList)info.GetValue("DataSet.Relations", typeof(ArrayList));
			bool flag = true;
			for (int l = 0; l < arrayList2.Count; l++)
			{
				ArrayList arrayList3 = (ArrayList)arrayList2[l];
				ArrayList arrayList4 = new ArrayList();
				ArrayList arrayList5 = new ArrayList();
				for (int m = 0; m < arrayList3.Count; m++)
				{
					if (arrayList3[m] != null && typeof(int) == arrayList3[m].GetType().GetElementType())
					{
						Array array2 = (Array)arrayList3[m];
						if (flag)
						{
							arrayList5.Add(this.Tables[(int)array2.GetValue(0)].Columns[(int)array2.GetValue(1)]);
							flag = false;
						}
						else
						{
							arrayList4.Add(this.Tables[(int)array2.GetValue(0)].Columns[(int)array2.GetValue(1)]);
							flag = true;
						}
					}
				}
				this.Relations.Add((string)arrayList3[0], (DataColumn[])arrayList5.ToArray(typeof(DataColumn)), (DataColumn[])arrayList4.ToArray(typeof(DataColumn)), false);
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000146FC File Offset: 0x000128FC
		private void OnDataSetInitialized(EventArgs e)
		{
			if (this.Initialized != null)
			{
				this.Initialized(this, e);
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00014718 File Offset: 0x00012918
		private void DataSetInitialized()
		{
			EventArgs eventArgs = new EventArgs();
			this.OnDataSetInitialized(eventArgs);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00014734 File Offset: 0x00012934
		protected virtual void InitializeDerivedDataSet()
		{
		}

		/// <summary>Determines the <see cref="P:System.Data.DataSet.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>An <see cref="T:System.Data.SchemaSerializationMode" /> enumeration indicating whether schema information has been omitted from the payload.</returns>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> instance that is passed during deserialization of the <see cref="T:System.Data.DataSet" />.</param>
		// Token: 0x06000325 RID: 805 RVA: 0x00014738 File Offset: 0x00012938
		protected SchemaSerializationMode DetermineSchemaSerializationMode(XmlReader reader)
		{
			return SchemaSerializationMode.IncludeSchema;
		}

		/// <summary>Determines the <see cref="P:System.Data.DataSet.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>An <see cref="T:System.Data.SchemaSerializationMode" /> enumeration indicating whether schema information has been omitted from the payload.</returns>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that a DataSet’s protected constructor <see cref="M:System.Data.DataSet.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> is invoked with during deserialization in remoting scenarios. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that a DataSet’s protected constructor <see cref="M:System.Data.DataSet.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> is invoked with during deserialization in remoting scenarios.</param>
		// Token: 0x06000326 RID: 806 RVA: 0x0001473C File Offset: 0x0001293C
		protected SchemaSerializationMode DetermineSchemaSerializationMode(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "SchemaSerializationMode.DataSet")
				{
					return (SchemaSerializationMode)((int)enumerator.Value);
				}
			}
			return SchemaSerializationMode.IncludeSchema;
		}

		/// <summary>Inspects the format of the serialized representation of the DataSet.</summary>
		/// <returns>true if the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> represents a DataSet serialized in its binary format, false otherwise.</returns>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06000327 RID: 807 RVA: 0x00014784 File Offset: 0x00012984
		protected bool IsBinarySerialized(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.ObjectType == typeof(SerializationFormat))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000116 RID: 278
		private string dataSetName;

		// Token: 0x04000117 RID: 279
		private string _namespace = string.Empty;

		// Token: 0x04000118 RID: 280
		private string prefix;

		// Token: 0x04000119 RID: 281
		private bool caseSensitive;

		// Token: 0x0400011A RID: 282
		private bool enforceConstraints = true;

		// Token: 0x0400011B RID: 283
		private DataTableCollection tableCollection;

		// Token: 0x0400011C RID: 284
		private DataRelationCollection relationCollection;

		// Token: 0x0400011D RID: 285
		private PropertyCollection properties;

		// Token: 0x0400011E RID: 286
		private DataViewManager defaultView;

		// Token: 0x0400011F RID: 287
		private CultureInfo locale;

		// Token: 0x04000120 RID: 288
		internal XmlDataDocument _xmlDataDocument;

		// Token: 0x04000121 RID: 289
		internal TableAdapterSchemaInfo tableAdapterSchemaInfo;

		// Token: 0x04000122 RID: 290
		private bool initInProgress;

		// Token: 0x04000123 RID: 291
		private bool dataSetInitialized = true;

		// Token: 0x04000124 RID: 292
		private SerializationFormat remotingFormat;
	}
}
