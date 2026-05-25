using System;
using System.Reflection;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004B9 RID: 1209
	internal class StackBuilderSink : IMessageSink
	{
		// Token: 0x06003107 RID: 12551 RVA: 0x000A0FF4 File Offset: 0x0009F1F4
		public StackBuilderSink(MarshalByRefObject obj, bool forceInternalExecute)
		{
			this._target = obj;
			if (!forceInternalExecute && RemotingServices.IsTransparentProxy(obj))
			{
				this._rp = RemotingServices.GetRealProxy(obj);
			}
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000A102C File Offset: 0x0009F22C
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.CheckParameters(msg);
			if (this._rp != null)
			{
				return this._rp.Invoke(msg);
			}
			return RemotingServices.InternalExecuteMessage(this._target, (IMethodCallMessage)msg);
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000A106C File Offset: 0x0009F26C
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			object[] array = new object[] { msg, replySink };
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.ExecuteAsyncMessage), array);
			return null;
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000A109C File Offset: 0x0009F29C
		private void ExecuteAsyncMessage(object ob)
		{
			object[] array = (object[])ob;
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)array[0];
			IMessageSink messageSink = (IMessageSink)array[1];
			this.CheckParameters(methodCallMessage);
			IMessage message;
			if (this._rp != null)
			{
				message = this._rp.Invoke(methodCallMessage);
			}
			else
			{
				message = RemotingServices.InternalExecuteMessage(this._target, methodCallMessage);
			}
			messageSink.SyncProcessMessage(message);
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x000A10FC File Offset: 0x0009F2FC
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000A1100 File Offset: 0x0009F300
		private void CheckParameters(IMessage msg)
		{
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)msg;
			ParameterInfo[] parameters = methodCallMessage.MethodBase.GetParameters();
			int num = 0;
			foreach (ParameterInfo parameterInfo in parameters)
			{
				object arg = methodCallMessage.GetArg(num++);
				Type type = parameterInfo.ParameterType;
				if (type.IsByRef)
				{
					type = type.GetElementType();
				}
				if (arg != null && !type.IsInstanceOfType(arg))
				{
					throw new RemotingException(string.Concat(new object[]
					{
						"Cannot cast argument ",
						parameterInfo.Position,
						" of type '",
						arg.GetType().AssemblyQualifiedName,
						"' to type '",
						type.AssemblyQualifiedName,
						"'"
					}));
				}
			}
		}

		// Token: 0x040014BF RID: 5311
		private MarshalByRefObject _target;

		// Token: 0x040014C0 RID: 5312
		private RealProxy _rp;
	}
}
