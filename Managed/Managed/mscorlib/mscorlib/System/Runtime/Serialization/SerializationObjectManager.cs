using System;
using System.Collections;

namespace System.Runtime.Serialization
{
	/// <summary>Manages serialization processes at run time. This class cannot be inherited.</summary>
	// Token: 0x0200050C RID: 1292
	public sealed class SerializationObjectManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.SerializationObjectManager" /> class. </summary>
		/// <param name="context">An instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class that contains information about the current serialization operation.</param>
		// Token: 0x0600337E RID: 13182 RVA: 0x000A69F4 File Offset: 0x000A4BF4
		public SerializationObjectManager(StreamingContext context)
		{
			this.context = context;
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600337F RID: 13183 RVA: 0x000A6A10 File Offset: 0x000A4C10
		// (remove) Token: 0x06003380 RID: 13184 RVA: 0x000A6A2C File Offset: 0x000A4C2C
		private event SerializationCallbacks.CallbackHandler callbacks;

		/// <summary>Registers the object upon which events will be raised.</summary>
		/// <param name="obj">The object to register.</param>
		// Token: 0x06003381 RID: 13185 RVA: 0x000A6A48 File Offset: 0x000A4C48
		public void RegisterObject(object obj)
		{
			if (this.seen.Contains(obj))
			{
				return;
			}
			SerializationCallbacks sc = SerializationCallbacks.GetSerializationCallbacks(obj.GetType());
			this.seen[obj] = 1;
			sc.RaiseOnSerializing(obj, this.context);
			if (sc.HasSerializedCallbacks)
			{
				this.callbacks = (SerializationCallbacks.CallbackHandler)Delegate.Combine(this.callbacks, new SerializationCallbacks.CallbackHandler(delegate(StreamingContext ctx)
				{
					sc.RaiseOnSerialized(obj, ctx);
				}));
			}
		}

		/// <summary>Invokes the OnSerializing callback event if the type of the object has one; and registers the object for raising the OnSerialized event if the type of the object has one.</summary>
		// Token: 0x06003382 RID: 13186 RVA: 0x000A6AF0 File Offset: 0x000A4CF0
		public void RaiseOnSerializedEvent()
		{
			if (this.callbacks != null)
			{
				this.callbacks(this.context);
			}
		}

		// Token: 0x04001564 RID: 5476
		private readonly StreamingContext context;

		// Token: 0x04001565 RID: 5477
		private readonly Hashtable seen = new Hashtable();
	}
}
