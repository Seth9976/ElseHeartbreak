using System;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x020000F1 RID: 241
	internal class DerivedPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x0001C984 File Offset: 0x0001AB84
		protected DerivedPropertyDescriptor(string name, Attribute[] attrs)
			: base(name, attrs)
		{
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0001C990 File Offset: 0x0001AB90
		public DerivedPropertyDescriptor(string name, Attribute[] attrs, int dummy)
			: this(name, attrs)
		{
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0001C99C File Offset: 0x0001AB9C
		public void SetReadOnly(bool value)
		{
			this.readOnly = value;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0001C9A8 File Offset: 0x0001ABA8
		public void SetComponentType(Type type)
		{
			this.componentType = type;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0001C9B4 File Offset: 0x0001ABB4
		public void SetPropertyType(Type type)
		{
			this.propertyType = type;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0001C9C0 File Offset: 0x0001ABC0
		public override object GetValue(object component)
		{
			if (this.prop == null)
			{
				this.prop = this.componentType.GetProperty(this.Name);
			}
			return this.prop.GetValue(component, null);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0001C9F4 File Offset: 0x0001ABF4
		public override void SetValue(object component, object value)
		{
			if (this.prop == null)
			{
				this.prop = this.componentType.GetProperty(this.Name);
			}
			this.prop.SetValue(component, value, null);
			this.OnValueChanged(component, new PropertyChangedEventArgs(this.Name));
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0001CA44 File Offset: 0x0001AC44
		[global::System.MonoTODO]
		public override void ResetValue(object component)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0001CA4C File Offset: 0x0001AC4C
		[global::System.MonoTODO]
		public override bool CanResetValue(object component)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0001CA54 File Offset: 0x0001AC54
		[global::System.MonoTODO]
		public override bool ShouldSerializeValue(object component)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0001CA5C File Offset: 0x0001AC5C
		public override Type ComponentType
		{
			get
			{
				return this.componentType;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0001CA64 File Offset: 0x0001AC64
		public override bool IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0001CA6C File Offset: 0x0001AC6C
		public override Type PropertyType
		{
			get
			{
				return this.propertyType;
			}
		}

		// Token: 0x040002A2 RID: 674
		private bool readOnly;

		// Token: 0x040002A3 RID: 675
		private Type componentType;

		// Token: 0x040002A4 RID: 676
		private Type propertyType;

		// Token: 0x040002A5 RID: 677
		private PropertyInfo prop;
	}
}
