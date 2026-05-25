using System;
using System.Collections;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000498 RID: 1176
	[Serializable]
	internal class ErrorMessage : IMessage, IMethodCallMessage, IMethodMessage
	{
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x0009DED0 File Offset: 0x0009C0D0
		public int ArgCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x0009DED4 File Offset: 0x0009C0D4
		public object[] Args
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x0009DED8 File Offset: 0x0009C0D8
		public bool HasVarArgs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x0009DEDC File Offset: 0x0009C0DC
		public MethodBase MethodBase
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x0009DEE0 File Offset: 0x0009C0E0
		public string MethodName
		{
			get
			{
				return "unknown";
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x0009DEE8 File Offset: 0x0009C0E8
		public object MethodSignature
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x0009DEEC File Offset: 0x0009C0EC
		public virtual IDictionary Properties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x0009DEF0 File Offset: 0x0009C0F0
		public string TypeName
		{
			get
			{
				return "unknown";
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002FD8 RID: 12248 RVA: 0x0009DEF8 File Offset: 0x0009C0F8
		// (set) Token: 0x06002FD9 RID: 12249 RVA: 0x0009DF00 File Offset: 0x0009C100
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x0009DF0C File Offset: 0x0009C10C
		public object GetArg(int arg_num)
		{
			return null;
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x0009DF10 File Offset: 0x0009C110
		public string GetArgName(int arg_num)
		{
			return "unknown";
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x0009DF18 File Offset: 0x0009C118
		public int InArgCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x0009DF1C File Offset: 0x0009C11C
		public string GetInArgName(int index)
		{
			return null;
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x0009DF20 File Offset: 0x0009C120
		public object GetInArg(int argNum)
		{
			return null;
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x0009DF24 File Offset: 0x0009C124
		public object[] InArgs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x0009DF28 File Offset: 0x0009C128
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400145C RID: 5212
		private string _uri = "Exception";
	}
}
