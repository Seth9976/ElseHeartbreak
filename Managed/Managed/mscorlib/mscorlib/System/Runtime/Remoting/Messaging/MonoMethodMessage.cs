using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004AF RID: 1199
	[Serializable]
	internal class MonoMethodMessage : IInternalMessage, IMessage, IMethodCallMessage, IMethodMessage, IMethodReturnMessage
	{
		// Token: 0x060030B0 RID: 12464 RVA: 0x000A03F8 File Offset: 0x0009E5F8
		public MonoMethodMessage(MethodBase method, object[] out_args)
		{
			if (method != null)
			{
				this.InitMessage((MonoMethod)method, out_args);
			}
			else
			{
				this.args = null;
			}
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000A0420 File Offset: 0x0009E620
		public MonoMethodMessage(Type type, string method_name, object[] in_args)
		{
			MethodInfo methodInfo = type.GetMethod(method_name);
			this.InitMessage((MonoMethod)methodInfo, null);
			int num = in_args.Length;
			for (int i = 0; i < num; i++)
			{
				this.args[i] = in_args[i];
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x000A046C File Offset: 0x0009E66C
		// (set) Token: 0x060030B3 RID: 12467 RVA: 0x000A0474 File Offset: 0x0009E674
		Identity IInternalMessage.TargetIdentity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
			}
		}

		// Token: 0x060030B4 RID: 12468
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InitMessage(MonoMethod method, object[] out_args);

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000A0480 File Offset: 0x0009E680
		public IDictionary Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new MethodCallDictionary(this);
				}
				return this.properties;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060030B6 RID: 12470 RVA: 0x000A04A0 File Offset: 0x0009E6A0
		public int ArgCount
		{
			get
			{
				if (this.CallType == CallType.EndInvoke)
				{
					return -1;
				}
				if (this.args == null)
				{
					return 0;
				}
				return this.args.Length;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x000A04C8 File Offset: 0x0009E6C8
		public object[] Args
		{
			get
			{
				return this.args;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x000A04D0 File Offset: 0x0009E6D0
		public bool HasVarArgs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x000A04D4 File Offset: 0x0009E6D4
		// (set) Token: 0x060030BA RID: 12474 RVA: 0x000A04DC File Offset: 0x0009E6DC
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				return this.ctx;
			}
			set
			{
				this.ctx = value;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x000A04E8 File Offset: 0x0009E6E8
		public MethodBase MethodBase
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x000A04F0 File Offset: 0x0009E6F0
		public string MethodName
		{
			get
			{
				if (this.method == null)
				{
					return string.Empty;
				}
				return this.method.Name;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x000A0510 File Offset: 0x0009E710
		public object MethodSignature
		{
			get
			{
				if (this.methodSignature == null)
				{
					ParameterInfo[] parameters = this.method.GetParameters();
					this.methodSignature = new Type[parameters.Length];
					for (int i = 0; i < parameters.Length; i++)
					{
						this.methodSignature[i] = parameters[i].ParameterType;
					}
				}
				return this.methodSignature;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x000A056C File Offset: 0x0009E76C
		public string TypeName
		{
			get
			{
				if (this.method == null)
				{
					return string.Empty;
				}
				return this.method.DeclaringType.AssemblyQualifiedName;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x000A0590 File Offset: 0x0009E790
		// (set) Token: 0x060030C0 RID: 12480 RVA: 0x000A0598 File Offset: 0x0009E798
		public string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x060030C1 RID: 12481 RVA: 0x000A05A4 File Offset: 0x0009E7A4
		public object GetArg(int arg_num)
		{
			if (this.args == null)
			{
				return null;
			}
			return this.args[arg_num];
		}

		// Token: 0x060030C2 RID: 12482 RVA: 0x000A05BC File Offset: 0x0009E7BC
		public string GetArgName(int arg_num)
		{
			if (this.args == null)
			{
				return string.Empty;
			}
			return this.names[arg_num];
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x000A05D8 File Offset: 0x0009E7D8
		public int InArgCount
		{
			get
			{
				if (this.CallType == CallType.EndInvoke)
				{
					return -1;
				}
				if (this.args == null)
				{
					return 0;
				}
				int num = 0;
				foreach (byte b in this.arg_types)
				{
					if ((b & 1) != 0)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x000A0630 File Offset: 0x0009E830
		public object[] InArgs
		{
			get
			{
				int inArgCount = this.InArgCount;
				object[] array = new object[inArgCount];
				int num2;
				int num = (num2 = 0);
				foreach (byte b in this.arg_types)
				{
					if ((b & 1) != 0)
					{
						array[num++] = this.args[num2];
					}
					num2++;
				}
				return array;
			}
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000A0694 File Offset: 0x0009E894
		public object GetInArg(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			foreach (byte b in this.arg_types)
			{
				if ((b & 1) != 0 && num2++ == arg_num)
				{
					return this.args[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000A06EC File Offset: 0x0009E8EC
		public string GetInArgName(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			foreach (byte b in this.arg_types)
			{
				if ((b & 1) != 0 && num2++ == arg_num)
				{
					return this.names[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060030C7 RID: 12487 RVA: 0x000A0744 File Offset: 0x0009E944
		public Exception Exception
		{
			get
			{
				return this.exc;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x000A074C File Offset: 0x0009E94C
		public int OutArgCount
		{
			get
			{
				if (this.args == null)
				{
					return 0;
				}
				int num = 0;
				foreach (byte b in this.arg_types)
				{
					if ((b & 2) != 0)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x000A0794 File Offset: 0x0009E994
		public object[] OutArgs
		{
			get
			{
				if (this.args == null)
				{
					return null;
				}
				int outArgCount = this.OutArgCount;
				object[] array = new object[outArgCount];
				int num2;
				int num = (num2 = 0);
				foreach (byte b in this.arg_types)
				{
					if ((b & 2) != 0)
					{
						array[num++] = this.args[num2];
					}
					num2++;
				}
				return array;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060030CA RID: 12490 RVA: 0x000A0808 File Offset: 0x0009EA08
		public object ReturnValue
		{
			get
			{
				return this.rval;
			}
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000A0810 File Offset: 0x0009EA10
		public object GetOutArg(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			foreach (byte b in this.arg_types)
			{
				if ((b & 2) != 0 && num2++ == arg_num)
				{
					return this.args[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000A0868 File Offset: 0x0009EA68
		public string GetOutArgName(int arg_num)
		{
			int num = 0;
			int num2 = 0;
			foreach (byte b in this.arg_types)
			{
				if ((b & 2) != 0 && num2++ == arg_num)
				{
					return this.names[num];
				}
				num++;
			}
			return null;
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060030CD RID: 12493 RVA: 0x000A08C0 File Offset: 0x0009EAC0
		public bool IsAsync
		{
			get
			{
				return this.asyncResult != null;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x000A08D0 File Offset: 0x0009EAD0
		public AsyncResult AsyncResult
		{
			get
			{
				return this.asyncResult;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060030CF RID: 12495 RVA: 0x000A08D8 File Offset: 0x0009EAD8
		internal CallType CallType
		{
			get
			{
				if (this.call_type == CallType.Sync && RemotingServices.IsOneWay(this.method))
				{
					this.call_type = CallType.OneWay;
				}
				return this.call_type;
			}
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000A0910 File Offset: 0x0009EB10
		public bool NeedsOutProcessing(out int outCount)
		{
			bool flag = false;
			outCount = 0;
			foreach (byte b in this.arg_types)
			{
				if ((b & 2) != 0)
				{
					outCount++;
				}
				else if ((b & 4) != 0)
				{
					flag = true;
				}
			}
			return outCount > 0 || flag;
		}

		// Token: 0x04001496 RID: 5270
		private MonoMethod method;

		// Token: 0x04001497 RID: 5271
		private object[] args;

		// Token: 0x04001498 RID: 5272
		private string[] names;

		// Token: 0x04001499 RID: 5273
		private byte[] arg_types;

		// Token: 0x0400149A RID: 5274
		public LogicalCallContext ctx;

		// Token: 0x0400149B RID: 5275
		public object rval;

		// Token: 0x0400149C RID: 5276
		public Exception exc;

		// Token: 0x0400149D RID: 5277
		private AsyncResult asyncResult;

		// Token: 0x0400149E RID: 5278
		private CallType call_type;

		// Token: 0x0400149F RID: 5279
		private string uri;

		// Token: 0x040014A0 RID: 5280
		private MethodCallDictionary properties;

		// Token: 0x040014A1 RID: 5281
		private Type[] methodSignature;

		// Token: 0x040014A2 RID: 5282
		private Identity identity;
	}
}
