using System;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.IO.Pipes
{
	/// <summary>Represents an abstraction of an access control entry (ACE) that defines an access rule for a pipe.</summary>
	// Token: 0x0200006D RID: 109
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class PipeAccessRule : AccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeAccessRule" /> class with the specified identity, pipe access rights, and access control type.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that encapsulates a reference to a user account.</param>
		/// <param name="rights">One of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether to allow or deny the operation.</param>
		// Token: 0x060005B4 RID: 1460 RVA: 0x0001950C File Offset: 0x0001770C
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeAccessRule(IdentityReference identity, PipeAccessRights rights, AccessControlType type)
			: base(identity, 0, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeAccessRule" /> class with the specified identity, pipe access rights, and access control type.</summary>
		/// <param name="identity">The name of the user account.</param>
		/// <param name="rights">One of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="type">One of the <see cref="T:System.Security.AccessControl.AccessControlType" /> values that specifies whether to allow or deny the operation.</param>
		// Token: 0x060005B5 RID: 1461 RVA: 0x00019524 File Offset: 0x00017724
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeAccessRule(string identity, PipeAccessRights rights, AccessControlType type)
			: this(null, rights, type)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Gets the <see cref="T:System.IO.Pipes.PipeAccessRights" /> flags that are associated with the current <see cref="T:System.IO.Pipes.PipeAccessRule" /> object.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001953C File Offset: 0x0001773C
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x00019544 File Offset: 0x00017744
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeAccessRights PipeAccessRights { get; private set; }
	}
}
