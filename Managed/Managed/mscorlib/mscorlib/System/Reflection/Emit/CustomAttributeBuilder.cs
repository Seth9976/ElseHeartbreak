using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	/// <summary>Helps build custom attributes.</summary>
	// Token: 0x020002C8 RID: 712
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_CustomAttributeBuilder))]
	[ComVisible(true)]
	public class CustomAttributeBuilder : _CustomAttributeBuilder
	{
		// Token: 0x06002401 RID: 9217 RVA: 0x00080E88 File Offset: 0x0007F088
		internal CustomAttributeBuilder(ConstructorInfo con, byte[] cdata)
		{
			this.ctor = con;
			this.data = (byte[])cdata.Clone();
		}

		/// <summary>Initializes an instance of the CustomAttributeBuilder class given the constructor for the custom attribute and the arguments to the constructor.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="constructorArgs">The arguments to the constructor of the custom attribute. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="con" /> is static or private.-or- The number of supplied arguments does not match the number of parameters of the constructor as required by the calling convention of the constructor.-or- The type of supplied argument does not match the type of the parameter declared in the constructor. -or-A supplied argument is a reference type other than <see cref="T:System.String" /> or <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="constructorArgs" /> is null. </exception>
		// Token: 0x06002402 RID: 9218 RVA: 0x00080EA8 File Offset: 0x0007F0A8
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs)
		{
			this.Initialize(con, constructorArgs, new PropertyInfo[0], new object[0], new FieldInfo[0], new object[0]);
		}

		/// <summary>Initializes an instance of the CustomAttributeBuilder class given the constructor for the custom attribute, the arguments to the constructor, and a set of named field/value pairs.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="constructorArgs">The arguments to the constructor of the custom attribute. </param>
		/// <param name="namedFields">Named fields of the custom attribute. </param>
		/// <param name="fieldValues">Values for the named fields of the custom attribute. </param>
		/// <exception cref="T:System.ArgumentException">The lengths of the <paramref name="namedFields" /> and <paramref name="fieldValues" /> arrays are different.-or- <paramref name="con" /> is static or private.-or- The number of supplied arguments does not match the number of parameters of the constructor as required by the calling convention of the constructor.-or- The type of supplied argument does not match the type of the parameter declared in the constructor.-or- The types of the field values do not match the types of the named fields.-or- The field does not belong to the same class or base class as the constructor. -or-A supplied argument or named field is a reference type other than <see cref="T:System.String" /> or <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One of the parameters is null. </exception>
		// Token: 0x06002403 RID: 9219 RVA: 0x00080EDC File Offset: 0x0007F0DC
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, FieldInfo[] namedFields, object[] fieldValues)
		{
			this.Initialize(con, constructorArgs, new PropertyInfo[0], new object[0], namedFields, fieldValues);
		}

		/// <summary>Initializes an instance of the CustomAttributeBuilder class given the constructor for the custom attribute, the arguments to the constructor, and a set of named property or value pairs.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="constructorArgs">The arguments to the constructor of the custom attribute. </param>
		/// <param name="namedProperties">Named properties of the custom attribute. </param>
		/// <param name="propertyValues">Values for the named properties of the custom attribute. </param>
		/// <exception cref="T:System.ArgumentException">The lengths of the <paramref name="namedProperties" /> and <paramref name="propertyValues" /> arrays are different.-or- <paramref name="con" /> is static or private.-or- The number of supplied arguments does not match the number of parameters of the constructor as required by the calling convention of the constructor.-or- The type of supplied argument does not match the type of the parameter declared in the constructor.-or- The types of the property values do not match the types of the named properties.-or- A property has no setter method.-or- The property does not belong to the same class or base class as the constructor. -or-A supplied argument or named property is a reference type other than <see cref="T:System.String" /> or <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One of the parameters is null. </exception>
		// Token: 0x06002404 RID: 9220 RVA: 0x00080F08 File Offset: 0x0007F108
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues)
		{
			this.Initialize(con, constructorArgs, namedProperties, propertyValues, new FieldInfo[0], new object[0]);
		}

		/// <summary>Initializes an instance of the CustomAttributeBuilder class given the constructor for the custom attribute, the arguments to the constructor, a set of named property or value pairs, and a set of named field or value pairs.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="constructorArgs">The arguments to the constructor of the custom attribute. </param>
		/// <param name="namedProperties">Named properties of the custom attribute. </param>
		/// <param name="propertyValues">Values for the named properties of the custom attribute. </param>
		/// <param name="namedFields">Named fields of the custom attribute. </param>
		/// <param name="fieldValues">Values for the named fields of the custom attribute. </param>
		/// <exception cref="T:System.ArgumentException">The lengths of the <paramref name="namedProperties" /> and <paramref name="propertyValues" /> arrays are different.-or- The lengths of the <paramref name="namedFields" /> and <paramref name="fieldValues" /> arrays are different.-or- <paramref name="con" /> is static or private.-or- The number of supplied arguments does not match the number of parameters of the constructor as required by the calling convention of the constructor.-or- The type of supplied argument does not match the type of the parameter declared in the constructor.-or- The types of the property values do not match the types of the named properties.-or- The types of the field values do not match the types of the corresponding field types.-or- A property has no setter.-or- The property or field does not belong to the same class or base class as the constructor. -or-A supplied argument, named property, or named field is a reference type other than <see cref="T:System.String" /> or <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One of the parameters is null. </exception>
		// Token: 0x06002405 RID: 9221 RVA: 0x00080F34 File Offset: 0x0007F134
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
		{
			this.Initialize(con, constructorArgs, namedProperties, propertyValues, namedFields, fieldValues);
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002406 RID: 9222 RVA: 0x00080F58 File Offset: 0x0007F158
		void _CustomAttributeBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002407 RID: 9223 RVA: 0x00080F60 File Offset: 0x0007F160
		void _CustomAttributeBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002408 RID: 9224 RVA: 0x00080F68 File Offset: 0x0007F168
		void _CustomAttributeBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002409 RID: 9225 RVA: 0x00080F70 File Offset: 0x0007F170
		void _CustomAttributeBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x00080F78 File Offset: 0x0007F178
		internal ConstructorInfo Ctor
		{
			get
			{
				return this.ctor;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x00080F80 File Offset: 0x0007F180
		internal byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x0600240C RID: 9228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] GetBlob(Assembly asmb, ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues);

		// Token: 0x0600240D RID: 9229 RVA: 0x00080F88 File Offset: 0x0007F188
		private bool IsValidType(Type t)
		{
			if (t.IsArray && t.GetArrayRank() > 1)
			{
				return false;
			}
			if (t is TypeBuilder && t.IsEnum)
			{
				Enum.GetUnderlyingType(t);
			}
			return true;
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x00080FCC File Offset: 0x0007F1CC
		private void Initialize(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
		{
			this.ctor = con;
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (constructorArgs == null)
			{
				throw new ArgumentNullException("constructorArgs");
			}
			if (namedProperties == null)
			{
				throw new ArgumentNullException("namedProperties");
			}
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			if (namedFields == null)
			{
				throw new ArgumentNullException("namedFields");
			}
			if (fieldValues == null)
			{
				throw new ArgumentNullException("fieldValues");
			}
			if (con.GetParameterCount() != constructorArgs.Length)
			{
				throw new ArgumentException("Parameter count does not match passed in argument value count.");
			}
			if (namedProperties.Length != propertyValues.Length)
			{
				throw new ArgumentException("Array lengths must be the same.", "namedProperties, propertyValues");
			}
			if (namedFields.Length != fieldValues.Length)
			{
				throw new ArgumentException("Array lengths must be the same.", "namedFields, fieldValues");
			}
			if ((con.Attributes & MethodAttributes.Static) == MethodAttributes.Static || (con.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private)
			{
				throw new ArgumentException("Cannot have private or static constructor.");
			}
			Type declaringType = this.ctor.DeclaringType;
			int num = 0;
			foreach (FieldInfo fieldInfo in namedFields)
			{
				Type declaringType2 = fieldInfo.DeclaringType;
				if (!this.IsValidType(declaringType2))
				{
					throw new ArgumentException("Field '" + fieldInfo.Name + "' does not have a valid type.");
				}
				if (declaringType != declaringType2 && !declaringType2.IsSubclassOf(declaringType) && !declaringType.IsSubclassOf(declaringType2))
				{
					throw new ArgumentException("Field '" + fieldInfo.Name + "' does not belong to the same class as the constructor");
				}
				if (fieldValues[num] != null && !(fieldInfo.FieldType is TypeBuilder) && !fieldInfo.FieldType.IsEnum && !fieldInfo.FieldType.IsInstanceOfType(fieldValues[num]) && !fieldInfo.FieldType.IsArray)
				{
					throw new ArgumentException(string.Concat(new object[] { "Value of field '", fieldInfo.Name, "' does not match field type: ", fieldInfo.FieldType }));
				}
				num++;
			}
			num = 0;
			foreach (PropertyInfo propertyInfo in namedProperties)
			{
				if (!propertyInfo.CanWrite)
				{
					throw new ArgumentException("Property '" + propertyInfo.Name + "' does not have a setter.");
				}
				Type declaringType3 = propertyInfo.DeclaringType;
				if (!this.IsValidType(declaringType3))
				{
					throw new ArgumentException("Property '" + propertyInfo.Name + "' does not have a valid type.");
				}
				if (declaringType != declaringType3 && !declaringType3.IsSubclassOf(declaringType) && !declaringType.IsSubclassOf(declaringType3))
				{
					throw new ArgumentException("Property '" + propertyInfo.Name + "' does not belong to the same class as the constructor");
				}
				if (propertyValues[num] != null && !(propertyInfo.PropertyType is TypeBuilder) && !propertyInfo.PropertyType.IsEnum && !propertyInfo.PropertyType.IsInstanceOfType(propertyValues[num]) && !propertyInfo.PropertyType.IsArray)
				{
					throw new ArgumentException(string.Concat(new object[]
					{
						"Value of property '",
						propertyInfo.Name,
						"' does not match property type: ",
						propertyInfo.PropertyType,
						" -> ",
						propertyValues[num]
					}));
				}
				num++;
			}
			num = 0;
			foreach (ParameterInfo parameterInfo in CustomAttributeBuilder.GetParameters(con))
			{
				if (parameterInfo != null)
				{
					Type parameterType = parameterInfo.ParameterType;
					if (!this.IsValidType(parameterType))
					{
						throw new ArgumentException("Argument " + num + " does not have a valid type.");
					}
					if (constructorArgs[num] != null && !(parameterType is TypeBuilder) && !parameterType.IsEnum && !parameterType.IsInstanceOfType(constructorArgs[num]) && !parameterType.IsArray)
					{
						throw new ArgumentException(string.Concat(new object[]
						{
							"Value of argument ",
							num,
							" does not match parameter type: ",
							parameterType,
							" -> ",
							constructorArgs[num]
						}));
					}
				}
				num++;
			}
			this.data = CustomAttributeBuilder.GetBlob(declaringType.Assembly, con, constructorArgs, namedProperties, propertyValues, namedFields, fieldValues);
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x00081434 File Offset: 0x0007F634
		internal static int decode_len(byte[] data, int pos, out int rpos)
		{
			int num;
			if ((data[pos] & 128) == 0)
			{
				num = (int)(data[pos++] & 127);
			}
			else if ((data[pos] & 64) == 0)
			{
				num = ((int)(data[pos] & 63) << 8) + (int)data[pos + 1];
				pos += 2;
			}
			else
			{
				num = ((int)(data[pos] & 31) << 24) + ((int)data[pos + 1] << 16) + ((int)data[pos + 2] << 8) + (int)data[pos + 3];
				pos += 4;
			}
			rpos = pos;
			return num;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000814B0 File Offset: 0x0007F6B0
		internal static string string_from_bytes(byte[] data, int pos, int len)
		{
			return Encoding.UTF8.GetString(data, pos, len);
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000814C0 File Offset: 0x0007F6C0
		internal string string_arg()
		{
			int num = 2;
			int num2 = CustomAttributeBuilder.decode_len(this.data, num, out num);
			return CustomAttributeBuilder.string_from_bytes(this.data, num, num2);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000814EC File Offset: 0x0007F6EC
		internal static UnmanagedMarshal get_umarshal(CustomAttributeBuilder customBuilder, bool is_field)
		{
			byte[] array = customBuilder.Data;
			UnmanagedType unmanagedType = (UnmanagedType)80;
			int num = -1;
			int num2 = -1;
			bool flag = false;
			string text = null;
			Type type = null;
			string text2 = string.Empty;
			int num3 = (int)array[2];
			num3 |= (int)array[3] << 8;
			string fullName = CustomAttributeBuilder.GetParameters(customBuilder.Ctor)[0].ParameterType.FullName;
			int num4 = 6;
			if (fullName == "System.Int16")
			{
				num4 = 4;
			}
			int num5 = (int)array[num4++];
			num5 |= (int)array[num4++] << 8;
			int i = 0;
			while (i < num5)
			{
				num4++;
				int num6 = (int)array[num4++];
				if (num6 == 85)
				{
					int num7 = CustomAttributeBuilder.decode_len(array, num4, out num4);
					CustomAttributeBuilder.string_from_bytes(array, num4, num7);
					num4 += num7;
				}
				int num8 = CustomAttributeBuilder.decode_len(array, num4, out num4);
				string text3 = CustomAttributeBuilder.string_from_bytes(array, num4, num8);
				num4 += num8;
				string text4 = text3;
				if (text4 != null)
				{
					if (CustomAttributeBuilder.<>f__switch$map1E == null)
					{
						CustomAttributeBuilder.<>f__switch$map1E = new Dictionary<string, int>(9)
						{
							{ "ArraySubType", 0 },
							{ "SizeConst", 1 },
							{ "SafeArraySubType", 2 },
							{ "IidParameterIndex", 3 },
							{ "SafeArrayUserDefinedSubType", 4 },
							{ "SizeParamIndex", 5 },
							{ "MarshalType", 6 },
							{ "MarshalTypeRef", 7 },
							{ "MarshalCookie", 8 }
						};
					}
					int num9;
					if (CustomAttributeBuilder.<>f__switch$map1E.TryGetValue(text4, out num9))
					{
						switch (num9)
						{
						case 0:
						{
							int num10 = (int)array[num4++];
							num10 |= (int)array[num4++] << 8;
							num10 |= (int)array[num4++] << 16;
							num10 |= (int)array[num4++] << 24;
							unmanagedType = (UnmanagedType)num10;
							break;
						}
						case 1:
						{
							int num10 = (int)array[num4++];
							num10 |= (int)array[num4++] << 8;
							num10 |= (int)array[num4++] << 16;
							num10 |= (int)array[num4++] << 24;
							num = num10;
							flag = true;
							break;
						}
						case 2:
						{
							int num10 = (int)array[num4++];
							num10 |= (int)array[num4++] << 8;
							num10 |= (int)array[num4++] << 16;
							num10 |= (int)array[num4++] << 24;
							unmanagedType = (UnmanagedType)num10;
							break;
						}
						case 3:
							num4 += 4;
							break;
						case 4:
							num8 = CustomAttributeBuilder.decode_len(array, num4, out num4);
							CustomAttributeBuilder.string_from_bytes(array, num4, num8);
							num4 += num8;
							break;
						case 5:
						{
							int num10 = (int)array[num4++];
							num10 |= (int)array[num4++] << 8;
							num2 = num10;
							flag = true;
							break;
						}
						case 6:
							num8 = CustomAttributeBuilder.decode_len(array, num4, out num4);
							text = CustomAttributeBuilder.string_from_bytes(array, num4, num8);
							num4 += num8;
							break;
						case 7:
							num8 = CustomAttributeBuilder.decode_len(array, num4, out num4);
							text = CustomAttributeBuilder.string_from_bytes(array, num4, num8);
							type = Type.GetType(text);
							num4 += num8;
							break;
						case 8:
							num8 = CustomAttributeBuilder.decode_len(array, num4, out num4);
							text2 = CustomAttributeBuilder.string_from_bytes(array, num4, num8);
							num4 += num8;
							break;
						default:
							goto IL_034F;
						}
						i++;
						continue;
					}
				}
				IL_034F:
				throw new Exception("Unknown MarshalAsAttribute field: " + text3);
			}
			UnmanagedType unmanagedType2 = (UnmanagedType)num3;
			switch (unmanagedType2)
			{
			case UnmanagedType.LPArray:
				if (flag)
				{
					return UnmanagedMarshal.DefineLPArrayInternal(unmanagedType, num, num2);
				}
				return UnmanagedMarshal.DefineLPArray(unmanagedType);
			default:
				if (unmanagedType2 == UnmanagedType.SafeArray)
				{
					return UnmanagedMarshal.DefineSafeArray(unmanagedType);
				}
				if (unmanagedType2 != UnmanagedType.ByValArray)
				{
					if (unmanagedType2 != UnmanagedType.ByValTStr)
					{
						return UnmanagedMarshal.DefineUnmanagedMarshal((UnmanagedType)num3);
					}
					return UnmanagedMarshal.DefineByValTStr(num);
				}
				else
				{
					if (!is_field)
					{
						throw new ArgumentException("Specified unmanaged type is only valid on fields");
					}
					return UnmanagedMarshal.DefineByValArray(num);
				}
				break;
			case UnmanagedType.CustomMarshaler:
				return UnmanagedMarshal.DefineCustom(type, text2, text, Guid.Empty);
			}
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000818F8 File Offset: 0x0007FAF8
		private static Type elementTypeToType(int elementType)
		{
			switch (elementType)
			{
			case 2:
				return typeof(bool);
			case 3:
				return typeof(char);
			case 4:
				return typeof(sbyte);
			case 5:
				return typeof(byte);
			case 6:
				return typeof(short);
			case 7:
				return typeof(ushort);
			case 8:
				return typeof(int);
			case 9:
				return typeof(uint);
			case 10:
				return typeof(long);
			case 11:
				return typeof(ulong);
			case 12:
				return typeof(float);
			case 13:
				return typeof(double);
			case 14:
				return typeof(string);
			default:
				throw new Exception("Unknown element type '" + elementType + "'");
			}
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000819F4 File Offset: 0x0007FBF4
		private static object decode_cattr_value(Type t, byte[] data, int pos, out int rpos)
		{
			TypeCode typeCode = Type.GetTypeCode(t);
			switch (typeCode)
			{
			case TypeCode.Object:
			{
				int num = (int)data[pos];
				pos++;
				if (num >= 2 && num <= 14)
				{
					return CustomAttributeBuilder.decode_cattr_value(CustomAttributeBuilder.elementTypeToType(num), data, pos, out rpos);
				}
				throw new Exception("Subtype '" + num + "' of type object not yet handled in decode_cattr_value");
			}
			default:
			{
				if (typeCode == TypeCode.Int32)
				{
					rpos = pos + 4;
					return (int)data[pos] + ((int)data[pos + 1] << 8) + ((int)data[pos + 2] << 16) + ((int)data[pos + 3] << 24);
				}
				if (typeCode != TypeCode.String)
				{
					throw new Exception("FIXME: Type " + t + " not yet handled in decode_cattr_value.");
				}
				if (data[pos] == 255)
				{
					rpos = pos + 1;
					return null;
				}
				int num2 = CustomAttributeBuilder.decode_len(data, pos, out pos);
				rpos = pos + num2;
				return CustomAttributeBuilder.string_from_bytes(data, pos, num2);
			}
			case TypeCode.Boolean:
				rpos = pos + 1;
				return data[pos] != 0;
			}
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x00081AF8 File Offset: 0x0007FCF8
		internal static CustomAttributeBuilder.CustomAttributeInfo decode_cattr(CustomAttributeBuilder customBuilder)
		{
			byte[] array = customBuilder.Data;
			ConstructorInfo constructorInfo = customBuilder.Ctor;
			int num = 0;
			CustomAttributeBuilder.CustomAttributeInfo customAttributeInfo = default(CustomAttributeBuilder.CustomAttributeInfo);
			if (array.Length < 2)
			{
				throw new Exception("Custom attr length is only '" + array.Length + "'");
			}
			if (array[0] != 1 || array[1] != 0)
			{
				throw new Exception("Prolog invalid");
			}
			num = 2;
			ParameterInfo[] parameters = CustomAttributeBuilder.GetParameters(constructorInfo);
			customAttributeInfo.ctor = constructorInfo;
			customAttributeInfo.ctorArgs = new object[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				customAttributeInfo.ctorArgs[i] = CustomAttributeBuilder.decode_cattr_value(parameters[i].ParameterType, array, num, out num);
			}
			int num2 = (int)array[num] + (int)array[num + 1] * 256;
			num += 2;
			customAttributeInfo.namedParamNames = new string[num2];
			customAttributeInfo.namedParamValues = new object[num2];
			for (int j = 0; j < num2; j++)
			{
				int num3 = (int)array[num++];
				int num4 = (int)array[num++];
				string text = null;
				if (num4 == 85)
				{
					int num5 = CustomAttributeBuilder.decode_len(array, num, out num);
					text = CustomAttributeBuilder.string_from_bytes(array, num, num5);
					num += num5;
				}
				int num6 = CustomAttributeBuilder.decode_len(array, num, out num);
				string text2 = CustomAttributeBuilder.string_from_bytes(array, num, num6);
				customAttributeInfo.namedParamNames[j] = text2;
				num += num6;
				if (num3 != 83)
				{
					throw new Exception("Unknown named type: " + num3);
				}
				FieldInfo field = constructorInfo.DeclaringType.GetField(text2, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (field == null)
				{
					throw new Exception(string.Concat(new object[] { "Custom attribute type '", constructorInfo.DeclaringType, "' doesn't contain a field named '", text2, "'" }));
				}
				object obj = CustomAttributeBuilder.decode_cattr_value(field.FieldType, array, num, out num);
				if (text != null)
				{
					Type type = Type.GetType(text);
					obj = Enum.ToObject(type, obj);
				}
				customAttributeInfo.namedParamValues[j] = obj;
			}
			return customAttributeInfo;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x00081D10 File Offset: 0x0007FF10
		private static ParameterInfo[] GetParameters(ConstructorInfo ctor)
		{
			ConstructorBuilder constructorBuilder = ctor as ConstructorBuilder;
			if (constructorBuilder != null)
			{
				return constructorBuilder.GetParametersInternal();
			}
			return ctor.GetParameters();
		}

		// Token: 0x04000DB1 RID: 3505
		private ConstructorInfo ctor;

		// Token: 0x04000DB2 RID: 3506
		private byte[] data;

		// Token: 0x020002C9 RID: 713
		internal struct CustomAttributeInfo
		{
			// Token: 0x04000DB4 RID: 3508
			public ConstructorInfo ctor;

			// Token: 0x04000DB5 RID: 3509
			public object[] ctorArgs;

			// Token: 0x04000DB6 RID: 3510
			public string[] namedParamNames;

			// Token: 0x04000DB7 RID: 3511
			public object[] namedParamValues;
		}
	}
}
