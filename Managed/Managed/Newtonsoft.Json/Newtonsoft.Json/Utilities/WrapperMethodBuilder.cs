using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200009E RID: 158
	internal class WrapperMethodBuilder
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0001AEFB File Offset: 0x000190FB
		public WrapperMethodBuilder(Type realObjectType, TypeBuilder proxyBuilder)
		{
			this._realObjectType = realObjectType;
			this._wrapperBuilder = proxyBuilder;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001AF24 File Offset: 0x00019124
		public void Generate(MethodInfo newMethod)
		{
			if (newMethod.IsGenericMethod)
			{
				newMethod = newMethod.GetGenericMethodDefinition();
			}
			FieldInfo field = typeof(DynamicWrapperBase).GetField("UnderlyingObject", BindingFlags.Instance | BindingFlags.NonPublic);
			ParameterInfo[] parameters = newMethod.GetParameters();
			Type[] array = parameters.Select((ParameterInfo parameter) => parameter.ParameterType).ToArray<Type>();
			MethodBuilder methodBuilder = this._wrapperBuilder.DefineMethod(newMethod.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual, newMethod.ReturnType, array);
			if (newMethod.IsGenericMethod)
			{
				methodBuilder.DefineGenericParameters((from arg in newMethod.GetGenericArguments()
					select arg.Name).ToArray<string>());
			}
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			WrapperMethodBuilder.LoadUnderlyingObject(ilgenerator, field);
			WrapperMethodBuilder.PushParameters(parameters, ilgenerator);
			this.ExecuteMethod(newMethod, array, ilgenerator);
			WrapperMethodBuilder.Return(ilgenerator);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001B008 File Offset: 0x00019208
		private static void Return(ILGenerator ilGenerator)
		{
			ilGenerator.Emit(OpCodes.Ret);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001B018 File Offset: 0x00019218
		private void ExecuteMethod(MethodBase newMethod, Type[] parameterTypes, ILGenerator ilGenerator)
		{
			MethodInfo method = this.GetMethod(newMethod, parameterTypes);
			if (method == null)
			{
				throw new MissingMethodException("Unable to find method " + newMethod.Name + " on " + this._realObjectType.FullName);
			}
			ilGenerator.Emit(OpCodes.Call, method);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001B063 File Offset: 0x00019263
		private MethodInfo GetMethod(MethodBase realMethod, Type[] parameterTypes)
		{
			if (realMethod.IsGenericMethod)
			{
				return this._realObjectType.GetGenericMethod(realMethod.Name, parameterTypes);
			}
			return this._realObjectType.GetMethod(realMethod.Name, parameterTypes);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001B094 File Offset: 0x00019294
		private static void PushParameters(ICollection<ParameterInfo> parameters, ILGenerator ilGenerator)
		{
			for (int i = 1; i < parameters.Count + 1; i++)
			{
				ilGenerator.Emit(OpCodes.Ldarg, i);
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001B0C0 File Offset: 0x000192C0
		private static void LoadUnderlyingObject(ILGenerator ilGenerator, FieldInfo srcField)
		{
			ilGenerator.Emit(OpCodes.Ldarg_0);
			ilGenerator.Emit(OpCodes.Ldfld, srcField);
		}

		// Token: 0x0400025A RID: 602
		private readonly Type _realObjectType;

		// Token: 0x0400025B RID: 603
		private readonly TypeBuilder _wrapperBuilder;
	}
}
