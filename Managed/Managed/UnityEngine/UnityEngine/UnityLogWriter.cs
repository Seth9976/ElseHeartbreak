using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnityEngine
{
	// Token: 0x0200014F RID: 335
	internal sealed class UnityLogWriter : TextWriter
	{
		// Token: 0x06000E14 RID: 3604
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WriteStringToUnityLog(string s);

		// Token: 0x06000E15 RID: 3605 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
		public static void Init()
		{
			Console.SetOut(new UnityLogWriter());
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0001E3EC File Offset: 0x0001C5EC
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
		public override void Write(char value)
		{
			UnityLogWriter.WriteStringToUnityLog(value.ToString());
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0001E404 File Offset: 0x0001C604
		public override void Write(string s)
		{
			UnityLogWriter.WriteStringToUnityLog(s);
		}
	}
}
