using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Annotates the returned result of a user-defined type (UDT) with additional information that can be used in Transact-SQL.</summary>
	// Token: 0x0200014E RID: 334
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
	public class SqlFacetAttribute : Attribute
	{
		/// <summary>An optional attribute on a user-defined type (UDT) return type, used to annotate the returned result with additional information that can be used in Transact-SQL.</summary>
		// Token: 0x060011C7 RID: 4551 RVA: 0x00045D98 File Offset: 0x00043F98
		public SqlFacetAttribute()
		{
			this.isFixedLength = false;
			this.isNullable = false;
			this.maxSize = 0;
			this.precision = 0;
			this.scale = 0;
		}

		/// <summary>Indicates whether the return type of the user-defined type is of a fixed length.</summary>
		/// <returns>true if the return type is of a fixed length; otherwise false.</returns>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x00045DC4 File Offset: 0x00043FC4
		// (set) Token: 0x060011C9 RID: 4553 RVA: 0x00045DCC File Offset: 0x00043FCC
		public bool IsFixedLength
		{
			get
			{
				return this.isFixedLength;
			}
			set
			{
				this.isFixedLength = value;
			}
		}

		/// <summary>Indicates whether the return type of the user-defined type can be null.</summary>
		/// <returns>true if the return type of the user-defined type can be null; otherwise false.</returns>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x00045DD8 File Offset: 0x00043FD8
		// (set) Token: 0x060011CB RID: 4555 RVA: 0x00045DE0 File Offset: 0x00043FE0
		public bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
			set
			{
				this.isNullable = value;
			}
		}

		/// <summary>The maximum size, in logical units, of the underlying field type of the user-defined type.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the maximum size, in logical units, of the underlying field type.</returns>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x00045DEC File Offset: 0x00043FEC
		// (set) Token: 0x060011CD RID: 4557 RVA: 0x00045DF4 File Offset: 0x00043FF4
		public int MaxSize
		{
			get
			{
				return this.maxSize;
			}
			set
			{
				this.maxSize = value;
			}
		}

		/// <summary>The precision of the return type of the user-defined type.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the precision of the return type.</returns>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x00045E00 File Offset: 0x00044000
		// (set) Token: 0x060011CF RID: 4559 RVA: 0x00045E08 File Offset: 0x00044008
		public int Precision
		{
			get
			{
				return this.precision;
			}
			set
			{
				this.precision = value;
			}
		}

		/// <summary>The scale of the return type of the user-defined type.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the scale of the return type.</returns>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00045E14 File Offset: 0x00044014
		// (set) Token: 0x060011D1 RID: 4561 RVA: 0x00045E1C File Offset: 0x0004401C
		public int Scale
		{
			get
			{
				return this.scale;
			}
			set
			{
				this.scale = value;
			}
		}

		// Token: 0x0400069C RID: 1692
		private bool isFixedLength;

		// Token: 0x0400069D RID: 1693
		private bool isNullable;

		// Token: 0x0400069E RID: 1694
		private int maxSize;

		// Token: 0x0400069F RID: 1695
		private int precision;

		// Token: 0x040006A0 RID: 1696
		private int scale;
	}
}
