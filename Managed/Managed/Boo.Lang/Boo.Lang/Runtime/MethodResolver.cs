using System;
using System.Collections.Generic;
using System.Reflection;

namespace Boo.Lang.Runtime
{
	// Token: 0x0200003B RID: 59
	public class MethodResolver
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x0000604C File Offset: 0x0000424C
		public MethodResolver(params Type[] argumentTypes)
		{
			this._arguments = argumentTypes;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000605C File Offset: 0x0000425C
		public static Type[] GetArgumentTypes(object[] arguments)
		{
			if (arguments.Length == 0)
			{
				return Type.EmptyTypes;
			}
			Type[] array = new Type[arguments.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = MethodResolver.GetObjectTypeOrNull(arguments[i]);
			}
			return array;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000060A0 File Offset: 0x000042A0
		private static Type GetObjectTypeOrNull(object arg)
		{
			if (arg == null)
			{
				return null;
			}
			return arg.GetType();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000060B0 File Offset: 0x000042B0
		public CandidateMethod ResolveMethod(IEnumerable<MethodInfo> candidates)
		{
			List<CandidateMethod> list = this.FindApplicableMethods(candidates);
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			List<CandidateMethod> list2 = list.FindAll(new Predicate<CandidateMethod>(MethodResolver.DoesNotRequireConversions));
			if (list2.Count > 0)
			{
				return this.BestMethod(list2);
			}
			return this.BestMethod(list);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006114 File Offset: 0x00004314
		private static bool DoesNotRequireConversions(CandidateMethod candidate)
		{
			return candidate.DoesNotRequireConversions;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000611C File Offset: 0x0000431C
		private CandidateMethod BestMethod(List<CandidateMethod> applicable)
		{
			applicable.Sort(new Comparison<CandidateMethod>(this.BetterCandidate));
			return applicable[applicable.Count - 1];
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000614C File Offset: 0x0000434C
		private static int TotalScore(CandidateMethod c1)
		{
			int num = 0;
			foreach (int num2 in c1.ArgumentScores)
			{
				num += num2;
			}
			return num;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006180 File Offset: 0x00004380
		private int BetterCandidate(CandidateMethod c1, CandidateMethod c2)
		{
			int num = Math.Sign(MethodResolver.TotalScore(c1) - MethodResolver.TotalScore(c2));
			if (num != 0)
			{
				return num;
			}
			if (c1.VarArgs && !c2.VarArgs)
			{
				return -1;
			}
			if (c2.VarArgs && !c1.VarArgs)
			{
				return 1;
			}
			int num2 = Math.Min(c1.MinimumArgumentCount, c2.MinimumArgumentCount);
			for (int i = 0; i < num2; i++)
			{
				num += MethodResolver.MoreSpecificType(c1.GetParameterType(i), c2.GetParameterType(i));
			}
			if (num != 0)
			{
				return num;
			}
			if (c1.VarArgs && c2.VarArgs)
			{
				return MethodResolver.MoreSpecificType(c1.VarArgsParameterType, c2.VarArgsParameterType);
			}
			return 0;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006244 File Offset: 0x00004444
		private static int MoreSpecificType(Type t1, Type t2)
		{
			int num = MethodResolver.GetTypeGenerity(t2) - MethodResolver.GetTypeGenerity(t1);
			if (num != 0)
			{
				return num;
			}
			return MethodResolver.GetLogicalTypeDepth(t1) - MethodResolver.GetLogicalTypeDepth(t2);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006274 File Offset: 0x00004474
		private static int GetTypeGenerity(Type type)
		{
			if (!type.ContainsGenericParameters)
			{
				return 0;
			}
			return type.GetGenericArguments().Length;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000628C File Offset: 0x0000448C
		private static int GetLogicalTypeDepth(Type type)
		{
			int typeDepth = MethodResolver.GetTypeDepth(type);
			return (!type.IsValueType) ? typeDepth : (typeDepth - 1);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000062B4 File Offset: 0x000044B4
		private static int GetTypeDepth(Type type)
		{
			if (type.IsByRef)
			{
				return MethodResolver.GetTypeDepth(type.GetElementType());
			}
			if (type.IsInterface)
			{
				return MethodResolver.GetInterfaceDepth(type);
			}
			return MethodResolver.GetClassDepth(type);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000062F0 File Offset: 0x000044F0
		private static int GetClassDepth(Type type)
		{
			int num = 0;
			Type typeFromHandle = typeof(object);
			while (type != null && type != typeFromHandle)
			{
				type = type.BaseType;
				num++;
			}
			return num;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000632C File Offset: 0x0000452C
		private static int GetInterfaceDepth(Type type)
		{
			Type[] interfaces = type.GetInterfaces();
			if (interfaces.Length > 0)
			{
				int num = 0;
				foreach (Type type2 in interfaces)
				{
					int interfaceDepth = MethodResolver.GetInterfaceDepth(type2);
					if (interfaceDepth > num)
					{
						num = interfaceDepth;
					}
				}
				return 1 + num;
			}
			return 1;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006384 File Offset: 0x00004584
		private List<CandidateMethod> FindApplicableMethods(IEnumerable<MethodInfo> candidates)
		{
			List<CandidateMethod> list = new List<CandidateMethod>();
			foreach (MethodInfo methodInfo in candidates)
			{
				CandidateMethod candidateMethod = this.IsApplicableMethod(methodInfo);
				if (candidateMethod != null)
				{
					list.Add(candidateMethod);
				}
			}
			return list;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006400 File Offset: 0x00004600
		private CandidateMethod IsApplicableMethod(MethodInfo method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			bool flag = this.IsVarArgs(parameters);
			if (!this.ValidArgumentCount(parameters, flag))
			{
				return null;
			}
			CandidateMethod candidateMethod = new CandidateMethod(method, this._arguments.Length, flag);
			if (this.CalculateCandidateScore(candidateMethod))
			{
				return candidateMethod;
			}
			return null;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000644C File Offset: 0x0000464C
		private bool ValidArgumentCount(ParameterInfo[] parameters, bool varargs)
		{
			if (varargs)
			{
				int num = parameters.Length - 1;
				return this._arguments.Length >= num;
			}
			return this._arguments.Length == parameters.Length;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006484 File Offset: 0x00004684
		private bool IsVarArgs(ParameterInfo[] parameters)
		{
			return parameters.Length != 0 && this.HasParamArrayAttribute(parameters[parameters.Length - 1]);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000064A0 File Offset: 0x000046A0
		private bool HasParamArrayAttribute(ParameterInfo info)
		{
			return info.IsDefined(typeof(ParamArrayAttribute), true);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000064B4 File Offset: 0x000046B4
		private bool CalculateCandidateScore(CandidateMethod candidateMethod)
		{
			ParameterInfo[] parameters = candidateMethod.Parameters;
			for (int i = 0; i < candidateMethod.MinimumArgumentCount; i++)
			{
				if (parameters[i].IsOut)
				{
					return false;
				}
				if (!this.CalculateCandidateArgumentScore(candidateMethod, i, parameters[i].ParameterType))
				{
					return false;
				}
			}
			if (candidateMethod.VarArgs)
			{
				Type varArgsParameterType = candidateMethod.VarArgsParameterType;
				for (int j = candidateMethod.MinimumArgumentCount; j < this._arguments.Length; j++)
				{
					if (!this.CalculateCandidateArgumentScore(candidateMethod, j, varArgsParameterType))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006548 File Offset: 0x00004748
		private bool CalculateCandidateArgumentScore(CandidateMethod candidateMethod, int argumentIndex, Type paramType)
		{
			int num = CandidateMethod.CalculateArgumentScore(paramType, this._arguments[argumentIndex]);
			if (num < 0)
			{
				return false;
			}
			candidateMethod.ArgumentScores[argumentIndex] = num;
			return true;
		}

		// Token: 0x04000145 RID: 325
		private readonly Type[] _arguments;
	}
}
