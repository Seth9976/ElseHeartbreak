using System;
using System.Transactions;

namespace System.EnterpriseServices
{
	/// <summary>Obtains information about the COM+ object context. This class cannot be inherited.</summary>
	// Token: 0x02000013 RID: 19
	public sealed class ContextUtil
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000023BC File Offset: 0x000005BC
		internal ContextUtil()
		{
		}

		/// <summary>Gets a GUID representing the activity containing the component.</summary>
		/// <returns>The GUID for an activity if the current context is part of an activity; otherwise, GUID_NULL.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000023C4 File Offset: 0x000005C4
		public static Guid ActivityId
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a GUID for the current application.</summary>
		/// <returns>The GUID for the current application.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows XP or later. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000023CC File Offset: 0x000005CC
		public static Guid ApplicationId
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a GUID for the current application instance.</summary>
		/// <returns>The GUID for the current application instance.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows XP or later. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000023D4 File Offset: 0x000005D4
		public static Guid ApplicationInstanceId
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a GUID for the current context.</summary>
		/// <returns>The GUID for the current context.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000023DC File Offset: 0x000005DC
		public static Guid ContextId
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the done bit in the COM+ context.</summary>
		/// <returns>true if the object is to be deactivated when the method returns; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000023E4 File Offset: 0x000005E4
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000023EC File Offset: 0x000005EC
		public static bool DeactivateOnReturn
		{
			get
			{
				return ContextUtil.deactivateOnReturn;
			}
			set
			{
				ContextUtil.deactivateOnReturn = value;
			}
		}

		/// <summary>Gets a value that indicates whether the current context is transactional.</summary>
		/// <returns>true if the current context is transactional; otherwise, false.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000023F4 File Offset: 0x000005F4
		public static bool IsInTransaction
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether role-based security is active in the current context.</summary>
		/// <returns>true if the current context has security enabled; otherwise, false.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000023FC File Offset: 0x000005FC
		public static bool IsSecurityEnabled
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the consistent bit in the COM+ context.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.TransactionVote" /> values, either Commit or Abort.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002404 File Offset: 0x00000604
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000240C File Offset: 0x0000060C
		[MonoTODO]
		public static TransactionVote MyTransactionVote
		{
			get
			{
				return ContextUtil.myTransactionVote;
			}
			set
			{
				ContextUtil.myTransactionVote = value;
			}
		}

		/// <summary>Gets a GUID for the current partition.</summary>
		/// <returns>The GUID for the current partition.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows XP or later. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002414 File Offset: 0x00000614
		public static Guid PartitionId
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an object describing the current COM+ DTC transaction.</summary>
		/// <returns>An object that represents the current transaction.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000241C File Offset: 0x0000061C
		public static object Transaction
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the current transaction context.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> that represents the current transaction context.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later. </exception>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002424 File Offset: 0x00000624
		public static Transaction SystemTransaction
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the GUID of the current COM+ DTC transaction.</summary>
		/// <returns>A GUID representing the current COM+ DTC transaction, if one exists.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000242C File Offset: 0x0000062C
		public static Guid TransactionId
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Sets both the consistent bit and the done bit to false in the COM+ context.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">No COM+ context is available.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600004E RID: 78 RVA: 0x00002434 File Offset: 0x00000634
		[MonoTODO]
		public static void DisableCommit()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the consistent bit to true and the done bit to false in the COM+ context.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">No COM+ context is available. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600004F RID: 79 RVA: 0x0000243C File Offset: 0x0000063C
		[MonoTODO]
		public static void EnableCommit()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a named property from the COM+ context.</summary>
		/// <returns>The named property for the context.</returns>
		/// <param name="name">The name of the requested property. </param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000050 RID: 80 RVA: 0x00002444 File Offset: 0x00000644
		[MonoTODO]
		public static object GetNamedProperty(string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the caller is in the specified role.</summary>
		/// <returns>true if the caller is in the specified role; otherwise, false.</returns>
		/// <param name="role">The name of the role to check. </param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000051 RID: 81 RVA: 0x0000244C File Offset: 0x0000064C
		[MonoTODO]
		public static bool IsCallerInRole(string role)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the serviced component is activated in the default context. Serviced components that do not have COM+ catalog information are activated in the default context.</summary>
		/// <returns>true if the serviced component is activated in the default context; otherwise, false.</returns>
		// Token: 0x06000052 RID: 82 RVA: 0x00002454 File Offset: 0x00000654
		[MonoTODO]
		public static bool IsDefaultContext()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the consistent bit to false and the done bit to true in the COM+ context.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000053 RID: 83 RVA: 0x0000245C File Offset: 0x0000065C
		[MonoTODO]
		public static void SetAbort()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the consistent bit and the done bit to true in the COM+ context.</summary>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000054 RID: 84 RVA: 0x00002464 File Offset: 0x00000664
		[MonoTODO]
		public static void SetComplete()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the named property for the COM+ context.</summary>
		/// <param name="name">The name of the property to set. </param>
		/// <param name="value">Object that represents the property value to set.</param>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">There is no COM+ context available. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The platform is not Windows 2000 or later. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000055 RID: 85 RVA: 0x0000246C File Offset: 0x0000066C
		[MonoTODO]
		public static void SetNamedProperty(string name, object property)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000041 RID: 65
		private static bool deactivateOnReturn;

		// Token: 0x04000042 RID: 66
		private static TransactionVote myTransactionVote;
	}
}
