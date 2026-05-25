using System;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Represents an SQL statement or stored procedure to execute against a data source. Provides a base class for database-specific classes that represent commands.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000B4 RID: 180
	public abstract class DbCommand : Component, IDisposable, IDbCommand
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0002819C File Offset: 0x0002639C
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x000281A4 File Offset: 0x000263A4
		IDbConnection IDbCommand.Connection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (DbConnection)value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x000281B4 File Offset: 0x000263B4
		IDataParameterCollection IDbCommand.Parameters
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x000281BC File Offset: 0x000263BC
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x000281C4 File Offset: 0x000263C4
		IDbTransaction IDbCommand.Transaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (DbTransaction)value;
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000281D4 File Offset: 0x000263D4
		IDbDataParameter IDbCommand.CreateParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000281DC File Offset: 0x000263DC
		IDataReader IDbCommand.ExecuteReader()
		{
			return this.ExecuteReader();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000281E4 File Offset: 0x000263E4
		IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		/// <summary>Gets or sets the text command to run against the data source.</summary>
		/// <returns>The text command to execute. The default value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600087B RID: 2171
		// (set) Token: 0x0600087C RID: 2172
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		public abstract string CommandText { get; set; }

		/// <summary>Gets or sets the wait time before terminating the attempt to execute a command and generating an error.</summary>
		/// <returns>The time in seconds to wait for the command to execute.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600087D RID: 2173
		// (set) Token: 0x0600087E RID: 2174
		public abstract int CommandTimeout { get; set; }

		/// <summary>Indicates or specifies how the <see cref="P:System.Data.Common.DbCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is Text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600087F RID: 2175
		// (set) Token: 0x06000880 RID: 2176
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(CommandType.Text)]
		public abstract CommandType CommandType { get; set; }

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbConnection" /> used by this <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x000281F0 File Offset: 0x000263F0
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x000281F8 File Offset: 0x000263F8
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbConnection Connection
		{
			get
			{
				return this.DbConnection;
			}
			set
			{
				this.DbConnection = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbConnection" /> used by this <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000883 RID: 2179
		// (set) Token: 0x06000884 RID: 2180
		protected abstract DbConnection DbConnection { get; set; }

		/// <summary>Gets the collection of <see cref="T:System.Data.Common.DbParameter" /> objects.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000885 RID: 2181
		protected abstract DbParameterCollection DbParameterCollection { get; }

		/// <summary>Gets or sets the <see cref="P:System.Data.Common.DbCommand.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.</summary>
		/// <returns>The transaction within which a Command object of a .NET Framework data provider executes. The default value is a null reference (Nothing in Visual Basic).</returns>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000886 RID: 2182
		// (set) Token: 0x06000887 RID: 2183
		protected abstract DbTransaction DbTransaction { get; set; }

		/// <summary>Gets or sets a value indicating whether the command object should be visible in a customized interface control.</summary>
		/// <returns>true, if the command object should be visible in a control; otherwise false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000888 RID: 2184
		// (set) Token: 0x06000889 RID: 2185
		[DefaultValue(true)]
		[Browsable(false)]
		[DesignOnly(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract bool DesignTimeVisible { get; set; }

		/// <summary>Gets the collection of <see cref="T:System.Data.Common.DbParameter" /> objects. For more information on parameters, see Configuring Parameters and Parameter Data Types (ADO.NET).</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00028204 File Offset: 0x00026404
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbParameterCollection Parameters
		{
			get
			{
				return this.DbParameterCollection;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.</summary>
		/// <returns>The transaction within which a Command object of a .NET Framework data provider executes. The default value is a null reference (Nothing in Visual Basic).</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0002820C File Offset: 0x0002640C
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x00028214 File Offset: 0x00026414
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		[Browsable(false)]
		public DbTransaction Transaction
		{
			get
			{
				return this.DbTransaction;
			}
			set
			{
				this.DbTransaction = value;
			}
		}

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the Update method of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values. The default is Both unless the command is automatically generated. Then the default is None.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600088D RID: 2189
		// (set) Token: 0x0600088E RID: 2190
		[DefaultValue(UpdateRowSource.Both)]
		public abstract UpdateRowSource UpdatedRowSource { get; set; }

		/// <summary>Attempts to cancels the execution of a <see cref="T:System.Data.Common.DbCommand" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600088F RID: 2191
		public abstract void Cancel();

		/// <summary>Creates a new instance of a <see cref="T:System.Data.Common.DbParameter" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
		// Token: 0x06000890 RID: 2192
		protected abstract DbParameter CreateDbParameter();

		/// <summary>Creates a new instance of a <see cref="T:System.Data.Common.DbParameter" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000891 RID: 2193 RVA: 0x00028220 File Offset: 0x00026420
		public DbParameter CreateParameter()
		{
			return this.CreateDbParameter();
		}

		/// <summary>Executes the command text against the connection.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" />.</returns>
		/// <param name="behavior">An instance of <see cref="T:System.Data.CommandBehavior" />.</param>
		// Token: 0x06000892 RID: 2194
		protected abstract DbDataReader ExecuteDbDataReader(CommandBehavior behavior);

		/// <summary>Executes a SQL statement against a connection object.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000893 RID: 2195
		public abstract int ExecuteNonQuery();

		/// <summary>Executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" />, and returns an <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000894 RID: 2196 RVA: 0x00028228 File Offset: 0x00026428
		public DbDataReader ExecuteReader()
		{
			return this.ExecuteDbDataReader(CommandBehavior.Default);
		}

		/// <summary>Executes the <see cref="P:System.Data.Common.DbCommand.CommandText" /> against the <see cref="P:System.Data.Common.DbCommand.Connection" />, and returns an <see cref="T:System.Data.Common.DbDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values. </summary>
		/// <returns>An <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000895 RID: 2197 RVA: 0x00028234 File Offset: 0x00026434
		public DbDataReader ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteDbDataReader(behavior);
		}

		/// <summary>Executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.</summary>
		/// <returns>The first column of the first row in the result set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000896 RID: 2198
		public abstract object ExecuteScalar();

		/// <summary>Creates a prepared (or compiled) version of the command on the data source.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000897 RID: 2199
		public abstract void Prepare();
	}
}
