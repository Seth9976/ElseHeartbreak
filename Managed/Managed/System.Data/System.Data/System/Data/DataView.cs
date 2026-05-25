using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Text;
using Mono.Data.SqlExpressions;

namespace System.Data
{
	/// <summary>Represents a databindable, customized view of a <see cref="T:System.Data.DataTable" /> for sorting, filtering, searching, editing, and navigation.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200003B RID: 59
	[DefaultEvent("PositionChanged")]
	[Editor("Microsoft.VSDesigner.Data.Design.DataSourceEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("Microsoft.VSDesigner.Data.VS.DataViewDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Table")]
	public class DataView : MarshalByValueComponent, IList, IEnumerable, ITypedList, IBindingListView, IBindingList, ICollection, ISupportInitialize, ISupportInitializeNotification
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataView" /> class.</summary>
		// Token: 0x06000462 RID: 1122 RVA: 0x0001ADF0 File Offset: 0x00018FF0
		public DataView()
		{
			this.rowState = DataViewRowState.CurrentRows;
			this.Open();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataView" /> class with the specified <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> to add to the <see cref="T:System.Data.DataView" />. </param>
		// Token: 0x06000463 RID: 1123 RVA: 0x0001AE58 File Offset: 0x00019058
		public DataView(DataTable table)
			: this(table, null)
		{
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001AE64 File Offset: 0x00019064
		internal DataView(DataTable table, DataViewManager manager)
		{
			this.dataTable = table;
			this.rowState = DataViewRowState.CurrentRows;
			this.dataViewManager = manager;
			this.Open();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataView" /> class with the specified <see cref="T:System.Data.DataTable" />, <see cref="P:System.Data.DataView.RowFilter" />, <see cref="P:System.Data.DataView.Sort" />, and <see cref="T:System.Data.DataViewRowState" />.</summary>
		/// <param name="table">A <see cref="T:System.Data.DataTable" /> to add to the <see cref="T:System.Data.DataView" />. </param>
		/// <param name="RowFilter">A <see cref="P:System.Data.DataView.RowFilter" /> to apply to the <see cref="T:System.Data.DataView" />. </param>
		/// <param name="Sort">A <see cref="P:System.Data.DataView.Sort" /> to apply to the <see cref="T:System.Data.DataView" />. </param>
		/// <param name="RowState">A <see cref="T:System.Data.DataViewRowState" /> to apply to the <see cref="T:System.Data.DataView" />. </param>
		// Token: 0x06000465 RID: 1125 RVA: 0x0001AED8 File Offset: 0x000190D8
		public DataView(DataTable table, string RowFilter, string Sort, DataViewRowState RowState)
			: this(table, null, RowFilter, Sort, RowState)
		{
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001AEE8 File Offset: 0x000190E8
		internal DataView(DataTable table, DataViewManager manager, string RowFilter, string Sort, DataViewRowState RowState)
		{
			this.dataTable = table;
			this.dataViewManager = manager;
			this.rowState = DataViewRowState.CurrentRows;
			this.RowFilter = RowFilter;
			this.Sort = Sort;
			this.rowState = RowState;
			this.Open();
		}

		/// <summary>Occurs when the list managed by the <see cref="T:System.Data.DataView" /> changes.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000468 RID: 1128 RVA: 0x0001AF84 File Offset: 0x00019184
		// (remove) Token: 0x06000469 RID: 1129 RVA: 0x0001AFA0 File Offset: 0x000191A0
		[DataCategory("Data")]
		public event ListChangedEventHandler ListChanged;

		/// <summary>Occurs when initialization of the <see cref="T:System.Data.DataView" /> is completed.</summary>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600046A RID: 1130 RVA: 0x0001AFBC File Offset: 0x000191BC
		// (remove) Token: 0x0600046B RID: 1131 RVA: 0x0001AFD8 File Offset: 0x000191D8
		public event EventHandler Initialized;

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetItemProperties(System.ComponentModel.PropertyDescriptor[])" />.</summary>
		// Token: 0x0600046C RID: 1132 RVA: 0x0001AFF4 File Offset: 0x000191F4
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			if (this.dataTable == null)
			{
				return new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			}
			PropertyDescriptor[] array = new PropertyDescriptor[this.dataTable.Columns.Count + this.dataTable.ChildRelations.Count];
			int num = 0;
			for (int i = 0; i < this.dataTable.Columns.Count; i++)
			{
				DataColumn dataColumn = this.dataTable.Columns[i];
				DataColumnPropertyDescriptor dataColumnPropertyDescriptor = new DataColumnPropertyDescriptor(dataColumn.ColumnName, i, null);
				dataColumnPropertyDescriptor.SetComponentType(typeof(DataRowView));
				dataColumnPropertyDescriptor.SetPropertyType(dataColumn.DataType);
				dataColumnPropertyDescriptor.SetReadOnly(dataColumn.ReadOnly);
				dataColumnPropertyDescriptor.SetBrowsable(dataColumn.ColumnMapping != MappingType.Hidden);
				array[num++] = dataColumnPropertyDescriptor;
			}
			for (int j = 0; j < this.dataTable.ChildRelations.Count; j++)
			{
				DataRelation dataRelation = this.dataTable.ChildRelations[j];
				DataRelationPropertyDescriptor dataRelationPropertyDescriptor = new DataRelationPropertyDescriptor(dataRelation);
				array[num++] = dataRelationPropertyDescriptor;
			}
			return new PropertyDescriptorCollection(array);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ITypedList.GetListName(System.ComponentModel.PropertyDescriptor[])" />.</summary>
		// Token: 0x0600046D RID: 1133 RVA: 0x0001B11C File Offset: 0x0001931C
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			if (this.dataTable != null)
			{
				return this.dataTable.TableName;
			}
			return string.Empty;
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001B13C File Offset: 0x0001933C
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0001B140 File Offset: 0x00019340
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0001B144 File Offset: 0x00019344
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001B148 File Offset: 0x00019348
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		// Token: 0x170000B0 RID: 176
		object IList.this[int recordIndex]
		{
			get
			{
				return this[recordIndex];
			}
			[MonoTODO]
			set
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		// Token: 0x06000474 RID: 1140 RVA: 0x0001B160 File Offset: 0x00019360
		int IList.Add(object value)
		{
			throw new ArgumentException("Cannot add external objects to this list.");
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
		// Token: 0x06000475 RID: 1141 RVA: 0x0001B16C File Offset: 0x0001936C
		void IList.Clear()
		{
			throw new ArgumentException("Cannot clear this list.");
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		// Token: 0x06000476 RID: 1142 RVA: 0x0001B178 File Offset: 0x00019378
		bool IList.Contains(object value)
		{
			DataRowView dataRowView = value as DataRowView;
			return dataRowView != null && dataRowView.DataView == this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		// Token: 0x06000477 RID: 1143 RVA: 0x0001B1A0 File Offset: 0x000193A0
		int IList.IndexOf(object value)
		{
			DataRowView dataRowView = value as DataRowView;
			if (dataRowView != null && dataRowView.DataView == this)
			{
				return dataRowView.Index;
			}
			return -1;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		// Token: 0x06000478 RID: 1144 RVA: 0x0001B1D0 File Offset: 0x000193D0
		void IList.Insert(int index, object value)
		{
			throw new ArgumentException("Cannot insert external objects to this list.");
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		// Token: 0x06000479 RID: 1145 RVA: 0x0001B1DC File Offset: 0x000193DC
		void IList.Remove(object value)
		{
			DataRowView dataRowView = value as DataRowView;
			if (dataRowView != null && dataRowView.DataView == this)
			{
				((IList)this).RemoveAt(dataRowView.Index);
			}
			throw new ArgumentException("Cannot remove external objects to this list.");
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		// Token: 0x0600047A RID: 1146 RVA: 0x0001B218 File Offset: 0x00019418
		void IList.RemoveAt(int index)
		{
			this.Delete(index);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		// Token: 0x0600047B RID: 1147 RVA: 0x0001B224 File Offset: 0x00019424
		[MonoTODO]
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>The item added to the list.</returns>
		// Token: 0x0600047C RID: 1148 RVA: 0x0001B22C File Offset: 0x0001942C
		object IBindingList.AddNew()
		{
			return this.AddNew();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		// Token: 0x0600047D RID: 1149 RVA: 0x0001B234 File Offset: 0x00019434
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			if (!(property is DataColumnPropertyDescriptor))
			{
				throw new ArgumentException("Dataview accepts only DataColumnPropertyDescriptors", "property");
			}
			this.sortProperty = property;
			string text = string.Format("[{0}]", property.Name);
			if (direction == ListSortDirection.Descending)
			{
				text += " DESC";
			}
			this.Sort = text;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" />.</summary>
		// Token: 0x0600047E RID: 1150 RVA: 0x0001B290 File Offset: 0x00019490
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			DataColumn dataColumn = this.Table.Columns[property.Name];
			Index index = this.Table.FindIndex(new DataColumn[] { dataColumn }, this.sortOrder, this.RowStateFilter, this.FilterExpression);
			if (index == null)
			{
				index = new Index(new Key(this.Table, new DataColumn[] { dataColumn }, this.sortOrder, this.RowStateFilter, this.FilterExpression));
			}
			return index.FindIndex(new object[] { key });
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveIndex(System.ComponentModel.PropertyDescriptor)" />.</summary>
		// Token: 0x0600047F RID: 1151 RVA: 0x0001B320 File Offset: 0x00019520
		[MonoTODO]
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingList.RemoveSort" />.</summary>
		// Token: 0x06000480 RID: 1152 RVA: 0x0001B328 File Offset: 0x00019528
		void IBindingList.RemoveSort()
		{
			this.sortProperty = null;
			this.Sort = string.Empty;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowEdit" />.</summary>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001B33C File Offset: 0x0001953C
		bool IBindingList.AllowEdit
		{
			get
			{
				return this.AllowEdit;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowNew" />.</summary>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001B344 File Offset: 0x00019544
		bool IBindingList.AllowNew
		{
			get
			{
				return this.AllowNew;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.AllowRemove" />.</summary>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001B34C File Offset: 0x0001954C
		bool IBindingList.AllowRemove
		{
			[MonoTODO]
			get
			{
				return this.AllowDelete;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.IsSorted" />.</summary>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001B354 File Offset: 0x00019554
		bool IBindingList.IsSorted
		{
			get
			{
				return this.Sort != null && this.Sort.Length != 0;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortDirection" />.</summary>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001B380 File Offset: 0x00019580
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				if (this.sortOrder != null && this.sortOrder.Length > 0)
				{
					return this.sortOrder[0];
				}
				return ListSortDirection.Ascending;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SortProperty" />.</summary>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0001B3A8 File Offset: 0x000195A8
		PropertyDescriptor IBindingList.SortProperty
		{
			get
			{
				if (this.sortProperty == null && this.sortColumns != null && this.sortColumns.Length > 0)
				{
					PropertyDescriptorCollection itemProperties = ((ITypedList)this).GetItemProperties(null);
					return itemProperties.Find(this.sortColumns[0].ColumnName, false);
				}
				return this.sortProperty;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification" />.</summary>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001B3FC File Offset: 0x000195FC
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" />.</summary>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0001B400 File Offset: 0x00019600
		bool IBindingList.SupportsSearching
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" />.</summary>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0001B404 File Offset: 0x00019604
		bool IBindingList.SupportsSorting
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.Filter" />.</summary>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0001B408 File Offset: 0x00019608
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x0001B410 File Offset: 0x00019610
		string IBindingListView.Filter
		{
			get
			{
				return this.RowFilter;
			}
			set
			{
				this.RowFilter = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SortDescriptions" />.</summary>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0001B41C File Offset: 0x0001961C
		ListSortDescriptionCollection IBindingListView.SortDescriptions
		{
			get
			{
				ListSortDescriptionCollection listSortDescriptionCollection = new ListSortDescriptionCollection();
				for (int i = 0; i < this.sortColumns.Length; i++)
				{
					ListSortDescription listSortDescription = new ListSortDescription(new DataColumnPropertyDescriptor(this.sortColumns[i]), this.sortOrder[i]);
					((IList)listSortDescriptionCollection).Add(listSortDescription);
				}
				return listSortDescriptionCollection;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SupportsAdvancedSorting" />.</summary>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0001B46C File Offset: 0x0001966C
		bool IBindingListView.SupportsAdvancedSorting
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IBindingListView.SupportsFiltering" />.</summary>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0001B470 File Offset: 0x00019670
		bool IBindingListView.SupportsFiltering
		{
			get
			{
				return true;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingListView.ApplySort(System.ComponentModel.ListSortDescriptionCollection)" />.</summary>
		// Token: 0x0600048F RID: 1167 RVA: 0x0001B474 File Offset: 0x00019674
		[MonoTODO]
		void IBindingListView.ApplySort(ListSortDescriptionCollection sorts)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in ((IEnumerable)sorts))
			{
				ListSortDescription listSortDescription = (ListSortDescription)obj;
				stringBuilder.AppendFormat("[{0}]{1},", listSortDescription.PropertyDescriptor.Name, (listSortDescription.SortDirection != ListSortDirection.Descending) ? string.Empty : " DESC");
			}
			this.Sort = stringBuilder.ToString(0, stringBuilder.Length - 1);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IBindingListView.RemoveFilter" />.</summary>
		// Token: 0x06000490 RID: 1168 RVA: 0x0001B524 File Offset: 0x00019724
		void IBindingListView.RemoveFilter()
		{
			((IBindingListView)this).Filter = string.Empty;
		}

		/// <summary>Sets or gets a value that indicates whether deletes are allowed.</summary>
		/// <returns>true, if deletes are allowed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0001B534 File Offset: 0x00019734
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0001B53C File Offset: 0x0001973C
		[DefaultValue(true)]
		[DataCategory("Data")]
		public bool AllowDelete
		{
			get
			{
				return this.allowDelete;
			}
			set
			{
				this.allowDelete = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether edits are allowed.</summary>
		/// <returns>true, if edits are allowed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001B548 File Offset: 0x00019748
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0001B550 File Offset: 0x00019750
		[DataCategory("Data")]
		[DefaultValue(true)]
		public bool AllowEdit
		{
			get
			{
				return this.allowEdit;
			}
			set
			{
				this.allowEdit = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the new rows can be added by using the <see cref="M:System.Data.DataView.AddNew" /> method.</summary>
		/// <returns>true, if new rows can be added; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001B55C File Offset: 0x0001975C
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x0001B564 File Offset: 0x00019764
		[DefaultValue(true)]
		[DataCategory("Data")]
		public bool AllowNew
		{
			get
			{
				return this.allowNew;
			}
			set
			{
				this.allowNew = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to use the default sort.</summary>
		/// <returns>true, if the default sort is used; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0001B570 File Offset: 0x00019770
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x0001B578 File Offset: 0x00019778
		[RefreshProperties(RefreshProperties.All)]
		[DataCategory("Data")]
		[DefaultValue(false)]
		public bool ApplyDefaultSort
		{
			get
			{
				return this.applyDefaultSort;
			}
			set
			{
				if (this.isInitPhase)
				{
					this.initApplyDefaultSort = value;
					return;
				}
				if (this.applyDefaultSort == value)
				{
					return;
				}
				this.applyDefaultSort = value;
				if (this.applyDefaultSort && (this.sort == null || this.sort == string.Empty))
				{
					this.PopulateDefaultSort();
				}
				if (!this.inEndInit)
				{
					this.UpdateIndex(true);
					this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1, -1));
				}
			}
		}

		/// <summary>Gets the number of records in the <see cref="T:System.Data.DataView" /> after <see cref="P:System.Data.DataView.RowFilter" /> and <see cref="P:System.Data.DataView.RowStateFilter" /> have been applied.</summary>
		/// <returns>The number of records in the <see cref="T:System.Data.DataView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0001B600 File Offset: 0x00019800
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.rowCache.Length;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewManager" /> associated with this view.</summary>
		/// <returns>The DataViewManager that created this view. If this is the default <see cref="T:System.Data.DataView" /> for a <see cref="T:System.Data.DataTable" />, the DataViewManager property returns the default DataViewManager for the DataSet. Otherwise, if the DataView was created without a DataViewManager, this property is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0001B60C File Offset: 0x0001980C
		[Browsable(false)]
		public DataViewManager DataViewManager
		{
			get
			{
				return this.dataViewManager;
			}
		}

		/// <summary>Gets a row of data from a specified table.</summary>
		/// <returns>A <see cref="T:System.Data.DataRowView" /> of the row that you want.</returns>
		/// <param name="recordIndex">The index of a record in the <see cref="T:System.Data.DataTable" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000C4 RID: 196
		public DataRowView this[int recordIndex]
		{
			get
			{
				if (recordIndex > this.rowCache.Length)
				{
					throw new IndexOutOfRangeException("There is no row at position: " + recordIndex + ".");
				}
				return this.rowCache[recordIndex];
			}
		}

		/// <summary>Gets or sets the expression used to filter which rows are viewed in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A string that specifies how rows are to be filtered. For more information, see the Remarks section.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0001B648 File Offset: 0x00019848
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0001B650 File Offset: 0x00019850
		[DataCategory("Data")]
		[DefaultValue("")]
		public virtual string RowFilter
		{
			get
			{
				return this.rowFilter;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.isInitPhase)
				{
					this.initRowFilter = value;
					return;
				}
				CultureInfo cultureInfo = ((this.Table == null) ? CultureInfo.CurrentCulture : this.Table.Locale);
				if (string.Compare(this.rowFilter, value, false, cultureInfo) == 0)
				{
					return;
				}
				if (value.Length == 0)
				{
					this.rowFilterExpr = null;
				}
				else
				{
					Parser parser = new Parser();
					this.rowFilterExpr = parser.Compile(value);
				}
				this.rowFilter = value;
				if (!this.inEndInit)
				{
					this.UpdateIndex(true);
					this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1, -1));
				}
			}
		}

		/// <summary>Gets or sets the row state filter used in the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataViewRowState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001B704 File Offset: 0x00019904
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0001B70C File Offset: 0x0001990C
		[DataCategory("Data")]
		[DefaultValue(DataViewRowState.CurrentRows)]
		public DataViewRowState RowStateFilter
		{
			get
			{
				return this.rowState;
			}
			set
			{
				if (this.isInitPhase)
				{
					this.initRowState = value;
					return;
				}
				if (value == this.rowState)
				{
					return;
				}
				this.rowState = value;
				if (!this.inEndInit)
				{
					this.UpdateIndex(true);
					this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1, -1));
				}
			}
		}

		/// <summary>Gets or sets the sort column or columns, and sort order for the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A string that contains the column name followed by "ASC" (ascending) or "DESC" (descending). Columns are sorted ascending by default. Multiple columns can be separated by commas.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0001B760 File Offset: 0x00019960
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0001B77C File Offset: 0x0001997C
		[DataCategory("Data")]
		[DefaultValue("")]
		public string Sort
		{
			get
			{
				if (this.useDefaultSort)
				{
					return string.Empty;
				}
				return this.sort;
			}
			set
			{
				if (this.isInitPhase)
				{
					this.initSort = value;
					return;
				}
				if (value == this.sort)
				{
					return;
				}
				if (value == null || value.Length == 0)
				{
					this.useDefaultSort = true;
					if (this.ApplyDefaultSort)
					{
						this.PopulateDefaultSort();
					}
				}
				else
				{
					this.useDefaultSort = false;
					this.sort = value;
				}
				if (!this.inEndInit)
				{
					this.UpdateIndex(true);
					this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1, -1));
				}
			}
		}

		/// <summary>Gets or sets the source <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that provides the data for this view.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0001B80C File Offset: 0x00019A0C
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0001B814 File Offset: 0x00019A14
		[DataCategory("Data")]
		[TypeConverter(typeof(DataTableTypeConverter))]
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.All)]
		public DataTable Table
		{
			get
			{
				return this.dataTable;
			}
			set
			{
				if (value == this.dataTable)
				{
					return;
				}
				if (this.isInitPhase)
				{
					this.initTable = value;
					return;
				}
				if (value != null && value.TableName.Equals(string.Empty))
				{
					throw new DataException("Cannot bind to DataTable with no name.");
				}
				if (this.dataTable != null)
				{
					this.UnregisterEventHandlers();
				}
				this.dataTable = value;
				if (this.dataTable != null)
				{
					this.RegisterEventHandlers();
					this.OnListChanged(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, 0, 0));
					this.sort = string.Empty;
					this.rowFilter = string.Empty;
					if (!this.inEndInit)
					{
						this.UpdateIndex(true);
						this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1, -1));
					}
				}
			}
		}

		/// <summary>Adds a new row to the <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataRowView" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004A4 RID: 1188 RVA: 0x0001B8D4 File Offset: 0x00019AD4
		public virtual DataRowView AddNew()
		{
			if (!this.IsOpen)
			{
				throw new DataException("DataView is not open.");
			}
			if (!this.AllowNew)
			{
				throw new DataException("Cannot call AddNew on a DataView where AllowNew is false.");
			}
			if (this._lastAdded != null)
			{
				this.CompleteLastAdded(true);
			}
			this._lastAdded = this.dataTable.NewRow();
			this.UpdateIndex(true);
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, this.Count - 1, -1));
			return this[this.Count - 1];
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001B95C File Offset: 0x00019B5C
		internal void CompleteLastAdded(bool add)
		{
			DataRow lastAdded = this._lastAdded;
			if (add)
			{
				try
				{
					this.dataTable.Rows.Add(this._lastAdded);
					this._lastAdded = null;
					this.UpdateIndex();
				}
				catch (Exception)
				{
					this._lastAdded = lastAdded;
					throw;
				}
			}
			else
			{
				this._lastAdded.CancelEdit();
				this._lastAdded = null;
				this.UpdateIndex();
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, this.Count, -1));
			}
		}

		/// <summary>Starts the initialization of a <see cref="T:System.Data.DataView" /> that is used on a form or used by another component. The initialization occurs at runtime.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060004A6 RID: 1190 RVA: 0x0001B9FC File Offset: 0x00019BFC
		public void BeginInit()
		{
			this.initTable = this.Table;
			this.initApplyDefaultSort = this.ApplyDefaultSort;
			this.initSort = this.Sort;
			this.initRowFilter = this.RowFilter;
			this.initRowState = this.RowStateFilter;
			this.isInitPhase = true;
			this.DataViewInitialized(false);
		}

		/// <summary>Copies items into an array. Only for Web Forms Interfaces.</summary>
		/// <param name="array">array to copy into. </param>
		/// <param name="index">index to start at. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004A7 RID: 1191 RVA: 0x0001BA54 File Offset: 0x00019C54
		public void CopyTo(Array array, int index)
		{
			if (index + this.rowCache.Length > array.Length)
			{
				throw new IndexOutOfRangeException();
			}
			int num = 0;
			while (num < this.rowCache.Length && num < array.Length)
			{
				array.SetValue(this.rowCache[num], index + num);
				num++;
			}
		}

		/// <summary>Deletes a row at the specified index.</summary>
		/// <param name="index">The index of the row to delete. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004A8 RID: 1192 RVA: 0x0001BAB4 File Offset: 0x00019CB4
		public void Delete(int index)
		{
			if (!this.IsOpen)
			{
				throw new DataException("DataView is not open.");
			}
			if (this._lastAdded != null && index == this.Count)
			{
				this.CompleteLastAdded(false);
				return;
			}
			if (!this.AllowDelete)
			{
				throw new DataException("Cannot delete on a DataSource where AllowDelete is false.");
			}
			if (index > this.rowCache.Length)
			{
				throw new IndexOutOfRangeException("There is no row at position: " + index + ".");
			}
			DataRowView dataRowView = this.rowCache[index];
			dataRowView.Row.Delete();
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Data.DataView" /> that is used on a form or used by another component. The initialization occurs at runtime.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004A9 RID: 1193 RVA: 0x0001BB4C File Offset: 0x00019D4C
		public void EndInit()
		{
			this.isInitPhase = false;
			this.inEndInit = true;
			this.Table = this.initTable;
			this.ApplyDefaultSort = this.initApplyDefaultSort;
			this.Sort = this.initSort;
			this.RowFilter = this.initRowFilter;
			this.RowStateFilter = this.initRowState;
			this.inEndInit = false;
			this.UpdateIndex(true);
			this.DataViewInitialized(true);
		}

		/// <summary>Finds a row in the <see cref="T:System.Data.DataView" /> by the specified sort key value.</summary>
		/// <returns>The index of the row in the <see cref="T:System.Data.DataView" /> that contains the sort key value specified; otherwise -1 if the sort key value does not exist.</returns>
		/// <param name="key">The object to search for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004AA RID: 1194 RVA: 0x0001BBB8 File Offset: 0x00019DB8
		public int Find(object key)
		{
			object[] array = new object[] { key };
			return this.Find(array);
		}

		/// <summary>Finds a row in the <see cref="T:System.Data.DataView" /> by the specified sort key values.</summary>
		/// <returns>The index of the position of the first row in the <see cref="T:System.Data.DataView" /> that matches the sort key values specified; otherwise -1 if there are no matching sort key values. </returns>
		/// <param name="key">An array of values, typed as <see cref="T:System.Object" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004AB RID: 1195 RVA: 0x0001BBD8 File Offset: 0x00019DD8
		public int Find(object[] key)
		{
			if (this.sort == null || this.sort.Length == 0)
			{
				throw new ArgumentException("Find finds a row based on a Sort order, and no Sort order is specified");
			}
			if (this.Index == null)
			{
				this.UpdateIndex(true);
			}
			int num = -1;
			try
			{
				num = this.Index.FindIndex(key);
			}
			catch (FormatException)
			{
			}
			catch (InvalidCastException)
			{
			}
			return num;
		}

		/// <summary>Returns an array of <see cref="T:System.Data.DataRowView" /> objects whose columns match the specified sort key value.</summary>
		/// <returns>An array of DataRowView objects whose columns match the specified sort key value; or, if no rows contain the specified sort key values, an empty DataRowView array.</returns>
		/// <param name="key">The column value, typed as <see cref="T:System.Object" />, to search for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004AC RID: 1196 RVA: 0x0001BC74 File Offset: 0x00019E74
		public DataRowView[] FindRows(object key)
		{
			return this.FindRows(new object[] { key });
		}

		/// <summary>Returns an array of <see cref="T:System.Data.DataRowView" /> objects whose columns match the specified sort key value.</summary>
		/// <returns>An array of DataRowView objects whose columns match the specified sort key value; or, if no rows contain the specified sort key values, an empty DataRowView array.</returns>
		/// <param name="key">An array of column values, typed as <see cref="T:System.Object" />, to search for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004AD RID: 1197 RVA: 0x0001BC88 File Offset: 0x00019E88
		public DataRowView[] FindRows(object[] key)
		{
			if (this.sort == null || this.sort.Length == 0)
			{
				throw new ArgumentException("Find finds a row based on a Sort order, and no Sort order is specified");
			}
			if (this.Index == null)
			{
				this.UpdateIndex(true);
			}
			int[] array = this.Index.FindAllIndexes(key);
			DataRowView[] array2 = new DataRowView[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.rowCache[array[i]];
			}
			return array2;
		}

		/// <summary>Gets an enumerator for this <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for navigating through the list.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004AE RID: 1198 RVA: 0x0001BD08 File Offset: 0x00019F08
		public IEnumerator GetEnumerator()
		{
			DataRowView[] array = new DataRowView[this.Count];
			this.CopyTo(array, 0);
			return array.GetEnumerator();
		}

		/// <summary>Gets a value that indicates whether the data source is currently open and projecting views of data on the <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>true, if the source is open; otherwise, false.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0001BD30 File Offset: 0x00019F30
		[Browsable(false)]
		protected bool IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001BD38 File Offset: 0x00019F38
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0001BD40 File Offset: 0x00019F40
		internal Index Index
		{
			get
			{
				return this._index;
			}
			set
			{
				if (this._index != null)
				{
					this._index.RemoveRef();
					this.Table.DropIndex(this._index);
				}
				this._index = value;
				if (this._index != null)
				{
					this._index.AddRef();
				}
			}
		}

		/// <summary>Closes the <see cref="T:System.Data.DataView" />.</summary>
		// Token: 0x060004B2 RID: 1202 RVA: 0x0001BD94 File Offset: 0x00019F94
		protected void Close()
		{
			if (this.dataTable != null)
			{
				this.UnregisterEventHandlers();
			}
			this.Index = null;
			this.rowCache = new DataRowView[0];
			this.isOpen = false;
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Data.DataView" /> object.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060004B3 RID: 1203 RVA: 0x0001BDC4 File Offset: 0x00019FC4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
			base.Dispose(disposing);
		}

		/// <summary>Occurs after a <see cref="T:System.Data.DataView" /> has been changed successfully.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x060004B4 RID: 1204 RVA: 0x0001BDDC File Offset: 0x00019FDC
		protected virtual void IndexListChanged(object sender, ListChangedEventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Data.DataView.ListChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x060004B5 RID: 1205 RVA: 0x0001BDE0 File Offset: 0x00019FE0
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			try
			{
				if (this.ListChanged != null)
				{
					this.ListChanged(this, e);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001BE2C File Offset: 0x0001A02C
		internal void ChangedList(ListChangedType listChangedType, int newIndex, int oldIndex)
		{
			ListChangedEventArgs listChangedEventArgs = new ListChangedEventArgs(listChangedType, newIndex, oldIndex);
			this.OnListChanged(listChangedEventArgs);
		}

		/// <summary>Opens a <see cref="T:System.Data.DataView" />.</summary>
		// Token: 0x060004B7 RID: 1207 RVA: 0x0001BE4C File Offset: 0x0001A04C
		protected void Open()
		{
			this.UpdateIndex(true);
			if (this.dataTable != null)
			{
				this.RegisterEventHandlers();
			}
			this.isOpen = true;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001BE70 File Offset: 0x0001A070
		private void RegisterEventHandlers()
		{
			this.dataTable.ColumnChanged += this.OnColumnChanged;
			this.dataTable.RowChanged += this.OnRowChanged;
			this.dataTable.RowDeleted += this.OnRowDeleted;
			this.dataTable.Columns.CollectionChanged += this.ColumnCollectionChanged;
			this.dataTable.Columns.CollectionMetaDataChanged += this.ColumnCollectionChanged;
			this.dataTable.Constraints.CollectionChanged += this.OnConstraintCollectionChanged;
			this.dataTable.ChildRelations.CollectionChanged += this.OnRelationCollectionChanged;
			this.dataTable.ParentRelations.CollectionChanged += this.OnRelationCollectionChanged;
			this.dataTable.Rows.ListChanged += this.OnRowCollectionChanged;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001BF6C File Offset: 0x0001A16C
		private void OnRowCollectionChanged(object sender, ListChangedEventArgs args)
		{
			if (args.ListChangedType == ListChangedType.Reset)
			{
				this.rowCache = new DataRowView[0];
				this.UpdateIndex(true);
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1, -1));
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001BFA8 File Offset: 0x0001A1A8
		private void UnregisterEventHandlers()
		{
			this.dataTable.ColumnChanged -= this.OnColumnChanged;
			this.dataTable.RowChanged -= this.OnRowChanged;
			this.dataTable.RowDeleted -= this.OnRowDeleted;
			this.dataTable.Columns.CollectionChanged -= this.ColumnCollectionChanged;
			this.dataTable.Columns.CollectionMetaDataChanged -= this.ColumnCollectionChanged;
			this.dataTable.Constraints.CollectionChanged -= this.OnConstraintCollectionChanged;
			this.dataTable.ChildRelations.CollectionChanged -= this.OnRelationCollectionChanged;
			this.dataTable.ParentRelations.CollectionChanged -= this.OnRelationCollectionChanged;
			this.dataTable.Rows.ListChanged -= this.OnRowCollectionChanged;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
		private void OnColumnChanged(object sender, DataColumnChangeEventArgs args)
		{
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001C0A8 File Offset: 0x0001A2A8
		private void OnRowChanged(object sender, DataRowChangeEventArgs args)
		{
			int num = this.IndexOf(args.Row);
			this.UpdateIndex(true);
			int num2 = this.IndexOf(args.Row);
			if (args.Action == DataRowAction.Add && num != num2)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, num2, -1));
			}
			if (args.Action == DataRowAction.Change)
			{
				if (num != -1 && num == num2)
				{
					this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, num2, -1));
				}
				else if (num != num2)
				{
					if (num2 < 0)
					{
						this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, num2, num));
					}
					else
					{
						this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemMoved, num2, num));
					}
				}
			}
			if (args.Action == DataRowAction.Rollback)
			{
				if (num < 0 && num2 > -1)
				{
					this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, num2, -1));
				}
				else if (num > -1 && num2 < 0)
				{
					this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, num2, num));
				}
				else if (num != -1 && num == num2)
				{
					this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, num2, -1));
				}
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001C1C4 File Offset: 0x0001A3C4
		private void OnRowDeleted(object sender, DataRowChangeEventArgs args)
		{
			int count = this.Count;
			int num = this.IndexOf(args.Row);
			this.UpdateIndex(true);
			if (count != this.Count)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, num, -1));
			}
		}

		/// <summary>Occurs after a <see cref="T:System.Data.DataColumnCollection" /> has been changed successfully.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data. </param>
		// Token: 0x060004BE RID: 1214 RVA: 0x0001C208 File Offset: 0x0001A408
		protected virtual void ColumnCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Add)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, 0, 0));
			}
			if (e.Action == CollectionChangeAction.Remove)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, 0, 0));
			}
			if (e.Action == CollectionChangeAction.Refresh)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, 0, 0));
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001C264 File Offset: 0x0001A464
		private void OnConstraintCollectionChanged(object sender, CollectionChangeEventArgs args)
		{
			if (args.Action == CollectionChangeAction.Add && args.Element is UniqueConstraint && this.ApplyDefaultSort && this.useDefaultSort)
			{
				this.PopulateDefaultSort((UniqueConstraint)args.Element);
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001C2B4 File Offset: 0x0001A4B4
		private void OnRelationCollectionChanged(object sender, CollectionChangeEventArgs args)
		{
			if (args.Action == CollectionChangeAction.Add)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.PropertyDescriptorAdded, 0, 0));
			}
			if (args.Action == CollectionChangeAction.Remove)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.PropertyDescriptorDeleted, 0, 0));
			}
			if (args.Action == CollectionChangeAction.Refresh)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.PropertyDescriptorChanged, 0, 0));
			}
		}

		/// <summary>Reserved for internal use only.</summary>
		// Token: 0x060004C1 RID: 1217 RVA: 0x0001C310 File Offset: 0x0001A510
		protected void Reset()
		{
			this.Close();
			this.rowCache = new DataRowView[0];
			this.Open();
			this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1, -1));
		}

		/// <summary>Reserved for internal use only.</summary>
		// Token: 0x060004C2 RID: 1218 RVA: 0x0001C344 File Offset: 0x0001A544
		protected void UpdateIndex()
		{
			this.UpdateIndex(false);
		}

		/// <summary>Reserved for internal use only.</summary>
		/// <param name="force">Reserved for internal use only. </param>
		// Token: 0x060004C3 RID: 1219 RVA: 0x0001C350 File Offset: 0x0001A550
		protected virtual void UpdateIndex(bool force)
		{
			if (this.Table == null)
			{
				return;
			}
			if (this.Index == null || force)
			{
				this.sortColumns = DataTable.ParseSortString(this.Table, this.Sort, out this.sortOrder, false);
				this.Index = this.dataTable.GetIndex(this.sortColumns, this.sortOrder, this.RowStateFilter, this.FilterExpression, true);
			}
			else
			{
				this.Index.Key.RowStateFilter = this.RowStateFilter;
				this.Index.Reset();
			}
			int[] all = this.Index.GetAll();
			if (all != null)
			{
				this.InitDataRowViewArray(all, this.Index.Size);
			}
			else
			{
				this.rowCache = new DataRowView[0];
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0001C420 File Offset: 0x0001A620
		internal virtual IExpression FilterExpression
		{
			get
			{
				return this.rowFilterExpr;
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001C428 File Offset: 0x0001A628
		private void InitDataRowViewArray(int[] records, int size)
		{
			if (this._lastAdded != null)
			{
				this.rowCache = new DataRowView[size + 1];
			}
			else
			{
				this.rowCache = new DataRowView[size];
			}
			for (int i = 0; i < size; i++)
			{
				this.rowCache[i] = new DataRowView(this, this.Table.RecordCache[records[i]], i);
			}
			if (this._lastAdded != null)
			{
				this.rowCache[size] = new DataRowView(this, this._lastAdded, size);
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001C4B4 File Offset: 0x0001A6B4
		private int IndexOf(DataRow dr)
		{
			for (int i = 0; i < this.rowCache.Length; i++)
			{
				if (dr.Equals(this.rowCache[i].Row))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001C4F8 File Offset: 0x0001A6F8
		private void PopulateDefaultSort()
		{
			this.sort = string.Empty;
			foreach (object obj in this.dataTable.Constraints)
			{
				Constraint constraint = (Constraint)obj;
				if (constraint is UniqueConstraint)
				{
					this.PopulateDefaultSort((UniqueConstraint)constraint);
					break;
				}
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001C58C File Offset: 0x0001A78C
		private void PopulateDefaultSort(UniqueConstraint uc)
		{
			if (this.isInitPhase)
			{
				return;
			}
			DataColumn[] columns = uc.Columns;
			if (columns.Length == 0)
			{
				this.sort = string.Empty;
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(columns[0].ColumnName);
			for (int i = 1; i < columns.Length; i++)
			{
				stringBuilder.Append(", ");
				stringBuilder.Append(columns[i].ColumnName);
			}
			this.sort = stringBuilder.ToString();
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001C610 File Offset: 0x0001A810
		internal DataView CreateChildView(DataRelation relation, int index)
		{
			if (relation == null || relation.ParentTable != this.Table)
			{
				throw new ArgumentException("The relation is not parented to the table to which this DataView points.");
			}
			int record = this.GetRecord(index);
			object[] array = new object[relation.ParentColumns.Length];
			for (int i = 0; i < relation.ParentColumns.Length; i++)
			{
				array[i] = relation.ParentColumns[i][record];
			}
			return new RelatedDataView(relation.ChildColumns, array);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001C68C File Offset: 0x0001A88C
		private int GetRecord(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new IndexOutOfRangeException(string.Format("There is no row at position {0}.", index));
			}
			return (index != this.Index.Size) ? this.Index.IndexToRecord(index) : this._lastAdded.IndexFromVersion(DataRowVersion.Default);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001C6F4 File Offset: 0x0001A8F4
		internal DataRowVersion GetRowVersion(int index)
		{
			int record = this.GetRecord(index);
			return this.Table.RecordCache[record].VersionFromIndex(record);
		}

		/// <summary>Gets a value that indicates whether the component is initialized.</summary>
		/// <returns>true to indicate the component has completed initialization; otherwise, false. </returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x0001C720 File Offset: 0x0001A920
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return this.dataViewInitialized;
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001C728 File Offset: 0x0001A928
		private void DataViewInitialized(bool value)
		{
			this.dataViewInitialized = value;
			if (value)
			{
				this.OnDataViewInitialized(new EventArgs());
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001C744 File Offset: 0x0001A944
		private void OnDataViewInitialized(EventArgs e)
		{
			if (this.Initialized != null)
			{
				this.Initialized(this, e);
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.DataView" /> instances are considered equal. </summary>
		/// <returns>true if the two <see cref="T:System.Data.DataView" /> instances are equal; otherwise, false. </returns>
		/// <param name="view">The <see cref="T:System.Data.DataView" /> to be compared.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004CF RID: 1231 RVA: 0x0001C760 File Offset: 0x0001A960
		public virtual bool Equals(DataView dv)
		{
			if (this == dv)
			{
				return true;
			}
			if (this.Table != dv.Table || !(this.Sort == dv.Sort) || !(this.RowFilter == dv.RowFilter) || this.RowStateFilter != dv.RowStateFilter || this.AllowEdit != dv.AllowEdit || this.AllowNew != dv.AllowNew || this.AllowDelete != dv.AllowDelete || this.Count != dv.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (!this[i].Equals(dv[i]))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Creates and returns a new <see cref="T:System.Data.DataTable" /> based on rows in an existing <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> instance that contains the requested rows and columns.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004D0 RID: 1232 RVA: 0x0001C83C File Offset: 0x0001AA3C
		public DataTable ToTable()
		{
			return this.ToTable(this.Table.TableName, false, new string[0]);
		}

		/// <summary>Creates and returns a new <see cref="T:System.Data.DataTable" /> based on rows in an existing <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> instance that contains the requested rows and columns.</returns>
		/// <param name="tableName">The name of the returned <see cref="T:System.Data.DataTable" />.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004D1 RID: 1233 RVA: 0x0001C864 File Offset: 0x0001AA64
		public DataTable ToTable(string tableName)
		{
			return this.ToTable(tableName, false, new string[0]);
		}

		/// <summary>Creates and returns a new <see cref="T:System.Data.DataTable" /> based on rows in an existing <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> instance that contains the requested rows and columns.</returns>
		/// <param name="distinct">If true, the returned <see cref="T:System.Data.DataTable" /> contains rows that have distinct values for all its columns. The default value is false.</param>
		/// <param name="columnNames">A string array that contains a list of the column names to be included in the returned <see cref="T:System.Data.DataTable" />. The <see cref="T:System.Data.DataTable" /> contains the specified columns in the order they appear within this array.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004D2 RID: 1234 RVA: 0x0001C874 File Offset: 0x0001AA74
		public DataTable ToTable(bool isDistinct, params string[] columnNames)
		{
			return this.ToTable(this.Table.TableName, isDistinct, columnNames);
		}

		/// <summary>Creates and returns a new <see cref="T:System.Data.DataTable" /> based on rows in an existing <see cref="T:System.Data.DataView" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataTable" /> instance that contains the requested rows and columns.</returns>
		/// <param name="tableName">The name of the returned <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="distinct">If true, the returned <see cref="T:System.Data.DataTable" /> contains rows that have distinct values for all its columns. The default value is false.</param>
		/// <param name="columnNames">A string array that contains a list of the column names to be included in the returned <see cref="T:System.Data.DataTable" />. The DataTable contains the specified columns in the order they appear within this array.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004D3 RID: 1235 RVA: 0x0001C894 File Offset: 0x0001AA94
		public DataTable ToTable(string tablename, bool isDistinct, params string[] columnNames)
		{
			if (columnNames == null)
			{
				throw new ArgumentNullException("columnNames", "'columnNames' argument cannot be null.");
			}
			DataTable dataTable = new DataTable(tablename);
			ListSortDirection[] array = null;
			DataColumn[] array2;
			if (columnNames.Length > 0)
			{
				array2 = new DataColumn[columnNames.Length];
				for (int i = 0; i < columnNames.Length; i++)
				{
					array2[i] = this.Table.Columns[columnNames[i]];
				}
				if (this.sortColumns != null)
				{
					array = new ListSortDirection[columnNames.Length];
					for (int j = 0; j < columnNames.Length; j++)
					{
						array[j] = ListSortDirection.Ascending;
						for (int k = 0; k < this.sortColumns.Length; k++)
						{
							if (this.sortColumns[k] == array2[j])
							{
								array[j] = this.sortOrder[k];
							}
						}
					}
				}
			}
			else
			{
				array2 = (DataColumn[])this.Table.Columns.ToArray(typeof(DataColumn));
				array = this.sortOrder;
			}
			ArrayList arrayList = new ArrayList();
			for (int l = 0; l < array2.Length; l++)
			{
				DataColumn dataColumn = array2[l].Clone();
				if (dataColumn.Expression != string.Empty)
				{
					dataColumn.Expression = string.Empty;
					arrayList.Add(dataColumn);
				}
				if (dataColumn.ReadOnly)
				{
					dataColumn.ReadOnly = false;
				}
				dataTable.Columns.Add(dataColumn);
			}
			Index index;
			if (this.sort != string.Empty)
			{
				index = this.Table.GetIndex(this.sortColumns, this.sortOrder, this.RowStateFilter, this.FilterExpression, true);
			}
			else
			{
				index = new Index(new Key(this.Table, array2, array, this.RowStateFilter, this.rowFilterExpr));
			}
			DataRow[] array3;
			if (isDistinct)
			{
				array3 = index.GetDistinctRows();
			}
			else
			{
				array3 = index.GetAllRows();
			}
			foreach (DataRow dataRow in array3)
			{
				DataRow dataRow2 = dataTable.NewNotInitializedRow();
				dataTable.Rows.AddInternal(dataRow2);
				dataRow2.Original = -1;
				if (dataRow.HasVersion(DataRowVersion.Current))
				{
					dataRow2.Current = dataTable.RecordCache.CopyRecord(this.Table, dataRow.Current, -1);
				}
				else if (dataRow.HasVersion(DataRowVersion.Original))
				{
					dataRow2.Current = dataTable.RecordCache.CopyRecord(this.Table, dataRow.Original, -1);
				}
				foreach (object obj in arrayList)
				{
					DataColumn dataColumn2 = (DataColumn)obj;
					dataRow2[dataColumn2] = dataRow[dataColumn2.ColumnName];
				}
				dataRow2.Original = -1;
			}
			return dataTable;
		}

		// Token: 0x0400016D RID: 365
		internal DataTable dataTable;

		// Token: 0x0400016E RID: 366
		private string rowFilter = string.Empty;

		// Token: 0x0400016F RID: 367
		private IExpression rowFilterExpr;

		// Token: 0x04000170 RID: 368
		private string sort = string.Empty;

		// Token: 0x04000171 RID: 369
		private ListSortDirection[] sortOrder;

		// Token: 0x04000172 RID: 370
		private PropertyDescriptor sortProperty;

		// Token: 0x04000173 RID: 371
		private DataColumn[] sortColumns;

		// Token: 0x04000174 RID: 372
		internal DataViewRowState rowState;

		// Token: 0x04000175 RID: 373
		internal DataRowView[] rowCache = new DataRowView[0];

		// Token: 0x04000176 RID: 374
		private bool isInitPhase;

		// Token: 0x04000177 RID: 375
		private bool inEndInit;

		// Token: 0x04000178 RID: 376
		private DataTable initTable;

		// Token: 0x04000179 RID: 377
		private bool initApplyDefaultSort;

		// Token: 0x0400017A RID: 378
		private string initSort;

		// Token: 0x0400017B RID: 379
		private string initRowFilter;

		// Token: 0x0400017C RID: 380
		private DataViewRowState initRowState;

		// Token: 0x0400017D RID: 381
		private bool allowNew = true;

		// Token: 0x0400017E RID: 382
		private bool allowEdit = true;

		// Token: 0x0400017F RID: 383
		private bool allowDelete = true;

		// Token: 0x04000180 RID: 384
		private bool applyDefaultSort;

		// Token: 0x04000181 RID: 385
		private bool isOpen;

		// Token: 0x04000182 RID: 386
		private bool useDefaultSort = true;

		// Token: 0x04000183 RID: 387
		private Index _index;

		// Token: 0x04000184 RID: 388
		internal DataRow _lastAdded;

		// Token: 0x04000185 RID: 389
		private DataViewManager dataViewManager;

		// Token: 0x04000186 RID: 390
		internal static ListChangedEventArgs ListResetEventArgs = new ListChangedEventArgs(ListChangedType.Reset, -1, -1);

		// Token: 0x04000187 RID: 391
		private bool dataViewInitialized = true;
	}
}
