using System;
using System.Runtime.Serialization;

namespace System.Xml.Linq
{
	/// <summary>Represents a name of an XML element or attribute. </summary>
	// Token: 0x02000016 RID: 22
	[Serializable]
	public sealed class XName : ISerializable, IEquatable<XName>
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x0000535C File Offset: 0x0000355C
		private XName(SerializationInfo info, StreamingContext context)
		{
			string @string = info.GetString("name");
			string text;
			string text2;
			XName.ExpandName(@string, out text, out text2);
			this.local = text;
			this.ns = XNamespace.Get(text2);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005398 File Offset: 0x00003598
		internal XName(string local, XNamespace ns)
		{
			this.local = XmlConvert.VerifyNCName(local);
			this.ns = ns;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000053B4 File Offset: 0x000035B4
		bool IEquatable<XName>.Equals(XName other)
		{
			return this == other;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data required to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x060000FB RID: 251 RVA: 0x000053C0 File Offset: 0x000035C0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("name", this.ToString());
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000053F0 File Offset: 0x000035F0
		private static Exception ErrorInvalidExpandedName()
		{
			return new ArgumentException("Invalid expanded name.");
		}

		/// <summary>Gets the local (unqualified) part of the name.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the local (unqualified) part of the name.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000053FC File Offset: 0x000035FC
		public string LocalName
		{
			get
			{
				return this.local;
			}
		}

		/// <summary>Gets the namespace part of the fully qualified name.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> that contains the namespace part of the name.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005404 File Offset: 0x00003604
		public XNamespace Namespace
		{
			get
			{
				return this.ns;
			}
		}

		/// <summary>Returns the URI of the <see cref="T:System.Xml.Linq.XNamespace" /> for this <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>The URI of the <see cref="T:System.Xml.Linq.XNamespace" /> for this <see cref="T:System.Xml.Linq.XName" />.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000540C File Offset: 0x0000360C
		public string NamespaceName
		{
			get
			{
				return this.ns.NamespaceName;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Xml.Linq.XName" /> is equal to this <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Xml.Linq.XName" /> is equal to the current <see cref="T:System.Xml.Linq.XName" />; otherwise false.</returns>
		/// <param name="obj">The <see cref="T:System.Xml.Linq.XName" /> to compare to the current <see cref="T:System.Xml.Linq.XName" />.</param>
		// Token: 0x06000100 RID: 256 RVA: 0x0000541C File Offset: 0x0000361C
		public override bool Equals(object obj)
		{
			XName xname = obj as XName;
			return xname != null && this == xname;
		}

		/// <summary>Gets an <see cref="T:System.Xml.Linq.XName" /> object from an expanded name.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> object constructed from the expanded name.</returns>
		/// <param name="expandedName">A <see cref="T:System.String" /> that contains an expanded XML name in the format {namespace}localname.</param>
		// Token: 0x06000101 RID: 257 RVA: 0x00005448 File Offset: 0x00003648
		public static XName Get(string expandedName)
		{
			string text;
			string text2;
			XName.ExpandName(expandedName, out text, out text2);
			return XName.Get(text, text2);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005468 File Offset: 0x00003668
		private static void ExpandName(string expandedName, out string local, out string ns)
		{
			if (expandedName == null)
			{
				throw new ArgumentNullException("expandedName");
			}
			ns = null;
			local = null;
			if (expandedName.Length == 0)
			{
				throw XName.ErrorInvalidExpandedName();
			}
			if (expandedName[0] == '{')
			{
				for (int i = 1; i < expandedName.Length; i++)
				{
					if (expandedName[i] == '}')
					{
						ns = expandedName.Substring(1, i - 1);
					}
				}
				if (string.IsNullOrEmpty(ns))
				{
					throw XName.ErrorInvalidExpandedName();
				}
				if (expandedName.Length == ns.Length + 2)
				{
					throw XName.ErrorInvalidExpandedName();
				}
				local = expandedName.Substring(ns.Length + 2);
			}
			else
			{
				local = expandedName;
				ns = string.Empty;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.Linq.XName" /> object from a local name and a namespace.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> object created from the specified local name and namespace.</returns>
		/// <param name="localName">A local (unqualified) name.</param>
		/// <param name="namespaceName">An XML namespace.</param>
		// Token: 0x06000103 RID: 259 RVA: 0x00005528 File Offset: 0x00003728
		public static XName Get(string localName, string namespaceName)
		{
			return XNamespace.Get(namespaceName).GetName(localName);
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the hash code for the <see cref="T:System.Xml.Linq.XName" />.</returns>
		// Token: 0x06000104 RID: 260 RVA: 0x00005538 File Offset: 0x00003738
		public override int GetHashCode()
		{
			return this.local.GetHashCode() ^ this.ns.GetHashCode();
		}

		/// <summary>Returns the expanded XML name in the format {namespace}localname.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the expanded XML name in the format {namespace}localname.</returns>
		// Token: 0x06000105 RID: 261 RVA: 0x00005554 File Offset: 0x00003754
		public override string ToString()
		{
			if (this.ns == XNamespace.None)
			{
				return this.local;
			}
			return "{" + this.ns.NamespaceName + "}" + this.local;
		}

		/// <summary>Returns a value indicating whether two instances of <see cref="T:System.Xml.Linq.XName" /> are equal.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise false.</returns>
		/// <param name="left">The first <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		// Token: 0x06000106 RID: 262 RVA: 0x000055A0 File Offset: 0x000037A0
		public static bool operator ==(XName n1, XName n2)
		{
			if (n1 == null)
			{
				return n2 == null;
			}
			return n2 != null && (object.ReferenceEquals(n1, n2) || (n1.local == n2.local && n1.ns == n2.ns));
		}

		/// <summary>Converts a string formatted as an expanded XML name (that is,{namespace}localname) to an <see cref="T:System.Xml.Linq.XName" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> object constructed from the expanded name.</returns>
		/// <param name="expandedName">A string that contains an expanded XML name in the format {namespace}localname.</param>
		// Token: 0x06000107 RID: 263 RVA: 0x000055FC File Offset: 0x000037FC
		public static implicit operator XName(string s)
		{
			return (s != null) ? XName.Get(s) : null;
		}

		/// <summary>Returns a value indicating whether two instances of <see cref="T:System.Xml.Linq.XName" /> are not equal.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise false.</returns>
		/// <param name="left">The first <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Xml.Linq.XName" /> to compare.</param>
		// Token: 0x06000108 RID: 264 RVA: 0x00005610 File Offset: 0x00003810
		public static bool operator !=(XName n1, XName n2)
		{
			return !(n1 == n2);
		}

		// Token: 0x0400003E RID: 62
		private string local;

		// Token: 0x0400003F RID: 63
		private XNamespace ns;
	}
}
