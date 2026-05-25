using System;
using System.Collections;
using Mono.Data.Tds.Protocol;

namespace System.Data.SqlClient
{
	/// <summary>Provides data for the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200016E RID: 366
	public sealed class SqlInfoMessageEventArgs : EventArgs
	{
		// Token: 0x06001397 RID: 5015 RVA: 0x00051BB0 File Offset: 0x0004FDB0
		internal SqlInfoMessageEventArgs(TdsInternalErrorCollection tdsErrors)
		{
			foreach (object obj in ((IEnumerable)tdsErrors))
			{
				TdsInternalError tdsInternalError = (TdsInternalError)obj;
				this.errors.Add(tdsInternalError.Class, tdsInternalError.LineNumber, tdsInternalError.Message, tdsInternalError.Number, tdsInternalError.Procedure, tdsInternalError.Server, "Mono SqlClient Data Provider", tdsInternalError.State);
			}
		}

		/// <summary>Gets the collection of warnings sent from the server.</summary>
		/// <returns>The collection of warnings sent from the server.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x00051C60 File Offset: 0x0004FE60
		public SqlErrorCollection Errors
		{
			get
			{
				return this.errors;
			}
		}

		/// <summary>Gets the full text of the error sent from the database.</summary>
		/// <returns>The full text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x00051C68 File Offset: 0x0004FE68
		public string Message
		{
			get
			{
				return this.errors[0].Message;
			}
		}

		/// <summary>Gets the name of the object that generated the error.</summary>
		/// <returns>The name of the object that generated the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x00051C7C File Offset: 0x0004FE7C
		public string Source
		{
			get
			{
				return this.errors[0].Source;
			}
		}

		/// <summary>Retrieves a string representation of the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event.</summary>
		/// <returns>A string representing the <see cref="E:System.Data.SqlClient.SqlConnection.InfoMessage" /> event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600139B RID: 5019 RVA: 0x00051C90 File Offset: 0x0004FE90
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x040007ED RID: 2029
		private SqlErrorCollection errors = new SqlErrorCollection();
	}
}
