using System;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML comment. </summary>
	// Token: 0x0200000F RID: 15
	public class XComment : XNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XComment" /> class with the specified string content. </summary>
		/// <param name="value">A string that contains the contents of the new <see cref="T:System.Xml.Linq.XComment" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		// Token: 0x0600004B RID: 75 RVA: 0x00002EA0 File Offset: 0x000010A0
		public XComment(string value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XComment" /> class from an existing comment node. </summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XComment" /> node to copy from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="other" /> parameter is null.</exception>
		// Token: 0x0600004C RID: 76 RVA: 0x00002EB0 File Offset: 0x000010B0
		public XComment(XComment other)
		{
			this.value = other.value;
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XComment" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Comment" />.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002EC4 File Offset: 0x000010C4
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Comment;
			}
		}

		/// <summary>Gets or sets the string value of this comment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the string value of this comment.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is null.</exception>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002EC8 File Offset: 0x000010C8
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002ED0 File Offset: 0x000010D0
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		/// <summary>Write this comment to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000050 RID: 80 RVA: 0x00002EDC File Offset: 0x000010DC
		public override void WriteTo(XmlWriter w)
		{
			w.WriteComment(this.value);
		}

		// Token: 0x0400002C RID: 44
		private string value;
	}
}
