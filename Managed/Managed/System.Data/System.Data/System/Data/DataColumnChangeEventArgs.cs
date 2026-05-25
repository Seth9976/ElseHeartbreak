using System;

namespace System.Data
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataTable.ColumnChanging" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200001D RID: 29
	public class DataColumnChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataColumnChangeEventArgs" /> class.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> of the column with the changing value. </param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> with the changing value. </param>
		/// <param name="value">The new value. </param>
		// Token: 0x06000157 RID: 343 RVA: 0x0000AC0C File Offset: 0x00008E0C
		public DataColumnChangeEventArgs(DataRow row, DataColumn column, object value)
		{
			this.Initialize(row, column, value);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000AC20 File Offset: 0x00008E20
		internal DataColumnChangeEventArgs()
		{
		}

		/// <summary>Gets the <see cref="T:System.Data.DataColumn" /> with a changing value.</summary>
		/// <returns>The <see cref="T:System.Data.DataColumn" /> with a changing value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000AC28 File Offset: 0x00008E28
		public DataColumn Column
		{
			get
			{
				return this._column;
			}
		}

		/// <summary>Gets or sets the proposed new value for the column.</summary>
		/// <returns>The proposed value, of type <see cref="T:System.Object" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000AC30 File Offset: 0x00008E30
		// (set) Token: 0x0600015B RID: 347 RVA: 0x0000AC38 File Offset: 0x00008E38
		public object ProposedValue
		{
			get
			{
				return this._proposedValue;
			}
			set
			{
				this._proposedValue = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataRow" /> of the column with a changing value.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> of the column with a changing value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000AC44 File Offset: 0x00008E44
		public DataRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000AC4C File Offset: 0x00008E4C
		internal void Initialize(DataRow row, DataColumn column, object value)
		{
			this._column = column;
			this._row = row;
			this._proposedValue = value;
		}

		// Token: 0x040000B9 RID: 185
		private DataColumn _column;

		// Token: 0x040000BA RID: 186
		private DataRow _row;

		// Token: 0x040000BB RID: 187
		private object _proposedValue;
	}
}
