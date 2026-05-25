using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace System.Data
{
	/// <summary>Contains a default <see cref="T:System.Data.DataViewSettingCollection" /> for each <see cref="T:System.Data.DataTable" /> in a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200003C RID: 60
	[Designer("Microsoft.VSDesigner.Data.VS.DataViewManagerDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class DataViewManager : MarshalByValueComponent, IList, IEnumerable, ITypedList, IBindingList, ICollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataViewManager" /> class.</summary>
		// Token: 0x060004D4 RID: 1236 RVA: 0x0001CBB8 File Offset: 0x0001ADB8
		public DataViewManager()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataViewManager" /> class for the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataSet">The name of the <see cref="T:System.Data.DataSet" /> to use. </param>
		// Token: 0x060004D5 RID: 1237 RVA: 0x0001CBC4 File Offset: 0x0001ADC4
		public DataViewManager(DataSet dataSet)
		{
			this.SetDataSet(dataSet);
		}

		/// <summary>Occurs after a row is added to or deleted from a <see cref="T:System.Data.DataView" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060004D6 RID: 1238 RVA: 0x0001CBD4 File Offset: 0x0001ADD4
		// (remove) Token: 0x060004D7 RID: 1239 RVA: 0x0001CBF0 File Offset: 0x0001ADF0
		public event ListChangedEventHandler ListChanged;

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.Count" />.</summary>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0001CC0C File Offset: 0x0001AE0C
		int ICollection.Count
		{
			get
			{
				return 1;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0001CC10 File Offset: 0x0001AE10
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0001CC14 File Offset: 0x0001AE14
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001CC18 File Offset: 0x0001AE18
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0001CC1C File Offset: 0x0001AE1C
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		// Token: 0x170000D2 RID: 210
		object IList.this[int index]
		{
			get
			{
				if (this.descriptor == null)
				{
					this.descriptor = new DataViewManagerListItemTypeDescriptor(this);
				}
				return this.descriptor;
			}
			set
			{
				throw new ArgumentException("Not modifiable");
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</summary>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001CC4C File Offset: 0x0001AE4C
		bool IBindingList.AllowEdit
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</summary>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001CC50 File Offset: 0x0001AE50
		bool IBindingList.AllowNew
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</summary>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0001CC54 File Offset: 0x0001AE54
		bool IBindingList.AllowRemove
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</summary>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0001CC58 File Offset: 0x0001AE58
		bool IBindingList.IsSorted
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</summary>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0001CC60 File Offset: 0x0001AE60
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</summary>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x0001CC68 File Offset: 0x0001AE68
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</summary>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0001CC70 File Offset: 0x0001AE70
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</summary>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0001CC74 File Offset: 0x0001AE74
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</summary>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0001CC78 File Offset: 0x0001AE78
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		// Token: 0x060004E8 RID: 1256 RVA: 0x0001CC7C File Offset: 0x0001AE7C
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		// Token: 0x060004E9 RID: 1257 RVA: 0x0001CC80 File Offset: 0x0001AE80
		object IBindingList.AddNew()
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		// Token: 0x060004EA RID: 1258 RVA: 0x0001CC88 File Offset: 0x0001AE88
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" />.</summary>
		// Token: 0x060004EB RID: 1259 RVA: 0x0001CC90 File Offset: 0x0001AE90
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		// Token: 0x060004EC RID: 1260 RVA: 0x0001CC98 File Offset: 0x0001AE98
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveSort" />.</summary>
		// Token: 0x060004ED RID: 1261 RVA: 0x0001CC9C File Offset: 0x0001AE9C
		void IBindingList.RemoveSort()
		{
			throw new NotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		// Token: 0x060004EE RID: 1262 RVA: 0x0001CCA4 File Offset: 0x0001AEA4
		void ICollection.CopyTo(Array array, int index)
		{
			array.SetValue(this.descriptor, index);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		// Token: 0x060004EF RID: 1263 RVA: 0x0001CCB4 File Offset: 0x0001AEB4
		IEnumerator IEnumerable.GetEnumerator()
		{
			DataViewManagerListItemTypeDescriptor[] array = new DataViewManagerListItemTypeDescriptor[((ICollection)this).Count];
			((ICollection)this).CopyTo(array, 0);
			return array.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		// Token: 0x060004F0 RID: 1264 RVA: 0x0001CCDC File Offset: 0x0001AEDC
		int IList.Add(object value)
		{
			throw new ArgumentException("Not modifiable");
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		// Token: 0x060004F1 RID: 1265 RVA: 0x0001CCE8 File Offset: 0x0001AEE8
		void IList.Clear()
		{
			throw new ArgumentException("Not modifiable");
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		// Token: 0x060004F2 RID: 1266 RVA: 0x0001CCF4 File Offset: 0x0001AEF4
		bool IList.Contains(object value)
		{
			return value == this.descriptor;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		// Token: 0x060004F3 RID: 1267 RVA: 0x0001CD00 File Offset: 0x0001AF00
		int IList.IndexOf(object value)
		{
			if (value == this.descriptor)
			{
				return 0;
			}
			return -1;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		// Token: 0x060004F4 RID: 1268 RVA: 0x0001CD14 File Offset: 0x0001AF14
		void IList.Insert(int index, object value)
		{
			throw new ArgumentException("Not modifiable");
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		// Token: 0x060004F5 RID: 1269 RVA: 0x0001CD20 File Offset: 0x0001AF20
		void IList.Remove(object value)
		{
			throw new ArgumentException("Not modifiable");
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		// Token: 0x060004F6 RID: 1270 RVA: 0x0001CD2C File Offset: 0x0001AF2C
		void IList.RemoveAt(int index)
		{
			throw new ArgumentException("Not modifiable");
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetItemProperties(System.ComponentModel.PropertyDescriptor[])" />.</summary>
		// Token: 0x060004F7 RID: 1271 RVA: 0x0001CD38 File Offset: 0x0001AF38
		[MonoLimitation("Supported only empty list of listAccessors")]
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			if (this.dataSet == null)
			{
				throw new DataException("dataset is null");
			}
			if (listAccessors == null || listAccessors.Length == 0)
			{
				ICustomTypeDescriptor customTypeDescriptor = new DataViewManagerListItemTypeDescriptor(this);
				return customTypeDescriptor.GetProperties();
			}
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetListName(System.ComponentModel.PropertyDescriptor[])" />.</summary>
		// Token: 0x060004F8 RID: 1272 RVA: 0x0001CD7C File Offset: 0x0001AF7C
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			if (this.dataSet != null && (listAccessors == null || listAccessors.Length == 0))
			{
				return this.dataSet.DataSetName;
			}
			return string.Empty;
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataSet" /> to use with the <see cref="T:System.Data.DataViewManager" />.</summary>
		/// <returns>The <see cref="T:System.Data.DataSet" /> to use.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0001CDB4 File Offset: 0x0001AFB4
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x0001CDBC File Offset: 0x0001AFBC
		[DefaultValue(null)]
		public DataSet DataSet
		{
			get
			{
				return this.dataSet;
			}
			set
			{
				if (value == null)
				{
					throw new DataException("Cannot set null DataSet.");
				}
				this.SetDataSet(value);
			}
		}

		/// <summary>Gets or sets a value that is used for code persistence.</summary>
		/// <returns>A value that is used for code persistence.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0001CDD8 File Offset: 0x0001AFD8
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0001CDE0 File Offset: 0x0001AFE0
		public string DataViewSettingCollectionString
		{
			get
			{
				return this.xml;
			}
			set
			{
				try
				{
					this.ParseSettingString(value);
					this.xml = this.BuildSettingString();
				}
				catch (XmlException ex)
				{
					throw new DataException("Cannot set DataViewSettingCollectionString.", ex);
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSettingCollection" /> for each <see cref="T:System.Data.DataTable" /> in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewSettingCollection" /> for each DataTable.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0001CE34 File Offset: 0x0001B034
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataViewSettingCollection DataViewSettings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001CE3C File Offset: 0x0001B03C
		private void SetDataSet(DataSet ds)
		{
			if (this.dataSet != null)
			{
				this.dataSet.Tables.CollectionChanged -= this.TableCollectionChanged;
				this.dataSet.Relations.CollectionChanged -= this.RelationCollectionChanged;
			}
			this.dataSet = ds;
			this.settings = new DataViewSettingCollection(this);
			this.xml = this.BuildSettingString();
			if (this.dataSet != null)
			{
				this.dataSet.Tables.CollectionChanged += this.TableCollectionChanged;
				this.dataSet.Relations.CollectionChanged += this.RelationCollectionChanged;
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		private void ParseSettingString(string source)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(source, XmlNodeType.Element, null);
			xmlTextReader.Read();
			if (xmlTextReader.Name != "DataViewSettingCollectionString")
			{
				xmlTextReader.ReadStartElement("DataViewSettingCollectionString");
			}
			if (xmlTextReader.IsEmptyElement)
			{
				return;
			}
			xmlTextReader.Read();
			do
			{
				xmlTextReader.MoveToContent();
				if (xmlTextReader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
				if (xmlTextReader.NodeType == XmlNodeType.Element)
				{
					this.ReadTableSetting(xmlTextReader);
				}
				else
				{
					xmlTextReader.Skip();
				}
			}
			while (!xmlTextReader.EOF);
			if (xmlTextReader.NodeType == XmlNodeType.EndElement)
			{
				xmlTextReader.ReadEndElement();
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001CF9C File Offset: 0x0001B19C
		private void ReadTableSetting(XmlReader reader)
		{
			DataTable dataTable = this.DataSet.Tables[XmlConvert.DecodeName(reader.LocalName)];
			DataViewSetting dataViewSetting = this.settings[dataTable];
			string attribute = reader.GetAttribute("Sort");
			if (attribute != null)
			{
				dataViewSetting.Sort = attribute.Trim();
			}
			string attribute2 = reader.GetAttribute("ApplyDefaultSort");
			if (attribute2 != null && attribute2.Trim() == "true")
			{
				dataViewSetting.ApplyDefaultSort = true;
			}
			string attribute3 = reader.GetAttribute("RowFilter");
			if (attribute3 != null)
			{
				dataViewSetting.RowFilter = attribute3.Trim();
			}
			string attribute4 = reader.GetAttribute("RowStateFilter");
			if (attribute4 != null)
			{
				dataViewSetting.RowStateFilter = (DataViewRowState)((int)Enum.Parse(typeof(DataViewRowState), attribute4.Trim()));
			}
			reader.Skip();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001D07C File Offset: 0x0001B27C
		private string BuildSettingString()
		{
			if (this.dataSet == null)
			{
				return string.Empty;
			}
			StringWriter stringWriter = new StringWriter();
			stringWriter.Write('<');
			stringWriter.Write("DataViewSettingCollectionString>");
			foreach (object obj in this.DataViewSettings)
			{
				DataViewSetting dataViewSetting = (DataViewSetting)obj;
				stringWriter.Write('<');
				stringWriter.Write(XmlConvert.EncodeName(dataViewSetting.Table.TableName));
				stringWriter.Write(" Sort=\"");
				stringWriter.Write(this.Escape(dataViewSetting.Sort));
				stringWriter.Write('"');
				if (dataViewSetting.ApplyDefaultSort)
				{
					stringWriter.Write(" ApplyDefaultSort=\"true\"");
				}
				stringWriter.Write(" RowFilter=\"");
				stringWriter.Write(this.Escape(dataViewSetting.RowFilter));
				stringWriter.Write("\" RowStateFilter=\"");
				stringWriter.Write(dataViewSetting.RowStateFilter.ToString());
				stringWriter.Write("\"/>");
			}
			stringWriter.Write("</DataViewSettingCollectionString>");
			return stringWriter.ToString();
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001D1C4 File Offset: 0x0001B3C4
		private string Escape(string s)
		{
			return s.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("'", "&apos;")
				.Replace("<", "&lt;")
				.Replace(">", "&gt;");
		}

		/// <summary>Creates a <see cref="T:System.Data.DataView" /> for the specified <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataView" /> object.</returns>
		/// <param name="table">The name of the <see cref="T:System.Data.DataTable" /> to use in the <see cref="T:System.Data.DataView" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000503 RID: 1283 RVA: 0x0001D220 File Offset: 0x0001B420
		public DataView CreateDataView(DataTable table)
		{
			if (this.settings[table] != null)
			{
				DataViewSetting dataViewSetting = this.settings[table];
				return new DataView(table, this, dataViewSetting.Sort, dataViewSetting.RowFilter, dataViewSetting.RowStateFilter);
			}
			return new DataView(table);
		}

		/// <summary>Raises the <see cref="E:System.Data.DataViewManager.ListChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x06000504 RID: 1284 RVA: 0x0001D26C File Offset: 0x0001B46C
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			if (this.ListChanged != null)
			{
				this.ListChanged(this, e);
			}
		}

		/// <summary>Raises a <see cref="E:System.Data.DataRelationCollection.CollectionChanged" /> event when a <see cref="T:System.Data.DataRelation" /> is added to or removed from the <see cref="T:System.Data.DataRelationCollection" />.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x06000505 RID: 1285 RVA: 0x0001D288 File Offset: 0x0001B488
		protected virtual void RelationCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			this.OnListChanged(this.CollectionToListChangeEventArgs(e));
		}

		/// <summary>Raises a <see cref="E:System.Data.DataTableCollection.CollectionChanged" /> event when a <see cref="T:System.Data.DataTable" /> is added to or removed from the <see cref="T:System.Data.DataTableCollection" />.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x06000506 RID: 1286 RVA: 0x0001D298 File Offset: 0x0001B498
		protected virtual void TableCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			this.OnListChanged(this.CollectionToListChangeEventArgs(e));
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001D2A8 File Offset: 0x0001B4A8
		private ListChangedEventArgs CollectionToListChangeEventArgs(CollectionChangeEventArgs e)
		{
			ListChangedEventArgs listChangedEventArgs;
			if (e.Action == CollectionChangeAction.Remove)
			{
				listChangedEventArgs = null;
			}
			else if (e.Action == CollectionChangeAction.Refresh)
			{
				listChangedEventArgs = new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, null);
			}
			else
			{
				object obj;
				if (typeof(DataTable).IsAssignableFrom(e.Element.GetType()))
				{
					obj = new DataTablePropertyDescriptor((DataTable)e.Element);
				}
				else
				{
					obj = new DataRelationPropertyDescriptor((DataRelation)e.Element);
				}
				if (e.Action == CollectionChangeAction.Add)
				{
					listChangedEventArgs = new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, (PropertyDescriptor)obj);
				}
				else
				{
					listChangedEventArgs = new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, (PropertyDescriptor)obj);
				}
			}
			return listChangedEventArgs;
		}

		// Token: 0x0400018A RID: 394
		private DataSet dataSet;

		// Token: 0x0400018B RID: 395
		private DataViewManagerListItemTypeDescriptor descriptor;

		// Token: 0x0400018C RID: 396
		private DataViewSettingCollection settings;

		// Token: 0x0400018D RID: 397
		private string xml;
	}
}
