using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a type definition in an assembly as a user-defined type (UDT) in SQL Server. The properties on the attribute reflect the physical characteristics used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x0200014C RID: 332
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
	public sealed class SqlUserDefinedTypeAttribute : Attribute
	{
		/// <summary>A required attribute on a user-defined type (UDT), used to confirm that the given type is a UDT and to indicate the storage format of the UDT.</summary>
		/// <param name="format">One of the <see cref="T:Microsoft.SqlServer.Server.Format" /> values representing the serialization format of the type.</param>
		// Token: 0x060011BF RID: 4543 RVA: 0x00045D20 File Offset: 0x00043F20
		public SqlUserDefinedTypeAttribute(Format f)
		{
			this.format = f;
			this.IsByteOrdered = false;
			this.IsFixedLength = false;
			this.MaxByteSize = 8000;
		}

		/// <summary>The serialization format as a <see cref="T:Microsoft.SqlServer.Server.Format" />.</summary>
		/// <returns>A <see cref="T:Microsoft.SqlServer.Server.Format" /> value representing the serialization format.</returns>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x00045D54 File Offset: 0x00043F54
		public Format Format
		{
			get
			{
				return this.format;
			}
		}

		/// <summary>Indicates whether the user-defined type is byte ordered.</summary>
		/// <returns>true if the user-defined type is byte ordered; otherwise false.</returns>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00045D5C File Offset: 0x00043F5C
		// (set) Token: 0x060011C2 RID: 4546 RVA: 0x00045D64 File Offset: 0x00043F64
		public bool IsByteOrdered
		{
			get
			{
				return this.isByteOrdered;
			}
			set
			{
				this.isByteOrdered = value;
			}
		}

		/// <summary>Indicates whether all instances of this user-defined type are the same length.</summary>
		/// <returns>true if all instances of this type are the same length; otherwise false.</returns>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x00045D70 File Offset: 0x00043F70
		// (set) Token: 0x060011C4 RID: 4548 RVA: 0x00045D78 File Offset: 0x00043F78
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

		/// <summary>The maximum size of the instance, in bytes.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the maximum size of the instance.</returns>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00045D84 File Offset: 0x00043F84
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x00045D8C File Offset: 0x00043F8C
		public int MaxByteSize
		{
			get
			{
				return this.maxByteSize;
			}
			set
			{
				this.maxByteSize = value;
			}
		}

		// Token: 0x04000694 RID: 1684
		private const int MaxByteSizeValue = 8000;

		// Token: 0x04000695 RID: 1685
		private Format format;

		// Token: 0x04000696 RID: 1686
		private bool isByteOrdered;

		// Token: 0x04000697 RID: 1687
		private bool isFixedLength;

		// Token: 0x04000698 RID: 1688
		private int maxByteSize;
	}
}
