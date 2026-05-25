using System;
using System.Reflection;
using System.Security;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000042 RID: 66
	public sealed class Security
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00003FF0 File Offset: 0x000021F0
		private static MethodInfo GetUnityCrossDomainHelperMethod(string methodname)
		{
			Type type = Types.GetType("UnityEngine.UnityCrossDomainHelper", "CrossDomainPolicyParser, Version=1.0.0.0, Culture=neutral");
			if (type == null)
			{
				throw new SecurityException("Cant find type UnityCrossDomainHelper");
			}
			MethodInfo method = type.GetMethod(methodname);
			if (method == null)
			{
				throw new SecurityException("Cant find " + methodname);
			}
			return method;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004040 File Offset: 0x00002240
		internal static string TokenToHex(byte[] token)
		{
			if (token == null || 8 > token.Length)
			{
				return string.Empty;
			}
			return string.Format("{0:x2}{1:x2}{2:x2}{3:x2}{4:x2}{5:x2}{6:x2}{7:x2}", new object[]
			{
				token[0],
				token[1],
				token[2],
				token[3],
				token[4],
				token[5],
				token[6],
				token[7]
			});
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000040CC File Offset: 0x000022CC
		[SecuritySafeCritical]
		public static Assembly LoadAndVerifyAssembly(byte[] assemblyData, string authorizationKey)
		{
			return null;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000040D0 File Offset: 0x000022D0
		[SecuritySafeCritical]
		public static Assembly LoadAndVerifyAssembly(byte[] assemblyData)
		{
			return null;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000040D4 File Offset: 0x000022D4
		[SecuritySafeCritical]
		private static Assembly LoadAndVerifyAssemblyInternal(byte[] assemblyData)
		{
			return null;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000040D8 File Offset: 0x000022D8
		[ExcludeFromDocs]
		public static bool PrefetchSocketPolicy(string ip, int atPort)
		{
			int num = 3000;
			return Security.PrefetchSocketPolicy(ip, atPort, num);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000040F4 File Offset: 0x000022F4
		public static bool PrefetchSocketPolicy(string ip, int atPort, [DefaultValue("3000")] int timeout)
		{
			return true;
		}
	}
}
