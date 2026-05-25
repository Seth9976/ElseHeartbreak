using System;
using System.Collections.Generic;
using System.Reflection;
using GameTypes;

namespace RelayLib
{
	// Token: 0x0200000F RID: 15
	public class InstantiatorTwo
	{
		// Token: 0x06000072 RID: 114 RVA: 0x0000411C File Offset: 0x0000231C
		public static Type[] GetSubclasses(Type baseType)
		{
			List<Type> list = new List<Type>();
			List<Type> list2 = new List<Type>();
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				try
				{
					if (!assembly.GetName().Name.Contains("NUnit"))
					{
						list2.AddRange(assembly.GetTypes());
					}
				}
				catch (ReflectionTypeLoadException)
				{
					Console.WriteLine("There was an error loading the assembly " + assembly.FullName + " ");
				}
			}
			foreach (Type type in list2.ToArray())
			{
				if (type.IsSubclassOf(baseType))
				{
					list.Add(type);
				}
			}
			list.Add(baseType);
			return list.ToArray();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004214 File Offset: 0x00002414
		public static Type GetType(string pName, Type[] pTypeCollection)
		{
			foreach (Type type in pTypeCollection)
			{
				if (type.Name == pName)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004250 File Offset: 0x00002450
		public static List<T> Process<T>(TableTwo pTable, Type[] pSubTypes) where T : RelayObjectTwo
		{
			List<T> list = new List<T>();
			foreach (TableRow tableRow in pTable)
			{
				string text = tableRow.Get<string>(RelayObjectTwo.CSHARP_CLASS_FIELD_NAME);
				Type type = InstantiatorTwo.GetType(text, pSubTypes);
				if (type == null)
				{
					throw new CantFindClassWithNameException("Can't find class with name " + text + " to instantiate");
				}
				T t = Activator.CreateInstance(type) as T;
				D.isNull(t);
				t.LoadFromExistingRelayEntry(pTable, tableRow.row, text);
				list.Add(t);
			}
			return list;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004328 File Offset: 0x00002528
		public static List<T> Process<T>(TableTwo pTable, Type pType) where T : RelayObjectTwo
		{
			Type[] subclasses = InstantiatorTwo.GetSubclasses(pType);
			return InstantiatorTwo.Process<T>(pTable, subclasses);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004344 File Offset: 0x00002544
		public static List<T> Process<T>(TableTwo pTable) where T : RelayObjectTwo
		{
			return InstantiatorTwo.Process<T>(pTable, typeof(T));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004358 File Offset: 0x00002558
		public static T Create<T>(TableTwo pTable) where T : RelayObjectTwo
		{
			Type typeFromHandle = typeof(T);
			T t = Activator.CreateInstance(typeFromHandle) as T;
			t.CreateNewRelayEntry(pTable, typeFromHandle.Name);
			return t;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004398 File Offset: 0x00002598
		private static void PrintSubTypes(List<Type> subTypes)
		{
			foreach (Type type in subTypes)
			{
				Console.WriteLine(type.Name);
			}
			Console.ReadKey();
			Console.WriteLine("= " + subTypes.Count);
		}
	}
}
