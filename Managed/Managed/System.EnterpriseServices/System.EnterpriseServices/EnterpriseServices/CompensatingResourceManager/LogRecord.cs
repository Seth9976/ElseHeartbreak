using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Represents an unstructured log record delivered as a COM+ CrmLogRecordRead structure. This class cannot be inherited.</summary>
	// Token: 0x0200005C RID: 92
	public sealed class LogRecord
	{
		// Token: 0x06000174 RID: 372 RVA: 0x00002F04 File Offset: 0x00001104
		[MonoTODO]
		internal LogRecord()
		{
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00002F0C File Offset: 0x0000110C
		[MonoTODO]
		internal LogRecord(_LogRecord logRecord)
		{
			this.flags = (LogRecordFlags)logRecord.dwCrmFlags;
			this.sequence = logRecord.dwSequenceNumber;
			this.record = logRecord.blobUserData;
		}

		/// <summary>Gets a value that indicates when the log record was written.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.LogRecordFlags" /> values which provides information about when this record was written.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00002F3C File Offset: 0x0000113C
		public LogRecordFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		/// <summary>Gets the log record user data.</summary>
		/// <returns>A single BLOB that contains the user data.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00002F44 File Offset: 0x00001144
		public object Record
		{
			get
			{
				return this.record;
			}
		}

		/// <summary>The sequence number of the log record.</summary>
		/// <returns>An integer value that specifies the sequence number of the log record.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00002F4C File Offset: 0x0000114C
		public int Sequence
		{
			get
			{
				return this.sequence;
			}
		}

		// Token: 0x040000B2 RID: 178
		private LogRecordFlags flags;

		// Token: 0x040000B3 RID: 179
		private object record;

		// Token: 0x040000B4 RID: 180
		private int sequence;
	}
}
