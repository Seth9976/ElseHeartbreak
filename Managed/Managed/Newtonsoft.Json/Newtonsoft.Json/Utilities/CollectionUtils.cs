using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000BA RID: 186
	internal static class CollectionUtils
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x0001DD0C File Offset: 0x0001BF0C
		public static IEnumerable<T> CastValid<T>(this IEnumerable enumerable)
		{
			ValidationUtils.ArgumentNotNull(enumerable, "enumerable");
			return (from object o in enumerable
				where o is T
				select o).Cast<T>();
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001DD35 File Offset: 0x0001BF35
		public static List<T> CreateList<T>(params T[] values)
		{
			return new List<T>(values);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001DD3D File Offset: 0x0001BF3D
		public static bool IsNullOrEmpty(ICollection collection)
		{
			return collection == null || collection.Count == 0;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001DD4D File Offset: 0x0001BF4D
		public static bool IsNullOrEmpty<T>(ICollection<T> collection)
		{
			return collection == null || collection.Count == 0;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001DD5D File Offset: 0x0001BF5D
		public static bool IsNullOrEmptyOrDefault<T>(IList<T> list)
		{
			return CollectionUtils.IsNullOrEmpty<T>(list) || ReflectionUtils.ItemsUnitializedValue<T>(list);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001DD70 File Offset: 0x0001BF70
		public static IList<T> Slice<T>(IList<T> list, int? start, int? end)
		{
			return CollectionUtils.Slice<T>(list, start, end, null);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0001DD90 File Offset: 0x0001BF90
		public static IList<T> Slice<T>(IList<T> list, int? start, int? end, int? step)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (step == 0)
			{
				throw new ArgumentException("Step cannot be zero.", "step");
			}
			List<T> list2 = new List<T>();
			if (list.Count == 0)
			{
				return list2;
			}
			int num = step ?? 1;
			int num2 = start ?? 0;
			int num3 = end ?? list.Count;
			num2 = ((num2 < 0) ? (list.Count + num2) : num2);
			num3 = ((num3 < 0) ? (list.Count + num3) : num3);
			num2 = Math.Max(num2, 0);
			num3 = Math.Min(num3, list.Count - 1);
			for (int i = num2; i < num3; i += num)
			{
				list2.Add(list[i]);
			}
			return list2;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001DE84 File Offset: 0x0001C084
		public static Dictionary<K, List<V>> GroupBy<K, V>(ICollection<V> source, Func<V, K> keySelector)
		{
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			Dictionary<K, List<V>> dictionary = new Dictionary<K, List<V>>();
			foreach (V v in source)
			{
				K k = keySelector(v);
				List<V> list;
				if (!dictionary.TryGetValue(k, out list))
				{
					list = new List<V>();
					dictionary.Add(k, list);
				}
				list.Add(v);
			}
			return dictionary;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001DF08 File Offset: 0x0001C108
		public static void AddRange<T>(this IList<T> initial, IEnumerable<T> collection)
		{
			if (initial == null)
			{
				throw new ArgumentNullException("initial");
			}
			if (collection == null)
			{
				return;
			}
			foreach (T t in collection)
			{
				initial.Add(t);
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001DF64 File Offset: 0x0001C164
		public static void AddRange(this IList initial, IEnumerable collection)
		{
			ValidationUtils.ArgumentNotNull(initial, "initial");
			ListWrapper<object> listWrapper = new ListWrapper<object>(initial);
			listWrapper.AddRange(collection.Cast<object>());
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001DF90 File Offset: 0x0001C190
		public static List<T> Distinct<T>(List<T> collection)
		{
			List<T> list = new List<T>();
			foreach (T t in collection)
			{
				if (!list.Contains(t))
				{
					list.Add(t);
				}
			}
			return list;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001DFF0 File Offset: 0x0001C1F0
		public static List<List<T>> Flatten<T>(params IList<T>[] lists)
		{
			List<List<T>> list = new List<List<T>>();
			Dictionary<int, T> dictionary = new Dictionary<int, T>();
			CollectionUtils.Recurse<T>(new List<IList<T>>(lists), 0, dictionary, list);
			return list;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001E018 File Offset: 0x0001C218
		private static void Recurse<T>(IList<IList<T>> global, int current, Dictionary<int, T> currentSet, List<List<T>> flattenedResult)
		{
			IList<T> list = global[current];
			for (int i = 0; i < list.Count; i++)
			{
				currentSet[current] = list[i];
				if (current == global.Count - 1)
				{
					List<T> list2 = new List<T>();
					for (int j = 0; j < currentSet.Count; j++)
					{
						list2.Add(currentSet[j]);
					}
					flattenedResult.Add(list2);
				}
				else
				{
					CollectionUtils.Recurse<T>(global, current + 1, currentSet, flattenedResult);
				}
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001E090 File Offset: 0x0001C290
		public static List<T> CreateList<T>(ICollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			T[] array = new T[collection.Count];
			collection.CopyTo(array, 0);
			return new List<T>(array);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001E0C8 File Offset: 0x0001C2C8
		public static bool ListEquals<T>(IList<T> a, IList<T> b)
		{
			if (a == null || b == null)
			{
				return a == null && b == null;
			}
			if (a.Count != b.Count)
			{
				return false;
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int i = 0; i < a.Count; i++)
			{
				if (!@default.Equals(a[i], b[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001E125 File Offset: 0x0001C325
		public static bool TryGetSingleItem<T>(IList<T> list, out T value)
		{
			return CollectionUtils.TryGetSingleItem<T>(list, false, out value);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001E14C File Offset: 0x0001C34C
		public static bool TryGetSingleItem<T>(IList<T> list, bool returnDefaultIfEmpty, out T value)
		{
			return MiscellaneousUtils.TryAction<T>(() => CollectionUtils.GetSingleItem<T>(list, returnDefaultIfEmpty), out value);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001E17F File Offset: 0x0001C37F
		public static T GetSingleItem<T>(IList<T> list)
		{
			return CollectionUtils.GetSingleItem<T>(list, false);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001E188 File Offset: 0x0001C388
		public static T GetSingleItem<T>(IList<T> list, bool returnDefaultIfEmpty)
		{
			if (list.Count == 1)
			{
				return list[0];
			}
			if (returnDefaultIfEmpty && list.Count == 0)
			{
				return default(T);
			}
			throw new Exception("Expected single {0} in list but got {1}.".FormatWith(CultureInfo.InvariantCulture, new object[]
			{
				typeof(T),
				list.Count
			}));
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0001E1F4 File Offset: 0x0001C3F4
		public static IList<T> Minus<T>(IList<T> list, IList<T> minus)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			List<T> list2 = new List<T>(list.Count);
			foreach (T t in list)
			{
				if (minus == null || !minus.Contains(t))
				{
					list2.Add(t);
				}
			}
			return list2;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001E260 File Offset: 0x0001C460
		public static IList CreateGenericList(Type listType)
		{
			ValidationUtils.ArgumentNotNull(listType, "listType");
			return (IList)ReflectionUtils.CreateGeneric(typeof(List<>), listType, new object[0]);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001E288 File Offset: 0x0001C488
		public static IDictionary CreateGenericDictionary(Type keyType, Type valueType)
		{
			ValidationUtils.ArgumentNotNull(keyType, "keyType");
			ValidationUtils.ArgumentNotNull(valueType, "valueType");
			return (IDictionary)ReflectionUtils.CreateGeneric(typeof(Dictionary<, >), keyType, new object[] { valueType });
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001E2CC File Offset: 0x0001C4CC
		public static bool IsListType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			return type.IsArray || typeof(IList).IsAssignableFrom(type) || ReflectionUtils.ImplementsGenericDefinition(type, typeof(IList<>));
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001E30C File Offset: 0x0001C50C
		public static bool IsCollectionType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			return type.IsArray || typeof(ICollection).IsAssignableFrom(type) || ReflectionUtils.ImplementsGenericDefinition(type, typeof(ICollection<>));
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001E34C File Offset: 0x0001C54C
		public static bool IsDictionaryType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			return typeof(IDictionary).IsAssignableFrom(type) || ReflectionUtils.ImplementsGenericDefinition(type, typeof(IDictionary<, >));
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001E3C8 File Offset: 0x0001C5C8
		public static IWrappedCollection CreateCollectionWrapper(object list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			Type collectionDefinition;
			if (ReflectionUtils.ImplementsGenericDefinition(list.GetType(), typeof(ICollection<>), out collectionDefinition))
			{
				Type collectionItemType = ReflectionUtils.GetCollectionItemType(collectionDefinition);
				Func<Type, IList<object>, object> func = delegate(Type t, IList<object> a)
				{
					ConstructorInfo constructor = t.GetConstructor(new Type[] { collectionDefinition });
					return constructor.Invoke(new object[] { list });
				};
				return (IWrappedCollection)ReflectionUtils.CreateGeneric(typeof(CollectionWrapper<>), new Type[] { collectionItemType }, func, new object[] { list });
			}
			if (list is IList)
			{
				return new CollectionWrapper<object>((IList)list);
			}
			throw new Exception("Can not create ListWrapper for type {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { list.GetType() }));
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0001E4FC File Offset: 0x0001C6FC
		public static IWrappedList CreateListWrapper(object list)
		{
			ValidationUtils.ArgumentNotNull(list, "list");
			Type listDefinition;
			if (ReflectionUtils.ImplementsGenericDefinition(list.GetType(), typeof(IList<>), out listDefinition))
			{
				Type collectionItemType = ReflectionUtils.GetCollectionItemType(listDefinition);
				Func<Type, IList<object>, object> func = delegate(Type t, IList<object> a)
				{
					ConstructorInfo constructor = t.GetConstructor(new Type[] { listDefinition });
					return constructor.Invoke(new object[] { list });
				};
				return (IWrappedList)ReflectionUtils.CreateGeneric(typeof(ListWrapper<>), new Type[] { collectionItemType }, func, new object[] { list });
			}
			if (list is IList)
			{
				return new ListWrapper<object>((IList)list);
			}
			throw new Exception("Can not create ListWrapper for type {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { list.GetType() }));
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0001E630 File Offset: 0x0001C830
		public static IWrappedDictionary CreateDictionaryWrapper(object dictionary)
		{
			ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
			Type dictionaryDefinition;
			if (ReflectionUtils.ImplementsGenericDefinition(dictionary.GetType(), typeof(IDictionary<, >), out dictionaryDefinition))
			{
				Type dictionaryKeyType = ReflectionUtils.GetDictionaryKeyType(dictionaryDefinition);
				Type dictionaryValueType = ReflectionUtils.GetDictionaryValueType(dictionaryDefinition);
				Func<Type, IList<object>, object> func = delegate(Type t, IList<object> a)
				{
					ConstructorInfo constructor = t.GetConstructor(new Type[] { dictionaryDefinition });
					return constructor.Invoke(new object[] { dictionary });
				};
				return (IWrappedDictionary)ReflectionUtils.CreateGeneric(typeof(DictionaryWrapper<, >), new Type[] { dictionaryKeyType, dictionaryValueType }, func, new object[] { dictionary });
			}
			if (dictionary is IDictionary)
			{
				return new DictionaryWrapper<object, object>((IDictionary)dictionary);
			}
			throw new Exception("Can not create DictionaryWrapper for type {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { dictionary.GetType() }));
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001E740 File Offset: 0x0001C940
		public static object CreateAndPopulateList(Type listType, Action<IList, bool> populateList)
		{
			ValidationUtils.ArgumentNotNull(listType, "listType");
			ValidationUtils.ArgumentNotNull(populateList, "populateList");
			bool flag = false;
			IList list;
			Type type;
			if (listType.IsArray)
			{
				list = new List<object>();
				flag = true;
			}
			else if (ReflectionUtils.InheritsGenericDefinition(listType, typeof(ReadOnlyCollection<>), out type))
			{
				Type type2 = type.GetGenericArguments()[0];
				Type type3 = ReflectionUtils.MakeGenericType(typeof(IEnumerable<>), new Type[] { type2 });
				bool flag2 = false;
				foreach (ConstructorInfo constructorInfo in listType.GetConstructors())
				{
					IList<ParameterInfo> parameters = constructorInfo.GetParameters();
					if (parameters.Count == 1 && type3.IsAssignableFrom(parameters[0].ParameterType))
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					throw new Exception("Read-only type {0} does not have a public constructor that takes a type that implements {1}.".FormatWith(CultureInfo.InvariantCulture, new object[] { listType, type3 }));
				}
				list = CollectionUtils.CreateGenericList(type2);
				flag = true;
			}
			else if (typeof(IList).IsAssignableFrom(listType))
			{
				if (ReflectionUtils.IsInstantiatableType(listType))
				{
					list = (IList)Activator.CreateInstance(listType);
				}
				else if (listType == typeof(IList))
				{
					list = new List<object>();
				}
				else
				{
					list = null;
				}
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(listType, typeof(ICollection<>)))
			{
				if (ReflectionUtils.IsInstantiatableType(listType))
				{
					list = CollectionUtils.CreateCollectionWrapper(Activator.CreateInstance(listType));
				}
				else
				{
					list = null;
				}
			}
			else
			{
				list = null;
			}
			if (list == null)
			{
				throw new Exception("Cannot create and populate list type {0}.".FormatWith(CultureInfo.InvariantCulture, new object[] { listType }));
			}
			populateList(list, flag);
			if (flag)
			{
				if (listType.IsArray)
				{
					list = CollectionUtils.ToArray(((List<object>)list).ToArray(), ReflectionUtils.GetCollectionItemType(listType));
				}
				else if (ReflectionUtils.InheritsGenericDefinition(listType, typeof(ReadOnlyCollection<>)))
				{
					list = (IList)ReflectionUtils.CreateInstance(listType, new object[] { list });
				}
			}
			else if (list is IWrappedCollection)
			{
				return ((IWrappedCollection)list).UnderlyingCollection;
			}
			return list;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001E94C File Offset: 0x0001CB4C
		public static Array ToArray(Array initial, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Array array = Array.CreateInstance(type, initial.Length);
			Array.Copy(initial, 0, array, 0, initial.Length);
			return array;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001E984 File Offset: 0x0001CB84
		public static bool AddDistinct<T>(this IList<T> list, T value)
		{
			return list.AddDistinct(value, EqualityComparer<T>.Default);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001E992 File Offset: 0x0001CB92
		public static bool AddDistinct<T>(this IList<T> list, T value, IEqualityComparer<T> comparer)
		{
			if (list.ContainsValue(value, comparer))
			{
				return false;
			}
			list.Add(value);
			return true;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
		public static bool ContainsValue<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TSource>.Default;
			}
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			foreach (TSource tsource in source)
			{
				if (comparer.Equals(tsource, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001EA14 File Offset: 0x0001CC14
		public static bool AddRangeDistinct<T>(this IList<T> list, IEnumerable<T> values)
		{
			return list.AddRangeDistinct(values, EqualityComparer<T>.Default);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001EA24 File Offset: 0x0001CC24
		public static bool AddRangeDistinct<T>(this IList<T> list, IEnumerable<T> values, IEqualityComparer<T> comparer)
		{
			bool flag = true;
			foreach (T t in values)
			{
				if (!list.AddDistinct(t, comparer))
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001EA74 File Offset: 0x0001CC74
		public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
		{
			int num = 0;
			foreach (T t in collection)
			{
				if (predicate(t))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001EACC File Offset: 0x0001CCCC
		public static int IndexOf<TSource>(this IEnumerable<TSource> list, TSource value) where TSource : IEquatable<TSource>
		{
			return list.IndexOf(value, EqualityComparer<TSource>.Default);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001EADC File Offset: 0x0001CCDC
		public static int IndexOf<TSource>(this IEnumerable<TSource> list, TSource value, IEqualityComparer<TSource> comparer)
		{
			int num = 0;
			foreach (TSource tsource in list)
			{
				if (comparer.Equals(tsource, value))
				{
					return num;
				}
				num++;
			}
			return -1;
		}
	}
}
