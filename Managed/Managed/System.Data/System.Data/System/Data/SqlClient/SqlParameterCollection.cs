using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using Mono.Data.Tds;

namespace System.Data.SqlClient
{
	/// <summary>Represents a collection of parameters associated with a <see cref="T:System.Data.SqlClient.SqlCommand" /> and their respective mappings to columns in a <see cref="T:System.Data.DataSet" />. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000172 RID: 370
	[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class SqlParameterCollection : DbParameterCollection, IList, IDataParameterCollection, IEnumerable, ICollection
	{
		// Token: 0x060013E6 RID: 5094 RVA: 0x00053CE4 File Offset: 0x00051EE4
		internal SqlParameterCollection(SqlCommand command)
		{
			this.command = command;
			this.metaParameters = new TdsMetaParameterCollection();
		}

		/// <summary>Returns an Integer that contains the number of elements in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />. Read-only. </summary>
		/// <returns>The number of elements in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> as an Integer.</returns>
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00053D0C File Offset: 0x00051F0C
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> has a fixed size. </summary>
		/// <returns>Returns true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> has a fixed size; otherwise false.</returns>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00053D1C File Offset: 0x00051F1C
		public override bool IsFixedSize
		{
			get
			{
				return this.list.IsFixedSize;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> is read-only. </summary>
		/// <returns>Returns true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> is read only; otherwise false.</returns>
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x00053D2C File Offset: 0x00051F2C
		public override bool IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> is synchronized.</summary>
		/// <returns>Returns true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> is synchronized; otherwise false.</returns>
		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00053D3C File Offset: 0x00051F3C
		public override bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />. </summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</returns>
		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x00053D4C File Offset: 0x00051F4C
		public override object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlParameter" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlParameter" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the parameter to retrieve. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified index does not exist. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003BB RID: 955
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SqlParameter this[int index]
		{
			get
			{
				if (index < 0 || index >= this.list.Count)
				{
					throw new IndexOutOfRangeException("The specified index is out of range.");
				}
				return (SqlParameter)this.list[index];
			}
			set
			{
				if (index < 0 || index >= this.list.Count)
				{
					throw new IndexOutOfRangeException("The specified index is out of range.");
				}
				this.list[index] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlParameter" /> with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlParameter" /> with the specified name.</returns>
		/// <param name="parameterName">The name of the parameter to retrieve. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified <paramref name="parameterName" /> is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003BC RID: 956
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SqlParameter this[string parameterName]
		{
			get
			{
				foreach (object obj in this.list)
				{
					SqlParameter sqlParameter = (SqlParameter)obj;
					if (sqlParameter.ParameterName.Equals(parameterName))
					{
						return sqlParameter;
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

		// Token: 0x060013F0 RID: 5104 RVA: 0x00053EAC File Offset: 0x000520AC
		protected override DbParameter GetParameter(int index)
		{
			return this[index];
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00053EB8 File Offset: 0x000520B8
		protected override DbParameter GetParameter(string parameterName)
		{
			return this[parameterName];
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00053EC4 File Offset: 0x000520C4
		protected override void SetParameter(int index, DbParameter value)
		{
			this[index] = (SqlParameter)value;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00053ED4 File Offset: 0x000520D4
		protected override void SetParameter(string parameterName, DbParameter value)
		{
			this[parameterName] = (SqlParameter)value;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00053EE4 File Offset: 0x000520E4
		internal TdsMetaParameterCollection MetaParameters
		{
			get
			{
				return this.metaParameters;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> object to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>The index of the new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <param name="value">An <see cref="T:System.Object" />.</param>
		// Token: 0x060013F5 RID: 5109 RVA: 0x00053EEC File Offset: 0x000520EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			if (!(value is SqlParameter))
			{
				throw new InvalidCastException("The parameter was not an SqlParameter.");
			}
			this.Add((SqlParameter)value);
			return this.IndexOf(value);
		}

		/// <summary>Adds the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> object to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlClient.SqlParameter" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.SqlClient.SqlParameter" /> specified in the <paramref name="value" /> parameter is already added to this or another <see cref="T:System.Data.SqlClient.SqlParameterCollection" />. </exception>
		/// <exception cref="T:System.InvalidCastException">The parameter passed was not a <see cref="T:System.Data.SqlClient.SqlParameter" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060013F6 RID: 5110 RVA: 0x00053F24 File Offset: 0x00052124
		public SqlParameter Add(SqlParameter value)
		{
			if (value.Container != null)
			{
				throw new ArgumentException("The SqlParameter specified in the value parameter is already added to this or another SqlParameterCollection.");
			}
			value.Container = this;
			this.list.Add(value);
			this.metaParameters.Add(value.MetaParameter);
			return value;
		}

		/// <summary>Adds the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> object to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.Use caution when you are using this overload of the SqlParameterCollection.Add method to specify integer parameter values. Because this overload takes a <paramref name="value" /> of type <see cref="T:System.Object" />, you must convert the integral value to an <see cref="T:System.Object" /> type when the value is zero, as the following C# example demonstrates. Copy Codeparameters.Add("@pname", Convert.ToInt32(0));If you do not perform this conversion, the compiler assumes that you are trying to call the SqlParameterCollection.Add (string, SqlDbType) overload.</returns>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.SqlClient.SqlParameter" /> to add to the collection.</param>
		/// <param name="value">A <see cref="T:System.Object" />.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.SqlClient.SqlParameter" /> specified in the <paramref name="value" /> parameter is already added to this or another <see cref="T:System.Data.SqlClient.SqlParameterCollection" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060013F7 RID: 5111 RVA: 0x00053F70 File Offset: 0x00052170
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Do not call this method.")]
		public SqlParameter Add(string parameterName, object value)
		{
			return this.Add(new SqlParameter(parameterName, value));
		}

		/// <summary>Adds a value to the end of the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter.</param>
		/// <param name="value">The value to be added.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060013F8 RID: 5112 RVA: 0x00053F80 File Offset: 0x00052180
		public SqlParameter AddWithValue(string parameterName, object value)
		{
			return this.Add(new SqlParameter(parameterName, value));
		}

		/// <summary>Adds a <see cref="T:System.Data.SqlClient.SqlParameter" /> to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> given the parameter name and the data type.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="sqlDbType">One of the <see cref="T:System.Data.SqlDbType" /> values. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060013F9 RID: 5113 RVA: 0x00053F90 File Offset: 0x00052190
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType));
		}

		/// <summary>Adds a <see cref="T:System.Data.SqlClient.SqlParameter" /> to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />, given the specified parameter name, <see cref="T:System.Data.SqlDbType" /> and size.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="sqlDbType">The <see cref="T:System.Data.SqlDbType" /> of the <see cref="T:System.Data.SqlClient.SqlParameter" /> to add to the collection. </param>
		/// <param name="size">The size as an <see cref="T:System.Int32" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060013FA RID: 5114 RVA: 0x00053FA0 File Offset: 0x000521A0
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType, size));
		}

		/// <summary>Adds a <see cref="T:System.Data.SqlClient.SqlParameter" /> to the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> with the parameter name, the data type, and the column length.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="sqlDbType">One of the <see cref="T:System.Data.SqlDbType" /> values. </param>
		/// <param name="size">The column length.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060013FB RID: 5115 RVA: 0x00053FB0 File Offset: 0x000521B0
		public SqlParameter Add(string parameterName, SqlDbType sqlDbType, int size, string sourceColumn)
		{
			return this.Add(new SqlParameter(parameterName, sqlDbType, size, sourceColumn));
		}

		/// <summary>Removes all the <see cref="T:System.Data.SqlClient.SqlParameter" /> objects from the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		// Token: 0x060013FC RID: 5116 RVA: 0x00053FC4 File Offset: 0x000521C4
		public override void Clear()
		{
			this.metaParameters.Clear();
			foreach (object obj in this.list)
			{
				SqlParameter sqlParameter = (SqlParameter)obj;
				sqlParameter.Container = null;
			}
			this.list.Clear();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is in this <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> contains the value; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> value.</param>
		// Token: 0x060013FD RID: 5117 RVA: 0x0005404C File Offset: 0x0005224C
		public override bool Contains(object value)
		{
			if (!(value is SqlParameter))
			{
				throw new InvalidCastException("The parameter was not an SqlParameter.");
			}
			return this.Contains(((SqlParameter)value).ParameterName);
		}

		/// <summary>Determines whether the specified <see cref="T:System.String" /> is in this <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> contains the value; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.String" /> value.</param>
		// Token: 0x060013FE RID: 5118 RVA: 0x00054078 File Offset: 0x00052278
		public override bool Contains(string value)
		{
			foreach (object obj in this.list)
			{
				SqlParameter sqlParameter = (SqlParameter)obj;
				if (sqlParameter.ParameterName.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> is in this <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> contains the value; otherwise false.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlClient.SqlParameter" /> value.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060013FF RID: 5119 RVA: 0x000540FC File Offset: 0x000522FC
		public bool Contains(SqlParameter value)
		{
			return this.IndexOf(value) != -1;
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Array" /> at which copying starts.</param>
		// Token: 0x06001400 RID: 5120 RVA: 0x0005410C File Offset: 0x0005230C
		public override void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />. </returns>
		// Token: 0x06001401 RID: 5121 RVA: 0x0005411C File Offset: 0x0005231C
		public override IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Object" /> within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.SqlClient.SqlParameter" /> within the collection. Returns -1 when the object does not exist in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</returns>
		/// <param name="value">The <see cref="T:System.Object" /> to find. </param>
		// Token: 0x06001402 RID: 5122 RVA: 0x0005412C File Offset: 0x0005232C
		public override int IndexOf(object value)
		{
			if (!(value is SqlParameter))
			{
				throw new InvalidCastException("The parameter was not an SqlParameter.");
			}
			return this.IndexOf(((SqlParameter)value).ParameterName);
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> with the specified name.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> with the specified case-sensitive name. Returns -1 when the object does not exist in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</returns>
		/// <param name="parameterName">The case-sensitive name of the <see cref="T:System.Data.SqlClient.SqlParameter" /> to find.</param>
		// Token: 0x06001403 RID: 5123 RVA: 0x00054158 File Offset: 0x00052358
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

		/// <summary>Gets the location of the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> that is a <see cref="T:System.Data.SqlClient.SqlParameter" /> within the collection. Returns -1 when the object does not exist in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlClient.SqlParameter" /> to find. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001404 RID: 5124 RVA: 0x00054198 File Offset: 0x00052398
		public int IndexOf(SqlParameter value)
		{
			return this.list.IndexOf(value);
		}

		/// <summary>Inserts an <see cref="T:System.Object" /> into the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">An <see cref="T:System.Object" /> to be inserted in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</param>
		// Token: 0x06001405 RID: 5125 RVA: 0x000541A8 File Offset: 0x000523A8
		public override void Insert(int index, object value)
		{
			this.list.Insert(index, value);
		}

		/// <summary>Inserts a <see cref="T:System.Data.SqlClient.SqlParameter" /> object into the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index at which value should be inserted.</param>
		/// <param name="value">A <see cref="T:System.Data.SqlClient.SqlParameter" /> object to be inserted in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001406 RID: 5126 RVA: 0x000541B8 File Offset: 0x000523B8
		public void Insert(int index, SqlParameter value)
		{
			this.list.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> from the collection.</summary>
		/// <param name="value">The object to remove from the collection. </param>
		// Token: 0x06001407 RID: 5127 RVA: 0x000541C8 File Offset: 0x000523C8
		public override void Remove(object value)
		{
			((SqlParameter)value).Container = null;
			this.metaParameters.Remove(((SqlParameter)value).MetaParameter);
			this.list.Remove(value);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.SqlClient.SqlParameter" /> from the collection.</summary>
		/// <param name="value">A <see cref="T:System.Data.SqlClient.SqlParameter" /> object to remove from the collection. </param>
		/// <exception cref="T:System.InvalidCastException">The parameter is not a <see cref="T:System.Data.SqlClient.SqlParameter" />. </exception>
		/// <exception cref="T:System.SystemException">The parameter does not exist in the collection. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001408 RID: 5128 RVA: 0x00054204 File Offset: 0x00052404
		public void Remove(SqlParameter value)
		{
			value.Container = null;
			this.metaParameters.Remove(value.MetaParameter);
			this.list.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.Data.SqlClient.SqlParameter" /> from the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.SqlClient.SqlParameter" /> object to remove.</param>
		// Token: 0x06001409 RID: 5129 RVA: 0x00054238 File Offset: 0x00052438
		public override void RemoveAt(int index)
		{
			this[index].Container = null;
			this.metaParameters.RemoveAt(index);
			this.list.RemoveAt(index);
		}

		/// <summary>Removes the <see cref="T:System.Data.SqlClient.SqlParameter" /> from the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at the specified parameter name.</summary>
		/// <param name="parameterName">The name of the <see cref="T:System.Data.SqlClient.SqlParameter" /> to remove.</param>
		// Token: 0x0600140A RID: 5130 RVA: 0x0005426C File Offset: 0x0005246C
		public override void RemoveAt(string parameterName)
		{
			this.RemoveAt(this.IndexOf(parameterName));
		}

		/// <summary>Adds an array of values to the end of the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Array" /> values to add.</param>
		// Token: 0x0600140B RID: 5131 RVA: 0x0005427C File Offset: 0x0005247C
		public override void AddRange(Array values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("The argument passed was null");
			}
			foreach (object obj in values)
			{
				if (!(obj is SqlParameter))
				{
					throw new InvalidCastException("Element in the array parameter was not an SqlParameter.");
				}
				SqlParameter sqlParameter = (SqlParameter)obj;
				if (sqlParameter.Container != null)
				{
					throw new ArgumentException("An SqlParameter specified in the array is already added to this or another SqlParameterCollection.");
				}
				sqlParameter.Container = this;
				this.list.Add(sqlParameter);
				this.metaParameters.Add(sqlParameter.MetaParameter);
			}
		}

		/// <summary>Adds an array of <see cref="T:System.Data.SqlClient.SqlParameter" /> values to the end of the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <param name="values">The <see cref="T:System.Data.SqlClient.SqlParameter" /> values to add.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600140C RID: 5132 RVA: 0x00054344 File Offset: 0x00052544
		public void AddRange(SqlParameter[] values)
		{
			this.AddRange(values);
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> to the specified <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> starting at the specified destination index.</summary>
		/// <param name="array">The <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> that is the destination of the elements copied from the current <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</param>
		/// <param name="index">A 32-bit integer that represents the index in the <see cref="T:System.Data.SqlClient.SqlParameterCollection" /> at which copying starts.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600140D RID: 5133 RVA: 0x00054350 File Offset: 0x00052550
		public void CopyTo(SqlParameter[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		// Token: 0x04000802 RID: 2050
		private ArrayList list = new ArrayList();

		// Token: 0x04000803 RID: 2051
		private TdsMetaParameterCollection metaParameters;

		// Token: 0x04000804 RID: 2052
		private SqlCommand command;
	}
}
