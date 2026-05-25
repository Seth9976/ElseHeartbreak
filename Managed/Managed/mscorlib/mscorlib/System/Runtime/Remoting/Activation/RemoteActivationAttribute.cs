using System;
using System.Collections;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000442 RID: 1090
	internal class RemoteActivationAttribute : Attribute, IContextAttribute
	{
		// Token: 0x06002E08 RID: 11784 RVA: 0x00099520 File Offset: 0x00097720
		public RemoteActivationAttribute()
		{
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x00099528 File Offset: 0x00097728
		public RemoteActivationAttribute(IList contextProperties)
		{
			this._contextProperties = contextProperties;
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x00099538 File Offset: 0x00097738
		public bool IsContextOK(Context ctx, IConstructionCallMessage ctor)
		{
			return false;
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x0009953C File Offset: 0x0009773C
		public void GetPropertiesForNewContext(IConstructionCallMessage ctor)
		{
			if (this._contextProperties != null)
			{
				foreach (object obj in this._contextProperties)
				{
					ctor.ContextProperties.Add(obj);
				}
			}
		}

		// Token: 0x040013C3 RID: 5059
		private IList _contextProperties;
	}
}
