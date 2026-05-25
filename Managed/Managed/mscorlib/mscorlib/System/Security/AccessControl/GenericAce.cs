using System;

namespace System.Security.AccessControl
{
	/// <summary>Represents an Access Control Entry (ACE), and is the base class for all other ACE classes.</summary>
	// Token: 0x02000579 RID: 1401
	public abstract class GenericAce
	{
		// Token: 0x06003657 RID: 13911 RVA: 0x000B203C File Offset: 0x000B023C
		internal GenericAce(InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			this.inheritance = inheritanceFlags;
			this.propagation = propagationFlags;
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000B2054 File Offset: 0x000B0254
		internal GenericAce(AceType type)
		{
			if (type <= AceType.SystemAlarmCallbackObject)
			{
				throw new ArgumentOutOfRangeException("type");
			}
			this.ace_type = type;
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.AccessControl.AceFlags" /> associated with this <see cref="T:System.Security.AccessControl.GenericAce" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.AceFlags" /> associated with this <see cref="T:System.Security.AccessControl.GenericAce" /> object.</returns>
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06003659 RID: 13913 RVA: 0x000B2084 File Offset: 0x000B0284
		// (set) Token: 0x0600365A RID: 13914 RVA: 0x000B208C File Offset: 0x000B028C
		public AceFlags AceFlags
		{
			get
			{
				return this.aceflags;
			}
			set
			{
				this.aceflags = value;
			}
		}

		/// <summary>Gets the type of this Access Control Entry (ACE).</summary>
		/// <returns>The type of this ACE.</returns>
		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600365B RID: 13915 RVA: 0x000B2098 File Offset: 0x000B0298
		public AceType AceType
		{
			get
			{
				return this.ace_type;
			}
		}

		/// <summary>Gets the audit information associated with this Access Control Entry (ACE).</summary>
		/// <returns>The audit information associated with this Access Control Entry (ACE).</returns>
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600365C RID: 13916 RVA: 0x000B20A0 File Offset: 0x000B02A0
		public AuditFlags AuditFlags
		{
			get
			{
				AuditFlags auditFlags = AuditFlags.None;
				if ((byte)(this.aceflags & AceFlags.SuccessfulAccess) != 0)
				{
					auditFlags |= AuditFlags.Success;
				}
				if ((byte)(this.aceflags & AceFlags.FailedAccess) != 0)
				{
					auditFlags |= AuditFlags.Failure;
				}
				return auditFlags;
			}
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericAce" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.GenericAce.GetBinaryForm" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.GenericAce" /> object.</returns>
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600365D RID: 13917
		public abstract int BinaryLength { get; }

		/// <summary>Gets flags that specify the inheritance properties of this Access Control Entry (ACE).</summary>
		/// <returns>Flags that specify the inheritance properties of this ACE.</returns>
		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600365E RID: 13918 RVA: 0x000B20DC File Offset: 0x000B02DC
		public InheritanceFlags InheritanceFlags
		{
			get
			{
				return this.inheritance;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether this Access Control Entry (ACE) is inherited or is set explicitly.</summary>
		/// <returns>true if this ACE is inherited; otherwise, false.</returns>
		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600365F RID: 13919 RVA: 0x000B20E4 File Offset: 0x000B02E4
		[MonoTODO]
		public bool IsInherited
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets flags that specify the inheritance propagation properties of this Access Control Entry (ACE).</summary>
		/// <returns>Flags that specify the inheritance propagation properties of this ACE.</returns>
		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06003660 RID: 13920 RVA: 0x000B20E8 File Offset: 0x000B02E8
		public PropagationFlags PropagationFlags
		{
			get
			{
				return this.propagation;
			}
		}

		/// <summary>Creates a deep copy of this Access Control Entry (ACE).</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.GenericAce" /> object that this method creates.</returns>
		// Token: 0x06003661 RID: 13921 RVA: 0x000B20F0 File Offset: 0x000B02F0
		[MonoTODO]
		public GenericAce Copy()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a <see cref="T:System.Security.AccessControl.GenericAce" /> object from the specified binary data.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.GenericAce" /> object this method creates.</returns>
		/// <param name="binaryForm">The binary data from which to create the new <see cref="T:System.Security.AccessControl.GenericAce" /> object.</param>
		/// <param name="offset">The offset at which to begin unmarshaling.</param>
		// Token: 0x06003662 RID: 13922 RVA: 0x000B20F8 File Offset: 0x000B02F8
		[MonoTODO]
		public static GenericAce CreateFromBinaryForm(byte[] binaryForm, int offset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.AccessControl.GenericAce" /> object is equal to the current <see cref="T:System.Security.AccessControl.GenericAce" /> object.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.AccessControl.GenericAce" /> object is equal to the current <see cref="T:System.Security.AccessControl.GenericAce" /> object; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.AccessControl.GenericAce" /> object to compare to the current <see cref="T:System.Security.AccessControl.GenericAce" /> object.</param>
		// Token: 0x06003663 RID: 13923 RVA: 0x000B2100 File Offset: 0x000B0300
		[MonoTODO]
		public sealed override bool Equals(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.GenericAce" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.GenericAce" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.GenericAcl" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x06003664 RID: 13924
		[MonoTODO]
		public abstract void GetBinaryForm(byte[] binaryForm, int offset);

		/// <summary>Serves as a hash function for the <see cref="T:System.Security.AccessControl.GenericAce" /> class. The  <see cref="M:System.Security.AccessControl.GenericAce.GetHashCode" /> method is suitable for use in hashing algorithms and data structures like a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.AccessControl.GenericAce" /> object.</returns>
		// Token: 0x06003665 RID: 13925 RVA: 0x000B2108 File Offset: 0x000B0308
		[MonoTODO]
		public sealed override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.AccessControl.GenericAce" /> objects are considered equal.</summary>
		/// <returns>true if the two <see cref="T:System.Security.AccessControl.GenericAce" /> objects are equal; otherwise, false.</returns>
		/// <param name="left">The first <see cref="T:System.Security.AccessControl.GenericAce" /> object to compare.</param>
		/// <param name="right">The second <see cref="T:System.Security.AccessControl.GenericAce" /> to compare.</param>
		// Token: 0x06003666 RID: 13926 RVA: 0x000B2110 File Offset: 0x000B0310
		[MonoTODO]
		public static bool operator ==(GenericAce left, GenericAce right)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.AccessControl.GenericAce" /> objects are considered unequal.</summary>
		/// <returns>true if the two <see cref="T:System.Security.AccessControl.GenericAce" /> objects are unequal; otherwise, false.</returns>
		/// <param name="left">The first <see cref="T:System.Security.AccessControl.GenericAce" /> object to compare.</param>
		/// <param name="right">The second <see cref="T:System.Security.AccessControl.GenericAce" /> to compare.</param>
		// Token: 0x06003667 RID: 13927 RVA: 0x000B2118 File Offset: 0x000B0318
		[MonoTODO]
		public static bool operator !=(GenericAce left, GenericAce right)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001719 RID: 5913
		private InheritanceFlags inheritance;

		// Token: 0x0400171A RID: 5914
		private PropagationFlags propagation;

		// Token: 0x0400171B RID: 5915
		private AceFlags aceflags;

		// Token: 0x0400171C RID: 5916
		private AceType ace_type;
	}
}
