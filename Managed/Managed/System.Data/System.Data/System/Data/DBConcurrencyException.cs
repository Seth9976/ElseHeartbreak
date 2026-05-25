using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>The exception that is thrown by the <see cref="T:System.Data.Common.DataAdapter" /> during an insert, update, or delete operation if the number of rows affected equals zero.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000041 RID: 65
	[Serializable]
	public sealed class DBConcurrencyException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		// Token: 0x0600052E RID: 1326 RVA: 0x0001D6CC File Offset: 0x0001B8CC
		public DBConcurrencyException()
			: base("Concurrency violation.")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		/// <param name="message">The text string describing the details of the exception. </param>
		// Token: 0x0600052F RID: 1327 RVA: 0x0001D6DC File Offset: 0x0001B8DC
		public DBConcurrencyException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		/// <param name="message">The text string describing the details of the exception. </param>
		/// <param name="inner">A reference to an inner exception. </param>
		// Token: 0x06000530 RID: 1328 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		public DBConcurrencyException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		/// <param name="message">The error message that explains the reason for this exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		/// <param name="dataRows">An array containing the <see cref="T:System.Data.DataRow" /> objects whose update failure generated this exception.</param>
		// Token: 0x06000531 RID: 1329 RVA: 0x0001D6F4 File Offset: 0x0001B8F4
		public DBConcurrencyException(string message, Exception inner, DataRow[] dataRows)
			: base(message, inner)
		{
			this.rows = dataRows;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001D708 File Offset: 0x0001B908
		private DBConcurrencyException(SerializationInfo si, StreamingContext sc)
			: base(si, sc)
		{
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Data.DataRow" /> that generated the <see cref="T:System.Data.DBConcurrencyException" />.</summary>
		/// <returns>The value of the <see cref="T:System.Data.DataRow" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001D714 File Offset: 0x0001B914
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0001D72C File Offset: 0x0001B92C
		public DataRow Row
		{
			get
			{
				if (this.rows != null)
				{
					return this.rows[0];
				}
				return null;
			}
			set
			{
				this.rows = new DataRow[] { value };
			}
		}

		/// <summary>Gets the number of rows whose update failed, generating this exception.</summary>
		/// <returns>An integer containing a count of the number of rows whose update failed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001D740 File Offset: 0x0001B940
		public int RowCount
		{
			get
			{
				if (this.rows != null)
				{
					return this.rows.Length;
				}
				return 0;
			}
		}

		/// <summary>Copies the <see cref="T:System.Data.DataRow" /> objects whose update failure generated this exception, to the specified array of <see cref="T:System.Data.DataRow" /> objects.</summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Data.DataRow" /> objects to copy the <see cref="T:System.Data.DataRow" /> objects into.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000536 RID: 1334 RVA: 0x0001D758 File Offset: 0x0001B958
		[MonoTODO]
		public void CopyToRows(DataRow[] array)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies the <see cref="T:System.Data.DataRow" /> objects whose update failure generated this exception, to the specified array of <see cref="T:System.Data.DataRow" /> objects, starting at the specified destination array index.</summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Data.DataRow" /> objects to copy the <see cref="T:System.Data.DataRow" /> objects into.</param>
		/// <param name="arrayIndex">The destination array index to start copying into.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000537 RID: 1335 RVA: 0x0001D760 File Offset: 0x0001B960
		[MonoTODO]
		public void CopyToRows(DataRow[] array, int ArrayIndex)
		{
			throw new NotImplementedException();
		}

		/// <summary>Populates the aprcified serialization information object with the data needed to serialize the <see cref="T:System.Data.DBConcurrencyException" />.</summary>
		/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized data associated with the <see cref="T:System.Data.DBConcurrencyException" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream associated with the <see cref="T:System.Data.DBConcurrencyException" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06000538 RID: 1336 RVA: 0x0001D768 File Offset: 0x0001B968
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			if (si == null)
			{
				throw new ArgumentNullException("si");
			}
			base.GetObjectData(si, context);
		}

		// Token: 0x040001A0 RID: 416
		private DataRow[] rows;
	}
}
