using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

namespace System.Security.Permissions
{
	/// <summary>Controls the ability to access registry variables. This class cannot be inherited.</summary>
	// Token: 0x02000617 RID: 1559
	[ComVisible(true)]
	[Serializable]
	public sealed class RegistryPermission : CodeAccessPermission, IBuiltInPermission, IUnrestrictedPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermission" /> class with either fully restricted or unrestricted permission as specified.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x06003B56 RID: 15190 RVA: 0x000CB9FC File Offset: 0x000C9BFC
		public RegistryPermission(PermissionState state)
		{
			this._state = CodeAccessPermission.CheckPermissionState(state, true);
			this.createList = new ArrayList();
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermission" /> class with the specified access to the specified registry variables.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values. </param>
		/// <param name="pathList">A list of registry variables (semicolon-separated) to which access is granted. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.-or- The <paramref name="pathList" /> parameter is not a valid string. </exception>
		// Token: 0x06003B57 RID: 15191 RVA: 0x000CBA40 File Offset: 0x000C9C40
		public RegistryPermission(RegistryPermissionAccess access, string pathList)
		{
			this._state = PermissionState.None;
			this.createList = new ArrayList();
			this.readList = new ArrayList();
			this.writeList = new ArrayList();
			this.AddPathList(access, pathList);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.RegistryPermission" /> class with the specified access to the specified registry variables and the specified access rights to registry control information.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values.</param>
		/// <param name="control">A bitwise combination of the <see cref="T:System.Security.AccessControl.AccessControlActions" />  values.</param>
		/// <param name="pathList">A list of registry variables (semicolon-separated) to which access is granted.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.-or- The <paramref name="pathList" /> parameter is not a valid string. </exception>
		// Token: 0x06003B58 RID: 15192 RVA: 0x000CBA84 File Offset: 0x000C9C84
		public RegistryPermission(RegistryPermissionAccess access, AccessControlActions control, string pathList)
		{
			if (!Enum.IsDefined(typeof(AccessControlActions), control))
			{
				string text = string.Format(Locale.GetText("Invalid enum {0}"), control);
				throw new ArgumentException(text, "AccessControlActions");
			}
			this._state = PermissionState.None;
			this.AddPathList(access, control, pathList);
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x000CBAE4 File Offset: 0x000C9CE4
		int IBuiltInPermission.GetTokenIndex()
		{
			return 5;
		}

		/// <summary>Adds access for the specified registry variables to the existing state of the permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values. </param>
		/// <param name="pathList">A list of registry variables (semicolon-separated). </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.-or- The <paramref name="pathList" /> parameter is not a valid string. </exception>
		// Token: 0x06003B5A RID: 15194 RVA: 0x000CBAE8 File Offset: 0x000C9CE8
		public void AddPathList(RegistryPermissionAccess access, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
				return;
			case RegistryPermissionAccess.Read:
				this.AddWithUnionKey(this.readList, pathList);
				return;
			case RegistryPermissionAccess.Write:
				this.AddWithUnionKey(this.writeList, pathList);
				return;
			case RegistryPermissionAccess.Create:
				this.AddWithUnionKey(this.createList, pathList);
				return;
			case RegistryPermissionAccess.AllAccess:
				this.AddWithUnionKey(this.createList, pathList);
				this.AddWithUnionKey(this.readList, pathList);
				this.AddWithUnionKey(this.writeList, pathList);
				return;
			}
			this.ThrowInvalidFlag(access, false);
		}

		/// <summary>Adds access for the specified registry variables to the existing state of the permission, specifying registry permission access and access control actions.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values. </param>
		/// <param name="control">One of the <see cref="T:System.Security.AccessControl.AccessControlActions" /> values. </param>
		/// <param name="pathList">A list of registry variables (separated by semicolons).</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.-or- The <paramref name="pathList" /> parameter is not a valid string. </exception>
		// Token: 0x06003B5B RID: 15195 RVA: 0x000CBBA8 File Offset: 0x000C9DA8
		[MonoTODO("(2.0) Access Control isn't implemented")]
		public void AddPathList(RegistryPermissionAccess access, AccessControlActions control, string pathList)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets paths for all registry variables with the specified <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.</summary>
		/// <returns>A list of the registry variables (semicolon-separated) with the specified <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.</returns>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values that represents a single type of registry variable access. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="access" /> is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.-or- <paramref name="access" /> is <see cref="F:System.Security.Permissions.RegistryPermissionAccess.AllAccess" />, which represents more than one type of registry variable access, or <see cref="F:System.Security.Permissions.RegistryPermissionAccess.NoAccess" />, which does not represent any type of registry variable access. </exception>
		// Token: 0x06003B5C RID: 15196 RVA: 0x000CBBB0 File Offset: 0x000C9DB0
		public string GetPathList(RegistryPermissionAccess access)
		{
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
			case RegistryPermissionAccess.AllAccess:
				this.ThrowInvalidFlag(access, true);
				goto IL_006E;
			case RegistryPermissionAccess.Read:
				return this.GetPathList(this.readList);
			case RegistryPermissionAccess.Write:
				return this.GetPathList(this.writeList);
			case RegistryPermissionAccess.Create:
				return this.GetPathList(this.createList);
			}
			this.ThrowInvalidFlag(access, false);
			IL_006E:
			return null;
		}

		/// <summary>Sets new access for the specified registry variable names to the existing state of the permission.</summary>
		/// <param name="access">One of the <see cref="T:System.Security.Permissions.RegistryPermissionAccess" /> values. </param>
		/// <param name="pathList">A list of registry variables (semicolon-separated). </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="access" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.-or- The <paramref name="pathList" /> parameter is not a valid string. </exception>
		// Token: 0x06003B5D RID: 15197 RVA: 0x000CBC2C File Offset: 0x000C9E2C
		public void SetPathList(RegistryPermissionAccess access, string pathList)
		{
			if (pathList == null)
			{
				throw new ArgumentNullException("pathList");
			}
			switch (access)
			{
			case RegistryPermissionAccess.NoAccess:
				return;
			case RegistryPermissionAccess.Read:
			{
				this.readList.Clear();
				string[] array = pathList.Split(new char[] { ';' });
				foreach (string text in array)
				{
					this.readList.Add(text);
				}
				return;
			}
			case RegistryPermissionAccess.Write:
			{
				this.writeList.Clear();
				string[] array = pathList.Split(new char[] { ';' });
				foreach (string text2 in array)
				{
					this.writeList.Add(text2);
				}
				return;
			}
			case RegistryPermissionAccess.Create:
			{
				this.createList.Clear();
				string[] array = pathList.Split(new char[] { ';' });
				foreach (string text3 in array)
				{
					this.createList.Add(text3);
				}
				return;
			}
			case RegistryPermissionAccess.AllAccess:
			{
				this.createList.Clear();
				this.readList.Clear();
				this.writeList.Clear();
				string[] array = pathList.Split(new char[] { ';' });
				foreach (string text4 in array)
				{
					this.createList.Add(text4);
					this.readList.Add(text4);
					this.writeList.Add(text4);
				}
				return;
			}
			}
			this.ThrowInvalidFlag(access, false);
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06003B5E RID: 15198 RVA: 0x000CBE00 File Offset: 0x000CA000
		public override IPermission Copy()
		{
			RegistryPermission registryPermission = new RegistryPermission(this._state);
			string text = this.GetPathList(RegistryPermissionAccess.Create);
			if (text != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Create, text);
			}
			text = this.GetPathList(RegistryPermissionAccess.Read);
			if (text != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Read, text);
			}
			text = this.GetPathList(RegistryPermissionAccess.Write);
			if (text != null)
			{
				registryPermission.SetPathList(RegistryPermissionAccess.Write, text);
			}
			return registryPermission;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.-or- The <paramref name="esd" /> parameter's version number is not valid. </exception>
		// Token: 0x06003B5F RID: 15199 RVA: 0x000CBE5C File Offset: 0x000CA05C
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			if (CodeAccessPermission.IsUnrestricted(esd))
			{
				this._state = PermissionState.Unrestricted;
			}
			string text = esd.Attribute("Create");
			if (text != null && text.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Create, text);
			}
			string text2 = esd.Attribute("Read");
			if (text2 != null && text2.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Read, text2);
			}
			string text3 = esd.Attribute("Write");
			if (text3 != null && text3.Length > 0)
			{
				this.SetPathList(RegistryPermissionAccess.Write, text3);
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is null if the intersection is empty.</returns>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003B60 RID: 15200 RVA: 0x000CBF0C File Offset: 0x000CA10C
		public override IPermission Intersect(IPermission target)
		{
			RegistryPermission registryPermission = this.Cast(target);
			if (registryPermission == null)
			{
				return null;
			}
			if (this.IsUnrestricted())
			{
				return registryPermission.Copy();
			}
			if (registryPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			RegistryPermission registryPermission2 = new RegistryPermission(PermissionState.None);
			this.IntersectKeys(this.createList, registryPermission.createList, registryPermission2.createList);
			this.IntersectKeys(this.readList, registryPermission.readList, registryPermission2.readList);
			this.IntersectKeys(this.writeList, registryPermission.writeList, registryPermission2.writeList);
			return (!registryPermission2.IsEmpty()) ? registryPermission2 : null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <returns>true if the current permission is a subset of the specified permission; otherwise, false.</returns>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003B61 RID: 15201 RVA: 0x000CBFB0 File Offset: 0x000CA1B0
		public override bool IsSubsetOf(IPermission target)
		{
			RegistryPermission registryPermission = this.Cast(target);
			if (registryPermission == null)
			{
				return false;
			}
			if (registryPermission.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (this.IsUnrestricted())
			{
				return registryPermission.IsUnrestricted();
			}
			return registryPermission.IsUnrestricted() || (this.KeyIsSubsetOf(this.createList, registryPermission.createList) && this.KeyIsSubsetOf(this.readList, registryPermission.readList) && this.KeyIsSubsetOf(this.writeList, registryPermission.writeList));
		}

		/// <summary>Returns a value indicating whether the current permission is unrestricted.</summary>
		/// <returns>true if the current permission is unrestricted; otherwise, false.</returns>
		// Token: 0x06003B62 RID: 15202 RVA: 0x000CC04C File Offset: 0x000CA24C
		public bool IsUnrestricted()
		{
			return this._state == PermissionState.Unrestricted;
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06003B63 RID: 15203 RVA: 0x000CC058 File Offset: 0x000CA258
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this._state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else
			{
				string text = this.GetPathList(RegistryPermissionAccess.Create);
				if (text != null)
				{
					securityElement.AddAttribute("Create", text);
				}
				text = this.GetPathList(RegistryPermissionAccess.Read);
				if (text != null)
				{
					securityElement.AddAttribute("Read", text);
				}
				text = this.GetPathList(RegistryPermissionAccess.Write);
				if (text != null)
				{
					securityElement.AddAttribute("Write", text);
				}
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <param name="other">A permission to combine with the current permission. It must be of the same type as the current permission. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="other" /> parameter is not null and is not of the same type as the current permission. </exception>
		// Token: 0x06003B64 RID: 15204 RVA: 0x000CC0E0 File Offset: 0x000CA2E0
		public override IPermission Union(IPermission other)
		{
			RegistryPermission registryPermission = this.Cast(other);
			if (registryPermission == null)
			{
				return this.Copy();
			}
			if (this.IsUnrestricted() || registryPermission.IsUnrestricted())
			{
				return new RegistryPermission(PermissionState.Unrestricted);
			}
			if (this.IsEmpty() && registryPermission.IsEmpty())
			{
				return null;
			}
			RegistryPermission registryPermission2 = (RegistryPermission)this.Copy();
			string text = registryPermission.GetPathList(RegistryPermissionAccess.Create);
			if (text != null)
			{
				registryPermission2.AddPathList(RegistryPermissionAccess.Create, text);
			}
			text = registryPermission.GetPathList(RegistryPermissionAccess.Read);
			if (text != null)
			{
				registryPermission2.AddPathList(RegistryPermissionAccess.Read, text);
			}
			text = registryPermission.GetPathList(RegistryPermissionAccess.Write);
			if (text != null)
			{
				registryPermission2.AddPathList(RegistryPermissionAccess.Write, text);
			}
			return registryPermission2;
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x000CC188 File Offset: 0x000CA388
		private bool IsEmpty()
		{
			return this._state == PermissionState.None && this.createList.Count == 0 && this.readList.Count == 0 && this.writeList.Count == 0;
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x000CC1D4 File Offset: 0x000CA3D4
		private RegistryPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			RegistryPermission registryPermission = target as RegistryPermission;
			if (registryPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(RegistryPermission));
			}
			return registryPermission;
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x000CC208 File Offset: 0x000CA408
		internal void ThrowInvalidFlag(RegistryPermissionAccess flag, bool context)
		{
			string text;
			if (context)
			{
				text = Locale.GetText("Unknown flag '{0}'.");
			}
			else
			{
				text = Locale.GetText("Invalid flag '{0}' in this context.");
			}
			throw new ArgumentException(string.Format(text, flag), "flag");
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x000CC250 File Offset: 0x000CA450
		private string GetPathList(ArrayList list)
		{
			if (this.IsUnrestricted())
			{
				return string.Empty;
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in list)
			{
				string text = (string)obj;
				stringBuilder.Append(text);
				stringBuilder.Append(";");
			}
			string text2 = stringBuilder.ToString();
			int length = text2.Length;
			if (length > 0)
			{
				return text2.Substring(0, length - 1);
			}
			return string.Empty;
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x000CC31C File Offset: 0x000CA51C
		internal bool KeyIsSubsetOf(IList local, IList target)
		{
			bool flag = false;
			foreach (object obj in local)
			{
				string text = (string)obj;
				foreach (object obj2 in target)
				{
					string text2 = (string)obj2;
					if (text.StartsWith(text2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x000CC404 File Offset: 0x000CA604
		internal void AddWithUnionKey(IList list, string pathList)
		{
			string[] array = pathList.Split(new char[] { ';' });
			foreach (string text in array)
			{
				int count = list.Count;
				if (count == 0)
				{
					list.Add(text);
				}
				else
				{
					for (int j = 0; j < count; j++)
					{
						string text2 = (string)list[j];
						if (text2.StartsWith(text))
						{
							list[j] = text;
						}
						else if (!text.StartsWith(text2))
						{
							list.Add(text);
						}
					}
				}
			}
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x000CC4B4 File Offset: 0x000CA6B4
		internal void IntersectKeys(IList local, IList target, IList result)
		{
			foreach (object obj in local)
			{
				string text = (string)obj;
				foreach (object obj2 in target)
				{
					string text2 = (string)obj2;
					if (text2.Length > text.Length)
					{
						if (text2.StartsWith(text))
						{
							result.Add(text2);
						}
					}
					else if (text.StartsWith(text2))
					{
						result.Add(text);
					}
				}
			}
		}

		// Token: 0x040019D1 RID: 6609
		private const int version = 1;

		// Token: 0x040019D2 RID: 6610
		private PermissionState _state;

		// Token: 0x040019D3 RID: 6611
		private ArrayList createList;

		// Token: 0x040019D4 RID: 6612
		private ArrayList readList;

		// Token: 0x040019D5 RID: 6613
		private ArrayList writeList;
	}
}
