using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a method definition in an assembly as a stored procedure. The properties on the attribute reflect the physical characteristics used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x02000151 RID: 337
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlProcedureAttribute : Attribute
	{
		/// <summary>An attribute on a method definition in an assembly, used to indicate that the given method should be registered as a stored procedure in SQL Server.</summary>
		// Token: 0x060011D7 RID: 4567 RVA: 0x00045E94 File Offset: 0x00044094
		public SqlProcedureAttribute()
		{
			this.name = null;
		}

		/// <summary>The name of the stored procedure.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of the stored procedure.</returns>
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00045EA4 File Offset: 0x000440A4
		// (set) Token: 0x060011D9 RID: 4569 RVA: 0x00045EAC File Offset: 0x000440AC
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x040006F0 RID: 1776
		private string name;
	}
}
