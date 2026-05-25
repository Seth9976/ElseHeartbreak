using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a method definition in an assembly as a trigger in SQL Server. The properties on the attribute reflect the physical attributes used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x02000152 RID: 338
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlTriggerAttribute : Attribute
	{
		/// <summary>An attribute on a method definition in an assembly, used to mark the method as a trigger in SQL Server.</summary>
		// Token: 0x060011DA RID: 4570 RVA: 0x00045EB8 File Offset: 0x000440B8
		public SqlTriggerAttribute()
		{
			this.triggerEvent = null;
			this.name = null;
			this.target = null;
		}

		/// <summary>The type of trigger and what data manipulation language (DML) action activates the trigger.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the type of trigger and what data manipulation language (DML) action activates the trigger.</returns>
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00045ED8 File Offset: 0x000440D8
		// (set) Token: 0x060011DC RID: 4572 RVA: 0x00045EE0 File Offset: 0x000440E0
		public string Event
		{
			get
			{
				return this.triggerEvent;
			}
			set
			{
				this.triggerEvent = value;
			}
		}

		/// <summary>The name of the trigger.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the name of the trigger.</returns>
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00045EEC File Offset: 0x000440EC
		// (set) Token: 0x060011DE RID: 4574 RVA: 0x00045EF4 File Offset: 0x000440F4
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

		/// <summary>The table to which the trigger applies.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the table name.</returns>
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x00045F00 File Offset: 0x00044100
		// (set) Token: 0x060011E0 RID: 4576 RVA: 0x00045F08 File Offset: 0x00044108
		public string Target
		{
			get
			{
				return this.target;
			}
			set
			{
				this.target = value;
			}
		}

		// Token: 0x040006F1 RID: 1777
		private string triggerEvent;

		// Token: 0x040006F2 RID: 1778
		private string name;

		// Token: 0x040006F3 RID: 1779
		private string target;
	}
}
