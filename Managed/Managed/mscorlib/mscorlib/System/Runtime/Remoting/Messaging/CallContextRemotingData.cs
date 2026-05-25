using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004A4 RID: 1188
	[Serializable]
	internal class CallContextRemotingData : ICloneable
	{
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x0600300D RID: 12301 RVA: 0x0009E1B4 File Offset: 0x0009C3B4
		// (set) Token: 0x0600300E RID: 12302 RVA: 0x0009E1BC File Offset: 0x0009C3BC
		public string LogicalCallID
		{
			get
			{
				return this._logicalCallID;
			}
			set
			{
				this._logicalCallID = value;
			}
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x0009E1C8 File Offset: 0x0009C3C8
		public object Clone()
		{
			return new CallContextRemotingData
			{
				_logicalCallID = this._logicalCallID
			};
		}

		// Token: 0x04001460 RID: 5216
		private string _logicalCallID;
	}
}
