using System;

namespace System.Xml.Linq
{
	/// <summary>Represents a text node. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000024 RID: 36
	public class XText : XNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XText" /> class. </summary>
		/// <param name="value">The <see cref="T:System.String" /> that contains the value of the <see cref="T:System.Xml.Linq.XText" /> node.</param>
		// Token: 0x060001E2 RID: 482 RVA: 0x00008FE4 File Offset: 0x000071E4
		public XText(string value)
		{
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XText" /> class from another <see cref="T:System.Xml.Linq.XText" /> object.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XText" /> node to copy from.</param>
		// Token: 0x060001E3 RID: 483 RVA: 0x00008FF4 File Offset: 0x000071F4
		public XText(XText other)
		{
			this.value = other.value;
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XText" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Text" />.</returns>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00009008 File Offset: 0x00007208
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Text;
			}
		}

		/// <summary>Gets or sets the value of this node.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value of this node.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000900C File Offset: 0x0000720C
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00009014 File Offset: 0x00007214
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.value = value;
			}
		}

		/// <summary>Writes this node to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001E7 RID: 487 RVA: 0x00009030 File Offset: 0x00007230
		public override void WriteTo(XmlWriter w)
		{
			w.WriteString(this.value);
		}

		// Token: 0x0400007A RID: 122
		private string value;
	}
}
