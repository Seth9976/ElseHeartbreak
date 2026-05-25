using System;
using System.Collections.ObjectModel;

namespace System.Net.Mail
{
	/// <summary>Stores linked resources to be sent as part of an e-mail message.</summary>
	// Token: 0x0200033A RID: 826
	public sealed class LinkedResourceCollection : Collection<LinkedResource>, IDisposable
	{
		// Token: 0x06001D48 RID: 7496 RVA: 0x00058DD4 File Offset: 0x00056FD4
		internal LinkedResourceCollection()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.LinkedResourceCollection" />.</summary>
		// Token: 0x06001D49 RID: 7497 RVA: 0x00058DDC File Offset: 0x00056FDC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x00058DEC File Offset: 0x00056FEC
		private void Dispose(bool disposing)
		{
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x00058DF0 File Offset: 0x00056FF0
		protected override void ClearItems()
		{
			base.ClearItems();
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x00058DF8 File Offset: 0x00056FF8
		protected override void InsertItem(int index, LinkedResource item)
		{
			base.InsertItem(index, item);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x00058E04 File Offset: 0x00057004
		protected override void RemoveItem(int index)
		{
			base.RemoveItem(index);
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x00058E10 File Offset: 0x00057010
		protected override void SetItem(int index, LinkedResource item)
		{
			base.SetItem(index, item);
		}
	}
}
