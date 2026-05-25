using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000041 RID: 65
	internal static class Extensions
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x00013620 File Offset: 0x00011820
		public static bool IsGenericInstanceOf(this Type self, Type type)
		{
			return self.IsGenericType && self.GetGenericTypeDefinition() == type;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00013638 File Offset: 0x00011838
		public static bool IsNullable(this Type self)
		{
			return self.IsValueType && self.IsGenericInstanceOf(typeof(Nullable<>));
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00013658 File Offset: 0x00011858
		public static bool IsExpression(this Type self)
		{
			return self == typeof(Expression) || self.IsSubclassOf(typeof(Expression));
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00013680 File Offset: 0x00011880
		public static bool IsGenericImplementationOf(this Type self, Type type)
		{
			foreach (Type type2 in self.GetInterfaces())
			{
				if (type2.IsGenericInstanceOf(type))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000136BC File Offset: 0x000118BC
		public static bool IsAssignableTo(this Type self, Type type)
		{
			return type.IsAssignableFrom(self) || Extensions.ArrayTypeIsAssignableTo(self, type);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000136D4 File Offset: 0x000118D4
		public static Type GetFirstGenericArgument(this Type self)
		{
			return self.GetGenericArguments()[0];
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000136E0 File Offset: 0x000118E0
		public static Type MakeGenericTypeFrom(this Type self, Type type)
		{
			return self.MakeGenericType(type.GetGenericArguments());
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000136F0 File Offset: 0x000118F0
		public static Type MakeNullableType(this Type self)
		{
			return typeof(Nullable<>).MakeGenericType(new Type[] { self });
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001370C File Offset: 0x0001190C
		public static Type GetNotNullableType(this Type self)
		{
			return (!self.IsNullable()) ? self : self.GetFirstGenericArgument();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00013728 File Offset: 0x00011928
		public static MethodInfo GetInvokeMethod(this Type self)
		{
			return self.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00013738 File Offset: 0x00011938
		public static MethodInfo MakeGenericMethodFrom(this MethodInfo self, MethodInfo method)
		{
			return self.MakeGenericMethod(method.GetGenericArguments());
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00013748 File Offset: 0x00011948
		public static Type[] GetParameterTypes(this MethodBase self)
		{
			ParameterInfo[] parameters = self.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			return array;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00013788 File Offset: 0x00011988
		private static bool ArrayTypeIsAssignableTo(Type type, Type candidate)
		{
			return type.IsArray && candidate.IsArray && type.GetArrayRank() == candidate.GetArrayRank() && type.GetElementType().IsAssignableTo(candidate.GetElementType());
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000137D4 File Offset: 0x000119D4
		public static void OnFieldOrProperty(this MemberInfo self, Action<FieldInfo> onfield, Action<PropertyInfo> onprop)
		{
			MemberTypes memberType = self.MemberType;
			if (memberType == MemberTypes.Field)
			{
				onfield((FieldInfo)self);
				return;
			}
			if (memberType != MemberTypes.Property)
			{
				throw new ArgumentException();
			}
			onprop((PropertyInfo)self);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001381C File Offset: 0x00011A1C
		public static T OnFieldOrProperty<T>(this MemberInfo self, Func<FieldInfo, T> onfield, Func<PropertyInfo, T> onprop)
		{
			MemberTypes memberType = self.MemberType;
			if (memberType == MemberTypes.Field)
			{
				return onfield((FieldInfo)self);
			}
			if (memberType != MemberTypes.Property)
			{
				throw new ArgumentException();
			}
			return onprop((PropertyInfo)self);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00013864 File Offset: 0x00011A64
		public static Type MakeStrongBoxType(this Type self)
		{
			return typeof(StrongBox<>).MakeGenericType(new Type[] { self });
		}
	}
}
