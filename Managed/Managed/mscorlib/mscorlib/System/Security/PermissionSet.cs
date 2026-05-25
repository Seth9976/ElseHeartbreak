using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;

namespace System.Security
{
	/// <summary>Represents a collection that can contain many different types of permissions.</summary>
	// Token: 0x0200053D RID: 1341
	[MonoTODO("CAS support is experimental (and unsupported).")]
	[ComVisible(true)]
	[PermissionSet(SecurityAction.InheritanceDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                PublicKeyBlob=\"002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293\"/>\n</PermissionSet>\n")]
	[Serializable]
	public class PermissionSet : IEnumerable, ICollection, IDeserializationCallback, ISecurityEncodable, IStackWalk
	{
		// Token: 0x060034AE RID: 13486 RVA: 0x000ACAC0 File Offset: 0x000AACC0
		internal PermissionSet()
		{
			this.list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.PermissionSet" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid <see cref="T:System.Security.Permissions.PermissionState" />. </exception>
		// Token: 0x060034AF RID: 13487 RVA: 0x000ACAD4 File Offset: 0x000AACD4
		public PermissionSet(PermissionState state)
			: this()
		{
			this.state = CodeAccessPermission.CheckPermissionState(state, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.PermissionSet" /> class with initial values taken from the <paramref name="permSet" /> parameter.</summary>
		/// <param name="permSet">The <see cref="T:System.Security.PermissionSet" /> from which to take the value of the new <see cref="T:System.Security.PermissionSet" />, or null to create an empty <see cref="T:System.Security.PermissionSet" />. </param>
		// Token: 0x060034B0 RID: 13488 RVA: 0x000ACAEC File Offset: 0x000AACEC
		public PermissionSet(PermissionSet permSet)
			: this()
		{
			if (permSet != null)
			{
				this.state = permSet.state;
				foreach (object obj in permSet.list)
				{
					IPermission permission = (IPermission)obj;
					this.list.Add(permission);
				}
			}
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000ACB7C File Offset: 0x000AAD7C
		internal PermissionSet(string xml)
			: this()
		{
			this.state = PermissionState.None;
			if (xml != null)
			{
				SecurityElement securityElement = SecurityElement.FromString(xml);
				this.FromXml(securityElement);
			}
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000ACBAC File Offset: 0x000AADAC
		internal PermissionSet(IPermission perm)
			: this()
		{
			if (perm != null)
			{
				this.list.Add(perm);
			}
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		// Token: 0x060034B4 RID: 13492 RVA: 0x000ACC00 File Offset: 0x000AAE00
		[MonoTODO("may not be required")]
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		/// <summary>Adds a specified permission to the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>The union of the permission added and any permission of the same type that already exists in the <see cref="T:System.Security.PermissionSet" />.</returns>
		/// <param name="perm">The permission to add. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060034B5 RID: 13493 RVA: 0x000ACC04 File Offset: 0x000AAE04
		public IPermission AddPermission(IPermission perm)
		{
			if (perm == null || this._readOnly)
			{
				return perm;
			}
			if (this.state == PermissionState.Unrestricted)
			{
				return (IPermission)Activator.CreateInstance(perm.GetType(), PermissionSet.psUnrestricted);
			}
			IPermission permission = this.RemovePermission(perm.GetType());
			if (permission != null)
			{
				perm = perm.Union(permission);
			}
			this.list.Add(perm);
			return perm;
		}

		/// <summary>Declares that the calling code can access the resource protected by a permission demand through the code that calls this method, even if callers higher in the stack have not been granted permission to access the resource. Using <see cref="M:System.Security.PermissionSet.Assert" /> can create security vulnerabilities.</summary>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Security.PermissionSet" /> instance asserted has not been granted to the asserting code.-or- There is already an active <see cref="M:System.Security.PermissionSet.Assert" /> for the current frame. </exception>
		// Token: 0x060034B6 RID: 13494 RVA: 0x000ACC70 File Offset: 0x000AAE70
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"Assertion\"/>\n</PermissionSet>\n")]
		public void Assert()
		{
			int num = this.Count;
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				if (permission is IStackWalk)
				{
					if (!SecurityManager.IsGranted(permission))
					{
						return;
					}
				}
				else
				{
					num--;
				}
			}
			if (SecurityManager.SecurityEnabled && num > 0)
			{
				throw new NotSupportedException("Currently only declarative Assert are supported.");
			}
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000ACD20 File Offset: 0x000AAF20
		internal void Clear()
		{
			this.list.Clear();
		}

		/// <summary>Creates a copy of the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x060034B8 RID: 13496 RVA: 0x000ACD30 File Offset: 0x000AAF30
		public virtual PermissionSet Copy()
		{
			return new PermissionSet(this);
		}

		/// <summary>Copies the permission objects of the set to the indicated location in an <see cref="T:System.Array" />.</summary>
		/// <param name="array">The target array to which to copy. </param>
		/// <param name="index">The starting position in the array to begin copying (zero based). </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="array" /> parameter has more than one dimension. </exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is out of the range of the <paramref name="array" /> parameter. </exception>
		// Token: 0x060034B9 RID: 13497 RVA: 0x000ACD38 File Offset: 0x000AAF38
		public virtual void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (this.list.Count > 0)
			{
				if (array.Rank > 1)
				{
					throw new ArgumentException(Locale.GetText("Array has more than one dimension"));
				}
				if (index < 0 || index >= array.Length)
				{
					throw new IndexOutOfRangeException("index");
				}
				this.list.CopyTo(array, index);
			}
		}

		/// <summary>Forces a <see cref="T:System.Security.SecurityException" /> at run time if all callers higher in the call stack have not been granted the permissions specified by the current instance.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have the permission demanded. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x060034BA RID: 13498 RVA: 0x000ACDB0 File Offset: 0x000AAFB0
		public void Demand()
		{
		}

		// Token: 0x060034BB RID: 13499 RVA: 0x000ACDB4 File Offset: 0x000AAFB4
		internal void CasOnlyDemand(int skip)
		{
			Assembly assembly = null;
			AppDomain appDomain = null;
			if (this._ignored == null)
			{
				this._ignored = new bool[this.list.Count];
			}
			ArrayList stack = SecurityFrame.GetStack(skip);
			if (stack != null && stack.Count > 0)
			{
				SecurityFrame securityFrame = (SecurityFrame)stack[0];
				assembly = securityFrame.Assembly;
				appDomain = securityFrame.Domain;
				foreach (object obj in stack)
				{
					SecurityFrame securityFrame2 = (SecurityFrame)obj;
					if (this.ProcessFrame(securityFrame2, ref assembly, ref appDomain) && this.AllIgnored())
					{
						return;
					}
				}
				SecurityFrame securityFrame3 = (SecurityFrame)stack[stack.Count - 1];
				this.CheckAssembly(assembly, securityFrame3);
				this.CheckAppDomain(appDomain, securityFrame3);
			}
			CompressedStack compressedStack = Thread.CurrentThread.GetCompressedStack();
			if (compressedStack != null && !compressedStack.IsEmpty())
			{
				foreach (object obj2 in compressedStack.List)
				{
					SecurityFrame securityFrame4 = (SecurityFrame)obj2;
					if (this.ProcessFrame(securityFrame4, ref assembly, ref appDomain) && this.AllIgnored())
					{
						break;
					}
				}
			}
		}

		/// <summary>Causes any <see cref="M:System.Security.PermissionSet.Demand" /> that passes through the calling code for a permission that has an intersection with a permission of a type contained in the current <see cref="T:System.Security.PermissionSet" /> to fail.</summary>
		/// <exception cref="T:System.Security.SecurityException">A previous call to <see cref="M:System.Security.PermissionSet.Deny" /> has already restricted the permissions for the current stack frame. </exception>
		// Token: 0x060034BC RID: 13500 RVA: 0x000ACF64 File Offset: 0x000AB164
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public void Deny()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				if (permission is IStackWalk)
				{
					throw new NotSupportedException("Currently only declarative Deny are supported.");
				}
			}
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="et">The XML encoding to use to reconstruct the security object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="et" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="et" /> parameter is not a valid permission element.-or- The <paramref name="et" /> parameter's version number is not supported. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060034BD RID: 13501 RVA: 0x000ACFF0 File Offset: 0x000AB1F0
		public virtual void FromXml(SecurityElement et)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (et.Tag != "PermissionSet")
			{
				string text = string.Format("Invalid tag {0} expected {1}", et.Tag, "PermissionSet");
				throw new ArgumentException(text, "et");
			}
			this.list.Clear();
			if (CodeAccessPermission.IsUnrestricted(et))
			{
				this.state = PermissionState.Unrestricted;
				return;
			}
			this.state = PermissionState.None;
			if (et.Children != null)
			{
				foreach (object obj in et.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					string text2 = securityElement.Attribute("class");
					if (text2 == null)
					{
						throw new ArgumentException(Locale.GetText("No permission class is specified."));
					}
					if (this.Resolver != null)
					{
						text2 = this.Resolver.ResolveClassName(text2);
					}
					this.list.Add(PermissionBuilder.Create(text2, securityElement));
				}
			}
		}

		/// <summary>Returns an enumerator for the permissions of the set.</summary>
		/// <returns>An enumerator object for the permissions of the set.</returns>
		// Token: 0x060034BE RID: 13502 RVA: 0x000AD120 File Offset: 0x000AB320
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Determines whether the current <see cref="T:System.Security.PermissionSet" /> is a subset of the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>true if the current <see cref="T:System.Security.PermissionSet" /> is a subset of the <paramref name="target" /> parameter; otherwise, false.</returns>
		/// <param name="target">A <see cref="T:System.Security.PermissionSet" /> to test for the subset relationship. This must be either a <see cref="T:System.Security.PermissionSet" /> or a <see cref="T:System.Security.NamedPermissionSet" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060034BF RID: 13503 RVA: 0x000AD130 File Offset: 0x000AB330
		public bool IsSubsetOf(PermissionSet target)
		{
			if (target == null || target.IsEmpty())
			{
				return this.IsEmpty();
			}
			if (target.IsUnrestricted())
			{
				return true;
			}
			if (this.IsUnrestricted())
			{
				return false;
			}
			if (this.IsUnrestricted() && (target == null || !target.IsUnrestricted()))
			{
				return false;
			}
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				Type type = permission.GetType();
				IPermission permission2;
				if (target.IsUnrestricted() && permission is CodeAccessPermission && permission is IUnrestrictedPermission)
				{
					permission2 = (IPermission)Activator.CreateInstance(type, PermissionSet.psUnrestricted);
				}
				else
				{
					permission2 = target.GetPermission(type);
				}
				if (!permission.IsSubsetOf(permission2))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Causes any <see cref="M:System.Security.PermissionSet.Demand" /> that passes through the calling code for any <see cref="T:System.Security.PermissionSet" /> that is not a subset of the current <see cref="T:System.Security.PermissionSet" /> to fail.</summary>
		// Token: 0x060034C0 RID: 13504 RVA: 0x000AD24C File Offset: 0x000AB44C
		[MonoTODO("CAS support is experimental (and unsupported). Imperative mode is not implemented.")]
		public void PermitOnly()
		{
			if (!SecurityManager.SecurityEnabled)
			{
				return;
			}
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				if (permission is IStackWalk)
				{
					throw new NotSupportedException("Currently only declarative Deny are supported.");
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Security.PermissionSet" /> contains permissions that are not derived from <see cref="T:System.Security.CodeAccessPermission" />.</summary>
		/// <returns>true if the <see cref="T:System.Security.PermissionSet" /> contains permissions that are not derived from <see cref="T:System.Security.CodeAccessPermission" />; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060034C1 RID: 13505 RVA: 0x000AD2D8 File Offset: 0x000AB4D8
		public bool ContainsNonCodeAccessPermissions()
		{
			if (this.list.Count > 0)
			{
				foreach (object obj in this.list)
				{
					IPermission permission = (IPermission)obj;
					if (!permission.GetType().IsSubclassOf(typeof(CodeAccessPermission)))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		/// <summary>Converts an encoded <see cref="T:System.Security.PermissionSet" /> from one XML encoding format to another XML encoding format.</summary>
		/// <returns>An encrypted <see cref="T:System.Security.PermissionSet" /> with the specified output format.</returns>
		/// <param name="inFormat">A string representing one of the following encoding formats: ASCII, Unicode, or Binary. Possible values are "XMLASCII" or "XML", "XMLUNICODE", and "BINARY".</param>
		/// <param name="inData">An XML-encoded permission set.</param>
		/// <param name="outFormat">A string representing one of the following encoding formats: ASCII, Unicode, or Binary. Possible values are "XMLASCII" or "XML", "XMLUNICODE", and "BINARY".</param>
		/// <exception cref="T:System.NotImplementedException">In all cases.</exception>
		// Token: 0x060034C2 RID: 13506 RVA: 0x000AD378 File Offset: 0x000AB578
		public static byte[] ConvertPermissionSet(string inFormat, byte[] inData, string outFormat)
		{
			if (inFormat == null)
			{
				throw new ArgumentNullException("inFormat");
			}
			if (outFormat == null)
			{
				throw new ArgumentNullException("outFormat");
			}
			if (inData == null)
			{
				return null;
			}
			if (inFormat == outFormat)
			{
				return inData;
			}
			PermissionSet permissionSet = null;
			if (inFormat == "BINARY")
			{
				if (outFormat.StartsWith("XML"))
				{
					using (MemoryStream memoryStream = new MemoryStream(inData))
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						permissionSet = (PermissionSet)binaryFormatter.Deserialize(memoryStream);
						memoryStream.Close();
					}
					string text = permissionSet.ToString();
					if (outFormat != null)
					{
						if (PermissionSet.<>f__switch$map2B == null)
						{
							PermissionSet.<>f__switch$map2B = new Dictionary<string, int>(3)
							{
								{ "XML", 0 },
								{ "XMLASCII", 0 },
								{ "XMLUNICODE", 1 }
							};
						}
						int num;
						if (PermissionSet.<>f__switch$map2B.TryGetValue(outFormat, out num))
						{
							if (num == 0)
							{
								return Encoding.ASCII.GetBytes(text);
							}
							if (num == 1)
							{
								return Encoding.Unicode.GetBytes(text);
							}
						}
					}
				}
			}
			else
			{
				if (!inFormat.StartsWith("XML"))
				{
					return null;
				}
				if (outFormat == "BINARY")
				{
					string text2 = null;
					if (inFormat != null)
					{
						if (PermissionSet.<>f__switch$map2C == null)
						{
							PermissionSet.<>f__switch$map2C = new Dictionary<string, int>(3)
							{
								{ "XML", 0 },
								{ "XMLASCII", 0 },
								{ "XMLUNICODE", 1 }
							};
						}
						int num;
						if (PermissionSet.<>f__switch$map2C.TryGetValue(inFormat, out num))
						{
							if (num != 0)
							{
								if (num == 1)
								{
									text2 = Encoding.Unicode.GetString(inData);
								}
							}
							else
							{
								text2 = Encoding.ASCII.GetString(inData);
							}
						}
					}
					if (text2 != null)
					{
						permissionSet = new PermissionSet(PermissionState.None);
						permissionSet.FromXml(SecurityElement.FromString(text2));
						MemoryStream memoryStream2 = new MemoryStream();
						BinaryFormatter binaryFormatter2 = new BinaryFormatter();
						binaryFormatter2.Serialize(memoryStream2, permissionSet);
						memoryStream2.Close();
						return memoryStream2.ToArray();
					}
				}
				else if (outFormat.StartsWith("XML"))
				{
					string text3 = string.Format(Locale.GetText("Can't convert from {0} to {1}"), inFormat, outFormat);
					throw new XmlSyntaxException(text3);
				}
			}
			throw new SerializationException(string.Format(Locale.GetText("Unknown output format {0}."), outFormat));
		}

		/// <summary>Gets a permission object of the specified type, if it exists in the set.</summary>
		/// <returns>A copy of the permission object of the type specified by the <paramref name="permClass" /> parameter contained in the <see cref="T:System.Security.PermissionSet" />, or null if none exists.</returns>
		/// <param name="permClass">The <see cref="T:System.Type" /> of the desired permission object. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060034C3 RID: 13507 RVA: 0x000AD608 File Offset: 0x000AB808
		public IPermission GetPermission(Type permClass)
		{
			if (permClass == null || this.list.Count == 0)
			{
				return null;
			}
			foreach (object obj in this.list)
			{
				if (obj != null && obj.GetType().Equals(permClass))
				{
					return (IPermission)obj;
				}
			}
			return null;
		}

		/// <summary>Creates and returns a permission set that is the intersection of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A new <see cref="T:System.Security.PermissionSet" /> that represents the intersection of the current <see cref="T:System.Security.PermissionSet" /> and the specified target. This object is null if the intersection is empty.</returns>
		/// <param name="other">A <see cref="T:System.Security.PermissionSet" /> to intersect with the current <see cref="T:System.Security.PermissionSet" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060034C4 RID: 13508 RVA: 0x000AD6AC File Offset: 0x000AB8AC
		public PermissionSet Intersect(PermissionSet other)
		{
			if (other == null || other.IsEmpty() || this.IsEmpty())
			{
				return null;
			}
			PermissionState permissionState = PermissionState.None;
			if (this.IsUnrestricted() && other.IsUnrestricted())
			{
				permissionState = PermissionState.Unrestricted;
			}
			PermissionSet permissionSet;
			if (permissionState == PermissionState.Unrestricted)
			{
				permissionSet = new PermissionSet(permissionState);
			}
			else if (this.IsUnrestricted())
			{
				permissionSet = other.Copy();
			}
			else if (other.IsUnrestricted())
			{
				permissionSet = this.Copy();
			}
			else
			{
				permissionSet = new PermissionSet(permissionState);
				this.InternalIntersect(permissionSet, this, other, false);
			}
			return permissionSet;
		}

		// Token: 0x060034C5 RID: 13509 RVA: 0x000AD748 File Offset: 0x000AB948
		internal void InternalIntersect(PermissionSet intersect, PermissionSet a, PermissionSet b, bool unrestricted)
		{
			foreach (object obj in b.list)
			{
				IPermission permission = (IPermission)obj;
				IPermission permission2 = a.GetPermission(permission.GetType());
				if (permission2 != null)
				{
					intersect.AddPermission(permission.Intersect(permission2));
				}
				else if (unrestricted)
				{
					intersect.AddPermission(permission);
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Security.PermissionSet" /> is empty.</summary>
		/// <returns>true if the <see cref="T:System.Security.PermissionSet" /> is empty; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060034C6 RID: 13510 RVA: 0x000AD7E8 File Offset: 0x000AB9E8
		public bool IsEmpty()
		{
			if (this.state == PermissionState.Unrestricted)
			{
				return false;
			}
			if (this.list == null || this.list.Count == 0)
			{
				return true;
			}
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				if (!permission.IsSubsetOf(null))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Determines whether the <see cref="T:System.Security.PermissionSet" /> is Unrestricted.</summary>
		/// <returns>true if the <see cref="T:System.Security.PermissionSet" /> is Unrestricted; otherwise, false.</returns>
		// Token: 0x060034C7 RID: 13511 RVA: 0x000AD894 File Offset: 0x000ABA94
		public bool IsUnrestricted()
		{
			return this.state == PermissionState.Unrestricted;
		}

		/// <summary>Removes a permission of a certain type from the set.</summary>
		/// <returns>The permission removed from the set.</returns>
		/// <param name="permClass">The <see cref="T:System.Type" /> of permission to delete. </param>
		// Token: 0x060034C8 RID: 13512 RVA: 0x000AD8A0 File Offset: 0x000ABAA0
		public IPermission RemovePermission(Type permClass)
		{
			if (permClass == null || this._readOnly)
			{
				return null;
			}
			foreach (object obj in this.list)
			{
				if (obj.GetType().Equals(permClass))
				{
					this.list.Remove(obj);
					return (IPermission)obj;
				}
			}
			return null;
		}

		/// <summary>Sets a permission to the <see cref="T:System.Security.PermissionSet" />, replacing any existing permission of the same type.</summary>
		/// <returns>The set permission.</returns>
		/// <param name="perm">The permission to set. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060034C9 RID: 13513 RVA: 0x000AD944 File Offset: 0x000ABB44
		public IPermission SetPermission(IPermission perm)
		{
			if (perm == null || this._readOnly)
			{
				return perm;
			}
			IUnrestrictedPermission unrestrictedPermission = perm as IUnrestrictedPermission;
			if (unrestrictedPermission == null)
			{
				this.state = PermissionState.None;
			}
			else
			{
				this.state = ((!unrestrictedPermission.IsUnrestricted()) ? PermissionState.None : this.state);
			}
			this.RemovePermission(perm.GetType());
			this.list.Add(perm);
			return perm;
		}

		/// <summary>Returns a string representation of the <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A representation of the <see cref="T:System.Security.PermissionSet" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060034CA RID: 13514 RVA: 0x000AD9B8 File Offset: 0x000ABBB8
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060034CB RID: 13515 RVA: 0x000AD9C8 File Offset: 0x000ABBC8
		public virtual SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("PermissionSet");
			securityElement.AddAttribute("class", base.GetType().FullName);
			securityElement.AddAttribute("version", 1.ToString());
			if (this.state == PermissionState.Unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			foreach (object obj in this.list)
			{
				IPermission permission = (IPermission)obj;
				securityElement.AddChild(permission.ToXml());
			}
			return securityElement;
		}

		/// <summary>Creates a <see cref="T:System.Security.PermissionSet" /> that is the union of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>A new <see cref="T:System.Security.PermissionSet" /> that represents the union of the current <see cref="T:System.Security.PermissionSet" /> and the specified <see cref="T:System.Security.PermissionSet" />.</returns>
		/// <param name="other">A <see cref="T:System.Security.PermissionSet" /> to form the union with the current <see cref="T:System.Security.PermissionSet" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060034CC RID: 13516 RVA: 0x000ADA94 File Offset: 0x000ABC94
		public PermissionSet Union(PermissionSet other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			PermissionSet permissionSet = null;
			if (this.IsUnrestricted() || other.IsUnrestricted())
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			permissionSet = this.Copy();
			foreach (object obj in other.list)
			{
				IPermission permission = (IPermission)obj;
				permissionSet.AddPermission(permission);
			}
			return permissionSet;
		}

		/// <summary>Gets the number of permission objects contained in the permission set.</summary>
		/// <returns>The number of permission objects contained in the <see cref="T:System.Security.PermissionSet" />.</returns>
		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000ADB38 File Offset: 0x000ABD38
		public virtual int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is guaranteed to be thread safe.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x060034CE RID: 13518 RVA: 0x000ADB48 File Offset: 0x000ABD48
		public virtual bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>Always false.</returns>
		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x000ADB58 File Offset: 0x000ABD58
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the root object of the current collection.</summary>
		/// <returns>The root object of the current collection.</returns>
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x060034D0 RID: 13520 RVA: 0x000ADB5C File Offset: 0x000ABD5C
		public virtual object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x060034D1 RID: 13521 RVA: 0x000ADB60 File Offset: 0x000ABD60
		// (set) Token: 0x060034D2 RID: 13522 RVA: 0x000ADB68 File Offset: 0x000ABD68
		internal bool DeclarativeSecurity
		{
			get
			{
				return this._declsec;
			}
			set
			{
				this._declsec = value;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.PermissionSet" /> or <see cref="T:System.Security.NamedPermissionSet" /> object is equal to the current <see cref="T:System.Security.PermissionSet" />.</summary>
		/// <returns>true if the specified object is equal to the current <see cref="T:System.Security.PermissionSet" /> object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current <see cref="T:System.Security.PermissionSet" />. </param>
		// Token: 0x060034D3 RID: 13523 RVA: 0x000ADB74 File Offset: 0x000ABD74
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			PermissionSet permissionSet = obj as PermissionSet;
			if (permissionSet == null)
			{
				return false;
			}
			if (this.state != permissionSet.state)
			{
				return false;
			}
			if (this.list.Count != permissionSet.Count)
			{
				return false;
			}
			for (int i = 0; i < this.list.Count; i++)
			{
				bool flag = false;
				int num = 0;
				while (i < permissionSet.list.Count)
				{
					if (this.list[i].Equals(permissionSet.list[num]))
					{
						flag = true;
						break;
					}
					num++;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.PermissionSet" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.PermissionSet" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060034D4 RID: 13524 RVA: 0x000ADC30 File Offset: 0x000ABE30
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return (this.list.Count != 0) ? base.GetHashCode() : ((int)this.state);
		}

		/// <summary>Causes any previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame to be removed and no longer be in effect.</summary>
		/// <exception cref="T:System.ExecutionEngineException">There is no previous <see cref="M:System.Security.CodeAccessPermission.Assert" /> for the current frame. </exception>
		// Token: 0x060034D5 RID: 13525 RVA: 0x000ADC54 File Offset: 0x000ABE54
		public static void RevertAssert()
		{
			CodeAccessPermission.RevertAssert();
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060034D6 RID: 13526 RVA: 0x000ADC5C File Offset: 0x000ABE5C
		// (set) Token: 0x060034D7 RID: 13527 RVA: 0x000ADC64 File Offset: 0x000ABE64
		internal PolicyLevel Resolver
		{
			get
			{
				return this._policyLevel;
			}
			set
			{
				this._policyLevel = value;
			}
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000ADC70 File Offset: 0x000ABE70
		internal void SetReadOnly(bool value)
		{
			this._readOnly = value;
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000ADC7C File Offset: 0x000ABE7C
		private bool AllIgnored()
		{
			if (this._ignored == null)
			{
				throw new NotSupportedException("bad bad bad");
			}
			for (int i = 0; i < this._ignored.Length; i++)
			{
				if (!this._ignored[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000ADCC8 File Offset: 0x000ABEC8
		internal bool ProcessFrame(SecurityFrame frame, ref Assembly current, ref AppDomain domain)
		{
			if (this.IsUnrestricted())
			{
				if (frame.Deny != null)
				{
					CodeAccessPermission.ThrowSecurityException(this, "Deny", frame, SecurityAction.Demand, null);
				}
				else if (frame.PermitOnly != null && !frame.PermitOnly.IsUnrestricted())
				{
					CodeAccessPermission.ThrowSecurityException(this, "PermitOnly", frame, SecurityAction.Demand, null);
				}
			}
			if (frame.HasStackModifiers)
			{
				for (int i = 0; i < this.list.Count; i++)
				{
					CodeAccessPermission codeAccessPermission = (CodeAccessPermission)this.list[i];
					if (codeAccessPermission.ProcessFrame(frame))
					{
						this._ignored[i] = true;
						if (this.AllIgnored())
						{
							return true;
						}
					}
				}
			}
			if (frame.Assembly != current)
			{
				this.CheckAssembly(current, frame);
				current = frame.Assembly;
			}
			if (frame.Domain != domain)
			{
				this.CheckAppDomain(domain, frame);
				domain = frame.Domain;
			}
			return false;
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000ADDC8 File Offset: 0x000ABFC8
		internal void CheckAssembly(Assembly a, SecurityFrame frame)
		{
			IPermission permission = SecurityManager.CheckPermissionSet(a, this, false);
			if (permission != null)
			{
				CodeAccessPermission.ThrowSecurityException(this, "Demand failed assembly permissions checks.", frame, SecurityAction.Demand, permission);
			}
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000ADDF4 File Offset: 0x000ABFF4
		internal void CheckAppDomain(AppDomain domain, SecurityFrame frame)
		{
			IPermission permission = SecurityManager.CheckPermissionSet(domain, this);
			if (permission != null)
			{
				CodeAccessPermission.ThrowSecurityException(this, "Demand failed appdomain permissions checks.", frame, SecurityAction.Demand, permission);
			}
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000ADE20 File Offset: 0x000AC020
		internal static PermissionSet CreateFromBinaryFormat(byte[] data)
		{
			if (data == null || data[0] != 46 || data.Length < 2)
			{
				string text = Locale.GetText("Invalid data in 2.0 metadata format.");
				throw new SecurityException(text);
			}
			int num = 1;
			int num2 = PermissionSet.ReadEncodedInt(data, ref num);
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			for (int i = 0; i < num2; i++)
			{
				IPermission permission = PermissionSet.ProcessAttribute(data, ref num);
				if (permission == null)
				{
					string text2 = Locale.GetText("Unsupported data found in 2.0 metadata format.");
					throw new SecurityException(text2);
				}
				permissionSet.AddPermission(permission);
			}
			return permissionSet;
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x000ADEB0 File Offset: 0x000AC0B0
		internal static int ReadEncodedInt(byte[] data, ref int position)
		{
			int num;
			if ((data[position] & 128) == 0)
			{
				num = (int)data[position];
				position++;
			}
			else if ((data[position] & 64) == 0)
			{
				num = ((int)(data[position] & 63) << 8) | (int)data[position + 1];
				position += 2;
			}
			else
			{
				num = ((int)(data[position] & 31) << 24) | ((int)data[position + 1] << 16) | ((int)data[position + 2] << 8) | (int)data[position + 3];
				position += 4;
			}
			return num;
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x000ADF34 File Offset: 0x000AC134
		internal static IPermission ProcessAttribute(byte[] data, ref int position)
		{
			int num = PermissionSet.ReadEncodedInt(data, ref position);
			string @string = Encoding.UTF8.GetString(data, position, num);
			position += num;
			Type type = Type.GetType(@string);
			SecurityAttribute securityAttribute = Activator.CreateInstance(type, PermissionSet.action) as SecurityAttribute;
			if (securityAttribute == null)
			{
				return null;
			}
			PermissionSet.ReadEncodedInt(data, ref position);
			int num2 = PermissionSet.ReadEncodedInt(data, ref position);
			for (int i = 0; i < num2; i++)
			{
				byte b = data[position++];
				bool flag;
				if (b != 83)
				{
					if (b != 84)
					{
						return null;
					}
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = false;
				byte b2 = data[position++];
				if (b2 == 29)
				{
					flag2 = true;
					b2 = data[position++];
				}
				int num3 = PermissionSet.ReadEncodedInt(data, ref position);
				string string2 = Encoding.UTF8.GetString(data, position, num3);
				position += num3;
				int num4 = 1;
				if (flag2)
				{
					num4 = BitConverter.ToInt32(data, position);
					position += 4;
				}
				object[] array = null;
				for (int j = 0; j < num4; j++)
				{
					if (flag2)
					{
					}
					b = b2;
					object obj;
					switch (b)
					{
					case 2:
						obj = Convert.ToBoolean(data[position++]);
						break;
					case 3:
						obj = Convert.ToChar(data[position]);
						position += 2;
						break;
					case 4:
						obj = Convert.ToSByte(data[position++]);
						break;
					case 5:
						obj = Convert.ToByte(data[position++]);
						break;
					case 6:
						obj = Convert.ToInt16(data[position]);
						position += 2;
						break;
					case 7:
						obj = Convert.ToUInt16(data[position]);
						position += 2;
						break;
					case 8:
						obj = Convert.ToInt32(data[position]);
						position += 4;
						break;
					case 9:
						obj = Convert.ToUInt32(data[position]);
						position += 4;
						break;
					case 10:
						obj = Convert.ToInt64(data[position]);
						position += 8;
						break;
					case 11:
						obj = Convert.ToUInt64(data[position]);
						position += 8;
						break;
					case 12:
						obj = Convert.ToSingle(data[position]);
						position += 4;
						break;
					case 13:
						obj = Convert.ToDouble(data[position]);
						position += 8;
						break;
					case 14:
					{
						string text = null;
						if (data[position] != 255)
						{
							int num5 = PermissionSet.ReadEncodedInt(data, ref position);
							text = Encoding.UTF8.GetString(data, position, num5);
							position += num5;
						}
						else
						{
							position++;
						}
						obj = text;
						break;
					}
					default:
					{
						if (b != 80)
						{
							return null;
						}
						int num6 = PermissionSet.ReadEncodedInt(data, ref position);
						obj = Type.GetType(Encoding.UTF8.GetString(data, position, num6));
						position += num6;
						break;
					}
					}
					if (flag)
					{
						PropertyInfo property = type.GetProperty(string2);
						property.SetValue(securityAttribute, obj, array);
					}
					else
					{
						FieldInfo field = type.GetField(string2);
						field.SetValue(securityAttribute, obj);
					}
				}
			}
			return securityAttribute.CreatePermission();
		}

		// Token: 0x04001622 RID: 5666
		private const string tagName = "PermissionSet";

		// Token: 0x04001623 RID: 5667
		private const int version = 1;

		// Token: 0x04001624 RID: 5668
		private static object[] psUnrestricted = new object[] { PermissionState.Unrestricted };

		// Token: 0x04001625 RID: 5669
		private PermissionState state;

		// Token: 0x04001626 RID: 5670
		private ArrayList list;

		// Token: 0x04001627 RID: 5671
		private PolicyLevel _policyLevel;

		// Token: 0x04001628 RID: 5672
		private bool _declsec;

		// Token: 0x04001629 RID: 5673
		private bool _readOnly;

		// Token: 0x0400162A RID: 5674
		private bool[] _ignored;

		// Token: 0x0400162B RID: 5675
		private static object[] action = new object[] { (SecurityAction)0 };
	}
}
