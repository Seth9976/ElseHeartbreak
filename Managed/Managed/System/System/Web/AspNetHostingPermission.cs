using System;
using System.Security;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Controls access permissions in ASP.NET hosted environments. This class cannot be inherited.</summary>
	// Token: 0x020004BD RID: 1213
	[Serializable]
	public sealed class AspNetHostingPermission : CodeAccessPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.AspNetHostingPermission" /> class with the specified permission level.</summary>
		/// <param name="level">An <see cref="T:System.Web.AspNetHostingPermissionLevel" /> enumeration value. </param>
		// Token: 0x06002BB1 RID: 11185 RVA: 0x000988D4 File Offset: 0x00096AD4
		public AspNetHostingPermission(AspNetHostingPermissionLevel level)
		{
			this.Level = level;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.AspNetHostingPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" /> enumeration value.</summary>
		/// <param name="state">A <see cref="T:System.Security.Permissions.PermissionState" /> enumeration value. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="state" /> is not set to one of the <see cref="T:System.Security.Permissions.PermissionState" /> enumeration values.</exception>
		// Token: 0x06002BB2 RID: 11186 RVA: 0x000988E4 File Offset: 0x00096AE4
		public AspNetHostingPermission(PermissionState state)
		{
			if (global::System.Security.Permissions.PermissionHelper.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._level = AspNetHostingPermissionLevel.Unrestricted;
			}
			else
			{
				this._level = AspNetHostingPermissionLevel.None;
			}
		}

		/// <summary>Gets or sets the current hosting permission level for an ASP.NET application.</summary>
		/// <returns>One of the <see cref="T:System.Web.AspNetHostingPermissionLevel" /> enumeration values.</returns>
		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x00098914 File Offset: 0x00096B14
		// (set) Token: 0x06002BB4 RID: 11188 RVA: 0x0009891C File Offset: 0x00096B1C
		public AspNetHostingPermissionLevel Level
		{
			get
			{
				return this._level;
			}
			set
			{
				if (value < AspNetHostingPermissionLevel.None || value > AspNetHostingPermissionLevel.Unrestricted)
				{
					string text = global::Locale.GetText("Invalid enum {0}.");
					throw new ArgumentException(string.Format(text, value), "Level");
				}
				this._level = value;
			}
		}

		/// <summary>Returns a value indicating whether unrestricted access to the resource that is protected by the current permission is allowed.</summary>
		/// <returns>true if unrestricted use of the resource protected by the permission is allowed; otherwise, false.</returns>
		// Token: 0x06002BB5 RID: 11189 RVA: 0x00098968 File Offset: 0x00096B68
		public bool IsUnrestricted()
		{
			return this._level == AspNetHostingPermissionLevel.Unrestricted;
		}

		/// <summary>When implemented by a derived class, creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x06002BB6 RID: 11190 RVA: 0x00098978 File Offset: 0x00096B78
		public override IPermission Copy()
		{
			return new AspNetHostingPermission(this._level);
		}

		/// <summary>Reconstructs a permission object with a specified state from an XML encoding.</summary>
		/// <param name="securityElement">The <see cref="T:System.Security.SecurityElement" /> containing the XML encoding to use to reconstruct the permission object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="securityElement" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.SecurityElement.Tag" /> property of <paramref name="securityElement" /> is not equal to "IPermission". - or- The class <see cref="M:System.Security.SecurityElement.Attribute(System.String)" /> of <paramref name="securityElement" /> is null or an empty string (""). </exception>
		// Token: 0x06002BB7 RID: 11191 RVA: 0x00098988 File Offset: 0x00096B88
		public override void FromXml(SecurityElement securityElement)
		{
			global::System.Security.Permissions.PermissionHelper.CheckSecurityElement(securityElement, "securityElement", 1, 1);
			if (securityElement.Tag != "IPermission")
			{
				string text = global::Locale.GetText("Invalid tag '{0}' for permission.");
				throw new ArgumentException(string.Format(text, securityElement.Tag), "securityElement");
			}
			if (securityElement.Attribute("version") == null)
			{
				string text2 = global::Locale.GetText("Missing version attribute.");
				throw new ArgumentException(text2, "securityElement");
			}
			if (global::System.Security.Permissions.PermissionHelper.IsUnrestricted(securityElement))
			{
				this._level = AspNetHostingPermissionLevel.Unrestricted;
			}
			else
			{
				string text3 = securityElement.Attribute("Level");
				if (text3 != null)
				{
					this._level = (AspNetHostingPermissionLevel)((int)Enum.Parse(typeof(AspNetHostingPermissionLevel), text3));
				}
				else
				{
					this._level = AspNetHostingPermissionLevel.None;
				}
			}
		}

		/// <summary>Creates an XML encoding of the permission object and its current state.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> containing the XML encoding of the permission object, including any state information.</returns>
		// Token: 0x06002BB8 RID: 11192 RVA: 0x00098A58 File Offset: 0x00096C58
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = global::System.Security.Permissions.PermissionHelper.Element(typeof(AspNetHostingPermission), 1);
			if (this.IsUnrestricted())
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			securityElement.AddAttribute("Level", this._level.ToString());
			return securityElement;
		}

		/// <summary>When implemented by a derived class, creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the intersection of the current permission and the specified permission; otherwise, null if the intersection is empty.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Web.AspNetHostingPermission" />. </exception>
		// Token: 0x06002BB9 RID: 11193 RVA: 0x00098AB0 File Offset: 0x00096CB0
		public override IPermission Intersect(IPermission target)
		{
			AspNetHostingPermission aspNetHostingPermission = this.Cast(target);
			if (aspNetHostingPermission == null)
			{
				return null;
			}
			return new AspNetHostingPermission((this._level > aspNetHostingPermission.Level) ? aspNetHostingPermission.Level : this._level);
		}

		/// <summary>Returns a value indicating whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current <see cref="T:System.Security.IPermission" /> is a subset of the specified <see cref="T:System.Security.IPermission" />; otherwise, false.</returns>
		/// <param name="target">The <see cref="T:System.Security.IPermission" /> to combine with the current permission. It must be of the same type as the current <see cref="T:System.Security.IPermission" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Web.AspNetHostingPermission" />. </exception>
		// Token: 0x06002BBA RID: 11194 RVA: 0x00098AF4 File Offset: 0x00096CF4
		public override bool IsSubsetOf(IPermission target)
		{
			AspNetHostingPermission aspNetHostingPermission = this.Cast(target);
			if (aspNetHostingPermission == null)
			{
				return this.IsEmpty();
			}
			return this._level <= aspNetHostingPermission._level;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>An <see cref="T:System.Security.IPermission" /> that represents the union of the current permission and the specified permission.</returns>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not an <see cref="T:System.Web.AspNetHostingPermission" />. </exception>
		// Token: 0x06002BBB RID: 11195 RVA: 0x00098B28 File Offset: 0x00096D28
		public override IPermission Union(IPermission target)
		{
			AspNetHostingPermission aspNetHostingPermission = this.Cast(target);
			if (aspNetHostingPermission == null)
			{
				return this.Copy();
			}
			return new AspNetHostingPermission((this._level <= aspNetHostingPermission.Level) ? aspNetHostingPermission.Level : this._level);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x00098B74 File Offset: 0x00096D74
		private bool IsEmpty()
		{
			return this._level == AspNetHostingPermissionLevel.None;
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x00098B80 File Offset: 0x00096D80
		private AspNetHostingPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			AspNetHostingPermission aspNetHostingPermission = target as AspNetHostingPermission;
			if (aspNetHostingPermission == null)
			{
				global::System.Security.Permissions.PermissionHelper.ThrowInvalidPermission(target, typeof(AspNetHostingPermission));
			}
			return aspNetHostingPermission;
		}

		// Token: 0x04001B87 RID: 7047
		private const int version = 1;

		// Token: 0x04001B88 RID: 7048
		private AspNetHostingPermissionLevel _level;
	}
}
