using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000004 RID: 4
	public class FunctionDefinitionCreator
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021A8 File Offset: 0x000003A8
		public static void Log(string pString)
		{
			if (FunctionDefinitionCreator.logger != null)
			{
				FunctionDefinitionCreator.logger(pString);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021C0 File Offset: 0x000003C0
		public static FunctionDefinition[] CreateDefinitions(object pProgramTarget, Type pClassType)
		{
			MethodInfo[] methods = pClassType.GetMethods();
			Dictionary<string, FunctionDocumentation> dictionary = FunctionDefinitionCreator.CreateDocumentation(methods);
			List<FunctionDefinition> list = FunctionDefinitionCreator.CreateFunctionDefinitions(pProgramTarget, dictionary, methods);
			return list.ToArray();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021EC File Offset: 0x000003EC
		private static Dictionary<string, FunctionDocumentation> CreateDocumentation(MethodInfo[] methodInfos)
		{
			Dictionary<string, FunctionDocumentation> dictionary = new Dictionary<string, FunctionDocumentation>();
			foreach (MethodInfo methodInfo in methodInfos)
			{
				if (methodInfo.Name.StartsWith("API_"))
				{
					SprakAPI[] array = (SprakAPI[])methodInfo.GetCustomAttributes(typeof(SprakAPI), true);
					if (array.Length > 0)
					{
						List<string> list = new List<string>();
						for (int j = 1; j < array[0].Values.Length; j++)
						{
							list.Add(array[0].Values[j]);
						}
						string text = methodInfo.Name.Substring(4);
						FunctionDocumentation functionDocumentation = new FunctionDocumentation(array[0].Values[0], list.ToArray());
						dictionary.Add(text, functionDocumentation);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022BC File Offset: 0x000004BC
		private static List<FunctionDefinition> CreateFunctionDefinitions(object pProgramTarget, Dictionary<string, FunctionDocumentation> functionDocumentations, MethodInfo[] methodInfos)
		{
			List<FunctionDefinition> list = new List<FunctionDefinition>();
			for (int i = 0; i < methodInfos.Length; i++)
			{
				MethodInfo methodInfo = methodInfos[i];
				if (methodInfo.Name.StartsWith("API_"))
				{
					MethodInfo lambdaMethodInfo = methodInfo;
					string shortname = lambdaMethodInfo.Name.Substring(4);
					List<ReturnValueType> list2 = new List<ReturnValueType>();
					List<string> list3 = new List<string>();
					List<string> list4 = new List<string>();
					foreach (ParameterInfo parameterInfo in lambdaMethodInfo.GetParameters())
					{
						ReturnValueType returnValueType = ReturnValueConversions.SystemTypeToReturnValueType(parameterInfo.ParameterType);
						list3.Add(parameterInfo.Name);
						list2.Add(returnValueType);
						list4.Add(returnValueType.ToString().ToLower());
					}
					ExternalFunctionCreator.OnFunctionCall onFunctionCall = delegate(object[] sprakArguments)
					{
						ParameterInfo[] parameters2 = lambdaMethodInfo.GetParameters();
						if (sprakArguments.Count<object>() != parameters2.Length)
						{
							throw new Error(string.Concat(new object[]
							{
								"Should call '",
								shortname,
								"' with ",
								parameters2.Length,
								" argument",
								(parameters2.Length != 1) ? "s" : ""
							}));
						}
						int num = 0;
						foreach (object obj in sprakArguments)
						{
							Type type = parameters2[num].ParameterType;
							if (obj.GetType() == typeof(SortedDictionary<KeyWrapper, object>))
							{
								sprakArguments[num] = (obj as SortedDictionary<KeyWrapper, object>).Values.ToArray<object>();
							}
							if (obj.GetType() == typeof(int))
							{
								sprakArguments[num] = (float)obj;
								type = typeof(int);
							}
							if (!FunctionDefinitionCreator.acceptableTypes.Contains(type))
							{
								throw new Error(string.Concat(new object[]
								{
									"Can't deal with parameter ",
									num.ToString(),
									" of type ",
									type,
									" in function ",
									shortname
								}));
							}
							num++;
						}
						object obj2 = null;
						try
						{
							obj2 = lambdaMethodInfo.Invoke(pProgramTarget, sprakArguments.ToArray<object>());
						}
						catch (TargetInvocationException ex)
						{
							throw ex.GetBaseException();
						}
						if (lambdaMethodInfo.ReturnType == typeof(int))
						{
							return (float)((int)obj2);
						}
						if (lambdaMethodInfo.ReturnType.IsSubclassOf(typeof(Array)))
						{
							SortedDictionary<KeyWrapper, object> sortedDictionary = new SortedDictionary<KeyWrapper, object>();
							int num2 = 0;
							IEnumerator enumerator = ((Array)obj2).GetEnumerator();
							try
							{
								while (enumerator.MoveNext())
								{
									object obj3 = enumerator.Current;
									sortedDictionary.Add(new KeyWrapper((float)num2++), obj3);
								}
							}
							finally
							{
								IDisposable disposable;
								if ((disposable = enumerator as IDisposable) != null)
								{
									disposable.Dispose();
								}
							}
							return sortedDictionary;
						}
						if (!FunctionDefinitionCreator.acceptableTypes.Contains(lambdaMethodInfo.ReturnType))
						{
							throw new Error("Function '" + shortname + "' can't return value of type " + lambdaMethodInfo.ReturnType.ToString());
						}
						if (lambdaMethodInfo.ReturnType == typeof(void))
						{
							return VoidType.voidType;
						}
						return obj2;
					};
					ReturnValueType returnValueType2 = ReturnValueConversions.SystemTypeToReturnValueType(lambdaMethodInfo.ReturnType);
					FunctionDocumentation functionDocumentation;
					if (functionDocumentations.ContainsKey(shortname))
					{
						functionDocumentation = functionDocumentations[shortname];
					}
					else
					{
						functionDocumentation = FunctionDocumentation.Default();
					}
					list.Add(new FunctionDefinition(returnValueType2.ToString(), shortname, list4.ToArray(), list3.ToArray(), onFunctionCall, functionDocumentation));
				}
			}
			return list;
		}

		// Token: 0x04000002 RID: 2
		public static Log logger;

		// Token: 0x04000003 RID: 3
		private static HashSet<Type> acceptableTypes = new HashSet<Type>
		{
			typeof(float),
			typeof(string),
			typeof(bool),
			typeof(Range),
			typeof(void),
			typeof(object[]),
			typeof(SortedDictionary<KeyWrapper, object>)
		};
	}
}
