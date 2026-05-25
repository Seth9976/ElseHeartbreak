using System;
using System.Collections.Generic;

namespace UnityScript.Lang
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	internal class Expando
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000272C File Offset: 0x0000092C
		public Expando(object target)
		{
			this._attributes = new Dictionary<string, object>();
			this._target = new WeakReference(target);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000274C File Offset: 0x0000094C
		public object Target
		{
			get
			{
				return this._target.Target;
			}
		}

		// Token: 0x17000004 RID: 4
		public object this[string key]
		{
			get
			{
				return this._attributes[key];
			}
			set
			{
				if (value == null)
				{
					this._attributes.Remove(key);
				}
				else
				{
					this._attributes[key] = value;
				}
			}
		}

		// Token: 0x04000002 RID: 2
		protected WeakReference _target;

		// Token: 0x04000003 RID: 3
		protected Dictionary<string, object> _attributes;
	}
}
