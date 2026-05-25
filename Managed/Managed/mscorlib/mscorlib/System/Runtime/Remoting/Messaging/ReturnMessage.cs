using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Holds a message returned in response to a method call on a remote object.</summary>
	// Token: 0x020004B5 RID: 1205
	[ComVisible(true)]
	public class ReturnMessage : IInternalMessage, IMessage, IMethodMessage, IMethodReturnMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> class with all the information returning to the caller after the method call.</summary>
		/// <param name="ret">The object returned by the invoked method from which the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> instance originated. </param>
		/// <param name="outArgs">The objects returned from the invoked method as out parameters. </param>
		/// <param name="outArgsCount">The number of out parameters returned from the invoked method. </param>
		/// <param name="callCtx">The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> of the method call. </param>
		/// <param name="mcm">The original method call to the invoked method. </param>
		// Token: 0x060030E2 RID: 12514 RVA: 0x000A0AFC File Offset: 0x0009ECFC
		public ReturnMessage(object ret, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IMethodCallMessage mcm)
		{
			this._returnValue = ret;
			this._args = outArgs;
			this._outArgsCount = outArgsCount;
			this._callCtx = callCtx;
			if (mcm != null)
			{
				this._uri = mcm.Uri;
				this._methodBase = mcm.MethodBase;
			}
			if (this._args == null)
			{
				this._args = new object[outArgsCount];
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> class.</summary>
		/// <param name="e">The exception that was thrown during execution of the remotely called method. </param>
		/// <param name="mcm">An <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> with which to create an instance of the <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> class. </param>
		// Token: 0x060030E3 RID: 12515 RVA: 0x000A0B64 File Offset: 0x0009ED64
		public ReturnMessage(Exception e, IMethodCallMessage mcm)
		{
			this._exception = e;
			if (mcm != null)
			{
				this._methodBase = mcm.MethodBase;
				this._callCtx = mcm.LogicalCallContext;
			}
			this._args = new object[0];
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x000A0BA8 File Offset: 0x0009EDA8
		// (set) Token: 0x060030E5 RID: 12517 RVA: 0x000A0BB0 File Offset: 0x0009EDB0
		string IInternalMessage.Uri
		{
			get
			{
				return this.Uri;
			}
			set
			{
				this.Uri = value;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x000A0BBC File Offset: 0x0009EDBC
		// (set) Token: 0x060030E7 RID: 12519 RVA: 0x000A0BC4 File Offset: 0x0009EDC4
		Identity IInternalMessage.TargetIdentity
		{
			get
			{
				return this._targetIdentity;
			}
			set
			{
				this._targetIdentity = value;
			}
		}

		/// <summary>Gets the number of arguments of the called method.</summary>
		/// <returns>The number of arguments of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x000A0BD0 File Offset: 0x0009EDD0
		public int ArgCount
		{
			get
			{
				return this._args.Length;
			}
		}

		/// <summary>Gets a specified argument passed to the method called on the remote object.</summary>
		/// <returns>An argument passed to the method called on the remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060030E9 RID: 12521 RVA: 0x000A0BDC File Offset: 0x0009EDDC
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		/// <summary>Gets a value indicating whether the called method accepts a variable number of arguments.</summary>
		/// <returns>true if the called method accepts a variable number of arguments; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060030EA RID: 12522 RVA: 0x000A0BE4 File Offset: 0x0009EDE4
		public bool HasVarArgs
		{
			get
			{
				return this._methodBase != null && (this._methodBase.CallingConvention | CallingConventions.VarArgs) != (CallingConventions)0;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060030EB RID: 12523 RVA: 0x000A0C14 File Offset: 0x0009EE14
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				if (this._callCtx == null)
				{
					this._callCtx = new LogicalCallContext();
				}
				return this._callCtx;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060030EC RID: 12524 RVA: 0x000A0C34 File Offset: 0x0009EE34
		public MethodBase MethodBase
		{
			get
			{
				return this._methodBase;
			}
		}

		/// <summary>Gets the name of the called method.</summary>
		/// <returns>The name of the method that the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> originated from.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060030ED RID: 12525 RVA: 0x000A0C3C File Offset: 0x0009EE3C
		public string MethodName
		{
			get
			{
				if (this._methodBase != null && this._methodName == null)
				{
					this._methodName = this._methodBase.Name;
				}
				return this._methodName;
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Type" /> objects containing the method signature.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects containing the method signature.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060030EE RID: 12526 RVA: 0x000A0C6C File Offset: 0x0009EE6C
		public object MethodSignature
		{
			get
			{
				if (this._methodBase != null && this._methodSignature == null)
				{
					ParameterInfo[] parameters = this._methodBase.GetParameters();
					this._methodSignature = new Type[parameters.Length];
					for (int i = 0; i < parameters.Length; i++)
					{
						this._methodSignature[i] = parameters[i].ParameterType;
					}
				}
				return this._methodSignature;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> of properties contained in the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> of properties contained in the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060030EF RID: 12527 RVA: 0x000A0CD4 File Offset: 0x0009EED4
		public virtual IDictionary Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodReturnDictionary(this);
				}
				return this._properties;
			}
		}

		/// <summary>Gets the name of the type on which the remote method was called.</summary>
		/// <returns>The type name of the remote object on which the remote method was called.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x000A0CF4 File Offset: 0x0009EEF4
		public string TypeName
		{
			get
			{
				if (this._methodBase != null && this._typeName == null)
				{
					this._typeName = this._methodBase.DeclaringType.AssemblyQualifiedName;
				}
				return this._typeName;
			}
		}

		/// <summary>Gets or sets the URI of the remote object on which the remote method was called.</summary>
		/// <returns>The URI of the remote object on which the remote method was called.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x000A0D34 File Offset: 0x0009EF34
		// (set) Token: 0x060030F2 RID: 12530 RVA: 0x000A0D3C File Offset: 0x0009EF3C
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

		/// <summary>Returns a specified argument passed to the remote method during the method call.</summary>
		/// <returns>An argument passed to the remote method during the method call.</returns>
		/// <param name="argNum">The zero-based index of the requested argument. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060030F3 RID: 12531 RVA: 0x000A0D48 File Offset: 0x0009EF48
		public object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		/// <summary>Returns the name of a specified method argument.</summary>
		/// <returns>The name of a specified method argument.</returns>
		/// <param name="index">The zero-based index of the requested argument name. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060030F4 RID: 12532 RVA: 0x000A0D54 File Offset: 0x0009EF54
		public string GetArgName(int index)
		{
			return this._methodBase.GetParameters()[index].Name;
		}

		/// <summary>Gets the exception that was thrown during the remote method call.</summary>
		/// <returns>The exception thrown during the method call, or null if an exception did not occur during the call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x000A0D68 File Offset: 0x0009EF68
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		/// <summary>Gets the number of out or ref arguments on the called method.</summary>
		/// <returns>The number of out or ref arguments on the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x000A0D70 File Offset: 0x0009EF70
		public int OutArgCount
		{
			get
			{
				if (this._args == null || this._args.Length == 0)
				{
					return 0;
				}
				if (this._inArgInfo == null)
				{
					this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
				}
				return this._inArgInfo.GetInOutArgCount();
			}
		}

		/// <summary>Gets a specified object passed as an out or ref parameter to the called method.</summary>
		/// <returns>An object passed as an out or ref parameter to the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060030F7 RID: 12535 RVA: 0x000A0DC0 File Offset: 0x0009EFC0
		public object[] OutArgs
		{
			get
			{
				if (this._outArgs == null && this._args != null)
				{
					if (this._inArgInfo == null)
					{
						this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
					}
					this._outArgs = this._inArgInfo.GetInOutArgs(this._args);
				}
				return this._outArgs;
			}
		}

		/// <summary>Gets the object returned by the called method.</summary>
		/// <returns>The object returned by the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x000A0E20 File Offset: 0x0009F020
		public virtual object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		/// <summary>Returns the object passed as an out or ref parameter during the remote method call.</summary>
		/// <returns>The object passed as an out or ref parameter during the remote method call.</returns>
		/// <param name="argNum">The zero-based index of the requested out or ref parameter. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060030F9 RID: 12537 RVA: 0x000A0E28 File Offset: 0x0009F028
		public object GetOutArg(int argNum)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._args[this._inArgInfo.GetInOutArgIndex(argNum)];
		}

		/// <summary>Returns the name of a specified out or ref parameter passed to the remote method.</summary>
		/// <returns>A string representing the name of the specified out or ref parameter, or null if the current method is not implemented.</returns>
		/// <param name="index">The zero-based index of the requested argument. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060030FA RID: 12538 RVA: 0x000A0E68 File Offset: 0x0009F068
		public string GetOutArgName(int index)
		{
			if (this._inArgInfo == null)
			{
				this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
			}
			return this._inArgInfo.GetInOutArgName(index);
		}

		// Token: 0x040014AE RID: 5294
		private object[] _outArgs;

		// Token: 0x040014AF RID: 5295
		private object[] _args;

		// Token: 0x040014B0 RID: 5296
		private int _outArgsCount;

		// Token: 0x040014B1 RID: 5297
		private LogicalCallContext _callCtx;

		// Token: 0x040014B2 RID: 5298
		private object _returnValue;

		// Token: 0x040014B3 RID: 5299
		private string _uri;

		// Token: 0x040014B4 RID: 5300
		private Exception _exception;

		// Token: 0x040014B5 RID: 5301
		private MethodBase _methodBase;

		// Token: 0x040014B6 RID: 5302
		private string _methodName;

		// Token: 0x040014B7 RID: 5303
		private Type[] _methodSignature;

		// Token: 0x040014B8 RID: 5304
		private string _typeName;

		// Token: 0x040014B9 RID: 5305
		private MethodReturnDictionary _properties;

		// Token: 0x040014BA RID: 5306
		private Identity _targetIdentity;

		// Token: 0x040014BB RID: 5307
		private ArgInfo _inArgInfo;
	}
}
