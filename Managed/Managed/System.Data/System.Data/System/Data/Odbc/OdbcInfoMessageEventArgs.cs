using System;

namespace System.Data.Odbc
{
	/// <summary>Provides data for the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000140 RID: 320
	public sealed class OdbcInfoMessageEventArgs : EventArgs
	{
		// Token: 0x06001137 RID: 4407 RVA: 0x00043420 File Offset: 0x00041620
		internal OdbcInfoMessageEventArgs(OdbcErrorCollection errors)
		{
			foreach (object obj in errors)
			{
				OdbcError odbcError = (OdbcError)obj;
				this.errors.Add(odbcError);
			}
		}

		/// <summary>Gets the collection of warnings sent from the data source.</summary>
		/// <returns>The collection of warnings sent from the data source.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x000434A0 File Offset: 0x000416A0
		public OdbcErrorCollection Errors
		{
			get
			{
				return this.errors;
			}
		}

		/// <summary>Gets the full text of the error sent from the database.</summary>
		/// <returns>The full text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x000434A8 File Offset: 0x000416A8
		public string Message
		{
			get
			{
				return this.errors[0].Message;
			}
		}

		/// <summary>Retrieves a string representation of the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event.</summary>
		/// <returns>A string representing the <see cref="E:System.Data.Odbc.OdbcConnection.InfoMessage" /> event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600113A RID: 4410 RVA: 0x000434BC File Offset: 0x000416BC
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x04000667 RID: 1639
		private OdbcErrorCollection errors = new OdbcErrorCollection();
	}
}
