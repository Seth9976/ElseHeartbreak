using System;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.IO.Pipes
{
	/// <summary>Represents an abstraction of an access control entry (ACE) that defines an audit rule for a pipe.</summary>
	// Token: 0x0200006E RID: 110
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public sealed class PipeAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeAuditRule" /> class for a user account specified in a <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		/// <param name="identity">An <see cref="T:System.Security.Principal.IdentityReference" /> object that encapsulates a reference to a user account.</param>
		/// <param name="rights">One of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		// Token: 0x060005B8 RID: 1464 RVA: 0x00019550 File Offset: 0x00017750
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeAuditRule(IdentityReference identity, PipeAccessRights rights, AuditFlags flags)
			: base(identity, 0, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeAuditRule" /> class for a named user account.</summary>
		/// <param name="identity">The name of the user account.</param>
		/// <param name="rights">One of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values that specifies the type of operation associated with the access rule.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies when to perform auditing.</param>
		// Token: 0x060005B9 RID: 1465 RVA: 0x00019568 File Offset: 0x00017768
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeAuditRule(string identity, PipeAccessRights rights, AuditFlags flags)
			: this(null, rights, flags)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Gets the <see cref="T:System.IO.Pipes.PipeAccessRights" /> flags that are associated with the current <see cref="T:System.IO.Pipes.PipeAuditRule" /> object.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.IO.Pipes.PipeAccessRights" /> values. </returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00019580 File Offset: 0x00017780
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00019588 File Offset: 0x00017788
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeAccessRights PipeAccessRights { get; private set; }
	}
}
