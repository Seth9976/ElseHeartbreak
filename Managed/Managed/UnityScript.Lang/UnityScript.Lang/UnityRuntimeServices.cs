using System;
using System.Collections;
using Boo.Lang;
using Boo.Lang.Runtime;

namespace UnityScript.Lang
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	public class UnityRuntimeServices
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00002AA0 File Offset: 0x00000CA0
		static UnityRuntimeServices()
		{
			UnityRuntimeServices.$static_initializer$();
			RuntimeServices.RegisterExtensions(typeof(Extensions));
			UnityRuntimeServices.Initialized = true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public static object Invoke(object target, string name, object[] args, Type scriptBaseType)
		{
			if (!UnityRuntimeServices.Initialized)
			{
				throw new AssertionFailedException("Initialized");
			}
			object obj = RuntimeServices.Invoke(target, name, args);
			return (obj != null) ? (UnityRuntimeServices.IsGenerator(obj) ? (target.GetType().IsSubclassOf(scriptBaseType) ? ((!UnityRuntimeServices.IsStaticMethod(target.GetType(), name, args)) ? RuntimeServices.Invoke(target, "StartCoroutine_Auto", new object[] { obj }) : obj) : obj) : obj) : null;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002B50 File Offset: 0x00000D50
		public static object GetProperty(object target, string name)
		{
			if (!UnityRuntimeServices.Initialized)
			{
				throw new AssertionFailedException("Initialized");
			}
			object obj;
			try
			{
				obj = RuntimeServices.GetProperty(target, name);
			}
			catch (MissingMemberException ex)
			{
				if (target.GetType().IsValueType)
				{
					throw;
				}
				obj = ExpandoServices.GetExpandoProperty(target, name);
			}
			return obj;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public static object SetProperty(object target, string name, object value)
		{
			if (!UnityRuntimeServices.Initialized)
			{
				throw new AssertionFailedException("Initialized");
			}
			object obj;
			try
			{
				obj = RuntimeServices.SetProperty(target, name, value);
			}
			catch (MissingMemberException ex)
			{
				if (target.GetType().IsValueType)
				{
					throw;
				}
				obj = ExpandoServices.SetExpandoProperty(target, name, value);
			}
			return obj;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002C40 File Offset: 0x00000E40
		public static Type GetTypeOf(object o)
		{
			return (o != null) ? o.GetType() : null;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002C54 File Offset: 0x00000E54
		public static bool IsGenerator(object obj)
		{
			Type type = obj.GetType();
			return type == UnityRuntimeServices.EnumeratorType || UnityRuntimeServices.EnumeratorType.IsAssignableFrom(type) || typeof(AbstractGenerator).IsAssignableFrom(type);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public static bool IsStaticMethod(Type type, string name, object[] args)
		{
			bool flag;
			try
			{
				flag = type.GetMethod(name).IsStatic;
			}
			catch (Exception ex)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public static IEnumerator GetEnumerator(object obj)
		{
			IEnumerator enumerator;
			if (obj == null)
			{
				enumerator = UnityRuntimeServices.EmptyEnumerator;
			}
			else if (UnityRuntimeServices.IsValueTypeArray(obj) || obj is Array)
			{
				object obj2 = obj;
				if (!(obj is IList))
				{
					obj2 = RuntimeServices.Coerce(obj, typeof(IList));
				}
				enumerator = new ListUpdateableEnumerator((IList)obj2);
			}
			else
			{
				IEnumerable enumerable = obj as IEnumerable;
				if (enumerable != null)
				{
					enumerator = enumerable.GetEnumerator();
				}
				else
				{
					IEnumerator enumerator2 = obj as IEnumerator;
					enumerator = ((enumerator2 == null) ? RuntimeServices.GetEnumerable(obj).GetEnumerator() : enumerator2);
				}
			}
			return enumerator;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002D84 File Offset: 0x00000F84
		public static void Update(IEnumerator e, object newValue)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (e is ListUpdateableEnumerator)
			{
				((ListUpdateableEnumerator)e).Update(newValue);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public static bool IsValueTypeArray(object obj)
		{
			return obj is Array && obj.GetType().GetElementType().IsValueType;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public static void PropagateValueTypeChanges(UnityRuntimeServices.ValueTypeChange[] changes)
		{
			int i = 0;
			int length = changes.Length;
			checked
			{
				while (i < length)
				{
					if (!changes[i].Propagate())
					{
						break;
					}
					i++;
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002E14 File Offset: 0x00001014
		private static void $static_initializer$()
		{
			UnityRuntimeServices.EmptyEnumerator = new object[0].GetEnumerator();
			UnityRuntimeServices.EnumeratorType = typeof(IEnumerator);
		}

		// Token: 0x04000009 RID: 9
		[NonSerialized]
		public static IEnumerator EmptyEnumerator;

		// Token: 0x0400000A RID: 10
		[NonSerialized]
		protected static Type EnumeratorType;

		// Token: 0x0400000B RID: 11
		[NonSerialized]
		public static readonly bool Initialized;

		// Token: 0x0200000D RID: 13
		[Serializable]
		public abstract class ValueTypeChange
		{
			// Token: 0x0600006E RID: 110 RVA: 0x00002E38 File Offset: 0x00001038
			public ValueTypeChange(object target, object value)
			{
				this.Target = target;
				this.Value = value;
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600006F RID: 111 RVA: 0x00002E50 File Offset: 0x00001050
			public bool IsValid
			{
				get
				{
					return this.Value is ValueType;
				}
			}

			// Token: 0x06000070 RID: 112
			public abstract override bool Propagate();

			// Token: 0x0400000C RID: 12
			public object Target;

			// Token: 0x0400000D RID: 13
			public object Value;
		}

		// Token: 0x0200000E RID: 14
		[Serializable]
		public class MemberValueTypeChange : UnityRuntimeServices.ValueTypeChange
		{
			// Token: 0x06000071 RID: 113 RVA: 0x00002E60 File Offset: 0x00001060
			public MemberValueTypeChange(object target, string member, object value)
				: base(target, value)
			{
				this.Member = member;
			}

			// Token: 0x06000072 RID: 114 RVA: 0x00002E74 File Offset: 0x00001074
			public override bool Propagate()
			{
				bool flag;
				if (!this.IsValid)
				{
					flag = false;
				}
				else
				{
					bool flag2;
					try
					{
						RuntimeServices.SetProperty(this.Target, this.Member, this.Value);
					}
					catch (MissingFieldException ex)
					{
						flag2 = false;
						goto IL_003E;
					}
					return true;
					IL_003E:
					flag = flag2;
				}
				return flag;
			}

			// Token: 0x0400000E RID: 14
			public string Member;
		}

		// Token: 0x0200000F RID: 15
		[Serializable]
		public class SliceValueTypeChange : UnityRuntimeServices.ValueTypeChange
		{
			// Token: 0x06000073 RID: 115 RVA: 0x00002EDC File Offset: 0x000010DC
			public SliceValueTypeChange(object target, object index, object value)
				: base(target, value)
			{
				this.Index = index;
			}

			// Token: 0x06000074 RID: 116 RVA: 0x00002EF0 File Offset: 0x000010F0
			public override bool Propagate()
			{
				bool flag;
				if (!this.IsValid)
				{
					flag = false;
				}
				else
				{
					IList list = this.Target as IList;
					if (list != null)
					{
						list[RuntimeServices.UnboxInt32(this.Index)] = this.Value;
						flag = true;
					}
					else
					{
						bool flag2;
						try
						{
							RuntimeServices.SetSlice(this.Target, string.Empty, new object[] { this.Index, this.Value });
						}
						catch (MissingFieldException ex)
						{
							flag2 = false;
							goto IL_007E;
						}
						return true;
						IL_007E:
						flag = flag2;
					}
				}
				return flag;
			}

			// Token: 0x0400000F RID: 15
			public object Index;
		}
	}
}
