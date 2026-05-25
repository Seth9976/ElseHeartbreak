using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the value to pass to a property to cause the property to get its value from another source. This is known as ambience. This class cannot be inherited.</summary>
	// Token: 0x020000C4 RID: 196
	[AttributeUsage(AttributeTargets.All)]
	public sealed class AmbientValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a Boolean value for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06000880 RID: 2176 RVA: 0x00019660 File Offset: 0x00017860
		public AmbientValueAttribute(bool value)
		{
			this.AmbientValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given an 8-bit unsigned integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06000881 RID: 2177 RVA: 0x00019674 File Offset: 0x00017874
		public AmbientValueAttribute(byte value)
		{
			this.AmbientValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a Unicode character for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06000882 RID: 2178 RVA: 0x00019688 File Offset: 0x00017888
		public AmbientValueAttribute(char value)
		{
			this.AmbientValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a double-precision floating-point number for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06000883 RID: 2179 RVA: 0x0001969C File Offset: 0x0001789C
		public AmbientValueAttribute(double value)
		{
			this.AmbientValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 16-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06000884 RID: 2180 RVA: 0x000196B0 File Offset: 0x000178B0
		public AmbientValueAttribute(short value)
		{
			this.AmbientValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 32-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06000885 RID: 2181 RVA: 0x000196C4 File Offset: 0x000178C4
		public AmbientValueAttribute(int value)
		{
			this.AmbientValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a 64-bit signed integer for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06000886 RID: 2182 RVA: 0x000196D8 File Offset: 0x000178D8
		public AmbientValueAttribute(long value)
		{
			this.AmbientValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given an object for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06000887 RID: 2183 RVA: 0x000196EC File Offset: 0x000178EC
		public AmbientValueAttribute(object value)
		{
			this.AmbientValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a single-precision floating point number for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06000888 RID: 2184 RVA: 0x000196FC File Offset: 0x000178FC
		public AmbientValueAttribute(float value)
		{
			this.AmbientValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a string for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06000889 RID: 2185 RVA: 0x00019710 File Offset: 0x00017910
		public AmbientValueAttribute(string value)
		{
			this.AmbientValue = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given the value and its type.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the <paramref name="value" /> parameter. </param>
		/// <param name="value">The value for this attribute. </param>
		// Token: 0x0600088A RID: 2186 RVA: 0x00019720 File Offset: 0x00017920
		public AmbientValueAttribute(Type type, string value)
		{
			try
			{
				this.AmbientValue = Convert.ChangeType(value, type);
			}
			catch
			{
				this.AmbientValue = null;
			}
		}

		/// <summary>Gets the object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <returns>The object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00019770 File Offset: 0x00017970
		public object Value
		{
			get
			{
				return this.AmbientValue;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.AmbientValueAttribute" /> to compare with the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</param>
		// Token: 0x0600088C RID: 2188 RVA: 0x00019778 File Offset: 0x00017978
		public override bool Equals(object obj)
		{
			return obj is AmbientValueAttribute && (obj == this || ((AmbientValueAttribute)obj).Value == this.AmbientValue);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x0600088D RID: 2189 RVA: 0x000197A4 File Offset: 0x000179A4
		public override int GetHashCode()
		{
			return this.AmbientValue.GetHashCode();
		}

		// Token: 0x04000237 RID: 567
		private object AmbientValue;
	}
}
