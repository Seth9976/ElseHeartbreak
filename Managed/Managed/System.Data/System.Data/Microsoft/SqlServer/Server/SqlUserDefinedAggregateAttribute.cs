using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Indicates that the type should be registered as a user-defined aggregate. The properties on the attribute reflect the physical attributes used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x0200014B RID: 331
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public sealed class SqlUserDefinedAggregateAttribute : Attribute
	{
		/// <summary>A required attribute on a user-defined aggregate, used to indicate that the given type is a user-defined aggregate and the storage format of the user-defined aggregate.</summary>
		/// <param name="format">One of the <see cref="T:Microsoft.SqlServer.Server.Format" /> values representing the serialization format of the aggregate.</param>
		// Token: 0x060011B3 RID: 4531 RVA: 0x00045C70 File Offset: 0x00043E70
		public SqlUserDefinedAggregateAttribute(Format f)
		{
			this.format = f;
			this.IsInvariantToDuplicates = false;
			this.IsInvariantToNulls = false;
			this.IsInvariantToOrder = false;
			this.IsNullIfEmpty = false;
			this.MaxByteSize = 8000;
		}

		/// <summary>The serialization format as a <see cref="T:Microsoft.SqlServer.Server.Format" />.</summary>
		/// <returns>A <see cref="T:Microsoft.SqlServer.Server.Format" /> representing the serialization format.</returns>
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x00045CB4 File Offset: 0x00043EB4
		public Format Format
		{
			get
			{
				return this.format;
			}
		}

		/// <summary>Indicates whether the aggregate is invariant to duplicates.</summary>
		/// <returns>true if the aggregate is invariant to duplicates; otherwise false.</returns>
		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x00045CBC File Offset: 0x00043EBC
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x00045CC4 File Offset: 0x00043EC4
		public bool IsInvariantToDuplicates
		{
			get
			{
				return this.isInvariantToDuplicates;
			}
			set
			{
				this.isInvariantToDuplicates = value;
			}
		}

		/// <summary>Indicates whether the aggregate is invariant to nulls.</summary>
		/// <returns>true if the aggregate is invariant to nulls; otherwise false.</returns>
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00045CD0 File Offset: 0x00043ED0
		// (set) Token: 0x060011B8 RID: 4536 RVA: 0x00045CD8 File Offset: 0x00043ED8
		public bool IsInvariantToNulls
		{
			get
			{
				return this.isInvariantToNulls;
			}
			set
			{
				this.isInvariantToNulls = value;
			}
		}

		/// <summary>Indicates whether the aggregate is invariant to order.</summary>
		/// <returns>true if the aggregate is invariant to order; otherwise false.</returns>
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00045CE4 File Offset: 0x00043EE4
		// (set) Token: 0x060011BA RID: 4538 RVA: 0x00045CEC File Offset: 0x00043EEC
		public bool IsInvariantToOrder
		{
			get
			{
				return this.isInvariantToOrder;
			}
			set
			{
				this.isInvariantToOrder = value;
			}
		}

		/// <summary>Indicates whether the aggregate returns null if no values have been accumulated.</summary>
		/// <returns>true if the aggregate returns null if no values have been accumulated; otherwise false.</returns>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00045CF8 File Offset: 0x00043EF8
		// (set) Token: 0x060011BC RID: 4540 RVA: 0x00045D00 File Offset: 0x00043F00
		public bool IsNullIfEmpty
		{
			get
			{
				return this.isNullIfEmpty;
			}
			set
			{
				this.isNullIfEmpty = value;
			}
		}

		/// <summary>The maximum size, in bytes, of the aggregate instance.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the maximum size of the aggregate instance.</returns>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00045D0C File Offset: 0x00043F0C
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x00045D14 File Offset: 0x00043F14
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

		/// <summary>The maximum size, in bytes, required to store the state of this aggregate instance during computation.</summary>
		// Token: 0x0400068D RID: 1677
		public const int MaxByteSizeValue = 8000;

		// Token: 0x0400068E RID: 1678
		private Format format;

		// Token: 0x0400068F RID: 1679
		private bool isInvariantToDuplicates;

		// Token: 0x04000690 RID: 1680
		private bool isInvariantToNulls;

		// Token: 0x04000691 RID: 1681
		private bool isInvariantToOrder;

		// Token: 0x04000692 RID: 1682
		private bool isNullIfEmpty;

		// Token: 0x04000693 RID: 1683
		private int maxByteSize;
	}
}
