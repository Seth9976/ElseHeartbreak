using System;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML processing instruction. </summary>
	// Token: 0x02000022 RID: 34
	public class XProcessingInstruction : XNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XProcessingInstruction" /> class. </summary>
		/// <param name="target">A <see cref="T:System.String" /> containing the target application for this <see cref="T:System.Xml.Linq.XProcessingInstruction" />.</param>
		/// <param name="data">The string data for this <see cref="T:System.Xml.Linq.XProcessingInstruction" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="target" /> or <paramref name="data" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> does not follow the constraints of an XML name.</exception>
		// Token: 0x060001C8 RID: 456 RVA: 0x00008B64 File Offset: 0x00006D64
		public XProcessingInstruction(string name, string data)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.name = name;
			this.data = data;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XProcessingInstruction" /> class. </summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XProcessingInstruction" /> node to copy from.</param>
		// Token: 0x060001C9 RID: 457 RVA: 0x00008BA8 File Offset: 0x00006DA8
		public XProcessingInstruction(XProcessingInstruction other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.name = other.name;
			this.data = other.data;
		}

		/// <summary>Gets or sets the string value of this processing instruction.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the string value of this processing instruction.</returns>
		/// <exception cref="T:System.ArgumentNullException">The string <paramref name="value" /> is null.</exception>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00008BDC File Offset: 0x00006DDC
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00008BE4 File Offset: 0x00006DE4
		public string Data
		{
			get
			{
				return this.data;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.data = value;
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XProcessingInstruction" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.ProcessingInstruction" />.</returns>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00008C00 File Offset: 0x00006E00
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.ProcessingInstruction;
			}
		}

		/// <summary>Gets or sets a string containing the target application for this processing instruction.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the target application for this processing instruction.</returns>
		/// <exception cref="T:System.ArgumentNullException">The string <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> does not follow the constraints of an XML name.</exception>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008C04 File Offset: 0x00006E04
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00008C0C File Offset: 0x00006E0C
		public string Target
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.name = value;
			}
		}

		/// <summary>Writes this processing instruction to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to write this processing instruction to.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060001CF RID: 463 RVA: 0x00008C28 File Offset: 0x00006E28
		public override void WriteTo(XmlWriter w)
		{
			w.WriteProcessingInstruction(this.name, this.data);
		}

		// Token: 0x04000076 RID: 118
		private string name;

		// Token: 0x04000077 RID: 119
		private string data;
	}
}
