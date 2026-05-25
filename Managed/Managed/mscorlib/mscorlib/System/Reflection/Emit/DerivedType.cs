using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020002CB RID: 715
	internal abstract class DerivedType : Type
	{
		// Token: 0x06002417 RID: 9239 RVA: 0x00081D38 File Offset: 0x0007FF38
		internal DerivedType(Type elementType)
		{
			this.elementType = elementType;
		}

		// Token: 0x06002418 RID: 9240
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void create_unmanaged_type(Type type);

		// Token: 0x06002419 RID: 9241
		internal abstract string FormatName(string elementName);

		// Token: 0x0600241A RID: 9242 RVA: 0x00081D48 File Offset: 0x0007FF48
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x00081D50 File Offset: 0x0007FF50
		public override Type[] GetInterfaces()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x00081D58 File Offset: 0x0007FF58
		public override Type GetElementType()
		{
			return this.elementType;
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x00081D60 File Offset: 0x0007FF60
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x00081D68 File Offset: 0x0007FF68
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x00081D70 File Offset: 0x0007FF70
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x00081D78 File Offset: 0x0007FF78
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x00081D80 File Offset: 0x0007FF80
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x00081D88 File Offset: 0x0007FF88
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x00081D90 File Offset: 0x0007FF90
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00081D98 File Offset: 0x0007FF98
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x00081DA0 File Offset: 0x0007FFA0
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x00081DA8 File Offset: 0x0007FFA8
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x00081DB0 File Offset: 0x0007FFB0
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x00081DB8 File Offset: 0x0007FFB8
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x00081DC0 File Offset: 0x0007FFC0
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.elementType.Attributes;
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00081DD0 File Offset: 0x0007FFD0
		protected override bool HasElementTypeImpl()
		{
			return true;
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x00081DD4 File Offset: 0x0007FFD4
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x00081DD8 File Offset: 0x0007FFD8
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x00081DDC File Offset: 0x0007FFDC
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x00081DE0 File Offset: 0x0007FFE0
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x00081DE4 File Offset: 0x0007FFE4
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x00081DE8 File Offset: 0x0007FFE8
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x00081DF0 File Offset: 0x0007FFF0
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x00081DF8 File Offset: 0x0007FFF8
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00081E00 File Offset: 0x00080000
		public override bool IsInstanceOfType(object o)
		{
			return false;
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x00081E04 File Offset: 0x00080004
		public override bool IsAssignableFrom(Type c)
		{
			return false;
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06002435 RID: 9269 RVA: 0x00081E08 File Offset: 0x00080008
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.elementType.ContainsGenericParameters;
			}
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x00081E18 File Offset: 0x00080018
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00081E20 File Offset: 0x00080020
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x00081E2C File Offset: 0x0008002C
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00081E44 File Offset: 0x00080044
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x00081E4C File Offset: 0x0008004C
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00081E54 File Offset: 0x00080054
		public override string ToString()
		{
			return this.FormatName(this.elementType.ToString());
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600243C RID: 9276 RVA: 0x00081E68 File Offset: 0x00080068
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x0600243D RID: 9277 RVA: 0x00081E70 File Offset: 0x00080070
		public override StructLayoutAttribute StructLayoutAttribute
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600243E RID: 9278 RVA: 0x00081E78 File Offset: 0x00080078
		public override Assembly Assembly
		{
			get
			{
				return this.elementType.Assembly;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x00081E88 File Offset: 0x00080088
		public override string AssemblyQualifiedName
		{
			get
			{
				string text = this.FormatName(this.elementType.FullName);
				if (text == null)
				{
					return null;
				}
				return text + ", " + this.elementType.Assembly.FullName;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x00081ECC File Offset: 0x000800CC
		public override string FullName
		{
			get
			{
				return this.FormatName(this.elementType.FullName);
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x00081EE0 File Offset: 0x000800E0
		public override string Name
		{
			get
			{
				return this.FormatName(this.elementType.Name);
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x00081EF4 File Offset: 0x000800F4
		public override Guid GUID
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x00081EFC File Offset: 0x000800FC
		public override Module Module
		{
			get
			{
				return this.elementType.Module;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x00081F0C File Offset: 0x0008010C
		public override string Namespace
		{
			get
			{
				return this.elementType.Namespace;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002445 RID: 9285 RVA: 0x00081F1C File Offset: 0x0008011C
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002446 RID: 9286 RVA: 0x00081F24 File Offset: 0x00080124
		public override Type UnderlyingSystemType
		{
			get
			{
				DerivedType.create_unmanaged_type(this);
				return this;
			}
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x00081F30 File Offset: 0x00080130
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x00081F38 File Offset: 0x00080138
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00081F40 File Offset: 0x00080140
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000DBB RID: 3515
		internal Type elementType;
	}
}
