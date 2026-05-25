using System;

namespace System.Runtime.Serialization
{
	// Token: 0x02000020 RID: 32
	internal static class TypeExtensions
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public static T GetCustomAttribute<T>(this Type type, bool inherit)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(T), inherit);
			return (customAttributes == null || customAttributes.Length != 1) ? default(T) : ((T)((object)customAttributes[0]));
		}
	}
}
