using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Contains a read-only collection of <see cref="T:System.Data.DataViewSetting" /> objects for each <see cref="T:System.Data.DataTable" /> in a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000040 RID: 64
	[Editor("Microsoft.VSDesigner.Data.Design.DataViewSettingsCollectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class DataViewSettingCollection : IEnumerable, ICollection
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x0001D4F0 File Offset: 0x0001B6F0
		internal DataViewSettingCollection(DataViewManager manager)
		{
			this.settingList = new ArrayList();
			if (manager.DataSet != null)
			{
				foreach (object obj in manager.DataSet.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					this.settingList.Add(new DataViewSetting(manager, dataTable));
				}
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Data.DataViewSetting" /> objects in the <see cref="T:System.Data.DataViewSettingCollection" />.</summary>
		/// <returns>The number of <see cref="T:System.Data.DataViewSetting" /> objects in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001D58C File Offset: 0x0001B78C
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this.settingList.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataViewSettingCollection" /> is read-only.</summary>
		/// <returns>Returns true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001D59C File Offset: 0x0001B79C
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.settingList.IsReadOnly;
			}
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Data.DataViewSettingCollection" /> is synchronized (thread-safe).</summary>
		/// <returns>This property is always false, unless overridden by a derived class.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001D5AC File Offset: 0x0001B7AC
		[Browsable(false)]
		public bool IsSynchronized
		{
			get
			{
				return this.settingList.IsSynchronized;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> objects of the specified <see cref="T:System.Data.DataTable" /> from the collection. </summary>
		/// <returns>A <see cref="T:System.Data.DataViewSetting" />.</returns>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000E9 RID: 233
		public virtual DataViewSetting this[DataTable table]
		{
			get
			{
				for (int i = 0; i < this.settingList.Count; i++)
				{
					DataViewSetting dataViewSetting = (DataViewSetting)this.settingList[i];
					if (dataViewSetting.Table == table)
					{
						return dataViewSetting;
					}
				}
				return null;
			}
			set
			{
				this[table] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> of the <see cref="T:System.Data.DataTable" /> specified by its name.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewSetting" />.</returns>
		/// <param name="tableName">The name of the <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000EA RID: 234
		public virtual DataViewSetting this[string tableName]
		{
			get
			{
				for (int i = 0; i < this.settingList.Count; i++)
				{
					DataViewSetting dataViewSetting = (DataViewSetting)this.settingList[i];
					if (dataViewSetting.Table.TableName == tableName)
					{
						return dataViewSetting;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataViewSetting" /> objects of the <see cref="T:System.Data.DataTable" /> specified by its index.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewSetting" />.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000EB RID: 235
		public virtual DataViewSetting this[int index]
		{
			get
			{
				return (DataViewSetting)this.settingList[index];
			}
			set
			{
				this.settingList[index] = value;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.DataViewSettingCollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.DataViewSettingCollection" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001D68C File Offset: 0x0001B88C
		[Browsable(false)]
		public object SyncRoot
		{
			get
			{
				return this.settingList.SyncRoot;
			}
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to start inserting. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600052B RID: 1323 RVA: 0x0001D69C File Offset: 0x0001B89C
		public void CopyTo(Array ar, int index)
		{
			this.settingList.CopyTo(ar, index);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
		/// <param name="ar">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to start inserting. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600052C RID: 1324 RVA: 0x0001D6AC File Offset: 0x0001B8AC
		public void CopyTo(DataViewSetting[] ar, int index)
		{
			this.settingList.CopyTo(ar, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600052D RID: 1325 RVA: 0x0001D6BC File Offset: 0x0001B8BC
		public IEnumerator GetEnumerator()
		{
			return this.settingList.GetEnumerator();
		}

		// Token: 0x0400019F RID: 415
		private readonly ArrayList settingList;
	}
}
