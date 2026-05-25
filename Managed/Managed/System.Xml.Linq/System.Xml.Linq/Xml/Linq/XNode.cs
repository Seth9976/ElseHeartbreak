using System;
using System.Collections.Generic;
using System.IO;

namespace System.Xml.Linq
{
	/// <summary>Represents the abstract concept of a node (element, comment, document type, processing instruction, or text node) in the XML tree.  </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000018 RID: 24
	public abstract class XNode : XObject
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00005838 File Offset: 0x00003A38
		internal XNode()
		{
		}

		/// <summary>Compares two nodes to determine their relative XML document order.</summary>
		/// <returns>An int containing 0 if the nodes are equal; -1 if <paramref name="n1" /> is before <paramref name="n2" />; 1 if <paramref name="n1" /> is after <paramref name="n2" />.</returns>
		/// <param name="n1">First <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="n2">Second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <exception cref="T:System.InvalidOperationException">The two nodes do not share a common ancestor.</exception>
		// Token: 0x0600011A RID: 282 RVA: 0x00005858 File Offset: 0x00003A58
		public static int CompareDocumentOrder(XNode n1, XNode n2)
		{
			return XNode.order_comparer.Compare(n1, n2);
		}

		/// <summary>Compares the values of two nodes, including the values of all descendant nodes.</summary>
		/// <returns>true if the nodes are equal; otherwise false.</returns>
		/// <param name="n1">The first <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="n2">The second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		// Token: 0x0600011B RID: 283 RVA: 0x00005868 File Offset: 0x00003A68
		public static bool DeepEquals(XNode n1, XNode n2)
		{
			return XNode.eq_comparer.Equals(n1, n2);
		}

		/// <summary>Gets a comparer that can compare the relative position of two nodes.</summary>
		/// <returns>A <see cref="T:System.Xml.Linq.XNodeDocumentOrderComparer" /> that can compare the relative position of two nodes.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005878 File Offset: 0x00003A78
		public static XNodeDocumentOrderComparer DocumentOrderComparer
		{
			get
			{
				return XNode.order_comparer;
			}
		}

		/// <summary>Gets a comparer that can compare two nodes for value equality.</summary>
		/// <returns>A <see cref="T:System.Xml.Linq.XNodeEqualityComparer" /> that can compare two nodes for value equality.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005880 File Offset: 0x00003A80
		public static XNodeEqualityComparer EqualityComparer
		{
			get
			{
				return XNode.eq_comparer;
			}
		}

		/// <summary>Gets the previous sibling node of this node.</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNode" /> that contains the previous sibling node.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005888 File Offset: 0x00003A88
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00005890 File Offset: 0x00003A90
		public XNode PreviousNode
		{
			get
			{
				return this.previous;
			}
			internal set
			{
				this.previous = value;
			}
		}

		/// <summary>Gets the next sibling node of this node.</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNode" /> that contains the next sibling node.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000589C File Offset: 0x00003A9C
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000058A4 File Offset: 0x00003AA4
		public XNode NextNode
		{
			get
			{
				return this.next;
			}
			internal set
			{
				this.next = value;
			}
		}

		/// <summary>Returns the XML for this node, optionally disabling formatting.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the XML.</returns>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		// Token: 0x06000122 RID: 290 RVA: 0x000058B0 File Offset: 0x00003AB0
		public string ToString(SaveOptions options)
		{
			StringWriter stringWriter = new StringWriter();
			XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
			{
				ConformanceLevel = ConformanceLevel.Auto,
				Indent = (options != SaveOptions.DisableFormatting)
			});
			this.WriteTo(xmlWriter);
			xmlWriter.Close();
			return stringWriter.ToString();
		}

		/// <summary>Adds the specified content immediately after this node.</summary>
		/// <param name="content">A content object that contains simple content or a collection of content objects to be added after this node.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is null.</exception>
		// Token: 0x06000123 RID: 291 RVA: 0x000058F8 File Offset: 0x00003AF8
		public void AddAfterSelf(object content)
		{
			if (base.Parent == null)
			{
				throw new InvalidOperationException();
			}
			XNode xnode = this;
			XNode xnode2 = this.next;
			foreach (object obj in XUtil.ExpandArray(content))
			{
				if (!base.Owner.OnAddingObject(obj, true, xnode, false))
				{
					XNode xnode3 = XUtil.ToNode(obj);
					xnode3 = (XNode)XUtil.GetDetachedObject(xnode3);
					xnode3.SetOwner(base.Parent);
					xnode3.previous = xnode;
					xnode.next = xnode3;
					xnode3.next = xnode2;
					if (xnode2 != null)
					{
						xnode2.previous = xnode3;
					}
					else
					{
						base.Parent.LastNode = xnode3;
					}
					xnode = xnode3;
				}
			}
		}

		/// <summary>Adds the specified content immediately after this node.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is null.</exception>
		// Token: 0x06000124 RID: 292 RVA: 0x000059F0 File Offset: 0x00003BF0
		public void AddAfterSelf(params object[] content)
		{
			if (base.Parent == null)
			{
				throw new InvalidOperationException();
			}
			this.AddAfterSelf(content);
		}

		/// <summary>Adds the specified content immediately before this node.</summary>
		/// <param name="content">A content object that contains simple content or a collection of content objects to be added before this node.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is null.</exception>
		// Token: 0x06000125 RID: 293 RVA: 0x00005A0C File Offset: 0x00003C0C
		public void AddBeforeSelf(object content)
		{
			if (base.Parent == null)
			{
				throw new InvalidOperationException();
			}
			foreach (object obj in XUtil.ExpandArray(content))
			{
				if (!base.Owner.OnAddingObject(obj, true, this.previous, true))
				{
					XNode xnode = XUtil.ToNode(obj);
					xnode = (XNode)XUtil.GetDetachedObject(xnode);
					xnode.SetOwner(base.Parent);
					xnode.previous = this.previous;
					xnode.next = this;
					if (this.previous != null)
					{
						this.previous.next = xnode;
					}
					this.previous = xnode;
					if (base.Parent.FirstNode == this)
					{
						base.Parent.FirstNode = xnode;
					}
				}
			}
		}

		/// <summary>Adds the specified content immediately before this node.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is null.</exception>
		// Token: 0x06000126 RID: 294 RVA: 0x00005B0C File Offset: 0x00003D0C
		public void AddBeforeSelf(params object[] content)
		{
			if (base.Parent == null)
			{
				throw new InvalidOperationException();
			}
			this.AddBeforeSelf(content);
		}

		/// <summary>Creates an <see cref="T:System.Xml.Linq.XNode" /> from an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNode" /> that contains the node and its descendant nodes that were read from the reader. The runtime type of the node is determined by the node type (<see cref="P:System.Xml.Linq.XObject.NodeType" />) of the first node encountered in the reader.</returns>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" /> positioned at the node to read into this <see cref="T:System.Xml.Linq.XNode" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on a recognized node type.</exception>
		/// <exception cref="T:System.Xml.XmlException">The underlying <see cref="T:System.Xml.XmlReader" /> throws an exception.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000127 RID: 295 RVA: 0x00005B28 File Offset: 0x00003D28
		public static XNode ReadFrom(XmlReader r)
		{
			return XNode.ReadFrom(r, LoadOptions.None);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005B34 File Offset: 0x00003D34
		internal static XNode ReadFrom(XmlReader r, LoadOptions options)
		{
			switch (r.NodeType)
			{
			case XmlNodeType.Element:
				return XElement.LoadCore(r, options);
			case XmlNodeType.Text:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			{
				XText xtext = new XText(r.Value);
				xtext.FillLineInfoAndBaseUri(r, options);
				r.Read();
				return xtext;
			}
			case XmlNodeType.CDATA:
			{
				XCData xcdata = new XCData(r.Value);
				xcdata.FillLineInfoAndBaseUri(r, options);
				r.Read();
				return xcdata;
			}
			case XmlNodeType.ProcessingInstruction:
			{
				XProcessingInstruction xprocessingInstruction = new XProcessingInstruction(r.Name, r.Value);
				xprocessingInstruction.FillLineInfoAndBaseUri(r, options);
				r.Read();
				return xprocessingInstruction;
			}
			case XmlNodeType.Comment:
			{
				XComment xcomment = new XComment(r.Value);
				xcomment.FillLineInfoAndBaseUri(r, options);
				r.Read();
				return xcomment;
			}
			case XmlNodeType.DocumentType:
			{
				XDocumentType xdocumentType = new XDocumentType(r.Name, r.GetAttribute("PUBLIC"), r.GetAttribute("SYSTEM"), r.Value);
				xdocumentType.FillLineInfoAndBaseUri(r, options);
				r.Read();
				return xdocumentType;
			}
			}
			throw new InvalidOperationException(string.Format("Node type {0} is not supported", r.NodeType));
		}

		/// <summary>Removes this node from its parent.</summary>
		/// <exception cref="T:System.InvalidOperationException">The parent is null.</exception>
		// Token: 0x06000129 RID: 297 RVA: 0x00005C68 File Offset: 0x00003E68
		public void Remove()
		{
			if (base.Parent == null)
			{
				throw new InvalidOperationException("Parent is missing");
			}
			if (base.Parent.FirstNode == this)
			{
				base.Parent.FirstNode = this.next;
			}
			if (base.Parent.LastNode == this)
			{
				base.Parent.LastNode = this.previous;
			}
			if (this.previous != null)
			{
				this.previous.next = this.next;
			}
			if (this.next != null)
			{
				this.next.previous = this.previous;
			}
			this.previous = null;
			this.next = null;
			base.SetOwner(null);
		}

		/// <summary>Returns the indented XML for this node.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the indented XML.</returns>
		// Token: 0x0600012A RID: 298 RVA: 0x00005D1C File Offset: 0x00003F1C
		public override string ToString()
		{
			return this.ToString(SaveOptions.None);
		}

		/// <summary>Writes this node to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600012B RID: 299
		public abstract void WriteTo(XmlWriter w);

		/// <summary>Returns a collection of the ancestor elements of this node.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the ancestor elements of this node.</returns>
		// Token: 0x0600012C RID: 300 RVA: 0x00005D28 File Offset: 0x00003F28
		public IEnumerable<XElement> Ancestors()
		{
			for (XElement el = base.Parent; el != null; el = el.Parent)
			{
				yield return el;
			}
			yield break;
		}

		/// <summary>Returns a filtered collection of the ancestor elements of this node. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the ancestor elements of this node. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.The nodes in the returned collection are in reverse document order.This method uses deferred execution.</returns>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		// Token: 0x0600012D RID: 301 RVA: 0x00005D4C File Offset: 0x00003F4C
		public IEnumerable<XElement> Ancestors(XName name)
		{
			foreach (XElement el in this.Ancestors())
			{
				if (el.Name == name)
				{
					yield return el;
				}
			}
			yield break;
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlReader" /> for this node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> that can be used to read this node and its descendants.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600012E RID: 302 RVA: 0x00005D80 File Offset: 0x00003F80
		public XmlReader CreateReader()
		{
			return new XNodeReader(this);
		}

		/// <summary>Returns a collection of the sibling elements after this node, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the sibling elements after this node, in document order.</returns>
		// Token: 0x0600012F RID: 303 RVA: 0x00005D88 File Offset: 0x00003F88
		public IEnumerable<XElement> ElementsAfterSelf()
		{
			foreach (XNode i in this.NodesAfterSelf())
			{
				if (i is XElement)
				{
					yield return (XElement)i;
				}
			}
			yield break;
		}

		/// <summary>Returns a filtered collection of the sibling elements after this node, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the sibling elements after this node, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		// Token: 0x06000130 RID: 304 RVA: 0x00005DAC File Offset: 0x00003FAC
		public IEnumerable<XElement> ElementsAfterSelf(XName name)
		{
			foreach (XElement el in this.ElementsAfterSelf())
			{
				if (el.Name == name)
				{
					yield return el;
				}
			}
			yield break;
		}

		/// <summary>Returns a collection of the sibling elements before this node, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the sibling elements before this node, in document order.</returns>
		// Token: 0x06000131 RID: 305 RVA: 0x00005DE0 File Offset: 0x00003FE0
		public IEnumerable<XElement> ElementsBeforeSelf()
		{
			foreach (XNode i in this.NodesBeforeSelf())
			{
				if (i is XElement)
				{
					yield return (XElement)i;
				}
			}
			yield break;
		}

		/// <summary>Returns a filtered collection of the sibling elements before this node, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the sibling elements before this node, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		// Token: 0x06000132 RID: 306 RVA: 0x00005E04 File Offset: 0x00004004
		public IEnumerable<XElement> ElementsBeforeSelf(XName name)
		{
			foreach (XElement el in this.ElementsBeforeSelf())
			{
				if (el.Name == name)
				{
					yield return el;
				}
			}
			yield break;
		}

		/// <summary>Determines if the current node appears after a specified node in terms of document order.</summary>
		/// <returns>true if this node appears after the specified node; otherwise false.</returns>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> to compare for document order.</param>
		// Token: 0x06000133 RID: 307 RVA: 0x00005E38 File Offset: 0x00004038
		public bool IsAfter(XNode other)
		{
			return XNode.DocumentOrderComparer.Compare(this, other) > 0;
		}

		/// <summary>Determines if the current node appears before a specified node in terms of document order.</summary>
		/// <returns>true if this node appears before the specified node; otherwise false.</returns>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> to compare for document order.</param>
		// Token: 0x06000134 RID: 308 RVA: 0x00005E4C File Offset: 0x0000404C
		public bool IsBefore(XNode other)
		{
			return XNode.DocumentOrderComparer.Compare(this, other) < 0;
		}

		/// <summary>Returns a collection of the sibling nodes after this node, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> of the sibling nodes after this node, in document order.</returns>
		// Token: 0x06000135 RID: 309 RVA: 0x00005E60 File Offset: 0x00004060
		public IEnumerable<XNode> NodesAfterSelf()
		{
			if (base.Parent == null)
			{
				yield break;
			}
			for (XNode i = this.NextNode; i != null; i = i.NextNode)
			{
				yield return i;
			}
			yield break;
		}

		/// <summary>Returns a collection of the sibling nodes before this node, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> of the sibling nodes before this node, in document order.</returns>
		// Token: 0x06000136 RID: 310 RVA: 0x00005E84 File Offset: 0x00004084
		public IEnumerable<XNode> NodesBeforeSelf()
		{
			for (XNode i = base.Parent.FirstNode; i != this; i = i.NextNode)
			{
				yield return i;
			}
			yield break;
		}

		/// <summary>Replaces this node with the specified content.</summary>
		/// <param name="content">Content that replaces this node.</param>
		// Token: 0x06000137 RID: 311 RVA: 0x00005EA8 File Offset: 0x000040A8
		public void ReplaceWith(object item)
		{
			this.AddAfterSelf(item);
			this.Remove();
		}

		/// <summary>Replaces this node with the specified content.</summary>
		/// <param name="content">A parameter list of the new content.</param>
		// Token: 0x06000138 RID: 312 RVA: 0x00005EB8 File Offset: 0x000040B8
		public void ReplaceWith(params object[] items)
		{
			this.AddAfterSelf(items);
			this.Remove();
		}

		// Token: 0x04000046 RID: 70
		private static XNodeEqualityComparer eq_comparer = new XNodeEqualityComparer();

		// Token: 0x04000047 RID: 71
		private static XNodeDocumentOrderComparer order_comparer = new XNodeDocumentOrderComparer();

		// Token: 0x04000048 RID: 72
		private XNode previous;

		// Token: 0x04000049 RID: 73
		private XNode next;
	}
}
