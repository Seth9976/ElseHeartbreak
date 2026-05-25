using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Exposes the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method, which supports a simple iteration over a collection by a .NET Framework data provider.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C4 RID: 196
	public class DbEnumerator : IEnumerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbEnumerator" /> class using the specified DataReader.</summary>
		/// <param name="reader">The DataReader through which to iterate. </param>
		// Token: 0x0600099F RID: 2463 RVA: 0x0002E6E0 File Offset: 0x0002C8E0
		public DbEnumerator(IDataReader reader)
			: this(reader, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DbEnumerator" /> class using the specified DataReader, and indicates whether to automatically close the DataReader after iterating through its data.</summary>
		/// <param name="reader">The DataReader through which to iterate. </param>
		/// <param name="closeReader">true to automatically close the DataReader after iterating through its data; otherwise, false. </param>
		// Token: 0x060009A0 RID: 2464 RVA: 0x0002E6EC File Offset: 0x0002C8EC
		public DbEnumerator(IDataReader reader, bool closeReader)
		{
			this.reader = reader;
			this.closeReader = closeReader;
			this.values = new object[reader.FieldCount];
			this.schema = DbEnumerator.LoadSchema(reader);
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0002E720 File Offset: 0x0002C920
		public object Current
		{
			get
			{
				this.reader.GetValues(this.values);
				return new DbDataRecordImpl(this.schema, this.values);
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0002E748 File Offset: 0x0002C948
		private static SchemaInfo[] LoadSchema(IDataReader reader)
		{
			int fieldCount = reader.FieldCount;
			SchemaInfo[] array = new SchemaInfo[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = new SchemaInfo
				{
					ColumnName = reader.GetName(i),
					ColumnOrdinal = i,
					DataTypeName = reader.GetDataTypeName(i),
					FieldType = reader.GetFieldType(i)
				};
			}
			return array;
		}

		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060009A3 RID: 2467 RVA: 0x0002E7B0 File Offset: 0x0002C9B0
		public bool MoveNext()
		{
			if (this.reader.Read())
			{
				return true;
			}
			if (this.closeReader)
			{
				this.reader.Close();
			}
			return false;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060009A4 RID: 2468 RVA: 0x0002E7DC File Offset: 0x0002C9DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000337 RID: 823
		private readonly IDataReader reader;

		// Token: 0x04000338 RID: 824
		private readonly bool closeReader;

		// Token: 0x04000339 RID: 825
		private readonly SchemaInfo[] schema;

		// Token: 0x0400033A RID: 826
		private readonly object[] values;
	}
}
