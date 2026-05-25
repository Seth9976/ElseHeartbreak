using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Represents a customized view of a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000030 RID: 48
	public class DataRowView : ICustomTypeDescriptor, IEditableObject, IDataErrorInfo, INotifyPropertyChanged
	{
		// Token: 0x0600027B RID: 635 RVA: 0x00011334 File Offset: 0x0000F534
		internal DataRowView(DataView dataView, DataRow row, int index)
		{
			this._dataView = dataView;
			this._dataRow = row;
			this._index = index;
		}

		/// <summary>Event that is raised when a <see cref="T:System.Data.DataRowView" /> property is changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600027C RID: 636 RVA: 0x00011364 File Offset: 0x0000F564
		// (remove) Token: 0x0600027D RID: 637 RVA: 0x00011380 File Offset: 0x0000F580
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetAttributes" />.</summary>
		// Token: 0x0600027E RID: 638 RVA: 0x0001139C File Offset: 0x0000F59C
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetClassName" />.</summary>
		// Token: 0x0600027F RID: 639 RVA: 0x000113B0 File Offset: 0x0000F5B0
		[MonoTODO("Not implemented.   Always returns String.Empty")]
		string ICustomTypeDescriptor.GetClassName()
		{
			return string.Empty;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetComponentName" />.</summary>
		// Token: 0x06000280 RID: 640 RVA: 0x000113B8 File Offset: 0x0000F5B8
		[MonoTODO("Not implemented.   Always returns null")]
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetConverter" />.</summary>
		// Token: 0x06000281 RID: 641 RVA: 0x000113BC File Offset: 0x0000F5BC
		[MonoTODO("Not implemented.   Always returns null")]
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetDefaultEvent" />.</summary>
		// Token: 0x06000282 RID: 642 RVA: 0x000113C0 File Offset: 0x0000F5C0
		[MonoTODO("Not implemented.   Always returns null")]
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetDefaultProperty" />.</summary>
		// Token: 0x06000283 RID: 643 RVA: 0x000113C4 File Offset: 0x0000F5C4
		[MonoTODO("Not implemented.   Always returns null")]
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetEditor(System.Type)" />.</summary>
		// Token: 0x06000284 RID: 644 RVA: 0x000113C8 File Offset: 0x0000F5C8
		[MonoTODO("Not implemented.   Always returns null")]
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetEvents" />.</summary>
		// Token: 0x06000285 RID: 645 RVA: 0x000113CC File Offset: 0x0000F5CC
		[MonoTODO("Not implemented.   Always returns an empty collection")]
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetEvents(System.Attribute[])" />.</summary>
		// Token: 0x06000286 RID: 646 RVA: 0x000113D4 File Offset: 0x0000F5D4
		[MonoTODO("Not implemented.   Always returns an empty collection")]
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetProperties" />.</summary>
		// Token: 0x06000287 RID: 647 RVA: 0x000113DC File Offset: 0x0000F5DC
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			if (this.DataView == null)
			{
				ITypedList dataView = this._dataView;
				return dataView.GetItemProperties(new PropertyDescriptor[0]);
			}
			return this.DataView.Table.GetPropertyDescriptorCollection();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetProperties(System.Attribute[])" />.</summary>
		// Token: 0x06000288 RID: 648 RVA: 0x00011418 File Offset: 0x0000F618
		[MonoTODO("It currently reports more descriptors than necessary")]
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return ((ICustomTypeDescriptor)this).GetProperties();
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.ICustomTypeDescriptor.GetPropertyOwner(System.ComponentModel.PropertyDescriptor)" />.</summary>
		// Token: 0x06000289 RID: 649 RVA: 0x00011430 File Offset: 0x0000F630
		[MonoTODO]
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IDataErrorInfo.Error" />.</summary>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00011434 File Offset: 0x0000F634
		string IDataErrorInfo.Error
		{
			[MonoTODO("Not implemented, always returns String.Empty")]
			get
			{
				return string.Empty;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IDataErrorInfo.Item(System.String)" />.</summary>
		// Token: 0x1700005D RID: 93
		string IDataErrorInfo.this[string colName]
		{
			[MonoTODO("Not implemented, always returns String.Empty")]
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Data.DataRowView" /> is identical to the specified object.</summary>
		/// <returns>true if <paramref name="object" /> is a <see cref="T:System.Data.DataRowView" /> and it returns the same row as the current <see cref="T:System.Data.DataRowView" />; otherwise false.</returns>
		/// <param name="other">An <see cref="T:System.Object" /> to be compared. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600028C RID: 652 RVA: 0x00011444 File Offset: 0x0000F644
		public override bool Equals(object other)
		{
			return other != null && other is DataRowView && ((DataRowView)other)._dataRow != null && ((DataRowView)other)._dataRow.Equals(this._dataRow);
		}

		/// <summary>Begins an edit procedure.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600028D RID: 653 RVA: 0x0001148C File Offset: 0x0000F68C
		public void BeginEdit()
		{
			this._dataRow.BeginEdit();
		}

		/// <summary>Cancels an edit procedure.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600028E RID: 654 RVA: 0x0001149C File Offset: 0x0000F69C
		public void CancelEdit()
		{
			if (this.Row == this.DataView._lastAdded)
			{
				this.DataView.CompleteLastAdded(false);
			}
			else
			{
				this._dataRow.CancelEdit();
			}
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> object. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600028F RID: 655 RVA: 0x000114DC File Offset: 0x0000F6DC
		public DataView CreateChildView(DataRelation relation)
		{
			return this.DataView.CreateChildView(relation, this._index);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified <see cref="T:System.Data.DataRelation" /> name.</summary>
		/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="relationName">A string containing the <see cref="T:System.Data.DataRelation" /> name. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000290 RID: 656 RVA: 0x000114F0 File Offset: 0x0000F6F0
		public DataView CreateChildView(string relationName)
		{
			return this.CreateChildView(this.Row.Table.ChildRelations[relationName]);
		}

		/// <summary>Deletes a row.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000291 RID: 657 RVA: 0x0001151C File Offset: 0x0000F71C
		public void Delete()
		{
			this.DataView.Delete(this._index);
		}

		/// <summary>Ends an edit procedure.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000292 RID: 658 RVA: 0x00011530 File Offset: 0x0000F730
		public void EndEdit()
		{
			if (this.Row == this.DataView._lastAdded)
			{
				this.DataView.CompleteLastAdded(true);
			}
			else
			{
				this._dataRow.EndEdit();
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00011570 File Offset: 0x0000F770
		private void CheckAllowEdit()
		{
			if (!this.DataView.AllowEdit && this.Row != this.DataView._lastAdded)
			{
				throw new DataException("Cannot edit on a DataSource where AllowEdit is false.");
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataView" /> to which this row belongs.</summary>
		/// <returns>The DataView to which this row belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000294 RID: 660 RVA: 0x000115B0 File Offset: 0x0000F7B0
		public DataView DataView
		{
			get
			{
				return this._dataView;
			}
		}

		/// <summary>Indicates whether the row is in edit mode.</summary>
		/// <returns>true if the row is in edit mode; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000115B8 File Offset: 0x0000F7B8
		public bool IsEdit
		{
			get
			{
				return this._dataRow.HasVersion(DataRowVersion.Proposed);
			}
		}

		/// <summary>Indicates whether a <see cref="T:System.Data.DataRowView" /> is new.</summary>
		/// <returns>true if the row is new; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000115CC File Offset: 0x0000F7CC
		public bool IsNew
		{
			get
			{
				return this.Row == this.DataView._lastAdded;
			}
		}

		/// <summary>Gets or sets a value in a specified column.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="property">String that contains the specified column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000061 RID: 97
		public object this[string property]
		{
			get
			{
				DataColumn dataColumn = this._dataView.Table.Columns[property];
				if (dataColumn == null)
				{
					throw new ArgumentException(property + " is neither a DataColumn nor a DataRelation for table " + this._dataView.Table.TableName);
				}
				return this._dataRow[dataColumn, this.GetActualRowVersion()];
			}
			set
			{
				this.CheckAllowEdit();
				DataColumn dataColumn = this._dataView.Table.Columns[property];
				if (dataColumn == null)
				{
					throw new ArgumentException(property + " is neither a DataColumn nor a DataRelation for table " + this._dataView.Table.TableName);
				}
				this._dataRow[dataColumn] = value;
			}
		}

		/// <summary>Gets or sets a value in a specified column.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="ndx">The specified column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000062 RID: 98
		public object this[int ndx]
		{
			get
			{
				DataColumn dataColumn = this._dataView.Table.Columns[ndx];
				if (dataColumn == null)
				{
					throw new ArgumentException(ndx + " is neither a DataColumn nor a DataRelation for table " + this._dataView.Table.TableName);
				}
				return this._dataRow[dataColumn, this.GetActualRowVersion()];
			}
			set
			{
				this.CheckAllowEdit();
				DataColumn dataColumn = this._dataView.Table.Columns[ndx];
				if (dataColumn == null)
				{
					throw new ArgumentException(ndx + " is neither a DataColumn nor a DataRelation for table " + this._dataView.Table.TableName);
				}
				this._dataRow[dataColumn] = value;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0001176C File Offset: 0x0000F96C
		private DataRowVersion GetActualRowVersion()
		{
			DataViewRowState rowStateFilter = this._dataView.RowStateFilter;
			switch (rowStateFilter)
			{
			case DataViewRowState.Unchanged:
				break;
			default:
				if (rowStateFilter != DataViewRowState.Deleted)
				{
					if (rowStateFilter == DataViewRowState.ModifiedCurrent)
					{
						return DataRowVersion.Current;
					}
					if (rowStateFilter != DataViewRowState.ModifiedOriginal && rowStateFilter != DataViewRowState.OriginalRows)
					{
						return DataRowVersion.Default;
					}
				}
				break;
			case DataViewRowState.Added:
				return DataRowVersion.Proposed;
			}
			return DataRowVersion.Original;
		}

		/// <summary>Gets the <see cref="T:System.Data.DataRow" /> being viewed.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> being viewed by the <see cref="T:System.Data.DataRowView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000117D4 File Offset: 0x0000F9D4
		public DataRow Row
		{
			get
			{
				return this._dataRow;
			}
		}

		/// <summary>Gets the current version description of the <see cref="T:System.Data.DataRow" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. Possible values for the <see cref="P:System.Data.DataRowView.RowVersion" /> property are Default, Original, Current, and Proposed.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600029D RID: 669 RVA: 0x000117DC File Offset: 0x0000F9DC
		public DataRowVersion RowVersion
		{
			get
			{
				DataRowVersion dataRowVersion = this.DataView.GetRowVersion(this._index);
				if (dataRowVersion != DataRowVersion.Original)
				{
					dataRowVersion = DataRowVersion.Current;
				}
				return dataRowVersion;
			}
		}

		/// <summary>Returns the hash code of the <see cref="T:System.Data.DataRow" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code 1, which represents Boolean true if the value of this instance is nonzero; otherwise the integer zero, which represents Boolean false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600029E RID: 670 RVA: 0x00011810 File Offset: 0x0000FA10
		public override int GetHashCode()
		{
			return this._dataRow.GetHashCode();
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00011820 File Offset: 0x0000FA20
		internal int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00011828 File Offset: 0x0000FA28
		private void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				PropertyChangedEventArgs propertyChangedEventArgs = new PropertyChangedEventArgs(propertyName);
				this.PropertyChanged(this, propertyChangedEventArgs);
			}
		}

		// Token: 0x04000112 RID: 274
		private DataView _dataView;

		// Token: 0x04000113 RID: 275
		private DataRow _dataRow;

		// Token: 0x04000114 RID: 276
		private int _index = -1;
	}
}
