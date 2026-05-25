using System;

namespace System.Security.Permissions
{
	// Token: 0x020005FD RID: 1533
	[Serializable]
	internal sealed class HostProtectionPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		// Token: 0x06003A93 RID: 14995 RVA: 0x000C91D4 File Offset: 0x000C73D4
		public HostProtectionPermission(PermissionState state)
		{
			if (CodeAccessPermission.CheckPermissionState(state, true) == PermissionState.Unrestricted)
			{
				this._resources = HostProtectionResource.All;
			}
			else
			{
				this._resources = HostProtectionResource.None;
			}
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x000C920C File Offset: 0x000C740C
		public HostProtectionPermission(HostProtectionResource resources)
		{
			this.Resources = this._resources;
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x000C9220 File Offset: 0x000C7420
		int IBuiltInPermission.GetTokenIndex()
		{
			return 9;
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06003A96 RID: 14998 RVA: 0x000C9224 File Offset: 0x000C7424
		// (set) Token: 0x06003A97 RID: 14999 RVA: 0x000C922C File Offset: 0x000C742C
		public HostProtectionResource Resources
		{
			get
			{
				return this._resources;
			}
			set
			{
				if (!Enum.IsDefined(typeof(HostProtectionResource), value))
				{
					string text = string.Format(Locale.GetText("Invalid enum {0}"), value);
					throw new ArgumentException(text, "HostProtectionResource");
				}
				this._resources = value;
			}
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x000C927C File Offset: 0x000C747C
		public override IPermission Copy()
		{
			return new HostProtectionPermission(this._resources);
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000C928C File Offset: 0x000C748C
		public override IPermission Intersect(IPermission target)
		{
			HostProtectionPermission hostProtectionPermission = this.Cast(target);
			if (hostProtectionPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted() && hostProtectionPermission.IsUnrestricted())
			{
				return new HostProtectionPermission(PermissionState.Unrestricted);
			}
			if (this.IsUnrestricted())
			{
				return hostProtectionPermission.Copy();
			}
			if (hostProtectionPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			return new HostProtectionPermission(this._resources & hostProtectionPermission._resources);
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000C92FC File Offset: 0x000C74FC
		public override IPermission Union(IPermission target)
		{
			HostProtectionPermission hostProtectionPermission = this.Cast(target);
			if (hostProtectionPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || hostProtectionPermission.IsUnrestricted())
			{
				return new HostProtectionPermission(PermissionState.Unrestricted);
			}
			return new HostProtectionPermission(this._resources | hostProtectionPermission._resources);
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000C9350 File Offset: 0x000C7550
		public override bool IsSubsetOf(IPermission target)
		{
			HostProtectionPermission hostProtectionPermission = this.Cast(target);
			if (hostProtectionPermission == null)
			{
				return this._resources == HostProtectionResource.None;
			}
			return hostProtectionPermission.IsUnrestricted() || (!this.IsUnrestricted() && (this._resources & ~hostProtectionPermission._resources) == HostProtectionResource.None);
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000C93A0 File Offset: 0x000C75A0
		public override void FromXml(SecurityElement e)
		{
			CodeAccessPermission.CheckSecurityElement(e, "e", 1, 1);
			this._resources = (HostProtectionResource)((int)Enum.Parse(typeof(HostProtectionResource), e.Attribute("Resources")));
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000C93D8 File Offset: 0x000C75D8
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			securityElement.AddAttribute("Resources", this._resources.ToString());
			return securityElement;
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x000C940C File Offset: 0x000C760C
		public bool IsUnrestricted()
		{
			return this._resources == HostProtectionResource.All;
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000C941C File Offset: 0x000C761C
		private HostProtectionPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			HostProtectionPermission hostProtectionPermission = target as HostProtectionPermission;
			if (hostProtectionPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(HostProtectionPermission));
			}
			return hostProtectionPermission;
		}

		// Token: 0x0400195C RID: 6492
		private const int version = 1;

		// Token: 0x0400195D RID: 6493
		private HostProtectionResource _resources;
	}
}
