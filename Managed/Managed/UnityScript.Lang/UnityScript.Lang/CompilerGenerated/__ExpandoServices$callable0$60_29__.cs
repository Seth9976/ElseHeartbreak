using System;
using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Runtime;
using UnityScript.Lang;

namespace CompilerGenerated
{
	// Token: 0x02000010 RID: 16
	[CompilerGenerated]
	[Serializable]
	public sealed class __ExpandoServices$callable0$60_29__ : MulticastDelegate, ICallable
	{
		// Token: 0x06000075 RID: 117
		public extern __ExpandoServices$callable0$60_29__(object instance, IntPtr method);

		// Token: 0x06000076 RID: 118 RVA: 0x00002F98 File Offset: 0x00001198
		public override object Call(object[] args)
		{
			object obj2;
			object obj = (obj2 = args[0]);
			if (!(obj is Expando))
			{
				obj2 = RuntimeServices.Coerce(obj, typeof(Expando));
			}
			return this((Expando)obj2);
		}

		// Token: 0x06000077 RID: 119
		public override extern bool Invoke(Expando e);

		// Token: 0x06000078 RID: 120
		public override extern IAsyncResult BeginInvoke(Expando e, AsyncCallback callback, object asyncState);

		// Token: 0x06000079 RID: 121
		public override extern bool EndInvoke(IAsyncResult result);
	}
}
