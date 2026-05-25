using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200046A RID: 1130
	[MonoTODO("Handle domain unloading?")]
	internal class CrossAppDomainSink : IMessageSink
	{
		// Token: 0x06002EC5 RID: 11973 RVA: 0x0009AE00 File Offset: 0x00099000
		internal CrossAppDomainSink(int domainID)
		{
			this._domainID = domainID;
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x0009AE38 File Offset: 0x00099038
		internal static CrossAppDomainSink GetSink(int domainID)
		{
			object syncRoot = CrossAppDomainSink.s_sinks.SyncRoot;
			CrossAppDomainSink crossAppDomainSink;
			lock (syncRoot)
			{
				if (CrossAppDomainSink.s_sinks.ContainsKey(domainID))
				{
					crossAppDomainSink = (CrossAppDomainSink)CrossAppDomainSink.s_sinks[domainID];
				}
				else
				{
					CrossAppDomainSink crossAppDomainSink2 = new CrossAppDomainSink(domainID);
					CrossAppDomainSink.s_sinks[domainID] = crossAppDomainSink2;
					crossAppDomainSink = crossAppDomainSink2;
				}
			}
			return crossAppDomainSink;
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002EC8 RID: 11976 RVA: 0x0009AED0 File Offset: 0x000990D0
		internal int TargetDomainId
		{
			get
			{
				return this._domainID;
			}
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x0009AED8 File Offset: 0x000990D8
		private static CrossAppDomainSink.ProcessMessageRes ProcessMessageInDomain(byte[] arrRequest, CADMethodCallMessage cadMsg)
		{
			CrossAppDomainSink.ProcessMessageRes processMessageRes = default(CrossAppDomainSink.ProcessMessageRes);
			try
			{
				AppDomain.CurrentDomain.ProcessMessageInDomain(arrRequest, cadMsg, out processMessageRes.arrResponse, out processMessageRes.cadMrm);
			}
			catch (Exception ex)
			{
				IMessage message = new MethodResponse(ex, new ErrorMessage());
				processMessageRes.arrResponse = CADSerializer.SerializeMessage(message).GetBuffer();
			}
			return processMessageRes;
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x0009AF50 File Offset: 0x00099150
		public virtual IMessage SyncProcessMessage(IMessage msgRequest)
		{
			IMessage message = null;
			try
			{
				byte[] array = null;
				byte[] array2 = null;
				CADMethodReturnMessage cadmethodReturnMessage = null;
				CADMethodCallMessage cadmethodCallMessage = CADMethodCallMessage.Create(msgRequest);
				if (cadmethodCallMessage == null)
				{
					MemoryStream memoryStream = CADSerializer.SerializeMessage(msgRequest);
					array2 = memoryStream.GetBuffer();
				}
				Context currentContext = Thread.CurrentContext;
				try
				{
					CrossAppDomainSink.ProcessMessageRes processMessageRes = (CrossAppDomainSink.ProcessMessageRes)AppDomain.InvokeInDomainByID(this._domainID, CrossAppDomainSink.processMessageMethod, null, new object[] { array2, cadmethodCallMessage });
					array = processMessageRes.arrResponse;
					cadmethodReturnMessage = processMessageRes.cadMrm;
				}
				finally
				{
					AppDomain.InternalSetContext(currentContext);
				}
				if (array != null)
				{
					MemoryStream memoryStream2 = new MemoryStream(array);
					message = CADSerializer.DeserializeMessage(memoryStream2, msgRequest as IMethodCallMessage);
				}
				else
				{
					message = new MethodResponse(msgRequest as IMethodCallMessage, cadmethodReturnMessage);
				}
			}
			catch (Exception ex)
			{
				try
				{
					message = new ReturnMessage(ex, msgRequest as IMethodCallMessage);
				}
				catch (Exception)
				{
				}
			}
			return message;
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x0009B070 File Offset: 0x00099270
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			AsyncRequest asyncRequest = new AsyncRequest(reqMsg, replySink);
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.SendAsyncMessage), asyncRequest);
			return null;
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x0009B09C File Offset: 0x0009929C
		public void SendAsyncMessage(object data)
		{
			AsyncRequest asyncRequest = (AsyncRequest)data;
			IMessage message = this.SyncProcessMessage(asyncRequest.MsgRequest);
			asyncRequest.ReplySink.SyncProcessMessage(message);
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x0009B0CC File Offset: 0x000992CC
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040013EB RID: 5099
		private static Hashtable s_sinks = new Hashtable();

		// Token: 0x040013EC RID: 5100
		private static MethodInfo processMessageMethod = typeof(CrossAppDomainSink).GetMethod("ProcessMessageInDomain", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040013ED RID: 5101
		private int _domainID;

		// Token: 0x0200046B RID: 1131
		private struct ProcessMessageRes
		{
			// Token: 0x040013EE RID: 5102
			public byte[] arrResponse;

			// Token: 0x040013EF RID: 5103
			public CADMethodReturnMessage cadMrm;
		}
	}
}
