using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	/// <summary>Represents a node that can contain other nodes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000010 RID: 16
	public abstract class XContainer : XNode
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002EEC File Offset: 0x000010EC
		internal XContainer()
		{
		}

		/// <summary>Get the first child node of this node.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNode" /> containing the first child node of the <see cref="T:System.Xml.Linq.XContainer" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002EF4 File Offset: 0x000010F4
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002EFC File Offset: 0x000010FC
		public XNode FirstNode
		{
			get
			{
				return this.first;
			}
			internal set
			{
				this.first = value;
			}
		}

		/// <summary>Get the last child node of this node.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNode" /> containing the last child node of the <see cref="T:System.Xml.Linq.XContainer" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002F08 File Offset: 0x00001108
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002F10 File Offset: 0x00001110
		public XNode LastNode
		{
			get
			{
				return this.last;
			}
			internal set
			{
				this.last = value;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F1C File Offset: 0x0000111C
		private void CheckChildType(object o, bool addFirst)
		{
			if (o == null || o is string || o is XNode)
			{
				return;
			}
			if (o is IEnumerable)
			{
				foreach (object obj in ((IEnumerable)o))
				{
					this.CheckChildType(obj, addFirst);
				}
				return;
			}
			throw new ArgumentException("Invalid child type: " + o.GetType());
		}

		/// <summary>Adds the specified content as children of this <see cref="T:System.Xml.Linq.XContainer" />.</summary>
		/// <param name="content">A content object containing simple content or a collection of content objects to be added.</param>
		// Token: 0x06000057 RID: 87 RVA: 0x00002FC8 File Offset: 0x000011C8
		public void Add(object content)
		{
			if (content == null)
			{
				return;
			}
			foreach (object obj in XUtil.ExpandArray(content))
			{
				if (!this.OnAddingObject(obj, false, this.last, false))
				{
					this.AddNode(XUtil.ToNode(obj));
				}
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003054 File Offset: 0x00001254
		private void AddNode(XNode n)
		{
			this.CheckChildType(n, false);
			n = (XNode)XUtil.GetDetachedObject(n);
			n.SetOwner(this);
			if (this.first == null)
			{
				this.last = (this.first = n);
			}
			else
			{
				this.last.NextNode = n;
				n.PreviousNode = this.last;
				this.last = n;
			}
		}

		/// <summary>Adds the specified content as children of this <see cref="T:System.Xml.Linq.XContainer" />.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		// Token: 0x06000059 RID: 89 RVA: 0x000030BC File Offset: 0x000012BC
		public void Add(params object[] content)
		{
			if (content == null)
			{
				return;
			}
			foreach (object obj in XUtil.ExpandArray(content))
			{
				this.Add(obj);
			}
		}

		/// <summary>Adds the specified content as the first children of this document or element.</summary>
		/// <param name="content">A content object containing simple content or a collection of content objects to be added.</param>
		// Token: 0x0600005A RID: 90 RVA: 0x00003130 File Offset: 0x00001330
		public void AddFirst(object content)
		{
			if (this.first == null)
			{
				this.Add(content);
			}
			else
			{
				this.first.AddBeforeSelf(XUtil.ExpandArray(content));
			}
		}

		/// <summary>Adds the specified content as the first children of this document or element.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is null.</exception>
		// Token: 0x0600005B RID: 91 RVA: 0x00003168 File Offset: 0x00001368
		public void AddFirst(params object[] content)
		{
			if (content == null)
			{
				return;
			}
			if (this.first == null)
			{
				this.Add(content);
			}
			else
			{
				foreach (object obj in XUtil.ExpandArray(content))
				{
					if (!this.OnAddingObject(obj, false, this.first.PreviousNode, true))
					{
						this.first.AddBeforeSelf(obj);
					}
				}
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003210 File Offset: 0x00001410
		internal virtual bool OnAddingObject(object o, bool rejectAttribute, XNode refNode, bool addFirst)
		{
			return false;
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlWriter" /> that can be used to add nodes to the <see cref="T:System.Xml.Linq.XContainer" />.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlWriter" /> that is ready to have content written to it.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600005D RID: 93 RVA: 0x00003214 File Offset: 0x00001414
		public XmlWriter CreateWriter()
		{
			return new XNodeWriter(this);
		}

		/// <summary>Returns a collection of the child nodes of this element or document, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> containing the contents of this <see cref="T:System.Xml.Linq.XContainer" />, in document order.</returns>
		// Token: 0x0600005E RID: 94 RVA: 0x0000321C File Offset: 0x0000141C
		public IEnumerable<XNode> Nodes()
		{
			XNode next;
			for (XNode i = this.FirstNode; i != null; i = next)
			{
				next = i.NextNode;
				yield return i;
			}
			yield break;
		}

		/// <summary>Returns a collection of the descendant nodes for this document or element, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> containing the descendant nodes of the <see cref="T:System.Xml.Linq.XContainer" />, in document order.</returns>
		// Token: 0x0600005F RID: 95 RVA: 0x00003240 File Offset: 0x00001440
		public IEnumerable<XNode> DescendantNodes()
		{
			foreach (XNode i in this.Nodes())
			{
				yield return i;
				XContainer c = i as XContainer;
				if (c != null)
				{
					foreach (XNode d in c.DescendantNodes())
					{
						yield return d;
					}
				}
			}
			yield break;
		}

		/// <summary>Returns a collection of the descendant elements for this document or element, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> containing the descendant elements of the <see cref="T:System.Xml.Linq.XContainer" />.</returns>
		// Token: 0x06000060 RID: 96 RVA: 0x00003264 File Offset: 0x00001464
		public IEnumerable<XElement> Descendants()
		{
			foreach (XNode i in this.DescendantNodes())
			{
				XElement el = i as XElement;
				if (el != null)
				{
					yield return el;
				}
			}
			yield break;
		}

		/// <summary>Returns a filtered collection of the descendant elements for this document or element, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> containing the descendant elements of the <see cref="T:System.Xml.Linq.XContainer" /> that match the specified <see cref="T:System.Xml.Linq.XName" />.</returns>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		// Token: 0x06000061 RID: 97 RVA: 0x00003288 File Offset: 0x00001488
		public IEnumerable<XElement> Descendants(XName name)
		{
			foreach (XElement el in this.Descendants())
			{
				if (el.Name == name)
				{
					yield return el;
				}
			}
			yield break;
		}

		/// <summary>Returns a collection of the child elements of this element or document, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> containing the child elements of this <see cref="T:System.Xml.Linq.XContainer" />, in document order.</returns>
		// Token: 0x06000062 RID: 98 RVA: 0x000032BC File Offset: 0x000014BC
		public IEnumerable<XElement> Elements()
		{
			foreach (XNode i in this.Nodes())
			{
				XElement el = i as XElement;
				if (el != null)
				{
					yield return el;
				}
			}
			yield break;
		}

		/// <summary>Returns a filtered collection of the child elements of this element or document, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> containing the children of the <see cref="T:System.Xml.Linq.XContainer" /> that have a matching <see cref="T:System.Xml.Linq.XName" />, in document order.</returns>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		// Token: 0x06000063 RID: 99 RVA: 0x000032E0 File Offset: 0x000014E0
		public IEnumerable<XElement> Elements(XName name)
		{
			foreach (XElement el in this.Elements())
			{
				if (el.Name == name)
				{
					yield return el;
				}
			}
			yield break;
		}

		/// <summary>Gets the first (in document order) child element with the specified <see cref="T:System.Xml.Linq.XName" />.</summary>
		/// <returns>A <see cref="T:System.Xml.Linq.XElement" /> that matches the specified <see cref="T:System.Xml.Linq.XName" />, or null.</returns>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		// Token: 0x06000064 RID: 100 RVA: 0x00003314 File Offset: 0x00001514
		public XElement Element(XName name)
		{
			foreach (XElement xelement in this.Elements())
			{
				if (xelement.Name == name)
				{
					return xelement;
				}
			}
			return null;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000338C File Offset: 0x0000158C
		internal void ReadContentFrom(XmlReader reader, LoadOptions options)
		{
			while (!reader.EOF)
			{
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
				this.Add(XNode.ReadFrom(reader, options));
			}
		}

		/// <summary>Removes the child nodes from this document or element.</summary>
		// Token: 0x06000066 RID: 102 RVA: 0x000033C0 File Offset: 0x000015C0
		public void RemoveNodes()
		{
			foreach (XNode xnode in this.Nodes())
			{
				xnode.Remove();
			}
		}

		/// <summary>Replaces the children nodes of this document or element with the specified content.</summary>
		/// <param name="content">A content object containing simple content or a collection of content objects that replace the children nodes.</param>
		// Token: 0x06000067 RID: 103 RVA: 0x00003424 File Offset: 0x00001624
		public void ReplaceNodes(object content)
		{
			XNode firstNode = this.FirstNode;
			XNode lastNode = this.LastNode;
			this.Add(content);
			if (firstNode == null)
			{
				return;
			}
			XNode nextNode;
			for (XNode xnode = firstNode; xnode != lastNode; xnode = nextNode)
			{
				nextNode = xnode.NextNode;
				xnode.Remove();
			}
			lastNode.Remove();
		}

		/// <summary>Replaces the children nodes of this document or element with the specified content.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		// Token: 0x06000068 RID: 104 RVA: 0x00003470 File Offset: 0x00001670
		public void ReplaceNodes(params object[] content)
		{
			this.ReplaceNodes(content);
		}

		// Token: 0x0400002D RID: 45
		private XNode first;

		// Token: 0x0400002E RID: 46
		private XNode last;
	}
}
