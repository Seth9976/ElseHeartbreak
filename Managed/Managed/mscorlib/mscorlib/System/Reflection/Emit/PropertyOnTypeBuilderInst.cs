using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x020002F8 RID: 760
	internal class PropertyOnTypeBuilderInst : PropertyInfo
	{
		// Token: 0x060026F1 RID: 9969 RVA: 0x0008A9E4 File Offset: 0x00088BE4
		internal PropertyOnTypeBuilderInst(MonoGenericClass instantiation, PropertyInfo prop)
		{
			this.instantiation = instantiation;
			this.prop = prop;
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060026F2 RID: 9970 RVA: 0x0008A9FC File Offset: 0x00088BFC
		public override PropertyAttributes Attributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x0008AA04 File Offset: 0x00088C04
		public override bool CanRead
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060026F4 RID: 9972 RVA: 0x0008AA0C File Offset: 0x00088C0C
		public override bool CanWrite
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x0008AA14 File Offset: 0x00088C14
		public override Type PropertyType
		{
			get
			{
				return this.instantiation.InflateType(this.prop.PropertyType);
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x0008AA2C File Offset: 0x00088C2C
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation.InflateType(this.prop.DeclaringType);
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x0008AA44 File Offset: 0x00088C44
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x0008AA4C File Offset: 0x00088C4C
		public override string Name
		{
			get
			{
				return this.prop.Name;
			}
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x0008AA5C File Offset: 0x00088C5C
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			MethodInfo getMethod = this.GetGetMethod(nonPublic);
			MethodInfo setMethod = this.GetSetMethod(nonPublic);
			int num = 0;
			if (getMethod != null)
			{
				num++;
			}
			if (setMethod != null)
			{
				num++;
			}
			MethodInfo[] array = new MethodInfo[num];
			num = 0;
			if (getMethod != null)
			{
				array[num++] = getMethod;
			}
			if (setMethod != null)
			{
				array[num] = setMethod;
			}
			return array;
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x0008AAB4 File Offset: 0x00088CB4
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			MethodInfo methodInfo = this.prop.GetGetMethod(nonPublic);
			if (methodInfo != null && this.prop.DeclaringType == this.instantiation.generic_type)
			{
				methodInfo = TypeBuilder.GetMethod(this.instantiation, methodInfo);
			}
			return methodInfo;
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x0008AB00 File Offset: 0x00088D00
		public override ParameterInfo[] GetIndexParameters()
		{
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod != null)
			{
				return getMethod.GetParameters();
			}
			return new ParameterInfo[0];
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x0008AB28 File Offset: 0x00088D28
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			MethodInfo methodInfo = this.prop.GetSetMethod(nonPublic);
			if (methodInfo != null && this.prop.DeclaringType == this.instantiation.generic_type)
			{
				methodInfo = TypeBuilder.GetMethod(this.instantiation, methodInfo);
			}
			return methodInfo;
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x0008AB74 File Offset: 0x00088D74
		public override string ToString()
		{
			return string.Format("{0} {1}", this.PropertyType, this.Name);
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x0008AB8C File Offset: 0x00088D8C
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x0008AB94 File Offset: 0x00088D94
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x0008AB9C File Offset: 0x00088D9C
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x0008ABA4 File Offset: 0x00088DA4
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x0008ABAC File Offset: 0x00088DAC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000FAA RID: 4010
		private MonoGenericClass instantiation;

		// Token: 0x04000FAB RID: 4011
		private PropertyInfo prop;
	}
}
