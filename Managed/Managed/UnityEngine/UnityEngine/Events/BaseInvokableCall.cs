using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000056 RID: 86
	internal abstract class BaseInvokableCall
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x000084F4 File Offset: 0x000066F4
		protected BaseInvokableCall()
		{
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000084FC File Offset: 0x000066FC
		protected BaseInvokableCall(object target, MethodInfo function)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
		}

		// Token: 0x060001C2 RID: 450
		public abstract void Invoke(object[] args);

		// Token: 0x060001C3 RID: 451 RVA: 0x00008534 File Offset: 0x00006734
		protected static void ThrowOnInvalidArg<T>(object arg)
		{
			if (arg != null && !(arg is T))
			{
				throw new ArgumentException(UnityString.Format("Passed argument 'args[0]' is of the wrong type. Type:{0} Expected:{1}", new object[]
				{
					arg.GetType(),
					typeof(T)
				}));
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008574 File Offset: 0x00006774
		protected static bool AllowInvoke(Delegate @delegate)
		{
			return @delegate.Method.IsStatic || @delegate.Target != null;
		}

		// Token: 0x060001C5 RID: 453
		public abstract bool Find(object targetObj, MethodInfo method);
	}
}
