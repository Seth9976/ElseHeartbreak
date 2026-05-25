using System;
using System.Globalization;
using System.Text;

namespace System.Reflection.Emit
{
	// Token: 0x020002E8 RID: 744
	internal class MethodOnTypeBuilderInst : MethodInfo
	{
		// Token: 0x0600261A RID: 9754 RVA: 0x00086CF8 File Offset: 0x00084EF8
		public MethodOnTypeBuilderInst(MonoGenericClass instantiation, MethodBuilder mb)
		{
			this.instantiation = instantiation;
			this.mb = mb;
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x00086D10 File Offset: 0x00084F10
		internal MethodOnTypeBuilderInst(MethodOnTypeBuilderInst gmd, Type[] typeArguments)
		{
			this.instantiation = gmd.instantiation;
			this.mb = gmd.mb;
			this.method_arguments = new Type[typeArguments.Length];
			typeArguments.CopyTo(this.method_arguments, 0);
			this.generic_method_definition = gmd;
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x00086D60 File Offset: 0x00084F60
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x00086D68 File Offset: 0x00084F68
		public override string Name
		{
			get
			{
				return this.mb.Name;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600261E RID: 9758 RVA: 0x00086D78 File Offset: 0x00084F78
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x00086D80 File Offset: 0x00084F80
		public override Type ReturnType
		{
			get
			{
				if (!((ModuleBuilder)this.mb.Module).assemblyb.IsCompilerContext)
				{
					return this.mb.ReturnType;
				}
				return this.instantiation.InflateType(this.mb.ReturnType, this.method_arguments);
			}
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x00086DD4 File Offset: 0x00084FD4
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x00086DDC File Offset: 0x00084FDC
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x00086DE4 File Offset: 0x00084FE4
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x00086DEC File Offset: 0x00084FEC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.ReturnType.ToString());
			stringBuilder.Append(" ");
			stringBuilder.Append(this.mb.Name);
			stringBuilder.Append("(");
			if (((ModuleBuilder)this.mb.Module).assemblyb.IsCompilerContext)
			{
				ParameterInfo[] parameters = this.GetParameters();
				for (int i = 0; i < parameters.Length; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(parameters[i].ParameterType);
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x00086EA4 File Offset: 0x000850A4
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.mb.GetMethodImplementationFlags();
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x00086EB4 File Offset: 0x000850B4
		public override ParameterInfo[] GetParameters()
		{
			if (!((ModuleBuilder)this.mb.Module).assemblyb.IsCompilerContext)
			{
				throw new NotSupportedException();
			}
			ParameterInfo[] array = new ParameterInfo[this.mb.parameters.Length];
			for (int i = 0; i < this.mb.parameters.Length; i++)
			{
				Type type = this.instantiation.InflateType(this.mb.parameters[i], this.method_arguments);
				array[i] = new ParameterInfo((this.mb.pinfo != null) ? this.mb.pinfo[i + 1] : null, type, this, i + 1);
			}
			return array;
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x00086F6C File Offset: 0x0008516C
		public override int MetadataToken
		{
			get
			{
				if (!((ModuleBuilder)this.mb.Module).assemblyb.IsCompilerContext)
				{
					return base.MetadataToken;
				}
				return this.mb.MetadataToken;
			}
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x00086FAC File Offset: 0x000851AC
		internal override int GetParameterCount()
		{
			return this.mb.GetParameterCount();
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x00086FBC File Offset: 0x000851BC
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x00086FC4 File Offset: 0x000851C4
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x00086FCC File Offset: 0x000851CC
		public override MethodAttributes Attributes
		{
			get
			{
				return this.mb.Attributes;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x00086FDC File Offset: 0x000851DC
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.mb.CallingConvention;
			}
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x00086FEC File Offset: 0x000851EC
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			if (this.mb.generic_params == null || this.method_arguments != null)
			{
				throw new NotSupportedException();
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			if (this.mb.generic_params.Length != typeArguments.Length)
			{
				throw new ArgumentException("Invalid argument array length");
			}
			return new MethodOnTypeBuilderInst(this, typeArguments);
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x00087080 File Offset: 0x00085280
		public override Type[] GetGenericArguments()
		{
			if (this.mb.generic_params == null)
			{
				return null;
			}
			Type[] array = this.method_arguments ?? this.mb.generic_params;
			Type[] array2 = new Type[array.Length];
			array.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x000870CC File Offset: 0x000852CC
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this.generic_method_definition ?? this.mb;
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x000870E4 File Offset: 0x000852E4
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.mb.generic_params == null)
				{
					throw new NotSupportedException();
				}
				if (this.method_arguments == null)
				{
					return true;
				}
				foreach (Type type in this.method_arguments)
				{
					if (type.ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x00087144 File Offset: 0x00085344
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.mb.generic_params != null && this.method_arguments == null;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x00087164 File Offset: 0x00085364
		public override bool IsGenericMethod
		{
			get
			{
				return this.mb.generic_params != null;
			}
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x00087178 File Offset: 0x00085378
		public override MethodInfo GetBaseDefinition()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x00087180 File Offset: 0x00085380
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04000E5A RID: 3674
		private MonoGenericClass instantiation;

		// Token: 0x04000E5B RID: 3675
		internal MethodBuilder mb;

		// Token: 0x04000E5C RID: 3676
		private Type[] method_arguments;

		// Token: 0x04000E5D RID: 3677
		private MethodOnTypeBuilderInst generic_method_definition;
	}
}
