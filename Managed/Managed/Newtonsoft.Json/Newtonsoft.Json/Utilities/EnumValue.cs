using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000B3 RID: 179
	internal class EnumValue<T> where T : struct
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0001D9EC File Offset: 0x0001BBEC
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0001D9F4 File Offset: 0x0001BBF4
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001D9FC File Offset: 0x0001BBFC
		public EnumValue(string name, T value)
		{
			this._name = name;
			this._value = value;
		}

		// Token: 0x0400027C RID: 636
		private string _name;

		// Token: 0x0400027D RID: 637
		private T _value;
	}
}
