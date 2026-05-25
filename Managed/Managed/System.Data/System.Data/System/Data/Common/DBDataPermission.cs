using System;
using System.Collections;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Common
{
	/// <summary>Enables a .NET Framework data provider to help ensure that a user has a security level adequate for accessing data.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000BF RID: 191
	[Serializable]
	public abstract class DBDataPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of a DBDataPermission class.</summary>
		// Token: 0x0600091D RID: 2333 RVA: 0x0002D840 File Offset: 0x0002BA40
		[Obsolete("use DBDataPermission (PermissionState.None)", true)]
		protected DBDataPermission()
			: this(PermissionState.None)
		{
		}

		/// <summary>Initializes a new instance of a DBDataPermission class using an existing DBDataPermission.</summary>
		/// <param name="permission">An existing DBDataPermission used to create a new DBDataPermission. </param>
		// Token: 0x0600091E RID: 2334 RVA: 0x0002D84C File Offset: 0x0002BA4C
		protected DBDataPermission(DBDataPermission permission)
		{
			if (permission == null)
			{
				throw new ArgumentNullException("permission");
			}
			this.state = permission.state;
			if (this.state != PermissionState.Unrestricted)
			{
				this.allowBlankPassword = permission.allowBlankPassword;
				this._connections = (Hashtable)permission._connections.Clone();
			}
		}

		/// <summary>Initializes a new instance of a DBDataPermission class with the specified DBDataPermissionAttribute.</summary>
		/// <param name="permissionAttribute">A security action associated with a custom security attribute. </param>
		// Token: 0x0600091F RID: 2335 RVA: 0x0002D8AC File Offset: 0x0002BAAC
		protected DBDataPermission(DBDataPermissionAttribute permissionAttribute)
		{
			if (permissionAttribute == null)
			{
				throw new ArgumentNullException("permissionAttribute");
			}
			this._connections = new Hashtable();
			if (permissionAttribute.Unrestricted)
			{
				this.state = PermissionState.Unrestricted;
			}
			else
			{
				this.state = PermissionState.None;
				this.allowBlankPassword = permissionAttribute.AllowBlankPassword;
				if (permissionAttribute.ConnectionString.Length > 0)
				{
					this.Add(permissionAttribute.ConnectionString, permissionAttribute.KeyRestrictions, permissionAttribute.KeyRestrictionBehavior);
				}
			}
		}

		/// <summary>Initializes a new instance of a DBDataPermission class with the specified <see cref="T:System.Security.Permissions.PermissionState" /> value.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		// Token: 0x06000920 RID: 2336 RVA: 0x0002D930 File Offset: 0x0002BB30
		protected DBDataPermission(PermissionState state)
		{
			this.state = PermissionHelper.CheckPermissionState(state, true);
			this._connections = new Hashtable();
		}

		/// <summary>Initializes a new instance of a DBDataPermission class with the specified <see cref="T:System.Security.Permissions.PermissionState" /> value, and a value indicating whether a blank password is allowed.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <param name="allowBlankPassword">Indicates whether a blank password is allowed. </param>
		// Token: 0x06000921 RID: 2337 RVA: 0x0002D950 File Offset: 0x0002BB50
		[Obsolete("use DBDataPermission (PermissionState.None)", true)]
		protected DBDataPermission(PermissionState state, bool allowBlankPassword)
			: this(state)
		{
			this.allowBlankPassword = allowBlankPassword;
		}

		/// <summary>Gets a value indicating whether a blank password is allowed.</summary>
		/// <returns>true if a blank password is allowed, otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0002D960 File Offset: 0x0002BB60
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0002D968 File Offset: 0x0002BB68
		public bool AllowBlankPassword
		{
			get
			{
				return this.allowBlankPassword;
			}
			set
			{
				this.allowBlankPassword = value;
			}
		}

		/// <summary>Adds access for the specified connection string to the existing state of the DBDataPermission. </summary>
		/// <param name="connectionString">A permitted connection string.</param>
		/// <param name="restrictions">String that identifies connection string parameters that are allowed or disallowed.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.KeyRestrictionBehavior" /> properties.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000924 RID: 2340 RVA: 0x0002D974 File Offset: 0x0002BB74
		public virtual void Add(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
		{
			this.state = PermissionState.None;
			this._connections[connectionString] = new object[] { restrictions, behavior };
		}

		/// <summary>Removes all permissions that were previous added using the <see cref="M:System.Data.Common.DBDataPermission.Add(System.String,System.String,System.Data.KeyRestrictionBehavior)" /> method.</summary>
		// Token: 0x06000925 RID: 2341 RVA: 0x0002D9A8 File Offset: 0x0002BBA8
		protected void Clear()
		{
			this._connections.Clear();
		}

		/// <summary>Creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000926 RID: 2342 RVA: 0x0002D9B8 File Offset: 0x0002BBB8
		public override IPermission Copy()
		{
			DBDataPermission dbdataPermission = this.CreateInstance();
			dbdataPermission.allowBlankPassword = this.allowBlankPassword;
			dbdataPermission._connections = (Hashtable)this._connections.Clone();
			return dbdataPermission;
		}

		/// <summary>Creates a new instance of the DBDataPermission class.</summary>
		/// <returns>A new DBDataPermission object.</returns>
		// Token: 0x06000927 RID: 2343 RVA: 0x0002D9F0 File Offset: 0x0002BBF0
		protected virtual DBDataPermission CreateInstance()
		{
			return (DBDataPermission)Activator.CreateInstance(base.GetType(), new object[] { PermissionState.None });
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The XML encoding to use to reconstruct the security object. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000928 RID: 2344 RVA: 0x0002DA14 File Offset: 0x0002BC14
		public override void FromXml(SecurityElement securityElement)
		{
			PermissionHelper.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			this.state = ((!PermissionHelper.IsUnrestricted(securityElement)) ? PermissionState.None : PermissionState.Unrestricted);
			this.allowBlankPassword = false;
			string text = securityElement.Attribute("AllowBlankPassword");
			if (text != null && !bool.TryParse(text, out this.allowBlankPassword))
			{
				this.allowBlankPassword = false;
			}
			if (securityElement.Children != null)
			{
				foreach (object obj in securityElement.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					string text2 = securityElement2.Attribute("ConnectionString");
					string text3 = securityElement2.Attribute("KeyRestrictions");
					KeyRestrictionBehavior keyRestrictionBehavior = (KeyRestrictionBehavior)((int)Enum.Parse(typeof(KeyRestrictionBehavior), securityElement2.Attribute("KeyRestrictionBehavior")));
					if (text2 != null && text2.Length > 0)
					{
						this.Add(text2, text3, keyRestrictionBehavior);
					}
				}
			}
		}

		/// <summary>Returns a new permission object representing the intersection of the current permission object and the specified permission object.</summary>
		/// <returns>A new permission object that represents the intersection of the current permission object and the specified permission object. This new permission object is a null reference (Nothing in Visual Basic) if the intersection is empty.</returns>
		/// <param name="target">A permission object to intersect with the current permission object. It must be of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not a null reference (Nothing in Visual Basic) and is not an instance of the same class as the current permission object. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000929 RID: 2345 RVA: 0x0002DB38 File Offset: 0x0002BD38
		public override IPermission Intersect(IPermission target)
		{
			DBDataPermission dbdataPermission = this.Cast(target);
			if (dbdataPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				if (dbdataPermission.IsUnrestricted())
				{
					DBDataPermission dbdataPermission2 = this.CreateInstance();
					dbdataPermission2.state = PermissionState.Unrestricted;
					return dbdataPermission2;
				}
				return dbdataPermission.Copy();
			}
			else
			{
				if (dbdataPermission.IsUnrestricted())
				{
					return this.Copy();
				}
				if (this.IsEmpty() || dbdataPermission.IsEmpty())
				{
					return null;
				}
				DBDataPermission dbdataPermission3 = this.CreateInstance();
				dbdataPermission3.allowBlankPassword = this.allowBlankPassword && dbdataPermission.allowBlankPassword;
				foreach (object obj in this._connections)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					object obj2 = dbdataPermission._connections[dictionaryEntry.Key];
					if (obj2 != null)
					{
						dbdataPermission3._connections.Add(dictionaryEntry.Key, dictionaryEntry.Value);
					}
				}
				return (dbdataPermission3._connections.Count <= 0) ? null : dbdataPermission3;
			}
		}

		/// <summary>Returns a value indicating whether the current permission object is a subset of the specified permission object.</summary>
		/// <returns>true if the current permission object is a subset of the specified permission object, otherwise false.</returns>
		/// <param name="target">A permission object that is to be tested for the subset relationship. This object must be of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is an object that is not of the same type as the current permission object. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600092A RID: 2346 RVA: 0x0002DC7C File Offset: 0x0002BE7C
		public override bool IsSubsetOf(IPermission target)
		{
			DBDataPermission dbdataPermission = this.Cast(target);
			if (dbdataPermission == null)
			{
				return this.IsEmpty();
			}
			if (dbdataPermission.IsUnrestricted())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return dbdataPermission.IsUnrestricted();
			}
			if (this.allowBlankPassword && !dbdataPermission.allowBlankPassword)
			{
				return false;
			}
			if (this._connections.Count > dbdataPermission._connections.Count)
			{
				return false;
			}
			foreach (object obj in this._connections)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (dbdataPermission._connections[dictionaryEntry.Key] == null)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns a value indicating whether the permission can be represented as unrestricted without any knowledge of the permission semantics.</summary>
		/// <returns>true if the permission can be represented as unrestricted.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600092B RID: 2347 RVA: 0x0002DD78 File Offset: 0x0002BF78
		public bool IsUnrestricted()
		{
			return this.state == PermissionState.Unrestricted;
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600092C RID: 2348 RVA: 0x0002DD84 File Offset: 0x0002BF84
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = PermissionHelper.Element(base.GetType(), 1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				securityElement.AddAttribute("AllowBlankPassword", this.allowBlankPassword.ToString());
				foreach (object obj in this._connections)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					SecurityElement securityElement2 = new SecurityElement("add");
					securityElement2.AddAttribute("ConnectionString", (string)dictionaryEntry.Key);
					object[] array = (object[])dictionaryEntry.Value;
					securityElement2.AddAttribute("KeyRestrictions", (string)array[0]);
					KeyRestrictionBehavior keyRestrictionBehavior = (KeyRestrictionBehavior)((int)array[1]);
					securityElement2.AddAttribute("KeyRestrictionBehavior", keyRestrictionBehavior.ToString());
					securityElement.AddChild(securityElement2);
				}
			}
			return securityElement;
		}

		/// <summary>Returns a new permission object that is the union of the current and specified permission objects.</summary>
		/// <returns>A new permission object that represents the union of the current permission object and the specified permission object.</returns>
		/// <param name="target">A permission object to combine with the current permission object. It must be of the same type as the current permission object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> object is not the same type as the current permission object.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600092D RID: 2349 RVA: 0x0002DEA0 File Offset: 0x0002C0A0
		public override IPermission Union(IPermission target)
		{
			DBDataPermission dbdataPermission = this.Cast(target);
			if (dbdataPermission == null)
			{
				return this.Copy();
			}
			if (this.IsEmpty() && dbdataPermission.IsEmpty())
			{
				return this.Copy();
			}
			DBDataPermission dbdataPermission2 = this.CreateInstance();
			if (this.IsUnrestricted() || dbdataPermission.IsUnrestricted())
			{
				dbdataPermission2.state = PermissionState.Unrestricted;
			}
			else
			{
				dbdataPermission2.allowBlankPassword = this.allowBlankPassword || dbdataPermission.allowBlankPassword;
				dbdataPermission2._connections = new Hashtable(this._connections.Count + dbdataPermission._connections.Count);
				foreach (object obj in this._connections)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					dbdataPermission2._connections.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				}
				foreach (object obj2 in dbdataPermission._connections)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					dbdataPermission2._connections[dictionaryEntry2.Key] = dictionaryEntry2.Value;
				}
			}
			return dbdataPermission2;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0002E034 File Offset: 0x0002C234
		private bool IsEmpty()
		{
			return this.state != PermissionState.Unrestricted && this._connections.Count == 0;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0002E054 File Offset: 0x0002C254
		private DBDataPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			DBDataPermission dbdataPermission = target as DBDataPermission;
			if (dbdataPermission == null)
			{
				PermissionHelper.ThrowInvalidPermission(target, base.GetType());
			}
			return dbdataPermission;
		}

		// Token: 0x04000330 RID: 816
		private const int version = 1;

		// Token: 0x04000331 RID: 817
		private bool allowBlankPassword;

		// Token: 0x04000332 RID: 818
		private PermissionState state;

		// Token: 0x04000333 RID: 819
		private Hashtable _connections;
	}
}
