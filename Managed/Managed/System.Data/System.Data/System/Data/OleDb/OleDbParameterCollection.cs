using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents a collection of parameters relevant to an <see cref="T:System.Data.OleDb.OleDbCommand" /> as well as their respective mappings to columns in a <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F8 RID: 248
	[ListBindable(false)]
	[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class OleDbParameterCollection : DbParameterCollection, IList, IDataParameterCollection, IEnumerable, ICollection
	{
		// Token: 0x06000BFB RID: 3067 RVA: 0x00033D7C File Offset: 0x00031F7C
		internal OleDbParameterCollection()
		{
		}

		/// <summary>Returns an integer that contains the number of elements in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />. Read-only. </summary>
		/// <returns>The number of elements in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> as an integer.</returns>
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00033D90 File Offset: 0x00031F90
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbParameter" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbParameter" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the parameter to retrieve. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index specified does not exist. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000244 RID: 580
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public OleDbParameter this[int index]
		{
			get
			{
				return (OleDbParameter)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbParameter" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbParameter" /> with the specified name.</returns>
		/// <param name="parameterName">The name of the parameter to retrieve. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified does not exist. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000245 RID: 581
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public OleDbParameter this[string parameterName]
		{
			get
			{
				foreach (object obj in this.list)
				{
					OleDbParameter oleDbParameter = (OleDbParameter)obj;
					if (oleDbParameter.ParameterName.Equals(parameterName))
					{
						return oleDbParameter;
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

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> has a fixed size. Read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> has a fixed size; otherwise false.</returns>
		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00033E90 File Offset: 0x00032090
		public override bool IsFixedSize
		{
			get
			{
				return this.list.IsFixedSize;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> is read only; otherwise false.</returns>
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00033EA0 File Offset: 0x000320A0
		public override bool IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> is synchronized. Read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> is synchronized; otherwise false.</returns>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00033EB0 File Offset: 0x000320B0
		public override bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />. Read-only.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</returns>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00033EC0 File Offset: 0x000320C0
		public override object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x00033ED0 File Offset: 0x000320D0
		internal IntPtr GdaParameterList
		{
			[MonoTODO]
			get
			{
				return libgda.gda_parameter_list_new();
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> object to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object in the collection.</returns>
		/// <param name="value">A <see cref="T:System.Object" />.</param>
		// Token: 0x06000C06 RID: 3078 RVA: 0x00033EE4 File Offset: 0x000320E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			if (!(value is OleDbParameter))
			{
				throw new InvalidCastException("The parameter was not an OleDbParameter.");
			}
			this.Add((OleDbParameter)value);
			return this.IndexOf(value);
		}

		/// <summary>Adds the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <param name="value">The <see cref="T:System.Data.OleDb.OleDbParameter" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.OleDb.OleDbParameter" /> specified in the <paramref name="value" /> parameter is already added to this or another <see cref="T:System.Data.OleDb.OleDbParameterCollection" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C07 RID: 3079 RVA: 0x00033F1C File Offset: 0x0003211C
		public OleDbParameter Add(OleDbParameter value)
		{
			if (value.Container != null)
			{
				throw new ArgumentException("The OleDbParameter specified in the value parameter is already added to this or another OleDbParameterCollection.");
			}
			value.Container = this;
			this.list.Add(value);
			return value;
		}

		/// <summary>Adds an <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> given the parameter name and value.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="value">The <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> of the <see cref="T:System.Data.OleDb.OleDbParameter" /> to add to the collection. </param>
		/// <exception cref="T:System.InvalidCastException">The <paramref name="value" /> parameter is not an <see cref="T:System.Data.OleDb.OleDbParameter" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C08 RID: 3080 RVA: 0x00033F4C File Offset: 0x0003214C
		[Obsolete("OleDbParameterCollection.Add(string, value) is now obsolete. Use OleDbParameterCollection.AddWithValue(string, object) instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public OleDbParameter Add(string parameterName, object value)
		{
			return this.Add(new OleDbParameter(parameterName, value));
		}

		/// <summary>Adds a value to the end of the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="value">The value to be added.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C09 RID: 3081 RVA: 0x00033F5C File Offset: 0x0003215C
		public OleDbParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new OleDbParameter(parameterName, value));
		}

		/// <summary>Adds an <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />, given the parameter name and data type.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="oleDbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C0A RID: 3082 RVA: 0x00033F6C File Offset: 0x0003216C
		public OleDbParameter Add(string parameterName, OleDbType oleDbType)
		{
			return this.Add(new OleDbParameter(parameterName, oleDbType));
		}

		/// <summary>Adds an <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> given the parameter name, data type, and column length.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="oleDbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the column. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C0B RID: 3083 RVA: 0x00033F7C File Offset: 0x0003217C
		public OleDbParameter Add(string parameterName, OleDbType oleDbType, int size)
		{
			return this.Add(new OleDbParameter(parameterName, oleDbType, size));
		}

		/// <summary>Adds an <see cref="T:System.Data.OleDb.OleDbParameter" /> to the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> given the parameter name, data type, column length, and source column name.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="oleDbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the column. </param>
		/// <param name="sourceColumn">The name of the source column. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C0C RID: 3084 RVA: 0x00033F8C File Offset: 0x0003218C
		public OleDbParameter Add(string parameterName, OleDbType oleDbType, int size, string sourceColumn)
		{
			return this.Add(new OleDbParameter(parameterName, oleDbType, size, sourceColumn));
		}

		/// <summary>Adds an array of values to the end of the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Array" /> values to add.</param>
		// Token: 0x06000C0D RID: 3085 RVA: 0x00033FA0 File Offset: 0x000321A0
		public override void AddRange(Array values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			foreach (object obj in values)
			{
				this.Add(obj);
			}
		}

		/// <summary>Adds an array of <see cref="T:System.Data.OleDb.OleDbParameter" /> values to the end of the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Data.OleDbParameter" /> values to add.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C0E RID: 3086 RVA: 0x00034018 File Offset: 0x00032218
		public void AddRange(OleDbParameter[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			foreach (OleDbParameter oleDbParameter in values)
			{
				this.Add(oleDbParameter);
			}
		}

		/// <summary>Removes all <see cref="T:System.Data.OleDb.OleDbParameter" /> objects from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		// Token: 0x06000C0F RID: 3087 RVA: 0x00034058 File Offset: 0x00032258
		public override void Clear()
		{
			foreach (object obj in this.list)
			{
				OleDbParameter oleDbParameter = (OleDbParameter)obj;
				oleDbParameter.Container = null;
			}
			this.list.Clear();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is in this <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> contains <paramref name="value" />; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> value.</param>
		// Token: 0x06000C10 RID: 3088 RVA: 0x000340D4 File Offset: 0x000322D4
		public override bool Contains(object value)
		{
			if (!(value is OleDbParameter))
			{
				throw new InvalidCastException("The parameter was not an OleDbParameter.");
			}
			return this.Contains(((OleDbParameter)value).ParameterName);
		}

		/// <summary>Determines whether the specified <see cref="T:System.String" /> is in this <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> contains the value; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.String" /> value.</param>
		// Token: 0x06000C11 RID: 3089 RVA: 0x00034100 File Offset: 0x00032300
		public override bool Contains(string value)
		{
			foreach (object obj in this.list)
			{
				OleDbParameter oleDbParameter = (OleDbParameter)obj;
				if (oleDbParameter.ParameterName.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> is in this <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>true if the <see cref="P:System.Data.OleDb.OleDbParameter" /> is in the collection; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Data.OleDb.OleDbParameter" /> value.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C12 RID: 3090 RVA: 0x00034184 File Offset: 0x00032384
		public bool Contains(OleDbParameter value)
		{
			return this.IndexOf(value) != -1;
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Array" /> at which copying starts.</param>
		// Token: 0x06000C13 RID: 3091 RVA: 0x00034194 File Offset: 0x00032394
		public override void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> to the specified <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> starting at the specified destination index.</summary>
		/// <param name="array">The <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> that is the destination of the elements copied from the current <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at which copying starts.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000C14 RID: 3092 RVA: 0x000341A4 File Offset: 0x000323A4
		public void CopyTo(OleDbParameter[] array, int index)
		{
			this.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator" /> for the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</returns>
		// Token: 0x06000C15 RID: 3093 RVA: 0x000341B0 File Offset: 0x000323B0
		public override IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x000341C0 File Offset: 0x000323C0
		[MonoTODO]
		protected override DbParameter GetParameter(int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x000341C8 File Offset: 0x000323C8
		[MonoTODO]
		protected override DbParameter GetParameter(string parameterName)
		{
			throw new NotImplementedException();
		}

		/// <summary>The location of the specified <see cref="T:System.Object" /> within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> within the collection.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to find.</param>
		// Token: 0x06000C18 RID: 3096 RVA: 0x000341D0 File Offset: 0x000323D0
		public override int IndexOf(object value)
		{
			if (!(value is OleDbParameter))
			{
				throw new InvalidCastException("The parameter was not an OleDbParameter.");
			}
			return this.IndexOf(((OleDbParameter)value).ParameterName);
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> that is a <see cref="T:System.Data.OleDb.OleDbParameter" /> within the collection.</returns>
		/// <param name="value">The <see cref="T:System.Data.OleDb.OleDbParameter" /> object in the collection to find.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C19 RID: 3097 RVA: 0x000341FC File Offset: 0x000323FC
		public int IndexOf(OleDbParameter value)
		{
			return this.IndexOf(value);
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> with the specified name.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.OleDb.OleDbParameter" /> with the specified case-sensitive name.</returns>
		/// <param name="parameterName">The case-sensitive name of the <see cref="T:System.Data.OleDb.OleDbParameter" /> to find.</param>
		// Token: 0x06000C1A RID: 3098 RVA: 0x00034208 File Offset: 0x00032408
		public override int IndexOf(string parameterName)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].ParameterName.Equals(parameterName))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Inserts a <see cref="T:System.Object" /> into the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">A <see cref="T:System.Object" /> to be inserted in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		// Token: 0x06000C1B RID: 3099 RVA: 0x00034248 File Offset: 0x00032448
		public override void Insert(int index, object value)
		{
			this.list.Insert(index, value);
		}

		/// <summary>Inserts a <see cref="T:System.Data.OleDb.OleDbParameter" /> object into the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">An <see cref="T:System.Data.OleDb.OleDbParameter" /> object to be inserted in the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C1C RID: 3100 RVA: 0x00034258 File Offset: 0x00032458
		public void Insert(int index, OleDbParameter value)
		{
			this.Insert(index, value);
		}

		/// <summary>Removes the <see cref="T:System.Object" /> object from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> to be removed from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</param>
		// Token: 0x06000C1D RID: 3101 RVA: 0x00034264 File Offset: 0x00032464
		public override void Remove(object value)
		{
			((OleDbParameter)value).Container = null;
			this.list.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.Data.OleDb.OleDbParameter" /> from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <param name="value">An <see cref="T:System.Data.OleDb.OleDbParameter" /> object to remove from the collection.</param>
		/// <exception cref="T:System.InvalidCastException">The parameter is not a <see cref="T:System.Data.OleDb.OleDbParameter" />. </exception>
		/// <exception cref="T:System.SystemException">The parameter does not exist in the collection. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C1E RID: 3102 RVA: 0x00034280 File Offset: 0x00032480
		public void Remove(OleDbParameter value)
		{
			this.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.Data.OleDb.OleDbParameter" /> from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.OleDb.OleDbParameter" /> object to remove.</param>
		// Token: 0x06000C1F RID: 3103 RVA: 0x0003428C File Offset: 0x0003248C
		public override void RemoveAt(int index)
		{
			this[index].Container = null;
			this.list.RemoveAt(index);
		}

		/// <summary>Removes the <see cref="T:System.Data.OleDb.OleDbParameter" /> from the <see cref="T:System.Data.OleDb.OleDbParameterCollection" /> at the specified parameter name.</summary>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.OleDb.OleDbParameter" /> object to remove.</param>
		// Token: 0x06000C20 RID: 3104 RVA: 0x000342A8 File Offset: 0x000324A8
		public override void RemoveAt(string parameterName)
		{
			this.RemoveAt(this.IndexOf(parameterName));
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000342B8 File Offset: 0x000324B8
		[MonoTODO]
		protected override void SetParameter(int index, DbParameter value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000342C0 File Offset: 0x000324C0
		[MonoTODO]
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000475 RID: 1141
		private ArrayList list = new ArrayList();
	}
}
