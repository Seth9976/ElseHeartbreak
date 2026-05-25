using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Boo.Lang.Runtime;

namespace Boo.Lang
{
	// Token: 0x02000006 RID: 6
	public class Builtins
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002340 File Offset: 0x00000540
		public static Version BooVersion
		{
			get
			{
				return new Version("0.9.7.0");
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000234C File Offset: 0x0000054C
		public static void print(object o)
		{
			Console.WriteLine(o);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002354 File Offset: 0x00000554
		public static string gets()
		{
			return Console.ReadLine();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000235C File Offset: 0x0000055C
		public static string prompt(string message)
		{
			Console.Write(message);
			return Console.ReadLine();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000236C File Offset: 0x0000056C
		public static string join(IEnumerable enumerable, string separator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			IEnumerator enumerator = enumerable.GetEnumerator();
			using (enumerator as IDisposable)
			{
				if (enumerator.MoveNext())
				{
					stringBuilder.Append(enumerator.Current);
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(separator);
						stringBuilder.Append(enumerator.Current);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002400 File Offset: 0x00000600
		public static string join(IEnumerable enumerable, char separator)
		{
			return Builtins.join(enumerable, separator.ToString());
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002410 File Offset: 0x00000610
		public static string join(IEnumerable enumerable)
		{
			return Builtins.join(enumerable, " ");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002420 File Offset: 0x00000620
		public static IEnumerable map(object enumerable, ICallable function)
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			object[] args = new object[1];
			foreach (object item in Builtins.iterator(enumerable))
			{
				args[0] = item;
				yield return function.Call(args);
			}
			yield break;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002458 File Offset: 0x00000658
		public static object[] array(IEnumerable enumerable)
		{
			return new List(enumerable).ToArray();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002468 File Offset: 0x00000668
		private static Array ArrayFromCollection(Type elementType, ICollection collection)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			Array array = Array.CreateInstance(elementType, collection.Count);
			if (RuntimeServices.IsPromotableNumeric(Type.GetTypeCode(elementType)))
			{
				int num = 0;
				foreach (object obj in collection)
				{
					object obj2 = RuntimeServices.CheckNumericPromotion(obj).ToType(elementType, null);
					array.SetValue(obj2, num);
					num++;
				}
			}
			else
			{
				collection.CopyTo(array, 0);
			}
			return array;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002534 File Offset: 0x00000734
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		public static Array array(Type elementType, IEnumerable enumerable)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			ICollection collection = enumerable as ICollection;
			if (collection != null)
			{
				return Builtins.ArrayFromCollection(elementType, collection);
			}
			List list = null;
			if (RuntimeServices.IsPromotableNumeric(Type.GetTypeCode(elementType)))
			{
				list = new List();
				foreach (object obj in enumerable)
				{
					object obj2 = RuntimeServices.CheckNumericPromotion(obj).ToType(elementType, null);
					list.Add(obj2);
				}
			}
			else
			{
				list = new List(enumerable);
			}
			return list.ToArray(elementType);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002610 File Offset: 0x00000810
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		public static Array array(Type elementType, int length)
		{
			if (length < 0)
			{
				throw new ArgumentException("`length' cannot be negative", "length");
			}
			return Builtins.matrix(elementType, new int[] { length });
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000263C File Offset: 0x0000083C
		public static Array matrix(Type elementType, params int[] lengths)
		{
			if (elementType == null)
			{
				throw new ArgumentNullException("elementType");
			}
			if (lengths == null || lengths.Length == 0)
			{
				throw new ArgumentException("A matrix must have at least one dimension", "lengths");
			}
			return Array.CreateInstance(elementType, lengths);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002680 File Offset: 0x00000880
		public static T[] array<T>(int length)
		{
			throw new NotSupportedException("Operation should have been optimized away by the compiler!");
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000268C File Offset: 0x0000088C
		public static T[,] matrix<T>(int length0, int length1)
		{
			throw new NotSupportedException("Operation should have been optimized away by the compiler!");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002698 File Offset: 0x00000898
		public static T[,,] matrix<T>(int length0, int length1, int length2)
		{
			throw new NotSupportedException("Operation should have been optimized away by the compiler!");
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026A4 File Offset: 0x000008A4
		public static T[,,,] matrix<T>(int length0, int length1, int length2, int length3)
		{
			throw new NotSupportedException("Operation should have been optimized away by the compiler!");
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026B0 File Offset: 0x000008B0
		public static IEnumerable iterator(object enumerable)
		{
			return RuntimeServices.GetEnumerable(enumerable);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026B8 File Offset: 0x000008B8
		public static Process shellp(string filename, string arguments)
		{
			Process process = new Process();
			process.StartInfo.Arguments = arguments;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardInput = true;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.FileName = filename;
			process.Start();
			return process;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002728 File Offset: 0x00000928
		public static string shell(string filename, string arguments)
		{
			Process process = Builtins.shellp(filename, arguments);
			string text = process.StandardOutput.ReadToEnd();
			process.WaitForExit();
			return text;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002750 File Offset: 0x00000950
		public static IEnumerable<object[]> enumerate(object enumerable)
		{
			int i = 0;
			foreach (object item in Builtins.iterator(enumerable))
			{
				object[] array = new object[2];
				int num = 0;
				int num2;
				i = (num2 = i) + 1;
				array[num] = num2;
				array[1] = item;
				yield return array;
			}
			yield break;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000277C File Offset: 0x0000097C
		public static IEnumerable<int> range(int max)
		{
			if (max < 0)
			{
				throw new ArgumentOutOfRangeException("max < 0");
			}
			return Builtins.range(0, max);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002798 File Offset: 0x00000998
		public static IEnumerable<int> range(int begin, int end)
		{
			if (begin < end)
			{
				for (int i = begin; i < end; i++)
				{
					yield return i;
				}
			}
			else if (begin > end)
			{
				for (int j = begin; j > end; j--)
				{
					yield return j;
				}
			}
			yield break;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027D0 File Offset: 0x000009D0
		public static IEnumerable<int> range(int begin, int end, int step)
		{
			if (step == 0)
			{
				throw new ArgumentOutOfRangeException("step == 0");
			}
			if (step < 0)
			{
				if (begin < end)
				{
					throw new ArgumentOutOfRangeException("begin < end && step < 0");
				}
				for (int i = begin; i > end; i += step)
				{
					yield return i;
				}
			}
			else
			{
				if (begin > end)
				{
					throw new ArgumentOutOfRangeException("begin > end && step > 0");
				}
				for (int j = begin; j < end; j += step)
				{
					yield return j;
				}
			}
			yield break;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002818 File Offset: 0x00000A18
		public static IEnumerable reversed(object enumerable)
		{
			return new List(Builtins.iterator(enumerable)).Reversed;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000282C File Offset: 0x00000A2C
		public static Builtins.ZipEnumerator zip(params object[] enumerables)
		{
			IEnumerator[] array = new IEnumerator[enumerables.Length];
			for (int i = 0; i < enumerables.Length; i++)
			{
				array[i] = Builtins.GetEnumerator(enumerables[i]);
			}
			return new Builtins.ZipEnumerator(array);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002868 File Offset: 0x00000A68
		public static IEnumerable<object> cat(params object[] args)
		{
			foreach (object e in args)
			{
				foreach (object item in Builtins.iterator(e))
				{
					yield return item;
				}
			}
			yield break;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002894 File Offset: 0x00000A94
		private static IEnumerator GetEnumerator(object enumerable)
		{
			return RuntimeServices.GetEnumerable(enumerable).GetEnumerator();
		}

		// Token: 0x02000007 RID: 7
		public class duck
		{
		}

		// Token: 0x02000008 RID: 8
		[EnumeratorItemType(typeof(object[]))]
		public class ZipEnumerator : IEnumerable, IEnumerator, IDisposable
		{
			// Token: 0x06000030 RID: 48 RVA: 0x000028AC File Offset: 0x00000AAC
			internal ZipEnumerator(params IEnumerator[] enumerators)
			{
				this._enumerators = enumerators;
			}

			// Token: 0x06000031 RID: 49 RVA: 0x000028BC File Offset: 0x00000ABC
			public void Dispose()
			{
				for (int i = 0; i < this._enumerators.Length; i++)
				{
					IDisposable disposable = this._enumerators[i] as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}

			// Token: 0x06000032 RID: 50 RVA: 0x000028FC File Offset: 0x00000AFC
			public void Reset()
			{
				for (int i = 0; i < this._enumerators.Length; i++)
				{
					this._enumerators[i].Reset();
				}
			}

			// Token: 0x06000033 RID: 51 RVA: 0x00002930 File Offset: 0x00000B30
			public bool MoveNext()
			{
				for (int i = 0; i < this._enumerators.Length; i++)
				{
					if (!this._enumerators[i].MoveNext())
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000034 RID: 52 RVA: 0x0000296C File Offset: 0x00000B6C
			public object Current
			{
				get
				{
					object[] array = new object[this._enumerators.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this._enumerators[i].Current;
					}
					return array;
				}
			}

			// Token: 0x06000035 RID: 53 RVA: 0x000029AC File Offset: 0x00000BAC
			public IEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x04000005 RID: 5
			private IEnumerator[] _enumerators;
		}
	}
}
