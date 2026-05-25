using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000A0 RID: 160
	internal static class TypeExtensions
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x0001B194 File Offset: 0x00019394
		public static MethodInfo GetGenericMethod(this Type type, string name, params Type[] parameterTypes)
		{
			IEnumerable<MethodInfo> enumerable = from method in type.GetMethods()
				where method.Name == name
				select method;
			foreach (MethodInfo methodInfo in enumerable)
			{
				if (methodInfo.HasParameters(parameterTypes))
				{
					return methodInfo;
				}
			}
			return null;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001B218 File Offset: 0x00019418
		public static bool HasParameters(this MethodInfo method, params Type[] parameterTypes)
		{
			Type[] array = (from parameter in method.GetParameters()
				select parameter.ParameterType).ToArray<Type>();
			if (array.Length != parameterTypes.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].ToString() != parameterTypes[i].ToString())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001B4E0 File Offset: 0x000196E0
		public static IEnumerable<Type> AllInterfaces(this Type target)
		{
			foreach (Type IF in target.GetInterfaces())
			{
				yield return IF;
				foreach (Type childIF in IF.AllInterfaces())
				{
					yield return childIF;
				}
			}
			yield break;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001B508 File Offset: 0x00019708
		public static IEnumerable<MethodInfo> AllMethods(this Type target)
		{
			List<Type> list = target.AllInterfaces().ToList<Type>();
			list.Add(target);
			return from type in list
				from method in type.GetMethods()
				select method;
		}
	}
}
