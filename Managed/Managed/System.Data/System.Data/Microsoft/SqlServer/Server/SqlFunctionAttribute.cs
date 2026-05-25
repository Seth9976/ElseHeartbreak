using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a method definition of a user-defined aggregate as a function in SQL Server. The properties on the attribute reflect the physical characteristics used when the type is registered with SQL Server.</summary>
	// Token: 0x02000148 RID: 328
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public class SqlFunctionAttribute : Attribute
	{
		/// <summary>An optional attribute on a user-defined aggregate, used to indicate that the method should be registered in SQL Server as a function. Also used to set the <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.DataAccess" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.FillRowMethodName" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.IsDeterministic" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.IsPrecise" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.Name" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.SystemDataAccess" />, and <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.TableDefinition" /> properties of the function attribute.</summary>
		// Token: 0x06001172 RID: 4466 RVA: 0x00044538 File Offset: 0x00042738
		public SqlFunctionAttribute()
		{
			this.dataAccess = DataAccessKind.None;
			this.isDeterministic = false;
			this.isPrecise = false;
			this.systemDataAccess = SystemDataAccessKind.None;
		}

		/// <summary>Indicates whether the function involves access to user data stored in the local instance of SQL Server.</summary>
		/// <returns>
		///   <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.None: Does not access data. <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.Read: Only reads user data.</returns>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x00044568 File Offset: 0x00042768
		// (set) Token: 0x06001174 RID: 4468 RVA: 0x00044570 File Offset: 0x00042770
		public DataAccessKind DataAccess
		{
			get
			{
				return this.dataAccess;
			}
			set
			{
				this.dataAccess = value;
			}
		}

		/// <summary>Indicates whether the user-defined function is deterministic.</summary>
		/// <returns>true if the function is deterministic; otherwise false.</returns>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0004457C File Offset: 0x0004277C
		// (set) Token: 0x06001176 RID: 4470 RVA: 0x00044584 File Offset: 0x00042784
		public bool IsDeterministic
		{
			get
			{
				return this.isDeterministic;
			}
			set
			{
				this.isDeterministic = value;
			}
		}

		/// <summary>Indicates whether the function involves imprecise computations, such as floating point operations.</summary>
		/// <returns>true if the function involves precise computations; otherwise false.</returns>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x00044590 File Offset: 0x00042790
		// (set) Token: 0x06001178 RID: 4472 RVA: 0x00044598 File Offset: 0x00042798
		public bool IsPrecise
		{
			get
			{
				return this.isPrecise;
			}
			set
			{
				this.isPrecise = value;
			}
		}

		/// <summary>Indicates whether the function requires access to data stored in the system catalogs or virtual system tables of SQL Server.</summary>
		/// <returns>
		///   <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.None: Does not access system data. <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.Read: Only reads system data.</returns>
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x000445A4 File Offset: 0x000427A4
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x000445AC File Offset: 0x000427AC
		public SystemDataAccessKind SystemDataAccess
		{
			get
			{
				return this.systemDataAccess;
			}
			set
			{
				this.systemDataAccess = value;
			}
		}

		// Token: 0x0400067A RID: 1658
		private DataAccessKind dataAccess;

		// Token: 0x0400067B RID: 1659
		private bool isDeterministic;

		// Token: 0x0400067C RID: 1660
		private bool isPrecise;

		// Token: 0x0400067D RID: 1661
		private SystemDataAccessKind systemDataAccess;
	}
}
