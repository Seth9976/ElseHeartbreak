using System;
using System.Reflection;

namespace Boo.Lang.Runtime
{
	// Token: 0x02000028 RID: 40
	public class CandidateMethod
	{
		// Token: 0x060000EB RID: 235 RVA: 0x000041AC File Offset: 0x000023AC
		public CandidateMethod(MethodInfo method, int argumentCount, bool varArgs)
		{
			this._method = method;
			this._argumentScores = new int[argumentCount];
			this._varArgs = varArgs;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000041DC File Offset: 0x000023DC
		public static int CalculateArgumentScore(Type paramType, Type argType)
		{
			if (argType == null)
			{
				return paramType.IsValueType ? (-1) : 7;
			}
			if (paramType == argType)
			{
				return 7;
			}
			if (paramType.IsAssignableFrom(argType))
			{
				return 6;
			}
			if (argType.IsAssignableFrom(paramType))
			{
				return 2;
			}
			if (CandidateMethod.IsNumericPromotion(paramType, argType))
			{
				return (!NumericTypes.IsWideningPromotion(paramType, argType)) ? 3 : 5;
			}
			MethodInfo methodInfo = RuntimeServices.FindImplicitConversionOperator(argType, paramType);
			if (methodInfo != null)
			{
				return 4;
			}
			return -1;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004258 File Offset: 0x00002458
		public MethodInfo Method
		{
			get
			{
				return this._method;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004260 File Offset: 0x00002460
		public int[] ArgumentScores
		{
			get
			{
				return this._argumentScores;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004268 File Offset: 0x00002468
		public bool VarArgs
		{
			get
			{
				return this._varArgs;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004270 File Offset: 0x00002470
		public int MinimumArgumentCount
		{
			get
			{
				return (!this._varArgs) ? this.Parameters.Length : (this.Parameters.Length - 1);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000042A0 File Offset: 0x000024A0
		public ParameterInfo[] Parameters
		{
			get
			{
				return this._method.GetParameters();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000042B0 File Offset: 0x000024B0
		public Type VarArgsParameterType
		{
			get
			{
				return this.GetParameterType(this.Parameters.Length - 1).GetElementType();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000042C8 File Offset: 0x000024C8
		public bool DoesNotRequireConversions
		{
			get
			{
				return !this.RequiresConversions;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000042D4 File Offset: 0x000024D4
		private bool RequiresConversions
		{
			get
			{
				return Array.Exists<int>(this._argumentScores, new Predicate<int>(CandidateMethod.RequiresConversion));
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000042F0 File Offset: 0x000024F0
		private static bool RequiresConversion(int score)
		{
			return score < 5;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000042F8 File Offset: 0x000024F8
		public Type GetParameterType(int i)
		{
			return this.Parameters[i].ParameterType;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004308 File Offset: 0x00002508
		public static bool IsNumericPromotion(Type paramType, Type argType)
		{
			return RuntimeServices.IsPromotableNumeric(Type.GetTypeCode(paramType)) && RuntimeServices.IsPromotableNumeric(Type.GetTypeCode(argType));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004328 File Offset: 0x00002528
		public object DynamicInvoke(object target, object[] args)
		{
			return this._method.Invoke(target, this.AdjustArgumentsForInvocation(args));
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004340 File Offset: 0x00002540
		private object[] AdjustArgumentsForInvocation(object[] arguments)
		{
			if (this.VarArgs)
			{
				Type varArgsParameterType = this.VarArgsParameterType;
				int minimumArgumentCount = this.MinimumArgumentCount;
				object[] array = new object[minimumArgumentCount + 1];
				for (int i = 0; i < minimumArgumentCount; i++)
				{
					array[i] = ((!CandidateMethod.RequiresConversion(this.ArgumentScores[i])) ? arguments[i] : RuntimeServices.Coerce(arguments[i], this.GetParameterType(i)));
				}
				array[minimumArgumentCount] = CandidateMethod.CreateVarArgsArray(arguments, minimumArgumentCount, varArgsParameterType);
				return array;
			}
			if (this.RequiresConversions)
			{
				for (int j = 0; j < arguments.Length; j++)
				{
					arguments[j] = ((!CandidateMethod.RequiresConversion(this.ArgumentScores[j])) ? arguments[j] : RuntimeServices.Coerce(arguments[j], this.GetParameterType(j)));
				}
			}
			return arguments;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004410 File Offset: 0x00002610
		private static Array CreateVarArgsArray(object[] arguments, int minimumArgumentCount, Type varArgsParameterType)
		{
			int num = arguments.Length - minimumArgumentCount;
			Array array = Array.CreateInstance(varArgsParameterType, num);
			for (int i = 0; i < array.Length; i++)
			{
				array.SetValue(RuntimeServices.Coerce(arguments[minimumArgumentCount + i], varArgsParameterType), i);
			}
			return array;
		}

		// Token: 0x04000125 RID: 293
		public const int ExactMatchScore = 7;

		// Token: 0x04000126 RID: 294
		public const int UpCastScore = 6;

		// Token: 0x04000127 RID: 295
		public const int WideningPromotion = 5;

		// Token: 0x04000128 RID: 296
		public const int ImplicitConversionScore = 4;

		// Token: 0x04000129 RID: 297
		public const int NarrowingPromotion = 3;

		// Token: 0x0400012A RID: 298
		public const int DowncastScore = 2;

		// Token: 0x0400012B RID: 299
		private readonly MethodInfo _method;

		// Token: 0x0400012C RID: 300
		private readonly int[] _argumentScores;

		// Token: 0x0400012D RID: 301
		private readonly bool _varArgs;
	}
}
