using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading;

namespace System.Data.Common
{
	/// <summary>Provides a base class for strongly typed connection string builders.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000B2 RID: 178
	public class DbConnectionStringBuilder : IEnumerable, ICustomTypeDescriptor, ICollection, IDictionary
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> class.</summary>
		// Token: 0x06000837 RID: 2103 RVA: 0x00026F40 File Offset: 0x00025140
		public DbConnectionStringBuilder()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> class, optionally using ODBC rules for quoting values.</summary>
		/// <param name="useOdbcRules">true to use {} to delimit fields; false to use quotation marks.</param>
		// Token: 0x06000838 RID: 2104 RVA: 0x00026F4C File Offset: 0x0002514C
		public DbConnectionStringBuilder(bool useOdbcRules)
		{
			this.useOdbcRules = useOdbcRules;
			this._dictionary = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00026F70 File Offset: 0x00025170
		bool ICollection.IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00026F78 File Offset: 0x00025178
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000170 RID: 368
		object IDictionary.this[object keyword]
		{
			get
			{
				return this[(string)keyword];
			}
			set
			{
				this[(string)keyword] = value;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00026FA0 File Offset: 0x000251A0
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			KeyValuePair<string, object>[] array2 = array as KeyValuePair<string, object>[];
			if (array2 == null)
			{
				throw new ArgumentException("Target array type is not compatible with the type of items in the collection");
			}
			((ICollection<KeyValuePair<string, object>>)this._dictionary).CopyTo(array2, index);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00026FE4 File Offset: 0x000251E4
		void IDictionary.Add(object keyword, object value)
		{
			this.Add((string)keyword, value);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00026FF4 File Offset: 0x000251F4
		bool IDictionary.Contains(object keyword)
		{
			return this.ContainsKey((string)keyword);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00027004 File Offset: 0x00025204
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00027018 File Offset: 0x00025218
		void IDictionary.Remove(object keyword)
		{
			this.Remove((string)keyword);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00027028 File Offset: 0x00025228
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._dictionary.GetEnumerator();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0002703C File Offset: 0x0002523C
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			object obj = DbConnectionStringBuilder._staticAttributeCollection;
			if (obj == null)
			{
				CLSCompliantAttribute clscompliantAttribute = new CLSCompliantAttribute(true);
				DefaultMemberAttribute defaultMemberAttribute = new DefaultMemberAttribute("Item");
				Attribute[] array = new Attribute[] { clscompliantAttribute, defaultMemberAttribute };
				obj = new AttributeCollection(array);
			}
			Interlocked.CompareExchange(ref DbConnectionStringBuilder._staticAttributeCollection, obj, null);
			return DbConnectionStringBuilder._staticAttributeCollection as AttributeCollection;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00027094 File Offset: 0x00025294
		string ICustomTypeDescriptor.GetClassName()
		{
			return base.GetType().ToString();
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000270A4 File Offset: 0x000252A4
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x000270A8 File Offset: 0x000252A8
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return new CollectionConverter();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000270B0 File Offset: 0x000252B0
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000270B4 File Offset: 0x000252B4
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000270B8 File Offset: 0x000252B8
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000270BC File Offset: 0x000252BC
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000270C4 File Offset: 0x000252C4
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000270CC File Offset: 0x000252CC
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return PropertyDescriptorCollection.Empty;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000270D4 File Offset: 0x000252D4
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return PropertyDescriptorCollection.Empty;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000270DC File Offset: 0x000252DC
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="P:System.Data.Common.DbConnectionStringBuilder.ConnectionString" /> property is visible in Visual Studio designers.</summary>
		/// <returns>true if the connection string is visible within designers; false otherwise. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x000270E4 File Offset: 0x000252E4
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x000270EC File Offset: 0x000252EC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DesignOnly(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool BrowsableConnectionString
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the connection string associated with the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>The current connection string, created from the key/value pairs that are contained within the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />. The default value is an empty string.</returns>
		/// <exception cref="T:System.ArgumentException">An invalid connection string argument has been supplied.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x000270F4 File Offset: 0x000252F4
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x000271A0 File Offset: 0x000253A0
		[RefreshProperties(RefreshProperties.All)]
		public string ConnectionString
		{
			get
			{
				IDictionary<string, object> dictionary = this._dictionary;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this.Keys)
				{
					string text = (string)obj;
					object obj2 = null;
					if (dictionary.TryGetValue(text, out obj2))
					{
						string text2 = obj2.ToString();
						DbConnectionStringBuilder.AppendKeyValuePair(stringBuilder, text, text2, this.useOdbcRules);
					}
				}
				return stringBuilder.ToString();
			}
			set
			{
				this.Clear();
				if (value == null)
				{
					return;
				}
				if (value.Trim().Length == 0)
				{
					return;
				}
				this.ParseConnectionString(value);
			}
		}

		/// <summary>Gets the current number of keys that are contained within the <see cref="P:System.Data.Common.DbConnectionStringBuilder.ConnectionString" /> property.</summary>
		/// <returns>The number of keys that are contained within the connection string maintained by the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x000271D4 File Offset: 0x000253D4
		[Browsable(false)]
		public virtual int Count
		{
			get
			{
				return this._dictionary.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size; otherwise false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x000271E4 File Offset: 0x000253E4
		[Browsable(false)]
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x000271E8 File Offset: 0x000253E8
		[Browsable(false)]
		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <returns>The value associated with the specified key. If the specified key is not found, trying to get it returns a null reference (Nothing in Visual Basic), and trying to set it creates a new element using the specified key.Passing a null (Nothing in Visual Basic) key throws an <see cref="T:System.ArgumentNullException" />. Assigning a null value removes the key/value pair.</returns>
		/// <param name="keyword">The key of the item to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set, and the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only. -or-The property is set, <paramref name="keyword" /> does not exist in the collection, and the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000176 RID: 374
		[Browsable(false)]
		public virtual object this[string keyword]
		{
			get
			{
				if (this.ContainsKey(keyword))
				{
					return this._dictionary[keyword];
				}
				throw new ArgumentException(string.Format("Keyword '{0}' does not exist", keyword));
			}
			set
			{
				if (value == null)
				{
					this.Remove(keyword);
					return;
				}
				if (keyword == null)
				{
					throw new ArgumentNullException("keyword");
				}
				if (keyword.Length == 0)
				{
					throw DbConnectionStringBuilder.CreateInvalidKeywordException(keyword);
				}
				for (int i = 0; i < keyword.Length; i++)
				{
					char c = keyword[i];
					if (i == 0 && (char.IsWhiteSpace(c) || c == ';'))
					{
						throw DbConnectionStringBuilder.CreateInvalidKeywordException(keyword);
					}
					if (i == keyword.Length - 1 && char.IsWhiteSpace(c))
					{
						throw DbConnectionStringBuilder.CreateInvalidKeywordException(keyword);
					}
					if (char.IsControl(c))
					{
						throw DbConnectionStringBuilder.CreateInvalidKeywordException(keyword);
					}
				}
				if (this.ContainsKey(keyword))
				{
					this._dictionary[keyword] = value;
				}
				else
				{
					this._dictionary.Add(keyword, value);
				}
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x000272F8 File Offset: 0x000254F8
		[Browsable(false)]
		public virtual ICollection Keys
		{
			get
			{
				string[] array = new string[this._dictionary.Keys.Count];
				((ICollection<string>)this._dictionary.Keys).CopyTo(array, 0);
				return new ReadOnlyCollection<string>(array);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the values in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the values in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00027338 File Offset: 0x00025538
		[Browsable(false)]
		public virtual ICollection Values
		{
			get
			{
				object[] array = new object[this._dictionary.Values.Count];
				((ICollection<object>)this._dictionary.Values).CopyTo(array, 0);
				return new ReadOnlyCollection<object>(array);
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <param name="keyword">The key to add to the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		/// <param name="value">The value for the specified key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only. -or-The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600085B RID: 2139 RVA: 0x00027378 File Offset: 0x00025578
		public void Add(string keyword, object value)
		{
			this[keyword] = value;
		}

		/// <summary>Provides an efficient and safe way to append a key and value to an existing <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <param name="builder">The <see cref="T:System.Text.StringBuilder" /> to which to add the key/value pair.</param>
		/// <param name="keyword">The key to be added.</param>
		/// <param name="value">The value for the supplied key.</param>
		/// <param name="useOdbcRules">true to use {} to delimit fields, false to use quotation marks.</param>
		// Token: 0x0600085C RID: 2140 RVA: 0x00027384 File Offset: 0x00025584
		public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value, bool useOdbcRules)
		{
			if (builder == null)
			{
				throw new ArgumentNullException("builder");
			}
			if (keyword == null)
			{
				throw new ArgumentNullException("keyName");
			}
			if (keyword.Length == 0)
			{
				throw new ArgumentException("Empty keyword is not valid.");
			}
			if (builder.Length > 0)
			{
				builder.Append(';');
			}
			if (!useOdbcRules)
			{
				builder.Append(keyword.Replace("=", "=="));
			}
			else
			{
				builder.Append(keyword);
			}
			builder.Append('=');
			if (value == null || value.Length == 0)
			{
				return;
			}
			if (!useOdbcRules)
			{
				bool flag = value.IndexOf('"') > -1;
				bool flag2 = value.IndexOf('\'') > -1;
				if (flag && flag2)
				{
					builder.Append('"');
					builder.Append(value.Replace("\"", "\"\""));
					builder.Append('"');
				}
				else if (flag)
				{
					builder.Append('\'');
					builder.Append(value);
					builder.Append('\'');
				}
				else if (flag2 || value.IndexOf('=') > -1 || value.IndexOf(';') > -1)
				{
					builder.Append('"');
					builder.Append(value);
					builder.Append('"');
				}
				else if (DbConnectionStringBuilder.ValueNeedsQuoting(value))
				{
					builder.Append('"');
					builder.Append(value);
					builder.Append('"');
				}
				else
				{
					builder.Append(value);
				}
			}
			else
			{
				int num = 0;
				bool flag3 = false;
				int length = value.Length;
				bool flag4 = false;
				int num2 = -1;
				int i = 0;
				while (i < length)
				{
					int num3;
					if (i == length - 1)
					{
						num3 = -1;
					}
					else
					{
						num3 = (int)value[i + 1];
					}
					char c = value[i];
					char c2 = c;
					switch (c2)
					{
					case '{':
						num++;
						goto IL_0237;
					default:
						if (c2 != ';')
						{
							goto IL_0237;
						}
						flag3 = true;
						goto IL_0237;
					case '}':
						if (!num3.Equals((int)c))
						{
							num--;
							if (num3 != -1)
							{
								flag4 = true;
							}
							goto IL_0237;
						}
						i++;
						break;
					}
					IL_023B:
					i++;
					continue;
					IL_0237:
					num2 = (int)c;
					goto IL_023B;
				}
				if (value[0] == '{' && (num2 != 125 || (num == 0 && flag4)))
				{
					builder.Append('{');
					builder.Append(value.Replace("}", "}}"));
					builder.Append('}');
					return;
				}
				bool flag5 = string.Compare(keyword, "Driver", StringComparison.InvariantCultureIgnoreCase) == 0;
				if (flag5)
				{
					if (value[0] == '{' && num2 == 125 && !flag4)
					{
						builder.Append(value);
						return;
					}
					builder.Append('{');
					builder.Append(value.Replace("}", "}}"));
					builder.Append('}');
					return;
				}
				else
				{
					if (value[0] == '{' && (num != 0 || num2 != 125) && flag4)
					{
						builder.Append('{');
						builder.Append(value.Replace("}", "}}"));
						builder.Append('}');
						return;
					}
					if (value[0] != '{' && flag3)
					{
						builder.Append('{');
						builder.Append(value.Replace("}", "}}"));
						builder.Append('}');
						return;
					}
					builder.Append(value);
				}
			}
		}

		/// <summary>Provides an efficient and safe way to append a key and value to an existing <see cref="T:System.Text.StringBuilder" /> object.</summary>
		/// <param name="builder">The <see cref="T:System.Text.StringBuilder" /> to which to add the key/value pair.</param>
		/// <param name="keyword">The key to be added.</param>
		/// <param name="value">The value for the supplied key.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600085D RID: 2141 RVA: 0x00027728 File Offset: 0x00025928
		public static void AppendKeyValuePair(StringBuilder builder, string keyword, string value)
		{
			DbConnectionStringBuilder.AppendKeyValuePair(builder, keyword, value, false);
		}

		/// <summary>Clears the contents of the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600085E RID: 2142 RVA: 0x00027734 File Offset: 0x00025934
		public virtual void Clear()
		{
			this._dictionary.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> contains an entry with the specified key; otherwise false.</returns>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600085F RID: 2143 RVA: 0x00027744 File Offset: 0x00025944
		public virtual bool ContainsKey(string keyword)
		{
			if (keyword == null)
			{
				throw new ArgumentNullException("keyword");
			}
			return this._dictionary.ContainsKey(keyword);
		}

		/// <summary>Compares the connection information in this <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> object with the connection information in the supplied object.</summary>
		/// <returns>true if the connection information in both of the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> objects causes an equivalent connection string; otherwise false.</returns>
		/// <param name="connectionStringBuilder">The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> to be compared with this <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> object.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000860 RID: 2144 RVA: 0x00027764 File Offset: 0x00025964
		public virtual bool EquivalentTo(DbConnectionStringBuilder connectionStringBuilder)
		{
			bool flag = true;
			try
			{
				if (this.Count != connectionStringBuilder.Count)
				{
					flag = false;
				}
				else
				{
					foreach (object obj in this.Keys)
					{
						string text = (string)obj;
						if (!this[text].Equals(connectionStringBuilder[text]))
						{
							flag = false;
							break;
						}
					}
				}
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		/// <summary>Fills a supplied <see cref="T:System.Collections.Hashtable" /> with information about all the properties of this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <param name="propertyDescriptors">The <see cref="T:System.Collections.Hashtable" /> to be filled with information about this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		// Token: 0x06000861 RID: 2145 RVA: 0x0002782C File Offset: 0x00025A2C
		[MonoTODO]
		protected virtual void GetProperties(Hashtable propertyDescriptors)
		{
			throw new NotImplementedException();
		}

		/// <summary>Clears the collection of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects on the associated <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		// Token: 0x06000862 RID: 2146 RVA: 0x00027834 File Offset: 0x00025A34
		[MonoTODO]
		protected internal void ClearPropertyDescriptors()
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>true if the key existed within the connection string and was removed; false if the key did not exist.</returns>
		/// <param name="keyword">The key of the key/value pair to be removed from the connection string in this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (Nothing in Visual Basic)</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> is read-only, or the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> has a fixed size.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000863 RID: 2147 RVA: 0x0002783C File Offset: 0x00025A3C
		public virtual bool Remove(string keyword)
		{
			if (keyword == null)
			{
				throw new ArgumentNullException("keyword");
			}
			return this._dictionary.Remove(keyword);
		}

		/// <summary>Indicates whether the specified key exists in this <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> instance.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> contains an entry with the specified key; otherwise false.</returns>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000864 RID: 2148 RVA: 0x0002785C File Offset: 0x00025A5C
		public virtual bool ShouldSerialize(string keyword)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the connection string associated with this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>The current <see cref="P:System.Data.Common.DbConnectionStringBuilder.ConnectionString" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000865 RID: 2149 RVA: 0x00027864 File Offset: 0x00025A64
		public override string ToString()
		{
			return this.ConnectionString;
		}

		/// <summary>Retrieves a value corresponding to the supplied key from this <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</summary>
		/// <returns>true if <paramref name="keyword" /> was found within the connection string, false otherwise.</returns>
		/// <param name="keyword">The key of the item to retrieve.</param>
		/// <param name="value">The value corresponding to the <paramref name="key" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> contains a null value (Nothing in Visual Basic).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000866 RID: 2150 RVA: 0x0002786C File Offset: 0x00025A6C
		public virtual bool TryGetValue(string keyword, out object value)
		{
			bool flag = this.ContainsKey(keyword);
			if (flag)
			{
				value = this[keyword];
			}
			else
			{
				value = null;
			}
			return flag;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002789C File Offset: 0x00025A9C
		private static ArgumentException CreateInvalidKeywordException(string keyword)
		{
			return new ArgumentException("A keyword cannot contain control characters, leading semicolons or leading or trailing whitespace.", keyword);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000278AC File Offset: 0x00025AAC
		private static ArgumentException CreateConnectionStringInvalidException(int index)
		{
			return new ArgumentException("Format of initialization string does not conform to specifications at index " + index + ".");
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000278C8 File Offset: 0x00025AC8
		private static bool ValueNeedsQuoting(string value)
		{
			foreach (char c in value)
			{
				if (char.IsWhiteSpace(c))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00027904 File Offset: 0x00025B04
		private void ParseConnectionString(string connectionString)
		{
			if (this.useOdbcRules)
			{
				this.ParseConnectionStringOdbc(connectionString);
			}
			else
			{
				this.ParseConnectionStringNonOdbc(connectionString);
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00027924 File Offset: 0x00025B24
		private void ParseConnectionStringOdbc(string connectionString)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			bool flag4 = false;
			string text = string.Empty;
			string text2 = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			int length = connectionString.Length;
			for (int i = 0; i < length; i++)
			{
				char c = connectionString[i];
				int num = ((i != length - 1) ? ((int)connectionString[i + 1]) : (-1));
				char c2 = c;
				switch (c2)
				{
				case ';':
					if (flag3 || flag4)
					{
						stringBuilder.Append(c);
					}
					else
					{
						if (text.Length > 0 && stringBuilder.Length > 0)
						{
							text2 = stringBuilder.ToString();
							text = text.ToLower().TrimEnd(new char[0]);
							this[text] = text2;
						}
						else if (stringBuilder.Length > 0)
						{
							throw DbConnectionStringBuilder.CreateConnectionStringInvalidException((int)c);
						}
						flag3 = true;
						text = string.Empty;
						stringBuilder.Length = 0;
					}
					break;
				default:
					switch (c2)
					{
					case '{':
						if (flag3)
						{
							stringBuilder.Append(c);
							goto IL_0297;
						}
						if (stringBuilder.Length == 0)
						{
							flag4 = true;
						}
						stringBuilder.Append(c);
						goto IL_0297;
					case '}':
						if (flag3 || !flag4)
						{
							stringBuilder.Append(c);
							goto IL_0297;
						}
						if (num == -1)
						{
							stringBuilder.Append(c);
							flag4 = false;
						}
						else if (num.Equals((int)c))
						{
							stringBuilder.Append(c);
							stringBuilder.Append(c);
							i++;
						}
						else
						{
							int num2 = DbConnectionStringBuilder.NextNonWhitespaceChar(connectionString, i);
							if (num2 != -1 && (ushort)num2 != 59)
							{
								throw DbConnectionStringBuilder.CreateConnectionStringInvalidException(num2);
							}
							stringBuilder.Append(c);
							flag4 = false;
						}
						goto IL_0297;
					}
					if (flag2 || flag || flag4)
					{
						stringBuilder.Append(c);
					}
					else if (char.IsWhiteSpace(c))
					{
						if (stringBuilder.Length > 0)
						{
							int num3 = DbConnectionStringBuilder.SkipTrailingWhitespace(connectionString, i);
							if (num3 == -1)
							{
								stringBuilder.Append(c);
							}
							else
							{
								i = num3;
							}
						}
					}
					else
					{
						stringBuilder.Append(c);
					}
					break;
				case '=':
					if (flag4 || !flag3)
					{
						stringBuilder.Append(c);
					}
					else
					{
						text = stringBuilder.ToString();
						if (text.Length == 0)
						{
							throw DbConnectionStringBuilder.CreateConnectionStringInvalidException((int)c);
						}
						stringBuilder.Length = 0;
						flag3 = false;
					}
					break;
				}
				IL_0297:;
			}
			if ((flag3 && stringBuilder.Length > 0) || flag2 || flag || flag4)
			{
				throw DbConnectionStringBuilder.CreateConnectionStringInvalidException(length - 1);
			}
			if (text.Length > 0 && stringBuilder.Length > 0)
			{
				text2 = stringBuilder.ToString();
				text = text.ToLower().TrimEnd(new char[0]);
				this[text] = text2;
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00027C48 File Offset: 0x00025E48
		private void ParseConnectionStringNonOdbc(string connectionString)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			string text = string.Empty;
			string text2 = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			int length = connectionString.Length;
			for (int i = 0; i < length; i++)
			{
				char c = connectionString[i];
				int num = ((i != length - 1) ? ((int)connectionString[i + 1]) : (-1));
				char c2 = c;
				switch (c2)
				{
				case ';':
					if (flag3)
					{
						stringBuilder.Append(c);
					}
					else if (flag2 || flag)
					{
						stringBuilder.Append(c);
					}
					else
					{
						if (text.Length > 0 && stringBuilder.Length > 0)
						{
							text2 = stringBuilder.ToString();
							text = text.ToLower().TrimEnd(new char[0]);
							this[text] = text2;
						}
						else if (stringBuilder.Length > 0)
						{
							throw DbConnectionStringBuilder.CreateConnectionStringInvalidException((int)c);
						}
						flag3 = true;
						text = string.Empty;
						stringBuilder.Length = 0;
					}
					break;
				default:
					if (c2 != '"')
					{
						if (c2 != '\'')
						{
							if (flag2 || flag)
							{
								stringBuilder.Append(c);
							}
							else if (char.IsWhiteSpace(c))
							{
								if (stringBuilder.Length > 0)
								{
									int num2 = DbConnectionStringBuilder.SkipTrailingWhitespace(connectionString, i);
									if (num2 == -1)
									{
										stringBuilder.Append(c);
									}
									else
									{
										i = num2;
									}
								}
							}
							else
							{
								stringBuilder.Append(c);
							}
						}
						else if (flag3)
						{
							stringBuilder.Append(c);
						}
						else if (flag2)
						{
							stringBuilder.Append(c);
						}
						else if (flag)
						{
							if (num == -1)
							{
								flag = false;
							}
							else if (num.Equals((int)c))
							{
								stringBuilder.Append(c);
								i++;
							}
							else
							{
								int num3 = DbConnectionStringBuilder.NextNonWhitespaceChar(connectionString, i);
								if (num3 != -1 && (ushort)num3 != 59)
								{
									throw DbConnectionStringBuilder.CreateConnectionStringInvalidException(num3);
								}
								flag = false;
							}
							if (!flag)
							{
								text2 = stringBuilder.ToString();
								text = text.ToLower().TrimEnd(new char[0]);
								this[text] = text2;
								flag3 = true;
								text = string.Empty;
								stringBuilder.Length = 0;
							}
						}
						else if (stringBuilder.Length == 0)
						{
							flag = true;
						}
						else
						{
							stringBuilder.Append(c);
						}
					}
					else if (flag3)
					{
						stringBuilder.Append(c);
					}
					else if (flag)
					{
						stringBuilder.Append(c);
					}
					else if (flag2)
					{
						if (num == -1)
						{
							flag2 = false;
						}
						else if (num.Equals((int)c))
						{
							stringBuilder.Append(c);
							i++;
						}
						else
						{
							int num4 = DbConnectionStringBuilder.NextNonWhitespaceChar(connectionString, i);
							if (num4 != -1 && (ushort)num4 != 59)
							{
								throw DbConnectionStringBuilder.CreateConnectionStringInvalidException(num4);
							}
							flag2 = false;
						}
					}
					else if (stringBuilder.Length == 0)
					{
						flag2 = true;
					}
					else
					{
						stringBuilder.Append(c);
					}
					break;
				case '=':
					if (flag2 || flag || !flag3)
					{
						stringBuilder.Append(c);
					}
					else if (num != -1 && num.Equals((int)c))
					{
						stringBuilder.Append(c);
						i++;
					}
					else
					{
						text = stringBuilder.ToString();
						if (text.Length == 0)
						{
							throw DbConnectionStringBuilder.CreateConnectionStringInvalidException((int)c);
						}
						stringBuilder.Length = 0;
						flag3 = false;
					}
					break;
				}
			}
			if ((flag3 && stringBuilder.Length > 0) || flag2 || flag)
			{
				throw DbConnectionStringBuilder.CreateConnectionStringInvalidException(length - 1);
			}
			if (text.Length > 0 && stringBuilder.Length > 0)
			{
				text2 = stringBuilder.ToString();
				text = text.ToLower().TrimEnd(new char[0]);
				this[text] = text2;
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00028064 File Offset: 0x00026264
		private static int SkipTrailingWhitespace(string value, int index)
		{
			int length = value.Length;
			for (int i = index + 1; i < length; i++)
			{
				char c = value[i];
				if (c == ';')
				{
					return i - 1;
				}
				if (!char.IsWhiteSpace(c))
				{
					return -1;
				}
			}
			return length - 1;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000280B0 File Offset: 0x000262B0
		private static int NextNonWhitespaceChar(string value, int index)
		{
			int length = value.Length;
			for (int i = index + 1; i < length; i++)
			{
				char c = value[i];
				if (!char.IsWhiteSpace(c))
				{
					return (int)c;
				}
			}
			return -1;
		}

		// Token: 0x04000310 RID: 784
		private readonly Dictionary<string, object> _dictionary;

		// Token: 0x04000311 RID: 785
		private readonly bool useOdbcRules;

		// Token: 0x04000312 RID: 786
		private static object _staticAttributeCollection;
	}
}
