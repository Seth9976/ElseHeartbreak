using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000027 RID: 39
	public class ReturnValueConversions
	{
		// Token: 0x0600015B RID: 347 RVA: 0x0000A568 File Offset: 0x00008768
		public static T SafeUnwrap<T>(object[] args, int index)
		{
			if (args[index].GetType() == typeof(T))
			{
				return (T)((object)args[index]);
			}
			throw new Error(string.Concat(new object[]
			{
				"Arg ",
				index,
				" is of wrong type (",
				ReturnValueConversions.PrettyObjectType(args[index].GetType()),
				"), should be ",
				ReturnValueConversions.PrettyObjectType(typeof(T))
			}));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A5E8 File Offset: 0x000087E8
		public static ReturnValueType SystemTypeToReturnValueType(Type t)
		{
			if (t == typeof(SortedDictionary<KeyWrapper, object>))
			{
				return ReturnValueType.ARRAY;
			}
			if (t == typeof(float))
			{
				return ReturnValueType.NUMBER;
			}
			if (t == typeof(bool))
			{
				return ReturnValueType.BOOL;
			}
			if (t == typeof(string))
			{
				return ReturnValueType.STRING;
			}
			if (t == typeof(Range))
			{
				return ReturnValueType.RANGE;
			}
			if (t == typeof(VoidType))
			{
				return ReturnValueType.VOID;
			}
			return ReturnValueType.UNKNOWN_TYPE;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A664 File Offset: 0x00008864
		public static string PrettyObjectType(Type t)
		{
			if (t == typeof(SortedDictionary<KeyWrapper, object>))
			{
				return "array";
			}
			string text;
			if (ReturnValueConversions.typeToString.TryGetValue(t, out text))
			{
				return text;
			}
			return "unknown";
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A6A0 File Offset: 0x000088A0
		public static string PrettyStringRepresenation(object o)
		{
			if (o.GetType() == typeof(string))
			{
				return (string)o;
			}
			if (o.GetType() == typeof(bool))
			{
				return (!(bool)o) ? "false" : "true";
			}
			if (o.GetType() == typeof(float))
			{
				return o.ToString();
			}
			if (o.GetType() == typeof(int))
			{
				return o.ToString() + "i";
			}
			if (o.GetType() == typeof(object[]))
			{
				return ReturnValueConversions.MakePrimitiveObjectArrayString((object[])o);
			}
			if (o is Range)
			{
				return ((Range)o).ToString();
			}
			if (o is KeyWrapper)
			{
				return "{" + ((KeyWrapper)o).value.ToString() + "}";
			}
			if (o is SortedDictionary<KeyWrapper, object>)
			{
				return ReturnValueConversions.MakeArrayString(o as SortedDictionary<KeyWrapper, object>);
			}
			if (o.GetType() == typeof(UnknownType))
			{
				return o.ToString();
			}
			throw new Error(string.Concat(new object[]
			{
				"Can't pretty print ",
				o.ToString(),
				" of type ",
				o.GetType()
			}));
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000A810 File Offset: 0x00008A10
		private static string MakePrimitiveObjectArrayString(object[] array)
		{
			if (array != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("@[");
				int num = array.Length;
				int num2 = 0;
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(ReturnValueConversions.PrettyStringRepresenation(array[i]));
					num--;
					if (num > 0)
					{
						stringBuilder.Append(", ");
					}
					num2++;
					if (num2 > 10)
					{
						stringBuilder.Append("...");
						break;
					}
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}
			return "";
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000A8AC File Offset: 0x00008AAC
		private static string MakeArrayString(SortedDictionary<KeyWrapper, object> array)
		{
			if (array != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("[");
				int num = array.Count;
				int num2 = 0;
				foreach (KeyWrapper keyWrapper in array.Keys)
				{
					stringBuilder.Append(ReturnValueConversions.PrettyStringRepresenation(array[keyWrapper]));
					num--;
					if (num > 0)
					{
						stringBuilder.Append(", ");
					}
					num2++;
					if (num2 > 10)
					{
						stringBuilder.Append("...");
						break;
					}
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}
			return "";
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000A990 File Offset: 0x00008B90
		public static object ChangeTypeBasedOnReturnValueType(object obj, ReturnValueType type)
		{
			if (type == ReturnValueType.STRING)
			{
				return ReturnValueConversions.PrettyStringRepresenation(obj);
			}
			if (type == ReturnValueType.NUMBER)
			{
				if (obj.GetType() == typeof(float))
				{
					return (float)obj;
				}
				if (obj.GetType() == typeof(int))
				{
					return (float)((int)obj);
				}
				if (obj.GetType() == typeof(string))
				{
					try
					{
						return (float)Convert.ToDouble((string)obj, CultureInfo.InvariantCulture);
					}
					catch (FormatException)
					{
						throw new Error("Can't convert " + obj.ToString() + " to a number");
					}
				}
			}
			else
			{
				if (type == ReturnValueType.RANGE)
				{
					return (Range)obj;
				}
				if (type == ReturnValueType.ARRAY)
				{
					if (obj.GetType() == typeof(object[]))
					{
						return obj;
					}
					if (obj.GetType() == typeof(SortedDictionary<KeyWrapper, object>))
					{
						return obj;
					}
					if (obj.GetType() == typeof(Range))
					{
						return obj;
					}
					if (obj.GetType() == typeof(string))
					{
						SortedDictionary<KeyWrapper, object> sortedDictionary = new SortedDictionary<KeyWrapper, object>();
						string text = (string)obj;
						for (int i = 0; i < text.Length; i++)
						{
							sortedDictionary.Add(new KeyWrapper(i), text);
						}
						return sortedDictionary;
					}
					throw new Error("Can't convert " + obj.ToString() + " to an array");
				}
				else
				{
					if (type == ReturnValueType.BOOL)
					{
						return (bool)obj;
					}
					if (type == ReturnValueType.UNKNOWN_TYPE)
					{
						return obj;
					}
				}
			}
			throw new Exception(string.Concat(new object[]
			{
				"Can't change type from ",
				obj.GetType(),
				" to ",
				type
			}));
		}

		// Token: 0x040000B8 RID: 184
		private static Dictionary<Type, string> typeToString = new Dictionary<Type, string>
		{
			{
				typeof(float),
				"number"
			},
			{
				typeof(string),
				"string"
			},
			{
				typeof(bool),
				"bool"
			},
			{
				typeof(object[]),
				"prim-array"
			}
		};
	}
}
