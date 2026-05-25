using System;

namespace System.Data.OleDb
{
	/// <summary>Collects information relevant to a warning or error returned by the data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EF RID: 239
	[Serializable]
	public sealed class OleDbError
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x00033360 File Offset: 0x00031560
		internal OleDbError(string msg, int code, string source, string sql)
		{
			this.message = msg;
			this.nativeError = code;
			this.source = source;
			this.sqlState = sql;
		}

		/// <summary>Gets a short description of the error.</summary>
		/// <returns>A short description of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00033388 File Offset: 0x00031588
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		/// <summary>Gets the database-specific error information.</summary>
		/// <returns>The database-specific error information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x00033390 File Offset: 0x00031590
		public int NativeError
		{
			get
			{
				return this.nativeError;
			}
		}

		/// <summary>Gets the name of the provider that generated the error.</summary>
		/// <returns>The name of the provider that generated the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x00033398 File Offset: 0x00031598
		public string Source
		{
			get
			{
				return this.source;
			}
		}

		/// <summary>Gets the five-character error code following the ANSI SQL standard for the database.</summary>
		/// <returns>The five-character error code, which identifies the source of the error, if the error can be issued from more than one place.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x000333A0 File Offset: 0x000315A0
		public string SQLState
		{
			get
			{
				return this.sqlState;
			}
		}

		/// <summary>Gets the complete text of the error message.</summary>
		/// <returns>The complete text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BB0 RID: 2992 RVA: 0x000333A8 File Offset: 0x000315A8
		[MonoTODO]
		public override string ToString()
		{
			string text = " <Stack Trace>";
			return "OleDbError:" + this.message + text;
		}

		// Token: 0x04000437 RID: 1079
		private string message;

		// Token: 0x04000438 RID: 1080
		private int nativeError;

		// Token: 0x04000439 RID: 1081
		private string source;

		// Token: 0x0400043A RID: 1082
		private string sqlState;
	}
}
