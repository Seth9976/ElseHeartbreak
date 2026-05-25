using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004BE RID: 1214
	internal class CADMethodReturnMessage : CADMessageBase
	{
		// Token: 0x06003123 RID: 12579 RVA: 0x000A1C2C File Offset: 0x0009FE2C
		internal CADMethodReturnMessage(IMethodReturnMessage retMsg)
		{
			ArrayList arrayList = null;
			this._propertyCount = CADMessageBase.MarshalProperties(retMsg.Properties, ref arrayList);
			this._returnValue = base.MarshalArgument(retMsg.ReturnValue, ref arrayList);
			this._args = base.MarshalArguments(retMsg.Args, ref arrayList);
			if (retMsg.Exception != null)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._exception = new CADArgHolder(arrayList.Count);
				arrayList.Add(retMsg.Exception);
			}
			base.SaveLogicalCallContext(retMsg, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CADSerializer.SerializeObject(arrayList.ToArray());
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000A1CD8 File Offset: 0x0009FED8
		internal static CADMethodReturnMessage Create(IMessage callMsg)
		{
			IMethodReturnMessage methodReturnMessage = callMsg as IMethodReturnMessage;
			if (methodReturnMessage == null)
			{
				return null;
			}
			return new CADMethodReturnMessage(methodReturnMessage);
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000A1CFC File Offset: 0x0009FEFC
		internal ArrayList GetArguments()
		{
			ArrayList arrayList = null;
			if (this._serializedArgs != null)
			{
				object[] array = (object[])CADSerializer.DeserializeObject(new MemoryStream(this._serializedArgs));
				arrayList = new ArrayList(array);
				this._serializedArgs = null;
			}
			return arrayList;
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000A1D3C File Offset: 0x0009FF3C
		internal object[] GetArgs(ArrayList args)
		{
			return base.UnmarshalArguments(this._args, args);
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000A1D4C File Offset: 0x0009FF4C
		internal object GetReturnValue(ArrayList args)
		{
			return base.UnmarshalArgument(this._returnValue, args);
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000A1D5C File Offset: 0x0009FF5C
		internal Exception GetException(ArrayList args)
		{
			if (this._exception == null)
			{
				return null;
			}
			return (Exception)args[this._exception.index];
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000A1D84 File Offset: 0x0009FF84
		internal int PropertiesCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x040014CB RID: 5323
		private object _returnValue;

		// Token: 0x040014CC RID: 5324
		private CADArgHolder _exception;
	}
}
