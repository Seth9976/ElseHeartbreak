using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Enum" /> objects to and from various other representations.</summary>
	// Token: 0x02000145 RID: 325
	public class EnumConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EnumConverter" /> class for the given type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of enumeration to associate with this enumeration converter. </param>
		// Token: 0x06000BEC RID: 3052 RVA: 0x0001F1C8 File Offset: 0x0001D3C8
		public EnumConverter(Type type)
		{
			this.type = type;
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to. </param>
		// Token: 0x06000BED RID: 3053 RVA: 0x0001F1D8 File Offset: 0x0001D3D8
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(global::System.ComponentModel.Design.Serialization.InstanceDescriptor) || destinationType == typeof(Enum[]) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given value object to the specified destination type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">An optional <see cref="T:System.Globalization.CultureInfo" />. If not supplied, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a valid value for the enumeration. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000BEE RID: 3054 RVA: 0x0001F214 File Offset: 0x0001D414
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != typeof(string) || value == null)
			{
				if (destinationType == typeof(global::System.ComponentModel.Design.Serialization.InstanceDescriptor) && value != null)
				{
					string text = base.ConvertToString(context, culture, value);
					if (this.IsFlags && text.IndexOf(",") != -1)
					{
						if (value is IConvertible)
						{
							Type underlyingType = Enum.GetUnderlyingType(this.type);
							object obj = ((IConvertible)value).ToType(underlyingType, culture);
							MethodInfo method = typeof(Enum).GetMethod("ToObject", new Type[]
							{
								typeof(Type),
								underlyingType
							});
							return new global::System.ComponentModel.Design.Serialization.InstanceDescriptor(method, new object[] { this.type, obj });
						}
					}
					else
					{
						FieldInfo field = this.type.GetField(text);
						if (field != null)
						{
							return new global::System.ComponentModel.Design.Serialization.InstanceDescriptor(field, null);
						}
					}
				}
				else if (destinationType == typeof(Enum[]) && value != null)
				{
					if (!this.IsFlags)
					{
						return new Enum[] { (Enum)Enum.ToObject(this.type, value) };
					}
					long num = Convert.ToInt64((Enum)value, culture);
					Array values = Enum.GetValues(this.type);
					long[] array = new long[values.Length];
					for (int i = 0; i < values.Length; i++)
					{
						array[i] = Convert.ToInt64(values.GetValue(i));
					}
					ArrayList arrayList = new ArrayList();
					bool flag = false;
					while (!flag)
					{
						flag = true;
						foreach (long num2 in array)
						{
							if ((num2 != 0L && (num2 & num) == num2) || num2 == num)
							{
								arrayList.Add(Enum.ToObject(this.type, num2));
								num &= ~num2;
								flag = false;
							}
						}
						if (num == 0L)
						{
							flag = true;
						}
					}
					if (num != 0L)
					{
						arrayList.Add(Enum.ToObject(this.type, num));
					}
					return arrayList.ToArray(typeof(Enum));
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value is IConvertible)
			{
				Type underlyingType2 = Enum.GetUnderlyingType(this.type);
				if (underlyingType2 != value.GetType())
				{
					value = ((IConvertible)value).ToType(underlyingType2, culture);
				}
			}
			if (!this.IsFlags && !this.IsValid(context, value))
			{
				throw this.CreateValueNotValidException(value);
			}
			return Enum.Format(this.type, value, "G");
		}

		/// <summary>Gets a value indicating whether this converter can convert an object in the given source type to an enumeration object using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from. </param>
		// Token: 0x06000BEF RID: 3055 RVA: 0x0001F4C0 File Offset: 0x0001D6C0
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(Enum[]) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the specified value object to an enumeration object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">An optional <see cref="T:System.Globalization.CultureInfo" />. If not supplied, the current culture is assumed. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a valid value for the target type. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000BF0 RID: 3056 RVA: 0x0001F4FC File Offset: 0x0001D6FC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = value as string;
				try
				{
					if (text.IndexOf(',') == -1)
					{
						return Enum.Parse(this.type, text, true);
					}
					long num = 0L;
					string[] array = text.Split(new char[] { ',' });
					foreach (string text2 in array)
					{
						Enum @enum = (Enum)Enum.Parse(this.type, text2, true);
						num |= Convert.ToInt64(@enum, culture);
					}
					return Enum.ToObject(this.type, num);
				}
				catch (Exception ex)
				{
					throw new FormatException(text + " is not a valid value for " + this.type.Name, ex);
				}
			}
			if (value is Enum[])
			{
				long num2 = 0L;
				foreach (Enum enum2 in (Enum[])value)
				{
					num2 |= Convert.ToInt64(enum2, culture);
				}
				return Enum.ToObject(this.type, num2);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Gets a value indicating whether the given object value is valid for this type.</summary>
		/// <returns>true if the specified value is valid for this object; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to test. </param>
		// Token: 0x06000BF1 RID: 3057 RVA: 0x0001F64C File Offset: 0x0001D84C
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			return Enum.IsDefined(this.type, value);
		}

		/// <summary>Gets a value indicating whether this object supports a standard set of values that can be picked from a list using the specified context.</summary>
		/// <returns>true because <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000BF2 RID: 3058 RVA: 0x0001F65C File Offset: 0x0001D85C
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Gets a value indicating whether the list of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list using the specified context.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; false if other values are possible.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000BF3 RID: 3059 RVA: 0x0001F660 File Offset: 0x0001D860
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return !this.IsFlags;
		}

		/// <summary>Gets a collection of standard values for the data type this validator is designed for.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000BF4 RID: 3060 RVA: 0x0001F66C File Offset: 0x0001D86C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.stdValues == null)
			{
				Array values = Enum.GetValues(this.type);
				Array.Sort(values);
				this.stdValues = new TypeConverter.StandardValuesCollection(values);
			}
			return this.stdValues;
		}

		/// <summary>Gets an <see cref="T:System.Collections.IComparer" /> that can be used to sort the values of the enumeration.</summary>
		/// <returns>An <see cref="T:System.Collections.IComparer" /> for sorting the enumeration values.</returns>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		protected virtual IComparer Comparer
		{
			get
			{
				return new EnumConverter.EnumComparer();
			}
		}

		/// <summary>Specifies the type of the enumerator this converter is associated with.</summary>
		/// <returns>The type of the enumerator this converter is associated with.</returns>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0001F6B0 File Offset: 0x0001D8B0
		protected Type EnumType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that specifies the possible values for the enumeration.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that specifies the possible values for the enumeration.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0001F6B8 File Offset: 0x0001D8B8
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x0001F6C0 File Offset: 0x0001D8C0
		protected TypeConverter.StandardValuesCollection Values
		{
			get
			{
				return this.stdValues;
			}
			set
			{
				this.stdValues = value;
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0001F6CC File Offset: 0x0001D8CC
		private ArgumentException CreateValueNotValidException(object value)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "The value '{0}' is not a valid value for the enum '{1}'", new object[]
			{
				value,
				this.type.Name
			});
			return new ArgumentException(text);
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0001F708 File Offset: 0x0001D908
		private bool IsFlags
		{
			get
			{
				return this.type.IsDefined(typeof(FlagsAttribute), false);
			}
		}

		// Token: 0x04000365 RID: 869
		private Type type;

		// Token: 0x04000366 RID: 870
		private TypeConverter.StandardValuesCollection stdValues;

		// Token: 0x02000146 RID: 326
		private class EnumComparer : IComparer
		{
			// Token: 0x06000BFC RID: 3068 RVA: 0x0001F728 File Offset: 0x0001D928
			int IComparer.Compare(object compareObject1, object compareObject2)
			{
				string text = compareObject1 as string;
				string text2 = compareObject2 as string;
				if (text == null || text2 == null)
				{
					return global::System.Collections.Comparer.Default.Compare(compareObject1, compareObject2);
				}
				return CultureInfo.InvariantCulture.CompareInfo.Compare(text, text2);
			}
		}
	}
}
