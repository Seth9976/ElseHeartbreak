using System;
using System.Globalization;

namespace System.Reflection.Emit
{
	// Token: 0x020002C7 RID: 711
	internal class ConstructorOnTypeBuilderInst : ConstructorInfo
	{
		// Token: 0x060023ED RID: 9197 RVA: 0x00080C94 File Offset: 0x0007EE94
		public ConstructorOnTypeBuilderInst(MonoGenericClass instantiation, ConstructorBuilder cb)
		{
			this.instantiation = instantiation;
			this.cb = cb;
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060023EE RID: 9198 RVA: 0x00080CAC File Offset: 0x0007EEAC
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x00080CB4 File Offset: 0x0007EEB4
		public override string Name
		{
			get
			{
				return this.cb.Name;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x00080CC4 File Offset: 0x0007EEC4
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x00080CCC File Offset: 0x0007EECC
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.cb.IsDefined(attributeType, inherit);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x00080CDC File Offset: 0x0007EEDC
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.cb.GetCustomAttributes(inherit);
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x00080CEC File Offset: 0x0007EEEC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.cb.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x00080CFC File Offset: 0x0007EEFC
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.cb.GetMethodImplementationFlags();
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x00080D0C File Offset: 0x0007EF0C
		public override ParameterInfo[] GetParameters()
		{
			if (!((ModuleBuilder)this.cb.Module).assemblyb.IsCompilerContext && !this.instantiation.generic_type.is_created)
			{
				throw new NotSupportedException();
			}
			ParameterInfo[] array = new ParameterInfo[this.cb.parameters.Length];
			for (int i = 0; i < this.cb.parameters.Length; i++)
			{
				Type type = this.instantiation.InflateType(this.cb.parameters[i]);
				array[i] = new ParameterInfo((this.cb.pinfo != null) ? this.cb.pinfo[i] : null, type, this, i + 1);
			}
			return array;
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060023F6 RID: 9206 RVA: 0x00080DD0 File Offset: 0x0007EFD0
		public override int MetadataToken
		{
			get
			{
				if (!((ModuleBuilder)this.cb.Module).assemblyb.IsCompilerContext)
				{
					return base.MetadataToken;
				}
				return this.cb.MetadataToken;
			}
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x00080E10 File Offset: 0x0007F010
		internal override int GetParameterCount()
		{
			return this.cb.GetParameterCount();
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00080E20 File Offset: 0x0007F020
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.cb.Invoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060023F9 RID: 9209 RVA: 0x00080E34 File Offset: 0x0007F034
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.cb.MethodHandle;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060023FA RID: 9210 RVA: 0x00080E44 File Offset: 0x0007F044
		public override MethodAttributes Attributes
		{
			get
			{
				return this.cb.Attributes;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060023FB RID: 9211 RVA: 0x00080E54 File Offset: 0x0007F054
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.cb.CallingConvention;
			}
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x00080E64 File Offset: 0x0007F064
		public override Type[] GetGenericArguments()
		{
			return this.cb.GetGenericArguments();
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x00080E74 File Offset: 0x0007F074
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x00080E78 File Offset: 0x0007F078
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x00080E7C File Offset: 0x0007F07C
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x00080E80 File Offset: 0x0007F080
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04000DAF RID: 3503
		private MonoGenericClass instantiation;

		// Token: 0x04000DB0 RID: 3504
		private ConstructorBuilder cb;
	}
}
