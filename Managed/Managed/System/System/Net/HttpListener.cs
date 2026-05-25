using System;
using System.Collections;
using System.Threading;

namespace System.Net
{
	/// <summary>Provides a simple, programmatically controlled HTTP protocol listener. This class cannot be inherited.</summary>
	// Token: 0x02000316 RID: 790
	public sealed class HttpListener : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListener" /> class.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">This class cannot be used on the current operating system. Windows Server 2003 or Windows XP SP2 is required to use instances of this class.</exception>
		// Token: 0x06001B78 RID: 7032 RVA: 0x0004E6C0 File Offset: 0x0004C8C0
		public HttpListener()
		{
			this.prefixes = new HttpListenerPrefixCollection(this);
			this.registry = new Hashtable();
			this.ctx_queue = new ArrayList();
			this.wait_queue = new ArrayList();
			this.auth_schemes = AuthenticationSchemes.Anonymous;
		}

		/// <summary>Releases the resources held by this <see cref="T:System.Net.HttpListener" /> object.</summary>
		// Token: 0x06001B79 RID: 7033 RVA: 0x0004E70C File Offset: 0x0004C90C
		void IDisposable.Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.Close(true);
			this.disposed = true;
		}

		/// <summary>Gets or sets the scheme used to authenticate clients.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Net.AuthenticationSchemes" /> enumeration values that indicates how clients are to be authenticated. The default value is <see cref="F:System.Net.AuthenticationSchemes.Anonymous" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x0004E728 File Offset: 0x0004C928
		// (set) Token: 0x06001B7B RID: 7035 RVA: 0x0004E730 File Offset: 0x0004C930
		public AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return this.auth_schemes;
			}
			set
			{
				this.CheckDisposed();
				this.auth_schemes = value;
			}
		}

		/// <summary>Gets or sets the delegate called to determine the protocol used to authenticate clients.</summary>
		/// <returns>An <see cref="T:System.Net.AuthenticationSchemeSelector" /> delegate that invokes the method used to select an authentication protocol. The default value is null.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x0004E740 File Offset: 0x0004C940
		// (set) Token: 0x06001B7D RID: 7037 RVA: 0x0004E748 File Offset: 0x0004C948
		public AuthenticationSchemeSelector AuthenticationSchemeSelectorDelegate
		{
			get
			{
				return this.auth_selector;
			}
			set
			{
				this.CheckDisposed();
				this.auth_selector = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether your application receives exceptions that occur when an <see cref="T:System.Net.HttpListener" /> sends the response to the client.</summary>
		/// <returns>true if this <see cref="T:System.Net.HttpListener" /> should not return exceptions that occur when sending the response to the client; otherwise false. The default value is false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x0004E758 File Offset: 0x0004C958
		// (set) Token: 0x06001B7F RID: 7039 RVA: 0x0004E760 File Offset: 0x0004C960
		public bool IgnoreWriteExceptions
		{
			get
			{
				return this.ignore_write_exceptions;
			}
			set
			{
				this.CheckDisposed();
				this.ignore_write_exceptions = value;
			}
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Net.HttpListener" /> has been started.</summary>
		/// <returns>true if the <see cref="T:System.Net.HttpListener" /> was started; otherwise, false.</returns>
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x0004E770 File Offset: 0x0004C970
		public bool IsListening
		{
			get
			{
				return this.listening;
			}
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Net.HttpListener" /> can be used with the current operating system.</summary>
		/// <returns>true if <see cref="T:System.Net.HttpListener" /> is supported; otherwise, false.</returns>
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x0004E778 File Offset: 0x0004C978
		public static bool IsSupported
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) prefixes handled by this <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerPrefixCollection" /> that contains the URI prefixes that this <see cref="T:System.Net.HttpListener" /> object is configured to handle. </returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0004E77C File Offset: 0x0004C97C
		public HttpListenerPrefixCollection Prefixes
		{
			get
			{
				this.CheckDisposed();
				return this.prefixes;
			}
		}

		/// <summary>Gets or sets the realm, or resource partition, associated with this <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the name of the realm associated with the <see cref="T:System.Net.HttpListener" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x0004E78C File Offset: 0x0004C98C
		// (set) Token: 0x06001B84 RID: 7044 RVA: 0x0004E794 File Offset: 0x0004C994
		public string Realm
		{
			get
			{
				return this.realm;
			}
			set
			{
				this.CheckDisposed();
				this.realm = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether, when NTLM is used, additional requests using the same Transmission Control Protocol (TCP) connection are required to authenticate.</summary>
		/// <returns>true if the <see cref="T:System.Security.Principal.IIdentity" /> of the first request will be used for subsequent requests on the same connection; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x0004E7A4 File Offset: 0x0004C9A4
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x0004E7AC File Offset: 0x0004C9AC
		[global::System.MonoTODO("Support for NTLM needs some loving.")]
		public bool UnsafeConnectionNtlmAuthentication
		{
			get
			{
				return this.unsafe_ntlm_auth;
			}
			set
			{
				this.CheckDisposed();
				this.unsafe_ntlm_auth = value;
			}
		}

		/// <summary>Shuts down the <see cref="T:System.Net.HttpListener" /> object immediately, discarding all currently queued requests.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001B87 RID: 7047 RVA: 0x0004E7BC File Offset: 0x0004C9BC
		public void Abort()
		{
			if (this.disposed)
			{
				return;
			}
			if (!this.listening)
			{
				return;
			}
			this.Close(true);
		}

		/// <summary>Shuts down the <see cref="T:System.Net.HttpListener" /> after processing all currently queued requests.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001B88 RID: 7048 RVA: 0x0004E7E0 File Offset: 0x0004C9E0
		public void Close()
		{
			if (this.disposed)
			{
				return;
			}
			if (!this.listening)
			{
				this.disposed = true;
				return;
			}
			this.Close(false);
			this.disposed = true;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0004E810 File Offset: 0x0004CA10
		private void Close(bool force)
		{
			this.CheckDisposed();
			EndPointManager.RemoveListener(this);
			this.Cleanup(force);
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x0004E828 File Offset: 0x0004CA28
		private void Cleanup(bool close_existing)
		{
			Hashtable hashtable = this.registry;
			lock (hashtable)
			{
				if (close_existing)
				{
					foreach (object obj in this.registry.Keys)
					{
						HttpListenerContext httpListenerContext = (HttpListenerContext)obj;
						httpListenerContext.Connection.Close();
					}
					this.registry.Clear();
				}
				ArrayList arrayList = this.ctx_queue;
				lock (arrayList)
				{
					foreach (object obj2 in this.ctx_queue)
					{
						HttpListenerContext httpListenerContext2 = (HttpListenerContext)obj2;
						httpListenerContext2.Connection.Close();
					}
					this.ctx_queue.Clear();
				}
				ArrayList arrayList2 = this.wait_queue;
				lock (arrayList2)
				{
					foreach (object obj3 in this.wait_queue)
					{
						ListenerAsyncResult listenerAsyncResult = (ListenerAsyncResult)obj3;
						listenerAsyncResult.Complete("Listener was closed.");
					}
					this.wait_queue.Clear();
				}
			}
		}

		/// <summary>Begins asynchronously retrieving an incoming request.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that indicates the status of the asynchronous operation.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when a client request is available.</param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="callback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.InvalidOperationException">This object has not been started or is currently stopped.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x06001B8B RID: 7051 RVA: 0x0004EA38 File Offset: 0x0004CC38
		public IAsyncResult BeginGetContext(AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			if (!this.listening)
			{
				throw new InvalidOperationException("Please, call Start before using this method.");
			}
			ListenerAsyncResult listenerAsyncResult = new ListenerAsyncResult(callback, state);
			ArrayList arrayList = this.wait_queue;
			lock (arrayList)
			{
				ArrayList arrayList2 = this.ctx_queue;
				lock (arrayList2)
				{
					HttpListenerContext contextFromQueue = this.GetContextFromQueue();
					if (contextFromQueue != null)
					{
						listenerAsyncResult.Complete(contextFromQueue, true);
						return listenerAsyncResult;
					}
				}
				this.wait_queue.Add(listenerAsyncResult);
			}
			return listenerAsyncResult;
		}

		/// <summary>Completes an asynchronous operation to retrieve an incoming client request.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerContext" /> object that represents the client request.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that was obtained when the asynchronous operation was started.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling the <see cref="M:System.Net.HttpListener.BeginGetContext(System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.HttpListener.EndGetContext(System.IAsyncResult)" /> method was already called for the specified <paramref name="asyncResult" /> object.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x06001B8C RID: 7052 RVA: 0x0004EB00 File Offset: 0x0004CD00
		public HttpListenerContext EndGetContext(IAsyncResult asyncResult)
		{
			this.CheckDisposed();
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			ListenerAsyncResult listenerAsyncResult = asyncResult as ListenerAsyncResult;
			if (listenerAsyncResult == null)
			{
				throw new ArgumentException("Wrong IAsyncResult.", "asyncResult");
			}
			if (!listenerAsyncResult.IsCompleted)
			{
				listenerAsyncResult.AsyncWaitHandle.WaitOne();
			}
			ArrayList arrayList = this.wait_queue;
			lock (arrayList)
			{
				int num = this.wait_queue.IndexOf(listenerAsyncResult);
				if (num >= 0)
				{
					this.wait_queue.RemoveAt(num);
				}
			}
			HttpListenerContext context = listenerAsyncResult.GetContext();
			if (this.auth_schemes != AuthenticationSchemes.Anonymous)
			{
				context.ParseAuthentication(this.auth_schemes);
			}
			return context;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0004EBD4 File Offset: 0x0004CDD4
		internal AuthenticationSchemes SelectAuthenticationScheme(HttpListenerContext context)
		{
			if (this.AuthenticationSchemeSelectorDelegate != null)
			{
				return this.AuthenticationSchemeSelectorDelegate(context.Request);
			}
			return this.auth_schemes;
		}

		/// <summary>Waits for an incoming request and returns when one is received.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerContext" /> object that represents a client request.</returns>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.InvalidOperationException">This object has not been started or is currently stopped.-or-The <see cref="T:System.Net.HttpListener" /> does not have any Uniform Resource Identifier (URI) prefixes to respond to. See Remarks.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x06001B8E RID: 7054 RVA: 0x0004EC04 File Offset: 0x0004CE04
		public HttpListenerContext GetContext()
		{
			if (this.prefixes.Count == 0)
			{
				throw new InvalidOperationException("Please, call AddPrefix before using this method.");
			}
			IAsyncResult asyncResult = this.BeginGetContext(null, null);
			return this.EndGetContext(asyncResult);
		}

		/// <summary>Allows this instance to receive incoming requests.</summary>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001B8F RID: 7055 RVA: 0x0004EC3C File Offset: 0x0004CE3C
		public void Start()
		{
			this.CheckDisposed();
			if (this.listening)
			{
				return;
			}
			EndPointManager.AddListener(this);
			this.listening = true;
		}

		/// <summary>Causes this instance to stop receiving incoming requests.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001B90 RID: 7056 RVA: 0x0004EC60 File Offset: 0x0004CE60
		public void Stop()
		{
			this.CheckDisposed();
			this.listening = false;
			this.Close(false);
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0004EC78 File Offset: 0x0004CE78
		internal void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0004EC98 File Offset: 0x0004CE98
		private HttpListenerContext GetContextFromQueue()
		{
			if (this.ctx_queue.Count == 0)
			{
				return null;
			}
			HttpListenerContext httpListenerContext = (HttpListenerContext)this.ctx_queue[0];
			this.ctx_queue.RemoveAt(0);
			return httpListenerContext;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0004ECD8 File Offset: 0x0004CED8
		internal void RegisterContext(HttpListenerContext context)
		{
			try
			{
				Monitor.Enter(this.registry);
				this.registry[context] = context;
				Monitor.Enter(this.wait_queue);
				Monitor.Enter(this.ctx_queue);
				if (this.wait_queue.Count == 0)
				{
					this.ctx_queue.Add(context);
				}
				else
				{
					ListenerAsyncResult listenerAsyncResult = (ListenerAsyncResult)this.wait_queue[0];
					this.wait_queue.RemoveAt(0);
					listenerAsyncResult.Complete(context);
				}
			}
			finally
			{
				Monitor.Exit(this.ctx_queue);
				Monitor.Exit(this.wait_queue);
				Monitor.Exit(this.registry);
			}
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x0004EDA0 File Offset: 0x0004CFA0
		internal void UnregisterContext(HttpListenerContext context)
		{
			try
			{
				Monitor.Enter(this.registry);
				Monitor.Enter(this.ctx_queue);
				int num = this.ctx_queue.IndexOf(context);
				if (num >= 0)
				{
					this.ctx_queue.RemoveAt(num);
				}
				this.registry.Remove(context);
			}
			finally
			{
				Monitor.Exit(this.ctx_queue);
				Monitor.Exit(this.registry);
			}
		}

		// Token: 0x04001108 RID: 4360
		private AuthenticationSchemes auth_schemes;

		// Token: 0x04001109 RID: 4361
		private HttpListenerPrefixCollection prefixes;

		// Token: 0x0400110A RID: 4362
		private AuthenticationSchemeSelector auth_selector;

		// Token: 0x0400110B RID: 4363
		private string realm;

		// Token: 0x0400110C RID: 4364
		private bool ignore_write_exceptions;

		// Token: 0x0400110D RID: 4365
		private bool unsafe_ntlm_auth;

		// Token: 0x0400110E RID: 4366
		private bool listening;

		// Token: 0x0400110F RID: 4367
		private bool disposed;

		// Token: 0x04001110 RID: 4368
		private Hashtable registry;

		// Token: 0x04001111 RID: 4369
		private ArrayList ctx_queue;

		// Token: 0x04001112 RID: 4370
		private ArrayList wait_queue;
	}
}
