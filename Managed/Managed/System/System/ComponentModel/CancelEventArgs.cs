using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for a cancelable event.</summary>
	// Token: 0x020000D5 RID: 213
	public class CancelEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to false.</summary>
		// Token: 0x06000933 RID: 2355 RVA: 0x0001AAB8 File Offset: 0x00018CB8
		public CancelEventArgs()
		{
			this.cancel = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CancelEventArgs" /> class with the <see cref="P:System.ComponentModel.CancelEventArgs.Cancel" /> property set to the given value.</summary>
		/// <param name="cancel">true to cancel the event; otherwise, false. </param>
		// Token: 0x06000934 RID: 2356 RVA: 0x0001AAC8 File Offset: 0x00018CC8
		public CancelEventArgs(bool cancel)
		{
			this.cancel = cancel;
		}

		/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
		/// <returns>true if the event should be canceled; otherwise, false.</returns>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0001AAD8 File Offset: 0x00018CD8
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x0001AAE0 File Offset: 0x00018CE0
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}

		// Token: 0x04000267 RID: 615
		private bool cancel;
	}
}
