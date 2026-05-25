using System;

namespace System.EnterpriseServices
{
	/// <summary>Represents the base class of all classes using COM+ services.</summary>
	// Token: 0x0200003F RID: 63
	[Serializable]
	public abstract class ServicedComponent : ContextBoundObject, IDisposable, IRemoteDispatch, IServicedComponentInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class.</summary>
		// Token: 0x060000F4 RID: 244 RVA: 0x00002990 File Offset: 0x00000B90
		public ServicedComponent()
		{
		}

		/// <summary>Ensures that, in the COM+ context, the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class object's done bit is set to true after a remote method invocation</summary>
		/// <returns>A string converted from a response object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface.</returns>
		/// <param name="s">A string to be converted into a request object that implements the <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface.</param>
		// Token: 0x060000F5 RID: 245 RVA: 0x00002998 File Offset: 0x00000B98
		[MonoTODO]
		string IRemoteDispatch.RemoteDispatchAutoDone(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>Does not ensure that, in the COM+ context, the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class object's done bit is set to true after a remote method invocation.</summary>
		/// <returns>A string converted from a response object implementing the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface.</returns>
		/// <param name="s">A string to be converted into a request object implementing the <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> interface.</param>
		// Token: 0x060000F6 RID: 246 RVA: 0x000029A0 File Offset: 0x00000BA0
		[MonoTODO]
		string IRemoteDispatch.RemoteDispatchNotAutoDone(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>Obtains information about the <see cref="T:System.EnterpriseServices.ServicedComponent" /> class instance.</summary>
		/// <param name="infoMask">A bitmask where 0x00000001 is a key for the serviced component's process ID, 0x00000002 is a key for the application domain ID, and 0x00000004 is a key for the serviced component's remote URI.</param>
		/// <param name="infoArray">A string array that may contain any or all of the following, in order: the serviced component's process ID, the application domain ID, and the serviced component's remote URI.</param>
		// Token: 0x060000F7 RID: 247 RVA: 0x000029A8 File Offset: 0x00000BA8
		[MonoTODO]
		void IServicedComponentInfo.GetComponentInfo(ref int infoMask, out string[] infoArray)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by the infrastructure when the object is created or allocated from a pool. Override this method to add custom initialization code to objects.</summary>
		// Token: 0x060000F8 RID: 248 RVA: 0x000029B0 File Offset: 0x00000BB0
		[MonoTODO]
		protected internal virtual void Activate()
		{
			throw new NotImplementedException();
		}

		/// <summary>This method is called by the infrastructure before the object is put back into the pool. Override this method to vote on whether the object is put back into the pool.</summary>
		/// <returns>true if the serviced component can be pooled; otherwise, false.</returns>
		// Token: 0x060000F9 RID: 249 RVA: 0x000029B8 File Offset: 0x00000BB8
		[MonoTODO]
		protected internal virtual bool CanBePooled()
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by the infrastructure just after the constructor is called, passing in the constructor string. Override this method to make use of the construction string value.</summary>
		/// <param name="s">The construction string. </param>
		// Token: 0x060000FA RID: 250 RVA: 0x000029C0 File Offset: 0x00000BC0
		[MonoTODO]
		protected internal virtual void Construct(string s)
		{
			throw new NotImplementedException();
		}

		/// <summary>Called by the infrastructure when the object is about to be deactivated. Override this method to add custom finalization code to objects when just-in-time (JIT) compiled code or object pooling is used.</summary>
		// Token: 0x060000FB RID: 251 RVA: 0x000029C8 File Offset: 0x00000BC8
		[MonoTODO]
		protected internal virtual void Deactivate()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.EnterpriseServices.ServicedComponent" />.</summary>
		// Token: 0x060000FC RID: 252 RVA: 0x000029D0 File Offset: 0x00000BD0
		[MonoTODO]
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.EnterpriseServices.ServicedComponent" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; otherwise, false to release only unmanaged resources. </param>
		// Token: 0x060000FD RID: 253 RVA: 0x000029D8 File Offset: 0x00000BD8
		[MonoTODO]
		protected virtual void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Finalizes the object and removes the associated COM+ reference.</summary>
		/// <param name="sc">The object to dispose. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlThread" />
		/// </PermissionSet>
		// Token: 0x060000FE RID: 254 RVA: 0x000029E0 File Offset: 0x00000BE0
		[MonoTODO]
		public static void DisposeObject(ServicedComponent sc)
		{
			throw new NotImplementedException();
		}
	}
}
