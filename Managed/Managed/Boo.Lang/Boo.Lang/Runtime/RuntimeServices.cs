using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Boo.Lang.Runtime.DynamicDispatching;
using Boo.Lang.Runtime.DynamicDispatching.Emitters;

namespace Boo.Lang.Runtime
{
	// Token: 0x0200003E RID: 62
	public class RuntimeServices
	{
		// Token: 0x060001EF RID: 495 RVA: 0x0000665C File Offset: 0x0000485C
		public static void WithExtensions(Type extensions, RuntimeServices.CodeBlock block)
		{
			RuntimeServices.RegisterExtensions(extensions);
			try
			{
				block();
			}
			finally
			{
				RuntimeServices.UnRegisterExtensions(extensions);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000066A0 File Offset: 0x000048A0
		public static void RegisterExtensions(Type extensions)
		{
			RuntimeServices._extensions.Register(extensions);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000066B0 File Offset: 0x000048B0
		public static void UnRegisterExtensions(Type extensions)
		{
			RuntimeServices._extensions.UnRegister(extensions);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000066C0 File Offset: 0x000048C0
		public static object Invoke(object target, string name, object[] args)
		{
			Dispatcher dispatcher = RuntimeServices.GetDispatcher(target, args, name, () => RuntimeServices.CreateMethodDispatcher(target, name, args));
			return dispatcher(target, args);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006720 File Offset: 0x00004920
		private static Dispatcher CreateMethodDispatcher(object target, string name, object[] args)
		{
			IQuackFu quackFu = target as IQuackFu;
			if (quackFu != null)
			{
				return (object o, object[] arguments) => ((IQuackFu)o).QuackInvoke(name, arguments);
			}
			Type type = target as Type;
			if (type != null)
			{
				return RuntimeServices.DoCreateMethodDispatcher(null, type, name, args);
			}
			Type type2 = target.GetType();
			if (type2.IsCOMObject)
			{
				return (object o, object[] arguments) => o.GetType().InvokeMember(name, 262524, null, target, arguments);
			}
			return RuntimeServices.DoCreateMethodDispatcher(target, type2, name, args);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000067B8 File Offset: 0x000049B8
		private static Dispatcher DoCreateMethodDispatcher(object target, Type targetType, string name, object[] args)
		{
			return new MethodDispatcherFactory(RuntimeServices._extensions, target, targetType, name, args).Create();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000067D0 File Offset: 0x000049D0
		private static Dispatcher GetDispatcher(object target, object[] args, string cacheKeyName, DispatcherCache.DispatcherFactory factory)
		{
			Type[] argumentTypes = MethodResolver.GetArgumentTypes(args);
			return RuntimeServices.GetDispatcher(target, cacheKeyName, argumentTypes, factory);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000067F0 File Offset: 0x000049F0
		private static Dispatcher GetDispatcher(object target, string cacheKeyName, Type[] cacheKeyTypes, DispatcherCache.DispatcherFactory factory)
		{
			Type type = (target as Type) ?? target.GetType();
			DispatcherKey dispatcherKey = new DispatcherKey(type, cacheKeyName, cacheKeyTypes);
			return RuntimeServices._cache.Get(dispatcherKey, factory);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006828 File Offset: 0x00004A28
		public static object GetProperty(object target, string name)
		{
			Dispatcher dispatcher = RuntimeServices.GetDispatcher(target, RuntimeServices.NoArguments, name, () => RuntimeServices.CreatePropGetDispatcher(target, name));
			return dispatcher(target, RuntimeServices.NoArguments);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006880 File Offset: 0x00004A80
		private static Dispatcher CreatePropGetDispatcher(object target, string name)
		{
			IQuackFu quackFu = target as IQuackFu;
			if (quackFu != null)
			{
				return (object o, object[] args) => ((IQuackFu)o).QuackGet(name, null);
			}
			Type type = target as Type;
			if (type != null)
			{
				return RuntimeServices.DoCreatePropGetDispatcher(null, type, name);
			}
			Type type2 = target.GetType();
			if (type2.IsCOMObject)
			{
				return (object o, object[] args) => o.GetType().InvokeMember(name, 267388, null, o, null);
			}
			return RuntimeServices.DoCreatePropGetDispatcher(target, target.GetType(), name);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006900 File Offset: 0x00004B00
		private static Dispatcher DoCreatePropGetDispatcher(object target, Type type, string name)
		{
			return new PropertyDispatcherFactory(RuntimeServices._extensions, target, type, name, new object[0]).CreateGetter();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000691C File Offset: 0x00004B1C
		public static object SetProperty(object target, string name, object value)
		{
			object[] array = new object[] { value };
			Dispatcher dispatcher = RuntimeServices.GetDispatcher(target, array, name, () => RuntimeServices.CreatePropSetDispatcher(target, name, value));
			return dispatcher(target, array);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006980 File Offset: 0x00004B80
		private static Dispatcher CreatePropSetDispatcher(object target, string name, object value)
		{
			IQuackFu quackFu = target as IQuackFu;
			if (quackFu != null)
			{
				return (object o, object[] args) => ((IQuackFu)o).QuackSet(name, null, args[0]);
			}
			Type type = target as Type;
			if (type != null)
			{
				return RuntimeServices.DoCreatePropSetDispatcher(null, type, name, value);
			}
			Type type2 = target.GetType();
			if (type2.IsCOMObject)
			{
				return (object o, object[] args) => o.GetType().InvokeMember(name, 272508, null, o, args);
			}
			return RuntimeServices.DoCreatePropSetDispatcher(target, type2, name, value);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006A00 File Offset: 0x00004C00
		private static Dispatcher DoCreatePropSetDispatcher(object target, Type type, string name, object value)
		{
			return new PropertyDispatcherFactory(RuntimeServices._extensions, target, type, name, new object[] { value }).CreateSetter();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006A20 File Offset: 0x00004C20
		public static void PropagateValueTypeChanges(RuntimeServices.ValueTypeChange[] changes)
		{
			foreach (RuntimeServices.ValueTypeChange valueTypeChange in changes)
			{
				if (!(valueTypeChange.Value is ValueType))
				{
					break;
				}
				try
				{
					RuntimeServices.SetProperty(valueTypeChange.Target, valueTypeChange.Member, valueTypeChange.Value);
				}
				catch (MissingFieldException)
				{
					break;
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006AB0 File Offset: 0x00004CB0
		public static object Coerce(object value, Type toType)
		{
			if (value == null)
			{
				return null;
			}
			object[] array = new object[] { toType };
			Dispatcher dispatcher = RuntimeServices.GetDispatcher(value, "$Coerce$", new Type[] { toType }, () => RuntimeServices.CreateCoerceDispatcher(value, toType));
			return dispatcher(value, array);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006B28 File Offset: 0x00004D28
		private static Dispatcher CreateCoerceDispatcher(object value, Type toType)
		{
			if (toType.IsInstanceOfType(value))
			{
				return new Dispatcher(RuntimeServices.IdentityDispatcher);
			}
			if (value is ICoercible)
			{
				return new Dispatcher(RuntimeServices.CoercibleDispatcher);
			}
			Type type = value.GetType();
			if (RuntimeServices.IsPromotableNumeric(type) && RuntimeServices.IsPromotableNumeric(toType))
			{
				return RuntimeServices.EmitPromotionDispatcher(type, toType);
			}
			MethodInfo methodInfo = RuntimeServices.FindImplicitConversionOperator(type, toType);
			if (methodInfo == null)
			{
				return new Dispatcher(RuntimeServices.IdentityDispatcher);
			}
			return RuntimeServices.EmitImplicitConversionDispatcher(methodInfo);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006BAC File Offset: 0x00004DAC
		private static Dispatcher EmitPromotionDispatcher(Type fromType, Type toType)
		{
			return (Dispatcher)Delegate.CreateDelegate(typeof(Dispatcher), typeof(NumericPromotions).GetMethod(string.Concat(new object[]
			{
				"From",
				Type.GetTypeCode(fromType),
				"To",
				Type.GetTypeCode(toType)
			})));
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006C14 File Offset: 0x00004E14
		private static bool IsPromotableNumeric(Type fromType)
		{
			return RuntimeServices.IsPromotableNumeric(Type.GetTypeCode(fromType));
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006C24 File Offset: 0x00004E24
		private static Dispatcher EmitImplicitConversionDispatcher(MethodInfo method)
		{
			return new ImplicitConversionEmitter(method).Emit();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00006C34 File Offset: 0x00004E34
		private static object CoercibleDispatcher(object o, object[] args)
		{
			return ((ICoercible)o).Coerce((Type)args[0]);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00006C4C File Offset: 0x00004E4C
		private static object IdentityDispatcher(object o, object[] args)
		{
			return o;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006C50 File Offset: 0x00004E50
		public static object GetSlice(object target, string name, object[] args)
		{
			Dispatcher dispatcher = RuntimeServices.GetDispatcher(target, args, name + "[]", () => RuntimeServices.CreateGetSliceDispatcher(target, name, args));
			return dispatcher(target, args);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006CB8 File Offset: 0x00004EB8
		private static Dispatcher CreateGetSliceDispatcher(object target, string name, object[] args)
		{
			IQuackFu quackFu = target as IQuackFu;
			if (quackFu != null)
			{
				return (object o, object[] arguments) => ((IQuackFu)o).QuackGet(name, arguments);
			}
			if (string.Empty == name && args.Length == 1 && target is Array)
			{
				return new Dispatcher(RuntimeServices.GetArraySlice);
			}
			return new SliceDispatcherFactory(RuntimeServices._extensions, target, target.GetType(), name, args).CreateGetter();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006D40 File Offset: 0x00004F40
		private static object GetArraySlice(object target, object[] args)
		{
			IList list = (IList)target;
			return list[RuntimeServices.NormalizeIndex(list.Count, (int)args[0])];
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006D70 File Offset: 0x00004F70
		public static object SetSlice(object target, string name, object[] args)
		{
			Dispatcher dispatcher = RuntimeServices.GetDispatcher(target, args, name + "[]=", () => RuntimeServices.CreateSetSliceDispatcher(target, name, args));
			return dispatcher(target, args);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006DD8 File Offset: 0x00004FD8
		private static Dispatcher CreateSetSliceDispatcher(object target, string name, object[] args)
		{
			IQuackFu quackFu = target as IQuackFu;
			if (quackFu != null)
			{
				return (object o, object[] arguments) => ((IQuackFu)o).QuackSet(name, (object[])RuntimeServices.GetRange2(arguments, 0, arguments.Length - 1), arguments[arguments.Length - 1]);
			}
			if (string.Empty == name && args.Length == 2 && target is Array)
			{
				return new Dispatcher(RuntimeServices.SetArraySlice);
			}
			return new SliceDispatcherFactory(RuntimeServices._extensions, target, target.GetType(), name, args).CreateSetter();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00006E60 File Offset: 0x00005060
		private static object SetArraySlice(object target, object[] args)
		{
			IList list = (IList)target;
			list[RuntimeServices.NormalizeIndex(list.Count, (int)args[0])] = args[1];
			return args[1];
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006E94 File Offset: 0x00005094
		internal static string GetDefaultMemberName(Type type)
		{
			DefaultMemberAttribute defaultMemberAttribute = (DefaultMemberAttribute)Attribute.GetCustomAttribute(type, typeof(DefaultMemberAttribute));
			return (defaultMemberAttribute == null) ? string.Empty : defaultMemberAttribute.MemberName;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006ED0 File Offset: 0x000050D0
		public static object InvokeCallable(object target, object[] args)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			ICallable callable = target as ICallable;
			if (callable != null)
			{
				return callable.Call(args);
			}
			Delegate @delegate = target as Delegate;
			if (@delegate != null)
			{
				return @delegate.DynamicInvoke(args);
			}
			Type type = target as Type;
			if (type != null)
			{
				return Activator.CreateInstance(type, args);
			}
			return ((MethodInfo)target).Invoke(null, args);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006F4C File Offset: 0x0000514C
		private static bool IsNumeric(TypeCode code)
		{
			switch (code)
			{
			case 5:
				return true;
			case 6:
				return true;
			case 7:
				return true;
			case 8:
				return true;
			case 9:
				return true;
			case 10:
				return true;
			case 11:
				return true;
			case 12:
				return true;
			case 13:
				return true;
			case 14:
				return true;
			case 15:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006FAC File Offset: 0x000051AC
		public static object InvokeBinaryOperator(string operatorName, object lhs, object rhs)
		{
			Type type = lhs.GetType();
			Type type2 = rhs.GetType();
			TypeCode typeCode = Type.GetTypeCode(type);
			TypeCode typeCode2 = Type.GetTypeCode(type2);
			if (RuntimeServices.IsNumeric(typeCode) && RuntimeServices.IsNumeric(typeCode2))
			{
				int num = (int)(((int)operatorName.get_Chars(3) << 8) + operatorName.get_Chars(operatorName.Length - 1));
				switch (num)
				{
				case 18284:
					return RuntimeServices.op_GreaterThanOrEqual(lhs, typeCode, rhs, typeCode2);
				default:
					switch (num)
					{
					case 19564:
						return RuntimeServices.op_LessThanOrEqual(lhs, typeCode, rhs, typeCode2);
					default:
						if (num != 19826)
						{
							if (num == 19827)
							{
								return RuntimeServices.op_Modulus(lhs, typeCode, rhs, typeCode2);
							}
							if (num == 16750)
							{
								return RuntimeServices.op_Addition(lhs, typeCode, rhs, typeCode2);
							}
							if (num == 16996)
							{
								return RuntimeServices.op_BitwiseAnd(lhs, typeCode, rhs, typeCode2);
							}
							if (num == 17010)
							{
								return RuntimeServices.op_BitwiseOr(lhs, typeCode, rhs, typeCode2);
							}
							if (num == 17518)
							{
								return RuntimeServices.op_Division(lhs, typeCode, rhs, typeCode2);
							}
							if (num == 17774)
							{
								return RuntimeServices.op_Exponentiation(lhs, typeCode, rhs, typeCode2);
							}
							if (num == 17778)
							{
								return RuntimeServices.op_ExclusiveOr(lhs, typeCode, rhs, typeCode2);
							}
							if (num != 19816)
							{
								if (num == 19833)
								{
									return RuntimeServices.op_Multiply(lhs, typeCode, rhs, typeCode2);
								}
								if (num != 20072 && num != 20082)
								{
									if (num == 21358)
									{
										return RuntimeServices.op_Subtraction(lhs, typeCode, rhs, typeCode2);
									}
									if (num == 21364)
									{
										return (operatorName.get_Chars(8) != 'L') ? RuntimeServices.op_ShiftRight(lhs, typeCode, rhs, typeCode2) : RuntimeServices.op_ShiftLeft(lhs, typeCode, rhs, typeCode2);
									}
								}
							}
						}
						throw new MissingMethodException(RuntimeServices.MissingOperatorMessageFor(operatorName, type, type2));
					case 19566:
						return RuntimeServices.op_LessThan(lhs, typeCode, rhs, typeCode2);
					}
					break;
				case 18286:
					return RuntimeServices.op_GreaterThan(lhs, typeCode, rhs, typeCode2);
				}
			}
			else
			{
				object[] array = new object[] { lhs, rhs };
				IQuackFu quackFu = lhs as IQuackFu;
				if (quackFu != null)
				{
					return quackFu.QuackInvoke(operatorName, array);
				}
				quackFu = rhs as IQuackFu;
				if (quackFu != null)
				{
					return quackFu.QuackInvoke(operatorName, array);
				}
				object obj;
				try
				{
					obj = RuntimeServices.Invoke(type, operatorName, array);
				}
				catch (MissingMethodException ex)
				{
					try
					{
						return RuntimeServices.Invoke(type2, operatorName, array);
					}
					catch (MissingMethodException)
					{
						try
						{
							return RuntimeServices.InvokeRuntimeServicesOperator(operatorName, array);
						}
						catch (MissingMethodException)
						{
						}
					}
					throw new MissingMethodException(RuntimeServices.MissingOperatorMessageFor(operatorName, type, type2), ex);
				}
				return obj;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000072A8 File Offset: 0x000054A8
		private static string MissingOperatorMessageFor(string operatorName, Type lhsType, Type rhsType)
		{
			return string.Format("{0} is not applicable to operands '{1}' and '{2}'.", RuntimeServices.FormatOperatorName(operatorName), lhsType, rhsType);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000072BC File Offset: 0x000054BC
		private static string FormatOperatorName(string operatorName)
		{
			StringBuilder stringBuilder = new StringBuilder(operatorName.Length);
			stringBuilder.Append(operatorName.get_Chars(3));
			string text = operatorName.Substring(4);
			for (int i = 0; i < text.Length; i++)
			{
				char c = text.get_Chars(i);
				if (char.IsUpper(c))
				{
					stringBuilder.Append(" ");
					stringBuilder.Append(char.ToLower(c));
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007344 File Offset: 0x00005544
		public static object InvokeUnaryOperator(string operatorName, object operand)
		{
			Type type = operand.GetType();
			TypeCode typeCode = Type.GetTypeCode(type);
			if (RuntimeServices.IsNumeric(typeCode))
			{
				int num = (int)(((int)operatorName.get_Chars(3) << 8) + operatorName.get_Chars(operatorName.Length - 1));
				if (num != 21870)
				{
					throw new ArgumentException(operatorName + " " + operand);
				}
				return RuntimeServices.op_UnaryNegation(operand, typeCode);
			}
			else
			{
				object[] array = new object[] { operand };
				IQuackFu quackFu = operand as IQuackFu;
				if (quackFu != null)
				{
					return quackFu.QuackInvoke(operatorName, array);
				}
				object obj;
				try
				{
					obj = RuntimeServices.Invoke(type, operatorName, array);
				}
				catch (MissingMethodException)
				{
					try
					{
						return RuntimeServices.InvokeRuntimeServicesOperator(operatorName, array);
					}
					catch (MissingMethodException)
					{
					}
					throw;
				}
				return obj;
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000743C File Offset: 0x0000563C
		private static object InvokeRuntimeServicesOperator(string operatorName, object[] args)
		{
			return RuntimeServices.Invoke(RuntimeServices.RuntimeServicesType, operatorName, args);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000744C File Offset: 0x0000564C
		public static object MoveNext(IEnumerator enumerator)
		{
			if (enumerator == null)
			{
				throw new ApplicationException("Cannot unpack null.");
			}
			if (!enumerator.MoveNext())
			{
				throw new ApplicationException("Unpack list of wrong size.");
			}
			return enumerator.Current;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000747C File Offset: 0x0000567C
		public static int Len(object obj)
		{
			if (obj != null)
			{
				ICollection collection = obj as ICollection;
				if (collection != null)
				{
					return collection.Count;
				}
				string text = obj as string;
				if (text != null)
				{
					return text.Length;
				}
			}
			throw new ArgumentException();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000074BC File Offset: 0x000056BC
		public static string Mid(string s, int begin, int end)
		{
			begin = RuntimeServices.NormalizeStringIndex(s, begin);
			end = RuntimeServices.NormalizeStringIndex(s, end);
			return s.Substring(begin, end - begin);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000074DC File Offset: 0x000056DC
		public static Array GetRange1(Array source, int begin)
		{
			return RuntimeServices.GetRange2(source, begin, source.Length);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000074EC File Offset: 0x000056EC
		public static Array GetRange2(Array source, int begin, int end)
		{
			int length = source.Length;
			begin = RuntimeServices.NormalizeIndex(length, begin);
			end = RuntimeServices.NormalizeIndex(length, end);
			int num = Math.Max(0, end - begin);
			Array array = Array.CreateInstance(source.GetType().GetElementType(), num);
			Array.Copy(source, begin, array, 0, num);
			return array;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000753C File Offset: 0x0000573C
		public static void SetMultiDimensionalRange1(Array source, Array dest, int[] ranges, bool[] compute_end, bool[] collapse)
		{
			if (dest.Rank != ranges.Length / 2)
			{
				throw new Exception(string.Concat(new object[]
				{
					"invalid range passed: ",
					ranges.Length / 2,
					", expected ",
					dest.Rank * 2
				}));
			}
			for (int i = 0; i < dest.Rank; i++)
			{
				if (compute_end[i])
				{
					ranges[2 * i + 1] = dest.GetLength(i);
				}
				if (ranges[2 * i] < 0 || ranges[2 * i] >= dest.GetLength(i) || ranges[2 * i + 1] > dest.GetLength(i) || ranges[2 * i + 1] <= ranges[2 * i])
				{
					throw new ApplicationException("Invalid array.");
				}
			}
			int num = 0;
			for (int j = 0; j < collapse.Length; j++)
			{
				if (!collapse[j])
				{
					num++;
				}
			}
			if (num == 0)
			{
				num = 1;
			}
			if (source.Rank != num)
			{
				throw new ApplicationException(string.Format("Cannot assign array of rank {0} into an array subset of rank {1}.", source.Rank, num));
			}
			int[] array = new int[dest.Rank];
			int[] array2 = new int[num];
			int[] array3 = new int[source.Rank];
			int num2 = 0;
			bool flag = false;
			for (int k = 0; k < dest.Rank; k++)
			{
				array[k] = ranges[2 * k + 1] - ranges[2 * k];
				if (!collapse[k])
				{
					array2[num2] = array[k];
					array3[num2] = source.GetLength(num2);
					if (array3[num2] != array[k])
					{
						flag = true;
					}
					num2++;
				}
			}
			if (flag)
			{
				StringBuilder stringBuilder = new StringBuilder(array3[0]);
				StringBuilder stringBuilder2 = new StringBuilder(array2[0]);
				for (int l = 1; l < source.Rank; l++)
				{
					stringBuilder.Append(" x ");
					stringBuilder.Append(array3[l]);
					stringBuilder2.Append(" x ");
					stringBuilder2.Append(array2[l]);
				}
				throw new ApplicationException(string.Format("Cannot assign array with dimensions {0} into array subset of dimensions {1}.", stringBuilder.ToString(), stringBuilder2.ToString()));
			}
			int[] array4 = new int[source.Rank];
			array4[0] = array3[0];
			for (int m = 1; m < source.Rank; m++)
			{
				array4[m] = array4[m - 1] * array3[m];
			}
			int[] array5 = new int[dest.Rank];
			int[] array6 = new int[source.Rank];
			for (int n = 0; n < source.Length; n++)
			{
				int num3 = 0;
				for (int num4 = 0; num4 < dest.Rank; num4++)
				{
					if (collapse[num4])
					{
						array5[num4] = ranges[2 * num4];
					}
					else
					{
						array6[num3] = n % array4[num3];
						array5[num4] = array6[num3] + ranges[2 * num4];
						num3++;
					}
				}
				dest.SetValue(source.GetValue(array6), array5);
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007870 File Offset: 0x00005A70
		public static Array GetMultiDimensionalRange1(Array source, int[] ranges, bool[] compute_end, bool[] collapse)
		{
			int rank = source.Rank;
			int[] array = new int[rank];
			int num = 0;
			for (int i = 0; i < rank; i++)
			{
				ranges[2 * i] = RuntimeServices.NormalizeIndex(source.GetLength(i), ranges[2 * i]);
				if (compute_end[i])
				{
					ranges[2 * i + 1] = source.GetLength(i);
				}
				else
				{
					ranges[2 * i + 1] = RuntimeServices.NormalizeIndex(source.GetLength(i), ranges[2 * i + 1]);
				}
				array[i] = ranges[2 * i + 1] - ranges[2 * i];
				num += ((!collapse[i]) ? 0 : 1);
			}
			int num2 = rank - num;
			int[] array2 = new int[num2];
			int num3 = 0;
			for (int j = 0; j < rank; j++)
			{
				if (!collapse[j])
				{
					array2[num3] = array[j];
					num3++;
				}
			}
			if (num2 == 0)
			{
				num2 = 1;
				array2 = new int[] { 1 };
			}
			Array array3 = Array.CreateInstance(source.GetType().GetElementType(), array2);
			int[] array4 = new int[rank];
			int[] array5 = new int[num2];
			int[] array6 = new int[rank];
			for (int k = 0; k < rank; k++)
			{
				if (k == 0)
				{
					array4[k] = array3.Length;
				}
				else
				{
					array4[k] = array4[k - 1] / array[k - 1];
				}
			}
			for (int l = 0; l < array3.Length; l++)
			{
				int num4 = 0;
				for (int m = 0; m < rank; m++)
				{
					int num5 = l % array4[m] / (array4[m] / array[m]);
					array6[m] = ranges[2 * m] + num5;
					if (!collapse[m])
					{
						array5[num4] = array6[m] - ranges[2 * m];
						num4++;
					}
				}
				array3.SetValue(source.GetValue(array6), array5);
			}
			return array3;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007A5C File Offset: 0x00005C5C
		public static void CheckArrayUnpack(Array array, int expected)
		{
			if (array == null)
			{
				throw new ApplicationException("Cannot unpack null.");
			}
			if (expected > array.Length)
			{
				RuntimeServices.Error("Unpack array of wrong size (expected={0}, actual={1}).", new object[] { expected, array.Length });
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007AB0 File Offset: 0x00005CB0
		public static int NormalizeIndex(int len, int index)
		{
			return (index >= 0) ? Math.Min(index, len) : Math.Max(0, index + len);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public static int NormalizeArrayIndex(Array array, int index)
		{
			return (index >= 0) ? Math.Min(index, array.Length) : Math.Max(0, index + array.Length);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007B04 File Offset: 0x00005D04
		public static int NormalizeStringIndex(string s, int index)
		{
			return (index >= 0) ? Math.Min(index, s.Length) : Math.Max(0, index + s.Length);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007B38 File Offset: 0x00005D38
		public static IEnumerable GetEnumerable(object enumerable)
		{
			if (enumerable == null)
			{
				throw new ApplicationException("Cannot enumerate null.");
			}
			IEnumerable enumerable2 = enumerable as IEnumerable;
			if (enumerable2 != null)
			{
				return enumerable2;
			}
			TextReader textReader = enumerable as TextReader;
			if (textReader != null)
			{
				return TextReaderEnumerator.lines(textReader);
			}
			throw new ApplicationException("Argument is not enumerable (does not implement System.Collections.IEnumerable).");
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007B84 File Offset: 0x00005D84
		public static Array AddArrays(Type resultingElementType, Array lhs, Array rhs)
		{
			int num = lhs.Length + rhs.Length;
			Array array = Array.CreateInstance(resultingElementType, num);
			Array.Copy(lhs, 0, array, 0, lhs.Length);
			Array.Copy(rhs, 0, array, lhs.Length, rhs.Length);
			return array;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007BCC File Offset: 0x00005DCC
		public static string op_Addition(string lhs, string rhs)
		{
			return lhs + rhs;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007BD8 File Offset: 0x00005DD8
		public static string op_Addition(string lhs, object rhs)
		{
			return lhs + rhs;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007BE4 File Offset: 0x00005DE4
		public static string op_Addition(object lhs, string rhs)
		{
			return lhs + rhs;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007BF0 File Offset: 0x00005DF0
		public static Array op_Multiply(Array lhs, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			Type type = lhs.GetType();
			if (type.GetArrayRank() != 1)
			{
				throw new ArgumentException("lhs");
			}
			int length = lhs.Length;
			Array array = Array.CreateInstance(type.GetElementType(), length * count);
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				Array.Copy(lhs, 0, array, num, length);
				num += length;
			}
			return array;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007C6C File Offset: 0x00005E6C
		public static Array op_Multiply(int count, Array rhs)
		{
			return rhs * count;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007C78 File Offset: 0x00005E78
		public static string op_Multiply(string lhs, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			string text = null;
			if (lhs != null)
			{
				StringBuilder stringBuilder = new StringBuilder(lhs.Length * count);
				for (int i = 0; i < count; i++)
				{
					stringBuilder.Append(lhs);
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007CD0 File Offset: 0x00005ED0
		public static string op_Multiply(int count, string rhs)
		{
			return rhs * count;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007CDC File Offset: 0x00005EDC
		public static bool op_NotMember(string lhs, string rhs)
		{
			return !RuntimeServices.op_Member(lhs, rhs);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007CE8 File Offset: 0x00005EE8
		public static bool op_Member(string lhs, string rhs)
		{
			return lhs != null && rhs != null && rhs.IndexOf(lhs) > -1;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007D04 File Offset: 0x00005F04
		public static bool op_Member(char lhs, string rhs)
		{
			return rhs != null && rhs.IndexOf(lhs) > -1;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00007D18 File Offset: 0x00005F18
		public static bool op_Match(string input, Regex pattern)
		{
			return pattern.IsMatch(input);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00007D24 File Offset: 0x00005F24
		public static bool op_Match(string input, string pattern)
		{
			return Regex.IsMatch(input, pattern);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00007D30 File Offset: 0x00005F30
		public static bool op_NotMatch(string input, Regex pattern)
		{
			return !RuntimeServices.op_Match(input, pattern);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007D3C File Offset: 0x00005F3C
		public static bool op_NotMatch(string input, string pattern)
		{
			return !RuntimeServices.op_Match(input, pattern);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007D48 File Offset: 0x00005F48
		public static string op_Modulus(string lhs, IEnumerable rhs)
		{
			return string.Format(lhs, Builtins.array(rhs));
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007D58 File Offset: 0x00005F58
		public static string op_Modulus(string lhs, object[] rhs)
		{
			return string.Format(lhs, rhs);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007D64 File Offset: 0x00005F64
		public static bool op_Member(object lhs, IList rhs)
		{
			return rhs != null && rhs.Contains(lhs);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00007D78 File Offset: 0x00005F78
		public static bool op_NotMember(object lhs, IList rhs)
		{
			return !RuntimeServices.op_Member(lhs, rhs);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007D84 File Offset: 0x00005F84
		public static bool op_Member(object lhs, IDictionary rhs)
		{
			return rhs != null && rhs.Contains(lhs);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007D98 File Offset: 0x00005F98
		public static bool op_NotMember(object lhs, IDictionary rhs)
		{
			return !RuntimeServices.op_Member(lhs, rhs);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public static bool op_Member(object lhs, IEnumerable rhs)
		{
			if (rhs == null)
			{
				return false;
			}
			foreach (object obj in rhs)
			{
				if (RuntimeServices.EqualityOperator(lhs, obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007E20 File Offset: 0x00006020
		public static bool op_NotMember(object lhs, IEnumerable rhs)
		{
			return !RuntimeServices.op_Member(lhs, rhs);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007E2C File Offset: 0x0000602C
		public static bool EqualityOperator(object lhs, object rhs)
		{
			if (lhs == rhs)
			{
				return true;
			}
			if (lhs == null)
			{
				return rhs.Equals(lhs);
			}
			if (rhs == null)
			{
				return lhs.Equals(rhs);
			}
			TypeCode typeCode = Type.GetTypeCode(lhs.GetType());
			TypeCode typeCode2 = Type.GetTypeCode(rhs.GetType());
			if (RuntimeServices.IsNumeric(typeCode) && RuntimeServices.IsNumeric(typeCode2))
			{
				return RuntimeServices.EqualityOperator(lhs, typeCode, rhs, typeCode2);
			}
			Array array = lhs as Array;
			if (array != null)
			{
				Array array2 = rhs as Array;
				if (array2 != null)
				{
					return RuntimeServices.ArrayEqualityImpl(array, array2);
				}
			}
			return lhs.Equals(rhs) || rhs.Equals(lhs);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007ED0 File Offset: 0x000060D0
		public static bool op_Equality(Array lhs, Array rhs)
		{
			return lhs == rhs || (lhs != null && rhs != null && RuntimeServices.ArrayEqualityImpl(lhs, rhs));
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007EF0 File Offset: 0x000060F0
		private static bool ArrayEqualityImpl(Array lhs, Array rhs)
		{
			if (lhs.Rank != 1 || rhs.Rank != 1)
			{
				throw new ArgumentException("array rank must be 1");
			}
			if (lhs.Length != rhs.Length)
			{
				return false;
			}
			for (int i = 0; i < lhs.Length; i++)
			{
				if (!RuntimeServices.EqualityOperator(lhs.GetValue(i), rhs.GetValue(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007F68 File Offset: 0x00006168
		private static TypeCode GetConvertTypeCode(TypeCode lhsTypeCode, TypeCode rhsTypeCode)
		{
			if (lhsTypeCode == 15 || rhsTypeCode == 15)
			{
				return 15;
			}
			if (lhsTypeCode == 14 || rhsTypeCode == 14)
			{
				return 14;
			}
			if (lhsTypeCode == 13 || rhsTypeCode == 13)
			{
				return 13;
			}
			if (lhsTypeCode == 12)
			{
				if (rhsTypeCode == 5 || rhsTypeCode == 7 || rhsTypeCode == 9 || rhsTypeCode == 11)
				{
					return 11;
				}
				return 12;
			}
			else if (rhsTypeCode == 12)
			{
				if (lhsTypeCode == 5 || lhsTypeCode == 7 || lhsTypeCode == 9 || lhsTypeCode == 11)
				{
					return 11;
				}
				return 12;
			}
			else
			{
				if (lhsTypeCode == 11 || rhsTypeCode == 11)
				{
					return 11;
				}
				if (lhsTypeCode == 10)
				{
					if (rhsTypeCode == 5 || rhsTypeCode == 7 || rhsTypeCode == 9)
					{
						return 11;
					}
					return 10;
				}
				else
				{
					if (rhsTypeCode != 10)
					{
						return 9;
					}
					if (lhsTypeCode == 5 || lhsTypeCode == 7 || lhsTypeCode == 9)
					{
						return 11;
					}
					return 10;
				}
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00008064 File Offset: 0x00006264
		private static object op_Multiply(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case 10:
				return convertible.ToUInt32(null) * convertible2.ToUInt32(null);
			case 11:
				return convertible.ToInt64(null) * convertible2.ToInt64(null);
			case 12:
				return convertible.ToUInt64(null) * convertible2.ToUInt64(null);
			case 13:
				return convertible.ToSingle(null) * convertible2.ToSingle(null);
			case 14:
				return convertible.ToDouble(null) * convertible2.ToDouble(null);
			case 15:
				return convertible.ToDecimal(null) * convertible2.ToDecimal(null);
			default:
				return convertible.ToInt32(null) * convertible2.ToInt32(null);
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00008144 File Offset: 0x00006344
		private static object op_Division(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case 10:
				return convertible.ToUInt32(null) / convertible2.ToUInt32(null);
			case 11:
				return convertible.ToInt64(null) / convertible2.ToInt64(null);
			case 12:
				return convertible.ToUInt64(null) / convertible2.ToUInt64(null);
			case 13:
				return convertible.ToSingle(null) / convertible2.ToSingle(null);
			case 14:
				return convertible.ToDouble(null) / convertible2.ToDouble(null);
			case 15:
				return convertible.ToDecimal(null) / convertible2.ToDecimal(null);
			default:
				return convertible.ToInt32(null) / convertible2.ToInt32(null);
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00008224 File Offset: 0x00006424
		private static object op_Addition(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case 10:
				return convertible.ToUInt32(null) + convertible2.ToUInt32(null);
			case 11:
				return convertible.ToInt64(null) + convertible2.ToInt64(null);
			case 12:
				return convertible.ToUInt64(null) + convertible2.ToUInt64(null);
			case 13:
				return convertible.ToSingle(null) + convertible2.ToSingle(null);
			case 14:
				return convertible.ToDouble(null) + convertible2.ToDouble(null);
			case 15:
				return convertible.ToDecimal(null) + convertible2.ToDecimal(null);
			default:
				return convertible.ToInt32(null) + convertible2.ToInt32(null);
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008304 File Offset: 0x00006504
		private static object op_Subtraction(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case 10:
				return convertible.ToUInt32(null) - convertible2.ToUInt32(null);
			case 11:
				return convertible.ToInt64(null) - convertible2.ToInt64(null);
			case 12:
				return convertible.ToUInt64(null) - convertible2.ToUInt64(null);
			case 13:
				return convertible.ToSingle(null) - convertible2.ToSingle(null);
			case 14:
				return convertible.ToDouble(null) - convertible2.ToDouble(null);
			case 15:
				return convertible.ToDecimal(null) - convertible2.ToDecimal(null);
			default:
				return convertible.ToInt32(null) - convertible2.ToInt32(null);
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000083E4 File Offset: 0x000065E4
		private static bool EqualityOperator(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case 10:
				return convertible.ToUInt32(null) == convertible2.ToUInt32(null);
			case 11:
				return convertible.ToInt64(null) == convertible2.ToInt64(null);
			case 12:
				return convertible.ToUInt64(null) == convertible2.ToUInt64(null);
			case 13:
				return convertible.ToSingle(null) == convertible2.ToSingle(null);
			case 14:
				return convertible.ToDouble(null) == convertible2.ToDouble(null);
			case 15:
				return convertible.ToDecimal(null) == convertible2.ToDecimal(null);
			default:
				return convertible.ToInt32(null) == convertible2.ToInt32(null);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000084A8 File Offset: 0x000066A8
		private static bool op_GreaterThan(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case 10:
				return convertible.ToUInt32(null) > convertible2.ToUInt32(null);
			case 11:
				return convertible.ToInt64(null) > convertible2.ToInt64(null);
			case 12:
				return convertible.ToUInt64(null) > convertible2.ToUInt64(null);
			case 13:
				return convertible.ToSingle(null) > convertible2.ToSingle(null);
			case 14:
				return convertible.ToDouble(null) > convertible2.ToDouble(null);
			case 15:
				return convertible.ToDecimal(null) > convertible2.ToDecimal(null);
			default:
				return convertible.ToInt32(null) > convertible2.ToInt32(null);
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000856C File Offset: 0x0000676C
		private static bool op_GreaterThanOrEqual(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case 10:
				return convertible.ToUInt32(null) >= convertible2.ToUInt32(null);
			case 11:
				return convertible.ToInt64(null) >= convertible2.ToInt64(null);
			case 12:
				return convertible.ToUInt64(null) >= convertible2.ToUInt64(null);
			case 13:
				return convertible.ToSingle(null) >= convertible2.ToSingle(null);
			case 14:
				return convertible.ToDouble(null) >= convertible2.ToDouble(null);
			case 15:
				return convertible.ToDecimal(null) >= convertible2.ToDecimal(null);
			default:
				return convertible.ToInt32(null) >= convertible2.ToInt32(null);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00008640 File Offset: 0x00006840
		private static bool op_LessThan(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case 10:
				return convertible.ToUInt32(null) < convertible2.ToUInt32(null);
			case 11:
				return convertible.ToInt64(null) < convertible2.ToInt64(null);
			case 12:
				return convertible.ToUInt64(null) < convertible2.ToUInt64(null);
			case 13:
				return convertible.ToSingle(null) < convertible2.ToSingle(null);
			case 14:
				return convertible.ToDouble(null) < convertible2.ToDouble(null);
			case 15:
				return convertible.ToDecimal(null) < convertible2.ToDecimal(null);
			default:
				return convertible.ToInt32(null) < convertible2.ToInt32(null);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00008704 File Offset: 0x00006904
		private static bool op_LessThanOrEqual(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case 10:
				return convertible.ToUInt32(null) <= convertible2.ToUInt32(null);
			case 11:
				return convertible.ToInt64(null) <= convertible2.ToInt64(null);
			case 12:
				return convertible.ToUInt64(null) <= convertible2.ToUInt64(null);
			case 13:
				return convertible.ToSingle(null) <= convertible2.ToSingle(null);
			case 14:
				return convertible.ToDouble(null) <= convertible2.ToDouble(null);
			case 15:
				return convertible.ToDecimal(null) <= convertible2.ToDecimal(null);
			default:
				return convertible.ToInt32(null) <= convertible2.ToInt32(null);
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000087D8 File Offset: 0x000069D8
		private static object op_Modulus(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case 10:
				return convertible.ToUInt32(null) % convertible2.ToUInt32(null);
			case 11:
				return convertible.ToInt64(null) % convertible2.ToInt64(null);
			case 12:
				return convertible.ToUInt64(null) % convertible2.ToUInt64(null);
			case 13:
				return convertible.ToSingle(null) % convertible2.ToSingle(null);
			case 14:
				return convertible.ToDouble(null) % convertible2.ToDouble(null);
			case 15:
				return convertible.ToDecimal(null) % convertible2.ToDecimal(null);
			default:
				return convertible.ToInt32(null) % convertible2.ToInt32(null);
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000088B8 File Offset: 0x00006AB8
		private static double op_Exponentiation(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			return Math.Pow(convertible.ToDouble(null), convertible2.ToDouble(null));
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000088E8 File Offset: 0x00006AE8
		private static object op_BitwiseAnd(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case 10:
				return convertible.ToUInt32(null) & convertible2.ToUInt32(null);
			case 11:
				return convertible.ToInt64(null) & convertible2.ToInt64(null);
			case 12:
				return convertible.ToUInt64(null) & convertible2.ToUInt64(null);
			case 13:
			case 14:
			case 15:
				throw new ArgumentException(lhsTypeCode + " & " + rhsTypeCode);
			default:
				return convertible.ToInt32(null) & convertible2.ToInt32(null);
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000089A0 File Offset: 0x00006BA0
		private static object op_BitwiseOr(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case 10:
				return convertible.ToUInt32(null) | convertible2.ToUInt32(null);
			case 11:
				return convertible.ToInt64(null) | convertible2.ToInt64(null);
			case 12:
				return convertible.ToUInt64(null) | convertible2.ToUInt64(null);
			case 13:
			case 14:
			case 15:
				throw new ArgumentException(lhsTypeCode + " | " + rhsTypeCode);
			default:
				return convertible.ToInt32(null) | convertible2.ToInt32(null);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008A58 File Offset: 0x00006C58
		private static object op_ExclusiveOr(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (RuntimeServices.GetConvertTypeCode(lhsTypeCode, rhsTypeCode))
			{
			case 10:
				return convertible.ToUInt32(null) ^ convertible2.ToUInt32(null);
			case 11:
				return convertible.ToInt64(null) ^ convertible2.ToInt64(null);
			case 12:
				return convertible.ToUInt64(null) ^ convertible2.ToUInt64(null);
			case 13:
			case 14:
			case 15:
				throw new ArgumentException(lhsTypeCode + " ^ " + rhsTypeCode);
			default:
				return convertible.ToInt32(null) ^ convertible2.ToInt32(null);
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00008B10 File Offset: 0x00006D10
		private static object op_ShiftLeft(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (rhsTypeCode)
			{
			case 13:
			case 14:
			case 15:
				throw new ArgumentException(lhsTypeCode + " << " + rhsTypeCode);
			default:
				switch (lhsTypeCode)
				{
				case 10:
					return convertible.ToUInt32(null) << convertible2.ToInt32(null);
				case 11:
					return convertible.ToInt64(null) << convertible2.ToInt32(null);
				case 12:
					return convertible.ToUInt64(null) << convertible2.ToInt32(null);
				case 13:
				case 14:
				case 15:
					throw new ArgumentException(lhsTypeCode + " << " + rhsTypeCode);
				default:
					return convertible.ToInt32(null) << convertible2.ToInt32(null);
				}
				break;
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008C0C File Offset: 0x00006E0C
		private static object op_ShiftRight(object lhs, TypeCode lhsTypeCode, object rhs, TypeCode rhsTypeCode)
		{
			IConvertible convertible = (IConvertible)lhs;
			IConvertible convertible2 = (IConvertible)rhs;
			switch (rhsTypeCode)
			{
			case 13:
			case 14:
			case 15:
				throw new ArgumentException(lhsTypeCode + " >> " + rhsTypeCode);
			default:
				switch (lhsTypeCode)
				{
				case 10:
					return convertible.ToUInt32(null) >> convertible2.ToInt32(null);
				case 11:
					return convertible.ToInt64(null) >> convertible2.ToInt32(null);
				case 12:
					return convertible.ToUInt64(null) >> convertible2.ToInt32(null);
				case 13:
				case 14:
				case 15:
					throw new ArgumentException(lhsTypeCode + " >> " + rhsTypeCode);
				default:
					return convertible.ToInt32(null) >> convertible2.ToInt32(null);
				}
				break;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00008D08 File Offset: 0x00006F08
		private static object op_UnaryNegation(object operand, TypeCode operandTypeCode)
		{
			IConvertible convertible = (IConvertible)operand;
			switch (operandTypeCode)
			{
			case 10:
				return -convertible.ToInt64(null);
			case 11:
				return -convertible.ToInt64(null);
			case 12:
				return -convertible.ToInt64(null);
			case 13:
				return -convertible.ToSingle(null);
			case 14:
				return -convertible.ToDouble(null);
			case 15:
				return -convertible.ToDecimal(null);
			default:
				return -convertible.ToInt32(null);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008DAC File Offset: 0x00006FAC
		internal static bool IsPromotableNumeric(TypeCode code)
		{
			switch (code)
			{
			case 3:
				return true;
			case 4:
				return true;
			case 5:
				return true;
			case 6:
				return true;
			case 7:
				return true;
			case 8:
				return true;
			case 9:
				return true;
			case 10:
				return true;
			case 11:
				return true;
			case 12:
				return true;
			case 13:
				return true;
			case 14:
				return true;
			case 15:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00008E18 File Offset: 0x00007018
		public static IConvertible CheckNumericPromotion(object value)
		{
			IConvertible convertible = (IConvertible)value;
			return RuntimeServices.CheckNumericPromotion(convertible);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00008E34 File Offset: 0x00007034
		public static IConvertible CheckNumericPromotion(IConvertible convertible)
		{
			if (RuntimeServices.IsPromotableNumeric(convertible.GetTypeCode()))
			{
				return convertible;
			}
			throw new InvalidCastException();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008E50 File Offset: 0x00007050
		public static byte UnboxByte(object value)
		{
			if (value is byte)
			{
				return (byte)value;
			}
			return RuntimeServices.CheckNumericPromotion(value).ToByte(null);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008E70 File Offset: 0x00007070
		public static sbyte UnboxSByte(object value)
		{
			if (value is sbyte)
			{
				return (sbyte)value;
			}
			return RuntimeServices.CheckNumericPromotion(value).ToSByte(null);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008E90 File Offset: 0x00007090
		public static char UnboxChar(object value)
		{
			if (value is char)
			{
				return (char)value;
			}
			return RuntimeServices.CheckNumericPromotion(value).ToChar(null);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00008EB0 File Offset: 0x000070B0
		public static short UnboxInt16(object value)
		{
			if (value is short)
			{
				return (short)value;
			}
			return RuntimeServices.CheckNumericPromotion(value).ToInt16(null);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008ED0 File Offset: 0x000070D0
		public static ushort UnboxUInt16(object value)
		{
			if (value is ushort)
			{
				return (ushort)value;
			}
			return RuntimeServices.CheckNumericPromotion(value).ToUInt16(null);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00008EF0 File Offset: 0x000070F0
		public static int UnboxInt32(object value)
		{
			if (value is int)
			{
				return (int)value;
			}
			return RuntimeServices.CheckNumericPromotion(value).ToInt32(null);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008F10 File Offset: 0x00007110
		public static uint UnboxUInt32(object value)
		{
			if (value is uint)
			{
				return (uint)value;
			}
			return RuntimeServices.CheckNumericPromotion(value).ToUInt32(null);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008F30 File Offset: 0x00007130
		public static long UnboxInt64(object value)
		{
			if (value is long)
			{
				return (long)value;
			}
			return RuntimeServices.CheckNumericPromotion(value).ToInt64(null);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008F50 File Offset: 0x00007150
		public static ulong UnboxUInt64(object value)
		{
			if (value is ulong)
			{
				return (ulong)value;
			}
			return RuntimeServices.CheckNumericPromotion(value).ToUInt64(null);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008F70 File Offset: 0x00007170
		public static float UnboxSingle(object value)
		{
			if (value is float)
			{
				return (float)value;
			}
			return RuntimeServices.CheckNumericPromotion(value).ToSingle(null);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00008F90 File Offset: 0x00007190
		public static double UnboxDouble(object value)
		{
			if (value is double)
			{
				return (double)value;
			}
			return RuntimeServices.CheckNumericPromotion(value).ToDouble(null);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00008FB0 File Offset: 0x000071B0
		public static decimal UnboxDecimal(object value)
		{
			if (value is decimal)
			{
				return (decimal)value;
			}
			return RuntimeServices.CheckNumericPromotion(value).ToDecimal(null);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00008FD0 File Offset: 0x000071D0
		public static bool UnboxBoolean(object value)
		{
			if (value is bool)
			{
				return (bool)value;
			}
			return RuntimeServices.CheckNumericPromotion(value).ToBoolean(null);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00008FF0 File Offset: 0x000071F0
		public static bool ToBool(object value)
		{
			if (value == null)
			{
				return false;
			}
			if (value is bool)
			{
				return (bool)value;
			}
			if (value is string)
			{
				return !string.IsNullOrEmpty((string)value);
			}
			Type type = value.GetType();
			Dispatcher dispatcher = RuntimeServices.GetDispatcher(value, "$ToBool$", new Type[] { type }, () => RuntimeServices.CreateBoolConverter(type));
			return (bool)dispatcher(value, new object[] { value });
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009080 File Offset: 0x00007280
		public static bool ToBool(decimal value)
		{
			return 0m != value;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009090 File Offset: 0x00007290
		public static bool ToBool(float value)
		{
			return 0f != value;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000090A0 File Offset: 0x000072A0
		public static bool ToBool(double value)
		{
			return 0.0 != value;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000090B4 File Offset: 0x000072B4
		private static object ToBoolTrue(object value, object[] arguments)
		{
			return RuntimeServices.True;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000090BC File Offset: 0x000072BC
		private static object UnboxBooleanDispatcher(object value, object[] arguments)
		{
			return RuntimeServices.UnboxBoolean(value);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000090CC File Offset: 0x000072CC
		private static Dispatcher CreateBoolConverter(Type type)
		{
			MethodInfo methodInfo = RuntimeServices.FindImplicitConversionOperator(type, typeof(bool));
			if (methodInfo != null)
			{
				return RuntimeServices.EmitImplicitConversionDispatcher(methodInfo);
			}
			if (type.IsValueType)
			{
				return new Dispatcher(RuntimeServices.UnboxBooleanDispatcher);
			}
			return new Dispatcher(RuntimeServices.ToBoolTrue);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000911C File Offset: 0x0000731C
		internal static MethodInfo FindImplicitConversionOperator(Type from, Type to)
		{
			MethodInfo methodInfo;
			if ((methodInfo = RuntimeServices.FindImplicitConversionMethod(from.GetMethods(88), from, to)) == null)
			{
				methodInfo = RuntimeServices.FindImplicitConversionMethod(to.GetMethods(88), from, to) ?? RuntimeServices.FindImplicitConversionMethod(RuntimeServices.GetExtensionMethods(), from, to);
			}
			return methodInfo;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009164 File Offset: 0x00007364
		private static IEnumerable<MethodInfo> GetExtensionMethods()
		{
			foreach (MemberInfo member in RuntimeServices._extensions.Extensions)
			{
				if (member.MemberType == 8)
				{
					yield return (MethodInfo)member;
				}
			}
			yield break;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009180 File Offset: 0x00007380
		private static MethodInfo FindImplicitConversionMethod(IEnumerable<MethodInfo> candidates, Type from, Type to)
		{
			foreach (MethodInfo methodInfo in candidates)
			{
				if (!(methodInfo.Name != "op_Implicit"))
				{
					if (methodInfo.ReturnType == to)
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						if (parameters.Length == 1)
						{
							if (parameters[0].ParameterType.IsAssignableFrom(from))
							{
								return methodInfo;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000923C File Offset: 0x0000743C
		private static void Error(string format, params object[] args)
		{
			throw new ApplicationException(string.Format(format, args));
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000924C File Offset: 0x0000744C
		public static string RuntimeDisplayName
		{
			get
			{
				Type type = Type.GetType("Mono.Runtime");
				return (type == null) ? ("CLR " + Environment.Version.ToString()) : ((string)type.GetMethod("GetDisplayName", 40).Invoke(null, null));
			}
		}

		// Token: 0x04000146 RID: 326
		internal const BindingFlags InstanceMemberFlags = 52;

		// Token: 0x04000147 RID: 327
		internal const BindingFlags DefaultBindingFlags = 262268;

		// Token: 0x04000148 RID: 328
		private const BindingFlags InvokeBindingFlags = 262524;

		// Token: 0x04000149 RID: 329
		private const BindingFlags SetPropertyBindingFlags = 272508;

		// Token: 0x0400014A RID: 330
		private const BindingFlags GetPropertyBindingFlags = 267388;

		// Token: 0x0400014B RID: 331
		private static readonly object[] NoArguments = new object[0];

		// Token: 0x0400014C RID: 332
		private static readonly Type RuntimeServicesType = typeof(RuntimeServices);

		// Token: 0x0400014D RID: 333
		private static readonly DispatcherCache _cache = new DispatcherCache();

		// Token: 0x0400014E RID: 334
		private static readonly ExtensionRegistry _extensions = new ExtensionRegistry();

		// Token: 0x0400014F RID: 335
		private static readonly object True = true;

		// Token: 0x0200003F RID: 63
		public struct ValueTypeChange
		{
			// Token: 0x06000267 RID: 615 RVA: 0x0000929C File Offset: 0x0000749C
			public ValueTypeChange(object target, string member, object value)
			{
				this.Target = target;
				this.Member = member;
				this.Value = value;
			}

			// Token: 0x04000150 RID: 336
			public object Target;

			// Token: 0x04000151 RID: 337
			public string Member;

			// Token: 0x04000152 RID: 338
			public object Value;
		}

		// Token: 0x02000045 RID: 69
		// (Invoke) Token: 0x06000276 RID: 630
		public delegate void CodeBlock();
	}
}
