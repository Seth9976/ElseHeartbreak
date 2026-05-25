using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security
{
	/// <summary>Defines the underlying structure of all code access permissions.</summary>
	// Token: 0x02000532 RID: 1330
	[MonoTODO("CAS support is experimental (and unsupported).")]
	[ComVisible(true)]
	[PermissionSet(SecurityAction.InheritanceDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence, ControlPolicy\"/>\n</PermissionSet>\n")]
	[Serializable]
	public abstract class CodeAccessPermission : IPermission, ISecurityEncodable, IStackWalk
	{
		/// <summary>Declares that the calling code can access the resource protected by a permission demand through the code that calls this method, even if callers higher in the stack have not been granted permission to access the resource. Using <see cref="M:System.Security.CodeAccessPermission.Assert" /> can create security issues.</summary>
		/// <exception cref="T:System.Security.SecurityException">The calling code does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Assertion" />.-or- There is already an active <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame. </exception>
		// Token: 0x0600345E RID: 13406 RVA: 0x000ABFD8 File Offset: 0x000AA1D8
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public void Assert()
		{
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x000ABFDC File Offset: 0x000AA1DC
		internal bool CheckAssert(CodeAccessPermission asserted)
		{
			return asserted != null && asserted.GetType() == base.GetType() && this.IsSubsetOf(asserted);
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x000AC00C File Offset: 0x000AA20C
		internal bool CheckDemand(CodeAccessPermission target)
		{
			return target != null && target.GetType() == base.GetType() && this.IsSubsetOf(target);
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x000AC03C File Offset: 0x000AA23C
		internal bool CheckDeny(CodeAccessPermission denied)
		{
			if (denied == null)
			{
				return true;
			}
			Type type = denied.GetType();
			return type != base.GetType() || this.Intersect(denied) == null || denied.IsSubsetOf(PermissionBuilder.Create(type));
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x000AC084 File Offset: 0x000AA284
		internal bool CheckPermitOnly(CodeAccessPermission target)
		{
			return target != null && target.GetType() == base.GetType() && this.IsSubsetOf(target);
		}

		/// <summary>When implemented by a derived class, creates and returns an identical copy of the current permission object.</summary>
		/// <returns>A copy of the current permission object.</returns>
		// Token: 0x06003463 RID: 13411
		public abstract IPermission Copy();

		/// <summary>Forces a <see cref="T:System.Security.SecurityException" /> at run time if all callers higher in the call stack have not been granted the permission specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have the permission specified by the current instance.-or- A caller higher in the call stack has called <see cref="M:System.Security.CodeAccessPermission.Deny" /> on the current permission object. </exception>
		// Token: 0x06003464 RID: 13412 RVA: 0x000AC0B4 File Offset: 0x000AA2B4
		public void Demand()
		{
		}

		/// <summary>Prevents callers higher in the call stack from using the code that calls this method to access the resource specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">There is already an active <see cref="M:System.Security.CodeAccessPermission.Deny" /> for the current frame. </exception>
		// Token: 0x06003465 RID: 13413 RVA: 0x000AC0B8 File Offset: 0x000AA2B8
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public void Deny()
		{
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.CodeAccessPermission" /> object is equal to the current <see cref="T:System.Security.CodeAccessPermission" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.CodeAccessPermission" /> object is equal to the current <see cref="T:System.Security.CodeAccessPermission" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Security.CodeAccessPermission" /> object to compare with the current <see cref="T:System.Security.CodeAccessPermission" />. </param>
		// Token: 0x06003466 RID: 13414 RVA: 0x000AC0BC File Offset: 0x000AA2BC
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj.GetType() != base.GetType())
			{
				return false;
			}
			CodeAccessPermission codeAccessPermission = obj as CodeAccessPermission;
			return this.IsSubsetOf(codeAccessPermission) && codeAccessPermission.IsSubsetOf(this);
		}

		/// <summary>When overridden in a derived class, reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="elem">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elem" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="elem" /> parameter does not contain the XML encoding for an instance of the same type as the current instance.-or- The version number of the <paramref name="elem" /> parameter is not supported. </exception>
		// Token: 0x06003467 RID: 13415
		public abstract void FromXml(SecurityElement elem);

		/// <summary>Gets a hash code for the <see cref="T:System.Security.CodeAccessPermission" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.CodeAccessPermission" /> object.</returns>
		// Token: 0x06003468 RID: 13416 RVA: 0x000AC104 File Offset: 0x000AA304
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>When implemented by a derived class, creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not an instance of the same class as the current permission. </exception>
		// Token: 0x06003469 RID: 13417
		public abstract IPermission Intersect(IPermission target);

		/// <summary>When implemented by a derived class, determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x0600346A RID: 13418
		public abstract bool IsSubsetOf(IPermission target);

		/// <summary>Creates and returns a string representation of the current permission object.</summary>
		/// <returns>A string representation of the current permission object.</returns>
		// Token: 0x0600346B RID: 13419 RVA: 0x000AC10C File Offset: 0x000AA30C
		public override string ToString()
		{
			SecurityElement securityElement = this.ToXml();
			return securityElement.ToString();
		}

		/// <summary>When overridden in a derived class, creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x0600346C RID: 13420
		public abstract SecurityElement ToXml();

		/// <summary>When overridden in a derived class, creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="other" /> parameter is not null. This method is only supported at this level when passed null. </exception>
		// Token: 0x0600346D RID: 13421 RVA: 0x000AC128 File Offset: 0x000AA328
		public virtual IPermission Union(IPermission other)
		{
			if (other != null)
			{
				throw new NotSupportedException();
			}
			return null;
		}

		/// <summary>Prevents callers higher in the call stack from using the code that calls this method to access all resources except for the resource specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">There is already an active <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> for the current frame. </exception>
		// Token: 0x0600346E RID: 13422 RVA: 0x000AC138 File Offset: 0x000AA338
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public void PermitOnly()
		{
		}

		/// <summary>Causes all previous overrides for the current frame to be removed and no longer in effect.</summary>
		/// <exception cref="T:System.ExecutionEngineException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Assert" />, <see cref="M:System.Security.CodeAccessPermission.Deny" />, or <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> for the current frame. </exception>
		// Token: 0x0600346F RID: 13423 RVA: 0x000AC13C File Offset: 0x000AA33C
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public static void RevertAll()
		{
		}

		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame to be removed and no longer in effect.</summary>
		/// <exception cref="T:System.ExecutionEngineException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame. </exception>
		// Token: 0x06003470 RID: 13424 RVA: 0x000AC140 File Offset: 0x000AA340
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public static void RevertAssert()
		{
		}

		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.Deny" /> for the current frame to be removed and no longer in effect.</summary>
		/// <exception cref="T:System.ExecutionEngineException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Deny" /> for the current frame. </exception>
		// Token: 0x06003471 RID: 13425 RVA: 0x000AC144 File Offset: 0x000AA344
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public static void RevertDeny()
		{
		}

		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> for the current frame to be removed and no longer in effect.</summary>
		/// <exception cref="T:System.ExecutionEngineException">There is no previous <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> for the current frame. </exception>
		// Token: 0x06003472 RID: 13426 RVA: 0x000AC148 File Offset: 0x000AA348
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public static void RevertPermitOnly()
		{
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x000AC14C File Offset: 0x000AA34C
		internal SecurityElement Element(int version)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			Type type = base.GetType();
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000AC1B0 File Offset: 0x000AA3B0
		internal static PermissionState CheckPermissionState(PermissionState state, bool allowUnrestricted)
		{
			if (state != PermissionState.None)
			{
				if (state != PermissionState.Unrestricted)
				{
					string text = string.Format(Locale.GetText("Invalid enum {0}"), state);
					throw new ArgumentException(text, "state");
				}
			}
			return state;
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000AC200 File Offset: 0x000AA400
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != "IPermission")
			{
				string text = string.Format(Locale.GetText("Invalid tag {0}"), se.Tag);
				throw new ArgumentException(text, parameterName);
			}
			int num = minimumVersion;
			string text2 = se.Attribute("version");
			if (text2 != null)
			{
				try
				{
					num = int.Parse(text2);
				}
				catch (Exception ex)
				{
					string text3 = Locale.GetText("Couldn't parse version from '{0}'.");
					text3 = string.Format(text3, text2);
					throw new ArgumentException(text3, parameterName, ex);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				string text4 = Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}'].");
				text4 = string.Format(text4, num, minimumVersion, maximumVersion);
				throw new ArgumentException(text4, parameterName);
			}
			return num;
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x000AC2F0 File Offset: 0x000AA4F0
		internal static bool IsUnrestricted(SecurityElement se)
		{
			string text = se.Attribute("Unrestricted");
			return text != null && string.Compare(text, bool.TrueString, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x000AC328 File Offset: 0x000AA528
		internal bool ProcessFrame(SecurityFrame frame)
		{
			if (frame.PermitOnly != null)
			{
				bool flag = frame.PermitOnly.IsUnrestricted();
				if (!flag)
				{
					foreach (object obj in frame.PermitOnly)
					{
						IPermission permission = (IPermission)obj;
						if (this.CheckPermitOnly(permission as CodeAccessPermission))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					CodeAccessPermission.ThrowSecurityException(this, "PermitOnly", frame, SecurityAction.Demand, null);
				}
			}
			if (frame.Deny != null)
			{
				if (frame.Deny.IsUnrestricted())
				{
					CodeAccessPermission.ThrowSecurityException(this, "Deny", frame, SecurityAction.Demand, null);
				}
				foreach (object obj2 in frame.Deny)
				{
					IPermission permission2 = (IPermission)obj2;
					if (!this.CheckDeny(permission2 as CodeAccessPermission))
					{
						CodeAccessPermission.ThrowSecurityException(this, "Deny", frame, SecurityAction.Demand, permission2);
					}
				}
			}
			if (frame.Assert != null)
			{
				if (frame.Assert.IsUnrestricted())
				{
					return true;
				}
				foreach (object obj3 in frame.Assert)
				{
					IPermission permission3 = (IPermission)obj3;
					if (this.CheckAssert(permission3 as CodeAccessPermission))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000AC528 File Offset: 0x000AA728
		internal static void ThrowInvalidPermission(IPermission target, Type expected)
		{
			string text = Locale.GetText("Invalid permission type '{0}', expected type '{1}'.");
			text = string.Format(text, target.GetType(), expected);
			throw new ArgumentException(text, "target");
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x000AC55C File Offset: 0x000AA75C
		internal static void ThrowExecutionEngineException(SecurityAction stackmod)
		{
			string text = Locale.GetText("No {0} modifier is present on the current stack frame.");
			text = text + Environment.NewLine + "Currently only declarative stack modifiers are supported.";
			throw new ExecutionEngineException(string.Format(text, stackmod));
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000AC598 File Offset: 0x000AA798
		internal static void ThrowSecurityException(object demanded, string message, SecurityFrame frame, SecurityAction action, IPermission failed)
		{
			Assembly assembly = frame.Assembly;
			throw new SecurityException(Locale.GetText(message), assembly.UnprotectedGetName(), assembly.GrantedPermissionSet, assembly.DeniedPermissionSet, frame.Method, action, demanded, failed, assembly.UnprotectedGetEvidence());
		}
	}
}
