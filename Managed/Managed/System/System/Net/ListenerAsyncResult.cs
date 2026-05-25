using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000331 RID: 817
	internal class ListenerAsyncResult : IAsyncResult
	{
		// Token: 0x06001CF8 RID: 7416 RVA: 0x00055D00 File Offset: 0x00053F00
		public ListenerAsyncResult(AsyncCallback cb, object state)
		{
			this.cb = cb;
			this.state = state;
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x00055D24 File Offset: 0x00053F24
		internal void Complete(string error)
		{
			if (this.forward != null)
			{
				this.forward.Complete(error);
				return;
			}
			this.exception = new HttpListenerException(0, error);
			object obj = this.locker;
			lock (obj)
			{
				this.completed = true;
				if (this.handle != null)
				{
					this.handle.Set();
				}
				if (this.cb != null)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(ListenerAsyncResult.InvokeCallback), this);
				}
			}
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x00055DC8 File Offset: 0x00053FC8
		private static void InvokeCallback(object o)
		{
			ListenerAsyncResult listenerAsyncResult = (ListenerAsyncResult)o;
			if (listenerAsyncResult.forward != null)
			{
				listenerAsyncResult.forward.cb(listenerAsyncResult);
				return;
			}
			listenerAsyncResult.cb(listenerAsyncResult);
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x00055E08 File Offset: 0x00054008
		internal void Complete(HttpListenerContext context)
		{
			this.Complete(context, false);
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x00055E14 File Offset: 0x00054014
		internal void Complete(HttpListenerContext context, bool synch)
		{
			if (this.forward != null)
			{
				this.forward.Complete(context, synch);
				return;
			}
			this.synch = synch;
			this.context = context;
			object obj = this.locker;
			lock (obj)
			{
				AuthenticationSchemes authenticationSchemes = context.Listener.SelectAuthenticationScheme(context);
				if ((authenticationSchemes == AuthenticationSchemes.Basic || context.Listener.AuthenticationSchemes == AuthenticationSchemes.Negotiate) && context.Request.Headers["Authorization"] == null)
				{
					context.Response.StatusCode = 401;
					context.Response.Headers["WWW-Authenticate"] = string.Concat(new object[]
					{
						authenticationSchemes,
						" realm=\"",
						context.Listener.Realm,
						"\""
					});
					context.Response.OutputStream.Close();
					IAsyncResult asyncResult = context.Listener.BeginGetContext(this.cb, this.state);
					this.forward = (ListenerAsyncResult)asyncResult;
					object obj2 = this.forward.locker;
					lock (obj2)
					{
						if (this.handle != null)
						{
							this.forward.handle = this.handle;
						}
					}
					ListenerAsyncResult listenerAsyncResult = this.forward;
					int num = 0;
					while (listenerAsyncResult.forward != null)
					{
						if (num > 20)
						{
							this.Complete("Too many authentication errors");
						}
						listenerAsyncResult = listenerAsyncResult.forward;
						num++;
					}
				}
				else
				{
					this.completed = true;
					if (this.handle != null)
					{
						this.handle.Set();
					}
					if (this.cb != null)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(ListenerAsyncResult.InvokeCallback), this);
					}
				}
			}
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0005601C File Offset: 0x0005421C
		internal HttpListenerContext GetContext()
		{
			if (this.forward != null)
			{
				return this.forward.GetContext();
			}
			if (this.exception != null)
			{
				throw this.exception;
			}
			return this.context;
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x00056050 File Offset: 0x00054250
		public object AsyncState
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.AsyncState;
				}
				return this.state;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x00056070 File Offset: 0x00054270
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.AsyncWaitHandle;
				}
				object obj = this.locker;
				lock (obj)
				{
					if (this.handle == null)
					{
						this.handle = new ManualResetEvent(this.completed);
					}
				}
				return this.handle;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x000560EC File Offset: 0x000542EC
		public bool CompletedSynchronously
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.CompletedSynchronously;
				}
				return this.synch;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0005610C File Offset: 0x0005430C
		public bool IsCompleted
		{
			get
			{
				if (this.forward != null)
				{
					return this.forward.IsCompleted;
				}
				object obj = this.locker;
				bool flag;
				lock (obj)
				{
					flag = this.completed;
				}
				return flag;
			}
		}

		// Token: 0x0400121C RID: 4636
		private ManualResetEvent handle;

		// Token: 0x0400121D RID: 4637
		private bool synch;

		// Token: 0x0400121E RID: 4638
		private bool completed;

		// Token: 0x0400121F RID: 4639
		private AsyncCallback cb;

		// Token: 0x04001220 RID: 4640
		private object state;

		// Token: 0x04001221 RID: 4641
		private Exception exception;

		// Token: 0x04001222 RID: 4642
		private HttpListenerContext context;

		// Token: 0x04001223 RID: 4643
		private object locker = new object();

		// Token: 0x04001224 RID: 4644
		private ListenerAsyncResult forward;
	}
}
