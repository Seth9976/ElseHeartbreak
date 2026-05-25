using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.IO.Pipes
{
	/// <summary>Represents the access control and audit security for a pipe.</summary>
	// Token: 0x02000076 RID: 118
	[MonoNotSupported("ACL is not supported in Mono")]
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public class PipeSecurity : NativeObjectSecurity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.Pipes.PipeSecurity" /> class.</summary>
		// Token: 0x060005C6 RID: 1478 RVA: 0x00019594 File Offset: 0x00017794
		[MonoNotSupported("ACL is not supported in Mono")]
		public PipeSecurity()
			: base(false, ResourceType.FileObject)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the securable object that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <returns>The type of the securable object that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</returns>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x000195A8 File Offset: 0x000177A8
		public override Type AccessRightType
		{
			get
			{
				return typeof(PipeAccessRights);
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the object that is associated with the access rules of the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <returns>The type of the object that is associated with the access rules of the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</returns>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x000195B4 File Offset: 0x000177B4
		public override Type AccessRuleType
		{
			get
			{
				return typeof(PipeAccessRule);
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object associated with the audit rules of the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <returns>The type of the object that is associated with the audit rules of the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000195C0 File Offset: 0x000177C0
		public override Type AuditRuleType
		{
			get
			{
				return typeof(PipeAuditRule);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AccessRule" /> class with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AccessRule" /> object that this method creates.</returns>
		/// <param name="identityReference">The identity that the access rule applies to. It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container; otherwise false.</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="type">Specifies the valid access control type.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="type" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identityReference" /> is null. -or-<paramref name="accessMask" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identityReference" /> is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> nor of a type, such as <see cref="T:System.Security.Principal.NTAccount" />, that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x060005CA RID: 1482 RVA: 0x000195CC File Offset: 0x000177CC
		[MonoNotSupported("ACL is not supported in Mono")]
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Adds an access rule to the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The access rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x060005CB RID: 1483 RVA: 0x000195D8 File Offset: 0x000177D8
		[MonoNotSupported("ACL is not supported in Mono")]
		public void AddAccessRule(PipeAccessRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Adds an audit rule to the System Access Control List (SACL)that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x060005CC RID: 1484 RVA: 0x000195E4 File Offset: 0x000177E4
		[MonoNotSupported("ACL is not supported in Mono")]
		public void AddAuditRule(PipeAuditRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.AuditRule" /> class with the specified values.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AuditRule" /> object that this method creates.</returns>
		/// <param name="identityReference">The identity that the access rule applies to. It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container; otherwise, false..</param>
		/// <param name="inheritanceFlags">One of the <see cref="T:System.Security.AccessControl.InheritanceFlags" /> values that specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">One of the <see cref="T:System.Security.AccessControl.PropagationFlags" /> values that specifies whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="flags">One of the <see cref="T:System.Security.AccessControl.AuditFlags" /> values that specifies the valid access control type.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="accessMask" />, <paramref name="inheritanceFlags" />, <paramref name="propagationFlags" />, or <paramref name="flags" /> properties specify an invalid value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="identityReference" /> property is null. -or-The <paramref name="accessMask" /> property is zero.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="identityReference" /> property is neither of type <see cref="T:System.Security.Principal.SecurityIdentifier" /> nor of a type, such as <see cref="T:System.Security.Principal.NTAccount" />, that can be converted to type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
		// Token: 0x060005CD RID: 1485 RVA: 0x000195F0 File Offset: 0x000177F0
		[MonoNotSupported("ACL is not supported in Mono")]
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Saves the specified sections of the security descriptor that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object to permanent storage.</summary>
		/// <param name="handle">The handle of the securable object that the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object is associated with.</param>
		// Token: 0x060005CE RID: 1486 RVA: 0x000195FC File Offset: 0x000177FC
		[MonoNotSupported("ACL is not supported in Mono")]
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		protected internal void Persist(SafeHandle handle)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Saves the specified sections of the security descriptor that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object to permanent storage.</summary>
		/// <param name="name">The name of the securable object that the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object is associated with.</param>
		// Token: 0x060005CF RID: 1487 RVA: 0x00019608 File Offset: 0x00017808
		[MonoNotSupported("ACL is not supported in Mono")]
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		protected internal void Persist(string name)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Removes an access rule from the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <returns>true if the operation is successful; otherwise, false.</returns>
		/// <param name="rule">The access rule to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x060005D0 RID: 1488 RVA: 0x00019614 File Offset: 0x00017814
		[MonoNotSupported("ACL is not supported in Mono")]
		public bool RemoveAccessRule(PipeAccessRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Removes the specified access rule from the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The access rule to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x060005D1 RID: 1489 RVA: 0x00019620 File Offset: 0x00017820
		[MonoNotSupported("ACL is not supported in Mono")]
		public void RemoveAccessRuleSpecific(PipeAccessRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Removes an audit rule from the System Access Control List (SACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <returns>true if the audit rule was removed; otherwise, false</returns>
		/// <param name="rule">The audit rule to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x060005D2 RID: 1490 RVA: 0x0001962C File Offset: 0x0001782C
		[MonoNotSupported("ACL is not supported in Mono")]
		public bool RemoveAuditRule(PipeAuditRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Removes all audit rules that have the same security identifier as the specified audit rule from the System Access Control List (SACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x060005D3 RID: 1491 RVA: 0x00019638 File Offset: 0x00017838
		[MonoNotSupported("ACL is not supported in Mono")]
		public void RemoveAuditRuleAll(PipeAuditRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Removes the specified audit rule from the System Access Control List (SACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The audit rule to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x060005D4 RID: 1492 RVA: 0x00019644 File Offset: 0x00017844
		[MonoNotSupported("ACL is not supported in Mono")]
		public void RemoveAuditRuleSpecific(PipeAuditRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Removes all access rules in the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object and then adds the specified access rule.</summary>
		/// <param name="rule">The access rule to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x060005D5 RID: 1493 RVA: 0x00019650 File Offset: 0x00017850
		[MonoNotSupported("ACL is not supported in Mono")]
		public void ResetAccessRule(PipeAccessRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Sets an access rule in the Discretionary Access Control List (DACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The rule to set.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x060005D6 RID: 1494 RVA: 0x0001965C File Offset: 0x0001785C
		[MonoNotSupported("ACL is not supported in Mono")]
		public void SetAccessRule(PipeAccessRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}

		/// <summary>Sets an audit rule in the System Access Control List (SACL) that is associated with the current <see cref="T:System.IO.Pipes.PipeSecurity" /> object.</summary>
		/// <param name="rule">The rule to set.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rule" /> parameter is null.</exception>
		// Token: 0x060005D7 RID: 1495 RVA: 0x00019668 File Offset: 0x00017868
		[MonoNotSupported("ACL is not supported in Mono")]
		public void SetAuditRule(PipeAuditRule rule)
		{
			throw new NotImplementedException("ACL is not supported in Mono");
		}
	}
}
