using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Represents a collection of parameters relevant to an <see cref="T:System.Data.Odbc.OdbcCommand" /> and their respective mappings to columns in a <see cref="T:System.Data.DataSet" />. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000128 RID: 296
	[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class OdbcParameterCollection : DbParameterCollection
	{
		// Token: 0x060010A7 RID: 4263 RVA: 0x00041A28 File Offset: 0x0003FC28
		internal OdbcParameterCollection()
		{
		}

		/// <summary>Returns an Integer that contains the number of elements in the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />. Read-only.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> as an Integer.</returns>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x00041A44 File Offset: 0x0003FC44
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcParameter" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.Odbc.OdbcParameter" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the parameter to retrieve. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index specified does not exist. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002D7 RID: 727
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public OdbcParameter this[int index]
		{
			get
			{
				return (OdbcParameter)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcParameter" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.Odbc.OdbcParameter" /> with the specified name.</returns>
		/// <param name="parameterName">The name of the parameter to retrieve. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified does not exist. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002D8 RID: 728
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public OdbcParameter this[string parameterName]
		{
			get
			{
				foreach (object obj in this.list)
				{
					OdbcParameter odbcParameter = (OdbcParameter)obj;
					if (odbcParameter.ParameterName.Equals(parameterName))
					{
						return odbcParameter;
					}
				}
				throw new IndexOutOfRangeException("The specified name does not exist: " + parameterName);
			}
			set
			{
				if (!this.Contains(parameterName))
				{
					throw new IndexOutOfRangeException("The specified name does not exist: " + parameterName);
				}
				this[this.IndexOf(parameterName)] = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> has a fixed size. Read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> has a fixed size, otherwise false.</returns>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x00041B44 File Offset: 0x0003FD44
		public override bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> is read only, otherwise, false.</returns>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x00041B48 File Offset: 0x0003FD48
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> is synchronized. Read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> is synchronized; otherwise, false.</returns>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x00041B4C File Offset: 0x0003FD4C
		public override bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />. Read-only.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</returns>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x00041B5C File Offset: 0x0003FD5C
		public override object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> object to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.Odbc.OdbcParameter" /> object in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Object" />.</param>
		// Token: 0x060010B1 RID: 4273 RVA: 0x00041B6C File Offset: 0x0003FD6C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			if (!(value is OdbcParameter))
			{
				throw new InvalidCastException("The parameter was not an OdbcParameter.");
			}
			this.Add((OdbcParameter)value);
			return this.IndexOf(value);
		}

		/// <summary>Adds the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		/// <param name="value">The <see cref="T:System.Data.Odbc.OdbcParameter" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.Odbc.OdbcParameter" /> specified in the <paramref name="value" /> parameter is already added to this or another <see cref="T:System.Data.Odbc.OdbcParameterCollection" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010B2 RID: 4274 RVA: 0x00041BA4 File Offset: 0x0003FDA4
		public OdbcParameter Add(OdbcParameter value)
		{
			if (value.Container != null)
			{
				throw new ArgumentException("The OdbcParameter specified in the value parameter is already added to this or another OdbcParameterCollection.");
			}
			if (value.ParameterName == null || value.ParameterName.Length == 0)
			{
				value.ParameterName = "Parameter" + this.nullParamCount;
				this.nullParamCount++;
			}
			value.Container = this;
			this.list.Add(value);
			return value;
		}

		/// <summary>Adds an <see cref="T:System.Data.Odbc.OdbcParameter" /> to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> given the parameter name and value.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="value">The <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> of the <see cref="T:System.Data.Odbc.OdbcParameter" /> to add to the collection. </param>
		/// <exception cref="T:System.InvalidCastException">The <paramref name="value" /> parameter is not an <see cref="T:System.Data.Odbc.OdbcParameter" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010B3 RID: 4275 RVA: 0x00041C20 File Offset: 0x0003FE20
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Add(String parameterName, Object value) has been deprecated.  Use AddWithValue(String parameterName, Object value).")]
		public OdbcParameter Add(string parameterName, object value)
		{
			return this.Add(new OdbcParameter(parameterName, value));
		}

		/// <summary>Adds an <see cref="T:System.Data.Odbc.OdbcParameter" /> to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />, given the parameter name and data type.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="odbcType">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010B4 RID: 4276 RVA: 0x00041C30 File Offset: 0x0003FE30
		public OdbcParameter Add(string parameterName, OdbcType odbcType)
		{
			return this.Add(new OdbcParameter(parameterName, odbcType));
		}

		/// <summary>Adds an <see cref="T:System.Data.Odbc.OdbcParameter" /> to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />, given the parameter name, data type, and column length.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="odbcType">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <param name="size">The length of the column. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010B5 RID: 4277 RVA: 0x00041C40 File Offset: 0x0003FE40
		public OdbcParameter Add(string parameterName, OdbcType odbcType, int size)
		{
			return this.Add(new OdbcParameter(parameterName, odbcType, size));
		}

		/// <summary>Adds an <see cref="T:System.Data.Odbc.OdbcParameter" /> to the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> given the parameter name, data type, column length, and source column name.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="odbcType">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <param name="size">The length of the column. </param>
		/// <param name="sourceColumn">The name of the source column. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010B6 RID: 4278 RVA: 0x00041C50 File Offset: 0x0003FE50
		public OdbcParameter Add(string parameterName, OdbcType odbcType, int size, string sourceColumn)
		{
			return this.Add(new OdbcParameter(parameterName, odbcType, size, sourceColumn));
		}

		/// <summary>Removes all <see cref="T:System.Data.Odbc.OdbcParameter" /> objects from the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		// Token: 0x060010B7 RID: 4279 RVA: 0x00041C64 File Offset: 0x0003FE64
		public override void Clear()
		{
			foreach (object obj in this.list)
			{
				OdbcParameter odbcParameter = (OdbcParameter)obj;
				odbcParameter.Container = null;
			}
			this.list.Clear();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is in this <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> contains the value otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> value.</param>
		// Token: 0x060010B8 RID: 4280 RVA: 0x00041CE0 File Offset: 0x0003FEE0
		public override bool Contains(object value)
		{
			if (value == null)
			{
				return false;
			}
			if (!(value is OdbcParameter))
			{
				throw new InvalidCastException("The parameter was not an OdbcParameter.");
			}
			return this.Contains(((OdbcParameter)value).ParameterName);
		}

		/// <summary>Gets a value indicating whether an <see cref="T:System.Data.Odbc.OdbcParameter" /> object with the specified parameter name exists in the collection.</summary>
		/// <returns>true if the collection contains the parameter; otherwise, false.</returns>
		/// <param name="value">The name of the <see cref="T:System.Data.Odbc.OdbcParameter" /> object to find. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010B9 RID: 4281 RVA: 0x00041D14 File Offset: 0x0003FF14
		public override bool Contains(string value)
		{
			if (value == null || value.Length == 0)
			{
				return false;
			}
			string text = value.ToUpper();
			foreach (object obj in this)
			{
				OdbcParameter odbcParameter = (OdbcParameter)obj;
				if (odbcParameter.ParameterName.ToUpper().Equals(text))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Array" /> at which copying starts.</param>
		// Token: 0x060010BA RID: 4282 RVA: 0x00041DB4 File Offset: 0x0003FFB4
		public override void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator" /> for the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</returns>
		// Token: 0x060010BB RID: 4283 RVA: 0x00041DC4 File Offset: 0x0003FFC4
		public override IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Object" /> within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Odbc.OdbcParameter" /> within the collection.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to find.</param>
		// Token: 0x060010BC RID: 4284 RVA: 0x00041DD4 File Offset: 0x0003FFD4
		public override int IndexOf(object value)
		{
			if (value == null)
			{
				return -1;
			}
			if (!(value is OdbcParameter))
			{
				throw new InvalidCastException("The parameter was not an OdbcParameter.");
			}
			return this.list.IndexOf(value);
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> with the specified name.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> with the specified case-sensitive name.</returns>
		/// <param name="parameterName">The case-sensitive name of the <see cref="T:System.Data.Odbc.OdbcParameter" /> to find.</param>
		// Token: 0x060010BD RID: 4285 RVA: 0x00041E0C File Offset: 0x0004000C
		public override int IndexOf(string parameterName)
		{
			if (parameterName == null || parameterName.Length == 0)
			{
				return -1;
			}
			string text = parameterName.ToUpper();
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].ParameterName.ToUpper().Equals(text))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Inserts a <see cref="T:System.Object" /> into the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which the object should be inserted.</param>
		/// <param name="value">A <see cref="T:System.Object" /> to be inserted in the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</param>
		// Token: 0x060010BE RID: 4286 RVA: 0x00041E6C File Offset: 0x0004006C
		public override void Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is OdbcParameter))
			{
				throw new InvalidCastException("The parameter was not an OdbcParameter.");
			}
			this.Insert(index, (OdbcParameter)value);
		}

		/// <summary>Removes the <see cref="T:System.Object" /> object from the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <param name="value">A <see cref="T:System.Object" /> to be removed from the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</param>
		// Token: 0x060010BF RID: 4287 RVA: 0x00041EB0 File Offset: 0x000400B0
		public override void Remove(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is OdbcParameter))
			{
				throw new InvalidCastException("The parameter was not an OdbcParameter.");
			}
			this.Remove((OdbcParameter)value);
		}

		/// <summary>Removes the <see cref="T:System.Data.Odbc.OdbcParameter" /> from the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Odbc.OdbcParameter" /> object to remove.</param>
		// Token: 0x060010C0 RID: 4288 RVA: 0x00041EE8 File Offset: 0x000400E8
		public override void RemoveAt(int index)
		{
			if (index >= this.list.Count || index < 0)
			{
				throw new IndexOutOfRangeException(string.Format("Invalid index {0} for this OdbcParameterCollection with count = {1}", index, this.list.Count));
			}
			this[index].Container = null;
			this.list.RemoveAt(index);
		}

		/// <summary>Removes the <see cref="T:System.Data.Odbc.OdbcParameter" /> from the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> with the specified parameter name.</summary>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.Odbc.OdbcParameter" /> object to remove.</param>
		// Token: 0x060010C1 RID: 4289 RVA: 0x00041F4C File Offset: 0x0004014C
		public override void RemoveAt(string parameterName)
		{
			this.RemoveAt(this.IndexOf(parameterName));
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00041F5C File Offset: 0x0004015C
		protected override DbParameter GetParameter(string name)
		{
			return this[name];
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00041F68 File Offset: 0x00040168
		protected override DbParameter GetParameter(int index)
		{
			return this[index];
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00041F74 File Offset: 0x00040174
		protected override void SetParameter(string name, DbParameter value)
		{
			this[name] = (OdbcParameter)value;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00041F84 File Offset: 0x00040184
		protected override void SetParameter(int index, DbParameter value)
		{
			this[index] = (OdbcParameter)value;
		}

		/// <summary>Adds an array of values to the end of the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Array" /> values to add.</param>
		// Token: 0x060010C6 RID: 4294 RVA: 0x00041F94 File Offset: 0x00040194
		public override void AddRange(Array values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			using (IEnumerator enumerator = values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if ((OdbcParameter)enumerator.Current == null)
					{
						throw new ArgumentNullException("values", "The OdbcParameterCollection only accepts non-null OdbcParameter type objects");
					}
				}
			}
			foreach (object obj in values)
			{
				OdbcParameter odbcParameter = (OdbcParameter)obj;
				this.Add(odbcParameter);
			}
		}

		/// <summary>Adds an array of <see cref="T:System.Data.Odbc.OdbcParameter" /> values to the end of the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <param name="values">An array of <see cref="T:System.Data.Odbc.OdbcParameter" /> objects to add to the collection.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010C7 RID: 4295 RVA: 0x00042080 File Offset: 0x00040280
		public void AddRange(OdbcParameter[] values)
		{
			this.AddRange(values);
		}

		/// <summary>Inserts a <see cref="T:System.Data.Odbc.OdbcParameter" /> object into the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which the object should be inserted.</param>
		/// <param name="value">A <see cref="T:System.Data.Odbc.OdbcParameter" /> object to be inserted in the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010C8 RID: 4296 RVA: 0x0004208C File Offset: 0x0004028C
		public void Insert(int index, OdbcParameter value)
		{
			if (index > this.list.Count || index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "The index must be non-negative and less than or equal to size of the collection");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Container != null)
			{
				throw new ArgumentException("The OdbcParameter is already contained by another collection");
			}
			if (string.IsNullOrEmpty(value.ParameterName))
			{
				value.ParameterName = "Parameter" + this.nullParamCount;
				this.nullParamCount++;
			}
			value.Container = this;
			this.list.Insert(index, value);
		}

		/// <summary>Adds a value to the end of the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />. </summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="value">The value to be added.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010C9 RID: 4297 RVA: 0x00042138 File Offset: 0x00040338
		public OdbcParameter AddWithValue(string parameterName, object value)
		{
			if (value == null)
			{
				return this.Add(new OdbcParameter(parameterName, OdbcType.NVarChar));
			}
			return this.Add(new OdbcParameter(parameterName, value));
		}

		/// <summary>Removes the <see cref="T:System.Data.Odbc.OdbcParameter" /> from the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <param name="value">A <see cref="T:System.Data.Odbc.OdbcParameter" /> object to remove from the collection.</param>
		/// <exception cref="T:System.InvalidCastException">The parameter is not a <see cref="T:System.Data.Odbc.OdbcParameter" />.</exception>
		/// <exception cref="T:System.SystemException">The parameter does not exist in the collection.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010CA RID: 4298 RVA: 0x00042168 File Offset: 0x00040368
		public void Remove(OdbcParameter value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Container != this)
			{
				throw new ArgumentException("values", "Attempted to remove an OdbcParameter that is not contained in this OdbcParameterCollection");
			}
			value.Container = null;
			this.list.Remove(value);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> is in this <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.Odbc.OdbcParameter" /> is in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Data.Odbc.OdbcParameter" /> value.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010CB RID: 4299 RVA: 0x000421B8 File Offset: 0x000403B8
		public bool Contains(OdbcParameter value)
		{
			return value != null && value.Container == this && this.Contains(value.ParameterName);
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.Odbc.OdbcParameter" /> within the collection.</returns>
		/// <param name="value">The <see cref="T:System.Data.Odbc.OdbcParameter" /> object in the collection to find.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060010CC RID: 4300 RVA: 0x000421E8 File Offset: 0x000403E8
		public int IndexOf(OdbcParameter value)
		{
			if (value == null)
			{
				return -1;
			}
			return this.IndexOf(value);
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> to the specified <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> starting at the specified destination index.</summary>
		/// <param name="array">The <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> that is the destination of the elements copied from the current <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Data.Odbc.OdbcParameterCollection" /> at which copying starts.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010CD RID: 4301 RVA: 0x000421FC File Offset: 0x000403FC
		public void CopyTo(OdbcParameter[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x04000584 RID: 1412
		private readonly ArrayList list = new ArrayList();

		// Token: 0x04000585 RID: 1413
		private int nullParamCount = 1;
	}
}
