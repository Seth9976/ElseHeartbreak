using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x0200019F RID: 415
	internal class ReflectionPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06000EA3 RID: 3747 RVA: 0x00025AB4 File Offset: 0x00023CB4
		public ReflectionPropertyDescriptor(Type componentType, PropertyDescriptor oldPropertyDescriptor, Attribute[] attributes)
			: base(oldPropertyDescriptor, attributes)
		{
			this._componentType = componentType;
			this._propertyType = oldPropertyDescriptor.PropertyType;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00025AD4 File Offset: 0x00023CD4
		public ReflectionPropertyDescriptor(Type componentType, string name, Type type, Attribute[] attributes)
			: base(name, attributes)
		{
			this._componentType = componentType;
			this._propertyType = type;
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00025AF0 File Offset: 0x00023CF0
		public ReflectionPropertyDescriptor(PropertyInfo info)
			: base(info.Name, null)
		{
			this._member = info;
			this._componentType = this._member.DeclaringType;
			this._propertyType = info.PropertyType;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00025B30 File Offset: 0x00023D30
		private PropertyInfo GetPropertyInfo()
		{
			if (this._member == null)
			{
				this._member = this._componentType.GetProperty(this.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
				if (this._member == null)
				{
					throw new ArgumentException("Accessor methods for the " + this.Name + " property are missing");
				}
			}
			return this._member;
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x00025BA4 File Offset: 0x00023DA4
		public override Type ComponentType
		{
			get
			{
				return this._componentType;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00025BAC File Offset: 0x00023DAC
		public override bool IsReadOnly
		{
			get
			{
				ReadOnlyAttribute readOnlyAttribute = (ReadOnlyAttribute)this.Attributes[typeof(ReadOnlyAttribute)];
				return !this.GetPropertyInfo().CanWrite || readOnlyAttribute.IsReadOnly;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x00025BF0 File Offset: 0x00023DF0
		public override Type PropertyType
		{
			get
			{
				return this._propertyType;
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00025BF8 File Offset: 0x00023DF8
		protected override void FillAttributes(IList attributeList)
		{
			base.FillAttributes(attributeList);
			if (!this.GetPropertyInfo().CanWrite)
			{
				attributeList.Add(ReadOnlyAttribute.Yes);
			}
			int num = 0;
			Type type = this.ComponentType;
			while (type != null && type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			Attribute[][] array = new Attribute[num][];
			type = this.ComponentType;
			while (type != null && type != typeof(object))
			{
				PropertyInfo property = type.GetProperty(this.Name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
				if (property != null)
				{
					object[] customAttributes = property.GetCustomAttributes(false);
					Attribute[] array2 = new Attribute[customAttributes.Length];
					customAttributes.CopyTo(array2, 0);
					array[--num] = array2;
				}
				type = type.BaseType;
			}
			foreach (Attribute[] array4 in array)
			{
				if (array4 != null)
				{
					foreach (Attribute attribute in array4)
					{
						attributeList.Add(attribute);
					}
				}
			}
			foreach (object obj in TypeDescriptor.GetAttributes(this.PropertyType))
			{
				Attribute attribute2 = (Attribute)obj;
				attributeList.Add(attribute2);
			}
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00025DA0 File Offset: 0x00023FA0
		public override object GetValue(object component)
		{
			component = MemberDescriptor.GetInvokee(this._componentType, component);
			this.InitAccessors();
			return this.getter.GetValue(component, null);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00025DC4 File Offset: 0x00023FC4
		private global::System.ComponentModel.Design.DesignerTransaction CreateTransaction(object obj, string description)
		{
			IComponent component = obj as IComponent;
			if (component == null || component.Site == null)
			{
				return null;
			}
			global::System.ComponentModel.Design.IDesignerHost designerHost = (global::System.ComponentModel.Design.IDesignerHost)component.Site.GetService(typeof(global::System.ComponentModel.Design.IDesignerHost));
			if (designerHost == null)
			{
				return null;
			}
			global::System.ComponentModel.Design.DesignerTransaction designerTransaction = designerHost.CreateTransaction(description);
			global::System.ComponentModel.Design.IComponentChangeService componentChangeService = (global::System.ComponentModel.Design.IComponentChangeService)component.Site.GetService(typeof(global::System.ComponentModel.Design.IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.OnComponentChanging(component, this);
			}
			return designerTransaction;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00025E40 File Offset: 0x00024040
		private void EndTransaction(object obj, global::System.ComponentModel.Design.DesignerTransaction tran, object oldValue, object newValue, bool commit)
		{
			if (tran == null)
			{
				this.OnValueChanged(obj, new PropertyChangedEventArgs(this.Name));
				return;
			}
			if (commit)
			{
				IComponent component = obj as IComponent;
				global::System.ComponentModel.Design.IComponentChangeService componentChangeService = (global::System.ComponentModel.Design.IComponentChangeService)component.Site.GetService(typeof(global::System.ComponentModel.Design.IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, oldValue, newValue);
				}
				tran.Commit();
				this.OnValueChanged(obj, new PropertyChangedEventArgs(this.Name));
			}
			else
			{
				tran.Cancel();
			}
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00025EC4 File Offset: 0x000240C4
		private void InitAccessors()
		{
			if (this.accessors_inited)
			{
				return;
			}
			PropertyInfo propertyInfo = this.GetPropertyInfo();
			MethodInfo methodInfo = propertyInfo.GetSetMethod(true);
			MethodInfo methodInfo2 = propertyInfo.GetGetMethod(true);
			if (methodInfo2 != null)
			{
				this.getter = propertyInfo;
			}
			if (methodInfo != null)
			{
				this.setter = propertyInfo;
			}
			if (methodInfo != null && methodInfo2 != null)
			{
				this.accessors_inited = true;
				return;
			}
			if (methodInfo == null && methodInfo2 == null)
			{
				this.accessors_inited = true;
				return;
			}
			MethodInfo methodInfo3 = ((methodInfo2 == null) ? methodInfo : methodInfo2);
			if (methodInfo3 == null || !methodInfo3.IsVirtual || (methodInfo3.Attributes & MethodAttributes.VtableLayoutMask) == MethodAttributes.VtableLayoutMask)
			{
				this.accessors_inited = true;
				return;
			}
			Type type = this._componentType.BaseType;
			while (type != null && type != typeof(object))
			{
				propertyInfo = type.GetProperty(this.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
				if (propertyInfo == null)
				{
					break;
				}
				if (methodInfo == null)
				{
					methodInfo3 = (methodInfo = propertyInfo.GetSetMethod());
				}
				else
				{
					methodInfo3 = (methodInfo2 = propertyInfo.GetGetMethod());
				}
				if (methodInfo2 != null && this.getter == null)
				{
					this.getter = propertyInfo;
				}
				if (methodInfo != null && this.setter == null)
				{
					this.setter = propertyInfo;
				}
				if (methodInfo3 != null)
				{
					break;
				}
				type = type.BaseType;
			}
			this.accessors_inited = true;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00026038 File Offset: 0x00024238
		public override void SetValue(object component, object value)
		{
			global::System.ComponentModel.Design.DesignerTransaction designerTransaction = this.CreateTransaction(component, "Set Property '" + this.Name + "'");
			object invokee = MemberDescriptor.GetInvokee(this._componentType, component);
			object value2 = this.GetValue(invokee);
			try
			{
				this.InitAccessors();
				this.setter.SetValue(invokee, value, null);
				this.EndTransaction(component, designerTransaction, value2, value, true);
			}
			catch
			{
				this.EndTransaction(component, designerTransaction, value2, value, false);
				throw;
			}
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000260CC File Offset: 0x000242CC
		private MethodInfo FindPropertyMethod(object o, string method_name)
		{
			MethodInfo methodInfo = null;
			string text = method_name + this.Name;
			foreach (MethodInfo methodInfo2 in o.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (methodInfo2.Name == text && methodInfo2.GetParameters().Length == 0)
				{
					methodInfo = methodInfo2;
					break;
				}
			}
			return methodInfo;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002613C File Offset: 0x0002433C
		public override void ResetValue(object component)
		{
			object invokee = MemberDescriptor.GetInvokee(this._componentType, component);
			DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
			if (defaultValueAttribute != null)
			{
				this.SetValue(invokee, defaultValueAttribute.Value);
			}
			global::System.ComponentModel.Design.DesignerTransaction designerTransaction = this.CreateTransaction(component, "Reset Property '" + this.Name + "'");
			object value = this.GetValue(invokee);
			try
			{
				MethodInfo methodInfo = this.FindPropertyMethod(invokee, "Reset");
				if (methodInfo != null)
				{
					methodInfo.Invoke(invokee, null);
				}
				this.EndTransaction(component, designerTransaction, value, this.GetValue(invokee), true);
			}
			catch
			{
				this.EndTransaction(component, designerTransaction, value, this.GetValue(invokee), false);
				throw;
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00026214 File Offset: 0x00024414
		public override bool CanResetValue(object component)
		{
			component = MemberDescriptor.GetInvokee(this._componentType, component);
			DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
			if (defaultValueAttribute != null)
			{
				object value = this.GetValue(component);
				if (defaultValueAttribute.Value == null || value == null)
				{
					if (defaultValueAttribute.Value != value)
					{
						return true;
					}
					if (defaultValueAttribute.Value == null && value == null)
					{
						return false;
					}
				}
				return !defaultValueAttribute.Value.Equals(value);
			}
			if (!this._member.CanWrite)
			{
				return false;
			}
			MethodInfo methodInfo = this.FindPropertyMethod(component, "ShouldPersist");
			if (methodInfo != null)
			{
				return (bool)methodInfo.Invoke(component, null);
			}
			methodInfo = this.FindPropertyMethod(component, "ShouldSerialize");
			if (methodInfo != null && !(bool)methodInfo.Invoke(component, null))
			{
				return false;
			}
			methodInfo = this.FindPropertyMethod(component, "Reset");
			return methodInfo != null;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00026308 File Offset: 0x00024508
		public override bool ShouldSerializeValue(object component)
		{
			component = MemberDescriptor.GetInvokee(this._componentType, component);
			if (this.IsReadOnly)
			{
				MethodInfo methodInfo = this.FindPropertyMethod(component, "ShouldSerialize");
				if (methodInfo != null)
				{
					return (bool)methodInfo.Invoke(component, null);
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			else
			{
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)this.Attributes[typeof(DefaultValueAttribute)];
				if (defaultValueAttribute == null)
				{
					MethodInfo methodInfo2 = this.FindPropertyMethod(component, "ShouldSerialize");
					return methodInfo2 == null || (bool)methodInfo2.Invoke(component, null);
				}
				object value = this.GetValue(component);
				if (defaultValueAttribute.Value == null || value == null)
				{
					return defaultValueAttribute.Value != value;
				}
				return !defaultValueAttribute.Value.Equals(value);
			}
		}

		// Token: 0x0400041E RID: 1054
		private PropertyInfo _member;

		// Token: 0x0400041F RID: 1055
		private Type _componentType;

		// Token: 0x04000420 RID: 1056
		private Type _propertyType;

		// Token: 0x04000421 RID: 1057
		private PropertyInfo getter;

		// Token: 0x04000422 RID: 1058
		private PropertyInfo setter;

		// Token: 0x04000423 RID: 1059
		private bool accessors_inited;
	}
}
