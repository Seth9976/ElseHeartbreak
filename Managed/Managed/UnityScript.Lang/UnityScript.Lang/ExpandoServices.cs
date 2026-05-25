using System;
using System.Collections.Generic;
using CompilerGenerated;

namespace UnityScript.Lang
{
	// Token: 0x02000006 RID: 6
	public static class ExpandoServices
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000027A0 File Offset: 0x000009A0
		public static object GetExpandoProperty(object target, string name)
		{
			Expando expandoFor = ExpandoServices.GetExpandoFor(target);
			return (expandoFor != null) ? expandoFor[name] : null;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000027C8 File Offset: 0x000009C8
		public static object SetExpandoProperty(object target, string name, object value)
		{
			Expando orCreateExpandoFor = ExpandoServices.GetOrCreateExpandoFor(target);
			orCreateExpandoFor[name] = value;
			return value;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000027E8 File Offset: 0x000009E8
		public static Expando GetExpandoFor(object o)
		{
			ExpandoServices.$GetExpandoFor$locals$15 $GetExpandoFor$locals$ = new ExpandoServices.$GetExpandoFor$locals$15();
			$GetExpandoFor$locals$.$o = o;
			List<Expando> expandos = ExpandoServices._expandos;
			Expando expando;
			lock (expandos)
			{
				ExpandoServices.Purge();
				expando = ExpandoServices._expandos.Find($adaptor$__ExpandoServices$callable0$60_29__$Predicate$0.Adapt(new __ExpandoServices$callable0$60_29__(new ExpandoServices.$GetExpandoFor$closure$4($GetExpandoFor$locals$).Invoke)));
			}
			return expando;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002864 File Offset: 0x00000A64
		public static Expando GetOrCreateExpandoFor(object o)
		{
			List<Expando> expandos = ExpandoServices._expandos;
			Expando expando2;
			lock (expandos)
			{
				Expando expando = ExpandoServices.GetExpandoFor(o);
				if (expando == null)
				{
					expando = new Expando(o);
					ExpandoServices._expandos.Add(expando);
				}
				expando2 = expando;
			}
			return expando2;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000028CC File Offset: 0x00000ACC
		public static int ExpandoObjectCount
		{
			get
			{
				ExpandoServices.Purge();
				return ExpandoServices._expandos.Count;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000028E0 File Offset: 0x00000AE0
		public static void Purge()
		{
			List<Expando> expandos = ExpandoServices._expandos;
			lock (expandos)
			{
				ExpandoServices._expandos.RemoveAll($adaptor$__ExpandoServices$callable0$60_29__$Predicate$0.Adapt(new __ExpandoServices$callable0$60_29__(ExpandoServices.$Purge$closure$5)));
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002940 File Offset: 0x00000B40
		internal static bool $Purge$closure$5(Expando e)
		{
			return e.Target == null;
		}

		// Token: 0x04000004 RID: 4
		[NonSerialized]
		private static List<Expando> _expandos = new List<Expando>();

		// Token: 0x02000007 RID: 7
		[Serializable]
		internal class $GetExpandoFor$locals$15
		{
			// Token: 0x04000005 RID: 5
			internal object $o;
		}

		// Token: 0x02000008 RID: 8
		[Serializable]
		internal class $GetExpandoFor$closure$4
		{
			// Token: 0x0600004A RID: 74 RVA: 0x00002954 File Offset: 0x00000B54
			public $GetExpandoFor$closure$4(ExpandoServices.$GetExpandoFor$locals$15 $$locals$16)
			{
				this.$$locals$16 = $$locals$16;
			}

			// Token: 0x0600004B RID: 75 RVA: 0x00002964 File Offset: 0x00000B64
			public bool Invoke(Expando e)
			{
				return e.Target == this.$$locals$16.$o;
			}

			// Token: 0x04000006 RID: 6
			internal ExpandoServices.$GetExpandoFor$locals$15 $$locals$16;
		}
	}
}
