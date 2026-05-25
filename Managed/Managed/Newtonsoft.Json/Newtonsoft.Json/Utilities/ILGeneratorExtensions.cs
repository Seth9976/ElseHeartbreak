using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000A4 RID: 164
	internal static class ILGeneratorExtensions
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x0001BBE1 File Offset: 0x00019DE1
		public static void PushInstance(this ILGenerator generator, Type type)
		{
			generator.Emit(OpCodes.Ldarg_0);
			if (type.IsValueType)
			{
				generator.Emit(OpCodes.Unbox, type);
				return;
			}
			generator.Emit(OpCodes.Castclass, type);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001BC0F File Offset: 0x00019E0F
		public static void BoxIfNeeded(this ILGenerator generator, Type type)
		{
			if (type.IsValueType)
			{
				generator.Emit(OpCodes.Box, type);
				return;
			}
			generator.Emit(OpCodes.Castclass, type);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001BC32 File Offset: 0x00019E32
		public static void UnboxIfNeeded(this ILGenerator generator, Type type)
		{
			if (type.IsValueType)
			{
				generator.Emit(OpCodes.Unbox_Any, type);
				return;
			}
			generator.Emit(OpCodes.Castclass, type);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001BC55 File Offset: 0x00019E55
		public static void CallMethod(this ILGenerator generator, MethodInfo methodInfo)
		{
			if (methodInfo.IsFinal || !methodInfo.IsVirtual)
			{
				generator.Emit(OpCodes.Call, methodInfo);
				return;
			}
			generator.Emit(OpCodes.Callvirt, methodInfo);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001BC80 File Offset: 0x00019E80
		public static void Return(this ILGenerator generator)
		{
			generator.Emit(OpCodes.Ret);
		}
	}
}
