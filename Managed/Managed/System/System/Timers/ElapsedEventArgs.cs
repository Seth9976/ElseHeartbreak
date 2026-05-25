using System;

namespace System.Timers
{
	/// <summary>Provides data for the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
	// Token: 0x020004AD RID: 1197
	public class ElapsedEventArgs : EventArgs
	{
		// Token: 0x06002AF7 RID: 10999 RVA: 0x00093A58 File Offset: 0x00091C58
		internal ElapsedEventArgs(DateTime time)
		{
			this.time = time;
		}

		/// <summary>Gets the time the <see cref="E:System.Timers.Timer.Elapsed" /> event was raised.</summary>
		/// <returns>The time the <see cref="E:System.Timers.Timer.Elapsed" /> event was raised.</returns>
		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x00093A68 File Offset: 0x00091C68
		public DateTime SignalTime
		{
			get
			{
				return this.time;
			}
		}

		// Token: 0x04001B1E RID: 6942
		private DateTime time;
	}
}
