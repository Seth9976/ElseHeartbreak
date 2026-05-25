using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI
{
	// Token: 0x0200008D RID: 141
	internal class ObjectPool<T> where T : new()
	{
		// Token: 0x060004C4 RID: 1220 RVA: 0x00013CA0 File Offset: 0x00011EA0
		public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00013CC4 File Offset: 0x00011EC4
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x00013CCC File Offset: 0x00011ECC
		public int countAll { get; private set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00013CD8 File Offset: 0x00011ED8
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00013CE8 File Offset: 0x00011EE8
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00013CF8 File Offset: 0x00011EF8
		public T Get()
		{
			T t;
			if (this.m_Stack.Count == 0)
			{
				t = ((default(T) == null) ? new T() : default(T));
				this.countAll++;
			}
			else
			{
				t = this.m_Stack.Pop();
			}
			if (this.m_ActionOnGet != null)
			{
				this.m_ActionOnGet(t);
			}
			return t;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00013D70 File Offset: 0x00011F70
		public void Release(T element)
		{
			if (this.m_Stack.Count > 0 && object.ReferenceEquals(this.m_Stack.Peek(), element))
			{
				Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
			}
			if (this.m_ActionOnRelease != null)
			{
				this.m_ActionOnRelease(element);
			}
			this.m_Stack.Push(element);
		}

		// Token: 0x0400024B RID: 587
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x0400024C RID: 588
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x0400024D RID: 589
		private readonly UnityAction<T> m_ActionOnRelease;
	}
}
