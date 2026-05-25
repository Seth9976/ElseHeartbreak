using System;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML namespace. This class cannot be inherited. </summary>
	// Token: 0x02000017 RID: 23
	public sealed class XNamespace
	{
		// Token: 0x06000109 RID: 265 RVA: 0x0000561C File Offset: 0x0000381C
		private XNamespace(string namespaceName)
		{
			if (namespaceName == null)
			{
				throw new ArgumentNullException("namespaceName");
			}
			this.uri = namespaceName;
		}

		/// <summary>Gets the <see cref="T:System.Xml.Linq.XNamespace" /> object that corresponds to no namespace.</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNamespace" /> that corresponds to no namespace.</returns>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005678 File Offset: 0x00003878
		public static XNamespace None
		{
			get
			{
				return XNamespace.blank;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Linq.XNamespace" /> object that corresponds to the XML URI (http://www.w3.org/XML/1998/namespace).</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNamespace" /> that corresponds to the XML URI (http://www.w3.org/XML/1998/namespace).</returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005680 File Offset: 0x00003880
		public static XNamespace Xml
		{
			get
			{
				return XNamespace.xml;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.Linq.XNamespace" /> object that corresponds to the xmlns URI (http://www.w3.org/2000/xmlns/).</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNamespace" /> that corresponds to the xmlns URI (http://www.w3.org/2000/xmlns/).</returns>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005688 File Offset: 0x00003888
		public static XNamespace Xmlns
		{
			get
			{
				return XNamespace.xmlns;
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.Linq.XNamespace" /> for the specified Uniform Resource Identifier (URI).</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> created from the specified URI.</returns>
		/// <param name="namespaceName">A <see cref="T:System.String" /> that contains a namespace URI.</param>
		// Token: 0x0600010E RID: 270 RVA: 0x00005690 File Offset: 0x00003890
		public static XNamespace Get(string uri)
		{
			Dictionary<string, XNamespace> dictionary = XNamespace.nstable;
			XNamespace xnamespace2;
			lock (dictionary)
			{
				XNamespace xnamespace;
				if (!XNamespace.nstable.TryGetValue(uri, out xnamespace))
				{
					xnamespace = new XNamespace(uri);
					XNamespace.nstable[uri] = xnamespace;
				}
				xnamespace2 = xnamespace;
			}
			return xnamespace2;
		}

		/// <summary>Returns an <see cref="T:System.Xml.Linq.XName" /> object created from this <see cref="T:System.Xml.Linq.XNamespace" /> and the specified local name.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> created from this <see cref="T:System.Xml.Linq.XNamespace" /> and the specified local name.</returns>
		/// <param name="localName">A <see cref="T:System.String" /> that contains a local name.</param>
		// Token: 0x0600010F RID: 271 RVA: 0x00005700 File Offset: 0x00003900
		public XName GetName(string localName)
		{
			if (this.table == null)
			{
				this.table = new Dictionary<string, XName>();
			}
			Dictionary<string, XName> dictionary = this.table;
			XName xname2;
			lock (dictionary)
			{
				XName xname;
				if (!this.table.TryGetValue(localName, out xname))
				{
					xname = new XName(localName, this);
					this.table[localName] = xname;
				}
				xname2 = xname;
			}
			return xname2;
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of this namespace.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the URI of the namespace.</returns>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005788 File Offset: 0x00003988
		public string NamespaceName
		{
			get
			{
				return this.uri;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Xml.Linq.XNamespace" /> is equal to the current <see cref="T:System.Xml.Linq.XNamespace" />.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> that indicates whether the specified <see cref="T:System.Xml.Linq.XNamespace" /> is equal to the current <see cref="T:System.Xml.Linq.XNamespace" />.</returns>
		/// <param name="obj">The <see cref="T:System.Xml.Linq.XNamespace" /> to compare to the current <see cref="T:System.Xml.Linq.XNamespace" />.</param>
		// Token: 0x06000111 RID: 273 RVA: 0x00005790 File Offset: 0x00003990
		public override bool Equals(object other)
		{
			if (object.ReferenceEquals(this, other))
			{
				return true;
			}
			XNamespace xnamespace = other as XNamespace;
			return xnamespace != null && this.uri == xnamespace.uri;
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Xml.Linq.XNamespace" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the hash code for the <see cref="T:System.Xml.Linq.XNamespace" />.</returns>
		// Token: 0x06000112 RID: 274 RVA: 0x000057D4 File Offset: 0x000039D4
		public override int GetHashCode()
		{
			return this.uri.GetHashCode();
		}

		/// <summary>Returns the URI of this <see cref="T:System.Xml.Linq.XNamespace" />.</summary>
		/// <returns>The URI of this <see cref="T:System.Xml.Linq.XNamespace" />.</returns>
		// Token: 0x06000113 RID: 275 RVA: 0x000057E4 File Offset: 0x000039E4
		public override string ToString()
		{
			return this.uri;
		}

		/// <summary>Returns a value indicating whether two instances of <see cref="T:System.Xml.Linq.XNamespace" /> are equal.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> that indicates whether <paramref name="left" /> and <paramref name="right" /> are equal.</returns>
		/// <param name="left">The first <see cref="T:System.Xml.Linq.XNamespace" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Xml.Linq.XNamespace" /> to compare.</param>
		// Token: 0x06000114 RID: 276 RVA: 0x000057EC File Offset: 0x000039EC
		public static bool operator ==(XNamespace o1, XNamespace o2)
		{
			return (o1 == null) ? (o2 == null) : o1.Equals(o2);
		}

		/// <summary>Returns a value indicating whether two instances of <see cref="T:System.Xml.Linq.XNamespace" /> are not equal.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> that indicates whether <paramref name="left" /> and <paramref name="right" /> are not equal.</returns>
		/// <param name="left">The first <see cref="T:System.Xml.Linq.XNamespace" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Xml.Linq.XNamespace" /> to compare.</param>
		// Token: 0x06000115 RID: 277 RVA: 0x00005804 File Offset: 0x00003A04
		public static bool operator !=(XNamespace o1, XNamespace o2)
		{
			return !(o1 == o2);
		}

		/// <summary>Combines an <see cref="T:System.Xml.Linq.XNamespace" /> object with a local name to create an <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>The new <see cref="T:System.Xml.Linq.XName" /> constructed from the namespace and local name.</returns>
		/// <param name="ns">An <see cref="T:System.Xml.Linq.XNamespace" /> that contains the namespace.</param>
		/// <param name="localName">A <see cref="T:System.String" /> that contains the local name.</param>
		// Token: 0x06000116 RID: 278 RVA: 0x00005810 File Offset: 0x00003A10
		public static XName operator +(XNamespace ns, string localName)
		{
			return new XName(localName, ns.NamespaceName);
		}

		/// <summary>Converts a string containing a Uniform Resource Identifier (URI) to an <see cref="T:System.Xml.Linq.XNamespace" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNamespace" /> constructed from the URI string.</returns>
		/// <param name="namespaceName">A <see cref="T:System.String" /> that contains the namespace URI.</param>
		// Token: 0x06000117 RID: 279 RVA: 0x00005824 File Offset: 0x00003A24
		public static implicit operator XNamespace(string s)
		{
			return (s == null) ? null : XNamespace.Get(s);
		}

		// Token: 0x04000040 RID: 64
		private static readonly XNamespace blank = XNamespace.Get(string.Empty);

		// Token: 0x04000041 RID: 65
		private static readonly XNamespace xml = XNamespace.Get("http://www.w3.org/XML/1998/namespace");

		// Token: 0x04000042 RID: 66
		private static readonly XNamespace xmlns = XNamespace.Get("http://www.w3.org/2000/xmlns/");

		// Token: 0x04000043 RID: 67
		private static Dictionary<string, XNamespace> nstable = new Dictionary<string, XNamespace>();

		// Token: 0x04000044 RID: 68
		private string uri;

		// Token: 0x04000045 RID: 69
		private Dictionary<string, XName> table;
	}
}
