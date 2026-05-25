using System;

namespace System.Data.Odbc
{
	/// <summary>Collects information relevant to a warning or error returned by the data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000122 RID: 290
	[Serializable]
	public sealed class OdbcError
	{
		// Token: 0x06001067 RID: 4199 RVA: 0x00040B74 File Offset: 0x0003ED74
		internal OdbcError(OdbcConnection connection)
		{
			this._nativeerror = 1;
			this._source = connection.SafeDriver;
			this._message = "Error in " + this._source;
			this._state = string.Empty;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00040BBC File Offset: 0x0003EDBC
		internal OdbcError(string message, string state, int nativeerror)
		{
			this._message = message;
			this._state = state;
			this._nativeerror = nativeerror;
		}

		/// <summary>Gets a short description of the error.</summary>
		/// <returns>A description of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00040BDC File Offset: 0x0003EDDC
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		/// <summary>Gets the data source-specific error information.</summary>
		/// <returns>The data source-specific error information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x00040BE4 File Offset: 0x0003EDE4
		public int NativeError
		{
			get
			{
				return this._nativeerror;
			}
		}

		/// <summary>Gets the name of the driver that generated the error.</summary>
		/// <returns>The name of the driver that generated the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00040BEC File Offset: 0x0003EDEC
		public string Source
		{
			get
			{
				return this._source;
			}
		}

		/// <summary>Gets the five-character error code that follows the ANSI SQL standard for the database.</summary>
		/// <returns>The five-character error code, which identifies the source of the error if the error can be issued from more than one place.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x00040BF4 File Offset: 0x0003EDF4
		public string SQLState
		{
			get
			{
				return this._state;
			}
		}

		/// <summary>Gets the complete text of the error message.</summary>
		/// <returns>The complete text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600106D RID: 4205 RVA: 0x00040BFC File Offset: 0x0003EDFC
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00040C04 File Offset: 0x0003EE04
		internal void SetSource(string source)
		{
			this._source = source;
		}

		// Token: 0x04000567 RID: 1383
		private readonly string _message;

		// Token: 0x04000568 RID: 1384
		private string _source;

		// Token: 0x04000569 RID: 1385
		private readonly string _state;

		// Token: 0x0400056A RID: 1386
		private readonly int _nativeerror;
	}
}
