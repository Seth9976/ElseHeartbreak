using System;
using System.Globalization;
using System.Reflection;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000A1 RID: 161
	internal abstract class ReflectionDelegateFactory
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x0001B568 File Offset: 0x00019768
		public Func<T, object> CreateGet<T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				return this.CreateGet<T>(propertyInfo);
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			if (fieldInfo != null)
			{
				return this.CreateGet<T>(fieldInfo);
			}
			throw new Exception("Could not create getter for {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { memberInfo }));
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001B5BC File Offset: 0x000197BC
		public Action<T, object> CreateSet<T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				return this.CreateSet<T>(propertyInfo);
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			if (fieldInfo != null)
			{
				return this.CreateSet<T>(fieldInfo);
			}
			throw new Exception("Could not create setter for {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { memberInfo }));
		}

		// Token: 0x06000788 RID: 1928
		public abstract MethodCall<T, object> CreateMethodCall<T>(MethodBase method);

		// Token: 0x06000789 RID: 1929
		public abstract Func<T> CreateDefaultConstructor<T>(Type type);

		// Token: 0x0600078A RID: 1930
		public abstract Func<T, object> CreateGet<T>(PropertyInfo propertyInfo);

		// Token: 0x0600078B RID: 1931
		public abstract Func<T, object> CreateGet<T>(FieldInfo fieldInfo);

		// Token: 0x0600078C RID: 1932
		public abstract Action<T, object> CreateSet<T>(FieldInfo fieldInfo);

		// Token: 0x0600078D RID: 1933
		public abstract Action<T, object> CreateSet<T>(PropertyInfo propertyInfo);
	}
}
